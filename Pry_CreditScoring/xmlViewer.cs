using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using wfcModel;

namespace Docsis_Application
{
    public partial class xmlViewer : Form
    {
        public DataAccess da;
        public Int32 p_solicitud = 0;
        string xml;

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
        #endregion
        #region variables


        private const int cGrip = 10;      // Grip size
        private const int cCaption = 1;   // Caption bar height;
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
        #endregion

        
        public xmlViewer(string xl_input)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            xml = xl_input;
        }
        private void xmlViewer_Load(object sender, EventArgs e)
        {

            
            if (xml.Length == 0)
            {
                string xml_enviado_basedatos = da.ObtenerXmlxSolicitud(p_solicitud);
                if (string.IsNullOrEmpty(xml_enviado_basedatos))
                {
                    labelVentana.Text = "XMLReader - Construido";
                    xml_enviado_basedatos = xml;
                }
                else
                {
                    labelVentana.Text = "XMLReader - Base de datos";
                }
                xmlRichTextBox.Text = PrintXML(xml_enviado_basedatos);
            }
            else
            {                
                procesar_xml();
            }
        }


        private void procesar_xml()
        {
            try
            {
                XmlTextReader reader = new XmlTextReader(new System.IO.StringReader(xml));
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element: // The node is an element.
                            this.xmlRichTextBox.SelectionColor = Color.Blue;
                            this.xmlRichTextBox.AppendText("  ");
                            this.xmlRichTextBox.AppendText("<");
                            this.xmlRichTextBox.SelectionColor = Color.Brown;
                            this.xmlRichTextBox.AppendText(reader.Name);
                            this.xmlRichTextBox.SelectionColor = Color.Blue;
                            this.xmlRichTextBox.AppendText(">");

                            //if (reader.Name == "Request" | reader.Name == "Authentication" | reader.Name == "DCRequest" | reader.Name == "Input" | reader.Name == "Single" | reader.Name == "Multi")
                            //    this.xmlRichTextBox.AppendText("\n");
                            break;
                        case XmlNodeType.Text: //Display the text in each element.
                            this.xmlRichTextBox.SelectionColor = Color.Black;
                            this.xmlRichTextBox.AppendText(reader.Value);
                            break;
                        case XmlNodeType.EndElement: //Display the end of the element.
                            this.xmlRichTextBox.SelectionColor = Color.Blue;
                            this.xmlRichTextBox.AppendText("</");
                            this.xmlRichTextBox.SelectionColor = Color.Brown;
                            this.xmlRichTextBox.AppendText(reader.Name);
                            this.xmlRichTextBox.SelectionColor = Color.Blue;
                            this.xmlRichTextBox.AppendText(">");
                            this.xmlRichTextBox.AppendText("\n");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            xmlRichTextBox.Text = PrintXML(xml);
            return;
            //MessageBox.Show( PrintXML(xml));
        }
        string FormatXml(string xml)
        {
            try
            {
                XDocument doc = XDocument.Parse(xml);
                return doc.ToString();
            }
            catch (Exception)
            {
                return xml;
            }
        }
        public static String PrintXML(String XML)
        {
            String Result = "";

            MemoryStream mStream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(mStream, Encoding.Unicode);
            XmlDocument document = new XmlDocument();

            try
            {
                // Load the XmlDocument with the XML.
                document.LoadXml(XML);

                writer.Formatting = Formatting.Indented;

                // Write the XML into a formatting XmlTextWriter
                document.WriteContentTo(writer);
                writer.Flush();
                mStream.Flush();

                // Have to rewind the MemoryStream in order to read
                // its contents.
                mStream.Position = 0;

                // Read MemoryStream contents into a StreamReader.
                StreamReader sReader = new StreamReader(mStream);

                // Extract the text from the StreamReader.
                String FormattedXML = sReader.ReadToEnd();

                Result = FormattedXML;
            }
            catch (XmlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            //mStream.Close();
            //writer.Close();

            return Result;
        }
        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }
        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {

        }
        private void button9_Click(object sender, EventArgs e)
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


    }
}
