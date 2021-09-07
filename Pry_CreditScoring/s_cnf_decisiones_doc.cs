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
    public partial class s_cnf_decisiones_doc : Form
    {
        string vl_accion="";
        int vl_decision_id = 0;
		string v_gridDec = string.Empty;//felvir01
		private const string tituloAgregar = " :::Agregar Decisión:::";//felvir01
		private const string tituloModificar = " :::Modificar Decisión:::";//felvir01
		private const string tituloEliminar = " :::Eliminar Decisión:::";//felvir01

		public s_cnf_decisiones_doc(string pa_accion,int pa_decision_id, string pa_gridDec)
        {
            InitializeComponent();
            vl_accion = pa_accion;
            vl_decision_id = pa_decision_id;
			this.v_gridDec = pa_gridDec; //felvir01
        }

        private void s_cnf_decisiones_doc_Load(object sender, EventArgs e)
        {
			if (vl_accion == u_Globales.accionAgregar)
            {				
				this.rbtExcepcion.Checked = false; //felvir01
				this.rbtSolicitud.Checked = true; //felvir01
				p_llenar_combo_estados();
				label_Titulo.Text = tituloAgregar;
                txtDecision_id.Focus();
                button1.Text = "Guardar";
            }

            if (vl_accion == u_Globales.accionModificar)
            {
				//felvir01
				this.rbtExcepcion.Visible = false;
				this.rbtSolicitud.Visible = false;
				
				if(this.v_gridDec == u_Globales.flujoSol)
				{
					label_Titulo.Text = tituloModificar + " SOLICITUD";
					p_datos_decision();
					p_llenar_combo_estados();
				}
				else
				{
					label_Titulo.Text = tituloModificar + " EXCEPCIÓN";
					this.info_decision_excep();
					this.p_llenar_estados_excepciones();
				}

                txtDecision_id.ReadOnly = true;
                txtNombre_decision.Focus();                
                button1.Focus();
                button1.Text = "Modificar";
            }

            if (vl_accion == u_Globales.accionEliminar)
            {
				//felvir01
				this.rbtExcepcion.Visible = false;
				this.rbtSolicitud.Visible = false;

				if (this.v_gridDec == u_Globales.flujoSol)
				{
					label_Titulo.Text = tituloEliminar + " SOLICITUD";
					p_datos_decision();
					p_llenar_combo_estados();
				}
				else
				{
					label_Titulo.Text = tituloEliminar + " EXCEPCIÓN";
					this.info_decision_excep();
					this.p_llenar_estados_excepciones();
				}				
                
                txtDecision_id.ReadOnly = true;
                txtNombre_decision.ReadOnly = true;
                radioButton_activo_no.Enabled = false;
                radioButton_activo_si.Enabled = false;                
                button1.Focus();
                button1.Text = "Eliminar";
            }
        }
       
        private void p_datos_decision()
        {
            try
            {
                string vl_sql = @"select * from dcs_wf_decisiones where decision_id=:pa_decision_id";
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd2.CommandType = CommandType.Text;
                //───────────────────
                OracleParameter pa_decision_id = new OracleParameter("pa_decision_id", OracleType.Int32);
                cmd2.Parameters.Add(pa_decision_id);
                pa_decision_id.Direction = ParameterDirection.Input;
                pa_decision_id.Value = vl_decision_id;
                //───────────────────            
                OracleDataReader dr = cmd2.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {

                    txtDecision_id.Text = dr["decision_id"].ToString();
                    txtNombre_decision.Text = dr["descripcion"].ToString();                    
                    string vl_activo = dr["activo"].ToString();
                    int vl_estado_solicitud_id_to = int.Parse(dr["estado_solicitud_id_to"].ToString());
                    comboBox_estados_solic.SelectedValue = vl_estado_solicitud_id_to;
                    comboBox_estados_solic_SelectionChangeCommitted(null, null);
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

                }
                dr.Close();
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
                string vl_query = @"select * from dcs_wf_estado_solicitudes where activo='S' and estado_id>1 order by estado_id";
                OracleCommand cmd = new OracleCommand(vl_query, DocSys.connOracle);
                cmd.CommandType = CommandType.Text;
                //───────────────────

                OracleDataAdapter adaptador = new OracleDataAdapter(cmd);
                DataSet registros = new DataSet();
                adaptador.Fill(registros, "dcs_wf_estado_solicitudes");
                comboBox_estados_solic.DataSource = registros;
                comboBox_estados_solic.DisplayMember = "dcs_wf_estado_solicitudes.descripcion";
                comboBox_estados_solic.ValueMember = "dcs_wf_estado_solicitudes.estado_id";
                comboBox_estados_solic_SelectionChangeCommitted(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en llenar combo_decisiones : " + ex.Message);
            }
        }

        private void comboBox_estados_solic_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtEstado_id.Text = comboBox_estados_solic.SelectedValue.ToString();
        }

        private void button_cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
			//felvir01
			if(vl_accion == u_Globales.accionAgregar)
			{
				if (this.rbtSolicitud.Checked)
				{
					p_abm_decision();
				}
				else
				{
					this.p_mant_decisiones_excep();
				}
			}
			else
			{
				if(v_gridDec == u_Globales.flujoSol)
				{
					p_abm_decision();
				}
				else
				{
					this.p_mant_decisiones_excep();
				}
			}
        }

        private void p_abm_decision()
        {
            string vl_activo = "N";
            //string vl_ver_toda_filial = "N";
            //string vl_crear_solicitudes = "N";

            if (radioButton_activo_si.Checked)
                vl_activo = "S";           

            try
            {                
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string vl_sql = "DCS_P_ABM_DECISIONES";
                OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd.CommandType = CommandType.StoredProcedure;
                //───────────────────
                OracleParameter pa_accion = new OracleParameter("pa_accion", OracleType.VarChar, 10);
                cmd.Parameters.Add(pa_accion);
                pa_accion.Direction = ParameterDirection.Input;
                pa_accion.Value = vl_accion;
                //───────────────────
                OracleParameter pa_decision_id = new OracleParameter("pa_decision_id", OracleType.Int32);
                cmd.Parameters.Add(pa_decision_id);
                pa_decision_id.Direction = ParameterDirection.Input;
                pa_decision_id.Value = int.Parse(txtDecision_id.Text);
                //───────────────────
                OracleParameter pa_descripcion = new OracleParameter("pa_descripcion", OracleType.VarChar, 50);
                cmd.Parameters.Add(pa_descripcion);
                pa_descripcion.Direction = ParameterDirection.Input;
                pa_descripcion.Value = txtNombre_decision.Text;
                //───────────────────
                OracleParameter pa_estado_solicitud_id_to = new OracleParameter("pa_estado_solicitud_id_to", OracleType.Int32);
                cmd.Parameters.Add(pa_estado_solicitud_id_to);
                pa_estado_solicitud_id_to.Direction = ParameterDirection.Input;
                pa_estado_solicitud_id_to.Value = int.Parse(txtEstado_id.Text);
                //----------------------------------
                OracleParameter pa_activo = new OracleParameter("pa_activo", OracleType.VarChar, 1);
                cmd.Parameters.Add(pa_activo);
                pa_activo.Direction = ParameterDirection.Input;
                pa_activo.Value = vl_activo;
                //───────────────────
                cmd.ExecuteReader();

                if (vl_accion == "INS")
                {
                    MessageBox.Show("Estación ingresado satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (vl_accion == "MODIF")
                {
                    MessageBox.Show("Estación modificado satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (vl_accion == "ELIM")
                {
                    MessageBox.Show("Estación eliminada satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

		#region Excepciones
		private void rbtSolicitud_CheckedChanged(object sender, EventArgs e)
		{
			if (this.rbtSolicitud.Checked)
			{
				this.p_llenar_combo_estados();
				this.txtDecision_id.Enabled = true;				
				label_Titulo.Text = tituloAgregar + " SOLICITUD";
			}
		}

		private void rbtExcepcion_CheckedChanged(object sender, EventArgs e)
		{
			if (this.rbtExcepcion.Checked)
			{
				this.p_llenar_estados_excepciones();
				this.txtDecision_id.Enabled = false;				
				label_Titulo.Text = tituloAgregar + " EXCEPCIÓN";
			}
		}

		private void p_llenar_estados_excepciones()
		{
			try
			{
				string vl_query = @"select * from excp.DCS_EXC_ESTADOS_EXCEPCION where activo='S'  order by ESTADO_EXCEP_ID";
				OracleCommand cmd = new OracleCommand(vl_query, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;
				//───────────────────

				OracleDataAdapter adaptador = new OracleDataAdapter(cmd);
				DataSet registros = new DataSet();
				adaptador.Fill(registros, "DCS_EXC_ESTADOS_EXCEPCION");
				comboBox_estados_solic.DataSource = registros;
				comboBox_estados_solic.DisplayMember = "DCS_EXC_ESTADOS_EXCEPCION.descripcion";
				comboBox_estados_solic.ValueMember = "DCS_EXC_ESTADOS_EXCEPCION.ESTADO_EXCEP_ID";
				comboBox_estados_solic_SelectionChangeCommitted(null, null);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error en llenar combo_decisiones : " + ex.Message);
			}
		}

		private void info_decision_excep()
		{
			try
			{
				string vl_sql = @"select * from excp.DCS_EXC_TIPO_DECISIONES where decision_id=:pa_decision_id";
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}
				OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
				cmd2.CommandType = CommandType.Text;
				//───────────────────
				OracleParameter pa_decision_id = new OracleParameter("pa_decision_id", OracleType.Int32);
				cmd2.Parameters.Add(pa_decision_id);
				pa_decision_id.Direction = ParameterDirection.Input;
				pa_decision_id.Value = vl_decision_id;
				//───────────────────            
				OracleDataReader dr = cmd2.ExecuteReader();
				dr.Read();
				if (dr.HasRows)
				{

					txtDecision_id.Text = dr["decision_id"].ToString();
					txtNombre_decision.Text = dr["descripcion"].ToString();
					string vl_activo = dr["activo"].ToString();
					int vl_estado_solicitud_id_to = int.Parse(dr["ESTADO_EXCEP_ID"].ToString());
					comboBox_estados_solic.SelectedValue = vl_estado_solicitud_id_to;
					comboBox_estados_solic_SelectionChangeCommitted(null, null);
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

				}
				dr.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void p_mant_decisiones_excep()
		{
			string vl_activo = "N";
			//string vl_ver_toda_filial = "N";
			//string vl_crear_solicitudes = "N";

			if (radioButton_activo_si.Checked)
				vl_activo = "S";

			try
			{
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}
				string vl_sql = "EXCP.DCS_P_MANT_TIPO_DECISIONES";
				OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
				cmd.CommandType = CommandType.StoredProcedure;

				//───────────────────Decisión_id
				OracleParameter pa_decision_id = new OracleParameter("pa_decision_id", OracleType.Int32);
				cmd.Parameters.Add(pa_decision_id);
				pa_decision_id.Direction = ParameterDirection.Input;
				if (!this.txtDecision_id.Enabled)
				{
					pa_decision_id.Value = 0;
				}
				else
				{
					pa_decision_id.Value = int.Parse(txtDecision_id.Text);
				}				

				//───────────────────descripción
				OracleParameter pa_descripcion = new OracleParameter("pa_descripcion", OracleType.VarChar, 50);
				cmd.Parameters.Add(pa_descripcion);
				pa_descripcion.Direction = ParameterDirection.Input;
				pa_descripcion.Value = txtNombre_decision.Text;

				//───────────────────Activo
				OracleParameter pa_activo = new OracleParameter("pa_activo", OracleType.VarChar, 1);
				cmd.Parameters.Add(pa_activo);
				pa_activo.Direction = ParameterDirection.Input;
				pa_activo.Value = vl_activo;

				//───────────────────Estado excepcion id
				OracleParameter pa_estado_solicitud_id_to = new OracleParameter("pa_estado_excep_id", OracleType.Int32);
				cmd.Parameters.Add(pa_estado_solicitud_id_to);
				pa_estado_solicitud_id_to.Direction = ParameterDirection.Input;
				pa_estado_solicitud_id_to.Value = int.Parse(txtEstado_id.Text);

				//───────────────────
				OracleParameter pa_accion = new OracleParameter("pa_accion", OracleType.VarChar, 10);
				cmd.Parameters.Add(pa_accion);
				pa_accion.Direction = ParameterDirection.Input;
				pa_accion.Value = vl_accion;												
				
				//───────────────────
				cmd.ExecuteReader();

				if (vl_accion == u_Globales.accionAgregar)
				{
					MessageBox.Show("Decisión ingresado satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				if (vl_accion == u_Globales.accionModificar)
				{
					MessageBox.Show("Decisión modificado satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				if (vl_accion == u_Globales.accionEliminar)
				{
					MessageBox.Show("Decisión eliminada satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				this.Close();
				this.DialogResult = DialogResult.OK;
				return;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error en DCS_P_MANT_TIPO_DECISIONES :" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
		}
		#endregion
	}
}
