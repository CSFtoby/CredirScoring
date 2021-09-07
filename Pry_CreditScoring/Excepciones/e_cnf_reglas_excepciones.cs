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
	public partial class e_cnf_reglas_excepciones : Form
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

		public e_cnf_reglas_excepciones()
		{
			InitializeComponent();
		}

		private void panel1_MouseDown(object sender, MouseEventArgs e)
		{
			this.moverForm();
		}

		private void e_cnf_reglas_excepciones_Load(object sender, EventArgs e)
		{
			this.cargar_reglas_excepciones();
			this.get_record();
		}

		private void cargar_reglas_excepciones()
		{
			this.Cursor = Cursors.WaitCursor;
			try
			{
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				string sql = @"select r.no_condicion, nvl(r.codigo_lineamiento,'No aplica') codigo_lineamiento, 
                              nvl(r.cod_tipo_excepcion,'No aplica') cod_tipo_excepcion, nvl(r.cod_tipo_excep_prohibida,'No aplica') cod_tipo_excep_prohibida,
                              nvl(r.cod_linea_prohibida,'No aplica') cod_linea_prohibida,r.cod_tipo_condicion, c.tipo_condicion, r.estado
                              from excp.dcs_exc_reglas_excepciones r, excp.dcs_exc_tipo_condicion c
                              where r.cod_tipo_condicion = c.cod_tipo_condicion ";

				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;
				OracleDataReader dr = cmd.ExecuteReader();
				DataTable tabla = new DataTable();

				tabla.Columns.Add("no_condicion");
				tabla.Columns.Add("codigo_lineamiento");
				tabla.Columns.Add("cod_tipo_excepcion");
				tabla.Columns.Add("cod_tipo_excep_prohibida");
				tabla.Columns.Add("cod_linea_prohibida");
				tabla.Columns.Add("cod_tipo_condicion");
				tabla.Columns.Add("tipo_condicion");
				tabla.Columns.Add("estado");

				while (dr.Read())
				{
					string estadoTmp = (dr["estado"].ToString() == "S") ? "Activo" : "Inactivo";
					tabla.Rows.Add(
						dr["no_condicion"].ToString(),
						dr["codigo_lineamiento"].ToString(),
						dr["cod_tipo_excepcion"].ToString(),
						dr["cod_tipo_excep_prohibida"].ToString(),
						dr["cod_linea_prohibida"].ToString(),
						dr["cod_tipo_condicion"].ToString(),
						dr["tipo_condicion"].ToString(),
						estadoTmp
						);
				}

				this.dgvReglas.DataSource = tabla;
				this.dgvReglas.Refresh();
				dr.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void btnRefrescar_Click(object sender, EventArgs e)
		{
			this.get_record();
			this.cargar_reglas_excepciones();
			this.p_go_record_excep();
		}

		private void panel1_Paint(object sender, PaintEventArgs e)
		{
			this.moverForm();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void get_record()
		{
			DataGridViewRow row = this.dgvReglas.CurrentRow;
			if (this.dgvReglas.Rows.IndexOf(row) >= 0)
				vl_record_grid = this.dgvReglas.Rows.IndexOf(row);
			else
				vl_record_grid = 0;
			Label_currentrow.Text = vl_record_grid.ToString() + "/" + this.dgvReglas.RowCount.ToString();
		}

		private void p_go_record_excep()
		{
			if (this.dgvReglas.RowCount > 0) //Tiene que tener registros
			{
				if (this.dgvReglas.RowCount > vl_record_grid)
				{
					this.dgvReglas.CurrentCell = this.dgvReglas[0, vl_record_grid];
				}
				else
				{
					if (this.dgvReglas.RowCount == vl_record_grid)
					{
						this.dgvReglas.CurrentCell = this.dgvReglas[0, this.dgvReglas.RowCount - 1];
					}
					if (this.dgvReglas.RowCount == 0)
					{

					}
				}
			}
		}

		private void btnAgregar_Click(object sender, EventArgs e)
		{
			e_cnf_agregar_regla regla = new e_cnf_agregar_regla(0, u_Globales.accionAgregar);
			regla.ShowDialog();
		}
	}
}
