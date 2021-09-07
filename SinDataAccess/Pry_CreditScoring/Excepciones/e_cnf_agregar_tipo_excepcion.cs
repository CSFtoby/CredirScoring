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
	public partial class e_cnf_agregar_tipo_excepcion : Form
	{
		private string Accion;
		private string Codigo;
		private string Lineamiento;

		public e_cnf_agregar_tipo_excepcion(string _codigo, string _accion)
		{
			InitializeComponent();
			this.Accion = _accion;
			this.Codigo = _codigo;

			this.p_cargar_lineamientos();

			switch (this.Accion)
			{
				case u_Globales.accionAgregar:
					this.label_Titulo.Text = "Agregar Excepción";
					this.btnAgregar.Text = "Agregar";
					break;
				case u_Globales.accionModificar:
					this.label_Titulo.Text = "Modificar Excepción";
					this.btnAgregar.Text = "Modificar";
					this.cargar_info();
					break;
				case u_Globales.accionEliminar:
					this.label_Titulo.Text = "Eliminar Excepción";
					this.btnAgregar.Text = "Eliminar";
					this.cargar_info();
					break;
				default:
					break;
			}
		}

		private void btnCancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void e_cnf_agregar_tipo_excepcion_Load(object sender, EventArgs e)
		{			
		}

		private void cargar_info()
		{
			try
			{
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				string sql = @"select e.cod_tipo_excepcion, e.tipo_excepcion, e.estado, e.codigo_lineamiento, l.nombre, l.descripcion 
                          from excp.dcs_exc_tipo_excepciones e, excp.DCS_EXC_LINEAMIENTOS l 
                          where e.codigo_lineamiento = l.codigo_lineamiento and e.cod_tipo_excepcion = :cod_tipo_excep ";

				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;

				OracleParameter pa_codigo = new OracleParameter("cod_tipo_excep", OracleType.VarChar);
				cmd.Parameters.Add(pa_codigo);
				pa_codigo.Direction = ParameterDirection.Input;
				pa_codigo.Value = this.Codigo;

				OracleDataReader dr = cmd.ExecuteReader();
				dr.Read();

				if (dr.HasRows)
				{
					this.txtCodigo.Text = dr["cod_tipo_excepcion"].ToString();
					this.txtDescripcion.Text = dr["tipo_excepcion"].ToString();

					if (dr["estado"].ToString() == "S")
					{
						this.cbxEstado.Checked = true;
					}
					else
					{
						this.cbxEstado.Checked = false;
					}

					this.cmbLineamiento.SelectedValue = dr["codigo_lineamiento"].ToString();
				}

				dr.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void p_cargar_lineamientos()
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
				this.cmbLineamiento.DataSource = registrosExce;
				this.cmbLineamiento.DisplayMember = "dcs_exc_lineamientos.nombre";
				this.cmbLineamiento.ValueMember = "dcs_exc_lineamientos.codigo_lineamiento";
				this.cmbLineamiento_SelectionChangeCommitted(null, null);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			this.Cursor = Cursors.Default;
		}

		private void lblTipo_Click(object sender, EventArgs e)
		{

		}

		private void cmbLineamiento_SelectionChangeCommitted(object sender, EventArgs e)
		{
			this.lblTipo.Text = $"Lineamiento: ({this.cmbLineamiento.SelectedValue.ToString()})";
			this.Lineamiento = this.cmbLineamiento.SelectedValue.ToString();
		}

		private void btnAgregar_Click(object sender, EventArgs e)
		{
			if (!this.Accion.Equals(u_Globales.accionEliminar))
			{
				if (this.validarCampos()) this.p_mant_tipo_excepcion();
				else MessageBox.Show("Debe ingresar todos los campos");
			}
			else
			{
				this.p_mant_tipo_excepcion();
			}
		}

		private void p_mant_tipo_excepcion()
		{
			try
			{
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				string vl_sql = "excp.DCS_P_MANT_TIPO_EXCEPCIONES";
				OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
				cmd.CommandType = CommandType.StoredProcedure;

				//───────────────────PA_COD_TIP_EXC IN VARCHAR2
				OracleParameter pa_cod_tipo_exc = new OracleParameter("PA_COD_TIP_EXC", OracleType.VarChar);
				cmd.Parameters.Add(pa_cod_tipo_exc);
				pa_cod_tipo_exc.Direction = ParameterDirection.Input;
				pa_cod_tipo_exc.Value = this.txtCodigo.Text;

				//───────────────────PA_TIPO_EXCE IN VARCHAR2
				OracleParameter pa_tipo_excep = new OracleParameter("PA_TIPO_EXCE", OracleType.VarChar);
				cmd.Parameters.Add(pa_tipo_excep);
				pa_tipo_excep.Direction = ParameterDirection.Input;
				pa_tipo_excep.Value = this.txtDescripcion.Text;

				//───────────────────PA_ESTADO IN VARCHAR2
				OracleParameter pa_estado = new OracleParameter("PA_ESTADO", OracleType.VarChar);
				cmd.Parameters.Add(pa_estado);
				pa_estado.Direction = ParameterDirection.Input;
				pa_estado.Value = (this.cbxEstado.Checked) ? "S" : "N";

				//───────────────────PA_COD_LINEA IN VARCHAR2
				OracleParameter pa_cod_linea = new OracleParameter("PA_COD_LINEA", OracleType.VarChar);
				cmd.Parameters.Add(pa_cod_linea);
				pa_cod_linea.Direction = ParameterDirection.Input;
				pa_cod_linea.Value = this.Lineamiento;

				//───────────────────PA_ACCION IN VARCHAR2
				OracleParameter pa_accion = new OracleParameter("PA_ACCION", OracleType.VarChar);
				cmd.Parameters.Add(pa_accion);
				pa_accion.Direction = ParameterDirection.Input;
				pa_accion.Value = this.Accion;

				cmd.ExecuteReader();

				switch (this.Accion)
				{
					case u_Globales.accionAgregar:
						MessageBox.Show("Tipo Excepción ingresada satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
						break;
					case u_Globales.accionModificar:
						MessageBox.Show("Tipo Excepción modificada satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
						break;
					case u_Globales.accionEliminar:
						MessageBox.Show("Tipo Excepción eliminada satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

			this.Cursor = Cursors.Default;
		}

		private bool validarCampos()
		{
			bool modelState = true;

			if (this.txtCodigo.Text.Equals(string.Empty)) modelState = false;
			if (this.txtDescripcion.Text.Equals(string.Empty)) modelState = false;
			if (this.Lineamiento.Equals(string.Empty)) modelState = false;

			return modelState;
		}
	}
}
