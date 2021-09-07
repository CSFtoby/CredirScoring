using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Docsis_Application
{
    public partial class s_solicitud_marcaerror_doc : Form
    {
        int vl_no_solicitud = 0;
        int vl_codigo_error = 0;

        public s_solicitud_marcaerror_doc(int p_no_solicitud)
        {
            InitializeComponent();
            vl_no_solicitud = p_no_solicitud;
        }
        private void s_solicitud_marcaerror_doc_Load(object sender, EventArgs e)
        {
            textBox_no_solicitud.Text = vl_no_solicitud.ToString();
            p_datos_solicitud();
            p_llenar_combo_estados();
            textBox_detalle_KeyPress(null, null);
        }
        private void p_datos_solicitud()
        {
            try
            {
                string vl_sql = @"Select no_solicitud,Initcap(nombres) nombres,
                                         Initcap(primer_apellido) primer_apellido,initcap(segundo_apellido) segundo_apellido
                                    From dcs_solicitudes s,mgi_clientes c
                                   Where c.codigo_empresa=1
                                     and s.codigo_cliente=c.codigo_cliente
                                     and no_solicitud=:pa_no_solicitud";
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
                    txtNombre.Text = dr["nombres"].ToString();
                    txtPrimer_apellido.Text = dr["primer_apellido"].ToString();
                    txtSegundo_apellido.Text = dr["segundo_apellido"].ToString();                    
                }
                dr.Close();
                textBox_detalle.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void p_llenar_combo_estados()
        {
            try
            {
                string vl_query = @"select * from dcs_wf_codigos_error where activo='S' order by 1";
                OracleCommand cmd = new OracleCommand(vl_query, DocSys.connOracle);
                cmd.CommandType = CommandType.Text;
                //───────────────────

                OracleDataAdapter adaptador = new OracleDataAdapter(cmd);
                DataSet registros = new DataSet();
                adaptador.Fill(registros, "dcs_wf_codigos_error");
                comboBox_codigos_error.DataSource = registros;
                comboBox_codigos_error.DisplayMember = "dcs_wf_codigos_error.descripcion";
                comboBox_codigos_error.ValueMember = "dcs_wf_codigos_error.cod_error";
                comboBox_codigos_error_SelectionChangeCommitted(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en llenar combo_decisiones : " + ex.Message);
            }
        }
        private void p_marca_con_error(int p_no_solicitud)
        {
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string vl_sql = "";
                vl_sql = vl_sql + "DCS_P_MARCAR_CON_ERROR";
                OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd.CommandType = CommandType.StoredProcedure;
                //───────────────────
                OracleParameter pa_cod_error = new OracleParameter("pa_cod_error", OracleType.Int32);
                cmd.Parameters.Add(pa_cod_error);
                pa_cod_error.Direction = ParameterDirection.Input;
                pa_cod_error.Value = vl_codigo_error;

                OracleParameter pa_no_solicitud = new OracleParameter("pa_no_solicitud", OracleType.Int32);
                cmd.Parameters.Add(pa_no_solicitud);
                pa_no_solicitud.Direction = ParameterDirection.Input;
                pa_no_solicitud.Value = p_no_solicitud;

                OracleParameter pa_no_movimiento = new OracleParameter("pa_motivo_error", OracleType.VarChar, 200);
                cmd.Parameters.Add(pa_no_movimiento);
                pa_no_movimiento.Direction = ParameterDirection.Input;
                pa_no_movimiento.Value = textBox_detalle.Text;

                //───────────────────
                cmd.ExecuteReader();
                this.DialogResult = DialogResult.OK;
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_marca_como_leido :" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void comboBox_codigos_error_SelectionChangeCommitted(object sender, EventArgs e)
        {
            vl_codigo_error = int.Parse(comboBox_codigos_error.SelectedValue.ToString());
        }
        private void button_cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void textBox_detalle_KeyPress(object sender, KeyPressEventArgs e)
        {
            label_caracteres.Text = textBox_detalle.Text.Length.ToString() + " caracteres de 200 permitidos";
        }
        private void textBox_detalle_TextChanged(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("Desea marcar la solicitud con error ?", DocSys.vl_mensaje_avisos, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                return;
            }
            p_marca_con_error(vl_no_solicitud);
        }
                       
    }
}
