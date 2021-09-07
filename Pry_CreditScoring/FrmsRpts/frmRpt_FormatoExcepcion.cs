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
using Docsis_Application.Excepciones;
using System.Globalization;

namespace Docsis_Application.FrmsRpts
{
    public partial class frmRpt_FormatoExcepcion : Form
    {
        public int CodigoExcepcion { get; set; }
        public int NoSolicitud { get; set; }
        public bool MostrarImprimir = true;
        private DataAccess da;

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

        public frmRpt_FormatoExcepcion(int codigoExcepcion, int noSolicitud, DataAccess _da)
        {
            InitializeComponent();
            this.CodigoExcepcion = codigoExcepcion;
            this.NoSolicitud = noSolicitud;
            this.da = _da;
        }

        private void frmRpt_FormatoExcepcion_Load(object sender, EventArgs e)
        {
            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = ZoomMode.Percent;
            reportViewer1.PageCountMode = PageCountMode.Actual;
            reportViewer1.ZoomPercent = 100; //Seleccionamos el zoom que deseamos utilizar. En este caso un 100%
            mostrar_reporte();
            this.reportViewer1.RefreshReport();

        }

        private void mostrar_reporte()
        {
            DataTable dtmiembro1 = null;
            DataTable dtmiembro2 = null;
            DataTable dtmiembro3 = null;

            int hay = da.p_Cantidad_Comites(CodigoExcepcion);
            var comite = 0;

            if (hay > 1)
                 comite = da.p_comiteID(CodigoExcepcion);
            //Para generar un dtMiebro(firma y nombre) en blanco, para que cuando solo hay 1 ó 2 no de error en loop
            dtmiembro1 = da.p_firmas_EXCP(CodigoExcepcion, "");
            dtmiembro2 = da.p_firmas_EXCP(CodigoExcepcion, "");
            dtmiembro3 = da.p_firmas_EXCP(CodigoExcepcion, "");
            try
            {
                try
                {
                    DataTable dtmiembros = da.p_obtenerFirmasExcepciones(CodigoExcepcion);
                    int n = 1;
                    foreach (DataRow dtRow in dtmiembros.Rows)
                    {
                        string vl_usuario = dtRow[0].ToString();
                        if (n == 1)
                        {
                            dtmiembro1 = da.p_firmas_EXCP(CodigoExcepcion, vl_usuario);
                        }
                        if (n == 2)
                        {
                            dtmiembro2 = da.p_firmas_EXCP(CodigoExcepcion, vl_usuario);
                        }
                        if (n == 3)
                        {
                            dtmiembro3 = da.p_firmas_EXCP(CodigoExcepcion, vl_usuario);
                        }
                        n++;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                
                //info de la solicitud
                DataTable infoExcep = this.da.p_obtener_datos_n_excepcion_table(this.NoSolicitud);
                string xml = this.da.ObtenerXmlRespuestaEvaluacion(this.NoSolicitud);
                u_Globales.IngresoNetoD = 0;
                u_Globales.IngresoNeto = 0;
                u_Globales.CondicionTU = string.Empty;
                u_Globales.obtener_info_reporte_TU(xml);

                //info del detalle
                DataTable ddt = this.da.get_detalle_info(this.CodigoExcepcion);
                string excepciones = string.Empty;
                string justificacion = string.Empty;
                int contador = 1;
                foreach (DataRow item in ddt.Rows)
                {
                    if (item["cod_tipo_excepcion"].ToString().Equals("6-OE-7"))
                    {
                        int index = item["observaciones"].ToString().IndexOf('*');
                        excepciones += contador + "-.Otras no especificadas: " + item["observaciones"].ToString().Substring(0, index); //+ item["observaciones"].ToString() + "; ";
                        justificacion += contador + "-." + item["observaciones"].ToString().Substring(index + 1) + "; ";
                        contador++;
                    }
                    else
                    {
                        excepciones += contador + "-." + item["tipo_excepcion"].ToString() + "; ";
                        if (!item["observaciones"].ToString().Equals(string.Empty))
                        {
                            justificacion += contador + "-." + item["observaciones"].ToString() + "; ";
                        }
                        contador++;
                    }
                }

                //info del encabezado
                DataTable grales = new DataTable();
                grales = this.da.get_info_excep_gral(CodigoExcepcion);
                string pago_mediante = (grales.Rows[0]["pago_mediante"].ToString().Equals("V")) ? "Pago por ventanilla" : "Pago por planilla";
                //info de cierre
                DataTable resolucion = DocSys.get_datos_resolucion(this.CodigoExcepcion);
                //resolucion
                DataTable dtApro = this.da.get_aprobaciones_solicitud(this.CodigoExcepcion);
                int total_aprobaciones = dtApro.Rows.Count;
                int total_respondidas = 0;
                List<int> decisiones = new List<int>();
                string txtRecNivResl = string.Empty;
                foreach (DataRow item in dtApro.Rows)
                {
                    string pendienteR = item["pendiente_respuesta_b"].ToString();
                    string usuarioC = item["usuario_comite"].ToString();

                    //cuenta las aprobaciones ya respondidas
                    if (pendienteR.Equals(u_Globales.Negativo))
                    {
                        total_respondidas++;
                        decisiones.Add(Convert.ToInt32(item["decision_id"].ToString()));
                    }

                    string nombreUsuarioC = this.da.ObtenerNombreUsuario(usuarioC);
                    txtRecNivResl += item["observaciones"].ToString() + "\n" /*+ ". (" + nombreUsuarioC + ")"*/;
                }

                string aprobar = " ";
                string devolver = " ";
                string denegar = " ";

                if (total_aprobaciones == total_respondidas & total_aprobaciones > 0)
                {
                    int modif_Excep = 0;
                    int rechazarD = 0;
                    int aprobarD = 0;

                    foreach (int decisionID in decisiones)
                    {
                        DataTable tmp = this.da.get_resolucion_excepcion(decisionID);

                        if (tmp.Rows[0]["aprobar_excep"].ToString() == u_Globales.Afirmativo)
                        {
                            aprobarD++;
                        }

                        if (tmp.Rows[0]["denegar_excep"].ToString() == u_Globales.Afirmativo)
                        {
                            rechazarD++;
                        }

                        if (tmp.Rows[0]["modif_excep"].ToString() == u_Globales.Afirmativo)
                        {
                            modif_Excep++;
                        }
                    }

                    if (modif_Excep > 0)
                    {
                        devolver = "X";
                    }

                    if (aprobarD == total_aprobaciones)
                    {
                        aprobar = "X";
                    }

                    if (rechazarD > 0)
                    {
                        denegar = "X";
                    }
                }

                ReportDataSource rds = new ReportDataSource("dataset1", infoExcep);
                ReportDataSource rdmiembro1 = new ReportDataSource("dsMiembroEX1", dtmiembro1);
                ReportDataSource rdmiembro2 = new ReportDataSource("dsMiembroEX2", dtmiembro2);
                ReportDataSource rdmiembro3 = new ReportDataSource("dsMiembroEX3", dtmiembro3);
                ReportParameter pa_nombre_cliente = new ReportParameter("nombre_cliente", infoExcep.Rows[0]["nombre_cliente"].ToString());
                ReportParameter pa_codigo_cliente = new ReportParameter("codigo_cliente", infoExcep.Rows[0]["codigo_cliente"].ToString());
                var monto = Convert.ToDecimal(infoExcep.Rows[0]["monto_solicitado"].ToString());//Cambio
                ReportParameter pa_monto_solicitado = new ReportParameter("monto_solicitado", monto.ToString("0,0.00", CultureInfo.InvariantCulture));
                ReportParameter pa_nombre_agencia = new ReportParameter("nombre_agencia", infoExcep.Rows[0]["nombre_agencia"].ToString());
                ReportParameter pa_lugar_de_trabajo = new ReportParameter("lugar_de_trabajo", infoExcep.Rows[0]["lugar_de_trabajo"].ToString());
                ReportParameter pa_cargo_que_ocupa = new ReportParameter("cargo_que_ocupa", infoExcep.Rows[0]["cargo_que_ocupa"].ToString());
                var fecha_ingreso = Convert.ToDateTime(infoExcep.Rows[0]["fecha_ingreso_laboral"].ToString());
                ReportParameter pa_fecha_ingreso_laboral = new ReportParameter("fecha_ingreso_laboral", fecha_ingreso.ToShortDateString());
                ReportParameter pa_desc_sub_aplicacion = new ReportParameter("desc_sub_aplicacion", infoExcep.Rows[0]["desc_sub_aplicacion"].ToString());
                ReportParameter pa_descripcion_destino = new ReportParameter("descripcion_destino", infoExcep.Rows[0]["descripcion_destino"].ToString());
                var saldo_apor = Convert.ToDecimal(infoExcep.Rows[0]["saldo"].ToString());
                ReportParameter pa_saldo = new ReportParameter("saldo", saldo_apor.ToString("0,0.00", CultureInfo.InvariantCulture));
                //ReportParameter pa_saldo = new ReportParameter("saldo", string.Format("0,0.00", CultureInfo.InvariantCulture, infoExcep.Rows[0]["saldo"].ToString()));
                var saldo_bruto = Convert.ToDecimal(infoExcep.Rows[0]["saldo_bruto"].ToString());
                ReportParameter pa_saldo_bruto = new ReportParameter("saldo_bruto", saldo_bruto.ToString("0,0.00", CultureInfo.InvariantCulture));
                //ReportParameter pa_saldo_bruto = new ReportParameter("saldo_bruto", string.Format("0,0.00", CultureInfo.InvariantCulture, infoExcep.Rows[0]["saldo_bruto"].ToString()));
                ReportParameter pa_condicion_tu = new ReportParameter("condicion_tu", u_Globales.CondicionTU);
                ReportParameter pa_ingreso_neto = new ReportParameter("IngresoNeto", u_Globales.IngresoNeto.ToString("0,0.00", CultureInfo.InvariantCulture));
                ReportParameter pa_IngresoNetoD = new ReportParameter("IngresoNetoD", (Math.Truncate(u_Globales.IngresoNetoD * 100) / 100).ToString("0,0.00", CultureInfo.InvariantCulture));
                ReportParameter pa_fecha_presentacion = new ReportParameter("fecha_presentacion", grales.Rows[0]["fecha_presentacion"].ToString());
                ReportParameter pa_pago_mediante = new ReportParameter("pago_mediante", pago_mediante);
                ReportParameter pa_detalle_deudas_consol = new ReportParameter("detalle_deudas_consol", grales.Rows[0]["detalle_deudas_consol"].ToString());
                ReportParameter pa_plazo = new ReportParameter("pa_plazo", infoExcep.Rows[0]["plazo"].ToString());
                ReportParameter pa_puesto_referenciado = new ReportParameter("pa_puesto_referenciado", infoExcep.Rows[0]["puesto_referenciado"].ToString());
                ReportParameter pa_SOLICITUD_ANTERIOR = new ReportParameter("pa_SOLICITUD_ANTERIOR", infoExcep.Rows[0]["SOLICITUD_ANTERIOR"].ToString());
                ReportParameter pa_FECHA_SOL_ANTERIOR = new ReportParameter("pa_FECHA_SOL_ANTERIOR", infoExcep.Rows[0]["FECHA_SOL_ANTERIOR"].ToString());
                ReportParameter pa_RESOLUCION_ANTERIOR = new ReportParameter("pa_RESOLUCION_ANTERIOR", infoExcep.Rows[0]["RESOLUCION_ANTERIOR"].ToString());
                ReportParameter pa_NUEVA_FECHA = new ReportParameter("pa_NUEVA_FECHA", infoExcep.Rows[0]["NUEVA_FECHA"].ToString());
                ReportParameter pa_RECONSIDERACION_FECHA = new ReportParameter("pa_RECONSIDERACION_FECHA", infoExcep.Rows[0]["RECONSIDERACION_FECHA"].ToString());

                string fecha_resol = string.Empty;
                if (resolucion.Rows[0]["fecha_resolucion"].ToString().Equals(string.Empty))
                {
                    fecha_resol = " ";
                }
                else
                {
                    fecha_resol = resolucion.Rows[0]["fecha_resolucion"].ToString();
                }
                ReportParameter pa_fecha_resolucion = new ReportParameter("fecha_resolucion", fecha_resol);
                ReportParameter pa_excepciones = new ReportParameter("excepciones", excepciones);
                ReportParameter pa_justificacion = new ReportParameter("justificacion", justificacion);
                ReportParameter pa_aprobar = new ReportParameter("aprobar", aprobar);
                ReportParameter pa_devolver = new ReportParameter("devolver", devolver);
                ReportParameter pa_denegar = new ReportParameter("denegar", denegar);
                ReportParameter pa_rec_niv_resol = new ReportParameter("rec_niv_resol", txtRecNivResl);
                ReportParameter pa_rec_filial = new ReportParameter("recomen_grte_filial", grales.Rows[0]["recomendacion_filial"].ToString() + "\n" + grales.Rows[0]["RECOMENDACION_NIV_RES"].ToString());
                ReportParameter pa_usuario_ingr = new ReportParameter("usuarioCreacion", grales.Rows[0]["usuario_ing_excepcion"].ToString());
                ReportParameter pa_nombre_usuario_ingr = new ReportParameter("nombreUsuarioCreacion", this.da.ObtenerNombreUsuario(grales.Rows[0]["usuario_ing_excepcion"].ToString()));
                var prestaciones = Convert.ToDecimal(grales.Rows[0]["PRESTACIONES"].ToString());
                ReportParameter pa_edad_solicitante = new ReportParameter("pa_edad", grales.Rows[0]["EDAD_SOLICITANTE"].ToString());
                ReportParameter pa_prestaciones = new ReportParameter("pa_prestaciones", prestaciones.ToString("0,0.00", CultureInfo.InvariantCulture));

                this.reportViewer1.LocalReport.DataSources.Add(rds);
                this.reportViewer1.LocalReport.EnableExternalImages = true;
                this.reportViewer1.LocalReport.DataSources.Add(rdmiembro1);
                this.reportViewer1.LocalReport.DataSources.Add(rdmiembro2);
                this.reportViewer1.LocalReport.DataSources.Add(rdmiembro3);
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "Docsis_Application.Reportes.rptFormatoExcepcion.rdlc";
                this.reportViewer1.LocalReport.SetParameters(pa_nombre_cliente);
                this.reportViewer1.LocalReport.SetParameters(pa_codigo_cliente);
                this.reportViewer1.LocalReport.SetParameters(pa_monto_solicitado);
                this.reportViewer1.LocalReport.SetParameters(pa_nombre_agencia);
                this.reportViewer1.LocalReport.SetParameters(pa_lugar_de_trabajo);
                this.reportViewer1.LocalReport.SetParameters(pa_cargo_que_ocupa);
                this.reportViewer1.LocalReport.SetParameters(pa_fecha_ingreso_laboral);
                this.reportViewer1.LocalReport.SetParameters(pa_desc_sub_aplicacion);
                this.reportViewer1.LocalReport.SetParameters(pa_descripcion_destino);
                this.reportViewer1.LocalReport.SetParameters(pa_saldo);
                this.reportViewer1.LocalReport.SetParameters(pa_saldo_bruto);
                this.reportViewer1.LocalReport.SetParameters(pa_condicion_tu);
                this.reportViewer1.LocalReport.SetParameters(pa_ingreso_neto);
                this.reportViewer1.LocalReport.SetParameters(pa_IngresoNetoD);
                this.reportViewer1.LocalReport.SetParameters(pa_fecha_presentacion);
                this.reportViewer1.LocalReport.SetParameters(pa_pago_mediante);
                this.reportViewer1.LocalReport.SetParameters(pa_detalle_deudas_consol);
                this.reportViewer1.LocalReport.SetParameters(pa_fecha_resolucion);
                this.reportViewer1.LocalReport.SetParameters(pa_excepciones);
                this.reportViewer1.LocalReport.SetParameters(pa_justificacion);
                this.reportViewer1.LocalReport.SetParameters(pa_aprobar);
                this.reportViewer1.LocalReport.SetParameters(pa_denegar);
                this.reportViewer1.LocalReport.SetParameters(pa_devolver);
                this.reportViewer1.LocalReport.SetParameters(pa_rec_niv_resol);
                this.reportViewer1.LocalReport.SetParameters(pa_rec_filial);
                this.reportViewer1.LocalReport.SetParameters(pa_usuario_ingr);
                this.reportViewer1.LocalReport.SetParameters(pa_nombre_usuario_ingr);
                this.reportViewer1.LocalReport.SetParameters(pa_edad_solicitante);
                this.reportViewer1.LocalReport.SetParameters(pa_prestaciones);
                this.reportViewer1.LocalReport.SetParameters(pa_plazo);
                this.reportViewer1.LocalReport.SetParameters(pa_puesto_referenciado);
                this.reportViewer1.LocalReport.SetParameters(pa_SOLICITUD_ANTERIOR);
                this.reportViewer1.LocalReport.SetParameters(pa_FECHA_SOL_ANTERIOR);
                this.reportViewer1.LocalReport.SetParameters(pa_RESOLUCION_ANTERIOR);
                this.reportViewer1.LocalReport.SetParameters(pa_NUEVA_FECHA);
                this.reportViewer1.LocalReport.SetParameters(pa_RECONSIDERACION_FECHA);
                this.reportViewer1.ShowPrintButton = this.MostrarImprimir;
                this.reportViewer1.ShowExportButton = this.MostrarImprimir;

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

    public class dsMiembroEX1
    {
        public string nombre_miembro1 { get; set; }
        public Image firma_miembro1 { get; set; }
    }
    public class dsMiembroEX2
    {
        public string nombre_miembro2 { get; set; }
        public Image firma_miembro2 { get; set; }
    }
    public class dsMiembroEX3
    {
        public string nombre_miembro3 { get; set; }
        public Image firma_miembro3 { get; set; }
    }
}