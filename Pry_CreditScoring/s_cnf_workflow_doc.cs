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
    public partial class s_cnf_workflow_doc : Form
    {
        public string get_set_workflow_id { get; set; }
        public string get_set_nombre_workflow { get; set; }
        public string get_set_producto { get; set; }
        public string get_set_codigo_sub_aplicacion { get; set; }
        public string get_set_otros_fondos { get; set; }
        public string get_set_activo { get; set; }
        
        int vl_producto_id = 0;
        string vl_otros_fondos = "";
        string vl_activo = "";
        string vl_accion = "";

        public s_cnf_workflow_doc(string pa_accion)
        {
            InitializeComponent();
            vl_accion = pa_accion;
        }

        private void s_workflow_doc01_Load(object sender, EventArgs e)
        {
            p_llenar_combo_sub_aplicaciones();
            comboBox_sub_aplicacion_SelectionChangeCommitted(null, null);
            if (vl_accion == "INS")
            {
                this.Text = ":: Adicionando WorkFlow ::";
            }
            if (vl_accion == "MODIF")
            {
                this.Text = ":: Modificando WorkFlow ::";
                button_adicionar.Text = "Modificar";
                txtWorkflow_id.ReadOnly = true;
                txtNombre_workflow.ReadOnly = true;
                comboBox_sub_aplicacion.Enabled = false;
                txtNombre_workflow.Text       = get_set_nombre_workflow;
                txtCodigo_sub_aplicacion.Text = get_set_codigo_sub_aplicacion;
                txtWorkflow_id.Text           = get_set_workflow_id;
                txtCodigo_sub_aplicacion.Text = get_set_codigo_sub_aplicacion;
                comboBox_sub_aplicacion.SelectedValue=int.Parse(txtCodigo_sub_aplicacion.Text);
                if (get_set_activo == "S")
                {
                    radioButton_activo_si.Checked = true;
                    radioButton_activo_no.Checked = false;
                }
                else
                {
                    radioButton_activo_si.Checked = false;
                    radioButton_activo_no.Checked = true;
                }


                if (get_set_otros_fondos == "S")
                {
                    radioButton_otrosf_si.Checked = true;
                    radioButton_otrosf_no.Checked = false;
                }
                else
                {
                    radioButton_otrosf_si.Checked = false;
                    radioButton_otrosf_no.Checked = true;
                }
                radioButton_activo_si.Focus();
            }

            if (vl_accion == "ELIM")
            {
                this.Text = ":: Eliminando WorkFlow ::";
                button_adicionar.Text = "Eliminar";
                txtWorkflow_id.ReadOnly = true;
                txtNombre_workflow.ReadOnly = true;
                comboBox_sub_aplicacion.Enabled = false;
                radioButton_activo_si.Enabled = false;
                radioButton_activo_no.Enabled = false;
                radioButton_otrosf_si.Enabled = false;
                radioButton_otrosf_no.Enabled = false;
                txtNombre_workflow.Text = get_set_nombre_workflow;
                txtCodigo_sub_aplicacion.Text = get_set_codigo_sub_aplicacion;
                txtWorkflow_id.Text = get_set_workflow_id;
                txtCodigo_sub_aplicacion.Text = get_set_codigo_sub_aplicacion;
                comboBox_sub_aplicacion.SelectedValue = int.Parse(txtCodigo_sub_aplicacion.Text);
                if (get_set_activo == "S")
                {
                    radioButton_activo_si.Checked = true;
                    radioButton_activo_no.Checked = false;
                }
                else
                {
                    radioButton_activo_si.Checked = false;
                    radioButton_activo_no.Checked = true;
                }


                if (get_set_otros_fondos == "S")
                {
                    radioButton_otrosf_si.Checked = true;
                    radioButton_otrosf_no.Checked = false;
                }
                else
                {
                    radioButton_otrosf_si.Checked = false;
                    radioButton_otrosf_no.Checked = true;
                }
                button_adicionar.Focus();
            }   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void comboBox_sub_aplicacion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int vl_codigo_sub_aplicacion = 0;
            txtCodigo_sub_aplicacion.Text = comboBox_sub_aplicacion.SelectedValue.ToString();
            int.TryParse(txtCodigo_sub_aplicacion.Text, out vl_codigo_sub_aplicacion);            
        }

        private void button_adicionar_Click(object sender, EventArgs e)
        {
            if (txtNombre_workflow.Text==string.Empty)
            {
                MessageBox.Show("Debe indicar el nombre del WorkFlow..:", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtNombre_workflow.Text.Trim().Length < 5)
            {
                MessageBox.Show("Debe indicar el nombre del WorkFlow..:", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtCodigo_sub_aplicacion.Text == string.Empty)
            {
                MessageBox.Show("Debe indicar el producto del WorkFlow..:", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            vl_activo = "N";            
            vl_otros_fondos = "N";
            if (radioButton_activo_si.Checked)
                vl_activo = "S";

            if (radioButton_otrosf_si.Checked)
                vl_otros_fondos = "S";
                

            p_abm_workflow();
            
        }

        private void p_abm_workflow()
        {
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string vl_sql = "DCS_P_ABM_WORKFLOWS";
                OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd.CommandType = CommandType.StoredProcedure;
                //───────────────────
                OracleParameter pa_accion = new OracleParameter("pa_accion", OracleType.VarChar, 5);
                cmd.Parameters.Add(pa_accion);
                pa_accion.Direction = ParameterDirection.Input;
                pa_accion.Value = vl_accion;
                //───────────────────
                OracleParameter pa_workflow_id = new OracleParameter("pa_workflow_id", OracleType.Number);
                cmd.Parameters.Add(pa_workflow_id);
                pa_workflow_id.Direction = ParameterDirection.Input;
                pa_workflow_id.Value = int.Parse(txtWorkflow_id.Text);
                //───────────────────
                OracleParameter pa_tipo_respuesta = new OracleParameter("pa_descripcion", OracleType.VarChar,100);
                cmd.Parameters.Add(pa_tipo_respuesta);
                pa_tipo_respuesta.Direction = ParameterDirection.Input;
                pa_tipo_respuesta.Value = txtNombre_workflow.Text;
                //───────────────────
                OracleParameter pa_codigo_sub_aplicacion = new OracleParameter("pa_codigo_sub_aplicacion", OracleType.Number);
                cmd.Parameters.Add(pa_codigo_sub_aplicacion);
                pa_codigo_sub_aplicacion.Direction = ParameterDirection.Input;
                pa_codigo_sub_aplicacion.Value = txtCodigo_sub_aplicacion.Text;
                //───────────────────
                OracleParameter pa_otros_fondos = new OracleParameter("pa_otros_fondos", OracleType.VarChar,1);
                cmd.Parameters.Add(pa_otros_fondos);
                pa_otros_fondos.Direction = ParameterDirection.Input;
                pa_otros_fondos.Value = vl_otros_fondos;
                //───────────────────
                OracleParameter pa_activo = new OracleParameter("pa_activo", OracleType.VarChar, 1);
                cmd.Parameters.Add(pa_activo);
                pa_activo.Direction = ParameterDirection.Input;
                pa_activo.Value = vl_activo;
                //───────────────────
                
                cmd.ExecuteReader();
                if (vl_accion == "INS")
                {
                    MessageBox.Show("Workflow ingresado satisfactoriamente !", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (vl_accion == "MODIF")
                {
                    MessageBox.Show("Workflow modificado satisfactoriamente !", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Close();
                this.DialogResult = DialogResult.OK;
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_insertar_workflow() :" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
    }
}
