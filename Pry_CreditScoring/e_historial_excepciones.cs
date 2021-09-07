using Docsis_Application.Excepciones;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wfcModel;

namespace Docsis_Application
{
	public partial class e_historial_excepciones : Form
	{
		DataAccess da;
		private int global_estacion_id_to;

		public e_historial_excepciones(DataAccess _da, int _global_estacion_id_to)
		{
			InitializeComponent();
			this.da = _da;
			this.global_estacion_id_to = _global_estacion_id_to;
		}

		private void e_historial_excepciones_Load(object sender, EventArgs e)
		{

		}

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtCodigoCliente.Text))
            {
                MessageBox.Show("Agregue el número de cliente", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                DataTable dtHistorial = new DataTable();
                dtHistorial = this.da.generar_historial(this.txtCodigoCliente.Text);
                //cambio
                this.dgvDetalle.Columns["estacion_id"].Visible = true;
                this.dgvDetalle.Columns["estacion_id"].HeaderText = "Estacion ID";
                this.dgvDetalle.Columns["estacion_Actual"].Visible = true;
                this.dgvDetalle.Columns["estacion_Actual"].HeaderText = "Estacion Actual";
                this.dgvDetalle.Columns["ANTIGUEDAD_MESES"].Visible = true;
                this.dgvDetalle.Columns["ANTIGUEDAD_MESES"].HeaderText = "Meses";
                this.dgvDetalle.Columns["ANTIGUEDAD_DIAS"].Visible = true;
                this.dgvDetalle.Columns["ANTIGUEDAD_DIAS"].HeaderText = "Días";
                this.dgvDetalle.Columns["ANTIGUEDAD_HORAS"].Visible = true;
                this.dgvDetalle.Columns["ANTIGUEDAD_HORAS"].HeaderText = "Horas";
                this.dgvDetalle.Columns["ANTIGUEDAD_MINUTOS"].Visible = true;
                this.dgvDetalle.Columns["ANTIGUEDAD_MINUTOS"].HeaderText = "Minutos";
                this.dgvDetalle.Columns["ANTIGUEDAD_SEGUNDOS"].Visible = true;
                this.dgvDetalle.Columns["ANTIGUEDAD_SEGUNDOS"].HeaderText = "Segundos";
                this.dgvDetalle.DataSource = dtHistorial;
            }
        }

        private void button1_Click(object sender, EventArgs e)
		{
			int vl_no_solicitud = 0;
			int vl_cod_excepcion = 0;
			if (this.dgvDetalle.RowCount > 0)
			{
				DataGridViewRow row = this.dgvDetalle.CurrentRow;
				vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());
				vl_cod_excepcion = int.Parse(row.Cells["codigo_Excepcion"].Value.ToString());

				e_nueva_excepcion_solicitud forma = new e_nueva_excepcion_solicitud(vl_no_solicitud, u_Globales.accionModificar, this.da, vl_cod_excepcion, string.Empty, global_estacion_id_to);
				forma.noFilial = global_estacion_id_to;
				forma.ShowDialog();
			}
		}

		private void button_cerrar_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
