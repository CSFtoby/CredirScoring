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
    public partial class s_analisis_cuantitativo03 : Form
    {
        public s_analisis_cuantitativo03()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void s_analisis_cuantitativo03_Load(object sender, EventArgs e)
        {
            txtMonto_cobro.Focus();
        }

        private void txtMonto_cobro_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox texbox = txtMonto_cobro;
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }
            // al dar enter pasar al siguiente campo
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{TAB}");
            }
            //Si el texto esta sombreado todo caer encima al dato del textbox
            if (texbox.SelectionLength >= texbox.Text.Length)
            {
                texbox.Text = "";
            }

            //Para obligar a que sólo se introduzcan números 
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {

                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan 
                    e.Handled = true;
                }
            }
            //Permitir el punto decimal
            bool IsDec = false;
            int nroDec = 0;
            for (int i = 0; i < texbox.Text.Length; i++)
            {
                if (texbox.Text[i] == '.')
                    IsDec = true;

                if (IsDec && nroDec++ >= 2)
                {
                    e.Handled = true;
                    return;
                }
            }
            if (e.KeyChar >= 48 && e.KeyChar <= 57)
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = (IsDec) ? true : false;
            else
                e.Handled = true;
        }

        private void txtMonto_cobro_MouseLeave(object sender, EventArgs e)
        {
            TextBox texbox = txtMonto_cobro;
            double monto = 0;
            if (!string.IsNullOrEmpty(texbox.Text))
                monto = Convert.ToDouble(texbox.Text);

            texbox.Text = string.Format(String.Format("{0:#,###,###,##0.00}", monto));
        }
    }
}
