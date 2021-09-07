using System;
using System.Data;
using System.Data.OracleClient;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;
using System.IO;

namespace Docsis_Application
{
	static class DocSys
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		/// 
		public static string string_db_oracle;
		public static OracleConnection connOracle;
		public static string vl_tnsnames = "csf";
		//public static string vl_tnsnames = "devcsf";
		//public static string vl_tnsnames = "virtualdes";
		//public static string vl_tnsnames = "pruebacsf";
		public static string vl_user = "";
		public static string vl_pass = "";
		public static int vl_workflow_id = 0;
		public static int vl_agencia_usuario = 0;
		public static bool vl_todas_las_filiales = false;
		public static bool vl_todos_los_usuarios = false;
		public static string vl_operacion = "NORMAL";
		public static string vl_nodo_treview = "node_entradas";
		public static string vl_mensaje_avisos = "Avisos - WFC | Coopsafa";
		public static int EstacionGlobal = 0;
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new s_login());
		}

		public static System.Drawing.Bitmap p_CopyDataToBitmap(byte[] data)
		{
			System.Drawing.ImageConverter ic = new System.Drawing.ImageConverter();
			System.Drawing.Image img = (System.Drawing.Image)ic.ConvertFrom(data);
			System.Drawing.Bitmap bitmap1 = new System.Drawing.Bitmap(img);
			return bitmap1;
		}

		public static byte[] p_CopyImageToByteArray(System.Drawing.Image imageIn)
		{
			MemoryStream ms = new MemoryStream();
			imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
			return ms.ToArray();
		}

		public static void p_obtener_string_conexion_oracle(out string pa_string)
		{
			string vl_string = "Data Source=" + vl_tnsnames + ";Persist Security Info=True;User ID=" + vl_user + ";Password=" + vl_pass;
			pa_string = vl_string;
		}

		public static DataSet p_Obtener_un_dataset(string pa_sql, string pa_tabla)
		{
			OracleCommand cmd = new OracleCommand(pa_sql, DocSys.connOracle);
			cmd.CommandType = CommandType.Text;
			OracleDataAdapter adaptador = new OracleDataAdapter(cmd);
			DataSet registros = new DataSet();
			adaptador.Fill(registros, pa_tabla);
			adaptador.Dispose();
			return registros;
		}

		public static int p_obtener_estacion_usuario(string p_usuario)
		{
			int vl_return = 0;
			try
			{
				p_obtener_string_conexion_oracle(out string_db_oracle);
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}
				/*connOracle = new OracleConnection(string_db_oracle);
                if (connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    connOracle.Open();
                }*/
				string sql = "SELECT estacion_id FROM DCS_wf_usuarios_estaciones where upper(usuario)=upper(:pa_usuario) and activo='S'";
				OracleCommand cmd = new OracleCommand(sql, connOracle);
				cmd.CommandType = CommandType.Text;
				//────────────────────────────────────────────────────────────
				OracleParameter pa_usuario = new OracleParameter("pa_usuario", OracleType.VarChar, 30);
				cmd.Parameters.Add(pa_usuario);
				pa_usuario.Direction = ParameterDirection.Input;
				pa_usuario.Value = p_usuario;

				OracleDataReader dr = cmd.ExecuteReader();
				dr.Read();
				if (dr.HasRows)
				{
					vl_return = int.Parse(dr["estacion_id"].ToString());
				}
				else
				{
					MessageBox.Show("Usuario no tiene definida una estación en CreditScoring.", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
					vl_return = 0;
				}
				dr.Close();
				connOracle.Close();
				return vl_return;
			}
			catch
			{
				vl_return = -99;
				return vl_return;
			}
		}

		public static void p_obtener_oficial_remitente(int p_no_solicitud, out string p_oficial_remitente, out string p_nombre_oficial_remitente)
		{
			string vl_oficial_remitente = "";
			string vl_nombre_oficial_remitente = "";

			p_oficial_remitente = "";
			p_nombre_oficial_remitente = "";

			try
			{
				if (DocSys.connOracle.State == ConnectionState.Closed)
				{
					DocSys.connOracle.Open();
				}
				string sql = @"Select upper(oficial_servicio) codigo_oficial,nombres||' '||primer_apellido||' '||segundo_apellido nombre_oficial  
                                From dcs_solicitudes s,
                                     mgi_usuarios u
                               Where u.codigo_empresa=1 
                                 and upper(s.oficial_servicio)=codigo_usuario
                                 and no_solicitud=:pa_no_solicitud";
				OracleCommand cmd = new OracleCommand(sql, connOracle);
				cmd.CommandType = CommandType.Text;
				//────────────────────────────────────────────────────────────
				OracleParameter pa_param1 = new OracleParameter("pa_no_solicitud", OracleType.Int32);
				cmd.Parameters.Add(pa_param1);
				pa_param1.Direction = ParameterDirection.Input;
				pa_param1.Value = p_no_solicitud;

				OracleDataReader dr = cmd.ExecuteReader();
				dr.Read();
				if (dr.HasRows)
				{
					vl_oficial_remitente = dr["codigo_oficial"].ToString();
					vl_nombre_oficial_remitente = dr["nombre_oficial"].ToString();

				}
				else
				{
					vl_oficial_remitente = "";
					vl_nombre_oficial_remitente = "";
				}
				dr.Close();
				connOracle.Close();

				p_oficial_remitente = vl_oficial_remitente;
				p_nombre_oficial_remitente = vl_nombre_oficial_remitente;

			}
			catch
			{
				p_oficial_remitente = "";
				p_nombre_oficial_remitente = "";
			}
		}

		public static string p_Obtener_un_scalar_descripcion(string p_sql, int p_codigo)
		{
			string vl_return = "";
			if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
			{
				DocSys.connOracle.Open();
			}
			OracleCommand cmd = new OracleCommand(p_sql, DocSys.connOracle);
			cmd.CommandType = CommandType.Text;

			OracleParameter pa_parametro1 = new OracleParameter("pa_codigo", OracleType.Int16);
			cmd.Parameters.Add(pa_parametro1);
			pa_parametro1.Direction = ParameterDirection.Input;
			pa_parametro1.Value = p_codigo;

			OracleDataReader dr = cmd.ExecuteReader();
			dr.Read();
			if (dr.HasRows)
			{
				vl_return = dr["descripcion"].ToString();
			}
			else
			{
				vl_return = "";
			}
			dr.Close();
			return vl_return;
		}

		public static string p_get_valor_parametro(string p_parametro)
		{
			string vl_return = "";
			if (DocSys.connOracle.State == ConnectionState.Closed)
			{
				DocSys.connOracle.Open();
			}
			string sql = @"select valor from dcs_wf_parametros where parametro=:pa_parametro";
			OracleCommand cmd = new OracleCommand(sql, connOracle);
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("pa_parametro", OracleType.VarChar, 20).Value = p_parametro;

			OracleDataReader dr = cmd.ExecuteReader();
			dr.Read();
			if (dr.HasRows)
			{
				vl_return = dr["valor"].ToString();
			}
			dr.Close();
			return vl_return;
		}

		public static string p_get_nombre_estacion(int p_estacion_id)
		{
			string vl_return = "";
			if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
			{
				DocSys.connOracle.Open();
			}
			try
			{

				string vl_sql = "";
				vl_sql = vl_sql + "Select nombre from dcs_wf_estaciones where estacion_id=:pa_estacion_id";
				OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;

				OracleParameter pa_parametro1 = new OracleParameter("pa_estacion_id", OracleType.Int16);
				cmd.Parameters.Add(pa_parametro1);
				pa_parametro1.Direction = ParameterDirection.Input;
				pa_parametro1.Value = p_estacion_id;

				OracleDataReader dr = cmd.ExecuteReader();
				dr.Read();
				if (dr.HasRows)
				{
					vl_return = dr["nombre"].ToString();
				}
				else
				{
					vl_return = "";
				}
				dr.Close();
				return vl_return;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error en p_get_nombre_estacion :" + ex.Message + " " + ex.Source, "::Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return vl_return;
			}
		}

		public static string p_get_permitir_crear_solicitud(int p_estacion_id)
		{
			string vl_return = "";
			if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
			{
				DocSys.connOracle.Open();
			}
			try
			{

				string vl_sql = "";
				vl_sql = vl_sql + "Select crear_solicitudes from dcs_wf_estaciones where estacion_id=:pa_estacion_id";
				OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;

				OracleParameter pa_parametro1 = new OracleParameter("pa_estacion_id", OracleType.Int16);
				cmd.Parameters.Add(pa_parametro1);
				pa_parametro1.Direction = ParameterDirection.Input;
				pa_parametro1.Value = p_estacion_id;

				OracleDataReader dr = cmd.ExecuteReader();
				dr.Read();
				if (dr.HasRows)
				{
					vl_return = dr["crear_solicitudes"].ToString().ToUpper();
				}
				else
				{
					vl_return = "";
				}

				dr.Close();
				return vl_return;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error en p_get_nombre_estacion :" + ex.Message + " " + ex.Source, "::Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return vl_return;
			}
		}

		public static bool p_obtener_si_todas_las_filiales(int p_estacion_id)
		{
			bool vl_return = false;
			string vl_ver_toda_filial = "N";
			if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
			{
				DocSys.connOracle.Open();
			}
			try
			{

				string vl_sql = "";
				vl_sql = vl_sql + "Select ver_toda_filial from dcs_wf_estaciones where estacion_id=:pa_estacion_id";
				OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;

				OracleParameter pa_parametro1 = new OracleParameter("pa_estacion_id", OracleType.Int16);
				cmd.Parameters.Add(pa_parametro1);
				pa_parametro1.Direction = ParameterDirection.Input;
				pa_parametro1.Value = p_estacion_id;

				OracleDataReader dr = cmd.ExecuteReader();
				dr.Read();
				if (dr.HasRows)
				{
					vl_ver_toda_filial = dr["ver_toda_filial"].ToString();
					if (vl_ver_toda_filial == "S")
						vl_return = true;
					else
						vl_return = false;

				}
				else
				{
					vl_return = false;
				}
				dr.Close();
				if (DocSys.connOracle.State == ConnectionState.Open)
				{
					DocSys.connOracle.Close();
				}
				return vl_return;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error en p_obtener_si_todas_las_filiales :" + ex.Message + " " + ex.Source, "::Aviso DocSys", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return vl_return;
			}
		}

		public static bool p_obtener_es_gerente_filial(string p_usuario)
		{
			bool vl_return = false;
			if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
			{
				DocSys.connOracle.Open();
			}
			try
			{

				string vl_sql = "";
				vl_sql = vl_sql + "select count(*) cant from mgi_grupos_usuarios where codigo_grupo=7 and codigo_usuario=:pa_codigo_usuario";
				OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;

				OracleParameter pa_codigo_usuario = new OracleParameter("pa_codigo_usuario", OracleType.VarChar, 40);
				cmd.Parameters.Add(pa_codigo_usuario);
				pa_codigo_usuario.Direction = ParameterDirection.Input;
				pa_codigo_usuario.Value = p_usuario;

				OracleDataReader dr = cmd.ExecuteReader();
				dr.Read();
				if (dr.HasRows)
				{
					if (int.Parse(dr["cant"].ToString()) > 0)
						vl_return = true;
					else
						vl_return = false;
				}
				else
				{
					vl_return = false;
				}
				dr.Close();
				return vl_return;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error en p_obtener_es_gerente_filial :" + ex.Message + " " + ex.Source, "::Aviso DocSys", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return vl_return;
			}
		}

		public static bool p_obtener_es_usuario_prestamos(string p_usuario)
		{
			bool vl_return = false;
			if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
			{
				DocSys.connOracle.Open();
			}
			try
			{

				string vl_sql = "";
				vl_sql = vl_sql + "select count(*) cant from mgi_grupos_usuarios where codigo_grupo=10 and codigo_usuario=:pa_codigo_usuario";
				OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;

				OracleParameter pa_codigo_usuario = new OracleParameter("pa_codigo_usuario", OracleType.VarChar, 40);
				cmd.Parameters.Add(pa_codigo_usuario);
				pa_codigo_usuario.Direction = ParameterDirection.Input;
				pa_codigo_usuario.Value = p_usuario;

				OracleDataReader dr = cmd.ExecuteReader();
				dr.Read();
				if (dr.HasRows)
				{
					if (int.Parse(dr["cant"].ToString()) > 0)
						vl_return = true;
					else
						vl_return = false;
				}
				else
				{
					vl_return = false;
				}
				dr.Close();
				return vl_return;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error en p_obtener_es_gerente_filial :" + ex.Message + " " + ex.Source, "::Aviso DocSys", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return vl_return;
			}
		}

		public static bool p_obtener_es_admon_sistema(string p_usuario)
		{
			bool vl_return = false;
			if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
			{
				DocSys.connOracle.Open();
			}
			try
			{

				string vl_sql = "";
				vl_sql = vl_sql + "select count(*) cant from mgi_grupos_usuarios where codigo_grupo=3 and codigo_usuario=:pa_codigo_usuario";
				OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;

				OracleParameter pa_codigo_usuario = new OracleParameter("pa_codigo_usuario", OracleType.VarChar, 40);
				cmd.Parameters.Add(pa_codigo_usuario);
				pa_codigo_usuario.Direction = ParameterDirection.Input;
				pa_codigo_usuario.Value = p_usuario;

				OracleDataReader dr = cmd.ExecuteReader();
				dr.Read();
				if (dr.HasRows)
				{
					if (int.Parse(dr["cant"].ToString()) > 0)
						vl_return = true;
					else
						vl_return = false;
				}
				else
				{
					vl_return = false;
				}
				dr.Close();
				return vl_return;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error en p_obtener_es_admon_sistema :" + ex.Message + " " + ex.Source, "::Aviso DocSys", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return vl_return;
			}
		}

		public static void p_obtener_filial_usuario(string p_usuario, out int p_codigo_agencia, out string p_nombre_agencia)
		{
			int vl_return = 0;
			p_codigo_agencia = 0;
			p_nombre_agencia = "";
			try
			{
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}
				string sql = @"select a.codigo_agencia,nombre_agencia 
                                from mgi_agencias a,
                                     mgi_usuarios u 
                              where a.codigo_empresa=1
                                and a.codigo_empresa=u.codigo_empresa
                                and u.codigo_agencia=a.codigo_agencia 
                                and codigo_usuario=upper(:pa_usuario)";
				OracleCommand cmd = new OracleCommand(sql, connOracle);
				cmd.CommandType = CommandType.Text;
				//────────────────────────────────────────────────────────────
				OracleParameter pa_usuario = new OracleParameter("pa_usuario", OracleType.VarChar, 30);
				cmd.Parameters.Add(pa_usuario);
				pa_usuario.Direction = ParameterDirection.Input;
				pa_usuario.Value = p_usuario;

				OracleDataReader dr = cmd.ExecuteReader();
				dr.Read();
				if (dr.HasRows)
				{
					p_codigo_agencia = int.Parse(dr["codigo_agencia"].ToString());
					p_nombre_agencia = dr["nombre_agencia"].ToString();
				}
				else
				{
					p_codigo_agencia = 0;
					p_nombre_agencia = "";
					MessageBox.Show("Usuario no tiene definada una Filial de trabajo..", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);

				}
				dr.Close();
				connOracle.Close();
			}
			catch
			{
				p_codigo_agencia = 0;
				p_nombre_agencia = "";
			}
		}

		public static int p_obtener_clientes_pendientes_importar()
		{
			int vl_return = 0;
			try
			{
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}
				/*p_obtener_string_conexion_oracle(out string_db_oracle);
                connOracle = new OracleConnection(string_db_oracle);
                if (connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    connOracle.Open();
                }*/
				string sql = "select nvl(count(*),0) AS cant_cl from arplme where procesado_spr='N'";
				OracleCommand cmd = new OracleCommand(sql, connOracle);
				cmd.CommandType = CommandType.Text;
				//────────────────────────────────────────────────────────────
				OracleDataReader dr = cmd.ExecuteReader();
				dr.Read();
				if (dr.HasRows)
				{
					vl_return = int.Parse(dr["cant_cl"].ToString());
				}
				else
				{
					vl_return = 0;
				}
				dr.Close();
				connOracle.Close();
				return vl_return;
			}
			catch
			{
				vl_return = -99;
				return vl_return;
			}
		}

		public static string p_obtener_nombre_workflow(int p_workflow_id)
		{
			string vl_return = "";
			try
			{
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}
				/*p_obtener_string_conexion_oracle(out string_db_oracle);
                connOracle = new OracleConnection(string_db_oracle);
                if (connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    connOracle.Open();
                }*/
				string sql = "select * from dcs_workflows where workflow_id=:pa_workflow_id";
				OracleCommand cmd = new OracleCommand(sql, connOracle);
				cmd.CommandType = CommandType.Text;
				//────────────────────────────────────────────────────────────
				OracleParameter pa_workflow_id = new OracleParameter("pa_workflow_id", OracleType.VarChar, 30);
				cmd.Parameters.Add(pa_workflow_id);
				pa_workflow_id.Direction = ParameterDirection.Input;
				pa_workflow_id.Value = p_workflow_id;

				OracleDataReader dr = cmd.ExecuteReader();
				dr.Read();
				if (dr.HasRows)
				{
					vl_return = dr["nombre_workflow"].ToString();
				}
				else
				{
					vl_return = "";
				}
				dr.Close();
				connOracle.Close();
				return vl_return;
			}
			catch
			{
				vl_return = "";
				return vl_return;
			}
		}

		public static int p_verificar_doc_existe(int p_no_solicitud, int p_documento_id)
		{

			int vl_return = 0;
			string vl_sql = @"select count(*) cantidad 
                                from dcs_archivos_adjuntos 
                               where no_solicitud=:pa_no_solicitud
                                 and documento_id=:pa_documento_id";
			if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
			{
				DocSys.connOracle.Open();
			}
			OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
			cmd2.CommandType = CommandType.Text;
			//───────────────────
			OracleParameter pa_no_solicitud = new OracleParameter("pa_no_solicitud", OracleType.Int32);
			cmd2.Parameters.Add(pa_no_solicitud);
			pa_no_solicitud.Direction = ParameterDirection.Input;
			pa_no_solicitud.Value = p_no_solicitud;
			//───────────────────
			OracleParameter pa_documento_id = new OracleParameter("pa_documento_id", OracleType.Int32);
			cmd2.Parameters.Add(pa_documento_id);
			pa_documento_id.Direction = ParameterDirection.Input;
			pa_documento_id.Value = p_documento_id;
			//───────────────────
			OracleDataReader dr = cmd2.ExecuteReader();
			dr.Read();
			if (dr.HasRows)
			{
				vl_return = int.Parse(dr["cantidad"].ToString());
			}
			else
				vl_return = 0;
			dr.Close();
			return vl_return;
		}

		public static int p_get_estacion_current(int p_no_solicitud)
		{
			int vl_return = 0;
			string sql = @"Select estacion_id
                            from dcs_solicitudes s                                      
                           where no_solicitud=:pa_no_solicitud";

			if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
			{
				DocSys.connOracle.Open();
			}
			OracleCommand cmd2 = new OracleCommand(sql, DocSys.connOracle);
			cmd2.CommandType = CommandType.Text;
			//───────────────────
			OracleParameter pa_no_solicitud = new OracleParameter("pa_no_solicitud", OracleType.Int32);
			cmd2.Parameters.Add(pa_no_solicitud);
			pa_no_solicitud.Direction = ParameterDirection.Input;
			pa_no_solicitud.Value = p_no_solicitud;

			OracleDataReader dr = cmd2.ExecuteReader();
			dr.Read();
			if (dr.HasRows)
			{
				vl_return = int.Parse(dr["estacion_id"].ToString());
			}
			dr.Close();
			return vl_return;

		}

		public static string p_formato_24h(int p_hora24h)
		{
			string vl_return = p_hora24h.ToString().PadLeft(4, char.Parse("0")).Substring(0, 2) + ":" + p_hora24h.ToString().PadLeft(4, char.Parse("0")).Substring(2, 2);
			return vl_return;
		}

		public static bool p_get_esdia_laborable(DateTime p_fecha)
		{
			Int32 vlreturn = 1;

			if (DocSys.connOracle.State == ConnectionState.Closed)
			{
				DocSys.connOracle.Open();
			}
			string vl_sql = "DCS_F_DIA_LABORABLE";
			OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
			cmd.CommandType = CommandType.StoredProcedure;
			//───────────────────
			OracleParameter pa_fecha = new OracleParameter("pa_fecha", OracleType.DateTime);
			cmd.Parameters.Add(pa_fecha);
			pa_fecha.Direction = ParameterDirection.Input;
			pa_fecha.Value = p_fecha;

			OracleParameter vl_return = new OracleParameter("vl_respuesta", OracleType.Number);
			cmd.Parameters.Add(vl_return);
			vl_return.Direction = ParameterDirection.ReturnValue;

			cmd.ExecuteNonQuery();
			cmd.Dispose();
			vlreturn = int.Parse(vl_return.Value.ToString());


			if (vlreturn == 1)
				return true;
			else
				return false;
		}

		public static string p_get_dayname(DateTime pa_fecha)
		{
			System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("Es-Es");
			return ci.DateTimeFormat.GetDayName(pa_fecha.DayOfWeek).ToString().ToUpper();
		}

		public static bool p_valida_politicas_envio_credito()
		{
			if (DocSys.p_get_valor_parametro("WFC-0001") == "S")
			{
				if (!DocSys.p_get_esdia_laborable(CoopSafa_utils.Funciones_Oracle.fecha_servidor(DocSys.connOracle)))
				{
					MessageBox.Show("El dia de hoy " + DocSys.p_get_dayname(CoopSafa_utils.Funciones_Oracle.fecha_servidor(DocSys.connOracle)) + " esta indicado como feriado en el sistema, no es permitido enviar solicitudes", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Stop);
					return false;
				}
			}


			if (DocSys.p_get_valor_parametro("WFC-0002") == "S")
			{
				int nHoraActual = Convert.ToInt32(CoopSafa_utils.Funciones_Oracle.fecha_servidor(DocSys.connOracle).ToString("HHmm"));
				int nHoraLimite = Convert.ToInt32(DocSys.p_get_valor_parametro("WFC-0003"));



				if (nHoraActual >= nHoraLimite)
				{
					MessageBox.Show("La hora actual en el sistema es " + DocSys.p_formato_24h(nHoraActual) + ", la hora maxima para envio de solicitudes es de " + DocSys.p_formato_24h(nHoraLimite), DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Stop);
					return false;
				}
			}

			return true;

		}

		public static string ToTitleCase(this string s)
		{
			return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
		}

		public static int p_obtener_no_archivo(int p_no_solicitud, int p_documento_id)
		{

			int vl_return = 0;
			string vl_sql = @"select no_archivo 
                                from dcs_archivos_adjuntos 
                               where no_solicitud=:pa_no_solicitud
                                 and documento_id=:pa_documento_id";
			if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
			{
				DocSys.connOracle.Open();
			}
			OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
			cmd2.CommandType = CommandType.Text;
			//───────────────────
			OracleParameter pa_no_solicitud = new OracleParameter("pa_no_solicitud", OracleType.Int32);
			cmd2.Parameters.Add(pa_no_solicitud);
			pa_no_solicitud.Direction = ParameterDirection.Input;
			pa_no_solicitud.Value = p_no_solicitud;
			//───────────────────
			OracleParameter pa_documento_id = new OracleParameter("pa_documento_id", OracleType.Int32);
			cmd2.Parameters.Add(pa_documento_id);
			pa_documento_id.Direction = ParameterDirection.Input;
			pa_documento_id.Value = p_documento_id;
			//───────────────────
			OracleDataReader dr = cmd2.ExecuteReader();
			dr.Read();
			if (dr.HasRows)
			{
				vl_return = int.Parse(dr["no_archivo"].ToString());
			}
			else
				vl_return = 0;
			dr.Close();
			return vl_return;
		}

		public static bool checkIsInPolygon(List<Coordinate> poly, Coordinate pto)
		{
			//this method uses the ray tracing algorithm to determine if the point is in the polygon
			int nPoints = poly.Count;
			int j = -999;
			int i = -999;
			bool locatedInPolygon = false;
			for (i = 0; i < (nPoints); i++)
			{
				//repeat loop for all sets of points
				if (i == (nPoints - 1))
				{
					//if i is the last vertex, let j be the first vertex
					j = 0;
				}
				else
				{
					//for all-else, let j=(i+1)th vertex
					j = i + 1;
				}

				float vertY_i = (float)poly[i].Y;
				float vertX_i = (float)poly[i].X;
				float vertY_j = (float)poly[j].Y;
				float vertX_j = (float)poly[j].X;
				float testX = (float)pto.X;
				float testY = (float)pto.Y;

				// following statement checks if testPoint.Y is below Y-coord of i-th vertex
				bool belowLowY = vertY_i > testY;
				// following statement checks if testPoint.Y is below Y-coord of i+1-th vertex
				bool belowHighY = vertY_j > testY;

				/* following statement is true if testPoint.Y satisfies either (only one is possible) 
                -->(i).Y < testPoint.Y < (i+1).Y        OR  
                -->(i).Y > testPoint.Y > (i+1).Y

                (Note)
                Both of the conditions indicate that a point is located within the edges of the Y-th coordinate
                of the (i)-th and the (i+1)- th vertices of the polygon. If neither of the above
                conditions is satisfied, then it is assured that a semi-infinite horizontal line draw 
                to the right from the testpoint will NOT cross the line that connects vertices i and i+1 
                of the polygon
                */
				bool withinYsEdges = belowLowY != belowHighY;

				if (withinYsEdges)
				{
					// this is the slope of the line that connects vertices i and i+1 of the polygon
					float slopeOfLine = (vertX_j - vertX_i) / (vertY_j - vertY_i);

					// this looks up the x-coord of a point lying on the above line, given its y-coord
					float pointOnLine = (slopeOfLine * (testY - vertY_i)) + vertX_i;

					//checks to see if x-coord of testPoint is smaller than the point on the line with the same y-coord
					bool isLeftToLine = testX < pointOnLine;

					if (isLeftToLine)
					{
						//this statement changes true to false (and vice-versa)
						locatedInPolygon = !locatedInPolygon;
					}//end if (isLeftToLine)
				}//end if (withinYsEdges
			}

			return locatedInPolygon;
		}

		/**************************************EXCEPCIONES*****************************************/
		#region Excepciones

		//felvir01


		public static DataTable p_cargar_excepciones(int estacion_id, string filtrar_analista, bool afiliacion, string usuario, string tabla, int codigoExcepcion, int validacion = 0, string montoFiltro = "", int agencia = 0)
		{
			string dato_analista = (filtrar_analista.Equals(string.Empty)) ? "'' analista, " : "ea.analista, ";
			string condicion_analista = (filtrar_analista.Equals(string.Empty)) ? string.Empty : $"and ea.CODIGO_EXCEPCION = e.CODIGO_EXCEPCION and ea.analista = '{filtrar_analista}' ";
			string tabla_asignaciones = (!filtrar_analista.Equals(string.Empty)) ? ", excp.DCS_EXCEPCIONES_ASIGNACIONES ea " : string.Empty;
			string filtro_estacion = "and e.estacion_id = :estacion_id ";

			DataTable dt = new DataTable();
			dt.Columns.Add("img_estado_registro");
			dt.Columns.Add("cod_excepcion_sol");
			dt.Columns.Add("no_solicitud");
			dt.Columns.Add("estado_excep");
			dt.Columns.Add("condicion_tu");
			dt.Columns.Add("fecha_presentacion");
			dt.Columns.Add("pago_mediante");
			dt.Columns.Add("oficial_servicio");
			dt.Columns.Add("codigo_cliente");
			dt.Columns.Add("Monto_solicitado");
			dt.Columns.Add("nombre_cliente");
			dt.Columns.Add("nombre_agencia");
			dt.Columns.Add("analista");
			dt.Columns.Add("saldo_bruto_com");
			dt.Columns.Add("ingreso_neto");
			dt.Columns.Add("ingreso_neto_desp");
			dt.Columns.Add("estacion_actual");
			dt.Columns.Add("leido");
			dt.Columns.Add("no_movimiento");
			dt.Columns.Add("abierta");

			try
			{
				p_obtener_string_conexion_oracle(out string_db_oracle);

				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}
				string sql = string.Empty;

				//Si es distinto de validacion
				if (estacion_id != validacion)
				{

					sql = @"select e.CODIGO_EXCEPCION cod_excepcion_sol,
							e.no_solicitud, 
							estad.DESCRIPCION estado_excep,
							e.condicion_tu, 
							e.fecha_presentacion, 
							case when e.pago_mediante = 'V' then 'Pago por Ventanilla' else 'Pago por Planilla' end as pago_mediante, 
							e.OFICIAL_SERVICIOS oficial_servicio, 
							s.codigo_cliente,  
							s.Monto_solicitado, 
							initcap(trim(c.nombres)) || ' ' || initcap(Trim(c.primer_apellido)) || ' ' || initcap(Trim(c.segundo_apellido)) nombre_cliente, 
							a.nombre_agencia,  
							m.analista,
							e.saldo_bruto_com,  
							e.INGRESO_NETO_TU ingreso_neto, 
							e.ingreso_neto_desp,  
							es.NOMBRE estacion_actual, 
							m.leido,  
							m.no_movimiento, 
							e.abierta 
							from excp.dcs_excepcion_solicitud e, wfc.dcs_solicitudes s, mgi.mgi_clientes c, mgi.mgi_agencias a, 
							wfc.dcs_wf_estaciones es, excp.dcs_movimientos_excep m, excp.dcs_exc_estados_excepcion estad "
							+ tabla
							+ @"where e.NO_SOLICITUD = s.NO_SOLICITUD 
							and s.CODIGO_CLIENTE = c.CODIGO_CLIENTE 
							and e.CODIGO_AGENCIA_ORIGEN = a.CODIGO_AGENCIA 
							and es.ESTACION_ID = e.ESTACION_ID 
							and m.CODIGO_EXCEPCION = e.CODIGO_EXCEPCION 
							and m.ESTACION_VIGENTE = 'S' 
							and estad.ESTADO_EXCEP_ID = e.CODIGO_ESTADO_EXCEP "
							+ condicion_analista
							+ filtro_estacion
							+ usuario
							//+ " and e.codigo_estado_excep in (1,5) "
							+ @" and e.abierta = 'S' "
							+ "and e.codigo_estado_excep = " + codigoExcepcion
							+ @" union 
							select e.CODIGO_EXCEPCION cod_excepcion_sol,
							e.no_solicitud, 
							estad.DESCRIPCION estado_excep,
							e.condicion_tu, 
							e.fecha_presentacion, 
							case when e.pago_mediante = 'V' then 'Pago por Ventanilla' else 'Pago por Planilla' end as pago_mediante, 
							e.OFICIAL_SERVICIOS oficial_servicio, 
							s.codigo_cliente,  
							s.Monto_solicitado, 
							initcap(trim(c.nombres)) || ' ' || initcap(Trim(c.primer_apellido)) || ' ' || initcap(Trim(c.segundo_apellido)) nombre_cliente, 
							a.nombre_agencia,  
							m.analista,
							e.saldo_bruto_com,  
							e.INGRESO_NETO_TU ingreso_neto, 
							e.ingreso_neto_desp,  
							es.NOMBRE estacion_actual, 
							m.leido,  
							m.no_movimiento, 
							e.abierta 
							from excp.dcs_excepcion_solicitud e, wfc.dcs_solicitudes s, mgi.mgi_clientes c, mgi.mgi_agencias a, 
							wfc.dcs_wf_estaciones es, excp.dcs_movimientos_excep m, excp.dcs_exc_estados_excepcion estad "
							+ tabla
							+ "where e.NO_SOLICITUD = s.NO_SOLICITUD "
							+ "and s.CODIGO_CLIENTE = c.CODIGO_CLIENTE "
							+ "and e.CODIGO_AGENCIA_ORIGEN = a.CODIGO_AGENCIA "
							+ "and es.ESTACION_ID = e.ESTACION_ID "
							+ "and m.CODIGO_EXCEPCION = e.CODIGO_EXCEPCION "
							+ "and m.ESTACION_VIGENTE = 'S' "
							+ "and estad.ESTADO_EXCEP_ID = e.CODIGO_ESTADO_EXCEP "
							+ condicion_analista
							+ filtro_estacion
							+ usuario
							+ " and e.codigo_estado_excep = " + codigoExcepcion
							//+ " and e.codigo_estado_excep in (2,4,3) "
							+ " and e.abierta = 'S' " //En prueba 2019-05-17 comentar las siguientes dos líneas							
													  /*+ "and trunc(sysdate) - to_date(e.fecha_cierre, 'dd/mm/yy') <= 5 " 
													  + "and trunc(sysdate) - to_date(e.fecha_cierre, 'dd/mm/yy') >= 0 "*/
						   + "order by 1 desc ";
				}
				else
				{
					sql = @"select e.CODIGO_EXCEPCION cod_excepcion_sol,
							e.no_solicitud, 
							estad.DESCRIPCION estado_excep,
							e.condicion_tu, 
							e.fecha_presentacion, 
							case when e.pago_mediante = 'V' then 'Pago por Ventanilla' else 'Pago por Planilla' end as pago_mediante, 
							e.OFICIAL_SERVICIOS oficial_servicio, 
							s.codigo_cliente,  
							s.Monto_solicitado, 
							initcap(trim(c.nombres)) || ' ' || initcap(Trim(c.primer_apellido)) || ' ' || initcap(Trim(c.segundo_apellido)) nombre_cliente, 
							a.nombre_agencia,  
							m.analista,
							e.saldo_bruto_com,  
							e.INGRESO_NETO_TU ingreso_neto, 
							e.ingreso_neto_desp,  
							es.NOMBRE estacion_actual, 
							m.leido,  
							m.no_movimiento, 
							e.abierta  
							from excp.dcs_excepcion_solicitud e, 
							wfc.dcs_solicitudes s, 
							mgi.mgi_clientes c, 
							mgi.mgi_agencias a, 
							wfc.dcs_wf_estaciones es, 
							excp.dcs_movimientos_excep m, 
							excp.dcs_exc_estados_excepcion estad 
							where e.NO_SOLICITUD = s.NO_SOLICITUD  
							and s.CODIGO_CLIENTE = c.CODIGO_CLIENTE  
							and e.CODIGO_AGENCIA_ORIGEN = a.CODIGO_AGENCIA  
							and es.ESTACION_ID = e.ESTACION_ID  
							and m.CODIGO_EXCEPCION = e.CODIGO_EXCEPCION  
							and m.ESTACION_VIGENTE = 'S'  
							and estad.ESTADO_EXCEP_ID = e.CODIGO_ESTADO_EXCEP  							
							and e.ESTACION_ID  = :estacion_id 
							and e.abierta = 'S' 
							and s.monto_solicitado " + montoFiltro
							+ " order by m.fecha_envio desc";
				}

				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;

				//if (!afiliacion)
				//{
				OracleParameter pa_estacion_id = new OracleParameter("estacion_id", OracleType.Number);
				pa_estacion_id.Direction = ParameterDirection.Input;
				pa_estacion_id.Value = estacion_id;
				cmd.Parameters.Add(pa_estacion_id);
				//}

				OracleDataReader dr = cmd.ExecuteReader();

				while (dr.Read())
				{
					dt.Rows.Add(
						null,
						dr["cod_excepcion_sol"].ToString(),
						dr["no_solicitud"].ToString(),
						dr["estado_excep"].ToString(),
						dr["condicion_tu"].ToString(),
						dr["fecha_presentacion"].ToString(),
						dr["pago_mediante"].ToString(),
						dr["oficial_servicio"].ToString(),
						dr["codigo_cliente"].ToString(),
						dr["Monto_solicitado"].ToString(),
						dr["nombre_cliente"].ToString(),
						dr["nombre_agencia"].ToString(),
						dr["analista"].ToString(),
						dr["saldo_bruto_com"].ToString(),
						dr["ingreso_neto"].ToString(),
						dr["ingreso_neto_desp"].ToString(),
						dr["estacion_actual"].ToString(),
						dr["leido"].ToString(),
						dr["no_movimiento"].ToString(),
						dr["abierta"].ToString()
						);
				}

				dr.Close();
				cmd.Dispose();
				DocSys.connOracle.Close();

			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		/// <summary>
		/// Verifica si tiene permiso de ver las excepciones
		/// </summary>
		/// <param name="estacion_id">Estación presente</param>
		/// <returns>Retorna verdadero si tiene permiso</returns>
		public static string puede_ver_excepciones(int estacion_id)
		{
			try
			{
				bool puede_ver = true;
				string sql = "select nvl(count(*),0) puede_ver from excp.DCS_V_ESTACIONES where estacion_id = :estacion_id ";
				p_obtener_string_conexion_oracle(out string_db_oracle);

				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;

				OracleParameter pa_estacion_id = new OracleParameter("estacion_id", OracleType.Number);
				pa_estacion_id.Direction = ParameterDirection.Input;
				pa_estacion_id.Value = estacion_id;
				cmd.Parameters.Add(pa_estacion_id);

				OracleDataReader dr = cmd.ExecuteReader();
				dr.Read();

				if (dr.HasRows)
				{
					int total = Convert.ToInt32(dr["puede_ver"].ToString());

					if (total > 0)
					{
						puede_ver = true;
					}
					else
					{
						puede_ver = false;
					}
				}

				dr.Close();
				if (DocSys.connOracle.State == ConnectionState.Open)
				{
					DocSys.connOracle.Close();
				}

				if (puede_ver)
				{
					return string.Empty;
				}

				return "No tiene acceso a las excepciones";
			}
			catch (Exception ex)
			{
				return $"{ex.TargetSite} - {ex.InnerException}: {ex.Message}";
			}
		}

		public static int get_estacion_excepcion(int codExcepcion)
		{
			try
			{
				int estacionExcep = 0;

				string sql = "select estacion_id from excp.dcs_excepcion_solicitud where codigo_excepcion = :codigo_excepcion";
				p_obtener_string_conexion_oracle(out string_db_oracle);

				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;

				OracleParameter pa_codigo_excepcion = new OracleParameter("codigo_excepcion", OracleType.Number);
				pa_codigo_excepcion.Direction = ParameterDirection.Input;
				pa_codigo_excepcion.Value = codExcepcion;
				cmd.Parameters.Add(pa_codigo_excepcion);

				OracleDataReader dr = cmd.ExecuteReader();
				dr.Read();

				if (dr.HasRows)
				{
					estacionExcep = Convert.ToInt32(dr["estacion_id"].ToString());
				}

				dr.Close();

				return estacionExcep;
			}
			catch (Exception)
			{
				return 0;
			}
		}

		public static int get_paso_actual(int codExcepcion)
		{
			try
			{
				int paso_actual = 0;

				string sql = "select nvl(paso_actual,1) paso_actual from excp.dcs_excepcion_solicitud where codigo_excepcion = :codigo_excepcion ";
				p_obtener_string_conexion_oracle(out string_db_oracle);

				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;

				OracleParameter pa_codigo_excepcion = new OracleParameter("codigo_excepcion", OracleType.Number);
				pa_codigo_excepcion.Direction = ParameterDirection.Input;
				pa_codigo_excepcion.Value = codExcepcion;
				cmd.Parameters.Add(pa_codigo_excepcion);

				OracleDataReader dr = cmd.ExecuteReader();
				dr.Read();

				if (dr.HasRows)
				{
					paso_actual = Convert.ToInt32(dr["paso_actual"].ToString());
				}

				dr.Close();

				return paso_actual;
			}
			catch (Exception)
			{
				return -1;
			}
		}

		/// <summary>
		/// Retorna un valor indicando si el flujo continua (felvir01)
		/// </summary>
		/// <param name="paso_actual">Paso siguiente</param>
		/// <returns>El total de los pasos que le siguen</returns>
		public static int get_cantidad_respuestas_paso(int paso_actual)
		{
			try
			{
				int cantidad_pasos = 0;

				string sql = "select count(*) cant_respuestas from excp.dcs_exc_flujos where paso=:pa_paso and decision_id>0 ";
				p_obtener_string_conexion_oracle(out string_db_oracle);

				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;

				OracleParameter pa_paso = new OracleParameter("pa_paso", OracleType.Number);
				pa_paso.Direction = ParameterDirection.Input;
				pa_paso.Value = paso_actual;
				cmd.Parameters.Add(pa_paso);

				OracleDataReader dr = cmd.ExecuteReader();
				dr.Read();

				if (dr.HasRows)
				{
					paso_actual = Convert.ToInt32(dr["cant_respuestas"].ToString());
				}

				dr.Close();

				return cantidad_pasos;
			}
			catch (Exception)
			{
				return -1;
			}
		}

		public static DataTable generales_movimientos(int codigo_excepcion)
		{
			try
			{
				DataTable dt = new DataTable();

				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception($"Error al cargar la información general de los movimientos {ex.Message}");
			}
		}

		public static DateTime p_obtener_fecha_server()
		{

			string vl_sql = @"select sysdate as hoy from dual";
			if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
			{
				DocSys.connOracle.Open();
			}
			OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
			cmd2.CommandType = CommandType.Text;
			OracleDataReader dr = cmd2.ExecuteReader();
			dr.Read();
			DateTime fecha = DateTime.Parse(dr["hoy"].ToString());
			dr.Close();
			return fecha;
		}

		public static int total_Excepciones(int no_solicitud)
		{
			try
			{
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				int total = 0;

				string sql = "select nvl(count(*),0) total from excp.dcs_excepcion_solicitud where no_solicitud = :no_solicitud ";
				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;

				OracleParameter pa_noSolicitud = new OracleParameter("no_solicitud", OracleType.Number);
				pa_noSolicitud.Direction = ParameterDirection.Input;
				pa_noSolicitud.Value = no_solicitud;
				cmd.Parameters.Add(pa_noSolicitud);

				OracleDataReader dr = cmd.ExecuteReader();
				dr.Read();

				if (dr.HasRows)
				{
					total = Convert.ToInt32(dr["total"].ToString());
				}

				dr.Close();

				return total;
			}
			catch (Exception ex)
			{
				return -1;
			}
		}

		public static DataTable Obtener_estado_excepcion(int codigo_excepcion)
		{
			try
			{
				string sql = "select es.ESTADO_EXCEP_ID, es.DESCRIPCION "
							+ "from excp.dcs_excepcion_solicitud ex, "
							+ "excp.dcs_exc_estados_excepcion es "
							+ "where ex.CODIGO_ESTADO_EXCEP = es.ESTADO_EXCEP_ID "
							+ "and ex.CODIGO_EXCEPCION = :codigoExcepcion ";

				DataTable dt = new DataTable();

				OracleParameter pa_codigo_Excepcion = new OracleParameter("codigoExcepcion", OracleType.Number);
				pa_codigo_Excepcion.Direction = ParameterDirection.Input;
				pa_codigo_Excepcion.Value = codigo_excepcion;

				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.Add(pa_codigo_Excepcion);

				OracleDataAdapter da = new OracleDataAdapter(cmd);

				da.Fill(dt);
				da.Dispose();
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception($"Ha ocurrido un error {ex.Message}", ex.InnerException);
			}
		}

		public static bool anular_excepcion_permitido(int codigo_excepcion)
		{
			try
			{
				bool anular = false;

				string sql = "select codigo_estado_excep from excp.dcs_excepcion_solicitud "
							+ "where codigo_excepcion = :pa_codigExcepcion";


				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				OracleParameter pa_codigo_excepcion = new OracleParameter("pa_codigExcepcion", OracleType.Number);
				pa_codigo_excepcion.Direction = ParameterDirection.Input;
				pa_codigo_excepcion.Value = codigo_excepcion;

				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.Add(pa_codigo_excepcion);

				OracleDataReader dr = cmd.ExecuteReader();
				dr.Read();

				if (dr.HasRows)
				{
					int estadoExcep = Convert.ToInt32(dr["codigo_estado_excep"].ToString());
					anular = (estadoExcep == 2 || estadoExcep == 3) ? true : false;
				}

				dr.Close();

				return anular;
			}
			catch (Exception ex)
			{
				throw new Exception($"{ex.TargetSite} - Mensaje: {ex.Message}", ex.InnerException);
			}
		}

		public static int total_Excepciones_permitidas()
		{
			try
			{

				string sql = "select valor from wfc.dcs_wf_parametros where parametro = 'WFC-0027' ";
				int permitidas = 0;
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;

				OracleDataReader dr = cmd.ExecuteReader();
				dr.Read();

				if (dr.HasRows)
				{
					permitidas = Convert.ToInt32(dr["valor"].ToString());
				}

				dr.Close();

				return permitidas;
			}
			catch (Exception ex)
			{
				throw new Exception($"{ex.TargetSite} : {ex.Message}", ex.InnerException);
			}
		}

		public static DataTable acciones_decision(int decision_id)
		{
			try
			{
				string sql = "select APROBAR_EXCEP, DENEGAR_EXCEP, MODIF_EXCEP, ANULAR from excp.dcs_exc_tipo_decisiones where decision_id = :decision_id ";
				OracleParameter pa_decision_id = new OracleParameter("decision_id", OracleType.Number);
				pa_decision_id.Direction = ParameterDirection.Input;
				pa_decision_id.Value = decision_id;

				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.Add(pa_decision_id);

				OracleDataAdapter da = new OracleDataAdapter(cmd);
				DataTable dt = new DataTable();

				da.Fill(dt);

				return dt;

			}
			catch (Exception ex)
			{
				throw new Exception($"{ex.TargetSite} - {ex.Message}", ex.InnerException);
			}
		}

		public static DataTable get_datos_resolucion(int codigoExcepcion)
		{
			try
			{
				string sql = "select fecha_cierre, usuario_resolucion, fecha_resolucion "
							+ "from excp.dcs_excepcion_solicitud where codigo_Excepcion = :codExcep";

				OracleParameter pa_codExcep = new OracleParameter("codExcep", OracleType.Number);
				pa_codExcep.Direction = ParameterDirection.Input;
				pa_codExcep.Value = codigoExcepcion;

				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.Add(pa_codExcep);

				OracleDataAdapter da = new OracleDataAdapter(cmd);
				DataTable detalleResolucion = new DataTable();
				da.Fill(detalleResolucion);

				return detalleResolucion;
			}
			catch (Exception ex)
			{
				throw new Exception($"{ex.TargetSite} - {ex.Message}", ex.InnerException);
			}
		}

		public static int get_excepciones_Activas(int no_solicitud)
		{
			try
			{
				string sql = @"select (select nvl(count(*),0) 
                                         from excp.dcs_excepcion_solicitud 
							             where NO_SOLICITUD = :noSolicitud 
							               and CODIGO_ESTADO_EXCEP = 1) total_activas, 
							          (select nvl(count(*), 0) 
                                         from excp.dcs_excepcion_solicitud 
							             where NO_SOLICITUD = :noSolicitud 
							               and CODIGO_ESTADO_EXCEP = 5) total_devueltas from dual ";


				OracleParameter pa_codExcep = new OracleParameter("noSolicitud", OracleType.Number);
				pa_codExcep.Direction = ParameterDirection.Input;
				pa_codExcep.Value = no_solicitud;

				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.Add(pa_codExcep);

				OracleDataAdapter da = new OracleDataAdapter(cmd);
				DataTable detalleResolucion = new DataTable();
				da.Fill(detalleResolucion);

				int total_Activas = Convert.ToInt32(detalleResolucion.Rows[0]["total_activas"].ToString());
				int total_devueltas = Convert.ToInt32(detalleResolucion.Rows[0]["total_devueltas"].ToString());

				return total_Activas + total_devueltas;
			}
			catch (Exception ex)
			{
				throw new Exception($"{ex.TargetSite} - {ex.Message}", ex.InnerException);
			}
		}

		public static int get_Estado_excepciones(int no_solicitud)
		{
			try
			{
				string sql = "select nvl(count(*),0) total from excp.dcs_excepcion_solicitud "
							+ "where CODIGO_ESTADO_EXCEP >= 2 and CODIGO_ESTADO_EXCEP <= 3 "
							+ "and no_solicitud = :noSolicitud ";

				OracleParameter pa_codExcep = new OracleParameter("noSolicitud", OracleType.Number);
				pa_codExcep.Direction = ParameterDirection.Input;
				pa_codExcep.Value = no_solicitud;

				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.Add(pa_codExcep);

				OracleDataAdapter da = new OracleDataAdapter(cmd);
				DataTable detalleResolucion = new DataTable();
				da.Fill(detalleResolucion);

				int total_Activas = Convert.ToInt32(detalleResolucion.Rows[0]["total"].ToString());

				return total_Activas;
			}
			catch (Exception ex)
			{
				throw new Exception($"{ex.TargetSite} - {ex.Message}", ex.InnerException);
			}
		}

		public static generalesExcepcion p_obtener_datos_n_excepcion(int _noSolicitud, bool existe = false)
		{
			try
			{
				string sql = @"select s.codigo_cliente,
					  s.monto_solicitado,
					  s.codigo_agencia_origen,
					  a.nombre_agencia,
					  initcap(trim(c.nombres))||' '||initcap(Trim(c.primer_apellido))||' '||initcap(Trim(c.segundo_apellido)) nombre_cliente,
					  s2.patrono lugar_de_trabajo, 
					  s2.cargo cargo_que_ocupa,
					  s2.FEC_INGRESO_SOL fecha_ingreso_laboral,
					  initcap(sa.desc_sub_aplicacion) desc_sub_aplicacion,
					  d.descripcion_destino, 
					  nvl(cta.saldo, 0) saldo, 
					  (s2.ingresos + s2.otros_ingresos) saldo_bruto, 
					  nvl(s2.EDAD_PRESTA ,0) edad, 
					  s2.PLAZO  
					  from wfc.dcs_solicitudes s,mgi.mgi_clientes c,mgi_sub_aplicaciones sa,wfc.dcs_wf_destinos_credito d,
					  wfc.dcs_solicitudes2 s2,mgi.mgi_agencias a,mca_cuentas cta 
					  where s.codigo_cliente=c.codigo_cliente 
					  and s.codigo_sub_aplicacion=sa.codigo_sub_aplicacion 
					  and s.no_solicitud=s2.no_solicitud 
					  and s2.destino=d.destino_id 
					  and a.codigo_agencia=s.codigo_agencia_origen 
					  and cta.codigo_cliente=s.codigo_cliente 
					  and cta.codigo_sub_aplicacion=101 
					  and NVL(cta.cancelada_b,'N')='N'
					  and s.no_solicitud=:noSolicitud ";

				if (existe)
				{
					sql = @"select s.codigo_cliente,
							s.monto_solicitado,
							s.codigo_agencia_origen,
							a.nombre_agencia,
							initcap(trim(c.nombres))||' '||initcap(Trim(c.primer_apellido))||' '||initcap(Trim(c.segundo_apellido)) nombre_cliente,
							s2.patrono lugar_de_trabajo, 
							s2.cargo cargo_que_ocupa,
							s2.FEC_INGRESO_SOL fecha_ingreso_laboral,
							initcap(sa.desc_sub_aplicacion) desc_sub_aplicacion,
							d.descripcion_destino, 
							nvl(cta.saldo, 0) saldo, 
							ex.SALDO_BRUTO_COM saldo_bruto,
							ex.pago_mediante,
							ex.INGRESO_NETO_DESP, 
							ex.INGRESO_NETO_TU, 
							ex.CONDICION_TU, 
							nvl(ex.EDAD_SOLICITANTE, 0) edad, 
							nvl(ex.PRESTACIONES,0)PRESTACIONES,
							s2.PLAZO  
							from wfc.dcs_solicitudes s,mgi.mgi_clientes c,mgi_sub_aplicaciones sa,wfc.dcs_wf_destinos_credito d,
							wfc.dcs_solicitudes2 s2,mgi.mgi_agencias a,mca_cuentas cta, dcs_excepcion_solicitud ex  
							where s.codigo_cliente=c.codigo_cliente  
							and s.codigo_sub_aplicacion=sa.codigo_sub_aplicacion  
							and s.no_solicitud=s2.no_solicitud  
							and s2.destino=d.destino_id  
							and a.codigo_agencia=s.codigo_agencia_origen  
							and cta.codigo_cliente=s.codigo_cliente  
							and cta.codigo_sub_aplicacion=101  
							and NVL(cta.cancelada_b,'N')='N' 
							and s.no_solicitud= ex.no_solicitud 
							and ex.codigo_excepcion = :noSolicitud ";
				}

				p_obtener_string_conexion_oracle(out string_db_oracle);

				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;

				OracleParameter pa_noSolicitud = new OracleParameter("noSolicitud", OracleType.Int32);
				cmd.Parameters.Add(pa_noSolicitud);
				pa_noSolicitud.Direction = ParameterDirection.Input;
				pa_noSolicitud.Value = _noSolicitud;

				OracleDataReader dr = cmd.ExecuteReader();
				generalesExcepcion excp = new generalesExcepcion();

				dr.Read();
				if (dr.HasRows)
				{
					excp.cargo_que_ocupa = dr["cargo_que_ocupa"].ToString();
					excp.codigo_agencia_origen = Convert.ToInt32(dr["codigo_agencia_origen"].ToString());
					excp.codigo_cliente = Convert.ToInt32(dr["codigo_cliente"].ToString());
					excp.descripcion_destino = dr["descripcion_destino"].ToString();
					excp.desc_sub_aplicacion = dr["desc_sub_aplicacion"].ToString();
					var fecha = dr["fecha_ingreso_laboral"].ToString();
					excp.fecha_ingreso_laboral = Convert.ToDateTime(dr["fecha_ingreso_laboral"].ToString());
					excp.lugar_de_trabajo = dr["lugar_de_trabajo"].ToString();
					excp.monto_solicitado = Convert.ToDecimal(dr["monto_solicitado"].ToString());
					excp.nombre_agencia = dr["nombre_agencia"].ToString();
					excp.nombre_cliente = dr["nombre_cliente"].ToString();
					excp.saldo = Convert.ToDecimal(dr["saldo"].ToString());
					excp.sueldo_bruto = Convert.ToDecimal(dr["saldo_bruto"].ToString());
					excp.Edad = Convert.ToInt32(dr["edad"].ToString());
					excp.Plazo = Convert.ToInt32(dr["PLAZO"].ToString());

					//Si es información para modificar o consulta
					if (existe)
					{
						excp.condicionCS = dr["CONDICION_TU"].ToString();
						excp.ingresoNeto = Convert.ToDecimal(dr["INGRESO_NETO_TU"].ToString());
						excp.ingresoNetoD = Convert.ToDecimal(dr["INGRESO_NETO_DESP"].ToString());
						excp.pago_mediante = dr["pago_mediante"].ToString();
						excp.Prestaciones = Convert.ToDecimal(dr["PRESTACIONES"].ToString());
					}
				}
				dr.Close();

				return excp;

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <summary>
		/// Parametrización por zona
		/// </summary>
		/// <param name="noFilial">Codigo de filial</param>
		/// <returns></returns>
		public static int excepciones_region(int noFilial)
		{
			try
			{
				//cambio
				string sql = "select codigo_zona from cef_agencias_zona where codigo_agencia = :codAgencia";
				OracleParameter pa_noFilial = new OracleParameter("codAgencia", OracleType.Number);
				pa_noFilial.Direction = ParameterDirection.Input;
				pa_noFilial.Value = noFilial;

				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.Add(pa_noFilial);

				OracleDataReader dr = cmd.ExecuteReader();
				int codigoZona = 0;
				dr.Read();
				if (dr.HasRows)
				{
					codigoZona = Convert.ToInt32(dr["codigo_zona"].ToString());
				}

				return codigoZona;
			}
			catch (Exception ex)
			{
				return ex.HResult;
			}
		}

		#endregion
	}

	public class Coordinate
	{
		public float X { get; set; }
		public float Y { get; set; }

	}

	/********************************Excepciones*****************************/
	public class generalesExcepcion
	{
		public int codigo_cliente { get; set; }
		public decimal monto_solicitado { get; set; }
		public int codigo_agencia_origen { get; set; }
		public string nombre_agencia { get; set; }
		public string nombre_cliente { get; set; }
		public string lugar_de_trabajo { get; set; }
		public string cargo_que_ocupa { get; set; }
		public DateTime fecha_ingreso_laboral { get; set; }
		public string desc_sub_aplicacion { get; set; }
		public string descripcion_destino { get; set; }
		public decimal saldo { get; set; }
		public string pago_mediante { get; set; }
		public decimal sueldo_bruto { get; set; }
		public string condicionCS { get; set; }
		public decimal ingresoNeto { get; set; }
		public decimal ingresoNetoD { get; set; }
		public int Edad { get; set; }
		public decimal Prestaciones { get; set; }
		public int Plazo { get; set; }
	}
}
