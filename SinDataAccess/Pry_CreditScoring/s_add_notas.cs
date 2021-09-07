using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using wfcModel;

namespace Docsis_Application
{
    public partial class s_add_notas : Form
    {
        DataAccess da;
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
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
        #endregion

        int vl_estacion_actual_id = 0;
        int vl_no_solicitud = 0;
        int vl_no_anotacion = 0;
        string vl_anotacion = "";

        #region ### Constructor ##
        public s_add_notas(DataAccess da,string pa_accion,int pa_no_solicitud, Int32 pa_estacion_actual_id,int pa_no_anotacion)
        {
            InitializeComponent();
            this.da = da;
            
            if (pa_accion == "INS")
            {
                label_header.Text = "Agregar notas a Solicitud No. " + pa_no_solicitud.ToString();
                button1.Visible = true;
                txtTexto.ReadOnly = false;
            }

            if (pa_accion == "CONS")
            {
                label_header.Text = "Visualizar notas a Solicitud No. " + pa_no_solicitud.ToString();
                button1.Visible = false;
                radioButton_anota_condicion.Enabled = false;
                radioButton_anota_normal.Enabled = false;
                txtTexto.ReadOnly = true;
                
            }

            vl_no_solicitud = pa_no_solicitud;            
            vl_no_anotacion = pa_no_anotacion;
            vl_estacion_actual_id = pa_estacion_actual_id;
            txtTexto.Text = da.ObtenerAnotacionxNo(vl_no_anotacion);
        }
        #endregion

        private void s_add_notas_Load(object sender, EventArgs e)
        {
            p_datos_anotacion();
            txtTexto_KeyPress(null, null);            
        }
        private void p_datos_anotacion()
        {
            try
            {
                string vl_sql = @"select * from dcs_anotaciones_solicitudes where no_anotacion=:prm_no_anotacion";
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd2.CommandType = CommandType.Text;
                //───────────────────
                OracleParameter prm_no_anotacion = new OracleParameter("prm_no_anotacion", OracleType.Int32);
                cmd2.Parameters.Add(prm_no_anotacion);
                prm_no_anotacion.Direction = ParameterDirection.Input;
                prm_no_anotacion.Value = vl_no_anotacion;
                //───────────────────            
                OracleDataReader dr = cmd2.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    string vl_tipo_anotacion = dr["tipo_anotacion"].ToString();
                    string vl_aceptada = dr["aceptada"].ToString();
                    string vl_fecha_aceptacion=dr["fecha_aceptacion"].ToString();
                    string vl_aceptada_por = dr["aceptada_por"].ToString();
                    if (vl_aceptada == "S")
                    {
                        label_aceptacion.Text = "Aceptada :" + vl_aceptada + "\n" + "Fecha aceptación :" + vl_fecha_aceptacion + "\n" + "Aceptada Por :" + vl_aceptada_por;
                    }
                    else
                    {
                        label_aceptacion.Text = "";
                    }
                    txtTexto.Text = dr["anotacion"].ToString();
                    if (vl_tipo_anotacion == "N")
                    {
                        radioButton_anota_normal.Checked = true;
                        radioButton_anota_condicion.Checked = false;
                       // pictureBox1.Image = imageList1.Images[0];                        
                    }
                    if (vl_tipo_anotacion == "C")
                    {
                        radioButton_anota_normal.Checked = false;
                        radioButton_anota_condicion.Checked = true;
                       // pictureBox1.Image = imageList1.Images[1];                        
                    }                    
                }
                else
                    label_aceptacion.Text = "";
                dr.Close();
                button2.Focus();
                txtTexto.DeselectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }  
        private void Form1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            moverForm();
        }
        private void p_insertar_anotacion()
        {
            if (txtTexto.Text.Length <= 9)
            {
                MessageBox.Show("Debe indicar al menos 10 caracteres en la anotación ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                string vl_tipo_anotacion = "N";
                if (radioButton_anota_normal.Checked)
                    vl_tipo_anotacion = "N";

                if (radioButton_anota_condicion.Checked)
                    vl_tipo_anotacion ="C";

                da.InsertarAnotacion(vl_no_solicitud,vl_estacion_actual_id, txtTexto.Text, vl_tipo_anotacion);
                this.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " " + ex.Source, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            p_insertar_anotacion();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtTexto_KeyPress(object sender, KeyPressEventArgs e)
        {
            label_caracteres.Text = txtTexto.Text.Length.ToString() + " caracteres de "+txtTexto.MaxLength.ToString()+" permitidos";            
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
