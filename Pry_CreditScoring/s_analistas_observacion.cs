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
    public partial class s_analistas_observacion : Form
    {
        public DataAccess da;
        bool con_borde = MDI_Menu.con_borde;
        public Int32 gno_solicitud = 0;

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

        public s_analistas_observacion()
        {
            InitializeComponent();
            

        }
        private void s_analistas_observacion_Load(object sender, EventArgs e)
        {
            var dt1 = da.ObtenerSituacionActualNEWxSolicitud(gno_solicitud);
            txtPlazo.Text = dt1.Rows[0]["plazo"].ToString();
            txtTasa.Text = dt1.Rows[0]["tasa"].ToString();
            txtTexto.Text = dt1.Rows[0]["observaciones_analista"].ToString();
            txtDestino.Text = dt1.Rows[0]["DESCRIPCION_DESTINO"].ToString();
            txtFondos.Text = dt1.Rows[0]["descripcion_fuente"].ToString();
            txtFilial.Text = dt1.Rows[0]["NOMBRE_AGENCIA"].ToString();
        }        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtTexto_KeyPress(object sender, KeyPressEventArgs e)
        {
            label_caracteres.Text = txtTexto.Text.Length.ToString() + " caracteres de " + txtTexto.MaxLength.ToString() + " permitidos";            
        }
        private void btnCarlcularLiq_Click(object sender, EventArgs e)
        {
           
            if (da.Observaciones_analista(gno_solicitud,txtTexto.Text))
            {
                NotificacionesDll.Notificacion.show_Toast(1500, "Observaciones actualizadas satisfactoriamente..!");                
                return;
            }           
        }
        private void button1_Click(object sender, EventArgs e)
        {
            FrmsRpts.frmRpt_Analistas_observaciones forma = new FrmsRpts.frmRpt_Analistas_observaciones(da);
            forma.gno_solicitud = Int32.Parse(txtNo_solicitud.Text);
            forma.ShowDialog();
        }
        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }        
    }
}
