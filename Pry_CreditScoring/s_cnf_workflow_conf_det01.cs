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
	public partial class s_cnf_workflow_conf_det01 : Form
	{
		public int get_set_flujo_id { get; set; }
		public string get_set_no_paso { get; set; }
		public string get_set_descripcion_flujo { get; set; }
		public string get_set_paso_to { get; set; }
		public int get_set_decision_id { get; set; }
		private const string tituloAgregar = " :::Agregar Ruta:::"; //felvir01
		private const string tituloModificar = " :::Modificar Ruta:::"; //felvir01
		private const string tituloEliminar = " :::Eliminar Ruta:::"; //felvir01

		int vl_workflow_id = 0;
		int vl_decision_id = 0;
		int vl_flujo_id = 0;
		string vl_tipo_respuesta = "";
		string vl_accion = "";
		int vl_estacion_id_from = 0;
		int vl_estacion_id_to = 0;
		string v_tipo_flujo = string.Empty; //felvir01

		public s_cnf_workflow_conf_det01(string pa_accion, int pa_workflow_id, int pa_flujo_id, string pa_tipo_flujo)
		{
			InitializeComponent();
			vl_workflow_id = pa_workflow_id;
			vl_accion = pa_accion;
			vl_flujo_id = pa_flujo_id;
			this.v_tipo_flujo = pa_tipo_flujo;
		}

		private void s_workflow_conf_det01_Load(object sender, EventArgs e)
		{
			if (vl_accion == "INS")
			{
				this.rbtSolicitud.Checked = true;
				this.rbtExcepcion.Checked = false;

				p_llenar_combo_decisiones();
				p_llenar_combo_estaciones_de();
				p_llenar_combo_estaciones_para();

				label_Titulo.Text = tituloAgregar + " SOLICITUD";
				textBox_no_paso.Focus();
				button1.Text = "Guardar";
			}

			if (vl_accion == "MODIF")
			{
				this.rbtExcepcion.Visible = false;
				this.rbtSolicitud.Visible = false;

				if (this.v_tipo_flujo == u_Globales.flujoSol)
				{
					p_llenar_combo_decisiones();
					p_llenar_combo_estaciones_de();
					p_llenar_combo_estaciones_para();
					p_datos_ruta();
					label_Titulo.Text = tituloModificar + " SOLICITUD";
				}
				else
				{
					this.p_llenar_combo_decisiones_Excepcion();
					this.p_llenar_combo_estaciones_de_excep();
					this.p_llenar_combo_estaciones_para_Excep();
					this.p_info_ruta_excepcion();
					label_Titulo.Text = tituloModificar + " EXCEPCIÓN"; //felvir01
				}
				
				button1.Focus();
				button1.Text = "Modificar";
			}

			if (vl_accion == "ELIM")
			{
				this.rbtExcepcion.Visible = false;
				this.rbtSolicitud.Visible = false;

				if (this.v_tipo_flujo == u_Globales.flujoSol)
				{
					label_Titulo.Text = tituloEliminar + " SOLICITUD";
					p_datos_ruta();
				}
				else
				{
					label_Titulo.Text = tituloEliminar + " EXCEPCIÓN";
					this.p_info_ruta_excepcion();
				}

				textBox_no_paso.ReadOnly = true;
				ComboBox_decision.Enabled = false;
				radioButton_afirmativa.Enabled = false;
				radioButton_negativa.Enabled = false;
				textBox_descripcion_flujo.ReadOnly = true;
				comboBox_de.Enabled = false;
				comboBox_para.Enabled = false;
				textBox_paso_to.ReadOnly = true;
				button1.Focus();
				button1.Text = "Eliminar";
			}


			label_flujoid.Text = vl_flujo_id.ToString();
			StatusLabel_wf.Text = DocSys.p_obtener_nombre_workflow(vl_workflow_id);
		}


		private void p_datos_ruta()
		{
			string vl_sql = @"select * from dcs_wf_flujos where workflow_id=:pa_workflow_id and flujo_id=:pa_flujo_id";
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
			pa_workflow_id.Value = vl_workflow_id;
			//───────────────────
			OracleParameter pa_flujo_id = new OracleParameter("pa_flujo_id", OracleType.Int32);
			cmd2.Parameters.Add(pa_flujo_id);
			pa_flujo_id.Direction = ParameterDirection.Input;
			pa_flujo_id.Value = vl_flujo_id;
			//───────────────────

			OracleDataReader dr = cmd2.ExecuteReader();
			dr.Read();
			if (dr.HasRows)
			{
				textBox_no_paso.Text = dr["paso"].ToString();
				ComboBox_decision.SelectedValue = int.Parse(dr["decision_id"].ToString());
				ComboBox_decision_SelectionChangeCommitted(null, null);

				string vl_tipo_respuesta = dr["tipo_respuesta"].ToString();
				if (vl_tipo_respuesta == "T")
				{
					radioButton_afirmativa.Checked = true;
					radioButton_negativa.Checked = false;
				}
				if (vl_tipo_respuesta == "F")
				{
					radioButton_afirmativa.Checked = false;
					radioButton_negativa.Checked = true;
				}
				textBox_descripcion_flujo.Text = dr["descripcion_flujo"].ToString();
				comboBox_de.SelectedValue = int.Parse(dr["estacion_id_from"].ToString());
				ComboBox_estaciones_de_SelectionChangeCommitted(null, null);

				vl_estacion_id_from = int.Parse(dr["estacion_id_from"].ToString());
				comboBox_para.SelectedValue = int.Parse(dr["estacion_id_to"].ToString());
				ComboBox_estaciones_para_SelectionChangeCommitted(null, null);

				vl_estacion_id_to = int.Parse(dr["estacion_id_to"].ToString());
				textBox_paso_to.Text = dr["paso_to"].ToString();
			}

			dr.Close();
		}

		private void p_llenar_combo_decisiones()
		{
			try
			{
				string vl_query = @"select * from dcs_wf_decisiones where activo='S'";
				OracleCommand cmd = new OracleCommand(vl_query, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;
				//───────────────────


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

		private void p_llenar_combo_estaciones_de()
		{
			try
			{
				//felvir01: la última condición evita que traiga las estaciones de las excepciones
				string vl_query = @"select * from dcs_wf_estaciones where activo='S' and estacion_id < 9000";
				OracleCommand cmd = new OracleCommand(vl_query, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;
				//───────────────────

				OracleDataAdapter adaptador = new OracleDataAdapter(cmd);
				DataSet registros = new DataSet();
				adaptador.Fill(registros, "dcs_wf_estaciones");
				comboBox_de.DataSource = registros;
				comboBox_de.DisplayMember = "dcs_wf_estaciones.nombre";
				comboBox_de.ValueMember = "dcs_wf_decisiones.estacion_id";
				ComboBox_estaciones_de_SelectionChangeCommitted(null, null);

			}
			catch (Exception ex)
			{
				MessageBox.Show("Error en llenar combo_decisiones : " + ex.Message);
			}
		}

		private void p_llenar_combo_estaciones_para()
		{
			try
			{
				//felvir01: la última condición evita que traiga las estaciones de las excepciones
				string vl_query = @"select * from dcs_wf_estaciones where activo='S' and estacion_id <9000";
				OracleCommand cmd = new OracleCommand(vl_query, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;
				//───────────────────

				OracleDataAdapter adaptador = new OracleDataAdapter(cmd);
				DataSet registros = new DataSet();
				adaptador.Fill(registros, "dcs_wf_estaciones");
				comboBox_para.DataSource = registros;
				comboBox_para.DisplayMember = "dcs_wf_estaciones.nombre";
				comboBox_para.ValueMember = "dcs_wf_decisiones.estacion_id";
				ComboBox_estaciones_para_SelectionChangeCommitted(null, null);

			}
			catch (Exception ex)
			{
				MessageBox.Show("Error en llenar combo_decisiones : " + ex.Message);
			}
		}

		private void ComboBox_decision_SelectionChangeCommitted(object sender, EventArgs e)
		{

			label_decision.Text = "Decisión (" + ComboBox_decision.SelectedValue.ToString() + ") :";
			vl_decision_id = int.Parse(ComboBox_decision.SelectedValue.ToString());
		}

		private void ComboBox_estaciones_de_SelectionChangeCommitted(object sender, EventArgs e)
		{
			label_de.Text = "De (:" + comboBox_de.SelectedValue.ToString() + ") :";
			vl_estacion_id_from = int.Parse(comboBox_de.SelectedValue.ToString());
		}

		private void ComboBox_estaciones_para_SelectionChangeCommitted(object sender, EventArgs e)
		{
			label_para.Text = "Para (:" + comboBox_para.SelectedValue.ToString() + ") :";
			vl_estacion_id_to = int.Parse(comboBox_para.SelectedValue.ToString());
		}

		private void button_cerrar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (vl_accion == "INS")
			{
				if (vl_decision_id == 0)
				{
					MessageBox.Show("Debe indicar el tipo de decision ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}
				if (vl_estacion_id_from == 0)
				{
					MessageBox.Show("Debe indicar la estación origen ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}
				if (vl_estacion_id_to == 0)
				{
					MessageBox.Show("Debe indicar la estación destino ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}
				if (textBox_descripcion_flujo.Text == string.Empty)
				{
					MessageBox.Show("Debe indicar la descripcion del flujo ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}
				if (textBox_paso_to.Text == string.Empty)
				{
					MessageBox.Show("Debe indicar el paso de destino, si es un nuevo paso debe ser el paso inmediato superior al que esta ingresado, o ingresar un paso existe al que volvera el proceso", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}
			}
			if (vl_accion == "ELIM")
			{
				if (textBox_no_paso.Text == "1")
				{
					MessageBox.Show("El paso no. 1 no puede ser eliminado", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}
				if (DialogResult.No == MessageBox.Show("Desea eliminar ruta  ?", DocSys.vl_mensaje_avisos, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
					return;
				}
			}

			if (radioButton_afirmativa.Checked)
				vl_tipo_respuesta = "T";
			if (radioButton_negativa.Checked)
				vl_tipo_respuesta = "F";

			//felvir01
			if (!this.rbtSolicitud.Visible)
			{
				if(this.v_tipo_flujo == u_Globales.flujoSol)
				{
					p_insertar_flujo();
				}
				else
				{
					//código para excepciones
					this.p_mant_flujo_excepciones();
				}
			}
			else
			{
				if (this.rbtSolicitud.Checked)
				{
					p_insertar_flujo();
				}
				else
				{
					//código para excepciones
					this.p_mant_flujo_excepciones();
				}
			}
		}

		private void p_insertar_flujo()
		{
			try
			{
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}
				string vl_sql = "DCS_P_INSERTAR_FLUJO";
				OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
				cmd.CommandType = CommandType.StoredProcedure;
				//───────────────────
				OracleParameter pa_accion = new OracleParameter("pa_accion", OracleType.VarChar, 10);
				cmd.Parameters.Add(pa_accion);
				pa_accion.Direction = ParameterDirection.Input;
				pa_accion.Value = vl_accion;
				//───────────────────
				OracleParameter pa_flujo_id = new OracleParameter("pa_flujo_id", OracleType.Int32);
				cmd.Parameters.Add(pa_flujo_id);
				pa_flujo_id.Direction = ParameterDirection.Input;
				pa_flujo_id.Value = vl_flujo_id;
				//───────────────────
				OracleParameter pa_workflow_id = new OracleParameter("pa_workflow_id", OracleType.Int32);
				cmd.Parameters.Add(pa_workflow_id);
				pa_workflow_id.Direction = ParameterDirection.Input;
				pa_workflow_id.Value = vl_workflow_id;
				//───────────────────
				OracleParameter pa_decision_id = new OracleParameter("pa_decision_id", OracleType.Number);
				cmd.Parameters.Add(pa_decision_id);
				pa_decision_id.Direction = ParameterDirection.Input;
				pa_decision_id.Value = vl_decision_id;
				//───────────────────
				OracleParameter pa_tipo_respuesta = new OracleParameter("pa_tipo_respuesta", OracleType.VarChar);
				cmd.Parameters.Add(pa_tipo_respuesta);
				pa_tipo_respuesta.Direction = ParameterDirection.Input;
				pa_tipo_respuesta.Value = vl_tipo_respuesta;
				//───────────────────
				OracleParameter pa_descripcion_flujo = new OracleParameter("pa_descripcion_flujo", OracleType.VarChar, 120);
				cmd.Parameters.Add(pa_descripcion_flujo);
				pa_descripcion_flujo.Direction = ParameterDirection.Input;
				pa_descripcion_flujo.Value = textBox_descripcion_flujo.Text;
				//───────────────────
				OracleParameter pa_paso = new OracleParameter("pa_paso", OracleType.Number);
				cmd.Parameters.Add(pa_paso);
				pa_paso.Direction = ParameterDirection.Input;
				pa_paso.Value = textBox_no_paso.Text;
				//───────────────────
				OracleParameter pa_estacion_id_from = new OracleParameter("pa_estacion_id_from", OracleType.Number);
				cmd.Parameters.Add(pa_estacion_id_from);
				pa_estacion_id_from.Direction = ParameterDirection.Input;
				pa_estacion_id_from.Value = vl_estacion_id_from;
				//───────────────────
				OracleParameter pa_estacion_id_to = new OracleParameter("pa_estacion_id_to", OracleType.Number);
				cmd.Parameters.Add(pa_estacion_id_to);
				pa_estacion_id_to.Direction = ParameterDirection.Input;
				pa_estacion_id_to.Value = vl_estacion_id_to;
				//───────────────────
				OracleParameter pa_paso_to = new OracleParameter("pa_paso_to", OracleType.Number);
				cmd.Parameters.Add(pa_paso_to);
				pa_paso_to.Direction = ParameterDirection.Input;
				pa_paso_to.Value = textBox_paso_to.Text;
				cmd.ExecuteReader();

				if (vl_accion == "INS")
				{
					MessageBox.Show("Ruta ingresado satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}

				if (vl_accion == "MODIF")
				{
					MessageBox.Show("Ruta modificado satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}

				if (vl_accion == "ELIM")
				{
					MessageBox.Show("Ruta eliminada satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}

				this.Close();
				this.DialogResult = DialogResult.OK;
				return;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error en p_insertar_flujo :" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
		}

		//felvir01
		#region Excepciones

		private void rbtSolicitud_CheckedChanged(object sender, EventArgs e)
		{
			if (this.rbtSolicitud.Checked)
			{
				this.p_llenar_combo_decisiones();
				this.p_llenar_combo_estaciones_de();
				this.p_llenar_combo_estaciones_para();
				this.label_Titulo.Text = tituloAgregar + " SOLICITUD";
			}
		}

		private void rbtExcepcion_CheckedChanged(object sender, EventArgs e)
		{
			if (this.rbtExcepcion.Checked)
			{
				this.p_llenar_combo_decisiones_Excepcion();
				this.p_llenar_combo_estaciones_de_excep();
				this.p_llenar_combo_estaciones_para_Excep();
				this.label_Titulo.Text = tituloAgregar + " EXCEPCIÓN";
			}
		}

		private void p_llenar_combo_decisiones_Excepcion()
		{
			try
			{
				string vl_query = @"select * from excp.dcs_exc_tipo_decisiones where activo='S'";
				OracleCommand cmd = new OracleCommand(vl_query, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;
				//───────────────────


				OracleDataAdapter adaptador = new OracleDataAdapter(cmd);
				DataSet registrosExce = new DataSet();
				adaptador.Fill(registrosExce, "dcs_exc_tipo_decisiones");
				ComboBox_decision.DataSource = registrosExce;
				ComboBox_decision.DisplayMember = "dcs_exc_tipo_decisiones.descripcion";
				ComboBox_decision.ValueMember = "dcs_exc_tipo_decisiones.decision_id";
				ComboBox_decision_SelectionChangeCommitted(null, null);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error en llenar combo_decisiones excepción : " + ex.Message);
			}
		}

		private void p_llenar_combo_estaciones_de_excep()
		{
			try
			{
				string vl_query = @"select * from excp.dcs_v_estaciones where activo='S'";
				OracleCommand cmd = new OracleCommand(vl_query, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;
				//───────────────────

				OracleDataAdapter adaptador = new OracleDataAdapter(cmd);
				DataSet registros = new DataSet();
				adaptador.Fill(registros, "dcs_v_estaciones");
				comboBox_de.DataSource = registros;
				comboBox_de.DisplayMember = "dcs_v_estaciones.nombre";
				comboBox_de.ValueMember = "dcs_v_estaciones.estacion_id";
				ComboBox_estaciones_de_SelectionChangeCommitted(null, null);

			}
			catch (Exception ex)
			{
				MessageBox.Show("Error en llenar combo_decisiones : " + ex.Message);
			}
		}

		private void p_llenar_combo_estaciones_para_Excep()
		{
			try
			{
				string vl_query = @"select * from excp.dcs_v_estaciones where activo='S'";
				OracleCommand cmd = new OracleCommand(vl_query, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;
				//───────────────────

				OracleDataAdapter adaptador = new OracleDataAdapter(cmd);
				DataSet registros = new DataSet();
				adaptador.Fill(registros, "dcs_v_estaciones");
				comboBox_para.DataSource = registros;
				comboBox_para.DisplayMember = "dcs_v_estaciones.nombre";
				comboBox_para.ValueMember = "dcs_v_estaciones.estacion_id";
				ComboBox_estaciones_para_SelectionChangeCommitted(null, null);

			}
			catch (Exception ex)
			{
				MessageBox.Show("Error en llenar combo_decisiones : " + ex.Message);
			}
		}

		private void p_info_ruta_excepcion()
		{
			string vl_sql = @"select * from excp.dcs_exc_flujos where workflow_id=:pa_workflow_id and flujo_id=:pa_flujo_id";
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
			pa_workflow_id.Value = vl_workflow_id;
			//───────────────────
			OracleParameter pa_flujo_id = new OracleParameter("pa_flujo_id", OracleType.Int32);
			cmd2.Parameters.Add(pa_flujo_id);
			pa_flujo_id.Direction = ParameterDirection.Input;
			pa_flujo_id.Value = vl_flujo_id;
			//───────────────────

			OracleDataReader dr = cmd2.ExecuteReader();
			dr.Read();
			if (dr.HasRows)
			{
				textBox_no_paso.Text = dr["paso"].ToString();
				ComboBox_decision.SelectedValue = int.Parse(dr["decision_id"].ToString());
				ComboBox_decision_SelectionChangeCommitted(null, null);

				string vl_tipo_respuesta = dr["tipo_respuesta"].ToString();
				if (vl_tipo_respuesta == "T")
				{
					radioButton_afirmativa.Checked = true;
					radioButton_negativa.Checked = false;
				}
				if (vl_tipo_respuesta == "F")
				{
					radioButton_afirmativa.Checked = false;
					radioButton_negativa.Checked = true;
				}
				textBox_descripcion_flujo.Text = dr["descripcion_flujo"].ToString();
				comboBox_de.SelectedValue = int.Parse(dr["estacion_id_from"].ToString());
				ComboBox_estaciones_de_SelectionChangeCommitted(null, null);

				vl_estacion_id_from = int.Parse(dr["estacion_id_from"].ToString());
				comboBox_para.SelectedValue = int.Parse(dr["estacion_id_to"].ToString());
				ComboBox_estaciones_para_SelectionChangeCommitted(null, null);

				vl_estacion_id_to = int.Parse(dr["estacion_id_to"].ToString());
				textBox_paso_to.Text = dr["paso_to"].ToString();
			}

			dr.Close();
		}

		private void p_mant_flujo_excepciones()
		{
			try
			{
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}
				
				string vl_sql = "excp.DCS_P_MANT_FLUJOS";
				OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
				cmd.CommandType = CommandType.StoredProcedure;

				//───────────────────Flujo ID
				OracleParameter pa_flujo_id = new OracleParameter("PA_FLUJO_ID", OracleType.Int32);
				cmd.Parameters.Add(pa_flujo_id);
				pa_flujo_id.Direction = ParameterDirection.Input;
				pa_flujo_id.Value = vl_flujo_id;

				//───────────────────WorkflowId
				OracleParameter pa_workflow_id = new OracleParameter("PA_WORKFLOW_ID", OracleType.Int32);
				cmd.Parameters.Add(pa_workflow_id);
				pa_workflow_id.Direction = ParameterDirection.Input;
				pa_workflow_id.Value = vl_workflow_id;

				//───────────────────DecisionID
				OracleParameter pa_decision_id = new OracleParameter("PA_DECISION_ID", OracleType.Number);
				cmd.Parameters.Add(pa_decision_id);
				pa_decision_id.Direction = ParameterDirection.Input;
				pa_decision_id.Value = vl_decision_id;

				//───────────────────Tipo respuesta
				OracleParameter pa_tipo_respuesta = new OracleParameter("PA_TIPO_RESPUESTA", OracleType.VarChar);
				cmd.Parameters.Add(pa_tipo_respuesta);
				pa_tipo_respuesta.Direction = ParameterDirection.Input;
				pa_tipo_respuesta.Value = vl_tipo_respuesta;

				//───────────────────Descripcion flujo
				OracleParameter pa_descripcion_flujo = new OracleParameter("PA_DESCRIPCION_FLUJO", OracleType.VarChar, 120);
				cmd.Parameters.Add(pa_descripcion_flujo);
				pa_descripcion_flujo.Direction = ParameterDirection.Input;
				pa_descripcion_flujo.Value = textBox_descripcion_flujo.Text;

				//───────────────────Paso
				OracleParameter pa_paso = new OracleParameter("PA_PASO", OracleType.Number);
				cmd.Parameters.Add(pa_paso);
				pa_paso.Direction = ParameterDirection.Input;
				pa_paso.Value = textBox_no_paso.Text;

				//───────────────────Estación Id from
				OracleParameter pa_estacion_id_from = new OracleParameter("PA_ESTACION_ID_FROM", OracleType.Number);
				cmd.Parameters.Add(pa_estacion_id_from);
				pa_estacion_id_from.Direction = ParameterDirection.Input;
				pa_estacion_id_from.Value = vl_estacion_id_from;

				//───────────────────Estacion Id to
				OracleParameter pa_estacion_id_to = new OracleParameter("PA_ESTACION_ID_TO", OracleType.Number);
				cmd.Parameters.Add(pa_estacion_id_to);
				pa_estacion_id_to.Direction = ParameterDirection.Input;
				pa_estacion_id_to.Value = vl_estacion_id_to;

				//───────────────────paso to
				OracleParameter pa_paso_to = new OracleParameter("PA_PASO_TO", OracleType.Number);
				cmd.Parameters.Add(pa_paso_to);
				pa_paso_to.Direction = ParameterDirection.Input;
				pa_paso_to.Value = textBox_paso_to.Text;				

				//───────────────────Acción
				OracleParameter pa_accion = new OracleParameter("PA_ACCION", OracleType.VarChar, 10);
				cmd.Parameters.Add(pa_accion);
				pa_accion.Direction = ParameterDirection.Input;
				pa_accion.Value = vl_accion;
				cmd.ExecuteReader();

				if (vl_accion == "INS")
				{
					MessageBox.Show("Ruta ingresada satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				if (vl_accion == "MODIF")
				{
					MessageBox.Show("Ruta modificada satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				if (vl_accion == "ELIM")
				{
					MessageBox.Show("Ruta eliminada satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				this.Close();
				this.DialogResult = DialogResult.OK;
				return;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error en dcs_p_mant_flujos :" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
		}

		#endregion

		private void ComboBox_decision_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}
}
