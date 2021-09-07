using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using wfcModel;

namespace Docsis_Application.Excepciones
{
	public partial class e_buscar_excepcion : Form
	{
		DataAccess da;
		bool con_borde = MDI_Menu.con_borde;
		enum Filtros
		{
			CodigoExcepcion = 0,
			NoSolicitud = 1,
			CodigoCliente = 2,
			Nombre = 3,
			Filial = 4,
			OficialServicio = 5
		}

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

		bool global_ver_toda_filial = DocSys.p_obtener_si_todas_las_filiales(DocSys.p_obtener_estacion_usuario(DocSys.vl_user));
		bool global_gerente_filial = DocSys.p_obtener_es_gerente_filial(DocSys.vl_user);

		public e_buscar_excepcion(DataAccess _da)
		{
			InitializeComponent();
			this.da = _da;
		}

		private void e_buscar_excepcion_Load(object sender, EventArgs e)
		{

		}

		private void moverForm()
		{
			ReleaseCapture();
			SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
		}

		private void button_ejecutar_Click(object sender, EventArgs e)
		{
			if (txtTexto_busqueda.Text.Length <= 1)
			{
				MessageBox.Show("Debe ingresar el texto a buscar ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			this.RealizarBusqueda();
		}

		private void RealizarBusqueda()
		{
			int index = Convert.ToInt32(this.comboBoxCampos.SelectedIndex.ToString());
			int valorNumero = 0;
			bool esNumero = int.TryParse(this.txtTexto_busqueda.Text, out valorNumero);
			string condicion = string.Empty;
			bool ejecutar = true;
			string abierta = string.Empty;

			switch (index)
			{
				case (int)Filtros.CodigoExcepcion:
					{
						if (!esNumero)
						{
							ejecutar = false;
						}
						else
						{
							condicion = " and e.codigo_excepcion = " + valorNumero + " ";
						}
					}
					break;
				case (int)Filtros.NoSolicitud:
					{
						if (!esNumero)
						{
							ejecutar = false;
						}
						else
						{
							condicion = " and e.no_solicitud = " + valorNumero + " ";
						}
					}
					break;
				case (int)Filtros.CodigoCliente:
					{
						if (!esNumero)
						{
							ejecutar = false;
						}
						else
						{
							condicion = " and s.codigo_cliente = " + valorNumero + " ";
						}
					}
					break;
				case (int)Filtros.Nombre:
					{
						condicion = " and upper(trim(c.nombres) ||' '||trim(c.primer_apellido)||' '||trim(c.segundo_apellido)) like '%" + this.txtTexto_busqueda.Text.ToUpper() + "%' ";
					}
					break;
				case (int)Filtros.Filial:
					condicion = " and upper(f.nombre_agencia) like '%" + this.txtTexto_busqueda.Text.ToUpper() + "%' ";
					break;
				case (int)Filtros.OficialServicio:
					condicion = " and e.oficial_servicios like '%" + this.txtTexto_busqueda.Text.ToUpper() + "%' ";
					break;
				default:
					break;
			}

			if(!ejecutar)
			{
				MessageBox.Show("Debe ingresar un número");
				return;
			}

			if (this.radioButton_abiertas.Checked)
				abierta = " and e.abierta = 'S' ";

			if (this.radioButton_cerradas.Checked)
				abierta = " and e.abierta = 'N' ";

			if (this.radioButton_todas.Checked)
				abierta = string.Empty;

			this.dgvExcepciones.DataSource = null;
			this.dgvExcepciones.DataSource = this.da.BuscarExcepciones(condicion, abierta);

		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void dgvExcepciones_DoubleClick(object sender, EventArgs e)
		{
			if(this.dgvExcepciones.RowCount > 0)
			{
				DataGridViewRow row = this.dgvExcepciones.CurrentRow;
				int codigoExcepcion = int.Parse(row.Cells["codigo_excepcion"].Value.ToString());
				e_excepcion_mov forma = new e_excepcion_mov(codigoExcepcion, this.da);
				forma.ShowDialog();
			}
		}
	}
}
