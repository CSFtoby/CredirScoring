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
    public partial class s_buscarfiliales : Form
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
        public string codigo_filial_seleccionada;        
        public string vl_codigo_agencia;
        public DataAccess da;
        public s_buscarfiliales()
        {
            InitializeComponent();

        }
        private void s_buscarfiliales_Load(object sender, EventArgs e)
        {
            p_llenar_listbox_filiales();
        }
        private void p_llenar_listbox_filiales()
        {
            try
            {

                listBoxFiliales.DataSource = da.ObtenerListaAgencias(textBoxBusqueda.Text);
                listBoxFiliales.DisplayMember = "nombre_agencia";
                listBoxFiliales.ValueMember = "codigo_agencia";

                //listBoxFiliales_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_llenar_listbox_filiales() : " + ex.Message);
            }
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            p_llenar_listbox_filiales();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void listBoxFiliales_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                vl_codigo_agencia = listBoxFiliales.SelectedValue.ToString();
                codigo_filial_seleccionada = vl_codigo_agencia;
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

        private void textBoxBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnBuscar_Click(null, null);
            }
        }        
    }
}
