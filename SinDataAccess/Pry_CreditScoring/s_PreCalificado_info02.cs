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
    public partial class s_PreCalificado_info02 : Form
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
        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }
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
        public DataAccess da;
        public int cartera;

        public s_PreCalificado_info02()
        {
            InitializeComponent();
        }
        private void s_PreCalificado_info02_Load(object sender, EventArgs e)
        {
            if (cartera == 1)
            {

                gvDetalleAdtiva.AutoGenerateColumns = false;
                gvDetalleAdtiva.DataSource = da.ObtenerDetalleCreditosCarteraAdtiva(labelCodigo_Cliente.Text);
            }
            if (cartera == 2)
            {
                gvDetalleCoposafa2y3.AutoGenerateColumns = false;
                gvDetalleCoposafa2y3.DataSource = da.ObtenerDetalleCreditosCoopsafa_2_3(labelCodigo_Cliente.Text, cartera);
            }
        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }

       
    }
}
