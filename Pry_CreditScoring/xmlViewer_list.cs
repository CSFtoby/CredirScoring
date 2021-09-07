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
    public partial class xmlViewer_list : Form
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
        private const int CS_DROPSHADOW = 0x00020000;
        protected override CreateParams CreateParams
        {
            get
            {
                // add the drop shadow flag for automatically drawing
                // a drop shadow around the form
                CreateParams cp = base.CreateParams;


                if (s_login.ventana_con_borde)
                {
                    cp.Style |= 0x40000 | CS_DROPSHADOW;
                }
                else
                {
                    cp.ClassStyle |= CS_DROPSHADOW;
                }
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
        #endregion

        public DataAccess da;
        public xmlViewer_list()
        {
            InitializeComponent();
            
        }
        private void xmlViewer_list_Load(object sender, EventArgs e)
        {
            llenar_listaxml();
        }
        private void llCerrar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }        
        private void llenar_listaxml()
        {
            gvListaXMLs.AutoGenerateColumns = false;
            gvListaXMLs.DataSource = da.ObtenerListaXMLxSolicitud(Int32.Parse(labelNo_solicitud.Text));
            gvListaXMLs.Refresh();                
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string xmlRespuesta = "";
            DataGridViewRow row = gvListaXMLs.CurrentRow;
            if (row != null)
            {
                xmlRespuesta = row.Cells["xml_respuesta"].Value.ToString();
                xmlViewer forma = new xmlViewer(xmlRespuesta);
                forma.da = da;
                forma.p_solicitud = Int32.Parse(labelNo_solicitud.Text);
                forma.ShowDialog();

            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string xmlenviado = "";
            DataGridViewRow row = gvListaXMLs.CurrentRow;
            if (row != null)
            {
                xmlenviado = row.Cells["xml_enviado"].Value.ToString();
                xmlViewer forma = new xmlViewer(xmlenviado);
                forma.da = da;
                forma.p_solicitud = Int32.Parse(labelNo_solicitud.Text);
                forma.ShowDialog();
                
            }
            
        }
    }
}
