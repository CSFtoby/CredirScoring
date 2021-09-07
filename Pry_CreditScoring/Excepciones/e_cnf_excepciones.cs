using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wfcModel;

namespace Docsis_Application.Excepciones
{
	public partial class e_cnf_excepciones : Form
	{
		DataAccess da;
		int vl_workflow_id = 0;
		int vl_record_grid = 0;
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
		private void moverForm()
		{
			ReleaseCapture();
			SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
		}

		#endregion

		public e_cnf_excepciones()
		{
			InitializeComponent();
		}

		private void panel1_MouseDown(object sender, MouseEventArgs e)
		{
			this.moverForm();
		}

		private void opcionesToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void e_cnf_excepciones_Load(object sender, EventArgs e)
		{
			this.p_cargar_excepciones();
		}

		private void p_cargar_excepciones()
		{
			this.Cursor = Cursors.WaitCursor;
			try
			{
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				string sql = @"select e.cod_tipo_excepcion, e.tipo_excepcion, e.estado, e.codigo_lineamiento, l.nombre, l.descripcion 
                          from excp.dcs_exc_tipo_excepciones e, excp.DCS_EXC_LINEAMIENTOS l 
                          where e.codigo_lineamiento = l.codigo_lineamiento ";

				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;

				OracleDataReader dr = cmd.ExecuteReader();
				DataTable tabla = new DataTable();

				tabla.Columns.Add("cod_tipo_excepcion");
				tabla.Columns.Add("tipo_excepcion");
				tabla.Columns.Add("estado");
				tabla.Columns.Add("codigo_lineamiento");
				tabla.Columns.Add("nombre");
				tabla.Columns.Add("descripcion");

				while (dr.Read())
				{
					string estado = (dr["estado"].ToString() == "S") ? "Activo" : "Inactivo";
					tabla.Rows.Add(
						dr["cod_tipo_excepcion"].ToString(),
						dr["tipo_excepcion"].ToString(),
						estado,
						dr["codigo_lineamiento"].ToString(),
						dr["nombre"].ToString(),
						dr["descripcion"].ToString()
						);
				}

				this.dgvExcepciones.DataSource = tabla;
				this.dgvExcepciones.Refresh();

				dr.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnAgregar_Click(object sender, EventArgs e)
		{
			e_cnf_agregar_tipo_excepcion foma = new e_cnf_agregar_tipo_excepcion(string.Empty, u_Globales.accionAgregar);
			foma.ShowDialog();
		}

		private void btnRefrescar_Click(object sender, EventArgs e)
		{
			this.p_cargar_excepciones();
		}

		private void btnModificar_Click(object sender, EventArgs e)
		{
			this.get_record();
			DataGridViewRow row = this.dgvExcepciones.CurrentRow;

			if (row != null)
			{
				string codigo = row.Cells["cod_tipo_excepcion"].Value.ToString();
				e_cnf_agregar_tipo_excepcion mod = new e_cnf_agregar_tipo_excepcion(codigo, u_Globales.accionModificar);
				mod.ShowDialog();
			}
		}

		private void get_record()
		{
			DataGridViewRow row = this.dgvExcepciones.CurrentRow;
			if (this.dgvExcepciones.Rows.IndexOf(row) >= 0)
				vl_record_grid = this.dgvExcepciones.Rows.IndexOf(row);
			else
				vl_record_grid = 0;
			Label_currentrow.Text = vl_record_grid.ToString() + "/" + this.dgvExcepciones.RowCount.ToString();
		}

		private void btnEliminar_Click(object sender, EventArgs e)
		{
			this.get_record();
			DataGridViewRow row = this.dgvExcepciones.CurrentRow;

			if (row != null)
			{
				string codigo = row.Cells["cod_tipo_excepcion"].Value.ToString();
				e_cnf_agregar_tipo_excepcion mod = new e_cnf_agregar_tipo_excepcion(codigo, u_Globales.accionEliminar);
				mod.ShowDialog();
			}
		}
	}
}
