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
    public partial class s_PreCalificado_info03 : Form
    {


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

  

        public DataAccess da;
        public string codigo_cliente;
        public s_PreCalificado_info03()
        {
            InitializeComponent();
        }

        private void s_PreCalificado_info03_Load(object sender, EventArgs e)
        {
            string sin_huella;
            int dedo = 0;
            byte[] huella;
            byte[] huella_foto;
            string fecha_actualizacion;
            da.ObtenerHuellaUsuario(codigo_cliente, out sin_huella, out dedo, out huella, out huella_foto, out fecha_actualizacion);
            if (huella_foto != null)
            {
                pbHuellaImg.Image = DocSys.p_CopyDataToBitmap(huella_foto);                
            }
            labelCodigo_Cliente.Text = codigo_cliente;

        }        
        private void button9_Click(object sender, EventArgs e)
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
