using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using wfcModel;
using System.Xml;
using System.IO;

namespace Docsis_Application
{
    public partial class s_AnalisisCredito_01 : Form
    {
        bool con_borde = MDI_Menu.con_borde;

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
                //
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

        
        
   
        

        DataAccess da;
        public s_AnalisisCredito_01(DataAccess da,string p_Nosolicitud)
        {
            InitializeComponent();
            this.da=da;
            labelNoSolicitud.Text = p_Nosolicitud;
        }
        private void s_AnalisisCredito_01_Load(object sender, EventArgs e)
        {
            labelNoSolicitud_trasunion.Text=da.ObtenerApplicationIDxSolicitud(Int32.Parse(labelNoSolicitud.Text)).ToString();
            string resultado_consulta = da.ObtenerXmlRespuestaEvaluacion(Int32.Parse(labelNoSolicitud.Text));
            fecha_preca.Text = da.ObtenerFecha_creacion_tu(Int32.Parse(labelNoSolicitud.Text));
            procesar_xml_respuesta(resultado_consulta);
        }
        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }
        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
      
        private void procesar_xml_respuesta(string p_resultado_consulta)
        {
            if (string.IsNullOrEmpty(p_resultado_consulta))
                return;

            string vl_enter = "\r\n";
            XmlDocument xmlDoc = new XmlDocument();

            string resultado_consulta = p_resultado_consulta;

           
            try
            {
                xmlDoc.LoadXml(resultado_consulta);
                string gxml_respuesta = resultado_consulta;
                string vl_Status = xmlDoc.SelectSingleNode("DCResponse/Status").InnerText;
                if (vl_Status.ToString() == "Success")
                {
                    string vl_ApplicationId = xmlDoc.SelectSingleNode("DCResponse/ResponseInfo/ApplicationId").InnerText;
                    string vl_ReporteCreditoPrincipal = xmlDoc.SelectSingleNode("DCResponse/ContextData/ReporteCreditoPrincipal").InnerText;
                    string vl_gtabla_resultado = xmlDoc.SelectSingleNode("DCResponse/ContextData/TablaResultados").InnerText;


                    XmlDocument OutputXML = new XmlDocument();
                    //txtDecision_solicitud.Text = xmlDoc.SelectSingleNode("DCResponse/ContextData/Field[@key='Decision']").InnerText.ToString();
                    var FLAG_Cierre = xmlDoc.SelectSingleNode("DCResponse/ContextData/Field[@key='FLAG_Cierre']").InnerText.ToString();

                    OutputXML.LoadXml(xmlDoc.SelectSingleNode("DCResponse/ContextData/Field[@key='OutputXML']").InnerText);
                    //gxml_outputxml = OutputXML.InnerXml;

                    string vl_error = OutputXML.SelectSingleNode("DCResponse/Single/MensajeError").InnerText;
                    string vl_decision_solicitud = OutputXML.SelectSingleNode("DCResponse/Single/Decision").InnerText;

                    float nmonto_solicitado = 0;
                    float.TryParse(OutputXML.SelectSingleNode("DCResponse/Single/MotoSolicitado").InnerText.ToString(), out nmonto_solicitado);
                    string vl_monto_solicitado = string.Format(String.Format("{0:###,##0.00}", nmonto_solicitado));


                    float nmonto_aprobado = 0;
                    float.TryParse(OutputXML.SelectSingleNode("DCResponse/Single/MontoAprobado").InnerText, out nmonto_aprobado);
                    string vl_monto_aprobado = string.Format(String.Format("{0:###,##0.00}", nmonto_aprobado));
                    string vl_capacidad_deuda_final = OutputXML.SelectSingleNode("DCResponse/Single/CapacidadDeudaFinal").InnerText;
                    string vl_rci = OutputXML.SelectSingleNode("DCResponse/Single/RelacionCuotaIngreso").InnerText;

                    float ncuota_aprobado = 0;
                    float.TryParse(OutputXML.SelectSingleNode("DCResponse/Single/CuotaMontoAprobado").InnerText, out ncuota_aprobado);
                    string vl_cuotaMontoAprobado = string.Format(String.Format("{0:###,##0.00}", ncuota_aprobado));



                    #region Observaciones a nivel solicitud
                    string vl_obs_01_descripcion = OutputXML.SelectSingleNode("DCResponse/Single/OBS_01_Descripcion").InnerText.Trim();
                    if (!string.IsNullOrEmpty(vl_obs_01_descripcion))
                    {
                        txtObs_solicitud.AppendText(vl_obs_01_descripcion + "." + vl_enter);
                    }
                    string vl_obs_02_descripcion = OutputXML.SelectSingleNode("DCResponse/Single/OBS_02_Descripcion").InnerText.Trim();
                    if (!string.IsNullOrEmpty(vl_obs_02_descripcion))
                    {
                        txtObs_solicitud.AppendText(vl_obs_02_descripcion + "." + vl_enter);
                    }
                    string vl_obs_03_descripcion = OutputXML.SelectSingleNode("DCResponse/Single/OBS_03_Descripcion").InnerText.Trim();
                    if (!string.IsNullOrEmpty(vl_obs_03_descripcion))
                    {
                        txtObs_solicitud.AppendText(vl_obs_03_descripcion + "." + vl_enter);
                    }
                    string vl_obs_04_descripcion = OutputXML.SelectSingleNode("DCResponse/Single/OBS_04_Descripcion").InnerText.Trim();
                    if (!string.IsNullOrEmpty(vl_obs_04_descripcion))
                    {
                        txtObs_solicitud.AppendText(vl_obs_04_descripcion + "." + vl_enter);
                    }
                    string vl_obs_05_descripcion = OutputXML.SelectSingleNode("DCResponse/Single/OBS_05_Descripcion").InnerText.Trim();
                    if (!string.IsNullOrEmpty(vl_obs_05_descripcion))
                    {
                        txtObs_solicitud.AppendText(vl_obs_05_descripcion + "." + vl_enter);
                    }
                    string vl_obs_06_descripcion = OutputXML.SelectSingleNode("DCResponse/Single/OBS_06_Descripcion").InnerText.Trim();
                    if (!string.IsNullOrEmpty(vl_obs_06_descripcion))
                    {
                        txtObs_solicitud.AppendText(vl_obs_06_descripcion + "." + vl_enter);
                    }
                    string vl_obs_07_descripcion = OutputXML.SelectSingleNode("DCResponse/Single/OBS_07_Descripcion").InnerText.Trim();
                    if (!string.IsNullOrEmpty(vl_obs_07_descripcion))
                    {
                        txtObs_solicitud.AppendText(vl_obs_07_descripcion + "." + vl_enter);
                    }

                    string vl_obs_08_descripcion = OutputXML.SelectSingleNode("DCResponse/Single/OBS_08_Descripcion").InnerText.Trim();
                    if (!string.IsNullOrEmpty(vl_obs_08_descripcion))
                    {
                        txtObs_solicitud.AppendText(vl_obs_08_descripcion + "." + vl_enter);
                    }
                    #endregion


                    var tt = OutputXML.InnerText;
                    var t1 = OutputXML.InnerXml;

                    /*txtDecision_solicitud.Text = vl_decision_solicitud;
                    txtMonto_aprobado_resume.Text = vl_monto_aprobado;
                    txtMonto_solicitado_resume.Text = vl_monto_solicitado;
                    txtCapacidad_deuda_final.Text = vl_capacidad_deuda_final;
                    txtCuota_resume.Text = vl_cuotaMontoAprobado;
                    txtRci_resume.Text = vl_rci;
                    txtErrores_trans.Text = vl_error;*/


                    DataTable tblResultados = new DataTable();
                    tblResultados.Columns.Add("id");
                    tblResultados.Columns.Add("rol");
                    tblResultados.Columns.Add("nombre");
                    tblResultados.Columns.Add("edad");
                    tblResultados.Columns.Add("precalificado");
                    tblResultados.Columns.Add("score");
                    tblResultados.Columns.Add("flags");


                    StringReader theReader = new StringReader(t1);
                    DataSet theDataSet = new DataSet();
                    theDataSet.ReadXml(theReader);

                    //Del Solicitante
                    var dt = theDataSet.Tables[2];
                    StringBuilder vl_flags = new StringBuilder();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        #region descripcion de flags
                        if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_01_Descripcion"].ToString()))
                            vl_flags.Append(dt.Rows[0]["FLAG_01_Descripcion"].ToString() + "." + vl_enter);
                        if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_06_Descripcion"].ToString()))
                            vl_flags.Append(dt.Rows[0]["FLAG_06_Descripcion"].ToString() + "." + vl_enter);
                        if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_08_Descripcion"].ToString()))
                            vl_flags.Append(dt.Rows[0]["FLAG_08_Descripcion"].ToString() + "." + vl_enter);
                        if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_09_Descripcion"].ToString()))
                            vl_flags.Append(dt.Rows[0]["FLAG_09_Descripcion"].ToString() + "." + vl_enter);
                        if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_10_Descripcion"].ToString()))
                            vl_flags.Append(dt.Rows[0]["FLAG_10_Descripcion"].ToString() + "." + vl_enter);
                        if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_11_Descripcion"].ToString()))
                            vl_flags.Append(dt.Rows[0]["FLAG_11_Descripcion"].ToString() + "." + vl_enter);
                        if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_12_Descripcion"].ToString()))
                            vl_flags.Append(dt.Rows[0]["FLAG_12_Descripcion"].ToString() + "." + vl_enter);
                        if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_13_Descripcion"].ToString()))
                            vl_flags.Append(dt.Rows[0]["FLAG_13_Descripcion"].ToString() + "." + vl_enter);
                        if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_14_Descripcion"].ToString()))
                            vl_flags.Append(dt.Rows[0]["FLAG_14_Descripcion"].ToString() + "." + vl_enter);
                        if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_15_Descripcion"].ToString()))
                            vl_flags.Append(dt.Rows[0]["FLAG_15_Descripcion"].ToString() + "." + vl_enter);
                        if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_16_Descripcion"].ToString()))
                            vl_flags.Append(dt.Rows[0]["FLAG_16_Descripcion"].ToString() + "." + vl_enter);
                        if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_17_Descripcion"].ToString()))
                            vl_flags.Append(dt.Rows[0]["FLAG_17_Descripcion"].ToString() + "." + vl_enter);
                        if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_18_Descripcion"].ToString()))
                            vl_flags.Append(dt.Rows[0]["FLAG_18_Descripcion"].ToString() + "." + vl_enter);
                        if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_19_Descripcion"].ToString()))
                            vl_flags.Append(dt.Rows[0]["FLAG_19_Descripcion"].ToString() + "." + vl_enter);
                        if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_20_Descripcion"].ToString()))
                            vl_flags.Append(dt.Rows[0]["FLAG_20_Descripcion"].ToString() + "." + vl_enter);
                        if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_21_Descripcion"].ToString()))
                            vl_flags.Append(dt.Rows[0]["FLAG_21_Descripcion"].ToString() + "." + vl_enter);
                        if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_22_Descripcion"].ToString()))
                            vl_flags.Append(dt.Rows[0]["FLAG_22_Descripcion"].ToString() + "." + vl_enter);
                        if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_23_Descripcion"].ToString()))
                            vl_flags.Append(dt.Rows[0]["FLAG_23_Descripcion"].ToString() + "." + vl_enter);
                        if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_24_Descripcion"].ToString()))
                            vl_flags.Append(dt.Rows[0]["FLAG_24_Descripcion"].ToString() + "." + vl_enter);
                        if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_25_Descripcion"].ToString()))
                            vl_flags.Append(dt.Rows[0]["FLAG_25_Descripcion"].ToString() + "." + vl_enter);
                        if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_26_Descripcion"].ToString()))
                            vl_flags.Append(dt.Rows[0]["FLAG_26_Descripcion"].ToString() + "." + vl_enter);
                        if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_27_Descripcion"].ToString()))
                            vl_flags.Append(dt.Rows[0]["FLAG_27_Descripcion"].ToString() + "." + vl_enter);
                        if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_28_Descripcion"].ToString()))
                            vl_flags.Append(dt.Rows[0]["FLAG_28_Descripcion"].ToString() + "." + vl_enter);
                        if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_29_Descripcion"].ToString()))
                            vl_flags.Append(dt.Rows[0]["FLAG_29_Descripcion"].ToString() + "." + vl_enter);
                        if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_30_Descripcion"].ToString()))
                            vl_flags.Append(dt.Rows[0]["FLAG_30_Descripcion"].ToString() + "." + vl_enter);
                        if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_31_Descripcion"].ToString()))
                            vl_flags.Append(dt.Rows[0]["FLAG_31_Descripcion"].ToString() + "." + vl_enter);
                        if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_32_Descripcion"].ToString()))
                            vl_flags.Append(dt.Rows[0]["FLAG_32_Descripcion"].ToString() + "." + vl_enter);
                        #endregion
                        string dtCedula = "";
                        string dtRol = "";
                        string dtNombre = "";
                        string dtedad = "";
                        string dtdecisionPrecalificado = "";
                        string dtscore = "";
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
                        if (dt.Columns.Contains("score"))
                        {
                            dtscore = dt.Rows[0]["score"].ToString();
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
                                       dtscore,
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
                            #region descripcion de flags
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_01_Descripcion"].ToString()))
                                vl_flags_2persona.Append(dt.Rows[0]["FLAG_01_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_06_Descripcion"].ToString()))
                                vl_flags_2persona.Append(dt.Rows[0]["FLAG_06_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_08_Descripcion"].ToString()))
                                vl_flags_2persona.Append(dt.Rows[0]["FLAG_08_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_09_Descripcion"].ToString()))
                                vl_flags_2persona.Append(dt.Rows[0]["FLAG_09_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_10_Descripcion"].ToString()))
                                vl_flags_2persona.Append(dt.Rows[0]["FLAG_10_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_11_Descripcion"].ToString()))
                                vl_flags_2persona.Append(dt.Rows[0]["FLAG_11_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_12_Descripcion"].ToString()))
                                vl_flags_2persona.Append(dt.Rows[0]["FLAG_12_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_13_Descripcion"].ToString()))
                                vl_flags_2persona.Append(dt.Rows[0]["FLAG_13_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_14_Descripcion"].ToString()))
                                vl_flags_2persona.Append(dt.Rows[0]["FLAG_14_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_15_Descripcion"].ToString()))
                                vl_flags_2persona.Append(dt.Rows[0]["FLAG_15_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_16_Descripcion"].ToString()))
                                vl_flags_2persona.Append(dt.Rows[0]["FLAG_16_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_17_Descripcion"].ToString()))
                                vl_flags_2persona.Append(dt.Rows[0]["FLAG_17_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_18_Descripcion"].ToString()))
                                vl_flags_2persona.Append(dt.Rows[0]["FLAG_18_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_19_Descripcion"].ToString()))
                                vl_flags_2persona.Append(dt.Rows[0]["FLAG_19_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_20_Descripcion"].ToString()))
                                vl_flags_2persona.Append(dt.Rows[0]["FLAG_20_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_21_Descripcion"].ToString()))
                                vl_flags_2persona.Append(dt.Rows[0]["FLAG_21_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_22_Descripcion"].ToString()))
                                vl_flags_2persona.Append(dt.Rows[0]["FLAG_22_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_23_Descripcion"].ToString()))
                                vl_flags_2persona.Append(dt.Rows[0]["FLAG_23_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_24_Descripcion"].ToString()))
                                vl_flags_2persona.Append(dt.Rows[0]["FLAG_24_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_25_Descripcion"].ToString()))
                                vl_flags_2persona.Append(dt.Rows[0]["FLAG_25_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_26_Descripcion"].ToString()))
                                vl_flags_2persona.Append(dt.Rows[0]["FLAG_26_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_27_Descripcion"].ToString()))
                                vl_flags_2persona.Append(dt.Rows[0]["FLAG_27_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_28_Descripcion"].ToString()))
                                vl_flags_2persona.Append(dt.Rows[0]["FLAG_28_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_29_Descripcion"].ToString()))
                                vl_flags_2persona.Append(dt.Rows[0]["FLAG_29_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_30_Descripcion"].ToString()))
                                vl_flags_2persona.Append(dt.Rows[0]["FLAG_30_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_31_Descripcion"].ToString()))
                                vl_flags_2persona.Append(dt.Rows[0]["FLAG_31_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_32_Descripcion"].ToString()))
                                vl_flags_2persona.Append(dt.Rows[0]["FLAG_32_Descripcion"].ToString() + "." + vl_enter);
                            #endregion
                            tblResultados.Rows.Add(dt.Rows[0]["Cedula"].ToString(),
                                           dt.Rows[0]["Rol"].ToString(),
                                           dt.Rows[0]["Nombre"].ToString(),
                                           dt.Rows[0]["edad"].ToString(),
                                           dt.Rows[0]["DecisionPrecalificado"].ToString(),
                                           dt.Rows[0]["score"].ToString(),
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
                            #region descripcion de flags
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_01_Descripcion"].ToString()))
                                vl_flags_3persona.Append(dt.Rows[0]["FLAG_01_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_06_Descripcion"].ToString()))
                                vl_flags_3persona.Append(dt.Rows[0]["FLAG_06_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_08_Descripcion"].ToString()))
                                vl_flags_3persona.Append(dt.Rows[0]["FLAG_08_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_09_Descripcion"].ToString()))
                                vl_flags_3persona.Append(dt.Rows[0]["FLAG_09_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_10_Descripcion"].ToString()))
                                vl_flags_3persona.Append(dt.Rows[0]["FLAG_10_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_11_Descripcion"].ToString()))
                                vl_flags_3persona.Append(dt.Rows[0]["FLAG_11_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_12_Descripcion"].ToString()))
                                vl_flags_3persona.Append(dt.Rows[0]["FLAG_12_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_13_Descripcion"].ToString()))
                                vl_flags_3persona.Append(dt.Rows[0]["FLAG_13_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_14_Descripcion"].ToString()))
                                vl_flags_3persona.Append(dt.Rows[0]["FLAG_14_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_15_Descripcion"].ToString()))
                                vl_flags_3persona.Append(dt.Rows[0]["FLAG_15_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_16_Descripcion"].ToString()))
                                vl_flags_3persona.Append(dt.Rows[0]["FLAG_16_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_17_Descripcion"].ToString()))
                                vl_flags_3persona.Append(dt.Rows[0]["FLAG_17_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_18_Descripcion"].ToString()))
                                vl_flags_3persona.Append(dt.Rows[0]["FLAG_18_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_19_Descripcion"].ToString()))
                                vl_flags_3persona.Append(dt.Rows[0]["FLAG_19_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_20_Descripcion"].ToString()))
                                vl_flags_3persona.Append(dt.Rows[0]["FLAG_20_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_21_Descripcion"].ToString()))
                                vl_flags_3persona.Append(dt.Rows[0]["FLAG_21_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_22_Descripcion"].ToString()))
                                vl_flags_3persona.Append(dt.Rows[0]["FLAG_22_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_23_Descripcion"].ToString()))
                                vl_flags_3persona.Append(dt.Rows[0]["FLAG_23_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_24_Descripcion"].ToString()))
                                vl_flags_3persona.Append(dt.Rows[0]["FLAG_24_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_25_Descripcion"].ToString()))
                                vl_flags_3persona.Append(dt.Rows[0]["FLAG_25_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_26_Descripcion"].ToString()))
                                vl_flags_3persona.Append(dt.Rows[0]["FLAG_26_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_27_Descripcion"].ToString()))
                                vl_flags_3persona.Append(dt.Rows[0]["FLAG_27_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_28_Descripcion"].ToString()))
                                vl_flags_3persona.Append(dt.Rows[0]["FLAG_28_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_29_Descripcion"].ToString()))
                                vl_flags_3persona.Append(dt.Rows[0]["FLAG_29_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_30_Descripcion"].ToString()))
                                vl_flags_3persona.Append(dt.Rows[0]["FLAG_30_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_31_Descripcion"].ToString()))
                                vl_flags_3persona.Append(dt.Rows[0]["FLAG_31_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_32_Descripcion"].ToString()))
                                vl_flags_3persona.Append(dt.Rows[0]["FLAG_32_Descripcion"].ToString() + "." + vl_enter);
                            #endregion
                            tblResultados.Rows.Add(dt.Rows[0]["Cedula"].ToString(),
                                           dt.Rows[0]["Rol"].ToString(),
                                           dt.Rows[0]["Nombre"].ToString(),
                                           dt.Rows[0]["edad"].ToString(),
                                           dt.Rows[0]["DecisionPrecalificado"].ToString(),
                                           dt.Rows[0]["score"].ToString(),
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
                            #region descripcion de flags
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_01_Descripcion"].ToString()))
                                vl_flags_4persona.Append(dt.Rows[0]["FLAG_01_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_06_Descripcion"].ToString()))
                                vl_flags_4persona.Append(dt.Rows[0]["FLAG_06_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_08_Descripcion"].ToString()))
                                vl_flags_4persona.Append(dt.Rows[0]["FLAG_08_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_09_Descripcion"].ToString()))
                                vl_flags_4persona.Append(dt.Rows[0]["FLAG_09_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_10_Descripcion"].ToString()))
                                vl_flags_4persona.Append(dt.Rows[0]["FLAG_10_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_11_Descripcion"].ToString()))
                                vl_flags_4persona.Append(dt.Rows[0]["FLAG_11_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_12_Descripcion"].ToString()))
                                vl_flags_4persona.Append(dt.Rows[0]["FLAG_12_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_13_Descripcion"].ToString()))
                                vl_flags_4persona.Append(dt.Rows[0]["FLAG_13_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_14_Descripcion"].ToString()))
                                vl_flags_4persona.Append(dt.Rows[0]["FLAG_14_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_15_Descripcion"].ToString()))
                                vl_flags_4persona.Append(dt.Rows[0]["FLAG_15_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_16_Descripcion"].ToString()))
                                vl_flags_4persona.Append(dt.Rows[0]["FLAG_16_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_17_Descripcion"].ToString()))
                                vl_flags_4persona.Append(dt.Rows[0]["FLAG_17_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_18_Descripcion"].ToString()))
                                vl_flags_4persona.Append(dt.Rows[0]["FLAG_18_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_19_Descripcion"].ToString()))
                                vl_flags_4persona.Append(dt.Rows[0]["FLAG_19_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_20_Descripcion"].ToString()))
                                vl_flags_4persona.Append(dt.Rows[0]["FLAG_20_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_21_Descripcion"].ToString()))
                                vl_flags_4persona.Append(dt.Rows[0]["FLAG_21_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_22_Descripcion"].ToString()))
                                vl_flags_4persona.Append(dt.Rows[0]["FLAG_22_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_23_Descripcion"].ToString()))
                                vl_flags_4persona.Append(dt.Rows[0]["FLAG_23_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_24_Descripcion"].ToString()))
                                vl_flags_4persona.Append(dt.Rows[0]["FLAG_24_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_25_Descripcion"].ToString()))
                                vl_flags_4persona.Append(dt.Rows[0]["FLAG_25_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_26_Descripcion"].ToString()))
                                vl_flags_4persona.Append(dt.Rows[0]["FLAG_26_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_27_Descripcion"].ToString()))
                                vl_flags_4persona.Append(dt.Rows[0]["FLAG_27_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_28_Descripcion"].ToString()))
                                vl_flags_4persona.Append(dt.Rows[0]["FLAG_28_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_29_Descripcion"].ToString()))
                                vl_flags_4persona.Append(dt.Rows[0]["FLAG_29_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_30_Descripcion"].ToString()))
                                vl_flags_4persona.Append(dt.Rows[0]["FLAG_30_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_31_Descripcion"].ToString()))
                                vl_flags_4persona.Append(dt.Rows[0]["FLAG_31_Descripcion"].ToString() + "." + vl_enter);
                            if (!string.IsNullOrEmpty(dt.Rows[0]["FLAG_32_Descripcion"].ToString()))
                                vl_flags_4persona.Append(dt.Rows[0]["FLAG_32_Descripcion"].ToString() + "." + vl_enter);
                            #endregion
                            tblResultados.Rows.Add(dt.Rows[0]["Cedula"].ToString(),
                                           dt.Rows[0]["Rol"].ToString(),
                                           dt.Rows[0]["Nombre"].ToString(),
                                           dt.Rows[0]["edad"].ToString(),
                                           dt.Rows[0]["DecisionPrecalificado"].ToString(),
                                           dt.Rows[0]["score"].ToString(),
                                           vl_flags_4persona);
                        }
                    }
                    gvResultado_buro.DataSource = tblResultados;
                    gvResultado_buro.Refresh();

                    
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void lnkTablaresultado_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string resultado_consulta = da.ObtenerXmlRespuestaEvaluacion(Int32.Parse(labelNoSolicitud.Text));
            if (string.IsNullOrEmpty(resultado_consulta))
                return;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(resultado_consulta);
            try
            {
                string vl_gtabla_resultado = xmlDoc.SelectSingleNode("DCResponse/ContextData/TablaResultados").InnerText;
                p_abrir_tabla_resultado(vl_gtabla_resultado);
            }
            catch
            {
                MessageBox.Show("Hoja de resultados no disponible ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }
        private void p_abrir_tabla_resultado(string p_binario)
        {
            byte[] bits = System.Convert.FromBase64String(p_binario.ToString());
            string sFile = Application.StartupPath.ToString() + @"\tmp\" + DocSys.vl_user + "tmp" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf";
            System.IO.FileStream fs = new System.IO.FileStream(sFile, System.IO.FileMode.Create);
            fs.Write(bits, 0, Convert.ToInt32(bits.Length));
            fs.Close();
            fs.Dispose();
            try
            {
                System.Diagnostics.Process.Start(sFile);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = "rundll32.exe";
                p.StartInfo.Arguments = "shell32.dll,OpenAs_RunDLL " + sFile;
                p.Start();
            }
        }        
        private void lLreportecredito_princ_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string resultado_consulta = da.ObtenerXmlRespuestaEvaluacion(Int32.Parse(labelNoSolicitud.Text));
            if (string.IsNullOrEmpty(resultado_consulta))
                return;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(resultado_consulta);
            try
            {
                string vl_reporte_credioto_solic = xmlDoc.SelectSingleNode("DCResponse/ContextData/ReporteCreditoPrincipal").InnerText;
                p_abrir_reporte_credito(vl_reporte_credioto_solic);
            }
            catch
            {
                MessageBox.Show("Reporte de credito no disponible..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }
        private void lLreportecredito_aval1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string resultado_consulta = da.ObtenerXmlRespuestaEvaluacion(Int32.Parse(labelNoSolicitud.Text));
            if (string.IsNullOrEmpty(resultado_consulta))
                return;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(resultado_consulta);
            try
            {
                string vl_reporte_credioto_aval = xmlDoc.SelectSingleNode("DCResponse/ContextData/ReporteCreditoGarante").InnerText;
                p_abrir_reporte_credito(vl_reporte_credioto_aval);
            }
            catch
            {
                MessageBox.Show("Reporte de credito no disponible..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }
        private void lLreportecredito_aval2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string resultado_consulta = da.ObtenerXmlRespuestaEvaluacion(Int32.Parse(labelNoSolicitud.Text));
            if (string.IsNullOrEmpty(resultado_consulta))
                return;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(resultado_consulta);
            try
            {
                string vl_reporte_credioto_aval2 = xmlDoc.SelectSingleNode("DCResponse/ContextData/ReporteCreditoGarante2").InnerText;
                p_abrir_reporte_credito(vl_reporte_credioto_aval2);
            }
            catch
            {
                MessageBox.Show("Reporte de credito no disponible..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }
        private void p_abrir_reporte_credito(string p_binario)
        {
            byte[] bits = System.Convert.FromBase64String(p_binario.ToString());
            string sFile = Application.StartupPath.ToString() + @"\tmp\" + DocSys.vl_user + "tmp" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".html";
            System.IO.FileStream fs = new System.IO.FileStream(sFile, System.IO.FileMode.Create);
            fs.Write(bits, 0, Convert.ToInt32(bits.Length));
            fs.Close();
            fs.Dispose();
            try
            {
                System.Diagnostics.Process.Start(sFile);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = "rundll32.exe";
                p.StartInfo.Arguments = "shell32.dll,OpenAs_RunDLL " + sFile;
                p.Start();
            }
        }
        private void lLreportecredito_codeudor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string resultado_consulta = da.ObtenerXmlRespuestaEvaluacion(Int32.Parse(labelNoSolicitud.Text));
            if (string.IsNullOrEmpty(resultado_consulta))
                return;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(resultado_consulta);
            try
            {

                string vl_reporte_credioto_codeudor = xmlDoc.SelectSingleNode("DCResponse/ContextData/ReporteCreditoCosolicitante").InnerText;
                p_abrir_reporte_credito(vl_reporte_credioto_codeudor);
            }
            catch
            {
                MessageBox.Show("Reporte de credito no disponible..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        

        
    }
}
