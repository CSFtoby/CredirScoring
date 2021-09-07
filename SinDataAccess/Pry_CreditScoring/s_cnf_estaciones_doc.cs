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
    public partial class s_cnf_estaciones_doc : Form
    {
        DataAccess da;
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


        string vl_accion = "";
        int vl_estacion_id = 0;
        string vl_file_icono = "";
        public s_cnf_estaciones_doc(DataAccess da,string pa_accion,int pa_estacion_id)
        {
            InitializeComponent();
            vl_accion = pa_accion;
            vl_estacion_id = pa_estacion_id;
            this.da = da;
        }        
        private void s_cnf_estaciones_doc01_Load(object sender, EventArgs e)
        {
            if (vl_accion == "INS")
            {
                label_Titulo.Text = " :::Agregar Estación:::";
                txtEstacion_id.Focus();                
                button1.Text = "Guardar";
            }

            if (vl_accion == "MODIF")
            {
                label_Titulo.Text = " :::Modificar Estación:::";
                p_datos_estacion();                
                txtEstacion_id.ReadOnly = true;
                txtNombre_estacion.Focus();
                button1.Focus();
                button1.Text = "Modificar";
            }

            if (vl_accion == "ELIM")
            {
                label_Titulo.Text = " :::Eliminar Estación:::";
                p_datos_estacion();
                txtEstacion_id.ReadOnly = true;
                txtNombre_estacion.ReadOnly = true;
                radioButton_activo_no.Enabled = false;
                radioButton_activo_si.Enabled = false;
                radioButton_todafilial_si.Enabled = false;
                radioButton_todafilial_no.Enabled = false;
                radioButton_csolic_si.Enabled = false;
                radioButton_csolic_no.Enabled = false;
                button1.Focus();
                button1.Text = "Eliminar";
            }
        }
        private void p_datos_estacion()
        {
            try
            {
                var dt = da.ObtenerEstacionesxId(vl_estacion_id);
                if (dt.Rows.Count>0)
                {
                    txtEstacion_id.Text = dt.Rows[0]["estacion_id"].ToString();
                    txtNombre_estacion.Text = dt.Rows[0]["nombre"].ToString();
                    string vl_activo = dt.Rows[0]["activo"].ToString();
                    string vl_ver_toda_filial = dt.Rows[0]["ver_toda_filial"].ToString();
                    string vl_crear_solicitudes = dt.Rows[0]["crear_solicitudes"].ToString();
                    string vl_comite_resolutivo = dt.Rows[0]["comite_resolutivo"].ToString();
                    txtMonto_minimo.Text = dt.Rows[0]["monto_minimo_resolucion"].ToString();
                    txtMonto_maximo.Text = dt.Rows[0]["monto_maximo_resolucion"].ToString();

                    if (vl_activo == "S")
                    {
                        radioButton_activo_si.Checked = true;
                        radioButton_activo_no.Checked = false;
                    }
                    if (vl_activo == "N")
                    {
                        radioButton_activo_si.Checked = false;
                        radioButton_activo_no.Checked = true;
                    }

                    if (vl_ver_toda_filial == "S")
                    {
                        radioButton_todafilial_si.Checked = true;
                        radioButton_todafilial_no.Checked = false;
                    }
                    if (vl_ver_toda_filial == "N")
                    {
                        radioButton_todafilial_si.Checked = false;
                        radioButton_todafilial_no.Checked = true;
                    }

                    if (vl_crear_solicitudes == "S")
                    {
                        radioButton_csolic_si.Checked = true;
                        radioButton_csolic_no.Checked = false;
                    }
                    if (vl_crear_solicitudes == "N")
                    {
                        radioButton_csolic_si.Checked = false;
                        radioButton_csolic_no.Checked = true;
                    }

                    if (vl_comite_resolutivo == "S")
                    {
                        radioButton_Escomite_si.Checked = true;
                        radioButton_Escomite_no.Checked = false;
                    }
                    if (vl_comite_resolutivo == "N")
                    {
                        radioButton_Escomite_si.Checked = false;
                        radioButton_Escomite_no.Checked = true;
                    }
                    if (!DBNull.Value.Equals(dt.Rows[0]["icono"]))
                    {
                        byte[] bits = ((byte[])dt.Rows[0]["icono"]);
                        pbIcono.Image = new Bitmap(DocSys.p_CopyDataToBitmap(bits));

                    }

                   

                }             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void p_abm_estacion()
        {
            string vl_activo = "N";
            string vl_ver_toda_filial = "N";
            string vl_crear_solicitudes = "N";
            string vl_comite_resolutivo = "N";
            
            if (radioButton_activo_si.Checked)
                vl_activo = "S";
            
            if (radioButton_todafilial_si.Checked)
                vl_ver_toda_filial = "S";

            if (radioButton_csolic_si.Checked)
                vl_crear_solicitudes = "S";

            if (radioButton_Escomite_si.Checked)
                vl_comite_resolutivo = "S";

            try
            {

                byte[] p_image_icon = DocSys.p_CopyImageToByteArray(pbIcono.Image);
                decimal vl_monto_min = decimal.Parse(txtMonto_minimo.Text);
                decimal vl_monto_max = decimal.Parse(txtMonto_maximo.Text);

                da.ambEstaciones_workflow(vl_accion, Int32.Parse(txtEstacion_id.Text), txtNombre_estacion.Text, vl_activo, vl_ver_toda_filial, vl_crear_solicitudes, 
                    vl_comite_resolutivo, p_image_icon,vl_monto_min,vl_monto_max);

                if (vl_accion == "INS")
                {
                    MessageBox.Show("Estación ingresado satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (vl_accion == "MODIF")
                {
                    MessageBox.Show("Estación modificado satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (vl_accion == "ELIM")
                {
                    MessageBox.Show("Estación eliminada satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Close();
                this.DialogResult = DialogResult.OK;
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_insertar_flujo :" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtNombre_estacion.Text == string.Empty)
            {
                MessageBox.Show("Debe indicar la descripcion de la estación ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (radioButton_Escomite_si.Checked)
            {
                if (decimal.Parse(txtMonto_minimo.Text)==0)
                {
                    MessageBox.Show("Debe indicar el monto minimo del criterio de aprobación de este comite..!","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    return;
                }
                if (decimal.Parse(txtMonto_maximo.Text) == 0)
                {
                    MessageBox.Show("Debe indicar el monto minimo del criterio de aprobación de este comite..!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (decimal.Parse(txtMonto_minimo.Text) >= decimal.Parse(txtMonto_maximo.Text))
                {
                    MessageBox.Show("Hay incosistencia en el rango ingresado..!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            
            if (vl_accion == "ELIM")
            {                
                if (DialogResult.No == MessageBox.Show("Desea eliminar la estación  ?", DocSys.vl_mensaje_avisos, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    return;
                }
            }
            p_abm_estacion();
        }       
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button_cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }
        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }
        private void lLBuscar_imagen_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
                vl_file_icono = vl_file;
                pbIcono.Image = new Bitmap(vl_file_icono);
            }
        }

        

        
    }
}
