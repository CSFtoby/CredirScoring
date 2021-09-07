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
	public partial class s_buscarusuarios : Form
	{
		public string vl_usuario = "";
		public DataAccess da;
		bool con_borde = MDI_Menu.con_borde;
		public static bool esGerenteFilial = false;
		public static int CodigoAgencia = 0;		

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

        public s_buscarusuarios()
        {
            InitializeComponent();
            this.da = da;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string vl_condicion_busqueda = " and UPPER(codigo_cliente||codigo_usuario||nombres||' '||primer_apellido||' '||segundo_apellido||nombre_agencia) like " + "'%" + txtTexto_busqueda_todos.Text.ToUpper() + "%'";
			string extraCondicion = (esGerenteFilial) ? " and a.codigo_agencia = " + CodigoAgencia + " and e.estacion_id = 1000 "
													  : string.Empty;

            DataTable dtUsuarios= da.ObtenerListadoUsuarios(vl_condicion_busqueda, extraCondicion);
            gv_usuarios.DataSource = dtUsuarios;
            gv_usuarios.Refresh();
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {            
            DataGridViewRow row = gv_usuarios.CurrentRow;
            if (row != null)
            {
                vl_usuario = row.Cells["codigo_usuario"].Value.ToString();
                this.DialogResult = DialogResult.OK;
            }
            else
                return;
        }

        private void txtTexto_busqueda_todos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                pictureBox1_Click(null, null);
            }
        }

        private void gv_usuarios_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSeleccionar_Click(null, null);
        }
    }
}
