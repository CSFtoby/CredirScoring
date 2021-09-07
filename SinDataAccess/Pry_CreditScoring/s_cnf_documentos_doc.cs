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
    public partial class s_cnf_documentos_doc : Form
    {
        string vl_accion = "";
        int vl_documento_id = 0;

        public s_cnf_documentos_doc(string pa_accion, int pa_documento_id)
        {
            InitializeComponent();
            vl_documento_id = pa_documento_id;
            vl_accion = pa_accion;
        }

        private void s_cnf_documentos_doc_Load(object sender, EventArgs e)
        {
            if (vl_accion == "INS")
            {
                label_Titulo.Text = " :::Agregar Tipo Documento:::";
                txtDescripcion.Focus();
                button1.Text = "Guardar";
            }

            if (vl_accion == "MODIF")
            {
                label_Titulo.Text = " :::Modificar Tipo Documento:::";
                p_datos_documento();
                txtDocumento_id.ReadOnly = true;
                txtDescripcion.Focus();
                button1.Focus();
                button1.Text = "Modificar";
            }

            if (vl_accion == "ELIM")
            {
                label_Titulo.Text = " :::Eliminar Tipo de Documento:::";
                p_datos_documento();
                txtDocumento_id.ReadOnly = true;
                txtDescripcion.ReadOnly = true;
                txtSigla_doc.ReadOnly = true;
                radioButton_activo_no.Enabled = false;
                radioButton_activo_si.Enabled = false;
                rbPersona1.Enabled = false;
                rbPersona2.Enabled = false;
                rbPersona3.Enabled = false;
                rbPersona4.Enabled = false;
                button1.Focus();
                button1.Text = "Eliminar";
            }
        }

        private void p_datos_documento()
        {

            try
            {
                string vl_sql = @"select * from dcs_wf_tipo_documentos where documento_id=:pa_documento_id";
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd2.CommandType = CommandType.Text;
                //───────────────────
                OracleParameter pa_documento_id = new OracleParameter("pa_documento_id", OracleType.Int32);
                cmd2.Parameters.Add(pa_documento_id);
                pa_documento_id.Direction = ParameterDirection.Input;
                pa_documento_id.Value = vl_documento_id;
                //───────────────────            
                OracleDataReader dr = cmd2.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    txtDocumento_id.Text = dr["documento_id"].ToString();
                    txtDescripcion.Text = dr["descripcion"].ToString();
                    txtSigla_doc.Text = dr["sigla_doc"].ToString();
                    string vl_activo = dr["activo"].ToString();
                    string vl_persona = dr["persona_solicitud"].ToString();
                    if (vl_activo == "S")
                    {
                        radioButton_activo_si.Checked = true;
                        radioButton_activo_no.Checked = false;
                    }
                    if (vl_activo == "N")
                    {
                        radioButton_activo_si.Checked = false;
                        radioButton_activo_no.Checked = true;
                    }
                    if (vl_persona == "P1")
                        rbPersona1.Checked = true;
                    if (vl_persona == "P2")
                        rbPersona2.Checked = true;
                    if (vl_persona == "P3")
                        rbPersona3.Checked = true;
                    if (vl_persona == "P4")
                        rbPersona4.Checked = true;

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            p_abm_documentos();
        }

        private void p_abm_documentos()
        {
            string vl_activo = "N";
            string vl_ver_toda_filial = "N";
            string vl_crear_solicitudes = "N";
            string vl_error_code = "";

            if (radioButton_activo_si.Checked)
                vl_activo = "S";

            string vl_persona = "P1";
            if (rbPersona1.Checked)
                vl_persona = "P1";
            if (rbPersona2.Checked)
                vl_persona = "P2";
            if (rbPersona3.Checked)
                vl_persona = "P3";
            if (rbPersona4.Checked)
                vl_persona = "P4";
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string vl_sql = "dcs_p_abm_documentos";
                OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd.CommandType = CommandType.StoredProcedure;
                //───────────────────                
                OracleParameter pa_accion = new OracleParameter("pa_accion", OracleType.VarChar, 10);
                cmd.Parameters.Add(pa_accion);
                pa_accion.Direction = ParameterDirection.Input;
                pa_accion.Value = vl_accion;
                //───────────────────
                OracleParameter pa_persona = new OracleParameter("pa_persona", OracleType.VarChar, 2);
                cmd.Parameters.Add(pa_persona);
                pa_persona.Direction = ParameterDirection.Input;
                pa_persona.Value = vl_persona;
                
                //───────────────────
                OracleParameter pa_documento_id = new OracleParameter("pa_documento_id", OracleType.Int32);
                cmd.Parameters.Add(pa_documento_id);
                pa_documento_id.Direction = ParameterDirection.InputOutput;
                pa_documento_id.Value = txtDocumento_id.Text;

                //if (vl_accion != "INS")
                //    pa_documento_id.Value = int.Parse(txtDocumento_id.Text);

                //───────────────────
                OracleParameter pa_descripcion = new OracleParameter("pa_descripcion", OracleType.VarChar, 100);
                cmd.Parameters.Add(pa_descripcion);
                pa_descripcion.Direction = ParameterDirection.Input;
                pa_descripcion.Value = txtDescripcion.Text;
                //───────────────────
                OracleParameter pa_sigla_doc = new OracleParameter("pa_sigla_doc", OracleType.VarChar, 10);
                cmd.Parameters.Add(pa_sigla_doc);
                pa_sigla_doc.Direction = ParameterDirection.Input;
                pa_sigla_doc.Value = txtSigla_doc.Text;
                //----------------------------------
                OracleParameter pa_activo = new OracleParameter("pa_activo", OracleType.VarChar, 1);
                cmd.Parameters.Add(pa_activo);
                pa_activo.Direction = ParameterDirection.Input;
                pa_activo.Value = vl_activo;
                //───────────────────
                cmd.ExecuteReader();


                if (vl_accion == "INS")
                {
                    txtDocumento_id.Text = pa_documento_id.Value.ToString();
                    MessageBox.Show("Tipo de documento ingresado satisfactoriamente ! No. " + pa_documento_id.Value.ToString(), "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (vl_accion == "MODIF")
                {
                    MessageBox.Show("Tipo de documento modificado satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (vl_accion == "ELIM")
                {
                    MessageBox.Show("Tipo de documento eliminado satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.Close();
                this.DialogResult = DialogResult.OK;
                return;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + " ", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
    }
}
