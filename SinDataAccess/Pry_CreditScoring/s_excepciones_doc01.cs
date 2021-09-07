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

namespace Docsis_Application
{
	public partial class s_excepciones_doc01 : Form
	{
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
		#endregion
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

		private void moverForm()
		{
			ReleaseCapture();
			SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
		}
		#endregion

		private DataAccess da;
		private int NoSolicitud;

		public s_excepciones_doc01(DataAccess da, int no_solicitud, string nombre_cliente, string producto, string monto)
		{
			InitializeComponent();
			this.da = da;
			this.NoSolicitud = no_solicitud;

			this.txtNoSolicitud.Text = this.NoSolicitud.ToString();
			this.txtCliente.Text = nombre_cliente;
			this.txtMonto.Text = monto;
			this.txtProducto.Text = producto;

			this.llenar_grid();
		}

		public void llenar_grid()
		{
			try
			{
				string sql = "select ex.codigo_excepcion, es.descripcion estado_excep, ex.fecha_presentacion, est.nombre estacion_actual,"
							+ "case when ex.pago_mediante = 'V' then 'Pago por ventanilla' else 'Pago por Planilla' end pago_mediante,"
							+ "ex.fecha_cierre "
							+ "from excp.dcs_excepcion_solicitud ex, excp.dcs_exc_estados_excepcion es, "
							+ "wfc.dcs_wf_estaciones est "
							+ "where ex.codigo_estado_excep = es.estado_excep_id "
							+ "and ex.no_solicitud = :no_solicitud "
							+ "and est.estacion_id = ex.estacion_id ";

				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				OracleParameter pa_no_excepcion = new OracleParameter("no_solicitud", OracleType.Number);
				pa_no_excepcion.Direction = ParameterDirection.Input;
				pa_no_excepcion.Value = this.NoSolicitud;

				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.Parameters.Add(pa_no_excepcion);

				DataTable dt = new DataTable();

				OracleDataAdapter _da = new OracleDataAdapter(cmd);
				_da.Fill(dt);

				this.gvExcepciones.DataSource = dt;
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ha ocurrido un error {ex.Message}", "Error");
			}
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

		private void gvExcepciones_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (this.gvExcepciones.RowCount > 0)
			{				
				DataGridViewRow row = this.gvExcepciones.CurrentRow;
				int no_excepcion = Convert.ToInt32(row.Cells["codigo_excepcion"].Value);

				if (no_excepcion != 0)
				{
					s_excepciones_doc02 detalle = new s_excepciones_doc02(this.da, no_excepcion);
					detalle.ShowDialog();
				}

			}
		}
	}
}
