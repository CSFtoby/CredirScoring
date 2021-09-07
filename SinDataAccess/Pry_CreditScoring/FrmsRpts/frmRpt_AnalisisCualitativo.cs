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
using System.IO;
using System.Xml;


namespace Docsis_Application.FrmsRpts
{
    public partial class frmRpt_AnalisisCualitativo : Form
    {
        public string vl_enter = "\r\n";
        bool con_borde = MDI_Menu.con_borde;
        public Int32 gno_solicitud = 0;
        public string no_solicitud;
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

        public frmRpt_AnalisisCualitativo(DataAccess da)
        {
            InitializeComponent();
            this.da = da;
        }
        private void frmRpt_AnalisisCualitativo_Load(object sender, EventArgs e)
        {
            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);            
            reportViewer1.ZoomMode = ZoomMode.PageWidth;
            //reportViewer1.ZoomPercent = 100; //Seleccionamos el zoom que deseamos utilizar. En este caso un 100%
            mostrar_reporte();
            this.reportViewer1.RefreshReport();
        }
        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }
        private void mostrar_reporte()
        {

            try
            {
                string resultado_consulta = da.ObtenerXmlRespuestaEvaluacion(gno_solicitud);
                DataTable dtInfoBuro = procesar_xml_buro(resultado_consulta);

                DataTable dt = da.p_datos_AnalisisCualitativo(gno_solicitud);

                ReportDataSource rds = new ReportDataSource("dataset1", dt);
                txtComentarios_filial.Text = dt.Rows[0]["comentarios_filial"].ToString();
                string vl_no_identificacion = dt.Rows[0]["numero_identificacion"].ToString();
                string vl_edad_afiliado = dt.Rows[0]["edad"].ToString();
				var fecha = Convert.ToDateTime(dt.Rows[0]["fecha_inicio_relacion"].ToString());
				string vl_fecha_inicio_relacion = fecha.ToShortDateString();//dt.Rows[0]["fecha_inicio_relacion"].ToString().Substring(0, 10);
                string vl_lugar_trabajo = dt.Rows[0]["patrono"].ToString();
                string vl_antiguedad_laborar = dt.Rows[0]["antiguedad_laboral"].ToString();
                string vl_cargo = dt.Rows[0]["cargo"].ToString(); ;
                string vl_saldo_aportaciones = String.Format("{0:###,###,###,##0.00}", float.Parse(da.ObtenerSaldosAportacionesxCliente(vl_no_identificacion).ToString()));
                string vl_destino = dt.Rows[0]["descripcion_destino"].ToString();
                string vl_resultado_buro = dt.Rows[0]["resultado_buro"].ToString();
                string vl_metodo_cobro = dt.Rows[0]["ventanilla_planilla"].ToString();
                string vl_monto_balance_consolidar = dt.Rows[0]["monto_balance_consolidar"].ToString();
                string vl_estado_buro_interno = "";
                List<string> vl_list_observaciones_buro = new List<string>();
                da.ObtenerBuroInterno_xId(vl_no_identificacion, out vl_estado_buro_interno, out vl_list_observaciones_buro);

                DataRow[] Rowsdt = dtInfoBuro.Select("cedula='" + vl_no_identificacion + "'");
                string precalificado_principal = "";
                if (Rowsdt.Count() > 0)
                {
                    precalificado_principal = Rowsdt[0]["precalificado"].ToString();
                }


                //----------------------------  Codeudor ----------------------------------------
                string vl_nombre_codeudor = dt.Rows[0]["nombre_codeudor"].ToString();
                string vl_id_codeudor = dt.Rows[0]["no_identidad_codeudor"].ToString();
                string vl_cargo_codeudor = dt.Rows[0]["cargo_codeudor"].ToString();
                string vl_patrono_codeudor = dt.Rows[0]["patrono_codeudor"].ToString();
                string vl_antiguedad_laboral_codeudor = dt.Rows[0]["antiguedad_laboral_codeudor"].ToString();
                string vl_estado_buro_interno_codeudor = "";
                List<string> vl_list_observaciones_buro_codeudor = new List<string>();
                da.ObtenerBuroInterno_xId(vl_id_codeudor, out vl_estado_buro_interno_codeudor, out vl_list_observaciones_buro_codeudor);

                DataRow[] RowsdtCodeudor = dtInfoBuro.Select("cedula='" + vl_id_codeudor + "'");
                string precalificado_codeudor = "";
                if (RowsdtCodeudor.Count() > 0)
                {
                    precalificado_codeudor = RowsdtCodeudor[0]["precalificado"].ToString();
                }



                //----------------------------Aval 1----------------------------------------
                string vl_nombre_aval1 = dt.Rows[0]["nombre_aval1"].ToString();
                string vl_id_aval1 = dt.Rows[0]["no_identidad_aval1"].ToString();
                string vl_cargo_aval1 = dt.Rows[0]["cargo_aval1"].ToString();
                string vl_patrono_aval1 = dt.Rows[0]["patrono_aval1"].ToString();
                string vl_antiguedad_laboral_aval1 = dt.Rows[0]["antiguedad_laboral_aval1"].ToString();
                string vl_estado_buro_interno_aval1 = "";
                List<string> vl_list_observaciones_buro_aval1 = new List<string>();
                da.ObtenerBuroInterno_xId(vl_id_aval1, out vl_estado_buro_interno_aval1, out vl_list_observaciones_buro_aval1);

                DataRow[] RowsdtAval1 = dtInfoBuro.Select("cedula='" + vl_id_aval1 + "'");
                string precalificado_aval1 = "";
                if (RowsdtAval1.Count() > 0)
                {
                    precalificado_aval1 = RowsdtAval1[0]["precalificado"].ToString();
                }


                //----------------------------Aval 2----------------------------------------
                string vl_nombre_aval2 = dt.Rows[0]["nombre_aval2"].ToString();
                string vl_id_aval2 = dt.Rows[0]["no_identidad_aval2"].ToString();
                string vl_cargo_aval2 = dt.Rows[0]["cargo_aval2"].ToString();
                string vl_patrono_aval2 = dt.Rows[0]["patrono_aval2"].ToString();
                string vl_antiguedad_laboral_aval2 = dt.Rows[0]["antiguedad_laboral_aval2"].ToString();
                string vl_estado_buro_interno_aval2 = "";
                List<string> vl_list_observaciones_buro_aval2 = new List<string>();
                da.ObtenerBuroInterno_xId(vl_id_aval2, out vl_estado_buro_interno_aval2, out vl_list_observaciones_buro_aval2);
                DataRow[] RowsdtAval2 = dtInfoBuro.Select("cedula='" + vl_id_aval2 + "'");
                string precalificado_aval2 = "";
                if (RowsdtAval2.Count() > 0)
                {
                    precalificado_aval2 = RowsdtAval2[0]["precalificado"].ToString();
                }

                string sDescripcion_afiliado = @"El Afiliado tienen una edad " + vl_edad_afiliado + ", afiliado a la Cooperativa desde " + vl_fecha_inicio_relacion + "." + vl_enter +
                    "Labora en " + vl_lugar_trabajo + " con una antiguedad laboral de " + vl_antiguedad_laborar + " meses" + "." + vl_enter +
                    "Desempeñandose en el cargo de " + vl_cargo + "." + vl_enter +
                    "Su saldo en aportaciones ascienden a " + vl_saldo_aportaciones + "." + vl_enter;


                string sDescripcion_prestamo = @"El destino del prestamo sera " + vl_destino + "." + vl_enter +
                    "Su estado en la Central de Riegos es " + precalificado_principal.ToUpper() + ", Su pago será por " + vl_metodo_cobro + "." + vl_enter +
                    "Historial en Coopsafa es " + vl_estado_buro_interno + "." + vl_enter +
                    "  ";

                #region codeudor
                string sCodeudor = "N/A";
                if (string.IsNullOrEmpty(vl_id_codeudor))
                {
                    sCodeudor = "N/A";
                }
                else
                {
                    sCodeudor = @"Nombre :" + vl_nombre_codeudor + "     Identificación " + vl_id_codeudor + vl_enter +
                        "Quien se desempeña como " + vl_cargo_codeudor + vl_enter +
                    "En " + vl_patrono_codeudor + " con una antiguedad laboral de " + vl_antiguedad_laboral_codeudor + " meses " + vl_enter +
                    "Su estado en la central del riegos es " + precalificado_codeudor + "." + vl_enter +
                    "Historial en Coopsafa  es " + vl_estado_buro_interno_codeudor + "." + vl_enter;
                }
                #endregion

                #region Aval1
                string vl_aval1 = "N/A";
                if (string.IsNullOrEmpty(vl_id_aval1))
                {
                    vl_aval1 = "N/A";
                }
                else
                {
                    vl_aval1 = @"Nombre :" + vl_nombre_aval1 + "     Identificación " + vl_id_aval1 + vl_enter +
                        "Quien se desempeña como " + vl_cargo_aval1 + vl_enter +
                    "En " + vl_patrono_aval1 + " con una antiguedad laboral de " + vl_antiguedad_laboral_aval1 + " meses. " + vl_enter +
                    "Su estado en la central del riegos es " + precalificado_aval1 + "." + vl_enter +
                    "Historial en Coopsafa es " + vl_estado_buro_interno_aval1 + "." + vl_enter;
                }
                #endregion

                #region Aval2
                string vl_aval2 = "N/A";
                if (string.IsNullOrEmpty(vl_id_aval2))
                {
                    vl_aval2 = "N/A";
                }
                else
                {
                    vl_aval2 = @"Nombre :" + vl_nombre_aval2 + "     Identificación " + vl_id_aval2 + vl_enter +
                        "Quien se desempeña como " + vl_cargo_aval2 + vl_enter +
                    "En " + vl_patrono_aval2 + " con una antiguedad laboral de " + vl_antiguedad_laboral_aval2 + " meses. " + vl_enter +
                    "Su estado en la central del riegos es " + precalificado_aval2 + "." + vl_enter +
                    "Historial en Coopsafa es " + vl_estado_buro_interno_aval2 + "." + vl_enter;

                }
                #endregion
                string vl_observaciones_filial = "";
                vl_observaciones_filial = da.ObtenerObservacionesAfiliacion(gno_solicitud);
                if (string.IsNullOrEmpty(vl_observaciones_filial))
                {
                    vl_observaciones_filial = "No hay observaciones";
                }




                ReportParameter pa1 = new ReportParameter("pa_solicitud", gno_solicitud.ToString());
                ReportParameter pa2 = new ReportParameter("pa_descripcion_afiliado", sDescripcion_afiliado);
                ReportParameter pa3 = new ReportParameter("pa_descripcion_prestamo", sDescripcion_prestamo);
                ReportParameter pa4 = new ReportParameter("pa_codeudor", sCodeudor);
                ReportParameter pa5 = new ReportParameter("pa_aval1", vl_aval1);
                ReportParameter pa6 = new ReportParameter("pa_aval2", vl_aval2);
                ReportParameter pa7 = new ReportParameter("pa_observaciones_filial", vl_observaciones_filial);

                this.reportViewer1.Reset();

                this.reportViewer1.LocalReport.DataSources.Add(rds);

                reportViewer1.LocalReport.ReportEmbeddedResource = "Docsis_Application.Reportes.rptAnalisisCualitativo.rdlc";
                this.reportViewer1.LocalReport.SetParameters(pa1);
                this.reportViewer1.LocalReport.SetParameters(pa2);
                this.reportViewer1.LocalReport.SetParameters(pa3);
                this.reportViewer1.LocalReport.SetParameters(pa4);
                this.reportViewer1.LocalReport.SetParameters(pa5);
                this.reportViewer1.LocalReport.SetParameters(pa6);
                this.reportViewer1.LocalReport.SetParameters(pa7);
                   
                this.reportViewer1.LocalReport.Refresh();                
                this.reportViewer1.Clear();
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en reporte " + ex.Message);
            }
        }
        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }
        private DataTable procesar_xml_buro(string p_resultado_consulta)
        {

            DataTable tblResultados = new DataTable();
            tblResultados.Columns.Add("Cedula");
            tblResultados.Columns.Add("Rol");
            tblResultados.Columns.Add("Nombre");
            tblResultados.Columns.Add("edad");
            tblResultados.Columns.Add("Precalificado");
            tblResultados.Columns.Add("flags");

            if (string.IsNullOrEmpty(p_resultado_consulta))
                return tblResultados;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(p_resultado_consulta);

            string vl_Status = xmlDoc.SelectSingleNode("DCResponse/Status").InnerText;
            if (vl_Status.ToString() == "Success")
            {
                XmlDocument OutputXML = new XmlDocument();
                OutputXML.LoadXml(xmlDoc.SelectSingleNode("DCResponse/ContextData/Field[@key='OutputXML']").InnerText);
                var t1 = OutputXML.InnerXml;
                StringReader theReader = new StringReader(t1);
                DataSet theDataSet = new DataSet();
                theDataSet.ReadXml(theReader);
                //Del Solicitante
                var dt = theDataSet.Tables[2];
                StringBuilder vl_flags = new StringBuilder();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string dtCedula = "";
                    string dtRol = "";
                    string dtNombre = "";
                    string dtedad = "";
                    string dtdecisionPrecalificado = "";
                    if (dt.Columns.Contains("Cedula"))
                    {
                        dtCedula = dt.Rows[0]["Cedula"].ToString();
                    }
                    if (dt.Columns.Contains("Rol"))
                    {
                        dtRol = dt.Rows[0]["Rol"].ToString();
                    }
                    if (dt.Columns.Contains("Nombre"))
                    {
                        dtNombre = dt.Rows[0]["Nombre"].ToString();
                    }
                    if (dt.Columns.Contains("edad"))
                    {
                        dtedad = dt.Rows[0]["edad"].ToString();
                    }
                    if (dt.Columns.Contains("decisionPrecalificado"))
                    {
                        dtdecisionPrecalificado = dt.Rows[0]["decisionPrecalificado"].ToString();
                    }
                    tblResultados.Rows.Add(dtCedula,
                                       dtRol,
                                       dtNombre,
                                       dtedad,
                                       dtdecisionPrecalificado,
                                       vl_flags);

                }
                //para saber si tiene mas tablas el data set, garante,aval1,aval2
                if (theDataSet.Tables.Count >= 4)
                {
                    //De la 2da persona
                    dt = theDataSet.Tables[3];
                    StringBuilder vl_flags_2persona = new StringBuilder();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        tblResultados.Rows.Add(dt.Rows[0]["Cedula"].ToString(),
                                       dt.Rows[0]["Rol"].ToString(),
                                       dt.Rows[0]["Nombre"].ToString(),
                                       dt.Rows[0]["edad"].ToString(),
                                       dt.Rows[0]["DecisionPrecalificado"].ToString(),
                                       vl_flags_2persona);
                    }
                }
                if (theDataSet.Tables.Count >= 5)
                {
                    //De la 3era persona
                    dt = theDataSet.Tables[4];
                    StringBuilder vl_flags_3persona = new StringBuilder();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        tblResultados.Rows.Add(dt.Rows[0]["Cedula"].ToString(),
                                       dt.Rows[0]["Rol"].ToString(),
                                       dt.Rows[0]["Nombre"].ToString(),
                                       dt.Rows[0]["edad"].ToString(),
                                       dt.Rows[0]["DecisionPrecalificado"].ToString(),
                                       vl_flags_3persona);
                    }
                }
                if (theDataSet.Tables.Count >= 6)
                {
                    //De la 4ta persona
                    dt = theDataSet.Tables[5];
                    StringBuilder vl_flags_4persona = new StringBuilder();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        tblResultados.Rows.Add(dt.Rows[0]["Cedula"].ToString(),
                                       dt.Rows[0]["Rol"].ToString(),
                                       dt.Rows[0]["Nombre"].ToString(),
                                       dt.Rows[0]["edad"].ToString(),
                                       dt.Rows[0]["DecisionPrecalificado"].ToString(),
                                       vl_flags_4persona);

                    }

                }
            }
            return tblResultados;
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

        private void btnGuardarComentarios_Click(object sender, EventArgs e)
        {
            if (da.ActualizarComentariosFilial(gno_solicitud, txtComentarios_filial.Text))
            {
                NotificacionesDll.Notificacion.show_Toast(1500, "Observaciones actualizadas satisfactoriamente..!");      
                mostrar_reporte();                
            }
        }
    }
    public class rtp_ResolucionComite_cls
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
        public string comentarios_filial { get; set; }
    }
}
