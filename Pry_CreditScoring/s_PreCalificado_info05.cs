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
    public partial class s_PreCalificado_info05 : Form
    {
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
        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }
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
                if (MDI_Menu.con_borde)
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
        public int gdestino1 = 0;
        public int gdestino2 = 0;
        private int TipoPrestamo = 0;

        public s_PreCalificado_info05(int tipo_prestamo)
        {
            InitializeComponent();
            TipoPrestamo = tipo_prestamo;
        }
        private void s_PreCalificado_info05_Load(object sender, EventArgs e)
        {
            p_llenar_combo_destinos();
        }
        private void p_llenar_combo_destinos()
        {
            try
            {
                DataTable dtDestinos1 = da.ObtenerDestinoCredito(TipoPrestamo);
                cmbDestino_credito1.DataSource = dtDestinos1;
                cmbDestino_credito1.DisplayMember = "descripcion_destino";
                cmbDestino_credito1.ValueMember = "destino_id";


                DataTable dtDestinos2 = da.ObtenerDestinoCredito(TipoPrestamo);
                cmbDestino_credito2.DataSource = dtDestinos2;
                cmbDestino_credito2.DisplayMember = "descripcion_destino";
                cmbDestino_credito2.ValueMember = "destino_id";

                cmbDestino_credito1_SelectionChangeCommitted(null, null);
                cmbDestino_credito2_SelectionChangeCommitted(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  : " + ex.Message);
            }
        }
        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtMonto_destino1_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox texbox = txtMonto_destino1;
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
        private void txtMonto_destino1_Leave(object sender, EventArgs e)
        {
            TextBox texbox = txtMonto_destino1;
            if (texbox.Text.Trim() == ".")
            {
                texbox.Text = "0";
            }
            double ingresos = 0;
            if (!string.IsNullOrEmpty(texbox.Text))
                ingresos = Convert.ToDouble(texbox.Text);
            texbox.Text = string.Format(String.Format("{0:###,##0.00}", ingresos));
            realizar_calculos();

        }
        private void txtMonto_destino2_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox texbox = txtMonto_destino2;
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
        private void txtMonto_destino2_Leave(object sender, EventArgs e)
        {
            TextBox texbox = txtMonto_destino2;
            if (texbox.Text.Trim() == ".")
            {
                texbox.Text = "0";
            }
            double ingresos = 0;
            if (!string.IsNullOrEmpty(texbox.Text))
                ingresos = Convert.ToDouble(texbox.Text);
            texbox.Text = string.Format(String.Format("{0:###,##0.00}", ingresos));
            realizar_calculos();

        }
        private void cmbDestino_credito1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                gdestino1 = int.Parse(cmbDestino_credito1.SelectedValue.ToString());
                realizar_calculos();
            }
            catch (Exception ex)
            {
            }

        }
        private void cmbDestino_credito2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                gdestino2 = int.Parse(cmbDestino_credito2.SelectedValue.ToString());
                realizar_calculos();
            }
            catch (Exception ex)
            {
            }
        }
        private void realizar_calculos()
        {
            double vl_dummy11 = double.Parse(txtMonto_destino1.Text);
            double vl_dummy12 = double.Parse(txtMonto_solicitado.Text);
            txtPorcMontoD1.Text = Math.Round(((vl_dummy11 / vl_dummy12) * 100)).ToString("##0.00");

            DataTable dest1 = da.ObtenerTasaPlazoxDestino(gdestino1);
            if (dest1.Rows.Count > 0)
            {
                txtPlazoD1.Text = dest1.Rows[0]["plazo_destino"].ToString();
                txtTasaD1.Text = dest1.Rows[0]["tasa_destino"].ToString();
                ///
                if (!string.IsNullOrEmpty(txtPorcMontoD1.Text))
                {
                    double dummy1 = double.Parse(txtPorcMontoD1.Text);
                    double dummy2 = double.Parse(txtTasaD1.Text);
                    double dummy3 = (dummy2 * dummy1) / 100;


                    double dummy4 = double.Parse(txtPorcMontoD1.Text);
                    double dummy5 = double.Parse(txtPlazoD1.Text);
                    double dummy6 = (dummy5 * dummy4) / 100;

                    txtTasaP1.Text = dummy3.ToString("##0.####");
                    txtPlazoP1.Text = dummy6.ToString("###");
                }
            }

            double vl_dummy21 = double.Parse(txtMonto_destino2.Text);
            double vl_dummy22 = double.Parse(txtMonto_solicitado.Text);
            txtPorcMontoD2.Text = Math.Round(((vl_dummy21 / vl_dummy22) * 100)).ToString("##0.00");

            DataTable dest2 = da.ObtenerTasaPlazoxDestino(gdestino2);
            if (dest1.Rows.Count > 0)
            {
                txtPlazoD2.Text = dest2.Rows[0]["plazo_destino"].ToString();
                txtTasaD2.Text = dest2.Rows[0]["tasa_destino"].ToString();
                ///
                if (!string.IsNullOrEmpty(txtPorcMontoD2.Text))
                {
                    double dummy1 = double.Parse(txtPorcMontoD2.Text);
                    double dummy2 = double.Parse(txtTasaD2.Text);
                    double dummy3 = (dummy2 * dummy1) / 100;


                    double dummy4 = double.Parse(txtPorcMontoD2.Text);
                    double dummy5 = double.Parse(txtPlazoD2.Text);
                    double dummy6 = (dummy5 * dummy4) / 100;

                    txtTasaP2.Text = dummy3.ToString("##0.####");
                    txtPlazoP2.Text = dummy6.ToString("###");
                }
            }
            double tasa_ofrecer = 0;
            double plazo_ofrecer = 0;
            double tmpD11 = 0;
            double tmpD12 = 0;
            double tmpD21 = 0;
            double tmpD22 = 0;
            try
            {
                tmpD11 = double.Parse(txtTasaP1.Text);
                tmpD12 = double.Parse(txtPlazoP1.Text);
                tmpD21 = double.Parse(txtTasaP2.Text);
                tmpD22 = double.Parse(txtPlazoP2.Text);

                txtTasa_ofrecer.Text = (tmpD11 + tmpD21).ToString();
                txtPlazo_ofrecer.Text = (tmpD12 + tmpD22).ToString();
            }
            catch
            {

            }
        }

        private void lLPonderacion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTasa_ofrecer.Text) || string.IsNullOrEmpty(txtPlazo_ofrecer.Text))
            {
                MessageBox.Show("No ha completado el calculo...", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            double dummy0 = 0;
            double dummy1 = 0;
            double dummy2 = 0;
            double dummy3 = 0;
            try
            {
                dummy0 = double.Parse(txtMonto_solicitado.Text);
                dummy1 = double.Parse(txtMonto_destino1.Text);
                dummy2 = double.Parse(txtMonto_destino2.Text);
                dummy3 = dummy1 + dummy2;
            }
            catch
            {

            }
            if (dummy3 != dummy0)
            {
                MessageBox.Show("La suma de los destino no es igual al monto solicitado..!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
