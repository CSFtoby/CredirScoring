using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wfcModel.MGI.SubAplicaciones;

namespace Docsis_Application.FormsInformativo
{
    public partial class s_infoproducto : Form
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
        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }
        #endregion
        #region
        bool con_borde = MDI_Menu.con_borde;
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

        CR_TIPO_PRESTAMO cr_tipo_prestamo;
        public s_infoproducto(CR_TIPO_PRESTAMO cr_tipo_prestamo)
        {
            InitializeComponent();
            this.cr_tipo_prestamo = cr_tipo_prestamo;

        }

        private void s_infoproducto_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Control.MousePosition.X - 105, Control.MousePosition.Y + 10);
            txtCodigo_sub_app.Text = cr_tipo_prestamo.Cod_tipopres.ToString();
            txtDesc_sub_app.Text = cr_tipo_prestamo.desc_tipopres;
            

            txtNum_plazoomision.Text = cr_tipo_prestamo.Num_mesesplazo_base.ToString("##0");
            txtNum_plazominimo.Text = cr_tipo_prestamo.Num_mesesplazo_min.ToString("##0");
            txtNum_plazomaximo.Text = cr_tipo_prestamo.Num_mesesplazo.ToString("##0");

            txtPor_tasaminima.Text = cr_tipo_prestamo.Por_tasaminima.ToString("##0.##")+"%";
            txtPor_tasamaxima.Text = cr_tipo_prestamo.Por_tasamaxima.ToString("##0.##") + "%";
            txtPor_tasaomision.Text = cr_tipo_prestamo.Por_tasaomision.ToString("##0.##") + "%";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }

        private void lLCerrar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            btnClose_Click(null, null);
        }
    }
}
