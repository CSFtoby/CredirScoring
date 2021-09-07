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
	public partial class e_admnin_excepciones : Form
	{
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
		private void moverForm()
		{
			ReleaseCapture();
			SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
		}
		#endregion
		#region
		private const int CS_DROPSHADOW = 0x00020000;
		protected override CreateParams CreateParams
		{
			get
			{
				// add the drop shadow flag for automatically drawing
				// a drop shadow around the form
				CreateParams cp = base.CreateParams;


				if (s_login.ventana_con_borde)
				{
					cp.Style |= 0x40000 | CS_DROPSHADOW;
				}
				else
				{
					cp.ClassStyle |= CS_DROPSHADOW;
				}
				cp.ClassStyle |= CS_DROPSHADOW;
				return cp;
			}
		}
		#endregion

		public DataAccess da;
		private int estacion_actual = 0;
		DataTable dtAnalistas = new DataTable();
		private List<Aprobaciones> usuariosNuevos = new List<Aprobaciones>();
		private List<Aprobaciones> UsuariosAnteriores = new List<Aprobaciones>();
		private bool EsGerente;
		private int CodigoAgencia;

		public e_admnin_excepciones(DataAccess da, bool esGerente = false, int CodigoAgencia = 0)
		{
			InitializeComponent();
			this.da = da;
			this.EsGerente = esGerente;
			this.CodigoAgencia = CodigoAgencia;

			if (esGerente)
			{
				this.btnCambiarMiembro.Enabled = esGerente;
				this.btnEditarResolución.Enabled = false;
				this.pictureBox4.Enabled = esGerente;
				this.pbMiembros.Enabled = esGerente;
				this.button1.Enabled = esGerente;
			}
		}

		private void panel3_MouseDown(object sender, MouseEventArgs e)
		{
			this.moverForm();
		}

		private void pictureBox3_Click(object sender, EventArgs e)
		{
			s_buscarusuarios forma = new s_buscarusuarios();
			s_buscarusuarios.esGerenteFilial = this.EsGerente;
			s_buscarusuarios.CodigoAgencia = this.CodigoAgencia;

			forma.da = this.da;
			DialogResult resultado = forma.ShowDialog();
			if (resultado == DialogResult.OK)
			{
				txtOficial_servicio.Text = forma.vl_usuario.ToUpper();
				labelNombre_oficial_servicio.Text = da.ObtenerNombreUsuario(txtOficial_servicio.Text);
			}
		}

		private void pictureBox4_Click(object sender, EventArgs e)
		{
			if (!this.EsGerente)
			{
				s_buscarfiliales forma = new s_buscarfiliales();
				forma.da = this.da;
				DialogResult resultado = forma.ShowDialog();
				if (resultado == DialogResult.OK)
				{
					txtCodigo_agencia.Text = forma.vl_codigo_agencia;
					labelNombre_agencia.Text = da.ObtenerNombreAgencia(int.Parse(txtCodigo_agencia.Text));
				}
			}
			else
			{
				MessageBox.Show("No cuenta con permiso para cambio de filial. Comuniquese con el administrador para esta acción.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void btnCambiarOficAgencia_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtExcepcion.Text))
			{
				return;
			}
			if (string.IsNullOrEmpty(txtOficial_servicio.Text) || labelNombre_oficial_servicio.Text == "")
			{
				MessageBox.Show("Debe indicar el oficial de servicio de la solicitud...", "Aviso de Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (string.IsNullOrEmpty(txtCodigo_agencia.Text) || labelNombre_agencia.Text == "")
			{
				MessageBox.Show("Debe indicar la Filial de la solicitud...", "Aviso de Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			//--
			if (MessageBox.Show("Desea realizar cambios de Oficial de Servicio o Filial ?", "Aviso de Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
			{
				return;
			}
			if (da.ReasignarOficialFilialExcepcion(Int32.Parse(txtExcepcion.Text), int.Parse(txtCodigo_agencia.Text), txtOficial_servicio.Text))
			{
				MessageBox.Show("Cambios realizados satisfactoriamente...", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show("No se ha podido realizar los cambios solicitados", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void txtNo_solicitud_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				e.SuppressKeyPress = true;
				SelectNextControl(ActiveControl, true, true, true, true);
			}
		}

		private void txtNo_solicitud_KeyPress(object sender, KeyPressEventArgs e)
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

		private void txtNo_solicitud_Leave(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtExcepcion.Text))
			{
				return;
			}
			cargar_generales();
		}

		private void e_admnin_excepciones_Load(object sender, EventArgs e)
		{

		}

		private void cargar_generales()
		{
			try
			{
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				string sql = @"select e.no_solicitud, e.fecha_cierre, e.fecha_presentacion, e.codigo_agencia_origen, e.oficial_servicios, e.condicion_tu, e.abierta, 
								case when e.pago_mediante = 'V' then 'Pago por Ventanilla' else 'Pago por Planilla' end pago_mediante, 
								e.ANTIGUEDAD_MESES, e.ANTIGUEDAD_DIAS, e.ANTIGUEDAD_HORAS, e.ANTIGUEDAD_MINUTOS, e.ANTIGUEDAD_SEGUNDOS,   
								ee.descripcion estado, 
								u.nombres || ' ' || u.PRIMER_APELLIDO || ' ' || u.SEGUNDO_APELLIDO osNombre, 
								ag.nombre_agencia, 
								s.codigo_cliente, s.monto_solicitado, 
								c.NOMBRES || ' ' || c.PRIMER_APELLIDO || ' ' || c.segundo_apellido nombre_cliente, 
								sa.desc_sub_aplicacion, 
								(select max(no_movimiento) from excp.dcs_movimientos_excep m where m.codigo_excepcion = e.codigo_excepcion) mov_actual, 
								(select es.nombre from excp.dcs_movimientos_excep m, wfc.dcs_wf_estaciones es 
								 where m.codigo_excepcion = e.codigo_excepcion 
								and no_movimiento = (select max(no_movimiento) 
								from excp.dcs_movimientos_excep m 
								where m.codigo_excepcion = e.codigo_excepcion) and es.estacion_id = m.estacion_id_to 
								) estacion_id_actual, e.ESTACION_ID 
								from excp.dcs_excepcion_solicitud e, excp.dcs_exc_estados_excepcion ee, 
								mgi.mgi_usuarios u, mgi.mgi_agencias ag, wfc.dcs_solicitudes s, mgi.mgi_clientes c, mgi_sub_aplicaciones sa 
								where e.codigo_estado_excep = ee.ESTADO_EXCEP_ID 
								and u.codigo_usuario = e.oficial_servicios 
								and ag.codigo_agencia = e.codigo_agencia_origen 
								and s.no_solicitud = e.no_solicitud 
								and c.codigo_cliente = s.codigo_cliente 
								and s.codigo_sub_aplicacion = sa.codigo_sub_aplicacion 
								and e.codigo_excepcion = :codExcepcion ";

				OracleParameter pa_codigo_excepcion = new OracleParameter("codExcepcion", OracleType.Number);
				pa_codigo_excepcion.Direction = ParameterDirection.Input;
				int codigoExcepcion = Convert.ToInt32(this.txtExcepcion.Text);
				pa_codigo_excepcion.Value = codigoExcepcion;

				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.Add(pa_codigo_excepcion);

				DataTable dt = new DataTable();
				OracleDataAdapter da = new OracleDataAdapter(cmd);
				da.Fill(dt);

				if (dt.Rows.Count > 0)
				{
					this.txtSolicitud.Text = dt.Rows[0]["no_solicitud"].ToString();
					this.textBox_estado_solicitud.Text = dt.Rows[0]["estado"].ToString();
					this.txtNombre_solicitante.Text = dt.Rows[0]["nombre_cliente"].ToString();
					this.txtCodigo_agencia.Text = dt.Rows[0]["codigo_agencia_origen"].ToString();
					this.txtOficial_servicio.Text = dt.Rows[0]["oficial_servicios"].ToString();
					this.txtProducto.Text = dt.Rows[0]["desc_sub_aplicacion"].ToString();
					this.txtCondicionTu.Text = dt.Rows[0]["condicion_tu"].ToString();

					string vl_abierta = dt.Rows[0]["abierta"].ToString();

					this.labelMovimiento_actual.Text = dt.Rows[0]["mov_actual"].ToString();

					estacion_actual = Convert.ToInt32(dt.Rows[0]["ESTACION_ID"].ToString());
					this.cargar_niveles(estacion_actual, Convert.ToInt32(this.labelMovimiento_actual.Text));
					cargar_detalle_excepcion();
					this.CargarResoluciones(Convert.ToInt32(this.txtExcepcion.Text));
				}
				else
				{
					MessageBox.Show("No de excepción no existe..!", "Aviso de Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error");
			}
		}

		private void cargar_detalle_excepcion()
		{
			try
			{
				int codigoExcepcion = Convert.ToInt32(this.txtExcepcion.Text);
				DataTable detalle = this.da.get_detalle_movimiento_excep(codigoExcepcion);
				this.dgvDetalle.DataSource = null;
				this.dgvDetalle.DataSource = detalle;
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ha ocurrido un error {ex.Message}", "Error");
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void cargar_niveles(int estacion_id, int ultimo_mov)
		{
			try
			{
				string sql = "select estacion_id, nombre, comite_res_excep from excp.DCS_V_ESTACIONES "
							+ "where estacion_id = :estacionId ";
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				OracleParameter pa_estacion_id = new OracleParameter("estacionId", OracleType.Number);
				pa_estacion_id.Direction = ParameterDirection.Input;
				pa_estacion_id.Value = estacion_id;

				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.Add(pa_estacion_id);

				DataTable dt = new DataTable();
				OracleDataAdapter da = new OracleDataAdapter(cmd);
				da.Fill(dt);

				if (dt.Rows.Count > 0)
				{
					bool es_nivel_r = (dt.Rows[0]["comite_res_excep"].ToString().Equals(u_Globales.Afirmativo));

					if (es_nivel_r)
					{
						this.txtNivelRes.Text = dt.Rows[0]["nombre"].ToString();
						sql = "select usuario_comite, PENDIENTE_RESPUESTA_B, NO_REGISTRO_AP "
							+ "from excp.DCS_EXCEPCIONES_APROBACIONES where NO_MOVIMIENTO_ACTUAL = :mov_act and vigente = 'S' ";

						OracleParameter pa_mov_act = new OracleParameter("mov_act", OracleType.Number);
						pa_mov_act.Direction = ParameterDirection.Input;
						pa_mov_act.Value = ultimo_mov;

						cmd = new OracleCommand(sql, DocSys.connOracle);
						cmd.CommandType = CommandType.Text;
						cmd.Parameters.Add(pa_mov_act);

						dt = new DataTable();
						da = new OracleDataAdapter(cmd);
						da.Fill(dt);

						usuariosNuevos = new List<Aprobaciones>();
						if (dt.Rows.Count > 0)
						{
							//this.lbxMiembros.DataSource = dt;
							//this.lbxMiembros.DisplayMember = "usuario_comite";
							//this.lbxMiembros.ValueMember = "usuario_comite";

							this.lbxMiembros.Items.Clear();
							this.usuariosNuevos.Clear();
							this.UsuariosAnteriores.Clear();
							foreach (DataRow item in dt.Rows)
							{
								//usuarios aprobaciones nuevos, aquí se guardan los que se cambian
								usuariosNuevos.Add(new Aprobaciones
								{
									UsuarioComite = item["usuario_comite"].ToString(),
									PendienteR = item["PENDIENTE_RESPUESTA_B"].ToString(),
									NoRegistroAp = Convert.ToInt32(item["NO_REGISTRO_AP"].ToString())
								});

								//Usuarios aprobaciones anteriores
								UsuariosAnteriores.Add(new Aprobaciones
								{
									UsuarioAnterior = item["usuario_comite"].ToString(),
									PendienteR = item["PENDIENTE_RESPUESTA_B"].ToString(),
									NoRegistroAp = Convert.ToInt32(item["NO_REGISTRO_AP"].ToString())
								});

								this.lbxMiembros.Items.Add(item["usuario_comite"].ToString());
							}

							dtAnalistas = this.da.get_analistas_estacion(estacion_id);
						}

					}
					else
					{
						this.txtNivelRes.Text = "Pendiente";
						this.lbxMiembros.Enabled = false;
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"{ex.TargetSite} - {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnCambiarMiembro_Click(object sender, EventArgs e)
		{
			if (this.dgvResoluciones.Rows.Count > 0)
			{
				DataGridViewRow row = this.dgvResoluciones.CurrentRow;
				string pendiente = row.Cells["pendiente_respuesta_b"].Value.ToString();

				if (pendiente.Equals("N"))
				{
					MessageBox.Show("No puede modificar el usuario del comité porque ya fue dada la resolución.");
					return;
				}
				else
				{
					DialogResult result = MessageBox.Show($"¿Está seguro que desea cambiar los miembros?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

					if (result == DialogResult.Yes)
					{
						s_buscarusuarios forma = new s_buscarusuarios();
						forma.da = this.da;
						DialogResult resultado = forma.ShowDialog();
						{
							string usuario_ant = row.Cells["usuario_comite"].Value.ToString();
							string usuario_Nuevo = forma.vl_usuario;

							if (usuario_ant.Equals(usuario_Nuevo))
							{
								MessageBox.Show("Debe seleccionar un usuario diferente");
								return;
							}
							else
							{
								if (!string.IsNullOrEmpty(usuario_Nuevo))
									if (this.EditarResoluciones("CAMBIAR", usuario_ant, usuario_Nuevo))
									{
										row.Cells["usuario_comite"].Value = usuario_Nuevo;
									}
							}
						}
					}
				}
			}

		}

		private bool EditarResoluciones(string opcion, string usuario_anterior = "", string usuario_actual = "", string observaciones = "", string reiniciar = "N")
		{
			try
			{
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				string sql = "excp.dcs_p_util_Rea_minires";

				//El nuevo
				OracleParameter pa_usuario_comite = new OracleParameter("pa_usuario_nuevo", OracleType.VarChar);
				pa_usuario_comite.Direction = ParameterDirection.Input;
				pa_usuario_comite.Value = usuario_actual;

				//Movimiento actual
				OracleParameter pa_mov_actual = new OracleParameter("pa_mov_actual", OracleType.Number);
				pa_mov_actual.Direction = ParameterDirection.Input;
				pa_mov_actual.Value = Convert.ToInt32(this.labelMovimiento_actual.Text);

				//Código de la excepcion
				OracleParameter pa_codigo_excepcion = new OracleParameter("pa_codigo_excepcion", OracleType.Number);
				pa_codigo_excepcion.Direction = ParameterDirection.Input;
				pa_codigo_excepcion.Value = Convert.ToInt32(this.txtExcepcion.Text);

				//Usuario anterior
				OracleParameter pa_usuario_ant = new OracleParameter("pa_usuario_ant", OracleType.VarChar);
				pa_usuario_ant.Direction = ParameterDirection.Input;
				pa_usuario_ant.Value = usuario_anterior;

				//Observaciones
				OracleParameter pa_observaciones = new OracleParameter("pa_observaciones", OracleType.VarChar, 1000);
				pa_observaciones.Direction = ParameterDirection.Input;
				pa_observaciones.Value = observaciones;

				//Opcion
				OracleParameter pa_opcion = new OracleParameter("pa_opcion", OracleType.VarChar, 10);
				pa_opcion.Direction = ParameterDirection.Input;
				pa_opcion.Value = opcion;

				//pa_reiniciar
				OracleParameter pa_reiniciar = new OracleParameter("pa_reiniciar", OracleType.VarChar, 1);
				pa_reiniciar.Direction = ParameterDirection.Input;
				pa_reiniciar.Value = reiniciar;

				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add(pa_usuario_comite);
				cmd.Parameters.Add(pa_mov_actual);
				cmd.Parameters.Add(pa_codigo_excepcion);
				cmd.Parameters.Add(pa_usuario_ant);
				cmd.Parameters.Add(pa_observaciones);
				cmd.Parameters.Add(pa_opcion);
				cmd.Parameters.Add(pa_reiniciar);
				cmd.ExecuteNonQuery();

				cmd.Dispose();
				DocSys.connOracle.Close();

				MessageBox.Show($"Operación realizada con éxito", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ha ocurrido un error {ex.Message}", "Error");
				return false;
			}

		}

		private void pbMiembros_Click(object sender, EventArgs e)
		{
			int index = this.lbxMiembros.SelectedIndex;
			if (index == -1)
			{
				MessageBox.Show("Seleccione el miembro a cambiar");
				return;
			}
			string usuario = this.lbxMiembros.Items[index].ToString();
			var usuarioACambiar = UsuariosAnteriores.FirstOrDefault(c => c.UsuarioAnterior.Equals(usuario));

			if (usuarioACambiar != null)
			{
				if (!usuarioACambiar.PendienteR.Equals(u_Globales.Afirmativo))
				{
					MessageBox.Show("El miembro seleccionado ya emitió su resolución, no se puede cambiar", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
			}

			ListadoAnalistas buscar = new ListadoAnalistas(dtAnalistas);
			DialogResult result = buscar.ShowDialog();
			if (result == DialogResult.OK)
			{
				//usuarioACambiar.UsuarioComite = buscar.Usuario;
				var nuevo = usuariosNuevos.FirstOrDefault(c => c.UsuarioComite.Equals(usuarioACambiar.UsuarioAnterior));
				if (nuevo != null)
				{
					string seleccion = buscar.Usuario;
					var existe = UsuariosAnteriores.FirstOrDefault(c => c.UsuarioAnterior.Equals(seleccion));
					if (existe != null)
					{
						MessageBox.Show("El usuario ya está dentro de los miembros resolutivos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					else
					{
						nuevo.UsuarioComite = seleccion;
						this.lbxMiembros.Items[index] = seleccion;
					}
				}
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (this.button1.Enabled == false)
			{
				MessageBox.Show("No tiene autorización para abrir excepciones");
				return;
			}
			else
			{
				try
				{
					if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
					{
						DocSys.connOracle.Open();
					}

					string sql = @"excp.dcs_p_abrir_excepcion";

					OracleParameter pa_codigo_excepcion = new OracleParameter("p_codigo_excepcion", OracleType.Number);
					pa_codigo_excepcion.Direction = ParameterDirection.Input;
					pa_codigo_excepcion.Value = Convert.ToInt32(this.txtExcepcion.Text);

					OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add(pa_codigo_excepcion);
					cmd.ExecuteNonQuery();
					cmd.Dispose();

					MessageBox.Show("Excepción abierta con éxito");
				}
				catch (Exception ex)
				{

					throw;
				}
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			try
			{
				if (DocSys.anular_excepcion_permitido(Convert.ToInt32(this.txtExcepcion.Text)))
				{
					//e_add_notas nota_anulacion = new e_add_notas(Convert.ToInt32(this.txtExcepcion.Text), 1001, u_Globales.accionAgregar, 0, true);
					//nota_anulacion.ShowDialog();
					e_motivo_anulacion anular = new e_motivo_anulacion();
					anular.ShowDialog();

					if (e_motivo_anulacion.Motivo.Trim() == string.Empty)
					{
						MessageBox.Show("Debe ingresar el motivo");
						anular.ShowDialog();
					}
					else
					{
						if (!e_motivo_anulacion.Cancelo)
						{
							string motivo = e_motivo_anulacion.Motivo;
							if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
							{
								DocSys.connOracle.Open();
							}

							string sql = "excp.dcs_anular_excepcion";
							OracleParameter pa_codigo_Excepcion = new OracleParameter("pa_codigo_excepcion", OracleType.Number);
							pa_codigo_Excepcion.Direction = ParameterDirection.Input;
							pa_codigo_Excepcion.Value = Convert.ToInt32(this.txtExcepcion.Text);

							OracleParameter pa_opcion = new OracleParameter("pa_opcion", OracleType.VarChar);
							pa_opcion.Direction = ParameterDirection.Input;
							pa_opcion.Value = "ADMIN";

							OracleParameter pa_motivo = new OracleParameter("pa_motivo", OracleType.VarChar, 200);
							pa_motivo.Direction = ParameterDirection.Input;
							pa_motivo.Value = motivo;

							OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
							cmd.CommandType = CommandType.StoredProcedure;
							cmd.Parameters.Add(pa_codigo_Excepcion);
							cmd.Parameters.Add(pa_opcion);
							cmd.Parameters.Add(pa_motivo);

							OracleDataReader dr = cmd.ExecuteReader();
							dr.Close();
							cmd.Dispose();
							DocSys.connOracle.Close();

							this.txtNo_solicitud_Leave(null, null);
							MessageBox.Show("Excepción anulada con éxito", "Operación exitosa");
						}
						else
						{
							MessageBox.Show("Acción cancelada por el usuario", "¡Aviso!");
						}
					}
				}
				else
				{
					MessageBox.Show("La excepción no se puede anular debido a su estado", "Error");
				}
			}
			catch (Exception)
			{

				throw;
			}
		}

		private void CargarResoluciones(int codigoExcepcion)
		{
			var resoluciones = this.da.GetResoluciones(codigoExcepcion);
			if (resoluciones.Rows.Count > 0)
			{
				this.dgvResoluciones.DataSource = null;
				this.dgvResoluciones.DataSource = resoluciones;
			}
		}

		private void btnEditarResolución_Click(object sender, EventArgs e)
		{
			if (this.dgvResoluciones.Rows.Count > 0)
			{
				DataGridViewRow row = this.dgvResoluciones.CurrentRow;
				string pendiente = row.Cells["pendiente_respuesta_b"].Value.ToString();

				if (pendiente.Equals("S"))
				{
					MessageBox.Show("No puede modificar la resolución porque aún no ha sido respondida");
					return;
				}
				else
				{
					DialogResult result = MessageBox.Show($"¿Está seguro que desea cambiar la observación del comité?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
					if (result == DialogResult.Yes)
					{
						string observAnterior = row.Cells["observaciones"].Value.ToString();
						r_resoluciones_miembros forma = new r_resoluciones_miembros(observAnterior);
						result = forma.ShowDialog();
						if (result == DialogResult.OK)
						{
							string obsrNueva = forma.ObservacionNueva;
							string usuarioActual = row.Cells["usuario_comite"].Value.ToString();
							string reiniciar = "N";
							if (string.IsNullOrEmpty(obsrNueva))
							{
								reiniciar = "S";
							}
							if (this.EditarResoluciones("EDITAR", usuarioActual, string.Empty, obsrNueva, reiniciar))
							{
								row.Cells["observaciones"].Value = obsrNueva;
							}
						}
						else
						{
							return;
						}
					}
				}
			}
		}
	}

	public class Aprobaciones
	{
		public string UsuarioComite { get; set; }
		public string PendienteR { get; set; }
		public string UsuarioAnterior { get; set; }
		public int NoRegistroAp { get; set; }
	}
}
