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
    public partial class r_comentarios_solicitud : Form
    {

        public string ObservacionAnterior { get; set; }
        public string ObservacionNueva { get; set; }

        public r_comentarios_solicitud(string _obserAnt)
        {
            InitializeComponent();
            this.txtObservAnterior.Text = _obserAnt;
        }

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            this.ObservacionNueva = this.txtObservNueva.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
