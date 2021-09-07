using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using wfcModel;
namespace Docsis_Application
{
    public partial class s_filiales_usuario : Form
    {
        public string codigo_filial_seleccionada;
        public string nombre_filia_seleccionada;
        String vl_filial = "";
        public DataAccess da;
        bool con_borde = MDI_Menu.con_borde;
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
        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }
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
        public s_filiales_usuario()
        {
            InitializeComponent();
        }
        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void s_filiales_usuario_Load(object sender, EventArgs e)
        {
            p_llenar_listbox_filiales();
            LabelUsuario.Text = DocSys.vl_user;
        }
        private void p_llenar_listbox_filiales()
        {
            try
            {
                DataTable dtFiliales = new DataTable();
                if (da.EsAdministradorSistema(DocSys.vl_user) || this.da.EsAdminTemporal(DocSys.vl_user))
                {
                    dtFiliales = da.ObtenerFiliales();
                }
                else
                {
                    if (da.EsGerenteRegional(DocSys.vl_user))
                    {
                        dtFiliales = da.ObtenerFilialesxGerenteRegional(DocSys.vl_user);
                    }
                    else
                    {
                        dtFiliales = da.ObtenerFilialesxGerenteFilial(DocSys.vl_user);
                    }
                }
                listBoxFiliales.DataSource = dtFiliales;
                listBoxFiliales.DisplayMember = "nombre_agencia";
                listBoxFiliales.ValueMember = "codigo_agencia";

                //listBoxFiliales_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_llenar_listbox_filiales() : " + ex.Message);
            }
        }

        private void listBoxFiliales_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                vl_filial = listBoxFiliales.SelectedValue.ToString();
                codigo_filial_seleccionada = vl_filial;
                nombre_filia_seleccionada = listBoxFiliales.Text;
            }
            catch
            {
            }
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            listBoxFiliales_SelectedIndexChanged(null, null);
            this.DialogResult = DialogResult.OK;
        }
        private void listBoxFiliales_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSeleccionar_Click(null, null);
        }
    }
}
