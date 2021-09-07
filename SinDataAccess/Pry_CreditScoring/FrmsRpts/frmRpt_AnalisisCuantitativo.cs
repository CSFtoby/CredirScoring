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
    public partial class frmRpt_AnalisisCuantitativo : Form
    {
        public string vl_enter = "\r\n";
        bool con_borde = MDI_Menu.con_borde;
        public Int32 gno_solicitud = 0;
        public string no_solicitud;
        public DataAccess da;
        public DataTable dtDetalle_deduc;

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
        #endregion
        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }
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

        public frmRpt_AnalisisCuantitativo()
        {
            InitializeComponent();
        }
        private void frmRpt_AnalisisCuantitativo_Load(object sender, EventArgs e)
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
                DataTable dt= da.p_datos_AnalisisCuantitativo(gno_solicitud);                

                ReportDataSource rds = new ReportDataSource("dataset1", dt);
                ReportDataSource rds2 = new ReportDataSource("dataset2", dtDetalle_deduc);     
                this.reportViewer1.LocalReport.DataSources.Add(rds);
                this.reportViewer1.LocalReport.DataSources.Add(rds2);
                reportViewer1.LocalReport.ReportEmbeddedResource = "Docsis_Application.Reportes.rptAnalisisCuantitativo2.rdlc";                     
                this.reportViewer1.LocalReport.Refresh();
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en reporte " + ex.Message);
            }
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
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }        
    }

    public class rtp_AnalisisCuanti_cls
    {
        public string estado_solicitud { get; set; }
        public Int32 no_solicitud { get; set; }        
        public string numero_identificacion { get; set; }
        public string codigo_cliente { get; set; }
        public string nombre_completo { get; set; }
        public string producto { get; set; }
        public string descripcion_fuente { get; set; }
        public decimal monto_solicitado { get; set; }
        public string tasa_sol { get; set; }
        public string plazo_sol { get; set; }
        public decimal monto_aprobado { get; set; }
        public decimal tasa_aprobada { get; set; }
        public int plazo_aprobado { get; set; }
        public decimal ingresos { get; set; }
        public decimal otros_ingresos { get; set; }
        public decimal deducciones { get; set; }
        public decimal total_ingresos_sol { get; set; }
        public decimal cuota_nivelada { get; set; }
        public decimal coopsalud { get; set; }
        public decimal aportacion { get; set; }
        public decimal mejora_avalua { get; set; }
        public decimal cuota_anticipada { get; set; }
        public decimal consolidar_coopsafa { get; set; }
        public decimal consolidar_otros { get; set; }
        public decimal pago_terceros { get; set; }
        public decimal complemento_aportaciones { get; set; }
        public decimal timbres_cooperativistas { get; set; }
        public decimal honorarios_hipoteca { get; set; }
        public decimal honorarios_compra_venta { get; set; }
        public decimal capitalizacion { get; set; }
        public decimal seguro_vida { get; set; }
        public decimal seguro_danos { get; set; }
        public decimal seguro_incendio { get; set; }
        public decimal seguro_vida_mensual { get; set; }
        public decimal seguro_danos_mensual { get; set; }
        public decimal seguro_incendios_mensual { get; set; }
        public decimal gastos_administrativos { get; set; }
        public decimal papeleria { get; set; }
        public decimal avaluo_final { get; set; }
        public decimal central_riesgos { get; set; }
        public decimal total_deducciones { get; set; }
        public decimal neto_recibir { get; set; }
        public decimal cuota_total { get; set; }
        public decimal tir { get; set; }
        public decimal cat { get; set; }
        public string comentario_analista_credito { get; set; }        
    }
    public class rpt_AnalisisCuanti_detdeducciones
    {
        public string cod_tipocobro { get; set; }
        public string des_tipocobro { get; set; }
        public string mon_cobro { get; set; }
    }
}
