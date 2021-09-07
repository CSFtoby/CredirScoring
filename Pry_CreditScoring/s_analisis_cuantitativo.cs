using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using wfcModel;
using NotificacionesDll;
using System.Globalization;
using System.IO;
using System.Xml;
using Microsoft.VisualBasic;

namespace Docsis_Application
{
    public partial class s_analisis_cuantitativo : Form
    {
        public bool guardado = false;
        public bool hay_modificaciones = false;
        public string tipo_persona = string.Empty;

        public DataAccess da;
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

        public s_analisis_cuantitativo()
        {
            InitializeComponent();
        }

        private void s_analisis_cuantitativo_Load(object sender, EventArgs e)
        {
            llenar_analisis_cuanti();
            DataTable dt = da.ObtenerInfoSolicitud(Int32.Parse(txtNo_solicitud.Text));
        }

        private void llenar_analisis_cuanti()
        {
            Int32 vl_no_solicitud = Int32.Parse(txtNo_solicitud.Text);
            DataTable dtCuanti = da.p_datos_AnalisisCuantitativo(vl_no_solicitud);
            if (dtCuanti.Rows.Count > 0)
            {
                try
                {
                    txtCodigo_cliente.Text = dtCuanti.Rows[0]["codigo_cliente"].ToString();
                    txtFecha_nacimiento.Text = dtCuanti.Rows[0]["fecha_de_nacimiento"].ToString();
                    txtNombre_solicitante.Text = dtCuanti.Rows[0]["nombre_completo"].ToString();
                    txtCodigo_sub_aplicacion.Text = dtCuanti.Rows[0]["codigo_sub_aplicacion"].ToString();
                    labelProducto.Text = "Producto (" + txtCodigo_sub_aplicacion.Text.Trim() + ")";
                    txtProducto.Text = dtCuanti.Rows[0]["producto"].ToString();
                    txtTir.Text = dtCuanti.Rows[0]["tir"].ToString();
                    txtCat.Text = dtCuanti.Rows[0]["cat"].ToString();
                    txtFuente_fondos.Text = dtCuanti.Rows[0]["descripcion_fuente"].ToString();
                    txtEdad.Text = dtCuanti.Rows[0]["edad"].ToString();
                    txtDestino_credito.Text = dtCuanti.Rows[0]["destino"].ToString();
                    
                    txtDescripcion_destino.Text = da.ObtenerDescripcionDestinoCredito(int.Parse(txtDestino_credito.Text));
                    txtCuota_aportacion.Text = string.Format("{0:#,###,###,##0.00}", float.Parse(dtCuanti.Rows[0]["aportacion"].ToString()));
                    txtCoopsalud.Text= string.Format("{0:#,###,###,##0.00}", float.Parse(dtCuanti.Rows[0]["coopsalud"].ToString()));
                    txtMejoras_avaluo.Text = string.Format("{0:#,###,###,##0.00}", float.Parse(dtCuanti.Rows[0]["valor_compra_venta"].ToString()));
                    txtCuota_anticipada.Text = string.Format("{0:#,###,###,##0.00}", float.Parse(dtCuanti.Rows[0]["cuota_anticipada"].ToString()));
                    txtCuota_nivelada.Text = string.Format("{0:#,###,###,##0.00}", float.Parse(dtCuanti.Rows[0]["cuota_nivelada"].ToString()));
                    txtConsolidacion_coopsafa.Text = string.Format("{0:#,###,###,##0.00}", float.Parse(dtCuanti.Rows[0]["consolidar_coopsafa"].ToString()));
                    txtConsolidacion_otros.Text = string.Format("{0:#,###,###,##0.00}", float.Parse(dtCuanti.Rows[0]["consolidar_otros"].ToString()));
                    txtPago_terceros.Text = string.Format("{0:#,###,###,##0.00}", float.Parse(dtCuanti.Rows[0]["pago_terceros"].ToString()));
                    txtComplemento_aportaciones.Text = string.Format("{0:#,###,###,##0.00}", float.Parse(dtCuanti.Rows[0]["complemento_aportaciones"].ToString()));
                    txtTimbres_cooperativos.Text = string.Format("{0:#,###,###,##0.00}", float.Parse(dtCuanti.Rows[0]["timbres_cooperativistas"].ToString()));
                    txtHonorarios_hipoteca.Text = string.Format("{0:#,###,###,##0.00}", float.Parse(dtCuanti.Rows[0]["honorarios_hipoteca"].ToString()));
                    txtHonorarios_compra_venta.Text = string.Format("{0:#,###,###,##0.00}", float.Parse(dtCuanti.Rows[0]["honorarios_compra_venta"].ToString()));
                    txtCapitalizacion.Text = string.Format("{0:#,###,###,##0.00}", float.Parse(dtCuanti.Rows[0]["capitalizacion"].ToString()));
                    txtGastos_administrativos.Text = string.Format("{0:#,###,###,##0.00}", float.Parse(dtCuanti.Rows[0]["gastos_administrativos"].ToString()));
                    txtCentral_riegos.Text = string.Format("{0:#,###,###,##0.00}", float.Parse(dtCuanti.Rows[0]["central_riesgos"].ToString()));
                    txtAvaluo_final.Text = string.Format("{0:#,###,###,##0.00}", float.Parse(dtCuanti.Rows[0]["avaluo_final"].ToString()));
                    txtPapeleria.Text = string.Format("{0:#,###,###,##0.00}", float.Parse(dtCuanti.Rows[0]["papeleria"].ToString()));
                    txtCuota_total.Text = string.Format("{0:#,###,###,##0.00}", float.Parse(dtCuanti.Rows[0]["cuota_total"].ToString()));
                    txtTotal_deducciones.Text = string.Format("{0:#,###,###,##0.00}", float.Parse(dtCuanti.Rows[0]["total_deducciones"].ToString()));
                    txtNeto_recibir.Text = string.Format("{0:#,###,###,##0.00}", float.Parse(dtCuanti.Rows[0]["neto_recibir"].ToString()));

                    txtXmlDeduccionesMCR.Text = dtCuanti.Rows[0]["deducciones_mcr"].ToString();
                    try
                    {
                        if (float.Parse(dtCuanti.Rows[0]["seguro_vida"].ToString()) > 0)
                        {
                            cbSeguro_vida.Checked = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        cbSeguro_vida.Checked = false;
                    }
                }
                catch (Exception ex){}
                
                try
                {
                    txtMonto_solicitado.Text = string.Format("{0:#,###,###,##0.00}", float.Parse(dtCuanti.Rows[0]["monto_solicitado"].ToString()));
                }
                catch {}
                try
                {
                    txtPlazo.Text = string.Format("{0:###0}", int.Parse(dtCuanti.Rows[0]["plazo_sol"].ToString()));
                }
                catch {}
                try
                {
                    CultureInfo culture = CultureInfo.CreateSpecificCulture("es-HN");
                    decimal tasa_decimal = Convert.ToDecimal(dtCuanti.Rows[0]["tasa_sol"].ToString());
                    txtTasa.Text = string.Format("{0:#,###,###,##0.00}", decimal.Parse(tasa_decimal.ToString()));// tasa_decimal.ToString();
                }
                catch{}
                txtPlazo.Text = dtCuanti.Rows[0]["plazo_sol"].ToString();
                
                int vl_codigo_sub_app = 0;
                try
                {
                    vl_codigo_sub_app = int.Parse(txtCodigo_sub_aplicacion.Text);
                }
                catch
                {
                    MessageBox.Show("no se pudo obtener la cuota");
                }
                txtCuota_nivelada.Text = da.ObtenerCuotaNivela(vl_codigo_sub_app,float.Parse(txtMonto_solicitado.Text), decimal.Parse(txtTasa.Text), float.Parse(txtPlazo.Text)).ToString();                
                txtEdad.Text = dtCuanti.Rows[0]["edad"].ToString();
                txtGuidID_generado.Text = dtCuanti.Rows[0]["GUID"].ToString();
                txtInteres_total.Text = string.Format("{0:#,###,###,##0.00}", da.ObtenerInteresTotalTIRCAT(txtGuidID_generado.Text));
                
                try
                {
                    DataTable dt = da.ObtenerGastosDesembolsoMCR(txtCodigo_cliente.Text, txtNo_solicitud.Text);
                    gvGastos_desembolso.AutoGenerateColumns = false;
                    gvGastos_desembolso.DataSource = dt;
                    
                    //Recuperando las doceavas de los seguros de vida, incendio y daños de los registros generados desde el modulo de MCR
                    decimal doceava_seguro_vida = 0;
                    decimal doceava_seguro_daños = 0;
                    decimal doceava_seguro_incendio = 0;

                    var datarow1 = dt.Select("cod_tipocobro='DV'");
                    foreach (DataRow row in datarow1)
                    {
						try
						{
							decimal.TryParse(row.Field<string>("mon_cobro"),out doceava_seguro_vida);
						}
						catch (Exception)
						{
							var valor = row["mon_cobro"].ToString();
							doceava_seguro_vida = Convert.ToDecimal(valor);
						}
						doceava_seguro_vida = doceava_seguro_vida / 12;
                    }

                    var datarow2 = dt.Select("cod_tipocobro='DI'");
                    foreach (DataRow row in datarow2)
                    {
						try
						{
							decimal.TryParse(row.Field<string>("mon_cobro"), out doceava_seguro_incendio);
						}
						catch (Exception)
						{
							var valor = row["mon_cobro"].ToString();
							doceava_seguro_incendio = Convert.ToDecimal(valor);
						}
                        doceava_seguro_incendio = doceava_seguro_incendio / 12;
                    }
                    txtSeguro_vida_mens.Text = doceava_seguro_vida.ToString("###,###,###,##0.00");
                    txtSeguro_incendios_mens.Text = doceava_seguro_incendio.ToString("###,###,###,##0.00");
                }
                catch (Exception ex){}
                calcular_total();
            }
        }

        public DataTable xml_to_datatable(string xmlData)
        {
            StringReader theReader = new StringReader(xmlData);
            DataSet theDataSet = new DataSet();
            theDataSet.ReadXml(theReader);

            return theDataSet.Tables[1];
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (hay_modificaciones && !guardado)
            {
                if (MessageBox.Show("Ha realizado cambios sobre el Análisis cuantitativo, desea salir sin guardarlos ?","Aviso",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.No)
                {
                    return;
                }
            }
            this.Close();
        }

        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }

        private void btnCarlcularLiq_Click(object sender, EventArgs e)
        {
            txtPlazo_Leave(null, null);
            string vl_toma_seguro = "N";
            if (cbSeguro_vida.Checked)
            {
                vl_toma_seguro = "S";
            }
            //71	AGROCREDITO 8.7	1%	PARA PERSONAS NATURALES 1.50%	PARA PERSONAS JURIDICAS
            Int32 vl_no_solicitud = Int32.Parse(txtNo_solicitud.Text);
            DataTable dtCuanti = da.p_datos_AnalisisCuantitativo(vl_no_solicitud);
            if (dtCuanti.Rows.Count > 0)
            {
                tipo_persona = dtCuanti.Rows[0]["tipo_de_persona"].ToString();
            }

            DataTable dtLiqui = da.ObtenerValoresLiquidacion(Int32.Parse(txtNo_solicitud.Text),
                                                             int.Parse(txtCodigo_sub_aplicacion.Text),
                                                             (txtFuente_fondos.Text).ToString(),
                                                             int.Parse(txtDestino_credito.Text),
                                                             int.Parse(txtEdad.Text),
                                                             int.Parse(txtPlazo.Text),
                                                             vl_toma_seguro,
                                                             double.Parse(txtHonorarios_compra_venta.Text),
                                                             double.Parse(txtMejoras_avaluo.Text),
                                                             double.Parse(txtMonto_solicitado.Text),
                                                             double.Parse(txtCuota_nivelada.Text),
                                                             double.Parse(txtCuota_aportacion.Text),
                                                             double.Parse(txtCoopsalud.Text),
                                                             double.Parse(txtTasa.Text),
                                                             double.Parse(txtNeto_recibir.Text));
            if (dtLiqui.Rows.Count > 0)
            {
                txtTimbres_cooperativos.Text = string.Format("{0:#,###,###,##0.00}", Convert.ToDecimal(dtLiqui.Rows[0]["timbres_cooperativos"].ToString()));
                txtCapitalizacion.Text = string.Format("{0:#,###,###,##0.00}", Convert.ToDecimal(dtLiqui.Rows[0]["capitalizacion"].ToString()));
                txtCuota_anticipada.Text = string.Format("{0:#,###,###,##0.00}", Convert.ToDecimal(dtLiqui.Rows[0]["cuota_anticipada"].ToString()));

                txtSeguro_danos.Text = string.Format("{0:#,###,###,##0.00}", Convert.ToDecimal(dtLiqui.Rows[0]["seguro_danos"].ToString()));
                txtSeguro_vida.Text = string.Format("{0:#,###,###,##0.00}", Convert.ToDecimal(dtLiqui.Rows[0]["seguro_vida"].ToString()));
                txtSeguro_incendio.Text = string.Format("{0:#,###,###,##0.00}", Convert.ToDecimal(dtLiqui.Rows[0]["seguro_incendios"].ToString()));

                txtGastos_administrativos.Text = string.Format("{0:#,###,###,##0.00}", Convert.ToDecimal(dtLiqui.Rows[0]["gastos_administrativos"].ToString()));
                txtCentral_riegos.Text = string.Format("{0:#,###,###,##0.00}", Convert.ToDecimal(dtLiqui.Rows[0]["central_riesgos"].ToString()));
                txtPapeleria.Text = string.Format("{0:#,###,###,##0.00}", Convert.ToDecimal(dtLiqui.Rows[0]["papeleria"].ToString()));
                txtHonorarios_hipoteca.Text = string.Format("{0:#,###,###,##0.00}", Convert.ToDecimal(dtLiqui.Rows[0]["honorarios_hipoteca"].ToString()));


                txtSeguro_vida_mens.Text = string.Format("{0:#,###,###,##0.00}", float.Parse(dtLiqui.Rows[0]["seguro_vida_mensual"].ToString()));
                txtSeguro_incendios_mens.Text = string.Format("{0:#,###,###,##0.00}", float.Parse(dtLiqui.Rows[0]["seguro_incendios_mensual"].ToString()));

                double vl_prima_vida = Convert.ToDouble(dtLiqui.Rows[0]["prima_seguro"].ToString());
                double vl_prima_incendios = Convert.ToDouble(dtLiqui.Rows[0]["prima_incedios"].ToString());

                //da.ActualizarMontoGastos(Int32.Parse(txtNo_solicitud.Text), Convert.ToDouble(txtHonorarios_hipoteca.Text));
                //da.ActualizarMontoGastosDI(Int32.Parse(txtNo_solicitud.Text), vl_prima_incendios);
                //da.ActualizarMontoGastosDV(Int32.Parse(txtNo_solicitud.Text), vl_prima_vida);

                /*CAT / TIR*/
                float vl_tir = 0;
                float.TryParse(dtLiqui.Rows[0]["TIR"].ToString(), out vl_tir);
                txtTir.Text= string.Format("{0:#,###,###,##0.00}", vl_tir);

                float vl_cat = 0;
                float.TryParse(dtLiqui.Rows[0]["CAT"].ToString(), out vl_cat);
                txtCat.Text = string.Format("{0:#,###,###,##0.00}", vl_cat);               
                txtGuidID_generado.Text = dtLiqui.Rows[0]["GUID"].ToString();
                txtInteres_total.Text = string.Format("{0:#,###,###,##0.00}",da.ObtenerInteresTotalTIRCAT(txtGuidID_generado.Text));
                /*------------------*/
                if (cbMcr.Checked)
                {
                    da.p_generar_gastos_desembolso_mcr(txtCodigo_cliente.Text,DateTime.Parse(txtFecha_nacimiento.Text), 
                                                       txtCodigo_sub_aplicacion.Text, 
                                                       Int16.Parse(txtPlazo.Text), 
                                                       decimal.Parse(txtTasa.Text), 
                                                       decimal.Parse(txtMonto_solicitado.Text), 
                                                       DocSys.vl_user,
                                                       Int64.Parse(txtNo_solicitud.Text));

                    da.ActualizarMontoGastos(Int32.Parse(txtNo_solicitud.Text), Convert.ToDouble(txtHonorarios_hipoteca.Text));
                    da.ActualizarMontoGastosDI(Int32.Parse(txtNo_solicitud.Text), vl_prima_incendios);
                    da.ActualizarMontoGastosDV(Int32.Parse(txtNo_solicitud.Text), vl_prima_vida);

                    var dtGastos = da.ObtenerGastosDesembolsoMCR(txtCodigo_cliente.Text,txtNo_solicitud.Text);
                    var x = dtGastos.AsEnumerable().Select(r => Convert.ToDecimal(r.Field<decimal>("mon_cobro"))).Sum();
                    
                    gvGastos_desembolso.AutoGenerateColumns = false;
                    gvGastos_desembolso.DataSource = dtGastos;                    
                    gvGastos_desembolso.Refresh();
                    
                    if (dtGastos.Rows.Count>0)
                    {
                        //string xml = p_construir_xml_DeduccionesMCR((DataTable)gvGastos_desembolso.DataSource);
                        //txtXmlDeduccionesMCR.Text = xml;
                        
                        //DataTable dt = xml_to_datatable(xml);
                        DataTable dt = dtGastos;
                        
                        decimal cuota_nivelada = 0;
                        decimal.TryParse(txtCuota_nivelada.Text, out cuota_nivelada);
                        
                        decimal cuota_aportaciones = 0;
                        decimal.TryParse(txtCuota_aportacion.Text, out cuota_aportaciones);

                        decimal coopsalud = 0;
                        decimal.TryParse(txtCoopsalud.Text, out coopsalud);
                        //Recuperando las doceavas de los seguros de vida, incendio y daños de los registros generados desde el modulo de MCR
                        decimal doceava_seguro_vida = 0;
                        decimal doceava_seguro_daños = 0;
                        decimal doceava_seguro_incendio = 0;                        
                        /*--------vida----------------*/
                        //var datarow1 = dt.Select("cod_tipocobro='DV'");
                        //foreach (DataRow row in datarow1)
                        //{                            
                        //    decimal.TryParse(row[3].ToString(), out doceava_seguro_vida);
                        //    doceava_seguro_vida = Convert.ToDecimal(doceava_seguro_vida);
                        //}
                        doceava_seguro_vida = Convert.ToDecimal(txtSeguro_vida_mens.Text);

                        /*--------incendio----------------*/
                        //var datarow2 = dt.Select("cod_tipocobro='DI'");
                        //foreach (DataRow row in datarow2)
                        //{
                        //    decimal.TryParse(row[3].ToString(), out doceava_seguro_incendio);
                        //    doceava_seguro_incendio = Convert.ToDecimal(doceava_seguro_incendio);
                        //}
                        doceava_seguro_incendio = Convert.ToDecimal(txtSeguro_incendios_mens.Text);

                        //txtSeguro_vida_mens.Text = doceava_seguro_vida.ToString("###,###,###,##0.00");
                        //txtSeguro_incendios_mens.Text = doceava_seguro_incendio.ToString("###,###,###,##0.00");

                        decimal vl_cuota_total = cuota_nivelada + coopsalud + cuota_aportaciones + doceava_seguro_vida + doceava_seguro_incendio + doceava_seguro_daños;                         
                        txtCuota_total.Text = string.Format("{0:#,###,###,##0.00}", vl_cuota_total);

                        calcular_total();
                    }
                }
            }
            hay_modificaciones = true;
            guardado = false;
        }

        private void calcular_total()
        {
            decimal vl_cuota_anticipada = decimal.Parse(txtCuota_anticipada.Text);
            decimal vl_consolidacion_coopsafa = decimal.Parse(txtConsolidacion_coopsafa.Text);
            decimal vl_consolidacion_otros = decimal.Parse(txtConsolidacion_otros.Text);
            decimal vl_pago_terceros = decimal.Parse(txtPago_terceros.Text);
            decimal vl_complemento_aportaciones = decimal.Parse(txtComplemento_aportaciones.Text);
            decimal vl_timbres_cooperativos = decimal.Parse(txtTimbres_cooperativos.Text);
            decimal vl_honorarios_hipoteca = decimal.Parse(txtHonorarios_hipoteca.Text);
            decimal vl_honotarios_compra_venta = decimal.Parse(txtHonorarios_compra_venta.Text);

            decimal vl_capitalizacion = decimal.Parse(txtCapitalizacion.Text);
            decimal vl_seguro_vida = decimal.Parse(txtSeguro_vida.Text);
            decimal vl_seguro_danos = decimal.Parse(txtSeguro_danos.Text);
            decimal vl_seguro_incendios = decimal.Parse(txtSeguro_incendio.Text);
            decimal vl_gastos_administrativos = decimal.Parse(txtGastos_administrativos.Text);
            decimal vl_central_riesgos = decimal.Parse(txtCentral_riegos.Text);
            decimal vl_papeleria = decimal.Parse(txtPapeleria.Text);
            decimal vl_avaluo_final = decimal.Parse(txtAvaluo_final.Text);

            decimal vl_monto = decimal.Parse(txtMonto_solicitado.Text);
            decimal vl_deducciones = vl_cuota_anticipada + vl_consolidacion_coopsafa + vl_consolidacion_otros + vl_pago_terceros + vl_complemento_aportaciones + vl_timbres_cooperativos + vl_honorarios_hipoteca + vl_honotarios_compra_venta +
                                     vl_capitalizacion + vl_seguro_vida + vl_seguro_danos + vl_seguro_incendios + vl_gastos_administrativos + vl_central_riesgos + vl_papeleria + vl_avaluo_final;
            
            decimal vl_cuota_nivelada = decimal.Parse(txtCuota_nivelada.Text);
            decimal vl_aportaciones = decimal.Parse(txtCuota_aportacion.Text);
            decimal vl_coopsalud = decimal.Parse(txtCoopsalud.Text);
            decimal vl_seguro_vida_mens = decimal.Parse(txtSeguro_vida_mens.Text);
            decimal vl_seguro_danos_mens = decimal.Parse(txtSeguro_danos_mens.Text);
            decimal vl_seguro_incendio_mens = decimal.Parse(txtSeguro_incendios_mens.Text);
            decimal vl_total_cuota = vl_cuota_nivelada + vl_aportaciones + vl_coopsalud + vl_seguro_vida_mens + vl_seguro_incendio_mens + vl_seguro_danos_mens ;
            
            var dtGastos = da.ObtenerGastosDesembolsoMCR(txtCodigo_cliente.Text,txtNo_solicitud.Text);            
            var vl_dducciones_mcr = dtGastos.AsEnumerable().Select(r => Convert.ToDecimal(r.Field<decimal>("mon_cobro"))).Sum();
                        
            decimal vl_neto_recibir = vl_monto - vl_dducciones_mcr;

            txtCuota_total.Text = string.Format(String.Format("{0:#,###,###,##0.00}", vl_total_cuota));
            txtTotal_deducciones.Text = string.Format(String.Format("{0:#,###,###,##0.00}", vl_dducciones_mcr));            
            txtNeto_recibir.Text = string.Format(String.Format("{0:#,###,###,##0.00}", vl_neto_recibir));
        }

        private string p_construir_xml_DeduccionesMCR(DataTable dtparam)
        {

            StringBuilder xml = new StringBuilder();
            xml.Append(@"<DeduccionsMCR><Deducciones>");
            
            foreach (DataRow row in dtparam.Rows)
            {
                string xmlRoot = @"<DatosDeduccion>
                                  <cod_tipocobro>" + row[0].ToString() + @"</cod_tipocobro>
                                  <des_tipocobro>" + row[1].ToString() + @" </des_tipocobro >
                                      <mon_cobro>" + row[2].ToString() + @"</mon_cobro>
                             <mon_cobro_original>" + row[3].ToString() + @"</mon_cobro_original>
                               <cod_moneda_cobro>" + row[4].ToString() + @"</cod_moneda_cobro>                              
                                </DatosDeduccion>";
                xml.Append(xmlRoot);
            }
                
            xml.Append(@"</Deducciones></DeduccionsMCR>");
            StringReader theReader = new StringReader(xml.ToString());
            DataSet theDataSet = new DataSet();
            theDataSet.ReadXml(theReader);
            return xml.ToString();
        }

        private bool EsXmlConFormatoOk(string xml)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmsRpts.frmRpt_AnalisisCuantitativo forma = new FrmsRpts.frmRpt_AnalisisCuantitativo();
            forma.da = da;
            forma.gno_solicitud = Int32.Parse(txtNo_solicitud.Text);
            forma.dtDetalle_deduc = (DataTable)gvGastos_desembolso.DataSource;
            forma.ShowDialog();
        }

        private void txtMejoras_avaluo_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox texbox = txtMejoras_avaluo;
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }
            // al dar enter pasar al siguiente campo
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{TAB}");
            }
            //Si el texto esta sombreado todo caer encima al dato del textbox
            if (texbox.SelectionLength >= texbox.Text.Length)
            {
                texbox.Text = "";
            }

            //Para obligar a que sólo se introduzcan números 
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan 
                    e.Handled = true;
                }
            }
            //Permitir el punto decimal
            bool IsDec = false;
            int nroDec = 0;
            for (int i = 0; i < texbox.Text.Length; i++)
            {
                if (texbox.Text[i] == '.')
                    IsDec = true;

                if (IsDec && nroDec++ >= 2)
                {
                    e.Handled = true;
                    return;
                }
            }
            if (e.KeyChar >= 48 && e.KeyChar <= 57)
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = (IsDec) ? true : false;
            else
                e.Handled = true;
        }

        private void txtMejoras_avaluo_Leave(object sender, EventArgs e)
        {
            TextBox texbox = txtMejoras_avaluo;
            double valor_ingresado = 0;
            if (!string.IsNullOrEmpty(texbox.Text))
                valor_ingresado = Convert.ToDouble(texbox.Text);

            texbox.Text = string.Format(String.Format("{0:#,###,###,##0.00}", valor_ingresado));
        }

        private void txtCuota_aportacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox texbox = txtCuota_aportacion;
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }
            // al dar enter pasar al siguiente campo
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{TAB}");
            }
            //Si el texto esta sombreado todo caer encima al dato del textbox
            if (texbox.SelectionLength >= texbox.Text.Length)
            {
                texbox.Text = "";
            }

            //Para obligar a que sólo se introduzcan números 
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {

                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan 
                    e.Handled = true;
                }
            }
            //Permitir el punto decimal
            bool IsDec = false;
            int nroDec = 0;
            for (int i = 0; i < texbox.Text.Length; i++)
            {
                if (texbox.Text[i] == '.')
                    IsDec = true;

                if (IsDec && nroDec++ >= 2)
                {
                    e.Handled = true;
                    return;
                }
            }
            if (e.KeyChar >= 48 && e.KeyChar <= 57)
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = (IsDec) ? true : false;
            else
                e.Handled = true;
        }

        private void txtCuota_aportacion_Leave(object sender, EventArgs e)
        {
            TextBox texbox = txtCuota_aportacion;
            double valor_ingresado = 0;
            if (!string.IsNullOrEmpty(texbox.Text))
                valor_ingresado = Convert.ToDouble(texbox.Text);

            texbox.Text = string.Format(String.Format("{0:#,###,###,##0.00}", valor_ingresado));
        }

        private void txtCoopsalud_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox texbox = txtCoopsalud;
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }
            // al dar enter pasar al siguiente campo
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{TAB}");
            }
            //Si el texto esta sombreado todo caer encima al dato del textbox
            if (texbox.SelectionLength >= texbox.Text.Length)
            {
                texbox.Text = "";
            }

            //Para obligar a que sólo se introduzcan números 
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan 
                    e.Handled = true;
                }
            }
            //Permitir el punto decimal
            bool IsDec = false;
            int nroDec = 0;
            for (int i = 0; i < texbox.Text.Length; i++)
            {
                if (texbox.Text[i] == '.')
                    IsDec = true;

                if (IsDec && nroDec++ >= 2)
                {
                    e.Handled = true;
                    return;
                }
            }
            if (e.KeyChar >= 48 && e.KeyChar <= 57)
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = (IsDec) ? true : false;
            else
                e.Handled = true;
        }

        private void txtCoopsalud_Leave(object sender, EventArgs e)
        {
            TextBox texbox = txtCoopsalud;
            double valor_ingresado = 0;
            if (!string.IsNullOrEmpty(texbox.Text))
                valor_ingresado = Convert.ToDouble(texbox.Text);

            texbox.Text = string.Format(String.Format("{0:#,###,###,##0.00}", valor_ingresado));
        }

        private void txtConsolidacion_coopsafa_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox texbox = txtConsolidacion_coopsafa;
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }
            // al dar enter pasar al siguiente campo
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{TAB}");
            }
            //Si el texto esta sombreado todo caer encima al dato del textbox
            if (texbox.SelectionLength >= texbox.Text.Length)
            {
                texbox.Text = "";
            }

            //Para obligar a que sólo se introduzcan números 
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan 
                    e.Handled = true;
                }
            }
            //Permitir el punto decimal
            bool IsDec = false;
            int nroDec = 0;
            for (int i = 0; i < texbox.Text.Length; i++)
            {
                if (texbox.Text[i] == '.')
                    IsDec = true;

                if (IsDec && nroDec++ >= 2)
                {
                    e.Handled = true;
                    return;
                }
            }
            if (e.KeyChar >= 48 && e.KeyChar <= 57)
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = (IsDec) ? true : false;
            else
                e.Handled = true;
        }

        private void txtConsolidacion_coopsafa_Leave(object sender, EventArgs e)
        {
            TextBox texbox = txtConsolidacion_coopsafa;
            double valor_ingresado = 0;
            if (!string.IsNullOrEmpty(texbox.Text))
                valor_ingresado = Convert.ToDouble(texbox.Text);

            texbox.Text = string.Format(String.Format("{0:#,###,###,##0.00}", valor_ingresado));
            calcular_total();
        }

        private void txtConsolidacion_otros_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox texbox = txtConsolidacion_otros;
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }
            // al dar enter pasar al siguiente campo
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{TAB}");
            }
            //Si el texto esta sombreado todo caer encima al dato del textbox
            if (texbox.SelectionLength >= texbox.Text.Length)
            {
                texbox.Text = "";
            }
            //Para obligar a que sólo se introduzcan números 
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan 
                    e.Handled = true;
                }
            }
            //Permitir el punto decimal
            bool IsDec = false;
            int nroDec = 0;
            for (int i = 0; i < texbox.Text.Length; i++)
            {
                if (texbox.Text[i] == '.')
                    IsDec = true;

                if (IsDec && nroDec++ >= 2)
                {
                    e.Handled = true;
                    return;
                }
            }
            if (e.KeyChar >= 48 && e.KeyChar <= 57)
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = (IsDec) ? true : false;
            else
                e.Handled = true;
        }

        private void txtConsolidacion_otros_Leave(object sender, EventArgs e)
        {
            TextBox texbox = txtConsolidacion_otros;
            double valor_ingresado = 0;
            if (!string.IsNullOrEmpty(texbox.Text))
                valor_ingresado = Convert.ToDouble(texbox.Text);

            texbox.Text = string.Format(String.Format("{0:#,###,###,##0.00}", valor_ingresado));
            calcular_total();
        }

        private void txtPago_terceros_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox texbox = txtPago_terceros;
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }
            // al dar enter pasar al siguiente campo
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{TAB}");
            }
            //Si el texto esta sombreado todo caer encima al dato del textbox
            if (texbox.SelectionLength >= texbox.Text.Length)
            {
                texbox.Text = "";
            }

            //Para obligar a que sólo se introduzcan números 
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan 
                    e.Handled = true;
                }
            }
            //Permitir el punto decimal
            bool IsDec = false;
            int nroDec = 0;
            for (int i = 0; i < texbox.Text.Length; i++)
            {
                if (texbox.Text[i] == '.')
                    IsDec = true;

                if (IsDec && nroDec++ >= 2)
                {
                    e.Handled = true;
                    return;
                }
            }
            if (e.KeyChar >= 48 && e.KeyChar <= 57)
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = (IsDec) ? true : false;
            else
                e.Handled = true;
        }

        private void txtPago_terceros_Leave(object sender, EventArgs e)
        {
            TextBox texbox = txtPago_terceros;
            double valor_ingresado = 0;
            if (!string.IsNullOrEmpty(texbox.Text))
                valor_ingresado = Convert.ToDouble(texbox.Text);

            texbox.Text = string.Format(String.Format("{0:#,###,###,##0.00}", valor_ingresado));
            calcular_total();
        }

        private void txtComplemento_aportaciones_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox texbox = txtComplemento_aportaciones;
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }
            // al dar enter pasar al siguiente campo
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{TAB}");
            }
            //Si el texto esta sombreado todo caer encima al dato del textbox
            if (texbox.SelectionLength >= texbox.Text.Length)
            {
                texbox.Text = "";
            }
            //Para obligar a que sólo se introduzcan números 
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan 
                    e.Handled = true;
                }
            }
            //Permitir el punto decimal
            bool IsDec = false;
            int nroDec = 0;
            for (int i = 0; i < texbox.Text.Length; i++)
            {
                if (texbox.Text[i] == '.')
                    IsDec = true;

                if (IsDec && nroDec++ >= 2)
                {
                    e.Handled = true;
                    return;
                }
            }
            if (e.KeyChar >= 48 && e.KeyChar <= 57)
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = (IsDec) ? true : false;
            else
                e.Handled = true;
        }

        private void txtComplemento_aportaciones_Leave(object sender, EventArgs e)
        {
            TextBox texbox = txtComplemento_aportaciones;
            double valor_ingresado = 0;
            if (!string.IsNullOrEmpty(texbox.Text))
                valor_ingresado = Convert.ToDouble(texbox.Text);

            texbox.Text = string.Format(String.Format("{0:#,###,###,##0.00}", valor_ingresado));
            calcular_total();
        }

        private void txtAvaluo_final_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox texbox = txtAvaluo_final;
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }
            // al dar enter pasar al siguiente campo
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{TAB}");
            }
            //Si el texto esta sombreado todo caer encima al dato del textbox
            if (texbox.SelectionLength >= texbox.Text.Length)
            {
                texbox.Text = "";
            }
            //Para obligar a que sólo se introduzcan números 
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan 
                    e.Handled = true;
                }
            }
            //Permitir el punto decimal
            bool IsDec = false;
            int nroDec = 0;
            for (int i = 0; i < texbox.Text.Length; i++)
            {
                if (texbox.Text[i] == '.')
                    IsDec = true;

                if (IsDec && nroDec++ >= 2)
                {
                    e.Handled = true;
                    return;
                }
            }
            if (e.KeyChar >= 48 && e.KeyChar <= 57)
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = (IsDec) ? true : false;
            else
                e.Handled = true;
        }

        private void txtAvaluo_final_Leave(object sender, EventArgs e)
        {
            TextBox texbox = txtAvaluo_final;
            double valor_ingresado = 0;
            if (!string.IsNullOrEmpty(texbox.Text))
                valor_ingresado = Convert.ToDouble(texbox.Text);

            texbox.Text = string.Format(String.Format("{0:#,###,###,##0.00}", valor_ingresado));
            calcular_total();
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.EnableRaisingEvents = false;
            proc.StartInfo.FileName = "calc";
            proc.Start();
        }

        private void txtHonorarios_compra_venta_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox texbox = txtHonorarios_compra_venta;
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }
            // al dar enter pasar al siguiente campo
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{TAB}");
            }
            //Si el texto esta sombreado todo caer encima al dato del textbox
            if (texbox.SelectionLength >= texbox.Text.Length)
            {
                texbox.Text = "";
            }
            //Para obligar a que sólo se introduzcan números 
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan 
                    e.Handled = true;
                }
            }
            //Permitir el punto decimal
            bool IsDec = false;
            int nroDec = 0;
            for (int i = 0; i < texbox.Text.Length; i++)
            {
                if (texbox.Text[i] == '.')
                    IsDec = true;

                if (IsDec && nroDec++ >= 2)
                {
                    e.Handled = true;
                    return;
                }
            }
            if (e.KeyChar >= 48 && e.KeyChar <= 57)
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = (IsDec) ? true : false;
            else
                e.Handled = true;
        }

        private void txtHonorarios_compra_venta_Leave(object sender, EventArgs e)
        {
            TextBox texbox = txtHonorarios_compra_venta;
            double valor_ingresado = 0;
            if (!string.IsNullOrEmpty(texbox.Text))
                valor_ingresado = Convert.ToDouble(texbox.Text);

            texbox.Text = string.Format(String.Format("{0:#,###,###,##0.00}", valor_ingresado));
            calcular_total();
        }
        
        private void txtCuota_anticipada_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox texbox = txtCuota_anticipada;
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }
            // al dar enter pasar al siguiente campo
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{TAB}");
            }
            //Si el texto esta sombreado todo caer encima al dato del textbox
            if (texbox.SelectionLength >= texbox.Text.Length)
            {
                texbox.Text = "";
            }
            //Para obligar a que sólo se introduzcan números 
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan 
                    e.Handled = true;
                }
            }
            //Permitir el punto decimal
            bool IsDec = false;
            int nroDec = 0;
            for (int i = 0; i < texbox.Text.Length; i++)
            {
                if (texbox.Text[i] == '.')
                    IsDec = true;

                if (IsDec && nroDec++ >= 2)
                {
                    e.Handled = true;
                    return;
                }
            }
            if (e.KeyChar >= 48 && e.KeyChar <= 57)
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = (IsDec) ? true : false;
            else
                e.Handled = true;
        }

        private void txtCuota_anticipada_Leave(object sender, EventArgs e)
        {
            TextBox texbox = txtCuota_anticipada;
            double valor_ingresado = 0;
            if (!string.IsNullOrEmpty(texbox.Text))
                valor_ingresado = Convert.ToDouble(texbox.Text);

            texbox.Text = string.Format(String.Format("{0:#,###,###,##0.00}", valor_ingresado));
            calcular_total();
        }

        private void txtCapitalizacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox texbox = txtCapitalizacion;
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }
            // al dar enter pasar al siguiente campo
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{TAB}");
            }
            //Si el texto esta sombreado todo caer encima al dato del textbox
            if (texbox.SelectionLength >= texbox.Text.Length)
            {
                texbox.Text = "";
            }
            //Para obligar a que sólo se introduzcan números 
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan 
                    e.Handled = true;
                }
            }
            //Permitir el punto decimal
            bool IsDec = false;
            int nroDec = 0;
            for (int i = 0; i < texbox.Text.Length; i++)
            {
                if (texbox.Text[i] == '.')
                    IsDec = true;

                if (IsDec && nroDec++ >= 2)
                {
                    e.Handled = true;
                    return;
                }
            }
            if (e.KeyChar >= 48 && e.KeyChar <= 57)
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = (IsDec) ? true : false;
            else
                e.Handled = true;
        }

        private void txtCapitalizacion_Leave(object sender, EventArgs e)
        {
            TextBox texbox = txtCapitalizacion;
            double valor_ingresado = 0;
            if (!string.IsNullOrEmpty(texbox.Text))
                valor_ingresado = Convert.ToDouble(texbox.Text);

            texbox.Text = string.Format(String.Format("{0:#,###,###,##0.00}", valor_ingresado));
            calcular_total();
        }

        private void txtGastos_administrativos_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox texbox = txtGastos_administrativos;
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }
            // al dar enter pasar al siguiente campo
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{TAB}");
            }
            //Si el texto esta sombreado todo caer encima al dato del textbox
            if (texbox.SelectionLength >= texbox.Text.Length)
            {
                texbox.Text = "";
            }
            //Para obligar a que sólo se introduzcan números 
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan 
                    e.Handled = true;
                }
            }
            //Permitir el punto decimal
            bool IsDec = false;
            int nroDec = 0;
            for (int i = 0; i < texbox.Text.Length; i++)
            {
                if (texbox.Text[i] == '.')
                    IsDec = true;

                if (IsDec && nroDec++ >= 2)
                {
                    e.Handled = true;
                    return;
                }
            }
            if (e.KeyChar >= 48 && e.KeyChar <= 57)
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = (IsDec) ? true : false;
            else
                e.Handled = true;
        }

        private void txtGastos_administrativos_Leave(object sender, EventArgs e)
        {
            TextBox texbox = txtGastos_administrativos;
            double valor_ingresado = 0;
            if (!string.IsNullOrEmpty(texbox.Text))
                valor_ingresado = Convert.ToDouble(texbox.Text);

            texbox.Text = string.Format(String.Format("{0:#,###,###,##0.00}", valor_ingresado));
            calcular_total();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            /*------------------*/
            analisis_cuantitativo analisis = new analisis_cuantitativo();
            analisis.no_solicitud = Int32.Parse(txtNo_solicitud.Text);
            analisis.coopsalud = decimal.Parse(txtCoopsalud.Text);
            analisis.aportaciones = decimal.Parse(txtCuota_aportacion.Text);
            analisis.mejora_avalua = decimal.Parse(txtMejoras_avaluo.Text);
            analisis.cuota_anticipada = decimal.Parse(txtCuota_anticipada.Text);
            analisis.prestamos_consolidar_csf = decimal.Parse(txtConsolidacion_coopsafa.Text);
            analisis.prestamos_consolidar_otros = decimal.Parse(txtConsolidacion_otros.Text);
            analisis.pagos_terceros = decimal.Parse(txtPago_terceros.Text);
            analisis.complemento_aportaciones = decimal.Parse(txtComplemento_aportaciones.Text);
            analisis.timbres_cooperativos = decimal.Parse(txtTimbres_cooperativos.Text);
            analisis.honorarios_hipoteca = decimal.Parse(txtHonorarios_hipoteca.Text);
            analisis.honorarios_compraventa = decimal.Parse(txtHonorarios_compra_venta.Text);
            analisis.capitalizacion = decimal.Parse(txtCapitalizacion.Text);
            analisis.seguro_vida = decimal.Parse(txtSeguro_vida.Text);
            analisis.seguro_danos = decimal.Parse(txtSeguro_danos.Text);
            analisis.seguro_incendios = decimal.Parse(txtSeguro_incendio.Text);
            analisis.seguro_vida_mensual = decimal.Parse(txtSeguro_vida_mens.Text);
            analisis.seguro_danos_mensual = decimal.Parse(txtSeguro_danos_mens.Text);
            analisis.seguro_incendios_mensual = decimal.Parse(txtSeguro_incendios_mens.Text);
            analisis.gastos_administrativos = decimal.Parse(txtGastos_administrativos.Text);
            analisis.avaluo_final = decimal.Parse(txtAvaluo_final.Text);
            analisis.total_deducciones = decimal.Parse(txtTotal_deducciones.Text);
            analisis.total_desembolso = decimal.Parse(txtNeto_recibir.Text);
            analisis.cuota_nivelada = decimal.Parse(txtCuota_nivelada.Text);
            analisis.cuota_total = decimal.Parse(txtCuota_total.Text);
            analisis.central_riesgos = decimal.Parse(txtCentral_riegos.Text);
            analisis.papeleria = decimal.Parse(txtPapeleria.Text);

            string xml = p_construir_xml_DeduccionesMCR((DataTable)gvGastos_desembolso.DataSource);
            txtXmlDeduccionesMCR.Text = xml;

            analisis.xmlDeducciones_MCR = txtXmlDeduccionesMCR.Text;
            bool vl_ok = false;
            try
            {
                da.p_AlmacenarAnalisisCuantitativo(analisis);
                actualizarGastosMCR();
                guardado = true;
                vl_ok = true;
            }
            catch (Exception ex)
            {
                guardado = false;
            }

            calcular_total();
            if (vl_ok)
                Notificacion.show_Toast(1500, "Analisis actualizada satisfactoriamente !");
            else
                MessageBox.Show("No ha sido posible guardar el Analisis Cuantitativo..!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void actualizarGastosMCR()
        {
            Int32 no_sol = Int32.Parse(txtNo_solicitud.Text);
            DataTable table = (DataTable)gvGastos_desembolso.DataSource;
            foreach (DataRow row in table.Rows)
            {
                string scod_cobro = row[0].ToString();
                decimal dmonto_cobro = decimal.Parse(row[2].ToString());
                da.ActualizarMontoGastosMCRxCodCobro(no_sol, scod_cobro, dmonto_cobro);
                //MessageBox.Show(row[0].ToString() + " " + row[1].ToString());                        
            }
        }

        private void labelProducto_DoubleClick(object sender, EventArgs e)
        {
            btnCarlcularLiq_Click(null, null);
        }

        private void label35_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtXmlDeduccionesMCR.Visible = true;
        }

        private void txtPlazo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Para obligar a que sólo se introduzcan números 
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan 
                    e.Handled = true;
                }
            }
        }

        private void txtPlazo_Leave(object sender, EventArgs e)
        {
            TextBox texbox = txtPlazo;
            double ntexbox = 0;
            if (!string.IsNullOrEmpty(texbox.Text))
                ntexbox = Convert.ToDouble(texbox.Text);
            texbox.Text = string.Format(String.Format("{0:###0}", ntexbox));
            txtCuota_nivelada.Text = da.ObtenerCuotaNivela(Int16.Parse(txtCodigo_sub_aplicacion.Text), 
                                                             float.Parse(txtMonto_solicitado.Text), 
                                                             Decimal.Parse(txtTasa.Text), 
                                                             float.Parse(txtPlazo.Text)).ToString("#,###,###,##0.00");
        }

        private void label33_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (txtGuidID_generado.Visible)
            txtGuidID_generado.Visible = false;
            else
                txtGuidID_generado.Visible = true;
        }

        private void txtGuidID_generado_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            s_analisis_cuantitativo02 forma = new s_analisis_cuantitativo02();
            forma.da = this.da;
            forma.g_guid = txtGuidID_generado.Text;
            forma.ShowDialog();
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            txtGuidID_generado_MouseDoubleClick(null, null);
        }

        private void gvGastos_desembolso_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (gvGastos_desembolso.RowCount > 0)
            {
                DataGridViewRow rowgrid = gvGastos_desembolso.CurrentRow;
                string vl_cod_cobro = rowgrid.Cells["cod_tipocobro"].Value.ToString();
                string vl_desc_cobro = rowgrid.Cells["desc_tipocobro"].Value.ToString();
                string vl_monto = rowgrid.Cells["mon_cobro"].Value.ToString();

                DataTable dtGastos = (DataTable)gvGastos_desembolso.DataSource;

                DataRow[] Rowsdt = dtGastos.Select("cod_tipocobro='" + vl_cod_cobro+"'");

                s_analisis_cuantitativo03 forma = new s_analisis_cuantitativo03();
                forma.txtCod_cobro.Text = vl_cod_cobro;
                forma.txtDesc_cobro.Text = vl_desc_cobro;
                forma.txtMonto_cobro.Text = vl_monto;
                DialogResult nresult = forma.ShowDialog();
                if (nresult == DialogResult.OK)
                {
                    vl_monto = forma.txtMonto_cobro.Text;
                    Rowsdt[0]["mon_cobro"] = vl_monto;
                    calcular_total();
                }
            }
        }

        private void label26_Click(object sender, EventArgs e)
        {
            calcular_total();
        }

        public double ObtieneGastosOperativo(int subApp, double monto, double monto_aprobado)
        {
            double gastos = 0.0;
            switch (subApp){
                case 2:
                    gastos = monto * 0.02;
                    break;
                case 3:
                    gastos = 2000.00;
                    break;
                case 14:
                    gastos = 2000.00;
                    break;
                case 18:
                    gastos = monto * 0.02;
                    break;
                case 19:
                    gastos = monto * 0.02;
                    break;
                case 29:
                    gastos = monto * 0.02;
                    break;
                case 32:
                    gastos = monto * 0.015;
                    break;
                case 33:
                    gastos = monto * 0.01;
                    if (gastos > 10000.00) {
                        gastos = 10000.00;
                    }
                    break;
                case 40:
                    gastos = monto * 0.02;
                    break;
                case 44:
                    gastos = 2000.00;
                    break;
                case 45:
                    gastos = monto * 0.01;
                    if (gastos > 10000.00)
                    {
                        gastos = 10000.00;
                    }
                    break;
                case 46:
                    gastos = monto_aprobado * 0.005;
                    break;
                case 48:
                    gastos = 2000.00;
                    break;
                case 49:
                    gastos = 2000.00;
                    break;
                case 50:
                    gastos = monto_aprobado * 0.005;
                    break;
                case 51:
                    gastos = monto_aprobado * 0.02;
                    break;
                case 55:
                    gastos = monto_aprobado * 0.02;
                    break;
                case 56:
                    gastos = monto * 0.02;
                    break;
                case 58:
                    gastos = monto * 0.02;
                    break;
                case 59:
                    gastos = monto * 0.02;
                    break;
                case 63:
                    gastos = 2000.00;
                    break;
                case 64:
                    gastos = monto_aprobado * 0.005;
                    break;
                case 67:
                    gastos = 2000.00;
                    break;
                case 68:
                    gastos = monto_aprobado * 0.02;
                    break;
                case 69:
                    gastos = monto_aprobado * 0.02;
                    break;
                case 70:
                    gastos = monto_aprobado * 0.02;
                    break;
            }
            return gastos;
        }

    }
}

