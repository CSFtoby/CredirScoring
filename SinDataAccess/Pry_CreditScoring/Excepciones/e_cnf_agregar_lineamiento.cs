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

//felvir01
namespace Docsis_Application.Excepciones
{
	public partial class e_cnf_agregar_lineamiento : Form
	{
		private string Codigo;
		private string Accion;
		private const string tituloAgregar = "Agregar Lineamiento";
		private const string tituloModificar = "Modificar Lineamiento";
		private const string tituloEliminar = "Eliminar Lineamiento";

		public e_cnf_agregar_lineamiento(string _codigo, string _accion)
		{
			InitializeComponent();
			this.Codigo = _codigo;
			this.Accion = _accion;

			switch (this.Accion)
			{
				case u_Globales.accionAgregar:
					this.label_Titulo.Text = tituloAgregar;
					this.btnAgregar.Text = "Guardar";
					break;
				case u_Globales.accionModificar:
					this.label_Titulo.Text = tituloModificar;
					this.txtCodigo.Text = this.Codigo;
					this.btnAgregar.Text = "Modificar";
					this.p_cargar_info();
					break;
				case u_Globales.accionEliminar:
					this.label_Titulo.Text = tituloEliminar;
					this.txtCodigo.Text = this.Codigo;
					this.btnAgregar.Text = "Eliminar";
					this.p_cargar_info();
					break;
				default:
					break;
			}
		}

		private void textBox3_TextChanged(object sender, EventArgs e)
		{

		}

		private void btnCancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void e_cnf_agregar_lineamiento_Load(object sender, EventArgs e)
		{
		}

		private void p_cargar_info()
		{
			string sql = @"select nombre, descripcion, estado from excp.dcs_exc_lineamientos where codigo_lineamiento = :codigo ";
			if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
			{
				DocSys.connOracle.Open();
			}
			OracleCommand cmd2 = new OracleCommand(sql, DocSys.connOracle);
			cmd2.CommandType = CommandType.Text;

			OracleParameter pa_codigo_linea = new OracleParameter("codigo", OracleType.VarChar);
			cmd2.Parameters.Add(pa_codigo_linea);
			pa_codigo_linea.Direction = ParameterDirection.Input;
			pa_codigo_linea.Value = this.Codigo;

			OracleDataReader dr = cmd2.ExecuteReader();
			dr.Read();
			if (dr.HasRows)
			{
				this.txtNombre.Text = dr["nombre"].ToString();
				this.txtDescripcion.Text = dr["descripcion"].ToString();				

				if(dr["estado"].ToString() == "S")
				{
					this.cbxEstado.Checked = true;
				}
				else
				{
					this.cbxEstado.Checked = false;
				}
			}

			dr.Close();
		}

		private void p_mant_lineamientos()
		{
			try
			{
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				string vl_sql = "excp.DCS_P_MANT_LINEAMIENTO";
				OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
				cmd.CommandType = CommandType.StoredProcedure;

				//───────────────────Código de lineamiento
				OracleParameter pa_cod_linea = new OracleParameter("PA_COD_LINEA", OracleType.VarChar);
				cmd.Parameters.Add(pa_cod_linea);
				pa_cod_linea.Direction = ParameterDirection.Input;
				pa_cod_linea.Value = this.txtCodigo.Text;

				//───────────────────Nombre lineamiento
				OracleParameter pa_nom_linea = new OracleParameter("PA_NOMBRE_LINEA", OracleType.VarChar);
				cmd.Parameters.Add(pa_nom_linea);
				pa_nom_linea.Direction = ParameterDirection.Input;
				pa_nom_linea.Value = this.txtNombre.Text;

				//───────────────────Descripción lineamiento
				OracleParameter pa_descripcion = new OracleParameter("PA_DESCRIPCION", OracleType.VarChar);
				cmd.Parameters.Add(pa_descripcion);
				pa_descripcion.Direction = ParameterDirection.Input;
				pa_descripcion.Value = this.txtDescripcion.Text;

				//───────────────────Estado
				OracleParameter pa_estado = new OracleParameter("PA_ESTADO", OracleType.VarChar);
				cmd.Parameters.Add(pa_estado);
				pa_estado.Direction = ParameterDirection.Input;
				pa_estado.Value = (this.cbxEstado.Checked) ? "S" : "N";

				//───────────────────Acción
				OracleParameter pa_accion = new OracleParameter("PA_ACCION", OracleType.VarChar);
				cmd.Parameters.Add(pa_accion);
				pa_accion.Direction = ParameterDirection.Input;
				pa_accion.Value = this.Accion;

				cmd.ExecuteReader();

				switch (this.Accion)
				{
					case u_Globales.accionAgregar:
						MessageBox.Show("Lineamiento ingresado satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
						break;
					case u_Globales.accionModificar:
						MessageBox.Show("Lineamiento modificado satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
						break;
					case u_Globales.accionEliminar:
						MessageBox.Show("Lineamiento eliminado satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

		private void btnAgregar_Click(object sender, EventArgs e)
		{
			if(this.Accion != u_Globales.accionEliminar)
			{
				if (this.validarCampos())
				{
					this.p_mant_lineamientos();
				}
				else
				{
					MessageBox.Show("Debe ingresar todos los campos");
				}
			}
			else
			{
				this.p_mant_lineamientos();
			}		
		}

		private bool validarCampos()
		{
			bool modelState = true;

			if (this.txtCodigo.Text.Equals(string.Empty))
			{
				modelState = false;
			}

			if (this.txtDescripcion.Text.Equals(string.Empty))
			{
				modelState = false;
			}
			if (this.txtNombre.Equals(string.Empty))
			{
				modelState = false;
			}

			return modelState;
		}
	}
}
