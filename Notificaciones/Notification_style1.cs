using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NotificacionesDll
{
    public partial class Notification_style1 : TransDialog
    {
        public int intervalo_timer;
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


                if (Notificacion.con_borde)
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

        public Notification_style1(): base(true)
        {
            InitializeComponent();
        }

        #region Event handler
        private void Notification_Load(object sender, System.EventArgs e)
        {
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            this.Left = screenWidth - this.Width;
            this.Top = screenHeight - this.Height;

            timer1.Enabled = true;

            string link = "http://www.geocities.com/basuabhinaba";
            linkLabel1.Links.Add(0, link.Length, link);
            timer1.Interval = intervalo_timer;
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            string link = e.Link.LinkData.ToString();
            if (link != null && link.Length > 0)
                System.Diagnostics.Process.Start(link);
        }
        #endregion // Event handler

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Notification_MouseMove(object sender, MouseEventArgs e)
        {
            timer1.Stop();
        }
        private void Notification_MouseLeave(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
