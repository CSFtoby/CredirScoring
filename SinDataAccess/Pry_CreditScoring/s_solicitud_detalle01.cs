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
    public partial class s_solicitud_detalle01 : Form
    {
        public Int32 gno_solicitud = 0;
        public DataAccess da;
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

        public s_solicitud_detalle01()
        {
            InitializeComponent();
        }
        private void s_solicitud_detalle01_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = da.ObtenerInformacionCierreSolicitud(gno_solicitud);
                if (dt.Rows.Count > 0)
                {
                    txtFecha_cierre.Text = dt.Rows[0]["fecha_cierre"].ToString();
                    txtCerrada_por.Text = dt.Rows[0]["cerrada_por"].ToString().Trim() + " - " + dt.Rows[0]["nombres"].ToString().Trim();
                    textBoxInstrucciones_desem.Text = dt.Rows[0]["instrucciones_desembolso"].ToString().Trim();
                    txtComentarios.Text = dt.Rows[0]["comentarios"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void panelTop_MouseMove(object sender, MouseEventArgs e)
        {
            moverForm();
        }
    }
}
