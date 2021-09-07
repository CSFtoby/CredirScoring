using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CoopSafa_utils;

namespace Docsis_Application

    
{
    public partial class s_solicitud_doc : Form
    {

        string vl_valida_existe_no_solicitud = "N";
        //WorkFlowcreditos_Service ws_wfc = new WorkFlowcreditos_Service();
   
        public s_solicitud_doc()
        {
            InitializeComponent();
        }
        private void s_solicitud_doc_Load(object sender, EventArgs e)
        {
            DateTime vl_fecha_sol;

            p_llenar_combo_sub_aplicaciones();
            p_llenar_combo_Fuentes_financiamiento();
            vl_fecha_sol=Funciones_Oracle .fecha_servidor(DocSys.connOracle);

            if (vl_fecha_sol.ToString().Length>2)
            {
                txtFecha_solicitud.Text = vl_fecha_sol.ToString().Substring(0, 10);
            }
            p_obtener_sub_aplicacion_workflow(DocSys.vl_workflow_id);
        }
        
        private void p_insertar_solicitud()
        {
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string vl_sql = "DCS_P_INSERTAR_SOLICITUD";                
                OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd.CommandType = CommandType.StoredProcedure;
                //───────────────────
                OracleParameter pa_workflow_id = new OracleParameter("pa_workflow_id", OracleType.Int32);
                cmd.Parameters.Add(pa_workflow_id);
                pa_workflow_id.Direction = ParameterDirection.Input;
                pa_workflow_id.Value = DocSys.vl_workflow_id;
                //───────────────────
                OracleParameter pa_codigo_agencia_origen = new OracleParameter("pa_codigo_agencia_origen", OracleType.Number);
                cmd.Parameters.Add(pa_codigo_agencia_origen);
                pa_codigo_agencia_origen.Direction = ParameterDirection.Input;
                pa_codigo_agencia_origen.Value = DocSys.vl_agencia_usuario;
                //───────────────────
                OracleParameter pa_no_solicitud_formulario = new OracleParameter("pa_no_solicitud_formulario", OracleType.Number);
                cmd.Parameters.Add(pa_no_solicitud_formulario);
                pa_no_solicitud_formulario.Direction = ParameterDirection.Input;
                int vl_no_solicitud_formulario = 0;
                int.TryParse(txtNo_solicitud_formulario.Text,out vl_no_solicitud_formulario);
                pa_no_solicitud_formulario.Value = vl_no_solicitud_formulario;
                //───────────────────
                OracleParameter pa_codigo_cliente = new OracleParameter("pa_codigo_cliente", OracleType.Number);
                cmd.Parameters.Add(pa_codigo_cliente);
                pa_codigo_cliente.Direction = ParameterDirection.Input;
                pa_codigo_cliente.Value = int.Parse(txtCodigo_cliente.Text);
                //───────────────────
                OracleParameter pa_codigo_fuente = new OracleParameter("pa_codigo_fuente", OracleType.Number);
                cmd.Parameters.Add(pa_codigo_fuente);
                pa_codigo_fuente.Direction = ParameterDirection.Input;
                pa_codigo_fuente.Value = int.Parse(txtCodigo_fuente_financiamiento.Text);
                //───────────────────
                OracleParameter pa_codigo_sub_aplicacion = new OracleParameter("pa_codigo_sub_aplicacion", OracleType.Number);
                cmd.Parameters.Add(pa_codigo_sub_aplicacion);
                pa_codigo_sub_aplicacion.Direction = ParameterDirection.Input;                
                pa_codigo_sub_aplicacion.Value = int.Parse(txtCodigo_sub_aplicacion.Text);
                //───────────────────
                OracleParameter pa_codigo_moneda = new OracleParameter("pa_codigo_moneda", OracleType.Number);
                cmd.Parameters.Add(pa_codigo_moneda);
                pa_codigo_moneda.Direction = ParameterDirection.Input;
                pa_codigo_moneda.Value = int.Parse(txtcodigo_moneda.Text);
                //───────────────────
                OracleParameter pa_oficial_servicio = new OracleParameter("pa_oficial_servicio", OracleType.VarChar, 30);
                cmd.Parameters.Add(pa_oficial_servicio);
                pa_oficial_servicio.Direction = ParameterDirection.Input;
                pa_oficial_servicio.Value = DocSys.vl_user;
                //───────────────────
                OracleParameter pa_monto_solicitado = new OracleParameter("pa_monto_solicitado", OracleType.Number);
                cmd.Parameters.Add(pa_monto_solicitado);
                pa_monto_solicitado.Direction = ParameterDirection.Input;
                pa_monto_solicitado.Value = decimal.Parse(txtMonto_solicitado.Text);
                //───────────────────
                OracleParameter pa_meses_plazo = new OracleParameter("pa_meses_plazo", OracleType.Number);
                cmd.Parameters.Add(pa_meses_plazo);
                pa_meses_plazo.Direction = ParameterDirection.Input;
                pa_meses_plazo.Value = int.Parse(txtPlazo.Text);
                //───────────────────
                cmd.ExecuteReader();                
                MessageBox.Show("La solicitud ingresada satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                this.DialogResult = DialogResult.OK;
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_insertar_solicitud :" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }            
        }
        private void p_llenar_combo_sub_aplicaciones()
        {
            try
            {
                OracleCommand cmd = new OracleCommand("select codigo_sub_aplicacion,initcap(desc_sub_aplicacion) desc_sub_aplicacion from mgi_sub_aplicaciones where codigo_aplicacion='MCR' and activo_b='S'", DocSys.connOracle);
                cmd.CommandType = CommandType.Text;
                //───────────────────
                
                OracleDataAdapter adaptador = new OracleDataAdapter(cmd);
                DataSet registros = new DataSet();
                adaptador.Fill(registros, "mgi_sub_aplicaciones");
                comboBox_sub_aplicacion.DataSource = registros;
                comboBox_sub_aplicacion.DisplayMember = "mgi_sub_aplicaciones.desc_sub_aplicacion";
                comboBox_sub_aplicacion.ValueMember = "dcs_wf_decisiones.codigo_sub_aplicacion";

                comboBox_sub_aplicacion_SelectionChangeCommitted(null, null);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en llenar combo_decisiones : " + ex.Message);
            }
        }
        private void p_obtener_moneda_sub_aplicacion(int p_codigo_sub_aplicacion)
        {
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string vl_sql = @"select m.codigo_moneda,m.desc_moneda 
                                    from mgi_sub_aplicaciones sa,
                                         mgi_monedas m
                                   Where codigo_empresa=1          
                                     and sa.codigo_moneda=m.codigo_moneda
                                     and sa.codigo_sub_aplicacion=:pa_codigo_sub_aplicacion";
                OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd2.CommandType = CommandType.Text;
                //───────────────────
                OracleParameter pa_prametro1 = new OracleParameter("pa_codigo_sub_aplicacion", OracleType.Int32);
                cmd2.Parameters.Add(pa_prametro1);
                pa_prametro1.Direction = ParameterDirection.Input;
                pa_prametro1.Value = p_codigo_sub_aplicacion;

                //───────────────────                
                OracleDataReader dr = cmd2.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    txtcodigo_moneda.Text=dr["codigo_moneda"].ToString();
                    txtDesc_moneda.Text = dr["desc_moneda"].ToString();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_obtener_moneda_sub_aplicacion : " + ex.Message);
            }

        }
        private void p_obtener_sub_aplicacion_workflow(int pa_workflow_id)
        {
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string vl_sql = @"select sa.codigo_sub_aplicacion,
                                         sa.desc_sub_aplicacion,
                                         sa.codigo_moneda,
                                         m.desc_moneda,
                                         m.abreviatura,
                                         w.nombre_workflow,
                                         w.otros_fondos 
                                    from mgi_sub_aplicaciones sa,
                                         mgi_monedas m,
                                         dcs_workflows w
                                   Where sa.codigo_empresa=1
                                     and sa.codigo_sub_aplicacion=w.codigo_sub_aplicacion
                                     and sa.codigo_moneda=m.codigo_moneda
                                     and w.workflow_id=:pa_workflow_id";
                OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd2.CommandType = CommandType.Text;
                //───────────────────
                OracleParameter pa_prametro1 = new OracleParameter("pa_workflow_id", OracleType.Int32);
                cmd2.Parameters.Add(pa_prametro1);
                pa_prametro1.Direction = ParameterDirection.Input;
                pa_prametro1.Value = pa_workflow_id;

                //───────────────────                
                OracleDataReader dr = cmd2.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    string vl_sigla_moenda = dr["abreviatura"].ToString();
                    txtCodigo_sub_aplicacion.Text = dr["codigo_sub_aplicacion"].ToString();
                    labelProducto.Text = "Producto (" + txtCodigo_sub_aplicacion.Text.Trim() + ") :";
                    comboBox_sub_aplicacion.Text  = dr["desc_sub_aplicacion"].ToString();
                    txtcodigo_moneda.Text         = dr["codigo_moneda"].ToString();
                    labelMoneda.Text = "Moneda (" + vl_sigla_moenda + ") :";
                    txtDesc_moneda.Text           = dr["desc_moneda"].ToString();
                    if (dr["otros_fondos"].ToString().ToUpper() == "N")
                    {
                        txtCodigo_fuente_financiamiento.Text = "1";
                        labelFondos.Text = "Fondos ("+txtCodigo_fuente_financiamiento.Text+") :";                        
                        comboBox_fuentes_financiamiento.Text = "Fondos Propios Lps";
                        comboBox_fuentes_financiamiento.Enabled = false;
                    }
                    else
                    {
                        comboBox_fuentes_financiamiento.Enabled = true;
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_obtener_moneda_sub_aplicacion : " + ex.Message);
            }
        }
        private void p_llenar_combo_Fuentes_financiamiento()
        {
            try
            {
                OracleCommand cmd = new OracleCommand("select codigo_fuente,initcap(descripcion_fuente) descripcion_fuente from dcs_wf_fuentes_financiamiento where activo='S'", DocSys.connOracle);
                cmd.CommandType = CommandType.Text;
                //───────────────────

                OracleDataAdapter adaptador = new OracleDataAdapter(cmd);
                DataSet registros = new DataSet();
                adaptador.Fill(registros, "dcs_wf_fuentes_financiamiento");
                                               

                comboBox_fuentes_financiamiento.DataSource = registros;
                comboBox_fuentes_financiamiento.DisplayMember = "dcs_wf_fuentes_financiamiento.descripcion_fuente";
                comboBox_fuentes_financiamiento.ValueMember = "dcs_wf_fuentes_financiamiento.codigo_fuente";
                comboBox_fuentes_financiamiento_SelectionChangeCommitted(null, null);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en llenar combo_decisiones : " + ex.Message);
            }
        }        
        private void p_get_datos_cliente(int p_codigo_cliente)
        {
            
            
            
            /*string xmlData = ws_wfc.ws_obtener_datos_cliente(p_codigo_cliente);;

            DataSet dsXML = new DataSet();
            StringReader xmlSR = new System.IO.StringReader(xmlData);
            dsXML.ReadXml(xmlSR, XmlReadMode.InferSchema);


            txtNombre.Text           = dsXML.Tables[0].Rows[0]["nombres"].ToString();
            txtPrimer_apellido.Text  = dsXML.Tables[0].Rows[0]["primer_apellido"].ToString();
            txtSegundo_apellido.Text = dsXML.Tables[0].Rows[0]["segundo_apellido"].ToString();
            txtSexo.Text = dsXML.Tables[0].Rows[0]["sexo"].ToString();
            return;*/


            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string vl_sql = "select nombres,primer_apellido,segundo_apellido,decode(sexo,'M','Masculino','F','Femenino','') sexo,apellido_de_casada from mgi_clientes where codigo_cliente=:pa_codigo_cliente";
                OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd2.CommandType = CommandType.Text;
                //───────────────────
                OracleParameter pa_prametro1 = new OracleParameter("pa_codigo_cliente", OracleType.Int32);
                cmd2.Parameters.Add(pa_prametro1);
                pa_prametro1.Direction = ParameterDirection.Input;
                pa_prametro1.Value = p_codigo_cliente;
                //───────────────────                
                OracleDataReader dr = cmd2.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    txtNombre.Text          = dr["nombres"].ToString();
                    txtPrimer_apellido.Text = dr["primer_apellido"].ToString();
                    txtSegundo_apellido.Text= dr["segundo_apellido"].ToString();                    
                    txtApellido_casada.Text = dr["apellido_de_casada"].ToString();
                    txtSexo.Text = dr["sexo"].ToString();                    
                    txtNo_solicitud_formulario.Focus();
                    linkLabel_solhist.Visible = true;
                }
                else
                {
                    linkLabel_solhist.Visible = false;
                    MessageBox.Show("Cliente no encontrado ..!", "::DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_get_datos_solicitante :" + ex.Message + " " + ex.Source, "::DocSis", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private int p_verificar_existe_no_solicitud_formu(int p_no_solicitud_formu)
        {
            int vl_return = 0;
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string vl_sql = "select count(*) cant from dcs_solicitudes where no_solicitud_formulario=:pa_no_solicitud_formulario";
                OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd2.CommandType = CommandType.Text;
                //───────────────────
                OracleParameter pa_no_solicitud_formulario = new OracleParameter("pa_no_solicitud_formulario", OracleType.Int32);
                cmd2.Parameters.Add(pa_no_solicitud_formulario);
                pa_no_solicitud_formulario.Direction = ParameterDirection.Input;
                pa_no_solicitud_formulario.Value = p_no_solicitud_formu;
                //───────────────────                
                OracleDataReader dr = cmd2.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    vl_return = int.Parse(dr["cant"].ToString());
                }
                else
                {
                    vl_return = 0;
                }
                return vl_return;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_verificar_existe_no_solicitud_formu :" + ex.Message + " " + ex.Source, "::DocSis", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return vl_return;
            }
        }


        #region Eventos de los objetos
        private void comboBox_sub_aplicacion_Validated(object sender, EventArgs e)
        {
            comboBox_sub_aplicacion_SelectedIndexChanged(null, null);
        }
        private void comboBox_sub_aplicacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCodigo_sub_aplicacion.Text = comboBox_sub_aplicacion.SelectedValue.ToString();
        }
        private void txtCodigo_cliente_Leave(object sender, EventArgs e)
        {
            if (txtCodigo_cliente.Text != "")
            {
                p_get_datos_cliente(int.Parse(txtCodigo_cliente.Text));
            }
        }
        private void Button_guardar_Click(object sender, EventArgs e)
        {
            if (txtCodigo_cliente.Text == "")
            {
                MessageBox.Show("Debe indicar el código del cliente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (txtCodigo_sub_aplicacion.Text == "")
            {
                MessageBox.Show("Debe indicar el producto a colocar !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //--
            int vl_existe_no_solicitud_formu = 0;
            int vl_no_solicitud_formulario = 0;

            if (vl_valida_existe_no_solicitud == "S")
            {
                int.TryParse(txtNo_solicitud_formulario.Text, out vl_no_solicitud_formulario);
                if (vl_no_solicitud_formulario == 0)
                {
                    MessageBox.Show("Debe indicar el no. de solicitud del formulario de solicitud", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                vl_existe_no_solicitud_formu = p_verificar_existe_no_solicitud_formu(vl_no_solicitud_formulario);
                if (vl_existe_no_solicitud_formu > 0)
                {
                    MessageBox.Show("No de solicitud del formulario ya esta ingresado ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            //--

            try
            {
                decimal.Parse(txtMonto_solicitado.Text);
            }
            catch
            {
                MessageBox.Show("Debe indicar un monto valido !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (decimal.Parse(txtMonto_solicitado.Text) <= 0 | decimal.Parse(txtMonto_solicitado.Text) > 10000000)
            {
                MessageBox.Show("Debe indicar un monto mayor que cero o un monto valido !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                decimal.Parse(txtPlazo.Text);
            }
            catch
            {
                MessageBox.Show("Debe indicar un plazo valido !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (decimal.Parse(txtPlazo.Text) <= 0 | decimal.Parse(txtPlazo.Text) > 400)
            {
                MessageBox.Show("Debe indicar un plazo en meses mayor que cero o un plazo valido !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            //fin de validaciones
            if (DialogResult.Yes == MessageBox.Show("Desea guardar la solicitud ingresada  ?", "Aviso ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                p_insertar_solicitud();
            }

        }
        private void Button_salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button_cons_clientes_Click(object sender, EventArgs e)
        {
            s_consclientes forma = new s_consclientes();
            DialogResult res = forma.ShowDialog();           
            
            if (res == DialogResult.OK)
            {
                txtCodigo_cliente.Text = forma.txtCodigo_cliente.Text;
                txtCodigo_cliente_Leave(null, null);
  
            }
            
        }
        private void comboBox_sub_aplicacion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int vl_codigo_sub_aplicacion=0;            
            txtCodigo_sub_aplicacion.Text = comboBox_sub_aplicacion.SelectedValue.ToString();            
            int.TryParse(txtCodigo_sub_aplicacion.Text,out vl_codigo_sub_aplicacion);
            p_obtener_moneda_sub_aplicacion(vl_codigo_sub_aplicacion);
        }
        private void comboBox_fuentes_financiamiento_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtCodigo_fuente_financiamiento.Text=comboBox_fuentes_financiamiento.SelectedValue.ToString();
        }
        private void txtMonto_solicitado_Leave(object sender, EventArgs e)
        {
            try
            {                
                txtMonto_solicitado.Text = String.Format("{0:###,###,##0.00}", float.Parse(txtMonto_solicitado.Text.ToString()));

            }
            catch
            {
                MessageBox.Show("El monto solicitado no tiene un formato valido..!", "DocSis", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }
        private void txtCodigo_cliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCodigo_cliente_Leave(null, null);
            }
        }
        private void comboBox_sub_aplicacion_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
        #endregion

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int vl_codigo_cliente = 0;
            int.TryParse(txtCodigo_cliente.Text, out vl_codigo_cliente);
            if (vl_codigo_cliente == 0)
                return;
            
            s_solicitud_conshist forma = new s_solicitud_conshist(vl_codigo_cliente,txtNombre.Text);
            forma.ShowDialog();
        }




    }
}
