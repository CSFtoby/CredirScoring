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
    public partial class s_solicitud_conshist : Form
    {
        public DataAccess da;
        int vl_codigo_cliente = 0;
        string vl_nombres = "";
        string vl_apellidos = "";
        public s_solicitud_conshist(int p_codcliente, string p_nombre)
        {
            InitializeComponent();
            vl_codigo_cliente = p_codcliente;
            vl_nombres = p_nombre;
        }
        private void s_solicitud_conshist_Load(object sender, EventArgs e)
        {
            
            txtCodigo_cliente.Text=vl_codigo_cliente.ToString();
            txtNombre.Text=vl_nombres;
            txtPrimer_apellido.Text = vl_apellidos;

            this.Text = "::: Solicitudes Históricas del Afiliado " + vl_nombres;
            p_llenar_grid_solichist(vl_codigo_cliente);
        }

        private void p_llenar_grid_solichist(int p_codig_cliente)
        {
            try
            {
                string vl_sql;
                vl_sql = @"Select no_solicitud,
                                  abierta,
                                  descripcion estado,
                                  fecha_presentacion,
                                  nombre_agencia,
                                  oficial_servicio,
                                  sa.desc_sub_aplicacion producto,
                                  monto_solicitado,
                                  meses_plazo 
                             From dcs_solicitudes s,
                                  dcs_workflows w,
                                  mgi_agencias a, 
                                  mgi_sub_aplicaciones sa,
                                  dcs_wf_estado_solicitudes e
                            Where sa.codigo_empresa=1
                              and a.codigo_empresa=1
                              and w.codigo_sub_aplicacion=sa.codigo_sub_aplicacion   
                              and s.workflow_id=w.workflow_id
                              and s.codigo_agencia_origen=a.codigo_agencia
                              and s.estado_solicitud_id=e.estado_id      
                              and s.codigo_cliente=:pa_codigo_cliente
                              and no_solicitud>=0 ";
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd2.CommandType = CommandType.Text;

                //───────────────────
                OracleParameter pa_param1 = new OracleParameter("pa_codigo_cliente", OracleType.Int32);
                cmd2.Parameters.Add(pa_param1);
                pa_param1.Direction = ParameterDirection.Input;
                pa_param1.Value = p_codig_cliente;
                //───────────────────

                OracleDataReader dr = cmd2.ExecuteReader();

                DataTable table = new DataTable();
                table.Columns.Add("no_solicitud");
                table.Columns.Add("abierta");
                table.Columns.Add("estado");
                table.Columns.Add("fecha_presentacion");
                table.Columns.Add("nombre_agencia");
                table.Columns.Add("oficial_servicio");
                table.Columns.Add("desc_sub_aplicacion");
                table.Columns.Add("monto_solicitado");
                table.Columns.Add("meses_plazo");
                
                while (dr.Read())
                {
                    table.Rows.Add(dr["no_solicitud"].ToString(),
                                   dr["abierta"].ToString(),
                                   dr["estado"].ToString(),
                                   dr["fecha_presentacion"].ToString(),
                                   dr["nombre_agencia"].ToString(),
                                   dr["oficial_servicio"].ToString(),
                                   dr["producto"].ToString(),                                   
                                   dr["monto_solicitado"].ToString(),
                                   dr["meses_plazo"].ToString());
                }
                gvSolicHist.DataSource = table;
                gvSolicHist.Refresh();
                table.Dispose();
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int vl_no_solicitud = 0;
            int vl_no_movimiento = 0;
            if (gvSolicHist.RowCount > 0)
            {

                DataGridViewRow row = gvSolicHist.CurrentRow;
                vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());
                
                s_solicitud_detalle forma = new s_solicitud_detalle(vl_no_solicitud, vl_no_movimiento);
                forma.da = da;
                forma.ShowDialog();

            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            s_consclientes forma = new s_consclientes();
            DialogResult res = forma.ShowDialog();

            if (res == DialogResult.OK)
            {
                txtCodigo_cliente.Text = forma.txtCodigo_cliente.Text;
                txtCodigo_cliente_Leave(null, null);

            }
        }

        private void p_get_datos_cliente(int p_codigo_cliente)
        {
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string vl_sql = "select InitCap(nombres) Nombres,initcap(trim(primer_apellido)||' '||trim(segundo_apellido)) apellidos,decode(sexo,'M','Masculino','F','Femenino','') sexo,apellido_de_casada from mgi_clientes where codigo_cliente=:pa_codigo_cliente";
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
                    txtNombre.Text = dr["nombres"].ToString();
                    txtPrimer_apellido.Text = dr["apellidos"].ToString().Trim();
                    p_llenar_grid_solichist(p_codigo_cliente);

                }
                else
                {
                    txtNombre.Text = "";
                    txtPrimer_apellido.Text = "";
                    p_llenar_grid_solichist(0);
                    MessageBox.Show("Cliente no encontrado ..!", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_get_datos_solicitante :" + ex.Message + " " + ex.Source, "::DocSis", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void txtCodigo_cliente_Leave(object sender, EventArgs e)
        {
            int vl_codigo_cliente = 0;
            int.TryParse(txtCodigo_cliente.Text, out vl_codigo_cliente);
            p_get_datos_cliente(vl_codigo_cliente);
        }

        private void txtCodigo_cliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCodigo_cliente_Leave(null, null);
                gvSolicHist.Focus();
            }
        }

        private void gvSolicHist_DoubleClick(object sender, EventArgs e)
        {
            int vl_no_solicitud = 0;
            int vl_no_movimiento = 0;
            if (gvSolicHist.RowCount > 0)
            {
                DataGridViewRow row = gvSolicHist.CurrentRow;
                vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());
                
                s_solicitud_detalle forma = new s_solicitud_detalle(vl_no_solicitud, 0);
                forma.ShowDialog();

            }
        }

        
    }
}
