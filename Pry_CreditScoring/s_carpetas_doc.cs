using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Docsis_Application
{
    public partial class s_carpetas_doc : Form
    {
        bool con_borde = MDI_Menu.con_borde;
        #region Mover
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
        #region Sombra
        private const int CS_DROPSHADOW = 0x00020000;
        protected override CreateParams CreateParams
        {
            get
            {
                // add the drop shadow flag for automatically drawing
                // a drop shadow around the form
                CreateParams cp = base.CreateParams;
                if (con_borde)
                {
                    cp.Style |= 0x40000 | CS_DROPSHADOW;
                }
                else
                {
                    cp.ClassStyle |= CS_DROPSHADOW;
                }
                return cp;
            }
        }
        #endregion

        public s_carpetas_doc()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }  
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox_nombre_carpeta.Text.Length <= 3)
            {
                MessageBox.Show("Debe indicar un nombre de carpeta valido !", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (p_verficiar_nombre_repetido(textBox_nombre_carpeta.Text.Trim()) > 0)
            {
                MessageBox.Show("Nombre de carpeta ya existe ..!", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            p_crear_carpeta();
        }
        private void p_crear_carpeta()
        {
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string vl_sql = "";
                vl_sql = vl_sql + "DCS_P_CREAR_CARPETA";
                OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter pa_oficial_servicio = new OracleParameter("pa_usuario", OracleType.VarChar, 30);
                cmd.Parameters.Add(pa_oficial_servicio);
                pa_oficial_servicio.Direction = ParameterDirection.Input;
                pa_oficial_servicio.Value = DocSys.vl_user;
                //───────────────────
                OracleParameter pa_descripcion_carpeta = new OracleParameter("pa_descripcion_carpeta", OracleType.VarChar, 50);
                cmd.Parameters.Add(pa_descripcion_carpeta);
                pa_descripcion_carpeta.Direction = ParameterDirection.Input;
                pa_descripcion_carpeta.Value = textBox_nombre_carpeta.Text;
                //───────────────────                                
                cmd.ExecuteReader();
                //MessageBox.Show("Carpeta ingresada satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                this.DialogResult = DialogResult.OK;
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_crear_carpeta :" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        private void p_eliminar_carpeta()
        {
            int vl_carpeta_id = 0;
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string vl_sql = "";
                vl_sql = vl_sql + "DCS_P_ELIMINAR_CARPETA";
                OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter pa_carpeta_id = new OracleParameter("pa_carpeta_id", OracleType.Int32);
                cmd.Parameters.Add(pa_carpeta_id);
                pa_carpeta_id.Direction = ParameterDirection.Input;
                pa_carpeta_id.Value = vl_carpeta_id;
                                             
                cmd.ExecuteReader();                
                this.Close();
                this.DialogResult = DialogResult.OK;
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_eliminar_carpeta :" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }
        private int p_verficiar_nombre_repetido(string p_nombre_carpeta)
        {
            int vl_retorno = 0;
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string vl_sql = "";
                vl_sql = vl_sql + "select count(*) as cant from dcs_carpetas_usuarios where upper(descripcion)=upper(:pa_nombre_carpeta)";
                OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd.CommandType = CommandType.Text;

                OracleParameter pa_nombre_carpeta = new OracleParameter("pa_nombre_carpeta", OracleType.VarChar, 50);
                cmd.Parameters.Add(pa_nombre_carpeta);
                pa_nombre_carpeta.Direction = ParameterDirection.Input;
                pa_nombre_carpeta.Value = p_nombre_carpeta;
                
                OracleDataReader dr = cmd.ExecuteReader();
                dr.Read();
                vl_retorno = int.Parse(dr["cant"].ToString());
                return vl_retorno;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_verficiar_nombre_repetido :" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return vl_retorno;
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }
    }
}
