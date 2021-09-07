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
    public partial class s_cnf_documentos : Form
    {
        int vl_record_grid = 0;
        DataAccess da;

        public s_cnf_documentos()
        {
            InitializeComponent();
        }

        private void s_cnf_documentos_Load(object sender, EventArgs e)
        {
            p_llenar_grid_documentos();
        }

        private void p_llenar_grid_documentos()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string sql = @"Select documento_id,descripcion,sigla_doc,
                                  decode(nvl(persona_solicitud,'P1'),
                                        'P1','Solicitante',
                                        'P2','Codeudor',
                                        'P3','Aval 1',
                                        'P4','Aval 2', 
                                        'P5', 'Representante Legal 1', 
                                        'P6', 'Representante Legal 2', 
                                        'P7', 'Representante Legal 3') persona_solicitud,
                                  Activo
                            From dcs_wf_tipo_documentos
                            Order by 1";
                OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
                cmd.CommandType = CommandType.Text;
                //────────────────────────                
                //---------------
                OracleDataReader dr = cmd.ExecuteReader();
                DataTable mitable = new DataTable();
                mitable.Columns.Add("foto", System.Type.GetType("System.Byte[]"));
                mitable.Columns.Add("documento_id");
                mitable.Columns.Add("descripcion");
                mitable.Columns.Add("sigla_doc");
                mitable.Columns.Add("persona_solicitud");
                mitable.Columns.Add("activo");                                

                while (dr.Read())
                {
                    mitable.Rows.Add(null,
                                     dr["documento_id"].ToString(),
                                     dr["descripcion"].ToString(),
                                     dr["sigla_doc"].ToString(),
                                     dr["persona_solicitud"].ToString(),
                                     dr["activo"].ToString());
                }

                gvDocumentos.DataSource = mitable;
                gvDocumentos.Refresh();
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;
        }

        private void pbRefrescar_Click(object sender, EventArgs e)
        {
            p_get_record();
            p_llenar_grid_documentos();
            p_go_record();
        }
        private void p_go_record()
        {
            if (gvDocumentos.RowCount > 0) //Tiene que tener registros
            {
                if (gvDocumentos.RowCount > vl_record_grid)
                {
                    gvDocumentos.CurrentCell = this.gvDocumentos[1, vl_record_grid];
                }
                else
                {
                    if (gvDocumentos.RowCount == vl_record_grid)
                    {
                        gvDocumentos.CurrentCell = this.gvDocumentos[1, gvDocumentos.RowCount - 1];
                    }
                    if (gvDocumentos.RowCount == 0)
                    {

                    }
                }
            }
        }
        private void p_get_record()
        {
            DataGridViewRow row = gvDocumentos.CurrentRow;
            if (gvDocumentos.Rows.IndexOf(row) >= 0)
                vl_record_grid = gvDocumentos.Rows.IndexOf(row);
            else
                vl_record_grid = 0;
            Label_currentrow.Text = vl_record_grid.ToString() + "/" + gvDocumentos.RowCount.ToString();
        }

        private void button_cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_adicionar_Click(object sender, EventArgs e)
        {
            if (obtenerAdmin(DocSys.vl_user) > 0)
            {
                p_get_record();
                s_cnf_documentos_doc forma = new s_cnf_documentos_doc("INS", 0);
                DialogResult resul = forma.ShowDialog();
                if (resul == DialogResult.OK)
                {
                    p_llenar_grid_documentos();
                    p_go_record();
                }
            }
            else {
                MessageBox.Show("No tiene permisos");
            }
        }

        private void button_modificar_Click(object sender, EventArgs e)
        {
            if (obtenerAdmin(DocSys.vl_user) > 0)
            {
                p_get_record();
                DataGridViewRow row = gvDocumentos.CurrentRow;
                if (row != null)
                {
                    int vl_documento_id = int.Parse(row.Cells["documento_id"].Value.ToString());                
                    s_cnf_documentos_doc forma = new s_cnf_documentos_doc("MODIF", vl_documento_id);
                    DialogResult resul = forma.ShowDialog();
                    if (resul == DialogResult.OK)
                    {
                        p_llenar_grid_documentos();                    
                        p_go_record();
                    }
                }
            }
            else
            {
                MessageBox.Show("No tiene permisos");
            }
        }

        private void button_eliminar_Click(object sender, EventArgs e)
        {
            if (obtenerAdmin(DocSys.vl_user) > 0)
            {
                p_get_record();
                DataGridViewRow row = gvDocumentos.CurrentRow;
                if (row != null)
                {
                    int vl_documento_id = int.Parse(row.Cells["documento_id"].Value.ToString());
                    s_cnf_documentos_doc forma = new s_cnf_documentos_doc("ELIM", vl_documento_id);
                    DialogResult resul = forma.ShowDialog();
                    if (resul == DialogResult.OK)
                    {
                        p_llenar_grid_documentos();
                        p_go_record();
                    }
                }
            }
            else
            {
                MessageBox.Show("No tiene permisos");
            }
        }

        public int obtenerAdmin(string user) {
            int vl_return = 0;
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string sql = @"select count(*) hay
                                 From dcs_wf_usuarios_admon
                                Where codigo_usuario = :pa_usuario
                                  and activo_b = 'S'";
                OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
                cmd.CommandType = CommandType.Text;
                //────────────────────────────────────────────────────────────
                OracleParameter pa_usuario = new OracleParameter("pa_usuario", OracleType.VarChar, 30);
                cmd.Parameters.Add(pa_usuario);
                pa_usuario.Direction = ParameterDirection.Input;
                pa_usuario.Value = user;

                OracleDataReader dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    vl_return = int.Parse(dr["hay"].ToString());
                }
                else
                {
                    MessageBox.Show("Usuario no tiene definida una estación en CreditScoring.", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    vl_return = 0;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return vl_return;
        }
    }
}
