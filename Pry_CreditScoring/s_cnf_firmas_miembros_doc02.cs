using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Docsis_Application
{
    public partial class s_cnf_firmas_miembros_doc02 : Form
    {

        Bitmap sign, ok, clear, please;
        int lcdX, lcdY, screen;
        uint lcdSize;
        string data, data2;
        public Image ImgfirmaHecha;

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

        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);
        public static void alzheimer()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
        }
        public s_cnf_firmas_miembros_doc02()
        {
            InitializeComponent();
            this.sigPlusNET1.PenUp += new System.EventHandler(this.sigPlusNET1_PenUp);
        }

        private void s_cnf_firmas_miembros_doc02_Load(object sender, EventArgs e)
        {
            if (sigPlusNET1.TabletConnectQuery() == false)
            {

                MessageBox.Show("No hay conectadas pizarras de firmas", "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                this.Close();
            }
            inicializar_display();
        }


        private void inicializar_display()
        {
            sign = new System.Drawing.Bitmap(Application.StartupPath + "\\ImgTopaz\\Sign.bmp");
            ok = new System.Drawing.Bitmap(Application.StartupPath + "\\ImgTopaz\\OK.bmp");
            clear = new System.Drawing.Bitmap(Application.StartupPath + "\\ImgTopaz\\CLEAR.bmp");
            please = new System.Drawing.Bitmap(Application.StartupPath + "\\ImgTopaz\\please.bmp");

            sigPlusNET1.SetTabletState(1); //Turns tablet on to collect signature
            sigPlusNET1.LCDRefresh(0, 0, 0, 240, 64);
            sigPlusNET1.SetTranslateBitmapEnable(false);

            //Images sent to the background
            sigPlusNET1.LCDSendGraphic(1, 2, 0, 20, sign);
            sigPlusNET1.LCDSendGraphic(1, 2, 207, 4, ok);
            sigPlusNET1.LCDSendGraphic(1, 2, 15, 4, clear);

            //Get LCD size in pixels.
            lcdSize = sigPlusNET1.LCDGetLCDSize();
            lcdX = (int)(lcdSize & 0xFFFF);
            lcdY = (int)((lcdSize >> 16) & 0xFFFF);

            Font f = new System.Drawing.Font("Arial", 9.0F, System.Drawing.FontStyle.Regular);
            data = "Bienvenido al sistema de registro de firmas, Cooperativa Sagrada Familia, ";
            string[] words = data.Split(new char[] { ' ' });
            string writeData = "", tempData = "";
            int xSize, ySize, i, yPos = 0;
            for (i = 0; i < words.Length; i++)
            {
                tempData += words[i];
                xSize = sigPlusNET1.LCDStringWidth(f, tempData);
                if (xSize < lcdX)
                {
                    writeData = tempData;
                    tempData += " ";

                    xSize = sigPlusNET1.LCDStringWidth(f, tempData);

                    if (xSize < lcdX)
                    {
                        writeData = tempData;
                    }
                }
                else
                {
                    ySize = sigPlusNET1.LCDStringHeight(f, tempData);

                    sigPlusNET1.LCDWriteString(0, 2, 0, yPos, f, writeData);

                    tempData = "";
                    writeData = "";
                    yPos += (short)ySize;
                    i--;
                }
            }

            if (writeData != "")
            {
                sigPlusNET1.LCDWriteString(0, 2, 0, yPos, f, writeData);
            }

            ////Hotspot text
            sigPlusNET1.LCDWriteString(0, 2, 15, 45, f, "Ok");


            ////Create the hot spots for the Continue and Exit buttons
            sigPlusNET1.KeyPadAddHotSpot(0, 1, 15, 46, 30, 15); //For Continue button



            sigPlusNET1.ClearTablet();

            sigPlusNET1.LCDSetWindow(0, 0, 1, 1);
            sigPlusNET1.SetSigWindow(1, 0, 0, 1, 1); //Sets the area where ink is permitted in the SigPlus object
            sigPlusNET1.SetLCDCaptureMode(2);   //Sets mode so ink will not disappear after a few seconds

            screen = 1;
        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            sigPlusNET1.ClearSigWindow(1);

            sigPlusNET1.LCDRefresh(1, 210, 3, 14, 14); //Refresh LCD at 'OK' to indicate to user that this option has been sucessfully chosen
            if (sigPlusNET1.NumberOfTabletPoints() > 0)
            {
                sigPlusNET1.LCDRefresh(0, 0, 0, 240, 64);
                Font f = new System.Drawing.Font("Arial", 9.0F, System.Drawing.FontStyle.Regular);
                sigPlusNET1.LCDWriteString(0, 2, 35, 25, f, "Cooperativa Sagrada Familia");
                //System.Threading.Thread.Sleep(2000);
            }
            else
            {
                sigPlusNET1.LCDRefresh(0, 0, 0, 240, 64);
                sigPlusNET1.LCDSendGraphic(0, 2, 4, 20, please);
                System.Threading.Thread.Sleep(750);
                sigPlusNET1.ClearTablet();
                sigPlusNET1.LCDRefresh(2, 0, 0, 240, 64);
                sigPlusNET1.SetLCDCaptureMode(2);   //Sets mode so ink will not disappear after a few seconds
            }

            sigPlusNET1.SetImageXSize(500);
            sigPlusNET1.SetImageYSize(145);
            sigPlusNET1.SetJustifyMode(5);
            Image myimage;
            myimage = sigPlusNET1.GetSigImage();

            s_cnf_firmas_miembros_doc.firma = imageToByteArray(myimage);
            sigPlusNET1.SetJustifyMode(0);
            sigPlusNET1.SetTabletState(0);
            Dispose();
            ImgfirmaHecha = myimage;
            this.DialogResult = DialogResult.OK;
        }
        private void sigPlusNET1_PenUp(object sender, EventArgs e)
        {
            if (sigPlusNET1.KeyPadQueryHotSpot(0) > 0)//If the Continue hotspot is tapped, then...
            {
                if (screen == 1)
                {
                    sigPlusNET1.ClearSigWindow(1);
                    sigPlusNET1.LCDRefresh(1, 16, 45, 50, 15); //Refresh LCD at 'Continue' to indicate to user that this option has been sucessfully chosen
                    sigPlusNET1.ClearTablet();
                    sigPlusNET1.LCDRefresh(0, 0, 0, 240, 64);

                    //Demo text
                    Font f = new System.Drawing.Font("Arial", 9.0F, System.Drawing.FontStyle.Regular);
                    data2 = "";
                    string[] words = data2.Split(new char[] { ' ' });
                    string writeData = "", tempData = "";

                    int xSize, ySize, i, yPos = 0;

                    for (i = 0; i < words.Length; i++)
                    {
                        tempData += words[i];

                        xSize = sigPlusNET1.LCDStringWidth(f, tempData);

                        if (xSize < lcdX)
                        {
                            writeData = tempData;
                            tempData += " ";

                            xSize = sigPlusNET1.LCDStringWidth(f, tempData);

                            if (xSize < lcdX)
                            {
                                writeData = tempData;
                            }
                        }
                        else
                        {
                            ySize = sigPlusNET1.LCDStringHeight(f, tempData);

                            sigPlusNET1.LCDWriteString(0, 2, 0, yPos, f, writeData);

                            tempData = "";
                            writeData = "";
                            yPos += (short)ySize;
                            i--;
                        }
                    }

                    if (writeData != "")
                    {
                        sigPlusNET1.LCDWriteString(0, 2, 0, yPos, f, writeData);
                    }

                    sigPlusNET1.ClearSigWindow(1);
                    //sigPlusNET1.LCDRefresh(1, 16, 45, 50, 15); //Refresh LCD at 'Continue' to indicate to user that this option has been sucessfully chosen
                    sigPlusNET1.LCDRefresh(2, 0, 0, 240, 64); //Brings the background image already loaded into foreground
                    sigPlusNET1.ClearTablet();
                    sigPlusNET1.KeyPadClearHotSpotList();

                    sigPlusNET1.KeyPadAddHotSpot(2, 1, 15, 4, 35, 15); //For CLEAR button                    
                    sigPlusNET1.KeyPadAddHotSpot(5, 1, 205, 4, 30, 15); //For Continue button

                    sigPlusNET1.LCDSetWindow(2, 03, 236, 68);
                    sigPlusNET1.SetSigWindow(1, 0, 03, 240, 68); //Sets the area where ink is permitted in the SigPlus object
                }
                sigPlusNET1.SetLCDCaptureMode(2);
            }
            else if (sigPlusNET1.KeyPadQueryHotSpot(2) > 0) //If the CLEAR hotspot is tapped, then...
            {
                //buttonAceptar.Enabled = false;
                sigPlusNET1.ClearSigWindow(1);
                sigPlusNET1.LCDRefresh(1, 10, 0, 53, 17); //Refresh LCD at 'CLEAR' to indicate to user that this option has been sucessfully chosen
                sigPlusNET1.LCDRefresh(2, 0, 0, 240, 64); //Brings the background image already loaded into foreground
                sigPlusNET1.ClearTablet();
            }
            else if (sigPlusNET1.KeyPadQueryHotSpot(4) > 0) //Exit
            {
                sigPlusNET1.ClearSigWindow(1);
                sigPlusNET1.LCDRefresh(1, 200, 45, 20, 15); //Refresh (invert) LCD at 'EXIT' to indicate to user that this option has been sucessfully chosen
                sigPlusNET1.SetLCDCaptureMode(1);
                sigPlusNET1.LCDRefresh(0, 0, 0, 240, 64);

                //reset hardware
                sigPlusNET1.SetTabletState(0);
                Application.Exit();
            }
            else if (sigPlusNET1.KeyPadQueryHotSpot(5) > 0) //If the OK hotspot is tapped, then...
            {
                buttonAceptar.Enabled = true;
            }

            sigPlusNET1.ClearSigWindow(1);
        }
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sigPlusNET1.ClearSigWindow(1);
            sigPlusNET1.LCDRefresh(1, 10, 0, 53, 17); //Refresh LCD at 'CLEAR' to indicate to user that this option has been sucessfully chosen
            sigPlusNET1.LCDRefresh(2, 0, 0, 240, 64); //Brings the background image already loaded into foreground
            sigPlusNET1.ClearTablet();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.sigPlusNET1.PenUp -= new System.EventHandler(this.sigPlusNET1_PenUp);
            sigPlusNET1.Close();
            sigPlusNET1.Dispose();

            Dispose();
            sigPlusNET1.SetTabletState(0);
            sigPlusNET1.SetTabletState(0);
            sigPlusNET1.LCDRefresh(0, 0, 0, 240, 64);
            sigPlusNET1.SetTranslateBitmapEnable(false);

            sigPlusNET1.ClearSigWindow(1);

            sigPlusNET1.ClearTablet();
            sigPlusNET1.LCDRefresh(2, 0, 0, 240, 64);

            sigPlusNET1.LCDRefresh(0, 0, 0, 240, 64);
            Font f = new System.Drawing.Font("Arial", 9.0F, System.Drawing.FontStyle.Regular);
            sigPlusNET1.LCDWriteString(0, 2, 35, 25, f, " ");

            this.Close();
        }
    }
}
