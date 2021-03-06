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
    public partial class frmRpt_Prestamos_observaciones : Form
    {
        public string vl_enter = "\r\n";
        bool con_borde = MDI_Menu.con_borde;
        public Int32 gno_solicitud = 0;
        public string no_solicitud;
        public DataAccess da;

        #region Mover
        const int WM_SYSCOMMAND = 0x112;
        const int MOUSE_MOVE = 0xF012;
        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }
        //
        // Declaraciones del API
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        //
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        //
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

        public frmRpt_Prestamos_observaciones(DataAccess da)
        {
            InitializeComponent();
            this.da = da;
        }
        private void frmRpt_Prestamos_observaciones_Load(object sender, EventArgs e)
        {
            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = ZoomMode.PageWidth;
            //reportViewer1.ZoomPercent = 100; //Seleccionamos el zoom que deseamos utilizar. En este caso un 100%
            mostrar_reporte();
            this.reportViewer1.RefreshReport();
        }
        private void mostrar_reporte()
        {

            try
            {
                DataTable dt = da.p_datos_PrestamosObserva(gno_solicitud);
                ReportDataSource rds = new ReportDataSource("dataset1", dt);
                this.reportViewer1.LocalReport.DataSources.Add(rds);
                reportViewer1.LocalReport.ReportEmbeddedResource = "Docsis_Application.Reportes.rptPrestamosObserva.rdlc";
                this.reportViewer1.LocalReport.Refresh();
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en reporte " + ex.Message);
            }
        }
        public List<rtp_Prestamos_observ_cls> metodo()
        {
            return new List<rtp_Prestamos_observ_cls>();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }
    }

    public class rtp_Prestamos_observ_cls
    {
        public Int32 no_solicitud { get; set; }
        public string numero_identificacion { get; set; }        
        public string codigo_cliente { get; set; }
        public string nombre_completo { get; set; }
        public string producto { get; set; }
        public decimal monto_solicitado { get; set; }
        public string tasa_sol { get; set; }
        public string plazo_sol { get; set; }
        public string usuario_analista { get; set; }
        public string nombre_analista { get; set; }
        public string observaciones_prestamos { get; set; }
        public string codigo_agencia_origen { get; set; }
        public string nombre_agencia { get; set; }     
    }
}
