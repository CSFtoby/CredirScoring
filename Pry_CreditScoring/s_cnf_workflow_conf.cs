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
using wfcModel;

namespace Docsis_Application
{
	public partial class s_cnf_workflow_conf : Form
	{
		DataAccess da;
		int vl_workflow_id = 0;
		int vl_record_grid = 0;
		bool con_borde = MDI_Menu.con_borde;
        
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

		public s_cnf_workflow_conf(DataAccess da, int pa_workflow_id)
		{
			InitializeComponent();
			vl_workflow_id = pa_workflow_id;
			this.da = da;
		}

		private void s_workflow_conf_Load(object sender, EventArgs e)
		{
			p_llenar_combo_workflows();
			p_llenar_griview();
		}

		private void p_llenar_griview()
		{
			this.Cursor = Cursors.WaitCursor;
			try
			{
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}
				string sql = @"Select paso,flujo_id,0 decision_id,'Crear' Decision,'Afirmativa' tipo_decision,'Creacion del documento de seguimiento' descripcion_flujo,estacion_id_to,' ' de,e2.nombre Para,paso_to
                             from dcs_wf_flujos f,
                                  dcs_wf_estaciones e2         
                            Where f.estacion_id_to=e2.estacion_id
                              and f.workflow_id=:pa_workflow_id
                              and f.paso=1
                            UNION
                           Select paso,flujo_id,f.decision_id,d.descripcion Decision,Decode(tipo_respuesta,'T','Afirmativa','F','Negativa') tipo_Decision, f.descripcion_flujo,estacion_id_to,e1.nombre De, e2.nombre Para,paso_to
                             From dcs_wf_flujos f,
                                  dcs_wf_decisiones d, 
                                  dcs_wf_estaciones e1,
                                  dcs_wf_estaciones e2
                            Where f.workflow_id=:pa_workflow_id
                              and f.decision_id=d.decision_id
                              and f.estacion_id_from=e1.estacion_id
                              and f.estacion_id_to=e2.estacion_id   
                            Order by paso,flujo_id ";
				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;
				//────────────────────────
				OracleParameter pa_param1 = new OracleParameter("pa_workflow_id", OracleType.Int32);
				cmd.Parameters.Add(pa_param1);
				pa_param1.Direction = ParameterDirection.Input;
				pa_param1.Value = vl_workflow_id;
				//---------------
				OracleDataReader dr = cmd.ExecuteReader();
				DataTable mitable = new DataTable("clientes");

				mitable.Columns.Add("foto", System.Type.GetType("System.Byte[]"));
				mitable.Columns.Add("paso");
				mitable.Columns.Add("flujo_id");
				mitable.Columns.Add("Decision_id");
				mitable.Columns.Add("Decision");
				mitable.Columns.Add("tipo_Decision");
				mitable.Columns.Add("descripcion_flujo");
				mitable.Columns.Add("de");
				mitable.Columns.Add("para");
				mitable.Columns.Add("paso_to");

				while (dr.Read())
				{
					mitable.Rows.Add(null,
									 dr["paso"].ToString(),
									 dr["flujo_id"].ToString(),
									 dr["decision_id"].ToString(),
									 dr["decision"].ToString(),
									 dr["tipo_Decision"].ToString(),
									 dr["descripcion_flujo"].ToString(),
									 dr["de"].ToString(),
									 dr["para"].ToString(),
									 dr["paso_to"].ToString());
				}
				gvFlujos.DataSource = mitable;
				gvFlujos.Refresh();
				dr.Close();
                
				DataGridViewCell valueCell = null;
				DataGridViewImageCell displayCell = null;

				int vl_n = 0;
				for (int row = 0; row < gvFlujos.Rows.Count; row++)
				{
					if (checkBox_colorear.Checked)
					{
						if (gvFlujos.Rows[row].Cells["paso"].Value.ToString() == "1")
							gvFlujos.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(245, 168, 48);

						if (gvFlujos.Rows[row].Cells["paso"].Value.ToString() == "2")
							gvFlujos.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(145, 182, 66);

						if (gvFlujos.Rows[row].Cells["paso"].Value.ToString() == "3")
							gvFlujos.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(253, 251, 151);

						if (gvFlujos.Rows[row].Cells["paso"].Value.ToString() == "4")
							gvFlujos.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(54, 128, 223);

						if (gvFlujos.Rows[row].Cells["paso"].Value.ToString() == "5")
							gvFlujos.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(129, 206, 159);

						if (gvFlujos.Rows[row].Cells["paso"].Value.ToString() == "6")
							gvFlujos.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(224, 173, 58);

						if (gvFlujos.Rows[row].Cells["paso"].Value.ToString() == "7")
							gvFlujos.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(161, 164, 160);

						if (gvFlujos.Rows[row].Cells["paso"].Value.ToString() == "8")
							gvFlujos.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(245, 182, 238);

						if (gvFlujos.Rows[row].Cells["paso"].Value.ToString() == "9")
							gvFlujos.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(253, 251, 151);

						if (gvFlujos.Rows[row].Cells["paso"].Value.ToString() == "10")
							gvFlujos.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(253, 251, 151);
					}
					if (gvFlujos.Rows[row].Cells["tipo_decision"].Value.ToString() == "Afirmativa")
					{
						vl_n = 0;
					}
					else
					{
						vl_n = 2;
					}
					valueCell = gvFlujos[icono.Index, row];
					displayCell = (DataGridViewImageCell)gvFlujos[icono.Index, row];
					displayCell.Value = imageList.Images[vl_n];
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			this.Cursor = Cursors.Default;
		}
		private void p_llenar_combo_workflows()
		{
			try
			{
				DataSet dsCombo = new DataSet();
				dsCombo = DocSys.p_Obtener_un_dataset("select workflow_id,nombre_workflow from dcs_workflows where activo='S'", "dcs_workflows");
				ComboBox_wokflows.DataSource = dsCombo;
				ComboBox_wokflows.DisplayMember = "dcs_workflows.nombre_workflow";
				ComboBox_wokflows.ValueMember = "dcs_workflows.workflow_id";
				ComboBox_wokflows.SelectedValue = vl_workflow_id;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error en llenar combox Workflows : " + ex.Message);
			}
		}

		private void ComboBox_wokflows_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ComboBox_wokflows.Text != "")
			{
				try
				{
					vl_workflow_id = int.Parse(ComboBox_wokflows.SelectedValue.ToString());
					p_llenar_griview();
					this.p_llenarGridExcepciones();//felvir01
				}
				catch{}
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (vl_workflow_id <= 0)
			{
				MessageBox.Show("Debe seleccionar un workflow", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			p_get_record();
			s_cnf_workflow_conf_det01 forma = new s_cnf_workflow_conf_det01("INS", vl_workflow_id, 0, u_Globales.flujoSol);//felvir01
			DialogResult resul = forma.ShowDialog();
			if (resul == DialogResult.OK)
			{
				pictureBox3_Click(null, null);
				p_go_record();
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			//felvir01
			if (this.TabConf.SelectedTab == this.TabConf.TabPages["tpSolicitudes"])
			{
				p_get_record();
                DataGridViewRow row = gvFlujos.CurrentRow;
				if (row != null)
				{
					int vl_paso = int.Parse(row.Cells["paso"].Value.ToString());
					if (vl_paso == 1)
					{
						MessageBox.Show("El paso no. 1 no puede ser modificado ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
					int vl_flujo_id = int.Parse(row.Cells["flujo_id"].Value.ToString());
					s_cnf_workflow_conf_det01 forma = new s_cnf_workflow_conf_det01("MODIF", vl_workflow_id, vl_flujo_id, u_Globales.flujoSol);//felvir01
					DialogResult resul = forma.ShowDialog();
					if (resul == DialogResult.OK)
					{
						pictureBox3_Click(null, null);
						p_go_record();
					}
				}
			}
			else
            {
				//Excepciones
				this.p_get_record_excep();
				DataGridViewRow row = this.dgvFlujoExc.CurrentRow;
				if (row != null)
				{
					int vl_paso = int.Parse(row.Cells["dataGridViewTextBoxColumn1"].Value.ToString());
					if (vl_paso == 1)
					{
						MessageBox.Show("El paso no. 1 no puede ser modificado ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
					int vl_flujo_id = int.Parse(row.Cells["dataGridViewTextBoxColumn2"].Value.ToString());
					s_cnf_workflow_conf_det01 forma = new s_cnf_workflow_conf_det01("MODIF", vl_workflow_id, vl_flujo_id, u_Globales.flujoExc);
					DialogResult resul = forma.ShowDialog();
					if (resul == DialogResult.OK)
					{
						pictureBox3_Click(null, null);
						this.p_go_record_excep();
					}
				}
			}

		}

		private void button3_Click(object sender, EventArgs e)
		{
			//felvir01
			if (this.TabConf.SelectedTab == this.TabConf.TabPages["tpSolicitudes"])
			{
				p_get_record();
                DataGridViewRow row = gvFlujos.CurrentRow;
				if (row != null)
				{
					int vl_paso = int.Parse(row.Cells["paso"].Value.ToString());
					if (vl_paso == 1)
					{
						MessageBox.Show("El paso no. 1 no puede ser borrado ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
					int vl_flujo_id = int.Parse(row.Cells["flujo_id"].Value.ToString());
					s_cnf_workflow_conf_det01 forma = new s_cnf_workflow_conf_det01("ELIM", vl_workflow_id, vl_flujo_id, u_Globales.flujoSol);
					DialogResult resul = forma.ShowDialog();
					if (resul == DialogResult.OK)
					{
						pictureBox3_Click(null, null);
						p_go_record();
					}
				}
			}
			else
            {
                //excepciones
                this.p_get_record_excep();
				DataGridViewRow row = gvFlujos.CurrentRow;
				if (row != null)
				{
					int vl_paso = int.Parse(row.Cells["dataGridViewTextBoxColumn1"].Value.ToString());
					if (vl_paso == 1)
					{
						MessageBox.Show("El paso no. 1 no puede ser borrado ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
					int vl_flujo_id = int.Parse(row.Cells["dataGridViewTextBoxColumn2"].Value.ToString());
					s_cnf_workflow_conf_det01 forma = new s_cnf_workflow_conf_det01("ELIM", vl_workflow_id, vl_flujo_id, u_Globales.flujoSol);
					DialogResult resul = forma.ShowDialog();
					if (resul == DialogResult.OK)
					{
						pictureBox3_Click(null, null);
						this.p_go_record_excep();
					}
				}
			}

		}

		private void crearWorkflowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Opacity = .85;
			s_cnf_workflow forma = new s_cnf_workflow();
			forma.ShowDialog();
			this.Opacity = 100;
		}

		private void createDecisionesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Opacity = .85;
			s_cnf_decisiones forma = new s_cnf_decisiones();
			forma.ShowDialog();
			this.Opacity = 100;
		}

		private void estacionesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Opacity = .85;
			s_cnf_estaciones forma = new s_cnf_estaciones(da);
			forma.ShowDialog();
			this.Opacity = 100;
		}

		private void pictureBox2_Click(object sender, EventArgs e)
		{
			p_llenar_combo_workflows();
		}

		private void pictureBox3_Click(object sender, EventArgs e){}

		private void checkBox_colorear_CheckedChanged(object sender, EventArgs e)
		{
			//felvir01
			if (this.TabConf.SelectedTab == this.TabConf.TabPages["tpSolicitudes"])
			{
				p_get_record();
				p_llenar_griview();
				p_go_record();
			}
			else
			{
				this.p_get_record_excep();
				this.p_llenarGridExcepciones();
				this.p_go_record_excep();
			}
		}

		private void p_go_record()
		{
			if (gvFlujos.RowCount > 0) //Tiene que tener registros
			{
				if (gvFlujos.RowCount > vl_record_grid)
				{
					gvFlujos.CurrentCell = this.gvFlujos[1, vl_record_grid];
				}
				else
				{
					if (gvFlujos.RowCount == vl_record_grid)
					{
						gvFlujos.CurrentCell = this.gvFlujos[1, gvFlujos.RowCount - 1];
					}
					if (gvFlujos.RowCount == 0)
					{}
				}
			}
		}

		private void p_get_record()
		{
			DataGridViewRow row = gvFlujos.CurrentRow;
			if (gvFlujos.Rows.IndexOf(row) >= 0)
				vl_record_grid = gvFlujos.Rows.IndexOf(row);
			else
				vl_record_grid = 0;
			Label_currentrow.Text = vl_record_grid.ToString() + "/" + gvFlujos.RowCount.ToString();
		}

		private void moverForm()
		{
			ReleaseCapture();
			SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
		}

		private void adicionarRutaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			button1_Click(null, null);
		}

		private void modificaRutaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			button2_Click(null, null);
		}

		private void eliminarRutaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			button3_Click(null, null);
		}

		private void salirToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void usuariosPorEstacionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			s_cnf_workflow_user_estacion forma = new s_cnf_workflow_user_estacion(0);
			forma.ShowDialog();
		}

		private void tipoDeDocumentosToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Opacity = .85;
			s_cnf_documentos forma = new s_cnf_documentos();
			forma.ShowDialog();
			this.Opacity = 100;
		}

		private void documentosPorWorkflowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			s_cnf_workflow_doc_wf forma = new s_cnf_workflow_doc_wf();
			forma.da = da;
			forma.ShowDialog();
		}

		private void btnRefrescar_Click(object sender, EventArgs e)
		{
			//felvir01
			if (this.TabConf.SelectedTab == this.TabConf.TabPages["tpSolicitudes"])
			{
				p_get_record();
				p_llenar_griview();
				p_go_record();
			}
			else
			{
				//Excepciones
				this.p_get_record_excep();
				this.p_llenarGridExcepciones();
				this.p_go_record_excep();
			}
		}
        
		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void panel1_MouseDown(object sender, MouseEventArgs e)
		{
			moverForm();
		}

		private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
		{
			moverForm();
		}

		private void registroDeFirmasToolStripMenuItem_Click(object sender, EventArgs e)
		{
			s_cnf_firmas_miembros_doc forma = new s_cnf_firmas_miembros_doc();
			forma.da = da;
			forma.ShowDialog();
		}

		private void panel1_Paint(object sender, PaintEventArgs e){}

		#region Excepciones
		//felvir01
		private void p_llenarGridExcepciones()
		{
			this.Cursor = Cursors.WaitCursor;
			try
			{
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				string sql = @"Select paso,flujo_id,0 decision_id,'Crear' Decision,'Afirmativa' tipo_decision,'Creacion del documento de seguimiento' descripcion_flujo,estacion_id_to,' ' de,e2.nombre Para,paso_to
                             from excp.DCS_EXC_FLUJOS f,
                                  dcs_wf_estaciones e2         
                            Where f.estacion_id_to=e2.estacion_id
                              and f.workflow_id=:pa_workflow_id
                              and f.paso=1
                            UNION
                           Select paso,flujo_id,f.decision_id,d.descripcion Decision,Decode(TIPO_RESPUESTA,'T','Afirmativa','F','Negativa') tipo_Decision, f.descripcion_flujo,estacion_id_to,e1.nombre De, e2.nombre Para,paso_to
                             From excp.DCS_EXC_FLUJOS f,
                                  excp.dcs_exc_tipo_decisiones d, 
                                  dcs_wf_estaciones e1,
                                  dcs_wf_estaciones e2
                            Where f.workflow_id=:pa_workflow_id
                              and f.decision_id=d.decision_id
                              and f.estacion_id_from=e1.estacion_id
                              and f.estacion_id_to=e2.estacion_id   
                            Order by paso,flujo_id ";

				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;
				//────────────────────────
				OracleParameter pa_param1 = new OracleParameter("pa_workflow_id", OracleType.Int32);
				cmd.Parameters.Add(pa_param1);
				pa_param1.Direction = ParameterDirection.Input;
				pa_param1.Value = vl_workflow_id;
				//---------------
				OracleDataReader dr = cmd.ExecuteReader();
				DataTable mitable = new DataTable("flujosExcep");

				mitable.Columns.Add("foto", System.Type.GetType("System.Byte[]"));
				mitable.Columns.Add("paso");
				mitable.Columns.Add("flujo_id");
				mitable.Columns.Add("Decision_id");
				mitable.Columns.Add("Decision");
				mitable.Columns.Add("tipo_Decision");
				mitable.Columns.Add("descripcion_flujo");
				mitable.Columns.Add("de");
				mitable.Columns.Add("para");
				mitable.Columns.Add("paso_to");

				while (dr.Read())
				{
					mitable.Rows.Add(null,
					dr["paso"].ToString(),
					dr["flujo_id"].ToString(),
					dr["decision_id"].ToString(),
					dr["decision"].ToString(),
					dr["tipo_Decision"].ToString(),
					dr["descripcion_flujo"].ToString(),
					dr["de"].ToString(),
					dr["para"].ToString(),
					dr["paso_to"].ToString());
				}

				// lo asigna al datagridview
				this.dgvFlujoExc.DataSource = mitable;
				this.dgvFlujoExc.Refresh();
				dr.Close();

				DataGridViewCell valueCell = null;
				DataGridViewImageCell displayCell = null;

				int vl_n = 0;
				for (int row = 0; row < dgvFlujoExc.Rows.Count; row++)
				{
					if (checkBox_colorear.Checked)
					{
						if (dgvFlujoExc.Rows[row].Cells["paso"].Value.ToString() == "1")
							dgvFlujoExc.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(245, 168, 48);

						if (dgvFlujoExc.Rows[row].Cells["paso"].Value.ToString() == "2")
							dgvFlujoExc.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(145, 182, 66);

						if (dgvFlujoExc.Rows[row].Cells["paso"].Value.ToString() == "3")
							dgvFlujoExc.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(253, 251, 151);

						if (dgvFlujoExc.Rows[row].Cells["paso"].Value.ToString() == "4")
							dgvFlujoExc.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(54, 128, 223);

						if (dgvFlujoExc.Rows[row].Cells["paso"].Value.ToString() == "5")
							dgvFlujoExc.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(129, 206, 159);

						if (dgvFlujoExc.Rows[row].Cells["paso"].Value.ToString() == "6")
							dgvFlujoExc.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(224, 173, 58);

						if (dgvFlujoExc.Rows[row].Cells["paso"].Value.ToString() == "7")
							dgvFlujoExc.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(161, 164, 160);

						if (dgvFlujoExc.Rows[row].Cells["paso"].Value.ToString() == "8")
							dgvFlujoExc.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(245, 182, 238);

						if (dgvFlujoExc.Rows[row].Cells["paso"].Value.ToString() == "9")
							dgvFlujoExc.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(253, 251, 151);

						if (dgvFlujoExc.Rows[row].Cells["paso"].Value.ToString() == "10")
							dgvFlujoExc.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(253, 251, 151);
					}
					if (dgvFlujoExc.Rows[row].Cells["dataGridViewTextBoxColumn8"].Value.ToString() == "Afirmativa")
					{
						vl_n = 0;
					}
					else
					{
						vl_n = 2;
					}
					valueCell = dgvFlujoExc[icono.Index, row];
					displayCell = (DataGridViewImageCell)dgvFlujoExc[icono.Index, row];
					displayCell.Value = imageList.Images[vl_n];
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error:" + ex.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void p_go_record_excep()
		{
			if (dgvFlujoExc.RowCount > 0) //Tiene que tener registros
			{
				if (dgvFlujoExc.RowCount > vl_record_grid)
				{
					dgvFlujoExc.CurrentCell = this.dgvFlujoExc[1, vl_record_grid];
				}
				else
				{
					if (dgvFlujoExc.RowCount == vl_record_grid)
					{
						dgvFlujoExc.CurrentCell = this.dgvFlujoExc[1, dgvFlujoExc.RowCount - 1];
					}
					if (dgvFlujoExc.RowCount == 0)
					{

					}
				}
			}
		}

		private void p_get_record_excep()
		{
			DataGridViewRow row = dgvFlujoExc.CurrentRow;
			if (dgvFlujoExc.Rows.IndexOf(row) >= 0)
				vl_record_grid = dgvFlujoExc.Rows.IndexOf(row);
			else
				vl_record_grid = 0;
			Label_currentrow.Text = vl_record_grid.ToString() + "/" + dgvFlujoExc.RowCount.ToString();
		}
		#endregion

		private void excepcionesToolStripMenuItem_Click(object sender, EventArgs e){}

		private void lineamientosToolStripMenuItem_Click(object sender, EventArgs e)
		{
			e_cnf_lineamientos forma = new e_cnf_lineamientos();
			forma.ShowDialog();
		}

		private void excepcionesToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			e_cnf_excepciones nuevo = new e_cnf_excepciones();
			nuevo.ShowDialog();
		}

		private void reglasExcepcionesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			e_cnf_reglas_excepciones reglas = new e_cnf_reglas_excepciones();
			reglas.ShowDialog();
		}
        
        private void btnConsultarMiembros_Click(object sender, EventArgs e)
        {
            s_consulta_miembros conMiem = new s_consulta_miembros(da);
            conMiem.Show();
        }

        private void btn_AgregrarMiembro_Click_1(object sender, EventArgs e)
        {
            s_add_miembros addMiem = new s_add_miembros(da);
            addMiem.Show();
        }

        private void btn_tmp_adm_Click(object sender, EventArgs e){
            e_Add_Tmp addAdmin = new e_Add_Tmp();
            addAdmin.Show();
        }

        private void btnHorario_Click(object sender, EventArgs e)
        {
            e_Admin_Temp aAdmin = new e_Admin_Temp();
            aAdmin.Show();
        }
    }
}
