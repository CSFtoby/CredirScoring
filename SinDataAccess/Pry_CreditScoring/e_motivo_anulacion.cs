using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Docsis_Application
{
	public partial class e_motivo_anulacion : Form
	{
		public static string Motivo = string.Empty;
		public static bool Cancelo = false;

		public e_motivo_anulacion()
		{
			InitializeComponent();
		}

		private void btnAgregar_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(this.textBox1.Text))
			{
				MessageBox.Show("Debe agregar el motivo de anulación", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				Cancelo = true;
				return;
			}

			Motivo = this.textBox1.Text;
			Cancelo = false;
		}

		private void btnCancelar_Click(object sender, EventArgs e)
		{
			Cancelo = true;
			Motivo = string.Empty;
			this.Close();
		}
	}
}
