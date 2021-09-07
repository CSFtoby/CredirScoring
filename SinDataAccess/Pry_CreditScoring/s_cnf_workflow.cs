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
    public partial class s_cnf_workflow : Form
    {
        int vl_record_grid = 0;

        public s_cnf_workflow()
        {
            InitializeComponent();
        }
        private void s_workflow_doc_Load(object sender, EventArgs e)
        {
            p_llenar_grid_workflows();
        }

        private void p_llenar_grid_workflows()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string sql = @"Select w.workflow_id,nombre_workflow,w.codigo_sub_aplicacion,sa.desc_sub_aplicacion,otros_fondos,activo 
                                 From dcs_workflows w,
                                      mgi_sub_aplicaciones sa
                                Where sa.codigo_empresa=1
                                  and w.codigo_sub_aplicacion=sa.codigo_sub_aplicacion
                             Order by 1";
                OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
                cmd.CommandType = CommandType.Text;
                //────────────────────────                
                //---------------
                OracleDataReader dr = cmd.ExecuteReader();
                DataTable mitable = new DataTable();                
                mitable.Columns.Add("foto", System.Type.GetType("System.Byte[]"));
                mitable.Columns.Add("workflow_id");
                mitable.Columns.Add("nombre_workflows");
                mitable.Columns.Add("codigo_sub_aplicacion");
                mitable.Columns.Add("desc_sub_aplicacion");
                mitable.Columns.Add("otros_fondos");
                mitable.Columns.Add("activo");
                while (dr.Read())
                {
                    mitable.Rows.Add(null,
                                     dr["workflow_id"].ToString(),
                                     dr["nombre_workflow"].ToString(),
                                     dr["codigo_sub_aplicacion"].ToString(),
                                     dr["desc_sub_aplicacion"].ToString(),
                                     dr["otros_fondos"].ToString(),                                     
                                     dr["activo"].ToString());
                }
                gvWorkflows.DataSource = mitable;
                gvWorkflows.Refresh();
                dr.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;
        }

        private void button_adicionar_Click(object sender, EventArgs e)
        {
            s_cnf_workflow_doc forma = new s_cnf_workflow_doc("INS");
            DialogResult resul = forma.ShowDialog();
            if (resul == DialogResult.OK)
            {
                p_llenar_grid_workflows();                
            }
        }

        private void button_modificar_Click(object sender, EventArgs e)
        {
            p_get_record();            
            DataGridViewRow row = gvWorkflows.CurrentRow;
            if (row != null)
            {
                s_cnf_workflow_doc forma = new s_cnf_workflow_doc("MODIF");
                forma.get_set_workflow_id     = row.Cells["workflow_id"].Value.ToString();
                forma.get_set_nombre_workflow = row.Cells["nombre_workflows"].Value.ToString();
                forma.get_set_codigo_sub_aplicacion = row.Cells["codigo_sub_aplicacion"].Value.ToString();
                forma.get_set_otros_fondos = row.Cells["otros_fondos"].Value.ToString();
                forma.get_set_activo = row.Cells["activo"].Value.ToString();                
                DialogResult resul = forma.ShowDialog();
                if (resul == DialogResult.OK)
                {
                    p_llenar_grid_workflows();
                    p_go_record();
                }
            }
        }
       
        private void gvWorkflows_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            p_get_record();
            p_llenar_grid_workflows();
            p_go_record();                       
        }

        private void button_eliminar_Click(object sender, EventArgs e)
        {
            p_get_record();
            DataGridViewRow row = gvWorkflows.CurrentRow;
            if (row != null)
            {
                s_cnf_workflow_doc forma = new s_cnf_workflow_doc("ELIM");
                forma.get_set_workflow_id = row.Cells["workflow_id"].Value.ToString();
                forma.get_set_nombre_workflow = row.Cells["nombre_workflows"].Value.ToString();
                forma.get_set_codigo_sub_aplicacion = row.Cells["codigo_sub_aplicacion"].Value.ToString();
                forma.get_set_otros_fondos = row.Cells["otros_fondos"].Value.ToString();
                forma.get_set_activo = row.Cells["activo"].Value.ToString();
                DialogResult resul = forma.ShowDialog();
                if (resul == DialogResult.OK)
                {
                    p_llenar_grid_workflows();
                    p_go_record();
                }
            }
        }

        private void button_cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void p_go_record()
        {
            if (gvWorkflows.RowCount > 0) //Tiene que tener registros
            {
                if (gvWorkflows.RowCount > vl_record_grid)
                {
                    gvWorkflows.CurrentCell = this.gvWorkflows[1, vl_record_grid];
                }
                else
                {
                    if (gvWorkflows.RowCount == vl_record_grid)
                    {
                        gvWorkflows.CurrentCell = this.gvWorkflows[1, gvWorkflows.RowCount - 1];
                    }
                    if (gvWorkflows.RowCount == 0)
                    {

                    }
                }
            }
        }
        private void p_get_record()
        {
            DataGridViewRow row = gvWorkflows.CurrentRow;
            if (gvWorkflows.Rows.IndexOf(row) >= 0)
                vl_record_grid = gvWorkflows.Rows.IndexOf(row);
            else
                vl_record_grid = 0;
            Label_currentrow.Text = vl_record_grid.ToString() + "/" + gvWorkflows.RowCount.ToString();
        }
    }
       
}
