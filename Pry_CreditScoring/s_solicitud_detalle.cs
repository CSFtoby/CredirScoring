using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using wfcModel;

namespace Docsis_Application
{
    public partial class s_solicitud_detalle : Form
    {
        public bool primera_carga = true;
        public static bool con_borde = true;
        public DataAccess da;
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
        #endregion
        
        s_miniinfo_usuario miniinfo_user = new s_miniinfo_usuario();
        s_miniinfo_asignacion miniinfo_asig = new s_miniinfo_asignacion();
        string vl_usuario_det = "";
        string vl_abierta = "";
        int vl_no_solicitud = 0;
        int vl_no_movimiento = 0;
        bool vl_mostrar_miniinfo = true;
        
        public s_solicitud_detalle(int pa_no_solicitud, int pa_no_movimiento)
        {
            InitializeComponent();
            vl_no_solicitud = pa_no_solicitud;
            vl_no_movimiento = pa_no_movimiento;
            this.da = da;
        }

        private void s_solicitud_detalle_Load(object sender, EventArgs e)
        {
            textBox_no_solicitud.Text = vl_no_solicitud.ToString();
            p_obtener_datos_solicitud(vl_no_solicitud);
            p_obtener_estacion_actual();
            p_obtener_analista_actual();
            if (vl_abierta == "N")
            {
                timer1.Enabled = false;
            }
            if (vl_abierta == "S")
            {
                p_obtener_antiguedad_solicitud_abierta();
            }

            try
            {
                p_obtener_comite_resolucion();
            }
            catch (Exception ex)
            {
            }
            p_llenar_grid_rutas();
            p_llenar_adjuntos(vl_no_solicitud);
            p_llenar_anotaciones(vl_no_solicitud);
            //p_marca_como_no_nueva(vl_no_movimiento);
            list_adjuntos.View = View.Tile;
            list_anotaciones.View = View.Details;

            if (primera_carga)
            {
                primera_carga = false;
                //Obteniendo Fotografia
                string vl_fecha_ultima_act = "";
                byte[] foto;
                da.ObtenerFotoAfiliado(textBox_codigo_cliente.Text, out foto, out vl_fecha_ultima_act);
                if (foto != null)
                {
                    pbFotoVigente.Image = DocSys.p_CopyDataToBitmap(foto);
                }
            }
        }

        private void p_obtener_datos_solicitud(int p_no_solicitud)
        {
            int cont = da.ExisteRevivion(p_no_solicitud);
            string vl_sql;
            if (cont > 0)
            {
                vl_sql = @"Select s.no_solicitud,
                            s.no_solicitud_formulario,
                            s.estado_solicitud_id,
                            s.abierta,
                            es.descripcion estado_solicitud,
                            s.fecha_presentacion,
                            s.fecha_cierre,
                            s.codigo_cliente,
                            s.decision_final_solicitud,
                            s.fecha_aprobacion_rechazo,
                            c.Nombres||' '||c.primer_apellido||' '||c.segundo_apellido nombre,
                            sa.desc_sub_aplicacion,
                            s.monto_solicitado,
                            s.meses_plazo,
                            s2.tasa,
                            f.descripcion_fuente,
                            s2.MONTO_APROBADO,
                            s2.TASA_APROBADA,
                            s2.PLAZO_APROBADO,
                            a.RECHAZO,
                            '('||ag.codigo_agencia||') '||ag.nombre_agencia agencia,
                            s.oficial_servicio,
                            u.Nombres||' '||u.primer_apellido||' '||u.segundo_apellido nombre_oficial,
                            s.antiguedad_meses, 
                            s.antiguedad_dias,
                            s.antiguedad_horas,
                            s.antiguedad_minutos,
                            s.antiguedad_segundos
                    from dcs_solicitudes s,  
                            dcs_solicitudes2 s2,
                            mgi_clientes  c,
                            mgi_sub_aplicaciones sa,
                            dcs_wf_fuentes_financiamiento f,
                            mgi_agencias ag,
                            mgi_usuarios u,
                            dcs_wf_estado_solicitudes es,
                            WFC.DCS_SOLICITUDES_APROBACIONES a
                    where c.codigo_empresa=1 and sa.codigo_empresa=1 and ag.codigo_empresa=1 and u.codigo_empresa=1 
                        and s.no_solicitud=s2.no_solicitud
                        and s.codigo_cliente=c.codigo_cliente
                        and s.estado_solicitud_id=es.estado_id
                        and s.codigo_sub_aplicacion=sa.codigo_sub_aplicacion
                        and s.codigo_fuente=f.codigo_fuente
                        and s.codigo_agencia_origen=ag.codigo_agencia
                        and s.oficial_servicio=u.codigo_usuario 
                        and s.no_solicitud=a.NO_SOLICITUD
                        and ROWNUM = 1
                        and s.no_solicitud=:pa_no_solicitud";
            }
            else {
                vl_sql = @"Select s.no_solicitud,
                            s.no_solicitud_formulario,
                            s.estado_solicitud_id,
                            s.abierta,
                            es.descripcion estado_solicitud,
                            s.fecha_presentacion,
                            s.fecha_cierre,
                            s.codigo_cliente,
                            s.decision_final_solicitud,
                            s.fecha_aprobacion_rechazo,
                            c.Nombres||' '||c.primer_apellido||' '||c.segundo_apellido nombre,
                            sa.desc_sub_aplicacion,
                            s.monto_solicitado,
                            s.meses_plazo,
                            s2.tasa,
                            f.descripcion_fuente,
                            s2.MONTO_APROBADO,
                            s2.TASA_APROBADA,
                            s2.PLAZO_APROBADO,
                            '('||ag.codigo_agencia||') '||ag.nombre_agencia agencia,
                            s.oficial_servicio,
                            u.Nombres||' '||u.primer_apellido||' '||u.segundo_apellido nombre_oficial,
                            s.antiguedad_meses, 
                            s.antiguedad_dias,
                            s.antiguedad_horas,
                            s.antiguedad_minutos,
                            s.antiguedad_segundos
                    from dcs_solicitudes s,  
                            dcs_solicitudes2 s2,
                            mgi_clientes  c,
                            mgi_sub_aplicaciones sa,
                            dcs_wf_fuentes_financiamiento f,
                            mgi_agencias ag,
                            mgi_usuarios u,
                            dcs_wf_estado_solicitudes es
                    where c.codigo_empresa=1 and sa.codigo_empresa=1 and ag.codigo_empresa=1 and u.codigo_empresa=1 
                        and s.no_solicitud=s2.no_solicitud
                        and s.codigo_cliente=c.codigo_cliente
                        and s.estado_solicitud_id=es.estado_id
                        and s.codigo_sub_aplicacion=sa.codigo_sub_aplicacion
                        and s.codigo_fuente=f.codigo_fuente
                        and s.codigo_agencia_origen=ag.codigo_agencia
                        and s.oficial_servicio=u.codigo_usuario
                        and s.no_solicitud=:pa_no_solicitud";
            }
             
            if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
            {
                DocSys.connOracle.Open();
            }
            OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
            cmd2.CommandType = CommandType.Text;
            //───────────────────
            OracleParameter pa_workflow_id = new OracleParameter("pa_no_solicitud", OracleType.Int32);
            cmd2.Parameters.Add(pa_workflow_id);
            pa_workflow_id.Direction = ParameterDirection.Input;
            pa_workflow_id.Value = p_no_solicitud;
            //───────────────────            
            OracleDataReader dr = cmd2.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                vl_abierta = dr["abierta"].ToString();
                textBox_fecha.Text = dr["fecha_presentacion"].ToString();
                txtFecha_cierre.Text = dr["fecha_cierre"].ToString();
                textBox_estado_solicitud.Text = dr["estado_solicitud"].ToString();
                textBox_codigo_cliente.Text = dr["codigo_cliente"].ToString();
                textBox_no_solicitud_formulario.Text = dr["no_solicitud_formulario"].ToString();
                textBox_nombre_afiliado.Text = dr["nombre"].ToString();
                textBox_producto.Text = dr["desc_sub_aplicacion"].ToString();
                textBox_filial.Text = dr["agencia"].ToString();
                textBox_monto.Text = String.Format("{0:###,###,###,##0.00}", float.Parse(dr["monto_solicitado"].ToString()));
                textBox_plazo.Text = dr["meses_plazo"].ToString();
                textBoxTasa.Text = dr["tasa"].ToString();
                textBox_oficial.Text = dr["oficial_servicio"].ToString();
                textBox_fondos.Text = dr["descripcion_fuente"].ToString();
                textBox_nombre_oficial.Text = dr["nombre_oficial"].ToString();
                txtDecision.Text = dr["decision_final_solicitud"].ToString();
                txtx_fecha_apbrchz.Text = dr["fecha_aprobacion_rechazo"].ToString();
                txt_montoaprb.Text = dr["MONTO_APROBADO"].ToString();//MONTO_APROBADO
                txt_plazo_apr.Text = dr["PLAZO_APROBADO"].ToString();
                txtx_tasa_apr.Text = dr["TASA_APROBADA"].ToString();
                if (cont > 0)
                {
                    txt_cod_negacion.Text = dr["RECHAZO"].ToString();//RECHAZO
                }
                else {
                    txt_cod_negacion.Text = "N/A";//RECHAZO
                }
                
                if (dr["estado_solicitud_id"].ToString() == "98")
                {
                    p_obtener_motivo_abandonado_afiliado();
                    textBox_motivo.Visible = true;
                    label_motivo.Visible = true;
                }
                else
                {
                    textBox_motivo.Visible = false;
                    label_motivo.Visible = false;
                }
                if (vl_abierta == "N")
                {
                    textBox_antiguedad_meses.Text = dr["antiguedad_meses"].ToString();
                    textBox_antiguedad_dias.Text = dr["antiguedad_dias"].ToString();
                    textBox_antiguedad_horas.Text = dr["antiguedad_horas"].ToString();
                    textBox_antiguedad_minutos.Text = dr["antiguedad_minutos"].ToString();
                    textBox_antiguedad_segundos.Text = dr["antiguedad_segundos"].ToString();
                }
            }
        }

        private void p_obtener_motivo_abandonado_afiliado()
        {
            string vl_sql = @"select * from dcs_movimientos_solicitudes where no_movimiento=:pa_no_movimiento ";
            if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
            {
                DocSys.connOracle.Open();
            }
            OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
            cmd2.CommandType = CommandType.Text;
            //───────────────────
            OracleParameter pa_no_movimiento = new OracleParameter("pa_no_movimiento", OracleType.Int32);
            cmd2.Parameters.Add(pa_no_movimiento);
            pa_no_movimiento.Direction = ParameterDirection.Input;
            pa_no_movimiento.Value = vl_no_movimiento;
            //───────────────────            
            OracleDataReader dr = cmd2.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                textBox_motivo.Text = dr["comentarios"].ToString();
            }
        }

        private void p_obtener_estacion_actual()
        {
            string vl_sql = @"Select nombre nombre_estacion
                                from dcs_solicitudes s,
                                     dcs_wf_estaciones e
                               Where s.estacion_id=e.estacion_id
                                 and no_solicitud=:pa_no_solicitud";
            if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
            {
                DocSys.connOracle.Open();
            }
            OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
            cmd2.CommandType = CommandType.Text;
            //───────────────────
            OracleParameter pa_no_movimiento = new OracleParameter("pa_no_solicitud", OracleType.Int32);
            cmd2.Parameters.Add(pa_no_movimiento);
            pa_no_movimiento.Direction = ParameterDirection.Input;
            pa_no_movimiento.Value = vl_no_solicitud;
            //───────────────────            
            OracleDataReader dr = cmd2.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                textBox_estacion_actual.Text = dr["nombre_estacion"].ToString();
            }
        }
        private void p_obtener_analista_actual()
        {
            string vl_sql = @"Select analista 
                                From dcs_movimientos_solicitudes 
                               Where no_movimiento=(select no_movimiento_actual from dcs_solicitudes where no_solicitud=:pa_no_solicitud)";
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
            pa_no_solicitud.Value = vl_no_solicitud;
            //───────────────────            
            OracleDataReader dr = cmd2.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                textBox_analista_actual.Text = dr["analista"].ToString();
            }
        }
        private void p_obtener_comite_resolucion()
        {
            DataTable dtResol=da.ObtenerAdmineResolucion(vl_no_solicitud);
            if (dtResol.Rows.Count > 0)
            {
                txtComiteResolucion.Text = dtResol.Rows[0]["NOMBRE"].ToString();
            }
        }
        private void p_obtener_antiguedad_solicitud_abierta()
        {
            try
            {
                string vl_sql = @"DCS_P_UTIL_CALCULA_DIF_FECHAS";
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
                p_fec_inicio.Value = DateTime.Parse(textBox_fecha.Text);
                //───────────────────            
                OracleParameter p_fec_fin = new OracleParameter("p_fec_fin", OracleType.DateTime);
                cmd2.Parameters.Add(p_fec_fin);
                p_fec_fin.Direction = ParameterDirection.Input;
                p_fec_fin.Value = p_obtener_fecha_server();

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
        private DateTime p_obtener_fecha_server()
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
            return DateTime.Parse(dr["hoy"].ToString());
        }
        private void p_marca_como_no_nueva(int p_no_movimiento)
        {
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string vl_sql = "";
                vl_sql = vl_sql + "DCS_P_MARCAR_NO_NUEVA";
                OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd.CommandType = CommandType.StoredProcedure;
                //───────────────────
                OracleParameter pa_no_movimiento = new OracleParameter("pa_no_movimiento", OracleType.Int32);
                cmd.Parameters.Add(pa_no_movimiento);
                pa_no_movimiento.Direction = ParameterDirection.Input;
                pa_no_movimiento.Value = p_no_movimiento;
                //───────────────────
                cmd.ExecuteReader();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_marca_como_leido :" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        private void p_llenar_grid_rutas()
        {
            try
            {
                string vl_sql;
                vl_sql = @"Select enviado_por,
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
                               from dcs_movimientos_solicitudes ms, 
                                    dcs_wf_estaciones e1,
                                    dcs_wf_decisiones d
                              where ms.estacion_id_to=e1.estacion_id                                
                                and ms.decision_id=d.decision_id(+)
                                and ms.estacion_id_from=0
                                and no_solicitud= :pa_no_solicitud                                                              
                             UNION
                             Select enviado_por,
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
                               from dcs_movimientos_solicitudes ms, 
                                    dcs_wf_estaciones e1,
                                    dcs_wf_estaciones e2,
                                    dcs_wf_decisiones d
                              where ms.estacion_id_from=e1.estacion_id
                                and ms.decision_id=d.decision_id
                                and ms.estacion_id_to=e2.estacion_id
                                and no_solicitud= :pa_no_solicitud
                              order by no_movimiento ";

                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }

                OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd2.CommandType = CommandType.Text;

                //───────────────────
                OracleParameter pa_param1 = new OracleParameter("pa_no_solicitud", OracleType.Int32);
                cmd2.Parameters.Add(pa_param1);
                pa_param1.Direction = ParameterDirection.Input;
                pa_param1.Value = int.Parse(textBox_no_solicitud.Text);
                //───────────────────

                OracleDataReader dr = cmd2.ExecuteReader();

                DataTable table = new DataTable();
                table.Columns.Add("no_movimiento");
                table.Columns.Add("enviado_por");
                table.Columns.Add("fecha_envio");
                table.Columns.Add("nombre_from");
                table.Columns.Add("nombre_to");
                table.Columns.Add("decision");
                table.Columns.Add("analista");
                table.Columns.Add("Ant_dias");
                table.Columns.Add("Ant_horas");
                table.Columns.Add("Ant_minutos");
                table.Columns.Add("Ant_segundos");
                table.Columns.Add("estadia_movimiento");
                while (dr.Read())
                {
                    table.Rows.Add(dr["no_movimiento"].ToString(),
                                   dr["enviado_por"].ToString(),
                                   dr["fecha_envio"].ToString(),
                                   dr["nombre_from"].ToString(),
                                   dr["nombre_to"].ToString(),
                                   dr["decision"].ToString(),
                                   dr["analista"].ToString(),
                                   dr["antiguedad_dias"].ToString(),
                                   dr["antiguedad_horas"].ToString(),
                                   dr["antiguedad_minutos"].ToString(),
                                   dr["antiguedad_segundos"].ToString(),
                                   dr["estadia_movimiento"].ToString());
                }
                gvRuta.DataSource = table;
                gvRuta.Refresh();
                table.Dispose();
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void p_llenar_adjuntos(int p_no_solicitud)
        {
            string vl_sql;
            try
            {
                vl_sql = @"Select no_archivo,
                                  nombre_archivo,
                                  extension,
                                  descripcion 
                             From dcs_archivos_adjuntos a,
                                  dcs_wf_tipo_documentos b 
                            Where a.documento_id=b.documento_id 
                              and no_solicitud=:pa_no_solicitud 
                            Order by a.documento_id ";
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd2.CommandType = CommandType.Text;
                //───────────────────
                OracleParameter pa_parametro1 = new OracleParameter("pa_no_solicitud", OracleType.Int32);
                cmd2.Parameters.Add(pa_parametro1);
                pa_parametro1.Direction = ParameterDirection.Input;
                pa_parametro1.Value = p_no_solicitud;
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
        private void p_llenar_anotaciones(int p_no_solicitud)
        {
            list_anotaciones.Clear();
            string vl_condi_por_mov = "";
            string vl_orden = " anot.no_anotacion ";
            if (rbOrdenCrono.Checked)
                vl_orden = " anot.no_anotacion ";
            if (rbOrdenxEstacion.Checked)
                vl_orden = " nombre_estacion,anot.no_anotacion ";
            int vl_no_mov = 0;
            DataGridViewRow row = gvRuta.CurrentRow;
            if (row != null)
            {
                vl_no_mov = int.Parse(row.Cells["no_movimiento"].Value.ToString());
            }

            if (checkBox_por_movimiento.Checked)
            {
                vl_condi_por_mov = " and anot.no_movimiento_solicitud = " + vl_no_mov.ToString();
            }
            else
            {
                vl_condi_por_mov = "";
            }
            try
            {
                string vl_sql = @"Select est.nombre nombre_estacion,
                                         anot.no_movimiento_solicitud,
                                         anot.no_anotacion,
                                         anot.anotacion,
                                         anot.tipo_anotacion,
                                         anot.usuario_ing,
                                         anot.fecha_ing  
                                    from dcs_anotaciones_solicitudes anot,
                                         dcs_wf_estaciones est 
                                   where anot.estacion_id=est.estacion_id
                                     and no_solicitud=:pa_no_solicitud " +
                                          vl_condi_por_mov +
                                   "order by " + vl_orden;
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd2.CommandType = CommandType.Text;

                //───────────────────
                OracleParameter pa_parametro1 = new OracleParameter("pa_no_solicitud", OracleType.Int32);
                cmd2.Parameters.Add(pa_parametro1);
                pa_parametro1.Direction = ParameterDirection.Input;
                pa_parametro1.Value = p_no_solicitud;
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_llenar_anotaciones : " + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void p_abrir_anotacion(int p_no_anotacion)
        {
            string vl_sql = "";
            vl_sql = vl_sql + "Select * from dcs_anotaciones_solicitudes where no_anotacion=:pa_no_anotacion";
            if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
            {
                DocSys.connOracle.Open();
            }
            OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
            cmd2.CommandType = CommandType.Text;
            //───────────────────
            OracleParameter pa_parametro1 = new OracleParameter("pa_no_anotacion", OracleType.Int32);
            cmd2.Parameters.Add(pa_parametro1);
            pa_parametro1.Direction = ParameterDirection.Input;
            pa_parametro1.Value = p_no_anotacion;
            //───────────────────

            OracleDataReader dr = cmd2.ExecuteReader();
            dr.Read();
            if (!DBNull.Value.Equals(dr["anotacion"].ToString()))
            {
                /*s_add_notas forma = new s_add_notas("CONS", 
                                                    int.Parse(dr["no_solicitud"].ToString()),
                                                    p_no_anotacion,
                                                    dr["anotacion"].ToString());*/
                //forma.ShowDialog();
            }
        }
        private void p_abrir_adjunto(int p_no_archivo)
        {
            string vl_sql = "";
            vl_sql = vl_sql + "Select * from dcs_archivos_adjuntos where no_archivo=:pa_no_archivo";
            if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
            {
                DocSys.connOracle.Open();
            }
            OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
            cmd2.CommandType = CommandType.Text;
            //───────────────────
            OracleParameter pa_parametro1 = new OracleParameter("pa_no_archivo", OracleType.Int32);
            cmd2.Parameters.Add(pa_parametro1);
            pa_parametro1.Direction = ParameterDirection.Input;
            pa_parametro1.Value = p_no_archivo;
            //───────────────────

            OracleDataReader dr = cmd2.ExecuteReader();
            dr.Read();
            if (!DBNull.Value.Equals(dr["archivo_bin"]))
            {
                byte[] bits = ((byte[])dr["archivo_bin"]);
                string sFile = Application.StartupPath.ToString() + @"\tmp\" + DocSys.vl_user + "tmp" + dr["no_solicitud"] + DateTime.Now.ToString("yyyyMMddhhmmss") + "." + dr["extension"].ToString();
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
            }
        }

        #region Eventos de los Objetos
        private void list_anotaciones_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = list_anotaciones.GetItemAt(e.X, e.Y);
            if (item != null)
            {
                p_abrir_anotacion(int.Parse(item.SubItems[0].Text));
            }
        }
        private void list_adjuntos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = list_adjuntos.GetItemAt(e.X, e.Y);
            if (item != null)
            {
                p_abrir_adjunto(int.Parse(item.SubItems[2].Text));
                /*MessageBox.Show(item.Text);*/
            }
        }
        private void rbComentarios_iconos_CheckedChanged(object sender, EventArgs e)
        {
            list_anotaciones.View = View.LargeIcon;
        }
        private void rbComentarios_titulos_CheckedChanged(object sender, EventArgs e)
        {
            list_anotaciones.View = View.Tile;
        }
        private void rbComentarios_detalle_CheckedChanged(object sender, EventArgs e)
        {
            list_anotaciones.View = View.Details;
        }
        private void rbDocumentos_iconos_CheckedChanged(object sender, EventArgs e)
        {
            list_adjuntos.View = View.LargeIcon;
        }
        private void rbDocumentos_titulos_CheckedChanged(object sender, EventArgs e)
        {
            list_adjuntos.View = View.Tile;
        }
        private void rbDocumentos_detalle_CheckedChanged(object sender, EventArgs e)
        {
            list_adjuntos.View = View.Details;
        }
        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void checkBox_por_movimiento_CheckedChanged(object sender, EventArgs e)
        {
            p_llenar_anotaciones(vl_no_solicitud);
        }
        private void gvRuta_SelectionChanged(object sender, EventArgs e)
        {
            p_llenar_anotaciones(vl_no_solicitud);
        }
        private void gvRuta_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                this.Cursor = Cursors.Hand;
                if (vl_mostrar_miniinfo)
                {
                    if (e.RowIndex >= 0)
                    {
                        string vl_codigo = gvRuta.Rows[e.RowIndex].Cells["enviado_por"].Value.ToString();
                        miniinfo_user.get_set_codigo_usuario = vl_codigo;

                        Point pos = this.PointToScreen(e.Location);
                        miniinfo_user.da = this.da;
                        miniinfo_user.Show();                        
                        miniinfo_user.Location = new Point(Control.MousePosition.X + 5, Control.MousePosition.Y + 10);
                        miniinfo_user.Refresh();
                    }
                }
                vl_mostrar_miniinfo = false; //para que no se este cargando mientras tenga el mouse sobre la foto
            }

            if (e.ColumnIndex == 8)
            {
                if (e.RowIndex >= 0)
                {
                    if (gvRuta.Rows[e.RowIndex].Cells["analista"].Value.ToString() == string.Empty)
                        return;


                }
                this.Cursor = Cursors.Hand;
                if (vl_mostrar_miniinfo)
                {
                    if (e.RowIndex >= 0)
                    {
                        int vl_no_movimiento = int.Parse(gvRuta.Rows[e.RowIndex].Cells["no_movimiento"].Value.ToString());
                        miniinfo_asig.get_set_no_movimiento = vl_no_movimiento;


                        Point pos = this.PointToScreen(e.Location);
                        miniinfo_asig.Show();
                        miniinfo_asig.Location = new Point(Control.MousePosition.X + 5, Control.MousePosition.Y + 10);
                        miniinfo_asig.Refresh();
                    }
                }
                vl_mostrar_miniinfo = false; //para que no se este cargando mientras tenga el mouse sobre la foto
            }
        }
        private void gvRuta_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                vl_mostrar_miniinfo = true;
                miniinfo_user.Hide();
            }
            if (e.ColumnIndex == 8)
            {
                vl_mostrar_miniinfo = true;
                miniinfo_asig.Hide();
            }
            this.Cursor = Cursors.Default;
        }
        private void Button_refrescar_Click(object sender, EventArgs e)
        {
            s_solicitud_detalle_Load(null, null);
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (splitContainer2.Panel2Collapsed)
                splitContainer2.Panel2Collapsed = false;
            else
                splitContainer2.Panel2Collapsed = true;
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (splitContainer1.Panel2Collapsed)
                splitContainer1.Panel2Collapsed = false;
            else
                splitContainer1.Panel2Collapsed = true;
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            p_obtener_antiguedad_solicitud_abierta();
        }
        private void textBox_analista_actual_MouseMove(object sender, MouseEventArgs e)
        {
            if (textBox_analista_actual.Text == string.Empty)
                return;
            miniinfo_user.get_set_codigo_usuario = textBox_analista_actual.Text;
            Point pos = this.PointToScreen(e.Location);
            miniinfo_user.Show();
            miniinfo_user.Location = new Point(Control.MousePosition.X - 105, Control.MousePosition.Y + 10);
            miniinfo_user.Refresh();
        }
        private void textBox_analista_actual_MouseLeave(object sender, EventArgs e)
        {
            vl_mostrar_miniinfo = true;
            miniinfo_user.Hide();
            this.Cursor = Cursors.Default;
        }
        private void textBox_oficial_MouseMove(object sender, MouseEventArgs e)
        {
            if (textBox_oficial.Text == string.Empty)
                return;
            miniinfo_user.get_set_codigo_usuario = textBox_oficial.Text;
            Point pos = this.PointToScreen(e.Location);
            miniinfo_user.da = this.da;
            miniinfo_user.Show();
            miniinfo_user.Location = new Point(Control.MousePosition.X - 105, Control.MousePosition.Y + 10);
            miniinfo_user.Refresh();
        }
        private void textBox_oficial_MouseLeave(object sender, EventArgs e)
        {
            vl_mostrar_miniinfo = true;
            miniinfo_user.Hide();
            this.Cursor = Cursors.Default;
        }
        private void rbOrdenCrono_CheckedChanged(object sender, EventArgs e)
        {
            p_llenar_anotaciones(vl_no_solicitud);
        }
        private void rbOrdenxEstacion_CheckedChanged(object sender, EventArgs e)
        {
            p_llenar_anotaciones(vl_no_solicitud);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }
        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            s_AnalisisCredito_01 forma = new s_AnalisisCredito_01(da, textBox_no_solicitud.Text);
            forma.ShowDialog();
        }
        private void lnkTablaresultado_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmsRpts.frmRpt_ResolucionComite forma = new FrmsRpts.frmRpt_ResolucionComite(da);
            forma.gno_solicitud = Convert.ToInt32(textBox_no_solicitud.Text);
            forma.ShowDialog();
        }
        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            vl_no_solicitud = Int32.Parse(textBox_no_solicitud.Text);
            FrmsRpts.frmRpt_solicitud forma = new FrmsRpts.frmRpt_solicitud(da, vl_no_solicitud);
            forma.ShowDialog();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmsRpts.frmRpt_AnalisisCualitativo forma = new FrmsRpts.frmRpt_AnalisisCualitativo(da);
            forma.gno_solicitud = Convert.ToInt32(textBox_no_solicitud.Text);
            forma.ShowDialog();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            s_PreCalificado forma = new s_PreCalificado(da);
            forma.gmodo_coopsafa = "CONS";
            forma.txtNo_solicitud_coopsafa.Text = textBox_no_solicitud.Text;
            DialogResult res = forma.ShowDialog();
        }

        private void llMiniEstadoAfiliado_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            s_miniestado_cliente forma = new s_miniestado_cliente();
            forma.da = da;
            forma.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            s_analisis_cuantitativo forma = new s_analisis_cuantitativo();
            forma.txtNo_solicitud.Text = textBox_no_solicitud.Text;
            forma.btnCarlcularLiq.Visible = false;
            forma.da = da;
            DialogResult res = forma.ShowDialog();   
        }
        private void LlHojaRuta_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmsRpts.frmRpt_HojaRuta forma = new FrmsRpts.frmRpt_HojaRuta();
            forma.da = this.da;
            forma.gno_solicitud = Convert.ToInt32(textBox_no_solicitud.Text);
            forma.ShowDialog();
        }
        private void lLControlDocs_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmsRpts.frmRpt_ControlDocumental forma = new FrmsRpts.frmRpt_ControlDocumental();
            forma.da = this.da;
            forma.gno_solicitud = Convert.ToInt32(textBox_no_solicitud.Text);
            forma.ShowDialog();
        }

        private void pbFotoVigente_Click(object sender, EventArgs e)
        {
            s_PreCalificado_info03 forma = new s_PreCalificado_info03();
            forma.pbFotoVigente.Image = pbFotoVigente.Image;
            forma.codigo_cliente = textBox_codigo_cliente.Text;
            forma.da = da;
            forma.ShowDialog();
        }

        private void lLDetalle_cierre_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            s_solicitud_detalle01 forma = new s_solicitud_detalle01();
            forma.da = this.da;
            forma.gno_solicitud = Int32.Parse(textBox_no_solicitud.Text);
            forma.ShowDialog();
        }

        private void lLResoluciones_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            s_solicitudes_resoluciones forma = new s_solicitudes_resoluciones();
            forma.gno_solicitud = vl_no_solicitud;
            forma.da = this.da;
            forma.ShowDialog();
        }

        private void cbVer_CheckedChanged(object sender, EventArgs e)
        {
            if (cbVer.Checked)
            {
                gvRuta.Columns["nombre_from"].Visible = false;
                gvRuta.Columns["decision"].Visible = false;
                gvRuta.Columns["analista"].Visible = false;
                gvRuta.Columns["nombre_to"].HeaderText = "Estacion Responsable del tiempo";
            }
            else
            {
                gvRuta.Columns["nombre_from"].Visible = true;
                gvRuta.Columns["decision"].Visible = true;
                gvRuta.Columns["analista"].Visible = true;
                gvRuta.Columns["nombre_to"].HeaderText = "Para : (Destino)";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

		private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			s_excepciones_doc01 forma = new s_excepciones_doc01(da, vl_no_solicitud, this.textBox_nombre_afiliado.Text, 
																this.textBox_producto.Text, this.textBox_monto.Text);
			forma.ShowDialog();
		}
    }
}
