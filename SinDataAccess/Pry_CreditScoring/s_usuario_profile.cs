using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using wfcModel;

namespace Docsis_Application
{
    public partial class s_usuario_profile : Form
    {
        public DataAccess da;

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


        #region
        private const int CS_DROPSHADOW = 0x00020000;
        protected override CreateParams CreateParams
        {
            get
            {
                // add the drop shadow flag for automatically drawing
                // a drop shadow around the form
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
        #endregion

        string vl_parametro_poner_foto = "S";
        string vl_usuario = "";
        string vl_file_mebo = "";
        public s_usuario_profile(string pa_usuario)
        {
            InitializeComponent();
            vl_usuario = pa_usuario;
        }

        
        private void s_usuario_profile_Load(object sender, EventArgs e)
        {
            label_usuario.Text = vl_usuario;
            p_info_usuario();
        }

        private void p_info_usuario()
        {
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string vl_sql = @"Select codigo_cliente,
                                     Initcap(nombres) nombres,
                                     InitCap(primer_apellido) primer_apellido,
                                     InitCap(segundo_apellido) segundo_apellido,
                                     Initcap(nombres)||' '||InitCap(primer_apellido) nombre_comple,
                                     Initcap(a.nombre_agencia) nombre_agencia,
                                     up.correo_electronico,
                                     up.numero_extension,
                                     up.numero_celular,
                                     up.mebo,
                                     send_mail_arrival_station,
                                     send_mail_assign,
                                     e.nombre nombre_estacion                                    
                                From mgi_usuarios u,
                                     mgi_agencias a,
                                     dcs_wf_usuarios_estaciones ue,
                                     dcs_wf_usuarios_perfiles up,
                                     dcs_wf_estaciones e
                               Where a.codigo_empresa=1
                                 and u.codigo_empresa=1
                                 and u.codigo_agencia=a.codigo_agencia
                                 and u.codigo_usuario=up.usuario(+)
                                 and u.codigo_usuario=ue.usuario(+)
                                 and e.estacion_id=ue.estacion_id
                                 and codigo_usuario=:pa_codigo_usuario";
                OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd.CommandType = CommandType.Text;
                //───────────────────
                OracleParameter pa_codigo_usuario = new OracleParameter("pa_codigo_usuario", OracleType.VarChar, 100);
                cmd.Parameters.Add(pa_codigo_usuario);
                pa_codigo_usuario.Direction = ParameterDirection.Input;
                pa_codigo_usuario.Value = vl_usuario;

                OracleDataReader dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    label_codigo_cliente.Text = dr["codigo_cliente"].ToString().Trim();
                    label_nombre.Text = dr["nombres"].ToString().Trim();
                    label_primer_apellido.Text = dr["primer_apellido"].ToString().Trim();
                    label_segundo_apellido.Text = dr["segundo_apellido"].ToString().Trim();
                    label_filial.Text = dr["nombre_agencia"].ToString();
                    label_estacion_wf.Text = dr["nombre_estacion"].ToString().Trim();
                    textBox_correo_electronico.Text = dr["correo_electronico"].ToString().Trim();
                    textBox_numero_extension.Text = dr["numero_extension"].ToString().Trim();                    
                    textBox_numero_celular.Text = dr["numero_celular"].ToString().Trim();
                    if (dr["send_mail_arrival_station"].ToString()=="S")
                        checkBox_insolicitud.Checked=true;
                    else
                        checkBox_insolicitud.Checked=false;
                    if (dr["send_mail_assign"].ToString() == "S")
                        checkBox_asignar_solicitud.Checked=true;
                    else
                        checkBox_asignar_solicitud.Checked=false;
                        
                
                    if (!DBNull.Value.Equals(dr["Mebo"]) && vl_parametro_poner_foto == "S")
                    {
                        byte[] bits = ((byte[])dr["mebo"]);
                        pbMebo.Image = new Bitmap(DocSys.p_CopyDataToBitmap(bits));

                    }
                    else
                    {
                        pbMebo.Image = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                string vl_mostrar_fotocolaborador = da.ObtenerParametro("WFC-0016");
                if (vl_mostrar_fotocolaborador == "S")
                {
                    string codigo_cliente_usuario = da.ObtenerCodigoClientexUsuario(vl_usuario);
                    byte[] fotoemp = da.ObtenerFotoUsuario(codigo_cliente_usuario);
                    pbMebo.Image = DocSys.p_CopyDataToBitmap(fotoemp);
                }
            }
            catch (Exception ex)
            {
                pbMebo.Image = null;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!p_valida_email(textBox_correo_electronico.Text))
            {            
                MessageBox.Show("El correo electrónico ingresado no tiene el formato correcto");
                return;
            }
            if ((checkBox_asignar_solicitud.Checked || checkBox_insolicitud.Checked) && (textBox_correo_electronico.Text == string.Empty))
            {
                MessageBox.Show("Debe indicar la cuenta de correo electronico para recibir las alertas seleccionadas", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }            
            

                            
            p_actualizar_usuario();
        }

        private void p_actualizar_usuario()
        {            
            try
            {
               
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }

                OracleLob Fotomebo = OracleLob.Null;
                if (vl_file_mebo != string.Empty)
                {

                    FileInfo fi = new FileInfo(vl_file_mebo);
                    StreamReader sr = new StreamReader(fi.FullName);
                    String tempBuff = sr.ReadToEnd();
                    sr.Close();

                    System.IO.FileStream fs = new System.IO.FileStream(vl_file_mebo, System.IO.FileMode.Open, System.IO.FileAccess.Read);
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
                    Fotomebo = tempLob;                   

                    tempLob.BeginBatch(OracleLobOpenMode.ReadWrite);
                    tempLob.Write(data, 0, data.Length);
                    tempLob.EndBatch();
                    command.Parameters.Clear();
                    transaction.Commit();                    
                }
                string vl_quitarimage="N";
                if (checkBox_quitar.Checked)
                    vl_quitarimage="S";

                string vl_alertinsolicitud = "N";
                if (checkBox_insolicitud.Checked)
                    vl_alertinsolicitud = "S";
                string vl_alertasignacionsolic = "N";
                if (checkBox_asignar_solicitud.Checked)
                    vl_alertasignacionsolic = "S";


                string vl_sql = "";
                vl_sql = vl_sql + "DCS_P_ACTUALIZAR_PROFILE";
                OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd.CommandType = CommandType.StoredProcedure;
                //───────────────────
                OracleParameter pa_usuario = new OracleParameter("pa_usuario", OracleType.VarChar,50);
                cmd.Parameters.Add(pa_usuario);
                pa_usuario.Direction = ParameterDirection.Input;
                pa_usuario.Value = label_usuario.Text;
                //───────────────────
                OracleParameter pa_correo = new OracleParameter("pa_correo", OracleType.VarChar, 50);
                cmd.Parameters.Add(pa_correo);
                pa_correo.Direction = ParameterDirection.Input;
                pa_correo.Value = textBox_correo_electronico.Text;

                OracleParameter pa_extension = new OracleParameter("pa_extension", OracleType.VarChar, 10);
                cmd.Parameters.Add(pa_extension);
                pa_extension.Direction = ParameterDirection.Input;
                pa_extension.Value = textBox_numero_extension.Text;

                OracleParameter pa_celular = new OracleParameter("pa_celular", OracleType.VarChar, 10);
                cmd.Parameters.Add(pa_celular);
                pa_celular.Direction = ParameterDirection.Input;
                pa_celular.Value =textBox_numero_celular.Text;

                OracleParameter pa_alertinsolicitud = new OracleParameter("pa_alertinsolicitud", OracleType.VarChar, 1);
                cmd.Parameters.Add(pa_alertinsolicitud);
                pa_alertinsolicitud.Direction = ParameterDirection.Input;
                pa_alertinsolicitud.Value = vl_alertinsolicitud;

                OracleParameter pa_alertasignacionsolic = new OracleParameter("pa_alertasignacionsolic", OracleType.VarChar, 1);
                cmd.Parameters.Add(pa_alertasignacionsolic);
                pa_alertasignacionsolic.Direction = ParameterDirection.Input;
                pa_alertasignacionsolic.Value = vl_alertasignacionsolic;                                                                            

                OracleParameter pa_quitar_mebo = new OracleParameter("pa_quitar_mebo", OracleType.VarChar, 1);
                cmd.Parameters.Add(pa_quitar_mebo);
                pa_quitar_mebo.Direction = ParameterDirection.Input;
                pa_quitar_mebo.Value = vl_quitarimage;

                OracleParameter pa_parametro5 = new OracleParameter("pa_mebo", OracleType.Blob);
                cmd.Parameters.Add(pa_parametro5);
                pa_parametro5.Direction = ParameterDirection.Input;
                pa_parametro5.Value = Fotomebo;

                //───────────────────
                cmd.ExecuteReader();
                this.DialogResult = DialogResult.OK;
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_actualizar_usuario :" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "::DocSis - Seleccione un archivo";
            openFileDialog.InitialDirectory = Application.StartupPath + "\\logs";
            openFileDialog.Filter = "Archivos de Imagenes |*.jpge;*.jpg;*.bmp;*.png;*.bmp";
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
                vl_file_mebo = vl_file;
                pbMebo.Image = new Bitmap(vl_file_mebo);
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            s_usuario_preferencias01 forma = new s_usuario_preferencias01();
            forma.ShowDialog();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private Boolean p_valida_email(String email)
        {
            if (!email.Contains("@sagradafamilia.hn"))
            {
                MessageBox.Show("La cuenta de correo debe contener el dominio sagradafamilia.hn", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (System.Text.RegularExpressions.Regex.IsMatch(email, expresion))
            {
                if (System.Text.RegularExpressions.Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
