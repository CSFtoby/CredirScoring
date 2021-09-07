using Docsis_Application.Excepciones;
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
    public partial class s_cnf_decisiones : Form
    {
        int vl_record_grid = 0;
        public s_cnf_decisiones()
        {
            InitializeComponent();
        }

        private void s_decisiones_doc_Load(object sender, EventArgs e)
        {
            p_llenar_grid_decisiones();
			this.p_llenar_grid_decisiones_Excepciones(); //felvir01
        }

        private void p_llenar_grid_decisiones()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string sql = @"select d.decision_id,d.descripcion,d.activo,d.estado_solicitud_id_to,e.descripcion desc_estado 
                                 from dcs_wf_decisiones d,
                                      dcs_wf_estado_solicitudes e
                                Where d.estado_solicitud_id_to=e.estado_id 
                                order by 1";
                OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
                cmd.CommandType = CommandType.Text;
                //────────────────────────                
                //---------------
                OracleDataReader dr = cmd.ExecuteReader();
                DataTable mitable = new DataTable();
                mitable.Columns.Add("foto", System.Type.GetType("System.Byte[]"));
                mitable.Columns.Add("decision_id");
                mitable.Columns.Add("descripcion");
                mitable.Columns.Add("activo");
                mitable.Columns.Add("estado_solicitud_id_to");
                mitable.Columns.Add("desc_estado");
                
                while (dr.Read())
                {
                    mitable.Rows.Add(null,
                                     dr["decision_id"].ToString(),
                                     dr["descripcion"].ToString(),
                                     dr["activo"].ToString(),
                                     dr["estado_solicitud_id_to"].ToString(),
                                     dr["desc_estado"].ToString());
                }

                gvDecisiones.DataSource = mitable;
                gvDecisiones.Refresh();
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;
        }

        private void button_cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_adicionar_Click(object sender, EventArgs e)
        {
            p_get_record();
            s_cnf_decisiones_doc forma = new s_cnf_decisiones_doc("INS", 0, u_Globales.flujoSol);
            DialogResult resul = forma.ShowDialog();
            if (resul == DialogResult.OK)
            {
                p_llenar_grid_decisiones();
                p_go_record();
            }
        }

        private void button_modificar_Click(object sender, EventArgs e)
        {
			if(this.tabConf.SelectedTab == this.tabConf.TabPages["tpSolicitud"]) //felvir01
			{
				p_get_record();
				DataGridViewRow row = gvDecisiones.CurrentRow;
				if (row != null)
				{
					int vl_decision_id = int.Parse(row.Cells["decision_id"].Value.ToString());
					s_cnf_decisiones_doc forma = new s_cnf_decisiones_doc("MODIF", vl_decision_id, u_Globales.flujoSol);
					DialogResult resul = forma.ShowDialog();
					if (resul == DialogResult.OK)
					{
						p_llenar_grid_decisiones();
						p_go_record();
					}
				}
			}
			else
			{
				//Excepciones
				this.p_get_record_excep();
				DataGridViewRow row = this.dgvDecExcep.CurrentRow;
				if (row != null)
				{
					int vl_decision_id = int.Parse(row.Cells["decision_id_exc"].Value.ToString());
					s_cnf_decisiones_doc forma = new s_cnf_decisiones_doc("MODIF", vl_decision_id, u_Globales.flujoExc);
					DialogResult resul = forma.ShowDialog();
					if (resul == DialogResult.OK)
					{
						this.p_llenar_grid_decisiones_Excepciones();
						this.p_go_record_excep();
					}
				}
			}
        }

        private void button_eliminar_Click(object sender, EventArgs e)
        {
			if (this.tabConf.SelectedTab == this.tabConf.TabPages["tpSolicitud"]) //felvir01
			{
				p_get_record();
				DataGridViewRow row = gvDecisiones.CurrentRow;
				if (row != null)
				{
					int vl_decision_id = int.Parse(row.Cells["decision_id"].Value.ToString());
					s_cnf_decisiones_doc forma = new s_cnf_decisiones_doc("ELIM", vl_decision_id, u_Globales.flujoSol);
					DialogResult resul = forma.ShowDialog();
					if (resul == DialogResult.OK)
					{
						p_llenar_grid_decisiones();
						p_go_record();
					}
				}
			}
			else
			{
				this.p_get_record_excep();
				DataGridViewRow row = this.dgvDecExcep.CurrentRow;
				if (row != null)
				{
					int vl_decision_id = int.Parse(row.Cells["decision_id_exc"].Value.ToString());
					s_cnf_decisiones_doc forma = new s_cnf_decisiones_doc("ELIM", vl_decision_id, u_Globales.flujoExc);
					DialogResult resul = forma.ShowDialog();
					if (resul == DialogResult.OK)
					{
						this.p_llenar_grid_decisiones_Excepciones();
						this.p_go_record_excep();
					}
				}
			}
        }    

        private void pbRefrescar_Click(object sender, EventArgs e)
        {
            p_get_record();
            p_llenar_grid_decisiones();
            p_go_record();

			//felvir01
			this.p_get_record_excep();
			this.p_llenar_grid_decisiones_Excepciones();
			this.p_go_record_excep();
        }

        private void p_go_record()
        {
            if (gvDecisiones.RowCount > 0) //Tiene que tener registros
            {
                if (gvDecisiones.RowCount > vl_record_grid)
                {
                    gvDecisiones.CurrentCell = this.gvDecisiones[1, vl_record_grid];
                }
                else
                {
                    if (gvDecisiones.RowCount == vl_record_grid)
                    {
                        gvDecisiones.CurrentCell = this.gvDecisiones[1, gvDecisiones.RowCount - 1];
                    }
                    if (gvDecisiones.RowCount == 0)
                    {

                    }
                }
            }
        }

        private void p_get_record()
        {
            DataGridViewRow row = gvDecisiones.CurrentRow;
            if (gvDecisiones.Rows.IndexOf(row) >= 0)
                vl_record_grid = gvDecisiones.Rows.IndexOf(row);
            else
                vl_record_grid = 0;
            Label_currentrow.Text = vl_record_grid.ToString() + "/" + gvDecisiones.RowCount.ToString();
        }

		//felvir01
		#region Excepciones

		private void p_llenar_grid_decisiones_Excepciones()
		{
			this.Cursor = Cursors.WaitCursor;
			try
			{
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}
				string sql = @"select d.decision_id,d.descripcion,d.activo,d.ESTADO_EXCEP_ID,e.descripcion desc_estado 
                                 from excp.dcs_exc_tipo_decisiones d,
                                      excp.DCS_EXC_ESTADOS_EXCEPCION e
                                Where d.ESTADO_EXCEP_ID=e.ESTADO_EXCEP_ID 
                                order by 1";

				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;
				//────────────────────────                
				//---------------
				OracleDataReader dr = cmd.ExecuteReader();
				DataTable mitable = new DataTable();
				mitable.Columns.Add("foto", System.Type.GetType("System.Byte[]"));
				mitable.Columns.Add("decision_id");
				mitable.Columns.Add("descripcion");
				mitable.Columns.Add("activo");
				mitable.Columns.Add("ESTADO_EXCEP_ID");
				mitable.Columns.Add("desc_estado");

				while (dr.Read())
				{
					mitable.Rows.Add(null,
					dr["decision_id"].ToString(),
					dr["descripcion"].ToString(),
					dr["activo"].ToString(),
					dr["ESTADO_EXCEP_ID"].ToString(),
					dr["desc_estado"].ToString());
				}

				this.dgvDecExcep.DataSource = mitable;
				this.dgvDecExcep.Refresh();
				dr.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			this.Cursor = Cursors.Default;
		}

		private void p_go_record_excep()
		{
			if (this.dgvDecExcep.RowCount > 0) //Tiene que tener registros
			{
				if (this.dgvDecExcep.RowCount > vl_record_grid)
				{
					this.dgvDecExcep.CurrentCell = this.dgvDecExcep[0, vl_record_grid];
				}
				else
				{
					if (this.dgvDecExcep.RowCount == vl_record_grid)
					{
						this.dgvDecExcep.CurrentCell = this.dgvDecExcep[0, this.dgvDecExcep.RowCount - 1];
					}
					if (this.dgvDecExcep.RowCount == 0)
					{

					}
				}
			}
		}

		private void p_get_record_excep()
		{
			DataGridViewRow row = this.dgvDecExcep.CurrentRow;
			if (this.dgvDecExcep.Rows.IndexOf(row) >= 0)
				vl_record_grid = this.dgvDecExcep.Rows.IndexOf(row);
			else
				vl_record_grid = 0;
			Label_currentrow.Text = vl_record_grid.ToString() + "/" + this.dgvDecExcep.RowCount.ToString();
		}
		#endregion
	}
}
