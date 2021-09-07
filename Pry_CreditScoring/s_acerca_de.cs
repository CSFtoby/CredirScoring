using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace Docsis_Application
{
    public partial class s_acerca_de : Form
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

        private const int cGrip = 10;      // Grip size
        private const int cCaption = 5;   // Caption bar height;

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
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rc = new Rectangle(this.ClientSize.Width - cGrip, this.ClientSize.Height - cGrip, cGrip, cGrip);
            ControlPaint.DrawSizeGrip(e.Graphics, this.BackColor, rc);
            rc = new Rectangle(0, 0, this.ClientSize.Width, cCaption);
            e.Graphics.FillRectangle(Brushes.DarkBlue, rc);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {  // Trap WM_NCHITTEST
                Point pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);
                pos = this.PointToClient(pos);
                if (pos.Y < cCaption)
                {
                    m.Result = (IntPtr)2;  // HTCAPTION
                    return;
                }
                if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)17; // HTBOTTOMRIGHT
                    return;
                }
            }
            base.WndProc(ref m);
        }

        public s_acerca_de()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }   
             
        private void s_acerca_de_Load(object sender, EventArgs e)
        {
            txtNombre_pc.Text = Environment.MachineName;
            txtDominio.Text = Environment.UserDomainName;
            obtener_Ip();
        }

        public void obtener_Ip()
        {
            string strHostName = string.Empty;
            // Getting Ip address of local machine…
            // First get the host name of local machine.
            strHostName = Dns.GetHostName();
            // Then using host name, get the IP address list..
            IPAddress[] hostIPs = Dns.GetHostAddresses(strHostName);
            for (int i = 0; i < hostIPs.Length; i++)
            {
                txtIp.Text = hostIPs[i].ToString();
            }
        }

        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }

        private void button_Ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel4_MouseMove(object sender, MouseEventArgs e)
        {
            moverForm();
        }

        private void TimeBar_Tick(object sender, EventArgs e)
        {
            labelRelojPanel.Text = DateTime.Now.ToString("hh:mm");
            labelDiaPanel.Text = DateTime.Now.ToString("dddd");
            labelFechaPanel.Text = DateTime.Now.ToString("MMMM") + " " + DateTime.Now.ToString("dd");
        }

        private void pictureBox3_Click(object sender, EventArgs e){}
    }
}
