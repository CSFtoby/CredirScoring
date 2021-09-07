using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using wfcModel;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Globalization;
using NotificacionesDll;
using wfcModel.MGI.SubAplicaciones;
using Docsis_Application.FormsInformativo;
using System.Text.RegularExpressions;
using Docsis_Application.Excepciones;
using Docsis_Application.FrmsRpts;

namespace Docsis_Application
{
	public partial class s_PreCalificado : Form
	{
		public MDI_Menu formapadre;
		public DataAccess da;
		DataTable dtInfoFinan = new DataTable();
		DataTable dtCuotasBuro = new DataTable();
		delegate void Functionz();
		Thread myThread;
		bool bmaximizado = false;
		bool vl_mostrar_miniinfo = true;
		s_PreCalificado_info01 mini_info_resul = new s_PreCalificado_info01();
		public string gmodo_coopsafa = "INS";
		string gmodo_transunion = "CREAR";
		string usuario_wstransunion = MDI_Menu.usuario_wstransunion;
		string pass_wstransunion = MDI_Menu.pass_wstransunion;

		string gcodigo_agencia = DocSys.vl_agencia_usuario.ToString();
		string goficial_servicio = DocSys.vl_user;
		string goficial_serviciodesolicitud = "";

		double monto_destino1 = 0;
		double monto_destino2 = 0;

		public bool prestamo_vehiculos = false;
		public Int16 gestado_id_solicitud = 0;

		string gmodalidad = "INS";
		string gxml_respuesta = "";
		string gxml_crear_solicitud = "";
		string gxml_cuotas_buro = "";
		int gcant_cuotas_buro = 0;
		string gxml_referencias = "";
		string gxml_outputxml = "";
		string gdestino_credito_id = "";
		string gfuente_financiamiento = "";

		float saldo_aportaciones_principal = 0;
		string estado_burointerno_principal = "";
		string estado_desc_burointerno_principal = "";
		List<string> observa_burointerno_principal;

		float saldo_aportaciones_conyuge = 0;
		string estado_burointerno_conyuge = "0";
		string estado_desc_burointerno_conyuge = "";
		List<string> observa_burointerno_conyuge;

		float saldo_aportaciones_codeudor = 0;
		string estado_burointerno_codeudor = "0";
		string estado_desc_burointerno_codeudor = "";
		List<string> observa_burointerno_codeudor;

		float saldo_aportaciones_aval1 = 0;
		string estado_burointerno_aval1 = "0";
		string estado_desc_burointerno_aval1 = "";
		List<string> observa_burointerno_aval1;

		float saldo_aportaciones_aval2 = 0;
		string estado_burointerno_aval2 = "0";
		string estado_desc_burointerno_aval2 = "";
		List<string> observa_burointerno_aval2;
		string AdvertenciaGeneral = string.Empty;
		int CodigoExcepcion = 0;

		ResultadoBuroValores ResultadoPrestatario = ResultadoBuroValores.ERROR;
		ResultadoBuroValores ResultadoAval1 = ResultadoBuroValores.ERROR;
		ResultadoBuroValores ResultadoAval2 = ResultadoBuroValores.ERROR;

		bool retornar_ok = false;

		const int WM_SYSCOMMAND = 0x112;
		const int MOUSE_MOVE = 0xF012;
		const int SC_MINIMIZE = 0xF020;
		const int CS_DROPSHADOW = 0x00020000;
		const int WS_THICKFRAME = 0x00040000;
		const int WS_SIZEBOX = WS_THICKFRAME;
		const int cGrip = 10;      // Grip size
		const int cCaption = 1;   // Caption bar height;

		//FELVIR01-20190607
		private string MsjActualizarCampos = string.Empty;
		private bool conyugeAct = true;

		#region

		//
		// Declaraciones del API
		[System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
		private extern static void ReleaseCapture();
		//
		[System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
		private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
		//
		#endregion
		#region

		protected override CreateParams CreateParams
		{
			get
			{
				// add the drop shadow flag for automatically drawing
				// a drop shadow around the form
				CreateParams cp = base.CreateParams;
				//cp.ClassStyle |= CS_DROPSHADOW ;
				//cp.Style |= 0x20000 | 0x80000 | 0x00020000 | 0x40000 | 0x00040000; //WS_MINIMIZEBOX  | WS_SIZEBOX | WS
				cp.Style |= 0x40000;
				return cp;
			}
		}
		#endregion
		protected override void OnPaint(PaintEventArgs e)
		{
			Rectangle rc = new Rectangle(this.ClientSize.Width - cGrip, this.ClientSize.Height - cGrip, cGrip, cGrip);
			ControlPaint.DrawSizeGrip(e.Graphics, this.BackColor, rc);
			rc = new Rectangle(0, 0, this.ClientSize.Width, cCaption);
			e.Graphics.FillRectangle(Brushes.DarkBlue, rc);
		}
		protected override void WndProc(ref Message m)
		{

			if (m.Msg == 0x84)
			{  // Trap WM_NCHITTEST
				Point pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);
				pos = this.PointToClient(pos);
				if (pos.Y < cCaption)
				{
					m.Result = (IntPtr)2;  // HTCAPTION
					return;
				}
				if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
				{
					m.Result = (IntPtr)17; // HTBOTTOMRIGHT
					return;
				}
			}

			base.WndProc(ref m);
		}

		List<referencias_solicitud> LstReferencias_solicitante = new List<referencias_solicitud>();
		List<referencias_solicitud> LstReferencias_codeudor = new List<referencias_solicitud>();
		List<referencias_solicitud> LstReferencias_aval1 = new List<referencias_solicitud>();
		List<referencias_solicitud> LstReferencias_aval2 = new List<referencias_solicitud>();

		public s_PreCalificado(DataAccess da)
		{
			InitializeComponent();
			CheckForIllegalCrossThreadCalls = false;
			this.DoubleBuffered = true;
			this.SetStyle(ControlStyles.ResizeRedraw, true);

			dtInfoFinan.Columns.Add("numero_identificacion");
			dtInfoFinan.Columns.Add("codigo_cliente");
			dtInfoFinan.Columns.Add("nombre");
			dtInfoFinan.Columns.Add("rol");
			dtInfoFinan.Columns.Add("Aportaciones");
			dtInfoFinan.Columns.Add("ingresos");
			dtInfoFinan.Columns.Add("otros_ingresos");
			dtInfoFinan.Columns.Add("deducciones");
			dtInfoFinan.Columns.Add("ingresos_netos");
			dtInfoFinan.Columns.Add("estado_buro_interno");
			dtInfoFinan.Columns.Add("observaciones_buro");
			gvInfoFinanciera.DataSource = dtInfoFinan;

			dtCuotasBuro.Columns.Add("seleccion");
			dtCuotasBuro.Columns.Add("institucion");
			dtCuotasBuro.Columns.Add("cuota", typeof(float));
			dtCuotasBuro.Columns.Add("saldo", typeof(float));
			this.da = da;


		}

		//FELVIR01
		private void s_PreCalificado_Load(object sender, EventArgs e)
		{
			p_llenar_combo_sub_aplicaciones();
			p_llenar_combo_destinos();
			p_llenar_fuente_fondos();

			ocultar_tabs();
			btnDatosAfiliado_Click(null, null);
			txtModo_transunion.Text = "CREAR";
			int estacion_id = 0;

			if (gmodo_coopsafa == "UPD")
			{
				pbOficialServ.Image = Properties.Resources.icon_usuario;
				//Obteniendo el estado de la solicitud
				var dtEstadoSolicitud = da.ObtenerSituacionActualxSolicitud(Int32.Parse(txtNo_solicitud_coopsafa.Text));
				gestado_id_solicitud = Int16.Parse(dtEstadoSolicitud.Rows[0]["estado_id"].ToString());
				estacion_id = Int16.Parse(dtEstadoSolicitud.Rows[0]["estacion_id"].ToString());

				txtIDSolicitante.Enabled = false;
				txtCodigo_cliente.Enabled = false;
				p_obtener_solicitudxNo(Int32.Parse(txtNo_solicitud_coopsafa.Text));
				LabelNo_solic.Text = txtNo_solicitud_coopsafa.Text;
				comboBox_sub_aplicacion.Visible = false;

				int codigo_agencia = Int16.Parse(dtEstadoSolicitud.Rows[0]["codigo_agencia_origen"].ToString());
				string oficial_servicio = dtEstadoSolicitud.Rows[0]["oficial_servicio"].ToString();
				labelFilial.Text = da.ObtenerNombreAgencia(codigo_agencia);
				goficial_serviciodesolicitud = oficial_servicio.Trim();
				labelOficialDeServicio.Text = oficial_servicio.Trim() + " | " + da.ObtenerNombreUsuario(oficial_servicio);

				LabelNo_solic.Text = txtNo_solicitud_coopsafa.Text;
				this.btnActualizarInfo.Visible = true;

				try
				{
					string scodigo_cliente_usuario = da.ObtenerCodigoClientexUsuario(oficial_servicio);
					byte[] tmpfotoUsuario = da.ObtenerFotoUsuario(scodigo_cliente_usuario);
					pbOficialServ.Image = DocSys.p_CopyDataToBitmap(tmpfotoUsuario);
					if (pbOficialServ.Image == null)
					{
						pbOficialServ.Image = Properties.Resources.icon_usuario;
					}
				}
				catch
				{
					pbOficialServ.Image = Properties.Resources.icon_usuario;
				}
				this.CodigoExcepcion = this.da.GetExcepcionSolicitud(Int32.Parse(txtNo_solicitud_coopsafa.Text));
			}
			//Modo INS
			else
			{
				pbOficialServ.Image = Properties.Resources.icon_usuario;
				labelFilial.Text = MDI_Menu.nombre_agencia_usuario.Trim();
				labelOficialDeServicio.Text = MDI_Menu.nombre_y_usuario_oficial_servicio;
				labelOficialDeServicio.Text = goficial_servicio + " | " + da.ObtenerNombreUsuario(goficial_servicio);
				goficial_serviciodesolicitud = goficial_servicio;
				this.btnActualizarInfo.Visible = false;

				LabelNo_solic.Text = "";

				try
				{
					string scodigo_cliente_usuario = da.ObtenerCodigoClientexUsuario(goficial_servicio);
					byte[] tmpfotoUsuario = da.ObtenerFotoUsuario(scodigo_cliente_usuario);
					pbOficialServ.Image = DocSys.p_CopyDataToBitmap(tmpfotoUsuario);
					if (pbOficialServ.Image == null)
					{
						pbOficialServ.Image = Properties.Resources.icon_usuario;
					}

				}
				catch
				{
					pbOficialServ.Image = Properties.Resources.icon_usuario;
				}

				p_deshabilitar_figuras_solicitud();
				comboBox_sub_aplicacion.Visible = true;
			}

			if (gmodo_coopsafa == "CONS")
			{
				this.btnActualizarInfo.Visible = false;
				var dtEstadoSolicitud = da.ObtenerSituacionActualxSolicitud(Int32.Parse(txtNo_solicitud_coopsafa.Text));
				gestado_id_solicitud = Int16.Parse(dtEstadoSolicitud.Rows[0]["estado_id"].ToString());
				estacion_id = Int16.Parse(dtEstadoSolicitud.Rows[0]["estacion_id"].ToString());

				int codigo_agencia = Int16.Parse(dtEstadoSolicitud.Rows[0]["codigo_agencia_origen"].ToString());
				string oficial_servicio = dtEstadoSolicitud.Rows[0]["oficial_servicio"].ToString();
				labelFilial.Text = da.ObtenerNombreAgencia(codigo_agencia);
				goficial_serviciodesolicitud = oficial_servicio.Trim();
				labelOficialDeServicio.Text = oficial_servicio.Trim() + " | " + da.ObtenerNombreUsuario(oficial_servicio);
				LabelNo_solic.Text = txtNo_solicitud_coopsafa.Text;

				txtIDSolicitante.Enabled = false;
				txtCodigo_cliente.Enabled = false;
				p_obtener_solicitudxNo(Int32.Parse(txtNo_solicitud_coopsafa.Text));
				comboBox_sub_aplicacion.Visible = false;
				p_modo_consulta();

				pbOficialServ.Image = Properties.Resources.icon_usuario;
				try
				{
					string scodigo_cliente_usuario = da.ObtenerCodigoClientexUsuario(oficial_servicio);
					byte[] tmpfotoUsuario = da.ObtenerFotoUsuario(scodigo_cliente_usuario);
					pbOficialServ.Image = DocSys.p_CopyDataToBitmap(tmpfotoUsuario);
					if (pbOficialServ.Image == null)
					{
						pbOficialServ.Image = Properties.Resources.icon_usuario;
					}

				}
				catch
				{
					pbOficialServ.Image = Properties.Resources.icon_usuario;
				}

				this.CodigoExcepcion = this.da.GetExcepcionSolicitud(Int32.Parse(txtNo_solicitud_coopsafa.Text));
			}

			/*
			 * Valida si la solicitud está en proceso y en una estación distinta a Afiliación
			 */

			//Si está en proceso y no es Afiliación
			if ((this.gestado_id_solicitud > 2) & DocSys.EstacionGlobal != 1000)
			{
				switch (DocSys.EstacionGlobal)
				{
					case 1002: //Créditos TGU
						this.btnActualizarInfo.Visible = true;
						this.txtDescripcion_garantia.Enabled = true;
						break;
					case 2001: //Créditos SPS
						this.btnActualizarInfo.Visible = true;
						this.txtDescripcion_garantia.Enabled = true;
						break;
					case 2003: //Créditos LCBA
						this.btnActualizarInfo.Visible = true;
						this.txtDescripcion_garantia.Enabled = true;
						break;
					case 1001: //Nivel Resolutivo Filial
						this.btnActualizarInfo.Visible = true;
						break;
					default:
						this.btnActualizarInfo.Visible = false;
						break;
				}
			}

			bool global_es_admon_sistema = da.EsAdministradorSistema(DocSys.vl_user);
			if (global_es_admon_sistema)
			{
				this.btnActualizarInfo.Visible = true;
			}

			if (this.CodigoExcepcion != 0)
			{
				this.btnExcepcion.Visible = true;
			}
			else
			{
				this.btnExcepcion.Visible = false;
			}

			/*
			 * Si la estación es distinta de afiliación
			 */
			if (estacion_id != 1000)
			{
				this.label15.Text = "Usuario Estación:";
			}

			labelUsuario.Text = DocSys.vl_user;
			LabelTnsNames.Text = DocSys.vl_tnsnames;

			p_calcular_indice_concentracion_deuda();

			MD_Coop ws_transunion = new MD_Coop();
			txtUrlTransUnion.Text = ws_transunion.Url;

		}

		private void moverForm()
		{
			ReleaseCapture();
			SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
		}

		private void p_obtener_detalle_cuotas_en_buro_solicitante()
		{
			DataTable vl_retorno = new DataTable();
			this.Invoke(new Functionz(delegate ()
			{
				panelWait_cuotasconsolo.Visible = true;
				txtTotal_cuotas_consolidar.Text = "0.00";
				txtTotal_capital_consolidar.Text = "0.00";
			}));
			string xml_request = @"<Request>
                                      <Authentication>
                                         <UserId>" + usuario_wstransunion + @"</UserId>
                                         <Password>" + pass_wstransunion + @"</Password>
                                      </Authentication>" + @"
                                      <Cedula>" + txtIDSolicitante.Text + @"</Cedula>
                                      <Prestamo>" + txtVentanilla_planilla.Text + @"</Prestamo>
                                      <UsuarioWorkflow>" + labelUsuario.Text + @"</UsuarioWorkflow>
                                      <Sucursal>" + labelFilial.Text + @"</Sucursal>
                                   </Request>";
			try
			{
				MD_Coop ws_transunion = new MD_Coop();
				var resultado_consulta = ws_transunion.ObtenerCuotas(xml_request);
				if (resultado_consulta != "<tabla></tabla>")
				{
					gxml_cuotas_buro = resultado_consulta;
					DataTable dt = xml_to_datatable(resultado_consulta);
					dtCuotasBuro.Rows.Clear();

					Int32 vl_idBitacora = 0;
					Int32 vl_no_solicitud = 0;
					Int32 vl_appid = 0;
					try
					{
						vl_idBitacora = Int32.Parse(dt.DataSet.Tables[0].Rows[0][0].ToString());
						vl_no_solicitud = Int32.Parse(txtNo_solicitud_coopsafa.Text);
						vl_appid = Int32.Parse(txtApplicationID.Text);
					}
					catch
					{

					}
					//3 - Obtencion de Cuotas en el log
					da.p_generar_bitacorabusqueda_tu(vl_idBitacora, vl_no_solicitud, vl_appid, 3);

					if (dt.DataSet.Tables.Count > 1)
					{
						for (int x = 0; x < dt.DataSet.Tables[1].Rows.Count; x++)
						{
							dtCuotasBuro.Rows.Add(false,
												  dt.DataSet.Tables[1].Rows[x][0].ToString(),
												  dt.DataSet.Tables[1].Rows[x][2].ToString(),
												  dt.DataSet.Tables[1].Rows[x][1].ToString());
						}
						vl_retorno = dtCuotasBuro;
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			this.Invoke(new Functionz(delegate ()
			{
				panelWait_cuotasconsolo.Visible = false;

			}));
			gvCuotasBuro.DataSource = vl_retorno;
			if (vl_retorno.Rows.Count <= 0)
			{
				MessageBox.Show("No se encontró detalle de cuotas en el buro..!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}

		}
		private Double p_obtener_suma_cuotas_en_buroxID(string p_id, string p_modalidad)
		{
			Double suma_cuotas = 0;
			DataTable vl_retorno = new DataTable();
			this.Invoke(new Functionz(delegate ()
			{
				panelWait_cuotasconsolo.Visible = true;
				txtTotal_cuotas_consolidar.Text = "";
				txtTotal_capital_consolidar.Text = "";
			}));
			string xml_request = @"<Request>
                                     <Authentication>
                                         <UserId>sagrada_familia_test</UserId>
                                         <Password>Test123!!!</Password>
                                     </Authentication>
                                     <Cedula>" + p_id + @"</Cedula>
                                     <Prestamo>" + p_modalidad + @"</Prestamo>
                                  </Request>";


			//NetworkCredential nc = new NetworkCredential("asps", "aguas", "");
			//WebProxy pr = new WebProxy("http://193.1.1.254:3128/");
			//pr.Credentials = nc;
			try
			{
				MD_Coop ws_transunion = new MD_Coop();
				var resultado_consulta = ws_transunion.ObtenerCuotas(xml_request);

				if (resultado_consulta != "<tabla></tabla>")
				{
					DataTable dt = xml_to_datatable(resultado_consulta);
					double tempo = 0;
					foreach (DataRow row in dt.Rows)
					{
						string tempo1 = row["monto"].ToString();
						if (!string.IsNullOrEmpty(tempo1))
						{
							tempo = tempo + Convert.ToDouble(tempo1);
						}
					}
					suma_cuotas = tempo;
					vl_retorno = dt;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			this.Invoke(new Functionz(delegate ()
			{
				panelWait_cuotasconsolo.Visible = false;

			}));
			return suma_cuotas;

		}
		public DataTable xml_to_datatable(string xmlData)
		{
			StringReader theReader = new StringReader(xmlData);
			DataSet theDataSet = new DataSet();
			theDataSet.ReadXml(theReader);

			return theDataSet.Tables[0];
		}
		private void calcular_cuotas_consolo(int indice)
		{

			double total_cuotas = 0;
			double total_capital = 0;
			for (int i = 0; i < gvCuotasBuro.Rows.Count; i++)
			{
				//string seleccionada="false";
				//if (!string.IsNullOrEmpty(gvCuotasBuro.Rows[i].Cells[0].Value.ToString()))
				//{
				//    seleccionada = "true";
				//}

				string seleccionada = gvCuotasBuro.Rows[i].Cells[0].Value.ToString();
				if (seleccionada == "true")
				{

					var cuotas = gvCuotasBuro.Rows[i].Cells[2].Value.ToString();
					var saldos = gvCuotasBuro.Rows[i].Cells[3].Value.ToString();
					total_cuotas = total_cuotas + double.Parse(cuotas);
					total_capital = total_capital + double.Parse(saldos);
				}
			}
			txtTotal_cuotas_consolidar.Text = total_cuotas.ToString("###,###,##0.00");
			txtTotal_capital_consolidar.Text = total_capital.ToString("###,###,###,##0.00");
		}
		private void p_invocar_precalificado()
		{
			string modo = txtModo_transunion.Text;
			string xml_ = "";
			if (modo == "CREAR")
			{
				xml_ = p_construir_xml("CREARSOLICITUD");
			}

			if (modo == "RECALCULAR")
			{
				xml_ = p_construir_xml("RECALCULARSOLICITUD");
			}

			double dingresos_brutos = 0;
			double.TryParse(txtIngresos.Text, out dingresos_brutos);
			if (dingresos_brutos == 0)
			{
				MessageBox.Show("El ingreso bruto no puede ser cero (0), si usted ingreso un valor diferente de cero en esta casilla puede deberse a un problema en la configuracion regional de su equipo, la configuracion esperada debe ser es-HN, consulte con soporte tecnico ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			double dmonto_solicitado = 0;
			double.TryParse(txtMonto_solicitado.Text, out dmonto_solicitado);
			if (dmonto_solicitado == 0)
			{
				MessageBox.Show("El monto solicitado no puede ser cero (0), si usted ingreso un valor diferente de cero en esta casilla puede deberse a un problema en la configuracion regional de su equipo, la configuracion esperada debe ser es-HN, consulte con soporte tecnico ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			this.Invoke(new Functionz(delegate ()
			{
				panelPrecali_solic.Visible = true;
				btnInvocar_precalificado.Enabled = false;
				btnClose.Enabled = false;
			}));

			try
			{
				MD_Coop ws_transunion = new MD_Coop();
				XmlDocument xmlDoc = new XmlDocument();
				string resultado_consulta = "";
				if (modo == "CREAR")
				{
					try
					{
						ws_transunion.Timeout = 110000;
						resultado_consulta = ws_transunion.CrearSolicitud(xml_);
					}
					catch (Exception ex)
					{
						btnInvocar_precalificado.Enabled = true;
						btnClose.Enabled = true;
						da.RegistrarEventoErrorPrecali(Int32.Parse(txtNo_solicitud_coopsafa.Text), "CREAR", ex.Message, xml_);
						MessageBox.Show("El servicio de preclasificado(creacion) en este momento no esta  disponible, una posible causa puede ser el siguiente error -> " + ex.Message);
					}
				}

				if (modo == "RECALCULAR")
				{
					try
					{
						resultado_consulta = ws_transunion.RecalcularSolicitud(xml_);
						da.Actualizar_cantidad_recalculos(Int32.Parse(txtNo_solicitud_coopsafa.Text));
					}
					catch (Exception ex)
					{
						btnInvocar_precalificado.Enabled = true;
						btnClose.Enabled = true;
						da.RegistrarEventoErrorPrecali(Int32.Parse(txtNo_solicitud_coopsafa.Text), "RECALCULAR", ex.Message, xml_);
						MessageBox.Show("El servicio de preclasificado(recalculo) en este momento no esta  disponible, una posible causa puede ser el siguiente error -> " + ex.Message);
					}
				}

				if (!string.IsNullOrEmpty(resultado_consulta))
				{
					if (EsXmlConFormatoOk(resultado_consulta))
					{
						xmlDoc.LoadXml(resultado_consulta);
						string vl_Status = xmlDoc.SelectSingleNode("DCResponse/Status").InnerText;
						if (vl_Status.ToString() == "Success")
						{
							string modo_transunion_original = gmodo_transunion;

							string vl_ApplicationId = xmlDoc.SelectSingleNode("DCResponse/ResponseInfo/ApplicationId").InnerText;
							if (gmodo_transunion == "CREAR")
							{
								#region la primera vez que esta en modo crear y pasa a recalcular es la fecha de creacion de la solicitud en transunion.
								#endregion
								da.p_actualizarFecha_creacion_TU(Int32.Parse(txtNo_solicitud_coopsafa.Text));
								txtFecha_creacion_tu.Text = da.ObtenerFecha_creacion_tu(Int32.Parse(txtNo_solicitud_coopsafa.Text));
							}
							gmodo_transunion = "RECALCULAR";
							txtModo_transunion.Text = gmodo_transunion;
							txtApplicationID.Text = vl_ApplicationId;
							btnGuardar_solicitud_Click(null, null);
							da.guardarXMLRespuesta(Int32.Parse(txtNo_solicitud_coopsafa.Text), resultado_consulta, xml_);

							procesar_xml_respuesta(resultado_consulta, "AFTER_OF_PRECALIFICAR", modo_transunion_original);
							//Obtiene el dato del buró
							this.ResultadoPrestatario = u_Globales.resultadoBuroExcepcion(resultado_consulta, 3);
							if (this.cbAval1.Checked)
								this.ResultadoAval1 = u_Globales.resultadoBuroExcepcion(resultado_consulta, 1);
							if (this.cbAval2.Checked)
								this.ResultadoAval2 = u_Globales.resultadoBuroExcepcion(resultado_consulta, 2);

							//Evitar que modifiquen los datos del tipo de producto y solicitante principal una vez creada la solicitud en TransUnion
							this.Invoke(new Functionz(delegate ()
							{
								comboBox_sub_aplicacion.Visible = false;
								txtDesc_sub_aplicacion.Visible = true;

							}));
							txtDesc_sub_aplicacion.Text = comboBox_sub_aplicacion.Text;
							txtCodigo_cliente.Enabled = false;
							txtIDSolicitante.Enabled = false;
						}
					}
					else
					{
						MessageBox.Show("No se ha recibido un xml con formato correcto, la respuesta de TransUnion es : " + resultado_consulta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

			this.Invoke(new Functionz(delegate ()
			{
				panelWait_cuotasconsolo.Visible = false;

			}));

			this.Invoke(new Functionz(delegate ()
			{
				panelPrecali_solic.Visible = false;
				btnInvocar_precalificado.Enabled = true;
				btnClose.Enabled = true;
			}));

			p_actualizar_participantes_de_solicitud(Int32.Parse(txtNo_solicitud_coopsafa.Text));

			brnResultadoBuro_Click(null, null);
		}
		private void p_actualizar_participantes_de_solicitud(Int32 p_no_solicitud)
		{
			da.RegistrarIDs_x_Solicitud(p_no_solicitud);
		}

		private void txtCodigo_cliente_Leave(object sender, EventArgs e)
		{
			if (txtCodigo_cliente.Text != "")
			{
				p_get_datos_solicitantexCC(txtCodigo_cliente.Text);
			}
		}

		private void txtIDSolicitante_Leave(object sender, EventArgs e)
		{
			if (txtIDSolicitante.Text != "")
			{
				p_get_datos_solicitantexID(txtIDSolicitante.Text);
			}

		}

		//FELVIR01-20190618
		private void txtId_conyuge_Leave(object sender, EventArgs e)
		{
			if (txtId_conyuge.Text != "")
			{
				//p_get_datos_conyuge(txtId_conyuge.Text);
			}
		}

		//FELVIR01-20190618
		private void txtId_codeudor_Leave(object sender, EventArgs e)
		{
			if (txtId_codeudor.Text != "")
			{
				DataTable dt = da.ObtenerDatosClientexIdentificacion(txtId_codeudor.Text);
				if (dt.Rows.Count > 0)
				{
					bool cliente = true;
					bool dependientes = true;
					bool nucleo = true;
					bool referencias = true;
					bool direcciones = true;
					bool conyuge = true;
					string codigo_cliente = dt.Rows[0]["codigo_cliente"].ToString();

					string respuesta = this.RevisarUltimaActualizacion(int.Parse(codigo_cliente), out cliente, out dependientes, out nucleo, out referencias,
													out direcciones, out conyuge, false);

					if (!dt.Rows[0]["estado_civil"].ToString().Equals("C") || !dt.Rows[0]["estado_civil"].ToString().Equals("U"))
					{
						nucleo = true;
					}

					if (!cliente || !dependientes || !nucleo || !referencias || !direcciones)
					{
						string mensaje = "Para crear la solicitud debe actualizar campos del Codeudor: " + respuesta;
						DialogResult x = MessageBox.Show(mensaje, "Actualizar ficha de afiliación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						this.skipMethod();
					}
					else
					{
						p_get_datos_codeudor(txtId_codeudor.Text);
					}
				}
				else
				{
					MessageBox.Show("El codeudor aún no está afiliado.", "Ir a la Solicitud de Afiliación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					this.skipMethod();
				}
			}

		}

		//FELVIR01-20190618
		private void txtId_aval1_Leave(object sender, EventArgs e)
		{
			if (txtId_aval1.Text != "")
			{
				DataTable dt = da.ObtenerDatosClientexIdentificacion(txtId_aval1.Text);
				if (dt.Rows.Count > 0)
				{
					bool cliente = true;
					bool dependientes = true;
					bool nucleo = true;
					bool referencias = true;
					bool direcciones = true;
					bool conyuge = true;
					string codigo_cliente = dt.Rows[0]["codigo_cliente"].ToString();

					string respuesta = this.RevisarUltimaActualizacion(int.Parse(codigo_cliente), out cliente, out dependientes, out nucleo, out referencias,
													out direcciones, out conyuge, false);

					if (!dt.Rows[0]["estado_civil"].ToString().Equals("C") || !dt.Rows[0]["estado_civil"].ToString().Equals("U"))
					{
						nucleo = true;
					}

					if (!cliente || !dependientes || !nucleo || !referencias || !direcciones)
					{
						string mensaje = "Para crear la solicitud debe actualizar campos del Aval1: " + respuesta;
						DialogResult x = MessageBox.Show(mensaje, "Actualizar ficha de afiliación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						this.skipMethod();
					}
					else
					{
						p_get_datos_aval1(txtId_aval1.Text);
					}
				}
				else
				{
					MessageBox.Show("El Aval1 aún no está afiliado.", "Ir a la Solicitud de Afiliación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					this.skipMethod();
				}
			}
		}

		//FELVIR01-20190618
		private void txtId_aval2_Leave(object sender, EventArgs e)
		{
			if (txtId_aval2.Text != "")
			{
				DataTable dt = da.ObtenerDatosClientexIdentificacion(txtId_aval2.Text);
				if (dt.Rows.Count > 0)
				{
					bool cliente = true;
					bool dependientes = true;
					bool nucleo = true;
					bool referencias = true;
					bool direcciones = true;
					bool conyuge = true;
					string codigo_cliente = dt.Rows[0]["codigo_cliente"].ToString();

					string respuesta = this.RevisarUltimaActualizacion(int.Parse(codigo_cliente), out cliente, out dependientes, out nucleo, out referencias,
													out direcciones, out conyuge, false);

					if (!dt.Rows[0]["estado_civil"].ToString().Equals("C") || !dt.Rows[0]["estado_civil"].ToString().Equals("U"))
					{
						nucleo = true;
					}

					if (!cliente || !dependientes || !nucleo || !referencias || !direcciones)
					{
						string mensaje = "Para crear la solicitud debe actualizar campos del Aval2: " + respuesta;
						DialogResult x = MessageBox.Show(mensaje, "Actualizar ficha de afiliación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						this.skipMethod();
					}
					else
					{
						p_get_datos_aval2(txtId_aval2.Text);
					}
				}
				else
				{
					MessageBox.Show("El Aval2 aún no está afiliado.", "Ir a la Solicitud de Afiliación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					this.skipMethod();
				}
			}

		}

		//FELVIR01-20190618
		private string RevisarUltimaActualizacion(int codigo_cliente, out bool cliente, out bool dependientes, out bool nucleo,
												out bool referencias, out bool direcciones, out bool conyuge, bool incluir_nucleo = true)
		{

			cliente = true;
			dependientes = true;
			nucleo = true;
			referencias = true;
			direcciones = true;
			conyuge = true;
			int totRef = 0;
			string mensaje = string.Empty;

			DataTable actualizaciones = this.da.VerificarActualizacion(codigo_cliente);
			if (actualizaciones.Rows.Count > 0)
			{
				var totCliente = Convert.ToInt32(actualizaciones.Rows[0]["tot_exist"].ToString());
				var totAct = Convert.ToInt32(actualizaciones.Rows[0]["tot_mod"].ToString());
				var cliHoy = Convert.ToInt32(actualizaciones.Rows[0]["creadaHoy"].ToString());

				if (totCliente == 1)
				{
					if (cliHoy == 0)
					{
						cliente = (totAct == totCliente);
					}
					else
					{
						cliente = true;
					}
				}
				else
				{
					cliente = false;
				}

				//Evalua los dependientes
				var totDep = Convert.ToInt32(actualizaciones.Rows[1]["tot_exist"].ToString());
				var actDep = Convert.ToInt32(actualizaciones.Rows[1]["tot_mod"].ToString());
				var depHoy = Convert.ToInt32(actualizaciones.Rows[1]["creadaHoy"].ToString());
				if (totDep > 0)
				{
					if (depHoy == 0)
					{
						dependientes = (totDep == actDep);
					}
					else
					{
						dependientes = true;
					}
				}
				else
				{
					dependientes = false;
				}

				//Evalua el núcleo familiar
				var totNuc = Convert.ToInt32(actualizaciones.Rows[2]["tot_exist"].ToString());
				var actNuc = Convert.ToInt32(actualizaciones.Rows[2]["tot_mod"].ToString());
				var nucHoy = Convert.ToInt32(actualizaciones.Rows[2]["creadaHoy"].ToString());
				if (totNuc > 0)
				{
					if (nucHoy == 0)
					{
						nucleo = (totNuc == actNuc);
					}
					else
					{
						nucleo = true;
					}
				}
				else
				{
					nucleo = false;
				}

				//Evalua referencias
				totRef = Convert.ToInt32(actualizaciones.Rows[3]["tot_exist"].ToString());
				var actRef = Convert.ToInt32(actualizaciones.Rows[3]["tot_mod"].ToString());
				var refHoy = Convert.ToInt32(actualizaciones.Rows[3]["creadaHoy"].ToString());
				if (totRef > 0)
				{
					if (refHoy == 0)
					{
						referencias = (totRef == actRef);
					}
					else
					{
						referencias = true;
					}

					if (totRef < 3)
					{
						referencias = false;
					}
				}
				else
				{
					referencias = false;
				}

				//Evalua las direcciones
				var totDir = Convert.ToInt32(actualizaciones.Rows[4]["tot_exist"].ToString());
				var actDir = Convert.ToInt32(actualizaciones.Rows[4]["tot_mod"].ToString());
				var dirHoy = Convert.ToInt32(actualizaciones.Rows[4]["creadaHoy"].ToString());
				if (totDir > 0)
				{
					if (dirHoy == 0)
					{
						direcciones = (totDir == actDir);
					}
					else
					{
						direcciones = true;
					}
				}
				else
				{
					direcciones = false;
				}

				//Guarda la data para el conyuge
				var totalExisteC = Convert.ToInt32(actualizaciones.Rows[5]["tot_exist"].ToString());
				var totalModC = Convert.ToInt32(actualizaciones.Rows[5]["tot_mod"].ToString());
				var creadaHoyC = Convert.ToInt32(actualizaciones.Rows[5]["creadaHoy"].ToString());

				if (totalExisteC > 0)
				{
					if (creadaHoyC == 0)
					{
						conyuge = (totalExisteC == totalModC);
					}
					else
					{
						conyuge = true;
					}
				}
				else
				{
					conyuge = false;
				}
			}

			/*
			 * Realiza el juego para poder tomar como todos actualizados con una de las opciones modificadas
			 */
			#region Juego de Actualizaciones

			if (dependientes | direcciones | (referencias & totRef >= 3) | cliente)
			{
				cliente = true;
				dependientes = true;
				direcciones = true;
				referencias = true;
			}

			/*if(nucleo | conyuge)
			{
				nucleo = true;
				conyuge = true;
			}*/

			#endregion

			if (!cliente)
			{
				mensaje += "Lugar donde labora e ingresos. ";
			}
			if (!dependientes)
			{
				mensaje += "Dependientes. ";
			}
			if (!nucleo && incluir_nucleo)
			{
				mensaje += "Información del núcleo familiar. ";
			}
			if (!referencias)
			{
				mensaje += "Referencias personales (Dos amigos y un familiar). ";
			}
			if (!direcciones)
			{
				mensaje += "Dirección y teléfonos del afiliado. ";
			}


			return mensaje;
		}

		//FELVIR01
		private void p_get_datos_solicitantexCC(string p_codigo_cliente)
		{
			DataTable dt = da.ObtenerDatosClientexCodigoCliente(p_codigo_cliente);
			if (dt.Rows.Count > 0)
			{
				//FELVIR01-20190610: Primero evalúa si la persona tiene actualizados sus datos
				if (gmodo_coopsafa.Equals("INS"))
				{
					DataTable actualizaciones = this.da.VerificarActualizacion(Convert.ToInt32(p_codigo_cliente));
					if (actualizaciones.Rows.Count > 0)
					{
						bool cliente = true;
						bool dependientes = true;
						bool nucleo = true;
						bool referencias = true;
						bool direcciones = true;
						bool conyuge = true;
						/*
						 * Si son iguales las cantidades, es porque están actualizados
						 */
						string respuesta = this.RevisarUltimaActualizacion(int.Parse(p_codigo_cliente), out cliente, out dependientes, out nucleo, out referencias,
													out direcciones, out conyuge);

						string estado = dt.Rows[0]["estado_civil"].ToString();
						if (!estado.Equals("Casado") && !estado.Equals("Union Libre"))
						{
							nucleo = true;
						}

						this.conyugeAct = conyuge;

						//Valida si está actualizado o no
						if (!cliente || !dependientes || !nucleo || !referencias || !direcciones)
						{
							string mensaje = "Para crear la solicitud debe actualizar campos del prestatario: " + respuesta;
							DialogResult x = MessageBox.Show(mensaje, "Actualizar ficha de afiliación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							this.skipMethod();
						}
						else
						{
							var codigo_id = dt.Rows[0]["codigo_tipo_identificacion"].ToString();
							txtIDSolicitante.Text = dt.Rows[0]["numero_identificacion"].ToString();

							//Evalua que la identificación tenga el formato correcto
							Regex rx = new Regex(@"(^([0-9]{4})-([0-9]{4})-([0-9]{5}))|(^([0-9]{2})-([0-9]{4})-([0-9]{4})-([0-9]{5}))");
							Match match = rx.Match(this.txtIDSolicitante.Text);
							bool pasar = match.Success;

							if (codigo_id.Equals("2"))
							{
								pasar = true;
							}

							if (!pasar)
							{
								char guion = '-';
								int aparece = 0;

								foreach (char item in this.txtIDSolicitante.Text)
								{
									if (item.Equals(guion))
										aparece++;
								}

								if (aparece < 3)
								{
									MessageBox.Show("El formato de identificación no es válido, debe llevar guiones. Vaya a la solicitud de Afiliación para corregir", "Ir a la Solicitud de Afiliación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
									this.skipMethod();
								}
							}

							if (!string.IsNullOrEmpty(txtIDSolicitante.Text))
							{
								txtIDSolicitante_Leave(null, null);
							}
							gvInfoFinanciera.Refresh();
						}
					}
				}
			}
			else
			{
				MessageBox.Show("No existe información del cliente");
				pbFotoVigente.Image = Properties.Resources.contacto_icon;
				txtIDSolicitante.Text = "";
				p_get_datos_solicitantexID(txtIDSolicitante.Text);
			}
		}

		//FELVIR01-20190618
		private void p_get_datos_solicitantexID(string p_no_identificacion)
		{
			if (!gmodo_coopsafa.Equals("UPD"))
			{
				if (da.ExisteSolicitudesEnElPeriodo(p_no_identificacion))
				{
					string vl_dias = da.ObtenerParametro("WFC-0004");
					MessageBox.Show("Existe una solicitud para el número de identificación ingresado en un periodo menor a " + vl_dias + " dias, presione ok para continuar..!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}

			DataTable dt = da.ObtenerDatosClientexIdentificacion(p_no_identificacion);
			this.AdvertenciaGeneral = string.Empty;
			if (dt.Rows.Count > 0)
			{
				if (!gmodo_coopsafa.Equals("UPD") & !!gmodo_coopsafa.Equals("CONS"))
				{
					txtCodigo_cliente.Text = dt.Rows[0]["codigo_cliente"].ToString();
					//this.p_get_datos_solicitantexCC(txtCodigo_cliente.Text);
					DataTable dtA = da.ObtenerDatosClientexCodigoCliente(txtCodigo_cliente.Text);
					if (dtA.Rows.Count > 0)
					{
						//FELVIR01-20190610: Primero evalúa si la persona tiene actualizados sus datos
						if (gmodo_coopsafa.Equals("INS"))
						{
							DataTable actualizaciones = this.da.VerificarActualizacion(Convert.ToInt32(txtCodigo_cliente.Text));
							if (actualizaciones.Rows.Count > 0)
							{
								bool cliente = true;
								bool dependientes = true;
								bool nucleo = true;
								bool referencias = true;
								bool direcciones = true;
								bool conyuge = true;
								/*
								 * Si son iguales las cantidades, es porque están actualizados
								 */
								string respuesta = this.RevisarUltimaActualizacion(int.Parse(txtCodigo_cliente.Text), out cliente, out dependientes, out nucleo, out referencias,
															out direcciones, out conyuge);

								string estado = dtA.Rows[0]["estado_civil"].ToString();
								if (!estado.Equals("Casado") && !estado.Equals("Union Libre"))
								{
									nucleo = true;
								}

								this.conyugeAct = conyuge;

								//Valida si está actualizado o no
								if (!cliente || !dependientes || !nucleo || !referencias || !direcciones)
								{
									string mensaje = "Para crear la solicitud debe actualizar campos del prestatario: " + respuesta;
									DialogResult x = MessageBox.Show(mensaje, "Actualizar ficha de afiliación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
									this.skipMethod();
								}
								else
								{
									txtIDSolicitante.Text = dtA.Rows[0]["numero_identificacion"].ToString();

									//Evalua que la identificación tenga el formato correcto
									Regex rx = new Regex(@"(^([0-9]{4})-([0-9]{4})-([0-9]{5}))|(^([0-9]{2})-([0-9]{4})-([0-9]{4})-([0-9]{5}))");
									Match match = rx.Match(this.txtIDSolicitante.Text);

									if (!match.Success)
									{
										MessageBox.Show("El formato de identificación no es válido, debe llevar guiones. Vaya a la solicitud de Afiliación para corregir", "Ir a la Solicitud de Afiliación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
										this.skipMethod();
									}

									//if (!string.IsNullOrEmpty(txtIDSolicitante.Text))
									//{
									//	txtIDSolicitante_Leave(null, null);
									//}
									//gvInfoFinanciera.Refresh();
								}
							}
						}
					}
				}

				this.txtCodigo_cliente.Text = dt.Rows[0]["codigo_cliente"].ToString();
				txtIDSolicitante.Text = dt.Rows[0]["numero_identificacion"].ToString();
				txtNombre.Text = dt.Rows[0]["nombres"].ToString();
				txtPrimer_apellido.Text = dt.Rows[0]["primer_apellido"].ToString();
				txtSegundo_apellido.Text = dt.Rows[0]["segundo_apellido"].ToString();
				txtApellido_casada.Text = dt.Rows[0]["apellido_de_casada"].ToString();
				txtEstado_civil.Text = dt.Rows[0]["estado_civil"].ToString();
				txtSexo.Text = dt.Rows[0]["sexo"].ToString();
				string fec = dt.Rows[0]["fecha_de_nacimiento"].ToString();
				var convertida = Convert.ToDateTime(fec);
				//if (dt.Rows[0]["fecha_de_nacimiento"].ToString().Length >= 10)
				//{
				//Edad - FELVIR01 - 20190619
				txtFecha_nacimiento.Text = convertida.ToShortDateString();
				int edad = DateTime.Today.AddTicks(-convertida.Ticks).Year - 1;
				this.txtEdad_presta.Text = edad.ToString();
				//}
				string is_deduccion_x_planila = dt.Rows[0]["notaria"].ToString();
				if (is_deduccion_x_planila == "S")
				{
					txtVentanilla_planilla.Text = "PLANILLA";
				}
				else
					txtVentanilla_planilla.Text = "VENTANILLA";

				this.txtLugar_nacimiento.Text = dt.Rows[0]["lugar_nac"].ToString();
				txtNacionalidad.Text = dt.Rows[0]["nacionalidad"].ToString();
				txtDireccion_res.Text = dt.Rows[0]["direccion_res"].ToString();
				txtTelefono_fijo.Text = dt.Rows[0]["telefono_casa"].ToString();
				txtCelular.Text = dt.Rows[0]["celular"].ToString();
				txtCorreo_personal.Text = dt.Rows[0]["correo"].ToString().ToLower();

				if (dt.Rows[0]["NUMERO_IDENTIFICACION_R"].ToString().Equals("0") || string.IsNullOrEmpty(dt.Rows[0]["NUMERO_IDENTIFICACION_R"].ToString()))
				{
					this.AdvertenciaGeneral += "Falta RTN. ";
				}

				this.txtRtnSolicitante.Text = dt.Rows[0]["NUMERO_IDENTIFICACION_R"].ToString();
				//Tipo nivel educativo
				string nivel_educativo = dt.Rows[0]["nivel_educativo"].ToString();
				switch (nivel_educativo)
				{
					case "P":
						rbNePrimaria.Checked = true;
						break;
					case "S":
						rbNeSecundaria.Checked = true;
						break;
					case "U":
						rbNeUniversitaria.Checked = true;
						break;
					default:
						break;
				}
				//Tipo vivienda
				string tipo_vivienda = dt.Rows[0]["tipo_vivienda"].ToString();
				switch (tipo_vivienda)
				{
					case "PROPIA":
						rbTvPropia.Checked = true;
						break;
					case "ALQUILADA":
						rbTvAlquilada.Checked = true;
						break;
					case "FAMILIAR":
						rbTvFamiliar.Checked = true;
						break;
					default:
						rbTvOtros.Checked = true;
						break;
				}
				//Tipo empresa
				string tip_empresa = dt.Rows[0]["IND_TIPO_EMPRESA"].ToString();
				switch (tip_empresa)
				{
					case "P":
						this.rbTe_privado.Checked = true;
						break;
					case "U":
						this.rbTe_publico.Checked = true;
						break;
					case "C":
						this.rbTe_comerciante.Checked = true;
						break;
					case "O":
						this.rbTe_otros.Checked = true;
						this.txtTipo_empresa_especificar.Text = dt.Rows[0]["TIPO_EMPRESA"].ToString();
						break;
					default:
						break;
				}

				txtProfesion_oficio.Text = dt.Rows[0]["profesion_oficio"].ToString();
				txtPatrono.Text = dt.Rows[0]["lugar_de_trabajo"].ToString();
				txtCargo.Text = dt.Rows[0]["nombre_cargo"].ToString();
				txtDepto_labora.Text = dt.Rows[0]["persona_a_contactar_2"].ToString();
				string antiguedad_laboral = dt.Rows[0]["antiguedad_laboral_meses"].ToString();
				if (!string.IsNullOrEmpty(antiguedad_laboral))
				{
					txtAntiguedad_laboral.Text = antiguedad_laboral;
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["fecha_de_ingreso_pais"].ToString()))
				{
					var fechaIngreso = Convert.ToDateTime(dt.Rows[0]["fecha_de_ingreso_pais"].ToString());
					this.txtFechaIngresoLaboral.Text = fechaIngreso.ToShortDateString();
				}
				this.txtTipoContrato.Text = dt.Rows[0]["tipo_contrato"].ToString();
				txtDireccion_lab.Text = dt.Rows[0]["direccion_lab"].ToString();
				txtTelefono_trabajo1.Text = dt.Rows[0]["telefono_trabajo"].ToString();
				txtTelefono_trabajo2.Text = dt.Rows[0]["otro_telefono"].ToString();
				txtCorreo_laboral.Text = dt.Rows[0]["correo_lab"].ToString();
				//Otros ingresos - 20190607/FELVIR01
				try
				{
					var otrosIngresos = Convert.ToDecimal(dt.Rows[0]["ingresos_mes"].ToString());
					var ingresosExtra = Convert.ToDecimal(dt.Rows[0]["otros_ingresos"].ToString());
					this.txtIngresos.Text = otrosIngresos.ToString("0,0.00", CultureInfo.InvariantCulture);
					this.txtOtros_ingresos.Text = ingresosExtra.ToString("0,0.00", CultureInfo.InvariantCulture);
				}
				catch (Exception)
				{
					MessageBox.Show("El dato de otros ingresos contiene un valor no numérico. Corrija en la solicitud de afiliación antes de continuar.");
					this.skipMethod();
				}

				//Dependientes. FELVIR01-20190607
				var dep = this.da.ObtenerDependientes(int.Parse(this.txtCodigo_cliente.Text));
				//var dependientes = Convert.ToInt32(dep.Rows[0]["hijos"].ToString()) + Convert.ToInt32(dep.Rows[0]["otros"].ToString());

				if (gmodo_coopsafa.Equals("INS"))
				{
					this.txtNoHijos.Text = "0"; //dep.Rows[0]["hijos"].ToString();
					this.txtOtrospariente.Text = "0"; //dep.Rows[0]["otros"].ToString();
				}

				//FELVIR01-20190612. Trae la información del conyuge en caso de estar casados o en unión libre
				if (this.txtEstado_civil.Text.Equals("Casado") || this.txtEstado_civil.Text.Equals("Union Libre"))
				{
					if (!gmodo_coopsafa.Equals("UPD"))
					{
						if (!conyugeAct)
						{
							MessageBox.Show("No ha actualizado la información del cónyuge. Debe actualizarla para continuar.", "Ir a la solicitud de afiliación",
								MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							this.skipMethod();
						}
					}

					DataTable dtConyuge = this.da.InformacionConyuge(int.Parse(this.txtCodigo_cliente.Text));
					if (dtConyuge.Rows.Count > 0)
					{
						this.txtId_conyuge.Text = dtConyuge.Rows[0]["NUMERO_IDENTIFICACION"].ToString();
						this.txtPriNombre_tpConyuge.Text = dtConyuge.Rows[0]["PRIMER_NOMBRE"].ToString();
						this.txtSegNombre_tpConyuge.Text = dtConyuge.Rows[0]["SEGUNDO_NOMBRE"].ToString();
						this.txtPriApellido_tpConyuge.Text = dtConyuge.Rows[0]["PRIMER_APELLIDO"].ToString();
						this.txtSegApellido_tpConyuge.Text = dtConyuge.Rows[0]["SEGUNDO_APELLIDO"].ToString();
						string sexo = dtConyuge.Rows[0]["GENERO"].ToString();
						if (sexo == "M")
						{
							rbMasculino_tpConyuge.Checked = true;
							rbFemenino_tpConyuge.Checked = false;
						}
						else
						{
							rbMasculino_tpConyuge.Checked = false;
							rbFemenino_tpConyuge.Checked = true;
						}

						//numero de hijos
						this.txtNoHijos_tpConyuge.Text = dtConyuge.Rows[0]["NUM_HIJOS"].ToString();
						this.txtOtrospariente_tpConyuge.Text = dtConyuge.Rows[0]["NUM_PARIENTES"].ToString();

						this.txtDirecc_res_tpConyuge.Text = dtConyuge.Rows[0]["DIR_RESIDENCIA"].ToString();
						this.txtTelefono_tpConyuge.Text = dtConyuge.Rows[0]["TEL_RESIDENCIA"].ToString();
						this.txtCelular_tpConyuge.Text = dtConyuge.Rows[0]["NUM_CEL_RESIDENCIA"].ToString();
						this.txtCorreo_tpConyuge.Text = dtConyuge.Rows[0]["EMAIL_PERSONAL"].ToString().ToLower();

						//Teléfono adicionales
						this.txtOtrotelefono1_tpConyuge.Text = dtConyuge.Rows[0]["TELEFONO_ADIC1"].ToString().ToLower();
						this.txtOtrotelefono2_tpConyuge.Text = dtConyuge.Rows[0]["TELEFONO_ADIC2"].ToString().ToLower();

						string esAfiliado = dtConyuge.Rows[0]["IND_AFIL_COOPSAFA"].ToString();
						if (esAfiliado.Equals("S"))
						{
							this.rbSiAfiliado_tpConyuge.Checked = true;
							this.txtCodigo_cliente_TpConyuge.Text = dtConyuge.Rows[0]["CODIGO_CLIENTE_CONYUGE"].ToString();
						}
						else
						{
							this.rbSiAfiliado_tpConyuge.Checked = false;
							this.rbNoAfiliado_tpConyuge.Checked = true;
							this.txtCodigo_cliente_TpConyuge.Text = "0";
						}

						this.txtPatrono_tpConyuge.Text = dtConyuge.Rows[0]["LUGAR_TRABAJO"].ToString();
						this.txtCargo_tpConyuge.Text = dtConyuge.Rows[0]["CARGO_TRABAJO"].ToString();
						this.txtDeptolabora_tpConyuge.Text = dtConyuge.Rows[0]["DEPTO_TRABAJO"].ToString();
						this.txtDirecclaboral_tpConyuge.Text = dtConyuge.Rows[0]["DIR_LABORAL"].ToString();
						string antiguedad_laboralC = dtConyuge.Rows[0]["antiguedad_laboral_meses"].ToString();
						if (!string.IsNullOrEmpty(antiguedad_laboralC))
						{
							txtAntiglaboral_tpConyuge.Text = antiguedad_laboralC;
						}

						this.txtTellaboral1_tpConyuge.Text = dtConyuge.Rows[0]["TEL_LABORAL1"].ToString();
						this.txtExtlaboral1_tpConyuge.Text = dtConyuge.Rows[0]["EXT_LABORAL1"].ToString();
						this.txtTellaboral2_tpConyuge.Text = dtConyuge.Rows[0]["TEL_LABORAL2"].ToString();
						this.txtExtlaboral2_tpConyuge.Text = dtConyuge.Rows[0]["EXT_LABORAL2"].ToString();
						this.txtCorreolaboral_tpConyuge.Text = dtConyuge.Rows[0]["EMAIL_LABORAL"].ToString();
						this.txtProfesionCoyuge.Text = dtConyuge.Rows[0]["prof"].ToString();

						if (!string.IsNullOrEmpty(dtConyuge.Rows[0]["SALARIO_MENSUAL"].ToString()))
						{
							var otrosIngresosC = Convert.ToDecimal(dtConyuge.Rows[0]["SALARIO_MENSUAL"].ToString());
							this.txtIngresos_tpConyuge.Text = otrosIngresosC.ToString("0,0.00", CultureInfo.InvariantCulture);
						}
						else
						{
							this.txtIngresos_tpConyuge.Text = "0";
						}

						if (!string.IsNullOrEmpty(dtConyuge.Rows[0]["OTROS_INGRESOS"].ToString()))
						{
							var ingresosExtraC = Convert.ToDecimal(dtConyuge.Rows[0]["OTROS_INGRESOS"].ToString());
							this.txtOtrosIngresos_tpConyuge.Text = ingresosExtraC.ToString("0,0.00", CultureInfo.InvariantCulture);
						}
						else
						{
							this.txtOtrosIngresos_tpConyuge.Text = "0";
						}


						string nivel_educativoC = dtConyuge.Rows[0]["NIVEL_EDUCATIVO"].ToString();
						//Pendiente de la forma de almacenamiento
						switch (nivel_educativoC)
						{
							case "P":
								this.rbPrimario_Conyuge.Checked = true;
								break;
							case "S":
								this.rbSecundaria_Conyuge.Checked = true;
								break;
							case "U":
								this.rbUniversitario_Conyuge.Checked = true;
								break;
							default:
								break;
						}

						//Tipo de empresa
						string tipoEmpresa = dtConyuge.Rows[0]["TIPO_EMPRESA"].ToString();
						switch (tipoEmpresa)
						{
							case "P":
								this.rbTePrivado_tpConyuge.Checked = true;
								break;
							case "U":
								this.rbTePublico_tpConyuge.Checked = true;
								break;
							case "O":
								this.rbTeOtros_tpConyuge.Checked = true;
								this.txtTipoEmpresaotros_tpCoyuge.Text = dtConyuge.Rows[0]["OTRA_EMPRESA"].ToString();
								break;
							case "C":
								this.rbTeComerciante_tpConyuge.Checked = true;
								break;
							default:
								break;
						}


						this.saldo_aportaciones_conyuge = da.ObtenerSaldosAportacionesxCliente(txtId_conyuge.Text);
						this.estado_burointerno_conyuge = da.ObtenerBuroInterno_xId(txtId_conyuge.Text, out estado_desc_burointerno_conyuge, out observa_burointerno_conyuge).ToString();
						gvInfoFinanciera.Refresh();
					}
					else
					{
						MessageBox.Show("No existe información del conyuge. Debe ingresarla en la solicitud de afiliación para continuar", "Ir a la solicitud de afiliación",
							MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						this.skipMethod();
					}

				}

				//Referencias en duro 1 y 2
				DataTable dtRef = da.ObtenerReferenciasxCodigoCliente(txtCodigo_cliente.Text);
				//limpiando por si se ha hecho una consulta previa o se cambio la identificacion
				txtRef1.Text = "";
				txtRef1_direc.Text = "";
				txtRef1_telef.Text = "";
				txtRef2.Text = "";
				txtRef2_direc.Text = "";
				txtRef2_telef.Text = "";


				if (dtRef.Rows.Count < 3)
				{
					this.MsjActualizarCampos += "No se han actualizado las referencias";
				}

				for (int fila = 0; fila < dtRef.Rows.Count; fila++)
				{
					if (fila == 0)
					{
						txtRef1.Text = dtRef.Rows[fila]["nombre"].ToString();
						txtRef1_direc.Text = dtRef.Rows[fila]["metodo_localizacion"].ToString();
						txtRef1_telef.Text = dtRef.Rows[fila]["telefono"].ToString();
						//FELVIR01 - 20190607
						this.txtRef1_ptoref.Text = dtRef.Rows[fila]["pto_referencia"].ToString();
						this.txtRef1_casacolor.Text = dtRef.Rows[fila]["celular"].ToString();
					}
					if (fila == 1)
					{
						txtRef2.Text = dtRef.Rows[fila]["nombre"].ToString();
						txtRef2_direc.Text = dtRef.Rows[fila]["metodo_localizacion"].ToString();
						txtRef2_telef.Text = dtRef.Rows[fila]["telefono"].ToString();
						//FELVIR01 - 20190607
						this.txtRef2_ptoref.Text = dtRef.Rows[fila]["pto_referencia"].ToString();
						this.txtRef2_casacolor.Text = dtRef.Rows[fila]["celular"].ToString();
					}
					if (fila == 2)
					{
						txtRef3.Text = dtRef.Rows[fila]["nombre"].ToString();
						txtRef3_direc.Text = dtRef.Rows[fila]["metodo_localizacion"].ToString();
						txtRef3_telef.Text = dtRef.Rows[fila]["telefono"].ToString();
						//FELVIR01 - 20190607
						this.txtRef3_ptoref.Text = dtRef.Rows[fila]["pto_referencia"].ToString();
						this.txtRef3_casacolor.Text = dtRef.Rows[fila]["celular"].ToString();
						break;
					}
				}

				saldo_aportaciones_principal = da.ObtenerSaldosAportacionesxCliente(txtIDSolicitante.Text);
				estado_burointerno_principal = da.ObtenerBuroInterno_xId(txtIDSolicitante.Text, out estado_desc_burointerno_principal, out observa_burointerno_principal).ToString();


				//Control para creacion de solicitudes solo en OP para miembros de Junta Directiva.
				if (da.ObtenerParametro("WFC-0018") == "S")
				{
					if (da.EsMiembroJuntaDirectiva(txtCodigo_cliente.Text))
					{
						MessageBox.Show("El analisis y resoluciones de solicitudes de créditos de miembros de Junta Directiva, de acuerdo al proceso de creditos vigente, debe ser enviadas para su resolución al Depto. de Creditos & Riesgo y no en filial, por favor tomar nota..", "Recordatorio de Norma Vigente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}

				}

				string sid = txtIDSolicitante.Text.Replace("-", "");
				if (da.ExisteCEFDocentes(sid))
				{
					lLinfoCEF.Visible = true;
				}
				else
				{
					lLinfoCEF.Visible = false;
				}

				string vl_fecha_ultima_act = "";
				byte[] foto;
				da.ObtenerFotoAfiliado(txtCodigo_cliente.Text, out foto, out vl_fecha_ultima_act);
				if (foto != null)
				{
					pbFotoVigente.Image = DocSys.p_CopyDataToBitmap(foto);
				}
				else
				{
					pbFotoVigente.Image = Properties.Resources.contacto_icon;
				}

				if (!string.IsNullOrEmpty(this.AdvertenciaGeneral))
				{
					MessageBox.Show(this.AdvertenciaGeneral, "Ir a la Ficha de Solicitud!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					this.skipMethod();
				}

			}
			else
			{
				this.ReiniciarCamposSolicitante();

				DataRow row = dtInfoFinan.Select("rol='Solicitante'").FirstOrDefault();
				if (row == null)
				{
					dtInfoFinan.Rows.Add(txtIDSolicitante.Text, txtNombre.Text + " " + txtPrimer_apellido.Text + " " + txtSegundo_apellido.Text, "Solicitante", "", "", "", "");
				}
				else
				{
					row["numero_identificacion"] = txtIDSolicitante.Text;
					row["nombre"] = txtNombre.Text + " " + txtPrimer_apellido.Text + " " + txtSegundo_apellido.Text;
				}
			}


		}

		//FELVIR01-20190612
		private void skipMethod()
		{
			this.Close();
		}

		private bool EvaluarIdentificacion(string id)
		{
			Regex rx = new Regex(@"(^([0-9]{4})-([0-9]{4})-([0-9]{5}))|(^([0-9]{2})-([0-9]{4})-([0-9]{4})-([0-9]{5}))");
			Match match = rx.Match(id);

			if (!match.Success)
			{
				char guion = '-';
				int aparece = 0;

				foreach (char item in id)
				{
					if (item.Equals(guion))
						aparece++;
				}

				if (aparece < 3)
				{
					return false;
				}
				else
				{
					return true;
				}
			}

			return match.Success;
		}

		//FELVIR01-20190618
		private void p_get_datos_codeudor(string p_no_identificacion)
		{
			if (this.EvaluarIdentificacion(p_no_identificacion))
			{
				DataTable dt = da.ObtenerDatosClientexIdentificacion(p_no_identificacion);
				if (dt.Rows.Count > 0)
				{
					this.AdvertenciaGeneral = string.Empty;

					if (!gmodo_coopsafa.Equals("UPD"))
						this.LimpiarCamposCodeudor();

					//if (!gmodo_coopsafa.Equals("UPD"))
					{
						string codigo_cliente = dt.Rows[0]["codigo_cliente"].ToString();
						this.txtCodigo_cliente_TpCodeudor.Text = codigo_cliente;
					}

					string[] nombres = dt.Rows[0]["nombres"].ToString().Split(' ');
					txtPriNombre_tpCodeudor.Text = string.Empty;
					txtSegNombre_tpCodeudor.Text = string.Empty;
					for (int i = 0; i < nombres.Length; i++)
					{
						if (i == 0)
						{
							txtPriNombre_tpCodeudor.Text = nombres[i];
						}
						else
						{
							txtSegNombre_tpCodeudor.Text += nombres[i] + " ";
						}
					}

					txtPriApellido_tpCodeudor.Text = dt.Rows[0]["primer_apellido"].ToString();
					txtSegApellido_tpCodeudor.Text = dt.Rows[0]["segundo_apellido"].ToString();

					string sexo = dt.Rows[0]["sexo"].ToString();
					if (sexo == "Masculino")
					{
						rbMasculino_tpCodeudor.Checked = true;
						rbFemenino_tpCodeudor.Checked = false;
					}
					else
					{
						rbMasculino_tpCodeudor.Checked = false;
						rbFemenino_tpCodeudor.Checked = true;
					}

					this.txtEstadoCivil_tpCodeudor.Text = dt.Rows[0]["estado_civil"].ToString();
					string fec = dt.Rows[0]["fecha_de_nacimiento"].ToString();
					var convertida = Convert.ToDateTime(fec);
					int edad = DateTime.Today.AddTicks(-convertida.Ticks).Year - 1;
					this.txtEdad_tpCodeudor.Text = edad.ToString();

					txtDirecc_res_tpCodeudor.Text = dt.Rows[0]["direccion_res"].ToString();
					txtTelefono_tpCodeudor.Text = dt.Rows[0]["telefono_casa"].ToString();
					txtCelular_tpCodeudor.Text = dt.Rows[0]["celular"].ToString();
					txtCorreo_tpCodeudor.Text = dt.Rows[0]["correo"].ToString().ToLower();

					#region afiliado si o no

					string codigo_tipo_cliente = dt.Rows[0]["codigo_tipo_cliente"].ToString();
					if (codigo_tipo_cliente == "5") // 5 es afiliado 9 es Aval
					{
						rbSiAfiliado_tpCodeudor.Checked = true;
						rbNoAfiliado_tpCodeudor.Checked = false;
						txtCodigo_cliente_TpCodeudor.Text = dt.Rows[0]["codigo_cliente"].ToString();
					}
					else
					{
						rbSiAfiliado_tpCodeudor.Checked = false;
						rbNoAfiliado_tpCodeudor.Checked = true;
						txtCodigo_cliente_TpCodeudor.Text = dt.Rows[0]["codigo_cliente"].ToString();
					}

					#endregion

					if (string.IsNullOrEmpty(dt.Rows[0]["lugar_de_trabajo"].ToString()))
						this.AdvertenciaGeneral += "Falta el lugar de trabajo del Codeudor. ";
					else
						txtPatrono_tpCodeudor.Text = dt.Rows[0]["lugar_de_trabajo"].ToString();

					if (string.IsNullOrEmpty(dt.Rows[0]["nombre_cargo"].ToString()))
						this.AdvertenciaGeneral += "Falta el cargo que desempeña. ";
					else
						txtCargo_tpCodeudor.Text = dt.Rows[0]["nombre_cargo"].ToString();

					txtDeptolabora_tpCodeudor.Text = dt.Rows[0]["persona_a_contactar_2"].ToString();
					string antiguedad_laboral = dt.Rows[0]["antiguedad_laboral_meses"].ToString();
					if (!string.IsNullOrEmpty(antiguedad_laboral))
					{
						txtAntiglaboral_tpCodeudor.Text = antiguedad_laboral;
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["fecha_de_ingreso_pais"].ToString()))
					{
						var fechaIngreso = Convert.ToDateTime(dt.Rows[0]["fecha_de_ingreso_pais"].ToString());
						this.txtIngresoLab_Co.Text = fechaIngreso.ToShortDateString();
					}

					if (string.IsNullOrEmpty(dt.Rows[0]["NUMERO_IDENTIFICACION_R"].ToString()))
						this.AdvertenciaGeneral += "Falta el RTN del codeudor. ";
					else
						this.txtRtn_Codeurdor.Text = dt.Rows[0]["NUMERO_IDENTIFICACION_R"].ToString();

					if (string.IsNullOrEmpty(dt.Rows[0]["direccion_lab"].ToString()))
						this.AdvertenciaGeneral += "Falta la dirección laboral del codeudor. ";
					else
						txtDirecclaboral_tpCodeudor.Text = dt.Rows[0]["direccion_lab"].ToString();

					txtTellaboral1_tpCodeudor.Text = dt.Rows[0]["telefono_trabajo"].ToString();
					txtTellaboral2_tpCodeudor.Text = dt.Rows[0]["otro_telefono"].ToString();
					txtCorreolaboral_tpCodeudor.Text = dt.Rows[0]["correo_lab"].ToString();

					string tipo_vivienda = dt.Rows[0]["tipo_vivienda"].ToString();
					switch (tipo_vivienda)
					{
						case "PROPIA":
							rbTp_PropiaCodeudor.Checked = true;
							break;
						case "ALQUILADA":
							rbTp_AlquiladaCodeudor.Checked = true;
							break;
						case "FAMILIAR":
							rbTp_FamiliarCodeudor.Checked = true;
							break;
						default:
							rbTp_OtrosCodeudor.Checked = true;
							break;
					}

					string tip_empresa = dt.Rows[0]["IND_TIPO_EMPRESA"].ToString();
					switch (tip_empresa)
					{
						case "P":
							this.rbTePrivado_tpCodeudor.Checked = true;
							break;
						case "U":
							this.rbTePublico_tpCodeudor.Checked = true;
							break;
						case "C":
							this.rbTeComerciante_tpCodeudor.Checked = true;
							break;
						case "O":
							this.rbTeOtros_tpCodeudor.Checked = true;
							this.txtTipoEmpresaotros_tpCodeudor.Text = dt.Rows[0]["TIPO_EMPRESA"].ToString();
							break;
						default:
							break;
					}

					//FELVIR01-20190612
					var SaldoBase = Convert.ToDecimal(dt.Rows[0]["ingresos_mes"].ToString());

					if (SaldoBase == 0)
					{
						this.AdvertenciaGeneral += "Faltan los ingresos del Coduedor";
					}

					var ingresosExtra = Convert.ToDecimal(dt.Rows[0]["otros_ingresos"].ToString());
					this.txtIngresos_tpCodeudor.Text = SaldoBase.ToString("0,0.00", CultureInfo.InvariantCulture);
					this.txtOtrosIngresos_tpCodeudor.Text = ingresosExtra.ToString("0,0.00", CultureInfo.InvariantCulture);

					//Dependientes
					var dep = this.da.ObtenerDependientes(int.Parse(this.txtCodigo_cliente.Text));
					if (gmodo_coopsafa.Equals("INS"))
					{
						this.txtNoHijos_tpCodeudor.Text = "0";//dep.Rows[0]["hijos"].ToString();
						this.txtOtrospariente_tpCodeudor.Text = "0";//dep.Rows[0]["otros"].ToString();
					}

					string estado_civil = dt.Rows[0]["estado_civil"].ToString();
					if (estado_civil.Equals("Casado") || estado_civil.Equals("Union Libre"))
					{
						this.txtNombre_conyuge_tpCodeudor.Text = dt.Rows[0]["NOMBRE_CONJUGUE"].ToString();
						this.txtDirelab_conyuge_tpCodeudor.Text = dt.Rows[0]["lugar_trabajo_conyuge"].ToString();
					}

					//Referencias en duro 1 y 2
					DataTable dtRef = da.ObtenerReferenciasxCodigoCliente(this.txtCodigo_cliente_TpCodeudor.Text);
					//limpiando por si se ha hecho una consulta previa o se cambio la identificacion
					txtRef1_tpCodeudor.Text = "";
					txtRef1_direc_tpCodeudor.Text = "";
					txtRef1_telef_tpCodeudor.Text = "";
					txtRef2_tpCodeudor.Text = "";
					txtRef2_direc_tpCodeudor.Text = "";
					txtRef2_telef_tpCodeudor.Text = "";


					if (!gmodo_coopsafa.Equals("UPD"))
					{
						if (dtRef.Rows.Count < 3)
						{
							this.MsjActualizarCampos += "No se han actualizado las referencias";
						}
					}

					for (int fila = 0; fila < dtRef.Rows.Count; fila++)
					{
						if (fila == 0)
						{
							txtRef1_tpCodeudor.Text = dtRef.Rows[fila]["nombre"].ToString();
							txtRef1_direc_tpCodeudor.Text = dtRef.Rows[fila]["metodo_localizacion"].ToString();
							txtRef1_telef_tpCodeudor.Text = dtRef.Rows[fila]["telefono"].ToString();
							//FELVIR01-20190618
							this.txtRef1_ptoref_tpCodeudor.Text = dtRef.Rows[fila]["pto_referencia"].ToString();
							this.txtRef1_casacolor_tpCodeudor.Text = dtRef.Rows[fila]["celular"].ToString();
						}
						if (fila == 1)
						{
							txtRef2_tpCodeudor.Text = dtRef.Rows[fila]["nombre"].ToString();
							txtRef2_direc_tpCodeudor.Text = dtRef.Rows[fila]["metodo_localizacion"].ToString();
							txtRef2_telef_tpCodeudor.Text = dtRef.Rows[fila]["telefono"].ToString();
							//FELVIR01-20190618
							this.txtRef2_ptoref_tpCodeudor.Text = dtRef.Rows[fila]["pto_referencia"].ToString();
							this.txtRef2_casacolor_tpCodeudor.Text = dtRef.Rows[fila]["celular"].ToString();
						}

						if (fila == 2)
						{
							this.txtRef3_tpCodeudor.Text = dtRef.Rows[fila]["nombre"].ToString();
							this.txtRef3_direc_tpCodeudor.Text = dtRef.Rows[fila]["metodo_localizacion"].ToString();
							this.txtRef3_telef_tpCodeudor.Text = dtRef.Rows[fila]["telefono"].ToString();
							this.txtRef3_ptoref_tpCodeudor.Text = dtRef.Rows[fila]["pto_referencia"].ToString();
							this.txtRef3_casacolor_tpCodeudor.Text = dtRef.Rows[fila]["celular"].ToString();
							break;
						}
					}

					//if (!gmodo_coopsafa.Equals("UPD"))
					{
						saldo_aportaciones_codeudor = da.ObtenerSaldosAportacionesxCliente(txtId_codeudor.Text);
						estado_burointerno_codeudor = da.ObtenerBuroInterno_xId(txtId_codeudor.Text, out estado_desc_burointerno_codeudor, out observa_burointerno_codeudor).ToString();
						gvInfoFinanciera.Refresh();
					}

					if (!string.IsNullOrEmpty(this.AdvertenciaGeneral))
					{
						MessageBox.Show(this.AdvertenciaGeneral, "Ir a la Ficha de Solicitud!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						this.skipMethod();
					}
				}
				else
				{
					this.LimpiarCamposCodeudor();
				}
			}
			else
			{
				MessageBox.Show("El formato de identificación no es válido, debe llevar guiones. Vaya a la solicitud de Afiliación para corregir", "Ir a la Solicitud de Afiliación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				this.skipMethod();
			}

		}

		private void LimpiarCamposCodeudor()
		{
			txtPriNombre_tpCodeudor.Text = "";
			txtSegNombre_tpCodeudor.Text = "";
			txtPriApellido_tpCodeudor.Text = "";
			txtSegApellido_tpCodeudor.Text = "";
			txtDirecc_res_tpCodeudor.Text = "";
			txtTelefono_tpCodeudor.Text = "";
			txtCelular_tpCodeudor.Text = "";
			txtCorreo_tpCodeudor.Text = "";

			this.txtCargo_conyuge_tpCodeudor.Text = string.Empty;
			this.txtNombre_conyuge_tpCodeudor.Text = string.Empty;
			this.txtDirelab_conyuge_tpCodeudor.Text = string.Empty;
			this.txtDirelab_conyuge_tpCodeudor.Text = string.Empty;

			txtPatrono_tpCodeudor.Text = "";
			txtCargo_tpCodeudor.Text = "";
			txtAntiglaboral_tpCodeudor.Text = "";
			txtDirecclaboral_tpCodeudor.Text = "";
			txtTellaboral1_tpCodeudor.Text = "";
			txtTellaboral2_tpCodeudor.Text = "";
			txtCorreolaboral_tpCodeudor.Text = "";

			txtRef1_tpCodeudor.Text = "";
			txtRef1_direc_tpCodeudor.Text = "";
			txtRef1_telef_tpCodeudor.Text = "";
			txtRef2_tpCodeudor.Text = "";
			txtRef2_direc_tpCodeudor.Text = "";
			txtRef2_telef_tpCodeudor.Text = "";
			this.txtIngresoLab_Co.Text = string.Empty;
		}

		//FELVIR01-20190618
		private void p_get_datos_aval1(string p_no_identificacion)
		{

			if (!this.EvaluarIdentificacion(p_no_identificacion))
			{
				MessageBox.Show("El formato de identificación no es válido, debe llevar guiones. Vaya a la solicitud de Afiliación para corregir", "Ir a la Solicitud de Afiliación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				this.skipMethod();
			}

			DataTable dt = da.ObtenerDatosClientexIdentificacion(p_no_identificacion);
			if (dt.Rows.Count > 0)
			{

				this.AdvertenciaGeneral = string.Empty;

				if (!gmodo_coopsafa.Equals("UPD"))
					this.LimpiarCamposAval1();

				string codigo_cliente = dt.Rows[0]["codigo_cliente"].ToString();
				this.txtCodigo_cliente_TpAval1.Text = codigo_cliente;
				string[] nombres = dt.Rows[0]["nombres"].ToString().Split(' ');
				txtPriNombre_tpAval1.Text = string.Empty;
				txtSegNombre_tpAval1.Text = string.Empty;
				for (int i = 0; i < nombres.Length; i++)
				{
					if (i == 0)
					{
						txtPriNombre_tpAval1.Text = nombres[i];
					}
					else
					{
						txtSegNombre_tpAval1.Text += nombres[i] + " ";
					}
				}

				//if (nombres.Length >= 3)
				//{
				//	txtPriNombre_tpAval1.Text = nombres[0];
				//	txtSegNombre_tpAval1.Text = nombres[2];
				//}
				//else
				//{
				//	txtPriNombre_tpAval1.Text = dt.Rows[0]["nombres"].ToString();
				//}
				txtPriApellido_tpAval1.Text = dt.Rows[0]["primer_apellido"].ToString();
				txtSegApellido_tpAval1.Text = dt.Rows[0]["segundo_apellido"].ToString();


				string sexo = dt.Rows[0]["sexo"].ToString();
				if (sexo == "Masculino")
				{
					rbMasculino_tpAval1.Checked = true;
					rbFemenino_tpAval1.Checked = false;
				}
				else
				{
					rbMasculino_tpAval1.Checked = false;
					rbFemenino_tpAval1.Checked = true;
				}


				this.txtEstadoCivil_tpAval1.Text = dt.Rows[0]["estado_civil"].ToString();
				string fec = dt.Rows[0]["fecha_de_nacimiento"].ToString();
				var convertida = Convert.ToDateTime(fec);
				int edad = DateTime.Today.AddTicks(-convertida.Ticks).Year - 1;
				this.txtEdad_tpAval1.Text = edad.ToString();

				if (string.IsNullOrEmpty(dt.Rows[0]["NUMERO_IDENTIFICACION_R"].ToString()) || dt.Rows[0]["NUMERO_IDENTIFICACION_R"].ToString().Equals("0"))
					this.AdvertenciaGeneral += "Falta RNT del Aval1. ";
				else
					this.txtRtnAval1.Text = dt.Rows[0]["NUMERO_IDENTIFICACION_R"].ToString();

				txtDirecc_res_tpAval1.Text = dt.Rows[0]["direccion_res"].ToString();
				txtTelefono_tpAval1.Text = dt.Rows[0]["telefono_casa"].ToString();
				txtCelular_tpAval1.Text = dt.Rows[0]["celular"].ToString();
				txtCorreo_tpAval1.Text = dt.Rows[0]["correo"].ToString().ToLower();

				#region afiliado si o no
				string codigo_tipo_cliente = dt.Rows[0]["codigo_tipo_cliente"].ToString();
				txtCodigo_cliente_TpAval1.Text = dt.Rows[0]["codigo_cliente"].ToString();
				if (codigo_tipo_cliente == "5") // 5 es afiliado 9 es Aval
				{
					rbSiAfiliado_tpAval1.Checked = true;
					rbNoAfiliado_tpAval1.Checked = false;
					txtCodigo_cliente_TpAval1.Text = dt.Rows[0]["codigo_cliente"].ToString();
				}
				else
				{
					rbSiAfiliado_tpAval1.Checked = false;
					rbNoAfiliado_tpAval1.Checked = true;
					//if (!string.IsNullOrEmpty(dt.Rows[0]["codigo_cliente"].ToString()))
					txtCodigo_cliente_TpAval1.Text = dt.Rows[0]["codigo_cliente"].ToString();
					//else
					//	txtCodigo_cliente_TpAval1.Text = "0";
				}
				#endregion

				var dep = this.da.ObtenerDependientes(int.Parse(this.txtCodigo_cliente_TpAval1.Text));
				if (gmodo_coopsafa.Equals("INS"))
				{
					this.txtNoHijos_tpAval1.Text = "0";//dep.Rows[0]["hijos"].ToString();
					this.txtOtrospariente_tpAval1.Text = "0";//dep.Rows[0]["otros"].ToString();
				}

				if (string.IsNullOrEmpty(dt.Rows[0]["lugar_de_trabajo"].ToString()))
					this.AdvertenciaGeneral += "Falta el lugar de trabajo del Aval1. ";
				else
					txtPatrono_tpAval1.Text = dt.Rows[0]["lugar_de_trabajo"].ToString();

				if (string.IsNullOrEmpty(dt.Rows[0]["nombre_cargo"].ToString()))
					this.AdvertenciaGeneral += "Falta el cargo del Aval1. ";
				else
					txtCargo_tpAval1.Text = dt.Rows[0]["nombre_cargo"].ToString();

				txtDeptolabora_tpAval1.Text = dt.Rows[0]["persona_a_contactar_2"].ToString();
				string antiguedad_laboral = dt.Rows[0]["antiguedad_laboral_meses"].ToString();
				if (!string.IsNullOrEmpty(antiguedad_laboral))
				{
					txtAntiglaboral_tpAval1.Text = antiguedad_laboral;
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["fecha_de_ingreso_pais"].ToString()))
				{
					var fechaIngreso = Convert.ToDateTime(dt.Rows[0]["fecha_de_ingreso_pais"].ToString());
					this.txtFechaIngreso_Aval1.Text = fechaIngreso.ToShortDateString();
				}

				if (string.IsNullOrEmpty(dt.Rows[0]["direccion_lab"].ToString()))
					this.AdvertenciaGeneral += "Falta dirección laboral del Aval1. ";
				else
					txtDirecclaboral_tpAval1.Text = dt.Rows[0]["direccion_lab"].ToString();

				txtTellaboral1_tpAval1.Text = dt.Rows[0]["telefono_trabajo"].ToString();
				txtTellaboral2_tpAval1.Text = dt.Rows[0]["otro_telefono"].ToString();
				txtCorreolaboral_tpAval1.Text = dt.Rows[0]["correo_lab"].ToString();


				string tipo_vivienda = dt.Rows[0]["tipo_vivienda"].ToString();
				switch (tipo_vivienda)
				{
					case "PROPIA":
						rbTp_PropiaAval1.Checked = true;
						break;
					case "ALQUILADA":
						rbTp_AlquiladaAval1.Checked = true;
						break;
					case "FAMILIAR":
						rbTp_FamiliarAval1.Checked = true;
						break;
					default:
						rbTp_OtrosAval1.Checked = true;
						break;
				}

				string tip_empresa = dt.Rows[0]["IND_TIPO_EMPRESA"].ToString();
				switch (tip_empresa)
				{
					case "P":
						this.rbTePrivado_tpAval1.Checked = true;
						break;
					case "U":
						this.rbTePublico_tpAval1.Checked = true;
						break;
					case "C":
						this.rbTeComerciante_tpAval1.Checked = true;
						break;
					case "O":
						this.rbTeOtros_tpAval1.Checked = true;
						this.txtTipoEmpresaotros_tpAval1.Text = dt.Rows[0]["TIPO_EMPRESA"].ToString();
						break;
					default:
						break;
				}

				//FELVIR01-20190612
				var SueldoBase = Convert.ToDecimal(dt.Rows[0]["ingresos_mes"].ToString());
				var ingresosExtra = Convert.ToDecimal(dt.Rows[0]["otros_ingresos"].ToString());
				this.txtIngresos_tpAval1.Text = SueldoBase.ToString("0,0.00", CultureInfo.InvariantCulture);
				this.txtOtrosIngresos_tpAval1.Text = ingresosExtra.ToString("0,0.00", CultureInfo.InvariantCulture);

				if (SueldoBase == 0)
				{
					this.AdvertenciaGeneral += "Faltan los ingresos del Aval1. ";
				}

				string estado_civil = dt.Rows[0]["estado_civil"].ToString();
				if (estado_civil.Equals("Casado") || estado_civil.Equals("Union Libre"))
				{
					this.txtNombre_conyuge_tpAval1.Text = dt.Rows[0]["NOMBRE_CONJUGUE"].ToString();
					this.txtDirelab_conyuge_tpAval1.Text = dt.Rows[0]["lugar_trabajo_conyuge"].ToString();
				}

				//Referencias en duro 1 y 2
				DataTable dtRef = da.ObtenerReferenciasxCodigoCliente(this.txtCodigo_cliente_TpAval1.Text);
				//limpiando por si se ha hecho una consulta previa o se cambio la identificacion
				txtRef1_tpAval1.Text = "";
				txtRef1_direc_tpAval1.Text = "";
				txtRef1_telef_tpAval1.Text = "";
				txtRef2_tpAval1.Text = "";
				txtRef2_direc_tpAval1.Text = "";
				txtRef2_telef_tpAval1.Text = "";
				this.txtRef3_tpAval1.Text = string.Empty;
				this.txtRef3_direc_tpAval1.Text = string.Empty;
				this.txtRef3_telef_tpAval1.Text = string.Empty;

				for (int fila = 0; fila < dtRef.Rows.Count; fila++)
				{
					if (fila == 0)
					{
						txtRef1_tpAval1.Text = dtRef.Rows[fila]["nombre"].ToString();
						txtRef1_direc_tpAval1.Text = dtRef.Rows[fila]["metodo_localizacion"].ToString();
						txtRef1_telef_tpAval1.Text = dtRef.Rows[fila]["telefono"].ToString();
						//FELVIR01 - 20190607
						this.txtRef1_ptoref_tpAval1.Text = dtRef.Rows[fila]["pto_referencia"].ToString();
						this.txtRef1_casacolor_tpAval1.Text = dtRef.Rows[fila]["celular"].ToString();
					}
					if (fila == 1)
					{
						txtRef2_tpAval1.Text = dtRef.Rows[fila]["nombre"].ToString();
						txtRef2_direc_tpAval1.Text = dtRef.Rows[fila]["metodo_localizacion"].ToString();
						txtRef2_telef_tpAval1.Text = dtRef.Rows[fila]["telefono"].ToString();
						//FELVIR01 - 20190607
						this.txtRef2_ptoref_tpAval1.Text = dtRef.Rows[fila]["pto_referencia"].ToString();
						this.txtRef2_casacolor_tpAval1.Text = dtRef.Rows[fila]["celular"].ToString();
					}
					if (fila == 2)
					{
						txtRef3_tpAval1.Text = dtRef.Rows[fila]["nombre"].ToString();
						txtRef3_direc_tpAval1.Text = dtRef.Rows[fila]["metodo_localizacion"].ToString();
						txtRef3_telef_tpAval1.Text = dtRef.Rows[fila]["telefono"].ToString();
						this.txtRef3_ptoref_tpAval1.Text = dtRef.Rows[fila]["pto_referencia"].ToString();
						this.txtRef3_casacolor_tpAval1.Text = dtRef.Rows[fila]["celular"].ToString();
						break;
					}
				}

				//if (!gmodo_coopsafa.Equals("UPD"))
				//{
				saldo_aportaciones_aval1 = da.ObtenerSaldosAportacionesxCliente(txtId_aval1.Text);
				estado_burointerno_aval1 = da.ObtenerBuroInterno_xId(txtId_aval1.Text, out estado_desc_burointerno_aval1, out observa_burointerno_aval1).ToString();
				gvInfoFinanciera.Refresh();
				//}
				p_llenar_descripcion_garantia();

				if (!string.IsNullOrEmpty(this.AdvertenciaGeneral))
				{
					MessageBox.Show(this.AdvertenciaGeneral, "Ir a la ficha de afiliación!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					this.skipMethod();
				}
			}
			else
			{
				this.LimpiarCamposAval1();

			}
		}

		private void LimpiarCamposAval1()
		{
			//Personales
			txtPriNombre_tpAval1.Text = "";
			txtSegNombre_tpAval1.Text = "";
			txtPriApellido_tpAval1.Text = "";
			txtSegApellido_tpAval1.Text = "";
			txtDirecc_res_tpAval1.Text = "";
			txtTelefono_tpAval1.Text = "";
			txtCelular_tpAval1.Text = "";
			txtCorreo_tpAval1.Text = "";
			//Laborales
			txtPatrono_tpAval1.Text = "";
			txtCargo_tpAval1.Text = "";
			txtAntiglaboral_tpAval1.Text = "";
			txtDirecclaboral_tpAval1.Text = "";
			txtTellaboral1_tpAval1.Text = "";
			txtTellaboral2_tpAval1.Text = "";
			txtCorreolaboral_tpAval1.Text = "";
			//Conyuge
			this.txtNombre_conyuge_tpAval1.Text = string.Empty;
			this.txtCorreolaboral_tpAval1.Text = string.Empty;
			this.txtDirecclaboral_tpAval1.Text = string.Empty;
			this.txtTelefono_tpAval1.Text = string.Empty;
			//Referencias
			txtRef1_tpAval1.Text = "";
			txtRef1_direc_tpAval1.Text = "";
			txtRef1_telef_tpAval1.Text = "";
			txtRef2_tpAval1.Text = "";
			txtRef2_direc_tpAval1.Text = "";
			txtRef2_telef_tpAval1.Text = "";
			this.txtRef3_tpAval1.Text = string.Empty;
			this.txtRef3_direc_tpAval1.Text = string.Empty;
			this.txtRef3_telef_tpAval1.Text = string.Empty;
		}

		//FELVIR01-20190618
		private void p_get_datos_aval2(string p_no_identificacion)
		{
			if (!this.EvaluarIdentificacion(p_no_identificacion))
			{
				MessageBox.Show("El formato de identificación no es válido, debe llevar guiones. Vaya a la solicitud de Afiliación para corregir", "Ir a la Solicitud de Afiliación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				this.skipMethod();
			}

			DataTable dt = da.ObtenerDatosClientexIdentificacion(p_no_identificacion);
			if (dt.Rows.Count > 0)
			{
				this.AdvertenciaGeneral = string.Empty;

				if (!gmodo_coopsafa.Equals("UPD"))
					this.LimpiarCamposAval2();

				//if (!gmodo_coopsafa.Equals("UPD"))
				{
					string codigo_cliente = dt.Rows[0]["codigo_cliente"].ToString();
					this.txtCodigo_cliente_TpAval2.Text = codigo_cliente;
				}

				string[] nombres = dt.Rows[0]["nombres"].ToString().Split(' ');
				txtPriNombre_tpAval2.Text = string.Empty;
				txtSegNombre_tpAval2.Text = string.Empty;
				for (int i = 0; i < nombres.Length; i++)
				{
					if (i == 0)
					{
						txtPriNombre_tpAval2.Text = nombres[i];
					}
					else
					{
						txtSegNombre_tpAval2.Text += nombres[i] + " ";
					}
				}

				//if (nombres.Length >= 3)
				//{
				//	txtPriNombre_tpAval2.Text = nombres[0];
				//	txtSegNombre_tpAval2.Text = nombres[2];
				//}
				//else
				//{
				//	txtPriNombre_tpAval2.Text = dt.Rows[0]["nombres"].ToString();
				//}
				txtPriApellido_tpAval2.Text = dt.Rows[0]["primer_apellido"].ToString();
				txtSegApellido_tpAval2.Text = dt.Rows[0]["segundo_apellido"].ToString();

				string sexo = dt.Rows[0]["sexo"].ToString();
				if (sexo == "Masculino")
				{
					rbMasculino_tpAval2.Checked = true;
					rbFemenino_tpAval2.Checked = false;
				}
				else
				{
					rbMasculino_tpAval2.Checked = false;
					rbFemenino_tpAval2.Checked = true;
				}


				this.txtEstadoCivil_tpAval2.Text = dt.Rows[0]["estado_civil"].ToString();
				string fec = dt.Rows[0]["fecha_de_nacimiento"].ToString();
				var convertida = Convert.ToDateTime(fec);
				int edad = DateTime.Today.AddTicks(-convertida.Ticks).Year - 1;
				this.txtEdad_tpAval2.Text = edad.ToString();

				var dep = this.da.ObtenerDependientes(int.Parse(this.txtCodigo_cliente_TpAval2.Text));
				if (gmodo_coopsafa.Equals("INS"))
				{
					this.txtNoHijos_tpAval2.Text = "0";//dep.Rows[0]["hijos"].ToString();
					this.txtOtrospariente_tpAval2.Text = "0";//dep.Rows[0]["otros"].ToString();
				}

				txtDirecc_res_tpAval2.Text = dt.Rows[0]["direccion_res"].ToString();
				txtTelefono_tpAval2.Text = dt.Rows[0]["telefono_casa"].ToString();
				txtCelular_tpAval2.Text = dt.Rows[0]["celular"].ToString();
				txtCorreo_tpAval2.Text = dt.Rows[0]["correo"].ToString().ToLower();
				#region afiliado si o no
				string codigo_tipo_cliente = dt.Rows[0]["codigo_tipo_cliente"].ToString();
				txtCodigo_cliente_TpAval2.Text = dt.Rows[0]["codigo_cliente"].ToString();
				if (codigo_tipo_cliente == "5") // 5 es afiliado 9 es Aval
				{
					rbSiAfiliado_tpAval2.Checked = true;
					rbNoAfiliado_tpAval2.Checked = false;
					txtCodigo_cliente_TpAval2.Text = dt.Rows[0]["codigo_cliente"].ToString();
				}
				else
				{
					rbSiAfiliado_tpAval2.Checked = false;
					rbNoAfiliado_tpAval2.Checked = true;
					txtCodigo_cliente_TpAval2.Text = dt.Rows[0]["codigo_cliente"].ToString();
				}
				#endregion

				if (string.IsNullOrEmpty(dt.Rows[0]["lugar_de_trabajo"].ToString()))
					this.AdvertenciaGeneral += "Falta el lugar de trabajo del Aval2. ";
				else
					txtPatrono_tpAval2.Text = dt.Rows[0]["lugar_de_trabajo"].ToString();

				if (string.IsNullOrEmpty(dt.Rows[0]["nombre_cargo"].ToString()))
					this.AdvertenciaGeneral += "Falta el cargo del Aval2. ";
				else
					txtCargo_tpAval2.Text = dt.Rows[0]["nombre_cargo"].ToString();

				txtDeptolabora_tpAval2.Text = dt.Rows[0]["persona_a_contactar_2"].ToString();
				string antiguedad_laboral = dt.Rows[0]["antiguedad_laboral_meses"].ToString();
				if (!string.IsNullOrEmpty(antiguedad_laboral))
				{
					txtAntiglaboral_tpAval2.Text = antiguedad_laboral;
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["fecha_de_ingreso_pais"].ToString()))
				{
					var fechaIngreso = Convert.ToDateTime(dt.Rows[0]["fecha_de_ingreso_pais"].ToString());
					this.txtFechaIngreso_Aval2.Text = fechaIngreso.ToShortDateString();
				}

				if (string.IsNullOrEmpty(dt.Rows[0]["direccion_lab"].ToString()))
					this.AdvertenciaGeneral += "Falta la dirección laboral del Aval2. ";
				else
					txtDirecclaboral_tpAval2.Text = dt.Rows[0]["direccion_lab"].ToString();

				txtTellaboral1_tpAval2.Text = dt.Rows[0]["telefono_trabajo"].ToString();
				txtTellaboral2_tpAval2.Text = dt.Rows[0]["otro_telefono"].ToString();
				txtCorreolaboral_tpAval2.Text = dt.Rows[0]["correo_lab"].ToString();

				if (string.IsNullOrEmpty(dt.Rows[0]["NUMERO_IDENTIFICACION_R"].ToString()))
					this.AdvertenciaGeneral += "Falta el RTN del Aval2. ";
				else
					this.txtRtnAval2.Text = dt.Rows[0]["NUMERO_IDENTIFICACION_R"].ToString();

				string tipo_vivienda = dt.Rows[0]["tipo_vivienda"].ToString();
				switch (tipo_vivienda)
				{
					case "PROPIA":
						rbTp_PropiaAval2.Checked = true;
						break;
					case "ALQUILADA":
						rbTp_AlquiladaAval2.Checked = true;
						break;
					case "FAMILIAR":
						rbTp_FamiliarAval2.Checked = true;
						break;
					default:
						rbTp_OtrosAval2.Checked = true;
						break;
				}

				string tip_empresa = dt.Rows[0]["IND_TIPO_EMPRESA"].ToString();
				switch (tip_empresa)
				{
					case "P":
						this.rbTePrivado_tpAval2.Checked = true;
						break;
					case "U":
						this.rbTePublico_tpAval2.Checked = true;
						break;
					case "C":
						this.rbTeComerciante_tpAval2.Checked = true;
						break;
					case "O":
						this.rbTeOtros_tpAval2.Checked = true;
						this.txtTipoEmpresaotros_tpAval2.Text = dt.Rows[0]["TIPO_EMPRESA"].ToString();
						break;
					default:
						break;
				}

				//FELVIR01-20190612
				var SueldoBase = Convert.ToDecimal(dt.Rows[0]["ingresos_mes"].ToString());
				var ingresosExtra = Convert.ToDecimal(dt.Rows[0]["otros_ingresos"].ToString());
				this.txtIngresos_tpAval2.Text = SueldoBase.ToString("0,0.00", CultureInfo.InvariantCulture);
				this.txtOtrosIngresos_tpAval2.Text = ingresosExtra.ToString("0,0.00", CultureInfo.InvariantCulture);

				if (SueldoBase == 0)
					this.AdvertenciaGeneral += "Falta el ingreso del Aval2. ";

				string estado_civil = dt.Rows[0]["estado_civil"].ToString();
				if (estado_civil.Equals("Casado") || estado_civil.Equals("Union Libre"))
				{
					this.txtNombre_conyuge_tpAval2.Text = dt.Rows[0]["NOMBRE_CONJUGUE"].ToString();
					this.txtDirelab_conyuge_tpAval2.Text = dt.Rows[0]["lugar_trabajo_conyuge"].ToString();
				}

				//Referencias en duro 1 y 2
				DataTable dtRef = da.ObtenerReferenciasxCodigoCliente(this.txtCodigo_cliente_TpAval2.Text);
				//limpiando por si se ha hecho una consulta previa o se cambio la identificacion
				txtRef1_tpAval2.Text = "";
				txtRef1_direc_tpAval2.Text = "";
				txtRef1_telef_tpAval2.Text = "";
				txtRef2_tpAval2.Text = "";
				txtRef2_direc_tpAval2.Text = "";
				txtRef2_telef_tpAval2.Text = "";
				for (int fila = 0; fila < dtRef.Rows.Count; fila++)
				{
					if (fila == 0)
					{
						txtRef1_tpAval2.Text = dtRef.Rows[fila]["nombre"].ToString();
						txtRef1_direc_tpAval2.Text = dtRef.Rows[fila]["metodo_localizacion"].ToString();
						txtRef1_telef_tpAval2.Text = dtRef.Rows[fila]["telefono"].ToString();
						//FELVIR01 - 20190607
						this.txtRef1_ptoref_tpAval2.Text = dtRef.Rows[fila]["pto_referencia"].ToString();
						this.txtRef1_casacolor_tpAval2.Text = dtRef.Rows[fila]["celular"].ToString();
					}
					if (fila == 1)
					{
						txtRef2_tpAval2.Text = dtRef.Rows[fila]["nombre"].ToString();
						txtRef2_direc_tpAval2.Text = dtRef.Rows[fila]["metodo_localizacion"].ToString();
						txtRef2_telef_tpAval2.Text = dtRef.Rows[fila]["telefono"].ToString();
						//FELVIR01 - 20190607
						this.txtRef2_ptoref_tpAval2.Text = dtRef.Rows[fila]["pto_referencia"].ToString();
						this.txtRef2_casacolor_tpAval2.Text = dtRef.Rows[fila]["celular"].ToString();
					}
					if (fila == 2)
					{
						txtRef3_tpAval2.Text = dtRef.Rows[fila]["nombre"].ToString();
						txtRef3_direc_tpAval2.Text = dtRef.Rows[fila]["metodo_localizacion"].ToString();
						txtRef3_telef_tpAval2.Text = dtRef.Rows[fila]["telefono"].ToString();
						//FELVIR01 - 20190607
						this.txtRef3_ptoref_tpAval2.Text = dtRef.Rows[fila]["pto_referencia"].ToString();
						this.txtRef3_casacolor_tpAval2.Text = dtRef.Rows[fila]["celular"].ToString();
						break;
					}
				}

				//if (!gmodo_coopsafa.Equals("UPD"))
				{
					saldo_aportaciones_aval2 = da.ObtenerSaldosAportacionesxCliente(txtId_aval2.Text);
					estado_burointerno_aval2 = da.ObtenerBuroInterno_xId(txtId_aval2.Text, out estado_desc_burointerno_aval2, out observa_burointerno_aval2).ToString();

					gvInfoFinanciera.Refresh();
				}

				p_llenar_descripcion_garantia();

				if (!string.IsNullOrEmpty(this.AdvertenciaGeneral))
				{
					MessageBox.Show(this.AdvertenciaGeneral, "I a Ficha de Afiliación!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					this.skipMethod();
				}
			}
			else
			{
				this.LimpiarCamposAval2();
			}
		}

		private void LimpiarCamposAval2()
		{
			txtPriNombre_tpAval2.Text = "";
			txtSegNombre_tpAval2.Text = "";
			txtPriApellido_tpAval2.Text = "";
			txtSegApellido_tpAval2.Text = "";
			txtDirecc_res_tpAval2.Text = "";
			txtTelefono_tpAval2.Text = "";
			txtCelular_tpAval2.Text = "";
			txtCorreo_tpAval2.Text = "";
			//
			//Conyuge
			this.txtNombre_conyuge_tpAval2.Text = string.Empty;
			this.txtCorreolaboral_tpAval2.Text = string.Empty;
			this.txtDirecclaboral_tpAval2.Text = string.Empty;
			this.txtTelefono_tpAval2.Text = string.Empty;
			//
			txtPatrono_tpAval2.Text = "";
			txtCargo_tpAval2.Text = "";
			txtAntiglaboral_tpAval2.Text = "";
			txtDirecclaboral_tpAval2.Text = "";
			txtTellaboral1_tpAval2.Text = "";
			txtTellaboral2_tpAval2.Text = "";
			txtCorreolaboral_tpAval2.Text = "";
			//
			txtRef1_tpAval2.Text = "";
			txtRef1_direc_tpAval2.Text = "";
			txtRef1_telef_tpAval2.Text = "";
			txtRef2_tpAval2.Text = "";
			txtRef2_direc_tpAval2.Text = "";
			txtRef2_telef_tpAval2.Text = "";
		}

		private void p_llenar_combo_destinos()
		{
			try
			{
				DataTable dtDestinos = da.ObtenerDestinoCredito();
				cmbDestino_credito.DataSource = dtDestinos;
				cmbDestino_credito.DisplayMember = "descripcion_destino";
				cmbDestino_credito.ValueMember = "destino_id";
				//cmbDestino_credito_SelectionChangeCommitted(null, null);

			}
			catch (Exception ex)
			{
				MessageBox.Show("Error en llenar combo_decisiones : " + ex.Message);
			}
		}
		private void p_llenar_combo_sub_aplicaciones()
		{
			string cod_fondo_mg = da.ObtenerConversionFondoMG(txtCodigo_fuente_fondos.Text);
			comboBox_sub_aplicacion.DataSource = null;
			try
			{
				var dt = da.ObtenerSubAplicaciones("MCR", cod_fondo_mg);
				if (dt.Rows.Count > 0)
				{
					comboBox_sub_aplicacion.DataSource = dt;
					comboBox_sub_aplicacion.DisplayMember = "desc_sub_aplicacion";
					comboBox_sub_aplicacion.ValueMember = "codigo_sub_aplicacion";
					comboBox_sub_aplicacion_SelectionChangeCommitted(null, null);

				}
				else
				{
					txtCodigo_sub_aplicacion.Text = string.Empty;
					txtCodigo_moneda.Text = string.Empty;
					txtDesc_moneda.Text = string.Empty;
				}


			}
			catch (Exception ex)
			{
				MessageBox.Show("Error en llenar combo_decisiones : " + ex.Message);
			}
		}
		private void p_llenar_fuente_fondos()
		{
			try
			{
				var dt = da.ObtenerFuentesFondos();
				cmbFondos.DataSource = dt;
				cmbFondos.DisplayMember = "descripcion_fuente";
				cmbFondos.ValueMember = "codigo_fuente";
				cmbFondos_SelectionChangeCommitted(null, null);

			}
			catch (Exception ex)
			{
				MessageBox.Show("Error en llenar combo_decisiones : " + ex.Message);
			}

		}
		private void p_actualizar_info_financiera()
		{
			estado_burointerno_principal = da.ObtenerBuroInterno_xId(txtIDSolicitante.Text, out estado_desc_burointerno_principal, out observa_burointerno_principal).ToString();
			estado_burointerno_conyuge = da.ObtenerBuroInterno_xId(txtId_conyuge.Text, out estado_desc_burointerno_conyuge, out observa_burointerno_conyuge).ToString();
			estado_burointerno_codeudor = da.ObtenerBuroInterno_xId(txtId_codeudor.Text, out estado_desc_burointerno_codeudor, out observa_burointerno_codeudor).ToString();
			estado_burointerno_aval1 = da.ObtenerBuroInterno_xId(txtId_aval1.Text, out estado_desc_burointerno_aval1, out observa_burointerno_aval1).ToString();
			estado_burointerno_aval2 = da.ObtenerBuroInterno_xId(txtId_aval2.Text, out estado_desc_burointerno_aval2, out observa_burointerno_aval2).ToString();

			DataRow row = dtInfoFinan.Select("rol='Solicitante'").FirstOrDefault();
			if (row == null)
			{
				dtInfoFinan.Rows.Add(txtIDSolicitante.Text, txtCodigo_cliente.Text, txtNombre.Text + " " + txtPrimer_apellido.Text + " " + txtSegundo_apellido.Text, "Solicitante", saldo_aportaciones_principal.ToString(),
									 txtIngresos.Text, txtOtros_ingresos.Text, txtDeducciones.Text, "", estado_desc_burointerno_principal, ListToString(observa_burointerno_principal));
			}
			else
			{
				row["numero_identificacion"] = txtIDSolicitante.Text;
				row["codigo_cliente"] = txtCodigo_cliente.Text;
				row["nombre"] = txtNombre.Text + " " + txtPrimer_apellido.Text + " " + txtSegundo_apellido.Text;
				row["ingresos"] = txtIngresos.Text;
				row["otros_ingresos"] = txtOtros_ingresos.Text;
				row["deducciones"] = txtDeducciones.Text;
				row["aportaciones"] = saldo_aportaciones_principal.ToString();
				row["estado_buro_interno"] = estado_desc_burointerno_principal;
				row["observaciones_buro"] = ListToString(observa_burointerno_principal);
			}

			if (!string.IsNullOrEmpty(txtId_conyuge.Text))
			{
				row = dtInfoFinan.Select("rol='Conyuge'").FirstOrDefault();
				if (row == null)
				{
					dtInfoFinan.Rows.Add(txtId_conyuge.Text, txtCodigo_cliente_TpConyuge.Text, txtPriNombre_tpConyuge.Text + " " + txtSegNombre_tpConyuge.Text + "  " + txtPriApellido_tpConyuge.Text + " " + txtSegApellido_tpConyuge.Text, "Conyuge", saldo_aportaciones_conyuge.ToString(),
										 txtIngresos_tpConyuge.Text, txtOtrosIngresos_tpConyuge.Text, txtDeducciones_tpConyuge.Text, "", estado_desc_burointerno_conyuge, ListToString(observa_burointerno_conyuge));
				}
				else
				{
					row["numero_identificacion"] = txtId_conyuge.Text;
					row["codigo_cliente"] = txtCodigo_cliente_TpConyuge.Text;
					row["nombre"] = txtPriNombre_tpConyuge.Text + " " + txtSegNombre_tpConyuge.Text + "  " + txtPriApellido_tpConyuge.Text + " " + txtSegApellido_tpConyuge.Text;
					row["ingresos"] = txtIngresos_tpConyuge.Text;
					row["otros_ingresos"] = txtOtrosIngresos_tpConyuge.Text;
					row["deducciones"] = txtDeducciones_tpConyuge.Text;
					row["aportaciones"] = saldo_aportaciones_conyuge.ToString();
					row["estado_buro_interno"] = estado_desc_burointerno_conyuge;
					row["observaciones_buro"] = ListToString(observa_burointerno_conyuge);

				}
			}
			if (!string.IsNullOrEmpty(txtId_codeudor.Text))
			{
				row = dtInfoFinan.Select("rol='Codeudor'").FirstOrDefault();
				if (row == null)
				{
					dtInfoFinan.Rows.Add(txtId_codeudor.Text, txtCodigo_cliente_TpCodeudor.Text, txtPriNombre_tpCodeudor.Text + " " + txtSegNombre_tpCodeudor.Text + "  " + txtPriApellido_tpCodeudor.Text + " " + txtSegApellido_tpCodeudor.Text, "Codeudor", saldo_aportaciones_codeudor.ToString(),
										 txtIngresos_tpCodeudor.Text, txtOtrosIngresos_tpCodeudor.Text, txtDeducciones_tpCodeudor.Text, "", estado_desc_burointerno_codeudor, ListToString(observa_burointerno_codeudor));
				}
				else
				{
					row["numero_identificacion"] = txtId_codeudor.Text;
					row["codigo_cliente"] = txtCodigo_cliente_TpCodeudor.Text;
					row["nombre"] = txtPriNombre_tpCodeudor.Text + " " + txtSegNombre_tpCodeudor.Text + "  " + txtPriApellido_tpCodeudor.Text + " " + txtSegApellido_tpCodeudor.Text;
					row["ingresos"] = txtIngresos_tpCodeudor.Text;
					row["otros_ingresos"] = txtOtrosIngresos_tpCodeudor.Text;
					row["deducciones"] = txtDeducciones_tpCodeudor.Text;
					row["aportaciones"] = saldo_aportaciones_codeudor.ToString();
					row["estado_buro_interno"] = estado_desc_burointerno_codeudor;
					row["observaciones_buro"] = ListToString(observa_burointerno_aval1);
				}
			}
			if (!string.IsNullOrEmpty(txtId_aval1.Text))
			{


				row = dtInfoFinan.Select("rol='Aval1'").FirstOrDefault();
				if (row == null)
				{
					dtInfoFinan.Rows.Add(txtId_aval1.Text, txtCodigo_cliente_TpAval1.Text, txtPriNombre_tpAval1.Text + " " + txtSegNombre_tpAval1.Text + "  " + txtPriApellido_tpAval1.Text + " " + txtSegApellido_tpAval1.Text, "Aval1", saldo_aportaciones_aval1.ToString(),
										 txtIngresos_tpAval1.Text, txtOtrosIngresos_tpAval1.Text, txtDeducciones_tpAval1.Text, "", estado_desc_burointerno_aval1, ListToString(observa_burointerno_aval1));
				}
				else
				{
					row["numero_identificacion"] = txtId_aval1.Text;
					row["codigo_cliente"] = txtCodigo_cliente_TpAval1.Text;
					row["nombre"] = txtPriNombre_tpAval1.Text + " " + txtSegNombre_tpAval1.Text + "  " + txtPriApellido_tpAval1.Text + " " + txtSegApellido_tpAval1.Text;
					row["ingresos"] = txtIngresos_tpAval1.Text;
					row["otros_ingresos"] = txtOtrosIngresos_tpAval1.Text;
					row["deducciones"] = txtDeducciones_tpAval1.Text;
					row["aportaciones"] = saldo_aportaciones_aval1.ToString();
					row["estado_buro_interno"] = estado_desc_burointerno_aval1;
					row["observaciones_buro"] = ListToString(observa_burointerno_aval1);
				}
			}
			if (!string.IsNullOrEmpty(txtId_aval2.Text))
			{
				row = dtInfoFinan.Select("rol='Aval2'").FirstOrDefault();
				if (row == null)
				{
					dtInfoFinan.Rows.Add(txtId_aval2.Text, txtCodigo_cliente_TpAval1.Text, txtPriNombre_tpAval2.Text + " " + txtSegNombre_tpAval2.Text + "  " + txtPriApellido_tpAval2.Text + " " + txtSegApellido_tpAval2.Text, "Aval2", saldo_aportaciones_aval1.ToString(),
										 txtIngresos_tpAval2.Text, txtOtrosIngresos_tpAval2.Text, txtDeducciones_tpAval2.Text, "", estado_desc_burointerno_aval2, ListToString(observa_burointerno_aval2));
				}
				else
				{
					row["numero_identificacion"] = txtId_aval2.Text;
					row["codigo_cliente"] = txtCodigo_cliente_TpAval2.Text;
					row["nombre"] = txtPriNombre_tpAval2.Text + " " + txtSegNombre_tpAval2.Text + "  " + txtPriApellido_tpAval2.Text + " " + txtSegApellido_tpAval2.Text;
					row["ingresos"] = txtIngresos_tpAval2.Text;
					row["otros_ingresos"] = txtOtrosIngresos_tpAval2.Text;
					row["deducciones"] = txtDeducciones_tpAval2.Text;
					row["aportaciones"] = saldo_aportaciones_aval2.ToString();
					row["estado_buro_interno"] = estado_desc_burointerno_aval2;
					row["observaciones_buro"] = ListToString(observa_burointerno_aval2);
				}
			}
			gvInfoFinanciera.Refresh();

			p_calcular_indice_concentracion_deuda();




		}
		private string p_construir_xml(string p_tipo)
		{
			string xml = "";
			string xml_solicitante = "";
			string xml_codeudor = "";
			string xml_aval1 = "";
			string xml_aval2 = "";

			//Solicitante
			System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-HN");
			System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-HN");

			double dingresos_brutos = 0;
			double.TryParse(txtIngresos.Text, out dingresos_brutos);
			double dotros_ingresos = 0;
			double.TryParse(txtOtros_ingresos.Text, out dotros_ingresos);
			double ddeducciones = 0;
			double.TryParse(txtDeducciones.Text, out ddeducciones);
			double ddeudascanceladas_sol = 0;
			double.TryParse(txtDeudasCancelados_solic.Text, out ddeudascanceladas_sol);

			//Codeudor
			double dingresos_brutos_codeudor = 0;
			double.TryParse(txtIngresos_tpCodeudor.Text, out dingresos_brutos_codeudor);
			double dotros_ingresos_codeudor = 0;
			double.TryParse(txtOtrosIngresos_tpCodeudor.Text, out dotros_ingresos_codeudor);
			double ddeducciones_codeudor = 0;
			double.TryParse(txtDeducciones_tpCodeudor.Text, out ddeducciones_codeudor);
			double ddeudascanceladas_codeud = 0;
			double.TryParse(txtDeudasCancelados_codeud.Text, out ddeudascanceladas_codeud);

			//Aval1
			double dingresos_brutos_Aval1 = 0;
			double.TryParse(txtIngresos_tpAval1.Text, out dingresos_brutos_Aval1);
			double dotros_ingresos_Aval1 = 0;
			double.TryParse(txtOtrosIngresos_tpAval1.Text, out dotros_ingresos_Aval1);
			double ddeducciones_Aval1 = 0;
			double.TryParse(txtDeducciones_tpAval1.Text, out ddeducciones_Aval1);
			double ddeudascanceladas_aval1 = 0;
			double.TryParse(txtDeudasCancelados_aval1.Text, out ddeudascanceladas_aval1);

			//Aval2
			double dingresos_brutos_Aval2 = 0;
			double.TryParse(txtIngresos_tpAval2.Text, out dingresos_brutos_Aval2);
			double dotros_ingresos_Aval2 = 0;
			double.TryParse(txtOtrosIngresos_tpAval2.Text, out dotros_ingresos_Aval2);
			double ddeducciones_Aval2 = 0;
			double.TryParse(txtDeducciones_tpAval2.Text, out ddeducciones_Aval2);
			double ddeudascanceladas_aval2 = 0;
			double.TryParse(txtDeudasCancelados_aval2.Text, out ddeudascanceladas_aval2);


			string singresos_brutos = dingresos_brutos.ToString();
			string sotros_ingresos = dotros_ingresos.ToString();
			string sdeducciones = ddeducciones.ToString();
			string sdeudas_cancelados_sol = ddeudascanceladas_sol.ToString();


			string singresos_brutos_Codeudor = dingresos_brutos_codeudor.ToString();
			string sotros_ingresos_Codeudor = dotros_ingresos_codeudor.ToString();
			string sdeducciones_Codeudor = ddeducciones_codeudor.ToString();
			string sdeudas_cancelados_codeud = ddeudascanceladas_codeud.ToString();

			string singresos_brutos_Aval1 = dingresos_brutos_Aval1.ToString();
			string sotros_ingresos_Aval1 = dotros_ingresos_Aval1.ToString();
			string sdeducciones_Aval1 = ddeducciones_Aval1.ToString();
			string sdeudas_canceladas_aval1 = ddeudascanceladas_aval1.ToString();

			string singresos_brutos_Aval2 = dingresos_brutos_Aval2.ToString();
			string sotros_ingresos_Aval2 = dotros_ingresos_Aval2.ToString();
			string sdeducciones_Aval2 = ddeducciones_Aval2.ToString();
			string sdeudas_canceladas_aval2 = ddeudascanceladas_aval2.ToString();

			string antiguedad_cooperativo_solic = da.ObtenerAntiguedadCooperativa(txtIDSolicitante.Text);
			string antiguedad_cooperativo_codeudor = da.ObtenerAntiguedadCooperativa(txtId_codeudor.Text);
			string antiguedad_cooperativo_aval1 = da.ObtenerAntiguedadCooperativa(txtId_aval1.Text);
			string antiguedad_cooperativo_aval2 = da.ObtenerAntiguedadCooperativa(txtId_aval2.Text);


			string condicion_veh = "";
			if (rbAutoNuevo.Checked)
				condicion_veh = "NUEVO";
			else
				condicion_veh = "USADO";

			string is_consolidacion = "False";
			if (cbConsolidacion.Checked)
			{
				is_consolidacion = "True";
			}

			double monto = 0;
			double.TryParse(txtMonto_solicitado.Text, out monto);

			string vl_monto_solicitado = string.Format(String.Format("{0:########0.00}", monto));

			string porcion_xml_recalculo = "";
			string porcion_no_solicitud_coopsafa = "";

			#region porcion_cuotas_al_vencimiento
			string porcion_cuotas_vencimiento = "";
			if (txtMonto_cuotas_vencimiento_sol.Text == "0.00")
			{
				porcion_cuotas_vencimiento = "<MontoCuotaVencimiento/>";
			}
			else
			{

				porcion_cuotas_vencimiento = "<MontoCuotaVencimiento>" + float.Parse(txtMonto_cuotas_vencimiento_sol.Text).ToString("##########0.00") + "</MontoCuotaVencimiento>";
			}
			#endregion

			#region porcion_xml_vehiculos
			string porcion_xml_vehiculos = "";
			if (prestamo_vehiculos)
			{
				double valor_veh = 0;
				double.TryParse(txtValor_vehiculo.Text, out valor_veh);
				string vl_valor_veh = string.Format(String.Format("{0:########0.00}", valor_veh));
				porcion_xml_vehiculos = "<ValorVehiculo>" + vl_valor_veh + @"</ValorVehiculo>
                                     <CondicionVehiculo>" + condicion_veh + @"</CondicionVehiculo>";
			}
			else
			{
				porcion_xml_vehiculos = @"<ValorVehiculo/>
                                          <CondicionVehiculo/>";
			}
			#endregion

			#region porcion_xml_consolidacion
			string porcion_xml_consolidacion = "";
			if (is_consolidacion == "True")
			{
				string stotal_cuotas_consolidar = "0";
				string stotal_capital_consolidar = "0";
				try
				{
					stotal_cuotas_consolidar = string.Format(String.Format("{0:########0.00}", double.Parse(txtTotal_cuotas_consolidar.Text)));
					stotal_capital_consolidar = string.Format(String.Format("{0:########0.00}", double.Parse(txtTotal_capital_consolidar.Text)));
				}
				catch
				{

				}
				porcion_xml_consolidacion = @"<CuotaConsolidar>" + stotal_cuotas_consolidar + @"</CuotaConsolidar>
                                              <BalanceConsolidar>" + stotal_capital_consolidar + @"</BalanceConsolidar>";
			}
			else
			{
				porcion_xml_consolidacion = @"<CuotaConsolidar/>
                                              <BalanceConsolidar/>";
			}
			#endregion

			#region multi
			string vl_derecho_ganado = "0";
			if (txtDerechoGanado.Text == "0.00")
			{
				vl_derecho_ganado = "0";
			}
			else
			{
				vl_derecho_ganado = float.Parse(txtDerechoGanado.Text).ToString("##########0.00");
			}
			float complemento_aportaciones = 0;
			try
			{
				System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-HN");
				System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-HN");
				complemento_aportaciones = float.Parse(txtComplemento_aportaciones.Text);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			float saldo_aportaciones_con_complemento = saldo_aportaciones_principal + complemento_aportaciones;

			string antiguedad_labora_principal = txtAntiguedad_laboral.Text.Trim();
			if (string.IsNullOrEmpty(antiguedad_labora_principal))
			{
				antiguedad_labora_principal = "0";
			}

			xml_solicitante = @"<Multi>
                                     <Cedula>" + txtIDSolicitante.Text + @"</Cedula>
                                     <Rol>PRINCIPAL</Rol>
                                     <IngresoBruto>" + singresos_brutos + @"</IngresoBruto>
                                     <Deducciones>" + sdeducciones + @"</Deducciones>
                                     <OtrosIngresos>" + sotros_ingresos + @"</OtrosIngresos>
                                     <TelefonoMovil/>
                                     <EstadoInternoAplicante>" + estado_burointerno_principal + @"</EstadoInternoAplicante>
                                     <SaldoAportaciones>" + saldo_aportaciones_con_complemento.ToString() + @"</SaldoAportaciones>
                                     <AntiguedadCooperativista>" + antiguedad_cooperativo_solic + @"</AntiguedadCooperativista>
                                     <AntiguedadLaboral>" + antiguedad_labora_principal + @"</AntiguedadLaboral>
                                     <DerechoGanado>" + vl_derecho_ganado + @"</DerechoGanado>
                                     <TieneCuotaCancelada>" + cbTieneDeudascanc_solic.Checked.ToString() + @"</TieneCuotaCancelada>
                                     <CuotaaCancelar>" + sdeudas_cancelados_sol + @"</CuotaaCancelar>
                                  </Multi>";

			if (cbCodeudor.Checked)
			{
				string antiguedad_labora_Codeudor = txtAntiglaboral_tpCodeudor.Text;
				if (string.IsNullOrEmpty(antiguedad_labora_Codeudor))
				{
					antiguedad_labora_Codeudor = "0";
				}
				xml_codeudor = @"
                                     <Multi>
                                     <Cedula>" + txtId_codeudor.Text + @"</Cedula>
                                     <Rol>CODEUDOR</Rol>
                                     <IngresoBruto>" + singresos_brutos_Codeudor + @"</IngresoBruto>
                                     <Deducciones>" + sdeducciones_Codeudor + @"</Deducciones>
                                     <OtrosIngresos>" + sotros_ingresos_Codeudor + @"</OtrosIngresos>
                                     <TelefonoMovil/>
                                     <EstadoInternoAplicante>" + estado_burointerno_codeudor + @"</EstadoInternoAplicante>
                                     <SaldoAportaciones>" + saldo_aportaciones_codeudor + @"</SaldoAportaciones>
                                     <AntiguedadCooperativista>" + antiguedad_cooperativo_codeudor + @"</AntiguedadCooperativista>
                                     <AntiguedadLaboral>" + antiguedad_labora_Codeudor + @"</AntiguedadLaboral>
                                     <DerechoGanado>0</DerechoGanado>
                                     <TieneCuotaCancelada>" + cbTieneDeudascanc_codeud.Checked.ToString() + @"</TieneCuotaCancelada>
                                     <CuotaaCancelar>" + sdeudas_cancelados_codeud + @"</CuotaaCancelar>
                                  </Multi>";
			}

			if (cbAval1.Checked)
			{
				string antiguedad_laboral_aval1 = txtAntiglaboral_tpAval1.Text;
				if (string.IsNullOrEmpty(antiguedad_laboral_aval1))
				{
					antiguedad_laboral_aval1 = "0";
				}
				xml_aval1 = @"
                                     <Multi>
                                     <Cedula>" + txtId_aval1.Text + @"</Cedula>
                                     <Rol>GARANTE</Rol>
                                     <IngresoBruto>" + singresos_brutos_Aval1 + @"</IngresoBruto>
                                     <Deducciones>" + sdeducciones_Aval1 + @"</Deducciones>
                                     <OtrosIngresos>" + sotros_ingresos_Aval1 + @"</OtrosIngresos>
                                     <TelefonoMovil/>
                                     <EstadoInternoAplicante>" + estado_burointerno_aval1 + @"</EstadoInternoAplicante>
                                     <SaldoAportaciones>" + saldo_aportaciones_aval1 + @"</SaldoAportaciones>
                                     <AntiguedadCooperativista>" + antiguedad_cooperativo_aval1 + @"</AntiguedadCooperativista>
                                     <AntiguedadLaboral>" + antiguedad_laboral_aval1 + @"</AntiguedadLaboral>
                                     <DerechoGanado>0</DerechoGanado>
                                     <TieneCuotaCancelada>" + cbTieneDeudascanc_aval1.Checked.ToString() + @"</TieneCuotaCancelada>
                                     <CuotaaCancelar>" + sdeudas_canceladas_aval1 + @"</CuotaaCancelar>
                                  </Multi>";
			}
			if (cbAval2.Checked)
			{
				string antiguedad_laboral_aval2 = txtAntiglaboral_tpAval2.Text;
				if (string.IsNullOrEmpty(antiguedad_laboral_aval2))
				{
					antiguedad_laboral_aval2 = "0";
				}
				xml_aval2 = @"
                                     <Multi>
                                     <Cedula>" + txtId_aval2.Text + @"</Cedula>
                                     <Rol>GARANTE</Rol>
                                     <IngresoBruto>" + singresos_brutos_Aval2 + @"</IngresoBruto>
                                     <Deducciones>" + sdeducciones_Aval2 + @"</Deducciones>
                                     <OtrosIngresos>" + sotros_ingresos_Aval2 + @"</OtrosIngresos>
                                     <TelefonoMovil/>
                                     <EstadoInternoAplicante>" + estado_burointerno_aval2 + @"</EstadoInternoAplicante>
                                     <SaldoAportaciones>" + saldo_aportaciones_aval2 + @"</SaldoAportaciones>
                                     <AntiguedadCooperativista>" + antiguedad_cooperativo_aval2 + @"</AntiguedadCooperativista>
                                     <AntiguedadLaboral>" + antiguedad_laboral_aval2 + @"</AntiguedadLaboral>
                                     <DerechoGanado>0</DerechoGanado>
                                     <TieneCuotaCancelada>" + cbTieneDeudascanc_aval2.Checked.ToString() + @"</TieneCuotaCancelada>
                                     <CuotaaCancelar>" + sdeudas_canceladas_aval2 + @"</CuotaaCancelar>
                                  </Multi>";
			}
			#endregion

			if (p_tipo == "CREARSOLICITUD")
			{
				porcion_no_solicitud_coopsafa = "<IDSolicitudCOOPSAFA>" + txtNo_solicitud_coopsafa.Text.Trim() + "</IDSolicitudCOOPSAFA>";
			}
			if (p_tipo == "RECALCULARSOLICITUD")
			{
				//En el recalculo no se necesita el numero de solicitud de coopsafa, solo en la creacion
				porcion_no_solicitud_coopsafa = "";
				porcion_xml_recalculo = "<ApplicationId>" + txtApplicationID.Text.Trim() + @"</ApplicationId>";
			}
			xml = @"<Request>
                       <Authentication>
                           <UserId>" + usuario_wstransunion + @"</UserId>
                           <Password>" + pass_wstransunion + @"</Password>
                        </Authentication>" +
						porcion_xml_recalculo + @"
                        <DCRequest>
                           <Input>
                              <Single>
                                  <UsuarioWorkflow>" + labelUsuario.Text + @"</UsuarioWorkflow>
                                  <Sucursal>" + labelFilial.Text + @"</Sucursal>
                                  <MontoSolicitado>" + vl_monto_solicitado + @"</MontoSolicitado>
                                  <Plazo>" + txtPlazo.Text + @"</Plazo>
                                  <Tasa>" + txtTasa.Text + @"</Tasa>
                                  <CodigoProducto>" + txtCodigo_sub_aplicacion.Text + @"</CodigoProducto>
                                  <FormaPago>" + txtVentanilla_planilla.Text + @"</FormaPago>
                                  <DestinoCredito>" + txtDestino_credito.Text + @"</DestinoCredito>
                                  <Consolidacion>" + is_consolidacion + @"</Consolidacion>" +
								  porcion_xml_consolidacion +
								  porcion_cuotas_vencimiento + @"
                                  <TestMode>False</TestMode>" +
								  porcion_xml_vehiculos +
								  porcion_no_solicitud_coopsafa + @"   
                              </Single>" +
							  xml_solicitante +
							  xml_codeudor +
							  xml_aval1 +
							  xml_aval2 + @"                                                                    
                           </Input>
                        </DCRequest>
                    </Request>";
			return xml;
		}

		//FELVIR01
		private string p_construir_xml_referencias()
		{

			StringBuilder xml = new StringBuilder();
			xml.Append(@"<ReferenciasSol><Referencia>");
			#region Referencias Solicitante
			if (!string.IsNullOrEmpty(txtRef1.Text) && !string.IsNullOrEmpty(txtRef1_direc.Text))
			{
				string ref1 = @"<DatosReferencia>
                                  <ReferenciasDe>" + txtIDSolicitante.Text + @"</ReferenciasDe>
                                           <Rol>Principal</Rol>
                                 <no_referencia>1</no_referencia>
                                        <Nombre>" + txtRef1.Text + @"</Nombre>
                                      <DirecRes>" + txtRef1_direc.Text + @"</DirecRes>
                                       <TelFijo>" + txtRef1_telef.Text + @"</TelFijo>
                                    <Telcelular>" + txtRef1_casacolor.Text + @"</Telcelular>
                                 <Ptoreferencia>" + txtRef1_ptoref.Text + @" </Ptoreferencia>
                             </DatosReferencia>";
				xml.Append(ref1);
			}
			if (!string.IsNullOrEmpty(txtRef2.Text) && !string.IsNullOrEmpty(txtRef2_direc.Text))
			{
				string ref2 = @"<DatosReferencia>
                                  <ReferenciasDe>" + txtIDSolicitante.Text + @"</ReferenciasDe>
                                           <Rol>Principal</Rol>
                                 <no_referencia>2</no_referencia>
                                        <Nombre>" + txtRef2.Text + @"</Nombre>
                                      <DirecRes>" + txtRef2_direc.Text + @"</DirecRes>
                                       <TelFijo>" + txtRef2_telef.Text + @"</TelFijo>
                                    <Telcelular>" + txtRef2_casacolor.Text + @"</Telcelular>
                                 <Ptoreferencia>" + txtRef2_ptoref.Text + @" </Ptoreferencia>
                             </DatosReferencia>";
				xml.Append(ref2);
			}
			if (!string.IsNullOrEmpty(txtRef3.Text) && !string.IsNullOrEmpty(txtRef3_direc.Text))
			{
				string ref3 = @"<DatosReferencia>
                                  <ReferenciasDe>" + txtIDSolicitante.Text + @"</ReferenciasDe>
                                           <Rol>Principal</Rol>
                                 <no_referencia>3</no_referencia>
                                        <Nombre>" + txtRef3.Text + @"</Nombre>
                                      <DirecRes>" + txtRef3_direc.Text + @"</DirecRes>
                                       <TelFijo>" + txtRef3_telef.Text + @"</TelFijo>
                                    <Telcelular>" + this.txtRef3_casacolor.Text + @"</Telcelular>
                                 <Ptoreferencia>" + this.txtRef3_ptoref.Text + @" </Ptoreferencia>
                             </DatosReferencia>";
				xml.Append(ref3);
			}
			#endregion
			#region Referencias Codeudor
			if (!string.IsNullOrEmpty(txtRef1_tpCodeudor.Text) && !string.IsNullOrEmpty(txtRef1_direc_tpCodeudor.Text))
			{
				string ref1 = @"<DatosReferencia>
                                  <ReferenciasDe>" + txtId_codeudor.Text + @"</ReferenciasDe>
                                           <Rol>Codeudor</Rol>
                                 <no_referencia>1</no_referencia>
                                        <Nombre>" + txtRef1_tpCodeudor.Text + @"</Nombre>
                                      <DirecRes>" + txtRef1_direc_tpCodeudor.Text + @"</DirecRes>
                                       <TelFijo>" + txtRef1_telef_tpCodeudor.Text + @"</TelFijo>
                                    <Telcelular>" + txtRef1_casacolor_tpCodeudor.Text + @"</Telcelular>
                                 <Ptoreferencia>" + txtRef1_ptoref_tpCodeudor.Text + @" </Ptoreferencia>
                             </DatosReferencia>";
				xml.Append(ref1);
			}
			if (!string.IsNullOrEmpty(txtRef2_tpCodeudor.Text) && !string.IsNullOrEmpty(txtRef2_direc_tpCodeudor.Text))
			{
				string ref2 = @"<DatosReferencia>
                                  <ReferenciasDe>" + txtId_codeudor.Text + @"</ReferenciasDe>
                                           <Rol>Codeudor</Rol>
                                 <no_referencia>2</no_referencia>
                                        <Nombre>" + txtRef2_tpCodeudor.Text + @"</Nombre>
                                      <DirecRes>" + txtRef2_direc_tpCodeudor.Text + @"</DirecRes>
                                       <TelFijo>" + txtRef2_telef_tpCodeudor.Text + @"</TelFijo>
                                    <Telcelular>" + txtRef2_casacolor_tpCodeudor.Text + @"</Telcelular>
                                 <Ptoreferencia>" + txtRef2_ptoref_tpCodeudor.Text + @" </Ptoreferencia>
                             </DatosReferencia>";
				xml.Append(ref2);
			}
			if (!string.IsNullOrEmpty(txtRef3_tpCodeudor.Text) && !string.IsNullOrEmpty(txtRef3_direc_tpCodeudor.Text))
			{
				string ref3 = @"<DatosReferencia>
                                  <ReferenciasDe>" + txtId_codeudor.Text + @"</ReferenciasDe>
                                           <Rol>Codeudor</Rol>
                                 <no_referencia>3</no_referencia>
                                        <Nombre>" + txtRef3_tpCodeudor.Text + @"</Nombre>
                                      <DirecRes>" + txtRef3_direc_tpCodeudor.Text + @"</DirecRes>
                                       <TelFijo>" + txtRef3_telef_tpCodeudor.Text + @"</TelFijo>
                                    <Telcelular>" + this.txtRef3_casacolor_tpCodeudor.Text + @"</Telcelular>
                                 <Ptoreferencia>" + this.txtRef3_ptoref_tpCodeudor.Text + @" </Ptoreferencia>
                             </DatosReferencia>";
				xml.Append(ref3);
			}
			#endregion
			#region Referencias Aval1
			if (!string.IsNullOrEmpty(txtRef1_tpAval1.Text) && !string.IsNullOrEmpty(txtRef1_direc_tpAval1.Text))
			{
				string ref1 = @"<DatosReferencia>
                                  <ReferenciasDe>" + txtId_aval1.Text + @"</ReferenciasDe>
                                           <Rol>Aval1</Rol>
                                 <no_referencia>1</no_referencia>
                                        <Nombre>" + txtRef1_tpAval1.Text + @"</Nombre>
                                      <DirecRes>" + txtRef1_direc_tpAval1.Text + @"</DirecRes>
                                       <TelFijo>" + txtRef1_telef_tpAval1.Text + @"</TelFijo>
                                    <Telcelular>" + txtRef1_casacolor_tpAval1.Text + @"</Telcelular>
                                 <Ptoreferencia>" + txtRef1_ptoref_tpAval1.Text + @" </Ptoreferencia>
                             </DatosReferencia>";
				xml.Append(ref1);
			}
			if (!string.IsNullOrEmpty(txtRef2_tpAval1.Text) && !string.IsNullOrEmpty(txtRef2_direc_tpAval1.Text))
			{
				string ref2 = @"<DatosReferencia>
                                  <ReferenciasDe>" + txtId_aval1.Text + @"</ReferenciasDe>
                                           <Rol>Aval1</Rol>
                                 <no_referencia>2</no_referencia>
                                        <Nombre>" + txtRef2_tpAval1.Text + @"</Nombre>
                                      <DirecRes>" + txtRef2_direc_tpAval1.Text + @"</DirecRes>
                                       <TelFijo>" + txtRef2_telef_tpAval1.Text + @"</TelFijo>
                                    <Telcelular>" + txtRef2_casacolor_tpAval1.Text + @"</Telcelular>
                                 <Ptoreferencia>" + txtRef2_ptoref_tpAval1.Text + @" </Ptoreferencia>
                             </DatosReferencia>";
				xml.Append(ref2);
			}
			if (!string.IsNullOrEmpty(txtRef3_tpAval1.Text) && !string.IsNullOrEmpty(txtRef3_direc_tpAval1.Text))
			{
				string ref3 = @"<DatosReferencia>
                                  <ReferenciasDe>" + txtId_aval1.Text + @"</ReferenciasDe>
                                           <Rol>Aval1</Rol>
                                 <no_referencia>3</no_referencia>
                                        <Nombre>" + txtRef3_tpAval1.Text + @"</Nombre>
                                      <DirecRes>" + txtRef3_direc_tpAval1.Text + @"</DirecRes>
                                       <TelFijo>" + txtRef3_telef_tpAval1.Text + @"</TelFijo>
                                    <Telcelular>" + this.txtRef3_casacolor_tpAval1.Text + @"</Telcelular>
                                 <Ptoreferencia>" + this.txtRef3_ptoref_tpAval1.Text + @" </Ptoreferencia>
                             </DatosReferencia>";
				xml.Append(ref3);
			}
			#endregion
			#region Referencias Aval2
			if (!string.IsNullOrEmpty(txtRef1_tpAval2.Text) && !string.IsNullOrEmpty(txtRef1_direc_tpAval2.Text))
			{
				string ref1 = @"<DatosReferencia>
                                  <ReferenciasDe>" + txtId_aval2.Text + @"</ReferenciasDe>
                                           <Rol>Aval2</Rol>
                                 <no_referencia>1</no_referencia>
                                        <Nombre>" + txtRef1_tpAval2.Text + @"</Nombre>
                                      <DirecRes>" + txtRef1_direc_tpAval2.Text + @"</DirecRes>
                                       <TelFijo>" + txtRef1_telef_tpAval2.Text + @"</TelFijo>
                                    <Telcelular>" + txtRef1_casacolor_tpAval2.Text + @"</Telcelular>
                                 <Ptoreferencia>" + txtRef1_ptoref_tpAval2.Text + @" </Ptoreferencia>
                             </DatosReferencia>";
				xml.Append(ref1);
			}
			if (!string.IsNullOrEmpty(txtRef2_tpAval2.Text) && !string.IsNullOrEmpty(txtRef2_direc_tpAval2.Text))
			{
				string ref2 = @"<DatosReferencia>
                                  <ReferenciasDe>" + txtId_aval2.Text + @"</ReferenciasDe>
                                           <Rol>Aval2</Rol>
                                 <no_referencia>2</no_referencia>
                                        <Nombre>" + txtRef2_tpAval2.Text + @"</Nombre>
                                      <DirecRes>" + txtRef2_direc_tpAval2.Text + @"</DirecRes>
                                       <TelFijo>" + txtRef2_telef_tpAval2.Text + @"</TelFijo>
                                    <Telcelular>" + txtRef2_casacolor_tpAval2.Text + @"</Telcelular>
                                 <Ptoreferencia>" + txtRef2_ptoref_tpAval2.Text + @" </Ptoreferencia>
                             </DatosReferencia>";
				xml.Append(ref2);
			}
			if (!string.IsNullOrEmpty(txtRef3_tpAval2.Text) && !string.IsNullOrEmpty(txtRef3_direc_tpAval2.Text))
			{
				string ref3 = @"<DatosReferencia>
                                  <ReferenciasDe>" + txtId_aval2.Text + @"</ReferenciasDe>
                                           <Rol>Aval2</Rol>
                                 <no_referencia>3</no_referencia>
                                        <Nombre>" + txtRef3_tpAval2.Text + @"</Nombre>
                                      <DirecRes>" + txtRef3_direc_tpAval2.Text + @"</DirecRes>
                                       <TelFijo>" + txtRef3_telef_tpAval2.Text + @"</TelFijo>
                                    <Telcelular>" + this.txtRef3_casacolor_tpAval2.Text + @"</Telcelular>
                                 <Ptoreferencia>" + this.txtRef3_ptoref_tpAval2.Text + @" </Ptoreferencia>
                             </DatosReferencia>";
				xml.Append(ref3);
			}
			#endregion
			xml.Append(@"</Referencia></ReferenciasSol>");
			StringReader theReader = new StringReader(xml.ToString());
			DataSet theDataSet = new DataSet();
			try
			{
				theDataSet.ReadXml(theReader);
			}
			catch (Exception ex)
			{
				throw;
			}
			return xml.ToString();
		}

		private void p_construir_xml_cuotasburo_selec()
		{
			StringBuilder tempo = new StringBuilder();
			tempo.Append("<tabla>");
			foreach (DataGridViewRow row in gvCuotasBuro.Rows)
			{

				string columna0 = row.Cells[0].Value.ToString();
				string columna1 = row.Cells[1].Value.ToString();
				string columna2 = row.Cells[2].Value.ToString();
				string columna3 = row.Cells[3].Value.ToString();
				tempo.Append("<Entidad>");
				tempo.Append("<sel>" + columna0.Trim() + "</sel>");
				tempo.Append("<Nombre>" + columna1.Trim() + "</Nombre>");
				tempo.Append("<Balance>" + columna2.Trim() + "</Balance>");
				tempo.Append("<Cuota>" + columna3.Trim() + "</Cuota>");
				tempo.Append("</Entidad>");
			}
			tempo.Append("</tabla>");
			gxml_cuotas_buro = tempo.ToString();
		}

		//Modificado
		private void p_obtener_solicitudxNo(Int32 p_no_solicitud)
		{
			try
			{
				var solicitud = da.ObtenerSolicitudCredito(p_no_solicitud);
				#region General

				txtCodigo_sub_aplicacion.Text = solicitud.codigo_sub_aplicacion.ToString();
				txtDesc_sub_aplicacion.Text = da.ObtenerDescripcionSubApplicacion(solicitud.codigo_sub_aplicacion);
				try
				{
					p_tooltips_desc_producto(txtDesc_sub_aplicacion);
				}
				catch
				{

				}
				comboBox_sub_aplicacion.Visible = false;
				cmbFondos.Enabled = false;
				txtDesc_sub_aplicacion.Visible = true;

				gdestino_credito_id = solicitud.destino_credito;
				gfuente_financiamiento = solicitud.fuente_financiamiento.ToString();
				txtCodigo_fuente_fondos.Text = gfuente_financiamiento;

				if (txtCodigo_sub_aplicacion.Text.Trim() == "10" || txtCodigo_sub_aplicacion.Text.Trim() == "23" || txtCodigo_sub_aplicacion.Text.Trim() == "32")
				{
					pnlCondicion_vehiculo.Visible = true;
					prestamo_vehiculos = true;
				}

				if (solicitud.es_consolidacion == "T")
				{
					cbConsolidacion.Checked = true;
				}
				else
				{
					cbConsolidacion.Checked = false;
				}

				//Obteniendo cuotas del buro guardadas
				gxml_cuotas_buro = solicitud.xml_cuotas_buro;
				try
				{
					DataTable dt = xml_to_datatable(gxml_cuotas_buro);
					dtCuotasBuro.Rows.Clear();
					gcant_cuotas_buro = dt.Rows.Count;
					for (int x = 0; x < dt.Rows.Count; x++)
					{
						dtCuotasBuro.Rows.Add(dt.Rows[x][0].ToString(),
											  dt.Rows[x][1].ToString(),
											  dt.Rows[x][2].ToString(),
											  dt.Rows[x][3].ToString());
					}
					gvCuotasBuro.DataSource = dtCuotasBuro;
				}
				catch (Exception ex)
				{
					// MessageBox.Show(ex.Message);
				}
				txtTotal_cuotas_consolidar.Text = solicitud.monto_cuota_consolidar.ToString("###,###,##0.00");
				txtTotal_capital_consolidar.Text = solicitud.monto_balance_consolidar.ToString("###,###,##0.00");
				txtDerechoGanado.Text = solicitud.derecho_ganado.ToString("###,###,##0.00");
				txtMonto_cuotas_vencimiento_sol.Text = solicitud.monto_cuotas_vencimiento.ToString("###,###,##0.00");
				txtComplemento_aportaciones.Text = solicitud.complemento_aportaciones.ToString("###,###,##0.00");
				txtDeudasCancelados_solic.Text = solicitud.deudas_canceladas_solic.ToString("###,###,##0.00");
				txtDeudasCancelados_codeud.Text = solicitud.deudas_canceladas_codeud.ToString("###,###,##0.00");
				txtDeudasCancelados_aval1.Text = solicitud.deudas_canceladas_aval1.ToString("###,###,##0.00");
				txtDeudasCancelados_aval2.Text = solicitud.deudas_canceladas_aval2.ToString("###,###,##0.00");
				if (solicitud.deudas_canceladas_solic > 0)
					cbTieneDeudascanc_solic.Checked = true;
				if (solicitud.deudas_canceladas_codeud > 0)
					cbTieneDeudascanc_codeud.Checked = true;
				if (solicitud.deudas_canceladas_aval1 > 0)
					cbTieneDeudascanc_aval1.Checked = true;
				if (solicitud.deudas_canceladas_aval2 > 0)
					cbTieneDeudascanc_aval2.Checked = true;


				if (txtCodigo_sub_aplicacion.Text.Trim() == "10" || txtCodigo_sub_aplicacion.Text.Trim() == "23" || txtCodigo_sub_aplicacion.Text.Trim() == "32")
				{
					pnlCondicion_vehiculo.Visible = true;
				}
				else
					pnlCondicion_vehiculo.Visible = false;

				if (solicitud.condicion_vehiculo == "N")
					rbAutoNuevo.Checked = true;

				if (solicitud.condicion_vehiculo == "U")
					rbAutoUsado.Checked = true;
				txtValor_vehiculo.Text = solicitud.valor_vehiculo.ToString();
				txtCodigo_moneda.Text = solicitud.codigo_moneda.ToString();
				string vl_codigo_moneda = "";
				string vl_descripcion_moneda = "";
				string vl_sigla_moneda = "";
				da.ObtenerMonedaxSubAplicacion(txtCodigo_sub_aplicacion.Text, out vl_codigo_moneda, out vl_descripcion_moneda, out vl_sigla_moneda);
				txtCodigo_moneda.Text = vl_codigo_moneda;
				txtDesc_moneda.Text = vl_descripcion_moneda;
				txtMoneda_abreviatura.Text = vl_sigla_moneda;

				if (solicitud.requiere_codeudor == "S")
					cbCodeudor.Checked = true;
				else
					cbCodeudor.Checked = false;

				if (solicitud.requiere_aval1 == "S")
					cbAval1.Checked = true;
				else
					cbAval1.Checked = false;

				if (solicitud.requiere_aval2 == "S")
					cbAval2.Checked = true;
				else
					cbAval2.Checked = false;

				//FELVIR01 - 20190626
				if (solicitud.RequiereGarante.Equals("S"))
				{
					this.cbGarante.Checked = true;
					this.CargarGarante(p_no_solicitud);
				}
				else
				{
					this.cbGarante.Checked = false;
					this.BloquearGarante(false);
				}

				txtPlazo.Text = solicitud.plazo.ToString();
				txtTasa.Text = solicitud.tasa.ToString();
				txtMonto_solicitado.Text = solicitud.monto_solicitado.ToString();
				txtDestino_credito.Text = solicitud.destino_credito;
				txtDescripcion_garantia.Text = solicitud.descripcion_garantia.Trim();
				p_calcular_cuota_nivelada();
				gmodo_transunion = solicitud.modo_transunion;
				txtModo_transunion.Text = gmodo_transunion;
				txtApplicationID.Text = solicitud.application_id.ToString();
				txtFecha_creacion_tu.Text = solicitud.fecha_creacion_tu.ToString();

				#endregion

				#region solicitante

				txtCodigo_cliente.Text = solicitud.codigo_cliente.ToString();
				//Obteniendo Fotografia
				string vl_fecha_ultima_act = "";
				byte[] foto;
				da.ObtenerFotoAfiliado(txtCodigo_cliente.Text, out foto, out vl_fecha_ultima_act);
				if (foto != null)
				{
					pbFotoVigente.Image = DocSys.p_CopyDataToBitmap(foto);
				}
				txtIDSolicitante.Text = solicitud.no_identificacion;
				txtNombre.Text = solicitud.nombres;
				txtPrimer_apellido.Text = solicitud.primer_apellido;
				txtSegundo_apellido.Text = solicitud.segundo_apellido;
				txtApellido_casada.Text = solicitud.apellido_casada;
				txtSexo.Text = solicitud.sexo;
				txtEstado_civil.Text = solicitud.estado_civil;
				txtLugar_nacimiento.Text = solicitud.lugar_nacimiento;
				txtVentanilla_planilla.Text = solicitud.ventanilla_planilla;
				txtFecha_nacimiento.Text = solicitud.fecha_nacimiento.Value.ToString("d");
				txtNacionalidad.Text = solicitud.nacionalidad;
				string nivel_educativo = solicitud.nivel_educativo;
				switch (nivel_educativo)
				{
					case "P":
						rbNePrimaria.Checked = true;
						break;
					case "S":
						rbNeSecundaria.Checked = true;
						break;
					case "U":
						rbNeUniversitaria.Checked = true;
						break;
					case "G":
						rbNePostgrado.Checked = true;
						break;
					default:
						break;
				}
				txtProfesion_oficio.Text = solicitud.profesion_oficio;

				string tipo_vivienda = solicitud.tipo_vivienda;
				switch (tipo_vivienda)
				{
					case "PROPIA":
						rbTvPropia.Checked = true;
						break;
					case "ALQUILADA":
						rbTvAlquilada.Checked = true;
						break;
					case "FAMILIAR":
						rbTvFamiliar.Checked = true;
						break;
					case "APLAZOS":
						rbTvPagandola.Checked = true;
						break;
					default:
						rbTvOtros.Checked = true;
						break;
				}
				txtTipo_vivienda_especificar.Text = solicitud.tipo_vivienda_especificar;
				if (string.IsNullOrEmpty(solicitud.direccion_residencia))
					this.AdvertenciaGeneral += "Falta la residencia del solicitante. ";
				else
					txtDireccion_res.Text = solicitud.direccion_residencia;

				txtTelefono_fijo.Text = solicitud.telefono_fijo;

				if (string.IsNullOrEmpty(solicitud.telefono_celular))
					this.AdvertenciaGeneral += "Falta el celular del solicitante. ";
				else
					txtCelular.Text = solicitud.telefono_celular;

				txtTelAdicional1.Text = solicitud.telefono_adicional1;
				txtTelAdicional2.Text = solicitud.telefono_adicional2;
				txtCorreo_personal.Text = solicitud.correo_electronico;
				txtNoHijos.Text = solicitud.dependientes_hijos.ToString();
				txtOtrospariente.Text = solicitud.dependientes_otros.ToString();

				string vl_tipo_empresa = solicitud.tipo_empresa;
				if (vl_tipo_empresa == "Publico")
					rbTe_publico.Checked = true;

				if (vl_tipo_empresa == "Privado")
					rbTe_privado.Checked = true;

				if (vl_tipo_empresa == "Comerciante")
					rbTe_comerciante.Checked = true;

				if (vl_tipo_empresa == "Otros")
					rbTe_otros.Checked = true;
				txtTipo_empresa_especificar.Text = solicitud.tipo_empresa_especificar;
				if (string.IsNullOrEmpty(solicitud.patrono))
					this.AdvertenciaGeneral += "Falta el lugar de trabajo del solicitante. ";
				else
					txtPatrono.Text = solicitud.patrono;
				if (string.IsNullOrEmpty(solicitud.patrono))
					this.AdvertenciaGeneral += "Falta el cargo del solicitante. ";
				else
					txtCargo.Text = solicitud.cargo;
				txtDepto_labora.Text = solicitud.depto_labora;
				txtAntiguedad_laboral.Text = solicitud.antiguedad_laboral;
				if (solicitud.ingresos == 0)
					this.AdvertenciaGeneral += "Faltan los ingresos del solicitante. ";
				else
					txtIngresos.Text = String.Format("{0:###,##0.00}", solicitud.ingresos);
				txtOtros_ingresos.Text = String.Format("{0:###,##0.00}", solicitud.otros_ingresos);
				txtDeducciones.Text = String.Format("{0:###,##0.00}", solicitud.deducciones);

				if (string.IsNullOrEmpty(solicitud.direccion_laboral))
					this.AdvertenciaGeneral += "Falta la dirección laboral del solicitante. ";
				else
					txtDireccion_lab.Text = solicitud.direccion_laboral;

				txtTelefono_trabajo1.Text = solicitud.telefono_laboral1;
				txtExt1_trabajo.Text = solicitud.ext_laboral1;
				txtTelefono_trabajo2.Text = solicitud.telefono_laboral2;
				txtExt2_trabajo.Text = solicitud.ext_laboral2;
				txtCorreo_laboral.Text = solicitud.correo_laboral;

				saldo_aportaciones_principal = da.ObtenerSaldosAportacionesxCliente(txtIDSolicitante.Text);
				estado_burointerno_principal = da.ObtenerBuroInterno_xId(txtIDSolicitante.Text, out estado_desc_burointerno_principal, out observa_burointerno_principal).ToString();

				#endregion

				#region Conyuge

				txtId_conyuge.Text = solicitud.no_identificacion_conyuge;
				txtPriNombre_tpConyuge.Text = solicitud.primer_nombre_conyuge;
				txtSegNombre_tpConyuge.Text = solicitud.segundo_nombre_conyuge;
				txtPriApellido_tpConyuge.Text = solicitud.primer_apellido_conyuge;
				txtSegApellido_tpConyuge.Text = solicitud.segundo_apellido_conyuge;
				if (solicitud.sexo_conyuge == "M")
				{
					rbMasculino_tpConyuge.Checked = true;
				}
				if (solicitud.sexo_conyuge == "F")
				{
					rbFemenino_tpConyuge.Checked = true;
				}
				txtNoHijos_tpConyuge.Text = solicitud.dependientes_hijos_conyuge.ToString();
				txtOtrospariente_tpConyuge.Text = solicitud.dependientes_otros_conyuge.ToString();
				txtDirecc_res_tpConyuge.Text = solicitud.direccion_residencia_conyuge;
				txtTelefono_tpConyuge.Text = solicitud.telefono_fijo_conyuge;
				txtCelular_tpConyuge.Text = solicitud.celular_conyuge;
				txtOtrotelefono1_tpConyuge.Text = solicitud.telefono_adicional1_conyuge;
				txtOtrotelefono2_tpConyuge.Text = solicitud.telefono_adicional2_conyuge;
				txtCorreo_tpConyuge.Text = solicitud.correo_conyuge;
				if (solicitud.es_afiliado_conyuge == "S")
					rbSiAfiliado_tpConyuge.Checked = true;
				if (solicitud.es_afiliado_conyuge == "N")
					rbNoAfiliado_tpConyuge.Checked = true;
				txtCodigo_cliente_TpConyuge.Text = solicitud.codigo_cliente_conyuge;
				if (solicitud.tipo_empresa_conyuge == "Privado")
					rbTePrivado_tpConyuge.Checked = true;
				if (solicitud.tipo_empresa_conyuge == "Publico")
					rbTePublico_tpConyuge.Checked = true;
				if (solicitud.tipo_empresa_conyuge == "Comerciante")
					rbTeComerciante_tpConyuge.Checked = true;
				if (solicitud.tipo_empresa_conyuge == "Otros")
					rbTeOtros_tpConyuge.Checked = true;
				txtTipoEmpresaotros_tpCoyuge.Text = solicitud.tipo_empresa_especificar_conyuge;
				txtPatrono_tpConyuge.Text = solicitud.patrono_conyuge;
				txtDeptolabora_tpConyuge.Text = solicitud.depto_labora_conyuge;
				txtCargo_tpConyuge.Text = solicitud.cargo_conyuge;
				txtAntiglaboral_tpConyuge.Text = solicitud.antiguedad_conyuge;
				txtIngresos_tpConyuge.Text = String.Format("{0:###,##0.00}", solicitud.ingresos_conyuge);
				txtOtrosIngresos_tpConyuge.Text = String.Format("{0:###,##0.00}", solicitud.otros_ingresos_conyuge);
				txtDeducciones_tpConyuge.Text = String.Format("{0:###,##0.00}", solicitud.deducciones_conyuge);
				txtTellaboral1_tpConyuge.Text = solicitud.telefono_laboral1_conyuge;
				txtExtlaboral1_tpConyuge.Text = solicitud.ext_laboral1_conyuge;
				txtTellaboral2_tpConyuge.Text = solicitud.telefono_laboral2_conyuge;
				txtExtlaboral2_tpConyuge.Text = solicitud.ext_laboral2_conyuge;
				txtDirecclaboral_tpConyuge.Text = solicitud.direccion_laboral_conyuge;
				txtCorreolaboral_tpConyuge.Text = solicitud.correo_laboral_conyuge;

				saldo_aportaciones_conyuge = da.ObtenerSaldosAportacionesxCliente(txtId_conyuge.Text);
				estado_burointerno_conyuge = da.ObtenerBuroInterno_xId(txtId_conyuge.Text, out estado_desc_burointerno_conyuge, out observa_burointerno_conyuge).ToString();
				#endregion

				#region CoDeudor

				if (solicitud.requiere_codeudor.Equals("S"))
				{
					txtId_codeudor.Text = solicitud.no_identificacion_codeudor;
					txtPriNombre_tpCodeudor.Text = solicitud.primer_nombre_codeudor;
					txtSegNombre_tpCodeudor.Text = solicitud.segundo_nombre_codeudor;
					txtPriApellido_tpCodeudor.Text = solicitud.primer_apellido_codeudor;
					txtSegApellido_tpCodeudor.Text = solicitud.segundo_apellido_codeudor;
					if (solicitud.sexo_codeudor == "M")
					{
						rbMasculino_tpCodeudor.Checked = true;
					}
					if (solicitud.sexo_codeudor == "F")
					{
						rbFemenino_tpCodeudor.Checked = true;
					}
					txtNoHijos_tpCodeudor.Text = solicitud.dependientes_hijos_codeudor.ToString();
					txtOtrospariente_tpCodeudor.Text = solicitud.dependientes_otros_codeudor.ToString();

					if (string.IsNullOrEmpty(solicitud.direccion_residencia_codeudor))
						this.AdvertenciaGeneral += "Falta dirección del codeudor ";
					else
						txtDirecc_res_tpCodeudor.Text = solicitud.direccion_residencia_codeudor;

					txtTelefono_tpCodeudor.Text = solicitud.telefono_fijo_codeudor;

					if (string.IsNullOrEmpty(solicitud.celular_codeudor))
						this.AdvertenciaGeneral += "Falta el celular del codeudor. ";
					else
						txtCelular_tpCodeudor.Text = solicitud.celular_codeudor;

					txtOtrotelefono1_tpCodeudor.Text = solicitud.telefono_adicional1_codeudor;
					txtOtrotelefono2_tpCodeudor.Text = solicitud.telefono_adicional2_codeudor;
					txtCorreo_tpCodeudor.Text = solicitud.correo_codeudor;
					if (solicitud.es_afiliado_codeudor == "S")
						rbSiAfiliado_tpCodeudor.Checked = true;
					if (solicitud.es_afiliado_codeudor == "N")
						rbNoAfiliado_tpCodeudor.Checked = true;
					txtCodigo_cliente_TpCodeudor.Text = solicitud.codigo_cliente_codeudor;
					if (solicitud.tipo_empresa_codeudor == "Privado")
						rbTePrivado_tpCodeudor.Checked = true;
					if (solicitud.tipo_empresa_codeudor == "Publico")
						rbTePublico_tpCodeudor.Checked = true;
					if (solicitud.tipo_empresa_codeudor == "Comerciante")
						rbTeComerciante_tpCodeudor.Checked = true;
					if (solicitud.tipo_empresa_codeudor == "Otros")
						rbTeOtros_tpCodeudor.Checked = true;
					txtTipoEmpresaotros_tpCodeudor.Text = solicitud.tipo_empresa_especificar_codeudor;

					if (string.IsNullOrEmpty(solicitud.patrono_codeudor))
						this.AdvertenciaGeneral += "Falta el lugar de trabajo del codeudor. ";
					else
						txtPatrono_tpCodeudor.Text = solicitud.patrono_codeudor;

					txtDeptolabora_tpCodeudor.Text = solicitud.depto_labora_codeudor;

					if (string.IsNullOrEmpty(solicitud.cargo_codeudor))
						this.AdvertenciaGeneral += "Falta el cargo del codeudor. ";
					else
						txtCargo_tpCodeudor.Text = solicitud.cargo_codeudor;

					txtAntiglaboral_tpCodeudor.Text = solicitud.antiguedad_codeudor;

					if (solicitud.ingresos_codeudor == 0)
						this.AdvertenciaGeneral += "Faltan los ingresos del codeudor. ";
					else
						txtIngresos_tpCodeudor.Text = String.Format("{0:###,##0.00}", solicitud.ingresos_codeudor);

					txtOtrosIngresos_tpCodeudor.Text = String.Format("{0:###,##0.00}", solicitud.otros_ingresos_codeudor);
					txtDeducciones_tpCodeudor.Text = String.Format("{0:###,##0.00}", solicitud.deducciones_codeudor);
					txtTellaboral1_tpCodeudor.Text = solicitud.telefono_laboral1_codeudor;
					txtExtlaboral1_tpCodeudor.Text = solicitud.ext_laboral1_codeudor;
					txtTellaboral2_tpCodeudor.Text = solicitud.telefono_laboral2_codeudor;
					txtExtlaboral2_tpCodeudor.Text = solicitud.ext_laboral2_codeudor;

					tipo_vivienda = string.Empty;
					tipo_vivienda = solicitud.TipoViviendaCodeudor;
					switch (tipo_vivienda)
					{
						case "PROPIA":
							rbTp_PropiaCodeudor.Checked = true;
							break;
						case "ALQUILADA":
							rbTp_AlquiladaCodeudor.Checked = true;
							break;
						case "FAMILIAR":
							rbTp_FamiliarCodeudor.Checked = true;
							break;
						default:
							rbTp_OtrosCodeudor.Checked = true;
							break;
					}
					this.txtViviendaOtros_tpCod.Text = solicitud.TipoViviendaOtrosCodeudor;

					if (string.IsNullOrEmpty(solicitud.direccion_laboral_codeudor))
						this.AdvertenciaGeneral += "Falta la dirección laboral del codeudor. ";
					else
						txtDirecclaboral_tpCodeudor.Text = solicitud.direccion_laboral_codeudor;

					txtCorreolaboral_tpCodeudor.Text = solicitud.correo_laboral_codeudor;
					txtNombre_conyuge_tpCodeudor.Text = solicitud.nombre_conyuge_codeudor;
					txtDirelab_conyuge_tpCodeudor.Text = solicitud.direclab_conyuge_codeudor;
					txtCargo_conyuge_tpCodeudor.Text = solicitud.cargo_conyuge_codeudor;
					//FELVIR01 - 20190619
					if (this.txtCodigo_cliente_TpCodeudor.Text != string.Empty && this.txtCodigo_cliente_TpCodeudor.Text != "0")
					{
						string civilCodeudor = this.da.ObtenerEstadoCivil(int.Parse(this.txtCodigo_cliente_TpCodeudor.Text));
						this.txtEstadoCivil_tpCodeudor.Text = civilCodeudor;
					}
					//this.txtEdad_tpCodeudor.Text = solicitud.EdadCodeudor.ToString();

					saldo_aportaciones_codeudor = da.ObtenerSaldosAportacionesxCliente(txtId_codeudor.Text);
					estado_burointerno_codeudor = da.ObtenerBuroInterno_xId(txtId_codeudor.Text, out estado_desc_burointerno_codeudor, out observa_burointerno_codeudor).ToString();
				}
				#endregion

				#region Aval1

				if (solicitud.requiere_aval1.Equals("S"))
				{
					txtId_aval1.Text = solicitud.no_identificacion_aval1;
					txtPriNombre_tpAval1.Text = solicitud.primer_nombre_aval1;
					txtSegNombre_tpAval1.Text = solicitud.segundo_nombre_aval1;
					txtPriApellido_tpAval1.Text = solicitud.primer_apellido_aval1;
					txtSegApellido_tpAval1.Text = solicitud.segundo_apellido_aval1;
					if (solicitud.sexo_aval1 == "M")
					{
						rbMasculino_tpAval1.Checked = true;
					}
					if (solicitud.sexo_aval1 == "F")
					{
						rbFemenino_tpAval1.Checked = true;
					}
					txtNoHijos_tpAval1.Text = solicitud.dependientes_hijos_aval1.ToString();
					txtOtrospariente_tpAval1.Text = solicitud.dependientes_otros_aval1.ToString();

					if (string.IsNullOrEmpty(solicitud.direccion_residencia_aval1))
						this.AdvertenciaGeneral += "Falta dirección residencial del aval1. ";
					else
						txtDirecc_res_tpAval1.Text = solicitud.direccion_residencia_aval1;

					txtTelefono_tpAval1.Text = solicitud.telefono_fijo_aval1;

					if (string.IsNullOrEmpty(solicitud.celular_aval1))
						this.AdvertenciaGeneral += "Falta celular del aval1. ";
					else
						txtCelular_tpAval1.Text = solicitud.celular_aval1;

					txtOtrotelefono1_tpAval1.Text = solicitud.telefono_adicional1_aval1;
					txtOtrotelefono2_tpAval1.Text = solicitud.telefono_adicional2_aval1;
					txtCorreo_tpAval1.Text = solicitud.correo_aval1;
					if (solicitud.es_afiliado_aval1 == "S")
						rbSiAfiliado_tpAval1.Checked = true;
					if (solicitud.es_afiliado_aval1 == "N")
						rbNoAfiliado_tpAval1.Checked = true;
					txtCodigo_cliente_TpAval1.Text = solicitud.codigo_cliente_aval1;
					if (solicitud.tipo_empresa_aval1 == "Privado")
						rbTePrivado_tpAval1.Checked = true;
					if (solicitud.tipo_empresa_aval1 == "Publico")
						rbTePublico_tpAval1.Checked = true;
					if (solicitud.tipo_empresa_aval1 == "Comerciante")
						rbTeComerciante_tpAval1.Checked = true;
					if (solicitud.tipo_empresa_aval1 == "Otros")
						rbTeOtros_tpAval1.Checked = true;
					txtTipoEmpresaotros_tpAval1.Text = solicitud.tipo_empresa_especificar_aval1;

					if (string.IsNullOrEmpty(solicitud.patrono_aval1))
						this.AdvertenciaGeneral += "Falta el lugar de trabajo del aval1. ";
					else
						txtPatrono_tpAval1.Text = solicitud.patrono_aval1;

					txtDeptolabora_tpAval1.Text = solicitud.depto_labora_aval1;

					if (string.IsNullOrEmpty(solicitud.cargo_aval1))
						this.AdvertenciaGeneral += "Falta cargo del aval1. ";
					else
						txtCargo_tpAval1.Text = solicitud.cargo_aval1;

					txtAntiglaboral_tpAval1.Text = solicitud.antiguedad_aval1;

					if (solicitud.ingresos_aval1 == 0)
						this.AdvertenciaGeneral += "Faltan ingresos del aval1. ";
					else
						txtIngresos_tpAval1.Text = String.Format("{0:###,##0.00}", solicitud.ingresos_aval1);

					txtOtrosIngresos_tpAval1.Text = String.Format("{0:###,##0.00}", solicitud.otros_ingresos_aval1);
					txtDeducciones_tpAval1.Text = String.Format("{0:###,##0.00}", solicitud.deducciones_aval1);
					txtTellaboral1_tpAval1.Text = solicitud.telefono_laboral1_aval1;
					txtExtlaboral1_tpAval1.Text = solicitud.ext_laboral1_aval1;
					txtTellaboral2_tpAval1.Text = solicitud.telefono_laboral2_aval1;
					txtExtlaboral2_tpAval1.Text = solicitud.ext_laboral2_aval1;

					if (string.IsNullOrEmpty(solicitud.direccion_laboral_aval1))
						this.AdvertenciaGeneral += "Falta dirección laboral del aval1. ";
					else
						txtDirecclaboral_tpAval1.Text = solicitud.direccion_laboral_aval1;

					txtCorreolaboral_tpAval1.Text = solicitud.correo_laboral_aval1;
					txtNombre_conyuge_tpAval1.Text = solicitud.nombre_conyuge_aval1;
					txtDirelab_conyuge_tpAval1.Text = solicitud.direclab_conyuge_aval1;
					txtCargo_conyuge_tpAval1.Text = solicitud.cargo_conyuge_aval1;
					if (this.txtCodigo_cliente_TpAval1.Text != string.Empty && this.txtCodigo_cliente_TpAval1.Text != "0")
					{
						string civilAval1 = this.da.ObtenerEstadoCivil(int.Parse(this.txtCodigo_cliente_TpAval1.Text));
						this.txtEstadoCivil_tpAval1.Text = civilAval1;
					}
					//this.txtEdad_tpAval1.Text = solicitud.EdadAval1.ToString();

					tipo_vivienda = string.Empty;
					tipo_vivienda = solicitud.TipoViviendaAval1;
					switch (tipo_vivienda)
					{
						case "PROPIA":
							rbTp_PropiaAval1.Checked = true;
							break;
						case "ALQUILADA":
							rbTp_AlquiladaAval1.Checked = true;
							break;
						case "FAMILIAR":
							rbTp_FamiliarAval1.Checked = true;
							break;
						default:
							rbTp_OtrosAval1.Checked = true;
							break;
					}
					this.txtViviendaOtros_Aval1.Text = solicitud.TipoViviendaOtrosAval1;

					saldo_aportaciones_aval1 = da.ObtenerSaldosAportacionesxCliente(txtId_aval1.Text);
					estado_burointerno_aval1 = da.ObtenerBuroInterno_xId(txtId_aval1.Text, out estado_desc_burointerno_aval1, out observa_burointerno_aval1).ToString();
				}

				#endregion

				#region Aval2

				if (solicitud.requiere_aval2.Equals("S"))
				{
					txtId_aval2.Text = solicitud.no_identificacion_aval2;
					txtPriNombre_tpAval2.Text = solicitud.primer_nombre_aval2;
					txtSegNombre_tpAval2.Text = solicitud.segundo_nombre_aval2;
					txtPriApellido_tpAval2.Text = solicitud.primer_apellido_aval2;
					txtSegApellido_tpAval2.Text = solicitud.segundo_apellido_aval2;
					if (solicitud.sexo_aval2 == "M")
					{
						rbMasculino_tpAval2.Checked = true;
					}
					if (solicitud.sexo_aval2 == "F")
					{
						rbFemenino_tpAval2.Checked = true;
					}
					txtNoHijos_tpAval2.Text = solicitud.dependientes_hijos_aval2.ToString();
					txtOtrospariente_tpAval2.Text = solicitud.dependientes_otros_aval2.ToString();

					if (string.IsNullOrEmpty(solicitud.direccion_residencia_aval2))
						this.AdvertenciaGeneral += "Falta dirección residencial del aval2. ";
					else
						txtDirecc_res_tpAval2.Text = solicitud.direccion_residencia_aval2;

					//txtDirecc_res_tpAval2.Text = solicitud.direccion_residencia_aval2;
					txtTelefono_tpAval2.Text = solicitud.telefono_fijo_aval2;

					if (string.IsNullOrEmpty(solicitud.celular_aval2))
						this.AdvertenciaGeneral += "Falta celular del aval2. ";
					else
						txtCelular_tpAval2.Text = solicitud.celular_aval2;

					txtOtrotelefono1_tpAval2.Text = solicitud.telefono_adicional1_aval2;
					txtOtrotelefono2_tpAval2.Text = solicitud.telefono_adicional2_aval2;
					txtCorreo_tpAval2.Text = solicitud.correo_aval2;
					if (solicitud.es_afiliado_aval2 == "S")
						rbSiAfiliado_tpAval2.Checked = true;
					if (solicitud.es_afiliado_aval2 == "N")
						rbNoAfiliado_tpAval2.Checked = true;
					txtCodigo_cliente_TpAval2.Text = solicitud.codigo_cliente_aval2;
					if (solicitud.tipo_empresa_aval2 == "Privado")
						rbTePrivado_tpAval2.Checked = true;
					if (solicitud.tipo_empresa_aval2 == "Publico")
						rbTePublico_tpAval2.Checked = true;
					if (solicitud.tipo_empresa_aval2 == "Comerciante")
						rbTeComerciante_tpAval2.Checked = true;
					if (solicitud.tipo_empresa_aval2 == "Otros")
						rbTeOtros_tpAval2.Checked = true;
					txtTipoEmpresaotros_tpAval2.Text = solicitud.tipo_empresa_especificar_aval2;

					if (string.IsNullOrEmpty(solicitud.patrono_aval2))
						this.AdvertenciaGeneral += "Falta el lugar de trabajo del aval2. ";
					else
						txtPatrono_tpAval2.Text = solicitud.patrono_aval2;

					txtDeptolabora_tpAval2.Text = solicitud.depto_labora_aval2;

					if (string.IsNullOrEmpty(solicitud.cargo_aval2))
						this.AdvertenciaGeneral += "Falta cargo del aval2. ";
					else
						txtCargo_tpAval2.Text = solicitud.cargo_aval2;

					txtAntiglaboral_tpAval2.Text = solicitud.antiguedad_aval2;

					if (solicitud.ingresos_aval1 == 0)
						this.AdvertenciaGeneral += "Faltan ingresos del aval2. ";
					else
						txtIngresos_tpAval2.Text = String.Format("{0:###,##0.00}", solicitud.ingresos_aval2);


					txtOtrosIngresos_tpAval2.Text = String.Format("{0:###,##0.00}", solicitud.otros_ingresos_aval2);
					txtDeducciones_tpAval2.Text = String.Format("{0:###,##0.00}", solicitud.deducciones_aval2);
					txtTellaboral1_tpAval2.Text = solicitud.telefono_laboral1_aval2;
					txtExtlaboral1_tpAval2.Text = solicitud.ext_laboral1_aval2;
					txtTellaboral2_tpAval2.Text = solicitud.telefono_laboral2_aval2;
					txtExtlaboral2_tpAval2.Text = solicitud.ext_laboral2_aval2;

					if (string.IsNullOrEmpty(solicitud.direccion_laboral_aval1))
						this.AdvertenciaGeneral += "Falta dirección laboral del aval1. ";
					else
						txtDirecclaboral_tpAval2.Text = solicitud.direccion_laboral_aval2;

					txtCorreolaboral_tpAval2.Text = solicitud.correo_laboral_aval2;
					txtNombre_conyuge_tpAval2.Text = solicitud.nombre_conyuge_aval2;
					txtDirelab_conyuge_tpAval2.Text = solicitud.direclab_conyuge_aval2;
					txtCargo_conyuge_tpAval2.Text = solicitud.cargo_conyuge_aval2;
					if (this.txtCodigo_cliente_TpAval2.Text != string.Empty && this.txtCodigo_cliente_TpAval2.Text != "0")
					{
						string civilAval2 = this.da.ObtenerEstadoCivil(int.Parse(this.txtCodigo_cliente_TpAval2.Text));
						this.txtEstadoCivil_tpAval2.Text = civilAval2;
					}
					//this.txtEdad_tpAval2.Text = solicitud.EdadAval2.ToString();

					tipo_vivienda = string.Empty;
					tipo_vivienda = solicitud.TipoViviendaAval2;
					switch (tipo_vivienda)
					{
						case "PROPIA":
							rbTp_PropiaAval2.Checked = true;
							break;
						case "ALQUILADA":
							rbTp_AlquiladaAval2.Checked = true;
							break;
						case "FAMILIAR":
							rbTp_FamiliarAval2.Checked = true;
							break;
						default:
							rbTp_OtrosAval2.Checked = true;
							break;
					}
					this.txtViviendaOtros_tpAval2.Text = solicitud.TipoViviendaOtrosAval2;

					saldo_aportaciones_aval2 = da.ObtenerSaldosAportacionesxCliente(txtId_aval2.Text);
					estado_burointerno_aval2 = da.ObtenerBuroInterno_xId(txtId_aval2.Text, out estado_desc_burointerno_aval2, out observa_burointerno_aval2).ToString();
				}

				#endregion

				#region Concentracion Crediticia
				txtTotal_capitales_vigentes_grpfamiliar.Text = solicitud.total_capitalvigente_grpfam.ToString("###,###,##0.00");
				txtTotal_capitales_vigentes_titular.Text = solicitud.total_capitalvigente_solicitante.ToString("###,###,##0.00");
				txtSolicitado.Text = solicitud.monto_solicitado.ToString("###,###,##0.00");
				txtMonto_excluir_refcons.Text = solicitud.monto_excluir_refconsol.ToString("###,###,##0.00");
				txtNumerador_formula.Text = solicitud.total_paraindice.ToString("###,###,##0.00");
				txtPatrimonio.Text = solicitud.patrimonio_csf.ToString("###,###,###,##0.00");
				txtIndice_concentracion.Text = solicitud.porcentaje_concentracion.ToString("0.####");
				txtLimiteIndicador.Text = solicitud.limite_indicador.ToString("0.####");
				txtEvaluacionCDC.Text = solicitud.resultado_evaluacion_indicador;
				//Si tiene nucleo familiar
				if (solicitud.indicador_aplicado == 1)
				{
					rbIndicador1.Checked = true;
					rbIndicador2.Checked = false;
					rbIndicador3.Checked = false;
				}
				if (solicitud.indicador_aplicado == 2)
				{
					rbIndicador1.Checked = false;
					rbIndicador2.Checked = true;
					rbIndicador3.Checked = false;
				}
				if (solicitud.indicador_aplicado == 3)
				{
					rbIndicador1.Checked = false;
					rbIndicador2.Checked = false;
					rbIndicador3.Checked = true;
				}
				#endregion

				#region Nuevos Campos
				//FELVIR01
				this.txtEdad_presta.Text = solicitud.EdadPresta.ToString();
				this.txtEdad_tpAval1.Text = solicitud.EdadAval1.ToString();
				this.txtEdad_tpAval2.Text = solicitud.EdadAval2.ToString();
				this.txtEdad_tpCodeudor.Text = solicitud.EdadCodeudor.ToString();

				switch (solicitud.NivelEducConyuge)
				{
					case "P":
						this.rbPrimario_Conyuge.Checked = true;
						break;
					case "S":
						this.rbSecundaria_Conyuge.Checked = true;
						break;
					case "U":
						this.rbUniversitario_Conyuge.Checked = true;
						break;
					case "G":
						this.rbPostGrado_conyuge.Checked = true;
						break;
					default:
						break;
				}

				//Codeudor
				switch (solicitud.TipoViviendaCodeudor)
				{
					case "PROPIA":
						this.rbTp_PropiaCodeudor.Checked = true;
						break;
					case "APLAZOS":
						this.rbTp_PagandoCodeudor.Checked = true;
						break;
					case "FAMILIAR":
						this.rbTp_FamiliarCodeudor.Checked = true;
						break;
					case "ALQUILADA":
						this.rbTp_AlquiladaCodeudor.Checked = true;
						break;
					case "OTROS":
						this.rbTp_OtrosCodeudor.Checked = true;
						break;
					default:
						break;
				}
				this.txtViviendaOtros_tpCod.Text = solicitud.TipoViviendaOtrosCodeudor;

				//Aval1
				switch (solicitud.TipoViviendaAval1)
				{
					case "PROPIA":
						this.rbTp_PropiaAval1.Checked = true;
						break;
					case "APLAZOS":
						this.rbTp_PagandoAval1.Checked = true;
						break;
					case "FAMILIAR":
						this.rbTp_FamiliarAval1.Checked = true;
						break;
					case "ALQUILADA":
						this.rbTp_AlquiladaAval1.Checked = true;
						break;
					case "OTROS":
						this.rbTp_OtrosAval1.Checked = true;
						break;
					default:
						break;
				}
				this.txtViviendaOtros_Aval1.Text = solicitud.TipoViviendaOtrosAval1;

				//Aval 2
				switch (solicitud.TipoViviendaAval2)
				{
					case "PROPIA":
						this.rbTp_PropiaAval2.Checked = true;
						break;
					case "APLAZOS":
						this.rbTp_PagandoAval2.Checked = true;
						break;
					case "FAMILIAR":
						this.rbTp_FamiliarAval2.Checked = true;
						break;
					case "ALQUILADA":
						this.rbTp_AlquiladaAval2.Checked = true;
						break;
					case "OTROS":
						this.rbTp_OtrosAval2.Checked = true;
						break;
					default:
						break;
				}
				this.txtViviendaOtros_tpAval2.Text = solicitud.TipoViviendaOtrosAval2;

				this.txtTipoContrato.Text = solicitud.TipoContrato;
				this.txtRtnSolicitante.Text = solicitud.RTN;
				this.txtRtnAval1.Text = solicitud.RTN_Aval1;
				this.txtRtnAval2.Text = solicitud.RTN_Aval2;
				this.txtEstadoCivil_tpCodeudor.Text = solicitud.EstadoCivilCodeudor;
				this.txtEstadoCivil_tpAval1.Text = solicitud.EstadoCivilAval1;
				this.txtEstadoCivil_tpAval2.Text = solicitud.EstadoCivilAval2;
				this.txtEstado_civil.Text = solicitud.estado_civil;
				this.txtRtn_Codeurdor.Text = solicitud.RTN_Codeudor;
				this.txtFechaIngresoLaboral.Text = solicitud.FechaIngresoLaboral;
				this.txtIngresoLab_Co.Text = solicitud.FechaIngresoLaboralCodeudor;
				this.txtFechaIngreso_Aval1.Text = solicitud.FechaIngresoLaboralAval1;
				this.txtFechaIngreso_Aval2.Text = solicitud.FechaIngresoLaboralAval2;

				//this.txtMonto_cuotas_vencimiento_co.Text = solicitud.MontoCuotasVencCodeudor.ToString("###,###,##0.00");
				//this.txtMonto_cuotas_vencimiento_aval1.Text = solicitud.MontoCuotasVencAval1.ToString("###,###,##0.00");
				//this.txtMonto_cuotas_vencimiento_aval2.Text = solicitud.MontoCuotasVencAval2.ToString("###,###,##0.00");

				//this.txtDeudas_desc_sol.Text = solicitud.DeudasDescSol.ToString("###,###,##0.00");
				//this.txtDeudas_desc_co.Text = solicitud.DeudasDescCod.ToString("###,###,##0.00");
				//this.txtDeudas_desc_aval1.Text = solicitud.DeudasDescAval1.ToString("###,###,##0.00");
				//this.txtDeudas_desc_aval2.Text = solicitud.DeudasDescAval2.ToString("###,###,##0.00");

				#endregion


				if (!string.IsNullOrEmpty(this.AdvertenciaGeneral)/* & !gmodo_coopsafa.Equals("UPD")*/)
				{
					MessageBox.Show(this.AdvertenciaGeneral, "Ir a la ficha de solicitud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					//this.skipMethod();
				}

				ObtenerReferenciasSolicitud(solicitud.no_solicitud);
				Application.DoEvents();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void CargarGarante(int no_solicitud)
		{
			DataTable dtGar = this.da.ObtenerGarante(no_solicitud);
			if (dtGar.Rows.Count > 0)
			{
				this.txtId_tpGarante.Text = dtGar.Rows[0]["no_identificacion"].ToString();
				this.txtPrimerNom_tpGarante.Text = dtGar.Rows[0]["primer_nombre_g"].ToString();
				this.txtSegundoNom_tpGarante.Text = dtGar.Rows[0]["segundo_nombre_g"].ToString();
				this.txtPrimerAp_tpGarante.Text = dtGar.Rows[0]["primer_apellido_g"].ToString();
				this.txtSegundoAp_tpGarante.Text = dtGar.Rows[0]["segundo_apellido_g"].ToString();
				this.txtHijos_tpGarante.Text = dtGar.Rows[0]["dependientes_hijos_g"].ToString();
				this.txtOtrosDep_tpGarante.Text = dtGar.Rows[0]["dependientes_otros_g"].ToString();
				this.txtEdadGarante.Text = dtGar.Rows[0]["edad_garante"].ToString(); ;
				this.txtDirecRes_tpGarante.Text = dtGar.Rows[0]["direccion_residencial_g"].ToString();
				this.txtTelFijo_tpGarante.Text = dtGar.Rows[0]["telefono_fijo_garante"].ToString();
				this.txtCelular_tpGarante.Text = dtGar.Rows[0]["celular_garante"].ToString();
				this.txtTelAdic1_tpGarante.Text = dtGar.Rows[0]["telefono_adic1_garante"].ToString();
				this.txtTelAdic2_tpGarante.Text = dtGar.Rows[0]["telefono_adic2_garante"].ToString();
				this.txtCorreo_tpGarante.Text = dtGar.Rows[0]["correo_garante"].ToString();
				if (dtGar.Rows[0]["es_afiliado_garante"].ToString().Equals("S"))
				{
					this.rbEsAfiliadoSi_tpGarante.Checked = true;
					this.rbEsAfiliadoNo_tpGarante.Checked = false;
					this.txtCodigoCliente_Garante.Text = dtGar.Rows[0]["codigo_cliente_g"].ToString();
				}
				else
				{
					this.rbEsAfiliadoSi_tpGarante.Checked = false;
					this.rbEsAfiliadoNo_tpGarante.Checked = true;
				}
				if (dtGar.Rows[0]["genero_garante"].ToString().Equals("F"))
				{
					this.rbFemenino_tpGarante.Checked = true;
					this.rbMasculino_tpGarante.Checked = false;
				}
				else
				{
					this.rbFemenino_tpGarante.Checked = false;
					this.rbMasculino_tpGarante.Checked = true;
				}

				switch (dtGar.Rows[0]["tipo_empresa_garante"].ToString())
				{
					case "Privado":
						this.rbPrivado_Garante.Checked = true;
						break;
					case "Publico":
						this.rbPublico_Garante.Checked = true;
						break;
					case "Comerciante":
						this.rbComerciante_Garante.Checked = true;
						break;
					case "Otros":
						this.rbOtrosEmpresa_Garante.Checked = true;
						this.txtOtrosEmpresa_Garante.Text = dtGar.Rows[0]["tipo_empresa_otros_garante"].ToString();
						break;
					default:
						break;
				}

				switch (dtGar.Rows[0]["TIPO_VIVIENDA_GAR"].ToString())
				{
					case "Propia":
						this.rbPropia_Garante.Checked = true;
						break;
					case "Pagando":
						this.rbPagando_Garante.Checked = true;
						break;
					case "Alquilada":
						this.rbAlquilada_Garante.Checked = true;
						break;
					case "Familiar":
						this.rbFamiliar_Garante.Checked = true;
						break;
					case "Otros":
						this.rbOtros_Garante.Checked = true;
						break;
					default:
						break;
				}
				this.txtViviendaOtros_Garante.Text = dtGar.Rows[0]["TIPO_VIVIENDA_OTROS_GAR"].ToString();

				this.txtPatrono_Garante.Text = dtGar.Rows[0]["patrono_garante"].ToString();
				this.txtDepto_Garante.Text = dtGar.Rows[0]["depto_labora_garante"].ToString();
				this.txtCargo_Garante.Text = dtGar.Rows[0]["cargo_garante"].ToString();
				this.txtAntiguedad_Garante.Text = dtGar.Rows[0]["antiguedad_laboral_garante"].ToString();
				this.txtTel_LabGarante.Text = dtGar.Rows[0]["telefono_laboral1_garante"].ToString();
				this.txtTel_Labo2Garante.Text = dtGar.Rows[0]["telefono_laboral2_garante"].ToString();
				this.txtExt1_Garante.Text = dtGar.Rows[0]["ext_laboral1_garante"].ToString();
				this.txtExt2_Garante.Text = dtGar.Rows[0]["ext_laboral2_garante"].ToString();
				this.txtDireccionLab_Garante.Text = dtGar.Rows[0]["direccion_laboral_garante"].ToString();
				this.txtCorreoLab_Garante.Text = dtGar.Rows[0]["correo_laboral_garante"].ToString();
				this.txtConyuge_Garante.Text = dtGar.Rows[0]["nombre_conyuge_garante"].ToString();
				this.txtDirLabCony_Garante.Text = dtGar.Rows[0]["direc_lab_conyuge_garante"].ToString();
				this.txtCargoConyu_Garante.Text = dtGar.Rows[0]["cargo_conyuge_garante"].ToString();
				//Referencias
				this.txtRef1Nom_Garante.Text = dtGar.Rows[0]["nombre_ref1_garante"].ToString();
				this.txtRef1Res_Garante.Text = dtGar.Rows[0]["direccion_ref1_garante"].ToString();
				this.txtRef1Tel_Garante.Text = dtGar.Rows[0]["telefono_ref1_garante"].ToString();
				this.txtRef1PtoRef_Garante.Text = dtGar.Rows[0]["localizacion_ref1_garante"].ToString();
				this.txtRef1Cel_Garante.Text = dtGar.Rows[0]["celular_ref1_garante"].ToString();
				this.txtRef2Nom_Garante.Text = dtGar.Rows[0]["nombre_ref2_garante"].ToString();
				this.txtRef2Res_Garante.Text = dtGar.Rows[0]["direccion_ref2_garante"].ToString();
				this.txtRef2Tel_Garante.Text = dtGar.Rows[0]["telefono_ref2_garante"].ToString();
				this.txtRef2PtoRef_Garante.Text = dtGar.Rows[0]["localizacion_ref2_garante"].ToString();
				this.txtRef2Cel_Garante.Text = dtGar.Rows[0]["celular_ref2_garante"].ToString();
				this.txtRef3Nom_Garante.Text = dtGar.Rows[0]["nombre_ref3_garante"].ToString();
				this.txtRef3Res_Garante.Text = dtGar.Rows[0]["direccion_ref3_garante"].ToString();
				this.txtRef3Tel_Garante.Text = dtGar.Rows[0]["telefono_ref3_garante"].ToString();

			}
		}

		private void p_calcular_indice_concentracion_deuda()
		{
			if (gmodo_coopsafa == "INS")
			{
				gvDeudasNucleoFamiliar.DataSource = null;
				if (da.ObtenerParametro("WFC-0017") == "S")
				{
					double limite = 0;
					double limite11 = 0;
					double limite12 = 0;
					double limite13 = 0;
					Int32 vl_codigo_cliente = 0;
					try
					{
						limite11 = double.Parse(da.ObtenerParametro("WFC-0023"));
						limite12 = double.Parse(da.ObtenerParametro("WFC-0024"));
						limite13 = double.Parse(da.ObtenerParametro("WFC-0025"));
						vl_codigo_cliente = Int32.Parse(txtCodigo_cliente.Text);
					}

					catch
					{
						vl_codigo_cliente = 0;
					}
					groupbox_concentracioncrediticia.Visible = true;




					double monto_solicitado = 0;
					double monto_excluir_limite = 0;
					double numerador_formula = 0;
					double suma_capitales_vigentes_solicitante = 0;
					double suma_capitales_vigentes_grpfamiliar = 0;

					DataTable dtCapitalesVigentesxSolicitante = da.ObtenerCreditosVigenxDeudor(txtCodigo_cliente.Text);

					double.TryParse(dtCapitalesVigentesxSolicitante.Compute("SUM(mon_saldo_principal)", "").ToString(), out suma_capitales_vigentes_solicitante);
					txtTotal_capitales_vigentes_titular.Text = suma_capitales_vigentes_solicitante.ToString("###,###,###,##0.00");

					//Si tiene nucleo familiar
					if (da.TieneNucleoFamiliarconCreditos(vl_codigo_cliente))
					{
						limite = limite12;
						txtLimiteIndicador.Text = limite12.ToString();
						rbIndicador1.Checked = false;
						rbIndicador2.Checked = true;
						rbIndicador3.Checked = false;
						DataTable dtCapitalesVigentesGrpFamiliar = da.ObtenerCreditosVigenxNucleoFami(vl_codigo_cliente.ToString());
						gvDeudasNucleoFamiliar.DataSource = dtCapitalesVigentesGrpFamiliar;
						double.TryParse(dtCapitalesVigentesGrpFamiliar.Compute("SUM(mon_saldo_principal)", "").ToString(), out suma_capitales_vigentes_grpfamiliar);

						txtTotal_capitales_vigentes_grpfamiliar.Text = suma_capitales_vigentes_grpfamiliar.ToString("###,###,###,##0.00");
					}
					else
					{
						suma_capitales_vigentes_grpfamiliar = 0;
						txtTotal_capitales_vigentes_grpfamiliar.Text = "0.00";
						limite = limite11;
						txtLimiteIndicador.Text = limite11.ToString();
						rbIndicador1.Checked = true;
						rbIndicador2.Checked = false;
						rbIndicador3.Checked = false;
					}

					txtSolicitado.Text = txtMonto_solicitado.Text;


					double.TryParse(txtSolicitado.Text, out monto_solicitado);
					double.TryParse(txtMonto_excluir_refcons.Text, out monto_excluir_limite);

					numerador_formula = (suma_capitales_vigentes_solicitante + suma_capitales_vigentes_grpfamiliar + monto_solicitado) - monto_excluir_limite;
					double patrimonio = da.ObtenerInfoPatrimonio();
					var indice = (numerador_formula / patrimonio) * 100;

					txtSolicitado.Text = monto_solicitado.ToString("###,###,###,###,##0.00");
					txtNumerador_formula.Text = numerador_formula.ToString("###,###,###,###,##0.00");
					txtPatrimonio.Text = patrimonio.ToString("###,###,###,###,##0.00");
					txtIndice_concentracion.Text = indice.ToString("0.####");
					txtLimiteIndicador.Text = limite.ToString("0.####");



					if (indice > limite)
					{
						estado_desc_burointerno_principal = "RECHAZADO";
						txtEvaluacionCDC.Text = "RECHAZADO";
					}
					else
					{
						txtEvaluacionCDC.Text = "APROBADO";
					}
				}
			}
			else
			{
				if (da.ObtenerParametro("WFC-0017") == "S")
				{
					groupbox_concentracioncrediticia.Visible = true;
				}
			}
		}

		//FELVIR01 - 20190628
		private void ObtenerReferenciasSolicitud(Int32 p_no_solicitud)
		{
			string xml = da.ObtenerReferenciasxSolicitud2(p_no_solicitud);

			StringReader theReader = new StringReader(xml);
			DataSet theDataSet = new DataSet();
			theDataSet.ReadXml(theReader);
			var dt = theDataSet.Tables[1];
			// On all tables' rows
			foreach (DataRow dtRow in dt.Rows)
			{
				#region Principal
				if (dtRow["Rol"].ToString() == "Principal")
				{
					if (dtRow["no_referencia"].ToString() == "1")
					{
						txtRef1.Text = dtRow["Nombre"].ToString();
						txtRef1_direc.Text = dtRow["DirecRes"].ToString();
						txtRef1_telef.Text = dtRow["TelFijo"].ToString();
						txtRef1_ptoref.Text = dtRow["Ptoreferencia"].ToString();
						txtRef1_casacolor.Text = dtRow["Telcelular"].ToString();
					}
					if (dtRow["no_referencia"].ToString() == "2")
					{
						txtRef2.Text = dtRow["Nombre"].ToString();
						txtRef2_direc.Text = dtRow["DirecRes"].ToString();
						txtRef2_telef.Text = dtRow["TelFijo"].ToString();
						txtRef2_ptoref.Text = dtRow["Ptoreferencia"].ToString();
						txtRef2_casacolor.Text = dtRow["Telcelular"].ToString();
					}
					if (dtRow["no_referencia"].ToString() == "3")
					{
						txtRef3.Text = dtRow["Nombre"].ToString();
						txtRef3_direc.Text = dtRow["DirecRes"].ToString();
						txtRef3_telef.Text = dtRow["TelFijo"].ToString();
						this.txtRef3_ptoref.Text = dtRow["Ptoreferencia"].ToString();
						this.txtRef3_casacolor.Text = dtRow["Telcelular"].ToString();
					}
				}
				#endregion

				#region Codeudor
				if (dtRow["Rol"].ToString() == "Codeudor")
				{
					if (dtRow["no_referencia"].ToString() == "1")
					{
						txtRef1_tpCodeudor.Text = dtRow["Nombre"].ToString();
						txtRef1_direc_tpCodeudor.Text = dtRow["DirecRes"].ToString();
						txtRef1_telef_tpCodeudor.Text = dtRow["TelFijo"].ToString();
						txtRef1_ptoref_tpCodeudor.Text = dtRow["Ptoreferencia"].ToString();
						txtRef1_casacolor_tpCodeudor.Text = dtRow["Telcelular"].ToString();
					}
					if (dtRow["no_referencia"].ToString() == "2")
					{
						txtRef2_tpCodeudor.Text = dtRow["Nombre"].ToString();
						txtRef2_direc_tpCodeudor.Text = dtRow["DirecRes"].ToString();
						txtRef2_telef_tpCodeudor.Text = dtRow["TelFijo"].ToString();
						txtRef2_ptoref_tpCodeudor.Text = dtRow["Ptoreferencia"].ToString();
						txtRef2_casacolor_tpCodeudor.Text = dtRow["Telcelular"].ToString();
					}
					if (dtRow["no_referencia"].ToString() == "3")
					{
						txtRef3_tpCodeudor.Text = dtRow["Nombre"].ToString();
						txtRef3_direc_tpCodeudor.Text = dtRow["DirecRes"].ToString();
						txtRef3_telef_tpCodeudor.Text = dtRow["TelFijo"].ToString();
						this.txtRef3_ptoref_tpCodeudor.Text = dtRow["Ptoreferencia"].ToString();
						this.txtRef3_casacolor_tpCodeudor.Text = dtRow["Telcelular"].ToString();
					}
				}
				#endregion

				#region Aval1
				if (dtRow["Rol"].ToString() == "Aval1")
				{
					if (dtRow["no_referencia"].ToString() == "1")
					{
						txtRef1_tpAval1.Text = dtRow["Nombre"].ToString();
						txtRef1_direc_tpAval1.Text = dtRow["DirecRes"].ToString();
						txtRef1_telef_tpAval1.Text = dtRow["TelFijo"].ToString();
						txtRef1_ptoref_tpAval1.Text = dtRow["Ptoreferencia"].ToString();
						txtRef1_casacolor_tpAval1.Text = dtRow["Telcelular"].ToString();
					}
					if (dtRow["no_referencia"].ToString() == "2")
					{
						txtRef2_tpAval1.Text = dtRow["Nombre"].ToString();
						txtRef2_direc_tpAval1.Text = dtRow["DirecRes"].ToString();
						txtRef2_telef_tpAval1.Text = dtRow["TelFijo"].ToString();
						txtRef2_ptoref_tpAval1.Text = dtRow["Ptoreferencia"].ToString();
						txtRef2_casacolor_tpAval1.Text = dtRow["Telcelular"].ToString();
					}
					if (dtRow["no_referencia"].ToString() == "3")
					{
						txtRef3_tpAval1.Text = dtRow["Nombre"].ToString();
						txtRef3_direc_tpAval1.Text = dtRow["DirecRes"].ToString();
						txtRef3_telef_tpAval1.Text = dtRow["TelFijo"].ToString();
						this.txtRef3_ptoref_tpAval1.Text = dtRow["Ptoreferencia"].ToString();
						this.txtRef3_casacolor_tpAval1.Text = dtRow["Telcelular"].ToString();
					}
				}
				#endregion

				#region Aval2
				if (dtRow["Rol"].ToString() == "Aval2")
				{
					if (dtRow["no_referencia"].ToString() == "1")
					{
						txtRef1_tpAval2.Text = dtRow["Nombre"].ToString();
						txtRef1_direc_tpAval2.Text = dtRow["DirecRes"].ToString();
						txtRef1_telef_tpAval2.Text = dtRow["TelFijo"].ToString();
						txtRef1_ptoref_tpAval2.Text = dtRow["Ptoreferencia"].ToString();
						txtRef1_casacolor_tpAval2.Text = dtRow["Telcelular"].ToString();
					}
					if (dtRow["no_referencia"].ToString() == "2")
					{
						txtRef2_tpAval2.Text = dtRow["Nombre"].ToString();
						txtRef2_direc_tpAval2.Text = dtRow["DirecRes"].ToString();
						txtRef2_telef_tpAval2.Text = dtRow["TelFijo"].ToString();
						txtRef2_ptoref_tpAval2.Text = dtRow["Ptoreferencia"].ToString();
						txtRef2_casacolor_tpAval2.Text = dtRow["Telcelular"].ToString();
					}
					if (dtRow["no_referencia"].ToString() == "3")
					{
						txtRef3_tpAval2.Text = dtRow["Nombre"].ToString();
						txtRef3_direc_tpAval2.Text = dtRow["DirecRes"].ToString();
						txtRef3_telef_tpAval2.Text = dtRow["TelFijo"].ToString();
						this.txtRef3_ptoref_tpAval2.Text = dtRow["Ptoreferencia"].ToString();
						this.txtRef3_casacolor_tpAval2.Text = dtRow["Telcelular"].ToString();
					}
				}
				#endregion
			}
		}

		//FELVIR01 - 20190625
		private bool p_valida_solicitud()
		{
			bool vl_retorno = true;


			if (string.IsNullOrEmpty(txtIDSolicitante.Text) | string.IsNullOrEmpty(txtNombre.Text) | string.IsNullOrEmpty(txtPrimer_apellido.Text))//string.IsNullOrEmpty(txtSegundo_apellido.Text))
			{
				MessageBox.Show("Debe indicar los datos del solicitante..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtMonto_solicitado.Focus();
				return false;
			}
			if (string.IsNullOrEmpty(txtMonto_solicitado.Text))
			{
				MessageBox.Show("Debe indicar el monto a solicitar !", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtMonto_solicitado.Focus();
				return false;
			}
			try
			{
				if (float.Parse(txtMonto_solicitado.Text) <= 0)
				{
					MessageBox.Show("Debe indicar el monto a solicitar !", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return false;
				}
			}
			catch
			{
				return false;
			}

			if (string.IsNullOrEmpty(txtPlazo.Text))
			{
				MessageBox.Show("Debe indicar el plazo del prestamo !", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtPlazo.Focus();
				return false;
			}
			if (string.IsNullOrEmpty(txtTasa.Text))
			{
				MessageBox.Show("Debe indicar la tasa a otorgar !", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtTasa.Focus();
				return false;
			}
			if (string.IsNullOrEmpty(txtDestino_credito.Text))
			{
				MessageBox.Show("Debe indicar el destino del credito !", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtDestino_credito.Focus();
				return false;
			}

			if (rbTvOtros.Checked && string.IsNullOrEmpty(txtTipo_vivienda_especificar.Text))
			{
				MessageBox.Show("Debe especificar el tipo de vivienda que posee, ha seleccionado la opcion otros", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}


			if (string.IsNullOrEmpty(txtNoHijos.Text))
			{
				txtNoHijos.Text = "0";
			}
			if (string.IsNullOrEmpty(txtOtrospariente.Text))
			{
				txtOtrospariente.Text = "0";
			}




			//Usando el mismo ID en las 4 figuras
			if (cbAval1.Checked & cbAval2.Checked & cbCodeudor.Checked)
			{
				if (txtId_aval1.Text.Trim() == txtId_aval2.Text.Trim() | txtId_aval1.Text.Trim() == txtId_codeudor.Text.Trim())
				{
					MessageBox.Show("No puede utilizar la misma identificacion para Aval1, Aval 2 o Codeudor...!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return false;
				}
			}
			if (cbCodeudor.Checked & cbAval1.Checked & !cbAval2.Checked)
			{
				if (txtId_aval1.Text.Trim() == txtId_codeudor.Text.Trim())
				{
					MessageBox.Show("No puede utilizar la misma identificacion para Aval1 y Codeudor...!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return false;
				}
			}


			if (txtIDSolicitante.Text.Trim() == txtId_aval1.Text.Trim() | txtIDSolicitante.Text.Trim() == txtId_aval2.Text.Trim() | txtIDSolicitante.Text.Trim() == txtId_codeudor.Text.Trim())
			{
				MessageBox.Show("La identificacion del solicitante con el  Aval1, Aval 2 o Codeudor no puede ser la misma...!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			if (txtIDSolicitante.Text.Trim() == txtId_conyuge.Text)
			{
				MessageBox.Show("La identificacion del solicitante y el conyugue no puede ser la misma...", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}


			if ((cbTieneDeudascanc_solic.Checked) && (decimal.Parse(txtDeudasCancelados_solic.Text) == 0))
			{
				MessageBox.Show("Debe indicar la cantidad de cuotas canceladas para el solicitante si seleccionó el cheque de deudas canceladas ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			if ((cbTieneDeudascanc_codeud.Checked) && (decimal.Parse(txtDeudasCancelados_codeud.Text) == 0))
			{
				MessageBox.Show("Debe indicar la cantidad de cuotas canceladas para el codeudor si seleccionó el cheque de deudas canceladas ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			if ((cbTieneDeudascanc_aval1.Checked) && (decimal.Parse(txtDeudasCancelados_aval1.Text) == 0))
			{
				MessageBox.Show("Debe indicar la cantidad de cuotas canceladas para el aval1 si seleccionó el cheque de deudas canceladas ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			if ((cbTieneDeudascanc_aval2.Checked) && (decimal.Parse(txtDeudasCancelados_aval2.Text) == 0))
			{
				MessageBox.Show("Debe indicar la cantidad de cuotas canceladas para el aval2 si seleccionó el cheque de deudas canceladas ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}




			//Validacion de Codeudor
			if (cbCodeudor.Checked)
			{
				if (string.IsNullOrEmpty(txtPriNombre_tpCodeudor.Text) | string.IsNullOrEmpty(txtId_codeudor.Text))
				{
					MessageBox.Show("Debe espeficiar la información del Codeudor", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return false;
				}
			}

			//Validacion de Aval1
			if (cbAval1.Checked)
			{
				if (string.IsNullOrEmpty(txtId_aval1.Text) | string.IsNullOrEmpty(txtPriNombre_tpAval1.Text) | string.IsNullOrEmpty(txtPriApellido_tpAval1.Text))
				{
					MessageBox.Show("Debe espeficiar la información personal del Aval 1", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return false;
				}
				decimal vl_ingresos_aval1 = 0;
				decimal vl_otros_ingresos_aval1 = 0;
				decimal vl_deducciones_aval1 = 0;
				decimal.TryParse(txtIngresos_tpAval1.Text, out vl_ingresos_aval1);
				if (vl_ingresos_aval1 == 0)
				{
					MessageBox.Show("Debe indicar los ingresos, otros ingresos si tuviera y las deducciones del aval 1", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return false;
				}

			}

			//Validacion de Aval2
			if (cbAval2.Checked)
			{
				if (string.IsNullOrEmpty(txtId_aval2.Text) | string.IsNullOrEmpty(txtPriNombre_tpAval2.Text) | string.IsNullOrEmpty(txtPriApellido_tpAval2.Text))
				{
					MessageBox.Show("Debe espeficiar la información personal del Aval 2", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return false;
				}
				decimal vl_ingresos_aval2 = 0;
				decimal vl_otros_ingresos_aval2 = 0;
				decimal vl_deducciones_aval2 = 0;
				decimal.TryParse(txtIngresos_tpAval2.Text, out vl_ingresos_aval2);
				if (vl_ingresos_aval2 == 0)
				{
					MessageBox.Show("Debe indicar los ingresos, otros ingresos si tuviera y las deducciones del aval 2", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return false;
				}
			}

			//Validacion de Codedudor
			if (cbCodeudor.Checked)
			{
				if (string.IsNullOrEmpty(txtId_codeudor.Text) | string.IsNullOrEmpty(txtPriNombre_tpCodeudor.Text) | string.IsNullOrEmpty(txtPriApellido_tpCodeudor.Text))
				{
					MessageBox.Show("Debe espeficiar la información personal del Codeduor", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return false;
				}
				decimal vl_ingresos_codeudor = 0;
				decimal vl_otros_ingresos_codeudor = 0;
				decimal vl_deducciones_codeudor = 0;
				decimal.TryParse(txtIngresos_tpCodeudor.Text, out vl_ingresos_codeudor);
				if (vl_ingresos_codeudor == 0)
				{
					MessageBox.Show("Debe indicar los ingresos, otros ingresos si tuviera y las deducciones del Codeudor", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return false;
				}
			}




			//Validacion de Consolidacion
			if (cbConsolidacion.Checked)
			{
				decimal vl_monto_consolidar = 0;
				decimal vl_cuota_consolidar = 0;
				decimal vl_monto_solicitado = 0;
				decimal.TryParse(txtTotal_capital_consolidar.Text, out vl_monto_consolidar);
				decimal.TryParse(txtTotal_cuotas_consolidar.Text, out vl_cuota_consolidar);
				decimal.TryParse(txtMonto_solicitado.Text, out vl_monto_solicitado);
				if (vl_monto_consolidar == 0)
				{
					MessageBox.Show("Se debe indicar un monto a consolidar...!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return false;
				}
				if (vl_cuota_consolidar == 0)
				{
					MessageBox.Show("Se debe indicar un monto a consolidar...!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return false;
				}

				if (vl_monto_consolidar > vl_monto_solicitado)
				{
					MessageBox.Show("El monto solicitado es menor al monto a la suma de montos a consolidar..!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return false;
				}

				if (gcant_cuotas_buro > 0)
				{
					if (dtCuotasBuro.Rows.Count <= 0)
					{
						MessageBox.Show("Las cuotas obtenidas buro son una cantidad menor que las que tenia guardadas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return false;
					}
				}
			}

			//Validacion de Vehiculos
			if (txtCodigo_sub_aplicacion.Text.Trim() == "10" || txtCodigo_sub_aplicacion.Text.Trim() == "23" || txtCodigo_sub_aplicacion.Text.Trim() == "32")
			{
				if (!rbAutoNuevo.Checked && !rbAutoUsado.Checked)
				{
					MessageBox.Show("Debe seleccionar la condición del vehiculo (Nuevo o Usado)!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return false;
				}
				decimal vl_valor_vehiculo = 0;
				decimal.TryParse(txtValor_vehiculo.Text, out vl_valor_vehiculo);
				if (vl_valor_vehiculo == 0)
				{
					MessageBox.Show("Debe indicar el valor del vehiculo...", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return false;
				}

			}
			//Validacion de los ingresos del solicitante.
			if (txtIngresos.Text == "0.00" | txtDeducciones.Text == "0.00")
			{
				MessageBox.Show("Debe indicar los ingresos y deducciones del solicitante..!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}

			if (string.IsNullOrEmpty(txtDescripcion_garantia.Text))
			{
				MessageBox.Show("Debe indicar la descripcion de la garantia..!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}

			if (string.IsNullOrEmpty(txtCodigo_sub_aplicacion.Text))
			{
				MessageBox.Show("No se ha indicado el codigo de producto para esta solicitud..!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			//VALIDACION DE RANGOS DE ACUERDO A PARAMETRIZACION EN MGI PARA EL PRODUCTO
			try
			{
				string vl_controla_parametros_prod = da.ObtenerParametro("WFC-0026");
				if (vl_controla_parametros_prod.ToUpper() == "S")
				{
					CR_TIPO_PRESTAMO.da = this.da;
					var configuracionproducto = CR_TIPO_PRESTAMO.obtenerTipoPrestamo(Int16.Parse(txtCodigo_sub_aplicacion.Text));

					int plazo = 0;
					decimal tasa = 0;
					int.TryParse(txtPlazo.Text, out plazo);
					decimal.TryParse(txtTasa.Text, out tasa);


					if (plazo < configuracionproducto.Num_mesesplazo_min || plazo > configuracionproducto.Num_mesesplazo)
					{
						MessageBox.Show("Plazo fuera de los limites del producto...!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return false;
					}
					if (tasa < configuracionproducto.Por_tasaminima || tasa > configuracionproducto.Por_tasamaxima)
					{
						MessageBox.Show("Tasa fuera de los limites del producto...!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return false;
					}
				}
			}
			catch
			{

			}

			if (this.cbGarante.Checked && string.IsNullOrEmpty(this.txtId_tpGarante.Text))
			{
				MessageBox.Show("No ha llenado los campos del Garante Hipotecario", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}


			return vl_retorno;
		}

		public DataTable stam(string xmlData)
		{
			StringReader theReader = new StringReader(xmlData);
			DataSet theDataSet = new DataSet();
			theDataSet.ReadXml(theReader);

			return theDataSet.Tables[0];
		}
		private void procesar_xml_respuesta(string p_resultado_consulta, string p_accion, string p_modio_ori_transunion)
		{
			if (string.IsNullOrEmpty(p_resultado_consulta))
				return;
			string vl_enter = "\r\n";
			XmlDocument xmlDoc = new XmlDocument();

			string resultado_consulta = p_resultado_consulta;

			txtObs_solicitud.Text = "";
			txtErrores_trans.Text = "";
			try
			{
				xmlDoc.LoadXml(resultado_consulta);
				gxml_respuesta = resultado_consulta;
				string vl_Status = xmlDoc.SelectSingleNode("DCResponse/Status").InnerText;
				if (vl_Status.ToString() == "Success")
				{
					string vl_ApplicationId = xmlDoc.SelectSingleNode("DCResponse/ResponseInfo/ApplicationId").InnerText;
					string vl_ReporteCreditoPrincipal = xmlDoc.SelectSingleNode("DCResponse/ContextData/ReporteCreditoPrincipal").InnerText;
					string vl_gtabla_resultado = xmlDoc.SelectSingleNode("DCResponse/ContextData/TablaResultados").InnerText;

					//--cambio para fabiola
					Int32 vl_solicitud = Convert.ToInt32(txtNo_solicitud_coopsafa.Text);
					//-----

					XmlDocument OutputXML = new XmlDocument();
					txtDecision_solicitud.Text = xmlDoc.SelectSingleNode("DCResponse/ContextData/Field[@key='Decision']").InnerText.ToString();
					try
					{

						da.ActualizarResultado_Buro(vl_solicitud, txtDecision_solicitud.Text);
						if (txtDecision_solicitud.Text.Contains("RECHAZADO"))
						{
							pbDecisionSistema.Image = Docsis_Application.Properties.Resources.rechazado_icon;
						}
						if (txtDecision_solicitud.Text.Contains("APROBADO"))
						{
							pbDecisionSistema.Image = Docsis_Application.Properties.Resources.aprobado_icon;
						}
						if (txtDecision_solicitud.Text.Contains("REFERIDO"))
						{
							pbDecisionSistema.Image = Docsis_Application.Properties.Resources.referido_icon;
						}

					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
					}
					var FLAG_Cierre = xmlDoc.SelectSingleNode("DCResponse/ContextData/Field[@key='FLAG_Cierre']").InnerText.ToString();

					OutputXML.LoadXml(xmlDoc.SelectSingleNode("DCResponse/ContextData/Field[@key='OutputXML']").InnerText);
					gxml_outputxml = OutputXML.InnerXml;

					string vl_error = OutputXML.SelectSingleNode("DCResponse/Single/MensajeError").InnerText;
					string vl_decision_solicitud = OutputXML.SelectSingleNode("DCResponse/Single/Decision").InnerText;

					string vl_score_creticio = "";
					try
					{
						vl_score_creticio = OutputXML.SelectSingleNode("DCResponse/Multi/item0/Score").InnerText;
					}
					catch (Exception ex)
					{

					}

					float nmonto_solicitado = 0;
					float.TryParse(txtMonto_solicitado.Text, out nmonto_solicitado);

					System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-HN");
					System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-HN");
					string vl_monto_solicitado = string.Format(String.Format("{0:###,##0.00}", nmonto_solicitado));


					System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-HN");
					System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-HN");

					float nmonto_aprobado = 0;
					float.TryParse(OutputXML.SelectSingleNode("DCResponse/Single/MontoAprobado").InnerText, out nmonto_aprobado);
					string vl_monto_aprobado = string.Format(String.Format("{0:###,##0.00}", nmonto_aprobado));
					string vl_capacidad_deuda_final = OutputXML.SelectSingleNode("DCResponse/Single/CapacidadDeudaFinal").InnerText;
					string vl_rci = OutputXML.SelectSingleNode("DCResponse/Single/RelacionCuotaIngreso").InnerText;

					System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-HN");
					System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-HN");
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
					//Pre bruo
					string vl_OBS_PRE_BURO_01_Descripcion = OutputXML.SelectSingleNode("DCResponse/Single/OBS_PRE_BURO_01_Descripcion").InnerText.Trim();
					if (!string.IsNullOrEmpty(vl_OBS_PRE_BURO_01_Descripcion))
					{
						txtObs_solicitud.AppendText(vl_OBS_PRE_BURO_01_Descripcion + "." + vl_enter);
					}
					string vl_OBS_PRE_BURO_02_Descripcion = OutputXML.SelectSingleNode("DCResponse/Single/OBS_PRE_BURO_02_Descripcion").InnerText.Trim();
					if (!string.IsNullOrEmpty(vl_OBS_PRE_BURO_02_Descripcion))
					{
						txtObs_solicitud.AppendText(vl_OBS_PRE_BURO_02_Descripcion + "." + vl_enter);
					}
					string vl_OBS_PRE_BURO_03_Descripcion = OutputXML.SelectSingleNode("DCResponse/Single/OBS_PRE_BURO_03_Descripcion").InnerText.Trim();
					if (!string.IsNullOrEmpty(vl_OBS_PRE_BURO_03_Descripcion))
					{
						txtObs_solicitud.AppendText(vl_OBS_PRE_BURO_03_Descripcion + "." + vl_enter);
					}
					string vl_OBS_PRE_BURO_04_Descripcion = OutputXML.SelectSingleNode("DCResponse/Single/OBS_PRE_BURO_04_Descripcion").InnerText.Trim();
					if (!string.IsNullOrEmpty(vl_OBS_PRE_BURO_04_Descripcion))
					{
						txtObs_solicitud.AppendText(vl_OBS_PRE_BURO_04_Descripcion + "." + vl_enter);
					}
					string vl_OBS_PRE_BURO_05_Descripcion = OutputXML.SelectSingleNode("DCResponse/Single/OBS_PRE_BURO_05_Descripcion").InnerText.Trim();
					if (!string.IsNullOrEmpty(vl_OBS_PRE_BURO_05_Descripcion))
					{
						txtObs_solicitud.AppendText(vl_OBS_PRE_BURO_05_Descripcion + "." + vl_enter);
					}
					#endregion


					var tt = OutputXML.InnerText;
					var t1 = OutputXML.InnerXml;

					txtDecision_solicitud.Text = vl_decision_solicitud;
					txtMonto_aprobado_resume.Text = vl_monto_aprobado;
					txtMonto_solicitado_resume.Text = vl_monto_solicitado;
					txtCapacidad_deuda_final.Text = vl_capacidad_deuda_final;
					txtCuota_resume.Text = vl_cuotaMontoAprobado;
					txtRci_resume.Text = vl_rci;
					txtErrores_trans.Text = vl_error;

					//cambio para la version de Fabiola
					txtScore.Text = vl_score_creticio;
					lblScore.Text = vl_score_creticio;
					graficar_score_crediticio();
					da.p_actualizar_score_resul(vl_solicitud, txtScore.Text);


					DataTable tblResultados = new DataTable();
					tblResultados.Columns.Add("id");
					tblResultados.Columns.Add("rol");
					tblResultados.Columns.Add("nombre");
					tblResultados.Columns.Add("edad");
					tblResultados.Columns.Add("score");
					tblResultados.Columns.Add("precalificado");
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
						string dtscore = "";
						string dtdecisionPrecalificado = "";
						string id_bitacora_busqueda = "";
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
									   dtscore,
									   dtdecisionPrecalificado,
									   vl_flags);

						if (p_accion == "AFTER_OF_PRECALIFICAR")
						{
							//Generando la bitacora de acceso a motor, una acceso por cada individuo                                               
							int vl_tipo_acceso = 0;
							Int32 vl_idBitacora = 0;
							Int32 vl_no_solicitud = 0;
							Int32 vl_appid = 0;
							try
							{
								vl_idBitacora = Int32.Parse(id_bitacora_busqueda = dt.Rows[0]["idBitacoraBusqueda"].ToString());
								vl_no_solicitud = Int32.Parse(txtNo_solicitud_coopsafa.Text);
								vl_appid = Int32.Parse(txtApplicationID.Text);

							}
							catch
							{

							}
							if (p_modio_ori_transunion == "CREAR")
							{
								vl_tipo_acceso = 1;
							}
							else
							{
								vl_tipo_acceso = 2;
							}
							//1 - Creacion de Solicitud, 2 Recalcular, 3 Obtencion de cuotas
							da.p_generar_bitacorabusqueda_tu(vl_idBitacora, vl_no_solicitud, vl_appid, vl_tipo_acceso);
						}

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

							string dtCedula2 = "";
							string dtRol2 = "";
							string dtNombre2 = "";
							string dtedad2 = "";
							string dtscore2 = "";
							string dtdecisionPrecalificado2 = "";
							string id_bitacora_busqueda = "";
							if (dt.Columns.Contains("Cedula"))
							{
								dtCedula2 = dt.Rows[0]["Cedula"].ToString();
							}
							if (dt.Columns.Contains("Rol"))
							{
								dtRol2 = dt.Rows[0]["Rol"].ToString();
							}
							if (dt.Columns.Contains("Nombre"))
							{
								dtNombre2 = dt.Rows[0]["Nombre"].ToString();
							}
							if (dt.Columns.Contains("edad"))
							{
								dtedad2 = dt.Rows[0]["edad"].ToString();
							}
							if (dt.Columns.Contains("score"))
							{
								dtscore2 = dt.Rows[0]["score"].ToString();
							}
							if (dt.Columns.Contains("decisionPrecalificado"))
							{
								dtdecisionPrecalificado2 = dt.Rows[0]["decisionPrecalificado"].ToString();
							}



							tblResultados.Rows.Add(dtCedula2,
									   dtRol2,
									   dtNombre2,
									   dtedad2,
									   dtscore2,
									   dtdecisionPrecalificado2,
									   vl_flags_2persona);

							if (p_accion == "AFTER_OF_PRECALIFICAR")
							{
								//Generando la bitacora de acceso a motor, una acceso por cada individuo                                               
								int vl_tipo_acceso = 0;
								Int32 vl_idBitacora = 0;
								Int32 vl_no_solicitud = 0;
								Int32 vl_appid = 0;
								try
								{
									vl_idBitacora = Int32.Parse(id_bitacora_busqueda = dt.Rows[0]["idBitacoraBusqueda"].ToString());
									vl_no_solicitud = Int32.Parse(txtNo_solicitud_coopsafa.Text);
									vl_appid = Int32.Parse(txtApplicationID.Text);

								}
								catch
								{

								}
								if (p_modio_ori_transunion == "CREAR")
								{
									vl_tipo_acceso = 1;
								}
								else
								{
									vl_tipo_acceso = 2;
								}
								//1 - Creacion de Solicitud, 2 Recalcular, 3 Obtencion de cuotas
								da.p_generar_bitacorabusqueda_tu(vl_idBitacora, vl_no_solicitud, vl_appid, vl_tipo_acceso);
							}

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

							string dtCedula3 = "";
							string dtRol3 = "";
							string dtNombre3 = "";
							string dtedad3 = "";
							string dtscore3 = "";
							string dtdecisionPrecalificado3 = "";
							string id_bitacora_busqueda = "";
							if (dt.Columns.Contains("Cedula"))
							{
								dtCedula3 = dt.Rows[0]["Cedula"].ToString();
							}
							if (dt.Columns.Contains("Rol"))
							{
								dtRol3 = dt.Rows[0]["Rol"].ToString();
							}
							if (dt.Columns.Contains("Nombre"))
							{
								dtNombre3 = dt.Rows[0]["Nombre"].ToString();
							}
							if (dt.Columns.Contains("edad"))
							{
								dtedad3 = dt.Rows[0]["edad"].ToString();
							}
							if (dt.Columns.Contains("score"))
							{
								dtscore3 = dt.Rows[0]["score"].ToString();
							}
							if (dt.Columns.Contains("decisionPrecalificado"))
							{
								dtdecisionPrecalificado3 = dt.Rows[0]["decisionPrecalificado"].ToString();
							}

							tblResultados.Rows.Add(dtCedula3,
									   dtRol3,
									   dtNombre3,
									   dtedad3,
									   dtscore3,
									   dtdecisionPrecalificado3,
									   vl_flags_3persona);

							if (p_accion == "AFTER_OF_PRECALIFICAR")
							{
								//Generando la bitacora de acceso a motor, una acceso por cada individuo                                               
								int vl_tipo_acceso = 0;
								Int32 vl_idBitacora = 0;
								Int32 vl_no_solicitud = 0;
								Int32 vl_appid = 0;
								try
								{
									vl_idBitacora = Int32.Parse(id_bitacora_busqueda = dt.Rows[0]["idBitacoraBusqueda"].ToString());
									vl_no_solicitud = Int32.Parse(txtNo_solicitud_coopsafa.Text);
									vl_appid = Int32.Parse(txtApplicationID.Text);

								}
								catch
								{

								}
								if (p_modio_ori_transunion == "CREAR")
								{
									vl_tipo_acceso = 1;
								}
								else
								{
									vl_tipo_acceso = 2;
								}
								//1 - Creacion de Solicitud, 2 Recalcular, 3 Obtencion de cuotas
								da.p_generar_bitacorabusqueda_tu(vl_idBitacora, vl_no_solicitud, vl_appid, vl_tipo_acceso);
							}
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

							string dtCedula4 = "";
							string dtRol4 = "";
							string dtNombre4 = "";
							string dtedad4 = "";
							string dtscore4 = "";
							string dtdecisionPrecalificado4 = "";
							string id_bitacora_busqueda = "";
							if (dt.Columns.Contains("Cedula"))
							{
								dtCedula4 = dt.Rows[0]["Cedula"].ToString();
							}
							if (dt.Columns.Contains("Rol"))
							{
								dtRol4 = dt.Rows[0]["Rol"].ToString();
							}
							if (dt.Columns.Contains("Nombre"))
							{
								dtNombre4 = dt.Rows[0]["Nombre"].ToString();
							}
							if (dt.Columns.Contains("edad"))
							{
								dtedad4 = dt.Rows[0]["edad"].ToString();
							}
							if (dt.Columns.Contains("score"))
							{
								dtscore4 = dt.Rows[0]["score"].ToString();
							}
							if (dt.Columns.Contains("decisionPrecalificado"))
							{
								dtdecisionPrecalificado4 = dt.Rows[0]["decisionPrecalificado"].ToString();
							}


							tblResultados.Rows.Add(dtCedula4,
									   dtRol4,
									   dtNombre4,
									   dtedad4,
									   dtscore4,
									   dtdecisionPrecalificado4,
									   vl_flags_4persona);

							if (p_accion == "AFTER_OF_PRECALIFICAR")
							{
								//Generando la bitacora de acceso a motor, una acceso por cada individuo                                               
								int vl_tipo_acceso = 0;
								Int32 vl_idBitacora = 0;
								Int32 vl_no_solicitud = 0;
								Int32 vl_appid = 0;
								try
								{
									vl_idBitacora = Int32.Parse(id_bitacora_busqueda = dt.Rows[0]["idBitacoraBusqueda"].ToString());
									vl_no_solicitud = Int32.Parse(txtNo_solicitud_coopsafa.Text);
									vl_appid = Int32.Parse(txtApplicationID.Text);

								}
								catch
								{

								}
								if (p_modio_ori_transunion == "CREAR")
								{
									vl_tipo_acceso = 1;
								}
								else
								{
									vl_tipo_acceso = 2;
								}
								//1 - Creacion de Solicitud, 2 Recalcular, 3 Obtencion de cuotas
								da.p_generar_bitacorabusqueda_tu(vl_idBitacora, vl_no_solicitud, vl_appid, vl_tipo_acceso);
							}
						}
					}

					if (gvResultado_buro.InvokeRequired)
						gvResultado_buro.Invoke(new MethodInvoker(() => gvResultado_buro.DataSource = tblResultados));
					else
						gvResultado_buro.DataSource = tblResultados;

				}

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		public bool IsVisible(TabPage tabPage)
		{
			if (tabPage.Parent == null)
				return false;
			else if (tabPage.Parent.Contains(tabPage))
				return true;
			else
				return false;
		}
		public void ShowPageInTabControl(TabPage tabPage, TabControl parent)
		{
			parent.TabPages.Add(tabPage);
		}

		public void HidePage(TabPage tabPage)
		{
			TabControl parent = (TabControl)tabPage.Parent;
			parent.TabPages.Remove(tabPage);
		}

		private void ocultar_tabs()
		{
			if (IsVisible(urlActualTransUnion))
			{
				HidePage(urlActualTransUnion);
			}
			if (IsVisible(tpDatosAfiliado))
			{
				HidePage(tpDatosAfiliado);
			}

			//if (IsVisible(tpVehPropiedades))
			//{
			//    HidePage(tpVehPropiedades);
			//}
			if (IsVisible(tpConyuge))
			{
				HidePage(tpConyuge);
			}
			if (IsVisible(tpCosolicitante))
			{
				HidePage(tpCosolicitante);
			}
			if (IsVisible(tpAval1))
			{
				HidePage(tpAval1);
			}
			if (IsVisible(tpAval2))
			{
				HidePage(tpAval2);
			}

			if (IsVisible(tpInfoFinan))
			{
				HidePage(tpInfoFinan);
			}
			if (IsVisible(tpResultadoBuro))
			{
				HidePage(tpResultadoBuro);
			}
			//FELVIR01 - 20190619
			if (IsVisible(tpGaranteHipotecario))
			{
				HidePage(tpGaranteHipotecario);
			}
		}

		private void mostrar_tab(TabPage tabPage)
		{
			ocultar_tabs();
			if (!IsVisible(tabPage))
			{
				ShowPageInTabControl(tabPage, tabControl1);
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
		private void p_deshabilitar_figuras_solicitud()
		{
			txtId_codeudor.Enabled = false;
			txtId_aval1.Enabled = false;
			txtId_aval2.Enabled = false;
		}
		private void p_calcular_cuota_nivelada()
		{
			float monto = 0;
			float tasa = 0;
			float plazo = 0;
			try
			{
				monto = float.Parse(txtMonto_solicitado.Text);
				tasa = float.Parse(txtTasa.Text);
				plazo = float.Parse(txtPlazo.Text);
			}
			catch
			{
				return;
			}
			if (monto == 0 | tasa == 0 | plazo == 0)
			{
				txtCuota_nivelada.Text = "0.00";
				return;
			}
			int vl_codigo_sub_app = 0;
			try
			{
				vl_codigo_sub_app = int.Parse(txtCodigo_sub_aplicacion.Text);
			}
			catch
			{
				MessageBox.Show("no se pudo obtener la cuota");
			}
			txtCuota_nivelada.Text = da.ObtenerCuotaNivela(vl_codigo_sub_app, monto, tasa, plazo).ToString("#,###,###,##0.00");
		}
		private void p_llenar_descripcion_garantia()
		{
			string aval1 = "";
			string aval2 = "";
			string vl_enter = "\r\n";
			if (cbAval1.Checked || !string.IsNullOrEmpty(txtId_aval1.Text))
			{
				aval1 = txtId_aval1.Text.Trim() + " " + txtPriNombre_tpAval1.Text.Trim() + " " + txtSegNombre_tpAval1.Text.Trim() + " " + txtPriApellido_tpAval1.Text.Trim() + " " + txtSegApellido_tpAval1.Text.Trim();
			}
			if (cbAval2.Checked || !string.IsNullOrEmpty(txtId_aval2.Text))
			{
				aval2 = txtId_aval2.Text.Trim() + " " + txtPriNombre_tpAval2.Text.Trim() + " " + txtSegNombre_tpAval2.Text.Trim() + " " + txtPriApellido_tpAval2.Text.Trim() + " " + txtSegApellido_tpAval2.Text.Trim();
			}

			txtDescripcion_garantia.Text = aval1 + vl_enter + aval2;
		}

		//FELVIR01
		private void p_modo_consulta()
		{
			txtCodigo_cliente.Enabled = false;
			txtIDSolicitante.Enabled = false;
			txtId_codeudor.Enabled = false;
			txtId_aval1.Enabled = false;
			txtId_aval2.Enabled = false;
			this.txtId_tpGarante.Enabled = false;
			btnInvocar_precalificado.Visible = false;
			string vl_PermitirModifAprobado = da.ObtenerPermitirModifAprobxSolicitud(Int32.Parse(txtNo_solicitud_coopsafa.Text));
			if (vl_PermitirModifAprobado == "S")
			{
				btnGuardar_solicitud.Visible = true;
				gmodo_coopsafa = "UPD";
				this.BloquearGarante(false);
			}
			else
			{
				btnGuardar_solicitud.Visible = false;
				this.BloquearGarante(true);
			}
		}

		private void p_tooltips_desc_producto(Control control_para_tooltips)
		{
			try
			{
				CR_TIPO_PRESTAMO.da = this.da;
				var configuracionproducto = CR_TIPO_PRESTAMO.obtenerTipoPrestamo(Int16.Parse(txtCodigo_sub_aplicacion.Text));

				// Create the ToolTip and associate with the Form container.
				ToolTip toolTip1 = new ToolTip();

				toolTip1.AutoPopDelay = 25000;
				toolTip1.InitialDelay = 100;
				toolTip1.ReshowDelay = 100;
				toolTip1.ToolTipTitle = "Parametrización del Producto";
				toolTip1.UseFading = true;
				toolTip1.ToolTipIcon = ToolTipIcon.Info;

				toolTip1.ShowAlways = true;
				string texto = Environment.NewLine
							 + $"Producto : {configuracionproducto.desc_tipopres}" + Environment.NewLine
							 + $"Plazo Min : {configuracionproducto.Num_mesesplazo_min}  Plazo Max : {configuracionproducto.Num_mesesplazo} " + Environment.NewLine
							 + $"Tasa Min  : {configuracionproducto.Por_tasaminima}%    Tasa Max  : { configuracionproducto.Por_tasamaxima}%" + Environment.NewLine
							 + "──────────────────────────────────────────" + Environment.NewLine
							 + $"Valores por omisión : Plazo {configuracionproducto.Num_mesesplazo_base}    Tasa {configuracionproducto.Por_tasaomision}%  ";

				// Set up the ToolTip text for the Button and Checkbox.
				toolTip1.SetToolTip(control_para_tooltips, texto);
			}
			catch
			{

			}






		}

		#region Eventos
		private void TimeBar_Tick(object sender, EventArgs e)
		{
			labelRelojPanel.Text = DateTime.Now.ToString("hh:mm");
			labelDiaPanel.Text = DateTime.Now.ToString("dddd");
			labelFechaPanel.Text = DateTime.Now.ToString("MMMM") + " " + DateTime.Now.ToString("dd");
		}
		private void comboBox_sub_aplicacion_SelectionChangeCommitted(object sender, EventArgs e)
		{
			int vl_codigo_sub_aplicacion = 0;
			txtCodigo_sub_aplicacion.Text = comboBox_sub_aplicacion.SelectedValue.ToString();
			int.TryParse(txtCodigo_sub_aplicacion.Text, out vl_codigo_sub_aplicacion);

			string vl_codigo_moneda = "";
			string vl_descripcion_moneda = "";
			string vl_sigla_moneda = "";
			da.ObtenerMonedaxSubAplicacion(txtCodigo_sub_aplicacion.Text, out vl_codigo_moneda, out vl_descripcion_moneda, out vl_sigla_moneda);
			txtCodigo_moneda.Text = vl_codigo_moneda;
			txtDesc_moneda.Text = vl_descripcion_moneda;
			txtMoneda_abreviatura.Text = vl_sigla_moneda;

			p_tooltips_desc_producto(comboBox_sub_aplicacion);

		}
		private void btnDatosAfiliado_Click(object sender, EventArgs e)
		{
			mostrar_tab(tpDatosAfiliado);
		}
		private void btnDatosCredito_Click(object sender, EventArgs e)
		{

			mostrar_tab(urlActualTransUnion);
			cmbDestino_credito.SelectedValue = gdestino_credito_id;
			cmbFondos.SelectedValue = gfuente_financiamiento;


		}
		private void btnAval1_Click(object sender, EventArgs e)
		{
			mostrar_tab(tpAval1);
		}
		private void btnAval2_Click(object sender, EventArgs e)
		{
			mostrar_tab(tpAval2);
		}
		private void btnVehpropiedades_Click(object sender, EventArgs e)
		{
			//mostrar_tab(tpVehPropiedades);
		}
		private void btnDatosConyuge_Click(object sender, EventArgs e)
		{
			mostrar_tab(tpConyuge);
		}
		private void brnResultadoBuro_Click(object sender, EventArgs e)
		{
			mostrar_tab(tpResultadoBuro);
			labelProducto255.Text = "Producto: (" + txtCodigo_sub_aplicacion.Text + ")";
			int vl_codigo_sub_app = 0;
			try
			{
				vl_codigo_sub_app = int.Parse(txtCodigo_sub_aplicacion.Text);
				txtProducto_resumen.Text = da.ObtenerDescripcionSubApplicacion(vl_codigo_sub_app);
			}
			catch
			{
				txtProducto_resumen.Text = "";
			}


			string resultado_consulta = da.ObtenerXmlRespuestaEvaluacion(Int32.Parse(txtNo_solicitud_coopsafa.Text));
			procesar_xml_respuesta(resultado_consulta, "CONSULTA", "");
			//--------------------------------
			string vl_controla_recalculos = da.ObtenerParametro("WFC-0006");
			if (vl_controla_recalculos == "S")
			{
				int vl_recalculos_actuales = 0;
				int vl_recalculos_permitidos = 0;
				da.ObtenerSituacionVecesPrecalifica(Int32.Parse(txtNo_solicitud_coopsafa.Text), out vl_recalculos_actuales, out vl_recalculos_permitidos);
				labelRecalculos.Text = vl_recalculos_actuales.ToString().Trim() + "/" + vl_recalculos_permitidos.ToString().Trim();
				txtRecalculos_actuales.Text = vl_recalculos_actuales.ToString();
				txtRecalculos_permitidos.Text = vl_recalculos_permitidos.ToString();
			}

		}
		private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
		{
			// This event is called once for each tab button in your tab control

			// First paint the background with a color based on the current tab

			// e.Index is the index of the tab in the TabPages collection.
			switch (e.Index)
			{
				case 0:
					e.Graphics.FillRectangle(new SolidBrush(Color.Red), e.Bounds);
					break;
				case 1:
					e.Graphics.FillRectangle(new SolidBrush(Color.Blue), e.Bounds);
					break;
				default:
					break;
			}

			// Then draw the current tab button text 
			Rectangle paddedBounds = e.Bounds;
			paddedBounds.Inflate(-2, -2);
			e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, this.Font, SystemBrushes.HighlightText, paddedBounds);
		}
		private void button8_Click(object sender, EventArgs e)
		{
			string vl_controla_recalculos = da.ObtenerParametro("WFC-0006");
			string vl_controlar_antiguedad_tu = da.ObtenerParametro("WFC-0014");
			if (vl_controlar_antiguedad_tu == "S")
			{
				int maximo_antiguedad_sol_tu = 0;
				try
				{
					maximo_antiguedad_sol_tu = int.Parse(da.ObtenerParametro("WFC-0015"));
				}
				catch
				{
					maximo_antiguedad_sol_tu = 0;
				}
				int antiguedad_sol_tu = da.ObtenerAntiguedadSolicTU(Int32.Parse(txtNo_solicitud_coopsafa.Text));
				if (antiguedad_sol_tu > maximo_antiguedad_sol_tu)
				{
					MessageBox.Show("la solicitud no.  " + txtNo_solicitud_coopsafa.Text + " tiene  " + antiguedad_sol_tu.ToString().Trim() + " dias de antiguedad, el limite de dias de antiguedad es de " + maximo_antiguedad_sol_tu.ToString().Trim() + " dias, ya no se puede obtener cuotas sobre esta solicitud !", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}


			}

			myThread = new Thread(new ThreadStart(p_obtener_detalle_cuotas_en_buro_solicitante));
			myThread.Start();
		}
		private void cbConsolidacion_CheckedChanged(object sender, EventArgs e)
		{
			if (cbConsolidacion.Checked)
				btnObtener_cuotas_buro.Enabled = true;
			else
				btnObtener_cuotas_buro.Enabled = false;
		}
		private void btnSumar_consolidacion_Click(object sender, EventArgs e)
		{
			calcular_cuotas_consolo(0);
		}

		private void btnInvocar_precalificado_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtNo_solicitud_coopsafa.Text))
			{
				MessageBox.Show("Antes de precalificar a los postulantes del credito debe primero guardar la solicitud !", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (txtNo_solicitud_coopsafa.Text == "0")
			{
				MessageBox.Show("Antes de precalificar a los postulantes del credito debe primero guardar la solicitud !", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (da.ObtenerEsAnalizableProductoxCodigo(Int32.Parse(txtCodigo_sub_aplicacion.Text)) == "N")
			{
				MessageBox.Show("Este producto no requiere analisis de Buro..!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (!p_valida_solicitud())
			{
				return;
			}
			if (gmodo_coopsafa != "UPD")
			{
				MessageBox.Show("Antes de precalificar a los postulantes del credito debe primero guardar la solicitud !", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (MessageBox.Show("Desea enviar a precalificar el solicitante el solicitante ?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				return;
			if (gmodo_transunion == "RECALCULAR")
			{
				string vl_controla_recalculos = da.ObtenerParametro("WFC-0006");
				string vl_controlar_antiguedad_tu = da.ObtenerParametro("WFC-0014");
				if (vl_controlar_antiguedad_tu == "S")
				{
					int maximo_antiguedad_sol_tu = 0;
					try
					{
						maximo_antiguedad_sol_tu = int.Parse(da.ObtenerParametro("WFC-0015"));
					}
					catch
					{
						maximo_antiguedad_sol_tu = 0;
					}
					int antiguedad_sol_tu = da.ObtenerAntiguedadSolicTU(Int32.Parse(txtNo_solicitud_coopsafa.Text));
					if (antiguedad_sol_tu > maximo_antiguedad_sol_tu)
					{
						MessageBox.Show("la solicitud no.  " + txtNo_solicitud_coopsafa.Text + " tiene  " + antiguedad_sol_tu.ToString().Trim() + " dias de antiguedad, el limite de dias de antiguedad es de " + maximo_antiguedad_sol_tu.ToString().Trim() + " dias, ya no se puede recalcular este solicitud !", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
				}

				if (vl_controla_recalculos == "S")
				{
					int vl_recalculos_actuales = 0;
					int vl_recalculos_permitidos = 0;
					da.ObtenerSituacionVecesPrecalifica(Int32.Parse(txtNo_solicitud_coopsafa.Text), out vl_recalculos_actuales, out vl_recalculos_permitidos);
					if (vl_recalculos_actuales >= vl_recalculos_permitidos)
					{
						MessageBox.Show("Ha llegado al limite de recalculos permitidos para esta solicitud, maximo recalculos permitidos para esta solicitud son de " + vl_recalculos_permitidos.ToString(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
				}
			}
			txtObs_solicitud.Text = "";
			txtErrores_trans.Text = "";
			txtDecision_solicitud.Text = "";
			myThread = new Thread(new ThreadStart(p_invocar_precalificado));
			myThread.Start();
		}
		private void gvCuotasBuro_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			gvCuotasBuro.CommitEdit(DataGridViewDataErrorContexts.Commit);
		}
		private void gvCuotasBuro_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				var tempo = gvCuotasBuro.Rows[e.RowIndex].Cells[0].Value;
				calcular_cuotas_consolo(0);
			}
		}
		private void btnCosolicitante_Click(object sender, EventArgs e)
		{
			mostrar_tab(tpCosolicitante);
		}
		private void btnInfoFinan_Click(object sender, EventArgs e)
		{
			p_actualizar_info_financiera();
			mostrar_tab(tpInfoFinan);
		}
		private void button9_Click(object sender, EventArgs e)
		{
			if (retornar_ok)
			{
				this.DialogResult = DialogResult.OK;
			}
			else
			{
				this.Close();
			}
		}
		private void btnActualizar_datosfinan_Click(object sender, EventArgs e)
		{
			p_actualizar_info_financiera();
			p_construir_xml_referencias();

		}
		private void gvInfoFinanciera_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			//este codigo es para cuando doy click en el boton que tenia el grid
			//para obtener las cuotas en buro de todos los que estaban en este grid
			gvInfoFinanciera.CommitEdit(DataGridViewDataErrorContexts.Commit);
			var senderGrid = (DataGridView)sender;

			if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
				e.RowIndex >= 0)
			{
				string sid = "";
				try
				{
					sid = gvInfoFinanciera.Rows[e.RowIndex].Cells[2].Value.ToString();
					double monto = p_obtener_suma_cuotas_en_buroxID(sid, "VENTANILLA");

					DataRow row = dtInfoFinan.Select("rol='Solicitante'").FirstOrDefault();
					if (row != null)
					{
						row["otros_ingresos"] = monto.ToString();
						//row["cuotas_buro"] = monto.ToString();
					}

				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}
		private void gvInfoFinanciera_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (e != null)
			{
				if (e.RowIndex >= 0)
				{
					string sname = gvInfoFinanciera.Rows[e.RowIndex].Cells["NombreTpInfoFinant2"].Value.ToString();
					string srol = gvInfoFinanciera.Rows[e.RowIndex].Cells["rolTpInfoFinan"].Value.ToString();
					string codigo_cliente = gvInfoFinanciera.Rows[e.RowIndex].Cells["codigo_cliente"].Value.ToString();
					string tempo = null;
					string ingresos = gvInfoFinanciera.Rows[e.RowIndex].Cells["ingresos"].Value.ToString() ?? "0";
					string otros_ingresos = gvInfoFinanciera.Rows[e.RowIndex].Cells["otros_ingresos"].Value.ToString() ?? "0";
					string deducciones = gvInfoFinanciera.Rows[e.RowIndex].Cells["deducciones"].Value.ToString() ?? "0";
					string aportaciones = gvInfoFinanciera.Rows[e.RowIndex].Cells["aportaciones"].Value.ToString() ?? "0";
					string estado_buro = gvInfoFinanciera.Rows[e.RowIndex].Cells["estado_buro_interno"].Value.ToString();
					string obs_buro = gvInfoFinanciera.Rows[e.RowIndex].Cells["Observaciones_buro"].Value.ToString();

					labelRol.Text = srol;
					txtCodigo_cliente_tpDatosFinanc.Text = codigo_cliente;
					txtNombre_tpDatosFinanc.Text = sname;
					txtIngresos_tpDatosFinanc.Text = ingresos;
					txtOtros_ingresos_tpDatosFinanc.Text = otros_ingresos;
					txtDeducciones_tpDatosFinanc.Text = deducciones;
					txtAportaciones_tpDatosFinanc.Text = aportaciones;
					txtEstado_buro_interno.Text = estado_buro;
					txtObservaciones_buro_interno.Text = obs_buro;
					try
					{
						txtIngresos_netos_tpDatosFinanc.Text = ((decimal.Parse(ingresos) + decimal.Parse(otros_ingresos)) - decimal.Parse(deducciones)).ToString();
					}
					catch
					{
					}

				}
			}
		}
		private void gvInfoFinanciera_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			gvInfoFinanciera_CellValueChanged(sender, e);
		}
		private void rbFemenino_tpConyuge_CheckedChanged(object sender, EventArgs e)
		{
			if (rbFemenino_tpConyuge.Checked)
			{
				lblDependen_tpConyuge.Text = "Personas que dependen de ella :";
			}
		}
		private void rbMasculino_tpConyuge_CheckedChanged(object sender, EventArgs e)
		{
			if (rbMasculino_tpConyuge.Checked)
			{
				lblDependen_tpConyuge.Text = "Personas que dependen de el :";
			}
		}
		private void button5_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtNo_solicitud_coopsafa.Text))
			{
				return;
			}
			if (txtNo_solicitud_coopsafa.Text == "0")
			{
				return;
			}

			FrmsRpts.frmRpt_solicitud forma = new FrmsRpts.frmRpt_solicitud(da, Int32.Parse(txtNo_solicitud_coopsafa.Text));
			forma.ShowDialog();
		}
		private void btnAval12_Click(object sender, EventArgs e)
		{
			mostrar_tab(tpAval1);
		}
		private void btnAval22_Click(object sender, EventArgs e)
		{
			mostrar_tab(tpAval2);
		}
		private void btnCodeudor_Click(object sender, EventArgs e)
		{
			mostrar_tab(tpCosolicitante);
		}
		private void btnPrincipal_Click(object sender, EventArgs e)
		{
			mostrar_tab(tpDatosAfiliado);
		}
		private void btnConyuge_Click(object sender, EventArgs e)
		{
			mostrar_tab(tpConyuge);
		}
		private void label15_Click(object sender, EventArgs e)
		{


		}
		private void label101_Click(object sender, EventArgs e)
		{




		}
		private void lnkTablaresultado_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			string resultado_consulta = da.ObtenerXmlRespuestaEvaluacion(Int32.Parse(txtNo_solicitud_coopsafa.Text));
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
		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (string.IsNullOrEmpty(gxml_respuesta))
			{
				MessageBox.Show("No hay xml de respuesta, probablemente no se ha enviado a precalificar la solicitud de crédito ..!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			xmlViewer forma = new xmlViewer(gxml_respuesta);
			forma.ShowDialog();


		}

		//FELVIR01 - 20190619
		private void btnGuardar_solicitud_Click(object sender, EventArgs e)
		{
			if (!p_valida_solicitud())
			{
				return;
			}

			string vl_enter = "\r\n";
			if (gmodo_coopsafa == "INS")
			{
				if (MessageBox.Show("Desea guardar la solicitud siguiente :            " + vl_enter + vl_enter +
								" Producto " + comboBox_sub_aplicacion.Text + vl_enter +
								" Monto " + txtMonto_solicitado.Text + vl_enter +
								" Plazo " + txtPlazo.Text + vl_enter +
								" Tasa  " + txtTasa.Text, "Aviso de Confirmación ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				{
					return;
				}
			}

			decimal vl_monto_solicitado = 0;
			decimal.TryParse(txtMonto_solicitado.Text, out vl_monto_solicitado);
			int vl_meses_plazo = 0;
			int.TryParse(txtPlazo.Text, out vl_meses_plazo);

			string vl_es_consolidacion = "False";
			if (cbConsolidacion.Checked)
			{
				vl_es_consolidacion = "True";
			}

			p_construir_xml_cuotasburo_selec(); //recostruye el xml para guardalo con las cuotas que seleccione el usuario.


			//decimal vl_ingresos_conyuge = 0;
			//decimal vl_otros_ingresos_conyuge = 0;
			//decimal vl_deducciones_conyuge = 0;

			//decimal vl_ingresos_codeudor = 0;
			//decimal vl_otros_ingresos_codeudor = 0;
			//decimal vl_deducciones_codeudor = 0;

			//decimal vl_ingresos_aval1 = 0;
			//decimal vl_otros_ingresos_aval1 = 0;
			//decimal vl_deducciones_aval1 = 0;

			//decimal vl_ingresos_aval2 = 0;
			//decimal vl_otros_ingresos_aval2 = 0;
			//decimal vl_deducciones_aval2 = 0;

			solicitud_credito sol = new solicitud_credito();

			#region Generales
			sol.workflow_id = 2;
			sol.usuario_workflow = goficial_servicio;
			sol.codigo_agencia = Convert.ToInt32(gcodigo_agencia);
			sol.no_solicitud = Convert.ToInt32(txtNo_solicitud_coopsafa.Text);
			sol.codigo_sub_aplicacion = Convert.ToInt16(txtCodigo_sub_aplicacion.Text);
			sol.fuente_financiamiento = Convert.ToInt16(txtCodigo_fuente_fondos.Text);
			sol.codigo_moneda = Convert.ToInt16(txtCodigo_moneda.Text);
			sol.monto_solicitado = Convert.ToDecimal(txtMonto_solicitado.Text);
			sol.plazo = Convert.ToInt16(txtPlazo.Text);

			sol.tasa = Convert.ToDecimal(txtTasa.Text);

			sol.destino_credito = txtDestino_credito.Text;
			sol.descripcion_garantia = txtDescripcion_garantia.Text.Trim();
			sol.condicion_vehiculo = "";
			if (rbAutoNuevo.Checked)
				sol.condicion_vehiculo = "N";

			if (rbAutoUsado.Checked)
				sol.condicion_vehiculo = "U";
			sol.valor_vehiculo = Convert.ToDecimal(txtValor_vehiculo.Text);
			sol.modo_transunion = gmodo_transunion;

			sol.application_id = Int32.Parse(txtApplicationID.Text);
			if (cbConsolidacion.Checked)
			{
				sol.es_consolidacion = "T";
			}
			else
				sol.es_consolidacion = "F";

			sol.xml_cuotas_buro = gxml_cuotas_buro;
			sol.monto_cuota_consolidar = Convert.ToDecimal(txtTotal_cuotas_consolidar.Text);
			sol.monto_balance_consolidar = Convert.ToDecimal(txtTotal_capital_consolidar.Text);
			sol.destino_credito = txtDestino_credito.Text;

			if (cbCodeudor.Checked)
				sol.requiere_codeudor = "S";
			else
				sol.requiere_codeudor = "N";

			if (cbAval1.Checked)
				sol.requiere_aval1 = "S";
			else
				sol.requiere_aval1 = "N";


			if (cbAval2.Checked)
				sol.requiere_aval2 = "S";
			else
				sol.requiere_aval2 = "N";
			sol.derecho_ganado = decimal.Parse(txtDerechoGanado.Text);
			sol.monto_cuotas_vencimiento = decimal.Parse(txtMonto_cuotas_vencimiento_sol.Text);
			sol.complemento_aportaciones = decimal.Parse(txtComplemento_aportaciones.Text);
			sol.deudas_canceladas_solic = decimal.Parse(txtDeudasCancelados_solic.Text);
			sol.deudas_canceladas_codeud = decimal.Parse(txtDeudasCancelados_codeud.Text);
			sol.deudas_canceladas_aval1 = decimal.Parse(txtDeudasCancelados_aval1.Text);
			sol.deudas_canceladas_aval2 = decimal.Parse(txtDeudasCancelados_aval2.Text);
			//FELVIR01
			sol.MontoCuotasVencCodeudor = 0;
			sol.MontoCuotasVencAval1 = 0;
			sol.MontoCuotasVencAval2 = 0;

			sol.DeudasDescSol = 0;//Convert.ToDecimal(this.txtDeudas_desc_sol.Text);
			sol.DeudasDescCod = 0;
			sol.DeudasDescAval1 = 0;
			sol.DeudasDescAval2 = 0;

			#endregion

			//FELVIR01 - 20190619
			#region Solicitante

			sol.no_identificacion = txtIDSolicitante.Text;
			sol.codigo_cliente = Convert.ToInt32(txtCodigo_cliente.Text);
			sol.nombres = txtNombre.Text;
			sol.primer_apellido = txtPrimer_apellido.Text;
			sol.segundo_apellido = txtSegundo_apellido.Text;
			sol.nacionalidad = txtNacionalidad.Text;
			sol.lugar_nacimiento = txtLugar_nacimiento.Text;
			sol.estado_civil = this.txtEstado_civil.Text;
			sol.apellido_casada = this.txtApellido_casada.Text;
			sol.nivel_educativo = string.Empty;
			if (rbNePrimaria.Checked)
			{
				sol.nivel_educativo = "P";
			}
			if (rbNeSecundaria.Checked)
			{
				sol.nivel_educativo = "S";
			}
			if (rbNeUniversitaria.Checked)
			{
				sol.nivel_educativo = "U";
			}
			if (rbNePostgrado.Checked)
			{
				sol.nivel_educativo = "G";
			}
			sol.profesion_oficio = txtProfesion_oficio.Text;
			sol.tipo_vivienda = string.Empty;
			if (rbTvPropia.Checked)
			{
				sol.tipo_vivienda = "PROPIA";
			}
			if (rbTvPagandola.Checked)
			{
				sol.tipo_vivienda = "APLAZOS";
			}
			if (rbTvFamiliar.Checked)
			{
				sol.tipo_vivienda = "FAMILIAR";
			}
			if (rbTvAlquilada.Checked)
			{
				sol.tipo_vivienda = "ALQUILADA";
			}

			if (rbTvOtros.Checked)
			{
				sol.tipo_vivienda = "OTROS";
			}

			sol.tipo_vivienda_especificar = txtTipo_vivienda_especificar.Text;
			sol.direccion_residencia = txtDireccion_res.Text;
			sol.telefono_fijo = txtTelefono_fijo.Text;
			sol.telefono_celular = txtCelular.Text;
			sol.telefono_adicional1 = txtTelAdicional1.Text;
			sol.telefono_adicional2 = txtTelAdicional2.Text;
			sol.correo_electronico = txtCorreo_personal.Text;
			if (string.IsNullOrEmpty(txtNoHijos.Text))
				this.txtNoHijos.Text = "0";
			sol.dependientes_hijos = Convert.ToInt16(txtNoHijos.Text);
			if (string.IsNullOrEmpty(txtOtrospariente.Text))
				this.txtOtrospariente.Text = "0";
			sol.dependientes_otros = Convert.ToInt16(txtOtrospariente.Text);
			sol.tipo_empresa = string.Empty;
			if (rbTe_publico.Checked)
				sol.tipo_empresa = "Publico";
			if (rbTe_privado.Checked)
				sol.tipo_empresa = "Privado";
			if (rbTe_comerciante.Checked)
				sol.tipo_empresa = "Comerciante";
			if (rbTe_otros.Checked)
				sol.tipo_empresa = "Otros";
			sol.tipo_empresa_especificar = txtTipo_empresa_especificar.Text;
			sol.patrono = txtPatrono.Text;
			sol.depto_labora = txtDepto_labora.Text;
			sol.cargo = txtCargo.Text;
			sol.antiguedad_laboral = txtAntiguedad_laboral.Text;
			sol.FechaIngresoLaboral = this.txtFechaIngresoLaboral.Text;
			sol.ingresos = Convert.ToDecimal(txtIngresos.Text);
			sol.otros_ingresos = Convert.ToDecimal(txtOtros_ingresos.Text);
			sol.deducciones = Convert.ToDecimal(txtDeducciones.Text);
			sol.telefono_laboral1 = txtTelefono_trabajo1.Text;
			sol.ext_laboral1 = txtExt1_trabajo.Text;
			sol.telefono_laboral2 = txtTelefono_trabajo2.Text;
			sol.ext_laboral2 = txtExt2_trabajo.Text;
			sol.direccion_laboral = txtDireccion_lab.Text;
			sol.correo_laboral = txtCorreo_laboral.Text;
			if (!string.IsNullOrEmpty(this.txtEdad_presta.Text))
			{
				sol.EdadPresta = Convert.ToInt32(this.txtEdad_presta.Text);
			}
			sol.RTN = this.txtRtnSolicitante.Text;
			sol.sexo = this.txtSexo.Text;
			sol.estado_civil = this.txtEstado_civil.Text;

			#endregion

			#region Datos Conyuge

			sol.no_identificacion_conyuge = txtId_conyuge.Text;
			sol.primer_nombre_conyuge = txtPriNombre_tpConyuge.Text;
			sol.segundo_nombre_conyuge = txtSegNombre_tpConyuge.Text;
			sol.primer_apellido_conyuge = txtPriApellido_tpConyuge.Text;
			sol.segundo_apellido_conyuge = txtSegApellido_tpConyuge.Text;
			if (rbMasculino_tpConyuge.Checked)
				sol.sexo_conyuge = "M";
			if (rbFemenino_tpConyuge.Checked)
				sol.sexo_conyuge = "F";

			if (string.IsNullOrEmpty(txtNoHijos_tpConyuge.Text))
			{
				txtNoHijos_tpConyuge.Text = "0";
			}

			if (string.IsNullOrEmpty(txtOtrospariente_tpConyuge.Text))
			{
				txtNoHijos_tpConyuge.Text = "0";
			}

			sol.dependientes_hijos_conyuge = Convert.ToInt16(txtNoHijos_tpConyuge.Text);
			sol.dependientes_otros_conyuge = Convert.ToInt16(txtOtrospariente_tpConyuge.Text);
			sol.direccion_residencia_conyuge = txtDirecc_res_tpConyuge.Text;
			sol.telefono_fijo_conyuge = txtTelefono_tpConyuge.Text;
			sol.celular_conyuge = txtCelular_tpConyuge.Text;
			sol.telefono_adicional1_conyuge = txtOtrotelefono1_tpConyuge.Text;
			sol.telefono_adicional2_conyuge = txtOtrotelefono2_tpConyuge.Text;
			sol.correo_conyuge = txtCorreo_tpConyuge.Text;
			if (rbSiAfiliado_tpConyuge.Checked)
				sol.es_afiliado_conyuge = "S";
			else
				sol.es_afiliado_conyuge = "N";
			sol.codigo_cliente_conyuge = txtCodigo_cliente_TpConyuge.Text;

			sol.tipo_empresa_conyuge = string.Empty;
			if (rbTePrivado_tpConyuge.Checked)
				sol.tipo_empresa_conyuge = "Privado";
			if (rbTePublico_tpConyuge.Checked)
				sol.tipo_empresa_conyuge = "Publico";
			if (rbTeComerciante_tpConyuge.Checked)
				sol.tipo_empresa_conyuge = "Comerciante";
			if (rbTeOtros_tpConyuge.Checked)
				sol.tipo_empresa_conyuge = "Otros";

			sol.tipo_empresa_especificar_conyuge = txtTipoEmpresaotros_tpCoyuge.Text;
			sol.patrono_conyuge = txtPatrono_tpConyuge.Text;
			sol.depto_labora_conyuge = txtDeptolabora_tpConyuge.Text;
			sol.cargo_conyuge = txtCargo_tpConyuge.Text;
			sol.antiguedad_conyuge = txtAntiglaboral_tpConyuge.Text;
			sol.ingresos_conyuge = Convert.ToDecimal(txtIngresos_tpConyuge.Text);
			sol.otros_ingresos_conyuge = Convert.ToDecimal(txtOtrosIngresos_tpConyuge.Text);
			sol.deducciones_conyuge = Convert.ToDecimal(txtDeducciones_tpConyuge.Text);
			sol.telefono_laboral1_conyuge = txtTellaboral1_tpConyuge.Text;
			sol.ext_laboral1_conyuge = txtExtlaboral1_tpConyuge.Text;
			sol.telefono_laboral2_conyuge = txtTellaboral2_tpConyuge.Text;
			sol.ext_laboral2_conyuge = txtExtlaboral2_tpConyuge.Text;
			sol.direccion_laboral_conyuge = txtDirecclaboral_tpConyuge.Text;
			sol.correo_laboral_conyuge = txtCorreolaboral_tpConyuge.Text;

			sol.NivelEducConyuge = string.Empty;
			if (this.rbPrimario_Conyuge.Checked)
			{
				sol.NivelEducConyuge = "P";
			}
			if (this.rbSecundaria_Conyuge.Checked)
			{
				sol.NivelEducConyuge = "S";
			}
			if (this.rbUniversitario_Conyuge.Checked)
			{
				sol.NivelEducConyuge = "U";
			}
			if (this.rbPostGrado_conyuge.Checked)
			{
				sol.NivelEducConyuge = "G";
			}

			sol.ProfesionConyuge = this.txtProfesionCoyuge.Text;
			sol.TipoContrato = this.txtTipoContrato.Text;

			#endregion

			#region Datos Codeudor

			sol.no_identificacion_codeudor = txtId_codeudor.Text;
			sol.primer_nombre_codeudor = txtPriNombre_tpCodeudor.Text;
			sol.segundo_nombre_codeudor = txtSegNombre_tpCodeudor.Text;
			sol.primer_apellido_codeudor = txtPriApellido_tpCodeudor.Text;
			sol.segundo_apellido_codeudor = txtSegApellido_tpCodeudor.Text;
			string vl_sexo_codeudor = "";
			if (rbMasculino_tpCodeudor.Checked)
				vl_sexo_codeudor = "M";
			if (rbFemenino_tpCodeudor.Checked)
				vl_sexo_codeudor = "F";
			sol.sexo_codeudor = vl_sexo_codeudor;

			sol.dependientes_hijos_codeudor = Convert.ToInt16(txtNoHijos_tpCodeudor.Text);
			sol.dependientes_otros_codeudor = Convert.ToInt16(txtOtrospariente_tpCodeudor.Text);
			sol.direccion_residencia_codeudor = txtDirecc_res_tpCodeudor.Text;
			sol.telefono_fijo_codeudor = txtTelefono_tpCodeudor.Text;
			sol.celular_codeudor = txtCelular_tpCodeudor.Text;
			sol.telefono_adicional1_codeudor = txtOtrotelefono1_tpCodeudor.Text;
			sol.telefono_adicional2_codeudor = txtOtrotelefono2_tpCodeudor.Text;
			sol.correo_codeudor = txtCorreo_tpCodeudor.Text;

			string vl_es_afiliado_codeudor = "";
			if (rbSiAfiliado_tpCodeudor.Checked)
				vl_es_afiliado_codeudor = "S";
			else
				vl_es_afiliado_codeudor = "N";

			sol.es_afiliado_codeudor = vl_es_afiliado_codeudor;
			sol.codigo_cliente_codeudor = txtCodigo_cliente_TpCodeudor.Text;

			string vl_tipo_empresa_codeudor = string.Empty;
			if (rbTePrivado_tpCodeudor.Checked)
				vl_tipo_empresa_codeudor = "Privador";
			if (rbTePublico_tpCodeudor.Checked)
				vl_tipo_empresa_codeudor = "Publico";
			if (rbTeComerciante_tpCodeudor.Checked)
				vl_tipo_empresa_codeudor = "Comerciante";
			if (rbTeOtros_tpCodeudor.Checked)
				vl_tipo_empresa_codeudor = "Otros";

			sol.tipo_empresa_codeudor = vl_tipo_empresa_codeudor;
			sol.tipo_empresa_especificar_codeudor = txtTipoEmpresaotros_tpCodeudor.Text;
			sol.patrono_codeudor = txtPatrono_tpCodeudor.Text;
			sol.depto_labora_codeudor = txtDeptolabora_tpCodeudor.Text;
			sol.cargo_codeudor = txtCargo_tpCodeudor.Text;
			sol.antiguedad_codeudor = txtAntiglaboral_tpCodeudor.Text;
			sol.ingresos_codeudor = Convert.ToDecimal(txtIngresos_tpCodeudor.Text);
			sol.otros_ingresos_codeudor = Convert.ToDecimal(txtOtrosIngresos_tpCodeudor.Text);
			sol.deducciones_codeudor = Convert.ToDecimal(txtDeducciones_tpCodeudor.Text);
			sol.telefono_laboral1_codeudor = txtTellaboral1_tpCodeudor.Text;
			sol.ext_laboral1_codeudor = txtExtlaboral1_tpCodeudor.Text;
			sol.telefono_laboral2_codeudor = txtTellaboral2_tpCodeudor.Text;
			sol.ext_laboral2_codeudor = txtExtlaboral2_tpCodeudor.Text;
			sol.direccion_laboral_codeudor = txtDirecclaboral_tpCodeudor.Text;
			sol.correo_laboral_codeudor = txtCorreolaboral_tpCodeudor.Text;
			sol.nombre_conyuge_codeudor = txtNombre_conyuge_tpCodeudor.Text;
			sol.direclab_conyuge_codeudor = txtDirelab_conyuge_tpCodeudor.Text;
			sol.cargo_conyuge_codeudor = txtCargo_conyuge_tpCodeudor.Text;
			if (string.IsNullOrEmpty(this.txtEdad_tpCodeudor.Text))
				this.txtEdad_tpCodeudor.Text = "0";
			sol.EdadCodeudor = Convert.ToInt32(this.txtEdad_tpCodeudor.Text);

			sol.TipoViviendaCodeudor = string.Empty;
			if (this.rbTp_PropiaCodeudor.Checked)
			{
				sol.TipoViviendaCodeudor = "PROPIA";
			}
			if (this.rbTp_PagandoCodeudor.Checked)
			{
				sol.TipoViviendaCodeudor = "APLAZOS";
			}
			if (this.rbTp_FamiliarCodeudor.Checked)
			{
				sol.TipoViviendaCodeudor = "FAMILIAR";
			}
			if (this.rbTp_AlquiladaCodeudor.Checked)
			{
				sol.TipoViviendaCodeudor = "ALQUILADA";
			}
			if (this.rbTp_OtrosCodeudor.Checked)
			{
				sol.TipoViviendaCodeudor = "OTROS";
			}

			sol.DescripcionViviendaCodeudor = this.txtViviendaOtros_tpCod.Text;
			sol.EstadoCivilCodeudor = this.txtEstadoCivil_tpCodeudor.Text;
			sol.RTN_Codeudor = this.txtRtn_Codeurdor.Text;
			sol.FechaIngresoLaboralCodeudor = this.txtIngresoLab_Co.Text;

			#region referencias del CoDeudor
			LstReferencias_codeudor.Clear();
			//1
			referencias_solicitud referen1_codeud = new referencias_solicitud();
			referen1_codeud.no_referencia = 1;
			referen1_codeud.referencia_de = sol.no_identificacion_codeudor;
			referen1_codeud.nombre = txtRef1_tpCodeudor.Text;
			referen1_codeud.direccion = txtRef1_direc_tpCodeudor.Text;
			referen1_codeud.telefono_fijo = txtRef1_telef_tpCodeudor.Text;
			referen1_codeud.telefono_celular = "";
			referen1_codeud.punto_referencia = txtRef1_ptoref_tpCodeudor.Text;
			referen1_codeud.casa_color = txtRef1_casacolor_tpCodeudor.Text;
			referen1_codeud.bloque = "";
			LstReferencias_codeudor.Add(referen1_codeud);
			//2
			referencias_solicitud referen2_codeud = new referencias_solicitud();
			referen2_codeud.no_referencia = 2;
			referen2_codeud.referencia_de = sol.no_identificacion_codeudor;
			referen2_codeud.nombre = txtRef2_tpCodeudor.Text;
			referen2_codeud.direccion = txtRef2_direc_tpCodeudor.Text;
			referen2_codeud.telefono_fijo = txtRef2_telef_tpCodeudor.Text;
			referen2_codeud.telefono_celular = "";
			referen2_codeud.punto_referencia = txtRef2_ptoref_tpCodeudor.Text;
			referen2_codeud.casa_color = txtRef2_casacolor_tpCodeudor.Text;
			referen2_codeud.bloque = "";
			LstReferencias_codeudor.Add(referen2_codeud);
			//3
			referencias_solicitud referen3_codeud = new referencias_solicitud();
			referen3_codeud.no_referencia = 3;
			referen3_codeud.referencia_de = sol.no_identificacion;
			referen3_codeud.nombre = txtRef3.Text;
			referen3_codeud.direccion = txtRef3_direc.Text;
			referen3_codeud.telefono_fijo = txtRef3_telef.Text;
			referen3_codeud.telefono_celular = "";
			referen3_codeud.punto_referencia = "";
			referen3_codeud.casa_color = "";
			referen3_codeud.bloque = "";
			LstReferencias_codeudor.Add(referen3_codeud);
			#endregion

			#endregion

			#region Aval 1

			sol.no_identificacion_aval1 = txtId_aval1.Text;
			sol.primer_nombre_aval1 = txtPriNombre_tpAval1.Text;
			sol.segundo_nombre_aval1 = txtSegNombre_tpAval1.Text;
			sol.primer_apellido_aval1 = txtPriApellido_tpAval1.Text;
			sol.segundo_apellido_aval1 = txtSegApellido_tpAval1.Text;
			if (rbMasculino_tpAval1.Checked)
				sol.sexo_aval1 = "M";
			if (rbFemenino_tpAval1.Checked)
				sol.sexo_aval1 = "F";
			sol.EstadoCivilAval1 = this.txtEstadoCivil_tpAval1.Text;
			sol.dependientes_hijos_aval1 = Convert.ToInt16(txtNoHijos_tpAval1.Text);
			sol.dependientes_otros_aval1 = Convert.ToInt16(txtOtrospariente_tpAval1.Text);
			sol.direccion_residencia_aval1 = txtDirecc_res_tpAval1.Text;
			sol.telefono_fijo_aval1 = txtTelefono_tpAval1.Text;
			sol.celular_aval1 = txtCelular_tpAval1.Text;
			sol.telefono_adicional1_aval1 = txtOtrotelefono1_tpAval1.Text;
			sol.telefono_adicional2_aval1 = txtOtrotelefono2_tpAval1.Text;
			sol.correo_aval1 = txtCorreo_tpAval1.Text;
			if (rbSiAfiliado_tpAval1.Checked)
				sol.es_afiliado_aval1 = "S";
			else
				sol.es_afiliado_aval1 = "N";
			sol.codigo_cliente_aval1 = txtCodigo_cliente_TpAval1.Text;

			sol.tipo_empresa_aval1 = string.Empty;
			if (rbTePrivado_tpAval1.Checked)
				sol.tipo_empresa_aval1 = "Privado";
			if (rbTePublico_tpAval1.Checked)
				sol.tipo_empresa_aval1 = "Publico";
			if (rbTeComerciante_tpAval1.Checked)
				sol.tipo_empresa_aval1 = "Comerciante";
			if (rbTeOtros_tpAval1.Checked)
				sol.tipo_empresa_aval1 = "Otros";

			sol.tipo_empresa_especificar_aval1 = txtTipoEmpresaotros_tpAval1.Text;
			sol.patrono_aval1 = txtPatrono_tpAval1.Text;
			sol.depto_labora_aval1 = txtDeptolabora_tpAval1.Text;
			sol.cargo_aval1 = txtCargo_tpAval1.Text;
			sol.antiguedad_aval1 = txtAntiglaboral_tpAval1.Text;
			sol.ingresos_aval1 = Convert.ToDecimal(txtIngresos_tpAval1.Text);
			sol.otros_ingresos_aval1 = Convert.ToDecimal(txtOtrosIngresos_tpAval1.Text);
			sol.deducciones_aval1 = Convert.ToDecimal(txtDeducciones_tpAval1.Text);
			sol.telefono_laboral1_aval1 = txtTellaboral1_tpAval1.Text;
			sol.ext_laboral1_aval1 = txtExtlaboral1_tpAval1.Text;
			sol.telefono_laboral2_aval1 = txtTellaboral2_tpAval1.Text;
			sol.ext_laboral2_aval1 = txtExtlaboral2_tpAval1.Text;
			sol.direccion_laboral_aval1 = txtDirecclaboral_tpAval1.Text;
			sol.correo_laboral_aval1 = txtCorreolaboral_tpAval1.Text;
			sol.nombre_conyuge_aval1 = txtNombre_conyuge_tpAval1.Text;
			sol.direclab_conyuge_aval1 = txtDirelab_conyuge_tpAval1.Text;
			sol.cargo_conyuge_aval1 = txtCargo_conyuge_tpAval1.Text;

			if (!string.IsNullOrEmpty(this.txtEdad_tpAval1.Text))
			{
				sol.EdadAval1 = Convert.ToInt32(this.txtEdad_tpAval1.Text);
			}

			sol.TipoViviendaAval1 = string.Empty;
			if (this.rbTp_PropiaAval1.Checked)
			{
				sol.TipoViviendaAval1 = "PROPIA";
			}
			if (this.rbTp_PagandoAval1.Checked)
			{
				sol.TipoViviendaAval1 = "APLAZOS";
			}
			if (this.rbTp_FamiliarAval1.Checked)
			{
				sol.TipoViviendaAval1 = "FAMILIAR";
			}
			if (this.rbTp_AlquiladaAval1.Checked)
			{
				sol.TipoViviendaAval1 = "ALQUILADA";
			}
			if (this.rbTp_OtrosAval1.Checked)
			{
				sol.TipoViviendaAval1 = "OTROS";
			}

			sol.DescripcionViviendaAval1 = this.txtViviendaOtros_Aval1.Text;
			sol.RTN_Aval1 = this.txtRtnAval1.Text;
			sol.FechaIngresoLaboralAval1 = this.txtFechaIngreso_Aval1.Text;

			#endregion

			#region Aval 2

			sol.no_identificacion_aval2 = txtId_aval2.Text;
			sol.primer_nombre_aval2 = txtPriNombre_tpAval2.Text;
			sol.segundo_nombre_aval2 = txtSegNombre_tpAval2.Text;
			sol.primer_apellido_aval2 = txtPriApellido_tpAval2.Text;
			sol.segundo_apellido_aval2 = txtSegApellido_tpAval2.Text;
			sol.EstadoCivilAval2 = this.txtEstadoCivil_tpAval2.Text;
			if (rbMasculino_tpAval2.Checked)
				sol.sexo_aval2 = "M";
			if (rbFemenino_tpAval2.Checked)
				sol.sexo_aval2 = "F";

			sol.dependientes_hijos_aval2 = Convert.ToInt16(txtNoHijos_tpAval2.Text);
			sol.dependientes_otros_aval2 = Convert.ToInt16(txtOtrospariente_tpAval2.Text);
			sol.direccion_residencia_aval2 = txtDirecc_res_tpAval2.Text;
			sol.telefono_fijo_aval2 = txtTelefono_tpAval2.Text;
			sol.celular_aval2 = txtCelular_tpAval2.Text;
			sol.telefono_adicional1_aval2 = txtOtrotelefono1_tpAval2.Text;
			sol.telefono_adicional2_aval2 = txtOtrotelefono2_tpAval2.Text;
			sol.correo_aval2 = txtCorreo_tpAval2.Text;

			if (rbSiAfiliado_tpAval2.Checked)
				sol.es_afiliado_aval2 = "S";
			else
				sol.es_afiliado_aval2 = "N";
			sol.codigo_cliente_aval2 = txtCodigo_cliente_TpAval2.Text;

			sol.tipo_empresa_aval2 = string.Empty;
			if (rbTePrivado_tpAval2.Checked)
				sol.tipo_empresa_aval2 = "Privado";
			if (rbTePublico_tpAval2.Checked)
				sol.tipo_empresa_aval2 = "Publico";
			if (rbTeComerciante_tpAval2.Checked)
				sol.tipo_empresa_aval2 = "Comerciante";
			if (rbTeOtros_tpAval2.Checked)
				sol.tipo_empresa_aval2 = "Otros";

			sol.tipo_empresa_especificar_aval2 = txtTipoEmpresaotros_tpAval2.Text;
			sol.patrono_aval2 = txtPatrono_tpAval2.Text;
			sol.depto_labora_aval2 = txtDeptolabora_tpAval2.Text;
			sol.cargo_aval2 = txtCargo_tpAval2.Text;
			sol.antiguedad_aval2 = txtAntiglaboral_tpAval2.Text;
			sol.ingresos_aval2 = Convert.ToDecimal(txtIngresos_tpAval2.Text);
			sol.otros_ingresos_aval2 = Convert.ToDecimal(txtOtrosIngresos_tpAval2.Text);
			sol.deducciones_aval2 = Convert.ToDecimal(txtDeducciones_tpAval2.Text);
			sol.telefono_laboral1_aval2 = txtTellaboral1_tpAval2.Text;
			sol.ext_laboral1_aval2 = txtExtlaboral1_tpAval2.Text;
			sol.telefono_laboral2_aval2 = txtTellaboral2_tpAval2.Text;
			sol.ext_laboral2_aval2 = txtExtlaboral2_tpAval2.Text;
			sol.direccion_laboral_aval2 = txtDirecclaboral_tpAval2.Text;
			sol.correo_laboral_aval2 = txtCorreolaboral_tpAval2.Text;
			sol.nombre_conyuge_aval2 = txtNombre_conyuge_tpAval2.Text;
			sol.direclab_conyuge_aval2 = txtDirelab_conyuge_tpAval2.Text;
			sol.cargo_conyuge_aval2 = txtCargo_conyuge_tpAval2.Text;

			if (!string.IsNullOrEmpty(this.txtEdad_tpAval2.Text))
			{
				sol.EdadAval2 = Convert.ToInt32(this.txtEdad_tpAval2.Text);
			}

			sol.TipoViviendaAval2 = string.Empty;
			if (this.rbTp_PropiaAval2.Checked)
			{
				sol.TipoViviendaAval2 = "PROPIA";
			}
			if (this.rbTp_PagandoAval2.Checked)
			{
				sol.TipoViviendaAval2 = "APLAZOS";
			}
			if (this.rbTp_FamiliarAval2.Checked)
			{
				sol.TipoViviendaAval2 = "FAMILIAR";
			}
			if (this.rbTp_AlquiladaAval2.Checked)
			{
				sol.TipoViviendaAval2 = "ALQUILADA";
			}
			if (this.rbTp_OtrosAval2.Checked)
			{
				sol.TipoViviendaAval2 = "OTROS";
			}

			sol.DescripcionViviendaAval2 = this.txtViviendaOtros_Aval1.Text;
			sol.RTN_Aval2 = this.txtRtnAval2.Text;
			sol.FechaIngresoLaboralAval2 = this.txtFechaIngreso_Aval2.Text;

			#endregion

			GaranteHipotecario gar = new GaranteHipotecario();
			//Si tiene garante
			if (this.cbGarante.Checked)
			{
				sol.RequiereGarante = "S";
				#region Garante Hipotecario

				gar.NoIdentidadGarante = this.txtId_tpGarante.Text;
				if (string.IsNullOrEmpty(this.txtCodigoCliente_Garante.Text))
					this.txtCodigoCliente_Garante.Text = "0";
				gar.CodigoClienteGarante = Convert.ToInt32(this.txtCodigoCliente_Garante.Text);
				gar.PrimerNombreGarante = this.txtPrimerNom_tpGarante.Text;
				gar.SegundoNombreGarante = this.txtSegundoNom_tpGarante.Text;
				gar.PrimerApellidoGarante = this.txtPrimerAp_tpGarante.Text;
				gar.SegundoApellidoGarante = this.txtSegundoAp_tpGarante.Text;
				if (this.rbMasculino_tpGarante.Checked)
					gar.GeneroGarante = "M";
				if (this.rbFemenino_tpGarante.Checked)
					gar.GeneroGarante = "F";
				if (string.IsNullOrEmpty(this.txtHijos_tpGarante.Text))
					this.txtHijos_tpGarante.Text = "0";
				gar.DependientesHijosGarante = Convert.ToInt32(this.txtHijos_tpGarante.Text);
				if (string.IsNullOrEmpty(this.txtOtrosDep_tpGarante.Text))
					this.txtOtrosDep_tpGarante.Text = "0";
				gar.DependientesOtrosGarante = Convert.ToInt32(this.txtOtrosDep_tpGarante.Text);
				if (string.IsNullOrEmpty(this.txtDirecRes_tpGarante.Text))
					this.txtDirecRes_tpGarante.Text = string.Empty;
				if (string.IsNullOrEmpty(this.txtEdadGarante.Text))
					this.txtEdadGarante.Text = "0";
				gar.EdadGarante = Convert.ToInt32(this.txtEdadGarante.Text);
				gar.DireccionGarante = this.txtDirecRes_tpGarante.Text;
				gar.DirecLabGarante = this.txtDirecRes_tpGarante.Text;
				gar.TelefonoFijoGarante = this.txtTelFijo_tpGarante.Text;
				gar.CelularGarante = this.txtCelular_tpGarante.Text;
				gar.TelefonoAdic1Garante = this.txtTelAdic1_tpGarante.Text;
				gar.TelefonoAdic2Garante = this.txtTelAdic2_tpGarante.Text;
				gar.CorreoGarante = this.txtCorreo_tpGarante.Text;
				gar.EstadoCivil = this.cmbEstadoCivil.Text;
				if (this.rbEsAfiliadoSi_tpGarante.Checked)
					gar.EsAfliadoGarante = "S";
				else
					gar.EsAfliadoGarante = "N";

				gar.TipoEmpresaGarante = string.Empty;
				if (this.rbPrivado_Garante.Checked)
					gar.TipoEmpresaGarante = "PRIVADO";
				if (this.rbPublico_Garante.Checked)
					gar.TipoEmpresaGarante = "PUBLICO";
				if (this.rbComerciante_Garante.Checked)
					gar.TipoEmpresaGarante = "COMERCIANTE";
				if (this.rbOtrosEmpresa_Garante.Checked)
				{
					gar.TipoEmpresaGarante = "OTROS";
				}

				gar.TipoViviendaGar = string.Empty;
				if (this.rbPropia_Garante.Checked)
				{
					gar.TipoViviendaGar = "Propia";
				}
				if (this.rbPagando_Garante.Checked)
				{
					gar.TipoViviendaGar = "Pagando";
				}
				if (this.rbAlquilada_Garante.Checked)
				{
					gar.TipoViviendaGar = "Alquilada";
				}
				if (this.rbFamiliar_Garante.Checked)
				{
					gar.TipoViviendaGar = "Familiar";
				}
				if (this.rbOtros_Garante.Checked)
				{
					gar.TipoViviendaGar = "Otros";
				}

				gar.ViviendaGarante = this.txtViviendaOtros_Garante.Text;
				gar.TipoEmpresaOtrosGarante = this.txtOtrosEmpresa_Garante.Text;
				gar.PatronoGarante = this.txtPatrono_Garante.Text;
				gar.DeptoLaboraGarante = this.txtDepto_Garante.Text;
				gar.CargoGarante = this.txtCargo_Garante.Text;
				gar.AntiguedadLabGarante = this.txtAntiguedad_Garante.Text;
				gar.TelLab1Garante = this.txtTel_LabGarante.Text;
				gar.ExtLab1Garante = this.txtExt1_Garante.Text;
				gar.TelLab2Garante = this.txtTel_Labo2Garante.Text;
				gar.ExtLab2Garante = this.txtExt2_Garante.Text;
				gar.DirecLabGarante = this.txtDireccionLab_Garante.Text;
				gar.CorreoLabgarante = this.txtCorreoLab_Garante.Text;
				gar.NomConyugeGarante = this.txtConyuge_Garante.Text;
				gar.DirecLabConyuGarante = this.txtDirLabCony_Garante.Text;
				gar.CargoLabConyuGarante = this.txtCargoConyu_Garante.Text;
				//Referencias
				gar.NomRef1Garante = this.txtRef1Nom_Garante.Text;
				gar.DirRef1Garante = this.txtRef1Res_Garante.Text;
				gar.TelRef1Garante = this.txtRef1Tel_Garante.Text;
				gar.LocalRef1Garante = this.txtRef1PtoRef_Garante.Text;
				gar.CelularRef1Garante = this.txtRef1Cel_Garante.Text;

				gar.NomRef2Garante = this.txtRef2Nom_Garante.Text;
				gar.DirRef2Garante = this.txtRef2Res_Garante.Text;
				gar.TelRef2Garante = this.txtRef2Tel_Garante.Text;
				gar.LocalRef2Garante = this.txtRef2PtoRef_Garante.Text;
				gar.CelularRef2Garante = this.txtRef2Cel_Garante.Text;

				gar.NomRef3Garante = this.txtRef3Nom_Garante.Text;
				gar.DirRef3Garante = this.txtRef3Res_Garante.Text;
				gar.TelRef3Garante = this.txtRef3Tel_Garante.Text;

				#endregion
			}
			else
			{
				sol.RequiereGarante = "N";
			}

			#region referencias de la solicitud

			gxml_referencias = p_construir_xml_referencias();
			sol.xml_referencias = gxml_referencias;

			#endregion

			#region Datos de concentracion de deuda
			/****/
			p_calcular_indice_concentracion_deuda();
			/****/

			if (rbIndicador1.Checked)
			{
				sol.indicador_aplicado = 1;
			}
			if (rbIndicador2.Checked)
			{
				sol.indicador_aplicado = 2;
			}

			if (rbIndicador3.Checked)
			{
				sol.indicador_aplicado = 3;
			}

			decimal total_capvgte_grpfam = 0;
			decimal.TryParse(txtTotal_capitales_vigentes_grpfamiliar.Text, out total_capvgte_grpfam);
			sol.total_capitalvigente_grpfam = total_capvgte_grpfam;

			decimal total_capvgte_solicitante = 0;
			decimal.TryParse(txtTotal_capitales_vigentes_titular.Text, out total_capvgte_solicitante);
			sol.total_capitalvigente_solicitante = total_capvgte_solicitante;

			decimal monto_ensolic = 0;
			decimal.TryParse(txtSolicitado.Text, out monto_ensolic);
			sol.monto_ensolicitud = monto_ensolic;

			decimal monto_excluir_refcon = 0;
			decimal.TryParse(txtMonto_excluir_refcons.Text, out monto_excluir_refcon);
			sol.monto_excluir_refconsol = monto_excluir_refcon;

			decimal total_pindice = 0;
			decimal.TryParse(txtNumerador_formula.Text, out total_pindice);
			sol.total_paraindice = total_pindice;

			decimal patrominio_csf = 0;
			decimal.TryParse(txtPatrimonio.Text, out patrominio_csf);
			sol.patrimonio_csf = patrominio_csf;

			decimal porcentaje_concentracion = 0;
			decimal.TryParse(txtIndice_concentracion.Text, out porcentaje_concentracion);
			sol.porcentaje_concentracion = porcentaje_concentracion;

			decimal limite_indicador = 0;
			decimal.TryParse(txtLimiteIndicador.Text, out limite_indicador);
			sol.limite_indicador = limite_indicador;

			sol.resultado_evaluacion_indicador = txtEvaluacionCDC.Text;

			#endregion

			if (gmodo_coopsafa == "INS")
			{
				Int32 no_solicitud = 0;
				if (da.InsertarSolicitudCredito(sol, out no_solicitud, gar))
				{
					txtNo_solicitud_coopsafa.Text = no_solicitud.ToString();
					LabelNo_solic.Text = txtNo_solicitud_coopsafa.Text;
					gmodo_coopsafa = "UPD";
					btnGuardar_solicitud.Text = " Actualizar Solicitud";
					MessageBox.Show("Solicitud ingresada satisfactoriamente...!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
					retornar_ok = true;
					if (sol.codigo_sub_aplicacion == 19 & sol.patrono.Contains("MINISTERIO PUBLICO"))
					{
						var resp = MessageBox.Show("Se requiere la impresión de la autorización para empleados del Ministerio Público, ¿desea continuar?", "Imprimir Autorización", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

						if (resp == DialogResult.OK)
						{
							DocMinisterioPublico reporte = new DocMinisterioPublico(sol.no_solicitud, this.da, Convert.ToInt32(gcodigo_agencia));
							reporte.ShowDialog();
						}
					}
					return;
				}
			}

			if (gmodo_coopsafa == "UPD")
			{
				sol.no_solicitud = Int32.Parse(txtNo_solicitud_coopsafa.Text);
				da.ActualizarSolicitudCredito(sol, gar);
				retornar_ok = true;
				if (sol.codigo_sub_aplicacion == 19 & sol.patrono.Contains("MINISTERIO PUBLICO"))
				{
					var resp = MessageBox.Show("Se requiere la impresión de la autorización para empleados del Ministerio Público, ¿desea continuar?", "Imprimir Autorización", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

					if (resp == DialogResult.OK)
					{
						DocMinisterioPublico reporte = new DocMinisterioPublico(sol.no_solicitud, this.da, Convert.ToInt32(gcodigo_agencia));
						reporte.ShowDialog();
					}
				}
				Notificacion.show_Toast(1500, "Solicitud actualizada satisfactoriamente !");
			}

			retornar_ok = true;
			//this.NecesitaExcepciones(sol);
		}

		private void NecesitaExcepciones(solicitud_credito sol)
		{
			//Evalua si necesita excepción
			ExcepcionesAutomaticas excep = new ExcepcionesAutomaticas();
			var excepciones = excep.NecesidadExcepcion(Convert.ToInt32(this.txtCodigo_cliente.Text), sol.ingresos, sol.EdadPresta, Convert.ToInt32(sol.antiguedad_laboral), this.da,
									(sol.ventanilla_planilla.Equals("Ventanilla")), (int)this.ResultadoPrestatario, sol.complemento_aportaciones,
									Convert.ToInt32(sol.codigo_cliente_aval1), false, Convert.ToInt32(sol.antiguedad_aval2), sol.EdadAval1, sol.ingresos_aval1, (int)this.ResultadoAval1,
									Convert.ToInt32(sol.codigo_cliente_aval2), false, Convert.ToInt32(sol.antiguedad_aval2), sol.EdadAval2, sol.ingresos_aval2, (int)this.ResultadoAval2);

			string mensajeExcepciones = string.Empty;
			foreach (var item in excepciones)
			{
				switch (item.Figura)
				{
					case FigurasPrestatario.Prestatario:
						{
							if (item.CapacidadPagoInsuficiente != (int)TipoLineamientos.SinExcepcion)
								mensajeExcepciones += "El prestatario necesita excepción por capacidad de pago insuficiente.\n";
							if (item.GarantiaInsuficienteAval1 != (int)TipoLineamientos.SinExcepcion)
								mensajeExcepciones += "El prestatario necesita excepción por que no cuenta con garantía insuficiente por Aval1.\n";
							if (item.GarantiaInsuficienteAval2 != (int)TipoLineamientos.SinExcepcion)
								mensajeExcepciones += "El prestatario necesita excepción por que no cuenta con garantía insuficiente por Aval2.\n";
							if (item.IncumplimientoPoliticaEdad != (int)TipoLineamientos.SinExcepcion)
								mensajeExcepciones += "El prestatario necesita excepción por que no cumple con la edad reglamentada";
							if (item.IncumplimientoPoliticaLab != (int)TipoLineamientos.SinExcepcion)
								mensajeExcepciones += "El prestatario necesita excepción por que no cumple con la politica de antigüedad laboral.\n";
							if (item.OtrasExcepciones != (int)TipoLineamientos.SinExcepcion)
								mensajeExcepciones += "EL prestatario necesita una excepción por complemento de aportaciones.\n";
							if (item.RecordCrediticioNegativo != (int)TipoLineamientos.SinExcepcion)
								mensajeExcepciones += "El prestatario necesita una excepción por record creditcio negativo.\n";
						}
						break;
					case FigurasPrestatario.Aval1:
						{
							//if (item.CapacidadPagoInsuficiente != (int)TipoLineamientos.SinExcepcion)
							//	mensajeExcepciones += "El prestatario necesita excepción por Aval1 con capacidad de pago insuficiente.\n";
							if (item.RecordCrediticioNegativo != (int)TipoLineamientos.SinExcepcion)
								mensajeExcepciones += "El prestatario necesita una excepción por Aval1 con record creditcio negativo.\n";
							if (item.IncumplimientoPoliticaEdad != (int)TipoLineamientos.SinExcepcion)
								mensajeExcepciones += "El prestatario necesita excepción por Aval1, no cumple con la edad reglamentada";
							if (item.IncumplimientoPoliticaLab != (int)TipoLineamientos.SinExcepcion)
								mensajeExcepciones += "El prestatario necesita excepción por Aval1, no cumple con la politica de antigüedad laboral.\n";
						}
						break;
					case FigurasPrestatario.Aval2:
						{
							//if (item.CapacidadPagoInsuficiente != (int)TipoLineamientos.SinExcepcion)
							//	mensajeExcepciones += "El prestatario necesita excepción por Aval2 con capacidad de pago insuficiente.\n";
							if (item.RecordCrediticioNegativo != (int)TipoLineamientos.SinExcepcion)
								mensajeExcepciones += "El prestatario necesita una excepción por Aval2 con record creditcio negativo.\n";
							if (item.IncumplimientoPoliticaEdad != (int)TipoLineamientos.SinExcepcion)
								mensajeExcepciones += "El prestatario necesita excepción por Aval2, no cumple con la edad reglamentada";
							if (item.IncumplimientoPoliticaLab != (int)TipoLineamientos.SinExcepcion)
								mensajeExcepciones += "El prestatario necesita excepción por Aval2, no cumple con la politica de antigüedad laboral.\n";
						}
						break;
					default:
						break;
				}
			}

			string bandera = "N";
			if (!string.IsNullOrEmpty(mensajeExcepciones))
			{
				bandera = "S";
				MessageBox.Show(mensajeExcepciones, "¡¡SE NECESITA EXCEPCIÓN!!", MessageBoxButtons.OK);
			}

			bool creada;
			bool aceptar = this.da.GuardarAlertaExcepcion(bandera, sol.no_solicitud, out creada);
		}

		private void s_PreCalificado_Shown(object sender, EventArgs e)
		{


		}
		private void comboBox_sub_aplicacion_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBox_sub_aplicacion.Text != "")
			{
				try
				{
					txtCodigo_sub_aplicacion.Text = comboBox_sub_aplicacion.SelectedValue.ToString();
					if (txtCodigo_sub_aplicacion.Text.Trim() == "10" || txtCodigo_sub_aplicacion.Text.Trim() == "23" || txtCodigo_sub_aplicacion.Text.Trim() == "32")
					{
						pnlCondicion_vehiculo.Visible = true;
						prestamo_vehiculos = true;
					}
					else
					{
						pnlCondicion_vehiculo.Visible = false;
						prestamo_vehiculos = false;
					}
					string vl_codigo_moneda = "";
					string vl_descripcion_moneda = "";
					string vl_sigla_moneda = "";
					da.ObtenerMonedaxSubAplicacion(txtCodigo_sub_aplicacion.Text, out vl_codigo_moneda, out vl_descripcion_moneda, out vl_sigla_moneda);
					txtCodigo_moneda.Text = vl_codigo_moneda;
					txtDesc_moneda.Text = vl_descripcion_moneda;
					txtMoneda_abreviatura.Text = vl_sigla_moneda;


				}
				catch
				{

				}


			}
		}
		private void pictureBox13_Click(object sender, EventArgs e)
		{
			txtUrlTransUnion.Visible = true;
		}
		private void lLreportecredito_princ_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			string resultado_consulta = da.ObtenerXmlRespuestaEvaluacion(Int32.Parse(txtNo_solicitud_coopsafa.Text));
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
			string resultado_consulta = da.ObtenerXmlRespuestaEvaluacion(Int32.Parse(txtNo_solicitud_coopsafa.Text));
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
			string resultado_consulta = da.ObtenerXmlRespuestaEvaluacion(Int32.Parse(txtNo_solicitud_coopsafa.Text));
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
		private void gvResultado_buro_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
		{
			this.Cursor = Cursors.Hand;
			if (vl_mostrar_miniinfo)
			{
				if (e.RowIndex >= 0)
				{
					string vl_id = gvResultado_buro.Rows[e.RowIndex].Cells["id"].Value.ToString();
					string vl_nombre = gvResultado_buro.Rows[e.RowIndex].Cells["nombre"].Value.ToString();
					string vl_flags = gvResultado_buro.Rows[e.RowIndex].Cells["flags"].Value.ToString();
					string vl_decision = gvResultado_buro.Rows[e.RowIndex].Cells["precalificado"].Value.ToString();
					string vl_score = gvResultado_buro.Rows[e.RowIndex].Cells["score"].Value.ToString();
					string vl_edad = gvResultado_buro.Rows[e.RowIndex].Cells["edad"].Value.ToString();

					mini_info_resul.id.Text = vl_id;
					mini_info_resul.nombre.Text = vl_nombre;
					mini_info_resul.obs.Text = vl_flags.Trim();
					mini_info_resul.lblDecision.Text = vl_decision;
					mini_info_resul.labelScore.Text = vl_score;
					mini_info_resul.edad.Text = vl_edad;

					Point pos = this.PointToScreen(e.Location);
					mini_info_resul.Show();
					mini_info_resul.Location = new Point(Control.MousePosition.X + 5, Control.MousePosition.Y + 10);
					mini_info_resul.Refresh();
				}


			}
			vl_mostrar_miniinfo = false; //para que no se este cargando mientras tenga el mouse sobre la foto            
		}
		private void gvResultado_buro_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
		{
			vl_mostrar_miniinfo = true;
			mini_info_resul.Hide();
		}
		private string ListToString(List<string> p_lista)
		{
			StringBuilder vl_retorno = new StringBuilder();
			for (int x = 0; x < p_lista.Count; x++)
			{
				vl_retorno.Append(p_lista[x].ToString() + " ");
			}
			return vl_retorno.ToString();
		}
		private void txtMonto_solicitado_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtMonto_solicitado;
			double valor_ingresado = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				valor_ingresado = Convert.ToDouble(texbox.Text);

			texbox.Text = string.Format(String.Format("{0:#,###,###,##0.00}", valor_ingresado));
			p_calcular_cuota_nivelada();
		}
		private void txtMonto_solicitado_KeyPress(object sender, KeyPressEventArgs e)
		{
			TextBox texbox = txtMonto_solicitado;
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
		private void soloenteros_KeyPress(object sender, KeyPressEventArgs e)
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
		private void txtIngresos_KeyPress(object sender, KeyPressEventArgs e)
		{
			TextBox texbox = txtIngresos;
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
		private void txtIngresos_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtIngresos;
			if (texbox.Text.Trim() == ".")
			{
				texbox.Text = "0";
			}
			double ingresos = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ingresos = Convert.ToDouble(texbox.Text);

			texbox.Text = string.Format(String.Format("{0:###,##0.00}", ingresos));
		}
		private void txtOtros_ingresos_KeyPress(object sender, KeyPressEventArgs e)
		{
			TextBox texbox = txtOtros_ingresos;
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
		private void txtOtros_ingresos_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtOtros_ingresos;
			if (texbox.Text.Trim() == ".")
			{
				texbox.Text = "0";
			}
			double ingresos = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ingresos = Convert.ToDouble(texbox.Text);

			texbox.Text = string.Format(String.Format("{0:###,##0.00}", ingresos));
		}
		private void txtDeducciones_KeyPress(object sender, KeyPressEventArgs e)
		{
			TextBox texbox = txtDeducciones;
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
		private void txtDeducciones_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtDeducciones;
			if (texbox.Text.Trim() == ".")
			{
				texbox.Text = "0";
			}
			double ingresos = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ingresos = Convert.ToDouble(texbox.Text);

			texbox.Text = string.Format(String.Format("{0:###,##0.00}", ingresos));
		}
		private void txtTasa_KeyPress(object sender, KeyPressEventArgs e)
		{
			TextBox texbox = txtTasa;
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

		//FELVIR01 - 20190701
		private void txtNoHijos_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtNoHijos;
			double ntexbox = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ntexbox = Convert.ToDouble(texbox.Text);

			if (texbox.Text.Equals("0"))
			{
				DialogResult result = MessageBox.Show("¿Está seguro que el solicitante no tiene hijos?", "Verificar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				if (result == DialogResult.No)
				{
					return;
				}
			}

			texbox.Text = string.Format(String.Format("{0:#0}", ntexbox));
		}

		private void txtOtrospariente_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtOtrospariente;
			double ntexbox = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ntexbox = Convert.ToDouble(texbox.Text);

			if (texbox.Text.Equals("0"))
			{
				DialogResult result = MessageBox.Show("¿Está seguro que el solicitante no tiene otros dependientes?", "Verificar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				if (result == DialogResult.No)
				{
					return;
				}
			}

			texbox.Text = string.Format(String.Format("{0:#0}", ntexbox));
		}

		private void txtNoHijos_tpConyuge_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtNoHijos_tpConyuge;
			double ntexbox = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ntexbox = Convert.ToDouble(texbox.Text);
			texbox.Text = string.Format(String.Format("{0:#0}", ntexbox));
		}
		private void txtOtrospariente_tpConyuge_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtOtrospariente_tpConyuge;
			double ntexbox = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ntexbox = Convert.ToDouble(texbox.Text);
			texbox.Text = string.Format(String.Format("{0:#0}", ntexbox));
		}
		private void txtCodigo_cliente_TpConyuge_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtCodigo_cliente_TpConyuge;
			double ntexbox = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ntexbox = Convert.ToDouble(texbox.Text);
			texbox.Text = string.Format(String.Format("{0:##########0}", ntexbox));
		}
		private void txtIngresos_tpConyuge_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtIngresos_tpConyuge;
			if (texbox.Text.Trim() == ".")
			{
				texbox.Text = "0";
			}
			double ingresos = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ingresos = Convert.ToDouble(texbox.Text);
			texbox.Text = string.Format(String.Format("{0:###,##0.00}", ingresos));
		}
		private void txtOtrosIngresos_tpConyuge_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtOtrosIngresos_tpConyuge;
			if (texbox.Text.Trim() == ".")
			{
				texbox.Text = "0";
			}
			double ingresos = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ingresos = Convert.ToDouble(texbox.Text);
			texbox.Text = string.Format(String.Format("{0:###,##0.00}", ingresos));
		}
		private void txtDeducciones_tpConyuge_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtDeducciones_tpConyuge;
			if (texbox.Text.Trim() == ".")
			{
				texbox.Text = "0";
			}
			double ingresos = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ingresos = Convert.ToDouble(texbox.Text);
			texbox.Text = string.Format(String.Format("{0:###,##0.00}", ingresos));
		}
		private void txtIngresos_tpConyuge_KeyPress(object sender, KeyPressEventArgs e)
		{
			TextBox texbox = txtIngresos_tpConyuge;
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
		private void txtOtrosIngresos_tpConyuge_KeyPress(object sender, KeyPressEventArgs e)
		{
			TextBox texbox = txtOtrosIngresos_tpConyuge;
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
		private void txtDeducciones_tpConyuge_KeyPress(object sender, KeyPressEventArgs e)
		{
			TextBox texbox = txtDeducciones_tpConyuge;
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
		private void txtCodigo_cliente_TpCodeudor_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtCodigo_cliente_TpCodeudor;
			double ntexbox = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ntexbox = Convert.ToDouble(texbox.Text);
			texbox.Text = string.Format(String.Format("{0:##########0}", ntexbox));
		}
		private void txtIngresos_tpCodeudor_KeyPress(object sender, KeyPressEventArgs e)
		{
			TextBox texbox = txtIngresos_tpCodeudor;
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
		private void txtOtrosIngresos_tpCodeudor_KeyPress(object sender, KeyPressEventArgs e)
		{
			TextBox texbox = txtOtrosIngresos_tpCodeudor;
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
		private void txtDeducciones_tpCodeudor_KeyPress(object sender, KeyPressEventArgs e)
		{
			TextBox texbox = txtDeducciones_tpCodeudor;
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
		private void txtIngresos_tpCodeudor_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtIngresos_tpCodeudor;
			if (texbox.Text.Trim() == ".")
			{
				texbox.Text = "0";
			}
			double ingresos = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ingresos = Convert.ToDouble(texbox.Text);

			texbox.Text = string.Format(String.Format("{0:###,##0.00}", ingresos));
		}
		private void txtOtrosIngresos_tpCodeudor_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtOtrosIngresos_tpCodeudor;
			if (texbox.Text.Trim() == ".")
			{
				texbox.Text = "0";
			}
			double ingresos = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ingresos = Convert.ToDouble(texbox.Text);

			texbox.Text = string.Format(String.Format("{0:###,##0.00}", ingresos));
		}
		private void txtDeducciones_tpCodeudor_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtDeducciones_tpCodeudor;
			if (texbox.Text.Trim() == ".")
			{
				texbox.Text = "0";
			}
			double ingresos = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ingresos = Convert.ToDouble(texbox.Text);

			texbox.Text = string.Format(String.Format("{0:###,##0.00}", ingresos));
		}

		//FELVIR01 - 20190701
		private void txtNoHijos_tpCodeudor_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtNoHijos_tpCodeudor;
			double ntexbox = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ntexbox = Convert.ToDouble(texbox.Text);


			if (texbox.Text.Equals("0"))
			{
				DialogResult result = MessageBox.Show("¿Está seguro que el codeudor no tiene hijos?", "Verificar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				if (result == DialogResult.No)
				{
					return;
				}
			}

			texbox.Text = string.Format(String.Format("{0:#0}", ntexbox));
		}

		//FELVIR01 - 20190701
		private void txtOtrospariente_tpCodeudor_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtOtrospariente_tpCodeudor;
			double ntexbox = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ntexbox = Convert.ToDouble(texbox.Text);

			if (texbox.Text.Equals("0"))
			{
				DialogResult result = MessageBox.Show("¿Está seguro que el codeudor no tiene otros dependientes?", "Verificar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				if (result == DialogResult.No)
				{
					return;
				}
			}

			texbox.Text = string.Format(String.Format("{0:#0}", ntexbox));
		}

		private void txtNoHijos_tpAval1_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtNoHijos_tpAval1;
			double ntexbox = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ntexbox = Convert.ToDouble(texbox.Text);

			if (texbox.Text.Equals("0"))
			{
				DialogResult result = MessageBox.Show("¿Está seguro que el aval1 no tiene hijos?", "Verificar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				if (result == DialogResult.No)
				{
					return;
				}
			}

			texbox.Text = string.Format(String.Format("{0:#0}", ntexbox));
		}

		private void txtOtrospariente_tpAval1_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtOtrospariente_tpAval1;
			double ntexbox = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ntexbox = Convert.ToDouble(texbox.Text);

			if (texbox.Text.Equals("0"))
			{
				DialogResult result = MessageBox.Show("¿Está seguro que el codeudor no tiene otros dependientes?", "Verificar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				if (result == DialogResult.No)
				{
					return;
				}
			}

			texbox.Text = string.Format(String.Format("{0:#0}", ntexbox));
		}

		private void txtCodigo_cliente_TpAval1_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtCodigo_cliente_TpAval1;
			double ntexbox = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ntexbox = Convert.ToDouble(texbox.Text);
			texbox.Text = string.Format(String.Format("{0:##########0}", ntexbox));
		}
		private void txtIngresos_tpAval1_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtIngresos_tpAval1;
			if (texbox.Text.Trim() == ".")
			{
				texbox.Text = "0";
			}
			double ingresos = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ingresos = Convert.ToDouble(texbox.Text);

			texbox.Text = string.Format(String.Format("{0:###,##0.00}", ingresos));
		}
		private void txtIngresos_tpAval1_KeyPress(object sender, KeyPressEventArgs e)
		{
			TextBox texbox = txtIngresos_tpAval1;
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
		private void txtOtrosIngresos_tpAval1_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtOtrosIngresos_tpAval1;
			if (texbox.Text.Trim() == ".")
			{
				texbox.Text = "0";
			}
			double ingresos = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ingresos = Convert.ToDouble(texbox.Text);

			texbox.Text = string.Format(String.Format("{0:###,##0.00}", ingresos));
		}
		private void txtDeducciones_tpAval1_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtDeducciones_tpAval1;
			if (texbox.Text.Trim() == ".")
			{
				texbox.Text = "0";
			}
			double ingresos = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ingresos = Convert.ToDouble(texbox.Text);

			texbox.Text = string.Format(String.Format("{0:###,##0.00}", ingresos));
		}
		private void txtOtrosIngresos_tpAval1_KeyPress(object sender, KeyPressEventArgs e)
		{
			TextBox texbox = txtOtrosIngresos_tpAval1;
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
		private void txtDeducciones_tpAval1_KeyPress(object sender, KeyPressEventArgs e)
		{
			TextBox texbox = txtDeducciones_tpAval1;
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

		//FELVIR01 - 20190701
		private void txtNoHijos_tpAval2_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtNoHijos_tpAval2;
			double ntexbox = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ntexbox = Convert.ToDouble(texbox.Text);

			if (texbox.Text.Equals("0"))
			{
				DialogResult result = MessageBox.Show("¿Está seguro que el aval2 no tiene hijos?", "Verificar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				if (result == DialogResult.No)
				{
					return;
				}
			}

			texbox.Text = string.Format(String.Format("{0:#0}", ntexbox));
		}

		private void txtOtrospariente_tpAval2_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtOtrospariente_tpAval2;
			double ntexbox = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ntexbox = Convert.ToDouble(texbox.Text);

			if (texbox.Text.Equals("0"))
			{
				DialogResult result = MessageBox.Show("¿Está seguro que el codeudor no tiene aval2 dependientes?", "Verificar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				if (result == DialogResult.No)
				{
					return;
				}
			}

			texbox.Text = string.Format(String.Format("{0:#0}", ntexbox));
		}

		private void txtCodigo_cliente_TpAval2_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtCodigo_cliente_TpAval2;
			double ntexbox = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ntexbox = Convert.ToDouble(texbox.Text);
			texbox.Text = string.Format(String.Format("{0:##########0}", ntexbox));
		}
		private void txtIngresos_tpAval2_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtIngresos_tpAval2;
			if (texbox.Text.Trim() == ".")
			{
				texbox.Text = "0";
			}
			double monto = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				monto = Convert.ToDouble(texbox.Text);
			texbox.Text = string.Format(String.Format("{0:###,##0.00}", monto));
		}
		private void txtOtrosIngresos_tpAval2_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtOtrosIngresos_tpAval2;
			if (texbox.Text.Trim() == ".")
			{
				texbox.Text = "0";
			}
			double monto = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				monto = Convert.ToDouble(texbox.Text);
			texbox.Text = string.Format(String.Format("{0:###,##0.00}", monto));
		}
		private void txtDeducciones_tpAval2_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtDeducciones_tpAval2;
			if (texbox.Text.Trim() == ".")
			{
				texbox.Text = "0";
			}
			double monto = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				monto = Convert.ToDouble(texbox.Text);
			texbox.Text = string.Format(String.Format("{0:###,##0.00}", monto));
		}
		private void txtIngresos_tpAval2_KeyPress(object sender, KeyPressEventArgs e)
		{
			TextBox texbox = txtIngresos_tpAval2;
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
		private void txtOtrosIngresos_tpAval2_KeyPress(object sender, KeyPressEventArgs e)
		{
			TextBox texbox = txtOtrosIngresos_tpAval2;
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
		private void txtDeducciones_tpAval2_KeyPress(object sender, KeyPressEventArgs e)
		{
			TextBox texbox = txtDeducciones_tpAval2;
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
		private void cbAval1_CheckedChanged(object sender, EventArgs e)
		{
			if (cbAval1.Checked)
			{
				txtId_aval1.Enabled = true;
				cbTieneDeudascanc_aval1.Enabled = true;
			}
			else
			{
				if (cbAval2.Checked & !cbAval1.Checked)
				{
					MessageBox.Show("Si la solicitud ocupa un aval, debe seleccionar el Aval 1 primero", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					cbAval1.Checked = true;
				}
				txtId_aval1.Text = "";
				txtId_aval2.Text = "";
				txtPriNombre_tpAval1.Text = "";
				txtSegNombre_tpAval1.Text = "";
				txtPriApellido_tpAval1.Text = "";
				txtSegApellido_tpAval1.Text = "";
				txtId_aval1.Enabled = false;
				txtId_aval1_Leave(null, null);
				cbTieneDeudascanc_aval1.Checked = false;
				cbTieneDeudascanc_aval1.Enabled = false;
				p_llenar_descripcion_garantia();
			}
		}
		private void cbAval2_CheckedChanged(object sender, EventArgs e)
		{
			if (cbAval2.Checked & !cbAval1.Checked)
			{
				MessageBox.Show("Si la solicitud ocupa un aval, debe seleccionar el Aval 1 primero", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				cbAval2.Checked = false;
				return;
			}
			if (cbAval2.Checked)
			{
				txtId_aval2.Enabled = true;
				cbTieneDeudascanc_aval2.Enabled = true;
			}
			else
			{
				txtId_aval2.Text = "";
				txtPriNombre_tpAval2.Text = "";
				txtSegNombre_tpAval2.Text = "";
				txtPriApellido_tpAval2.Text = "";
				txtSegApellido_tpAval2.Text = "";
				txtId_aval2.Enabled = false;
				txtId_aval2_Leave(null, null);
				cbTieneDeudascanc_aval2.Enabled = false;
				cbTieneDeudascanc_aval2.Checked = false;
				p_llenar_descripcion_garantia();
			}
		}
		private void cbCodeudor_CheckedChanged(object sender, EventArgs e)
		{
			if (cbCodeudor.Checked)
			{
				txtId_codeudor.Enabled = true;
				cbTieneDeudascanc_codeud.Enabled = true;
			}
			else
			{
				txtId_codeudor.Text = "";
				txtId_codeudor.Enabled = false;
				cbTieneDeudascanc_codeud.Enabled = false;
				cbTieneDeudascanc_codeud.Checked = false;
			}
		}
		private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			string modo = txtModo_transunion.Text;
			string xmls = "";
			if (modo == "CREAR")
			{
				xmls = p_construir_xml("CREARSOLICITUD");
			}
			if (modo == "RECALCULAR")
			{
				xmls = p_construir_xml("RECALCULARSOLICITUD");
			}
			xmlViewer forma = new xmlViewer(xmls);
			Int32 no_sol = Convert.ToInt32(txtNo_solicitud_coopsafa.Text);
			forma.da = da;
			forma.p_solicitud = no_sol;
			forma.ShowDialog();
		}
		private void txtAntiguedad_laboral_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtAntiguedad_laboral;
			double ntexbox = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ntexbox = Convert.ToDouble(texbox.Text);
			texbox.Text = string.Format(String.Format("{0:#0}", ntexbox));
		}
		private void txtAntiglaboral_tpConyuge_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtAntiglaboral_tpConyuge;
			double ntexbox = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ntexbox = Convert.ToDouble(texbox.Text);
			texbox.Text = string.Format(String.Format("{0:#0}", ntexbox));
		}
		private void txtAntiglaboral_tpCodeudor_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtAntiglaboral_tpCodeudor;
			double ntexbox = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ntexbox = Convert.ToDouble(texbox.Text);
			texbox.Text = string.Format(String.Format("{0:#0}", ntexbox));
		}
		private void txtAntiglaboral_tpAval1_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtAntiglaboral_tpAval1;
			double ntexbox = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ntexbox = Convert.ToDouble(texbox.Text);
			texbox.Text = string.Format(String.Format("{0:#0}", ntexbox));
		}
		private void txtAntiglaboral_tpAval2_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtAntiglaboral_tpAval2;
			double ntexbox = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ntexbox = Convert.ToDouble(texbox.Text);
			texbox.Text = string.Format(String.Format("{0:#0}", ntexbox));
		}
		private void txtCodigo_cliente_KeyPress(object sender, KeyPressEventArgs e)
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
			// al dar enter pasar al siguiente campo
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}
		private void txtIDSolicitante_KeyPress(object sender, KeyPressEventArgs e)
		{
			// al dar enter pasar al siguiente campo
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}
		private void txtId_conyuge_KeyPress(object sender, KeyPressEventArgs e)
		{
			// al dar enter pasar al siguiente campo
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}
		private void txtId_codeudor_KeyPress(object sender, KeyPressEventArgs e)
		{
			// al dar enter pasar al siguiente campo
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}
		private void txtId_aval2_KeyPress(object sender, KeyPressEventArgs e)
		{
			// al dar enter pasar al siguiente campo
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}
		private void txtId_aval1_KeyPress(object sender, KeyPressEventArgs e)
		{
			// al dar enter pasar al siguiente campo
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}
		private void txtDerechoGanado_KeyPress(object sender, KeyPressEventArgs e)
		{
			TextBox texbox = txtDerechoGanado;
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
		private void txtDerechoGanado_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtDerechoGanado;
			if (texbox.Text.Trim() == ".")
			{
				texbox.Text = "0";
			}
			double ingresos = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ingresos = Convert.ToDouble(texbox.Text);

			texbox.Text = string.Format(String.Format("{0:###,##0.00}", ingresos));
		}
		private void txtCuotas_vencimiento_KeyPress(object sender, KeyPressEventArgs e)
		{
			TextBox texbox = txtMonto_cuotas_vencimiento_sol;
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
		private void txtCuotas_vencimiento_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtMonto_cuotas_vencimiento_sol;
			if (texbox.Text.Trim() == ".")
			{
				texbox.Text = "0";
			}
			double ingresos = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ingresos = Convert.ToDouble(texbox.Text);

			texbox.Text = string.Format(String.Format("{0:###,##0.00}", ingresos));
		}
		private void txtPlazo_Leave(object sender, EventArgs e)
		{
			string vl_controla_parametros_prod = da.ObtenerParametro("WFC-0026");
			if (!string.IsNullOrEmpty(txtCodigo_sub_aplicacion.Text))
			{
				if (vl_controla_parametros_prod.ToUpper() == "S")
				{
					CR_TIPO_PRESTAMO.da = this.da;
					var configuracionproducto = CR_TIPO_PRESTAMO.obtenerTipoPrestamo(Int16.Parse(txtCodigo_sub_aplicacion.Text));

					int plazo = 0;
					int.TryParse(txtPlazo.Text, out plazo);

					if (!string.IsNullOrEmpty(txtPlazo.Text))
					{
						if (plazo < configuracionproducto.Num_mesesplazo_min || plazo > configuracionproducto.Num_mesesplazo)
						{
							MessageBox.Show("Plazo fuera de los limites del producto...!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							return;
						}

						p_calcular_cuota_nivelada();
					}
				}
				else
				{
					p_calcular_cuota_nivelada();
				}
			}
			else
			{
				MessageBox.Show("Seleccione el producto..!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}
		private void txtTasa_Leave(object sender, EventArgs e)
		{
			string vl_controla_parametros_prod = da.ObtenerParametro("WFC-0026");
			if (!string.IsNullOrEmpty(txtCodigo_sub_aplicacion.Text))
			{
				if (vl_controla_parametros_prod.ToUpper() == "S")
				{
					CR_TIPO_PRESTAMO.da = this.da;
					var configuracionproducto = CR_TIPO_PRESTAMO.obtenerTipoPrestamo(Int16.Parse(txtCodigo_sub_aplicacion.Text));

					decimal tasa = 0;
					decimal.TryParse(txtTasa.Text, out tasa);
					if (!string.IsNullOrEmpty(txtTasa.Text))
					{
						if (tasa < configuracionproducto.Por_tasaminima || tasa > configuracionproducto.Por_tasamaxima)
						{
							MessageBox.Show("Tasa fuera de los limites del producto...!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							return;
						}
						p_calcular_cuota_nivelada();
					}
				}
				else
				{
					p_calcular_cuota_nivelada();
				}
			}
			else
			{
				MessageBox.Show("Seleccione el producto..!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}
		#endregion

		private void txtTotal_capital_consolidar_KeyPress(object sender, KeyPressEventArgs e)
		{
			TextBox texbox = txtTotal_capital_consolidar;
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
		private void txtTotal_capital_consolidar_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtTotal_capital_consolidar;
			double valor_ingresado = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				valor_ingresado = Convert.ToDouble(texbox.Text);

			texbox.Text = string.Format(String.Format("{0:###,##0.00}", valor_ingresado));
		}
		private void lLxmlreferencias_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			gxml_referencias = p_construir_xml_referencias();
			xmlViewer forma = new xmlViewer(gxml_referencias);
			forma.ShowDialog();
		}
		private void lLCuotasBuro_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			xmlViewer forma = new xmlViewer(gxml_cuotas_buro);
			forma.ShowDialog();
		}
		private void lLSeltodasCuotas_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			for (int v = 0; v < dtCuotasBuro.Rows.Count; v++)
			{
				gvCuotasBuro.Rows[v].Cells[0].Value = "true";
			}
			calcular_cuotas_consolo(0);

		}
		private void lLDeSeltodasCuotas_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			for (int v = 0; v < dtCuotasBuro.Rows.Count; v++)
			{
				gvCuotasBuro.Rows[v].Cells[0].Value = "false";
			}
			calcular_cuotas_consolo(0);
		}
		private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (string.IsNullOrEmpty(gxml_outputxml))
			{
				MessageBox.Show("No hay xml de respuesta, probablemente no se ha enviado a precalificar la solicitud de credito ..!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			xmlViewer forma = new xmlViewer(gxml_outputxml);
			forma.ShowDialog();
		}
		private void btnMinimizar_Click(object sender, EventArgs e)
		{
			NotifyIcon nIcon = new NotifyIcon();
			this.WindowState = FormWindowState.Minimized;
			Size sizeicon = new System.Drawing.Size(12, 12);

			nIcon.ShowBalloonTip(5000, "CreditScoring Coopsafa", "La aplicacion se ha minizido..", ToolTipIcon.Info);
		}
		private void btnClose_Click(object sender, EventArgs e)
		{
			if (retornar_ok == true)
				this.DialogResult = DialogResult.OK;
			else
			{
				this.Close();
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
		private void labelOficialDeServicio_Click(object sender, EventArgs e)
		{

		}
		private void panel3_MouseDown(object sender, MouseEventArgs e)
		{
			moverForm();
		}
		private void panel3_Click(object sender, EventArgs e)
		{

		}
		private void txtModo_transunion_TextChanged(object sender, EventArgs e)
		{
			labelModalidad_transunion.Text = txtModo_transunion.Text;
			string modo = txtModo_transunion.Text;
			if (modo == "CREAR")
			{
				btnInvocar_precalificado.Text = "Precalificar postulantes";
			}
			if (modo == "RECALCULAR")
			{
				btnInvocar_precalificado.Text = "Recalcular solicitud";
				//Re Obtener Cuotas
				//btnObtener_cuotas_buro.Visible = false;
				//btnObtener_cuotas_buro.Enabled = false;

				string vl_controla_recalculos = da.ObtenerParametro("WFC-0006");
				if (vl_controla_recalculos == "S")
				{
					labelRecalculos.Visible = true;
					int vl_recalculos_actuales = 0;
					int vl_recalculos_permitidos = 0;
					da.ObtenerSituacionVecesPrecalifica(Int32.Parse(txtNo_solicitud_coopsafa.Text), out vl_recalculos_actuales, out vl_recalculos_permitidos);
					labelRecalculos.Text = vl_recalculos_actuales.ToString().Trim() + "/" + vl_recalculos_permitidos.ToString().Trim();
				}

			}

		}
		private void txtValor_vehiculo_KeyPress(object sender, KeyPressEventArgs e)
		{
			TextBox texbox = txtValor_vehiculo;
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
		private void txtValor_vehiculo_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtValor_vehiculo;
			double valor_ingresado = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				valor_ingresado = Convert.ToDouble(texbox.Text);

			texbox.Text = string.Format(String.Format("{0:#,###,###,##0.00}", valor_ingresado));
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
			if (texbox.Text.Trim() == ".")
			{
				texbox.Text = "0";
			}
			double ingresos = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ingresos = Convert.ToDouble(texbox.Text);

			texbox.Text = string.Format(String.Format("{0:###,##0.00}", ingresos));
		}
		private void cmbDestino_credito_SelectionChangeCommitted(object sender, EventArgs e)
		{
			txtDestino_credito.Text = cmbDestino_credito.SelectedValue.ToString();
			gdestino_credito_id = txtDestino_credito.Text;
		}
		private void label7_Click(object sender, EventArgs e)
		{
			cmbDestino_credito.SelectedValue = 1;
		}
		private void btnSalir_Click(object sender, EventArgs e)
		{
			btnClose_Click(null, null);
		}
		private void LlCroquis_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			FrmsRpts.frmRpt_CroquisFormato forma = new FrmsRpts.frmRpt_CroquisFormato();
			forma.gno_solicitud = Int32.Parse(txtNo_solicitud_coopsafa.Text);
			forma.da = da;
			forma.ShowDialog();
		}
		private void label6_Click(object sender, EventArgs e)
		{
			s_consclientes forma = new s_consclientes();
			DialogResult result = forma.ShowDialog();
			if (result == DialogResult.OK)
			{
				txtCodigo_cliente.Text = forma.txtCodigo_cliente.Text;
				txtCodigo_cliente.Focus();
				txtCodigo_cliente_Leave(null, null);
				txtLugar_nacimiento.Focus();

			}

		}
		private void lLreportecredito_codeudor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			string resultado_consulta = da.ObtenerXmlRespuestaEvaluacion(Int32.Parse(txtNo_solicitud_coopsafa.Text));
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

		//FELVIR01 - 20190625
		private void cbTieneDeudascanc_solic_CheckedChanged(object sender, EventArgs e)
		{
			if (cbTieneDeudascanc_solic.Checked)
			{
				txtDeudasCancelados_solic.Enabled = true;
				this.txtMonto_cuotas_vencimiento_sol.Enabled = true;
				this.txtDeudas_desc_sol.Enabled = true;
				txtDeudasCancelados_solic.Focus();
			}
			else
			{
				txtDeudasCancelados_solic.Enabled = false;
				this.txtMonto_cuotas_vencimiento_sol.Enabled = false;
				this.txtDeudas_desc_sol.Enabled = false;
				txtDeudasCancelados_solic.Text = "0.00";
			}
		}

		private void txtDeudasCancelados_solic_KeyPress(object sender, KeyPressEventArgs e)
		{
			TextBox texbox = txtDeudasCancelados_solic;
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
		private void txtDeudasCancelados_solic_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtDeudasCancelados_solic;
			if (texbox.Text.Trim() == ".")
			{
				texbox.Text = "0";
			}
			double deudas_canc_solic = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				deudas_canc_solic = Convert.ToDouble(texbox.Text);

			texbox.Text = string.Format(String.Format("{0:###,##0.00}", deudas_canc_solic));
		}

		//FELVIR01
		private void cbTieneDeudascanc_aval1_CheckedChanged(object sender, EventArgs e)
		{
			if (cbTieneDeudascanc_aval1.Checked)
			{
				txtDeudasCancelados_aval1.Enabled = true;
				//this.txtMonto_cuotas_vencimiento_aval1.Enabled = true;
				//this.txtDeudas_desc_aval1.Enabled = true;
				txtDeudasCancelados_aval1.Focus();
			}
			else
			{
				txtDeudasCancelados_aval1.Enabled = false;
				//this.txtMonto_cuotas_vencimiento_aval1.Enabled = false;
				//this.txtDeudas_desc_aval1.Enabled = false;
				txtDeudasCancelados_aval1.Text = "0.00";
			}
		}

		private void txtDeudasCancelados_aval1_KeyPress(object sender, KeyPressEventArgs e)
		{
			TextBox texbox = txtDeudasCancelados_aval1;
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
		private void txtDeudasCancelados_aval1_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtDeudasCancelados_aval1;
			if (texbox.Text.Trim() == ".")
			{
				texbox.Text = "0";
			}
			double deudas_canc_aval1 = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				deudas_canc_aval1 = Convert.ToDouble(texbox.Text);

			texbox.Text = string.Format(String.Format("{0:###,##0.00}", deudas_canc_aval1));
		}

		//FELVIR01
		private void cbTieneDeudascanc_aval2_CheckedChanged(object sender, EventArgs e)
		{
			if (cbTieneDeudascanc_aval2.Checked)
			{
				txtDeudasCancelados_aval2.Enabled = true;
				//this.txtMonto_cuotas_vencimiento_aval2.Enabled = true;
				//this.txtDeudas_desc_aval2.Enabled = true;
				txtDeudasCancelados_aval2.Focus();
			}
			else
			{
				txtDeudasCancelados_aval2.Enabled = false;
				//this.txtMonto_cuotas_vencimiento_aval2.Enabled = false;
				//this.txtDeudas_desc_aval2.Enabled = false;
				txtDeudasCancelados_aval2.Text = "0.00";
			}
		}

		private void txtDeudasCancelados_aval2_KeyPress(object sender, KeyPressEventArgs e)
		{
			TextBox texbox = txtDeudasCancelados_aval2;
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
		private void txtDeudasCancelados_aval2_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtDeudasCancelados_aval2;
			if (texbox.Text.Trim() == ".")
			{
				texbox.Text = "0";
			}
			double deudas_canc_aval2 = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				deudas_canc_aval2 = Convert.ToDouble(texbox.Text);

			texbox.Text = string.Format(String.Format("{0:###,##0.00}", deudas_canc_aval2));
		}

		//FELVIR01-20190625
		private void cbTieneDeudascanc_codeud_CheckedChanged(object sender, EventArgs e)
		{
			if (cbTieneDeudascanc_codeud.Checked)
			{
				txtDeudasCancelados_codeud.Enabled = true;
				//this.txtMonto_cuotas_vencimiento_co.Enabled = true;
				//this.txtDeudas_desc_co.Enabled = true;
				txtDeudasCancelados_codeud.Focus();
			}
			else
			{
				txtDeudasCancelados_codeud.Enabled = false;
				//this.txtMonto_cuotas_vencimiento_co.Enabled = false;
				//this.txtDeudas_desc_co.Enabled = false;
				txtDeudasCancelados_codeud.Text = "0.00";
			}
		}

		private void txtDeudasCancelados_codeud_KeyPress(object sender, KeyPressEventArgs e)
		{
			TextBox texbox = txtDeudasCancelados_codeud;
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
		private void txtDeudasCancelados_codeud_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtDeudasCancelados_codeud;
			if (texbox.Text.Trim() == ".")
			{
				texbox.Text = "0";
			}
			double deudas_canc_codeudor = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				deudas_canc_codeudor = Convert.ToDouble(texbox.Text);

			texbox.Text = string.Format(String.Format("{0:###,##0.00}", deudas_canc_codeudor));
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
		private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (string.IsNullOrEmpty(txtCodigo_cliente_tpDatosFinanc.Text))
			{
				MessageBox.Show("Seleccione una de las personas de la solicitud...", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			s_PreCalificado_info02 forma = new s_PreCalificado_info02();
			forma.labelTitulo.Text = "Detalle de Creditos afectados en cartera Coopsafa 2 y 3";
			forma.da = da;
			forma.cartera = 2;
			forma.gvDetalleCoposafa2y3.Visible = true;
			forma.labelCodigo_Cliente.Text = txtCodigo_cliente_tpDatosFinanc.Text;
			forma.ShowDialog();
		}
		private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (string.IsNullOrEmpty(txtCodigo_cliente_tpDatosFinanc.Text))
			{
				MessageBox.Show("Seleccione una de las personas de la solicitud...", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			s_PreCalificado_info02 forma = new s_PreCalificado_info02();
			forma.labelTitulo.Text = "Detalle de Creditos afectados en cartera Administrativa con Mora";
			forma.da = da;
			forma.cartera = 1;
			forma.gvDetalleAdtiva.Visible = true;
			forma.labelCodigo_Cliente.Text = txtCodigo_cliente_tpDatosFinanc.Text;
			forma.ShowDialog();
		}
		private void pbFotoVigente_Click(object sender, EventArgs e)
		{
			s_PreCalificado_info03 forma = new s_PreCalificado_info03();
			forma.pbFotoVigente.Image = pbFotoVigente.Image;
			forma.codigo_cliente = txtCodigo_cliente.Text;
			forma.da = da;
			forma.ShowDialog();
		}
		private void gvResultado_buro_DoubleClick(object sender, EventArgs e)
		{
			if (gvResultado_buro.Rows.Count <= 0)
				return;
			DataGridViewRow row = gvResultado_buro.CurrentRow;

			if (row != null)
			{
				string vl_id = gvResultado_buro.Rows[row.Index].Cells["id"].Value.ToString();
				string vl_nombre = gvResultado_buro.Rows[row.Index].Cells["nombre"].Value.ToString();
				string vl_flags = gvResultado_buro.Rows[row.Index].Cells["flags"].Value.ToString();
				string vl_decision = gvResultado_buro.Rows[row.Index].Cells["precalificado"].Value.ToString();
				string vl_score = gvResultado_buro.Rows[row.Index].Cells["score"].Value.ToString();
				string vl_edad = gvResultado_buro.Rows[row.Index].Cells["edad"].Value.ToString();

				s_PreCalificado_info04 forma = new s_PreCalificado_info04();
				forma.id.Text = vl_id;
				forma.nombre.Text = vl_nombre;
				forma.obs.Text = vl_flags.Trim();
				forma.lblDecision.Text = vl_decision;
				forma.labelScore.Text = vl_score;
				forma.edad.Text = vl_edad;
				forma.ShowDialog();
				forma.BringToFront();
				forma.StartPosition = FormStartPosition.CenterParent;
			}
		}

		private void lLPonderacion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			double vl_monto_soli = 0;
			try
			{
				vl_monto_soli = double.Parse(txtMonto_solicitado.Text);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Debe ingresar un monto solicitado ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

			}
			if (vl_monto_soli > 0)
			{
				s_PreCalificado_info05 forma = new s_PreCalificado_info05();
				forma.txtMonto_solicitado.Text = txtMonto_solicitado.Text;
				forma.txtMonto_destino1.Text = monto_destino1.ToString();
				forma.txtMonto_destino2.Text = monto_destino2.ToString();
				forma.da = this.da;
				DialogResult result = forma.ShowDialog();
				if (result == DialogResult.OK)
				{
					try
					{
						monto_destino1 = double.Parse(forma.txtMonto_destino1.Text);
						monto_destino2 = double.Parse(forma.txtMonto_destino2.Text);
					}
					catch
					{

					}
					txtPlazo.Text = forma.txtPlazo_ofrecer.Text;
					txtTasa.Text = forma.txtTasa_ofrecer.Text;
					p_calcular_cuota_nivelada();
					cmbDestino_credito.Focus();
				}
			}
		}
		private void labelRelojPanel_DoubleClick(object sender, EventArgs e)
		{

		}
		private void label269_Click(object sender, EventArgs e)
		{

		}
		private void txtMonto_excluir_refcons_KeyPress(object sender, KeyPressEventArgs e)
		{
			TextBox texbox = txtMonto_excluir_refcons;
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
		private void txtMonto_excluir_refcons_Leave(object sender, EventArgs e)
		{
			TextBox texbox = txtMonto_excluir_refcons;
			if (texbox.Text.Trim() == ".")
			{
				texbox.Text = "0";
			}
			double ingresos = 0;
			if (!string.IsNullOrEmpty(texbox.Text))
				ingresos = Convert.ToDouble(texbox.Text);

			texbox.Text = string.Format(String.Format("{0:###,##0.00}", ingresos));
			p_calcular_indice_concentracion_deuda();
		}
		private void rbLimite1_Click(object sender, EventArgs e)
		{

		}
		private void rbLimite2_Click(object sender, EventArgs e)
		{

		}
		private void lLinfoCEF_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			s_cefinfodocentes_doc forma = new s_cefinfodocentes_doc();
			forma.da = this.da;
			forma.sidentificacion = txtIDSolicitante.Text;
			forma.ShowDialog();
		}
		private void lLVer_paramproduc_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (string.IsNullOrEmpty(txtCodigo_sub_aplicacion.Text))
			{
				return;
			}
			CR_TIPO_PRESTAMO.da = this.da;
			var configuracionproducto = CR_TIPO_PRESTAMO.obtenerTipoPrestamo(Int16.Parse(txtCodigo_sub_aplicacion.Text));
			s_infoproducto forma = new s_infoproducto(configuracionproducto);
			forma.ShowDialog();
		}

		private void pbOficialServ_Click(object sender, EventArgs e)
		{

			if (goficial_servicio == goficial_serviciodesolicitud)
			{
				s_usuario_profile forma = new s_usuario_profile(goficial_serviciodesolicitud);
				forma.da = this.da;
				forma.ShowDialog();
			}
			else
			{
				s_miniinfo_usuario miniinfo_user = new s_miniinfo_usuario();
				miniinfo_user.get_set_codigo_usuario = goficial_serviciodesolicitud;
				miniinfo_user.da = this.da;
				miniinfo_user.ShowDialog();
				miniinfo_user.Refresh();

			}


		}

		private void graficar_score_crediticio()
		{
			if (da.ObtenerParametro("WFC-0028") == "S")
			{
				this.Invoke(new Functionz(delegate ()
				{
					pnlScoreCrediticio.Visible = true;
				}));


				Int16 vl_escore = 0;
				Int16.TryParse(txtScore.Text, out vl_escore);
				if (vl_escore > -1000000)
				{
					var infografico = da.p_ObtenerInfoGraficarScore(vl_escore);
					lblrango_scala1.Text = infografico.rango_escala1;
					lblrango_scala2.Text = infografico.rango_escala2;
					lblResul_score.Text = infografico.resultado;

					if (infografico.img_indice == 1)
					{
						pbScore_result.Image = Properties.Resources.score_crediticio_meter1;
					}
					if (infografico.img_indice == 2)
					{
						pbScore_result.Image = Properties.Resources.score_crediticio_meter2;
					}
					if (infografico.img_indice == 3)
					{
						pbScore_result.Image = Properties.Resources.score_crediticio_meter3;
					}
					if (infografico.img_indice == 4)
					{
						pbScore_result.Image = Properties.Resources.score_crediticio_meter4;
					}
					if (infografico.img_indice == 5)
					{
						pbScore_result.Image = Properties.Resources.score_crediticio_meter5;
					}
				}
			}
			else
			{
				pnlScoreCrediticio.Visible = false;
			}
		}

		private void cmbFondos_SelectionChangeCommitted(object sender, EventArgs e)
		{
			int vl_codigo_ff = 0;
			txtCodigo_fuente_fondos.Text = cmbFondos.SelectedValue.ToString();
			gfuente_financiamiento = cmbFondos.SelectedValue.ToString();
			int.TryParse(txtCodigo_fuente_fondos.Text, out vl_codigo_ff);
			p_llenar_combo_sub_aplicaciones();

		}

		private void comboBox_sub_aplicacion_DropDown(object sender, EventArgs e)
		{
			//p_llenar_combo_sub_aplicaciones();
		}

		//FELVIR01
		private void btnActualizarInfo_Click(object sender, EventArgs e)
		{
			/*
			 * Actualiza los datos del solicitante
			 * Si existe codeudor lo actualiza
			 * Si existe Aval1 lo actualiza
			 * Si existe Aval2 lo actualiza
			 */
			this.p_get_datos_solicitantexID(this.txtIDSolicitante.Text);
			if (this.cbCodeudor.Checked)
				this.txtId_codeudor_Leave(null, null);
			if (this.cbAval1.Checked)
				this.txtId_aval1_Leave(null, null);
			if (this.cbAval2.Checked)
				this.txtId_aval2_Leave(null, null);
		}

		//FELVIR01 - 20190619
		private void btnGaranteHipotecario_Click(object sender, EventArgs e)
		{
			mostrar_tab(this.tpGaranteHipotecario);
		}

		private void btnGarante_Click(object sender, EventArgs e)
		{
			this.mostrar_tab(this.tpGaranteHipotecario);
		}

		private void txtId_tpGarante_KeyPress(object sender, KeyPressEventArgs e)
		{
			// al dar enter pasar al siguiente campo
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtId_tpGarante_Leave(object sender, EventArgs e)
		{
			if (this.txtId_tpGarante.Text != "")
			{

				if (!this.EvaluarIdentificacion(this.txtId_tpGarante.Text))
				{
					MessageBox.Show("El formato de identificación no es válido, debe llevar guiones. Vaya a la solicitud de Afiliación para corregir", "Ir a la Solicitud de Afiliación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					this.skipMethod();
				}

				DataTable dt = da.ObtenerDatosClientexIdentificacion(this.txtId_tpGarante.Text);
				if (dt.Rows.Count > 0)
				{
					string codigo_cliente = dt.Rows[0]["codigo_cliente"].ToString();
					p_get_datos_Garante(this.txtId_tpGarante.Text);
				}
			}
		}

		private void p_get_datos_Garante(string p_no_identificacion)
		{
			DataTable dt = da.ObtenerDatosClientexIdentificacion(p_no_identificacion);
			if (dt.Rows.Count > 0)
			{
				if (!gmodo_coopsafa.Equals("UPD"))
				{
					string codigo_cliente = dt.Rows[0]["codigo_cliente"].ToString();
					this.txtCodigoCliente_Garante.Text = codigo_cliente;
				}

				string[] nombres = dt.Rows[0]["nombres"].ToString().Split(' ');
				if (nombres.Length >= 2)
				{
					txtPrimerNom_tpGarante.Text = nombres[0];
					txtSegundoNom_tpGarante.Text = nombres[1];
				}
				else
				{
					txtPrimerNom_tpGarante.Text = dt.Rows[0]["nombres"].ToString();
				}

				txtPrimerAp_tpGarante.Text = dt.Rows[0]["primer_apellido"].ToString();
				txtSegundoAp_tpGarante.Text = dt.Rows[0]["segundo_apellido"].ToString();
				if (!gmodo_coopsafa.Equals("UPD"))
				{
					string sexo = dt.Rows[0]["sexo"].ToString();
					if (sexo == "Masculino")
					{
						this.rbMasculino_tpGarante.Checked = true;
						this.rbFemenino_tpGarante.Checked = false;
					}
					else
					{
						this.rbMasculino_tpGarante.Checked = false;
						this.rbFemenino_tpGarante.Checked = true;
					}
				}

				//this.txtEstadoCivil_tpGarante.Text = dt.Rows[0]["estado_civil"].ToString();
				string fec = dt.Rows[0]["fecha_de_nacimiento"].ToString();
				var convertida = Convert.ToDateTime(fec);
				int edad = DateTime.Today.AddTicks(-convertida.Ticks).Year - 1;
				this.txtEdadGarante.Text = edad.ToString();

				var dep = this.da.ObtenerDependientes(int.Parse(this.txtCodigo_cliente_TpAval2.Text));
				this.txtHijos_tpGarante.Text = dep.Rows[0]["hijos"].ToString();
				this.txtOtrosDep_tpGarante.Text = dep.Rows[0]["otros"].ToString();

				this.txtDirecRes_tpGarante.Text = dt.Rows[0]["direccion_res"].ToString();
				this.txtTelFijo_tpGarante.Text = dt.Rows[0]["telefono_casa"].ToString();
				this.txtCelular_tpGarante.Text = dt.Rows[0]["celular"].ToString();
				this.txtCorreo_tpGarante.Text = dt.Rows[0]["correo"].ToString().ToLower();

				#region afiliado si o no

				string codigo_tipo_cliente = dt.Rows[0]["codigo_tipo_cliente"].ToString();
				if (codigo_tipo_cliente == "5") // 5 es afiliado 9 es Aval
				{
					this.rbEsAfiliadoSi_tpGarante.Checked = true;
					this.txtCodigoCliente_Garante.Text = dt.Rows[0]["codigo_cliente"].ToString();
				}
				else
				{
					this.rbEsAfiliadoSi_tpGarante.Checked = false;
					this.rbEsAfiliadoNo_tpGarante.Checked = true;
					this.txtCodigoCliente_Garante.Text = "0";
				}

				#endregion


				this.txtPatrono_Garante.Text = dt.Rows[0]["lugar_de_trabajo"].ToString();
				this.txtCargo_Garante.Text = dt.Rows[0]["nombre_cargo"].ToString();
				this.txtDepto_Garante.Text = dt.Rows[0]["persona_a_contactar_2"].ToString();
				string antiguedad_laboral = dt.Rows[0]["antiguedad_laboral_meses"].ToString();
				if (!string.IsNullOrEmpty(antiguedad_laboral))
				{
					this.txtAntiguedad_Garante.Text = antiguedad_laboral;
				}
				this.txtDireccionLab_Garante.Text = dt.Rows[0]["direccion_lab"].ToString();
				this.txtTel_LabGarante.Text = dt.Rows[0]["telefono_trabajo"].ToString();
				this.txtTel_Labo2Garante.Text = dt.Rows[0]["otro_telefono"].ToString();
				this.txtCorreoLab_Garante.Text = dt.Rows[0]["correo_lab"].ToString();

				string estado_civil = dt.Rows[0]["estado_civil"].ToString();
				if (estado_civil.Equals("Casado") || estado_civil.Equals("Union Libre"))
				{
					this.txtConyuge_Garante.Text = dt.Rows[0]["NOMBRE_CONJUGUE"].ToString();
					this.txtDirLabCony_Garante.Text = dt.Rows[0]["lugar_trabajo_conyuge"].ToString();
				}

				//Referencias en duro 1 y 2
				DataTable dtRef = da.ObtenerReferenciasxCodigoCliente(this.txtCodigoCliente_Garante.Text);
				//limpiando por si se ha hecho una consulta previa o se cambio la identificacion
				this.txtRef1Nom_Garante.Text = string.Empty;
				this.txtRef1Res_Garante.Text = string.Empty;
				this.txtRef1Tel_Garante.Text = string.Empty;

				this.txtRef2Nom_Garante.Text = string.Empty;
				this.txtRef1Res_Garante.Text = string.Empty;
				this.txtRef1Tel_Garante.Text = string.Empty;

				for (int fila = 0; fila < dtRef.Rows.Count; fila++)
				{
					if (fila == 0)
					{
						this.txtRef1Nom_Garante.Text = dtRef.Rows[fila]["nombre"].ToString();
						this.txtRef1Res_Garante.Text = dtRef.Rows[fila]["metodo_localizacion"].ToString();
						this.txtRef1Tel_Garante.Text = dtRef.Rows[fila]["telefono"].ToString();
						//FELVIR01 - 20190607
						this.txtRef1PtoRef_Garante.Text = dtRef.Rows[fila]["pto_referencia"].ToString();
						this.txtRef1Cel_Garante.Text = dtRef.Rows[fila]["celular"].ToString();

					}
					if (fila == 1)
					{
						this.txtRef2Nom_Garante.Text = dtRef.Rows[fila]["nombre"].ToString();
						this.txtRef2Res_Garante.Text = dtRef.Rows[fila]["metodo_localizacion"].ToString();
						this.txtRef2Tel_Garante.Text = dtRef.Rows[fila]["telefono"].ToString();
						//FELVIR01 - 20190607
						this.txtRef2PtoRef_Garante.Text = dtRef.Rows[fila]["pto_referencia"].ToString();
						this.txtRef2Cel_Garante.Text = dtRef.Rows[fila]["celular"].ToString();
					}
					if (fila == 2)
					{
						this.txtRef3Nom_Garante.Text = dtRef.Rows[fila]["nombre"].ToString();
						this.txtRef3Res_Garante.Text = dtRef.Rows[fila]["metodo_localizacion"].ToString();
						this.txtRef3Tel_Garante.Text = dtRef.Rows[fila]["telefono"].ToString();
						break;
					}
				}

				if (!gmodo_coopsafa.Equals("UPD"))
				{
					/*saldo_aportaciones_aval2 = da.ObtenerSaldosAportacionesxCliente(txtId_aval2.Text);
					estado_burointerno_aval2 = da.ObtenerBuroInterno_xId(txtId_aval2.Text, out estado_desc_burointerno_aval2, out observa_burointerno_aval2).ToString();*/

					gvInfoFinanciera.Refresh();
				}

				p_llenar_descripcion_garantia();
			}
			else
			{
				this.txtId_tpGarante.Text = string.Empty;
				this.txtPrimerNom_tpGarante.Text = string.Empty;
				this.txtSegundoNom_tpGarante.Text = string.Empty;
				this.txtPrimerAp_tpGarante.Text = string.Empty;
				this.txtSegundoAp_tpGarante.Text = string.Empty;
				this.txtHijos_tpGarante.Text = "0";
				this.txtOtrosDep_tpGarante.Text = "0";
				this.txtEdadGarante.Text = "0";
				this.txtDirecRes_tpGarante.Text = string.Empty;
				this.txtTelFijo_tpGarante.Text = string.Empty;
				this.txtCelular_tpGarante.Text = string.Empty;
				this.txtTelAdic1_tpGarante.Text = string.Empty;
				this.txtTelAdic2_tpGarante.Text = string.Empty;
				this.txtCorreo_tpGarante.Text = string.Empty;
				this.txtCodigoCliente_Garante.Text = string.Empty;
				this.txtViviendaOtros_Garante.Text = string.Empty;
				this.txtOtrosEmpresa_Garante.Text = string.Empty;
				this.txtPatrono_Garante.Text = string.Empty;
				this.txtDepto_Garante.Text = string.Empty;
				this.txtCargo_Garante.Text = string.Empty;
				this.txtAntiguedad_Garante.Text = string.Empty;
				this.txtTel_LabGarante.Text = string.Empty;
				this.txtTel_Labo2Garante.Text = string.Empty;
				this.txtExt1_Garante.Text = string.Empty;
				this.txtExt2_Garante.Text = string.Empty;
				this.txtDireccionLab_Garante.Text = string.Empty;
				this.txtCorreoLab_Garante.Text = string.Empty;
				this.txtConyuge_Garante.Text = string.Empty;
				this.txtDirLabCony_Garante.Text = string.Empty;
				this.txtCargoConyu_Garante.Text = string.Empty;
				//Referencias
				this.txtRef1Nom_Garante.Text = string.Empty;
				this.txtRef1Res_Garante.Text = string.Empty;
				this.txtRef1Tel_Garante.Text = string.Empty;
				this.txtRef1PtoRef_Garante.Text = string.Empty;
				this.txtRef1Cel_Garante.Text = string.Empty;
				this.txtRef2Nom_Garante.Text = string.Empty;
				this.txtRef2Res_Garante.Text = string.Empty;
				this.txtRef2Tel_Garante.Text = string.Empty;
				this.txtRef2PtoRef_Garante.Text = string.Empty;
				this.txtRef2Cel_Garante.Text = string.Empty;
				this.txtRef3Nom_Garante.Text = string.Empty;
				this.txtRef3Res_Garante.Text = string.Empty;
				this.txtRef3Tel_Garante.Text = string.Empty;
			}
		}

		private void txtPrimerNom_tpGarante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtPrimerAp_tpGarante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtSegundoNom_tpGarante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtSegundoAp_tpGarante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void rbMasculino_tpGarante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void rbFemenino_tpGarante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtHijos_tpGarante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtOtrosDep_tpGarante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtEstadoCivil_tpGarante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtEdadGarante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtDirecRes_tpGarante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtTelFijo_tpGarante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtCelular_tpGarante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtTelAdi1_tpGarante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtTelAdic1_tpGarante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtCorreo_tpGarante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void rbEsAfiliadoSi_tpGarante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void rbEsAfiliadoNo_tpGarante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtCodigoCliente_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void rbPropia_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void rbPagando_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void rbAlquilada_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void rbFamiliar_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void rbOtros_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtViviendaOtros_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void rbPrivado_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void rbPublico_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void rbComerciante_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void rbOtrosEmpresa_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtOtrosEmpresa_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtPatrono_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtDepto_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtCargo_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtAntiguedad_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtTel_LabGarante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtExt1_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtTel_Labo2Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtExt2_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtDireccionLab_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtCorreoLab_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtConyuge_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtDirLab_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtCargoConyu_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtRef1Nom_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtRef1Res_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtRef1Tel_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtRef1PtoRef_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtRef1Cel_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtRef2Nom_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtRef2Res_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtRef2Tel_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtRef2PtoRef_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtRef2Cel_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtRef3Nom_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtRef3Res_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void txtRef3Tel_Garante_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private void cbGarante_CheckedChanged(object sender, EventArgs e)
		{
			if (this.cbGarante.Checked)
			{
				this.BloquearGarante(true);
			}
			else
			{
				this.BloquearGarante();
				this.BloquearGarante(false);
			}
		}

		private void BloquearGarante(bool opcion)
		{
			this.txtId_tpGarante.Enabled = opcion;
			this.txtPrimerNom_tpGarante.Enabled = opcion;
			this.txtSegundoAp_tpGarante.Enabled = opcion;
			this.txtPrimerAp_tpGarante.Enabled = opcion;
			this.txtSegundoAp_tpGarante.Enabled = opcion;
			this.rbMasculino_tpGarante.Enabled = opcion;
			this.rbFemenino_tpGarante.Enabled = opcion;
			this.txtHijos_tpGarante.Enabled = opcion;
			this.txtOtrosDep_tpGarante.Enabled = opcion;
			this.cmbEstadoCivil.Enabled = opcion;
			this.txtEdadGarante.Enabled = opcion;
			this.txtDirecRes_tpGarante.Enabled = opcion;
			this.txtTelFijo_tpGarante.Enabled = opcion;
			this.txtCelular_tpGarante.Enabled = opcion;
			this.txtTelAdic1_tpGarante.Enabled = opcion;
			this.txtTelAdic2_tpGarante.Enabled = opcion;
			this.txtCorreoLab_Garante.Enabled = opcion;
			this.rbEsAfiliadoSi_tpGarante.Enabled = opcion;
			this.rbEsAfiliadoNo_tpGarante.Enabled = opcion;
			this.txtCodigoCliente_Garante.Enabled = opcion;
			this.rbPropia_Garante.Enabled = opcion;
			this.rbPagando_Garante.Enabled = opcion;
			this.rbAlquilada_Garante.Enabled = opcion;
			this.rbFamiliar_Garante.Enabled = opcion;
			this.rbOtros_Garante.Enabled = opcion;
			this.txtViviendaOtros_Garante.Enabled = opcion;
			this.rbPrivado_Garante.Enabled = opcion;
			this.rbPublico_Garante.Enabled = opcion;
			this.rbComerciante_Garante.Enabled = opcion;
			this.rbOtrosEmpresa_Garante.Enabled = opcion;
			this.txtOtrosEmpresa_Garante.Enabled = opcion;
			this.txtPatrono_Garante.Enabled = opcion;
			this.txtDepto_Garante.Enabled = opcion;
			this.txtCargo_Garante.Enabled = opcion;
			this.txtAntiguedad_Garante.Enabled = opcion;
			this.txtTel_LabGarante.Enabled = opcion;
			this.txtTel_Labo2Garante.Enabled = opcion;
			this.txtExt1_Garante.Enabled = opcion;
			this.txtExt2_Garante.Enabled = opcion;
			this.txtDireccionLab_Garante.Enabled = opcion;
			this.txtCorreoLab_Garante.Enabled = opcion;
			this.txtConyuge_Garante.Enabled = opcion;
			this.txtDirLabCony_Garante.Enabled = opcion;
			this.txtCargoConyu_Garante.Enabled = opcion;
			this.txtRef1Nom_Garante.Enabled = opcion;
			this.txtRef1Res_Garante.Enabled = opcion;
			this.txtRef1Tel_Garante.Enabled = opcion;
			this.txtRef1PtoRef_Garante.Enabled = opcion;
			this.txtRef1Cel_Garante.Enabled = opcion;
			this.txtRef2Nom_Garante.Enabled = opcion;
			this.txtRef2Res_Garante.Enabled = opcion;
			this.txtRef2Tel_Garante.Enabled = opcion;
			this.txtRef2PtoRef_Garante.Enabled = opcion;
			this.txtRef2Cel_Garante.Enabled = opcion;
			this.txtRef3Nom_Garante.Enabled = opcion;
			this.txtRef3Res_Garante.Enabled = opcion;
			this.txtRef3Tel_Garante.Enabled = opcion;
			this.txtDirecRes_tpGarante.Enabled = opcion;
			this.txtCorreo_tpGarante.Enabled = opcion;
		}

		private void BloquearGarante()
		{
			this.txtId_tpGarante.Text = string.Empty;
			this.txtPrimerNom_tpGarante.Text = string.Empty;
			this.txtSegundoAp_tpGarante.Text = string.Empty;
			this.txtPrimerAp_tpGarante.Text = string.Empty;
			this.txtSegundoAp_tpGarante.Text = string.Empty;
			//this.rbMasculino_tpGarante.Text = opcion;
			//this.rbFemenino_tpGarante.Text = opcion;
			this.txtHijos_tpGarante.Text = string.Empty;
			this.txtOtrosDep_tpGarante.Text = string.Empty;
			//this.cmbEstadoCivil.Enabled = opcion;
			this.txtEdadGarante.Text = string.Empty;
			this.txtDireccion_res.Text = string.Empty;
			this.txtTelFijo_tpGarante.Text = string.Empty;
			this.txtCelular_tpGarante.Text = string.Empty;
			this.txtTelAdic1_tpGarante.Text = string.Empty;
			this.txtTelAdic2_tpGarante.Text = string.Empty;
			this.txtCorreoLab_Garante.Text = string.Empty;
			//this.rbEsAfiliadoSi_tpGarante.Enabled = opcion;
			//this.rbEsAfiliadoNo_tpGarante.Enabled = opcion;
			this.txtCodigoCliente_Garante.Text = string.Empty;
			//this.rbPropia_Garante.Enabled = opcion;
			//this.rbPagando_Garante.Enabled = opcion;
			//this.rbAlquilada_Garante.Enabled = opcion;
			//this.rbFamiliar_Garante.Enabled = opcion;
			//this.rbOtros_Garante.Enabled = opcion;
			this.txtViviendaOtros_Garante.Text = string.Empty;
			//this.rbPrivado_Garante.Enabled = opcion;
			//this.rbPublico_Garante.Enabled = opcion;
			//this.rbComerciante_Garante.Enabled = opcion;
			//this.rbOtrosEmpresa_Garante.Enabled = opcion;
			this.txtOtrosEmpresa_Garante.Text = string.Empty;
			this.txtPatrono_Garante.Text = string.Empty;
			this.txtDepto_Garante.Text = string.Empty;
			this.txtCargo_Garante.Text = string.Empty;
			this.txtAntiguedad_Garante.Text = string.Empty;
			this.txtTel_LabGarante.Text = string.Empty;
			this.txtTel_Labo2Garante.Text = string.Empty;
			this.txtExt1_Garante.Text = string.Empty;
			this.txtExt2_Garante.Text = string.Empty;
			this.txtDireccionLab_Garante.Text = string.Empty;
			this.txtCorreoLab_Garante.Text = string.Empty;
			this.txtConyuge_Garante.Text = string.Empty;
			this.txtDirLabCony_Garante.Text = string.Empty;
			this.txtCargoConyu_Garante.Text = string.Empty;
			this.txtRef1Nom_Garante.Text = string.Empty;
			this.txtRef1Res_Garante.Text = string.Empty;
			this.txtRef1Tel_Garante.Text = string.Empty;
			this.txtRef1PtoRef_Garante.Text = string.Empty;
			this.txtRef1Cel_Garante.Text = string.Empty;
			this.txtRef2Nom_Garante.Text = string.Empty;
			this.txtRef2Res_Garante.Text = string.Empty;
			this.txtRef2Tel_Garante.Text = string.Empty;
			this.txtRef2PtoRef_Garante.Text = string.Empty;
			this.txtRef2Cel_Garante.Text = string.Empty;
			this.txtRef3Nom_Garante.Text = string.Empty;
			this.txtRef3Res_Garante.Text = string.Empty;
			this.txtRef3Tel_Garante.Text = string.Empty;
			this.txtDirecRes_tpGarante.Text = string.Empty;
			this.txtCorreo_tpGarante.Text = string.Empty;
		}

		private void ReiniciarCamposSolicitante()
		{
			txtCodigo_cliente.Text = string.Empty;
			txtIDSolicitante.Text = string.Empty;
			txtNombre.Text = string.Empty;
			txtPrimer_apellido.Text = string.Empty;
			txtSegundo_apellido.Text = string.Empty;
			txtApellido_casada.Text = string.Empty;
			txtEstado_civil.Text = string.Empty;
			txtSexo.Text = string.Empty;
			txtFecha_nacimiento.Text = string.Empty;
			txtNacionalidad.Text = string.Empty;
			txtDireccion_res.Text = string.Empty;
			txtTelefono_fijo.Text = string.Empty;
			txtCelular.Text = string.Empty;
			txtCorreo_personal.Text = string.Empty;
			txtPatrono.Text = string.Empty;
			txtCargo.Text = string.Empty;
			txtDireccion_lab.Text = string.Empty;
			txtTelefono_trabajo1.Text = string.Empty;
			txtTelefono_trabajo2.Text = string.Empty;
			txtCorreo_laboral.Text = string.Empty;
			txtVentanilla_planilla.Text = string.Empty;
			txtRef1.Text = string.Empty;
			txtRef1_direc.Text = string.Empty;
			txtRef1_telef.Text = string.Empty;
			txtRef2.Text = string.Empty;
			txtRef2_direc.Text = string.Empty;
			txtRef2_telef.Text = string.Empty;
			this.txtIngresos.Text = string.Empty;
			this.txtOtros_ingresos.Text = string.Empty;
			this.txtNoHijos.Text = string.Empty;
			this.txtOtrospariente.Text = string.Empty;
			this.txtTipo_vivienda_especificar.Text = string.Empty;
			this.txtTipo_empresa_especificar.Text = string.Empty;

			#region Conyuge
			this.txtId_conyuge.Text = string.Empty;
			txtPriNombre_tpConyuge.Text = string.Empty;
			txtSegNombre_tpConyuge.Text = string.Empty;
			txtPriApellido_tpConyuge.Text = string.Empty;
			txtSegApellido_tpConyuge.Text = string.Empty;
			this.txtNoHijos_tpConyuge.Text = string.Empty;
			this.txtOtrospariente_tpConyuge.Text = string.Empty;
			this.txtOtrotelefono1_tpConyuge.Text = string.Empty;
			this.txtOtrotelefono2_tpConyuge.Text = string.Empty;
			txtDirecc_res_tpConyuge.Text = string.Empty;
			txtTelefono_tpConyuge.Text = string.Empty;
			txtCelular_tpConyuge.Text = string.Empty;
			txtCorreo_tpConyuge.Text = string.Empty;
			this.txtCodigo_cliente_TpConyuge.Text = string.Empty;
			this.txtPatrono_tpConyuge.Text = string.Empty;
			txtCargo_tpConyuge.Text = string.Empty;
			txtAntiglaboral_tpConyuge.Text = string.Empty;
			txtDirecclaboral_tpConyuge.Text = string.Empty;
			txtTellaboral1_tpConyuge.Text = string.Empty;
			this.txtExtlaboral1_tpConyuge.Text = string.Empty;
			txtTellaboral2_tpConyuge.Text = string.Empty;
			this.txtExtlaboral2_tpConyuge.Text = string.Empty;
			txtCorreolaboral_tpConyuge.Text = string.Empty;
			this.txtIngresos_tpConyuge.Text = string.Empty;
			this.txtOtrosIngresos_tpConyuge.Text = string.Empty;
			this.rbPrimario_Conyuge.Checked = true;
			this.txtProfesionCoyuge.Text = string.Empty;
			this.txtDeducciones_tpConyuge.Text = string.Empty;
			this.txtTipoEmpresaotros_tpCoyuge.Text = string.Empty;
			this.txtDeptolabora_tpConyuge.Text = string.Empty;
			#endregion

			#region Codeudor

			txtPriNombre_tpCodeudor.Text = "";
			txtSegNombre_tpCodeudor.Text = "";
			txtPriApellido_tpCodeudor.Text = "";
			txtSegApellido_tpCodeudor.Text = "";
			txtDirecc_res_tpCodeudor.Text = "";
			txtTelefono_tpCodeudor.Text = "";
			txtCelular_tpCodeudor.Text = "";
			txtCorreo_tpCodeudor.Text = "";
			txtPatrono_tpCodeudor.Text = "";
			txtCargo_tpCodeudor.Text = "";
			txtAntiglaboral_tpCodeudor.Text = "";
			txtDirecclaboral_tpCodeudor.Text = "";
			txtTellaboral1_tpCodeudor.Text = "";
			txtTellaboral2_tpCodeudor.Text = "";
			txtCorreolaboral_tpCodeudor.Text = "";

			txtRef1_tpCodeudor.Text = "";
			txtRef1_direc_tpCodeudor.Text = "";
			txtRef1_telef_tpCodeudor.Text = "";
			txtRef2_tpCodeudor.Text = "";
			txtRef2_direc_tpCodeudor.Text = "";
			txtRef2_telef_tpCodeudor.Text = "";
			this.txtIngresoLab_Co.Text = string.Empty;
			this.txtId_codeudor.Text = string.Empty;
			this.txtEstadoCivil_tpCodeudor.Text = string.Empty;
			this.txtOtrotelefono1_tpCodeudor.Text = string.Empty;
			this.txtOtrotelefono2_tpConyuge.Text = string.Empty;
			this.txtCodigo_cliente_TpCodeudor.Text = string.Empty;
			//Referencias
			txtRef1_tpCodeudor.Text = "";
			txtRef1_direc_tpCodeudor.Text = "";
			txtRef1_telef_tpCodeudor.Text = "";
			this.txtRef1_casacolor_tpCodeudor.Text = string.Empty;
			this.txtRef1_ptoref_tpCodeudor.Text = string.Empty;
			txtRef2_tpCodeudor.Text = "";
			txtRef2_direc_tpCodeudor.Text = "";
			txtRef2_telef_tpCodeudor.Text = "";
			this.txtRef2_casacolor_tpCodeudor.Text = string.Empty;
			this.txtRef2_ptoref_tpCodeudor.Text = string.Empty;
			this.txtRef3_tpCodeudor.Text = string.Empty;
			this.txtRef3_direc_tpCodeudor.Text = string.Empty;
			this.txtRef3_telef_tpCodeudor.Text = string.Empty;
			this.txtRef3_telef_tpCodeudor.Text = string.Empty;
			#endregion

			#region Aval1
			txtPriNombre_tpAval1.Text = "";
			txtSegNombre_tpAval1.Text = "";
			txtPriApellido_tpAval1.Text = "";
			txtSegApellido_tpAval1.Text = "";
			txtDirecc_res_tpAval1.Text = "";
			txtTelefono_tpAval1.Text = "";
			this.txtEstadoCivil_tpAval1.Text = string.Empty;
			txtCelular_tpAval1.Text = "";
			txtCorreo_tpAval1.Text = "";
			txtPatrono_tpAval1.Text = "";
			txtCargo_tpAval1.Text = "";
			txtAntiglaboral_tpAval1.Text = "";
			txtDirecclaboral_tpAval1.Text = "";
			txtTellaboral1_tpAval1.Text = "";
			txtTellaboral2_tpAval1.Text = "";
			txtCorreolaboral_tpAval1.Text = "";
			txtRef1_tpAval1.Text = "";
			txtRef1_direc_tpAval1.Text = "";
			txtRef1_telef_tpAval1.Text = "";
			txtRef2_tpAval1.Text = "";
			txtRef2_direc_tpAval1.Text = "";
			txtRef2_telef_tpAval1.Text = "";
			this.txtRef3_tpAval1.Text = string.Empty;
			this.txtRef3_direc_tpAval1.Text = string.Empty;
			this.txtRef3_telef_tpAval1.Text = string.Empty;
			#endregion

			#region Aval2
			txtPriNombre_tpAval2.Text = "";
			txtSegNombre_tpAval2.Text = "";
			txtPriApellido_tpAval2.Text = "";
			txtSegApellido_tpAval2.Text = "";
			txtDirecc_res_tpAval2.Text = "";
			txtTelefono_tpAval2.Text = "";
			this.txtEstadoCivil_tpAval2.Text = string.Empty;
			txtCelular_tpAval2.Text = "";
			txtCorreo_tpAval2.Text = "";
			txtPatrono_tpAval2.Text = "";
			txtCargo_tpAval2.Text = "";
			txtAntiglaboral_tpAval2.Text = "";
			txtDirecclaboral_tpAval2.Text = "";
			txtTellaboral1_tpAval2.Text = "";
			txtTellaboral2_tpAval2.Text = "";
			txtCorreolaboral_tpAval2.Text = "";
			txtRef1_tpAval2.Text = "";
			txtRef1_direc_tpAval2.Text = "";
			txtRef1_telef_tpAval2.Text = "";
			txtRef2_tpAval2.Text = "";
			txtRef2_direc_tpAval2.Text = "";
			txtRef2_telef_tpAval2.Text = "";
			this.txtRef3_tpAval2.Text = string.Empty;
			this.txtRef3_direc_tpAval2.Text = string.Empty;
			this.txtRef3_telef_tpAval2.Text = string.Empty;
			#endregion

			#region Garante

			this.txtId_tpGarante.Text = string.Empty;
			this.txtPrimerNom_tpGarante.Text = string.Empty;
			this.txtSegundoNom_tpGarante.Text = string.Empty;
			this.txtPrimerAp_tpGarante.Text = string.Empty;
			this.txtSegundoAp_tpGarante.Text = string.Empty;
			this.txtHijos_tpGarante.Text = "0";
			this.txtOtrosDep_tpGarante.Text = "0";
			this.txtEdadGarante.Text = "0";
			this.txtDirecRes_tpGarante.Text = string.Empty;
			this.txtTelFijo_tpGarante.Text = string.Empty;
			this.txtCelular_tpGarante.Text = string.Empty;
			this.txtTelAdic1_tpGarante.Text = string.Empty;
			this.txtTelAdic2_tpGarante.Text = string.Empty;
			this.txtCorreo_tpGarante.Text = string.Empty;
			this.txtCodigoCliente_Garante.Text = string.Empty;
			this.txtViviendaOtros_Garante.Text = string.Empty;
			this.txtOtrosEmpresa_Garante.Text = string.Empty;
			this.txtPatrono_Garante.Text = string.Empty;
			this.txtDepto_Garante.Text = string.Empty;
			this.txtCargo_Garante.Text = string.Empty;
			this.txtAntiguedad_Garante.Text = string.Empty;
			this.txtTel_LabGarante.Text = string.Empty;
			this.txtTel_Labo2Garante.Text = string.Empty;
			this.txtExt1_Garante.Text = string.Empty;
			this.txtExt2_Garante.Text = string.Empty;
			this.txtDireccionLab_Garante.Text = string.Empty;
			this.txtCorreoLab_Garante.Text = string.Empty;
			this.txtConyuge_Garante.Text = string.Empty;
			this.txtDirLabCony_Garante.Text = string.Empty;
			this.txtCargoConyu_Garante.Text = string.Empty;
			//Referencias
			this.txtRef1Nom_Garante.Text = string.Empty;
			this.txtRef1Res_Garante.Text = string.Empty;
			this.txtRef1Tel_Garante.Text = string.Empty;
			this.txtRef1PtoRef_Garante.Text = string.Empty;
			this.txtRef1Cel_Garante.Text = string.Empty;
			this.txtRef2Nom_Garante.Text = string.Empty;
			this.txtRef2Res_Garante.Text = string.Empty;
			this.txtRef2Tel_Garante.Text = string.Empty;
			this.txtRef2PtoRef_Garante.Text = string.Empty;
			this.txtRef2Cel_Garante.Text = string.Empty;
			this.txtRef3Nom_Garante.Text = string.Empty;
			this.txtRef3Res_Garante.Text = string.Empty;
			this.txtRef3Tel_Garante.Text = string.Empty;

			#endregion
		}

		private void btnExcepcion_Click(object sender, EventArgs e)
		{
			if (this.CodigoExcepcion != 0)
			{
				e_excepcion_mov movimientos = new e_excepcion_mov(this.CodigoExcepcion, this.da);
				movimientos.ShowDialog();
			}
		}
	}
}


