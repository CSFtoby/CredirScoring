using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using wfcModel;

using System.IO;
using System.Threading;

namespace Docsis_Application
{
    public partial class s_Conciliacion_doc : Form
    {
        bool archivo_cargado = false;
        public DataAccess da;

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

        public s_Conciliacion_doc()
        {
            InitializeComponent();
        }
        private void s_Conciliacion_doc_Load(object sender, EventArgs e)
        {
           // ocultar_tabs();
            mostrar_tab(tabPage1);
            if (da.p_Cantidad_linea_carga_det()>0)
            {
                mostrar_tab(tabPage3);
            }
        }
        private bool cargaArchivoToBase()
        {
            bool vl_retorno = false;
            DataTable dt = new DataTable();
            dt.Columns.Add("no_identificacion");
            dt.Columns.Add("facturado");
            dt.Columns.Add("application_id");
            dt.Columns.Add("fecha_creacion");
            dt.Columns.Add("fecha_ejecucion");
            dt.Columns.Add("oficial_servicio");
            dt.Columns.Add("rol");
            dt.Columns.Add("filial");
            dt.Columns.Add("producto");
            dt.Columns.Add("extra");
            dt.Columns.Add("forma_pago");
            dt.Columns.Add("id_bitacora");
            dt.Columns.Add("nombre_solucion");
            
            string csvPath = txtArchivo.Text;
            try
            {
                if (System.IO.File.Exists(csvPath))
                {
                    System.IO.StreamReader fileReader = new System.IO.StreamReader(csvPath, false);

                    //Reading Data
                    while (fileReader.Peek() != -1)
                    {
                        var fileRow = fileReader.ReadLine();
                        var fileDataField = fileRow.Split(',');
                        dt.Rows.Add(fileDataField);
                    }
                    fileReader.Dispose();
                    fileReader.Close();
                    vl_retorno = true;
                }
                else
                {
                    MessageBox.Show("Error en la busqueda del archivo seleccionado en el disco de su PC.");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                vl_retorno = false;
            }

            gvTabla.DataSource = dt;
            return vl_retorno;
        }
        public void generar_log(int identado, string p_texto)
        {
            string sidentado = string.Concat(Enumerable.Repeat(" ", identado));
            string vl_enter = "\r\n";
            txtLog.AppendText(sidentado + p_texto + vl_enter);
            rhtxtlog.AppendText(sidentado + p_texto + vl_enter);
        }

        #region Manejo de tabs
        public void HidePage(TabPage tabPage)
        {
            TabControl parent = (TabControl)tabPage.Parent;
            parent.TabPages.Remove(tabPage);
        }
        private void ocultar_tabs()
        {
            if (IsVisible(tabPage1))
            {
                HidePage(tabPage1);
            }
            if (IsVisible(tabPage2))
            {
                HidePage(tabPage2);
            }
        }
        private void mostrar_tab(TabPage tabPage)
        {
            //ocultar_tabs();
            if (!IsVisible(tabPage))
            {
                ShowPageInTabControl(tabPage, tabControl1);
            }
        }
        public bool IsVisible(TabPage tabPage)
        {
            if (tabPage.Parent == null)
                return false;
            else if (tabPage.Parent.Contains(tabPage))
                return true;
            else
                return false;
        }
        public void ShowPageInTabControl(TabPage tabPage, TabControl parent)
        {
            parent.TabPages.Add(tabPage);
        }
        #endregion

        #region Event handler
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }
        private void btnNextPage_Click(object sender, EventArgs e)
        {
            ocultar_tabs();
            mostrar_tab(tabPage2);
            mostrar_tab(tabPage3);

        }
        private void btnPrevPage_Click(object sender, EventArgs e)
        {
            ocultar_tabs();
            mostrar_tab(tabPage1);
        }
        private void btnCargar_Click(object sender, EventArgs e)
        {
            if (archivo_cargado == false)
            {
                if (MessageBox.Show("Ejecutar el proceso de carga del archivo...?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                if (MessageBox.Show("El archivo ya fue cargado a la base de datos, desea volver a cargar el archivo nuevamente..?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            if (da.p_conc_nueva_carga_cvs())
            {
                Int32 reg = 0;
                foreach (DataGridViewRow row in gvTabla.Rows)
                {
                    string vl_application_id = (string)row.Cells["application_id"].Value;
                    string vl_no_identificacion = (string)row.Cells["no_identificacion"].Value;
                    string vl_rol = (string)row.Cells["rol"].Value;
                    string vl_facturable = (string)row.Cells["facturado"].Value;
                    da.p_conc_insertar_registro_cvs(Int32.Parse(vl_application_id), vl_no_identificacion, vl_rol,vl_facturable);
                    Application.DoEvents();                    
                    this.gvTabla.CurrentCell = this.gvTabla[1, row.Index];
                    reg++;
                }
                btnNextPage.Visible = true;
                MessageBox.Show("Procesados " + reg.ToString() + " registros,presione el boton de siguiente paso...! ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                archivo_cargado = true;
            }
            return;
        }
        private void btnIniciarConci_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea ejecutar el proceso de conciliacion del archivo cargado...?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            generar_log(0, " Iniciando proceso....");
            var dt = da.p_conci_obtener_carga_head();
            Int32 reg = 0;
            foreach (DataRow row in dt.Rows)
            {
                bool appIDOk = true;
                string vl_application_id = (string)row["application_id"].ToString();
                string vl_estatus_application_id = (string)row["estatus_application_id"].ToString();
                int vl_cantidad_tu = int.Parse(row["cantidad_personas_tu"].ToString());
                int vl_cantidad_cs = int.Parse(row["cantidad_personas_cs"].ToString());
                string vl_observacion_head = "";

                generar_log(0, "Conciliando application id " + vl_application_id);
                if (vl_cantidad_tu == vl_cantidad_cs)
                {
                    generar_log(12, "cantidad personas ----> Ok");
                }
                else
                {
                    appIDOk = false;
                    generar_log(12, "cantidad personas ----> false : TranU.(" + vl_cantidad_tu.ToString() + ")    CreditS.(" + vl_cantidad_cs.ToString() + ")");
                    vl_observacion_head = "Cantidad de personas no es igual";
                }

                #region Procesando detalle de la carga
                /*---------------------------------*/
                //Procesando detalle de la carga
                /*---------------------------------*/
                var dtDet = da.p_conci_obtener_carga_det(Int32.Parse(vl_application_id));
                foreach (DataRow rowdet in dtDet.Rows)
                {
                    int vl_estado_conci_id = 0;
                    string vl_no_linea = rowdet["no_linea"].ToString();
                    string vl_id_tu = rowdet["no_identificacion_tu"].ToString();
                    string vl_id_cs = rowdet["no_identificacion_cs"].ToString();
                    string vl_rol = rowdet["rol_tu"].ToString();
                    string vl_observacion_detalle = "";
                    if (vl_id_tu == vl_id_cs)
                    {
                        vl_estado_conci_id = 1;
                        vl_observacion_detalle = " rol " + vl_rol + " " + vl_id_tu + "----> Ok";
                    }
                    else
                    {
                        appIDOk = false;
                        if (string.IsNullOrEmpty(vl_id_cs))
                        {
                            if (vl_rol == "PRINCIPAL")
                            {
                                vl_observacion_detalle = "ID " + vl_id_tu + " par el rol " + vl_rol + " no existe en CreditScoring";
                                // No existe la solicitud en cs
                                vl_estado_conci_id = -97;
                            }
                            else
                            {
                                vl_observacion_detalle = "ID " + vl_id_tu + " par el rol " + vl_rol + " no existe en CreditScoring";
                                // No existe la solicitud en cs
                                vl_estado_conci_id = -98;
                            }
                        }
                        else
                        {
                            vl_observacion_detalle = "ID " + vl_id_tu + " par el rol " + vl_rol + " no es igual en CreditScoring";
                            vl_estado_conci_id = -99;
                        }
                    }
                    generar_log(20, vl_observacion_detalle);
                    da.p_actualizar_linea_carga_det(Int32.Parse(vl_application_id), Int32.Parse(vl_no_linea), vl_observacion_detalle, vl_estado_conci_id);
                }
                #endregion

                /// 1  = Ok
                ///-90 = no esta conciliada, 91 a 99 detalle de los problemas encontrados                
                if (appIDOk)
                {
                    da.p_actualizar_linea_carga(Int32.Parse(vl_application_id), "Ok", 1);
                }
                else
                {
                    da.p_actualizar_linea_carga(Int32.Parse(vl_application_id), vl_observacion_head, -90);
                }
                Application.DoEvents();
                Thread.Sleep(100);
                this.gvTabla.CurrentCell = this.gvTabla[1, reg];
                reg++;

            }

            MessageBox.Show("Proceso finalizado satisfactoriamente..! ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        private void lLAbrir_archivo_cs_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Seleccione un archivo ";
            //openFileDialog.InitialDirectory = Application.StartupPath + "\\logs";
            openFileDialog.InitialDirectory = "C:\\CreditScoring_conciliacion";
            openFileDialog.Filter = "All Files (*.*)|*.*|Archivos CSV (*.csv)|*.csv";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtArchivo.Text = openFileDialog.FileName.ToString();
                if (cargaArchivoToBase())
                {
                    lLAbrir_archivo_cs.Visible = false;
                    btnCargar.Visible = true;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            if (gvResultado.RowCount <= 0)
                return;
            gvResultado.SelectAll();
            DataObject dataobj = gvResultado.GetClipboardContent();
            Clipboard.SetDataObject(dataobj, true);
            gvResultado.ClearSelection();
            MessageBox.Show("Datos exportados al clippart...", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void cmbTiposResul_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTiposResul.Text == "Todas Ok (1)")
            {
                gvResultado.DataSource = da.p_conci_resultado_det(1);
                gvResultado.Refresh();
            }
            if (cmbTiposResul.Text == "Solicitante no encontrado en CreditScoring (-97)")
            {
                gvResultado.DataSource = da.p_conci_resultado_det(-97);
                gvResultado.Refresh();
            }
            if (cmbTiposResul.Text == "Garantes o Codeudor no encontrado en CreditScoring (-98)")
            {
                gvResultado.DataSource = da.p_conci_resultado_det(-98);
                gvResultado.Refresh();
            }
            if (cmbTiposResul.Text == "ID no son iguales. (-99)")
            {
                gvResultado.DataSource = da.p_conci_resultado_det(-99);
                gvResultado.Refresh();
            }
            if (cmbTiposResul.Text == "Todas las Application con problemas")
            {
                gvResultado.DataSource = da.p_conci_resultado_det(-1);
                gvResultado.Refresh();
            }

            
            
                
        }
        #endregion

    }
}
