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
	public partial class frmRpt_Excepciones : Form
	{
		public DataAccess da;
		delegate void FunctionRpt1();
		Thread myThread;
		//string ruta_generacion = @"c:\CreditScoringTmp\";
		int codigoEstado = 0;
		int codigoRegion = 0;
		int codigoFilial = 0;
		int codigoNivel = 0;
		int codigoProducto = 0;
		int tipoReporte = 0;
		string tipo_pago = string.Empty;
		DataTable fondos = new DataTable();
		SaveFileDialog fichero = new SaveFileDialog();
		string[] EncabezadoTiempos = { "CODIGO CLIENTE", "NOMBRE CLIENTE", "CODIGO EXCEPCION", "NO SOLICITUD", "CONDICION TU", "PRODUCTO", "FECHA PRESENTACION", "PAGO MEDIANTE",
									   "DESTINO", "OFICIAL DE SERVICIO", "AGENCIA ORIGEN", "ZONA", "ESTADO", "ANTIGUEDAD MESES", "ANTIGUEDAD DIAS", "ANTIGUEDAD HORAS", "ANTIGUEDAD MINUTOS", "ANTIGUEDAD SEGUNDOS"};
		string[] EncabezadoResoluciones = { "CODIGO CLIENTE", "NOMBRE CLIENTE", "CODIGO_EXCEPCION", "NO SOLICITUD", "CONDICION TU", "PRODUCTO", "FECHA PRESENTACION", "PAGO MEDIANTE",
											"DESTINO", "OFICIAL DE SERVICIO", "AGENCIA ORIGEN", "ZONA", "ESTADO", "ANTIGUEDAD MESES", "ANTIGUEDAD DIAS", "ANTIGUEDAD HORAS", "ANTIGUEDAD MINUTOS", "ANTIGUEDAD SEGUNDOS",
											"FIGURA RESOLUCION", "MOTIVO APROBACION"};
		string[] EncabezadoDetalle = { "CODIGO CLIENTE", "NOMBRE CLIENTE", "CODIGO EXCEPCION", "NO SOLICITUD", "CONDICION TU", "PRODUCTO", "FECHA PRESENTACION", "PAGO MEDIANTE",
									   "DESTINO", "OFICIAL SERVICIOS", "AGENCIA ORIGEN", "ZONA", "ESTADO", "COD TIPO EXCEPCION", "DESCRIPCION", "JUSTIFICACION"};

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

		private void panelTop_MouseDown(object sender, MouseEventArgs e)
		{
			moverForm();
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
				if (MDI_Menu.con_borde)
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

		public frmRpt_Excepciones(DataAccess da)
		{
			InitializeComponent();
			this.da = da;
			this.llenar_combo_Regionales();
			this.llenar_Estados_excepcion();
			this.llenar_nivel_resolutivo();
			this.llenar_combo_pagos();
			this.llenar_fondos();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		#region Cargas

		private void llenar_combo_Regionales()
		{
			try
			{
				DataTable dtRegionales = da.ObtenerRegionales();
				cmbRegionales.DataSource = dtRegionales;
				cmbRegionales.DisplayMember = "descripcion";
				cmbRegionales.ValueMember = "codigo_zona";
				cmbRegionales.SelectedIndex = -1;
			}
			catch (Exception ex)
			{
				MessageBox.Show($"{ex.TargetSite}: ({ex.InnerException}) {ex.Message}");
			}
		}

		private void llenar_Estados_excepcion()
		{
			try
			{
				DataTable dtEstados = this.da.p_obtener_Estados_Excepcion();
				this.cmbEstados.DataSource = dtEstados;
				this.cmbEstados.DisplayMember = "DESCRIPCION";
				this.cmbEstados.ValueMember = "ESTADO_EXCEP_ID";
				this.cmbEstados.SelectedIndex = -1;
			}
			catch (Exception ex)
			{
				MessageBox.Show($"{ex.TargetSite}: ({ex.InnerException}) {ex.Message}");
			}
		}

		private void llenar_nivel_resolutivo()
		{
			try
			{
				DataTable dtNiveles = this.da.p_niveles_resolutivos();
				this.cmbNiveles.DataSource = dtNiveles;
				this.cmbNiveles.DisplayMember = "nombre";
				this.cmbNiveles.ValueMember = "estacion_id";
				this.cmbNiveles.SelectedIndex = -1;
			}
			catch (Exception ex)
			{
				MessageBox.Show($"{ex.TargetSite}: ({ex.InnerException}) {ex.Message}");
			}
		}

		private void llenar_combos_filiales()
		{
			string p_regional = this.cmbRegionales.SelectedValue.ToString();
			int ejemplo = 0;
			DataTable dtFiliales = null;
			if (!int.TryParse(p_regional, out ejemplo))
			{
				dtFiliales = da.ObtenerFiliales();
			}
			else
			{
				dtFiliales = da.ObtenerFiliales(p_regional);
			}

			cmbFiliales.DataSource = dtFiliales;
			cmbFiliales.DisplayMember = "nombre_agencia";
			cmbFiliales.ValueMember = "codigo_agencia";
			cmbFiliales.SelectedIndex = -1;
		}

		private void llenar_fondos()
		{
			fondos = da.ObtenerFuentesFondos();
			this.cmbFondos.DataSource = fondos;
			this.cmbFondos.DisplayMember = "descripcion_fuente";
			this.cmbFondos.ValueMember = "codigo_fuente";
		}

		private void llenar_combo_pagos()
		{
			try
			{
				var pagos = this.da.get_tipo_pagos();
				this.cmbTipoPagos.DataSource = pagos;
				this.cmbTipoPagos.DisplayMember = "descripcion";
				this.cmbTipoPagos.ValueMember = "tipo_id";
				this.cmbTipoPagos.SelectedIndex = -1;
			}
			catch (Exception)
			{

				throw;
			}
		}

		#endregion

		private void cmbRegionales_SelectionChangeCommitted(object sender, EventArgs e)
		{
			codigoRegion = Convert.ToInt32(this.cmbRegionales.SelectedValue.ToString());
			this.llenar_combos_filiales();
		}

		private void btnExportar_Click(object sender, EventArgs e)
		{
			fichero.Filter = "Excel (*.xls)|*.xls";
			if (this.rbnResoluciones.Checked)
			{
				this.tipoReporte = (int)TipoReporteExcepcion.RptResoluciones;
			}
			else if (this.rbnTiempos.Checked)
			{
				this.tipoReporte = (int)TipoReporteExcepcion.RptTiemposFinales;
			}
			else if (this.rbnDetalle.Checked)
			{
				this.tipoReporte = (int)TipoReporteExcepcion.RptDetalle;
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
			DataTable datatable = da.generar_reporte_excepciones(codigoEstado, dpDesde.Value, dpHasta.Value, codigoRegion, codigoFilial,
																 this.txtCodigoCliente.Text, this.tipoReporte, this.codigoProducto, this.tipo_pago);

			this.gvReporte1.DataSource = null;
			this.gvReporte1.DataSource = datatable;

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
					if (i == 0)
					{
						switch (this.tipoReporte)
						{
							case (int)TipoReporteExcepcion.RptResoluciones:
								hoja_trabajo.Cells[i + 1, j + 1] = EncabezadoResoluciones[j];
								break;
							case (int)TipoReporteExcepcion.RptTiemposFinales:
								hoja_trabajo.Cells[i + 1, j + 1] = EncabezadoTiempos[j];
								break;
							case (int)TipoReporteExcepcion.RptDetalle:
								hoja_trabajo.Cells[i + 1, j + 1] = EncabezadoDetalle[j];
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

		private void cmbFiliales_SelectionChangeCommitted(object sender, EventArgs e)
		{
			codigoFilial = Convert.ToInt32(this.cmbFiliales.SelectedValue.ToString());
		}

		private void cmbNiveles_SelectionChangeCommitted(object sender, EventArgs e)
		{
			codigoNivel = Convert.ToInt32(this.cmbNiveles.SelectedValue.ToString());
		}

		private void cmbEstados_SelectionChangeCommitted(object sender, EventArgs e)
		{
			codigoEstado = Convert.ToInt32(this.cmbEstados.SelectedValue.ToString());
		}

		private void cmbFondos_SelectionChangeCommitted(object sender, EventArgs e)
		{
			string fondoId = this.cmbFondos.SelectedValue.ToString();
			string cod_fondo_mg = "CSF";
			if (fondos.Rows.Count > 0)
			{
				for (int i = 0; i < fondos.Rows.Count; i++)
				{
					if (fondos.Rows[i]["codigo_fuente"].ToString().Equals(fondoId))
					{
						cod_fondo_mg = fondos.Rows[i]["cod_fondo_mg"].ToString();
						break;
					}
				}
			}
			var dt = da.ObtenerSubAplicaciones("MCR", cod_fondo_mg);
			this.cmbProductos.DataSource = dt;
			this.cmbProductos.DisplayMember = "desc_sub_aplicacion";
			this.cmbProductos.ValueMember = "codigo_sub_aplicacion";
		}

		private void cmbProductos_SelectionChangeCommitted(object sender, EventArgs e)
		{
			this.codigoProducto = Convert.ToInt32(this.cmbProductos.SelectedValue.ToString());
		}

		private void cmbTipoPagos_SelectionChangeCommitted(object sender, EventArgs e)
		{
			this.tipo_pago = this.cmbTipoPagos.SelectedValue.ToString();
		}
	}
}
