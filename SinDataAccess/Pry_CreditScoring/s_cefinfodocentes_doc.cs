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
    public partial class s_cefinfodocentes_doc : Form
    {
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


        public string sidentificacion = "";
        public s_cefinfodocentes_doc()
        {
            InitializeComponent();
        }

        private void s_cefinfodocentes_doc_Load(object sender, EventArgs e)
        {
            
            llenar_info_sueldos();
        }

        private void llenar_info_sueldos()
        {
            string sid = sidentificacion.Replace("-","");
            txtIDSolicitante.Text = sid;
            gvInfoSueldos.DataSource = da.ObtenerHistorialSueldosDocentesCEF(sid);
            gvInfoSueldos.Refresh();
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            btnClose_Click(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void labelComite_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }
    }
}
