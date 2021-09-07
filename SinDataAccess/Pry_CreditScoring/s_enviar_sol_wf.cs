using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OracleClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Docsis_Application
{
    public partial class s_enviar_sol_wf : Form
    {
        

        string vl_enproceso_envio = "";
        int vl_workflow_id = 0;
        int vl_paso = 0;        
        int vl_no_solicitud=0;
        Thread myThread;
        int global_estacion_id_to = DocSys.p_obtener_estacion_usuario(DocSys.vl_user);

        #region Para la barra de progreso
        /*  Delegados*/
        delegate void SetValueCallback(int valor);

        
        public s_enviar_sol_wf(int pa_workflow_id,int pa_no_solicitud)
        {
            InitializeComponent();
            vl_workflow_id = pa_workflow_id;
            vl_no_solicitud = pa_no_solicitud;
            vl_paso = 0;
        }

        private void SetValue(int valor)
        {
            if (this.InvokeRequired)
            {
                SetValueCallback d = new SetValueCallback(SetValue);
                this.Invoke(d, new object[] { valor });
            }
            else
            {
                
                this.progressBar1.Value = valor;
                if (valor == 100)
                {
                    this.progressBar1.Visible = false;
                    this.pboxLoading.Visible = false;
                    this.pboxLoading02.Visible = false;
                }                                
            }
        }
        private void SetMax(int valor)
        {
            if (this.InvokeRequired)
            {
                SetValueCallback d = new SetValueCallback(SetMax);
                this.Invoke(d, new object[] { valor });
            }
            else
            {
                this.button_ejecutar.Enabled = false;
                this.progressBar1.Maximum = valor;
                this.progressBar1.Visible = true;
                this.pboxLoading.Visible = true;
                this.pboxLoading02.Visible = true;
            }
        }
        #endregion
      
        private void s_enviar_sol_wf_Load(object sender, EventArgs e)
        {
            if (DocSys.p_get_valor_parametro("WFC-0002") == "S")
            {
                StatusLabel.Text = ":: Hora límite para envio de solicitudes :" + DocSys.p_formato_24h(int.Parse(DocSys.p_get_valor_parametro("WFC-0003"))).ToString();
            }
            else
            {
                StatusLabel.Visible = false;
            }
            
            p_get_datos_solicitud();
           
            txtPaso_actual.Text = vl_paso.ToString();
            txtPaso_siguiente.Text = (vl_paso + 1).ToString();
            p_llenar_combo_decisiones();
            p_popular_lista_documentos();
            label_doc_pendientes.Text = p_get_cant_doc_pendientes().ToString();
            
        }
        private void p_llenar_combo_decisiones()
        {
            try
            {
                string vl_query = @"Select dcs_wf_decisiones.decision_id,dcs_wf_decisiones.descripcion 
                                      from dcs_wf_flujos f,dcs_wf_decisiones 
                                     Where f.decision_id=dcs_wf_decisiones.decision_id 
                                       and f.workflow_id=:pa_workflow_id
                                       and f.paso=:pa_paso";
                OracleCommand cmd = new OracleCommand(vl_query, DocSys.connOracle);
                cmd.CommandType = CommandType.Text;
                //───────────────────
                OracleParameter pa_parametro1 = new OracleParameter("pa_paso", OracleType.Int32);
                cmd.Parameters.Add(pa_parametro1);
                pa_parametro1.Direction = ParameterDirection.Input;
                pa_parametro1.Value = int.Parse(txtPaso_actual.Text);
                //───────────────────
                OracleParameter pa_parametro2 = new OracleParameter("pa_workflow_id", OracleType.Int32);
                cmd.Parameters.Add(pa_parametro2);
                pa_parametro2.Direction = ParameterDirection.Input;
                pa_parametro2.Value = DocSys.vl_workflow_id;

                OracleDataAdapter adaptador = new OracleDataAdapter(cmd);
                DataSet registros = new DataSet();
                
                adaptador.Fill(registros, "dcs_wf_decisiones");                
                ComboBox_decision.DataSource = registros;                
                ComboBox_decision.DisplayMember = "dcs_wf_decisiones.descripcion";                
                ComboBox_decision.ValueMember = "dcs_wf_decisiones.decision_id";                
                ComboBox_decision_SelectionChangeCommitted(null, null);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en llenar combo_decisiones : " + ex.Message);
            }
        }
        private void p_popular_lista_documentos()
        {
            string vl_sql = @"Select td.documento_id,descripcion nombre_doc,decode(tipo_exigencia,'R','Requerido','O','Opcional') tipo_exigencia
                                from DCS_WF_DOCUMENTOS_WORKFLOW dw,
                                     DCS_WF_TIPO_DOCUMENTOS td
                               where dw.DOCUMENTO_ID=td.DOCUMENTO_ID
                                 and dw.activo='S'
                                 and dw.workflow_id=:pa_workflow_id";
            if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
            {
                DocSys.connOracle.Open();
            }
            OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
            cmd2.CommandType = CommandType.Text;
            //───────────────────
            OracleParameter pa_workflow_id = new OracleParameter("pa_workflow_id", OracleType.Int32);
            cmd2.Parameters.Add(pa_workflow_id);
            pa_workflow_id.Direction = ParameterDirection.Input;
            pa_workflow_id.Value = DocSys.vl_workflow_id;
            //───────────────────
            
            OracleDataReader dr = cmd2.ExecuteReader();
            DataTable table = new DataTable();
            table.Columns.Add("recibido_b");
            table.Columns.Add("documento_id");
            table.Columns.Add("nombre_doc");
            table.Columns.Add("tipo_exigencia");
            int vl_tipo_doc = 0;
            while (dr.Read())
            {
                vl_tipo_doc = int.Parse(dr["documento_id"].ToString());  
                table.Rows.Add(DocSys.p_verificar_doc_existe(vl_no_solicitud,vl_tipo_doc),
                               dr["documento_id"].ToString(),
                               dr["nombre_doc"].ToString(),
                               dr["tipo_exigencia"].ToString());
            }
            gvDocumentos_solicitud.DataSource = table;
            gvDocumentos_solicitud.Refresh();
            table.Dispose();
            dr.Close();
        }
        private void p_get_datos_solicitud()
        {
            if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
            {
                DocSys.connOracle.Open();
            }
            string vl_sql = @" Select s.no_solicitud,monto_solicitado,nombres||' '||primer_apellido nombre,desc_sub_aplicacion,paso_actual,estacion_id,enproceso_envio
                                 From dcs_solicitudes s,mgi_clientes c,mgi_sub_aplicaciones sa 
                                Where c.codigo_empresa=sa.codigo_empresa
                                  and c.codigo_empresa=1 
                                  and s.codigo_cliente=c.codigo_cliente
                                  and s.codigo_sub_aplicacion=sa.codigo_sub_aplicacion 
                                  and s.no_solicitud=:pa_no_solicitud";
            
            OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
            cmd.CommandType = CommandType.Text;
            //───────────────────
            OracleParameter pa_no_solicitud = new OracleParameter("pa_no_solicitud", OracleType.Int32);
            cmd.Parameters.Add(pa_no_solicitud);
            pa_no_solicitud.Direction = ParameterDirection.Input;
            pa_no_solicitud.Value = vl_no_solicitud;
            OracleDataReader dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                vl_paso = int.Parse(dr["paso_actual"].ToString());
                txtNombre_estacion_actual.Text=DocSys.p_get_nombre_estacion(int.Parse(dr["estacion_id"].ToString()));
                label_nombre.Text = dr["nombre"].ToString();
                label_producto.Text = dr["desc_sub_aplicacion"].ToString();
                label_no_solicitud.Text = dr["no_solicitud"].ToString();
                label_monto.Text = String.Format("{0:###,###,###,##0.00}", float.Parse(dr["monto_solicitado"].ToString()));
                vl_enproceso_envio = dr["enproceso_envio"].ToString();
            }
        }
        private int p_get_cant_doc_pendientes()
        {
            int vl_retorno = 0;

            if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
            {
                DocSys.connOracle.Open();
            }
            string vl_sql = "select dcs_f_cant_doc_pendientes(:pa_no_solicitud) cant from dual";
            
            OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
            cmd.CommandType = CommandType.Text;
            //───────────────────
            OracleParameter pa_no_solicitud = new OracleParameter("pa_no_solicitud", OracleType.Int32);
            cmd.Parameters.Add(pa_no_solicitud);
            pa_no_solicitud.Direction = ParameterDirection.Input;
            pa_no_solicitud.Value = vl_no_solicitud;           

            OracleDataReader dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                vl_retorno = int.Parse(dr["cant"].ToString());                
            }
            return vl_retorno;
        }        
        private void p_get_estaciones_de_la_decision(int p_paso,int p_decision_id)
        {
            if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
            {
                DocSys.connOracle.Open();
            }
            string vl_sql = @"Select e1.nombre estacion_origen,e2.nombre estacion_destino,tipo_respuesta,decision_id
                                from dcs_wf_flujos f,
                                     dcs_wf_estaciones e1,
                                     dcs_wf_estaciones e2
                               Where f.estacion_id_from=e1.estacion_id
                                 and f.estacion_id_to=e2.estacion_id
                                 and f.paso=:pa_paso_actual
                                 and f.decision_id=:pa_decision_id
                                 and f.workflow_id=:pa_workflow_id";
            OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
            cmd.CommandType = CommandType.Text;
            //───────────────────
            OracleParameter pa_parametro1 = new OracleParameter("pa_paso_actual", OracleType.Int32);
            cmd.Parameters.Add(pa_parametro1);
            pa_parametro1.Direction = ParameterDirection.Input;
            pa_parametro1.Value = p_paso;
            //───────────────────
            OracleParameter pa_parametro2 = new OracleParameter("pa_decision_id", OracleType.Int32);
            cmd.Parameters.Add(pa_parametro2);
            pa_parametro2.Direction = ParameterDirection.Input;
            pa_parametro2.Value = p_decision_id;
            //───────────────────
            OracleParameter pa_parametro3 = new OracleParameter("pa_workflow_id", OracleType.Int32);
            cmd.Parameters.Add(pa_parametro3);
            pa_parametro3.Direction = ParameterDirection.Input;
            pa_parametro3.Value = vl_workflow_id;
            //───────────────────
            OracleDataReader dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                txtEstacion_origen.Text = dr["estacion_origen"].ToString();
                txtEstacion_destino.Text = dr["estacion_destino"].ToString();                                
            }                 
        }
        private int p_get_cant_anotaciones_condi_pendientes(int p_no_solicitud)
        {
            int vl_return = 0;
            if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
            {
                DocSys.connOracle.Open();
            }
            string vl_sql = @"Select count(*) as cant
                                    from dcs_anotaciones_solicitudes 
                                   Where no_solicitud=:pa_no_solicitud
                                     and tipo_anotacion='C'
                                     and aceptada='N'";
            OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
            cmd.CommandType = CommandType.Text;
            //───────────────────
            OracleParameter pa_no_solicitud = new OracleParameter("pa_no_solicitud", OracleType.Int32);
            cmd.Parameters.Add(pa_no_solicitud);
            pa_no_solicitud.Direction = ParameterDirection.Input;
            pa_no_solicitud.Value = p_no_solicitud;
            OracleDataReader dr = cmd.ExecuteReader();
            dr.Read();
            vl_return = int.Parse(dr["cant"].ToString());
            return vl_return;
        }
        private string p_get_si_decision_cierre(int p_decision_id)
        {
            string vl_return = "";
            if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
            {
                DocSys.connOracle.Open();
            }
            string vl_sql = @"Select cerrar_solicitud From dcs_wf_decisiones Where decision_id=:pa_decision_id";
            OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
            cmd.CommandType = CommandType.Text;
            //───────────────────
            OracleParameter pa_decision_id = new OracleParameter("pa_decision_id", OracleType.Int32);
            cmd.Parameters.Add(pa_decision_id);
            pa_decision_id.Direction = ParameterDirection.Input;
            pa_decision_id.Value = p_decision_id;

            OracleDataReader dr = cmd.ExecuteReader();
            dr.Read();
            vl_return = dr["cerrar_solicitud"].ToString();
            return vl_return;
        }
        private string p_get_enproceso_solicitud()
        {
            string vl_return="";
            if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
            {
                DocSys.connOracle.Open();
            }
            string vl_sql = @" Select enproceso_envio
                                 From dcs_solicitudes s 
                                Where no_solicitud=:pa_no_solicitud";

            OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
            cmd.CommandType = CommandType.Text;
            //───────────────────
            cmd.Parameters.Add("pa_no_solicitud", OracleType.Int32).Value = vl_no_solicitud;
                        
            OracleDataReader dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                vl_return = dr["enproceso_envio"].ToString();
            }
            return vl_return;
        }
        private void p_procesar_envio(int p_workflow_id,
                                      int p_no_solicitud,
                                      int p_paso,
                                      int p_decision)
        {
            p_actualizar_enproceso_envio(vl_no_solicitud, "S");
            SetMax(101);                      
            Thread.Sleep(3000);
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string vl_sql = "";
                vl_sql = vl_sql + "DCS_P_ENVIAR_SOLICITUD";
                OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd.CommandType = CommandType.StoredProcedure;
                //───────────────────
                OracleParameter pa_workflow_id = new OracleParameter("pa_workflow_id", OracleType.Int32);
                cmd.Parameters.Add(pa_workflow_id);
                pa_workflow_id.Direction = ParameterDirection.Input;
                pa_workflow_id.Value = p_workflow_id;
                //───────────────────
                OracleParameter pa_no_solicitud = new OracleParameter("pa_no_solicitud", OracleType.Int32);
                cmd.Parameters.Add(pa_no_solicitud);
                pa_no_solicitud.Direction = ParameterDirection.Input;
                pa_no_solicitud.Value = p_no_solicitud;
                //───────────────────
                OracleParameter pa_paso = new OracleParameter("pa_paso", OracleType.Int32);
                cmd.Parameters.Add(pa_paso);
                pa_paso.Direction = ParameterDirection.Input;
                pa_paso.Value = p_paso;
                //───────────────────
                OracleParameter pa_decision = new OracleParameter("pa_decision", OracleType.Int32);
                cmd.Parameters.Add(pa_decision);
                pa_decision.Direction = ParameterDirection.Input;
                pa_decision.Value = p_decision;
                //───────────────────
                cmd.ExecuteReader();
                SetValue(100);
                p_actualizar_enproceso_envio(vl_no_solicitud, "N");
                MessageBox.Show("La solicitud se ha enviado satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_procesar_envio " + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }            
        }                
        private void ComboBox_decision_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
            txtDecision_id.Text = ComboBox_decision.SelectedValue.ToString();            
            if (txtDecision_id.Text != "" & txtPaso_actual.Text != "")
            {
                
                p_get_estaciones_de_la_decision(int.Parse(txtPaso_actual.Text), int.Parse(txtDecision_id.Text));
                
            }
        }
        private void p_puente()
        {            
            int vl_paso_actual = int.Parse(txtPaso_actual.Text);
            int vl_decision = int.Parse(txtDecision_id.Text);
            p_procesar_envio(vl_workflow_id, vl_no_solicitud, vl_paso_actual, vl_decision);
        }
        private void button_ejecutar_Click(object sender, EventArgs e)
        {
            if (!DocSys.p_valida_politicas_envio_credito())
            {
                return;
            }
            int vl_cant_doc_pend = 0;
            int.TryParse(label_doc_pendientes.Text, out vl_cant_doc_pend);
            if (vl_cant_doc_pend > 0)
            {
                MessageBox.Show("Hay documentos obligatorios o requeridos que no han sido subidos a la solicitud, no puede enviar la solicitud ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string vl_cierra_solicitud=p_get_si_decision_cierre(int.Parse(txtDecision_id.Text));
            
            if (vl_cierra_solicitud == "S")
            {
                int vl_anotaciones__condi_pendientes = p_get_cant_anotaciones_condi_pendientes(vl_no_solicitud);
                if (vl_anotaciones__condi_pendientes > 0)
                {
                    MessageBox.Show("Hay "+vl_anotaciones__condi_pendientes.ToString().Trim()+" condiciones pendientes de ser aceptadas por los usuario que las ingresaron para poder continuar con el siguiente paso", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            if (DialogResult.Yes == MessageBox.Show("Desea ejecutar el proceso  ?", "Aviso ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                if (global_estacion_id_to != DocSys.p_get_estacion_current(vl_no_solicitud))
                {
                    MessageBox.Show("La solicitud que esta tratando de enviar ya fue procesada por otro usuario !", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (p_get_enproceso_solicitud() == "S")
                {
                    MessageBox.Show("La solicitud se encuentra en proceso de envio, no puede ejecutar la operación, por favor intente nuevamente..!", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }
                myThread = new Thread(new ThreadStart(p_puente));
                myThread.Start();
            }
        }
        private void button_cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button_cons_clientes_Click(object sender, EventArgs e)
        {
            //s_add_notas forma = new s_add_notas(da,"INS", vl_no_solicitud,0);
            //forma.ShowDialog();
        }
        private void p_actualizar_enproceso_envio(int p_no_solicitud,string p_sino)
        {
            try
            {
                
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string vl_sql = @"Update dcs_solicitudes set enproceso_envio=:pa_sino
                                   Where no_solicitud=:pa_no_solicitud";
                OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd.CommandType = CommandType.Text;
                //───────────────────
                cmd.Parameters.Add("pa_no_solicitud", OracleType.Int32).Value = p_no_solicitud;
                cmd.Parameters.Add("pa_sino", OracleType.VarChar, 1).Value = p_sino;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            


            

        }
         
    }
}
