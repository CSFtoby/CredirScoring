using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using wfcModel;
using Microsoft.Reporting.WinForms;

namespace Docsis_Application.FrmsRpts
{
    public partial class frmRpt_HojaRuta : Form
    {
        public string vl_enter = "\r\n";
        bool con_borde = MDI_Menu.con_borde;
        public Int32 gno_solicitud = 0;
        public DataAccess da;

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

        public frmRpt_HojaRuta()
        {
            InitializeComponent();
        }
        private void frmRpt_HojaRuta_Load(object sender, EventArgs e)
        {
            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = ZoomMode.Percent;
            reportViewer1.ZoomPercent = 100; //Seleccionamos el zoom que deseamos utilizar. En este caso un 100%
            mostrar_reporte();
            this.reportViewer1.RefreshReport();
        }
        private void mostrar_reporte()
        {
            try
            {
                
                DataTable dt = da.p_datos_HojaRuta(gno_solicitud);                
                ReportDataSource rds = new ReportDataSource("dataset1", dt);                
                this.reportViewer1.LocalReport.DataSources.Add(rds);


                DataTable dt2 = da.p_datos_HojaRuta2(gno_solicitud);
                ReportParameter pa1 = new ReportParameter("pa_id_cliente", dt2.Rows[0]["numero_identificacion"].ToString());
                ReportParameter pa2 = new ReportParameter("pa_nombre_afiliado", dt2.Rows[0]["nombre_completo"].ToString());
                ReportParameter pa3 = new ReportParameter("pa_codigo_cliente", dt2.Rows[0]["codigo_cliente"].ToString());
                ReportParameter pa4 = new ReportParameter("pa_monto_solicitado", dt2.Rows[0]["monto_solicitado"].ToString());
                ReportParameter pa5 = new ReportParameter("pa_tasa", dt2.Rows[0]["tasa"].ToString());
                ReportParameter pa6 = new ReportParameter("pa_plazo", dt2.Rows[0]["plazo"].ToString());
                ReportParameter pa7 = new ReportParameter("pa_producto", dt2.Rows[0]["producto"].ToString());
                ReportParameter pa8 = new ReportParameter("pa_no_solicitud", dt2.Rows[0]["no_solicitud"].ToString());
                reportViewer1.LocalReport.ReportEmbeddedResource = "Docsis_Application.Reportes.rptHojaRutas.rdlc";

                this.reportViewer1.LocalReport.SetParameters(pa1);
                this.reportViewer1.LocalReport.SetParameters(pa2);
                this.reportViewer1.LocalReport.SetParameters(pa3);
                this.reportViewer1.LocalReport.SetParameters(pa4);
                this.reportViewer1.LocalReport.SetParameters(pa5);
                this.reportViewer1.LocalReport.SetParameters(pa6);
                this.reportViewer1.LocalReport.SetParameters(pa7);
                this.reportViewer1.LocalReport.SetParameters(pa8);
                this.reportViewer1.LocalReport.Refresh();
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en reporte " + ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            if (FormWindowState.Normal == WindowState)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }
    }

    public class rtp_HojaRuta_cls
    {
        public Int32 no_solicitud { get; set; }
        public string codigo_cliente { get; set; }
        public string nombre_completo { get; set; }
        public string producto { get; set; }
        public decimal monto_solicitado { get; set; }
        public string tasa_sol { get; set; }
        public string plazo_sol { get; set; }
        public decimal monto_aprobado { get; set; }
        public decimal tasa_aprobada { get; set; }
        public int plazo_aprobado { get; set; }        
        public Int32 no_movimiento { get; set; }
        public string enviado_por { get; set; }
        public DateTime fecha_envio { get; set; }
        public string nombre_from { get; set; }
        public string nombre_to { get; set; }
        public string decision { get; set; }
        public int antiguedad_meses { get; set; }
        public int antiguedad_dias { get; set; }
        public int antiguedad_horas { get; set; }
        public int antiguedad_minutos { get; set; }
        public int antiguedad_segundos { get; set; }
        public string estadia_movimiento { get; set; }
    }
}
