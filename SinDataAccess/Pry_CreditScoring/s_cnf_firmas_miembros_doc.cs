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
    public partial class s_cnf_firmas_miembros_doc : Form
    {
        public DataAccess da;
        static public byte[] foto = null;
        static public byte[] firma = null;

        #region
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
        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }
        #endregion

        public s_cnf_firmas_miembros_doc()
        {
            InitializeComponent();
        }


        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCodigo_cliente_Leave(object sender, EventArgs e)
        {
            if (txtCodigo_cliente.Text == string.Empty)
                return;
            var dtInfoEmp = da.P_Obtener_generales_empleado(txtCodigo_cliente.Text.Trim());

            if (dtInfoEmp.Rows.Count == 0)
            {
                MessageBox.Show("Codigo de empleado no encontrado..!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txtNombres.Text = dtInfoEmp.Rows[0]["nombre"].ToString();
            txtDepto.Text = dtInfoEmp.Rows[0]["descri_depto"].ToString();
            txtPuesto.Text = dtInfoEmp.Rows[0]["descri_puesto"].ToString();
            txtFilial_asignado.Text = dtInfoEmp.Rows[0]["nombre_agencia"].ToString();
            txtRegional.Text = dtInfoEmp.Rows[0]["nombre_zona"].ToString();

            byte[] phuella;
            byte[] pfoto;
            byte[] pfirma;
            int dedo = 0;

            da.ObtenerHuellaFotoUsuario(txtCodigo_cliente.Text, out dedo, out phuella, out pfoto, out pfirma);



            if (pfoto != null)
            {
                foto = pfoto;
                pbFotoemp.Image = DocSys.p_CopyDataToBitmap(foto);
            }
            else
            {
                pbFotoemp.Image = null;
            }
            if (pfirma != null)
            {
                pbFirmaEmp.Image = DocSys.p_CopyDataToBitmap(pfirma);
            }
            else
            {
                pbFirmaEmp.Image = null;
            }
        }

        private void LlTomar_firma_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            s_cnf_firmas_miembros_doc02 forma = new s_cnf_firmas_miembros_doc02();
            DialogResult nresult = forma.ShowDialog();
            if (nresult == DialogResult.OK)
            {

                pbFirmaEmp.Image = forma.ImgfirmaHecha;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            LlTomar_firma_LinkClicked(null, null);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (firma == null)
            {
                MessageBox.Show("Debe realizar la toma de la firma para poder guardarla..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtCodigo_cliente.Text))
            {
                MessageBox.Show("Debe ingresar el codigo de cliente del empleado...", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Desea guardar los cambios en la firma ?","Aviso",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.No)
            {
                return;
            }
            if (da.UpdateFirma(txtCodigo_cliente.Text, firma))
            {
                MessageBox.Show("Firma actualizada satisfactoriamente..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se ha podido almacenar la firma...", "Aviso de Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void txtCodigo_cliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txtCodigo_cliente_Leave(null, null);
                LlTomar_firma.Focus();
            }
        }
    }
}
