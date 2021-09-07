using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Docsis_Application.Excepciones
{
	public partial class ListadoAnalistas : Form
	{
		DataTable listaAnalistas = new DataTable();
		public string Usuario = string.Empty;
		public string NombreUsuario = string.Empty;

		public ListadoAnalistas(DataTable _analistas)
		{
			InitializeComponent();
			this.listaAnalistas = _analistas;
			this.lbxAnalistas.DataSource = this.listaAnalistas;
			this.lbxAnalistas.DisplayMember = "nombre";
			this.lbxAnalistas.ValueMember = "usuario";
		}

		private void lbxAnalistas_Click(object sender, EventArgs e)
		{			
		}

		private void btnMoverSolicitud_Click(object sender, EventArgs e)
		{			
			this.Usuario = this.lbxAnalistas.SelectedValue.ToString();
			this.Close();
		}
	}
}
