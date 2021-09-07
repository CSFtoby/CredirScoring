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
	public partial class e_add_notas : Form
	{
		#region
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
		#endregion
		#region
		private const int CS_DROPSHADOW = 0x00020000;
		protected override CreateParams CreateParams
		{
			get
			{
				// add the drop shadow flag for automatically drawing
				// a drop shadow around the form
				CreateParams cp = base.CreateParams;
				cp.ClassStyle |= CS_DROPSHADOW;
				return cp;
			}

		}

		private void moverForm()
		{
			ReleaseCapture();
			SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
		}
		#endregion

		private int CodigoExcepcion = 0;
		private int Estacion_Id = 0;
		private string Accion = string.Empty;
		private int codigo_Anotacion;
		public string respuesta = string.Empty;
		public bool cerrar = false;

		public e_add_notas(int codigo_excepcion, int estacion_id, string accion, int codigo_anotacion)
		{
			InitializeComponent();

			this.CodigoExcepcion = codigo_excepcion;
			this.Estacion_Id = estacion_id;
			this.Accion = accion;

			if (this.Accion.Equals(u_Globales.accionModificar))
			{
				this.btnAgregar.Visible = false;
				this.codigo_Anotacion = codigo_anotacion;

				try
				{
					string vl_sql = @"select anotacion from excp.dcs_anotaciones_excepciones  
										where CODIGO_EXCEPCION = :cod_excep 
										and no_anotacion = :no_anotacion ";
					if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
					{
						DocSys.connOracle.Open();
					}

					OracleParameter pa_codigo_excepcion = new OracleParameter("cod_excep", OracleType.Number);
					pa_codigo_excepcion.Direction = ParameterDirection.Input;
					pa_codigo_excepcion.Value = this.CodigoExcepcion;

					OracleParameter pa_no_anotacion = new OracleParameter("no_anotacion", OracleType.Number);
					pa_no_anotacion.Direction = ParameterDirection.Input;
					pa_no_anotacion.Value = this.codigo_Anotacion;

					OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
					cmd2.CommandType = CommandType.Text;
					cmd2.Parameters.Add(pa_codigo_excepcion);
					cmd2.Parameters.Add(pa_no_anotacion);

					OracleDataReader dr = cmd2.ExecuteReader();
					dr.Read();
					this.txtTexto.Text = dr["anotacion"].ToString();
					this.txtTexto.Enabled = false;
					dr.Close();					
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
			else
			{
				this.radioButton_anota_condicion.Visible = false;
				this.radioButton_anota_normal.Visible = false;
				this.btnAgregar.Visible = true;
			}
		}

		private void panel3_MouseDown(object sender, MouseEventArgs e)
		{
			moverForm();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.respuesta = this.txtTexto.Text;
			if (this.txtTexto.Text == string.Empty)
			{
				DialogResult result = MessageBox.Show("El campo de texto está vació, desea cancelar la operación", "Error", MessageBoxButtons.YesNo);
				if (result == DialogResult.Yes)
				{
					cerrar = true;
					this.Close();
				}
				else if (result == DialogResult.No)
				{
					return;
				}				
			}
			else
			{
				string tipo_anotacion = string.Empty;
				if (this.radioButton_anota_normal.Checked)
				{
					tipo_anotacion = "N";
				}
				else
				{
					tipo_anotacion = "C";
				}

				this.agregar_anotacion(tipo_anotacion, this.txtTexto.Text);
			}
		}

		private void agregar_anotacion(string tipo_anotacion, string anotacion)
		{
			try
			{
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				string sql = "excp.dcs_p_ins_anotacion_exc";
				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.StoredProcedure;

				//───────────────────pa_codigo_excepcion
				OracleParameter pa_codigo_excepcion = new OracleParameter("pa_codigo_excepcion", OracleType.Number);
				pa_codigo_excepcion.Direction = ParameterDirection.Input;
				pa_codigo_excepcion.Value = this.CodigoExcepcion;

				//───────────────────pa_anotacion
				OracleParameter pa_anotacion = new OracleParameter("pa_anotacion", OracleType.VarChar);
				pa_anotacion.Direction = ParameterDirection.Input;
				pa_anotacion.Value = anotacion;

				//───────────────────pa_tipo_anotacion
				OracleParameter pa_tipo_anotacion = new OracleParameter("pa_tipo_anotacion", OracleType.VarChar);
				pa_tipo_anotacion.Direction = ParameterDirection.Input;
				pa_tipo_anotacion.Value = tipo_anotacion;

				//───────────────────pa_estacion_id
				OracleParameter pa_estacion_id = new OracleParameter("pa_estacion_id", OracleType.Number);
				pa_estacion_id.Direction = ParameterDirection.Input;
				pa_estacion_id.Value = this.Estacion_Id;

				cmd.Parameters.Add(pa_codigo_excepcion);
				cmd.Parameters.Add(pa_anotacion);
				cmd.Parameters.Add(pa_tipo_anotacion);
				cmd.Parameters.Add(pa_estacion_id);

				cmd.ExecuteReader();

				MessageBox.Show("Anotación agregada con éxito", "Operación Exitosa");
				this.Close();

			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error: {ex.Message}", "Ha ocurrido un error");
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.cerrar = true;
			this.Close();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void e_add_notas_Load(object sender, EventArgs e)
		{

		}
	}
}
