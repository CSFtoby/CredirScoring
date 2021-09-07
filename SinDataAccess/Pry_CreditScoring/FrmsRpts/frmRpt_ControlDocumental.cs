using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using wfcModel;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace Docsis_Application.FrmsRpts
{
    public partial class frmRpt_ControlDocumental : Form
    {
        public string vl_enter = "\r\n";
        bool con_borde = MDI_Menu.con_borde;
        public Int32 gno_solicitud = 0;
        public DataAccess da;

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
        public frmRpt_ControlDocumental()
        {
            InitializeComponent();
        }
        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            if (FormWindowState.Normal == WindowState)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }

        private void frmRpt_ControlDocumental_Load(object sender, EventArgs e)
        {
            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = ZoomMode.Percent;
            reportViewer1.ZoomPercent = 100; //Seleccionamos el zoom que deseamos utilizar. En este caso un 100%
            mostrar_reporte();
            this.reportViewer1.RefreshReport();
        }
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
        private void mostrar_reporte()
        {
            Image img1 = pbIconCheck.Image;

            var imageConverter = new ImageConverter();
            byte[] imgg1 = imageToByteArray(img1);
                
            try
            {
                DataTable dt = da.p_datos_controldocs(gno_solicitud);

                DataTable dtData = new DataTable();
                dtData.Columns.Add("persona");
                dtData.Columns.Add("documento_id");
                dtData.Columns.Add("descripcion");
                dtData.Columns.Add("tipo_exigencia");
                dtData.Columns.Add("adjunto");
                dtData.Columns.Add("image1", typeof(Image));
                dtData.Clear();
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    var col0 = dt.Rows[x]["persona"].ToString();
                    var col1 = dt.Rows[x]["documento_id"].ToString();
                    var col2 = dt.Rows[x]["descripcion"].ToString();
                    var col3 = dt.Rows[x]["tipo_exigencia"].ToString();
                    var col4 = dt.Rows[x]["adjunto"].ToString();
                    dtData.Rows.Add(col0,col1, col2, col3, col4, img1);
                }
                ReportDataSource rds = new ReportDataSource("dataset1", dtData);                                                 
                this.reportViewer1.LocalReport.DataSources.Add(rds);
                DataTable dt2 = da.p_datos_HojaRuta2(gno_solicitud);

                ReportParameter pa1 = new ReportParameter("pa_id_cliente", dt2.Rows[0]["numero_identificacion"].ToString());
                ReportParameter pa2 = new ReportParameter("pa_nombre_afiliado", dt2.Rows[0]["nombre_completo"].ToString());
                ReportParameter pa3 = new ReportParameter("pa_codigo_cliente", dt2.Rows[0]["codigo_cliente"].ToString());
                ReportParameter pa4 = new ReportParameter("pa_monto_solicitado", dt2.Rows[0]["monto_solicitado"].ToString());
                ReportParameter pa5 = new ReportParameter("pa_tasa", dt2.Rows[0]["tasa"].ToString());
                ReportParameter pa6 = new ReportParameter("pa_plazo", dt2.Rows[0]["plazo"].ToString());
                ReportParameter pa7 = new ReportParameter("pa_producto", dt2.Rows[0]["producto"].ToString());
                ReportParameter pa8 = new ReportParameter("pa_no_solicitud", dt2.Rows[0]["no_solicitud"].ToString());
                ReportParameter pa9 = new ReportParameter("pa_nombre_oficial", dt2.Rows[0]["nombre_oficial"].ToString());
                ReportParameter pa10 = new ReportParameter("pa_filial", dt2.Rows[0]["nombre_agencia"].ToString());

                string codigo_formato = "";
                if (dt2.Rows[0]["codigo_sub_aplicacion"].ToString()=="2")
                {
                    codigo_formato = "F-RP-CR-16";
                }
                else if (dt2.Rows[0]["codigo_sub_aplicacion"].ToString() == "3")
                {
                    codigo_formato = "F-RP-CR-18";
                }
                else if (dt2.Rows[0]["codigo_sub_aplicacion"].ToString() == "19")
                {
                    codigo_formato = "F-RP-CR-19";
                }                
                else if (dt2.Rows[0]["codigo_sub_aplicacion"].ToString() == "29")
                {
                    codigo_formato = "F-RP-CR-19";
                }
                else if (dt2.Rows[0]["codigo_sub_aplicacion"].ToString() == "56")
                {
                    codigo_formato = "F-RP-CR-30";
                }
                else if (dt2.Rows[0]["codigo_sub_aplicacion"].ToString() == "32")
                {
                    codigo_formato = "F-RP-CR-32";
                }
                else if (dt2.Rows[0]["codigo_sub_aplicacion"].ToString() == "40")
                {
                    codigo_formato = "F-RP-CR-46";
                }
                else if (dt2.Rows[0]["codigo_sub_aplicacion"].ToString() == "58")
                {
                    codigo_formato = "F-RP-CR-41";
                }
                else if (dt2.Rows[0]["codigo_sub_aplicacion"].ToString() == "18")
                {
                    codigo_formato = "F-RP-CR-47";
                }


                ReportParameter pa11 = new ReportParameter("pa_doc_oym", codigo_formato);

                reportViewer1.LocalReport.ReportEmbeddedResource = "Docsis_Application.Reportes.rptControlDocumental.rdlc";
                this.reportViewer1.LocalReport.SetParameters(pa1);
                this.reportViewer1.LocalReport.SetParameters(pa2);
                this.reportViewer1.LocalReport.SetParameters(pa3);
                this.reportViewer1.LocalReport.SetParameters(pa4);
                this.reportViewer1.LocalReport.SetParameters(pa5);
                this.reportViewer1.LocalReport.SetParameters(pa6);
                this.reportViewer1.LocalReport.SetParameters(pa7);
                this.reportViewer1.LocalReport.SetParameters(pa8);
                this.reportViewer1.LocalReport.SetParameters(pa9);
                this.reportViewer1.LocalReport.SetParameters(pa10);
                this.reportViewer1.LocalReport.SetParameters(pa11);
                this.reportViewer1.LocalReport.Refresh();
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en reporte " + ex.Message);
            }
        }
    }

    public class rtp_ControlDocumental_cls
    {
        public Int32 no_solicitud { get; set; }
        public string persona { get; set; }
        public int documento_id { get; set; }
        public string descripcion { get; set; }
        public string tipo_exigencia { get; set; }        
        public string adjunto { get; set; }
        public Image img1 { get; set; }        
    }


}
