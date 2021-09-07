using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Docsis_Application
{
    public partial class r_fmrCambioTasa : Form
    {
        public double tasaAnterior { get; set; }
        public double tasaNueva { get; set; }

        public r_fmrCambioTasa(double _tasaAnterior)
        {
            InitializeComponent();
            this.txtTasaAnterio.Text = Convert.ToString(_tasaAnterior);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            double vl_tasa = 0;
            double.TryParse(this.txttasaNueva.Text, out vl_tasa);
            this.tasaNueva = vl_tasa;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
