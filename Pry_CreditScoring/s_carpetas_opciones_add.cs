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
    public partial class s_carpetas_opciones_add : Form
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

        int vl_solicitud = 0;
        int vl_carpeta_id = 0;

        public s_carpetas_opciones_add(int pa_no_solicitud)
        {
            InitializeComponent();
            vl_solicitud = pa_no_solicitud;
        }
        private void s_carpetas_opciones_Load(object sender, EventArgs e)
        {
            p_llenar_listbox_carpetas();
            listBox_carpetas_SelectedIndexChanged(null, null);
        }
        private void p_llenar_listbox_carpetas()
        {
            try
            {
                DataSet dsCombo = new DataSet();
                dsCombo = DocSys.p_Obtener_un_dataset("select * from dcs_carpetas_usuarios where codigo_usuario='"+DocSys.vl_user+"'", "dcs_carpetas_usuarios");
                listBox_carpetas.DataSource = dsCombo;
                listBox_carpetas.DisplayMember = "dcs_carpetas_usuarios.descripcion";
                listBox_carpetas.ValueMember = "dcs_carpetas_usuarios.carpeta_id";                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_llenar_listboxcarpetas : " + ex.Message);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBox_carpetas_SelectedIndexChanged(object sender, EventArgs e)
        {            
            try
            {
                vl_carpeta_id = int.Parse(listBox_carpetas.SelectedValue.ToString());
            }
            catch
            {
            }                          
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            p_copiar_solicitud();
            this.DialogResult = DialogResult.OK;
        }
        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }  
        private void p_copiar_solicitud()
        {
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }                
                string  vl_sql ="DCS_P_COPIAR_SOL_CARPETA";
                OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd.CommandType = CommandType.StoredProcedure;
                //───────────────────
                OracleParameter pa_solicitud = new OracleParameter("pa_solicitud", OracleType.Int32);
                cmd.Parameters.Add(pa_solicitud);
                pa_solicitud.Direction = ParameterDirection.Input;
                pa_solicitud.Value =vl_solicitud;
                //───────────────────
                OracleParameter pa_carpeta_id = new OracleParameter("pa_carpeta_id", OracleType.Number);
                cmd.Parameters.Add(pa_carpeta_id);
                pa_carpeta_id.Direction = ParameterDirection.Input;
                pa_carpeta_id.Value = vl_carpeta_id;
                //----------------------
                cmd.ExecuteReader();
                
                this.Close();
                this.DialogResult = DialogResult.OK;
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_insertar_solicitud :" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
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
