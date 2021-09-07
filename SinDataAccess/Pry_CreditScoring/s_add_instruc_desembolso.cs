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
    public partial class s_add_instruc_desembolso : Form
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
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
        #endregion

        DataAccess da;
        int vl_no_solicitud = 0;

        public s_add_instruc_desembolso(DataAccess da, int pa_no_solicitud)
        {
            InitializeComponent();
            this.da = da;
            vl_no_solicitud = pa_no_solicitud;
            txtTexto.Text = da.ObtenerInstruccionesDesembolsoxSolicitud(vl_no_solicitud);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            p_poner_instrucciones();
        }
        private void p_poner_instrucciones()
        {
            if (txtTexto.Text.Length <= 9)
            {
                MessageBox.Show("Debe indicar al menos 10 caracteres en la instrucción ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                da.Instrucciones_desembolso(vl_no_solicitud, txtTexto.Text);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " " + ex.Source, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }
        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
	}
}
