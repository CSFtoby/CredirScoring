using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using wfcModel;
using System.Threading;
using System.Data.OleDb;
//using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Diagnostics;
using System.IO;

namespace Docsis_Application.FrmsRpts
{
    public partial class frmRpt_Reporte1 : Form
    {
        delegate void FunctionRpt1();
        Thread myThread;
        public DataAccess da;
        string ruta_generacion = @"c:\CreditScoringTmp\";

        string sestado = "";
        string sregional = "";
        string sfilial = "";
        string sarea = "";

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

        public frmRpt_Reporte1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void frmRpt_Reporte1_Load(object sender, EventArgs e)
        {
            p_llenar_combo_regionales();
            p_llenar_combo_estados();
            p_llenar_combo_areas();
        }
        
        private void p_llenar_combo_regionales()
        {
            try
            {
                DataTable dtRegionales = da.ObtenerRegionales();
                cmbRegionales.DataSource = dtRegionales;
                cmbRegionales.DisplayMember = "descripcion";
                cmbRegionales.ValueMember = "codigo_zona";
                cmbRegionales.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex.TargetSite + " " + ex.Message);
            }
        }

        private void p_llenar_combo_areas()
        {
            try
            {
                DataTable dtAreas = da.ObtenerAreas();
                cboArea.DataSource = dtAreas;
                cboArea.DisplayMember = "NOMBRE";
                cboArea.ValueMember = "ESTACION_ID";
                cboArea.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex.TargetSite + " " + ex.Message);
            }
        }

        private void p_llenar_combo_filiales(string p_regional)
        {
            try
            {
                DataTable dtFiliales = null;
                if (string.IsNullOrEmpty(p_regional))
                {
                    dtFiliales = da.ObtenerFiliales();
                }
                else
                {
                    dtFiliales = da.ObtenerFiliales(p_regional);
                }

                cmbFiliales.DataSource = dtFiliales;
                cmbFiliales.DisplayMember = "nombre_agencia";
                cmbFiliales.ValueMember = "codigo_agencia";
                cmbFiliales.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex.TargetSite + " " + ex.Message);
            }
        }

        private void p_llenar_combo_estados()
        {
            try
            {
                DataTable dtestados_sol = da.ObtenerEstadoSolicitud();

                cmbEstadoSolic.DataSource = dtestados_sol;
                cmbEstadoSolic.DisplayMember = "descripcion";
                cmbEstadoSolic.ValueMember = "estado_id";
                cmbEstadoSolic.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex.TargetSite + " " + ex.Message);
            }
        }
       
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (rbCopíar.Checked)
            {
                myThread = new Thread(new ThreadStart(copiarData));
                myThread.Start();
            }
            if (rbExport.Checked)
            {
                myThread = new Thread(new ThreadStart(exportExcel));
                myThread.Start();
            }
        }

        private void exportExcel()
        {
            bool hay_error = false;
            this.Invoke(new FunctionRpt1(delegate()
            {
                panelWait.Visible = true;
                btnClose.Enabled = false;
            }));
			CheckForIllegalCrossThreadCalls = false;

            var datatable = new DataTable();

            if (ck_admin_rp.Checked == true)
            {
                datatable = da.p_datos_reporte_Ad(dpFecha1.Value, dpFecha2.Value, sregional, sfilial, sestado, txtNo_solicitud.Text, sarea);
            }
            else
            {
                datatable = da.p_datos_reporte1(dpFecha1.Value, dpFecha2.Value, sregional, sfilial, sestado, txtNo_solicitud.Text, sarea);
            }

            var lines = new List<string>();

            string[] columnNames = datatable.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName).
                                              ToArray();

            var header = string.Join(",", columnNames);
            lines.Add(header);

            var valueLines = datatable.AsEnumerable()
                               .Select(row => string.Join(",", row.ItemArray));
            lines.AddRange(valueLines);

            try
            {
                File.WriteAllLines(ruta_generacion + "excel1.csv", lines);
            }
            catch (Exception ex)
            {
                hay_error = true;
                MessageBox.Show(ex.Message);
            }

            this.Invoke(new FunctionRpt1(delegate()
            {
                gvReporte1.ClearSelection();
                panelWait.Visible = false;
                btnClose.Enabled = true;
            }));
            //Microsoft.Win32.RegistryKey RK = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Office\\12.0");
            p_abrir_archivo("excel1.csv");
        }

        private void copiarData()
        {
            this.Invoke(new FunctionRpt1(delegate()
            {
                panelWait.Visible = true;
                btnClose.Enabled = false;
            }));

            if (ck_admin_rp.Checked == true)
            {
                gvReporte1.DataSource = da.p_datos_reporte_Ad(dpFecha1.Value, dpFecha2.Value, sregional, sfilial, sestado, txtNo_solicitud.Text, sarea);
            }
            else
            {
                gvReporte1.DataSource = da.p_datos_reporte1(dpFecha1.Value, dpFecha2.Value, sregional, sfilial, sestado, txtNo_solicitud.Text, sarea);
            }

            //gvReporte1.DataSource = da.p_datos_reporte1(dpFecha1.Value, dpFecha2.Value, sregional, sfilial, sestado, txtNo_solicitud.Text, sarea);
            if (gvReporte1.RowCount <= 0)
                return;
            gvReporte1.SelectAll();
            DataObject dataobj = gvReporte1.GetClipboardContent();
            //Invoke((Action)(() => { Clipboard.SetDataObject(dataobj, true) }));
            //Clipboard.SetDataObject(dataobj, true);

            this.Invoke(new FunctionRpt1(delegate()
            {
                Clipboard.SetDataObject(dataobj, true);
                gvReporte1.ClearSelection();
                panelWait.Visible = false;
                btnClose.Enabled = true;
            }));
            MessageBox.Show("Datos exportados al clippart, seleccione el programa al que desea expoartar la data y ejecute el pegar ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void p_abrir_archivo(string p_archivo)
        {
            string sFile = ruta_generacion + p_archivo;
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "excel.exe";
                startInfo.Arguments = sFile;
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
            }
        }
        
        private void cmbRegionales_SelectionChangeCommitted(object sender, EventArgs e)
        {
            sregional = cmbRegionales.SelectedValue.ToString();
            labelRegional.Text = "Regional (" + sregional + ") :";
        }

        private void cmbFiliales_SelectionChangeCommitted(object sender, EventArgs e)
        {
            sfilial = cmbFiliales.SelectedValue.ToString();
            labelFilial.Text = "Filial (" + sfilial + ") :";
        }

        private void cmbFiliales_DropDown(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(sregional))
            {
                p_llenar_combo_filiales("");
            }
            else
            {
                p_llenar_combo_filiales(sregional);
            }
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

        private void cmbEstadoSolic_SelectionChangeCommitted(object sender, EventArgs e)
        {
            sestado = cmbEstadoSolic.SelectedValue.ToString();
            labelEstado.Text = "Estado solicitud (" + sestado + ") :";
        }

        private void cboArea_SelectionChangeCommitted(object sender, EventArgs e)
        {
            sarea = Convert.ToString(cboArea.SelectedValue.ToString());
            lbArea.Text = "Área (" + sarea + ") :";
        }
    }
}
