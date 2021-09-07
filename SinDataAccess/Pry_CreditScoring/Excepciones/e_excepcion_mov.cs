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
	public partial class e_excepcion_mov : Form
	{
		#region
		const int WM_SYSCOMMAND = 0x112;
		const int MOUSE_MOVE = 0xF012;
		public static bool con_borde = true;
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

		private void moverForm()
		{
			ReleaseCapture();
			SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
		}

		#endregion

		private int CodigoExcepcion = 0;
		private DataAccess da;
		private bool primeraCarga = true;
		private int estacion_id_actual = 0;
		private int no_filial = 0;

		public e_excepcion_mov(int _codExcepcion, DataAccess da)
		{
			InitializeComponent();

			this.CodigoExcepcion = _codExcepcion;
			this.txtExcepcion.Text = this.CodigoExcepcion.ToString();
			this.da = da;

			this.cargar_generales();
			this.cargar_detalle_excepcion();
			this.cargar_movmientos();			
			this.Resolucion_comite();
		}

		private void panelTop_MouseDown(object sender, MouseEventArgs e)
		{
			moverForm();
		}

		private void e_solicitud_detalle_Load(object sender, EventArgs e)
		{
			this.Llenar_anotaciones();
			this.llenar_adjuntos();
			list_adjuntos.View = View.Tile;
			list_anotaciones.View = View.Details;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		#region Cargas de datos

		private void cargar_generales()
		{
			try
			{
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				string sql = "select e.no_solicitud, e.fecha_cierre, e.fecha_presentacion, e.codigo_agencia_origen, e.oficial_servicios, e.condicion_tu, e.abierta, "
							+ "case when e.pago_mediante = 'V' then 'Pago por Ventanilla' else 'Pago por Planilla' end pago_mediante, "
							+ "e.ANTIGUEDAD_MESES, e.ANTIGUEDAD_DIAS, e.ANTIGUEDAD_HORAS, e.ANTIGUEDAD_MINUTOS, e.ANTIGUEDAD_SEGUNDOS, "
							+ "ee.descripcion estado, "
							+ "u.nombres || ' ' || u.PRIMER_APELLIDO || ' ' || u.SEGUNDO_APELLIDO osNombre, "
							+ "ag.nombre_agencia, "
							+ "s.codigo_cliente, s.monto_solicitado, "
							+ "c.NOMBRES || ' ' || c.PRIMER_APELLIDO || ' ' || c.segundo_apellido nombre_cliente, "
							+ "sa.desc_sub_aplicacion, "
							+ "(select max(no_movimiento) from excp.dcs_movimientos_excep m where m.codigo_excepcion = e.codigo_excepcion) mov_actual, "
							+ "(select es.nombre from excp.dcs_movimientos_excep m, wfc.dcs_wf_estaciones es "
							+ " where m.codigo_excepcion = e.codigo_excepcion "
							+ "and no_movimiento = (select max(no_movimiento) "
							+ "from excp.dcs_movimientos_excep m "
							+ "where m.codigo_excepcion = e.codigo_excepcion) and es.estacion_id = m.estacion_id_to "
							+ ") estacion_id_actual, "
							+ "(select es.estacion_id from excp.dcs_movimientos_excep m, wfc.dcs_wf_estaciones es "
							+ "where m.codigo_excepcion = e.codigo_excepcion "
							+ "and no_movimiento = (select max(no_movimiento) "
							+ "from excp.dcs_movimientos_excep m "
							+ "where m.codigo_excepcion = e.codigo_excepcion) and es.estacion_id = m.estacion_id_to "
							+ ") estacion_id "
							+ "from excp.dcs_excepcion_solicitud e, excp.dcs_exc_estados_excepcion ee, "
							+ "mgi.mgi_usuarios u, mgi.mgi_agencias ag, wfc.dcs_solicitudes s, mgi.mgi_clientes c, mgi_sub_aplicaciones sa "
							+ "where e.codigo_estado_excep = ee.ESTADO_EXCEP_ID "
							+ "and u.codigo_usuario = e.oficial_servicios "
							+ "and ag.codigo_agencia = e.codigo_agencia_origen "
							+ "and s.no_solicitud = e.no_solicitud "
							+ "and c.codigo_cliente = s.codigo_cliente "
							+ "and s.codigo_sub_aplicacion = sa.codigo_sub_aplicacion "
							+ "and e.codigo_excepcion = :codExcepcion ";

				OracleParameter pa_codigo_excepcion = new OracleParameter("codExcepcion", OracleType.Number);
				pa_codigo_excepcion.Direction = ParameterDirection.Input;
				pa_codigo_excepcion.Value = this.CodigoExcepcion;

				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.Add(pa_codigo_excepcion);

				DataTable dt = new DataTable();
				OracleDataAdapter da = new OracleDataAdapter(cmd);
				da.Fill(dt);

				this.txtSolicitud.Text = dt.Rows[0]["no_solicitud"].ToString();
				this.txtEstado.Text = dt.Rows[0]["estado"].ToString();
				this.txtFechaPresen.Text = dt.Rows[0]["fecha_presentacion"].ToString();
				this.txtFecha_cierre.Text = dt.Rows[0]["fecha_cierre"].ToString();
				this.txtCodigoCliente.Text = dt.Rows[0]["codigo_cliente"].ToString();
				this.txtNombreAfiliado.Text = dt.Rows[0]["nombre_cliente"].ToString();
				this.txtEstacionActual.Text = dt.Rows[0]["estacion_id_actual"].ToString();
				this.txtFilial.Text = dt.Rows[0]["nombre_agencia"].ToString();
				this.txtOficialServicio.Text = dt.Rows[0]["oficial_servicios"].ToString();
				this.txtNombreOficial.Text = dt.Rows[0]["osNombre"].ToString();
				this.txtProducto.Text = dt.Rows[0]["desc_sub_aplicacion"].ToString();
				this.txtCondicionTu.Text = dt.Rows[0]["condicion_tu"].ToString();
				this.txtMonto.Text = dt.Rows[0]["monto_solicitado"].ToString();
				this.txtPagoMediante.Text = dt.Rows[0]["pago_mediante"].ToString();
				this.estacion_id_actual = Convert.ToInt32(dt.Rows[0]["estacion_id"].ToString());
				this.no_filial = Convert.ToInt32(dt.Rows[0]["codigo_agencia_origen"].ToString());

				string vl_abierta = dt.Rows[0]["abierta"].ToString();

				if (primeraCarga)
				{
					primeraCarga = false;
					//Obteniendo Fotografia
					string vl_fecha_ultima_act = "";
					byte[] foto;
					this.da.ObtenerFotoAfiliado(this.txtCodigoCliente.Text, out foto, out vl_fecha_ultima_act);
					if (foto != null)
					{
						pbFotoVigente.Image = DocSys.p_CopyDataToBitmap(foto);
					}
				}

				if (vl_abierta == "N")
				{
					timer1.Enabled = false;
				}
				if (vl_abierta == "S")
				{
					this.antiguedad_Excepcion_abierta();
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
				DataTable detalle = this.da.get_detalle_info(this.CodigoExcepcion);
				this.dgvDetalle.DataSource = detalle;				
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ha ocurrido un error {ex.Message}", "Error");
			}
		}

		private void cargar_movmientos()
		{
			try
			{
				string sql = "Select enviado_por, "
							+ "fecha_envio, "
							+ "estacion_id_from, "
							+ "'' nombre_from, "
							+ "estacion_id_to, "
							+ "e1.nombre as nombre_to, "
							+ "no_movimiento, "
							+ "'' analista, "
							+ "'Creación de la excepcion' decision,      "
							+ "antiguedad_meses, "
							+ "antiguedad_dias Ant_dias, "
							+ "antiguedad_horas Ant_horas, "
							+ "antiguedad_minutos Ant_minutos, "
							+ "antiguedad_segundos Ant_segundos, "
							+ "estadia_mov estadia_movimiento "
							+ "from excp.dcs_movimientos_excep ms, "
							+ "wfc.dcs_wf_estaciones e1, "
							+ "excp.dcs_exc_tipo_decisiones d "
							+ "where ms.estacion_id_to = e1.estacion_id "
							+ "and ms.decision_id = d.decision_id(+) "
							+ "and ms.estacion_id_from = 0 "
							+ "and codigo_excepcion = :pa_codigo_excepcion "
							+ "UNION "
							+ "Select enviado_por, "
							+ "fecha_envio, "
							+ "estacion_id_from, "
							+ "e1.nombre as nombre_from, "
							+ "estacion_id_to, "
							+ "e2.nombre as nombre_to, "
							+ "no_movimiento,            "
							+ "'' analista, "
							+ "d.descripcion decision, "
							+ "antiguedad_meses, "
							+ "antiguedad_dias Ant_dias,"
							+ "antiguedad_horas Ant_horas, "
							+ "antiguedad_minutos Ant_minutos, "
							+ "antiguedad_segundos Ant_segundos, "
							+ "estadia_mov estadia_movimiento "
							+ "from excp.dcs_movimientos_excep ms,  "
							+ "wfc.dcs_wf_estaciones e1, "
							+ "wfc.dcs_wf_estaciones e2, "
							+ "excp.dcs_exc_tipo_decisiones d "
							+ "where ms.estacion_id_from = e1.estacion_id "
							+ "and ms.decision_id = d.decision_id "
							+ "and ms.estacion_id_to = e2.estacion_id "
							+ "and codigo_excepcion = :pa_codigo_excepcion "
							+ "order by no_movimiento ";

				OracleParameter pa_codigo_excepcion = new OracleParameter("pa_codigo_excepcion", OracleType.Number);
				pa_codigo_excepcion.Direction = ParameterDirection.Input;
				pa_codigo_excepcion.Value = this.CodigoExcepcion;				

				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.Parameters.Add(pa_codigo_excepcion);
				OracleDataAdapter oda = new OracleDataAdapter(cmd);
				DataTable dt = new DataTable();

				oda.Fill(dt);

				this.dgvMovimientos.DataSource = dt;
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error");
			}
		}

		private void antiguedad_Excepcion_abierta()
		{
			try
			{
				string vl_sql = @"wfc.DCS_P_UTIL_CALCULA_DIF_FECHAS";
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}
				OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
				cmd2.CommandType = CommandType.StoredProcedure;
				//───────────────────
				OracleParameter p_fec_inicio = new OracleParameter("p_fec_inicio", OracleType.DateTime);
				cmd2.Parameters.Add(p_fec_inicio);
				p_fec_inicio.Direction = ParameterDirection.Input;
				p_fec_inicio.Value = DateTime.Parse(txtFechaPresen.Text);
				//───────────────────            
				OracleParameter p_fec_fin = new OracleParameter("p_fec_fin", OracleType.DateTime);
				cmd2.Parameters.Add(p_fec_fin);
				p_fec_fin.Direction = ParameterDirection.Input;
				p_fec_fin.Value = DocSys.p_obtener_fecha_server();

				OracleParameter p_anyos = new OracleParameter("p_anyos", OracleType.Int32);
				cmd2.Parameters.Add(p_anyos);
				p_anyos.Direction = ParameterDirection.Output;

				OracleParameter p_meses = new OracleParameter("p_meses", OracleType.Int32);
				cmd2.Parameters.Add(p_meses);
				p_meses.Direction = ParameterDirection.Output;

				OracleParameter p_dias = new OracleParameter("p_dias", OracleType.Int32);
				cmd2.Parameters.Add(p_dias);
				p_dias.Direction = ParameterDirection.Output;

				OracleParameter p_horas = new OracleParameter("p_horas", OracleType.Int32);
				cmd2.Parameters.Add(p_horas);
				p_horas.Direction = ParameterDirection.Output;

				OracleParameter p_minutos = new OracleParameter("p_minutos", OracleType.Int32);
				cmd2.Parameters.Add(p_minutos);
				p_minutos.Direction = ParameterDirection.Output;

				OracleParameter p_segundos = new OracleParameter("p_segundos", OracleType.Int32);
				cmd2.Parameters.Add(p_segundos);
				p_segundos.Direction = ParameterDirection.Output;

				OracleDataReader dr = cmd2.ExecuteReader();
				dr.Read();
				string vl_anyos = p_anyos.Value.ToString();
				textBox_antiguedad_meses.Text = p_meses.Value.ToString();
				textBox_antiguedad_dias.Text = p_dias.Value.ToString();
				textBox_antiguedad_horas.Text = p_horas.Value.ToString();
				textBox_antiguedad_minutos.Text = p_minutos.Value.ToString();
				textBox_antiguedad_segundos.Text = p_segundos.Value.ToString();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Resolucion_comite()
		{
			try
			{
				this.txtComiteResolucion.Text = "Pendiente";
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error");
			}
		}

		#endregion

		private void timer1_Tick(object sender, EventArgs e)
		{
			this.antiguedad_Excepcion_abierta();
		}

		private void lLResoluciones_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			e_resoluciones_excp reso = new e_resoluciones_excp(this.da, this.CodigoExcepcion);
			reso.ShowDialog();
		}

		private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			e_nueva_excepcion_solicitud forma = new e_nueva_excepcion_solicitud(Convert.ToInt32(this.txtSolicitud.Text), u_Globales.accionModificar, 
																				this.da, Convert.ToInt32(this.txtExcepcion.Text), 
																				string.Empty, this.estacion_id_actual); //es el número no el nomber
			forma.noFilial = this.no_filial;
			forma.ShowDialog();
		}

		private void Llenar_anotaciones()
		{
			list_anotaciones.Clear();
			string orden = string.Empty;
			string porMovimiento = string.Empty;

			if (this.rbOrdenCrono.Checked)
				orden = " order by anot.no_anotacion asc";

			if (this.rbOrdenxEstacion.Checked)
				orden = " order by anot.estacion_id asc";

			int no_mov = 0;
			DataGridViewRow row = this.dgvMovimientos.CurrentRow;
			if (row != null)
			{
				if(this.checkBox_por_movimiento.Checked)				
				{
					no_mov = int.Parse(row.Cells["no_movimiento"].Value.ToString());
					porMovimiento = " and anot.no_movimiento_excepcion = " + no_mov + " ";
				}				
			}

			try
			{
				string query = @"Select est.nombre nombre_estacion,
									 anot.no_anotacion,
									 anot.anotacion,
									 anot.tipo_anotacion,
									 anot.usuario_ing,
									 anot.fecha_ing   
								from dcs_anotaciones_excepciones anot, 
									 dcs_wf_estaciones est  
								where anot.estacion_id=est.estacion_id 
								 and anot.codigo_excepcion = :codigoExcep "
								+ porMovimiento
								+ orden;

				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}
				OracleCommand cmd2 = new OracleCommand(query, DocSys.connOracle);
				cmd2.CommandType = CommandType.Text;

				OracleParameter pa_parametro1 = new OracleParameter("codigoExcep", OracleType.Int32);
				cmd2.Parameters.Add(pa_parametro1);
				pa_parametro1.Direction = ParameterDirection.Input;
				pa_parametro1.Value = this.CodigoExcepcion;

				OracleDataReader dr = cmd2.ExecuteReader();
				DataTable tabla = new DataTable();
				tabla.Columns.Add("estacion");
				tabla.Columns.Add("no_anotacion");
				tabla.Columns.Add("anotacion");
				tabla.Columns.Add("tipo_anotacion");
				tabla.Columns.Add("usuario_ing");
				tabla.Columns.Add("fecha_ing");

				while (dr.Read())
				{
					tabla.Rows.Add(dr["nombre_estacion"].ToString(),
								   dr["no_anotacion"].ToString(),
								   dr["anotacion"].ToString(),
								   dr["tipo_anotacion"].ToString(),
								   dr["usuario_ing"].ToString(),
								   dr["fecha_ing"].ToString());
				}

				list_anotaciones.BeginUpdate();
				list_anotaciones.SmallImageList = imagesSmall;
				list_anotaciones.LargeImageList = imagesLarge;
				list_anotaciones.Clear();
				list_anotaciones.Groups.Clear();

				string vl_estacion = "";
				bool inicio = true;
				int indgrupo = 0;
				foreach (DataRow fila in tabla.Rows)
				{
					ListViewItem listItem = new ListViewItem(fila["no_anotacion"].ToString());
					if (fila["tipo_anotacion"].ToString() == "N")
						listItem.ImageIndex = 4;
					if (fila["tipo_anotacion"].ToString() == "C")
						listItem.ImageIndex = 5;

					listItem.ToolTipText = fila["anotacion"].ToString();
					if (inicio)
					{
						indgrupo = 0;
						list_anotaciones.Groups.Add(new ListViewGroup(fila["estacion"].ToString(), HorizontalAlignment.Left));
						vl_estacion = fila["estacion"].ToString();
						inicio = false;
					}
					if (fila["estacion"].ToString() != vl_estacion)
					{
						list_anotaciones.Groups.Add(new ListViewGroup(fila["estacion"].ToString(), HorizontalAlignment.Left));
						vl_estacion = fila["estacion"].ToString();
						indgrupo++;
					}
					listItem.Group = list_anotaciones.Groups[indgrupo];
					listItem.SubItems.Add(fila["usuario_ing"].ToString());
					listItem.SubItems.Add(fila["fecha_ing"].ToString());
					list_anotaciones.Items.Add(listItem);
				}

				list_anotaciones.Columns.Add("Anotacion/#", 100, HorizontalAlignment.Left);
				list_anotaciones.Columns.Add("Usuario", 80, HorizontalAlignment.Left);
				list_anotaciones.Columns.Add("Fecha", 140, HorizontalAlignment.Left);
				list_anotaciones.EndUpdate();
				list_anotaciones.Sort();
				list_anotaciones.View = View.Details;

				dr.Close();
				cmd2.Dispose();
				DocSys.connOracle.Close();
			}
			catch (Exception)
			{

				throw;
			}
		}

		private void checkBox_por_movimiento_CheckedChanged(object sender, EventArgs e)
		{
			this.Llenar_anotaciones();
		}

		private void llenar_adjuntos()
		{
			try
			{
				string query = @"Select NO_DOCUMENTO no_archivo,
									  NOMBRE_DOCUMENTO nombre_archivo,
									  extension,
									  decode(formato_excepcion,'S','Formato Excepción', 'Otros documentos') descripcion  
								 From dcs_adjuntos_excep 
								Where codigo_excepcion = :pa_codigo_excep  
								Order by no_documento ";

				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				OracleCommand cmd2 = new OracleCommand(query, DocSys.connOracle);
				cmd2.CommandType = CommandType.Text;
				//───────────────────
				OracleParameter pa_parametro1 = new OracleParameter("pa_codigo_excep", OracleType.Int32);
				cmd2.Parameters.Add(pa_parametro1);
				pa_parametro1.Direction = ParameterDirection.Input;
				pa_parametro1.Value = this.CodigoExcepcion;

				OracleDataReader dr = cmd2.ExecuteReader();
				DataTable tabla = new DataTable();
				tabla.Columns.Add("no_archivo");
				tabla.Columns.Add("nombre_archivo");
				tabla.Columns.Add("extension");
				tabla.Columns.Add("descripcion");

				while (dr.Read())
				{
					tabla.Rows.Add(dr["no_archivo"].ToString(),
								   dr["nombre_archivo"].ToString(),
								   dr["extension"].ToString(),
								   dr["descripcion"].ToString());
				}

				list_adjuntos.BeginUpdate();
				list_adjuntos.SmallImageList = imagesSmall;
				list_adjuntos.LargeImageList = imagesLarge;
				list_adjuntos.Clear();

				foreach (DataRow fila in tabla.Rows)
				{
					ListViewItem listItem = new ListViewItem(fila["descripcion"].ToString());
					listItem.ImageIndex = 3;
					listItem.ToolTipText = fila["descripcion"].ToString();
					listItem.SubItems.Add(fila["extension"].ToString());
					listItem.SubItems.Add(fila["no_archivo"].ToString());
					list_adjuntos.Items.Add(listItem);
				}

				list_adjuntos.Columns.Add("Nombre Archivo", 180, HorizontalAlignment.Left);
				list_adjuntos.Columns.Add("Ext", 60, HorizontalAlignment.Left);
				list_adjuntos.Columns.Add("No. Documento", 60, HorizontalAlignment.Left);
				list_adjuntos.EndUpdate();
				list_adjuntos.Sort();
				list_adjuntos.View = View.LargeIcon;

			}
			catch (Exception ex)
			{
				MessageBox.Show("Error en p_llenar_adjuntos : " + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
		}

		private void list_adjuntos_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			ListViewItem item = list_adjuntos.GetItemAt(e.X, e.Y);
			if (item != null)
			{
				int codAdjunto = int.Parse(item.SubItems[2].Text);
				string query = @"SELECT no_documento, codigo_excepcion, nombre_documento, archivo_bin, extension 
								FROM excp.dcs_adjuntos_excep where no_documento = :CodDocumento";

				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				OracleCommand cmd2 = new OracleCommand(query, DocSys.connOracle);
				cmd2.CommandType = CommandType.Text;

				OracleParameter pa_parametro1 = new OracleParameter("CodDocumento", OracleType.Int32);
				cmd2.Parameters.Add(pa_parametro1);
				pa_parametro1.Direction = ParameterDirection.Input;
				pa_parametro1.Value = codAdjunto;

				OracleDataReader dr = cmd2.ExecuteReader();
				dr.Read();

				if (!DBNull.Value.Equals(dr["archivo_bin"]))
				{
					byte[] bits = ((byte[])dr["archivo_bin"]);
					string sFile = Application.StartupPath.ToString() + @"\tmp\" + DocSys.vl_user + "tmp" + dr["codigo_excepcion"] + DateTime.Now.ToString("yyyyMMddhhmmss") + "." + dr["extension"].ToString();
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

					dr.Close();
					cmd2.Dispose();
					DocSys.connOracle.Close();
				}
			}
		}

		private void list_anotaciones_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			ListViewItem item = list_anotaciones.GetItemAt(e.X, e.Y);
			if (item != null)
			{
				int noAnotacion = int.Parse(item.SubItems[0].Text);
				e_add_notas forma = new e_add_notas(this.CodigoExcepcion, this.estacion_id_actual, u_Globales.accionModificar, noAnotacion);
				forma.ShowDialog();
			}
		}
	}
}
