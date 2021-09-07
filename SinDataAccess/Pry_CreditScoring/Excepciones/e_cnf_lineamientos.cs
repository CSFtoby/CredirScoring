using Docsis_Application.Excepciones;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using wfcModel;

namespace Docsis_Application.Excepciones
{
	public partial class e_cnf_lineamientos : Form
	{
		DataAccess da;
		int vl_workflow_id = 0;
		int vl_record_grid = 0;
		bool con_borde = MDI_Menu.con_borde;

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


		#region
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

		private void moverForm()
		{
			ReleaseCapture();
			SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
		}

		public e_cnf_lineamientos()
		{
			InitializeComponent();
		}

		private void excepcionesToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void e_cnf_lineamientos_Load(object sender, EventArgs e)
		{
			this.p_cargar_lineamientos();
		}

		private void panel1_MouseDown(object sender, MouseEventArgs e)
		{
			this.moverForm();
		}

		private void p_cargar_lineamientos()
		{
			this.Cursor = Cursors.WaitCursor;
			try
			{
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				string sql = @"select codigo_lineamiento,nombre,descripcion,estado from excp.DCS_EXC_LINEAMIENTOS ";

				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;
				OracleDataReader dr = cmd.ExecuteReader();
				DataTable tabla = new DataTable("lineamientos");

				tabla.Columns.Add("foto", System.Type.GetType("System.Byte[]"));
				tabla.Columns.Add("codigo_lineamiento");
				tabla.Columns.Add("nombre");
				tabla.Columns.Add("descripcion");
				tabla.Columns.Add("estado");

				while (dr.Read())
				{
					string estadoTmp = (dr["estado"].ToString() == "S") ? "Activo" : "Inactivo";
					tabla.Rows.Add(
						null, 
						dr["codigo_lineamiento"].ToString(), 
						dr["nombre"].ToString(), 
						dr["descripcion"].ToString(), 
						estadoTmp
						);
				}

				this.dgvLineamientos.DataSource = tabla;
				this.dgvLineamientos.Refresh();
				dr.Close();					
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			this.Cursor = Cursors.Default;
		}

		private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{

		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnAgregar_Click(object sender, EventArgs e)
		{
			e_cnf_agregar_lineamiento nuevo = new e_cnf_agregar_lineamiento(string.Empty, u_Globales.accionAgregar);
			nuevo.ShowDialog();
		}

		private void get_record()
		{
			DataGridViewRow row = this.dgvLineamientos.CurrentRow;
			if (this.dgvLineamientos.Rows.IndexOf(row) >= 0)
				vl_record_grid = this.dgvLineamientos.Rows.IndexOf(row);
			else
				vl_record_grid = 0;
			Label_currentrow.Text = vl_record_grid.ToString() + "/" + this.dgvLineamientos.RowCount.ToString();
		}

		private void label2_Click(object sender, EventArgs e)
		{

		}

		private void btnRefrescar_Click(object sender, EventArgs e)
		{
			this.p_cargar_lineamientos();
		}

		private void btnModificar_Click(object sender, EventArgs e)
		{
			this.get_record();
			DataGridViewRow row = this.dgvLineamientos.CurrentRow;

			if(row != null)
			{
				string codigo = row.Cells["codigo_lineamiento"].Value.ToString();
				e_cnf_agregar_lineamiento mod = new e_cnf_agregar_lineamiento(codigo, u_Globales.accionModificar);
				mod.ShowDialog();
			}
		}

		private void btnEliminar_Click(object sender, EventArgs e)
		{
			this.get_record();
			DataGridViewRow row = this.dgvLineamientos.CurrentRow;

			if(row != null)
			{
				string codigo = row.Cells["codigo_lineamiento"].Value.ToString();
				e_cnf_agregar_lineamiento mod = new e_cnf_agregar_lineamiento(codigo, u_Globales.accionEliminar);
				mod.ShowDialog();
			}
		}
	}
}
