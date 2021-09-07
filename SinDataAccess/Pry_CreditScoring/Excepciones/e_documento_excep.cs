using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wfcModel;

namespace Docsis_Application.Excepciones
{
	public partial class e_documento_excep : Form
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


				if (s_login.ventana_con_borde)
				{
					cp.Style |= 0x40000 | CS_DROPSHADOW;
				}
				else
				{
					cp.ClassStyle |= CS_DROPSHADOW;
				}
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

		private static int contador = 0;
		private int CodigoExcepcion = 0;
		private DataAccess da;

		public e_documento_excep(int codigoExcepcion, DataAccess da)
		{
			InitializeComponent();

			this.CodigoExcepcion = codigoExcepcion;
			this.da = da;
		}

		private void e_documento_excep_Load(object sender, EventArgs e)
		{

		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void panel3_MouseDown(object sender, MouseEventArgs e)
		{
			moverForm();
		}

		private void button_cerrar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnSeleccionar_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Title = "::DocSis - Seleccione un archivo";
				openFileDialog.InitialDirectory = Application.StartupPath + "\\logs";
				openFileDialog.Filter = "Todos los Archivos (*.*)|*.*";
				openFileDialog.FilterIndex = 1;
				openFileDialog.Multiselect = false;

				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					string nombre_completo = openFileDialog.FileName;
					var vl_var = new System.IO.FileInfo(nombre_completo);
					string extension = vl_var.Extension;
					string nombre_corto = vl_var.Name;
					contador++;					
					this.dgvDocumentosExc.Rows.Add(contador, nombre_corto, false, nombre_completo, extension);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}


		private void dgvDocumentosExc_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (this.dgvDocumentosExc.RowCount > 0)
				{
					DataGridViewRow row = this.dgvDocumentosExc.CurrentRow;

					if((bool)row.Cells["quitar"].Value == false || row.Cells["quitar"].Value == null)
					{
						row.Cells["quitar"].Value = true;
					}
					else
					{
						row.Cells["quitar"].Value = false;
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btn_guardar_Click(object sender, EventArgs e)
		{
			if(this.dgvDocumentosExc.RowCount > 0)
			{
				foreach (DataGridViewRow item in this.dgvDocumentosExc.Rows)
				{
					if(item != null)
					{
						string direccion = item.Cells["nombre_archivo"].Value.ToString();
						string extension = item.Cells["extension"].Value.ToString();
						bool formato = (bool)item.Cells["quitar"].Value;
						string nombre = item.Cells["nombre_documento"].Value.ToString();

						bool valor = this.da.existe_formato_excepcion(this.CodigoExcepcion);

						if(!valor && formato)
							this.agregar_adjunto(this.CodigoExcepcion, direccion, extension, formato, nombre);
						else if(valor && formato)
						{
							if (DialogResult.No == MessageBox.Show("El documento ya existe, desea reemplazar el documento ingresando anteriormente", nombre, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
							{
								this.agregar_adjunto(this.CodigoExcepcion, direccion, extension, formato, nombre);
							}
							else
							{
								this.agregar_adjunto(this.CodigoExcepcion, direccion, extension, formato, nombre, true);
							}
						}
						else
						{
							this.agregar_adjunto(this.CodigoExcepcion, direccion, extension, formato, nombre);
						}
					}
				}
			}

			this.Close();
		}

		private void agregar_adjunto(int _codigo_excepcion, string _archivoCom, string _extension, bool _formato_excepcion, string _archivoN, bool repetido = false)
		{
			try
			{
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				FileInfo fi = new FileInfo(_archivoCom);
				StreamReader sr = new StreamReader(fi.FullName);
				String tempBuff = sr.ReadToEnd();
				sr.Close();

				System.IO.FileStream fs = new System.IO.FileStream(_archivoCom, System.IO.FileMode.Open, System.IO.FileAccess.Read);
				Byte[] data = new byte[fs.Length];
				fs.Read(data, 0, Convert.ToInt32(fs.Length));
				fs.Dispose();

				OracleTransaction transaction = DocSys.connOracle.BeginTransaction();
				OracleCommand command = DocSys.connOracle.CreateCommand();
				command.Transaction = transaction;
				command.CommandText = "declare xx blob; begin dbms_lob.createtemporary(xx, false, 0); :tempblob := xx; end;";
				command.Parameters.Add(new OracleParameter("tempblob", OracleType.Blob)).Direction = ParameterDirection.Output;
				command.ExecuteNonQuery();
				OracleLob tempLob = (OracleLob)command.Parameters[0].Value;

				tempLob.BeginBatch(OracleLobOpenMode.ReadWrite);
				tempLob.Write(data, 0, data.Length);
				tempLob.EndBatch();
				command.Parameters.Clear();
				transaction.Commit();

				string sql = "excp.dcs_p_ins_adj_excep";
				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.StoredProcedure;

				//───────────────────pa_codigo_excepcion
				OracleParameter pa_codigo_excepcion = new OracleParameter("pa_codigo_excepcion", OracleType.Number);
				pa_codigo_excepcion.Direction = ParameterDirection.Input;
				pa_codigo_excepcion.Value = _codigo_excepcion;
				cmd.Parameters.Add(pa_codigo_excepcion);

				//───────────────────pa_nombre_archivo
				OracleParameter pa_nombre_archivo = new OracleParameter("pa_nombre_archivo", OracleType.VarChar);
				pa_nombre_archivo.Direction = ParameterDirection.Input;
				pa_nombre_archivo.Value = _archivoN;
				cmd.Parameters.Add(pa_nombre_archivo);

				//───────────────────pa_extension
				OracleParameter pa_extension = new OracleParameter("pa_extension", OracleType.VarChar);
				pa_extension.Direction = ParameterDirection.Input;
				pa_extension.Value = _extension;
				cmd.Parameters.Add(pa_extension);

				//───────────────────pa_Archivo_bin
				OracleParameter pa_Archivo_bin = new OracleParameter("pa_Archivo_bin", OracleType.Blob);
				pa_Archivo_bin.Direction = ParameterDirection.Input;
				pa_Archivo_bin.Value = tempLob;
				cmd.Parameters.Add(pa_Archivo_bin);

				//───────────────────pa_formato_excepcion
				OracleParameter pa_formato_excepcion = new OracleParameter("pa_formato_excepcion", OracleType.VarChar, 1);
				pa_formato_excepcion.Direction = ParameterDirection.Input;
				pa_formato_excepcion.Value = (_formato_excepcion) ? "S" : "N";
				cmd.Parameters.Add(pa_formato_excepcion);

				//───────────────────pa_accion
				OracleParameter pa_accion = new OracleParameter("pa_accion", OracleType.VarChar);
				pa_accion.Direction = ParameterDirection.Input;
				pa_accion.Value = (!repetido) ? u_Globales.accionAgregar : u_Globales.accionModificar;
				cmd.Parameters.Add(pa_accion);

				cmd.ExecuteReader();

				MessageBox.Show("Adjunto ingresado satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error en p_insertar_solicitud :" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
		}
	}
}
