using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wfcModel;

namespace Docsis_Application.FrmsRpts
{
	public partial class DocMinisterioPublico : Form
	{
		private DataAccess da;
		private int NoSolicitud;
		private int Agencia;
        private bool anio_siguiente;

		public DocMinisterioPublico(int no_solicitud, DataAccess _da, int agencia, bool anio)
		{
			InitializeComponent();
			this.da = _da;
			this.NoSolicitud = no_solicitud;
			this.Agencia = agencia;
            this.anio_siguiente = anio;
		}

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

		private void DocMinisterioPublico_Load(object sender, EventArgs e)
		{
			this.rptDoc.SetDisplayMode(DisplayMode.PrintLayout);
			this.rptDoc.ZoomMode = ZoomMode.Percent;
			this.rptDoc.PageCountMode = PageCountMode.Actual;
			this.rptDoc.ZoomPercent = 100; //Seleccionamos el zoom que deseamos utilizar. En este caso un 100%
			GenerarDocumento();
			this.rptDoc.RefreshReport();			
		}

		private void GenerarDocumento()
		{
			try
			{
				var datosDocumento = this.da.GetDocMinisterioPublico(this.NoSolicitud, this.Agencia);
				ReportParameter pa_nombre = new ReportParameter("pa_nombre", datosDocumento.Rows[0]["nombre_completo"].ToString());
				ReportParameter pa_estado_civil = new ReportParameter("pa_estado_civil", datosDocumento.Rows[0]["estado_civil"].ToString());
				ReportParameter pa_profesion = new ReportParameter("pa_profesion", datosDocumento.Rows[0]["profesion"].ToString());
				ReportParameter pa_identidad = new ReportParameter("pa_identidad", datosDocumento.Rows[0]["numero_identificacion"].ToString());
				ReportParameter pa_no_afiliacion = new ReportParameter("pa_no_afiliacion", datosDocumento.Rows[0]["codigo_cliente"].ToString());
				int anio = DateTime.Today.Year;

                if (anio_siguiente == true) {
                    anio++;
                }

				//if (DateTime.Today.Month == 12)
				//	anio++;
				ReportParameter pa_anio = new ReportParameter("pa_anio", anio.ToString());
				var monto_solicitado = Convert.ToDecimal(datosDocumento.Rows[0]["MONTO_SOLICITADO"].ToString());
				ReportParameter pa_valor_lempiras = new ReportParameter("pa_valor_lempiras", monto_solicitado.ToString("0,0.00", CultureInfo.InvariantCulture));
				Convertir obj = new Convertir();
				string valorEnLetras = obj.enletras(datosDocumento.Rows[0]["MONTO_SOLICITADO"].ToString());
				ReportParameter pa_valor_letras = new ReportParameter("pa_valor_letras", valorEnLetras);
				ReportParameter pa_mes = new ReportParameter("pa_mes", this.MonthName(DateTime.Today.Month));
				ReportParameter pa_dias = new ReportParameter("pa_dias", DateTime.Today.Day.ToString());
				ReportParameter pa_ciudad = new ReportParameter("pa_ciudad", datosDocumento.Rows[0]["ciudad"].ToString());

				this.rptDoc.LocalReport.SetParameters(pa_nombre);
				this.rptDoc.LocalReport.SetParameters(pa_estado_civil);
				this.rptDoc.LocalReport.SetParameters(pa_profesion);
				this.rptDoc.LocalReport.SetParameters(pa_identidad);
				this.rptDoc.LocalReport.SetParameters(pa_no_afiliacion);
				this.rptDoc.LocalReport.SetParameters(pa_anio);
				this.rptDoc.LocalReport.SetParameters(pa_valor_lempiras);
				this.rptDoc.LocalReport.SetParameters(pa_valor_letras);
				this.rptDoc.LocalReport.SetParameters(pa_mes);
				this.rptDoc.LocalReport.SetParameters(pa_dias);
				this.rptDoc.LocalReport.SetParameters(pa_ciudad);

				//this.rptDoc.LocalReport.ReportEmbeddedResource = "Docsis_Application.Reportes.AutorizacionMinisterioPublico.rdlc";
				this.rptDoc.LocalReport.Refresh();
				this.rptDoc.RefreshReport();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error en reporte " + ex.Message);
			}
		}

		public string MonthName(int month)
		{
			System.Globalization.DateTimeFormatInfo dtinfo = new System.Globalization.CultureInfo("es-HN", false).DateTimeFormat;
			return dtinfo.GetMonthName(month);
		}

		private void panelTop_MouseDown(object sender, MouseEventArgs e)
		{
			moverForm();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
