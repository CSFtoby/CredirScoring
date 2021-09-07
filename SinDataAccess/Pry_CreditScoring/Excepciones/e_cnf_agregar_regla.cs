using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Docsis_Application.Excepciones
{
	public partial class e_cnf_agregar_regla : Form
	{
		private int noCondicion;
		private string accion;
		private string CodLineaB;
		private string CodLineaR;
		private string CodExcepB;
		private string CodExcepR;
		private int Cod_tipo_cond;

		public e_cnf_agregar_regla(int _noCondicion, string _accion)
		{
			InitializeComponent();

			this.noCondicion = _noCondicion;
			this.accion = _accion;

			switch (this.accion)
			{
				case u_Globales.accionAgregar:
					this.label_Titulo.Text = "Agregar Regla";
					this.btnAgregar.Text = "Agregar";
					break;
				case u_Globales.accionModificar:
					this.label_Titulo.Text = "Modificar Regla";
					this.btnAgregar.Text = "Modificar";
					this.txtNoCondicion.Text = this.noCondicion.ToString();
					break;
				case u_Globales.accionEliminar:
					this.label_Titulo.Text = "Eliminar Regla";
					this.btnAgregar.Text = "Eliminar";
					this.txtNoCondicion.Text = this.noCondicion.ToString();
					break;
				default:
					break;
			}
		}

		private void e_cnf_agregar_regla_Load(object sender, EventArgs e)
		{
			this.mostrarCheckBoxes();
			this.cargar_condiciones();
			this.cargar_excepcionesB();
			this.cargar_lineamientosB();
			this.cargar_excepcionesR();
			this.cargar_lineamientosR();
			this.cbxEstado.Checked = true;
		}

		private void btnCancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private bool validarCampos()
		{
			bool modelState = true;

			if(this.CodExcepB == null & this.CodExcepR == null
				& this.CodLineaB == null & this.CodLineaR == null)
			{
				modelState = false;
			}

			if(this.Cod_tipo_cond <= 0)
			{
				modelState = false;
			}

			return modelState;
		}

		#region Cargas comboboxes
		private void cargar_lineamientosB()
		{
			this.Cursor = Cursors.WaitCursor;
			try
			{
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				string sql = @"select codigo_lineamiento, nombre from excp.dcs_exc_lineamientos where estado = 'S' ";
				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;
				OracleDataAdapter adaptador = new OracleDataAdapter(cmd);
				DataSet registrosExce = new DataSet();
				
				adaptador.Fill(registrosExce, "dcs_exc_lineamientos");

				//lineamientos base
				this.cmbLineaB.DataSource = registrosExce;
				this.cmbLineaB.DisplayMember = "dcs_exc_lineamientos.nombre";
				this.cmbLineaB.ValueMember = "dcs_exc_lineamientos.codigo_lineamiento";
				this.cmbLineaB_SelectionChangeCommitted(null, null);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void cargar_lineamientosR()
		{
			this.Cursor = Cursors.WaitCursor;
			try
			{
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				string sql = @"select codigo_lineamiento, nombre from excp.dcs_exc_lineamientos where estado = 'S' ";
				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;
				OracleDataAdapter adaptador = new OracleDataAdapter(cmd);
				DataSet registrosExce = new DataSet("lineR");

				adaptador.Fill(registrosExce, "dcs_exc_lineamientos");

				//lineamientos restringidos
				this.cmbLineaR.DataSource = registrosExce;
				this.cmbLineaR.DisplayMember = "dcs_exc_lineamientos.nombre";
				this.cmbLineaR.ValueMember = "dcs_exc_lineamientos.codigo_lineamiento";
				this.cmbLineaR_SelectionChangeCommitted(null, null);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void cargar_excepcionesB()
		{
			this.Cursor = Cursors.WaitCursor;
			try
			{
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				string sql = @"select cod_tipo_excepcion, tipo_excepcion from excp.dcs_exc_tipo_excepciones where estado = 'S' ";
				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;
				OracleDataAdapter adaptador = new OracleDataAdapter(cmd);
				DataSet registrosExce = new DataSet("ExcepcionesB");

				adaptador.Fill(registrosExce, "dcs_exc_tipo_excepciones");

				//Excepción base
				this.cmbExcepB.DataSource = registrosExce;
				this.cmbExcepB.DisplayMember = "dcs_exc_tipo_excepciones.tipo_excepcion";
				this.cmbExcepB.ValueMember = "dcs_exc_tipo_excepciones.cod_tipo_excepcion";
				this.cmbExcepB_SelectionChangeCommitted(null, null);

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void cargar_excepcionesR()
		{
			this.Cursor = Cursors.WaitCursor;
			try
			{
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				string sql = @"select cod_tipo_excepcion, tipo_excepcion from excp.dcs_exc_tipo_excepciones where estado = 'S' ";
				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;
				OracleDataAdapter adaptador = new OracleDataAdapter(cmd);
				DataSet registrosExce = new DataSet("ExcepcionesR");

				adaptador.Fill(registrosExce, "dcs_exc_tipo_excepciones");	

				//Excepción Restricción
				this.cmbExcepR.DataSource = registrosExce;
				this.cmbExcepR.DisplayMember = "dcs_exc_tipo_excepciones.tipo_excepcion";
				this.cmbExcepR.ValueMember = "dcs_exc_tipo_excepciones.cod_tipo_excepcion";
				this.cmbExcepR_SelectionChangeCommitted(null, null);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void cargar_condiciones()
		{
			this.Cursor = Cursors.WaitCursor;
			try
			{
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				string sql = @"select cod_tipo_condicion, tipo_condicion from excp.dcs_exc_tipo_condicion where estado = 'S' ";
				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;
				OracleDataAdapter adaptador = new OracleDataAdapter(cmd);
				DataSet registrosExce = new DataSet("Excepciones");

				adaptador.Fill(registrosExce, "dcs_exc_tipo_condicion");

				//Excepción base
				this.cmbCondicion.DataSource = registrosExce;
				this.cmbCondicion.DisplayMember = "dcs_exc_tipo_condicion.tipo_condicion";
				this.cmbCondicion.ValueMember = "dcs_exc_tipo_condicion.cod_tipo_condicion";
				this.cmbCondicion_SelectionChangeCommitted(null, null);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			this.Cursor = Cursors.Default;
		}
		#endregion

		#region SelectionChangeCommitted
		//Lineamiento Base
		private void cmbLineaB_SelectionChangeCommitted(object sender, EventArgs e)
		{
			this.lblLineaB.Text = $"Lineamiento Base: ({this.cmbLineaB.SelectedValue.ToString()})";
			this.CodLineaB = this.cmbLineaB.SelectedValue.ToString();
		}

		//Lineamiento Restringido
		private void cmbLineaR_SelectionChangeCommitted(object sender, EventArgs e)
		{
			this.lblLineaR.Text = $"Lineamiento Restringido: ({this.cmbLineaR.SelectedValue.ToString()})";
			this.CodLineaR = this.cmbLineaR.SelectedValue.ToString();
		}

		//Excepción Base
		private void cmbExcepB_SelectionChangeCommitted(object sender, EventArgs e)
		{
			this.lblExceB.Text = $"Excepción Base: ({this.cmbExcepB.SelectedValue.ToString()})";
			this.CodExcepB = this.cmbExcepB.SelectedValue.ToString();
		}

		//Excepción Restringida
		private void cmbExcepR_SelectionChangeCommitted(object sender, EventArgs e)
		{
			this.lblExcepR.Text = $"Excepción Restringida: ({this.cmbExcepR.SelectedValue.ToString()})";
			this.CodExcepR = this.cmbExcepR.SelectedValue.ToString();
		}

		private void cmbCondicion_SelectionChangeCommitted(object sender, EventArgs e)
		{
			this.Cod_tipo_cond = Convert.ToInt32(this.cmbCondicion.SelectedValue.ToString());
		}
		#endregion

		#region CheckBoxes
		private void naLinB_CheckedChanged_1(object sender, EventArgs e)
		{
			if (this.naLinB.Visible)
			{
				if (!this.naLinB.Checked)
				{
					this.CodLineaB = null;				
				}
				else
				{
					this.cmbLineaB_SelectionChangeCommitted(null, null);
				}
			}
		}

		private void naExcB_CheckedChanged(object sender, EventArgs e)
		{
			if (this.naExcB.Visible)
			{
				if (!this.naExcB.Checked)
				{
					this.CodExcepB = null;
				}
				else
				{
					this.cmbExcepB_SelectionChangeCommitted(null, null);
				}
			}
		}

		private void naLinR_CheckedChanged_1(object sender, EventArgs e)
		{
			if (this.naLinR.Visible)
			{
				if (!this.naLinR.Checked)
				{
					this.CodLineaR = null;
				}
				else
				{
					this.cmbLineaR_SelectionChangeCommitted(null, null);
				}
			}
		}

		private void naExcR_CheckedChanged(object sender, EventArgs e)
		{
			if (this.naExcR.Visible)
			{
				if (!this.naExcR.Checked)
				{
					this.CodExcepR = null;
				}
				else
				{
					this.cmbExcepR_SelectionChangeCommitted(null, null);
				}
			}
		}
		#endregion

		private void btnAgregar_Click(object sender, EventArgs e)
		{			
			switch (this.Cod_tipo_cond)
			{
				case 1:
					this.CodLineaB = string.Empty;
					this.CodLineaR = string.Empty;
					break;
				case 2:
					this.CodLineaR = string.Empty;
					this.CodExcepB = string.Empty;
					break;
				default:
					if (!this.naExcB.Checked) this.CodExcepB = string.Empty;
					if (!this.naExcR.Checked) this.CodExcepR = string.Empty;
					if (!this.naLinB.Checked) this.CodLineaB = string.Empty;
					if (!this.naLinR.Checked) this.CodLineaR = string.Empty;
					break;
			}

			//if (!this.validarCampos())
			//{
			//	MessageBox.Show("Campos incompletos");
			//}
			//else
			//{
			this.p_mant_reglas();
			//}
		}

		private void p_mant_reglas()
		{
			this.Cursor = Cursors.WaitCursor;
			try
			{
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				string vl_sql = "excp.DCS_P_MAT_RGLAS_EXCEP";
				OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
				cmd.CommandType = CommandType.StoredProcedure;

				//───────────────────PA_NO_CONDICION IN NUMBER,
				OracleParameter pa_noCondicion = new OracleParameter("PA_NO_CONDICION", OracleType.Int32);
				cmd.Parameters.Add(pa_noCondicion);
				pa_noCondicion.Direction = ParameterDirection.Input;
				pa_noCondicion.Value = this.noCondicion;

				//───────────────────PA_CODIGO_LINEAMIENTO IN VARCHAR2,
				OracleParameter pa_cod_lineaB = new OracleParameter("PA_CODIGO_LINEAMIENTO", OracleType.VarChar);
				cmd.Parameters.Add(pa_cod_lineaB);
				pa_cod_lineaB.Direction = ParameterDirection.Input;
				pa_cod_lineaB.Value = this.CodLineaB;

				//───────────────────PA_COD_TIPO_EXCEPCION IN VARCHAR2,
				OracleParameter pa_cod_tip_excep = new OracleParameter("PA_COD_TIPO_EXCEPCION", OracleType.VarChar);
				cmd.Parameters.Add(pa_cod_tip_excep);
				pa_cod_tip_excep.Direction = ParameterDirection.Input;
				pa_cod_tip_excep.Value = this.CodExcepB;

				//───────────────────PA_COD_TIPO_EXCEP_PROHIBIDA IN VARCHAR2,
				OracleParameter pa_cod_tip_excp_r = new OracleParameter("PA_COD_TIPO_EXCEP_PROHIBIDA", OracleType.VarChar);
				cmd.Parameters.Add(pa_cod_tip_excp_r);
				pa_cod_tip_excp_r.Direction = ParameterDirection.Input;
				pa_cod_tip_excp_r.Value = this.CodExcepR;

				//───────────────────PA_COD_LINEA_PROHIBIDA IN VARCHAR2,
				OracleParameter pa_cod_lin_r = new OracleParameter("PA_COD_LINEA_PROHIBIDA", OracleType.VarChar);
				cmd.Parameters.Add(pa_cod_lin_r);
				pa_cod_lin_r.Direction = ParameterDirection.Input;
				pa_cod_lin_r.Value = this.CodLineaR;

				//───────────────────PA_COD_TIPO_CONDICION IN NUMBER,
				OracleParameter pa_cod_tip_cond = new OracleParameter("PA_COD_TIPO_CONDICION", OracleType.Int32);
				cmd.Parameters.Add(pa_cod_tip_cond);
				pa_cod_tip_cond.Direction = ParameterDirection.Input;
				pa_cod_tip_cond.Value = this.Cod_tipo_cond;

				//───────────────────PA_OBSERVACIONES IN VARCHAR2,
				OracleParameter pa_observaciones = new OracleParameter("PA_OBSERVACIONES", OracleType.VarChar);
				cmd.Parameters.Add(pa_observaciones);
				pa_observaciones.Direction = ParameterDirection.Input;
				pa_observaciones.Value = this.txtObservaciones.Text;

				//───────────────────PA_ESTADO IN VARCHAR2,
				OracleParameter pa_estado = new OracleParameter("PA_ESTADO", OracleType.VarChar);
				cmd.Parameters.Add(pa_estado);
				pa_estado.Direction = ParameterDirection.Input;
				pa_estado.Value = (this.cbxEstado.Checked) ? "S" : "N";

				//───────────────────PA_DOCUMENTACION IN VARCHAR2
				OracleParameter pa_documentacion = new OracleParameter("PA_DOCUMENTACION", OracleType.VarChar);
				cmd.Parameters.Add(pa_documentacion);
				pa_documentacion.Direction = ParameterDirection.Input;
				pa_documentacion.Value = (this.cbxDocumentacion.Checked) ? "S" : "N";

				//───────────────────PA_ACCION IN VARCHAR2
				OracleParameter pa_accion = new OracleParameter("PA_ACCION", OracleType.VarChar);
				cmd.Parameters.Add(pa_accion);
				pa_accion.Direction = ParameterDirection.Input;
				pa_accion.Value = this.accion;

				cmd.ExecuteReader();

				switch (this.accion)
				{
					case u_Globales.accionAgregar:
						MessageBox.Show("Regla ingresada satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
						break;
					case u_Globales.accionModificar:
						MessageBox.Show("Regla modificada satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
						break;
					case u_Globales.accionEliminar:
						MessageBox.Show("Regla eliminada satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
						break;
					default:
						break;
				}

				this.Close();
				this.DialogResult = DialogResult.OK;
				return;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void cmbCondicion_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(this.cmbCondicion != null)
			{
				int _item = this.cmbCondicion.SelectedIndex + 1;
				this.ReestablecerCombos();

				switch (_item)
				{
					case 1:
						this.cmbLineaB.Enabled = false;
						this.cmbLineaR.Enabled = false;						
						this.mostrarCheckBoxes();
						break;
					case 2:
						this.cmbLineaR.Enabled = false;
						this.cmbExcepB.Enabled = false;
						this.mostrarCheckBoxes();
						break;
					default:
						this.mostrarCheckBoxes(true);
						break;
				}
			}			
		}

		private void ReestablecerCombos()
		{
			this.cmbExcepB.Enabled = true;
			this.cmbExcepR.Enabled = true;
			this.cmbLineaB.Enabled = true;
			this.cmbLineaR.Enabled = true;
		}

		private void mostrarCheckBoxes(bool show = false)
		{
			this.naExcB.Visible = show;
			this.naExcR.Visible = show;
			this.naLinB.Visible = show;
			this.naLinR.Visible = show;
		}
	
	}	
}
