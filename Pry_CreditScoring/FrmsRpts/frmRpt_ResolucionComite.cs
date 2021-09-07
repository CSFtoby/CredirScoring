using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using wfcModel;
using Pry_QRGenerador;


namespace Docsis_Application.FrmsRpts
{
    public partial class frmRpt_ResolucionComite : Form
    {
        public string vl_enter = "\r\n";
        bool con_borde = MDI_Menu.con_borde;
        bool b_imprimir_qr = false;        
        public Int32 gno_solicitud = 0;

        public string nivel_resolutivo;
        public string no_acta;
        public string no_solicitud;

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
        public DataAccess da;
        public frmRpt_ResolucionComite(DataAccess da)
        {
            InitializeComponent();
            this.da = da;
        }
        private void frmRpt_ResolucionComite_Load(object sender, EventArgs e)
        {
            string vl_mostrar_codigoQR = da.ObtenerParametro("WFC-0009");
            string vl_mostrar_firmas_digi = da.ObtenerParametro("WFC-0013");
            if (vl_mostrar_codigoQR == "S")
            {
                b_imprimir_qr = true;
            }
            

            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = ZoomMode.PageWidth;
            reportViewer1.ZoomPercent = 100; //Seleccionamos el zoom que deseamos utilizar. En este caso un 100%
            mostrar_reporte();
            this.reportViewer1.RefreshReport();
        }

        private void mostrar_reporte()
        {
            DataTable dtmiembro1 = null;
            DataTable dtmiembro2 = null;
            DataTable dtmiembro3 = null;
            var comite = da.p_comiteID(gno_solicitud);
            //Para generar un dtMiebro(firma y nombre) en blanco, para que cuando solo hay 1 ó 2 no de error en loop
            dtmiembro1 = da.p_firmas_miembro1(gno_solicitud, "");
            dtmiembro2 = da.p_firmas_miembro2(gno_solicitud, "");
            dtmiembro3 = da.p_firmas_miembro3(gno_solicitud, "");
            try
            {
                try
                {
                    DataTable dtmiembros = da.p_obtenerFirmasResolutivas(gno_solicitud);
                    int n = 1;
                    foreach (DataRow dtRow in dtmiembros.Rows)
                    {
                        string vl_usuario = dtRow[0].ToString();
                        if (n == 1)
                        {
                            dtmiembro1 = da.p_firmas_miembro1(gno_solicitud, vl_usuario);
                        }
                        if (n == 2)
                        {
                            dtmiembro2 = da.p_firmas_miembro2(gno_solicitud, vl_usuario);
                        }
                        if (n == 3)
                        {
                            dtmiembro3 = da.p_firmas_miembro3(gno_solicitud, vl_usuario);
                        }
                        n++;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

                DataTable dt = da.p_datos_rptResolucionComite(gno_solicitud);
                Int32 vl_comite_id = Convert.ToInt32(dt.Rows[0]["estacion_id"].ToString());
                string vl_no_acta = dt.Rows[0]["no_acta_resolucion"].ToString();

                string tempofecha = "";
                try
                { tempofecha = dt.Rows[0]["fecha_aprobacion_rechazo"].ToString().Substring(0, 10); }
                catch
                {}

                string vl_fecha_aprobacion = tempofecha ?? " ";
                string vl_ciudad_resolucion = dt.Rows[0]["ciudad_resolucion"].ToString();

                #region elaboracio del codigoQR
                DataTable dtQR = new DataTable();
                dtQR.Columns.Add("texto_para_qr");
                dtQR.Columns.Add("imgQR", System.Type.GetType("System.Byte[]"));
                
                if (b_imprimir_qr)
                {
                    string vl_texto_para_gr = "";
                    try
                    {
                        string nombre_completo = dt.Rows[0]["nombre_completo"].ToString();
                        double monto_solicitado = double.Parse(dt.Rows[0]["monto_solicitado"].ToString());
                        double monto_aprobado = double.Parse(dt.Rows[0]["monto_aprobado"].ToString());
                        vl_texto_para_gr = "NoSolicitud:" + gno_solicitud.ToString() + "|Solicitante:" + nombre_completo.Trim() + "|MontoSolicitado:" + monto_solicitado.ToString("###,###,##0.00") +
                            "|MontoAprobado:" + monto_aprobado.ToString("###,###,##0.00") + "|Resolucion :" + dt.Rows[0]["decision_final_solicitud"].ToString();
                        
                        vl_texto_para_gr = (dt.Rows[0]["guid_sol"].ToString());

                        var imgqr = QRGenerador.generarCodigoQR(vl_texto_para_gr);
                        
                        pbQR.Image = imgqr;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                    dtQR.Rows.Add(vl_texto_para_gr, DocSys.p_CopyImageToByteArray(pbQR.Image));
                }
                #endregion
                
                ReportDataSource rds = new ReportDataSource("dataset1", dt);
                ReportDataSource rdsqr = new ReportDataSource("dscodigoQR", dtQR);
                ReportDataSource rdmiembro1 = new ReportDataSource("dsmiembro1", dtmiembro1);
                ReportDataSource rdmiembro2 = new ReportDataSource("dsmiembro2", dtmiembro2);
                ReportDataSource rdmiembro3 = new ReportDataSource("dsmiembro3", dtmiembro3);

                ReportParameter pa1 = new ReportParameter("pa_solicitud", gno_solicitud.ToString());
                DataTable dtEstacion = da.ObtenerEstacionesxId(vl_comite_id);
                string vl_observaciones_comite = da.p_obtenerObservacionesResolutivas(gno_solicitud);
                string vl_nombre_comite = da.ObtenerNombreComiteResolucion(gno_solicitud);
                
                string fecha_solicitud = Convert.ToDateTime(dt.Rows[0]["fecha_presentacion"].ToString()).ToShortDateString();//.Substring(0, 10);

                string sEncabezado = @"El Suscrito " + vl_nombre_comite + ", por medio de la presente certifica que en el Acta No. " + vl_no_acta + " Solicitud no. " + gno_solicitud.ToString() + " de fecha " + fecha_solicitud + "," + vl_enter +
                    "revisó la presente solicitud bajo las siguientes condiciones :";
                
                string sResolucion = "El suscrito " + vl_nombre_comite + ", acredita que en reunión celebrada en fecha " + vl_fecha_aprobacion + " en la ciudad " + vl_ciudad_resolucion + ", se emite la siguiente resolución:";

                string vl_decison = "";
                if (dt.Rows[0]["decision_final_solicitud"].ToString() == "APROBADO")
                {
                    vl_decison = "APROBAR";
                }
                if (dt.Rows[0]["decision_final_solicitud"].ToString() == "RECHAZADO")
                {
                    DataTable dt2 = (da.ObtenerMotivosRechazoXSol(gno_solicitud));
                    vl_decison = dt2.Rows[0]["DECISION"].ToString() + vl_enter + dt2.Rows[0]["RECHAZO"].ToString();
                }
                ReportParameter pa2 = new ReportParameter("pa_encabezado", sEncabezado);
                ReportParameter pa3 = new ReportParameter("pa_resolucion", sResolucion);
                ReportParameter pa4 = new ReportParameter("pa_decision", vl_decison);
                ReportParameter pa5 = new ReportParameter("pa_observaciones_comite", vl_observaciones_comite);

                this.reportViewer1.LocalReport.DataSources.Add(rds);
                this.reportViewer1.LocalReport.DataSources.Add(rdsqr);
                this.reportViewer1.LocalReport.DataSources.Add(rdmiembro1);
                this.reportViewer1.LocalReport.DataSources.Add(rdmiembro2);
                this.reportViewer1.LocalReport.DataSources.Add(rdmiembro3);

                reportViewer1.LocalReport.ReportEmbeddedResource = "Docsis_Application.Reportes.rptResolucionComite.rdlc";
                this.reportViewer1.LocalReport.SetParameters(pa1);
                this.reportViewer1.LocalReport.SetParameters(pa2);
                this.reportViewer1.LocalReport.SetParameters(pa3);
                this.reportViewer1.LocalReport.SetParameters(pa4);
                this.reportViewer1.LocalReport.SetParameters(pa5);

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
            this.reportViewer1.Dispose();
            this.Close();
        }
        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }
        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
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
    }



    public class rtp_AnalisisCualitivativo_cls
    {
        public string estado_solicitud { get; set; }
        public Int32 no_solicitud { get; set; }
        public string numero_identificacion { get; set; }
        public string codigo_cliente { get; set; }
        public string nombre_completo { get; set; }
        public string producto { get; set; }
        public decimal monto_solicitado { get; set; }
        public string destino { get; set; }
        public string plazo { get; set; }
        public string tasa { get; set; }
        public string ventanilla_planilla { get; set; }
        public string codeudor { get; set; }
        public string region { get; set; }
        public string filial { get; set; }
        public string oficial_servicios { get; set; }
        public string gerente_filial { get; set; }
        public string instrucciones_desembolso { get; set; }
        public string descripcion_garantia { get; set; }
        public decimal monto_aprobado { get; set; }
        public decimal plazo_aprobado { get; set; }
        public decimal tasa_aprobada { get; set; }
        public decimal cuota_aprobada { get; set; }
        public string no_acta_resolucion { get; set; }
    }

    public class dsMiembro1
    {
        public string nombre_miembro1 { get; set; }
        public Image firma_miembro1 { get; set; }
    }
    public class dsMiembro2
    {
        public string nombre_miembro2 { get; set; }
        public Image firma_miembro2 { get; set; }
    }
    public class dsMiembro3
    {
        public string nombre_miembro3 { get; set; }
        public Image firma_miembro3 { get; set; }
    }

    public class dscodigoQR
    {
        public string texto_para_qr { get; set; }
        public Image imgQR { get; set; }
    }


}
