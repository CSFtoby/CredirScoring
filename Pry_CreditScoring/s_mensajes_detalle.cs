using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OracleClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using wfcModel;

namespace Docsis_Application
{
    public partial class s_mensajes_detalle : Form
    {
		DataAccess da;
        DataTable mensajes = new DataTable();
		//DocSys.vl_user

		public s_mensajes_detalle(DataAccess _da)
        {
            InitializeComponent();

			this.mensajes.Columns.Add("no_entrada");
			this.mensajes.Columns.Add("agencia_from");
			this.mensajes.Columns.Add("estacion_from");
			this.mensajes.Columns.Add("usuario_from");
			this.mensajes.Columns.Add("tipoMensaje");
			this.mensajes.Columns.Add("leido");
			this.mensajes.Columns.Add("nuevo");
			this.mensajes.Columns.Add("codigo");

			this.da = _da;
        }

        private void s_mensajes_detalle_Load(object sender, EventArgs e)
        {
			this.mensajes = this.da.GetMensajes(DocSys.vl_user);
			this.gv_avisos.DataSource = null;
			this.gv_avisos.DataSource = this.mensajes;
        }

		private void gv_avisos_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (this.gv_avisos.CurrentCell.ColumnIndex.Equals(7) && e.RowIndex != -1)
				{
					var valor = this.gv_avisos.CurrentCell.Value;

					if (valor.Equals("N"))
					{
						this.gv_avisos.CurrentCell.Value = "S";
					}
					else
					{
						this.gv_avisos.CurrentCell.Value = "N";
					}
				}
			}
			catch (Exception)
			{
			}
		}

		private void s_mensajes_detalle_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				foreach (DataGridViewRow row in gv_avisos.Rows)
				{
					int noEntrada = Convert.ToInt32(row.Cells["no_entrada"].Value.ToString());
					string leido = row.Cells["leido"].Value.ToString();

					if (leido.Equals("S"))
					{
						this.da.ActualizarMensajes(noEntrada, leido);
					}
				}
			}
			catch (Exception)
			{
			}
		}
	}
}
