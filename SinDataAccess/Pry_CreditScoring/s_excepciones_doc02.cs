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
    public partial class s_excepciones_doc02 : Form
    {
        DataAccess da;
        public string vl_enter = "\r\n";
        public string excepcion_id = "";
        bool con_borde = MDI_Menu.con_borde;
		private int CodigoExcepcion;
          

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

        public s_excepciones_doc02(DataAccess da, int codigoExcepcion)
        {
            InitializeComponent();
            this.da = da;
			this.CodigoExcepcion = codigoExcepcion;
			p_llenar_lista();
        }
        public void p_llenar_lista()
        {
			try
			{
				DataTable detalle = this.da.get_detalle_movimiento_excep(this.CodigoExcepcion);
				this.dgvDetalle.DataSource = detalle;
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ha ocurrido un error {ex.Message}", "Error");
			}
		}

        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }
        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            this.DialogResult = DialogResult.OK;
        }

		private void s_excepciones_doc02_Load(object sender, EventArgs e)
		{

		}
	}
}
