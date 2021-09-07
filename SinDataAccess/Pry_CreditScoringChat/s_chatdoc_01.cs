using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NotificacionesDll;


namespace Pry_CreditScoringChat
{
    public partial class s_chatdoc_01 : Form
    {
         
        bool ventana_con_borde = true;
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


                if (ventana_con_borde)
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

        string persona_anterior = "";
        public s_chatdoc_01()
        {
            InitializeComponent();
        }
        void concatenarRichText(RichTextBox box, Color bkColor, Font fuente, Color ftColor, string texto, string origen)
        {//Con este metodo añado texto a un RichTextBox con un color deseado para esa seccion de texto
            int start = box.TextLength;// obtengo la longitu del texto contenido en el RichTextoBOx ANTES de agregarle nuevo texto

            box.AppendText("\r\n" + texto); // Agrego el nuevo texto            
            if (origen=="yo2" || origen=="otro2")
            {
                box.AppendText("\r\n" + " "); // nueva linea cuando se adjunta texto de la otra parte                 
            }

            int end = box.TextLength;// obtengo la longitu del texto contenido en el RichTextoBOx DESPUES de agregarle nuevo texto, para poder obtener luego el rango del nuevo texto

            // restando end - start obtengo el punto donde inició el texto recien agregado
            box.Select(start, end - start);
            {
                box.SelectionColor = ftColor;
                box.SelectionBackColor = bkColor;
                box.SelectionHangingIndent = 50;
                box.SelectionFont = fuente;
                //box.SelectionRightIndent = 10;
                if (origen.ToLower() == "yo1" || origen.ToLower() == "yo2")
                    box.SelectionAlignment = HorizontalAlignment.Right;
                    
                else
                    box.SelectionAlignment = HorizontalAlignment.Left;

            }
            box.SelectionLength = 0; // limpio
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
                return;

            Font fuente1 = new System.Drawing.Font("Arial", 10, FontStyle.Regular);
            Font fuente2 = new System.Drawing.Font("Arial", 7, FontStyle.Regular);
            Color bk1 = Color.FromArgb(227, 255, 202);
            

            concatenarRichText(richTextBox1, bk1, fuente1, Color.Black, textBox1.Text, "yo1");
            concatenarRichText(richTextBox1, bk1, fuente2, Color.Gray, "           " + DateTime.Now.ToString(), "yo2");
            persona_anterior = "yo";
        }
        private void button2_Click(object sender, EventArgs e)
        {

            Font fuente1 = new System.Drawing.Font("Arial", 9, FontStyle.Regular);
            Font fuente2 = new System.Drawing.Font("Arial", 7, FontStyle.Regular);
            Color bk1 = Color.White;

            concatenarRichText(richTextBox1, bk1, fuente1, Color.Black, " Soy Parte 2          ", "otro1");
            concatenarRichText(richTextBox1, bk1, fuente2, Color.Gray, "           " + DateTime.Now.ToString(), "otro2");
            persona_anterior = "otro";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void s_chatdoc_01_Load(object sender, EventArgs e)
        {
            
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            NotificacionesDll.Notificaciones.mostrar_toast_style1(5000);
            Notificaciones.show_Toast(2000, "Solicitud actualizada satisfactoriamente !");

            
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }

    }
}
