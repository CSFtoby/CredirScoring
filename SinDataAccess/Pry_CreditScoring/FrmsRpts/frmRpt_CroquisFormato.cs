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

namespace Docsis_Application.FrmsRpts
{
    public partial class frmRpt_CroquisFormato : Form
    {

        public string vl_enter = "\r\n";
        bool con_borde = MDI_Menu.con_borde;
        public Int32 gno_solicitud = 0;
        public string no_solicitud;
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

        public frmRpt_CroquisFormato()
        {
            InitializeComponent();
        }

        private void frmRpt_CroquisFormato_Load(object sender, EventArgs e)
        {
            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = ZoomMode.Percent;
            reportViewer1.ZoomPercent = 100; //Seleccionamos el zoom que deseamos utilizar. En este caso un 100%

            DataTable dtFiguras = da.ObtenerInfoSolicitud2(gno_solicitud);
            if (dtFiguras.Rows.Count > 0)
            {
                if (dtFiguras.Rows[0]["requiere_codeudor"].ToString() == "N")
                {
                    rbCodeudor.Enabled = false;
                }
                if (dtFiguras.Rows[0]["requiere_aval1"].ToString() == "N")
                {
                    rbAval1.Enabled = false;
                }
                if (dtFiguras.Rows[0]["requiere_aval2"].ToString() == "N")
                {
                    rbAval2.Enabled = false;
                }

            }

            mostrar_reporte();
            this.reportViewer1.RefreshReport();
        }
        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }
        private void mostrar_reporte()
        {

            try
            {
                DataTable dtInfoSolic = da.ObtenerInfoSolicitud(gno_solicitud);
                string vl_nombre_filial = da.ObtenerNombreAgencia(Int16.Parse(dtInfoSolic.Rows[0]["codigo_agencia_origen"].ToString()));
                string vl_rol="";
                DataTable dtDataCroquis = da.p_datos_croquis(gno_solicitud);
                
                if (rbSolicitante.Checked)
                    vl_rol = "SOLICITANTE";

                if (rbCodeudor.Checked)
                    vl_rol = "CODEUDOR";

                if (rbAval1.Checked)
                    vl_rol = "AVAL1";

                if (rbAval2.Checked)
                    vl_rol = "AVAL2";
                DataRow[] Rowsdt = dtDataCroquis.Select("rol='" + vl_rol + "'");
                string vl_codigo_cliente="";
                string vl_nombre="";
                string vl_direccion="";
                string vl_telefono_fijo="";
                string vl_telefono_celular="";
                if (Rowsdt.Count() > 0)
                {
                    vl_codigo_cliente = Rowsdt[0]["codigo_cliente"].ToString();
                    vl_nombre = Rowsdt[0]["nombre"].ToString();
                    vl_direccion = Rowsdt[0]["direccion_res"].ToString();
                    vl_telefono_fijo = Rowsdt[0]["telefono_fijo"].ToString();
                    vl_telefono_celular = Rowsdt[0]["telefono_celular"].ToString();
                }
                if (string.IsNullOrEmpty(vl_telefono_fijo))
                {
                    vl_telefono_fijo=" ";
                }
                if (string.IsNullOrEmpty(vl_telefono_celular))
                {
                    vl_telefono_celular=" ";
                }

                //ReportDataSource rds = new ReportDataSource("dataset1", dt);



                                           
                ReportParameter pa1 = new ReportParameter("pa_no_solicitud", gno_solicitud.ToString());
                ReportParameter pa2 = new ReportParameter("pa_codigo_cliente", vl_codigo_cliente);
                ReportParameter pa3 = new ReportParameter("pa_rol", vl_rol);
                ReportParameter pa4 = new ReportParameter("pa_nombre_afiliado", vl_nombre);
                ReportParameter pa5 = new ReportParameter("pa_nombre_filial", vl_nombre_filial);
                ReportParameter pa6 = new ReportParameter("pa_direccion", vl_direccion);
                ReportParameter pa7 = new ReportParameter("pa_telefono_fijo", vl_telefono_fijo);
                ReportParameter pa8 = new ReportParameter("pa_celular", vl_telefono_celular);
                

                //this.reportViewer1.LocalReport.DataSources.Add(rds);

                reportViewer1.LocalReport.ReportEmbeddedResource = "Docsis_Application.Reportes.rptCroquis.rdlc";
                this.reportViewer1.LocalReport.SetParameters(pa1);
                this.reportViewer1.LocalReport.SetParameters(pa2);
                this.reportViewer1.LocalReport.SetParameters(pa3);
                this.reportViewer1.LocalReport.SetParameters(pa4);
                this.reportViewer1.LocalReport.SetParameters(pa5);
                this.reportViewer1.LocalReport.SetParameters(pa6);
                this.reportViewer1.LocalReport.SetParameters(pa7);
                this.reportViewer1.LocalReport.SetParameters(pa8);
                

                this.reportViewer1.LocalReport.Refresh();
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en reporte " + ex.Message);
            }
        }

        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
        private void rbs_CheckedChanged(object sender, EventArgs e)
        {
            mostrar_reporte();
            this.reportViewer1.RefreshReport();            
        }

       
    }
}
