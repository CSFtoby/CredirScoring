using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Docsis_Application.Excepciones
{
	public partial class ComentarioResolucion : Form
	{
		public string Comentario { get; set; }

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

		private void moverForm()
		{
			ReleaseCapture();
			SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
		}

		private void panel3_MouseDown(object sender, MouseEventArgs e)
		{
			moverForm();
		}
		#endregion

		public ComentarioResolucion()
		{
			InitializeComponent();
		}

		private void btnAgregar_Click(object sender, EventArgs e)
		{			
			if (this.txtTexto.Text.Trim().Equals(string.Empty))
			{
				//MessageBox.Show("Debe ingresar la resolución final");
				return;
			}
			else
			{
				this.Comentario = this.txtTexto.Text;
			}
		}

		private void ComentarioResolucion_Load(object sender, EventArgs e)
		{

		}

		private void panel3_MouseDown_1(object sender, MouseEventArgs e)
		{
			this.moverForm();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			DialogResult result = MessageBox.Show("¿Seguro desea cancelar la operación?", "Cancelar", MessageBoxButtons.YesNo);
			if(result == DialogResult.Yes)
			{
				this.Close();
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			DialogResult result = MessageBox.Show("¿Seguro desea cancelar la operación?", "Cancelar", MessageBoxButtons.YesNo);
			if(result == DialogResult.Yes)
			{
				this.Close();
			}
		}
	}
}
