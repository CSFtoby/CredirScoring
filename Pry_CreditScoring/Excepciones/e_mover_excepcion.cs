using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wfcModel;

namespace Docsis_Application.Excepciones
{
	public partial class e_mover_excepcion : Form
	{
		private DataAccess da;
		private int CodigoExcepcion;
		private int EstacionActual;
		private int PasoActual;
		private int CodigoAgenciaExce = 0;
		private string gerenteFilial = string.Empty;
		private List<Flujos> posibles_movimientos = new List<Flujos>();
		private const int workflowId = 1;
		private DataTable dtExcepcion = new DataTable("dtExcepciones");
		private static int CodigoEstacion_C = 0;
		private static string AnalistaAsignado1 = string.Empty;
		private static string AnalistaAsignado2 = string.Empty;
		private static DataTable dtAnalistas = new DataTable();
		public string miembros_seleccionados = string.Empty;
		private string es_nivel_local;
		private string es_nivel_resolutivo;
		private string es_nivel_con_gtefilial;
		private int totalResolucionesRequeridas = 0;
		private static bool PuedeEnviarGerente = true; //Si cumple con las condiciones, permite enviar a Niverl Resolutivo Filial

		#region
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

		private const int CS_DROPSHADOW = 0x00020000;
		protected override CreateParams CreateParams
		{
			get
			{
				// add the drop shadow flag for automatically drawing
				// a drop shadow around the form
				CreateParams cp = base.CreateParams;
				cp.ClassStyle |= CS_DROPSHADOW;
				return cp;
			}
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
		#endregion

		public e_mover_excepcion(DataAccess da, int codExcepcion, int estacion_actual, int paso_Actual)
		{
			InitializeComponent();
			this.da = da;
			this.CodigoExcepcion = codExcepcion;
			this.EstacionActual = estacion_actual;
			this.PasoActual = paso_Actual;

			//------------------
			this.labelOrigen.Text = DocSys.p_get_nombre_estacion(this.EstacionActual);
		}

		private void e_mover_excepcion_Load(object sender, EventArgs e)
		{
			labelOficialDeServicio.Text = MDI_Menu.nombre_y_usuario_oficial_servicio;
			cargar_imagen_estacion();
			this.dtExcepcion = this.da.get_info_excep_gral(this.CodigoExcepcion);
			string vl_nombre_agencia;
			DocSys.p_obtener_filial_usuario(DocSys.vl_user, out this.CodigoAgenciaExce, out vl_nombre_agencia);
			//this.groupBoxGtesFilial.Visible = false;
			this.p_cargar_decisiones();
			//this.EvaluarAntesEnviar(this.CodigoExcepcion);
		}

		private void p_cargar_decisiones()
		{
			try
			{
				DataTable dt = this.da.get_Decisiones_por_paso(this.PasoActual, this.EstacionActual);

				foreach (DataRow item in dt.Rows)
				{
					Flujos pm = new Flujos();
					pm.Decision_Id = Convert.ToInt32(item["decision_id"].ToString());
					pm.Descripcion = item["descripcion"].ToString();
					pm.Estacion_id_to = Convert.ToInt32(item["estacion_id_to"].ToString());
					pm.Paso_to = Convert.ToInt32(item["paso_to"].ToString());
					pm.Estado_Excp_Id = Convert.ToInt32(item["estado_excep_id"].ToString());
					pm.Flujo_Id = Convert.ToInt32(item["flujo_id"].ToString());
					this.posibles_movimientos.Add(pm);
				}

				this.cmbDecision.ValueMember = "flujo_id";
				this.cmbDecision.DisplayMember = "descripcion";
				this.cmbDecision.DataSource = dt;
				this.cmbDecision_SelectionChangeCommitted(null, null);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void button9_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void cmbDecision_SelectionChangeCommitted(object sender, EventArgs e)
		{
			int flujo_id = Convert.ToInt32(this.cmbDecision.SelectedValue.ToString());
			var obj = this.posibles_movimientos.FirstOrDefault(c => c.Flujo_Id == flujo_id);

			/*if (!this.EvaluarAntesEnviar(this.CodigoExcepcion))
			{
				if(obj.Estacion_id_to == (int)Estaciones.NivelResolutivoFilial)
				{
					MessageBox.Show("Esta excepción no corresponde a Nivel Resolutivo Filial");
					this.btnMoverSolicitud.Enabled = false;
					return;
				}
				else
				{
					this.btnMoverSolicitud.Enabled = true;
				}
			}
			else
			{
				this.btnMoverSolicitud.Enabled = true;
			}*/

			labelDecisionID.Text = this.cmbDecision.SelectedValue.ToString();
			if (labelDecisionID.Text != string.Empty & labelPaso.Text != string.Empty)
			{
				if (!string.IsNullOrEmpty(miembros_seleccionados))
				{
					if (MessageBox.Show("Ya realizo la seleccion de almenos un miembro o Gerente de Comite, si continua hay que volver a seleccionar!, ¿Desea continuar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
					{
						return;
					}

					this.miembros_seleccionados = string.Empty;
					pbFotoMiembro1.Image = null;
					pbFotoMiembro2.Image = null;
					pbFotoMiembro3.Image = null;
					pbFotoMiembro4.Image = null;
					pbFotoMiembro5.Image = null;

					var i = this.miembros_seleccionados.Split('|').Count() - 1;
					//lblMiembrossele.Text = "Gerentes de Comité Seleccionados (" + i.ToString() + ")";
				}

				//Obtiene la información de la decisión
				var dt = this.da.get_info_Decision(obj.Estacion_id_to, flujo_id);

				if (!DBNull.Value.Equals(dt.Rows[0]["icono"]))
				{
					byte[] bits = (byte[])dt.Rows[0]["icono"];
					this.pbEstacion_Destino.Image = new Bitmap(DocSys.p_CopyDataToBitmap(bits));
				}
				else
				{
					this.pbEstacion_Destino.Image = null;
				}

				this.labelDestino.Text = dt.Rows[0]["nombre"].ToString();

				this.es_nivel_resolutivo = dt.Rows[0]["resol_excep"].ToString();
				this.es_nivel_local = dt.Rows[0]["nivel_resolutivo_local"].ToString();
				this.es_nivel_con_gtefilial = dt.Rows[0]["nivel_resol_con_gtefilial"].ToString();
				this.totalResolucionesRequeridas = Convert.ToInt32(dt.Rows[0]["resoluciones_requeridas"].ToString());
				this.txtResol_requeridas.Text = this.totalResolucionesRequeridas.ToString();

				if (dt.Rows[0]["resol_excep"].ToString().Equals("S"))
				{
					this.txtMonto_minimo.Text = dt.Rows[0]["MONTO_MIN_RES_EXCEP"].ToString();
					this.txtMonto_maximo.Text = dt.Rows[0]["MONTO_MAX_RES_EXCEP"].ToString();
					this.gbCriterios.Visible = true;
					this.pnlGtesSeleccionados.Visible = false;
				}
				else
				{
					this.gbCriterios.Visible = false;
					this.pnlGtesSeleccionados.Visible = false;
				}
			}
		}

		private void cargar_imagen_estacion()
		{
			object icono = this.da.get_icono_estacion(this.EstacionActual);

			if (!DBNull.Value.Equals(icono))
			{
				byte[] bits = ((byte[])icono);
				pbEstacion_actual.Image = new Bitmap(DocSys.p_CopyDataToBitmap(bits));
			}
			else
				pbEstacion_actual.Image = null;
		}

		private bool EvaluarAntesEnviar(int codigoExcepcion)
		{
			DataTable dt = this.da.Condiciones(codigoExcepcion);
			if(dt.Rows.Count > 0)
			{
				string condicionTU = dt.Rows[0]["condicion_tu"].ToString();
				string PagoMediante = dt.Rows[0]["pago_mediante"].ToString();
				decimal Monto = Convert.ToDecimal(dt.Rows[0]["MONTO_SOLICITADO"].ToString());

				if(condicionTU.Equals("APROBADO") & PagoMediante.Equals("P") & Monto <= 300000)
				{
					return true;
				}

				if (condicionTU.Equals("REFERIDO") & PagoMediante.Equals("P") & Monto <= 200000)
				{
					return true;
				}

				if (condicionTU.Equals("CONDICIONADO") & PagoMediante.Equals("P") & Monto <= 200000)
				{
					return true;
				}

				if (condicionTU.Equals("RECHAZADO") & PagoMediante.Equals("P") & Monto <= 200000)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		private void btnMoverSolicitud_Click(object sender, EventArgs e)
		{
			int flujo_id = Convert.ToInt32(this.cmbDecision.SelectedValue.ToString());
			var obj = this.posibles_movimientos.FirstOrDefault(c => c.Flujo_Id == flujo_id);
			bool cerrar_ventana = false;

			//Si no ha cumplido con la cantidad de miembros lo regresa
			var contestadas = miembros_seleccionados.Split('|');
			var cant_resoluciones_selec = contestadas.Count() - 1;

			if(cant_resoluciones_selec != this.totalResolucionesRequeridas && es_nivel_resolutivo == "S" && string.IsNullOrEmpty(this.miembros_seleccionados))
			{
				MessageBox.Show($"Se debe indicar {this.totalResolucionesRequeridas} figura(s) resolutiva(s) para este Comite..!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				linkLabel1_LinkClicked(null, null);
			}

			string[] resultado = miembros_seleccionados.Trim().Split('|');
			if (es_nivel_resolutivo == "S")
			{
				for (int i = 0; i < resultado.Length; i++)
				{
					switch (i)
					{
						case 0:
							if(!string.IsNullOrEmpty(resultado[i]))
								this.gerenteFilial = resultado[i];
							break;
						case 1:
							if (!string.IsNullOrEmpty(resultado[i]))
								AnalistaAsignado1 = resultado[i];
							break;
						case 2:
							if (!string.IsNullOrEmpty(resultado[i]))
								AnalistaAsignado2 = resultado[i];
							break;
						default:
							break;
					}
				}
			}

			DialogResult result2 = MessageBox.Show("¿Está seguro que desea enviar la excepción?", "Info", MessageBoxButtons.YesNo);

			if (result2 == DialogResult.Yes)
			{
				if (obj != null)
				{
					string aprobar = string.Empty;
					string denegar = string.Empty;
					string modificar = string.Empty;
					string anular = string.Empty;

					DataTable dt = DocSys.acciones_decision(obj.Decision_Id);
					aprobar = dt.Rows[0]["APROBAR_EXCEP"].ToString();
					denegar = dt.Rows[0]["DENEGAR_EXCEP"].ToString();
					modificar = dt.Rows[0]["MODIF_EXCEP"].ToString();
					anular = dt.Rows[0]["ANULAR"].ToString();

					if (aprobar.Equals(u_Globales.Afirmativo)
						| denegar.Equals(u_Globales.Afirmativo))
					{
						//Valida si ya fue dada la resolución para mostrar o no el comentario
						if (!this.excepcion_resolucion())
						{
                            bool tiene_coment = DocSys.tiene_comentario(this.CodigoExcepcion);

                            if (tiene_coment == false) {
                                ComentarioResolucion comen = new ComentarioResolucion();
                                DialogResult result = comen.ShowDialog();
                                if (result == DialogResult.Yes)
                                {
                                    string comentario = comen.Comentario;

                                    if (string.IsNullOrEmpty(comentario))
                                    {
                                        MessageBox.Show("Debe ingresar el motivo de resolución", "Error");
                                        return;
                                    }

                                    string sql = "excp.dcs_p_recomendacion_resol";

                                    if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                                    {
                                        DocSys.connOracle.Open();
                                    }

                                    OracleParameter pa_comentario = new OracleParameter("pa_comentario", OracleType.VarChar);
                                    pa_comentario.Direction = ParameterDirection.Input;
                                    pa_comentario.Value = comentario;

                                    OracleParameter pa_codigo_excep = new OracleParameter("pa_codigo_excepcion", OracleType.Number);
                                    pa_codigo_excep.Direction = ParameterDirection.Input;
                                    pa_codigo_excep.Value = this.CodigoExcepcion;

                                    OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.Add(pa_comentario);
                                    cmd.Parameters.Add(pa_codigo_excep);

                                    try
                                    {
                                        cmd.ExecuteNonQuery();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }

                                }
                                else
                                {
                                    cerrar_ventana = true;
                                }
                            }
                            else
                            {
                                cerrar_ventana = true;
                            }
                        }
					}

					//PARTE PARA MODIFICAR
					else if (modificar.Equals(u_Globales.Afirmativo))
					{
						e_add_notas nota_anulacion = new e_add_notas(this.CodigoExcepcion, this.EstacionActual, u_Globales.accionAgregar, 0);
						string respuesta = string.Empty;
						nota_anulacion.ShowDialog();
						respuesta = nota_anulacion.respuesta;
						while (this.EvaluarTexto(respuesta))
						{							
							if (nota_anulacion.cerrar)
							{
								DialogResult result = MessageBox.Show("¿Desea cancelar la operación?", "Cancelar Operación", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
								if(result == DialogResult.Yes)
								{
									cerrar_ventana = true;
									return;
								}
								else
								{
									nota_anulacion.ShowDialog();
								}
							}
							else
							{
								nota_anulacion.ShowDialog();
							}

							respuesta = nota_anulacion.respuesta;
						}
					}

					//TEMPORAL
					//Evalua que si va a validacion lleve llena la recomedacion del gerente
					if(obj.Estacion_id_to == (int)Estaciones.EstacionValidacion & this.EstacionActual == (int)Estaciones.NivelResolutivoFilial)
					{
						var grales = this.da.get_info_excep_gral(this.CodigoExcepcion);
						var rec = grales.Rows[0]["RECOMENDACION_NIV_RES"].ToString();

						if (string.IsNullOrEmpty(rec.Trim()))
						{
							MessageBox.Show("No ha llenado la recomendación del gerente de filial");
							cerrar_ventana = true;
						}
					}

					if (!cerrar_ventana)
					{
						this.p_mover_solicitud(workflowId, this.CodigoExcepcion,
										   obj.Paso_to, obj.Decision_Id, this.gerenteFilial,
										   this.EstacionActual, obj.Estacion_id_to, flujo_id);
					}
					else
					{
						this.Close();
					}
				}
			}
			else
			{
				this.Close();
			}
		}

		private bool EvaluarTexto(string texto)
		{
			return (texto.Equals(string.Empty));
		}

		/// <summary>
		/// Mueve la solicitud a su nuevo destino
		/// </summary>
		/// <param name="_workflow_id">Workflow al que pertenece</param>
		/// <param name="_codigo_excepcion">Código de la excepción a mover</param>
		/// <param name="_paso_to">Paso siguiente</param>
		/// <param name="_decision_id">Decisión tomada</param>
		/// <param name="_usuario_gerente">Usuario gerente al que se dirige</param>
		/// <param name="_estacion_id_from">Estación origen</param>
		/// <param name="_estacion_id_to">Estacion destino</param>
		/// <param name="_flujo_id">Flujo de trabajo</param>
		private void p_mover_solicitud(int _workflow_id, int _codigo_excepcion,
									   int _paso_to, int _decision_id, string _usuario_gerente,
									   int _estacion_id_from, int _estacion_id_to, int _flujo_id)
		{
			try
			{
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				string vl_sql = "excp.dcs_p_enviar_excepcion";
				OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
				cmd.CommandType = CommandType.StoredProcedure;

				//───────────────────pa_workflow_id
				OracleParameter pa_workflow_id = new OracleParameter("pa_workflow_id", OracleType.Number);
				pa_workflow_id.Direction = ParameterDirection.Input;
				pa_workflow_id.Value = _workflow_id;

				//───────────────────pa_codigo_excepcion
				OracleParameter pa_codigo_excepcion = new OracleParameter("pa_codigo_excepcion", OracleType.Number);
				pa_codigo_excepcion.Direction = ParameterDirection.Input;
				pa_codigo_excepcion.Value = _codigo_excepcion;

				//───────────────────pa_paso_to
				OracleParameter pa_paso_to = new OracleParameter("pa_paso_to", OracleType.Number);
				pa_paso_to.Direction = ParameterDirection.Input;
				pa_paso_to.Value = _paso_to;

				//───────────────────pa_decision_id
				OracleParameter pa_decision_id = new OracleParameter("pa_decision_id", OracleType.Number);
				pa_decision_id.Direction = ParameterDirection.Input;
				pa_decision_id.Value = _decision_id;

				//───────────────────pa_usuario_gerente
				OracleParameter pa_usuario_gerente = new OracleParameter("pa_usuario_gerente", OracleType.VarChar, 50);
				pa_usuario_gerente.Direction = ParameterDirection.Input;
				pa_usuario_gerente.Value = _usuario_gerente;

				//───────────────────pa_estacion_id_from
				OracleParameter pa_estacion_id_from = new OracleParameter("pa_estacion_id_from", OracleType.Number);
				pa_estacion_id_from.Direction = ParameterDirection.Input;
				pa_estacion_id_from.Value = _estacion_id_from;

				//───────────────────pa_estacion_id_to
				OracleParameter pa_estacion_id_to = new OracleParameter("pa_estacion_id_to", OracleType.Number);
				pa_estacion_id_to.Direction = ParameterDirection.Input;
				pa_estacion_id_to.Value = _estacion_id_to;

				//───────────────────pa_flujo_id
				OracleParameter pa_flujo_id = new OracleParameter("pa_flujo_id", OracleType.Number);
				pa_flujo_id.Direction = ParameterDirection.Input;
				pa_flujo_id.Value = _flujo_id;

				//───────────────────pa_analista_creditos
				OracleParameter pa_analista_creditos = new OracleParameter("pa_analista_creditos", OracleType.VarChar);
				pa_analista_creditos.Direction = ParameterDirection.Input;
				pa_analista_creditos.Value = AnalistaAsignado1;

				//───────────────────pa_jefatura
				OracleParameter pa_jefatura = new OracleParameter("pa_jefatura", OracleType.VarChar);
				pa_jefatura.Direction = ParameterDirection.Input;
				pa_jefatura.Value = AnalistaAsignado2;

				cmd.Parameters.Add(pa_workflow_id);
				cmd.Parameters.Add(pa_codigo_excepcion);
				cmd.Parameters.Add(pa_paso_to);
				cmd.Parameters.Add(pa_decision_id);
				cmd.Parameters.Add(pa_usuario_gerente);
				cmd.Parameters.Add(pa_estacion_id_from);
				cmd.Parameters.Add(pa_estacion_id_to);
				cmd.Parameters.Add(pa_flujo_id);
				cmd.Parameters.Add(pa_analista_creditos);
				cmd.Parameters.Add(pa_jefatura);

				cmd.ExecuteReader();

				MessageBox.Show("Solicitud enviada satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.Message);
			}
		}

		private void cmbGerentesFilial_SelectionChangeCommitted(object sender, EventArgs e)
		{
			gerenteFilial = cmbGerentesFilial.SelectedValue.ToString();
			//groupBoxGtesFilial.Text = "Gerente de Filial " + gerenteFilial;
		}

		private void LlenarComboGtes()
		{
			if (this.CodigoAgenciaExce != 0)
			{
				DataTable dtGerentes = da.ObtenerGerentesdeFilialxFilial(DocSys.vl_agencia_usuario);
				this.groupBoxGtesFilial.Visible = true;
				cmbGerentesFilial.DataSource = dtGerentes;
				cmbGerentesFilial.DisplayMember = "nombre_gerente";
				cmbGerentesFilial.ValueMember = "codigo_usuario";

				if (dtGerentes.Rows.Count > 0)
				{
					cmbGerentesFilial_SelectionChangeCommitted(null, null);
				}
				else
				{
					MessageBox.Show("No hay un gerente definido en su Filial ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
		}

		private void cmbDecision_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private bool excepcion_resolucion()
		{
			try
			{

				string sql = "select CODIGO_ESTADO_EXCEP from EXCP.DCS_EXCEPCION_SOLICITUD where CODIGO_EXCEPCION = :codigo_Excepc ";
				bool permitidas = false;
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				OracleParameter pa_codigo_excepcin = new OracleParameter("codigo_Excepc", OracleType.Number);
				pa_codigo_excepcin.Direction = ParameterDirection.Input;
				pa_codigo_excepcin.Value = this.CodigoExcepcion;

				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.Add(pa_codigo_excepcin);

				OracleDataReader dr = cmd.ExecuteReader();
				dr.Read();

				int estado = 0;

				if (dr.HasRows)
				{
					estado = Convert.ToInt32(dr["CODIGO_ESTADO_EXCEP"].ToString());
				}

				dr.Close();

				//ya fue dada su resolución
				if (estado != 1)
				{
					return true;
				}

				return permitidas;
			}
			catch (Exception ex)
			{
				throw new Exception($"{ex.TargetSite} : {ex.Message}", ex.InnerException);
			}
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			int flujo_id = Convert.ToInt32(this.cmbDecision.SelectedValue.ToString());
			var obj = this.posibles_movimientos.FirstOrDefault(c => c.Flujo_Id == flujo_id);
			int gestacion_id_to = obj.Estacion_id_to;
			if (gestacion_id_to == (int)Estaciones.Afiliacion)
			{
				if (MessageBox.Show("La visualización de los miembros de la Estación de Afiliación puede tardar varios minutos, desea continuar..?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				{
					return;
				}
			}

			s_cnf_estaciones_miembros forma = new s_cnf_estaciones_miembros(da, gestacion_id_to);
			forma.es_comite_local = es_nivel_local;
			forma.es_comite_resolutivo = es_nivel_resolutivo;
			forma.agencia_local = this.CodigoAgenciaExce;
			DialogResult result = forma.ShowDialog();
			if (result == DialogResult.OK)
			{
				pnlGtesSeleccionados.Visible = true;
				for (int x = 0; x < forma.imagenes_usr_sele.Count(); x++)
				{
					if (x == 0)
						pbFotoMiembro1.Image = forma.imagenes_usr_sele[x];
					if (x == 1)
						pbFotoMiembro2.Image = forma.imagenes_usr_sele[x];
					if (x == 2)
						pbFotoMiembro3.Image = forma.imagenes_usr_sele[x];
					if (x == 3)
						pbFotoMiembro4.Image = forma.imagenes_usr_sele[x];
					if (x == 4)
						pbFotoMiembro5.Image = forma.imagenes_usr_sele[x];
				}

				this.miembros_seleccionados = forma.usuarios_seleccionados;
				var i = this.miembros_seleccionados.Split('|').Count() - 1;
				linkLabel1.Text = "Gerentes de Comite Seleccionados (" + i.ToString() + ")";
			}

		}

		private void labelDestino_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			int flujo_id = Convert.ToInt32(this.cmbDecision.SelectedValue.ToString());
			var obj = this.posibles_movimientos.FirstOrDefault(c => c.Flujo_Id == flujo_id);
			int gestacion_id_to = obj.Estacion_id_to;
			if (gestacion_id_to == (int)Estaciones.Afiliacion)
			{
				if (MessageBox.Show("La visualización de los miembros de la Estación de Afiliación puede tardar varios minutos, desea continuar..?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				{
					return;
				}
			}

			var excepcion = this.da.get_info_excep_gral(this.CodigoExcepcion);
			this.CodigoAgenciaExce = Convert.ToInt32(excepcion.Rows[0]["codigo_agencia_origen"].ToString());

			s_cnf_estaciones_miembros forma = new s_cnf_estaciones_miembros(da, gestacion_id_to);
			forma.es_comite_local = es_nivel_local;
			forma.es_comite_resolutivo = es_nivel_resolutivo;
			forma.agencia_local = this.CodigoAgenciaExce;
			DialogResult result = forma.ShowDialog();
			if (result == DialogResult.OK)
			{
				pnlGtesSeleccionados.Visible = true;
				for (int x = 0; x < forma.imagenes_usr_sele.Count(); x++)
				{
					if (x == 0)
						pbFotoMiembro1.Image = forma.imagenes_usr_sele[x];
					if (x == 1)
						pbFotoMiembro2.Image = forma.imagenes_usr_sele[x];
					if (x == 2)
						pbFotoMiembro3.Image = forma.imagenes_usr_sele[x];
					if (x == 3)
						pbFotoMiembro4.Image = forma.imagenes_usr_sele[x];
					if (x == 4)
						pbFotoMiembro5.Image = forma.imagenes_usr_sele[x];
				}

				this.miembros_seleccionados = forma.usuarios_seleccionados;
				var i = this.miembros_seleccionados.Split('|').Count() - 1;
				linkLabel1.Text = "Gerentes de Comite Seleccionados (" + i.ToString() + ")";
			}
		}
	}

	public class Flujos
	{
		public int Decision_Id { get; set; }
		public string Descripcion { get; set; }
		public int Estacion_id_to { get; set; }
		public int Paso_to { get; set; }
		public int Estado_Excp_Id { get; set; }
		public int Flujo_Id { get; set; }
	}

	public enum EstacionesCreditos
	{
		CreditosTGU = 1002,
		CreditosSPS = 2001,
		CreditosLCBA = 3001
	}
}
