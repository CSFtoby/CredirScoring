using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using wfcModel;

namespace Docsis_Application
{
    public partial class s_calcliquidacion_doc : Form
    {
        bool con_borde = MDI_Menu.con_borde;

        #region Mover
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
        #region Sombra
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

        public DataAccess da;
        public s_calcliquidacion_doc()
        {
            InitializeComponent();
        }

        private void s_calcliquidacion_doc_Load(object sender, EventArgs e)
        {
            p_llenar_combo_sub_aplicaciones();
        }

        private void p_llenar_combo_sub_aplicaciones()
        {
            string cod_fondo_mg = da.ObtenerConversionFondoMG("1");
            try
            {

                var dt = da.ObtenerSubAplicaciones("MCR", cod_fondo_mg);
                comboBox_sub_aplicacion.DataSource = dt;
                comboBox_sub_aplicacion.DisplayMember = "desc_sub_aplicacion";
                comboBox_sub_aplicacion.ValueMember = "codigo_sub_aplicacion";

                comboBox_sub_aplicacion_SelectionChangeCommitted(null, null);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en llenar combo_decisiones : " + ex.Message);
            }
        }
        private void p_calcular_cuota_nivelada()
        {
            float monto = 0;
            decimal tasa = 0;
            float plazo = 0;
            try
            {
                monto = float.Parse(txtMonto_solicitado.Text);
                tasa = decimal.Parse(txtTasa.Text);
                plazo = float.Parse(txtPlazo.Text);
            }
            catch
            {
                return;
            }
            if (monto == 0 | tasa == 0 | plazo == 0)
            {
                txtCuota_nivelada.Text = "0.00";
                return;
            }

            int vl_codigo_sub_app = 0;
            try
            {
                vl_codigo_sub_app = int.Parse(txtCodigo_sub_aplicacion.Text);
            }
            catch
            {
                MessageBox.Show("no se pudo obtener la cuota");
            }
            txtCuota_nivelada.Text = da.ObtenerCuotaNivela(vl_codigo_sub_app,monto, tasa, plazo).ToString("#,###,###,##0.00");
        }
        private void comboBox_sub_aplicacion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int vl_codigo_sub_aplicacion = 0;
            txtCodigo_sub_aplicacion.Text = comboBox_sub_aplicacion.SelectedValue.ToString();
            int.TryParse(txtCodigo_sub_aplicacion.Text, out vl_codigo_sub_aplicacion);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTasa_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox texbox = txtTasa;
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
        private void txtPlazo_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPlazo.Text))
            {
                txtPlazo.Text = "0";
                return;
            }
            p_calcular_cuota_nivelada();
        }
        private void txtCuota_aportacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox texbox = txtCuota_aportacion;
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
        private void txtCoopsalud_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox texbox = txtCoopsalud;
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
        private void txtConsolidacion_coopsafa_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox texbox = txtConsolidacion_coopsafa;
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
        private void txtConsolidacion_coopsafa_Leave(object sender, EventArgs e)
        {
            TextBox texbox = txtConsolidacion_coopsafa;
            double valor_ingresado = 0;
            if (!string.IsNullOrEmpty(texbox.Text))
                valor_ingresado = Convert.ToDouble(texbox.Text);

            texbox.Text = string.Format(String.Format("{0:#,###,###,##0.00}", valor_ingresado));
        }
        private void txtConsolidacion_otros_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox texbox = txtConsolidacion_otros;
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
        private void txtMonto_solicitado_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox texbox = txtMonto_solicitado;
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
        private void txtPago_terceros_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox texbox = txtPago_terceros;
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
        private void txtAvaluo_final_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox texbox = txtAvaluo_final;
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
        private void txtComplemento_aportaciones_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox texbox = txtComplemento_aportaciones;
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

        private void txtMonto_solicitado_Leave(object sender, EventArgs e)
        {
            TextBox texbox = txtMonto_solicitado;
            double valor_ingresado = 0;
            if (!string.IsNullOrEmpty(texbox.Text))
                valor_ingresado = Convert.ToDouble(texbox.Text);

            texbox.Text = string.Format(String.Format("{0:#,###,###,##0.00}", valor_ingresado));

            p_calcular_cuota_nivelada();
        }
        private void txtConsolidacion_otros_Leave(object sender, EventArgs e)
        {
            TextBox texbox = txtConsolidacion_otros;
            double valor_ingresado = 0;
            if (!string.IsNullOrEmpty(texbox.Text))
                valor_ingresado = Convert.ToDouble(texbox.Text);

            texbox.Text = string.Format(String.Format("{0:#,###,###,##0.00}", valor_ingresado));
        }
        private void txtPago_terceros_Leave(object sender, EventArgs e)
        {
            TextBox texbox = txtPago_terceros;
            double valor_ingresado = 0;
            if (!string.IsNullOrEmpty(texbox.Text))
                valor_ingresado = Convert.ToDouble(texbox.Text);

            texbox.Text = string.Format(String.Format("{0:#,###,###,##0.00}", valor_ingresado));
        }
        private void txtComplemento_aportaciones_Leave(object sender, EventArgs e)
        {
            TextBox texbox = txtComplemento_aportaciones;
            double valor_ingresado = 0;
            if (!string.IsNullOrEmpty(texbox.Text))
                valor_ingresado = Convert.ToDouble(texbox.Text);

            texbox.Text = string.Format(String.Format("{0:#,###,###,##0.00}", valor_ingresado));
        }
        private void txtAvaluo_final_Leave(object sender, EventArgs e)
        {
            TextBox texbox = txtAvaluo_final;
            double valor_ingresado = 0;
            if (!string.IsNullOrEmpty(texbox.Text))
                valor_ingresado = Convert.ToDouble(texbox.Text);

            texbox.Text = string.Format(String.Format("{0:#,###,###,##0.00}", valor_ingresado));
        }
        private void soloenteros_KeyPress(object sender, KeyPressEventArgs e)
        {
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
        }
        private void txtTasa_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTasa.Text))
            {
                txtTasa.Text = "0";
                return;
            }
            p_calcular_cuota_nivelada();
        }
        private void txtCuota_aportacion_Leave(object sender, EventArgs e)
        {
            TextBox texbox = txtCuota_aportacion;
            double valor_ingresado = 0;
            if (!string.IsNullOrEmpty(texbox.Text))
                valor_ingresado = Convert.ToDouble(texbox.Text);

            texbox.Text = string.Format(String.Format("{0:#,###,###,##0.00}", valor_ingresado));
        }
        private void txtCoopsalud_Leave(object sender, EventArgs e)
        {
            TextBox texbox = txtCoopsalud;
            double valor_ingresado = 0;
            if (!string.IsNullOrEmpty(texbox.Text))
                valor_ingresado = Convert.ToDouble(texbox.Text);

            texbox.Text = string.Format(String.Format("{0:#,###,###,##0.00}", valor_ingresado));
        }

        private void txtMejoras_avaluo_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox texbox = txtMejoras_avaluo;
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
        private void txtMejoras_avaluo_Leave(object sender, EventArgs e)
        {
            TextBox texbox = txtMejoras_avaluo;
            double valor_ingresado = 0;
            if (!string.IsNullOrEmpty(texbox.Text))
                valor_ingresado = Convert.ToDouble(texbox.Text);

            texbox.Text = string.Format(String.Format("{0:#,###,###,##0.00}", valor_ingresado));
        }
        private void btnCarlcularLiq_Click(object sender, EventArgs e)
        {
            if (float.Parse(txtMonto_solicitado.Text) <= 0)
            {
                MessageBox.Show("Debe indicar el monto solicitado..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (int.Parse(txtPlazo.Text) <= 0)
            {
                MessageBox.Show("Debe indicar el plazo solicitado..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (float.Parse(txtTasa.Text) <= 0)
            {
                MessageBox.Show("Debe indicar la taza solicitada..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtEdad.Text))
            {
                MessageBox.Show("Debe indicar la edad del solicitante..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string vl_toma_seguro = "N";
            if (cbSeguro_vida.Checked)
            {
                vl_toma_seguro = "S";
            }

            int vl_codigo_sub_app = 0;
            try
            {
                vl_codigo_sub_app = int.Parse(txtCodigo_sub_aplicacion.Text);
            }
            catch
            {
                MessageBox.Show("no se pudo obtener la cuota");
            }
            txtCuota_nivelada.Text = da.ObtenerCuotaNivela(vl_codigo_sub_app,float.Parse(txtMonto_solicitado.Text), decimal.Parse(txtTasa.Text), float.Parse(txtPlazo.Text)).ToString();

            DataTable dtLiqui = da.ObtenerValoresLiquidacion(0,int.Parse(txtCodigo_sub_aplicacion.Text), null, 0, int.Parse(txtEdad.Text), int.Parse(txtPlazo.Text), vl_toma_seguro, double.Parse(txtHonorarios_compra_venta.Text), double.Parse(txtMejoras_avaluo.Text),
                                double.Parse(txtMonto_solicitado.Text), double.Parse(txtCuota_nivelada.Text), double.Parse(txtCuota_aportacion.Text), double.Parse(txtCoopsalud.Text),double.Parse(txtTasa.Text), 0.0);

            if (dtLiqui.Rows.Count > 0)
            {
                txtTimbres_cooperativos.Text = string.Format("{0:#,###,###,##0.00}", Convert.ToDecimal(dtLiqui.Rows[0]["timbres_cooperativos"].ToString()));
                txtCapitalizacion.Text = string.Format("{0:#,###,###,##0.00}", Convert.ToDecimal(dtLiqui.Rows[0]["capitalizacion"].ToString()));
                txtCuota_anticipada.Text = string.Format("{0:#,###,###,##0.00}", Convert.ToDecimal(dtLiqui.Rows[0]["cuota_anticipada"].ToString()));
                txtSeguro_danos.Text = string.Format("{0:#,###,###,##0.00}", Convert.ToDecimal(dtLiqui.Rows[0]["seguro_danos"].ToString()));
                txtSeguro_vida.Text = string.Format("{0:#,###,###,##0.00}", Convert.ToDecimal(dtLiqui.Rows[0]["seguro_vida"].ToString()));
                txtSeguro_incendio.Text = string.Format("{0:#,###,###,##0.00}", Convert.ToDecimal(dtLiqui.Rows[0]["seguro_incendios"].ToString()));
                txtGastos_administrativos.Text = string.Format("{0:#,###,###,##0.00}", Convert.ToDecimal(dtLiqui.Rows[0]["gastos_administrativos"].ToString()));
                txtCentral_riegos.Text = string.Format("{0:#,###,###,##0.00}", Convert.ToDecimal(dtLiqui.Rows[0]["central_riesgos"].ToString()));
                txtPapeleria.Text = string.Format("{0:#,###,###,##0.00}", Convert.ToDecimal(dtLiqui.Rows[0]["papeleria"].ToString()));

                float vl_segundo_vida_mensual = 0;
                float.TryParse(dtLiqui.Rows[0]["seguro_vida_mensual"].ToString(), out vl_segundo_vida_mensual);
                txtSeguro_vida_mens.Text = string.Format("{0:#,###,###,##0.00}", vl_segundo_vida_mensual);

                float vl_seguro_danos_mens = 0;
                float.TryParse(dtLiqui.Rows[0]["seguro_danos_mensual"].ToString(), out vl_seguro_danos_mens);
                txtSeguro_danos_mens.Text = string.Format("{0:#,###,###,##0.00}", vl_seguro_danos_mens);

                float vl_segundo_incendios_mens = 0;
                float.TryParse(dtLiqui.Rows[0]["seguro_incendios_mensual"].ToString(), out vl_segundo_incendios_mens);
                txtSeguro_incendios_mens.Text = string.Format("{0:#,###,###,##0.00}", vl_segundo_incendios_mens);

                float vl_cuota_total = 0;
                float.TryParse(dtLiqui.Rows[0]["cuota_total"].ToString(), out vl_cuota_total);
                txtCuota_total.Text = string.Format("{0:#,###,###,##0.00}", vl_cuota_total);



                decimal vl_cuota_anticipada = decimal.Parse(txtCuota_anticipada.Text);
                decimal vl_consolidacion_coopsafa = decimal.Parse(txtConsolidacion_coopsafa.Text);
                decimal vl_consolidacion_otros = decimal.Parse(txtConsolidacion_otros.Text);
                decimal vl_pago_terceros = decimal.Parse(txtPago_terceros.Text);
                decimal vl_complemento_aportaciones = decimal.Parse(txtComplemento_aportaciones.Text);
                decimal vl_timbres_cooperativos = decimal.Parse(txtTimbres_cooperativos.Text);
                decimal vl_honorarios_hipoteca = decimal.Parse(txtHonorarios_hipoteca.Text);
                decimal vl_honotarios_compra_venta = decimal.Parse(txtHonorarios_compra_venta.Text);


                decimal vl_capitalizacion = decimal.Parse(txtCapitalizacion.Text);
                decimal vl_seguro_vida = decimal.Parse(txtSeguro_vida.Text);
                decimal vl_seguro_danos = decimal.Parse(txtSeguro_danos.Text);
                decimal vl_seguro_incendios = decimal.Parse(txtSeguro_incendio.Text);
                decimal vl_gastos_administrativos = decimal.Parse(txtGastos_administrativos.Text);
                decimal vl_central_riesgos = decimal.Parse(txtCentral_riegos.Text);
                decimal vl_papeleria = decimal.Parse(txtPapeleria.Text);
                decimal vl_avaluo_final = decimal.Parse(txtAvaluo_final.Text);

                decimal vl_monto = decimal.Parse(txtMonto_solicitado.Text);
                decimal vl_deducciones = vl_cuota_anticipada + vl_consolidacion_coopsafa + vl_consolidacion_otros + vl_pago_terceros + vl_complemento_aportaciones + vl_timbres_cooperativos + vl_honorarios_hipoteca + vl_honotarios_compra_venta +
                                         vl_capitalizacion + vl_seguro_vida + vl_seguro_danos + vl_seguro_incendios + vl_gastos_administrativos + vl_papeleria + vl_avaluo_final;
                decimal vl_neto_recibir = vl_monto - vl_deducciones;

                txtTotal_deducciones.Text = string.Format(String.Format("{0:#,###,###,##0.00}", vl_deducciones));
                txtNeto_recibir.Text = string.Format(String.Format("{0:#,###,###,##0.00}", vl_neto_recibir));
            }
        }
        private void txtHonorarios_compra_venta_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox texbox = txtHonorarios_compra_venta;
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
        private void txtHonorarios_compra_venta_Leave(object sender, EventArgs e)
        {
            TextBox texbox = txtHonorarios_compra_venta;
            double valor_ingresado = 0;
            if (!string.IsNullOrEmpty(texbox.Text))
                valor_ingresado = Convert.ToDouble(texbox.Text);

            texbox.Text = string.Format(String.Format("{0:#,###,###,##0.00}", valor_ingresado));
        }

    }
}
