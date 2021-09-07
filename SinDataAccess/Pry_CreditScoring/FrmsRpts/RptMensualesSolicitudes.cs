using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using wfcModel;

namespace Docsis_Application.FrmsRpts
{
	public partial class RptMensualesSolicitudes : Form
	{
		DataAccess da;
		delegate void FunctionRpt1();
		Thread myThread;
		//string ruta_generacion = @"c:\CreditScoringTmp\";
		string condicion = string.Empty;
		int tipoReporte = 0;
		SaveFileDialog fichero = new SaveFileDialog();
		string ruta_generacion = @"c:\CreditScoringTmp\";
		string[] EncabezadoComite = { "NO. SOLICITUD", "MONTO SOLICITADO", "FECHA PRESENTACION", "MONTO APROBADO", "FECHA RESOLUCION", "COMITE ID", "COMITE RESOLUTIVO" };
		string[] EncabezadoAprobados = { "NO SOLICITUD", "FECHA PRESENTACION", "MONTO SOLICITADO", "CODIGO CLIENTE", "NOMBRES", "MONTO APROBADO", "CODIGO PRODUCTO", "PRODUCTO", "ZONA", "AGENCIA ORIGEN", "NOMBRE AGENCIA", "ESTACION ID", "NOMBRE ESTACION", "ESTADO SOLICITUD" };
		string[] EncabezadoNucleo = { "NO SOLICITUD", "FECHA_PRESENTACION", "CODIGO_CLIENTE", "NOMBRE", "MONTO SOLICITADO", "INDICADOR IMPLICADO", "TOTAL CAPITAL VIGENTE GRPFAM", "TOTAL CAPRITAL VIGENTE SOLIC", "TOTAL", "PATRIMONIO CSF", "PORCENTAJE CONCENTRACION", "LIMITE INDICADOR", "RESULTADO EVALUACION" };


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
		private void moverForm()
		{
			ReleaseCapture();
			SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
		}

		private void panelTop_MouseDown(object sender, MouseEventArgs e)
		{
			moverForm();
		}
		#endregion

		public RptMensualesSolicitudes(DataAccess _da)
		{
			InitializeComponent();
			this.da = _da;
			this.llenar_combo_Regionales();
			this.p_llenar_combo_estaciones();
			this.p_llenar_combo_estados();
		}

		private void cmbRegionales_SelectionChangeCommitted(object sender, EventArgs e)
		{
			//codigoRegion = Convert.ToInt32(this.cmbRegionales.SelectedValue.ToString());
			this.llenar_combos_filiales();
		}

		private void llenar_combos_filiales()
		{
			//string p_regional = this.cmbRegionales.SelectedValue.ToString();
			//int ejemplo = 0;
			//DataTable dtFiliales = null;
			//if (!int.TryParse(p_regional, out ejemplo))
			//{
			//	dtFiliales = da.ObtenerFiliales();
			//}
			//else
			//{
			//	dtFiliales = da.ObtenerFiliales(p_regional);
			//}

			//cmbFiliales.DataSource = dtFiliales;
			//cmbFiliales.DisplayMember = "nombre_agencia";
			//cmbFiliales.ValueMember = "codigo_agencia";
			//cmbFiliales.SelectedIndex = -1;
		}

		private void llenar_combo_Regionales()
		{
			try
			{
				DataTable dtRegionales = da.ObtenerRegionales();
				//cmbRegionales.DataSource = dtRegionales;
				//cmbRegionales.DisplayMember = "descripcion";
				//cmbRegionales.ValueMember = "codigo_zona";
				//cmbRegionales.SelectedIndex = -1;
			}
			catch (Exception ex)
			{
				MessageBox.Show($"{ex.TargetSite}: ({ex.InnerException}) {ex.Message}");
			}
		}

		private void cmbFiliales_SelectionChangeCommitted(object sender, EventArgs e)
		{
			//codigoFilial = Convert.ToInt32(this.cmbFiliales.SelectedValue.ToString());
		}

		private void p_llenar_combo_estados()
		{
			try
			{
				DataTable dtestados_sol = da.ObtenerEstadoSolicitud();

				//cmbEstadoSolic.DataSource = dtestados_sol;
				//cmbEstadoSolic.DisplayMember = "descripcion";
				//cmbEstadoSolic.ValueMember = "estado_id";
				//cmbEstadoSolic.SelectedIndex = -1;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error  " + ex.TargetSite + " " + ex.Message);
			}
		}

		private void p_llenar_combo_estaciones()
		{
			try
			{
				DataTable dtestados_sol = da.ObtenerListadeEstaciones();
				//cmbEstaciones.DataSource = dtestados_sol;
				//cmbEstaciones.DisplayMember = "nombre";
				//cmbEstaciones.ValueMember = "estacion_id";
				//cmbEstaciones.SelectedIndex = -1;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error  " + ex.TargetSite + " " + ex.Message);
			}
		}

		private void cmbEstadoSolic_SelectionChangeCommitted(object sender, EventArgs e)
		{
			//codigoEstado = int.Parse(this.cmbEstadoSolic.SelectedValue.ToString());
		}

		private void cmbEstaciones_SelectionChangeCommitted(object sender, EventArgs e)
		{
			//codigoEstacion = int.Parse(this.cmbEstaciones.SelectedValue.ToString());
		}

		private void btnExportar_Click(object sender, EventArgs e)
		{
			this.condicion = string.Empty;
			if (this.rbnAprobadas.Checked)
			{
				this.tipoReporte = (int)TipoReporteSolicitud.RptAprobProceso;
				this.condicion = " and fecha_presentacion>= to_date('" + this.dpFecha1.Value.ToShortDateString() + "','dd/mm/yyyy') ";
			}
			else if (this.rbnComite.Checked)
			{
				this.tipoReporte = (int)TipoReporteSolicitud.RptComiteResolucion;
				this.condicion = " to_date('" + this.dpFecha1.Value.ToShortDateString() + "','dd/mm/yyyy') and to_date('" + this.dpFecha2.Value.ToShortDateString() + "','dd/mm/yyyy') ";
			}
			else if (this.rbnNucleo.Checked)
			{
				this.tipoReporte = (int)TipoReporteSolicitud.RptNucleFamiliar;
				this.condicion = " to_date('" + this.dpFecha1.Value.ToShortDateString() + "','dd/mm/yyyy') and to_date('" + this.dpFecha2.Value.ToShortDateString() + "','dd/mm/yyyy') ";
			}

			if (fichero.ShowDialog() == DialogResult.OK)
			{
				myThread = new Thread(new ThreadStart(exportExcel));
				myThread.Start();
			}
		}

		private void exportExcel()
		{
			this.Invoke(new FunctionRpt1(delegate ()
			{
				panelWait.Visible = true;
				btnClose.Enabled = false;
			}));
			CheckForIllegalCrossThreadCalls = false;

			DataTable reporte = new DataTable();

			switch (this.tipoReporte)
			{
				case (int)TipoReporteSolicitud.RptAprobProceso:
					{
						reporte = this.da.GetProcesoAprobadas(this.condicion);
					}
					break;
				case (int)TipoReporteSolicitud.RptComiteResolucion:
					{
						reporte = this.da.GetAprobadosComite(this.condicion);
					}
					break;
				case (int)TipoReporteSolicitud.RptNucleFamiliar:
					{
						reporte = this.da.GetNucleoFamiliar(this.condicion);
					}
					break;
				default:
					break;
			}

			this.gvReporte1.DataSource = null;
			this.gvReporte1.DataSource = reporte;

			Microsoft.Office.Interop.Excel.Application aplicacion;
			Microsoft.Office.Interop.Excel.Workbook libros_trabajo;
			Microsoft.Office.Interop.Excel.Worksheet hoja_trabajo;
			aplicacion = new Microsoft.Office.Interop.Excel.Application();
			libros_trabajo = aplicacion.Workbooks.Add();
			hoja_trabajo = (Microsoft.Office.Interop.Excel.Worksheet)libros_trabajo.Worksheets.get_Item(1);

			//Recorremos el DataGridView rellenando la hoja de trabajo
			for (int i = 0; i < this.gvReporte1.Rows.Count + 1; i++)
			{
				for (int j = 0; j < this.gvReporte1.Columns.Count; j++)
				{
					if( i == 0)
					{
						switch (this.tipoReporte)
						{
							case (int)TipoReporteSolicitud.RptAprobProceso:
								hoja_trabajo.Cells[i + 1, j + 1] = EncabezadoAprobados[j];
								break;
							case (int)TipoReporteSolicitud.RptComiteResolucion:
								hoja_trabajo.Cells[i + 1, j + 1] = EncabezadoComite[j];
								break;
							case (int)TipoReporteSolicitud.RptNucleFamiliar:
								hoja_trabajo.Cells[i + 1, j + 1] = EncabezadoNucleo[j];
								break;
							default:
								break;
						}
					}
					else
					{
						int tmpI = i;
						tmpI -= 1;
						hoja_trabajo.Cells[i + 1, j + 1] = this.gvReporte1.Rows[tmpI].Cells[j].Value.ToString();
					}
				}
			}

			libros_trabajo.SaveAs(fichero.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
			libros_trabajo.Close(true);
			aplicacion.Quit();

			this.Invoke(new FunctionRpt1(delegate ()
			{
				gvReporte1.ClearSelection();
				panelWait.Visible = false;
				btnClose.Enabled = true;
			}));

			p_abrir_archivo(fichero.FileName);
		}

		private void p_abrir_archivo(string p_archivo)
		{

			string sFile = p_archivo;

			try
			{
				ProcessStartInfo startInfo = new ProcessStartInfo();
				startInfo.FileName = "excel.exe";
				startInfo.Arguments = sFile;
				Process.Start(startInfo);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
