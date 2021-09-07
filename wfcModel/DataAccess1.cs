using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

using System.Globalization;
using System.IO;
using wfcModel.MGI.SubAplicaciones;

namespace wfcModel
{
	public class DataAccess
	{
		public string cadenaConexionOracle = "";
		public string gusuario = "";
		public string gpassword = "";
		public string gtnsnames = "";

		public DataAccess(string p_usuario, string p_password, string p_tnsnames)
		{
			string gusuario = p_usuario;
			string gpassword = p_password;
			string gtnsnames = p_tnsnames;
			cadenaConexionOracle = "DATA SOURCE=" + gtnsnames + ";PERSIST SECURITY INFO=True;USER ID=" + gusuario + "; PASSWORD=" + gpassword;
		}

		public DataTable Llenar_lista_entradas(Int32 p_estacion_id_to, Int32 p_agencia_origen, string p_toda_filial, string p_gte_filial, string p_condi_analista, string p_texto_busqueda)
		{
			string vl_condi_toda_filial = p_toda_filial;
			string vl_condi_gte_filial = p_gte_filial;
			string vl_condi_analista = p_condi_analista;
			string vl_texto_busqueda = p_texto_busqueda;
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select s.no_solicitud,
                                          edos.DESCRIPCION estado_solicitud,
                                          s2.resultado_buro,
                                          initcap(desc_sub_aplicacion) desc_sub_aplicacion,
                                          to_char(fecha_presentacion,'dd/mm/yyyy hh24:mi:ss') fecha_presentacion,                                          
                                          initcap(nombre_agencia) nombre_agencia,
                                          oficial_servicio,
                                          ms.analista,
                                          s.codigo_cliente,
                                          f.descripcion_fuente,
                                          initcap(trim(nombres))||' '||initcap(trim(primer_apellido))||' '||initcap(Trim(segundo_apellido)) nombre_cliente,
                                          m.desc_moneda,
                                          s.monto_solicitado,
                                          meses_plazo,
                                          ms.attr_nueva,
                                          leido,
                                          leido_por,
                                          fecha_leido,
                                          no_movimiento,
                                          con_adjunto,
                                          e.nombre estacion_actual,
                                          s.banderin_id,
                                          score_crediticio
                                     from dcs_solicitudes s,
                                          dcs_solicitudes2 s2,
                                          mgi_sub_aplicaciones sa,
                                          mgi_monedas m,
                                          mgi_agencias a,
                                          mgi_clientes c,
                                          dcs_wf_estado_solicitudes edos,
                                          dcs_movimientos_solicitudes ms,
                                          dcs_wf_fuentes_financiamiento f,
                                          dcs_wf_estaciones e
                                    where s.no_solicitud=s2.no_solicitud
                                      and ms.no_solicitud=s.no_solicitud 
                                      and sa.codigo_empresa=1
                                      and s.codigo_sub_aplicacion=sa.codigo_sub_aplicacion
                                      and S.ESTADO_SOLICITUD_ID=EDOS.ESTADO_ID
                                      and s.codigo_fuente=f.codigo_fuente
                                      and s.codigo_agencia_origen=a.codigo_agencia
                                      and s.codigo_moneda=m.codigo_moneda
                                      and c.codigo_empresa=1
                                      and s.codigo_cliente=c.codigo_cliente
                                      and s.estacion_id=e.estacion_id
                                      and s.workflow_id=ms.workflow_id
                                      and s.estacion_id=ms.estacion_id_to
                                      and s.paso_actual=ms.paso
                                      and s.abierta='S'                                      
                                      and s.estacion_id=:pa_estacion_id_to
                                      and ms.estacion_vigente='S' " +
									  vl_condi_toda_filial +
									  vl_condi_gte_filial +
									  vl_condi_analista +
									  vl_texto_busqueda +
									  @" 
                                    Order by s.no_solicitud desc ";

					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_estacion_id_to", p_estacion_id_to);
						da.SelectCommand.Parameters.Add("pa_codigo_agencia_origen", p_agencia_origen);


						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//Listo
		public DataTable Llenar_lista_salidas(Int32 p_estacion_id_to, Int32 p_agencia_origen, string p_toda_filial, string p_gte_filial, string p_condi_analista)
		{
			string vl_condi_toda_filial = p_toda_filial;
			string vl_condi_gte_filial = p_gte_filial;
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select s.no_solicitud,
                                          EDOS.DESCRIPCION estado_solicitud,
                                          s2.resultado_buro,
                                          initcap(desc_sub_aplicacion) desc_sub_aplicacion,
                                          trunc(fecha_presentacion) fecha_presentacion,
                                          initcap(nombre_agencia) nombre_agencia,
                                          oficial_servicio,
                                          ms.analista,
                                          s.codigo_cliente,
                                          f.descripcion_fuente,
                                          initcap(nombres)||' '||initcap(primer_apellido)||' '||initcap(segundo_apellido) nombre_cliente,
                                          m.desc_moneda,
                                          s.monto_solicitado,
                                          meses_plazo,
                                          ms.attr_nueva,
                                          leido,
                                          leido_por,
                                          fecha_leido,
                                          no_movimiento,
                                          con_adjunto,
                                          e.nombre estacion_actual,
                                          s.banderin_id,
                                          score_crediticio                                      
                                     From dcs_solicitudes s,
                                          dcs_solicitudes2 s2,
                                          dcs_movimientos_solicitudes ms,
                                          dcs_wf_fuentes_financiamiento f,
                                          dcs_wf_estaciones e,    
                                          mgi_monedas m,
                                          mgi_agencias a,                                                                                
                                          mgi_sub_aplicaciones sa,
                                          mgi_clientes cl,
                                          dcs_wf_estado_solicitudes edos                                                  
                                    Where s.no_solicitud=s2.no_solicitud
                                      and s.codigo_fuente=f.codigo_fuente
                                      and s.codigo_moneda=m.codigo_moneda
                                      and s.codigo_agencia_origen=a.codigo_agencia
                                      and cl.codigo_empresa=1
                                      and s.codigo_cliente=cl.codigo_cliente
                                      and sa.codigo_empresa=1
                                      and s.codigo_sub_aplicacion=sa.codigo_sub_aplicacion
                                      and S.ESTADO_SOLICITUD_ID=EDOS.ESTADO_ID
                                      and s.no_solicitud=ms.no_solicitud                                                           
                                      and ms.estacion_id_to=e.estacion_id         
                                      and s.abierta='S'                                                                         
                                      and ms.estacion_id_from =:pa_estacion_id_to " +
									  vl_condi_toda_filial +
									  vl_condi_gte_filial + @"
                                  order by s.no_solicitud desc  ";

					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_estacion_id_to", p_estacion_id_to);
						da.SelectCommand.Parameters.Add("pa_codigo_agencia_origen", p_agencia_origen);


						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//Listo
		public DataTable Llenar_lista_rechazadas(Int32 p_estacion_id_to, Int32 p_agencia_origen, string p_toda_filial, string p_gte_filial, string p_condi_analista)
		{
			string vl_condi_toda_filial = p_toda_filial;
			string vl_condi_gte_filial = p_gte_filial;
			string vl_condi_analista = p_condi_analista;
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select s.no_solicitud,
                                          edos.DESCRIPCION estado_solicitud,
                                          s2.resultado_buro,
                                          initcap(desc_sub_aplicacion) desc_sub_aplicacion,
                                          to_char(fecha_presentacion,'dd/mm/yyyy hh24:mi:ss') fecha_presentacion,                                          
                                          initcap(nombre_agencia) nombre_agencia,
                                          oficial_servicio,
                                          ms.analista,
                                          s.codigo_cliente,
                                          f.descripcion_fuente,
                                          initcap(nombres)||' '||initcap(primer_apellido)||' '||initcap(segundo_apellido) nombre_cliente,
                                          m.desc_moneda,
                                          s.monto_solicitado,
                                          meses_plazo,
                                          ms.attr_nueva,
                                          leido,
                                          leido_por,
                                          fecha_leido,
                                          no_movimiento,
                                          con_adjunto,
                                          e.nombre estacion_actual,
                                          s.banderin_id,
                                          score_crediticio
                                     from dcs_solicitudes s,
                                          dcs_solicitudes2 s2,
                                          mgi_sub_aplicaciones sa,
                                          mgi_monedas m,
                                          mgi_agencias a,
                                          mgi_clientes c,
                                          dcs_wf_estado_solicitudes edos,
                                          dcs_movimientos_solicitudes ms,
                                          dcs_wf_fuentes_financiamiento f,
                                          dcs_wf_estaciones e
                                    where s.no_solicitud=s2.no_solicitud
                                      and ms.no_solicitud=s.no_solicitud 
                                      and sa.codigo_empresa=1
                                      and s.codigo_sub_aplicacion=sa.codigo_sub_aplicacion
                                      and S.ESTADO_SOLICITUD_ID=EDOS.ESTADO_ID
                                      and s.codigo_fuente=f.codigo_fuente
                                      and s.codigo_agencia_origen=a.codigo_agencia
                                      and s.codigo_moneda=m.codigo_moneda
                                      and c.codigo_empresa=1
                                      and s.codigo_cliente=c.codigo_cliente
                                      and s.estacion_id=e.estacion_id
                                      and s.workflow_id=ms.workflow_id
                                      and s.estacion_id=ms.estacion_id_to
                                      and s.paso_actual=ms.paso
                                      and s.abierta in ('S','N')
                                      and s.estacion_id=:pa_estacion_id_to
                                      and s.estado_solicitud_id in (4,6)
                                      and ms.estacion_vigente='S' " +
									  vl_condi_toda_filial +
									  vl_condi_gte_filial +
									  vl_condi_analista + @" 
                                    Order by s.no_solicitud desc ";

					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_estacion_id_to", p_estacion_id_to);
						da.SelectCommand.Parameters.Add("pa_codigo_agencia_origen", p_agencia_origen);


						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//Listo
		public DataTable Llenar_lista_por_carpetas(Int32 p_carpeta_id)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select s.no_solicitud,
                                          edos.DESCRIPCION estado_solicitud,
                                          s2.resultado_buro,
                                          initcap(desc_sub_aplicacion) desc_sub_aplicacion,
                                          to_char(fecha_presentacion,'dd/mm/yyyy hh24:mi:ss') fecha_presentacion,                                          
                                          initcap(nombre_agencia) nombre_agencia,
                                          oficial_servicio,
                                          ms.analista,
                                          s.codigo_cliente,
                                          f.descripcion_fuente,
                                          initcap(nombres)||' '||initcap(primer_apellido)||' '||initcap(segundo_apellido) nombre_cliente,
                                          m.desc_moneda,
                                          s.monto_solicitado,
                                          meses_plazo,
                                          ms.attr_nueva,
                                          leido,
                                          leido_por,
                                          fecha_leido,
                                          no_movimiento,
                                          con_adjunto,
                                          e.nombre estacion_actual,
                                          s.banderin_id,
                                          score_crediticio
                                     from dcs_solicitudes s,
                                          dcs_solicitudes2 s2,
                                          mgi_sub_aplicaciones sa,
                                          mgi_monedas m,
                                          mgi_agencias a,
                                          mgi_clientes c,
                                          dcs_wf_estado_solicitudes edos,
                                          dcs_movimientos_solicitudes ms,
                                          dcs_wf_fuentes_financiamiento f,
                                          dcs_wf_estaciones e,
                                          dcs_carpetas_solicitudes cs
                                    where s.no_solicitud=s2.no_solicitud
                                      and ms.no_solicitud=s.no_solicitud 
                                      and sa.codigo_empresa=1
                                      and s.codigo_sub_aplicacion=sa.codigo_sub_aplicacion
                                      and S.ESTADO_SOLICITUD_ID=EDOS.ESTADO_ID
                                      and s.codigo_fuente=f.codigo_fuente
                                      and s.codigo_agencia_origen=a.codigo_agencia
                                      and s.codigo_moneda=m.codigo_moneda
                                      and c.codigo_empresa=1
                                      and s.codigo_cliente=c.codigo_cliente
                                      and s.estacion_id=e.estacion_id
                                      and s.workflow_id=ms.workflow_id
                                      and s.estacion_id=ms.estacion_id_to
                                      and s.paso_actual=ms.paso
                                      --and s.abierta='S'                                                                           
                                      and s.no_solicitud=cs.no_solicitud
                                      and cs.carpeta_id=:pa_carpeta_id 
                                    Order by s.no_solicitud desc ";

					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_carpeta_id", p_carpeta_id);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//Listo
		public DataTable ObtenerDecisiones(Int32 p_no_solicitud)
		{
			var dt1 = ObtenerInfoSolicitud(p_no_solicitud);
			int vl_workflow_id = int.Parse(dt1.Rows[0]["workflow_id"].ToString());
			Int32 vl_paso = Int32.Parse(dt1.Rows[0]["paso_actual"].ToString());
			Int32 vl_estacion_actual = Int32.Parse(dt1.Rows[0]["estacion_id"].ToString());
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			dt.Columns.Add("decision_id");
			dt.Columns.Add("descripcion");
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select dcs_wf_decisiones.decision_id,dcs_wf_decisiones.descripcion 
                                      from dcs_wf_flujos f,dcs_wf_decisiones 
                                     Where f.decision_id=dcs_wf_decisiones.decision_id 
                                       and f.workflow_id=:pa_workflow_id
                                       and f.paso=:pa_paso
                                       and f.estacion_id_from=:pa_estacion_actual
                                       and f.flujo_activo_b='S'";

					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_workflow_id", vl_workflow_id);
					cmd.Parameters.Add("pa_paso", vl_paso);
					cmd.Parameters.Add("pa_estacion_actual", vl_estacion_actual);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						while (dr.Read())
						{
							dt.Rows.Add(dr["decision_id"],
										dr["descripcion"]);
						}
					}

					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return dt;


		}

		//Listo
		public DataTable ObtenerDecisionxId(Int32 p_decision_id)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select * from dcs_wf_decisiones where decision_id=:pa_decision_id";

					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_decision_id", p_decision_id);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//Listo
		public DataTable ObtenerDestinosxDecision(Int32 p_estacion_actual, Int32 p_paso_actual, int p_decision_id, int p_workflow_id)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select estacion_id_to,e1.nombre estacion_origen,
                                          e1.comite_resolutivo comite_resolutivo_origen,
                                          e2.nombre estacion_destino,tipo_respuesta,decision_id,
                                          e2.comite_resolutivo,e1.icono icono_origen,e2.icono icono_destino,
                                          e2.monto_minimo_resolucion,
                                          e2.monto_maximo_resolucion,
                                          e2.comite_resolutivo,
                                          e2.nivel_resolutivo_local,
                                          e2.nivel_resol_con_gtefilial,
                                          e2.resoluciones_requeridas,
                                          f.tipo_respuesta
                                     from dcs_wf_flujos f,
                                          dcs_wf_estaciones e1,
                                          dcs_wf_estaciones e2,
                                          dcs_comites c
                                    Where f.estacion_id_from=e1.estacion_id
                                      and f.estacion_id_to=e2.estacion_id
                                      and e2.estacion_id=c.comite_id(+)
                                      and f.estacion_id_from=:pa_estacion_actual
                                      and f.paso=:pa_paso_actual
                                      and f.decision_id=:pa_decision_id
                                      and f.workflow_id=:pa_workflow_id";

					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_estacion_actual", p_estacion_actual);
						da.SelectCommand.Parameters.Add("pa_paso_actual", p_paso_actual);
						da.SelectCommand.Parameters.Add("pa_decision_id", p_decision_id);
						da.SelectCommand.Parameters.Add("pa_workflow_id", p_workflow_id);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//Listo
		public DataTable ObtenerInfoSolicitud(Int32 p_no_solicitud)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select no_solicitud,workflow_id,codigo_sub_aplicacion,paso_actual,estacion_id,estado_solicitud_id,no_movimiento_actual,usuario_ing,codigo_cliente,codigo_agencia_origen,
                                          nvl(round((sysdate - fecha_presentacion)) + 1,0) dias_antiguedad,abierta
                                     from dcs_solicitudes where no_solicitud=:pa_no_solicitud";

					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_no_solicitud", p_no_solicitud);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//Listo
		public DataTable ObtenerInfoSolicitud2(Int32 p_no_solicitud)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select requiere_codeudor,requiere_aval1,requiere_aval2,plazo,tasa
                                     from dcs_solicitudes2 where no_solicitud=:pa_no_solicitud";

					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_no_solicitud", p_no_solicitud);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//Listo
		public DataTable ObtenerInfoSolicitud3(Int32 p_no_solicitud)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select s.no_solicitud,nombres||' '||primer_apellido nombre,desc_sub_aplicacion,s2.monto_solicitado,tasa,plazo,meses_plazo,no_movimiento_actual,banderin_id,estado_solicitud_id,
                                          oficial_servicio,codigo_agencia_origen,estacion_id,recalculos_permitidos,recalculos_actuales
                                 From dcs_solicitudes s,
                                      dcs_solicitudes2 s2,
                                      mgi_clientes c,
                                      mgi_sub_aplicaciones sa 
                                Where c.codigo_empresa=sa.codigo_empresa
                                  and c.codigo_empresa=1
                                  and s.no_solicitud=s2.no_solicitud 
                                  and s.codigo_cliente=c.codigo_cliente
                                  and s.codigo_sub_aplicacion=sa.codigo_sub_aplicacion 
                                  and s.no_solicitud=:pa_no_solicitud";

					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_no_solicitud", p_no_solicitud);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//Listo
		public DataTable ObtenerMiembrosxEstacion(Int32 p_estacion_id)
		{

			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select h.foto,
                                          h.codigo_cliente no_emple,
                                          nombres,
                                          primer_apellido,
                                          '' depto,
                                          '' descri_puesto,  
                                          codigo_usuario                                                                               
                                     From mgi_usuarios u,
                                          mrh_empleados_huellas h,
                                          dcs_wf_usuarios_estaciones ue
                                    Where u.codigo_empresa=1
                                      and u.codigo_cliente=to_number(h.codigo_cliente(+))
                                      and u.codigo_usuario=ue.usuario                  
                                      --and h.foto is not null
                                      and ue.estacion_id=:pa_estacion_id";

					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_estacion_id", p_estacion_id);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;

			#region sss
			/*DataTable dt = new DataTable();
        OracleCommand cmd = new OracleCommand();
        OracleDataReader rd;
        dt.Columns.Add("foto", System.Type.GetType("System.Byte[]")).AllowDBNull = true;
        dt.Columns.Add("no_emple");
        dt.Columns.Add("nombre");
        dt.Columns.Add("departamento");
        dt.Columns.Add("puesto");
        try
        {
            using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
            {
                string sql = @"Select h.foto,
                                       h.codigo_cliente no_emple,
                                       nombres,
                                       primer_apellido,
                                       '' depto,
                                       '' descri_puesto,  
                                       codigo_usuario                                                                               
                                 From mgi_usuarios u,
                                      mrh_empleados_huellas h,
                                      dcs_wf_usuarios_estaciones ue
                                Where u.codigo_cliente=h.codigo_cliente
                                  and u.codigo_usuario=ue.usuario                  
                                  and h.foto is not null
                                  and ue.estacion_id=:pa_estacion_id";
                cmd.CommandText = sql;
                cmd.Connection = ConexionOracle;
                cmd.Parameters.Add("pa_estacion_id", p_estacion_id);
                ConexionOracle.Open();

                rd = cmd.ExecuteReader();

                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (!DBNull.Value.Equals(rd["foto"]))
                        {
                            dt.Rows.Add((byte[])rd["foto"],
                                    rd["no_emple"],
                                    rd["nombres"],
                                    rd["depto"],
                                    rd["descri_puesto"]);
                        }
                        else
                        {
                            dt.Rows.Add(null,
                                    rd["no_emple"],
                                    rd["nombres"],
                                    rd["depto"],
                                    rd["descri_puesto"]);
                        }

                    }
                }

                rd.Close();
                cmd.Dispose();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error en :" + ex.TargetSite + " " + ex.Message);
        }*/
			#endregion



		}

		//Listo
		public DataTable ObtenerMiembrosxEstacion(Int32 p_estacion_id, string miembrolocal)
		{

			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select foto,a.codigo_usuario,a.codigo_cliente no_emple,nombres,primer_apellido,'' depto,'' descri_puesto
                                     From (Select codigo_cliente,codigo_usuario,nombres,primer_apellido 
                                             from mgi_usuarios
                                            Where codigo_cargo=7 --Gte de Filia
                                              and codigo_agencia=:pa_codigo_agencia
                                              and activo_b='S'
                                              and codigo_cliente>0 --para que no muestre esos usuario temporales con guiones(-)
                                            UNION
                                           Select u.codigo_cliente,usuario codigo_usuario,nombres,primer_apellido 
                                             from dcs_wf_miembros_alternos a,
                                                  mgi_usuarios u
                                            Where usuario=codigo_usuario
                                              and codigo_zona=(select codigo_zona from cef_agencias_zona where codigo_agencia=:pa_codigo_agencia )
                                              and codigo_agencia_acubrir=:pa_codigo_agencia
                                              and a.activo_b='S') a,
                                          mrh_empleados_huellas b
                                    where a.codigo_cliente=b.CODIGO_CLIENTE(+)";

					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_codigo_agencia", p_estacion_id);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;

		}

		//Listo
		public DataTable ObtenerEstacionesxUsuario(string p_usuario)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"SELECT ue.estacion_id,e.nombre 
                                     FROM DCS_wf_usuarios_estaciones ue,
                                          DCS_wf_estaciones e
                                    Where upper(usuario)=upper(:pa_usuario)
                                      and ue.estacion_id=e.estacion_id
                                      and ue.activo='S'";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_usuario", p_usuario);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//Listo
		public DataTable ObtenerFilialesxGerenteFilial(string p_usuario)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"SELECT distinct a.codigo_agencia,a.nombre_agencia 
                                     From mgi_usuarios u,
                                          mgi_agencias a 
                                    Where u.codigo_agencia=a.codigo_agencia
                                      and numero_identificacion=(select numero_identificacion from mgi_usuarios where codigo_usuario=:pa_usuario)
                                      and u.activo_b='S'";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_usuario", p_usuario);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//Listo
		public DataTable ObtenerFilialesxGerenteRegional(string p_usuario)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select ag.codigo_agencia,
                                          InitCap(nombre_agencia) nombre_agencia
                                     from cef_agencias_zona az, 
                                          dcs_wf_gerentes_regionales g,
                                          mgi_agencias  ag 
                                    Where g.codigo_region=AZ.CODIGO_ZONA 
                                      and az.codigo_agencia=ag.codigo_agencia
                                      and g.codigo_usuario=:pa_usuario
                                      and az.activo_b='S'
                                      and g.activo_b='S'
                                    Order by 1   ";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_usuario", p_usuario);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//Listo
		public DataTable ObtenerFiliales()
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"SELECT a.codigo_agencia,Initcap(a.nombre_agencia) nombre_agencia 
                                     From mgi_agencias a 
                                   Order by 1";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//Listo
		public DataTable ObtenerFiliales(string p_codigo_zona)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"SELECT a.codigo_agencia,Initcap(a.nombre_agencia) nombre_agencia 
                                     From mgi_agencias a,cef_agencias_zona b
                                    where a.CODIGO_AGENCIA=b.codigo_agencia 
                                      and b.codigo_zona=:pa_codigo_zona
                                    Order by 1";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_codigo_zona", p_codigo_zona);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//Listo
		public DataTable ObtenerRegionales()
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select * from cef_zonas order by codigo_zona";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//Listo
		public DataTable ObtenerFuentesFondos()
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select codigo_fuente,upper(descripcion_fuente) descripcion_fuente,'CSF' cod_fondo_mg
                                     from dcs_wf_fuentes_financiamiento a,
                                          cr_fondos_redescontados b
                                    Where a.COD_FONDO_MG=b.cod_fondo(+)
                                      and cod_fondo_mg is null
                                     UNION
                                    Select codigo_fuente,des_fondo descripcion_fuente,cod_fondo cod_fondo_mg
                                      from dcs_wf_fuentes_financiamiento a,
                                           cr_fondos_redescontados b
                                     Where a.COD_FONDO_MG=b.cod_fondo       
                                     Order by codigo_fuente";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//Listo
		public string ObtenerConversionFondoMG(string fuente_finaciamiento)
		{

			string vl_retorno = "";
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select nvl(cod_fondo_mg,'CSF') COD_FONDO_MG from dcs_wf_fuentes_financiamiento where codigo_fuente=:pa_codigo_fuente";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_codigo_fuente", fuente_finaciamiento);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = dr["COD_FONDO_MG"].ToString();

					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;

		}

		//Listo
		public DataTable ObtenerListaAgencias(string filtro)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			OracleDataReader rd;

			dt.Columns.Add("codigo_agencia");
			dt.Columns.Add("nombre_agencia");
			dt.Columns.Add("activo");

			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{

					string condi_filtro = "";
					if (filtro != string.Empty)
					{
						filtro = filtro.ToLower();
						condi_filtro = " Where lower(nombre_agencia) like '%" + filtro + "%' ";
					}
					string sql = @"select codigo_agencia,Initcap(nombre_agencia) nombre_agencia,activo_b 
                                     from mgi_agencias " + condi_filtro + @"
                                    order by codigo_agencia";

					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					ConexionOracle.Open();

					rd = cmd.ExecuteReader();

					if (rd.HasRows)
					{
						while (rd.Read())
						{
							dt.Rows.Add(rd["codigo_agencia"],
										rd["nombre_agencia"],
										rd["activo_b"]);
						}
					}

					rd.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en ObtenerListaAgencias " + ex.Message);
			}
			return dt;
		}

		//Listo
		public DataTable ObtenerDecisionesComitexSolicitud(Int32 p_no_solicitud)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select usuario_comite,nombres||' '||primer_apellido nombre,ap.fecha_creacion,comite_id,decision,ap.fecha_decision,ap.observaciones,pendiente_respuesta_b,no_movimiento_actual,
                                          to_number(to_char(ap.fecha_creacion, 'yyyymmddhh24miss')) llave
                                     from dcs_solicitudes_aprobaciones ap,
                                          mgi_usuarios u
                                    Where u.codigo_usuario=ap.usuario_comite       
                                      and ap.no_solicitud=:pa_no_solicitud";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_no_solicitud", p_no_solicitud);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//Listo
		public DataTable ObtenerDecisionesCommitexUsuarioxSolicitud(Int32 p_no_solicitud, string p_usuario_comite)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select decision,ap.fecha_decision
                                     from dcs_solicitudes_aprobaciones ap                                          
                                    Where ap.no_solicitud=:pa_no_solicitud
                                      and ap.usuario_comite=:pa_usuario_comite";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.AddWithValue("pa_no_solicitud", p_no_solicitud);
						da.SelectCommand.Parameters.AddWithValue("pa_usuario_comite", p_usuario_comite);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//Listo
		public DataTable ObtenerSituacionActualxSolicitud(Int32 p_no_solicitud)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select es.estado_id,estacion_id,attr_nueva,no_movimiento_actual,con_adjunto,plazo,tasa,
                                          monto_aprobado,plazo_aprobado,tasa_aprobada,
                                          no_acta_resolucion,
                                          ciudad_resolucion,
                                          s1.estado_solicitud_id,
                                          estacion_id,
                                          es.descripcion estado_solicitud,
                                          observaciones_analista,
                                          observaciones_prestamos,
                                          PERMITIR_MODIF_APROB,
                                          nvl(round((sysdate - fecha_presentacion)) + 1,0) dias_antiguedad,
                                          oficial_servicio,
                                          codigo_agencia_origen,         
                                          s1.codigo_cliente
                                     from dcs_solicitudes s1,
                                          dcs_solicitudes2 s2,
                                          dcs_wf_estado_solicitudes es 
                                    Where s1.no_solicitud=s2.no_solicitud
                                      and s1.estado_solicitud_id=es.estado_id
                                      and s1.no_solicitud=:pa_no_solicitud";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_no_solicitud", p_no_solicitud);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//Listo
		public DataTable ObtenerMontosAprobado(Int32 p_no_solicitud)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select monto_aprobado,tasa_aprobada,plazo_aprobado,no_acta_resolucion,ciudad_resolucion
                                     from dcs_solicitudes2 where no_solicitud=:pa_no_solicitud";

					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_no_solicitud", p_no_solicitud);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//Listo
		public DataTable P_Obtener_generales_empleado(string p_empleado)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			OracleDataReader rd;

			dt.Columns.Add("nombre");
			dt.Columns.Add("cedula");
			dt.Columns.Add("descri_depto");
			dt.Columns.Add("descri_puesto");
			dt.Columns.Add("fecha_ingreso");
			dt.Columns.Add("codigo_agencia");
			dt.Columns.Add("nombre_agencia");
			dt.Columns.Add("codigo_zona");
			dt.Columns.Add("nombre_zona");
			dt.Columns.Add("no_cia");
			dt.Columns.Add("no_emple");
			dt.Columns.Add("tipo_emp");
			dt.Columns.Add("condicion");
			dt.Columns.Add("tipo_cta");
			dt.Columns.Add("no_hora");
			dt.Columns.Add("area");
			dt.Columns.Add("depto");
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select nombre,RTRIM(LTRIM(REPLACE(CEDULA,'-'))) cedula,nombre_pila,Initcap(depto.descri) descri_depto, InitCap(pto.descri) descri_puesto,f_ingreso,u.codigo_agencia,a.nombre_agencia,z.codigo_zona,z.descripcion nombre_zona,
                                          e.no_cia,e.no_emple,e.tipo_emp,e.condicion,e.tipo_cta,e.no_hora,e.area,e.depto
                                     from arplme e,
                                          arplmp pto,
                                          arpldp depto,
                                          mgi_usuarios u,
                                          mgi_agencias a,
                                          cef_agencias_zona az,
                                          cef_zonas z   
                                    where e.no_emple=:pa_empleado
                                      and e.puesto=pto.puesto
                                      and e.depto=depto.depa
                                      and e.area=depto.area
                                      and e.no_emple=u.codigo_cliente(+)
                                      and u.codigo_agencia=AZ.CODIGO_AGENCIA(+)
                                      and u.codigo_agencia=a.codigo_agencia(+)
                                      and az.codigo_zona=Z.CODIGO_ZONA(+)       
                                      and e.NO_CIA=pto.NO_CIA";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_empleado", p_empleado);
					ConexionOracle.Open();

					rd = cmd.ExecuteReader();

					if (rd.HasRows)
					{
						while (rd.Read())
						{
							dt.Rows.Add(rd["nombre"],
										rd["cedula"],
										rd["descri_depto"],
										rd["descri_puesto"],
										rd["f_ingreso"],
										rd["codigo_agencia"],
										rd["nombre_agencia"],
										rd["codigo_zona"],
										rd["nombre_zona"],
										rd["no_cia"],
										rd["no_emple"],
										rd["tipo_emp"],
										rd["condicion"],
										rd["tipo_cta"],
										rd["no_hora"],
										rd["area"],
										rd["depto"]);
						}
					}

					rd.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en :" + ex.TargetSite + " " + ex.Message);
			}

			return dt;
		}

		//Listo
		public bool p_actualizarFecha_creacion_TU(Int32 p_no_solicitud)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"DCS_P_UPD_FECHACREACION_TU";

					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_no_solicitud", OracleType.Number).Value = p_no_solicitud;
					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();
					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public string ObtenerCodigoClientexUsuario(string p_usuario)
		{
			string vl_retorno = "";
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select codigo_cliente from mgi_usuarios where codigo_usuario=:pa_usuario";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_usuario", p_usuario);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = dr["codigo_cliente"].ToString();

					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public byte[] ObtenerFotoUsuario(string p_codigo_cliente)
		{
			byte[] vl_retorno = null;

			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select foto from mrh_empleados_huellas where codigo_cliente=:pa_cliente_id";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_cliente_id", p_codigo_cliente);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						if (!DBNull.Value.Equals(dr.GetValue(0)))
						{
							vl_retorno = (byte[])dr.GetValue(0);
						}
					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public bool ObtenerHuellaFotoUsuario(string codigo_cliente, out int dedo, out byte[] huella, out byte[] foto, out byte[] firma)
		{
			bool vl_retorno = false;
			foto = null;
			huella = null;
			firma = null;
			dedo = 0;

			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select dedo,huella,foto,firma 
                                     From mrh_empleados_huellas a,
                                          mrh_empleados_firmas b 
                                    Where a.codigo_cliente=b.codigo_cliente(+)
                                     and a.codigo_cliente=:pa_codigo_cliente";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_codigo_cliente", codigo_cliente);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						dedo = int.Parse(dr.GetValue(0).ToString());

						if (!DBNull.Value.Equals(dr.GetValue(1)))
						{
							huella = (byte[])dr.GetValue(1);
						}

						if (!DBNull.Value.Equals(dr.GetValue(2)))
						{
							foto = (byte[])dr.GetValue(2);
						}
						if (!DBNull.Value.Equals(dr.GetValue(3)))
						{
							firma = (byte[])dr.GetValue(3);
						}

						vl_retorno = true;
					}
					dr.Close();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + " " + ex.Message);

			}
			return vl_retorno;
		}

		//Listo
		public bool ObtenerHuellaUsuario(string codigo_cliente, out string sin_huella, out int dedo, out byte[] huella, out byte[] huella_foto, out string fecha_act)
		{
			bool vl_retorno = false;
			huella = null;
			huella_foto = null;
			sin_huella = "N";
			dedo = 0;
			fecha_act = string.Empty;


			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select codigo_cliente,huella,huella_foto,codigo_dedo,decode(fecha_act,null,fecha_ing,fecha_act) fecha_ult_huella,sinhuella_b
                                    from mia_clientes_huellas 
                                   where codigo_cliente=:pa_codigo_cliente";

					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_codigo_cliente", codigo_cliente);
					ConexionOracle.Open();
					dr = cmd.ExecuteReader();
					if (dr.HasRows)
					{
						dr.Read();

						sin_huella = dr["sinhuella_b"].ToString();
						if (!DBNull.Value.Equals(dr.GetValue(1)))
						{
							huella = (byte[])dr.GetValue(1);
							dedo = int.Parse(dr.GetValue(3).ToString());
							fecha_act = dr.GetValue(4).ToString();
						}
						if (!DBNull.Value.Equals(dr.GetValue(2)))
						{
							huella_foto = (byte[])dr.GetValue(2);
						}

						vl_retorno = true;
					}
					dr.Close();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + " " + ex.Message);

			}
			return vl_retorno;
		}

		//Listo Pendiente
		public bool UpdateFirma(string codigo_cliente, byte[] pa_firma)
		{
			bool vl_retorno = false;
			Byte[] data = pa_firma;
			OracleLob firmablob = OracleLob.Null;



			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					ConexionOracle.Open();
					OracleTransaction transaction = ConexionOracle.BeginTransaction();
					OracleCommand command = ConexionOracle.CreateCommand();
					command.Transaction = transaction;
					command.CommandText = "declare xx blob; begin dbms_lob.createtemporary(xx, false, 0); :tempblob := xx; end;";
					command.Parameters.Add(new OracleParameter("tempblob", OracleType.Blob)).Direction = ParameterDirection.Output;
					command.ExecuteNonQuery();
					OracleLob tempLob = (OracleLob)command.Parameters[0].Value;
					firmablob = tempLob;

					tempLob.BeginBatch(OracleLobOpenMode.ReadWrite);
					tempLob.Write(data, 0, data.Length);
					tempLob.EndBatch();
					command.Parameters.Clear();
					transaction.Commit();

					string sql = @"DCS_P_GUARDAR_FIRMA";

					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_codigo_cliente", OracleType.Number).Value = codigo_cliente;
					cmd.Parameters.Add("pa_firma", OracleType.Blob, 1).Value = firmablob;


					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();

					vl_retorno = true;
					cmd.Dispose();

				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;

			//OracleCommand cmd = new OracleCommand();
			//OracleDataReader dr;
			//try
			//{
			//    using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
			//    {
			//        string sql = @"SCDP_P_GUARDAR_FIRMA";
			//        cmd.CommandText = sql;
			//        cmd.Connection = ConexionOracle;
			//        cmd.CommandType = CommandType.StoredProcedure;
			//        ConexionOracle.Open();
			//        cmd.Parameters.Add("pa_codigo_cliente", codigo_cliente);
			//        if (pa_firma != null)
			//        {
			//            cmd.Parameters.Add("pa_firma", OracleType.Blob, pa_firma, ParameterDirection.Input);
			//        }
			//        else
			//            cmd.Parameters.Add("pa_firma", OracleType.Blob, OracleBlob.Null, ParameterDirection.Input);
			//        cmd.ExecuteNonQuery();
			//        vl_retorno = true;


			//    }
			//}
			//catch (Exception ex)
			//{
			//    throw new Exception("Error en " + ex.TargetSite + " " + ex.Message);

			//}
			return vl_retorno;
		}

		//Listo
		public void ObtenerSituacionVecesPrecalifica(Int32 p_no_solicitud, out int p_recalculos_actuales, out int p_recalculos_permitidas)
		{
			p_recalculos_actuales = 0;
			p_recalculos_permitidas = 0;
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select recalculos_actuales,recalculos_permitidos 
                                     from dcs_solicitudes
                                     Where no_solicitud=:pa_no_solicitud";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_no_solicitud", p_no_solicitud);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						p_recalculos_actuales = int.Parse(dr["recalculos_actuales"].ToString());
						p_recalculos_permitidas = int.Parse(dr["recalculos_permitidos"].ToString());

					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

		}

		//Listo
		public bool RegistrarEventoErrorPrecali(Int32 p_no_solicitud, string p_accion, string p_desc_error, string p_xml_enviado)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"DCS_P_REGISTRAR_ERROR";

					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_no_solicitud", OracleType.Number).Value = p_no_solicitud;
					cmd.Parameters.Add("pa_accion", OracleType.VarChar, 10).Value = p_accion;
					cmd.Parameters.Add("pa_desc_error", OracleType.VarChar, 1000).Value = p_desc_error;
					cmd.Parameters.Add("pa_xml_enviado", OracleType.VarChar, 4000).Value = p_xml_enviado;
					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();
					cmd.Dispose();
					vl_retorno = true;
					return vl_retorno;

				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public bool RegistrarIDs_x_Solicitud(Int32 p_no_solicitud)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"DCS_P_IDS_SOLICITUDES";

					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_no_solicitud", OracleType.Number).Value = p_no_solicitud;
					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();
					cmd.Dispose();
					vl_retorno = true;
					return vl_retorno;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public bool ActualizarResultado_Buro(Int32 p_no_solicitud, string p_resultado_buro)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"DCS_P_ACTUALIZAR_RES_BURO";

					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_no_solicitud", OracleType.Number).Value = p_no_solicitud;
					cmd.Parameters.Add("pa_resultado_buro", OracleType.VarChar, 60).Value = p_resultado_buro;
					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();
					cmd.Dispose();
					vl_retorno = true;
					return vl_retorno;

				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public bool ActualizarResolucionolicitud(Int32 p_no_solicitud, string p_no_acta, string p_ciudad_resolucion, decimal p_monto_aprobado, decimal p_tasa_aprobada, decimal p_plazo_aprobado)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"DCS_P_ACTUALIZAR_RESOLCOMITE";

					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_no_solicitud", OracleType.Number).Value = p_no_solicitud;
					cmd.Parameters.Add("pa_no_acta", OracleType.VarChar, 10).Value = p_no_acta;
					cmd.Parameters.Add("pa_ciudad_resolucion", OracleType.VarChar, 100).Value = p_ciudad_resolucion;
					cmd.Parameters.Add("pa_monto_aprobado", OracleType.Number).Value = p_monto_aprobado;
					cmd.Parameters.Add("pa_plazo_aprobado", OracleType.Number).Value = p_plazo_aprobado;
					cmd.Parameters.Add("pa_tasa_aprobada", OracleType.Number).Value = p_tasa_aprobada;
					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();

					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public bool ObtenerSiHayResolucionesPendientesxSolicitud(Int32 p_no_solicitud, Int32 p_comite_id, Int32 p_movimiento_actual)
		{
			bool vl_retorno = false;
			int vl_cant = 0;

			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select count(*) n 
                                     from dcs_solicitudes_aprobaciones
                                    where pendiente_respuesta_b='S'
                                      and no_solicitud=:pa_no_solicitud
                                      and comite_id=:pa_comite_id
                                      and no_movimiento_actual=:pa_no_movimiento_actual";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_no_solicitud", p_no_solicitud);
					cmd.Parameters.AddWithValue("pa_comite_id", p_comite_id);
					cmd.Parameters.AddWithValue("pa_no_movimiento_actual", p_movimiento_actual);
					ConexionOracle.Open();
					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_cant = int.Parse(dr["n"].ToString());

					}
					dr.Close();
					if (vl_cant > 0)
						vl_retorno = true;
					else
						vl_retorno = false;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}
			return vl_retorno;
		}

		//Listo
		public bool EliminarResolucionesParaDevolver(Int32 p_no_solicitud)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"DCS_P_BORRAR_APROB_PEND_X_SOL";

					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_no_solicitud", OracleType.Number).Value = p_no_solicitud;
					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();


					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public bool EliminarResolucionxUsuario(Int32 p_no_solicitud, string p_usuario, Int32 p_movimiento_actual, string p_llave)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"DCS_P_UTIL_ELIMAPROBXUSER";

					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_no_solicitud", OracleType.Number).Value = p_no_solicitud;
					cmd.Parameters.Add("pa_usuario", OracleType.VarChar, 30).Value = p_usuario;
					cmd.Parameters.Add("pa_movimiento_actual", OracleType.Number).Value = p_movimiento_actual;
					cmd.Parameters.Add("pa_llave", OracleType.VarChar, 40).Value = p_llave;
					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();


					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public bool AdicionarResolucionxUsuario(Int32 p_no_solicitud, string p_usuario, int pa_comite_id, Int32 p_movimiento_actual)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"DCS_P_UTIL_ADD_APROB_XUSER";

					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_no_solicitud", OracleType.Number).Value = p_no_solicitud;
					cmd.Parameters.Add("pa_usuario", OracleType.VarChar, 30).Value = p_usuario;
					cmd.Parameters.Add("pa_comite_id", OracleType.Number).Value = pa_comite_id;
					cmd.Parameters.Add("pa_movimiento_actual", OracleType.Number).Value = p_movimiento_actual;
					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();


					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public bool ObtenerSiUltimaRespuestaResolucion(Int32 p_no_solicitud, Int32 p_no_movimiento, Int32 p_comite_id)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"DCS_F_ESULTIMA_RESP_RESOL";

					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_no_solicitud", OracleType.Number).Value = p_no_solicitud;
					cmd.Parameters.Add("pa_no_movimiento_actual", OracleType.Number).Value = p_no_movimiento;
					cmd.Parameters.Add("pa_comite_id", OracleType.Number).Value = p_comite_id;
					cmd.Parameters.Add("vl_respuesta", OracleType.Int16).Direction = ParameterDirection.ReturnValue;
					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();
					Int32 resultado = Int32.Parse(cmd.Parameters["vl_respuesta"].Value.ToString());
					if (resultado == 0)
						vl_retorno = false;
					if (resultado == 1)
						vl_retorno = true;
					cmd.Dispose();
					return vl_retorno;

				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public bool EsGerenteRegional(string pa_user)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"DCS_F_ES_GERENTE_REGIONAL";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_usuario", OracleType.VarChar, 30).Value = pa_user;
					cmd.Parameters.Add("vl_respuesta", OracleType.Int16).Direction = ParameterDirection.ReturnValue;
					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();
					Int32 resultado = Int32.Parse(cmd.Parameters["vl_respuesta"].Value.ToString());
					if (resultado == 0)
						vl_retorno = false;
					if (resultado == 1)
						vl_retorno = true;
					cmd.Dispose();
					return vl_retorno;

				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public bool EsAdministradorSistema(string pa_user)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"DCS_F_ES_ADMON_CS";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_usuario", OracleType.VarChar, 30).Value = pa_user;
					cmd.Parameters.Add("vl_respuesta", OracleType.Int16).Direction = ParameterDirection.ReturnValue;
					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();
					Int32 resultado = Int32.Parse(cmd.Parameters["vl_respuesta"].Value.ToString());
					if (resultado == 0)
						vl_retorno = false;
					if (resultado == 1)
						vl_retorno = true;
					cmd.Dispose();
					return vl_retorno;

				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public bool EsMiembroJuntaDirectiva(string codigo_cliente)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"DCS_F_ES_MIEMBRO_JUNTA";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_codigo_cliente", OracleType.VarChar, 30).Value = codigo_cliente;
					cmd.Parameters.Add("vl_respuesta", OracleType.Int16).Direction = ParameterDirection.ReturnValue;
					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();
					Int32 resultado = Int32.Parse(cmd.Parameters["vl_respuesta"].Value.ToString());
					if (resultado == 0)
						vl_retorno = false;
					if (resultado == 1)
						vl_retorno = true;
					cmd.Dispose();
					return vl_retorno;

				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public bool MoverSolicitud(int p_workflow_id, Int32 p_no_solicitud, int p_paso, int p_decision, string p_usuario_gerente, string miembros_gtes_selec)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"DCS_P_ENVIAR_SOLICITUD";

					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_workflow_id", OracleType.Number).Value = p_workflow_id;
					cmd.Parameters.Add("pa_no_solicitud", OracleType.Number).Value = p_no_solicitud;
					cmd.Parameters.Add("pa_paso", OracleType.Number).Value = p_paso;
					cmd.Parameters.Add("pa_decision", OracleType.Number).Value = p_decision;
					cmd.Parameters.Add("pa_usuario_gerente", OracleType.VarChar, 40).Value = p_usuario_gerente;
					cmd.Parameters.Add("pa_miembros_selec", OracleType.VarChar, 800).Value = miembros_gtes_selec;
					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();
					p_no_solicitud = Int32.Parse(cmd.Parameters[0].Value.ToString());

					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public bool ProcesarResolucionSolicitud(string p_usuario_comite, string p_decision, string p_comentarios, Int32 p_no_solicitud, out string pa_estado_final)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"DCS_P_PROCESAR_RESOLUCION";

					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_usuario_comite", OracleType.VarChar, 50).Value = p_usuario_comite;
					cmd.Parameters.Add("pa_decision", OracleType.VarChar, 50).Value = p_decision;
					cmd.Parameters.Add("pa_observaciones", OracleType.VarChar, 4000).Value = p_comentarios;
					cmd.Parameters.Add("pa_no_solicitud", OracleType.Number).Value = p_no_solicitud;
					cmd.Parameters.Add("pa_estado", OracleType.VarChar, 50).Direction = ParameterDirection.Output;

					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();
					pa_estado_final = cmd.Parameters[4].Value.ToString();

					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public bool ambEstaciones_workflow(string p_accion, Int32 p_estacion_id, string p_nombre_estacion, string p_activo, string p_ver_toda_filial,
											string p_crear_solicitudes, string p_comite_resolutivo, byte[] p_bytedata, decimal p_monto_minimo, decimal p_monto_maximo)
		{

			Byte[] data = p_bytedata;
			OracleLob icono_estacion = OracleLob.Null;


			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					ConexionOracle.Open();
					OracleTransaction transaction = ConexionOracle.BeginTransaction();
					OracleCommand command = ConexionOracle.CreateCommand();
					command.Transaction = transaction;
					command.CommandText = "declare xx blob; begin dbms_lob.createtemporary(xx, false, 0); :tempblob := xx; end;";
					command.Parameters.Add(new OracleParameter("tempblob", OracleType.Blob)).Direction = ParameterDirection.Output;
					command.ExecuteNonQuery();
					OracleLob tempLob = (OracleLob)command.Parameters[0].Value;
					icono_estacion = tempLob;

					tempLob.BeginBatch(OracleLobOpenMode.ReadWrite);
					tempLob.Write(data, 0, data.Length);
					tempLob.EndBatch();
					command.Parameters.Clear();
					transaction.Commit();

					string sql = @"DCS_P_ABM_ESTACIONES";

					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_accion", OracleType.VarChar, 10).Value = p_accion;
					cmd.Parameters.Add("pa_estacion_id", OracleType.Number).Value = p_estacion_id;
					cmd.Parameters.Add("pa_nombre_estacion", OracleType.VarChar, 50).Value = p_nombre_estacion;
					cmd.Parameters.Add("pa_activo", OracleType.VarChar, 1).Value = p_activo;
					cmd.Parameters.Add("pa_ver_toda_filial", OracleType.VarChar, 1).Value = p_ver_toda_filial;
					cmd.Parameters.Add("pa_crear_solicitudes", OracleType.VarChar, 1).Value = p_crear_solicitudes;
					cmd.Parameters.Add("pa_comite_resolutivo", OracleType.VarChar, 1).Value = p_comite_resolutivo;
					cmd.Parameters.Add("pa_icono", OracleType.Blob, 1).Value = icono_estacion;
					cmd.Parameters.Add("pa_monto_minimo", OracleType.Number).Value = p_monto_minimo;
					cmd.Parameters.Add("pa_monto_maximo", OracleType.Number).Value = p_monto_maximo;

					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();


					vl_retorno = true;
					cmd.Dispose();

				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public DataTable ObtenerComiteResolucion(Int32 p_no_solicitud)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select dcs_f_obt_comite_resol(:pa_no_solicitud) Comite_id,nombre,decode(nivel_resolutivo_local,'S','Comite local','Comite ...') tipo_comite
                                     from dcs_wf_estaciones e
                                    Where e.estacion_id=dcs_f_obt_comite_resol(:pa_no_solicitud)";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					da.SelectCommand.Parameters.Add("pa_no_solicitud", p_no_solicitud);
					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//Listo
		public DataTable ObtenerResolucionesDelComite(Int32 p_no_solicitud)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select foto,usuario_comite,InitCap(nombres||' '||primer_apellido) nombre_usuario,
                                          fecha_decision,decision,a.observaciones 
                                     from dcs_solicitudes_aprobaciones a,
                                          mrh_empleados_huellas h,
                                          mgi_usuarios u     
                                    Where a.usuario_comite=U.codigo_usuario
                                      and u.codigo_cliente=h.codigo_cliente(+)  
                                      and no_solicitud=:pa_no_solicitud";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					da.SelectCommand.Parameters.Add("pa_no_solicitud", p_no_solicitud);
					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//Listo
		public DataTable ObtenerListadeEstaciones()
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select * from dcs_wf_estaciones order by 1";

					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//Listo
		public DataTable ObtenerListaMotivosCierre(string p_motivo)
		{
			string p_condi = "";
			if (p_motivo == "PORRECHAZO")
			{
				p_condi = " and rechazada_b='S' ";
			}
			if (p_motivo == "PORAPROBACION")
			{
				p_condi = " and rechazada_b='N' and con_error_b='N'";
			}

			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select motivo_id,descripcion_motivo 
                                     From dcs_wf_motivos_cierre
                                    Where 1=1 " +
									  p_condi + @"
                                    Order by 1";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//No se usa
		public DataTable ObtenerListadeExcepciones()
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select * from dcs_wf_excepciones where activo_b='S'";

					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//Listo
		public DataTable ObtenerEstacionesxId(Int32 p_estacion_id)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select * from dcs_wf_estaciones where estacion_id=:pa_estacion_id";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_estacion_id", p_estacion_id);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//Listo
		public DataTable ObtenerEstadoSolicxID(Int32 p_estado_id)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select * from dcs_wf_estado_solicitudes where estado_id=:pa_estado_id";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_estado_id", p_estado_id);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//Listo
		public DataTable ObtenerEstadoSolicitud()
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select 0 estado_id,'Todos los Estados' descripcion from dual union select ESTADO_ID,InitCap(Descripcion) descripcion from dcs_wf_estado_solicitudes where activo='S'";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//Listo
		public DataTable ObtenerGerentesdeFilialxFilial(Int32 p_codigo_agencia)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select codigo_usuario,nombres||' '||primer_apellido nombre_gerente
                                     from mgi_usuarios
                                    Where codigo_cargo=7 --Gte de Filia
                                      and codigo_agencia=:pa_codigo_agencia
                                      and activo_b='S'
                                      and codigo_cliente>0 --para que no muestre esos usuario temporales con guiones(-)
                                    UNION
                                   Select usuario codigo_usuario,nombres||' '||primer_apellido nombre_gerente
                                     from dcs_wf_miembros_alternos a,
                                          mgi_usuarios u
                                    Where usuario=codigo_usuario
                                      and codigo_zona=(select codigo_zona from cef_agencias_zona where codigo_agencia=:pa_codigo_agencia )
                                      and codigo_agencia_acubrir=:pa_codigo_agencia
                                      and a.activo_b='S'";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_codigo_agencia", p_codigo_agencia);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//Listo
		public DataTable ObtenerAnotacionesxSolicitud(Int32 p_no_solicitud, string p_orden_anotaciones)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select est.nombre nombre_estacion,
                                          anot.no_movimiento_solicitud,
                                          anot.no_anotacion,
                                          anot.anotacion,
                                          anot.tipo_anotacion,
                                          anot.usuario_ing,
                                          anot.fecha_ing  
                                     from dcs_anotaciones_solicitudes anot,
                                          dcs_wf_estaciones est 
                                    Where anot.estacion_id=est.estacion_id
                                      and no_solicitud=:pa_no_solicitud 
                                    Order by " + p_orden_anotaciones;
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_no_solicitud", p_no_solicitud);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;

		}

		//Listo
		public DataTable ObtenerAdjuntosxSoliciud(Int32 p_no_solicitud)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select no_archivo,
                                          nombre_archivo,
                                          extension,
                                          descripcion 
                                     From dcs_archivos_adjuntos a,
                                          dcs_wf_tipo_documentos b 
                                    Where a.documento_id=b.documento_id 
                                      and no_solicitud=:pa_no_solicitud 
                                    Order by no_archivo";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_no_solicitud", p_no_solicitud);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;

		}

		//Listo
		public DataTable ObtenerListadoUsuarios(string p_condicion)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select codigo_cliente,u.codigo_usuario,InitCap(nombres||' '||primer_apellido||' '||segundo_apellido) nombres,Initcap(e.nombre) estacion_actual,Initcap(nombre_agencia) nombre_agencia
                                 From mgi_usuarios u,
                                      mgi_agencias a,
                                      dcs_wf_usuarios_estaciones ue,
                                      dcs_wf_estaciones e        
                                Where codigo_cliente is not null
                                  and u.codigo_empresa=a.codigo_empresa
                                  and u.codigo_agencia=a.codigo_agencia
                                  and u.codigo_usuario=ue.USUARIO(+)
                                  and ue.estacion_id=e.estacion_id(+)" +
								  p_condicion + @"
                                 and nvl(u.activo_b,'S')='S'
                                Order by 2";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//Listo
		public DataTable ObtenerListaXMLxSolicitud(Int32 p_no_solicitud)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select * from dcs_solicitudes2_resp_trans where no_solicitud=:pa_no_solicitud order by no_entrada";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_no_solicitud", p_no_solicitud);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//Listo
		public DataTable ObtenerHistorialSueldosDocentesCEF(string p_no_identificacion)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select ano_mes periodo,deduccion,to_char(sueldo,'999,999,999.99') sueldo 
                                     From cef_carga_docentes
                                    Where numero_identificacion=:pa_identificacion
                                   order by ano_mes desc";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_identificacion", p_no_identificacion);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//Listo
		public Byte[] ObternerArchivoxNo(Int32 p_no_archivo, out string p_extension)
		{
			Byte[] vl_retorno = new Byte[1];
			p_extension = "";
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select * from dcs_archivos_adjuntos where no_archivo=:pa_no_archivo";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_no_archivo", p_no_archivo);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = (byte[])dr["archivo_bin"];
						p_extension = dr["extension"].ToString();

					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public string ObtenerAnotacionxNo(int p_no_anotacion)
		{
			string vl_retorno = "";

			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select * from dcs_anotaciones_solicitudes where no_anotacion=:pa_no_anotacion";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_no_anotacion", p_no_anotacion);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = dr["anotacion"].ToString();

					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public bool ExisteCEFDocentes(string p_identificacion)
		{
			bool vl_retorno = false;

			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select count(*) n 
                                     From cef_carga_docentes 
                                    Where numero_identificacion=:pa_identificacion";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_identificacion", p_identificacion);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = false;
						float vl_n = 0;
						float.TryParse(dr["n"].ToString(), out vl_n);
						if (vl_n > 0)
						{
							vl_retorno = true;
						}
					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Antes
		public bool InsertarAnotacion(Int32 p_no_solicitud, Int32 p_estacion_id, string p_anotacion, string p_tipo_anotaion)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"DCS_P_INSERTAR_ANOTACION";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;

					cmd.Parameters.Add("pa_no_solicitud", OracleType.Int32).Value = p_no_solicitud;
					cmd.Parameters.Add("pa_anotacion", OracleType.VarChar, 4000).Value = p_anotacion;
					cmd.Parameters.Add("pa_tipo_anotacion", OracleType.VarChar, 1).Value = p_tipo_anotaion;
					cmd.Parameters.Add("pa_estacion_id", OracleType.Int32).Value = p_estacion_id;
					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();
					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public bool Instrucciones_desembolso(Int32 p_no_solicitud, string p_instruccion)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"DCS_P_PONER_INSTRUCCIONES_DESE";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;

					cmd.Parameters.Add("pa_no_solicitud", OracleType.Int32).Value = p_no_solicitud;
					cmd.Parameters.Add("pa_instruccion", OracleType.VarChar, 2000).Value = p_instruccion;
					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();
					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public bool Observaciones_analista(Int32 p_no_solicitud, string p_instruccion)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"DCS_P_PONER_OBS_ANALISTA";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;

					cmd.Parameters.Add("pa_no_solicitud", OracleType.Int32).Value = p_no_solicitud;
					cmd.Parameters.Add("pa_observacion", OracleType.VarChar, 3000).Value = p_instruccion;
					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();
					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public bool Observaciones_prestamos(Int32 p_no_solicitud, string p_instruccion)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"DCS_P_PONER_OBS_PRESTAMOS";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;

					cmd.Parameters.Add("pa_no_solicitud", OracleType.Int32).Value = p_no_solicitud;
					cmd.Parameters.Add("pa_observacion", OracleType.VarChar, 2000).Value = p_instruccion;
					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();
					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public bool Actualizar_cantidad_recalculos(Int32 p_no_solicitud)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"DCS_P_SET_CANT_RECALCULOS";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;

					cmd.Parameters.Add("pa_no_solicitud", OracleType.Int32).Value = p_no_solicitud;
					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();
					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public bool p_actualizar_score_resul(Int32 p_no_solicitud, string p_score)
		{

			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"UPDATE dcs_solicitudes set 
                                          score_crediticio=:pa_score
                                    Where no_solicitud=:pa_no_solicitud";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_no_solicitud", OracleType.Number).Value = p_no_solicitud;
					cmd.Parameters.Add("pa_score", OracleType.VarChar).Value = p_score;
					ConexionOracle.Open();
					cmd.ExecuteNonQuery();

					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}
			return vl_retorno;
		}
		/*----------------------------------------------------------*/
		//Listo

		public bool InsertarSolicitudCredito(solicitud_credito solicitud, out Int32 p_no_solicitud)
		{
			p_no_solicitud = 0;
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"DCS_P_INSERTAR_SOLICITUD";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;

					cmd.Parameters.Add("pa_no_solicitud", OracleType.Number).Direction = ParameterDirection.InputOutput;
					cmd.Parameters[0].Value = 0;
					cmd.Parameters.Add("pa_modo_transunion", OracleType.VarChar).Value = solicitud.modo_transunion;
					cmd.Parameters.Add("pa_application_id", OracleType.Int32).Value = solicitud.application_id;
					cmd.Parameters.Add("pa_workflow_id", OracleType.Number).Value = solicitud.workflow_id;
					cmd.Parameters.Add("pa_no_solicitud_formulario", OracleType.Number).Value = solicitud.no_solicitud;
					cmd.Parameters.Add("pa_oficial_servicio", OracleType.VarChar, 30).Value = solicitud.usuario_workflow;
					cmd.Parameters.Add("pa_codigo_agencia_origen", OracleType.Number).Value = solicitud.codigo_agencia;
					cmd.Parameters.Add("pa_codigo_fuente", OracleType.Number).Value = 1; //cuando era hipotecario con fodos rap/banprovi (1) fondos propios
					cmd.Parameters.Add("pa_codigo_sub_aplicacion", OracleType.Number).Value = solicitud.codigo_sub_aplicacion;
					cmd.Parameters.Add("pa_codigo_moneda", OracleType.Number).Value = solicitud.codigo_moneda;
					cmd.Parameters.Add("pa_monto_solicitado", OracleType.Number).Value = solicitud.monto_solicitado;
					cmd.Parameters.Add("pa_meses_plazo", OracleType.Number).Value = solicitud.plazo;
					cmd.Parameters.Add("pa_tasa", OracleType.Number, 6).Value = solicitud.tasa;
					cmd.Parameters.Add("pa_destino", OracleType.VarChar, 50).Value = solicitud.destino_credito;
					cmd.Parameters.Add("pa_es_consolidacion", OracleType.VarChar, 10).Value = solicitud.es_consolidacion;
					cmd.Parameters.Add("pa_descripcion_garantia", OracleType.VarChar, 1000).Value = solicitud.descripcion_garantia;
					cmd.Parameters.Add("pa_monto_cuota_consolidar", OracleType.Number).Value = solicitud.monto_cuota_consolidar;
					cmd.Parameters.Add("pa_monto_balance_consolidar", OracleType.Number).Value = solicitud.monto_balance_consolidar;
					cmd.Parameters.Add("pa_xml_cuotas_buro", OracleType.VarChar, 4000).Value = solicitud.xml_cuotas_buro;
					cmd.Parameters.Add("pa_condicion_vehiculo", OracleType.VarChar, 1).Value = solicitud.condicion_vehiculo;
					cmd.Parameters.Add("pa_valor_vehiculo", OracleType.Number).Value = solicitud.valor_vehiculo;
					cmd.Parameters.Add("pa_requiere_codeudor", OracleType.VarChar, 1).Value = solicitud.requiere_codeudor;
					cmd.Parameters.Add("pa_requiere_aval1", OracleType.VarChar, 1).Value = solicitud.requiere_aval1;
					cmd.Parameters.Add("pa_requiere_aval2", OracleType.VarChar, 1).Value = solicitud.requiere_aval2;
					cmd.Parameters.Add("pa_derecho_ganado", OracleType.Number).Value = solicitud.derecho_ganado;
					cmd.Parameters.Add("pa_monto_cuotas_vencimiento", OracleType.Number).Value = solicitud.monto_cuotas_vencimiento;
					cmd.Parameters.Add("pa_complemento_aportaciones", OracleType.Number).Value = solicitud.complemento_aportaciones;
					cmd.Parameters.Add("pa_deudas_canceladas_solic", OracleType.Number).Value = solicitud.deudas_canceladas_solic;
					cmd.Parameters.Add("pa_deudas_canceladas_codeud", OracleType.Number).Value = solicitud.deudas_canceladas_codeud;
					cmd.Parameters.Add("pa_deudas_canceladas_aval1", OracleType.Number).Value = solicitud.deudas_canceladas_aval1;
					cmd.Parameters.Add("pa_deudas_canceladas_aval2", OracleType.Number).Value = solicitud.deudas_canceladas_aval2;
					/*-Solicitante*/
					cmd.Parameters.Add("pa_no_identificacion", OracleType.VarChar, 20).Value = solicitud.no_identificacion;
					cmd.Parameters.Add("pa_codigo_cliente", OracleType.Number).Value = solicitud.codigo_cliente;
					cmd.Parameters.Add("pa_lugar_nacimiento", OracleType.VarChar, 100).Value = solicitud.lugar_nacimiento;
					cmd.Parameters.Add("pa_nivel_educativo", OracleType.VarChar, 20).Value = solicitud.nivel_educativo;
					cmd.Parameters.Add("pa_profesion_oficio", OracleType.VarChar, 100).Value = solicitud.profesion_oficio;
					cmd.Parameters.Add("pa_tipo_vivienda", OracleType.VarChar, 10).Value = solicitud.tipo_vivienda;
					cmd.Parameters.Add("pa_tipo_vivienda_especificar", OracleType.VarChar, 100).Value = solicitud.tipo_vivienda_especificar;
					cmd.Parameters.Add("pa_direccion_residencia", OracleType.VarChar, 100).Value = solicitud.direccion_residencia;
					cmd.Parameters.Add("pa_telefono_fijo", OracleType.VarChar, 20).Value = solicitud.telefono_fijo;
					cmd.Parameters.Add("pa_celular", OracleType.VarChar, 20).Value = solicitud.telefono_celular;
					cmd.Parameters.Add("pa_telefono_adicional1", OracleType.VarChar, 20).Value = solicitud.telefono_adicional1;
					cmd.Parameters.Add("pa_telefono_adicional2", OracleType.VarChar, 20).Value = solicitud.telefono_adicional2;
					cmd.Parameters.Add("pa_correo_electronico", OracleType.VarChar, 50).Value = solicitud.correo_electronico;
					cmd.Parameters.Add("pa_dependientes_hijos", OracleType.Number, 20).Value = solicitud.dependientes_hijos;
					cmd.Parameters.Add("pa_dependientes_otros", OracleType.Number, 20).Value = solicitud.dependientes_otros;
					cmd.Parameters.Add("pa_tipo_empresa", OracleType.VarChar, 20).Value = solicitud.tipo_empresa;
					cmd.Parameters.Add("pa_tipo_empresa_especificar", OracleType.VarChar, 100).Value = solicitud.tipo_empresa_especificar;
					cmd.Parameters.Add("pa_patrono", OracleType.VarChar, 100).Value = solicitud.patrono;
					cmd.Parameters.Add("pa_depto_labora", OracleType.VarChar, 100).Value = solicitud.depto_labora;
					cmd.Parameters.Add("pa_cargo", OracleType.VarChar, 100).Value = solicitud.cargo;
					cmd.Parameters.Add("pa_antiguedad_laboral", OracleType.VarChar, 20).Value = solicitud.antiguedad_laboral;
					cmd.Parameters.Add("pa_ingresos", OracleType.Number).Value = solicitud.ingresos;
					cmd.Parameters.Add("pa_otros_ingresos", OracleType.Number).Value = solicitud.otros_ingresos;
					cmd.Parameters.Add("pa_deducciones", OracleType.Number).Value = solicitud.deducciones;
					cmd.Parameters.Add("pa_telefono_laboral1", OracleType.VarChar, 20).Value = solicitud.telefono_laboral1;
					cmd.Parameters.Add("pa_ext_laboral1", OracleType.VarChar, 10).Value = solicitud.ext_laboral1;
					cmd.Parameters.Add("pa_telefono_laboral2", OracleType.VarChar, 20).Value = solicitud.telefono_laboral2;
					cmd.Parameters.Add("pa_ext_laboral2", OracleType.VarChar, 10).Value = solicitud.ext_laboral2;
					cmd.Parameters.Add("pa_direccion_laboral", OracleType.VarChar, 100).Value = solicitud.direccion_laboral;
					cmd.Parameters.Add("pa_correo_laboral", OracleType.VarChar, 50).Value = solicitud.correo_laboral;
					/*-Conyuge*/
					cmd.Parameters.Add("pa_no_identidad_conyuge", OracleType.VarChar, 20).Value = solicitud.no_identificacion_conyuge;
					cmd.Parameters.Add("pa_primer_nombre_conyuge", OracleType.VarChar, 50).Value = solicitud.primer_nombre_conyuge;
					cmd.Parameters.Add("pa_segundo_nombre_conyuge", OracleType.VarChar, 50).Value = solicitud.segundo_nombre_conyuge;
					cmd.Parameters.Add("pa_primer_apellido_conyuge", OracleType.VarChar, 50).Value = solicitud.primer_apellido_conyuge;
					cmd.Parameters.Add("pa_segundo_apellido_conyuge", OracleType.VarChar, 50).Value = solicitud.segundo_apellido_conyuge;
					cmd.Parameters.Add("pa_sexo_conyuge", OracleType.VarChar, 50).Value = solicitud.sexo_conyuge;
					cmd.Parameters.Add("pa_dependientes_hijos_conyuge", OracleType.Number, 20).Value = solicitud.dependientes_hijos_conyuge;
					cmd.Parameters.Add("pa_dependientes_otros_conyuge", OracleType.Number, 20).Value = solicitud.dependientes_otros_conyuge;
					cmd.Parameters.Add("pa_direccion_res_conyuge", OracleType.VarChar, 100).Value = solicitud.direccion_residencia_conyuge;
					cmd.Parameters.Add("pa_telefono_fijo_conyuge", OracleType.VarChar, 20).Value = solicitud.telefono_fijo_conyuge;
					cmd.Parameters.Add("pa_celular_conyuge", OracleType.VarChar, 20).Value = solicitud.celular_conyuge;
					cmd.Parameters.Add("pa_telefono_adicional1_conyuge", OracleType.VarChar, 20).Value = solicitud.telefono_adicional1_conyuge;
					cmd.Parameters.Add("pa_telefono_adicional2_conyuge", OracleType.VarChar, 20).Value = solicitud.telefono_adicional2_conyuge;
					cmd.Parameters.Add("pa_correo_conyuge", OracleType.VarChar, 50).Value = solicitud.correo_conyuge;
					cmd.Parameters.Add("pa_es_afiliado_conyuge", OracleType.VarChar, 1).Value = solicitud.es_afiliado_conyuge;
					cmd.Parameters.Add("pa_codigo_cilente_conyuge", OracleType.Number).Value = solicitud.codigo_cliente_conyuge;
					cmd.Parameters.Add("pa_tipo_empresa_conyuge", OracleType.VarChar, 12).Value = solicitud.tipo_empresa_conyuge;
					cmd.Parameters.Add("pa_tipo_empresa_otros_conyuge", OracleType.VarChar, 50).Value = solicitud.tipo_empresa_especificar_conyuge;
					cmd.Parameters.Add("pa_patrono_conyuge", OracleType.VarChar, 100).Value = solicitud.patrono_conyuge;
					cmd.Parameters.Add("pa_depto_labora_conyuge", OracleType.VarChar, 100).Value = solicitud.depto_labora_conyuge;
					cmd.Parameters.Add("pa_cargo_conyuge", OracleType.VarChar, 100).Value = solicitud.cargo_conyuge;
					cmd.Parameters.Add("pa_antiguedad_laboral_conyuge", OracleType.VarChar, 20).Value = solicitud.antiguedad_conyuge;
					cmd.Parameters.Add("pa_ingresos_conyuge", OracleType.Number).Value = solicitud.ingresos_conyuge;
					cmd.Parameters.Add("pa_otros_ingresos_conyuge", OracleType.Number).Value = solicitud.otros_ingresos_conyuge;
					cmd.Parameters.Add("pa_deducciones_conyuge", OracleType.Number).Value = solicitud.deducciones_conyuge;
					cmd.Parameters.Add("pa_telefono_laboral1_conyuge", OracleType.VarChar, 20).Value = solicitud.telefono_laboral1_conyuge;
					cmd.Parameters.Add("pa_ext_laboral1_conyuge", OracleType.VarChar, 10).Value = solicitud.ext_laboral1_conyuge;
					cmd.Parameters.Add("pa_telefono_laboral2_conyuge", OracleType.VarChar, 20).Value = solicitud.telefono_laboral2_conyuge;
					cmd.Parameters.Add("pa_ext_laboral2_conyuge", OracleType.VarChar, 10).Value = solicitud.ext_laboral2_conyuge;
					cmd.Parameters.Add("pa_direccion_laboral_conyuge", OracleType.VarChar, 100).Value = solicitud.direccion_laboral_conyuge;
					cmd.Parameters.Add("pa_correo_laboral_conyuge", OracleType.VarChar, 50).Value = solicitud.correo_laboral_conyuge;
					/*-Codeudor*/
					cmd.Parameters.Add("pa_no_identidad_codeudor", OracleType.VarChar, 20).Value = solicitud.no_identificacion_codeudor;
					cmd.Parameters.Add("pa_primer_nombre_codeudor", OracleType.VarChar, 50).Value = solicitud.primer_nombre_codeudor;
					cmd.Parameters.Add("pa_segundo_nombre_codeudor", OracleType.VarChar, 50).Value = solicitud.segundo_nombre_codeudor;
					cmd.Parameters.Add("pa_primer_apellido_codeudor", OracleType.VarChar, 50).Value = solicitud.primer_apellido_codeudor;
					cmd.Parameters.Add("pa_segundo_apellido_codeudor", OracleType.VarChar, 50).Value = solicitud.segundo_apellido_codeudor;
					cmd.Parameters.Add("pa_sexo_codeudor", OracleType.VarChar, 50).Value = solicitud.sexo_codeudor;
					cmd.Parameters.Add("pa_dependientes_hijos_codeudor", OracleType.Number, 20).Value = solicitud.dependientes_hijos_codeudor;
					cmd.Parameters.Add("pa_dependientes_otros_codeudor", OracleType.Number, 20).Value = solicitud.dependientes_otros_codeudor;
					cmd.Parameters.Add("pa_direccion_res_codeudor", OracleType.VarChar, 100).Value = solicitud.direccion_residencia_codeudor;
					cmd.Parameters.Add("pa_telefono_fijo_codeudor", OracleType.VarChar, 20).Value = solicitud.telefono_fijo_codeudor;
					cmd.Parameters.Add("pa_celular_codeudor", OracleType.VarChar, 20).Value = solicitud.celular_codeudor;
					cmd.Parameters.Add("pa_telefono_adicional1_cod", OracleType.VarChar, 20).Value = solicitud.telefono_adicional1_codeudor;
					cmd.Parameters.Add("pa_telefono_adicional2_cod", OracleType.VarChar, 20).Value = solicitud.telefono_adicional2_codeudor;
					cmd.Parameters.Add("pa_correo_codeudor", OracleType.VarChar, 50).Value = solicitud.correo_codeudor;
					cmd.Parameters.Add("pa_es_afiliado_codeudor", OracleType.VarChar, 1).Value = solicitud.es_afiliado_codeudor;
					cmd.Parameters.Add("pa_codigo_cilente_codeudor", OracleType.Number).Value = solicitud.codigo_cliente_codeudor;
					cmd.Parameters.Add("pa_tipo_empresa_codeudor", OracleType.VarChar, 12).Value = solicitud.tipo_empresa_codeudor;
					cmd.Parameters.Add("pa_tipo_empresa_otros_codeudor", OracleType.VarChar, 50).Value = solicitud.tipo_empresa_especificar_codeudor;
					cmd.Parameters.Add("pa_patrono_codeudor", OracleType.VarChar, 100).Value = solicitud.patrono_codeudor;
					cmd.Parameters.Add("pa_depto_labora_codeudor", OracleType.VarChar, 100).Value = solicitud.depto_labora_codeudor;
					cmd.Parameters.Add("pa_cargo_codeudor", OracleType.VarChar, 100).Value = solicitud.cargo_codeudor;
					cmd.Parameters.Add("pa_antiguedad_laboral_codeudor", OracleType.VarChar, 20).Value = solicitud.antiguedad_codeudor;
					cmd.Parameters.Add("pa_ingresos_codeudor", OracleType.Number).Value = solicitud.ingresos_codeudor;
					cmd.Parameters.Add("pa_otros_ingresos_codeudor", OracleType.Number).Value = solicitud.otros_ingresos_codeudor;
					cmd.Parameters.Add("pa_deducciones_codeudor", OracleType.Number).Value = solicitud.deducciones_codeudor;
					cmd.Parameters.Add("pa_telefono_laboral1_codeudor", OracleType.VarChar, 20).Value = solicitud.telefono_laboral1_codeudor;
					cmd.Parameters.Add("pa_ext_laboral1_codeudor", OracleType.VarChar, 10).Value = solicitud.ext_laboral1_codeudor;
					cmd.Parameters.Add("pa_telefono_laboral2_codeudor", OracleType.VarChar, 20).Value = solicitud.telefono_laboral2_codeudor;
					cmd.Parameters.Add("pa_ext_laboral2_codeudor", OracleType.VarChar, 10).Value = solicitud.ext_laboral2_codeudor;
					cmd.Parameters.Add("pa_direccion_laboral_codeudor", OracleType.VarChar, 100).Value = solicitud.direccion_laboral_codeudor;
					cmd.Parameters.Add("pa_correo_laboral_codeudor", OracleType.VarChar, 50).Value = solicitud.correo_laboral_codeudor;
					cmd.Parameters.Add("pa_nombre_conyuge_codeudor", OracleType.VarChar, 50).Value = solicitud.nombre_conyuge_codeudor;
					cmd.Parameters.Add("pa_direclab_conyuge_codeudor", OracleType.VarChar, 50).Value = solicitud.direclab_conyuge_codeudor;
					cmd.Parameters.Add("pa_cargo_conyuge_codeudor", OracleType.VarChar, 50).Value = solicitud.cargo_conyuge_codeudor;
					/*-Aval1*/
					cmd.Parameters.Add("pa_no_identidad_aval1", OracleType.VarChar, 20).Value = solicitud.no_identificacion_aval1;
					cmd.Parameters.Add("pa_primer_nombre_aval1", OracleType.VarChar, 50).Value = solicitud.primer_nombre_aval1;
					cmd.Parameters.Add("pa_segundo_nombre_aval1", OracleType.VarChar, 50).Value = solicitud.segundo_nombre_aval1;
					cmd.Parameters.Add("pa_primer_apellido_aval1", OracleType.VarChar, 50).Value = solicitud.primer_apellido_aval1;
					cmd.Parameters.Add("pa_segundo_apellido_aval1", OracleType.VarChar, 50).Value = solicitud.segundo_apellido_aval1;
					cmd.Parameters.Add("pa_sexo_aval1", OracleType.VarChar, 50).Value = solicitud.sexo_aval1;
					cmd.Parameters.Add("pa_dependientes_hijos_aval1", OracleType.Number, 20).Value = solicitud.dependientes_hijos_aval1;
					cmd.Parameters.Add("pa_dependientes_otros_aval1", OracleType.Number, 20).Value = solicitud.dependientes_otros_aval1;
					cmd.Parameters.Add("pa_direccion_res_aval1", OracleType.VarChar, 100).Value = solicitud.direccion_residencia_aval1;
					cmd.Parameters.Add("pa_telefono_fijo_aval1", OracleType.VarChar, 20).Value = solicitud.telefono_fijo_aval1;
					cmd.Parameters.Add("pa_celular_aval1", OracleType.VarChar, 20).Value = solicitud.celular_aval1;
					cmd.Parameters.Add("pa_telefono_adicional1_aval1", OracleType.VarChar, 20).Value = solicitud.telefono_adicional1_aval1;
					cmd.Parameters.Add("pa_telefono_adicional2_aval1", OracleType.VarChar, 20).Value = solicitud.telefono_adicional2_aval1;
					cmd.Parameters.Add("pa_correo_aval1", OracleType.VarChar, 50).Value = solicitud.correo_aval1;
					cmd.Parameters.Add("pa_es_afiliado_aval1", OracleType.VarChar, 1).Value = solicitud.es_afiliado_aval1;
					cmd.Parameters.Add("pa_codigo_cilente_aval1", OracleType.Number).Value = solicitud.codigo_cliente_aval1;
					cmd.Parameters.Add("pa_tipo_empresa_aval1", OracleType.VarChar, 12).Value = solicitud.tipo_empresa_aval1;
					cmd.Parameters.Add("pa_tipo_empresa_otros_aval1", OracleType.VarChar, 50).Value = solicitud.tipo_empresa_especificar_aval1;
					cmd.Parameters.Add("pa_patrono_aval1", OracleType.VarChar, 100).Value = solicitud.patrono_aval1;
					cmd.Parameters.Add("pa_depto_labora_aval1", OracleType.VarChar, 100).Value = solicitud.depto_labora_aval1;
					cmd.Parameters.Add("pa_cargo_aval1", OracleType.VarChar, 100).Value = solicitud.cargo_aval1;
					cmd.Parameters.Add("pa_antiguedad_laboral_aval1", OracleType.VarChar, 20).Value = solicitud.antiguedad_aval1;
					cmd.Parameters.Add("pa_ingresos_aval1", OracleType.Number).Value = solicitud.ingresos_aval1;
					cmd.Parameters.Add("pa_otros_ingresos_aval1", OracleType.Number).Value = solicitud.otros_ingresos_aval1;
					cmd.Parameters.Add("pa_deducciones_aval1", OracleType.Number).Value = solicitud.deducciones_aval1;
					cmd.Parameters.Add("pa_telefono_laboral1_aval1", OracleType.VarChar, 20).Value = solicitud.telefono_laboral1_aval1;
					cmd.Parameters.Add("pa_ext_laboral1_aval1", OracleType.VarChar, 10).Value = solicitud.ext_laboral1_aval1;
					cmd.Parameters.Add("pa_telefono_laboral2_aval1", OracleType.VarChar, 20).Value = solicitud.telefono_laboral2_aval1;
					cmd.Parameters.Add("pa_ext_laboral2_aval1", OracleType.VarChar, 10).Value = solicitud.ext_laboral2_aval1;
					cmd.Parameters.Add("pa_direccion_laboral_aval1", OracleType.VarChar, 100).Value = solicitud.direccion_laboral_aval1;
					cmd.Parameters.Add("pa_correo_laboral_aval1", OracleType.VarChar, 50).Value = solicitud.correo_laboral_aval1;
					cmd.Parameters.Add("pa_nombre_conyuge_aval1", OracleType.VarChar, 50).Value = solicitud.nombre_conyuge_aval1;
					cmd.Parameters.Add("pa_direclab_conyuge_aval1", OracleType.VarChar, 50).Value = solicitud.direclab_conyuge_aval1;
					cmd.Parameters.Add("pa_cargo_conyuge_aval1", OracleType.VarChar, 50).Value = solicitud.cargo_conyuge_aval1;
					/*-Aval2*/
					cmd.Parameters.Add("pa_no_identidad_aval2", OracleType.VarChar, 20).Value = solicitud.no_identificacion_aval2;
					cmd.Parameters.Add("pa_primer_nombre_aval2", OracleType.VarChar, 50).Value = solicitud.primer_nombre_aval2;
					cmd.Parameters.Add("pa_segundo_nombre_aval2", OracleType.VarChar, 50).Value = solicitud.segundo_nombre_aval2;
					cmd.Parameters.Add("pa_primer_apellido_aval2", OracleType.VarChar, 50).Value = solicitud.primer_apellido_aval2;
					cmd.Parameters.Add("pa_segundo_apellido_aval2", OracleType.VarChar, 50).Value = solicitud.segundo_apellido_aval2;
					cmd.Parameters.Add("pa_sexo_aval2", OracleType.VarChar, 50).Value = solicitud.sexo_aval2;
					cmd.Parameters.Add("pa_dependientes_hijos_aval2", OracleType.Number, 20).Value = solicitud.dependientes_hijos_aval2;
					cmd.Parameters.Add("pa_dependientes_otros_aval2", OracleType.Number, 20).Value = solicitud.dependientes_otros_aval2;
					cmd.Parameters.Add("pa_direccion_res_aval2", OracleType.VarChar, 100).Value = solicitud.direccion_residencia_aval2;
					cmd.Parameters.Add("pa_telefono_fijo_aval2", OracleType.VarChar, 20).Value = solicitud.telefono_fijo_aval2;
					cmd.Parameters.Add("pa_celular_aval2", OracleType.VarChar, 20).Value = solicitud.celular_aval2;
					cmd.Parameters.Add("pa_telefono_adicional1_aval2", OracleType.VarChar, 20).Value = solicitud.telefono_adicional1_aval2;
					cmd.Parameters.Add("pa_telefono_adicional2_aval2", OracleType.VarChar, 20).Value = solicitud.telefono_adicional2_aval2;
					cmd.Parameters.Add("pa_correo_aval2", OracleType.VarChar, 50).Value = solicitud.correo_aval2;
					cmd.Parameters.Add("pa_es_afiliado_aval2", OracleType.VarChar, 1).Value = solicitud.es_afiliado_aval2;
					cmd.Parameters.Add("pa_codigo_cilente_aval2", OracleType.Number).Value = solicitud.codigo_cliente_aval2;
					cmd.Parameters.Add("pa_tipo_empresa_aval2", OracleType.VarChar, 12).Value = solicitud.tipo_empresa_aval2;
					cmd.Parameters.Add("pa_tipo_empresa_otros_aval2", OracleType.VarChar, 50).Value = solicitud.tipo_empresa_especificar_aval2;
					cmd.Parameters.Add("pa_patrono_aval2", OracleType.VarChar, 100).Value = solicitud.patrono_aval2;
					cmd.Parameters.Add("pa_depto_labora_aval2", OracleType.VarChar, 100).Value = solicitud.depto_labora_aval2;
					cmd.Parameters.Add("pa_cargo_aval2", OracleType.VarChar, 100).Value = solicitud.cargo_aval2;
					cmd.Parameters.Add("pa_antiguedad_laboral_aval2", OracleType.VarChar, 20).Value = solicitud.antiguedad_aval2;
					cmd.Parameters.Add("pa_ingresos_aval2", OracleType.Number).Value = solicitud.ingresos_aval2;
					cmd.Parameters.Add("pa_otros_ingresos_aval2", OracleType.Number).Value = solicitud.otros_ingresos_aval2;
					cmd.Parameters.Add("pa_deducciones_aval2", OracleType.Number).Value = solicitud.deducciones_aval2;
					cmd.Parameters.Add("pa_telefono_laboral1_aval2", OracleType.VarChar, 20).Value = solicitud.telefono_laboral1_aval2;
					cmd.Parameters.Add("pa_ext_laboral1_aval2", OracleType.VarChar, 10).Value = solicitud.ext_laboral1_aval2;
					cmd.Parameters.Add("pa_telefono_laboral2_aval2", OracleType.VarChar, 20).Value = solicitud.telefono_laboral2_aval2;
					cmd.Parameters.Add("pa_ext_laboral2_aval2", OracleType.VarChar, 10).Value = solicitud.ext_laboral2_aval2;
					cmd.Parameters.Add("pa_direccion_laboral_aval2", OracleType.VarChar, 100).Value = solicitud.direccion_laboral_aval2;
					cmd.Parameters.Add("pa_correo_laboral_aval2", OracleType.VarChar, 50).Value = solicitud.correo_laboral_aval2;
					cmd.Parameters.Add("pa_nombre_conyuge_aval2", OracleType.VarChar, 50).Value = solicitud.nombre_conyuge_aval2;
					cmd.Parameters.Add("pa_direclab_conyuge_aval2", OracleType.VarChar, 50).Value = solicitud.direclab_conyuge_aval2;
					cmd.Parameters.Add("pa_cargo_conyuge_aval2", OracleType.VarChar, 50).Value = solicitud.cargo_conyuge_aval2;
					/*Referencias*/
					cmd.Parameters.Add("pa_xml_referencias", OracleType.LongVarChar).Value = solicitud.xml_referencias;
					/*Parametros calculo de concetracio crediticia*/
					cmd.Parameters.Add("pa_indicador_aplicado", OracleType.Number).Value = solicitud.indicador_aplicado;
					cmd.Parameters.Add("pa_total_capitalvigente_grpfam", OracleType.Number).Value = solicitud.total_capitalvigente_grpfam;
					cmd.Parameters.Add("pa_total_capitalvigente_solici", OracleType.Number).Value = solicitud.total_capitalvigente_solicitante;
					cmd.Parameters.Add("pa_monto_ensolicitud", OracleType.Number).Value = solicitud.monto_ensolicitud;
					cmd.Parameters.Add("pa_monto_excluir_refconsol", OracleType.Number).Value = solicitud.monto_excluir_refconsol;
					cmd.Parameters.Add("pa_total_paraindice", OracleType.Number).Value = solicitud.total_paraindice;
					cmd.Parameters.Add("pa_patrimonio_csf", OracleType.Number).Value = solicitud.patrimonio_csf;
					cmd.Parameters.Add("pa_porcentaje_concentracion", OracleType.Number).Value = solicitud.porcentaje_concentracion;
					cmd.Parameters.Add("pa_limite_indicador", OracleType.Number).Value = solicitud.limite_indicador;
					cmd.Parameters.Add("pa_resultado_evaluacion_ind", OracleType.VarChar, 50).Value = solicitud.resultado_evaluacion_indicador;
					/*---*/
					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();
					p_no_solicitud = Int32.Parse(cmd.Parameters[0].Value.ToString());

					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo - DESPUES ESTE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		public bool ActualizarSolicitudCredito(solicitud_credito solicitud, GaranteHipotecario garante)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"DCS_P_UPDATE_SOLICITUD";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;

					#region Generales

					cmd.Parameters.Add("pa_no_solicitud", OracleType.Number).Value = solicitud.no_solicitud;
					cmd.Parameters.Add("pa_modo_transunion", OracleType.VarChar, 30).Value = solicitud.modo_transunion;
					cmd.Parameters.Add("pa_application_id", OracleType.Int32).Value = solicitud.application_id;
					cmd.Parameters.Add("pa_workflow_id", OracleType.Number).Value = solicitud.workflow_id;
					cmd.Parameters.Add("pa_no_solicitud_formulario", OracleType.Number).Value = solicitud.no_solicitud;
					cmd.Parameters.Add("pa_oficial_servicio", OracleType.VarChar, 30).Value = solicitud.usuario_workflow;
					cmd.Parameters.Add("pa_codigo_agencia_origen", OracleType.Number).Value = solicitud.codigo_agencia;
					cmd.Parameters.Add("pa_codigo_fuente", OracleType.Number).Value = solicitud.fuente_financiamiento; //cuando era hipotecario con fodos rap/banprovi (1) fondos propios
					cmd.Parameters.Add("pa_codigo_sub_aplicacion", OracleType.Number).Value = solicitud.codigo_sub_aplicacion;
					cmd.Parameters.Add("pa_codigo_moneda", OracleType.Number).Value = solicitud.codigo_moneda;
					cmd.Parameters.Add("pa_monto_solicitado", OracleType.Number).Value = solicitud.monto_solicitado;
					cmd.Parameters.Add("pa_meses_plazo", OracleType.Number).Value = solicitud.plazo;
					cmd.Parameters.Add("pa_tasa", OracleType.Number, 6).Value = solicitud.tasa;
					cmd.Parameters.Add("pa_destino", OracleType.VarChar, 50).Value = solicitud.destino_credito;
					cmd.Parameters.Add("pa_es_consolidacion", OracleType.VarChar, 10).Value = solicitud.es_consolidacion;
					cmd.Parameters.Add("pa_descripcion_garantia", OracleType.VarChar, 1000).Value = solicitud.descripcion_garantia;
					cmd.Parameters.Add("pa_monto_cuota_consolidar", OracleType.Number).Value = solicitud.monto_cuota_consolidar;
					cmd.Parameters.Add("pa_monto_balance_consolidar", OracleType.Number).Value = solicitud.monto_balance_consolidar;
					cmd.Parameters.Add("pa_xml_cuotas_buro", OracleType.VarChar, 4000).Value = solicitud.xml_cuotas_buro;
					cmd.Parameters.Add("pa_condicion_vehiculo", OracleType.VarChar, 1).Value = solicitud.condicion_vehiculo;
					cmd.Parameters.Add("pa_valor_vehiculo", OracleType.Number).Value = solicitud.valor_vehiculo;
					cmd.Parameters.Add("pa_requiere_codeudor", OracleType.VarChar, 1).Value = solicitud.requiere_codeudor;
					cmd.Parameters.Add("pa_requiere_aval1", OracleType.VarChar, 1).Value = solicitud.requiere_aval1;
					cmd.Parameters.Add("pa_requiere_aval2", OracleType.VarChar, 1).Value = solicitud.requiere_aval2;
					cmd.Parameters.Add("pa_derecho_ganado", OracleType.Number).Value = solicitud.derecho_ganado;
					cmd.Parameters.Add("pa_monto_cuotas_vencimiento", OracleType.Number).Value = solicitud.monto_cuotas_vencimiento;
					cmd.Parameters.Add("pa_complemento_aportaciones", OracleType.Number).Value = solicitud.complemento_aportaciones;
					cmd.Parameters.Add("pa_deudas_canceladas_solic", OracleType.Number).Value = solicitud.deudas_canceladas_solic;
					cmd.Parameters.Add("pa_deudas_canceladas_codeud", OracleType.Number).Value = solicitud.deudas_canceladas_codeud;
					cmd.Parameters.Add("pa_deudas_canceladas_aval1", OracleType.Number).Value = solicitud.deudas_canceladas_aval1;
					cmd.Parameters.Add("pa_deudas_canceladas_aval2", OracleType.Number).Value = solicitud.deudas_canceladas_aval2;

					#endregion

					#region Solicitante

					cmd.Parameters.Add("pa_no_identificacion", OracleType.VarChar, 20).Value = solicitud.no_identificacion;
					cmd.Parameters.Add("pa_codigo_cliente", OracleType.Number).Value = solicitud.codigo_cliente;
					cmd.Parameters.Add("pa_lugar_nacimiento", OracleType.VarChar, 100).Value = solicitud.lugar_nacimiento;
					cmd.Parameters.Add("pa_nivel_educativo", OracleType.VarChar, 20).Value = solicitud.nivel_educativo;
					cmd.Parameters.Add("pa_profesion_oficio", OracleType.VarChar, 100).Value = solicitud.profesion_oficio;
					cmd.Parameters.Add("pa_tipo_vivienda", OracleType.VarChar, 10).Value = solicitud.tipo_vivienda;
					cmd.Parameters.Add("pa_tipo_vivienda_especificar", OracleType.VarChar, 100).Value = solicitud.tipo_vivienda_especificar;
					cmd.Parameters.Add("pa_direccion_residencia", OracleType.VarChar, 100).Value = solicitud.direccion_residencia;
					cmd.Parameters.Add("pa_telefono_fijo", OracleType.VarChar, 20).Value = solicitud.telefono_fijo;
					cmd.Parameters.Add("pa_celular", OracleType.VarChar, 20).Value = solicitud.telefono_celular;
					cmd.Parameters.Add("pa_telefono_adicional1", OracleType.VarChar, 20).Value = solicitud.telefono_adicional1;
					cmd.Parameters.Add("pa_telefono_adicional2", OracleType.VarChar, 20).Value = solicitud.telefono_adicional2;
					cmd.Parameters.Add("pa_correo_electronico", OracleType.VarChar, 50).Value = solicitud.correo_electronico;
					cmd.Parameters.Add("pa_dependientes_hijos", OracleType.Number, 20).Value = solicitud.dependientes_hijos;
					cmd.Parameters.Add("pa_dependientes_otros", OracleType.Number, 20).Value = solicitud.dependientes_otros;
					cmd.Parameters.Add("pa_tipo_empresa", OracleType.VarChar, 20).Value = solicitud.tipo_empresa;
					cmd.Parameters.Add("pa_tipo_empresa_especificar", OracleType.VarChar, 100).Value = solicitud.tipo_empresa_especificar;
					cmd.Parameters.Add("pa_patrono", OracleType.VarChar, 100).Value = solicitud.patrono;
					cmd.Parameters.Add("pa_depto_labora", OracleType.VarChar, 100).Value = solicitud.depto_labora;
					cmd.Parameters.Add("pa_cargo", OracleType.VarChar, 100).Value = solicitud.cargo;
					cmd.Parameters.Add("pa_antiguedad_laboral", OracleType.VarChar, 20).Value = solicitud.antiguedad_laboral;
					cmd.Parameters.Add("pa_ingresos", OracleType.Number).Value = solicitud.ingresos;
					cmd.Parameters.Add("pa_otros_ingresos", OracleType.Number).Value = solicitud.otros_ingresos;
					cmd.Parameters.Add("pa_deducciones", OracleType.Number).Value = solicitud.deducciones;
					cmd.Parameters.Add("pa_telefono_laboral1", OracleType.VarChar, 20).Value = solicitud.telefono_laboral1;
					cmd.Parameters.Add("pa_ext_laboral1", OracleType.VarChar, 10).Value = solicitud.ext_laboral1;
					cmd.Parameters.Add("pa_telefono_laboral2", OracleType.VarChar, 20).Value = solicitud.telefono_laboral2;
					cmd.Parameters.Add("pa_ext_laboral2", OracleType.VarChar, 10).Value = solicitud.ext_laboral2;
					cmd.Parameters.Add("pa_direccion_laboral", OracleType.VarChar, 100).Value = solicitud.direccion_laboral;
					cmd.Parameters.Add("pa_correo_laboral", OracleType.VarChar, 50).Value = solicitud.correo_laboral;

					#endregion

					#region Conyuge

					cmd.Parameters.Add("pa_no_identidad_conyuge", OracleType.VarChar, 20).Value = solicitud.no_identificacion_conyuge;
					cmd.Parameters.Add("pa_primer_nombre_conyuge", OracleType.VarChar, 50).Value = solicitud.primer_nombre_conyuge;
					cmd.Parameters.Add("pa_segundo_nombre_conyuge", OracleType.VarChar, 50).Value = solicitud.segundo_nombre_conyuge;
					cmd.Parameters.Add("pa_primer_apellido_conyuge", OracleType.VarChar, 50).Value = solicitud.primer_apellido_conyuge;
					cmd.Parameters.Add("pa_segundo_apellido_conyuge", OracleType.VarChar, 50).Value = solicitud.segundo_apellido_conyuge;
					cmd.Parameters.Add("pa_sexo_conyuge", OracleType.VarChar, 50).Value = solicitud.sexo_conyuge;
					cmd.Parameters.Add("pa_dependientes_hijos_conyuge", OracleType.Number, 20).Value = solicitud.dependientes_hijos_conyuge;
					cmd.Parameters.Add("pa_dependientes_otros_conyuge", OracleType.Number, 20).Value = solicitud.dependientes_otros_conyuge;
					cmd.Parameters.Add("pa_direccion_res_conyuge", OracleType.VarChar, 100).Value = solicitud.direccion_residencia_conyuge;
					cmd.Parameters.Add("pa_telefono_fijo_conyuge", OracleType.VarChar, 20).Value = solicitud.telefono_fijo_conyuge;
					cmd.Parameters.Add("pa_celular_conyuge", OracleType.VarChar, 20).Value = solicitud.celular_conyuge;
					cmd.Parameters.Add("pa_telefono_adicional1_conyuge", OracleType.VarChar, 20).Value = solicitud.telefono_adicional1_conyuge;
					cmd.Parameters.Add("pa_telefono_adicional2_conyuge", OracleType.VarChar, 20).Value = solicitud.telefono_adicional2_conyuge;
					cmd.Parameters.Add("pa_correo_conyuge", OracleType.VarChar, 50).Value = solicitud.correo_conyuge;
					cmd.Parameters.Add("pa_es_afiliado_conyuge", OracleType.VarChar, 1).Value = solicitud.es_afiliado_conyuge;
					cmd.Parameters.Add("pa_codigo_cliente_conyuge", OracleType.Number).Value = solicitud.codigo_cliente_conyuge;
					cmd.Parameters.Add("pa_tipo_empresa_conyuge", OracleType.VarChar, 12).Value = solicitud.tipo_empresa_conyuge;
					cmd.Parameters.Add("pa_tipo_empresa_otros_conyuge", OracleType.VarChar, 50).Value = solicitud.tipo_empresa_especificar_conyuge;
					cmd.Parameters.Add("pa_patrono_conyuge", OracleType.VarChar, 100).Value = solicitud.patrono_conyuge;
					cmd.Parameters.Add("pa_depto_labora_conyuge", OracleType.VarChar, 100).Value = solicitud.depto_labora_conyuge;
					cmd.Parameters.Add("pa_cargo_conyuge", OracleType.VarChar, 100).Value = solicitud.cargo_conyuge;
					cmd.Parameters.Add("pa_antiguedad_laboral_conyuge", OracleType.VarChar, 20).Value = solicitud.antiguedad_conyuge;
					cmd.Parameters.Add("pa_ingresos_conyuge", OracleType.Number).Value = solicitud.ingresos_conyuge;
					cmd.Parameters.Add("pa_otros_ingresos_conyuge", OracleType.Number).Value = solicitud.otros_ingresos_conyuge;
					cmd.Parameters.Add("pa_deducciones_conyuge", OracleType.Number).Value = solicitud.deducciones_conyuge;
					cmd.Parameters.Add("pa_telefono_laboral1_conyuge", OracleType.VarChar, 20).Value = solicitud.telefono_laboral1_conyuge;
					cmd.Parameters.Add("pa_ext_laboral1_conyuge", OracleType.VarChar, 10).Value = solicitud.ext_laboral1_conyuge;
					cmd.Parameters.Add("pa_telefono_laboral2_conyuge", OracleType.VarChar, 20).Value = solicitud.telefono_laboral2_conyuge;
					cmd.Parameters.Add("pa_ext_laboral2_conyuge", OracleType.VarChar, 10).Value = solicitud.ext_laboral2_conyuge;
					cmd.Parameters.Add("pa_direccion_laboral_conyuge", OracleType.VarChar, 100).Value = solicitud.direccion_laboral_conyuge;
					cmd.Parameters.Add("pa_correo_laboral_conyuge", OracleType.VarChar, 50).Value = solicitud.correo_laboral_conyuge;

					#endregion

					#region Codeudor

					cmd.Parameters.Add("pa_no_identidad_codeudor", OracleType.VarChar, 20).Value = solicitud.no_identificacion_codeudor;
					cmd.Parameters.Add("pa_primer_nombre_codeudor", OracleType.VarChar, 50).Value = solicitud.primer_nombre_codeudor;
					cmd.Parameters.Add("pa_segundo_nombre_codeudor", OracleType.VarChar, 50).Value = solicitud.segundo_nombre_codeudor;
					cmd.Parameters.Add("pa_primer_apellido_codeudor", OracleType.VarChar, 50).Value = solicitud.primer_apellido_codeudor;
					cmd.Parameters.Add("pa_segundo_apellido_codeudor", OracleType.VarChar, 50).Value = solicitud.segundo_apellido_codeudor;
					cmd.Parameters.Add("pa_sexo_codeudor", OracleType.VarChar, 50).Value = solicitud.sexo_codeudor;
					cmd.Parameters.Add("pa_dependientes_hijos_codeudor", OracleType.Number, 20).Value = solicitud.dependientes_hijos_codeudor;
					cmd.Parameters.Add("pa_dependientes_otros_codeudor", OracleType.Number, 20).Value = solicitud.dependientes_otros_codeudor;
					cmd.Parameters.Add("pa_direccion_res_codeudor", OracleType.VarChar, 100).Value = solicitud.direccion_residencia_codeudor;
					cmd.Parameters.Add("pa_telefono_fijo_codeudor", OracleType.VarChar, 20).Value = solicitud.telefono_fijo_codeudor;
					cmd.Parameters.Add("pa_celular_codeudor", OracleType.VarChar, 20).Value = solicitud.celular_codeudor;
					cmd.Parameters.Add("pa_telefono_adicional1_cod", OracleType.VarChar, 20).Value = solicitud.telefono_adicional1_codeudor;
					cmd.Parameters.Add("pa_telefono_adicional2_cod", OracleType.VarChar, 20).Value = solicitud.telefono_adicional2_codeudor;
					cmd.Parameters.Add("pa_correo_codeudor", OracleType.VarChar, 50).Value = solicitud.correo_codeudor;
					cmd.Parameters.Add("pa_es_afiliado_codeudor", OracleType.VarChar, 1).Value = solicitud.es_afiliado_codeudor;
					cmd.Parameters.Add("pa_codigo_cilente_codeudor", OracleType.Number).Value = solicitud.codigo_cliente_codeudor;
					cmd.Parameters.Add("pa_tipo_empresa_codeudor", OracleType.VarChar, 12).Value = solicitud.tipo_empresa_codeudor;
					cmd.Parameters.Add("pa_tipo_empresa_otros_codeudor", OracleType.VarChar, 50).Value = solicitud.tipo_empresa_especificar_codeudor;
					cmd.Parameters.Add("pa_patrono_codeudor", OracleType.VarChar, 100).Value = solicitud.patrono_codeudor;
					cmd.Parameters.Add("pa_depto_labora_codeudor", OracleType.VarChar, 100).Value = solicitud.depto_labora_codeudor;
					cmd.Parameters.Add("pa_cargo_codeudor", OracleType.VarChar, 100).Value = solicitud.cargo_codeudor;
					cmd.Parameters.Add("pa_antiguedad_laboral_codeudor", OracleType.VarChar, 20).Value = solicitud.antiguedad_codeudor;
					cmd.Parameters.Add("pa_ingresos_codeudor", OracleType.Number).Value = solicitud.ingresos_codeudor;
					cmd.Parameters.Add("pa_otros_ingresos_codeudor", OracleType.Number).Value = solicitud.otros_ingresos_codeudor;
					cmd.Parameters.Add("pa_deducciones_codeudor", OracleType.Number).Value = solicitud.deducciones_codeudor;
					cmd.Parameters.Add("pa_telefono_laboral1_codeudor", OracleType.VarChar, 20).Value = solicitud.telefono_laboral1_codeudor;
					cmd.Parameters.Add("pa_ext_laboral1_codeudor", OracleType.VarChar, 10).Value = solicitud.ext_laboral1_codeudor;
					cmd.Parameters.Add("pa_telefono_laboral2_codeudor", OracleType.VarChar, 20).Value = solicitud.telefono_laboral2_codeudor;
					cmd.Parameters.Add("pa_ext_laboral2_codeudor", OracleType.VarChar, 10).Value = solicitud.ext_laboral2_codeudor;
					cmd.Parameters.Add("pa_direccion_laboral_codeudor", OracleType.VarChar, 100).Value = solicitud.direccion_laboral_codeudor;
					cmd.Parameters.Add("pa_correo_laboral_codeudor", OracleType.VarChar, 50).Value = solicitud.correo_laboral_codeudor;
					cmd.Parameters.Add("pa_nombre_conyuge_codeudor", OracleType.VarChar, 50).Value = solicitud.nombre_conyuge_codeudor;
					cmd.Parameters.Add("pa_direclab_conyuge_codeudor", OracleType.VarChar, 50).Value = solicitud.direclab_conyuge_codeudor;
					cmd.Parameters.Add("pa_cargo_conyuge_codeudor", OracleType.VarChar, 50).Value = solicitud.cargo_conyuge_codeudor;

					#endregion

					#region Aval1

					cmd.Parameters.Add("pa_no_identidad_aval1", OracleType.VarChar, 20).Value = solicitud.no_identificacion_aval1;
					cmd.Parameters.Add("pa_primer_nombre_aval1", OracleType.VarChar, 50).Value = solicitud.primer_nombre_aval1;
					cmd.Parameters.Add("pa_segundo_nombre_aval1", OracleType.VarChar, 50).Value = solicitud.segundo_nombre_aval1;
					cmd.Parameters.Add("pa_primer_apellido_aval1", OracleType.VarChar, 50).Value = solicitud.primer_apellido_aval1;
					cmd.Parameters.Add("pa_segundo_apellido_aval1", OracleType.VarChar, 50).Value = solicitud.segundo_apellido_aval1;
					cmd.Parameters.Add("pa_sexo_aval1", OracleType.VarChar, 50).Value = solicitud.sexo_aval1;
					cmd.Parameters.Add("pa_dependientes_hijos_aval1", OracleType.Number, 20).Value = solicitud.dependientes_hijos_aval1;
					cmd.Parameters.Add("pa_dependientes_otros_aval1", OracleType.Number, 20).Value = solicitud.dependientes_otros_aval1;
					cmd.Parameters.Add("pa_direccion_res_aval1", OracleType.VarChar, 100).Value = solicitud.direccion_residencia_aval1;
					cmd.Parameters.Add("pa_telefono_fijo_aval1", OracleType.VarChar, 20).Value = solicitud.telefono_fijo_aval1;
					cmd.Parameters.Add("pa_celular_aval1", OracleType.VarChar, 20).Value = solicitud.celular_aval1;
					cmd.Parameters.Add("pa_telefono_adicional1_aval1", OracleType.VarChar, 20).Value = solicitud.telefono_adicional1_aval1;
					cmd.Parameters.Add("pa_telefono_adicional2_aval1", OracleType.VarChar, 20).Value = solicitud.telefono_adicional2_aval1;
					cmd.Parameters.Add("pa_correo_aval1", OracleType.VarChar, 50).Value = solicitud.correo_aval1;
					cmd.Parameters.Add("pa_es_afiliado_aval1", OracleType.VarChar, 1).Value = solicitud.es_afiliado_aval1;
					cmd.Parameters.Add("pa_codigo_cilente_aval1", OracleType.Number).Value = solicitud.codigo_cliente_aval1;
					cmd.Parameters.Add("pa_tipo_empresa_aval1", OracleType.VarChar, 12).Value = solicitud.tipo_empresa_aval1;
					cmd.Parameters.Add("pa_tipo_empresa_otros_aval1", OracleType.VarChar, 50).Value = solicitud.tipo_empresa_especificar_aval1;
					cmd.Parameters.Add("pa_patrono_aval1", OracleType.VarChar, 100).Value = solicitud.patrono_aval1;
					cmd.Parameters.Add("pa_depto_labora_aval1", OracleType.VarChar, 100).Value = solicitud.depto_labora_aval1;
					cmd.Parameters.Add("pa_cargo_aval1", OracleType.VarChar, 100).Value = solicitud.cargo_aval1;
					cmd.Parameters.Add("pa_antiguedad_laboral_aval1", OracleType.VarChar, 20).Value = solicitud.antiguedad_aval1;
					cmd.Parameters.Add("pa_ingresos_aval1", OracleType.Number).Value = solicitud.ingresos_aval1;
					cmd.Parameters.Add("pa_otros_ingresos_aval1", OracleType.Number).Value = solicitud.otros_ingresos_aval1;
					cmd.Parameters.Add("pa_deducciones_aval1", OracleType.Number).Value = solicitud.deducciones_aval1;
					cmd.Parameters.Add("pa_telefono_laboral1_aval1", OracleType.VarChar, 20).Value = solicitud.telefono_laboral1_aval1;
					cmd.Parameters.Add("pa_ext_laboral1_aval1", OracleType.VarChar, 10).Value = solicitud.ext_laboral1_aval1;
					cmd.Parameters.Add("pa_telefono_laboral2_aval1", OracleType.VarChar, 20).Value = solicitud.telefono_laboral2_aval1;
					cmd.Parameters.Add("pa_ext_laboral2_aval1", OracleType.VarChar, 10).Value = solicitud.ext_laboral2_aval1;
					cmd.Parameters.Add("pa_direccion_laboral_aval1", OracleType.VarChar, 100).Value = solicitud.direccion_laboral_aval1;
					cmd.Parameters.Add("pa_correo_laboral_aval1", OracleType.VarChar, 50).Value = solicitud.correo_laboral_aval1;
					cmd.Parameters.Add("pa_nombre_conyuge_aval1", OracleType.VarChar, 50).Value = solicitud.nombre_conyuge_aval1;
					cmd.Parameters.Add("pa_direclab_conyuge_aval1", OracleType.VarChar, 50).Value = solicitud.direclab_conyuge_aval1;
					cmd.Parameters.Add("pa_cargo_conyuge_aval1", OracleType.VarChar, 50).Value = solicitud.cargo_conyuge_aval1;

					#endregion

					#region Aval2

					cmd.Parameters.Add("pa_no_identidad_aval2", OracleType.VarChar, 20).Value = solicitud.no_identificacion_aval2;
					cmd.Parameters.Add("pa_primer_nombre_aval2", OracleType.VarChar, 50).Value = solicitud.primer_nombre_aval2;
					cmd.Parameters.Add("pa_segundo_nombre_aval2", OracleType.VarChar, 50).Value = solicitud.segundo_nombre_aval2;
					cmd.Parameters.Add("pa_primer_apellido_aval2", OracleType.VarChar, 50).Value = solicitud.primer_apellido_aval2;
					cmd.Parameters.Add("pa_segundo_apellido_aval2", OracleType.VarChar, 50).Value = solicitud.segundo_apellido_aval2;
					cmd.Parameters.Add("pa_sexo_aval2", OracleType.VarChar, 50).Value = solicitud.sexo_aval2;
					cmd.Parameters.Add("pa_dependientes_hijos_aval2", OracleType.Number, 20).Value = solicitud.dependientes_hijos_aval2;
					cmd.Parameters.Add("pa_dependientes_otros_aval2", OracleType.Number, 20).Value = solicitud.dependientes_otros_aval2;
					cmd.Parameters.Add("pa_direccion_res_aval2", OracleType.VarChar, 100).Value = solicitud.direccion_residencia_aval2;
					cmd.Parameters.Add("pa_telefono_fijo_aval2", OracleType.VarChar, 20).Value = solicitud.telefono_fijo_aval2;
					cmd.Parameters.Add("pa_celular_aval2", OracleType.VarChar, 20).Value = solicitud.celular_aval2;
					cmd.Parameters.Add("pa_telefono_adicional1_aval2", OracleType.VarChar, 20).Value = solicitud.telefono_adicional1_aval2;
					cmd.Parameters.Add("pa_telefono_adicional2_aval2", OracleType.VarChar, 20).Value = solicitud.telefono_adicional2_aval2;
					cmd.Parameters.Add("pa_correo_aval2", OracleType.VarChar, 50).Value = solicitud.correo_aval2;
					cmd.Parameters.Add("pa_es_afiliado_aval2", OracleType.VarChar, 1).Value = solicitud.es_afiliado_aval2;
					cmd.Parameters.Add("pa_codigo_cilente_aval2", OracleType.Number).Value = solicitud.codigo_cliente_aval2;
					cmd.Parameters.Add("pa_tipo_empresa_aval2", OracleType.VarChar, 12).Value = solicitud.tipo_empresa_aval2;
					cmd.Parameters.Add("pa_tipo_empresa_otros_aval2", OracleType.VarChar, 50).Value = solicitud.tipo_empresa_especificar_aval2;
					cmd.Parameters.Add("pa_patrono_aval2", OracleType.VarChar, 100).Value = solicitud.patrono_aval2;
					cmd.Parameters.Add("pa_depto_labora_aval2", OracleType.VarChar, 100).Value = solicitud.depto_labora_aval2;
					cmd.Parameters.Add("pa_cargo_aval2", OracleType.VarChar, 100).Value = solicitud.cargo_aval2;
					cmd.Parameters.Add("pa_antiguedad_laboral_aval2", OracleType.VarChar, 20).Value = solicitud.antiguedad_aval2;
					cmd.Parameters.Add("pa_ingresos_aval2", OracleType.Number).Value = solicitud.ingresos_aval2;
					cmd.Parameters.Add("pa_otros_ingresos_aval2", OracleType.Number).Value = solicitud.otros_ingresos_aval2;
					cmd.Parameters.Add("pa_deducciones_aval2", OracleType.Number).Value = solicitud.deducciones_aval2;
					cmd.Parameters.Add("pa_telefono_laboral1_aval2", OracleType.VarChar, 20).Value = solicitud.telefono_laboral1_aval2;
					cmd.Parameters.Add("pa_ext_laboral1_aval2", OracleType.VarChar, 10).Value = solicitud.ext_laboral1_aval2;
					cmd.Parameters.Add("pa_telefono_laboral2_aval2", OracleType.VarChar, 20).Value = solicitud.telefono_laboral2_aval2;
					cmd.Parameters.Add("pa_ext_laboral2_aval2", OracleType.VarChar, 10).Value = solicitud.ext_laboral2_aval2;
					cmd.Parameters.Add("pa_direccion_laboral_aval2", OracleType.VarChar, 100).Value = solicitud.direccion_laboral_aval2;
					cmd.Parameters.Add("pa_correo_laboral_aval2", OracleType.VarChar, 50).Value = solicitud.correo_laboral_aval2;
					cmd.Parameters.Add("pa_nombre_conyuge_aval2", OracleType.VarChar, 50).Value = solicitud.nombre_conyuge_aval2;
					cmd.Parameters.Add("pa_direclab_conyuge_aval2", OracleType.VarChar, 50).Value = solicitud.direclab_conyuge_aval2;
					cmd.Parameters.Add("pa_cargo_conyuge_aval2", OracleType.VarChar, 50).Value = solicitud.cargo_conyuge_aval2;

					#endregion

					/*Referencias*/
					cmd.Parameters.Add("pa_xml_referencias", OracleType.LongVarChar).Value = solicitud.xml_referencias;

					#region Parametros calculo de concetracio crediticia
									
					cmd.Parameters.Add("pa_indicador_aplicado", OracleType.Number).Value = solicitud.indicador_aplicado;
					cmd.Parameters.Add("pa_total_capitalvigente_grpfam", OracleType.Number).Value = solicitud.total_capitalvigente_grpfam;
					cmd.Parameters.Add("pa_total_capitalvigente_solici", OracleType.Number).Value = solicitud.total_capitalvigente_solicitante;
					cmd.Parameters.Add("pa_monto_ensolicitud", OracleType.Number).Value = solicitud.monto_ensolicitud;
					cmd.Parameters.Add("pa_monto_excluir_refconsol", OracleType.Number).Value = solicitud.monto_excluir_refconsol;
					cmd.Parameters.Add("pa_total_paraindice", OracleType.Number).Value = solicitud.total_paraindice;
					cmd.Parameters.Add("pa_patrimonio_csf", OracleType.Number).Value = solicitud.patrimonio_csf;
					cmd.Parameters.Add("pa_porcentaje_concentracion", OracleType.Number).Value = solicitud.porcentaje_concentracion;
					cmd.Parameters.Add("pa_limite_indicador", OracleType.Number).Value = solicitud.limite_indicador;
					cmd.Parameters.Add("pa_resultado_evaluacion_ind", OracleType.VarChar, 50).Value = solicitud.resultado_evaluacion_indicador;

					#endregion
					
					cmd.Parameters.Add("pa_edad_presta", OracleType.VarChar, 50).Value = solicitud.EdadPresta;
					cmd.Parameters.Add("pa_tipo_contrato", OracleType.VarChar, 20).Value = solicitud.TipoContrato; //FELVIR01 - 20190620
					cmd.Parameters.Add("pa_rtn_solicitante", OracleType.VarChar, 25).Value = solicitud.RTN; //FELVIR01 - 20190620
					cmd.Parameters.Add("pa_nivel_educativo_cony", OracleType.VarChar, 50).Value = solicitud.NivelEducConyuge;
					//cmd.Parameters.Add("pa_edad_codeudor", OracleType.VarChar, 50).Value = solicitud.EdadCodeudor;
					cmd.Parameters.Add("pa_tipo_vivienda_codeudor", OracleType.VarChar, 10).Value = solicitud.TipoViviendaCodeudor;
					cmd.Parameters.Add("pa_tipo_vivienda_espec_c", OracleType.VarChar, 100).Value = solicitud.DescripcionViviendaCodeudor;
					cmd.Parameters.Add("pa_edad_aval1", OracleType.VarChar, 50).Value = solicitud.EdadAval1;
					cmd.Parameters.Add("pa_tipo_vivienda_aval1", OracleType.VarChar, 10).Value = solicitud.TipoViviendaAval1;
					cmd.Parameters.Add("pa_tipo_vivienda_espec_a1", OracleType.VarChar, 100).Value = solicitud.DescripcionViviendaAval1;
					cmd.Parameters.Add("pa_rtn_aval1", OracleType.VarChar, 25).Value = solicitud.RTN_Aval1;
					cmd.Parameters.Add("pa_edad_aval2", OracleType.VarChar, 50).Value = solicitud.EdadAval2;
					cmd.Parameters.Add("pa_tipo_vivienda_aval2", OracleType.VarChar, 10).Value = solicitud.TipoViviendaAval2;
					cmd.Parameters.Add("pa_tipo_vivienda_espec_a2", OracleType.VarChar, 100).Value = solicitud.DescripcionViviendaAval2;
					cmd.Parameters.Add("pa_rtn_aval2", OracleType.VarChar, 25).Value = solicitud.RTN_Aval2;

					#region Garante Hipotecario

					cmd.Parameters.Add("pa_no_ident_garante", OracleType.VarChar, 25).Value = garante.NoIdentidadGarante;
					cmd.Parameters.Add("pa_codigo_cli_garante", OracleType.Number).Value = garante.CodigoClienteGarante;
					cmd.Parameters.Add("pa_prim_nom_garante", OracleType.VarChar, 50).Value = garante.PrimerNombreGarante;
					cmd.Parameters.Add("pa_seg_nom_garante", OracleType.VarChar, 50).Value = garante.SegundoNombreGarante;
					cmd.Parameters.Add("pa_prim_ape_garante", OracleType.VarChar, 50).Value = garante.PrimerApellidoGarante;
					cmd.Parameters.Add("pa_seg_ape_garante", OracleType.VarChar, 50).Value = garante.SegundoApellidoGarante;
					cmd.Parameters.Add("pa_genero_garante", OracleType.VarChar, 1).Value = garante.GeneroGarante;
					cmd.Parameters.Add("pa_dep_hijos_gar", OracleType.Number).Value = garante.DependientesHijosGarante;
					cmd.Parameters.Add("pa_dep_otros_gar", OracleType.Number).Value = garante.DependientesOtrosGarante;
					cmd.Parameters.Add("pa_direccion_gar", OracleType.VarChar, 100).Value = garante.DireccionGarante;
					cmd.Parameters.Add("pa_tel_fijo_gar", OracleType.VarChar, 20).Value = garante.TelefonoFijoGarante;
					cmd.Parameters.Add("pa_celular_gar", OracleType.VarChar, 20).Value = garante.CelularGarante;
					cmd.Parameters.Add("pa_tel_adic1_gar", OracleType.VarChar, 20).Value = garante.TelefonoAdic1Garante;
					cmd.Parameters.Add("pa_tel_adic2_gar", OracleType.VarChar, 20).Value = garante.TelefonoAdic2Garante;
					cmd.Parameters.Add("pa_correo_garante", OracleType.VarChar, 50).Value = garante.CorreoGarante;
					cmd.Parameters.Add("pa_es_afiliado_g", OracleType.VarChar, 1).Value = garante.EsAfliadoGarante;
					cmd.Parameters.Add("pa_tipo_emp_gara", OracleType.VarChar, 12).Value = garante.TipoEmpresaGarante;
					cmd.Parameters.Add("pa_tipo_emp_otros_g", OracleType.VarChar, 100).Value = garante.TipoEmpresaOtrosGarante;
					cmd.Parameters.Add("pa_patrono_garante", OracleType.VarChar, 100).Value = garante.PatronoGarante;
					cmd.Parameters.Add("pa_depto_lab_gar", OracleType.VarChar, 100).Value = garante.DeptoLaboraGarante;
					cmd.Parameters.Add("pa_cargo_garante", OracleType.VarChar, 100).Value = garante.CargoGarante;
					cmd.Parameters.Add("pa_ant_lab_garante", OracleType.VarChar, 20).Value = garante.AntiguedadLabGarante;
					cmd.Parameters.Add("pa_tel_lab1_gar", OracleType.VarChar, 20).Value = garante.TelLab1Garante;
					cmd.Parameters.Add("pa_ext1_lab_gar", OracleType.VarChar, 20).Value = garante.ExtLab1Garante;
					cmd.Parameters.Add("pa_tel2_lab_gar", OracleType.VarChar, 20).Value = garante.TelLab2Garante;
					cmd.Parameters.Add("pa_ext2_lab_gar", OracleType.VarChar, 20).Value = garante.ExtLab2Garante;
					cmd.Parameters.Add("pa_dir_lab_gar", OracleType.VarChar, 20).Value = garante.DirecLabGarante;
					cmd.Parameters.Add("pa_correo_lab_gar", OracleType.VarChar, 20).Value = garante.CorreoLabgarante;
					cmd.Parameters.Add("pa_nom_cony_gar", OracleType.VarChar, 20).Value = garante.NomConyugeGarante;
					cmd.Parameters.Add("pa_dir_lab_cony_gar", OracleType.VarChar, 20).Value = garante.DirecLabConyuGarante;
					cmd.Parameters.Add("pa_cargo_cony_gar", OracleType.VarChar, 20).Value = garante.CargoLabConyuGarante;
					cmd.Parameters.Add("pa_nom_ref1_gar", OracleType.VarChar, 20).Value = garante.NomRef1Garante;
					cmd.Parameters.Add("pa_dir_ref1_gar", OracleType.VarChar, 20).Value = garante.DirRef1Garante;
					cmd.Parameters.Add("pa_tel_ref1_gar", OracleType.VarChar, 20).Value = garante.TelRef1Garante;
					cmd.Parameters.Add("pa_loc_ref1_gar", OracleType.VarChar, 20).Value = garante.LocalRef1Garante;
					cmd.Parameters.Add("pa_cel_ref1_gar", OracleType.VarChar, 20).Value = garante.CelularRef1Garante;
					cmd.Parameters.Add("pa_nom_ref2_gar", OracleType.VarChar, 20).Value = garante.NomRef2Garante;
					cmd.Parameters.Add("pa_dir_ref2_gar", OracleType.VarChar, 20).Value = garante.DirRef2Garante;
					cmd.Parameters.Add("pa_tel_ref2_gar", OracleType.VarChar, 20).Value = garante.TelRef2Garante;
					cmd.Parameters.Add("pa_loc_ref2_gar", OracleType.VarChar, 20).Value = garante.LocalRef2Garante;
					cmd.Parameters.Add("pa_cel_ref2_gar", OracleType.VarChar, 20).Value = garante.CelularRef2Garante;
					cmd.Parameters.Add("pa_nom_ref3_gar", OracleType.VarChar, 20).Value = garante.NomRef3Garante;
					cmd.Parameters.Add("pa_dir_ref3_gar", OracleType.VarChar, 20).Value = garante.DirRef3Garante;
					cmd.Parameters.Add("pa_tel_ref3_gar", OracleType.VarChar, 20).Value = garante.TelRef3Garante;

					#endregion

					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();

					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public solicitud_credito ObtenerSolicitudCredito(Int32 p_no_solicitud)
		{
			solicitud_credito sol = new solicitud_credito();
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select nombres,primer_apellido,segundo_apellido,apellido_de_casada,
                                          decode(sexo,'M','Masculino','F','Femenino','') sexo,
                                          decode(upper(estado_civil),'C','Casado','S','Soltero','U','Union Libre','D','Divorciado','V','Viudo','Sin definir') estado_civil,                                          
                                          TRUNC(fecha_de_nacimiento) fecha_de_nacimiento,
                                          nacionalidad,
                                          cl.fecha_inicio_relacion,
                                          DCS_F_AFILIADO_X_PLANILLA(cl.codigo_cliente) notaria,
                                          sa.desc_sub_aplicacion,
                                          mo.desc_moneda,      
                                          modo_transunion,
                                          s2.destino,
                                          monto_aprobado,
                                          DE.DESCRIPCION_DESTINO descripcion_destino,
                                          fecha_creacion_tu,
                                          s2.*,
                                          s1.*,
                                          nvl(sind.indicador_aplicado,0) indicador_aplicado,
                                          nvl(sind.total_capitalvigente_grpfam,0) total_capitalvigente_grpfam,
                                          nvl(sind.total_capitalvigente_solic,0) total_capitalvigente_solic,
                                          nvl(sind.monto_solicitado,0) monto_solicitado,
                                          nvl(sind.monto_excluir_refcons,0) monto_excluir_refcons,
                                          nvl(sind.total,0) total,
                                          nvl(sind.patrimonio_csf,0) patrimonio_csf,
                                          nvl(sind.porcentaje_concentracion,0) porcentaje_concentracion,
                                          nvl(sind.limite_indicador,0) limite_indicador,
                                          resultado_evaluacion
                                     from dcs_solicitudes2 s2,
                                          dcs_solicitudes s1,
                                          dcs_solicitudes_indicador_deud sind,
                                          dcs_wf_destinos_credito de, 
                                          mgi_sub_aplicaciones sa, 
                                          mgi_monedas mo,                                         
                                          mgi_clientes cl,
                                          mgi_paises pa                      
                                    Where cl.codigo_empresa=cr_globales.c_empresa
                                      and cl.codigo_empresa=sa.codigo_empresa                                      
                                      and s2.codigo_cliente=cl.codigo_cliente
                                      and s2.destino=de.destino_id(+)
                                      and s1.no_solicitud=sind.no_solicitud(+)
                                      and s1.no_solicitud=s2.no_solicitud                                      
                                      and s2.codigo_sub_aplicacion=sa.codigo_sub_aplicacion
                                      and SA.CODIGO_MONEDA=mo.codigo_moneda
                                      and cl.codigo_pais=pa.codigo_pais(+) 
                                      and s2.no_solicitud=:pa_no_solicitud";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_no_solicitud", p_no_solicitud);
					OracleDataAdapter da = new OracleDataAdapter(cmd);
					try
					{
						da.Fill(dt);

						sol.modo_transunion = dt.Rows[0]["modo_transunion"].ToString();
						sol.application_id = Int32.Parse(dt.Rows[0]["application_id"].ToString());
						sol.no_solicitud = Int32.Parse(dt.Rows[0]["no_solicitud"].ToString());
						sol.codigo_agencia = Int32.Parse(dt.Rows[0]["codigo_agencia_origen"].ToString());
						sol.codigo_sub_aplicacion = Int16.Parse(dt.Rows[0]["codigo_sub_aplicacion"].ToString());
						sol.desc_sub_aplicacion = dt.Rows[0]["desc_sub_aplicacion"].ToString();
						sol.fuente_financiamiento = Int16.Parse(dt.Rows[0]["codigo_fuente"].ToString());
						sol.codigo_moneda = Int16.Parse(dt.Rows[0]["codigo_moneda"].ToString());
						sol.fecha_solicitud = DateTime.Parse(dt.Rows[0]["fecha_presentacion"].ToString());
						sol.desc_moneda = dt.Rows[0]["desc_moneda"].ToString();
						sol.oficial_servicio = dt.Rows[0]["oficial_servicio"].ToString();
						sol.monto_solicitado = decimal.Parse(dt.Rows[0]["monto_solicitado"].ToString());
						sol.plazo = Int16.Parse(dt.Rows[0]["plazo"].ToString());
						//sol.tasa = decimal.Parse(dt.Rows[0]["tasa"].ToString());
						//Para corregir problema del punto decimal o coma de la tasa gparedes
						CultureInfo culture = CultureInfo.CreateSpecificCulture("es-HN");
						sol.tasa = Convert.ToDecimal(dt.Rows[0]["tasa"].ToString().Replace('.', ','), System.Globalization.CultureInfo.GetCultureInfo("no"));

						sol.es_consolidacion = dt.Rows[0]["es_consolidacion"].ToString();
						sol.descripcion_garantia = dt.Rows[0]["descripcion_garantia"].ToString();
						sol.monto_cuota_consolidar = decimal.Parse(dt.Rows[0]["monto_cuota_consolidar"].ToString());
						sol.monto_balance_consolidar = decimal.Parse(dt.Rows[0]["monto_balance_consolidar"].ToString());
						sol.xml_cuotas_buro = dt.Rows[0]["xml_cuotas_buro"].ToString();
						sol.condicion_vehiculo = dt.Rows[0]["condicion_vehiculo"].ToString();
						DateTime vl_inicio_relacion;
						DateTime.TryParse(dt.Rows[0]["fecha_inicio_relacion"].ToString(), out vl_inicio_relacion);
						sol.fecha_ingreso_coop = vl_inicio_relacion;
						sol.valor_vehiculo = decimal.Parse(dt.Rows[0]["valor_veh"].ToString());
						sol.destino_credito = dt.Rows[0]["destino"].ToString();
						sol.descripcion_destino = dt.Rows[0]["descripcion_destino"].ToString();
						sol.requiere_codeudor = dt.Rows[0]["requiere_codeudor"].ToString();
						sol.requiere_aval1 = dt.Rows[0]["requiere_aval1"].ToString();
						sol.requiere_aval2 = dt.Rows[0]["requiere_aval2"].ToString();
						sol.derecho_ganado = decimal.Parse(dt.Rows[0]["derecho_ganado"].ToString());
						sol.monto_cuotas_vencimiento = decimal.Parse(dt.Rows[0]["monto_cuotas_vencimiento"].ToString());
						sol.complemento_aportaciones = decimal.Parse(dt.Rows[0]["complemento_aportaciones"].ToString());
						sol.deudas_canceladas_solic = decimal.Parse(dt.Rows[0]["deudas_canceladas_solic"].ToString());
						sol.deudas_canceladas_codeud = decimal.Parse(dt.Rows[0]["deudas_canceladas_codeud"].ToString());
						sol.deudas_canceladas_aval1 = decimal.Parse(dt.Rows[0]["deudas_canceladas_aval1"].ToString());
						sol.deudas_canceladas_aval2 = decimal.Parse(dt.Rows[0]["deudas_canceladas_aval2"].ToString());

						#region Datos Solicitante del Credito

						sol.no_identificacion = dt.Rows[0]["numero_identificacion"].ToString();
						sol.codigo_cliente = Int32.Parse(dt.Rows[0]["codigo_cliente"].ToString());
						sol.nombres = dt.Rows[0]["nombres"].ToString();
						sol.primer_apellido = dt.Rows[0]["primer_apellido"].ToString();
						sol.segundo_apellido = dt.Rows[0]["segundo_apellido"].ToString();
						sol.apellido_casada = dt.Rows[0]["apellido_de_casada"].ToString();
						sol.sexo = dt.Rows[0]["sexo"].ToString();
						sol.estado_civil = dt.Rows[0]["estado_civil"].ToString();
						if (dt.Rows[0]["notaria"].ToString() == "S")
						{
							sol.ventanilla_planilla = "PLANILLA";
						}
						else
							sol.ventanilla_planilla = "VENTANILLA";
						CultureInfo provider = CultureInfo.InvariantCulture;

						sol.fecha_nacimiento = ((DateTime)dt.Rows[0]["fecha_de_nacimiento"]).Date;
						sol.nacionalidad = dt.Rows[0]["nacionalidad"].ToString();
						sol.lugar_nacimiento = dt.Rows[0]["lugar_nacimiento"].ToString();
						sol.nivel_educativo = dt.Rows[0]["nivel_educativo"].ToString();
						sol.profesion_oficio = dt.Rows[0]["profesion_oficio"].ToString();
						sol.tipo_vivienda = dt.Rows[0]["tipo_vivienda"].ToString();
						sol.tipo_vivienda_especificar = dt.Rows[0]["tipo_vivienda_especifica"].ToString(); ;
						sol.direccion_residencia = dt.Rows[0]["direccion_residencia"].ToString();
						sol.telefono_fijo = dt.Rows[0]["telefono_fijo"].ToString();
						sol.telefono_celular = dt.Rows[0]["celular"].ToString();
						sol.telefono_adicional1 = dt.Rows[0]["telefono_adicional1"].ToString();
						sol.telefono_adicional2 = dt.Rows[0]["telefono_adicional2"].ToString();
						sol.correo_electronico = dt.Rows[0]["correo_electronico"].ToString();
						sol.dependientes_hijos = Convert.ToInt16(dt.Rows[0]["dependientes_hijos"].ToString());
						sol.dependientes_otros = Convert.ToInt16(dt.Rows[0]["dependientes_otros"].ToString());
						sol.tipo_empresa = dt.Rows[0]["tipo_empresa"].ToString();
						sol.tipo_empresa_especificar = dt.Rows[0]["tipo_empresa_otros"].ToString();
						sol.patrono = dt.Rows[0]["patrono"].ToString();
						sol.depto_labora = dt.Rows[0]["depto_labora"].ToString();
						sol.cargo = dt.Rows[0]["cargo"].ToString();
						sol.antiguedad_laboral = dt.Rows[0]["antiguedad_laboral"].ToString();
						sol.ingresos = decimal.Parse(dt.Rows[0]["ingresos"].ToString());
						sol.otros_ingresos = decimal.Parse(dt.Rows[0]["otros_ingresos"].ToString());
						sol.deducciones = decimal.Parse(dt.Rows[0]["deducciones"].ToString());
						sol.telefono_laboral1 = dt.Rows[0]["tel_laboral1"].ToString();
						sol.ext_laboral1 = dt.Rows[0]["ext_laboral1"].ToString();
						sol.telefono_laboral2 = dt.Rows[0]["tel_laboral2"].ToString();
						sol.ext_laboral2 = dt.Rows[0]["ext_laboral2"].ToString();
						sol.direccion_laboral = dt.Rows[0]["direccion_laboral"].ToString();
						sol.correo_laboral = dt.Rows[0]["correo_laboral"].ToString();
						if (!string.IsNullOrEmpty(dt.Rows[0]["EDAD_PRESTA"].ToString()))
							sol.edad = Convert.ToInt16(dt.Rows[0]["EDAD_PRESTA"].ToString());
						else
							sol.edad = 0;

						#endregion

						#region Datos Conyuge

						sol.no_identificacion_conyuge = dt.Rows[0]["no_identidad_conyuge"].ToString();
						sol.primer_nombre_conyuge = dt.Rows[0]["primer_nombre_conyuge"].ToString();
						sol.segundo_nombre_conyuge = dt.Rows[0]["segundo_nombre_conyuge"].ToString();
						sol.primer_apellido_conyuge = dt.Rows[0]["primer_apellido_conyuge"].ToString(); ;
						sol.segundo_apellido_conyuge = dt.Rows[0]["segundo_apellido_conyuge"].ToString(); ;
						sol.sexo_conyuge = dt.Rows[0]["sexo_conyuge"].ToString();
						sol.dependientes_hijos_conyuge = Convert.ToInt16(dt.Rows[0]["dependientes_hijos_conyuge"].ToString());
						sol.dependientes_otros_conyuge = Convert.ToInt16(dt.Rows[0]["dependientes_otros_conyuge"].ToString());
						sol.direccion_residencia_conyuge = dt.Rows[0]["direccion_res_conyuge"].ToString();
						sol.telefono_fijo_conyuge = dt.Rows[0]["telefono_fijo_conyuge"].ToString();
						sol.celular_conyuge = dt.Rows[0]["celular_conyuge"].ToString();
						sol.telefono_adicional1_conyuge = dt.Rows[0]["telefono_adicional1_conyuge"].ToString();
						sol.telefono_adicional2_conyuge = dt.Rows[0]["telefono_adicional2_conyuge"].ToString();
						sol.correo_conyuge = dt.Rows[0]["correo_conyuge"].ToString();
						sol.es_afiliado_conyuge = dt.Rows[0]["es_afiliado_conyuge"].ToString();
						if (!string.IsNullOrEmpty(dt.Rows[0]["codigo_cliente_conyuge"].ToString()))
							sol.codigo_cliente_conyuge = dt.Rows[0]["codigo_cliente_conyuge"].ToString();
						else
							sol.codigo_cliente_conyuge = "0";

						sol.tipo_empresa_conyuge = dt.Rows[0]["tipo_empresa_conyuge"].ToString();
						sol.tipo_empresa_especificar_conyuge = dt.Rows[0]["tipo_empresa_otros_conyuge"].ToString();
						sol.patrono_conyuge = dt.Rows[0]["patrono_conyuge"].ToString();
						sol.depto_labora_conyuge = dt.Rows[0]["depto_labora_conyuge"].ToString();
						sol.cargo_conyuge = dt.Rows[0]["cargo_conyuge"].ToString();
						sol.antiguedad_conyuge = dt.Rows[0]["antiguedad_laboral_conyuge"].ToString();
						sol.ingresos_conyuge = Convert.ToDecimal(dt.Rows[0]["ingresos_conyuge"].ToString());
						sol.otros_ingresos_conyuge = Convert.ToDecimal(dt.Rows[0]["otros_ingresos_conyuge"].ToString());
						sol.deducciones_conyuge = Convert.ToDecimal(dt.Rows[0]["deducciones_conyuge"].ToString());
						sol.telefono_laboral1_conyuge = dt.Rows[0]["telefono_laboral1_conyuge"].ToString(); ;
						sol.ext_laboral1_conyuge = dt.Rows[0]["ext_laboral1_conyuge"].ToString(); ;
						sol.telefono_laboral2_conyuge = dt.Rows[0]["telefono_laboral2_conyuge"].ToString();
						sol.ext_laboral2_conyuge = dt.Rows[0]["ext_laboral2_conyuge"].ToString();
						sol.direccion_laboral_conyuge = dt.Rows[0]["direccion_laboral_conyuge"].ToString();
						sol.correo_laboral_conyuge = dt.Rows[0]["correo_laboral_conyuge"].ToString();

						#endregion

						#region Datos Codeudor

						sol.no_identificacion_codeudor = dt.Rows[0]["no_identidad_codeudor"].ToString();
						sol.primer_nombre_codeudor = dt.Rows[0]["primer_nombre_codeudor"].ToString();
						sol.segundo_nombre_codeudor = dt.Rows[0]["segundo_nombre_codeudor"].ToString();
						sol.primer_apellido_codeudor = dt.Rows[0]["primer_apellido_codeudor"].ToString(); ;
						sol.segundo_apellido_codeudor = dt.Rows[0]["segundo_apellido_codeudor"].ToString(); ;
						sol.sexo_codeudor = dt.Rows[0]["sexo_codeudor"].ToString();
						sol.dependientes_hijos_codeudor = Convert.ToInt16(dt.Rows[0]["dependientes_hijos_codeudor"].ToString());
						sol.dependientes_otros_codeudor = Convert.ToInt16(dt.Rows[0]["dependientes_otros_codeudor"].ToString());
						sol.direccion_residencia_codeudor = dt.Rows[0]["direccion_res_codeudor"].ToString();
						sol.telefono_fijo_codeudor = dt.Rows[0]["telefono_fijo_codeudor"].ToString();
						sol.celular_codeudor = dt.Rows[0]["celular_codeudor"].ToString();
						sol.telefono_adicional1_codeudor = dt.Rows[0]["telefono_adicional1_codeudor"].ToString();
						sol.telefono_adicional2_codeudor = dt.Rows[0]["telefono_adicional2_codeudor"].ToString();
						sol.correo_codeudor = dt.Rows[0]["correo_codeudor"].ToString();
						sol.es_afiliado_codeudor = dt.Rows[0]["es_afiliado_codeudor"].ToString();
						if (!string.IsNullOrEmpty(dt.Rows[0]["codigo_cliente_codeudor"].ToString()))
							sol.codigo_cliente_codeudor = dt.Rows[0]["codigo_cliente_codeudor"].ToString();
						else
							sol.codigo_cliente_codeudor = "0";
						sol.tipo_empresa_codeudor = dt.Rows[0]["tipo_empresa_codeudor"].ToString();
						sol.tipo_empresa_especificar_codeudor = dt.Rows[0]["tipo_empresa_otros_codeudor"].ToString();
						sol.patrono_codeudor = dt.Rows[0]["patrono_codeudor"].ToString();
						sol.depto_labora_codeudor = dt.Rows[0]["depto_labora_codeudor"].ToString();
						sol.cargo_codeudor = dt.Rows[0]["cargo_codeudor"].ToString();
						sol.antiguedad_codeudor = dt.Rows[0]["antiguedad_laboral_codeudor"].ToString();
						sol.ingresos_codeudor = Convert.ToDecimal(dt.Rows[0]["ingresos_codeudor"].ToString());
						sol.otros_ingresos_codeudor = Convert.ToDecimal(dt.Rows[0]["otros_ingresos_codeudor"].ToString());
						sol.deducciones_codeudor = Convert.ToDecimal(dt.Rows[0]["deducciones_codeudor"].ToString());
						sol.telefono_laboral1_codeudor = dt.Rows[0]["telefono_laboral1_codeudor"].ToString(); ;
						sol.ext_laboral1_codeudor = dt.Rows[0]["ext_laboral1_codeudor"].ToString(); ;
						sol.telefono_laboral2_codeudor = dt.Rows[0]["telefono_laboral2_codeudor"].ToString();
						sol.ext_laboral2_codeudor = dt.Rows[0]["ext_laboral2_codeudor"].ToString();
						sol.direccion_laboral_codeudor = dt.Rows[0]["direccion_laboral_codeudor"].ToString(); ;
						sol.correo_laboral_codeudor = dt.Rows[0]["correo_laboral_codeudor"].ToString();
						sol.nombre_conyuge_codeudor = dt.Rows[0]["nombre_conyuge_codeudor"].ToString();
						sol.direclab_conyuge_codeudor = dt.Rows[0]["direc_laboral_conyuge_codeudor"].ToString();
						sol.cargo_conyuge_codeudor = dt.Rows[0]["cargo_conyuge_codeudor"].ToString();
						//FELVIR01-20190619
						sol.nombre_conyuge_codeudor = dt.Rows[0]["NOMBRE_CONYUGE_CODEUDOR"].ToString();
						sol.cargo_conyuge_codeudor = dt.Rows[0]["CARGO_CONYUGE_CODEUDOR"].ToString();
						sol.direclab_conyuge_codeudor = dt.Rows[0]["DIREC_LABORAL_CONYUGE_CODEUDOR"].ToString();
						if (!string.IsNullOrEmpty(dt.Rows[0]["EDAD_CODEUDOR"].ToString()))
							sol.EdadCodeudor = Convert.ToInt16(dt.Rows[0]["EDAD_CODEUDOR"].ToString());
						else
							sol.EdadCodeudor = 0;

						#endregion

						#region Aval 1

						sol.no_identificacion_aval1 = dt.Rows[0]["no_identidad_aval1"].ToString();
						sol.primer_nombre_aval1 = dt.Rows[0]["primer_nombre_aval1"].ToString();
						sol.segundo_nombre_aval1 = dt.Rows[0]["segundo_nombre_aval1"].ToString();
						sol.primer_apellido_aval1 = dt.Rows[0]["primer_apellido_aval1"].ToString(); ;
						sol.segundo_apellido_aval1 = dt.Rows[0]["segundo_apellido_aval1"].ToString(); ;
						sol.sexo_aval1 = dt.Rows[0]["sexo_aval1"].ToString();
						sol.dependientes_hijos_aval1 = Convert.ToInt16(dt.Rows[0]["dependientes_hijos_aval1"].ToString());
						sol.dependientes_otros_aval1 = Convert.ToInt16(dt.Rows[0]["dependientes_otros_aval1"].ToString());
						sol.direccion_residencia_aval1 = dt.Rows[0]["direccion_res_aval1"].ToString();
						sol.telefono_fijo_aval1 = dt.Rows[0]["telefono_fijo_aval1"].ToString();
						sol.celular_aval1 = dt.Rows[0]["celular_aval1"].ToString();
						sol.telefono_adicional1_aval1 = dt.Rows[0]["telefono_adicional1_aval1"].ToString();
						sol.telefono_adicional2_aval1 = dt.Rows[0]["telefono_adicional2_aval1"].ToString();
						sol.correo_aval1 = dt.Rows[0]["correo_aval1"].ToString();
						sol.es_afiliado_aval1 = dt.Rows[0]["es_afiliado_aval1"].ToString();
						if (!string.IsNullOrEmpty(dt.Rows[0]["codigo_cliente_aval1"].ToString()))
							sol.codigo_cliente_aval1 = dt.Rows[0]["codigo_cliente_aval1"].ToString();
						else
							sol.codigo_cliente_aval1 = "0";
						sol.tipo_empresa_aval1 = dt.Rows[0]["tipo_empresa_aval1"].ToString();
						sol.tipo_empresa_especificar_aval1 = dt.Rows[0]["tipo_empresa_otros_aval1"].ToString();
						sol.patrono_aval1 = dt.Rows[0]["patrono_aval1"].ToString();
						sol.depto_labora_aval1 = dt.Rows[0]["depto_labora_aval1"].ToString();
						sol.cargo_aval1 = dt.Rows[0]["cargo_aval1"].ToString();
						sol.antiguedad_aval1 = dt.Rows[0]["antiguedad_laboral_aval1"].ToString();
						sol.ingresos_aval1 = Convert.ToDecimal(dt.Rows[0]["ingresos_aval1"].ToString());
						sol.otros_ingresos_aval1 = Convert.ToDecimal(dt.Rows[0]["otros_ingresos_aval1"].ToString());
						sol.deducciones_aval1 = Convert.ToDecimal(dt.Rows[0]["deducciones_aval1"].ToString());
						sol.telefono_laboral1_aval1 = dt.Rows[0]["telefono_laboral1_aval1"].ToString(); ;
						sol.ext_laboral1_aval1 = dt.Rows[0]["ext_laboral1_aval1"].ToString(); ;
						sol.telefono_laboral2_aval1 = dt.Rows[0]["telefono_laboral2_aval1"].ToString();
						sol.ext_laboral2_aval1 = dt.Rows[0]["ext_laboral2_aval1"].ToString();
						sol.direccion_laboral_aval1 = dt.Rows[0]["direccion_laboral_aval1"].ToString(); ;
						sol.correo_laboral_aval1 = dt.Rows[0]["correo_laboral_aval1"].ToString();
						sol.nombre_conyuge_aval1 = dt.Rows[0]["nombre_conyuge_aval1"].ToString();
						sol.direclab_conyuge_aval1 = dt.Rows[0]["direc_laboral_conyuge_aval1"].ToString();
						sol.cargo_conyuge_aval1 = dt.Rows[0]["cargo_conyuge_aval1"].ToString();
						//FELVIR01-20190619
						sol.nombre_conyuge_aval1 = dt.Rows[0]["NOMBRE_CONYUGE_AVAL1"].ToString();
						sol.cargo_conyuge_aval1 = dt.Rows[0]["CARGO_CONYUGE_AVAL1"].ToString();
						sol.direclab_conyuge_aval1 = dt.Rows[0]["DIREC_LABORAL_CONYUGE_AVAL1"].ToString();
						if (!string.IsNullOrEmpty(dt.Rows[0]["EDAD_AVAL1"].ToString()))
							sol.EdadAval1 = Convert.ToInt16(dt.Rows[0]["EDAD_AVAL1"].ToString());
						else
							sol.EdadAval1 = 0;

						#endregion

						#region Aval 2

						sol.no_identificacion_aval2 = dt.Rows[0]["no_identidad_aval2"].ToString();
						sol.primer_nombre_aval2 = dt.Rows[0]["primer_nombre_aval2"].ToString();
						sol.segundo_nombre_aval2 = dt.Rows[0]["segundo_nombre_aval2"].ToString();
						sol.primer_apellido_aval2 = dt.Rows[0]["primer_apellido_aval2"].ToString(); ;
						sol.segundo_apellido_aval2 = dt.Rows[0]["segundo_apellido_aval2"].ToString(); ;
						sol.sexo_aval2 = dt.Rows[0]["sexo_aval2"].ToString();
						sol.dependientes_hijos_aval2 = Convert.ToInt16(dt.Rows[0]["dependientes_hijos_aval2"].ToString());
						sol.dependientes_otros_aval2 = Convert.ToInt16(dt.Rows[0]["dependientes_otros_aval2"].ToString());
						sol.direccion_residencia_aval2 = dt.Rows[0]["direccion_res_aval2"].ToString();
						sol.telefono_fijo_aval2 = dt.Rows[0]["telefono_fijo_aval2"].ToString();
						sol.celular_aval2 = dt.Rows[0]["celular_aval2"].ToString();
						sol.telefono_adicional1_aval2 = dt.Rows[0]["telefono_adicional1_aval2"].ToString();
						sol.telefono_adicional2_aval2 = dt.Rows[0]["telefono_adicional2_aval2"].ToString();
						sol.correo_aval2 = dt.Rows[0]["correo_aval2"].ToString();
						sol.es_afiliado_aval2 = dt.Rows[0]["es_afiliado_aval2"].ToString();
						if (!string.IsNullOrEmpty(dt.Rows[0]["codigo_cliente_aval2"].ToString()))
							sol.codigo_cliente_aval2 = dt.Rows[0]["codigo_cliente_aval2"].ToString();
						else
							sol.codigo_cliente_aval2 = "0";
						sol.tipo_empresa_aval2 = dt.Rows[0]["tipo_empresa_aval2"].ToString();
						sol.tipo_empresa_especificar_aval2 = dt.Rows[0]["tipo_empresa_otros_aval2"].ToString();
						sol.patrono_aval2 = dt.Rows[0]["patrono_aval2"].ToString();
						sol.depto_labora_aval2 = dt.Rows[0]["depto_labora_aval2"].ToString();
						sol.cargo_aval2 = dt.Rows[0]["cargo_aval2"].ToString();
						sol.antiguedad_aval2 = dt.Rows[0]["antiguedad_laboral_aval2"].ToString();
						sol.ingresos_aval2 = Convert.ToDecimal(dt.Rows[0]["ingresos_aval2"].ToString());
						sol.otros_ingresos_aval2 = Convert.ToDecimal(dt.Rows[0]["otros_ingresos_aval2"].ToString());
						sol.deducciones_aval2 = Convert.ToDecimal(dt.Rows[0]["deducciones_aval2"].ToString());
						sol.telefono_laboral1_aval2 = dt.Rows[0]["telefono_laboral1_aval2"].ToString(); ;
						sol.ext_laboral1_aval2 = dt.Rows[0]["ext_laboral1_aval2"].ToString(); ;
						sol.telefono_laboral2_aval2 = dt.Rows[0]["telefono_laboral2_aval2"].ToString();
						sol.ext_laboral2_aval2 = dt.Rows[0]["ext_laboral2_aval2"].ToString();
						sol.direccion_laboral_aval2 = dt.Rows[0]["direccion_laboral_aval2"].ToString(); ;
						sol.correo_laboral_aval2 = dt.Rows[0]["correo_laboral_aval2"].ToString();
						sol.nombre_conyuge_aval2 = dt.Rows[0]["nombre_conyuge_aval2"].ToString();
						sol.direclab_conyuge_aval2 = dt.Rows[0]["direc_laboral_conyuge_aval2"].ToString();
						sol.cargo_conyuge_aval2 = dt.Rows[0]["cargo_conyuge_aval2"].ToString();
						//FELVIR01-20190619
						sol.nombre_conyuge_aval2 = dt.Rows[0]["CARGO_CONYUGE_AVAL2"].ToString();
						sol.cargo_conyuge_aval2 = dt.Rows[0]["CARGO_CONYUGE_AVAL1"].ToString();
						sol.direclab_conyuge_aval2 = dt.Rows[0]["DIREC_LABORAL_CONYUGE_AVAL2"].ToString();
						if (!string.IsNullOrEmpty(dt.Rows[0]["EDAD_AVAL2"].ToString()))
							sol.EdadAval2 = Convert.ToInt16(dt.Rows[0]["EDAD_AVAL2"].ToString());
						else
							sol.EdadAval2 = 0;

						#endregion*/

						sol.banderin_id = int.Parse(dt.Rows[0]["banderin_id"].ToString());
						sol.monto_aprobado = decimal.Parse(dt.Rows[0]["monto_aprobado"].ToString());
						sol.plazo_aprobado = int.Parse(dt.Rows[0]["plazo_aprobado"].ToString());
						sol.tasa_aprobada = decimal.Parse(dt.Rows[0]["tasa_aprobada"].ToString());
						sol.instrucciones_desembolso = dt.Rows[0]["instrucciones_desembolso"].ToString();
						sol.estado_solicitud_id = int.Parse(dt.Rows[0]["estado_solicitud_id"].ToString());
						try
						{

							sol.fecha_creacion_tu = DateTime.Parse(dt.Rows[0]["fecha_creacion_tu"].ToString());
						}
						catch
						{

						}


						/*Concentracion de deuda*/
						sol.indicador_aplicado = Int16.Parse(dt.Rows[0]["indicador_aplicado"].ToString());
						sol.total_capitalvigente_grpfam = decimal.Parse(dt.Rows[0]["total_capitalvigente_grpfam"].ToString());
						sol.total_capitalvigente_solicitante = decimal.Parse(dt.Rows[0]["total_capitalvigente_solic"].ToString());
						sol.monto_ensolicitud = decimal.Parse(dt.Rows[0]["monto_solicitado"].ToString());
						sol.monto_excluir_refconsol = decimal.Parse(dt.Rows[0]["monto_excluir_refcons"].ToString());
						sol.total_paraindice = decimal.Parse(dt.Rows[0]["total"].ToString());
						sol.patrimonio_csf = decimal.Parse(dt.Rows[0]["patrimonio_csf"].ToString());
						sol.porcentaje_concentracion = decimal.Parse(dt.Rows[0]["porcentaje_concentracion"].ToString());
						sol.limite_indicador = decimal.Parse(dt.Rows[0]["limite_indicador"].ToString());
						sol.resultado_evaluacion_indicador = dt.Rows[0]["resultado_evaluacion"].ToString();




					}
					catch (Exception ex)
					{
						throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}
			return sol;

		}

		/*----------------------------------------------------------*/

		//Listo
		public bool QuitarSolicDeCarperta(Int32 p_no_sol, Int16 p_carpeta_id)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"DELETE FROM dcs_carpetas_solicitudes where carpeta_id=:pa_carpeta_id and no_solicitud=:pa_no_solicitud";



					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.CommandType = CommandType.Text;
					cmd.Parameters.Add("pa_carpeta_id", OracleType.Number).Value = p_carpeta_id;
					cmd.Parameters.Add("pa_no_solicitud", OracleType.Number).Value = p_no_sol;
					ConexionOracle.Open();
					cmd.ExecuteNonQuery();


					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}
			return vl_retorno;
		}

		//Listo
		public bool ActualizarComentariosFilial(Int32 p_no_solicitud, string p_comentarios)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"DCS_P_PONER_COMENTARIOS_FILIAL";

					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_no_solicitud", OracleType.Number).Value = p_no_solicitud;
					cmd.Parameters.Add("pa_comentarios_filial", OracleType.VarChar, 500).Value = p_comentarios;
					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();
					p_no_solicitud = Int32.Parse(cmd.Parameters[0].Value.ToString());

					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public bool ActualizarEstadoSolicitud(Int32 p_no_solicitud, int p_motivo_cierre_id, int p_estado_id, string p_comentarios)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"DCS_P_CAMBIAR_ESTADO_SOLICITUD";

					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_no_solicitud", OracleType.Number).Value = p_no_solicitud;
					cmd.Parameters.Add("pa_estado_id", OracleType.Number).Value = p_estado_id;
					cmd.Parameters.Add("pa_comentario", OracleType.VarChar, 1000).Value = p_comentarios;
					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();
					p_no_solicitud = Int32.Parse(cmd.Parameters[0].Value.ToString());

					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public bool ExisteUsuarioEnResoluciones(string p_usuario_comite, Int32 p_no_solicitud)
		{
			bool vl_retorno = false;

			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select count(*) n 
                                     From dcs_solicitudes_aprobaciones 
                                    Where usuario_comite=:pa_usuario_comite
                                      and no_solicitud=:pa_no_solicitud";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_usuario_comite", p_usuario_comite);
					cmd.Parameters.AddWithValue("pa_no_solicitud", p_no_solicitud);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = false;
						float vl_n = 0;
						float.TryParse(dr["n"].ToString(), out vl_n);
						if (vl_n > 0)
						{
							vl_retorno = true;
						}
					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public string ObtenerApplicationIDxSolicitud(Int32 p_no_solicitud)
		{
			string vl_retorno = "0";

			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select application_id from dcs_solicitudes2 where no_solicitud=:pa_no_solicitud";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_no_solicitud", p_no_solicitud);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = dr["application_id"].ToString();

					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public string ObtenerFecha_creacion_tu(Int32 p_no_solicitud)
		{
			string vl_retorno = "0";
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select fecha_creacion_tu from dcs_solicitudes where no_solicitud=:pa_no_solicitud";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_no_solicitud", p_no_solicitud);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = dr["fecha_creacion_tu"].ToString();

					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public string ObtenerPermitirModifAprobxSolicitud(Int32 p_no_solicitud)
		{
			string vl_retorno = "0";

			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select permitir_modif_aprob from dcs_solicitudes where no_solicitud=:pa_no_solicitud";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_no_solicitud", p_no_solicitud);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = dr["permitir_modif_aprob"].ToString();

					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public string ObtenerXmlxSolicitud(Int32 p_no_solicitud)
		{
			string vl_retorno = "";
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select xml_enviado 
                                     from dcs_solicitudes2_resp_trans 
                                    where no_solicitud=:pa_no_solicitud
                                      and no_entrada=(select max(no_entrada) from dcs_solicitudes2_resp_trans where no_solicitud=:pa_no_solicitud)";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_no_solicitud", p_no_solicitud);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = dr["xml_enviado"].ToString();

					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public string ObtenerNombreAgencia(Int32 p_codigo_agencia)
		{
			string vl_retorno = "0";

			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select InitCap(nombre_agencia) nombre_agencia 
                                     from mgi_agencias 
                                    Where codigo_Agencia=:pa_codigo_agencia";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_codigo_agencia", p_codigo_agencia);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = dr["nombre_agencia"].ToString();

					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Está repetido
		public string ObtenerNombreComiteResolucion(Int32 p_no_solicitud)
		{
			string vl_retorno = "";
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select distinct Nombre 
                                     from dcs_solicitudes_aprobaciones a,
                                          dcs_wf_estaciones e 
                                    Where no_solicitud=:pa_no_solicitud
                                      and a.comite_id=E.ESTACION_ID";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_no_solicitud", p_no_solicitud);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = dr["nombre"].ToString();

					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Repetido y listo
		public string ObtenerNombreUsuario(string p_usuario)
		{
			string vl_retorno = "";
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"SELECT nombres||' '||primer_apellido nombre_usuario
                                     from mgi_usuarios
                                    Where codigo_usuario=:pa_usuario";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_usuario", p_usuario);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = dr["nombre_usuario"].ToString();

					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo y ya existe
		public string ObtenerNombreEstacion(int p_estacion_id)
		{
			string vl_retorno = "";
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select nombre,comite_resolutivo from dcs_wf_estaciones where estacion_id=:pa_estacion_id";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_estacion_id", p_estacion_id);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = dr["nombre"].ToString();

					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public string ObtenerEstacionIsResolutivo(int p_estacion_id)
		{
			string vl_retorno = "";
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select comite_resolutivo from dcs_wf_estaciones where estacion_id=:pa_estacion_id";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_estacion_id", p_estacion_id);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = dr["comite_resolutivo"].ToString();

					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public string ObtenerEsAnalizableProducto(Int32 p_no_solicitud)
		{
			string vl_retorno = "N";
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select analizable_buro_b                                      
                                     from DCS_WORKFLOWS_PRODUCTOS 
                                    Where codigo_sub_aplicacion=(select codigo_sub_aplicacion from dcs_solicitudes where no_solicitud=:pa_no_solicitud)";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_no_solicitud", p_no_solicitud);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = dr["analizable_buro_b"].ToString();

					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}
			return vl_retorno;
		}

		//Listo
		public string ObtenerEsAnalizableProductoxCodigo(Int32 p_codigo_sub_aplicacion)
		{
			string vl_retorno = "N";
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select analizable_buro_b                                      
                                     from DCS_WORKFLOWS_PRODUCTOS 
                                    Where codigo_sub_aplicacion=:pa_codigo_sub_aplicacion";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_codigo_sub_aplicacion", p_codigo_sub_aplicacion);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = dr["analizable_buro_b"].ToString();
					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}
			return vl_retorno;
		}

		//Listo
		public string ObtenerEsVentanillaPlanilla(Int32 p_no_solicitud)
		{
			string vl_retorno = "N";
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select decode(DCS_F_AFILIADO_X_PLANILLA(codigo_cliente),'S','PLANILLA','VENTANILLA') as forma_pago
                                     from dcs_solicitudes
                                    Where no_solicitud=:pa_no_solicitud";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_no_solicitud", p_no_solicitud);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = dr["forma_pago"].ToString();

					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}
			return vl_retorno;
		}

		//Listo
		public bool ReasignarGerenteFilial(Int32 p_no_solicitud, string p_usuario, Int32 p_movimiento_actual, string p_llave)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"DCS_P_UTIL_CAMB_GTE_RESOL";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;

					cmd.Parameters.Add("pa_no_solicitud", OracleType.Number).Value = p_no_solicitud;
					cmd.Parameters.Add("pa_usuario", OracleType.VarChar, 30).Value = p_usuario;
					cmd.Parameters.Add("pa_movimiento_actual", OracleType.Number).Value = p_movimiento_actual;
					cmd.Parameters.Add("pa_llave", OracleType.VarChar, 40).Value = p_llave;
					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();
					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public bool ReasignarOficialFilial(Int32 p_no_solicitud, Int32 p_codigo_agencia_origen, string p_oficial)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"DCS_P_UTIL_REAOFICIALFILIAL";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;

					cmd.Parameters.Add("pa_no_solicitud", OracleType.Int32).Value = p_no_solicitud;
					cmd.Parameters.Add("pa_codigo_agencia_origen", OracleType.Int16).Value = p_codigo_agencia_origen;
					cmd.Parameters.Add("pa_oficial", OracleType.VarChar, 30).Value = p_oficial;

					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();
					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public bool RepermitirModifAprob(Int32 p_no_solicitud, string p_permite)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"DCS_P_UTIL_PERMITIR_MODIFAPROB";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;

					cmd.Parameters.Add("pa_no_solicitud", OracleType.Int32).Value = p_no_solicitud;
					cmd.Parameters.Add("pa_permite", OracleType.VarChar, 30).Value = p_permite;

					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();
					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public bool ReactualizarValoresAprobacion(Int32 p_no_solicitud, string p_no_acta, string p_ciudad_resolucion, decimal p_monto_aprobado, decimal p_tasa_aprobada, decimal p_plazo_aprobado)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"DCS_P_UTILl_CHANGEAPROBXSOLIC";

					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_no_solicitud", OracleType.Number).Value = p_no_solicitud;
					cmd.Parameters.Add("pa_no_acta", OracleType.VarChar, 10).Value = p_no_acta;
					cmd.Parameters.Add("pa_ciudad_resolucion", OracleType.VarChar, 100).Value = p_ciudad_resolucion;
					cmd.Parameters.Add("pa_monto_aprobado", OracleType.Number).Value = p_monto_aprobado;
					cmd.Parameters.Add("pa_plazo_aprobado", OracleType.Number).Value = p_plazo_aprobado;
					cmd.Parameters.Add("pa_tasa_aprobada", OracleType.Number).Value = p_tasa_aprobada;
					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();

					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public float ObtenerCuotaNivela(int p_codigo_sub_app, float p_monto, float p_tasa, float p_plazo)
		{
			float vl_retorno = 0;

			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					//string sql = @"select DCS_F_CALCULAR_CUOTA_NIVELADA(:pa_monto,:pa_tasa,:pa_plazo) cuota from dual";
					string sql = @"select ROUND(DCS_F_CALCULAR_CUOTA_VENCIMIEN(:pa_cod_sub_app,:pa_monto,:pa_tasa,:pa_plazo),2) cuota from dual";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_cod_sub_app", p_codigo_sub_app);
					cmd.Parameters.AddWithValue("pa_monto", p_monto);
					cmd.Parameters.AddWithValue("pa_tasa", p_tasa);
					cmd.Parameters.AddWithValue("pa_plazo", p_plazo);
					ConexionOracle.Open();
					dr = cmd.ExecuteReader();
					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = float.Parse(dr["cuota"].ToString());

					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//No tiene referencia
		public float ObtenerCuotaVencimiento(float p_monto, float p_tasa, float p_plazo)
		{
			float vl_retorno = 0;

			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select DCS_F_CALCULAR_CUOTA_NIVELADA(:pa_monto,:pa_tasa,:pa_plazo) cuota from dual";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_monto", p_monto);
					cmd.Parameters.AddWithValue("pa_tasa", p_tasa);
					cmd.Parameters.AddWithValue("pa_plazo", p_plazo);
					ConexionOracle.Open();
					dr = cmd.ExecuteReader();
					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = float.Parse(dr["cuota"].ToString());

					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public DataTable ObtenerDatosClientexCodigoCliente(string p_codigo_cliente)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;

			dt.Columns.Add("numero_identificacion");
			dt.Columns.Add("nombres");
			dt.Columns.Add("primer_apellido");
			dt.Columns.Add("segundo_apellido");
			dt.Columns.Add("apellido_de_casada");
			dt.Columns.Add("sexo");
			dt.Columns.Add("estado_civil");
			dt.Columns.Add("fecha_de_nacimiento");
			dt.Columns.Add("notaria");
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select numero_identificacion,nombres,primer_apellido,segundo_apellido,
                                          decode(sexo,'M','Masculino','F','Femenino','') sexo,
                                          decode(upper(estado_civil),'C','Casado','S','Soltero','U','Union Libre','D','Divorciado','V','Viudo','Sin definir') estado_civil,
                                          fecha_de_nacimiento,
                                          apellido_de_casada,notaria
                                     from mgi_clientes 
                                    where codigo_cliente=:pa_codigo_cliente ";

					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_codigo_cliente", p_codigo_cliente);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						while (dr.Read())
						{
							string vl_fecha_nacimiento = dr["fecha_de_nacimiento"].ToString();
							if (vl_fecha_nacimiento.Length >= 10)
							{
								vl_fecha_nacimiento = vl_fecha_nacimiento.Substring(0, 10);
							}
							dt.Rows.Add(dr["numero_identificacion"],
										dr["nombres"],
										dr["primer_apellido"],
										dr["segundo_apellido"],
										dr["apellido_de_casada"],
										dr["sexo"],
										dr["estado_civil"],
										vl_fecha_nacimiento,
										dr["notaria"]);
						}
					}

					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return dt;


		}

		//Listo
		public DataTable ObtenerDatosClientexIdentificacion(string p_numero_identificacion)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					//FELVIR01 - 20190611
					string sql = @"Select cl.codigo_cliente,
                                          numero_identificacion,
                                          nombres,
                                          primer_apellido,
                                          segundo_apellido,
                                          apellido_de_casada,
                                          cl.codigo_tipo_cliente,
                                          cl.fecha_de_nacimiento,
                                          decode(sexo,'M','Masculino','F','Femenino','') sexo,
                                          decode(upper(estado_civil),'C','Casado','S','Soltero','U','Union Libre','D','Divorciado','V','Viudo','Sin definir') estado_civil,   
                                          cl.nivel_educativo,
                                          cl.objetivo_social tipo_vivienda,                                                                                 
                                          p.descripcion profesion_oficio,
										  nombre lugar_nac,
                                          nacionalidad,
                                          dire_res.telefono_casa,
                                          dire_res.celular,
                                          nvl(dire_res.correo,'No Tiene') correo,
                                          dire_res.direccion_res,                                          
                                          cl.lugar_de_trabajo,
                                          cl.persona_a_contactar_2,
                                          cl.cargo_que_ocupa cargo,
                                          cl.nombre_auditores nombre_cargo,
                                          cl.fecha_de_ingreso_pais,
										  cl.ingresos_mes,
										  nvl(cl.otros_ingresos,0) otros_ingresos,
										  fecha_de_ingreso_pais, 
                                          abs(round(MONTHS_BETWEEN(fecha_de_ingreso_pais,trunc(sysdate)))) antiguedad_laboral_meses,
                                          dire_lab.telefono_trabajo,
                                          dire_lab.otro_telefono,                                          
                                          dire_lab.direccion_lab,
                                          dire_lab.correo_lab,
                                          DCS_F_AFILIADO_X_PLANILLA(cl.codigo_cliente) notaria,
										 cl.tipo_de_empresa,
										 cl.NOMBRE_CONJUGUE,
										 cl.lugar_trabajo_conyuge                                          
                                     from mgi_clientes cl, 
                                          mgi_profesiones p,           
                                          mgi_paises pa,                              
                                          (select codigo_empresa,codigo_cliente,dire_lab.telefonos telefono_trabajo,
                                                  dire_lab.fax otro_telefono,                                          
                                                  dire_lab.nomenclatura_2 direccion_lab,
                                                  dire_lab.correo correo_lab
                                             from mgi_direcciones dire_lab
                                            Where dire_lab.codigo_direccion=2 
                                              and codigo_empresa=1)  dire_lab,
                                          (select codigo_empresa,codigo_cliente,
                                                  dire_res.telefonos telefono_casa,
                                                  dire_res.fax celular,
                                                  dire_res.correo correo,
                                                  dire_res.nomenclatura_2 direccion_res
                                             from mgi_direcciones dire_res
                                            Where dire_res.codigo_direccion=1 
                                              and codigo_empresa=1) dire_res                                                  
                                    where 1=1                                                                           
                                      and cl.codigo_cliente=dire_lab.codigo_cliente(+)
                                      and cl.codigo_cliente=dire_res.codigo_cliente(+)                                      
                                      and cl.codigo_profesion=P.CODIGO_PROFESION(+)
                                      and cl.codigo_pais=pa.codigo_pais(+) 
                                      and cl.numero_identificacion=:pa_numero_identificacion ";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_numero_identificacion", p_numero_identificacion);
					OracleDataAdapter da = new OracleDataAdapter(cmd);
					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//Listo FELVIR01-20190610 (Listo y pediente)
		public DataTable ObtenerDependientes(int codigo_cliente)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select (select count(*) from mgi_nucleo_familiar where codigo_cliente = :pa_cod_cliente and cod_parentesco = 4) hijos, 
									((select count(*) from mgi_dependientes where codigo_cliente = :pa_cod_cliente) - 
									(select count(*) from mgi_nucleo_familiar where codigo_cliente = :pa_cod_cliente and cod_parentesco = 4)) otros  
									from dual ";

					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_cod_cliente", codigo_cliente);
					OracleDataAdapter da = new OracleDataAdapter(cmd);
					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}

				return dt;
			}
			catch (Exception)
			{

				throw;
			}
		}

		//FELVIR01 - 20190610 (Listo)
		public DataTable VerificarActualizacion(int codigo_cliente)
		{
			try
			{
				/*Orden de las filas: Clientes, dependientes, núcleo familiar, referencias personales y direcciones*/
				string sql = @"select
								(select count(*) from mgi_clientes where codigo_cliente = :cod_cliente ) tot_exist,
								nvl((select count(*) from mgi_clientes where codigo_cliente = :cod_cliente and trunc(fecha_modificacion) = trunc(sysdate)),0) tot_mod,
								nvl((select count(*) from mgi_clientes where codigo_cliente = :cod_cliente and trunc(fecha_creacion) = trunc(sysdate)),0) creadaHoy   
								from dual 
								union all 
								select 
								(select count(codigo_cliente) from mgi_dependientes where codigo_cliente = :cod_cliente) tot_exist, 
								nvl((select count(FECHA_MODIFICACION) from mgi_dependientes where codigo_cliente = :cod_cliente and trunc(fecha_modificacion) = trunc(sysdate)),0) tot_mod,  
								nvl((select count(fecha_creacion) from mgi_dependientes where codigo_cliente = :cod_cliente and trunc(fecha_creacion) = trunc(sysdate)),0) creadaHoy  
								from dual  
								union all  
								select  
								(select count(codigo_cliente) from mgi_nucleo_familiar where codigo_cliente = :cod_cliente) tot_exist,
								nvl((select count(FECHA_MODIFICACION) from mgi_nucleo_familiar where codigo_cliente = :cod_cliente and trunc(fecha_modificacion) = trunc(sysdate)),0)tot_mod,
								nvl((select count(fecha_creacion) from mgi_nucleo_familiar where codigo_cliente = :cod_cliente and trunc(fecha_creacion) = trunc(sysdate)),0) creadaHoy   
								from dual  
								union all 
								select 
								(select count(codigo_cliente) from mgi_referencias_personales where codigo_cliente = :cod_cliente) tot_exist,
								nvl((select nvl(count(FECHA_MODIFICACION),0) from mgi_referencias_personales where codigo_cliente = :cod_cliente  and trunc(fecha_modificacion) = trunc(sysdate)),0) tot_mod, 
								nvl((select nvl(count(fecha_creacion),0) from mgi_referencias_personales where codigo_cliente = :cod_cliente  and trunc(fecha_creacion) = trunc(sysdate)),0) creadaHoy   
								from dual 
								union all 
								select 
								(select count(codigo_cliente) from mgi_direcciones where codigo_cliente = :cod_cliente) tot_exist,
								nvl((select count(FECHA_MODIFICACION) from mgi_direcciones where codigo_cliente = :cod_cliente  and trunc(fecha_modificacion) = trunc(sysdate)),0) tot_mod, 
								nvl((select count(fecha_creacion) from mgi_direcciones where codigo_cliente = :cod_cliente  and trunc(fecha_creacion) = trunc(sysdate)),0) creadaHoy   
								from dual 
								union all
								select  
								(select count(codigo_cliente) from MGI.MGI_DATOS_CONYUGE where codigo_cliente = :cod_cliente) tot_exist,
								nvl((select count(FEC_MODIFICACION) from MGI_DATOS_CONYUGE where codigo_cliente = :cod_cliente  and trunc(FEC_MODIFICACION) = trunc(sysdate)),0) tot_mod,
								nvl((select count(FEC_ADICION) from MGI_DATOS_CONYUGE where codigo_cliente = :cod_cliente  and trunc(FEC_ADICION) = trunc(sysdate)),0) creadaHoy   
								from dual ";

				DataTable dtActualizaciones = new DataTable();
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					OracleCommand cmd = new OracleCommand();
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("cod_cliente", codigo_cliente);


					OracleDataAdapter da = new OracleDataAdapter(cmd);
					try
					{
						da.Fill(dtActualizaciones);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}

				return dtActualizaciones;
			}
			catch (Exception ex)
			{
				throw new Exception($"Error: {ex.TargetSite} - Mensaje: {ex.Message}");
			}
		}

		//FELVIR01 - 20160614
		public DataTable InformacionConyuge(int codigo_cliente)
		{
			try
			{
				string sql = @"SELECT codigo_cliente,
								numero_identificacion,
								primer_nombre,
								segundo_nombre,
								primer_apellido,
								segundo_apellido,
								genero,
								num_hijos,
								num_parientes,
								ind_afil_coopsafa,
								codigo_cliente_conyuge,
								nivel_educativo,    
								actividad_economica,
								ind_posee_casa,
								condicion,
								ind_posee_tarjetas,
								det_tarjetas_bco,
								dir_residencia,
								tel_residencia,
								num_cel_residencia,
								telefono_adic1,
								telefono_adic2,
								email_personal,
								ind_trabajo,
								lugar_trabajo,
								depto_trabajo,
								cargo_trabajo,
								abs(round(MONTHS_BETWEEN(d.FECHA_INGRESO, trunc(sysdate)))) antiguedad_laboral_meses,
								salario_mensual,
								otros_ingresos,
								tipo_empresa,
								otra_empresa,
								dir_laboral,
								tel_laboral1,
								ext_laboral1,
								tel_laboral2,
								ext_laboral2,
								email_laboral,
								p.DESCRIPCION prof  
							FROM mgi.mgi_datos_conyuge d, mgi_profesiones p  
							where d.codigo_cliente = :cod_cliente  
							and p.CODIGO_PROFESION = d.PROFESION";
				DataTable datos = new DataTable();
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					OracleCommand cmd = new OracleCommand();
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("cod_cliente", codigo_cliente);


					OracleDataAdapter da = new OracleDataAdapter(cmd);
					try
					{
						da.Fill(datos);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}

				return datos;
			}
			catch (Exception ex)
			{
				throw new Exception($"Error: {ex.TargetSite} - Mensaje: {ex.Message}");
			}
		}

		//Listo
		public DataTable ObtenerReferenciasxCodigoCliente(string p_codigo_cliente)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select A.codigo_referencia_personal,nombre||' '||primer_apellido||' '||segundo_apellido nombre,metodo_localizacion,telefono,celular,b.descripcion tipo_referencia,nvl(comentarios, 'NINGUNA') pto_referencia  
                                     from mgi_referencias_personales a,
                                          MGI_TIPOS_REF_PERSONALES b
                                    Where a.tipo_referencia=b.codigo_referencia_personal
                                      and codigo_cliente=:pa_codigo_cliente
                                     order by a.rowid";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_codigo_cliente", p_codigo_cliente);
					OracleDataAdapter da = new OracleDataAdapter(cmd);
					try
					{
						da.Fill(dt);
					}
					catch (Exception ex)
					{
						throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}
			return dt;
		}

		//No se usa
		public DataTable ObtenerReferenciasxSolic(string p_codigo_cliente)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select * from dcs_solicitudes2_referencias order by rol,no_referencia";
					//string sql = @"select * from dcs_solicitudes";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					//cmd.Parameters.AddWithValue("pa_codigo_cliente", p_codigo_cliente);
					OracleDataAdapter da = new OracleDataAdapter(cmd);
					try
					{
						da.Fill(dt);
					}
					catch (Exception ex)
					{
						throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}
			return dt;
		}

		//Listo
		public DataTable ObtenerCarpetasDelUsuario(string p_usuario)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;

			dt.Columns.Add("carpeta_id");
			dt.Columns.Add("descripcion");
			dt.Columns.Add("canti");

			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select carpeta_id,descripcion,dcs_f_cant_sol_x_carpeta(carpeta_id) canti
                                    From dcs_carpetas_usuarios cu
                                   Where codigo_usuario=:pa_usuario
                                   order by carpeta_id";

					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_usuario", p_usuario);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						while (dr.Read())
						{
							dt.Rows.Add(dr["carpeta_id"],
										dr["descripcion"],
										dr["canti"]);
						}
					}

					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return dt;
		}

		//Listo
		public bool ObtenerFotoAfiliado(string codigo_cliente, out byte[] foto, out string fecha_act)
		{
			bool vl_retorno = false;
			foto = null;
			fecha_act = "";


			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select codigo_cliente,foto,decode(fecha_act,null,fecha_ing,fecha_act) fecha_ult_foto,usa_huella_caja
                                     from mia_clientes_fotos 
                                    Where codigo_cliente=:pa_codigo_cliente";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_codigo_cliente", codigo_cliente);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();


						if (!DBNull.Value.Equals(dr.GetValue(1)))
						{
							foto = (byte[])dr.GetValue(1);
							fecha_act = dr.GetValue(2).ToString();

						}

						vl_retorno = true;
					}
					dr.Close();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + " " + ex.Message);

			}
			return vl_retorno;
		}

		//Listo
		public string ObtenerObservacionesAfiliacion(Int32 p_no_solicitud)
		{
			string vl_retorno = "";

			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select anotacion 
                                     from dcs_anotaciones_solicitudes 
                                     Where no_solicitud=:pa_no_solicitud
                                       and estacion_id=1000 --solo las anotaciones de Afiliacion";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_no_solicitud", p_no_solicitud);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						while (dr.Read())
						{
							vl_retorno = vl_retorno + dr["anotacion"].ToString() + ". ";
						}

					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//No se usa
		public List<referencias_solicitud> ObtenerReferenciasxSolicitud(Int32 p_no_solicitud)
		{
			List<referencias_solicitud> lstReferencias = new List<referencias_solicitud>();
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select rol,no_referencia,referencia_de,nombre,direccion,telefono_fijo,telefono_celular,punto_referencia,casa_color,bloque
                                     from dcs_solicitudes2_referencias
                                    Where no_solicitud=:pa_no_solicitud
                                   order by rol,no_referencia";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_no_solicitud", p_no_solicitud);
					ConexionOracle.Open();
					OracleDataReader dr = cmd.ExecuteReader();
					try
					{
						if (dr.HasRows)
						{
							int x = 0;
							while (dr.Read())
							{
								referencias_solicitud referen = new referencias_solicitud();
								referen.rol = dr["rol"].ToString();
								referen.no_referencia = int.Parse(dr["no_referencia"].ToString());
								referen.referencia_de = dr["referencia_de"].ToString();
								referen.nombre = dr["nombre"].ToString();
								referen.direccion = dr["direccion"].ToString();
								referen.telefono_fijo = dr["telefono_fijo"].ToString();
								referen.telefono_celular = dr["telefono_celular"].ToString();
								referen.punto_referencia = dr["punto_referencia"].ToString();
								referen.casa_color = dr["casa_color"].ToString();
								referen.bloque = dr["bloque"].ToString();
								lstReferencias.Add(referen);
							}
						}
						dr.Close();
					}
					catch (Exception ex)
					{
						throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return lstReferencias.ToList();
		}

		//Listo
		public bool ExisteSolicitudesEnElPeriodo(string p_id)
		{
			bool vl_retorno = false;

			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select count(*) n
                                     from dcs_solicitudes s1,
                                          dcs_solicitudes2 s2   
                                    where s1.no_solicitud=s2.no_solicitud
                                      and numero_identificacion=:pa_id                                      
                                      and trunc(sysdate)-trunc(fecha_presentacion)<(select TO_number(valor) from dcs_wf_parametros where parametro='WFC-0004')";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_id", p_id);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = false;
						float vl_n = 0;
						float.TryParse(dr["n"].ToString(), out vl_n);
						if (vl_n > 0)
						{
							vl_retorno = true;
						}
					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public float ObtenerSaldosAportacionesxCliente(string p_no_id)
		{
			float vl_retorno = 0;

			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select saldo 
                                     from mca_cuentas cta,
                                          mgi_clientes c                                          
                                    where cta.codigo_cliente=c.codigo_cliente
                                      and codigo_sub_aplicacion=101        
                                      and NVL(cancelada_b,'N')='N'                              
                                      and numero_identificacion=:pa_no_id";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_no_id", p_no_id);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = 0;
						float.TryParse(dr["saldo"].ToString(), out vl_retorno);
					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public string ObtenerAntiguedadCooperativa(string p_no_id)
		{
			string vl_retorno = "0";
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select abs(round(MONTHS_BETWEEN(fecha_inicio_relacion,trunc(sysdate)))) antiguedad_cooperativista from mgi_clientes where codigo_tipo_cliente=5 and numero_identificacion=:pa_no_id";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_no_id", p_no_id);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = dr["antiguedad_cooperativista"].ToString();

					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public int ObtenerBuroInterno_xId(string p_no_id, out string descripcionEstado, out List<string> observaciones)
		{
			// 0 Aprobado (No encontrado en), 
			// 1 Denegado (Se encontro registro en Coopsafa 2 y 3 e IBEA)
			// 2 Referido (Es aval de alguna operacion, o mora menor de 45 dias
			observaciones = new List<string>();
			descripcionEstado = "";

			int vl_retorno = 0;
			descripcionEstado = "APROBADO";
			int vl_cartera_castigada = ObtenerSituacionCoopsafa_2_3_xId(p_no_id);
			int vl_ibea = ObtenerSituacionIBEAxID(p_no_id);
			int vl_es_aval = ObtenerSituacionCarteraAdtiva_aval(p_no_id);
			int vl_en_mora = ObtenerSituacionCarteraAdtiva_mora(p_no_id);
			if (vl_cartera_castigada == 1)
			{
				vl_retorno = 1;// En coopsafa 2 y 3
				descripcionEstado = "RECHAZADO";
				observaciones.Add("Encontrado en cartera castigada 2 y 3,");
			}

			if (vl_ibea == 1)
			{
				vl_retorno = 1;
				descripcionEstado = "RECHAZADO";
				observaciones.Add("Encontrado en lista IBEA,");
			}

			if (vl_es_aval == 2)
			{
				vl_retorno = 2;
				descripcionEstado = "REFERIDO";
				observaciones.Add("Es Aval de otro Afiliado, ");
			}

			if (vl_en_mora == 2)
			{
				vl_retorno = 2;
				descripcionEstado = "REFERIDO";
				observaciones.Add("Tiene prestamos en mora de mas de un dia, ");
			}
			//Este codigo es para que recoga los posibles rechazos pero que predomine el rechazado si esta en la coopsafa 2 y 3, sino se quedaria con la ultima evaluacion del estado en mora
			if (vl_cartera_castigada == 1 | vl_ibea == 1)
			{
				vl_retorno = 1;// En coopsafa 2 y 3
				descripcionEstado = "RECHAZADO";
			}
			return vl_retorno;

		}

		//Listo
		public Int32 ObtenerUltimoMovAprobaciones(Int32 p_no_solicitud)
		{
			Int32 vl_retorno = 0;
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					//string sql = @"select max(no_movimiento_actual) n from dcs_solicitudes_aprobaciones where no_solicitud=:pa_no_solicitud";
					string sql = @"select no_movimiento_actual n from dcs_solicitudes  where no_solicitud=:pa_no_solicitud";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_no_solicitud", p_no_solicitud);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();

						Int32.TryParse(dr["n"].ToString(), out vl_retorno);
					}

					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public int ObtenerSituacionCarteraAdtiva_mora(string p_no_id)
		{
			// 0 Aprobado (No encontrado en), 
			// 1 Denegado (Se encontro registro en Coopsafa 2 y 3 e IBEA)
			// 2 Referido (Es aval de alguna operacion, o mora menor de 45 dias
			int vl_retorno = 0;
			int regs_found = 0;
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select count(*) n
                                     from cr_detalle_cartera car,
                                          mgi_clientes cl
                                    Where car.cod_compania=CR_GLOBALES.C_COMPANIA
                                      and car.cod_cliente=cl.codigo_cliente
                                      and numero_identificacion=:pa_no_id
                                      and num_diasmora>=1";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_no_id", p_no_id);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						regs_found = 0;
						int.TryParse(dr["n"].ToString(), out regs_found);
					}
					if (regs_found >= 1)
						vl_retorno = 2;//Referido
					else
						vl_retorno = 0;//Aprobado no tiene mora en cartera administrativa
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public int ObtenerSituacionCarteraAdtiva_aval(string p_no_id)
		{
			// 0 Aprobado (No encontrado en), 
			// 1 Denegado (Se encontro registro en Coopsafa 2 y 3 e IBEA)
			// 2 Referido (Es aval de alguna operacion, o mora menor de 45 dias
			int vl_retorno = 0;
			int regs_found = 0;
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select count(*) n
                                     from cr_fiadores f,
                                          mgi_clientes C,
                                          cr_operaciones o
                                    where f.cod_compania=CR_GLOBALES.C_COMPANIA
                                      and O.COD_COMPANIA=CR_GLOBALES.C_COMPANIA
                                      and f.cod_cliente=c.codigo_cliente
                                      and f.num_solicitud=O.NUM_SOLICITUD
                                      and o.ind_estado=3
                                      and c.numero_identificacion=:pa_no_id";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_no_id", p_no_id);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						regs_found = 0;
						int.TryParse(dr["n"].ToString(), out regs_found);
					}
					if (regs_found >= 1)
						vl_retorno = 2;//Referido
					else
						vl_retorno = 0;//Aprobado no es aval
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//Listo
		public int ObtenerAntiguedadSolicTU(Int32 p_no_solicitud)
		{
			int int_retorno = 0;
			string vl_retorno = "";
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select trunc(sysdate)-trunc(fecha_creacion_tu) antiguedad_tu
                                     from dcs_solicitudes
                                    Where no_solicitud=:pa_no_solicitud";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_no_solicitud", p_no_solicitud);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = dr["antiguedad_tu"].ToString();

						int_retorno = int.Parse(vl_retorno);
					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return int_retorno;
		}

		//Listo
		public int ObtenerSituacionCoopsafa_2_3_xId(string p_no_id)
		{
			// 0 Aprobado (No encontrado en), 
			// 1 Denegado (Se encontro registro en Coopsafa 2 y 3 )            
			int vl_retorno = 0;
			int regs_found = 0;
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select count(*) n 
                                     from gc_master                                          
                                    where no_id1=:pa_no_id
                                      and codigo_empresa in (2,3)";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_no_id", p_no_id);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						regs_found = 0;
						int.TryParse(dr["n"].ToString(), out regs_found);
					}
					if (regs_found >= 1)
						vl_retorno = 1;//denegado
					else
						vl_retorno = 0;//Aprobado para la Coopsafa 2 y 3
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//
		public DataTable ObtenerDetalleCreditosCoopsafa_2_3(string p_codigo_cliente, int empresa)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			string vl_condi_empresa = "";
			if (empresa == 1)
			{
				vl_condi_empresa = "and m.codigo_empresa=1 and dias_mora>0 ";
			}
			if (empresa >= 2)
			{
				vl_condi_empresa = "and m.codigo_empresa in (2,3)";
			}
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select codigo_cliente,
                                          nombre_cliente||' '||primer_apellido||' '||segundo_apellido nombre_cliente,
                                          desc_sub_aplicacion producto,
                                          tipo_garantia1,
                                          valor_garantia1,
                                          cuota_total,
                                          cuotas_pagadas,
                                          cuotas_vencidas,
                                          fecha_apertura,
                                          fecha_cancelacion,
                                          cancelado_b,
                                          cobro_judicial_b,
                                          nombre_aval1||' '||primer_apellido_aval1||' '||segundo_apellido_aval1 aval1,
                                          nombre_aval2||' '||primer_apellido_aval2||' '||segundo_apellido_aval1 aval2,
                                          valor_otorgado,
                                          saldo saldo 
                                     from gc_master m,
                                          mgi_sub_aplicaciones sa 
                                    Where trx_cod2=sa.codigo_sub_aplicacion
                                      and codigo_cliente=:pa_codigo_cliente " +
									  @vl_condi_empresa;

					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_codigo_cliente", p_codigo_cliente);
					OracleDataAdapter da = new OracleDataAdapter(cmd);
					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}


		//FELVIR01-20190619
		public string ObtenerEstadoCivil(int codigoCliente)
		{
			string estadoCivil = string.Empty;
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select decode(upper(estado_civil),'C','Casado','S','Soltero','U','Union Libre','D','Divorciado','V','Viudo','Sin definir') estado_civil  
									from mgi_clientes where codigo_cliente = :codCliente";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("codCliente", codigoCliente);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						estadoCivil = dr["estado_civil"].ToString();

					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return estadoCivil;
		}

		public DataTable ObtenerDetalleCreditosCarteraAdtiva(string p_codigo_cliente)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();

			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select num_operacion,cod_cliente,id_cliente,nom_cliente,des_tipopres,des_moneda,des_formapago,fec_constitucion,mon_original,num_diasmora
                                     From cr_detalle_cartera
                                    Where cod_cliente=:pa_codigo_cliente     
                                      and num_diasmora>0  ";

					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_codigo_cliente", p_codigo_cliente);
					OracleDataAdapter da = new OracleDataAdapter(cmd);
					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}
		public DataTable ObtenerInformacionCierreSolicitud(Int32 p_no_solicitud)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();

			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select fecha_cierre,cerrada_por,
                                          nombres||' '||primer_apellido||' '||segundo_apellido nombres,
                                          estado_solicitud_id,e.descripcion,
                                          instrucciones_desembolso,no_movimiento_actual,
                                          motivo_cierre_id,
                                          comentarios
                                     from dcs_solicitudes s1,
                                          dcs_wf_estado_solicitudes e,
                                          dcs_movimientos_solicitudes ms,
                                          mgi_usuarios u
                                    Where s1.no_solicitud=:pa_no_solicitud
                                      and s1.estado_solicitud_id=e.estado_id
                                      and no_movimiento_actual=ms.no_movimiento
                                      and cerrada_por=u.codigo_usuario";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_no_solicitud", p_no_solicitud);
					OracleDataAdapter da = new OracleDataAdapter(cmd);
					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//Listo
		public int ObtenerSituacionIBEAxID(string p_no_id)
		{
			// 0 Aprobado (No encontrado en), 
			// 1 Denegado (Se en IBEA)

			int vl_retorno = 0;
			int regs_found = 0;
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select COUNT(*) N 
                                     from CR_IBEA 
                                    Where num_identidad=:pa_no_id 
                                      and ind_activo='S'";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_no_id", p_no_id);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						regs_found = 0;
						int.TryParse(dr["n"].ToString(), out regs_found);
					}
					if (regs_found >= 1)
						vl_retorno = 1;//denegado
					else
						vl_retorno = 0;//Aprobado para la Lista IBEA
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		public string ObtenerReferenciasxSolicitud2(Int32 p_no_solicitud)
		{

			string vl_retorno = "";
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select xml_referencias from dcs_solicitudes2_referencias where no_solicitud=:pa_no_solicitud";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_no_solicitud", p_no_solicitud);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = dr["xml_referencias"].ToString();

					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}
		public string ObtenerParametro(string p_parametro)
		{

			string vl_retorno = "";
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select valor from dcs_wf_parametros where parametro=:pa_parametro";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_parametro", p_parametro);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = dr["valor"].ToString();

					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}
		public string ObtenerInstruccionesDesembolsoxSolicitud(Int32 p_no_solicitud)
		{
			string vl_retorno = "";
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select instrucciones_desembolso from dcs_solicitudes where no_solicitud=:pa_no_solicitud";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_no_solicitud", p_no_solicitud);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = dr["instrucciones_desembolso"].ToString();
					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}
		public string ObtenerDescripcionDestinoCredito(Int32 p_destino_credito)
		{
			string vl_retorno = "";
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select descripcion_destino from DCS_WF_DESTINOS_CREDITO where destino_id=:pa_destino_id";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_destino_id", p_destino_credito);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = dr["descripcion_destino"].ToString();
					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}
		public bool ObtenerMonedaxSubAplicacion(string p_codigo_sub_aplicacion, out string codigo_moneda, out string descripcion_moneda, out string sigla_moneda)
		{
			bool vl_retorno = false;
			codigo_moneda = "";
			descripcion_moneda = "";
			sigla_moneda = "";
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select sa.codigo_moneda,desc_moneda,abreviatura,desc_sub_aplicacion 
                                     from mgi_sub_aplicaciones sa,
                                          mgi_monedas m 
                                   where sa.codigo_moneda=m.codigo_moneda
                                     and sa.codigo_sub_aplicacion=:pa_codigo_sub_app";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_codigo_sub_app", p_codigo_sub_aplicacion);
					ConexionOracle.Open();
					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						codigo_moneda = dr["codigo_moneda"].ToString();
						descripcion_moneda = dr["desc_moneda"].ToString();
						sigla_moneda = dr["abreviatura"].ToString();
					}
					dr.Close();
					cmd.Dispose();
					vl_retorno = true;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}
		public int ObtenerCantCarpetasxUsuarios(string p_usuario)
		{
			int vl_retorno = 0;
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;

			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select count(*) cant
                                     from dcs_carpetas_usuarios cu       
                                    Where codigo_usuario=:pa_codigo_usuario";

					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_codigo_usuario", p_usuario);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = int.Parse(dr["cant"].ToString());
					}

					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}
		public DataTable ObtenerSubAplicaciones(string p_usuario, string cod_fondo)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;

			dt.Columns.Add("codigo_sub_aplicacion");
			dt.Columns.Add("desc_sub_aplicacion");

			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{

					/*string sql = @"Select w.codigo_sub_aplicacion,initcap(desc_sub_aplicacion) desc_sub_aplicacion 
                                     from mgi_sub_aplicaciones sa,
                                          Dcs_workflows_productos w 
                                    Where codigo_empresa=CR_GLOBALES.C_EMPRESA 
                                      and codigo_aplicacion=:pa_sub_aplicacion
                                      and sa.codigo_sub_aplicacion=w.codigo_sub_aplicacion
                                      and w.activo_b='S'  
                                      and sa.activo_b='S'";*/

					string sql = @"Select w.codigo_sub_aplicacion,initcap(desc_sub_aplicacion) desc_sub_aplicacion 
                                     from mgi_sub_aplicaciones sa,
                                          Dcs_workflows_productos w,
                                          cr_tipo_prestamo tp 
                                    Where codigo_empresa=CR_GLOBALES.C_EMPRESA 
                                      and codigo_aplicacion=:pa_sub_aplicacion
                                      and sa.codigo_sub_aplicacion=w.codigo_sub_aplicacion
                                      and sa.codigo_sub_aplicacion=cod_tipopres
                                      and w.activo_b='S'  
                                      and sa.activo_b='S'                                                                            
                                      and nvl(cod_fondo_redescontado,'CSF')=NVL(:pa_cod_fondo,'CSF')
                                      ORDER BY 1";


					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_sub_aplicacion", p_usuario);
					cmd.Parameters.AddWithValue("pa_cod_fondo", cod_fondo);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						while (dr.Read())
						{
							dt.Rows.Add(dr["codigo_sub_aplicacion"],
										dr["desc_sub_aplicacion"]);
						}
					}

					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return dt;
		}
		public DataTable ObtenerDestinoCredito()
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select * from dcs_wf_destinos_credito where activo_b='S'";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					//cmd.Parameters.AddWithValue("pa_activo_b", 'S');
					OracleDataAdapter da = new OracleDataAdapter(cmd);
					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//No se usa
		public bool insertar_numero(string p_numero)
		{

			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;


			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Insert into wfc_numeros(numero) values (:pa_numero)";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_numero", p_numero);
					ConexionOracle.Open();
					cmd.ExecuteNonQuery();
					vl_retorno = true;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;

		}

		public string ObtenerDescripcionSubApplicacion(int p_codigo_sub_aplicacion)
		{
			string vl_retorno = "";

			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select initcap(desc_sub_aplicacion) desc_sub_aplicacion
                                     from mgi_sub_aplicaciones sa                                          
                                   where sa.codigo_sub_aplicacion=:pa_codigo_sub_app";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_codigo_sub_app", p_codigo_sub_aplicacion);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = dr["desc_sub_aplicacion"].ToString();

					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}
		public bool guardarXMLRespuesta(Int32 p_solicitud, string p_xml, string p_xml_origen)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{

					string sql = @"INSERT INTO DCS_SOLICITUDES2_RESP_TRANS(no_solicitud,xml_enviado,xml_respuesta,no_entrada)
                                          values(:pa_no_solicitud,:pa_xml_origen,:pa_xml,DCS_SEC_XML_RESPUESTA.nextval)";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_no_solicitud", OracleType.Number).Value = p_solicitud;
					cmd.Parameters.Add("pa_xml_origen", OracleType.VarChar, 4000).Value = p_xml_origen;
					cmd.Parameters.Add("pa_xml", OracleType.LongVarChar).Value = p_xml;


					ConexionOracle.Open();
					cmd.CommandType = CommandType.Text;
					cmd.ExecuteNonQuery();

					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//aqui
		public string ObtenerXmlRespuestaEvaluacion(int p_no_solicitud)
		{
			string vl_retorno = "";

			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select xml_respuesta from dcs_solicitudes2_resp_trans where no_solicitud=:pa_no_solicitud and no_entrada=(select max(no_entrada) FROM dcs_solicitudes2_resp_trans WHERE no_solicitud=:pa_no_solicitud)";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_no_solicitud", p_no_solicitud);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = dr["xml_respuesta"].ToString();
					}

					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}
		public DataTable ObtenerDetalleAhorrosAfiliado(string p_codigo_cliente)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select c.codigo_sub_aplicacion,numero_cuenta,desc_sub_aplicacion,saldo
                                     from mca_cuentas c,
                                          mgi_sub_aplicaciones sa
                                    Where c.codigo_sub_aplicacion=sa.codigo_sub_aplicacion
                                      and c.codigo_cliente=:pa_codigo_cliente";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_codigo_cliente", p_codigo_cliente);
					OracleDataAdapter da = new OracleDataAdapter(cmd);
					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}
		public DataTable ObtenerDetallePrestamosAfiliado(string p_codigo_cliente)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select cod_tipopres,fec_constitucion fecha_apertura,                        
                                          des_tipopres,des_moneda,por_tasa||'%' tasa,mon_original monto,mon_cuota,
                                          mon_saldo_total saldo,des_gasto
                                     from cr_detalle_cartera cr                                          
                                    Where cod_cliente=:pa_codigo_cliente                                      
                                    order by fec_constitucion desc";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_codigo_cliente", p_codigo_cliente);
					OracleDataAdapter da = new OracleDataAdapter(cmd);
					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}
		public DataTable ObtenerInfoTarjetaDebicoop(string p_codigo_cliente)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select 'XXXXXXXXXXXX'||SUBSTR(no_tarjeta,13,4) no_tarjeta,nombre_propietario,decode(tipo_tarjeta,1,'Principal','Adicional') tipo_tarjeta
                                     from td_cuentas_tarjetas
                                    Where numero_cuenta=(select numero_cuenta from mca_cuentas where codigo_sub_aplicacion=102 and codigo_cliente=:pa_codigo_cliente and activo_b='S')
                                      and codigo_estado=2  ";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_codigo_cliente", p_codigo_cliente);
					OracleDataAdapter da = new OracleDataAdapter(cmd);
					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}
		public DataTable ObtenerInfoCoopcel(string p_codigo_cliente)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select coopcel_numero,decode(coopcel_redmovil,'CLA','CLARO','TIGO') red_movil 
                                     from td_cuentas_tarjetas t,
                                          mca_cuentas c 
                                    where c.numero_cuenta=t.numero_cuenta
                                      and coopcel_alerta='S'
                                      and codigo_cliente=:pa_codigo_cliente";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_codigo_cliente", p_codigo_cliente);
					OracleDataAdapter da = new OracleDataAdapter(cmd);
					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}
		public DataTable ObtenerInfoFE(string p_codigo_cliente)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select usuario,correo_elec,fecha_ultimo_login
                                    from mcw_usuarios_consultaweb a   
                                   Where codigo_cliente=:pa_codigo_cliente  ";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_codigo_cliente", p_codigo_cliente);
					OracleDataAdapter da = new OracleDataAdapter(cmd);
					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//No se usa
		public bool VeDatosEmpleados()
		{

			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"F_VE_DATOS_EMPLEADOS";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("cuenta_a_consultar", OracleType.Number).Value = 0;
					cmd.Parameters.Add("cliente_a_consultar", OracleType.Number).Value = 171998;
					cmd.Parameters.Add("usuario_consulta", OracleType.Char, 30).Value = "GPARED01";
					//cmd.Parameters.Add("Return_Value", OracleType.Int16).Direction = ParameterDirection.ReturnValue;

					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteScalar();
					Int32 resultado = Int32.Parse(cmd.Parameters["Return_Value"].Value.ToString());


					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;

		}

		public DataTable p_datos_rptResolucionComite(Int32 p_no_solicitud)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select upper(es.descripcion) estado_solicitud,
                                          s2.no_solicitud,
                                          s2.numero_identificacion,
                                          s2.codigo_cliente,
                                          fecha_presentacion,
                                          nombres||' '||primer_apellido||' '||segundo_apellido nombre_completo,        
                                          sa.desc_sub_aplicacion producto,
                                          s2.monto_solicitado,
                                          plazo,
                                          tasa,
                                          descripcion_destino destino,
                                          decode(DCS_F_AFILIADO_X_PLANILLA(s1.codigo_cliente),'S','PLANILLA','VENTANILLA') ventanilla_planilla,
                                          codigo_agencia_origen,
                                          z.descripcion region,        
                                          nombre_agencia filial,
                                          oficial_servicio oficial_servicios,                                          
                                          DCS_F_OBTENER_GTE_FILIAL_SOLIC(s2.no_solicitud) gerente_filial,
                                          instrucciones_desembolso,
                                          descripcion_garantia,
                                          monto_aprobado,
                                          plazo_aprobado,
                                          tasa_aprobada,
                                          --DCS_F_CALCULAR_CUOTA_NIVELADA(monto_aprobado,tasa_aprobada,plazo_aprobado) cuota_aprobada,
                                          round(DCS_F_CALCULAR_CUOTA_VENCIMIEN(s1.codigo_sub_aplicacion,monto_aprobado,tasa_aprobada,plazo_aprobado),2) cuota_aprobada,
                                          estacion_id,
                                          no_acta_resolucion,
                                          ciudad_resolucion,
                                          no_identidad_codeudor||'  '||rtrim(primer_nombre_codeudor)||' '||rtrim(segundo_nombre_codeudor)||' '||rtrim(primer_apellido_codeudor) codeudor,
                                          trunc(fecha_aprobacion_rechazo) fecha_aprobacion_rechazo,
                                          decision_final_solicitud,
                                          guid_sol
                                     from dcs_solicitudes2 s2,
                                          dcs_solicitudes s1,
                                          dcs_wf_estado_solicitudes es,
                                          DCS_WF_DESTINOS_CREDITO dst,
                                          mgi_clientes c,
                                          mgi_agencias ag,
                                          mgi_sub_aplicaciones sa,
                                          cef_agencias_zona az,
                                          cef_zonas z        
                                    where sa.codigo_empresa=1
                                      and c.codigo_empresa=1
                                      and ag.codigo_empresa=1
                                      and s1.codigo_cliente=c.codigo_cliente
                                      and s2.destino=DST.DESTINO_ID
                                      and s1.codigo_sub_aplicacion=sa.codigo_sub_aplicacion
                                      and s1.no_solicitud=s2.no_solicitud
                                      and s1.codigo_agencia_origen=az.codigo_agencia
                                      and s1.codigo_agencia_origen=ag.codigo_agencia
                                      and az.codigo_zona=z.codigo_zona     
                                      and s1.estado_solicitud_id=es.estado_id
                                      and s2.no_solicitud=:pa_no_solicitud ";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_no_solicitud", p_no_solicitud);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}
		public DataTable p_obtenerFirmasResolutivas(Int32 p_no_solicitud)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select usuario_comite 
                                     From dcs_solicitudes_aprobaciones
                                    Where no_solicitud=:pa_no_solicitud
                                      and pendiente_respuesta_B='N'";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_no_solicitud", p_no_solicitud);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}
		public DataTable ObtenerTasaPlazoxDestino(Int32 p_destino_id)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select plazo_destino,tasa_destino 
                                     From dcs_wf_destinos_credito
                                    Where destino_id=:pa_destino_id ";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_destino_id", p_destino_id);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;


		}
		public string p_obtenerObservacionesResolutivas(Int32 p_no_solicitud)
		{
			string vl_retorno = " ";
			string vl_enter = "\r\n";

			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select usuario_comite,observaciones 
                                     from dcs_solicitudes_aprobaciones
                                    Where no_solicitud=:pa_no_solicitud";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_no_solicitud", p_no_solicitud);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{

						while (dr.Read())
						{
							vl_retorno = vl_retorno + dr["usuario_comite"].ToString() + ": " + dr["observaciones"].ToString() + vl_enter;
						}

					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		public DataTable p_obtener_datos_n_excepcion_table(int _noSolicitud)
		{
			//cambio
			try
			{
				string sql = @"select s.codigo_cliente,s.monto_solicitado,s.codigo_agencia_origen,a.nombre_agencia,
                              initcap(trim(c.nombres))||' '||initcap(Trim(c.primer_apellido))||' '||initcap(Trim(c.segundo_apellido)) nombre_cliente,
                              c.lugar_de_trabajo, nvl(c.cargo_que_ocupa, c.nombre_auditores) cargo_que_ocupa,c.fecha_de_ingreso_pais fecha_ingreso_laboral,
                              initcap(sa.desc_sub_aplicacion) desc_sub_aplicacion,
                              d.descripcion_destino, nvl(cta.saldo, 0) saldo, (s2.ingresos + s2.otros_ingresos*0.75) saldo_bruto  
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

				DataTable dt = new DataTable();
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					OracleCommand cmd = new OracleCommand(sql, ConexionOracle);
					cmd.CommandType = CommandType.Text;

					OracleParameter pa_noSolicitud = new OracleParameter("noSolicitud", OracleType.Int32);
					cmd.Parameters.Add(pa_noSolicitud);
					pa_noSolicitud.Direction = ParameterDirection.Input;
					pa_noSolicitud.Value = _noSolicitud;

					OracleDataAdapter da = new OracleDataAdapter(cmd);
					da.Fill(dt);
				}

				return dt;

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		//----------------Analisis Cuantitativo, TIR y CAt-------------------------------------------------
		public DataTable ObtenerValoresLiquidacion(Int32 p_no_solicitud, Int32 p_codigo_sub_aplicacion, Int32 p_destino_credito, int p_edad, int p_plazo, string p_toma_seguro_vida, double p_honorarios_compra_venta, double avaluo_mejoras, double p_monto, double p_cuota, double p_valor_aportmensual, double p_valor_coopsalud)
		{
			double vl_cuota_anticipada = 0;
			double vl_timbres_cooperativos = 0;
			double vl_gastos_administrativos = 0;
			double vl_seguros = 0;
			double vl_seguros_vida = 0;
			double vl_seguro_incendios = 0;
			double vl_seguro_danos = 0;
			double vl_seguros_vida_mensual = 0;
			double vl_seguros_danos_mensual = 0;
			double vl_seguros_incendios_mensual = 0;
			double vl_honorarios_hipoteca = 0;
			double vl_honorarios_compra_venta = 0;
			double vl_capitalizacion = 0;
			double vl_papeleria = 0;
			double vl_cuota_total = 0;
			double vl_cat = 0;
			double vl_tir = 0;

			DataTable dt = new DataTable();
			dt.Columns.Add("cuota_anticipada");
			dt.Columns.Add("prestamos_consolidar_coopsafa");
			dt.Columns.Add("prestamos_consolidar_otros");
			dt.Columns.Add("pagos_terceros");
			dt.Columns.Add("complemento_aportaciones");
			dt.Columns.Add("timbres_cooperativos");
			dt.Columns.Add("honorarios_hipoteca");
			dt.Columns.Add("honorarios_compraventa");
			dt.Columns.Add("capitalizacion");
			dt.Columns.Add("seguro_vida");
			dt.Columns.Add("seguro_danos");
			dt.Columns.Add("seguro_incendios");
			dt.Columns.Add("seguro_vida_mensual");
			dt.Columns.Add("seguro_danos_mensual");
			dt.Columns.Add("seguro_incendios_mensual");
			dt.Columns.Add("gastos_administrativos");
			dt.Columns.Add("central_riesgos");
			dt.Columns.Add("papeleria");
			dt.Columns.Add("cuota_total");
			dt.Columns.Add("tir");
			dt.Columns.Add("cat");
			dt.Columns.Add("GUID");

			#region Calculo del timbres cooperativos
			if (p_codigo_sub_aplicacion > 0)
			{
				if (p_monto <= 10000)
				{
					vl_timbres_cooperativos = 0;
				}
				else if (p_monto <= 20000)
				{
					vl_timbres_cooperativos = 10;
				}
				else if (p_monto <= 50000)
				{
					vl_timbres_cooperativos = 20;
				}
				else if (p_monto <= 100000)
				{
					vl_timbres_cooperativos = 50;
				}
				else
				{
					//de cien en adelante se paga 50 por los primeros 100,000 y despues 10 lempiras por cada cien mil o fraccion de este
					vl_timbres_cooperativos = 50;
					decimal valorxcada_cien = 10M;

					decimal monto_sin_primer_cien = (decimal)p_monto - 100000M;
					decimal cantidad_de_cienes = monto_sin_primer_cien / 100000;
					decimal timbres_por_cada_cien = cantidad_de_cienes * valorxcada_cien;
					decimal mod = monto_sin_primer_cien % 100000;

					//recorre cada cien y suma el valor x cada cien, tiene el cast de int porque si tiene decimal hace una vez mas la vuelta
					//asi solo recorre la cantidad de veces de la parte entera.
					for (int i = 1; i <= (int)cantidad_de_cienes; i++)
					{
						vl_timbres_cooperativos += (double)valorxcada_cien;
					}
					//despues si hay francion en el monto suma otro valor_cada_cien
					//mod devuelve cero sin no hay franciones y mas cero si lo hay
					if (mod > 0)
					{
						vl_timbres_cooperativos += (double)valorxcada_cien;
					}

				}
				if (vl_timbres_cooperativos > 140)
				{
					vl_timbres_cooperativos = 140;
				}
				#region calculos de timbres anteriores
				//else if (p_monto <= 200000)
				//{
				//    vl_timbres_cooperativos = 60;
				//}
				//else if (p_monto <= 300000)
				//{
				//    vl_timbres_cooperativos = 70;
				//}
				//else if (p_monto <= 400000)
				//{
				//    vl_timbres_cooperativos = 80;
				//}
				//else if (p_monto <= 500000)
				//{
				//    vl_timbres_cooperativos = 90;
				//}
				//else
				//{
				//    vl_timbres_cooperativos = 140;
				//}
				#endregion
			}
			#endregion

			#region Calculo del seguro de vida
			if (p_toma_seguro_vida == "S")
			{

				/* Segun ultima comunicacion es 2.90 para automaticos, pero estos no se ingresan a creditscoring*/
				/* queda un 4 lps por millar para todos los demas prestamos, menores a 63 años y mayores o iguales a 64 lps 6 por millar*/
				if (p_edad <= 63)
					vl_seguros_vida = (p_monto / 1000) * 4;
				else
					vl_seguros_vida = (p_monto / 1000) * 6;


				//Esteo fue como se manejo con Marcela,
				//if (p_codigo_sub_aplicacion == 19 || p_codigo_sub_aplicacion == 30 || p_codigo_sub_aplicacion == 42 || p_codigo_sub_aplicacion == 43)
				//{
				//    double dummy = (vl_seguros_vida / 12) * p_plazo;
				//    vl_seguros_vida = dummy;
				//    vl_seguros_vida_mensual = (dummy / p_plazo);

				//}
				//else
				//{
				//    vl_seguros_vida_mensual = vl_seguros_vida / 12;
				//}


				//Ahora a partir Marzo 2018=>, será si el plazo es menor a un año, es proporcional en todos los productos....
				if (p_plazo < 12)
				{
					double dummy = (vl_seguros_vida / 12) * p_plazo;
					vl_seguros_vida = dummy;
					vl_seguros_vida_mensual = (dummy / p_plazo);
				}
				else
				{
					vl_seguros_vida_mensual = vl_seguros_vida / 12;
				}





				#region calculos anterior
				/*
                //Si es CrediOk
                if (p_codigo_sub_aplicacion == 56)
                {
                    double tempo = 0;
                    if (p_edad <= 60)
                    {
                        vl_seguros_vida = (p_monto / 1000) * 7;
                    }
                    else
                    {
                        vl_seguros_vida = (p_monto / 1000) * 9;
                    }
                }

                //Automatico por diferencial ???
                else if (p_codigo_sub_aplicacion == 1)
                {
                    double tempo = 0;
                    if (p_monto > 30000)
                    {
                        tempo = p_monto - 30000;
                    }
                    else
                    {
                        tempo = p_monto;
                    }

                    if (p_edad <= 60)
                    {
                        vl_seguros_vida = (tempo / 1000) * 9;
                    }
                    else
                    {
                        vl_seguros_vida = (tempo / 1000) * 12;
                    }
                }
                //Cualquier otro
                else
                {
                    if (p_edad <= 59)
                    {
                        vl_seguros_vida = p_monto / 1000 * 10;
                    }
                    else
                    {
                        vl_seguros_vida = p_monto / 1000 * 13;
                    }
                }


                if (p_plazo > 6)
                {
                    vl_seguros_vida = vl_seguros_vida * 1;
                    vl_seguros_vida_mensual = vl_seguros_vida / 12;
                }
                else
                {
                    double vl_doceava = vl_seguros_vida / 12;
                    vl_seguros_vida = vl_doceava * p_plazo;
                    vl_seguros_vida_mensual = vl_doceava;
                }            
                 */
				#endregion
			}
			#endregion

			#region Calculo del seguro de incendios
			/// Incendios hipotecarios y arrendamiento
			if (p_codigo_sub_aplicacion == 3 || p_codigo_sub_aplicacion == 14)
			{
				//Segun nuevos cambios se cambio la tasa de 5.5 a 2.75 por millar mas el impto (15%)
				//double tempo = ((p_monto / 1000) * 5.5 * 1.15);

				double tempo = ((avaluo_mejoras / 1000) * 2.75 * 1.15);
				vl_seguro_incendios = tempo;
				vl_seguros_incendios_mensual = vl_seguro_incendios / 12;
			}
			#endregion

			#region Calculo del seguro de daños
			//automotivles dolar y automoviles lempiras
			if (p_codigo_sub_aplicacion == 10 || p_codigo_sub_aplicacion == 32)
			{
				//Segun nuevos cambios la tasa pasa 4.7 a 4.3 en seguro de daños
				//vl_seguro_danos = ((avaluo_mejoras * 4.7) / 100);

				vl_seguro_danos = ((avaluo_mejoras * 3.02) / 100);
				vl_seguros_danos_mensual = vl_seguro_danos / 12;
			}
			#endregion

			#region Calculo honorarios de hipoteca y compra venta
			//Calculado Honorarios de Hipoteca
			if (p_codigo_sub_aplicacion == 3)
			{
				vl_honorarios_hipoteca = ((25000 * 5) / 100) + ((p_monto - 25000) * 1 / 100);
				vl_honorarios_compra_venta = (avaluo_mejoras * 0.5) / 100;
			}
			#endregion

			#region calculo de capitalizacion a aportaciones

			//nuevas politicas a partir del Junio el 2017, gparedes

			if (p_codigo_sub_aplicacion == 3 || p_codigo_sub_aplicacion == 14)
			{
				if (p_codigo_sub_aplicacion == 3)
				{
					if (p_destino_credito == 6 || p_destino_credito == 7 || p_destino_credito == 8 || p_destino_credito == 9 || p_destino_credito == 14)
					{
						vl_capitalizacion = 0;
					}
					else
					{
						vl_capitalizacion = p_monto * 0.02;
					}
				}
				if (p_codigo_sub_aplicacion == 14)
				{
					vl_capitalizacion = 0;
				}
			}
			else
			{
				if (p_codigo_sub_aplicacion == 32)
				{
					vl_capitalizacion = p_monto * 0.01;
				}
				if (p_codigo_sub_aplicacion == 58)
				{
					vl_capitalizacion = p_monto * 0.02;
				}
				if (p_codigo_sub_aplicacion == 2 || p_codigo_sub_aplicacion == 19 || p_codigo_sub_aplicacion == 29 || p_codigo_sub_aplicacion == 56 || p_codigo_sub_aplicacion == 19 || p_codigo_sub_aplicacion == 40 || p_codigo_sub_aplicacion == 18 || p_codigo_sub_aplicacion == 55)
				{
					vl_capitalizacion = p_monto * 0.03;
				}
			}

			//Nueva politica de creditos en lo referente a la capitalización de aportaciones, a partir del 21 de septiembre del 2015
			//Automatico por diferencial ???

			//if (p_codigo_sub_aplicacion == 1)
			//{
			//    vl_capitalizacion = p_monto * 0.10;
			//}
			////hipotecario
			//else if (p_codigo_sub_aplicacion == 3)
			//{
			//    vl_capitalizacion = p_monto * 0.02;
			//}
			////todos los demas
			//else
			//{
			//    //vl_capitalizacion = p_monto * 0.05;
			//    vl_capitalizacion = p_monto * 0.03;
			//}


			//Automatico por diferencial ???
			//if (p_codigo_sub_aplicacion == 1)
			//{
			//    vl_capitalizacion = p_monto * 0.10;
			//}
			////automotivles dolar y automoviles lempiras
			//else if (p_codigo_sub_aplicacion == 10 || p_codigo_sub_aplicacion == 32)
			//{
			//    vl_capitalizacion = p_monto * 0.01;
			//}
			////hipotecario, Arrendamiento y rapicoop batch
			//else if (p_codigo_sub_aplicacion == 3 || p_codigo_sub_aplicacion == 14 || p_codigo_sub_aplicacion == 55)
			//{
			//    vl_capitalizacion = p_monto * 0.02;
			//}
			////Educativo
			//else if (p_codigo_sub_aplicacion == 40)
			//{
			//    vl_capitalizacion = p_monto * 0.03;
			//}
			//else
			//{
			//    //vl_capitalizacion = p_monto * 0.05;
			//    vl_capitalizacion = p_monto * 0.03;
			//}

			#endregion

			#region Calculo de gastos administrativos

			// si es hipocaterio
			if (p_codigo_sub_aplicacion == 3)
			{
				//destinos vivienda es un valor fijo de 2000
				if (p_destino_credito == 6 || p_destino_credito == 7 || p_destino_credito == 8 || p_destino_credito == 9 || p_destino_credito == 14)
				{
					vl_gastos_administrativos = 2000;
				}
				//Cualquuier otro destino de hipotecario es un 1.5 hasta un maximo de 10000   
				else
				{
					double tempo = p_monto * 0.015;
					if (tempo > 10000)
						vl_gastos_administrativos = 10000;
					else
						vl_gastos_administrativos = tempo;
				}
			}
			else
			{
				vl_gastos_administrativos = p_monto * 0.02;
			}


			#region Calculo antes del Feb 2017
			//// si es 13/14 los gastos, extrasalarias sobre cesantia y otras bonificaciones son el 1%,
			//if (p_codigo_sub_aplicacion == 19 || p_codigo_sub_aplicacion == 30 || p_codigo_sub_aplicacion == 42 || p_codigo_sub_aplicacion == 43)
			//{
			//    vl_gastos_administrativos = p_monto * 0.01;
			//}

			//else
			//{

			//    // si es hipocaterio
			//    if (p_codigo_sub_aplicacion == 3)
			//    {
			//        //destinos vivienda es un valor fijo de 2000
			//        if (p_destino_credito == 6 || p_destino_credito == 7 || p_destino_credito == 8 || p_destino_credito == 9 || p_destino_credito == 14)
			//        {
			//            vl_gastos_administrativos = 2000;
			//        }
			//        //Cualquuier otro destino de hipotecario es un 1.5 hasta un maximo de 10000   
			//        else
			//        {
			//            double tempo = p_monto * 0.015;
			//            if (tempo > 10000)
			//                vl_gastos_administrativos = 10000;
			//            else
			//                vl_gastos_administrativos = tempo;
			//        }
			//    }

			//    //Caulquier otro tipo de producto es una tasa de 1.5%
			//    else
			//    {
			//        vl_gastos_administrativos = p_monto * 0.015;
			//    }

			//}
			#endregion

			#region calculos anterior
			////Automatico por diferencial ???
			//if (p_codigo_sub_aplicacion == 1)
			//{
			//    vl_gastos_administrativos = p_monto * 0.006;
			//}
			////CrediOk
			//else if (p_codigo_sub_aplicacion == 56)
			//{
			//    vl_gastos_administrativos = p_monto * 0.01;
			//}
			//else
			//{
			//    vl_gastos_administrativos = p_monto * 0.01;
			//}
			#endregion

			#endregion

			#region cuota anticipada

			vl_seguros = vl_seguros_vida + vl_seguro_incendios + vl_seguro_danos;
			//1 automatico dif, 3 hipotecario , 10 y 32 vehiculos no llevan cuota anticipada
			if (p_codigo_sub_aplicacion == 1 || p_codigo_sub_aplicacion == 3)
			{
				if (p_codigo_sub_aplicacion == 3)
				{
					if (p_destino_credito == 6 || p_destino_credito == 7 || p_destino_credito == 8 || p_destino_credito == 9 || p_destino_credito == 14)
					{
						vl_cuota_anticipada = 0;
					}
					else
					{
						vl_cuota_anticipada = p_cuota + vl_seguros_vida_mensual + p_valor_aportmensual + p_valor_coopsalud;
					}
				}
			}
			else
			{
				if (p_codigo_sub_aplicacion == 19)
				{
					vl_cuota_anticipada = 0;
				}
				else
				{
					vl_cuota_anticipada = p_cuota + vl_seguros_vida_mensual + p_valor_aportmensual + p_valor_coopsalud;
				}
			}

			/*Adendum No.01 -2017, Eliminacion de la cuota anticipada */
			vl_cuota_anticipada = 0;

			#endregion

			vl_cuota_total = p_cuota + vl_seguros_vida_mensual + vl_seguros_danos_mensual + vl_seguros_incendios_mensual + p_valor_aportmensual + p_valor_coopsalud;

			#region calculos de Costos de central de riesgos
			double vl_central_riesgo = 0;
			try
			{
				if (p_codigo_sub_aplicacion == 19 || p_codigo_sub_aplicacion == 52 || p_codigo_sub_aplicacion == 30)
				{
					vl_central_riesgo = 0;
				}
				else
				{
					vl_central_riesgo = double.Parse(ObtenerParametro("WFC-0007"));
				}

			}
			catch
			{
				vl_central_riesgo = 0;
			}
			if (p_codigo_sub_aplicacion == 19)
			{
				vl_central_riesgo = 0;
			}

			#endregion

			#region calculos de papeleria (hipotecarios y mejoras)
			if (p_codigo_sub_aplicacion == 3 || p_codigo_sub_aplicacion == 14)
			{

				try
				{
					vl_papeleria = double.Parse(ObtenerParametro("WFC-0008"));
				}
				catch
				{
					vl_papeleria = 0;
				}
			}
			#endregion

			//--Cat y tir
			double gastos = vl_gastos_administrativos + vl_central_riesgo + vl_papeleria;

			gastos = p_valor_coopsalud;
			string guid_generado = "";

			GenerarCAT(p_no_solicitud, gastos, out guid_generado);

			vl_cat = ObtenerCAT(p_no_solicitud);
			vl_tir = ObtenerTIR(p_no_solicitud);

			dt.Rows.Add(vl_cuota_anticipada, 0, 0, 0, 0, vl_timbres_cooperativos, vl_honorarios_hipoteca, p_honorarios_compra_venta, vl_capitalizacion, vl_seguros_vida, vl_seguro_danos, vl_seguro_incendios,
						vl_seguros_vida_mensual, vl_seguros_danos_mensual, vl_seguros_incendios_mensual, vl_gastos_administrativos, vl_central_riesgo, vl_papeleria, vl_cuota_total, vl_tir, vl_cat, guid_generado);




			return dt;

		}
		public bool p_generar_gastos_desembolso_mcr(string codigo_cliente, DateTime fecha_nacimiento, string p_codigo_sub_aplicacion, Int16 plazo, decimal tasa, decimal monto_solicitado, string codigo_usuario, Int64 no_solicitud)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"MCR.CR_CALCULAR_GASTOS_DESEMBOLSO.calcular_gastos";

					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("p_cod_compania", OracleType.VarChar).Value = "01001001";
					cmd.Parameters.Add("p_cliente", OracleType.VarChar, 30).Value = codigo_cliente;
					cmd.Parameters.Add("p_fecha_nac", OracleType.DateTime, 12).Value = fecha_nacimiento.Day.ToString().PadLeft(2, '0') + "/" + fecha_nacimiento.Month.ToString().PadLeft(2, '0') + "/" + fecha_nacimiento.Year.ToString();
					cmd.Parameters.Add("p_transaccion", OracleType.VarChar).Value = "DB";
					cmd.Parameters.Add("p_prestamo", OracleType.VarChar).Value = p_codigo_sub_aplicacion;
					cmd.Parameters.Add("p_plazo", OracleType.Number).Value = plazo;
					cmd.Parameters.Add("p_tasa", OracleType.Number).Value = tasa;
					cmd.Parameters.Add("p_monsolicitado", OracleType.Number).Value = monto_solicitado;
					cmd.Parameters.Add("p_monfinanciar", OracleType.Number).Direction = ParameterDirection.Output;
					cmd.Parameters.Add("p_moncredito", OracleType.Number).Direction = ParameterDirection.Output;
					cmd.Parameters.Add("p_usuario", OracleType.VarChar).Value = codigo_usuario;
					cmd.Parameters.Add("p_no_solicitud_cs", OracleType.Number).Value = no_solicitud;
					cmd.Parameters.Add("p_ind_simulacion", OracleType.VarChar).Value = 'N';
					cmd.Parameters.Add("p_fecregistro", OracleType.VarChar, 12).Direction = ParameterDirection.Output;
					cmd.Parameters.Add("p_feccobro", OracleType.VarChar, 12).Direction = ParameterDirection.Output;

					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();


					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}
		public bool GenerarCAT(Int32 p_no_solicitud, double p_gastos, out string p_guid)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"DCS_P_CAT";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add("p_no_solicitud", OracleType.Number).Value = p_no_solicitud;
					cmd.Parameters.Add("p_gastos", OracleType.Number).Value = p_gastos;
					cmd.Parameters.Add("p_guid", OracleType.VarChar, 64).Direction = ParameterDirection.Output;

					cmd.ExecuteNonQuery();
					p_guid = cmd.Parameters[2].Value.ToString();
					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}
		public bool p_AlmacenarAnalisisCuantitativo(analisis_cuantitativo analisis)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"DCS_P_INSERTAR_ANALISIS_CUAT";

					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_no_solicitud", OracleType.Number).Value = analisis.no_solicitud;
					cmd.Parameters.Add("pa_coopsalud", OracleType.Number).Value = analisis.coopsalud;
					cmd.Parameters.Add("pa_aportaciones", OracleType.Number).Value = analisis.aportaciones;
					cmd.Parameters.Add("pa_mejora_avalua", OracleType.Number).Value = analisis.mejora_avalua;
					cmd.Parameters.Add("pa_cuota_anticipada", OracleType.Number).Value = analisis.cuota_anticipada;
					cmd.Parameters.Add("pa_prestamos_consolidar_csf", OracleType.Number).Value = analisis.prestamos_consolidar_csf;
					cmd.Parameters.Add("pa_prestamos_consolidar_otros", OracleType.Number).Value = analisis.prestamos_consolidar_otros;
					cmd.Parameters.Add("pa_pagos_terceros", OracleType.Number).Value = analisis.pagos_terceros;
					cmd.Parameters.Add("pa_complemento_aportaciones", OracleType.Number).Value = analisis.complemento_aportaciones;
					cmd.Parameters.Add("pa_timbres_cooperativos", OracleType.Number).Value = analisis.timbres_cooperativos;
					cmd.Parameters.Add("pa_honorarios_hipoteca", OracleType.Number).Value = analisis.honorarios_hipoteca;
					cmd.Parameters.Add("pa_honorarios_compraventa", OracleType.Number).Value = analisis.honorarios_compraventa;
					cmd.Parameters.Add("pa_capitalizacion", OracleType.Number).Value = analisis.capitalizacion;
					cmd.Parameters.Add("pa_seguro_vida", OracleType.Number).Value = analisis.seguro_vida;
					cmd.Parameters.Add("pa_seguro_danos", OracleType.Number).Value = analisis.seguro_danos;
					cmd.Parameters.Add("pa_seguro_incendios", OracleType.Number).Value = analisis.seguro_incendios;
					cmd.Parameters.Add("pa_seguro_vida_mensual", OracleType.Number).Value = analisis.seguro_vida_mensual;
					cmd.Parameters.Add("pa_seguro_danos_mensual", OracleType.Number).Value = analisis.seguro_danos_mensual;
					cmd.Parameters.Add("pa_seguro_incendios_mensual", OracleType.Number).Value = analisis.seguro_incendios_mensual;
					cmd.Parameters.Add("pa_gastos_administrativos", OracleType.Number).Value = analisis.gastos_administrativos;
					cmd.Parameters.Add("pa_papeleria", OracleType.Number).Value = analisis.papeleria;
					cmd.Parameters.Add("pa_avaluo_final", OracleType.Number).Value = analisis.avaluo_final;
					cmd.Parameters.Add("pa_total_deducciones", OracleType.Number).Value = analisis.total_deducciones;
					cmd.Parameters.Add("pa_total_desembolso", OracleType.Number).Value = analisis.total_desembolso;
					cmd.Parameters.Add("pa_cuota_nivelada", OracleType.Number).Value = analisis.cuota_nivelada;
					cmd.Parameters.Add("pa_cuota_total", OracleType.Number).Value = analisis.cuota_total;
					cmd.Parameters.Add("pa_central_riesgos", OracleType.Number).Value = analisis.central_riesgos;
					cmd.Parameters.Add("pa_xml_deducciones_MCR", OracleType.VarChar).Value = "";
					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();
					cmd.Dispose();
					vl_retorno = true;
					return vl_retorno;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}
		public float ObtenerCAT(Int32 p_no_solicitud)
		{
			float vl_retorno = 0;

			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select nvl(CAT,0) cat from dcs_solicitudes_indices where no_solicitud=:pa_no_solicitud";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_no_solicitud", p_no_solicitud);
					ConexionOracle.Open();
					dr = cmd.ExecuteReader();
					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = float.Parse(dr["cat"].ToString());

					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}
		public float ObtenerTIR(Int32 p_no_solicitud)
		{
			float vl_retorno = 0;

			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select nvl(TIR,0) tir from dcs_solicitudes_indices where no_solicitud=:pa_no_solicitud";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_no_solicitud", p_no_solicitud);
					ConexionOracle.Open();
					dr = cmd.ExecuteReader();
					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = float.Parse(dr["tir"].ToString());

					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		//No se usa
		public DataTable ObtenerDataParaTIR(string p_guid)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();

			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select cuota_simple from cs_util_data_tir_cat where guid=:pa_guid AND ROWnum<20
                                            order by no_pago";

					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_guid", p_guid);
					OracleDataAdapter da = new OracleDataAdapter(cmd);
					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		public DataTable ObtenerGastosDesembolsoMCR(string p_codigo_cliente, string p_no_solicitud_cs)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();

			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select a.cod_tipocobro,des_tipocobro,mon_cobro,mon_cobro_original,cod_moneda_cobro
                                     from cr_deta_cargos_financiados a,
                                          cr_tiposcobro_enca b 
                                    Where cod_cliente=:pa_codigo_cliente 
                                      and no_solicitud_cs=:pa_no_solicitud_cs
                                      and a.COD_TIPOCOBRO = b.COD_TIPOCOBRO";

					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_codigo_cliente", p_codigo_cliente);
					cmd.Parameters.AddWithValue("pa_no_solicitud_cs", p_no_solicitud_cs);
					OracleDataAdapter da = new OracleDataAdapter(cmd);
					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}
		public float ObtenerInteresTotalTIRCAT(string p_guid)
		{
			float vl_retorno = 0;

			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select sum(interes) interes_total from cs_util_data_tir_cat where guid=:pa_guid";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_guid", p_guid);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = 0;
						float.TryParse(dr["interes_total"].ToString(), out vl_retorno);
					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}
		public DataTable ObtenerDataGeneradaTirCat(string p_guid)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();

			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select * from cs_util_data_tir_cat where guid=:pa_guid order by no_pago";

					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_guid", p_guid);
					OracleDataAdapter da = new OracleDataAdapter(cmd);
					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;

		}
		public bool ActualizarMontoGastosMCRxCodCobro(Int32 p_no_solicitud, string p_cod_cobro, decimal p_monto_cobronuevo)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Update cr_deta_cargos_financiados set
                                           mon_cobro=:pa_mon_cobro,
                                           mon_cobro_original=:pa_mon_cobro
                                     Where no_solicitud_cs=:pa_no_solicitud
                                       and cod_tipocobro=:pa_cod_tipocobro";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_mon_cobro", OracleType.Number).Value = p_monto_cobronuevo;
					cmd.Parameters.Add("pa_no_solicitud", OracleType.Number).Value = p_no_solicitud;
					cmd.Parameters.Add("pa_cod_tipocobro", OracleType.VarChar).Value = p_cod_cobro;

					ConexionOracle.Open();
					cmd.ExecuteNonQuery();

					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}
			return vl_retorno;
		}
		//------------------Concentracion Crediticia----------------------------------------------------
		public bool TieneNucleoFamiliarconCreditos(Int32 p_codigo_cliente)
		{
			#region Comentarios sobre el union en Query
			//GPAREDES
			//01/08/2018
			//Temporalmente se utiliza un union para determinar si hay dependientes con saldos para la concentración crediticia, ya que inicialmente estaba en la tabla dependientes,
			//pero posteriormente se hizo un nuevo desarrollo en donde estan solo los dependientes con las caracteristicas necesarias para consideralos candidatos a analisis de concentracion
			//entonces con van paulatinamente incorporando Filiales se hace un union, las filiales usan una nueva forma qe llena mgi_nucleo_familiar y no llenaria la tabla dependientes.
			#endregion

			bool vl_retorno = false;

			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					/*string sql = @"Select sum(n) n
                              from (
                                   Select count(*) n
                                     From cr_detalle_cartera a,
                                          mgi_dependientes b,
                                          mgi_clientes c
                                    Where b.codigo_cliente=:pa_codigo_cliente                                                                            
                                      and b.cod_afil_dependiente=a.cod_cliente(+)
                                      and b.cod_afil_dependiente=c.codigo_cliente(+)                                                                            
                                      and cod_parentesco in (4,8,11)                                      
                                      and nvl(reside_casa,'N')='S'          
                                      and cod_afil_dependiente is not null                            
                                      and NVL(dependencia_economica,'S')='N'
                                   UNION                                      
                                    Select count(*) n
                                      from cr_detalle_cartera a, 
                                           mgi_nucleo_familiar b 
                                     Where b.codigo_cliente=:pa_codigo_cliente 
                                       and a.cod_cliente=b.cod_cliente_familiar)";*/
					string sql = @" Select count(*) n
                                      from cr_detalle_cartera a, 
                                           mgi_nucleo_familiar b 
                                     Where b.codigo_cliente=:pa_codigo_cliente 
                                       and a.cod_cliente=b.cod_cliente_familiar";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_codigo_cliente", p_codigo_cliente);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						Int16 vl_n = Int16.Parse(dr["n"].ToString());
						if (vl_n > 0)
						{
							vl_retorno = true;
						}
					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}
			return vl_retorno;
		}
		public DataTable ObtenerCreditosVigenxNucleoFami(string p_codigo_cliente)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					/*string sql = @"Select id_cliente,cod_cliente,nom_cliente,parentesco,des_tipopres,mon_saldo_principal 
                                     From cr_detalle_cartera a,
                                          mgi_dependientes b,
                                          mgi_clientes c
                                    Where b.codigo_cliente=:pa_codigo_cliente                                                                            
                                      and b.cod_afil_dependiente=a.cod_cliente(+)
                                      and b.cod_afil_dependiente=c.codigo_cliente(+)                                                                            
                                      and cod_parentesco in (4,8,11)                                      
                                      and nvl(reside_casa,'N')='S'          
                                      and cod_afil_dependiente is not null                            
                                      and NVL(dependencia_economica,'S')='N'";*/
					/* string sql = @" Select id_cliente,cod_cliente,nom_cliente,parentesco,des_tipopres,mon_saldo_principal 
                                      From cr_detalle_cartera a,
                                           mgi_dependientes b,
                                           mgi_clientes c
                                     Where b.codigo_cliente =:pa_codigo_cliente
                                       and b.cod_afil_dependiente = a.cod_cliente(+)
                                       and b.cod_afil_dependiente = c.codigo_cliente(+)
                                       and cod_parentesco in (4,8,11)                                      
                                       and nvl(reside_casa,'N')= 'S'
                                       and cod_afil_dependiente is not null
                                       and NVL(dependencia_economica,'S')= 'N'
                                     UNION
                                    Select id_cliente,cod_cliente,nom_cliente,d.descripcion,des_tipopres,mon_saldo_principal
                                      From cr_detalle_cartera a,
                                           mgi_nucleo_familiar b,
                                           mgi_clientes c,
                                           mgi_tipos_ref_personales d
                                     Where b.codigo_cliente =:pa_codigo_cliente
                                       and b.cod_cliente_familiar = a.cod_cliente(+)
                                       and b.cod_cliente_familiar = c.codigo_cliente(+)
                                       and b.cod_parentesco = d.codigo_referencia_personal ";*/
					string sql = @"Select id_cliente,cod_cliente,nom_cliente,d.descripcion,des_tipopres,mon_saldo_principal
                                     From cr_detalle_cartera a,
                                          mgi_nucleo_familiar b,
                                          mgi_clientes c,
                                          mgi_tipos_ref_personales d
                                    Where b.codigo_cliente =:pa_codigo_cliente
                                      and b.cod_cliente_familiar = a.cod_cliente(+)
                                      and b.cod_cliente_familiar = c.codigo_cliente(+)
                                      and b.cod_parentesco = d.codigo_referencia_personal ";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_codigo_cliente", p_codigo_cliente);
					OracleDataAdapter da = new OracleDataAdapter(cmd);
					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}
		public DataTable ObtenerCreditosVigenxDeudor(string p_codigo_cliente)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select cod_cliente,des_tipopres,mon_saldo_principal 
                                     from cr_detalle_cartera a
                                    where cod_cliente=:pa_codigo_cliente";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_codigo_cliente", p_codigo_cliente);
					OracleDataAdapter da = new OracleDataAdapter(cmd);
					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}
		public double ObtenerInfoPatrimonio()
		{
			double vl_retorno = 0;

			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select monto 
                                     from DCS_WF_PATRIMONIOINFO                                          
                                    where no_entrada=(select max(no_entrada) from DCS_WF_PATRIMONIOINFO)";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = 0;
						double.TryParse(dr["monto"].ToString(), out vl_retorno);
					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}
		//----------------------------------------------------------------------
		public DataTable p_firmas_miembro1(Int32 p_no_solicitud, string p_usuario)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select usuario_comite,b.codigo_cliente,
                                          b.nombres||' '||primer_apellido nombre_miembro1,
                                          decode(DCS_F_OBTENER_PARAMETRO('WFC-0013'),'S',firma,null) firma_miembro1
                                     From dcs_solicitudes_aprobaciones a,
                                          mgi_usuarios b,
                                          mrh_empleados_firmas c
                                    Where no_solicitud=:pa_no_solicitud
                                      and a.usuario_comite=b.codigo_usuario
                                      and b.codigo_cliente=c.codigo_cliente(+)
                                      and usuario_comite=:pa_usuario_miembro";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_no_solicitud", p_no_solicitud);
						da.SelectCommand.Parameters.Add("pa_usuario_miembro", p_usuario);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}
		public DataTable p_firmas_miembro2(Int32 p_no_solicitud, string p_usuario)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select usuario_comite,b.codigo_cliente,
                                          b.nombres||' '||primer_apellido nombre_miembro2,
                                          decode(DCS_F_OBTENER_PARAMETRO('WFC-0013'),'S',firma,null) firma_miembro2
                                     From dcs_solicitudes_aprobaciones a,
                                          mgi_usuarios b,
                                          mrh_empleados_firmas c
                                    Where no_solicitud=:pa_no_solicitud
                                      and a.usuario_comite=b.codigo_usuario
                                      and b.codigo_cliente=c.codigo_cliente(+)
                                      and usuario_comite=:pa_usuario_miembro";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_no_solicitud", p_no_solicitud);
						da.SelectCommand.Parameters.Add("pa_usuario_miembro", p_usuario);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}
		public DataTable p_firmas_miembro3(Int32 p_no_solicitud, string p_usuario)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select usuario_comite,b.codigo_cliente,
                                          b.nombres||' '||primer_apellido nombre_miembro3,
                                          decode(DCS_F_OBTENER_PARAMETRO('WFC-0013'),'S',firma,null) firma_miembro3
                                     From dcs_solicitudes_aprobaciones a,
                                          mgi_usuarios b,
                                          mrh_empleados_firmas c
                                    Where no_solicitud=:pa_no_solicitud
                                      and a.usuario_comite=b.codigo_usuario
                                      and b.codigo_cliente=c.codigo_cliente(+)
                                      and usuario_comite=:pa_usuario_miembro";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_no_solicitud", p_no_solicitud);
						da.SelectCommand.Parameters.Add("pa_usuario_miembro", p_usuario);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		//No se usa
		public DataTable p_datos_rpt01Solicitud(Int32 p_no_solicitud)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @" select '171998' numero_afiliado,'Hermin' primer_nombre,'Giovany' segundo_nombre,'Paredes' primer_apellido from dual";

					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						//da.SelectCommand.Parameters.Add("pa_no_acta", p_no_solicitud);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		public DataTable p_datos_AnalisisCualitativo(Int32 p_no_solicitud)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"SELECT s2.codigo_cliente,
                                          s2.numero_identificacion,
                                          nombres||' '||primer_apellido||' '||segundo_apellido nombre_completo,
                                          s1.monto_solicitado,
                                          (to_number(to_char(sysdate,'YYYY')) - to_number(to_char(fecha_de_nacimiento,'YYYY'))) -  
                                          case  
                                            when to_char(sysdate,'MMDD') < to_char(fecha_de_nacimiento,'MMDD') then 1  
                                          else 0  
                                          end  edad,
                                          cargo,
                                          patrono,
                                          fecha_inicio_relacion,
                                          tasa,
                                          plazo,
                                          desc_sub_aplicacion producto,
                                          DE.DESCRIPCION_DESTINO,
                                          antiguedad_laboral,
                                          resultado_buro,
                                          decode(DCS_F_AFILIADO_X_PLANILLA(s2.codigo_cliente),'S','PLANILLA','VENTANILLA') ventanilla_planilla,
                                          monto_balance_consolidar,
                                          no_identidad_codeudor,
                                          primer_nombre_codeudor||' '||segundo_nombre_codeudor||' '||primer_apellido_codeudor||' '||segundo_apellido_codeudor nombre_codeudor,
                                          cargo_codeudor,
                                          patrono_codeudor,
                                          antiguedad_laboral_codeudor,                                                                                   
                                          no_identidad_aval1,
                                          primer_nombre_aval1||' '||segundo_nombre_aval1||' '||primer_apellido_aval1||' '||segundo_apellido_aval1 nombre_aval1,
                                          cargo_aval1,
                                          patrono_aval1,
                                          antiguedad_laboral_aval1,
                                          no_identidad_aval2,
                                          primer_nombre_aval2||' '||segundo_nombre_aval2||' '||primer_apellido_aval2||' '||segundo_apellido_aval2 nombre_aval2,
                                          cargo_aval2,
                                          patrono_aval2,
                                          antiguedad_laboral_aval2,
                                          comentarios_filial
                                     from mgi_clientes cl ,
                                          dcs_solicitudes2 s2,
                                          dcs_solicitudes s1,
                                          mgi_sub_aplicaciones sa,
                                          dcs_wf_destinos_credito de
                                    where cl.codigo_empresa=1
                                      and s1.no_solicitud=s2.no_solicitud
                                      and cl.codigo_cliente=s2.codigo_cliente
                                      and s2.codigo_sub_aplicacion=sa.codigo_sub_aplicacion
                                      and s2.destino=de.destino_id
                                      and s2.no_solicitud=:pa_no_solicitud";

					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_no_solicitud", p_no_solicitud);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}
		public DataTable p_datos_AnalisisCuantitativo(Int32 p_no_solicitud)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select s.no_solicitud,
                                          s.codigo_agencia_origen,
                                          nombre_agencia,
                                          s.estado_solicitud_id,
                                          s2.codigo_sub_aplicacion,
                                          desc_sub_aplicacion producto,
                                          f.descripcion_fuente,             
                                          guid,
                                          nvl(tir,0) tir,
                                          nvl(cat,0) cat,                                    
                                          cl.codigo_cliente,nombres||' '||primer_apellido||' '||segundo_apellido nombre_completo,   
                                          --nvl(round((sysdate - fecha_de_nacimiento) /365),0) edad,    
                                          trunc((trunc(sysdate) - to_date(fecha_de_nacimiento))/365,0) edad,
                                          trunc(fecha_de_nacimiento) fecha_de_nacimiento,
                                          s2.monto_solicitado,
                                          s2.plazo plazo_sol,
                                          s2.tasa tasa_sol,
                                          s2.monto_aprobado,
                                          s2.tasa_aprobada,
                                          s2.plazo_aprobado,
                                          s2.destino,
                                          valor_compra_venta,
                                          cuota_nivelada,aportacion,seguro_vida,seguro_incendio,seguro_danos,seguro_vida_mensual,seguro_incendios_mensual,seguro_danos_mensual,coopsalud,
                                          cuota_total,cuota_anticipada,consolidar_coopsafa,consolidar_otros,pago_terceros,a.complemento_aportaciones,timbres_cooperativistas,
                                          honorarios_hipoteca,honorarios_compra_venta,capitalizacion,gastos_administrativos,papeleria,avaluo_final,total_deducciones,neto_recibir,edad,                                          
                                          decision_final_solicitud,
                                          s2.ingresos,
                                          s2.otros_ingresos,
                                          s2.deducciones,
                                          (s2.ingresos + s2.otros_ingresos) - s2.deducciones total_ingresos_sol,
                                          central_riesgos,                                 
                                          deducciones_mcr     
                                     from dcs_solicitudes s,
                                          dcs_solicitudes2 s2,
                                          dcs_solicitudes_analisis_cuant a,
                                          mgi_clientes cl,
                                          mgi_sub_aplicaciones sa,
                                          mgi_agencias ag,
                                          dcs_wf_fuentes_financiamiento f,
                                          dcs_solicitudes_indices i
                                    Where cl.codigo_empresa=1
                                      and sa.codigo_empresa=1
                                      and s.codigo_cliente=cl.codigo_cliente
                                      and s.codigo_fuente=f.codigo_fuente(+)
                                      and s.no_solicitud=i.no_solicitud(+)
                                      and s.codigo_sub_aplicacion=sa.codigo_sub_aplicacion
                                      and s.no_solicitud=s2.no_solicitud
                                      and s.codigo_agencia_origen=ag.codigo_agencia      
                                      and a.no_solicitud(+)=s.no_solicitud 
                                      and s.no_solicitud=:pa_no_solicitud";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_no_solicitud", p_no_solicitud);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}
		public DataTable p_datos_croquis(Int32 p_no_solicitud)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select 1,'SOLICITANTE' ROL,
                                          numero_identificacion,
                                          cl.codigo_cliente,
                                          nombres||' '||primer_apellido||' '||segundo_apellido nombre,       
                                          dire_res.direccion_res,
                                          dire_res.telefono_casa telefono_fijo,
                                          to_char(dire_res.celular) telefono_celular                                                                                                                                                                                                            
                                     From mgi_clientes cl,
                                          dcs_solicitudes s,                                                                                                               
                                          (select codigo_empresa,codigo_cliente,
                                                  dire_res.telefonos telefono_casa,
                                                  dire_res.fax celular,
                                                  dire_res.correo correo,
                                                  dire_res.nomenclatura_2 direccion_res
                                             from mgi_direcciones dire_res
                                            Where dire_res.codigo_direccion=1 
                                              and codigo_empresa=1) dire_res                                                  
                                    where 1=1
                                      and cl.codigo_empresa=1
                                      and s.codigo_cliente=cl.codigo_cliente                                                                                                                 
                                      and cl.codigo_cliente=dire_res.codigo_cliente(+)                                                                                
                                      and s.no_solicitud=:pa_no_solicitud              
                                    UNION
                                   Select 2,'CODEUDOR' rol,
                                          NO_IDENTIDAD_CODEUDOR,
                                          codigo_cliente_codeudor,
                                          primer_nombre_codeudor||' '||segundo_nombre_codeudor||' '||primer_apellido_codeudor||' '||segundo_apellido_codeudor nombre,direccion_res_codeudor,telefono_fijo_codeudor telefono_fijo,celular_codeudor telefono_celular
                                     from dcs_solicitudes2
                                    Where no_solicitud=:pa_no_solicitud
                                    UNION
                                   Select 3,'AVAL1' rol,
                                          NO_IDENTIDAD_AVAL1, 
                                          codigo_cliente_aval1,
                                          primer_nombre_AVAL1||' '||segundo_nombre_AVAL1||' '||primer_apellido_AVAL1||' '||segundo_apellido_AVAL1 nombre,direccion_res_AVAL1,telefono_fijo_aval1 telefono_fijo,celular_aval1 telefono_celular
                                     From dcs_solicitudes2
                                    Where no_solicitud=:pa_no_solicitud
                                    UNION
                                   Select 4,'AVAL2' rol,
                                          NO_IDENTIDAD_AVAL2,
                                          codigo_cliente_aval2, 
                                          primer_nombre_AVAL2||' '||segundo_nombre_AVAL2||' '||primer_apellido_AVAL2||' '||segundo_apellido_AVAL2 nombre,direccion_res_AVAL2,telefono_fijo_aval2 telefono_fijo,celular_aval2 telefono_celular
                                     From dcs_solicitudes2
                                    Where no_solicitud=:pa_no_solicitud
                                    Order by 1";

					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_no_solicitud", p_no_solicitud);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}
		public DataTable p_datos_AnalistaObserva(Int32 p_no_solicitud)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select s1.codigo_cliente,s1.no_solicitud,s2.numero_identificacion,cl.nombres||cl.primer_apellido||' '||cl.segundo_apellido nombre_completo,desc_sub_aplicacion producto,
                                          s1.monto_solicitado monto_solicitado,plazo plazo_sol,tasa tasa_sol,oficial_servicio usuario_analista,u.nombres||' '||u.primer_apellido||' '||u.segundo_apellido nombre_analista,
                                          observaciones_analista
                                     from dcs_solicitudes s1,
                                          dcs_solicitudes2 s2,
                                          mgi_clientes cl,
                                          mgi_sub_aplicaciones sa,
                                          mgi_usuarios u
                                    where s1.no_solicitud=s2.no_solicitud
                                      and cl.codigo_empresa=1
                                      and u.codigo_empresa=cl.codigo_empresa
                                      and s1.codigo_cliente=cl.codigo_cliente
                                      and s1.codigo_sub_aplicacion=sa.codigo_sub_aplicacion
                                      and s1.oficial_servicio=u.codigo_usuario
                                      and s1.no_solicitud=:pa_no_solicitud";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_no_solicitud", p_no_solicitud);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}
		public DataTable p_datos_PrestamosObserva(Int32 p_no_solicitud)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select s1.codigo_cliente,s1.no_solicitud,s2.numero_identificacion,cl.nombres||cl.primer_apellido||' '||cl.segundo_apellido nombre_completo,desc_sub_aplicacion producto,
                                          s1.monto_solicitado monto_solicitado,plazo plazo_sol,tasa tasa_sol,oficial_servicio usuario_analista,u.nombres||' '||u.primer_apellido||' '||u.segundo_apellido nombre_analista,
                                          observaciones_prestamos,codigo_agencia_origen,ag.nombre_agencia
                                     from dcs_solicitudes s1,
                                          dcs_solicitudes2 s2,
                                          mgi_clientes cl,
                                          mgi_sub_aplicaciones sa,
                                          mgi_usuarios u,
                                          mgi_agencias ag
                                    where s1.no_solicitud=s2.no_solicitud
                                      and cl.codigo_empresa=1
                                      and u.codigo_empresa=cl.codigo_empresa
                                      and s1.codigo_cliente=cl.codigo_cliente
                                      and s1.codigo_sub_aplicacion=sa.codigo_sub_aplicacion
                                      and s1.oficial_servicio=u.codigo_usuario
                                      and s1.codigo_Agencia_origen=ag.codigo_Agencia
                                      and s1.no_solicitud=:pa_no_solicitud";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_no_solicitud", p_no_solicitud);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}
		public DataTable p_datos_HojaRuta(Int32 p_no_solicitud)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select no_solicitud,
                                          enviado_por,
                                          fecha_envio,
                                          estacion_id_from,
                                          '' nombre_from,
                                          estacion_id_to,
                                          e1.nombre as nombre_to,
                                          no_movimiento,                                    
                                          analista,
                                          'Creación de la solicitud' decision,     
                                          antiguedad_meses,
                                          antiguedad_dias,
                                          antiguedad_horas,
                                          antiguedad_minutos,
                                          antiguedad_segundos,
                                          estadia_movimiento
                                     From dcs_movimientos_solicitudes ms, 
                                          dcs_wf_estaciones e1,
                                          dcs_wf_decisiones d
                                    Where ms.estacion_id_to=e1.estacion_id                                
                                      and ms.decision_id=d.decision_id(+)
                                      and ms.estacion_id_from=0
                                      and no_solicitud= :pa_no_solicitud                                                              
                                      UNION
                                   Select no_solicitud ,
                                          enviado_por,
                                          fecha_envio,
                                          estacion_id_from,
                                          e1.nombre as nombre_from,
                                          estacion_id_to,
                                          e2.nombre as nombre_to,
                                          no_movimiento,                                    
                                          analista,
                                          d.descripcion decision,     
                                          antiguedad_meses,
                                          antiguedad_dias,
                                          antiguedad_horas,
                                          antiguedad_minutos,
                                          antiguedad_segundos,
                                          estadia_movimiento
                                     From dcs_movimientos_solicitudes ms, 
                                          dcs_wf_estaciones e1,
                                          dcs_wf_estaciones e2,
                                          dcs_wf_decisiones d
                                    Where ms.estacion_id_from=e1.estacion_id
                                      and ms.decision_id=d.decision_id
                                      and ms.estacion_id_to=e2.estacion_id
                                      and no_solicitud= :pa_no_solicitud
                                 Order by no_movimiento   ";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_no_solicitud", p_no_solicitud);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}
		public DataTable p_datos_HojaRuta2(Int32 p_no_solicitud)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select c.numero_identificacion,s1.codigo_cliente,c.nombres||' '||c.primer_apellido||' '||c.segundo_apellido nombre_completo,                                          
                                          s2.monto_solicitado,s2.plazo,S2.TASA,s2.codigo_sub_aplicacion,desc_sub_aplicacion producto,s1.no_solicitud,                                          
                                          oficial_servicio,
                                          u.nombres||' '||u.primer_apellido||' '||u.segundo_apellido nombre_oficial,
                                          s1.codigo_agencia_origen,
                                          nombre_agencia
                                     from dcs_solicitudes s1,
                                          dcs_solicitudes2 s2,
                                          mgi_clientes c,
                                          mgi_sub_aplicaciones sa,
                                          mgi_usuarios u,
                                          mgi_agencias ag                                          
                                    Where C.CODIGO_EMPRESA=CR_GLOBALES.C_EMPRESA        
                                      and s1.codigo_cliente=c.codigo_cliente
                                      and s1.no_solicitud=s2.no_solicitud
                                      and s2.codigo_sub_aplicacion=sa.codigo_sub_aplicacion
                                      and u.codigo_usuario=oficial_servicio
                                      and s1.codigo_agencia_origen=ag.codigo_agencia
                                      and s1.no_solicitud=:pa_no_solicitud  ";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_no_solicitud", p_no_solicitud);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}
		public DataTable p_datos_controldocs(Int32 p_no_solicitud)
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("persona");
			dt.Columns.Add("documento_id");
			dt.Columns.Add("descripcion");
			dt.Columns.Add("tipo_exigencia");
			dt.Columns.Add("adjunto");
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					//                    string sql = @"select dw.documento_id,d.descripcion,tipo_exigencia,DCS_F_TIENE_ADJUTOXDOC_ID(s.no_solicitud,dw.documento_id) adjunto
					//                                     from dcs_wf_documentos_workflow dw,       
					//                                          dcs_wf_tipo_documentos d,       
					//                                          dcs_solicitudes s       
					//                                    where s.no_solicitud=:pa_no_solicitud
					//                                      and dw.codigo_sub_aplicacion=s.codigo_sub_aplicacion
					//                                      and dw.documento_id=d.documento_id               
					//                                    Order by tipo_exigencia desc,documento_id";
					string sql = @"Select decode(d.persona_solicitud,'P1','SOLICITANTE','P2','CODEUDOR','P3','AVAL1','P4','AVAL2') PERSONA,
                                          dw.documento_id,d.descripcion,tipo_exigencia,DCS_F_TIENE_ADJUTOXDOC_ID(s.no_solicitud,dw.documento_id) adjunto
                                     from dcs_wf_documentos_workflow dw,       
                                          dcs_wf_tipo_documentos d,       
                                          dcs_solicitudes s,
                                          dcs_solicitudes2 s2       
                                    where s.no_solicitud=:pa_no_solicitud
                                      and dw.codigo_sub_aplicacion=s.codigo_sub_aplicacion
                                      and dw.documento_id=d.documento_id
                                      and s.no_solicitud=s2.no_solicitud
                                      and D.PERSONA_SOLICITUD in ('P1',
                                                                  decode(requiere_codeudor,'S','P2'),
                                                                  decode(requiere_aval1,'S','P3'),
                                                                  decode(requiere_aval2,'S','P4'))
                                    Order by persona_solicitud,orden_in_grupo asc,documento_id
                                    --Order by tipo_exigencia desc,documento_id";
					try
					{
						cmd.CommandText = sql;
						cmd.Connection = ConexionOracle;
						cmd.Parameters.Add("pa_no_solicitud", p_no_solicitud);
						ConexionOracle.Open();
						dr = cmd.ExecuteReader();

						if (dr.HasRows)
						{
							while (dr.Read())
							{
								dt.Rows.Add(dr["persona"],
											dr["documento_id"],
											dr["descripcion"],
											dr["tipo_exigencia"],
											dr["adjunto"]);
							}
						}
						dr.Close();
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}
		public DataTable p_datos_reporte1(DateTime d1, DateTime d2, string p_regional, string p_filial, string p_estado, string p_no_solicitud)
		{
			string scondi_fechas = "";
			string scondi_region = "";
			string scondi_filial = "";
			string scondi_estado = "";
			string scondi_nosolic = "";
			scondi_fechas = " and fecha_presentacion between :pa_date1 and :pa_date2";
			if (!string.IsNullOrEmpty(p_regional))
			{
				scondi_region = " and az.codigo_zona=" + p_regional;
			}
			if (!string.IsNullOrEmpty(p_filial))
			{
				scondi_filial = " and s1.codigo_agencia_origen=" + p_filial;
			}
			if (!string.IsNullOrEmpty(p_estado))
			{
				if (p_estado == "0")
				{
					scondi_estado = " and s1.estado_solicitud_id>=" + p_estado;
				}
				else
				{
					scondi_estado = " and s1.estado_solicitud_id=" + p_estado;
				}

			}
			if (!string.IsNullOrEmpty(p_no_solicitud))
			{
				scondi_nosolic = "and s1.no_solicitud=" + p_no_solicitud;
			}
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					//lA SUMA DE LAS ESTACIONES POR SOLICITUD
					string sql = @"select s1.no_solicitud,s1.codigo_cliente,nombres||' '||primer_apellido||' '||segundo_apellido nombre_afiliado,
                                          fecha_presentacion,s1.monto_solicitado,s2.monto_aprobado,sa.desc_sub_aplicacion,descripcion_destino,nombre_agencia,az.codigo_zona,
                                          dcs_f_obt_comite_resol(s1.no_solicitud) comite_resol,nombre nombre_comite_resol,
                                          estacion,meses,dias,horas,minutos,segundos,
                                          s1.antiguedad_meses antiguedad_total_meses,
                                          s1.antiguedad_dias antiguedad_total_dias,
                                          s1.antiguedad_horas antigueda_total_horas,
                                          s1.antiguedad_minutos antigueda_total_minutos 
                                     from dcs_solicitudes s1,
                                          dcs_solicitudes2 s2,
                                          mgi_sub_aplicaciones sa,
                                          mgi_agencias age,
                                          dcs_wf_destinos_credito dest,
                                          cef_agencias_zona az,
                                          mgi_clientes cl,
                                          dcs_wf_estaciones est,
                                          (select no_solicitud,nombre estacion,
                                                  sum(antiguedad_meses) meses,
                                                  sum(antiguedad_dias) dias,
                                                  sum(antiguedad_horas) horas,
                                                  sum(antiguedad_minutos) minutos,
                                                  sum(antiguedad_segundos) segundos                                             
                                             from dcs_movimientos_solicitudes ms,
                                                  dcs_wf_estaciones e
                                            Where ms.estacion_id_to=e.estacion_id(+)
                                            group by no_solicitud,nombre ) d         
                                    Where s1.no_solicitud=d.no_solicitud
                                      and s1.no_solicitud=s2.no_solicitud
                                      and s1.codigo_sub_aplicacion=sa.codigo_sub_aplicacion
                                      and s2.destino=dest.destino_id
                                      and s1.codigo_agencia_origen=age.codigo_agencia
                                      and s1.codigo_cliente=cl.codigo_cliente
                                      and age.codigo_agencia=az.codigo_agencia                                       
                                      and est.estacion_id=dcs_f_obt_comite_resol(s1.no_solicitud) " +
									  scondi_fechas +
									  scondi_region +
									  scondi_filial +
									  scondi_estado +
									  scondi_nosolic + @"
                                    Order by no_solicitud";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_date1", d1);
						da.SelectCommand.Parameters.Add("pa_date2", d2);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}
		public DataTable p_datos_reporte2(DateTime d1, DateTime d2, string p_regional, string p_filial, string p_estado, string p_estacion_actual, string p_no_solicitud)
		{
			string scondi_fechas = "";
			string scondi_region = "";
			string scondi_filial = "";
			string scondi_estado = "";
			string scondi_nosolic = "";
			scondi_fechas = " and fecha_presentacion between :pa_date1 and :pa_date2";
			if (!string.IsNullOrEmpty(p_regional))
			{
				scondi_region = " and az.codigo_zona=" + p_regional;
			}
			if (!string.IsNullOrEmpty(p_filial))
			{
				scondi_filial = " and s1.codigo_agencia_origen=" + p_filial;
			}
			if (!string.IsNullOrEmpty(p_estado))
			{
				if (p_estado == "0")
				{
					scondi_estado = " and s1.estado_solicitud_id>=" + p_estado;
				}
				else
				{
					scondi_estado = " and s1.estado_solicitud_id=" + p_estado;
				}
			}
			if (!string.IsNullOrEmpty(p_estacion_actual))
			{
				scondi_estado = " and s1.estacion_id=" + p_estacion_actual;
			}
			if (!string.IsNullOrEmpty(p_no_solicitud))
			{
				scondi_nosolic = "and s1.no_solicitud=" + p_no_solicitud;
			}
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					//lA SUMA DE LAS ESTACIONES POR SOLICITUD
					string sql = @"select s1.no_solicitud,s1.codigo_cliente,nombres||' '||primer_apellido||' '||segundo_apellido nombre_afiliado,
                                          fecha_presentacion,s1.monto_solicitado,s2.monto_aprobado,sa.desc_sub_aplicacion,descripcion_destino,nombre_agencia,az.codigo_zona,
                                          dcs_f_obt_comite_resol(s1.no_solicitud) comite_resol,est.nombre nombre_comite_resol,e.nombre estacion_actual,                                          
                                          s1.antiguedad_meses antiguedad_total_meses,
                                          s1.antiguedad_dias antiguedad_total_dias,
                                          s1.antiguedad_horas antigueda_total_horas,
                                          s1.antiguedad_minutos antigueda_total_minutos 
                                     from dcs_solicitudes s1,
                                          dcs_solicitudes2 s2,
                                          mgi_sub_aplicaciones sa,
                                          mgi_agencias age,
                                          dcs_wf_destinos_credito dest,
                                          cef_agencias_zona az,
                                          mgi_clientes cl,
                                          dcs_wf_estaciones est,
                                          dcs_wf_estaciones e                                                   
                                    Where s1.no_solicitud=s2.no_solicitud
                                      and s1.estacion_id=e.estacion_id
                                      and s1.codigo_sub_aplicacion=sa.codigo_sub_aplicacion
                                      and s2.destino=dest.destino_id
                                      and s1.codigo_agencia_origen=age.codigo_agencia
                                      and s1.codigo_cliente=cl.codigo_cliente
                                      and age.codigo_agencia=az.codigo_agencia                                       
                                      and est.estacion_id=dcs_f_obt_comite_resol(s1.no_solicitud)   " +
									  scondi_fechas +
									  scondi_region +
									  scondi_filial +
									  scondi_estado +
									  scondi_nosolic + @"
                                    Order by no_solicitud";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_date1", d1);
						da.SelectCommand.Parameters.Add("pa_date2", d2);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}

		public bool PonerOrderByDoc(Int32 p_workflow_id, Int16 p_codigo_sub_aplicacion, Int16 p_documento_id, Int16 p_orden)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"UPDATE dcs_wf_documentos_workflow set 
                                          orden_in_grupo=:pa_orden 
                                    Where workflow_id=:pa_workflow_id
                                      and codigo_sub_aplicacion=:pa_codigo_sub_aplicacion 
                                      and documento_id=:pa_documento_id";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_workflow_id", OracleType.Number).Value = p_workflow_id;
					cmd.Parameters.Add("pa_codigo_sub_aplicacion", OracleType.Number).Value = p_codigo_sub_aplicacion;
					cmd.Parameters.Add("pa_documento_id", OracleType.Number).Value = p_documento_id;
					cmd.Parameters.Add("pa_orden", OracleType.Number).Value = p_orden;
					ConexionOracle.Open();
					cmd.ExecuteNonQuery();

					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;

		}
		public bool PonerRecalcPermitidosxSolic(Int32 p_no_solicitud, int p_cant)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"UPDATE  dcs_solicitudes set 
                                          recalculos_permitidos=:pa_cantidad 
                                    Where no_solicitud=:pa_no_solicitud";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_no_solicitud", OracleType.Number).Value = p_no_solicitud;
					cmd.Parameters.Add("pa_cantidad", OracleType.Number).Value = p_cant;

					ConexionOracle.Open();
					cmd.ExecuteNonQuery();

					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;

		}
		public Int16 ObtenerOrderByDoc(Int32 p_workflow_id, Int16 p_codigo_sub_aplicacion, Int16 p_documento_id)
		{
			Int16 vl_retorno = 0;
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"SELECT orden_in_grupo 
                                     from dcs_wf_documentos_workflow 
                                    Where workflow_id=:pa_workflow_id
                                      and codigo_sub_aplicacion=:pa_codigo_sub_aplicacion 
                                      and documento_id=:pa_documento_id";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_workflow_id", OracleType.Number).Value = p_workflow_id;
					cmd.Parameters.Add("pa_codigo_sub_aplicacion", OracleType.Number).Value = p_codigo_sub_aplicacion;
					cmd.Parameters.Add("pa_documento_id", OracleType.Number).Value = p_documento_id;

					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = Int16.Parse(dr["orden_in_grupo"].ToString());
					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}
			return vl_retorno;

		}
		public bool p_conc_nueva_carga_cvs()
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"DCS_P_UTIL_NUEVACARGA_CSV";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();
					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}
		public bool p_conc_insertar_registro_cvs(Int32 p_application_id, string p_no_identificacion, string p_rol, string p_facturado)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"DCS_P_UTIL_INSERTAR_CARGA_CSV";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;

					cmd.Parameters.Add("pa_application_id", OracleType.Number).Value = p_application_id;
					cmd.Parameters.Add("pa_no_identificacion", OracleType.VarChar, 30).Value = p_no_identificacion;
					cmd.Parameters.Add("pa_rol", OracleType.VarChar, 10).Value = p_rol;
					cmd.Parameters.Add("pa_facturable", OracleType.VarChar, 2).Value = p_facturado;
					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();
					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}
			return vl_retorno;
		}
		public bool insertar_archivo1(string p_string1, string p_string2, string p_string3, string p_string4)
		{

			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;


			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Insert into dcs_wf_archivos_conci_det(applicationid,cedula_solicitante,monto_solicitado )
                                          values (:pa_string1,:pa_string3,:pa_string4)";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_string1", p_string1);
					cmd.Parameters.AddWithValue("pa_string3", p_string3);
					cmd.Parameters.AddWithValue("pa_string4", p_string4);

					ConexionOracle.Open();
					cmd.ExecuteNonQuery();
					vl_retorno = true;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;

		}
		public bool insertar_archivo2(string p_string1, string p_string2, string p_string3, string p_string4, string p_string5)
		{

			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;


			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Insert into dcs_wf_archivos_conci_det2(idbitacorabusqueda,identificacion,nombres,apellidos,tipo )
                                          values (:pa_string1,:pa_string3,:pa_string4,pa_string5)";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_string1", p_string1);
					cmd.Parameters.AddWithValue("pa_string2", p_string2);
					cmd.Parameters.AddWithValue("pa_string3", p_string3);
					cmd.Parameters.AddWithValue("pa_string4", p_string4);
					cmd.Parameters.AddWithValue("pa_string5", p_string5);
					ConexionOracle.Open();
					cmd.ExecuteNonQuery();
					vl_retorno = true;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;

		}
		public DataTable p_conci_obtener_carga_head()
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select a.application_id,cantidad_personas_cs,cantidad_personas_tu,decode(nvl(s.application_id,-1),-1,'No existe en CreditScoring','Existe') estatus_application_id        
                                     from dcs_wf_carga a,       
                                          dcs_solicitudes2 s
                                    Where a.application_id=s.application_id(+)
                                      and a.application_id!=0
                                    Order by a.application_id";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}
		public DataTable p_conci_obtener_carga_det(Int32 p_application_id)
		{
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select no_linea,application_id,no_identificacion_tu,rol_tu,no_identificacion_cs from dcs_wf_carga_det where application_id=:pa_application_id";

					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_application_id", p_application_id);
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}
		public DataTable p_conci_resultado_det(Int32 p_estado_conci_id)
		{
			string vl_condi = "";
			if (p_estado_conci_id != -1)
			{
				vl_condi = "and b.estado_conci_id=:pa_estado_conci_id";
			}
			else
			{
				vl_condi = "and b.estado_conci_id !=1";
			}
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select no_solicitud,a.application_id,no_identificacion_tu,b.rol_tu,b.no_identificacion_cs,b.rol_cs,b.observacion 
                                     from dcs_wf_carga a,dcs_wf_carga_det b 
                                    Where a.application_id=b.application_id " + vl_condi;

					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						if (p_estado_conci_id != -1)
						{
							da.SelectCommand.Parameters.Add("pa_estado_conci_id", p_estado_conci_id);
						}
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return dt;
		}
		public bool p_actualizar_linea_carga(Int32 p_application_id, string p_observacion, int p_estado_conci_id)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"UPDATE dcs_wf_carga set 
                                          observaciones=:pa_observacion,
                                          estado_conci_id=:pa_estado_conci_id
                                    Where application_id=:pa_application_id";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_observacion", OracleType.VarChar, 800).Value = p_observacion;
					cmd.Parameters.Add("pa_estado_conci_id", OracleType.Number).Value = p_estado_conci_id;
					cmd.Parameters.Add("pa_application_id", OracleType.Number).Value = p_application_id;
					ConexionOracle.Open();
					cmd.ExecuteNonQuery();
					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}
			return vl_retorno;
		}
		public bool p_actualizar_linea_carga_det(Int32 p_application_id, Int32 p_no_line, string p_observacion, int p_estado_conci_id)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"UPDATE dcs_wf_carga_det set 
                                          observacion=:pa_observacion,
                                          estado_conci_id=:pa_estado_conci_id
                                    Where application_id=:pa_application_id                                      
                                      and no_linea=:pa_no_linea";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.Add("pa_observacion", OracleType.VarChar, 800).Value = p_observacion;
					cmd.Parameters.Add("pa_estado_conci_id", OracleType.Number).Value = p_estado_conci_id;
					cmd.Parameters.Add("pa_application_id", OracleType.Number).Value = p_application_id;
					cmd.Parameters.Add("pa_no_linea", OracleType.Number).Value = p_no_line;
					ConexionOracle.Open();
					cmd.ExecuteNonQuery();
					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}
			return vl_retorno;
		}
		public int p_Cantidad_linea_carga_det()
		{
			int int_retorno = 0;
			string vl_retorno = "";
			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select count(*) n
                                     from dcs_wf_carga_det";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno = dr["n"].ToString();

						int_retorno = int.Parse(vl_retorno);
					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return int_retorno;
		}
		public bool p_generar_bitacorabusqueda_tu(Int32 p_id_bitacora, Int32 p_no_solicitud, Int32 p_application_id, int p_tipo_acceso)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"DCS_P_GENERAR_BITACORA_WU";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;

					cmd.Parameters.Add("pa_id_bitacora", OracleType.Int32).Value = p_id_bitacora;
					cmd.Parameters.Add("pa_no_solcitud", OracleType.Int32).Value = p_no_solicitud;
					cmd.Parameters.Add("pa_application_id", OracleType.Int32).Value = p_application_id;
					cmd.Parameters.Add("pa_tipo_acceso", OracleType.Int32).Value = p_tipo_acceso;
					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();
					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		public grafico_score_crediticio p_ObtenerInfoGraficarScore(Int16 p_score)
		{
			grafico_score_crediticio vl_retorno = new grafico_score_crediticio();

			OracleCommand cmd = new OracleCommand();
			OracleDataReader dr;
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"Select (select min(rango_ini) from dcs_wf_score_rango where rango_id<97) rango_escala1,
                                          rango_ini,
                                          rango_fin,
                                          :pa_score escore,
                                          resultado,
                                          img_indice,
                                          (select max(rango_fin) from dcs_wf_score_rango where rango_id<97) rango_escala2 
                                     From dcs_wf_score_rango
                                    Where :pa_score between rango_ini and rango_fin";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;
					cmd.Parameters.AddWithValue("pa_score", p_score);
					ConexionOracle.Open();

					dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						dr.Read();
						vl_retorno.rango_escala1 = dr["rango_escala1"].ToString();
						vl_retorno.rango_ini = dr["rango_ini"].ToString();
						vl_retorno.rango_fin = dr["rango_fin"].ToString();
						vl_retorno.escore = Int16.Parse(dr["escore"].ToString());
						vl_retorno.resultado = dr["resultado"].ToString();
						vl_retorno.img_indice = Int16.Parse(dr["img_indice"].ToString());
						vl_retorno.rango_escala2 = dr["rango_escala2"].ToString();

					}
					dr.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		public CR_TIPO_PRESTAMO ObtenerTipoPrestamo(Int16 pa_codigo_sub_aplicacion)
		{
			CR_TIPO_PRESTAMO vl_retorno = null;
			DataTable dt = new DataTable();
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"select  cod_tipopres,des_prestamo,
                                           num_mesesplazo,num_mesesplazo_min,num_mesesplazo_base,
                                           por_tasaomision,por_tasaminima,por_tasamaxima,por_tasamoros       
                                     from CR_TIPO_PRESTAMO
                                    Where cod_tipopres=:pa_tipo_pres";
					OracleDataAdapter da = new OracleDataAdapter(sql, ConexionOracle);
					try
					{
						da.SelectCommand.Parameters.Add("pa_tipo_pres", pa_codigo_sub_aplicacion);
						da.Fill(dt);
						foreach (DataRow row in dt.Rows)
						{
							vl_retorno = new CR_TIPO_PRESTAMO()
							{
								Cod_tipopres = Int16.Parse(row["cod_tipopres"].ToString()),
								desc_tipopres = row["des_prestamo"].ToString(),
								Num_mesesplazo = Int16.Parse(row["num_mesesplazo"].ToString()),
								Num_mesesplazo_min = Int16.Parse(row["num_mesesplazo_min"].ToString()),
								Num_mesesplazo_base = Int16.Parse(row["num_mesesplazo_base"].ToString()),
								Por_tasaomision = decimal.Parse(row["por_tasaomision"].ToString()),
								Por_tasaminima = decimal.Parse(row["por_tasaminima"].ToString()),
								Por_tasamaxima = decimal.Parse(row["por_tasamaxima"].ToString()),
								Por_tasamoros = decimal.Parse(row["por_tasamoros"].ToString()),
							};
						}

					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
			return vl_retorno;
		}

		#region Excepciones
		public DataTable get_Decisiones_por_paso(int paso_actual, int estacion_id)
		{
			try
			{
				DataTable dt = new DataTable();

				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = "select f.decision_id, f.estacion_id_to, d.descripcion, f.paso_to, d.estado_excep_id, f.flujo_id "
								+ "from excp.dcs_exc_flujos f, excp.dcs_exc_tipo_decisiones d "
								+ "where f.paso = :paso_actual "
								+ "and f.decision_id = d.decision_id "
								+ "and f.estacion_id_from = :estacion_id order by f.decision_id asc";

					OracleParameter pa_paso_actual = new OracleParameter("paso_actual", OracleType.Number);
					pa_paso_actual.Direction = ParameterDirection.Input;
					pa_paso_actual.Value = paso_actual;

					OracleParameter pa_estacion_id = new OracleParameter("estacion_id", OracleType.Number);
					pa_estacion_id.Direction = ParameterDirection.Input;
					pa_estacion_id.Value = estacion_id;

					OracleCommand cmd = new OracleCommand(sql, ConexionOracle);
					cmd.Parameters.Add(pa_paso_actual);
					cmd.Parameters.Add(pa_estacion_id);

					OracleDataAdapter da = new OracleDataAdapter(cmd);

					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}

				return dt;
			}
			catch (Exception e)
			{
				throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
			}
		}

		public DataTable get_info_Decision(int estacion_id, int flujo_id)
		{
			try
			{
				DataTable dt = new DataTable();

				string sql = "select e.estacion_id, e.nombre, e.comite_resolutivo, e.icono, e.monto_minimo_resolucion, e.monto_maximo_resolucion, "
							+ "e.nivel_resolutivo_local, e.nivel_resol_con_gtefilial "
							+ "from wfc.dcs_wf_estaciones e, excp.dcs_exc_flujos f "
							+ "where e.estacion_id = :estacion_id "
							+ "and e.estacion_id = f.estacion_id_to "
							+ "and f.flujo_id = :flujo_id ";

				OracleParameter pa_estacion_id = new OracleParameter("estacion_id", OracleType.Number);
				pa_estacion_id.Direction = ParameterDirection.Input;
				pa_estacion_id.Value = estacion_id;

				OracleParameter pa_flujo_id = new OracleParameter("flujo_id", OracleType.Number);
				pa_flujo_id.Direction = ParameterDirection.Input;
				pa_flujo_id.Value = flujo_id;

				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					OracleCommand cmd = new OracleCommand(sql, ConexionOracle);
					cmd.Parameters.Add(pa_estacion_id);
					cmd.Parameters.Add(pa_flujo_id);

					OracleDataAdapter da = new OracleDataAdapter(cmd);

					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}

				return dt;
			}
			catch (Exception)
			{
				return null;
			}
		}

		public DataTable get_info_excep_gral(int codigo_excepcion)
		{
			try
			{
				DataTable dt = new DataTable();
				string sql = "select codigo_excepcion, codigo_agencia_origen, no_solicitud, detalle_deudas_consol, recomendacion_filial, pago_mediante, usuario_ing_excepcion, fecha_presentacion "
							 + " from excp.dcs_excepcion_solicitud where codigo_excepcion = :codigoExc ";

				OracleParameter pa_codigo_excepcion = new OracleParameter("codigoExc", OracleType.Number);
				pa_codigo_excepcion.Direction = ParameterDirection.Input;
				pa_codigo_excepcion.Value = codigo_excepcion;

				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					OracleCommand cmd = new OracleCommand(sql, ConexionOracle);
					cmd.Parameters.Add(pa_codigo_excepcion);

					OracleDataAdapter da = new OracleDataAdapter(cmd);

					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}

				return dt;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public object get_icono_estacion(int estacion_id)
		{
			try
			{
				DataTable dt = new DataTable();
				string sql = @"select icono from wfc.dcs_wf_estaciones where estacion_id = :estacion_id ";

				OracleParameter param = new OracleParameter("estacion_id", OracleType.Number);
				param.Direction = ParameterDirection.Input;
				param.Value = estacion_id;

				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					OracleCommand cmd = new OracleCommand(sql, ConexionOracle);
					cmd.Parameters.Add(param);

					OracleDataAdapter da = new OracleDataAdapter(cmd);

					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}

					return dt.Rows[0]["icono"];
				}
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public DataTable get_detalle_excepcion(int _codExcepcion)
		{
			try
			{
				DataTable dt = new DataTable();

				string sql = "select d.cod_lineamiento, l.nombre lineamiento, d.cod_tipo_excepcion, d.justificacion observaciones, t.tipo_excepcion, cod_detalle_excep "
							+ "from excp.dcs_detalle_excep_solic d, excp.dcs_exc_tipo_excepciones t, "
							+ "excp.dcs_exc_lineamientos l "
							+ "where codigo_excepcion = :codExcepcion "
							+ "and d.cod_tipo_excepcion = t.COD_TIPO_EXCEPCION "
							+ "and l.codigo_lineamiento = d.cod_lineamiento ";

				OracleParameter pa_cod_excepcion = new OracleParameter("codExcepcion", OracleType.Number);
				pa_cod_excepcion.Direction = ParameterDirection.Input;
				pa_cod_excepcion.Value = _codExcepcion;

				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					OracleCommand cmd = new OracleCommand(sql, ConexionOracle);
					cmd.Parameters.Add(pa_cod_excepcion);

					OracleDataAdapter da = new OracleDataAdapter(cmd);

					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}

				return dt;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public bool existe_formato_excepcion(int codigoExcepcion)
		{
			try
			{
				bool existe = false;
				DataTable dt = new DataTable();
				//se modificó
				string sql = "select count(*) total from excp.dcs_adjuntos_excep "
							+ "where codigo_excepcion = :codigo and formato_excepcion = 'S' ";

				OracleParameter pa_codigo = new OracleParameter("codigo", OracleType.Number);
				pa_codigo.Direction = ParameterDirection.Input;
				pa_codigo.Value = codigoExcepcion;

				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					OracleCommand cmd = new OracleCommand(sql, ConexionOracle);
					cmd.Parameters.Add(pa_codigo);

					OracleDataAdapter da = new OracleDataAdapter(cmd);

					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}

					//se modificó
					int total = Convert.ToInt32(dt.Rows[0]["total"].ToString());

					return (total > 0) ? true : false;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Ha ocurrido un error al intentar comprobar la existencia de un formato de excepción");
			}
		}

		public DataTable get_adjuntos_excepcion(int codigo_excepcion)
		{
			try
			{
				DataTable dt = new DataTable();
				string sql = "select no_documento, nombre_documento, archivo_bin, formato_excepcion, extension from excp.dcs_adjuntos_excep "
							+ "where codigo_excepcion = :codExcepcion ";

				OracleParameter pa_cod_excepcion = new OracleParameter("codExcepcion", OracleType.Number);
				pa_cod_excepcion.Direction = ParameterDirection.Input;
				pa_cod_excepcion.Value = codigo_excepcion;

				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					OracleCommand cmd = new OracleCommand(sql, ConexionOracle);
					cmd.Parameters.Add(pa_cod_excepcion);

					OracleDataAdapter da = new OracleDataAdapter(cmd);

					try
					{
						da.Fill(dt);
					}
					catch (Exception e)
					{
						throw new Exception("Error en " + e.TargetSite.ToString() + " " + e.Message);
					}
				}

				return dt;

			}
			catch (Exception ex)
			{
				throw new Exception("Error: " + ex.Message);
			}
		}

		public DataTable get_aprobaciones_solicitud(int codigo_excepcion)
		{
			try
			{
				DataTable aprobaciones = new DataTable();

				string sql = "select usuario_comite, pendiente_respuesta_b, decision_id, observaciones "
							+ "from excp.DCS_EXCEPCIONES_APROBACIONES "
							+ "where vigente = 'S' "
							+ "and codigo_excepcion = :pa_codigo_excepcion ";

				OracleParameter pa_codigo_Excepcion = new OracleParameter("pa_codigo_excepcion", OracleType.Number);
				pa_codigo_Excepcion.Direction = ParameterDirection.Input;
				pa_codigo_Excepcion.Value = codigo_excepcion;

				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					OracleCommand cmd = new OracleCommand(sql, ConexionOracle);
					cmd.CommandType = CommandType.Text;
					cmd.Parameters.Add(pa_codigo_Excepcion);

					OracleDataAdapter da = new OracleDataAdapter(cmd);

					da.Fill(aprobaciones);
				}

				return aprobaciones;
			}
			catch (Exception ex)
			{
				throw new Exception($"Error: {ex.Message}", ex.InnerException);
			}
		}

		public DataTable get_resolucion_excepcion(int decisionID)
		{
			try
			{
				DataTable dt = new DataTable();

				string sql = "select aprobar_excep, denegar_excep, modif_excep "
							+ "from excp.dcs_exc_tipo_decisiones "
							+ "where decision_id = :decisionId ";

				OracleParameter pa_decisionId = new OracleParameter("decisionId", OracleType.Number);
				pa_decisionId.Direction = ParameterDirection.Input;
				pa_decisionId.Value = decisionID;

				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					OracleCommand cmd = new OracleCommand(sql, ConexionOracle);
					cmd.CommandType = CommandType.Text;
					cmd.Parameters.Add(pa_decisionId);

					OracleDataAdapter da = new OracleDataAdapter(cmd);

					da.Fill(dt);
				}

				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception($"Ha ocurrido un error: {ex.TargetSite} - {ex.Message}", ex.InnerException);
			}
		}

		//No se usa
		public string es_nivel_resolutivo(int estacion_id)
		{
			try
			{
				string respuesta = string.Empty;
				string sql = "select comite_res_excep from excp.dcs_v_estaciones where estacion_id = :estacion_id ";
				OracleParameter pa_estacion_id = new OracleParameter("estacion_id", OracleType.Number);
				pa_estacion_id.Direction = ParameterDirection.Input;
				pa_estacion_id.Value = estacion_id;

				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					OracleCommand cmd = new OracleCommand(sql, ConexionOracle);
					cmd.CommandType = CommandType.Text;
					cmd.Parameters.Add(pa_estacion_id);

					OracleDataReader dr = cmd.ExecuteReader();

					if (dr.HasRows)
					{
						respuesta = dr["comite_resol_excep"].ToString();
					}

					dr.Close();
					return respuesta;
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"{ex.TargetSite} - {ex.Message}", ex.InnerException);
			}
		}

		public DataTable p_obtener_Estados_Excepcion()
		{
			try
			{
				string sql = "select ESTADO_EXCEP_ID, DESCRIPCION from excp.DCS_EXC_ESTADOS_EXCEPCION where ACTIVO = 'S' ";
				DataTable dtEstados = new DataTable();

				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					OracleCommand cmd = new OracleCommand(sql, ConexionOracle);
					cmd.CommandType = CommandType.Text;

					OracleDataAdapter da = new OracleDataAdapter(cmd);
					da.Fill(dtEstados);
				}

				return dtEstados;
			}
			catch (Exception ex)
			{
				throw new Exception($"{ex.TargetSite}: {ex.Message}", ex.InnerException);
			}
		}

		public DataTable p_niveles_resolutivos()
		{
			try
			{
				string sql = "select estacion_id, nombre from excp.dcs_v_estaciones where comite_res_excep = 'S'";
				DataTable dtEstados = new DataTable();

				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					OracleCommand cmd = new OracleCommand(sql, ConexionOracle);
					cmd.CommandType = CommandType.Text;

					OracleDataAdapter da = new OracleDataAdapter(cmd);
					da.Fill(dtEstados);
				}

				return dtEstados;
			}
			catch (Exception ex)
			{
				throw new Exception($"{ex.TargetSite}: {ex.Message}", ex.InnerException);
			}
		}

		public DataTable generar_reporte_excepciones(int? codigo_estado, DateTime fecha_inicio, DateTime fecha_fin,
													int? codigo_region, int? codigo_filial, string condigo_cliente, int estacion_id)
		{
			try
			{
				string f_estado = (codigo_estado == 0) ? string.Empty : "and es.ESTADO_EXCEP_ID = :pa_estado ";
				string f_fechas = "and e.fecha_presentacion between :pa_fecha_inicio and :pa_fecha_fin ";
				//string f_region = (codigo_region == 0) ? string.Empty : "and a.CODIGO_ZONA = :pa_region ";
				string f_filial = (codigo_filial == 0) ? string.Empty : "and e.CODIGO_AGENCIA_ORIGEN = :pa_filial ";
				string f_cliente = (condigo_cliente.Equals(string.Empty)) ? string.Empty : "and s.codigo_cliente = :pa_cliente ";
				string f_estacion = (estacion_id == 0) ? string.Empty : "and e.estacion_id = :pa_estacion_id ";

				OracleParameter pa_estado = new OracleParameter("pa_estado", OracleType.Number);
				pa_estado.Direction = ParameterDirection.Input; ;

				OracleParameter pa_fecha_inicio = new OracleParameter("pa_fecha_inicio", OracleType.DateTime);
				pa_fecha_inicio.Direction = ParameterDirection.Input;
				pa_fecha_inicio.Value = fecha_inicio;

				OracleParameter pa_fecha_fin = new OracleParameter("pa_fecha_fin", OracleType.DateTime);
				pa_fecha_fin.Direction = ParameterDirection.Input;
				pa_fecha_fin.Value = fecha_fin;

				//OracleParameter pa_region = new OracleParameter("pa_region", OracleType.Number);
				//pa_region.Direction = ParameterDirection.Input;

				OracleParameter pa_filial = new OracleParameter("pa_filial", OracleType.Number);
				pa_filial.Direction = ParameterDirection.Input;

				OracleParameter pa_cliente = new OracleParameter("pa_cliente", OracleType.VarChar);
				pa_cliente.Direction = ParameterDirection.Input;

				OracleParameter pa_estacion_id = new OracleParameter("pa_estacion_id", OracleType.Number);
				pa_estacion_id.Direction = ParameterDirection.Input;

				DataTable reporte = new DataTable();
				string sql = "select e.codigo_Excepcion,e.no_solicitud,s.codigo_cliente, "
							+ "initcap(trim(c.nombres)) || ' ' || initcap(Trim(c.primer_apellido)) || ' ' || initcap(Trim(c.segundo_apellido)) nombre_cliente, "
							+ "e.fecha_presentacion,e.fecha_resolucion,es.DESCRIPCION estado, "
							+ "case when e.pago_mediante = 'V' then 'Pago por ventanilla' else 'Pago por planilla' end pago_mediante, "
							+ "s.monto_solicitado,e.condicion_tu,z.codigo_zona, z.DESCRIPCION nombre_zona, e.codigo_agencia_origen,a.nombre_agencia, "
							+ "initcap(sa.desc_sub_aplicacion) desc_sub_aplicacion,d.descripcion_destino,e.estacion_id,est.nombre estacion_Actual, "
							+ "nvl(e.ANTIGUEDAD_MESES,0) ANTIGUEDAD_MESES,nvl(e.ANTIGUEDAD_DIAS,0) ANTIGUEDAD_DIAS, nvl(e.ANTIGUEDAD_HORAS,0) ANTIGUEDAD_HORAS, nvl(e.ANTIGUEDAD_MINUTOS,0) ANTIGUEDAD_MINUTOS, nvl(e.ANTIGUEDAD_SEGUNDOS,0) ANTIGUEDAD_SEGUNDOS "
							+ "from wfc.dcs_solicitudes s, mgi.mgi_clientes c, mgi_sub_aplicaciones sa,wfc.dcs_wf_destinos_credito d, "
							+ "wfc.dcs_solicitudes2 s2, mgi.mgi_agencias a, excp.dcs_excepcion_solicitud e, excp.dcs_exc_estados_excepcion es, "
							+ "cef_zonas z, wfc.dcs_wf_estaciones est "
							+ "where s.codigo_cliente = c.codigo_cliente "
							+ "and s.codigo_sub_aplicacion = sa.codigo_sub_aplicacion "
							+ "and s.no_solicitud = s2.no_solicitud "
							+ "and s2.destino = d.destino_id "
							+ "and a.codigo_agencia = s.codigo_agencia_origen "
							+ "and e.no_solicitud = s.no_solicitud "
							+ "and es.ESTADO_EXCEP_ID = e.CODIGO_ESTADO_EXCEP "
							+ "and a.CODIGO_ZONA = z.codigo_zona "
							+ "and est.estacion_id = e.ESTACION_ID "
							+ f_fechas
							+ f_cliente
							+ f_estado
							+ f_filial
							//+ f_region
							+ f_estacion
							+ "order by e.codigo_excepcion asc ";

				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					OracleCommand cmd = new OracleCommand(sql, ConexionOracle);
					cmd.CommandType = CommandType.Text;

					cmd.Parameters.Add(pa_fecha_inicio);
					cmd.Parameters.Add(pa_fecha_fin);

					if (!condigo_cliente.Equals(string.Empty))
					{
						pa_cliente.Value = condigo_cliente;
						cmd.Parameters.Add(pa_cliente);
					}

					if (codigo_estado != 0)
					{
						pa_estado.Value = codigo_estado;
						cmd.Parameters.Add(pa_estado);
					}

					if (codigo_filial != 0)
					{
						pa_filial.Value = codigo_filial;
						cmd.Parameters.Add(pa_filial);
					}

					if (codigo_region != 0)
					{
						//pa_region.Value = codigo_region;
						//cmd.Parameters.Add(pa_region);
					}

					if (estacion_id != 0)
					{
						pa_estacion_id.Value = estacion_id;
						cmd.Parameters.Add(pa_estacion_id);
					}

					OracleDataAdapter da = new OracleDataAdapter(cmd);
					da.Fill(reporte);
				}

				return reporte;
			}
			catch (Exception ex)
			{
				throw new Exception($"{ex.TargetSite}: {ex.Message}", ex.InnerException);
			}
		}

		public int get_estacion_actual_excep(int codigoExcepcion)
		{
			try
			{
				string sql = "select estacion_id from excp.dcs_excepcion_solicitud where codigo_excepcion = :codExcepc ";
				int estacion = 0;

				OracleParameter pa_cod_excepcion = new OracleParameter("codExcepc", OracleType.Number);
				pa_cod_excepcion.Direction = ParameterDirection.Input;
				pa_cod_excepcion.Value = codigoExcepcion;

				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					OracleCommand cmd = new OracleCommand(sql, ConexionOracle);
					cmd.CommandType = CommandType.Text;
					cmd.Parameters.Add(pa_cod_excepcion);

					OracleDataAdapter da = new OracleDataAdapter(cmd);
					DataTable dt = new DataTable();

					da.Fill(dt);

					estacion = Convert.ToInt32(dt.Rows[0]["estacion_id"].ToString());

				}
				return estacion;
			}
			catch (Exception ex)
			{
				throw new Exception($"{ex.TargetSite}: {ex.Message}", ex.InnerException);
			}
		}


		//No se usa
		public DataTable get_estaciones_credito()
		{
			try
			{
				string sql = "select estacion_id, nombre from dcs_wf_estaciones where estacion_id = 1004 or estacion_id = 2003 or estacion_id = 3003 and activo = 'S' ";
				DataTable dtEstaciones = new DataTable();

				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					OracleCommand cmd = new OracleCommand(sql, ConexionOracle);
					cmd.CommandType = CommandType.Text;

					OracleDataAdapter da = new OracleDataAdapter(cmd);
					da.Fill(dtEstaciones);
				}

				return dtEstaciones;
			}
			catch (Exception ex)
			{
				throw new Exception($"{ex.TargetSite}: {ex.Message}", ex.InnerException);
			}
		}


		public DataTable get_analistas_estacion(int estacion_id)
		{
			try
			{
				string sql = "select ue.usuario, u.NOMBRES || ' ' || u.PRIMER_APELLIDO || ' ' || u.SEGUNDO_APELLIDO nombre "
							+ "from dcs_wf_usuarios_estaciones ue, mgi_usuarios u "
							+ "where ue.estacion_id = :pa_seleccion "
							+ "and activo = 'S' "
							+ "and u.CODIGO_USUARIO = ue.USUARIO "
							+ "order by ue.usuario asc";
				DataTable dtAnalistas = new DataTable();

				OracleParameter pa_comite = new OracleParameter("pa_seleccion", OracleType.Number);
				pa_comite.Direction = ParameterDirection.Input;
				pa_comite.Value = estacion_id;

				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					OracleCommand cmd = new OracleCommand(sql, ConexionOracle);
					cmd.CommandType = CommandType.Text;
					cmd.Parameters.Add(pa_comite);

					OracleDataAdapter da = new OracleDataAdapter(cmd);
					da.Fill(dtAnalistas);
				}

				return dtAnalistas;
			}
			catch (Exception ex)
			{
				throw new Exception($"{ex.TargetSite}: {ex.Message}", ex.InnerException);
			}
		}

		public DataTable get_firgura2()
		{
			try
			{
				string sql = "select distinct ue.usuario, u.NOMBRES || ' ' || u.PRIMER_APELLIDO || ' ' || u.SEGUNDO_APELLIDO nombre "
							+ "from dcs_wf_usuarios_estaciones ue, mgi_usuarios u "
							+ "where activo = 'S' "
							+ "and u.CODIGO_USUARIO = ue.USUARIO "
							+ "and ue.estacion_id >= 9000 and ue.estacion_id <= 9004 "
							+ "order by ue.usuario asc ";
				DataTable dtAnalistas = new DataTable();

				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					OracleCommand cmd = new OracleCommand(sql, ConexionOracle);
					cmd.CommandType = CommandType.Text;

					OracleDataAdapter da = new OracleDataAdapter(cmd);
					da.Fill(dtAnalistas);
				}

				return dtAnalistas;
			}
			catch (Exception ex)
			{
				throw new Exception($"{ex.TargetSite}: {ex.Message}", ex.InnerException);
			}
		}

		public bool ReasignarOficialFilialExcepcion(Int32 p_no_solicitud, Int32 p_codigo_agencia_origen, string p_oficial)
		{
			bool vl_retorno = false;
			OracleCommand cmd = new OracleCommand();
			try
			{
				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					string sql = @"excp.dcs_p_util_reasOfExcep";
					cmd.CommandText = sql;
					cmd.Connection = ConexionOracle;

					cmd.Parameters.Add("pa_codigo_excepcion", OracleType.Int32).Value = p_no_solicitud;
					cmd.Parameters.Add("pa_codigo_agencia_origen", OracleType.Int16).Value = p_codigo_agencia_origen;
					cmd.Parameters.Add("pa_oficial", OracleType.VarChar, 30).Value = p_oficial;

					ConexionOracle.Open();
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();
					vl_retorno = true;
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error en " + ex.TargetSite + "  " + ex.Message);
			}

			return vl_retorno;
		}

		public DataTable generar_historial(string condigo_cliente)
		{
			try
			{
				string f_cliente = (condigo_cliente.Equals(string.Empty)) ? string.Empty : "and s.codigo_cliente = :pa_cliente ";

				OracleParameter pa_estado = new OracleParameter("pa_estado", OracleType.Number);
				pa_estado.Direction = ParameterDirection.Input;

				OracleParameter pa_cliente = new OracleParameter("pa_cliente", OracleType.VarChar);
				pa_cliente.Direction = ParameterDirection.Input;

				DataTable reporte = new DataTable();
				string sql = "select e.codigo_Excepcion,e.no_solicitud,s.codigo_cliente, "
							+ "initcap(trim(c.nombres)) || ' ' || initcap(Trim(c.primer_apellido)) || ' ' || initcap(Trim(c.segundo_apellido)) nombre_cliente, "
							+ "e.fecha_presentacion,e.fecha_resolucion,es.DESCRIPCION estado, "
							+ "case when e.pago_mediante = 'V' then 'Pago por ventanilla' else 'Pago por planilla' end pago_mediante, "
							+ "s.monto_solicitado,e.condicion_tu,z.codigo_zona, z.DESCRIPCION nombre_zona, e.codigo_agencia_origen,a.nombre_agencia, "
							+ "initcap(sa.desc_sub_aplicacion) desc_sub_aplicacion,d.descripcion_destino,e.estacion_id,est.nombre estacion_Actual, "
							+ "nvl(e.ANTIGUEDAD_MESES,0) ANTIGUEDAD_MESES,nvl(e.ANTIGUEDAD_DIAS,0) ANTIGUEDAD_DIAS, nvl(e.ANTIGUEDAD_HORAS,0) ANTIGUEDAD_HORAS, nvl(e.ANTIGUEDAD_MINUTOS,0) ANTIGUEDAD_MINUTOS, nvl(e.ANTIGUEDAD_SEGUNDOS,0) ANTIGUEDAD_SEGUNDOS "
							+ "from wfc.dcs_solicitudes s, mgi.mgi_clientes c, mgi_sub_aplicaciones sa,wfc.dcs_wf_destinos_credito d, "
							+ "wfc.dcs_solicitudes2 s2, mgi.mgi_agencias a, excp.dcs_excepcion_solicitud e, excp.dcs_exc_estados_excepcion es, "
							+ "cef_zonas z, wfc.dcs_wf_estaciones est "
							+ "where s.codigo_cliente = c.codigo_cliente "
							+ "and s.codigo_sub_aplicacion = sa.codigo_sub_aplicacion "
							+ "and s.no_solicitud = s2.no_solicitud "
							+ "and s2.destino = d.destino_id "
							+ "and a.codigo_agencia = s.codigo_agencia_origen "
							+ "and e.no_solicitud = s.no_solicitud "
							+ "and es.ESTADO_EXCEP_ID = e.CODIGO_ESTADO_EXCEP "
							+ "and a.CODIGO_ZONA = z.codigo_zona "
							+ "and est.estacion_id = e.ESTACION_ID "
							+ f_cliente
							+ "order by e.codigo_excepcion asc ";

				using (OracleConnection ConexionOracle = new OracleConnection(cadenaConexionOracle))
				{
					OracleCommand cmd = new OracleCommand(sql, ConexionOracle);
					cmd.CommandType = CommandType.Text;

					pa_cliente.Value = condigo_cliente;
					cmd.Parameters.Add(pa_cliente);

					OracleDataAdapter da = new OracleDataAdapter(cmd);
					da.Fill(reporte);
				}

				return reporte;
			}
			catch (Exception ex)
			{
				throw new Exception($"{ex.TargetSite}: {ex.Message}", ex.InnerException);
			}
		}


		#endregion
	}

	public class grafico_score_crediticio
	{
		public string rango_escala1 { get; set; }
		public string rango_ini { get; set; }
		public string rango_fin { get; set; }
		public Int16 escore { get; set; }
		public string resultado { get; set; }
		public Int16 img_indice { get; set; }
		public string rango_escala2 { get; set; }
	}

	public class solicitud_credito
	{
		#region Generales Solicitud

		public Int32 no_solicitud { get; set; }
		public Int16 workflow_id { get; set; }
		public string usuario_workflow { get; set; }
		public Int32 codigo_agencia { get; set; }
		public int codigo_sub_aplicacion { get; set; }
		public string desc_sub_aplicacion { get; set; }
		public int fuente_financiamiento { get; set; }
		public int codigo_moneda { get; set; }
		public string desc_moneda { get; set; }
		public string oficial_servicio { get; set; }
		public DateTime fecha_solicitud { get; set; }
		public decimal monto_solicitado { get; set; }
		public Int16 plazo { get; set; }
		public decimal tasa { get; set; }
		public string condicion_vehiculo { get; set; }
		public decimal valor_vehiculo { get; set; }
		public string destino_credito { get; set; }
		public string descripcion_destino { get; set; }
		public string es_consolidacion { get; set; }
		public string descripcion_garantia { get; set; }
		public string xml_cuotas_buro { get; set; }
		public decimal monto_cuota_consolidar { get; set; }
		public decimal monto_balance_consolidar { get; set; }
		public string modo_transunion { get; set; }
		public Int32 application_id { get; set; }
		public string requiere_codeudor { get; set; }
		public string requiere_aval1 { get; set; }
		public string requiere_aval2 { get; set; }
		public decimal derecho_ganado { get; set; }
		public decimal monto_cuotas_vencimiento { get; set; }
		public decimal complemento_aportaciones { get; set; }
		public decimal deudas_canceladas_solic { get; set; }
		public decimal deudas_canceladas_codeud { get; set; }
		public decimal deudas_canceladas_aval1 { get; set; }
		public decimal deudas_canceladas_aval2 { get; set; }

		#endregion

		#region Solicitante

		public Int32 codigo_cliente { get; set; }
		public string no_identificacion { get; set; }
		public string nombres { get; set; }
		public string primer_apellido { get; set; }
		public string segundo_apellido { get; set; }
		public string apellido_casada { get; set; }
		public string sexo { get; set; }
		public string estado_civil { get; set; }
		public int edad { get; set; }
		public string ventanilla_planilla { get; set; }
		public DateTime? fecha_nacimiento { get; set; }
		public string lugar_nacimiento { get; set; }
		public string nacionalidad { get; set; }
		public DateTime? fecha_ingreso_coop { get; set; }
		public string nivel_educativo { get; set; }
		public string profesion_oficio { get; set; }
		public string tipo_vivienda { get; set; }
		public string tipo_vivienda_especificar { get; set; }
		public string direccion_residencia { get; set; }
		public string telefono_fijo { get; set; }
		public string telefono_celular { get; set; }
		public string telefono_adicional1 { get; set; }
		public string telefono_adicional2 { get; set; }
		public string correo_electronico { get; set; }
		public Int16 dependientes_hijos { get; set; }
		public Int16 dependientes_otros { get; set; }
		public string tipo_empresa { get; set; }
		public string tipo_empresa_especificar { get; set; }
		public string actividad_patrono_negocio { get; set; }
		public string patrono { get; set; }
		public string depto_labora { get; set; }
		public string cargo { get; set; }
		public string antiguedad_laboral { get; set; }
		public decimal ingresos { get; set; }
		public decimal otros_ingresos { get; set; }
		public decimal deducciones { get; set; }
		public string telefono_laboral1 { get; set; }
		public string ext_laboral1 { get; set; }
		public string telefono_laboral2 { get; set; }
		public string ext_laboral2 { get; set; }
		public string fax_laboral1 { get; set; }
		public string direccion_laboral { get; set; }
		public string correo_laboral { get; set; }
		public int EdadPresta { get; set; }
		public string RTN { get; set; }

		#endregion

		#region Conyuge

		public string no_identificacion_conyuge { get; set; }
		public string nombres_conyuge { get; set; }
		public string primer_nombre_conyuge { get; set; }
		public string segundo_nombre_conyuge { get; set; }
		public string primer_apellido_conyuge { get; set; }
		public string segundo_apellido_conyuge { get; set; }
		public string sexo_conyuge { get; set; }
		public int dependientes_hijos_conyuge { get; set; }
		public int dependientes_otros_conyuge { get; set; }
		public string direccion_residencia_conyuge { get; set; }
		public string telefono_fijo_conyuge { get; set; }
		public string celular_conyuge { get; set; }
		public string telefono_adicional1_conyuge { get; set; }
		public string telefono_adicional2_conyuge { get; set; }
		public string correo_conyuge { get; set; }
		public string es_afiliado_conyuge { get; set; }
		public string codigo_cliente_conyuge { get; set; }
		public string tipo_empresa_conyuge { get; set; }
		public string tipo_empresa_especificar_conyuge { get; set; }
		public string patrono_conyuge { get; set; }
		public string depto_labora_conyuge { get; set; }
		public string cargo_conyuge { get; set; }
		public string antiguedad_conyuge { get; set; }
		public decimal ingresos_conyuge { get; set; }
		public decimal otros_ingresos_conyuge { get; set; }
		public decimal deducciones_conyuge { get; set; }
		public string telefono_laboral1_conyuge { get; set; }
		public string ext_laboral1_conyuge { get; set; }
		public string telefono_laboral2_conyuge { get; set; }
		public string ext_laboral2_conyuge { get; set; }
		public string direccion_laboral_conyuge { get; set; }
		public string correo_laboral_conyuge { get; set; }
		public string NivelEducConyuge { get; set; }
		public string TipoContrato { get; set; }

		#endregion

		#region Codeudor

		public string no_identificacion_codeudor { get; set; }
		public string nombres_codeudor { get; set; }
		public string primer_nombre_codeudor { get; set; }
		public string segundo_nombre_codeudor { get; set; }
		public string primer_apellido_codeudor { get; set; }
		public string segundo_apellido_codeudor { get; set; }
		public string sexo_codeudor { get; set; }
		public int dependientes_hijos_codeudor { get; set; }
		public int dependientes_otros_codeudor { get; set; }
		public string direccion_residencia_codeudor { get; set; }
		public string telefono_fijo_codeudor { get; set; }
		public string celular_codeudor { get; set; }
		public string telefono_adicional1_codeudor { get; set; }
		public string telefono_adicional2_codeudor { get; set; }
		public string correo_codeudor { get; set; }
		public string es_afiliado_codeudor { get; set; }
		public string codigo_cliente_codeudor { get; set; }
		public string tipo_empresa_codeudor { get; set; }
		public string tipo_empresa_especificar_codeudor { get; set; }
		public string patrono_codeudor { get; set; }
		public string depto_labora_codeudor { get; set; }
		public string cargo_codeudor { get; set; }
		public string antiguedad_codeudor { get; set; }
		public decimal ingresos_codeudor { get; set; }
		public decimal otros_ingresos_codeudor { get; set; }
		public decimal deducciones_codeudor { get; set; }
		public string telefono_laboral1_codeudor { get; set; }
		public string ext_laboral1_codeudor { get; set; }
		public string telefono_laboral2_codeudor { get; set; }
		public string ext_laboral2_codeudor { get; set; }
		public string direccion_laboral_codeudor { get; set; }
		public string correo_laboral_codeudor { get; set; }
		public string nombre_conyuge_codeudor { get; set; }
		public string direclab_conyuge_codeudor { get; set; }
		public string cargo_conyuge_codeudor { get; set; }
		public int EdadCodeudor { get; set; }
		public string TipoViviendaCodeudor { get; set; }
		public string DescripcionViviendaCodeudor { get; set; }

		#endregion

		#region Aval1

		public string no_identificacion_aval1 { get; set; }
		public string nombres_aval1 { get; set; }
		public string primer_nombre_aval1 { get; set; }
		public string segundo_nombre_aval1 { get; set; }
		public string primer_apellido_aval1 { get; set; }
		public string segundo_apellido_aval1 { get; set; }
		public string sexo_aval1 { get; set; }
		public int dependientes_hijos_aval1 { get; set; }
		public int dependientes_otros_aval1 { get; set; }
		public string direccion_residencia_aval1 { get; set; }
		public string telefono_fijo_aval1 { get; set; }
		public string celular_aval1 { get; set; }
		public string telefono_adicional1_aval1 { get; set; }
		public string telefono_adicional2_aval1 { get; set; }
		public string correo_aval1 { get; set; }
		public string es_afiliado_aval1 { get; set; }
		public string codigo_cliente_aval1 { get; set; }
		public string tipo_empresa_aval1 { get; set; }
		public string tipo_empresa_especificar_aval1 { get; set; }
		public string patrono_aval1 { get; set; }
		public string depto_labora_aval1 { get; set; }
		public string cargo_aval1 { get; set; }
		public string antiguedad_aval1 { get; set; }
		public decimal ingresos_aval1 { get; set; }
		public decimal otros_ingresos_aval1 { get; set; }
		public decimal deducciones_aval1 { get; set; }
		public string telefono_laboral1_aval1 { get; set; }
		public string ext_laboral1_aval1 { get; set; }
		public string telefono_laboral2_aval1 { get; set; }
		public string ext_laboral2_aval1 { get; set; }
		public string direccion_laboral_aval1 { get; set; }
		public string correo_laboral_aval1 { get; set; }
		public string nombre_conyuge_aval1 { get; set; }
		public string direclab_conyuge_aval1 { get; set; }
		public string cargo_conyuge_aval1 { get; set; }
		public int EdadAval1 { get; set; }
		public string TipoViviendaAval1 { get; set; }
		public string DescripcionViviendaAval1 { get; set; }
		public string RTN_Aval1 { get; set; }

		#endregion

		#region Aval2

		public string no_identificacion_aval2 { get; set; }
		public string nombres_aval2 { get; set; }
		public string primer_nombre_aval2 { get; set; }
		public string segundo_nombre_aval2 { get; set; }
		public string primer_apellido_aval2 { get; set; }
		public string segundo_apellido_aval2 { get; set; }
		public string sexo_aval2 { get; set; }
		public int dependientes_hijos_aval2 { get; set; }
		public int dependientes_otros_aval2 { get; set; }
		public string direccion_residencia_aval2 { get; set; }
		public string telefono_fijo_aval2 { get; set; }
		public string celular_aval2 { get; set; }
		public string telefono_adicional1_aval2 { get; set; }
		public string telefono_adicional2_aval2 { get; set; }
		public string correo_aval2 { get; set; }
		public string es_afiliado_aval2 { get; set; }
		public string codigo_cliente_aval2 { get; set; }
		public string tipo_empresa_aval2 { get; set; }
		public string tipo_empresa_especificar_aval2 { get; set; }
		public string patrono_aval2 { get; set; }
		public string depto_labora_aval2 { get; set; }
		public string cargo_aval2 { get; set; }
		public string antiguedad_aval2 { get; set; }
		public decimal ingresos_aval2 { get; set; }
		public decimal otros_ingresos_aval2 { get; set; }
		public decimal deducciones_aval2 { get; set; }
		public string telefono_laboral1_aval2 { get; set; }
		public string ext_laboral1_aval2 { get; set; }
		public string telefono_laboral2_aval2 { get; set; }
		public string ext_laboral2_aval2 { get; set; }
		public string direccion_laboral_aval2 { get; set; }
		public string correo_laboral_aval2 { get; set; }
		public string nombre_conyuge_aval2 { get; set; }
		public string direclab_conyuge_aval2 { get; set; }
		public string cargo_conyuge_aval2 { get; set; }
		public int EdadAval2 { get; set; }
		public string TipoViviendaAval2 { get; set; }
		public string DescripcionViviendaAval2 { get; set; }
		public string RTN_Aval2 { get; set; }
		public string xml_referencias { get; set; }

		#endregion

		#region Datos desembolso

		public int banderin_id { get; set; }
		public decimal monto_aprobado { get; set; }
		public int plazo_aprobado { get; set; }
		public decimal tasa_aprobada { get; set; }
		public int estado_solicitud_id { get; set; }
		public string instrucciones_desembolso { get; set; }
		public DateTime? fecha_creacion_tu { get; set; }

		#endregion

		#region Indice de Concentracion de deuda

		public Int16 indicador_aplicado { get; set; }
		public decimal total_capitalvigente_grpfam { get; set; }
		public decimal total_capitalvigente_solicitante { get; set; }
		public decimal monto_ensolicitud { get; set; }
		public decimal monto_excluir_refconsol { get; set; }
		public decimal total_paraindice { get; set; }
		public decimal patrimonio_csf { get; set; }
		public decimal porcentaje_concentracion { get; set; }
		public decimal limite_indicador { get; set; }
		public string resultado_evaluacion_indicador { get; set; }

		#endregion		
	}

	public class GaranteHipotecario
	{
		#region Garante Hipotecario

		public string NoIdentidadGarante { get; set; }
		public int CodigoClienteGarante { get; set; }
		public string PrimerNombreGarante { get; set; }
		public string SegundoNombreGarante { get; set; }
		public string PrimerApellidoGarante { get; set; }
		public string SegundoApellidoGarante { get; set; }
		public string GeneroGarante { get; set; }
		public int DependientesHijosGarante { get; set; }
		public int DependientesOtrosGarante { get; set; }
		public string DireccionGarante { get; set; }
		public string TelefonoFijoGarante { get; set; }
		public string CelularGarante { get; set; }
		public string TelefonoAdic1Garante { get; set; }
		public string TelefonoAdic2Garante { get; set; }
		public string CorreoGarante { get; set; }
		public string EsAfliadoGarante { get; set; }
		public string TipoEmpresaGarante { get; set; }
		public string TipoEmpresaOtrosGarante { get; set; }
		public string PatronoGarante { get; set; }
		public string DeptoLaboraGarante { get; set; }
		public string CargoGarante { get; set; }
		public string AntiguedadLabGarante { get; set; }
		public string TelLab1Garante { get; set; }
		public string ExtLab1Garante { get; set; }
		public string TelLab2Garante { get; set; }
		public string ExtLab2Garante { get; set; }
		public string DirecLabGarante { get; set; }
		public string CorreoLabgarante { get; set; }
		public string NomConyugeGarante { get; set; }
		public string DirecLabConyuGarante { get; set; }
		public string CargoLabConyuGarante { get; set; }

		#region Referencias Garante

		public string NomRef1Garante { get; set; }
		public string DirRef1Garante { get; set; }
		public string TelRef1Garante { get; set; }
		public string LocalRef1Garante { get; set; }
		public string CelularRef1Garante { get; set; }
		public string NomRef2Garante { get; set; }
		public string DirRef2Garante { get; set; }
		public string TelRef2Garante { get; set; }
		public string LocalRef2Garante { get; set; }
		public string CelularRef2Garante { get; set; }
		public string NomRef3Garante { get; set; }
		public string DirRef3Garante { get; set; }
		public string TelRef3Garante { get; set; }

		#endregion

		#endregion
	}

	public class referencias_solicitud
	{
		public Int32 no_solicitud { get; set; }
		public string rol { get; set; }
		public Int32 no_referencia { get; set; }
		public string referencia_de { get; set; }
		public string nombre { get; set; }
		public string direccion { get; set; }
		public string telefono_fijo { get; set; }
		public string telefono_celular { get; set; }
		public string punto_referencia { get; set; }
		public string casa_color { get; set; }
		public string bloque { get; set; }
	}
	public class analisis_cuantitativo
	{
		public Int32 no_solicitud { get; set; }
		public decimal coopsalud { get; set; }
		public decimal aportaciones { get; set; }
		public decimal mejora_avalua { get; set; }
		public decimal cuota_anticipada { get; set; }
		public decimal prestamos_consolidar_csf { get; set; }
		public decimal prestamos_consolidar_otros { get; set; }
		public decimal pagos_terceros { get; set; }
		public decimal complemento_aportaciones { get; set; }
		public decimal timbres_cooperativos { get; set; }
		public decimal honorarios_hipoteca { get; set; }
		public decimal honorarios_compraventa { get; set; }
		public decimal capitalizacion { get; set; }
		public decimal seguro_vida { get; set; }
		public decimal seguro_danos { get; set; }
		public decimal seguro_incendios { get; set; }
		public decimal seguro_vida_mensual { get; set; }
		public decimal seguro_danos_mensual { get; set; }
		public decimal seguro_incendios_mensual { get; set; }
		public decimal gastos_administrativos { get; set; }
		public decimal avaluo_final { get; set; }
		public decimal central_riesgos { get; set; }
		public decimal total_deducciones { get; set; }
		public decimal total_desembolso { get; set; }
		public decimal cuota_total { get; set; }
		public decimal cuota_nivelada { get; set; }
		public decimal papeleria { get; set; }
		public string xmlDeducciones_MCR { get; set; }
		public string override_tir_cat { get; set; }
		public string tir_override { get; set; }
		public string cat_override { get; set; }
	}
}

