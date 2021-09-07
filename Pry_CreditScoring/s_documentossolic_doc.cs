using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using wfcModel;

namespace Docsis_Application
{
    public partial class s_documentossolic_doc : Form
    {

        #region
        const int WM_SYSCOMMAND = 0x112;
        const int MOUSE_MOVE = 0xF012;
        //
        // Declaraciones del API
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        //
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        //
        #endregion

        #region
        private const int CS_DROPSHADOW = 0x00020000;
        protected override CreateParams CreateParams
        {
            get
            {
                // add the drop shadow flag for automatically drawing
                // a drop shadow around the form
                CreateParams cp = base.CreateParams;

                
                if (s_login.ventana_con_borde)
                {
                    cp.Style |= 0x40000 | CS_DROPSHADOW;
                }
                else
                {
                    cp.ClassStyle |= CS_DROPSHADOW;
                }
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
        #endregion

        int vl_no_solicitud;
        DataTable mytable = new DataTable();
        public DataAccess da;
        public s_documentossolic_doc(DataAccess da, int pa_no_solicitud)
        {
            InitializeComponent();
                        
            mytable.Columns.Add("recibido_b");
            mytable.Columns.Add("documento_id");
            mytable.Columns.Add("nombre_doc");
            mytable.Columns.Add("tipo_exigencia");
            mytable.Columns.Add("no_archivo");
            

            vl_no_solicitud = pa_no_solicitud;
            this.da = da;
            p_popular_lista_documentos();
        }
        private void p_popular_lista_documentos()
        {
            DataTable dt = da.ObtenerInfoSolicitud(vl_no_solicitud);
            int vl_workflow = Convert.ToInt16(dt.Rows[0]["workflow_id"].ToString());
            int vl_codigo_sub_aplicacion = Convert.ToInt16(dt.Rows[0]["codigo_sub_aplicacion"].ToString());
            string vl_sql = @"select td.documento_id,descripcion nombre_doc,decode(tipo_exigencia,'R','Requerido','O','Opcional') tipo_exigencia
                                from DCS_WF_DOCUMENTOS_WORKFLOW dw,
                                     DCS_WF_TIPO_DOCUMENTOS td
                               where dw.DOCUMENTO_ID=td.DOCUMENTO_ID
                                 and dw.workflow_id=:pa_workflow_id
                                 and dw.codigo_sub_aplicacion=:pa_codigo_sub_aplicacion
                                 and dw.activo='S'
                                Order by sigla_doc";
            if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
            {
                DocSys.connOracle.Open();
            }
            OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
            cmd2.CommandType = CommandType.Text;
            //───────────────────
            OracleParameter pa_workflow_id = new OracleParameter("pa_workflow_id", OracleType.Int32);
            cmd2.Parameters.Add(pa_workflow_id);
            pa_workflow_id.Direction = ParameterDirection.Input;
            pa_workflow_id.Value = vl_workflow;
            //───────────────────
            OracleParameter pa_codigo_sub_aplicacion = new OracleParameter("pa_codigo_sub_aplicacion", OracleType.Int32);
            cmd2.Parameters.Add(pa_codigo_sub_aplicacion);
            pa_codigo_sub_aplicacion.Direction = ParameterDirection.Input;
            pa_codigo_sub_aplicacion.Value = vl_codigo_sub_aplicacion;
            //───────────────────
            mytable.Clear();
            OracleDataReader dr = cmd2.ExecuteReader();

            int vl_tipo_doc = 0;
            while (dr.Read())
            {                
                vl_tipo_doc = int.Parse(dr["documento_id"].ToString());  
              
                mytable.Rows.Add(DocSys.p_verificar_doc_existe(vl_no_solicitud,vl_tipo_doc),
                               dr["documento_id"].ToString(),
                               dr["nombre_doc"].ToString(),
                               dr["tipo_exigencia"].ToString(),
                               DocSys.p_obtener_no_archivo(vl_no_solicitud,vl_tipo_doc));
            }
            gvDocumentos_solicitud.DataSource = mytable;
            gvDocumentos_solicitud.Refresh();
            mytable.Dispose();
            dr.Close();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {    
            DataGridViewRow row = gvDocumentos_solicitud.CurrentRow;
            if (row != null)
            {
                int vl_fila = row.Index;

                int vl_documento_id = int.Parse(row.Cells["documento_id"].Value.ToString());
                string vl_desc_documento = row.Cells["nombre_doc"].Value.ToString();

                int vl_existe_doc = DocSys.p_verificar_doc_existe(vl_no_solicitud, vl_documento_id);
                if (vl_existe_doc >= 1)
                {
                    if (DialogResult.No == MessageBox.Show("El documento ya existe, desea reemplazar el documento ingresando anteriormente", vl_desc_documento, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        return;
                    }
                }

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "::DocSis - Seleccione un archivo";
                openFileDialog.InitialDirectory = Application.StartupPath + "\\logs";
                openFileDialog.Filter = "Todos los Archivos (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.Multiselect = false;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {


                    string vl_file = openFileDialog.FileName;             //archivo seleccionado con path
                    //string vl_only_file = openFileDialog.SafeFileName;    //archivo solamente sin path            
                    var vl_var = new System.IO.FileInfo(vl_file);
                    string vl_extesion = vl_var.Extension;                //extension del archivo seleccionado                                
                    string vl_only_file = vl_var.Name;
                    openFileDialog.Dispose();
                    p_insertar_adjunto(vl_documento_id, vl_file, vl_only_file, vl_extesion);

                    int vl_no_archivo_adj = DocSys.p_obtener_no_archivo(vl_no_solicitud, vl_documento_id);

                    DataRow selectedRow = mytable.Rows[vl_fila];
                    mytable.Rows[vl_fila].BeginEdit();
                    mytable.Rows[vl_fila]["recibido_b"] = 1;
                    mytable.Rows[vl_fila]["no_archivo"] = vl_no_archivo_adj;
                    mytable.AcceptChanges();

                    //mytable.Rows.Remove(selectedRow);


                }
            }
        }
        private void p_insertar_adjunto(int p_documento_id,string p_file, string p_only_file, string p_extension)
        {
            try
            {                
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                //System.IO.StreamReader sr = new System.IO.StreamReader(p_file);
                FileInfo fi = new FileInfo(p_file);
                StreamReader sr = new StreamReader(fi.FullName);
                String tempBuff = sr.ReadToEnd();
                sr.Close();

                System.IO.FileStream fs = new System.IO.FileStream(p_file, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                Byte[] data = new byte[fs.Length];
                fs.Read(data, 0, Convert.ToInt32(fs.Length));
                fs.Dispose();
                
                OracleTransaction transaction = DocSys.connOracle.BeginTransaction();
                OracleCommand command = DocSys.connOracle.CreateCommand();
                command.Transaction = transaction;
                command.CommandText = "declare xx blob; begin dbms_lob.createtemporary(xx, false, 0); :tempblob := xx; end;";
                command.Parameters.Add(new OracleParameter("tempblob", OracleType.Blob)).Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                OracleLob tempLob = (OracleLob)command.Parameters[0].Value;
                
                tempLob.BeginBatch(OracleLobOpenMode.ReadWrite);
                tempLob.Write(data, 0, data.Length);
                tempLob.EndBatch();
                command.Parameters.Clear();
                transaction.Commit();

                

                string vl_sql2 = "";
                vl_sql2 = vl_sql2 + "DCS_P_INS_UPD_ADJUNTO";
                OracleCommand cmd2 = new OracleCommand(vl_sql2, DocSys.connOracle);
                
                cmd2.CommandType = CommandType.StoredProcedure;
                //───────────────────
                OracleParameter pa_parametro1 = new OracleParameter("pa_no_solicitud", OracleType.Int32);
                cmd2.Parameters.Add(pa_parametro1);
                pa_parametro1.Direction = ParameterDirection.Input;
                pa_parametro1.Value = vl_no_solicitud;
                //───────────────────
                OracleParameter pa_parametro2 = new OracleParameter("pa_documento_id", OracleType.Int32);
                cmd2.Parameters.Add(pa_parametro2);
                pa_parametro2.Direction = ParameterDirection.Input;
                pa_parametro2.Value = p_documento_id;
                //───────────────────
                OracleParameter pa_parametro3 = new OracleParameter("pa_nombre_archivo", OracleType.VarChar, 30);
                cmd2.Parameters.Add(pa_parametro3);
                pa_parametro3.Direction = ParameterDirection.Input;
                pa_parametro3.Value = p_only_file;
                //───────────────────
                OracleParameter pa_parametro4 = new OracleParameter("pa_extension", OracleType.VarChar, 10);
                cmd2.Parameters.Add(pa_parametro4);
                pa_parametro4.Direction = ParameterDirection.Input;
                pa_parametro4.Value = p_extension;
                //───────────────────                                                   
                OracleParameter pa_parametro5 = new OracleParameter("pa_archivo_bin", OracleType.Blob);
                cmd2.Parameters.Add(pa_parametro5);
                pa_parametro5.Direction = ParameterDirection.Input;
                pa_parametro5.Value = tempLob;
                //───────────────────
                cmd2.ExecuteReader();

                


                Icon icono;
                icono = System.Drawing.Icon.ExtractAssociatedIcon(p_file);
                Image vl_Image = icono.ToBitmap();
                p_insertar_tipo_archivo(p_extension, vl_Image);
                
                MessageBox.Show("Adjunto ingresado satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_insertar_solicitud :" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        private void p_abrir_adjunto(int p_no_archivo)
        {
            string vl_sql = @"Select * 
                                From dcs_archivos_adjuntos 
                               Where no_archivo=:pa_no_archivo";           
            if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
            {
                DocSys.connOracle.Open();
            }
            OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
            cmd2.CommandType = CommandType.Text;
            //───────────────────
            OracleParameter pa_parametro1 = new OracleParameter("pa_no_archivo", OracleType.Int32);
            cmd2.Parameters.Add(pa_parametro1);
            pa_parametro1.Direction = ParameterDirection.Input;
            pa_parametro1.Value = p_no_archivo;
            //───────────────────

            OracleDataReader dr = cmd2.ExecuteReader();
            dr.Read();
            if (!DBNull.Value.Equals(dr["archivo_bin"]))
            {
                byte[] bits = ((byte[])dr["archivo_bin"]);
                string sFile = Application.StartupPath.ToString() + @"\tmp\" + DocSys.vl_user + "tmp" + dr["no_solicitud"] + DateTime.Now.ToString("yyyyMMddhhmmss") + "." + dr["extension"].ToString();
                System.IO.FileStream fs = new System.IO.FileStream(sFile, System.IO.FileMode.Create);
                fs.Write(bits, 0, Convert.ToInt32(bits.Length));
                fs.Close();
                fs.Dispose();
                try
                {
                    System.Diagnostics.Process.Start(sFile);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                    p.StartInfo.FileName = "rundll32.exe";
                    p.StartInfo.Arguments = "shell32.dll,OpenAs_RunDLL " + sFile;
                    p.Start();
                }

            }
        }
        private void p_insertar_tipo_archivo(string p_extension, Image p_icono)
        {
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                pictureBox4.Image = p_icono;
                Byte[] data = new byte[0];
                data = convertPicBoxImageToByte(pictureBox4);

                string vl_sql = "";
                vl_sql = vl_sql + "DCS_P_INSERTAR_TIPO_ARCHIVO";
                OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd.CommandType = CommandType.StoredProcedure;
                //───────────────────
                OracleParameter pa_parametro1 = new OracleParameter("pa_extension", OracleType.VarChar, 10);
                cmd.Parameters.Add(pa_parametro1);
                pa_parametro1.Direction = ParameterDirection.Input;
                pa_parametro1.Value = p_extension;
                //───────────────────
                OracleParameter pa_parametro4 = new OracleParameter("pa_icono_bin", OracleType.Blob);
                cmd.Parameters.Add(pa_parametro4);
                pa_parametro4.Direction = ParameterDirection.Input;
                pa_parametro4.Value = data;
                //───────────────────                
                cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_insertar_tipo_archivo :" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        private byte[] convertPicBoxImageToByte(System.Windows.Forms.PictureBox pbImage)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            pbImage.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }
        private void button_cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvDocumentos_solicitud_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
        }
        private void gvDocumentos_solicitud_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
            if (e.ColumnIndex == 1)
            {
                button1_Click(null, null);
            }
            if (e.ColumnIndex == 0) //es la columna de la imagen de la luoa
            {
                DataGridViewRow row = gvDocumentos_solicitud.CurrentRow;
                int vl_fila = row.Index;

                int vl_no_archivo = int.Parse(row.Cells["no_archivo"].Value.ToString());              
                if (vl_no_archivo>0)
                {
                    p_abrir_adjunto(vl_no_archivo);
                }
            }
            
        }
        private void gvDocumentos_solicitud_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            button1_Click(null, null);
        }
        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }
        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
	}
}
