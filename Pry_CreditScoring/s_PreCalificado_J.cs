using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using wfcModel;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Globalization;
using NotificacionesDll;
using wfcModel.MGI.SubAplicaciones;
using Docsis_Application.FormsInformativo;
using System.Text.RegularExpressions;
using Docsis_Application.Excepciones;
using Docsis_Application.FrmsRpts;

namespace Docsis_Application
{
    public partial class s_PreCalificado_J : Form
    {
        public MDI_Menu formapadre;
        public DataAccess da;
        DataTable dtInfoFinan = new DataTable();
        DataTable dtCuotasBuro = new DataTable();
        delegate void Functionz();
        Thread myThread;
        bool bmaximizado = false;
        bool vl_mostrar_miniinfo = true;
        s_PreCalificado_info01 mini_info_resul = new s_PreCalificado_info01();
        public string gmodo_coopsafa = "INS";
        string gmodo_transunion = "CREAR";
        string usuario_wstransunion = MDI_Menu.usuario_wstransunion;
        string pass_wstransunion = MDI_Menu.pass_wstransunion;

        string gcodigo_agencia = DocSys.vl_agencia_usuario.ToString();
        string goficial_servicio = DocSys.vl_user;
        string goficial_serviciodesolicitud = "";

        double monto_destino1 = 0;
        double monto_destino2 = 0;

        public bool prestamo_vehiculos = false;
        public Int16 gestado_id_solicitud = 0;

        string gmodalidad = "INS";
        string gxml_respuesta = "";
        string gxml_crear_solicitud = "";
        string gxml_cuotas_buro = "";
        int gcant_cuotas_buro = 0;
        string gxml_referencias = "";
        string gxml_outputxml = "";
        string gdestino_credito_id = "";
        string gfuente_financiamiento = "";

        float saldo_aportaciones_principal = 0;
        string estado_burointerno_principal = "";
        string estado_desc_burointerno_principal = "";
        List<string> observa_burointerno_principal;

        float saldo_aportaciones_conyuge = 0;
        string estado_burointerno_conyuge = "0";
        string estado_desc_burointerno_conyuge = "";
        List<string> observa_burointerno_conyuge;

        float saldo_aportaciones_codeudor = 0;
        string estado_burointerno_codeudor = "0";
        string estado_desc_burointerno_codeudor = "";
        List<string> observa_burointerno_codeudor;

        float saldo_aportaciones_aval1 = 0;
        string estado_burointerno_aval1 = "0";
        string estado_desc_burointerno_aval1 = "";
        List<string> observa_burointerno_aval1;

        float saldo_aportaciones_aval2 = 0;
        string estado_burointerno_aval2 = "0";
        string estado_desc_burointerno_aval2 = "";
        List<string> observa_burointerno_aval2;
        string AdvertenciaGeneral = string.Empty;
        int CodigoExcepcion = 0;

        ResultadoBuroValores ResultadoPrestatario = ResultadoBuroValores.ERROR;
        ResultadoBuroValores ResultadoAval1 = ResultadoBuroValores.ERROR;
        ResultadoBuroValores ResultadoAval2 = ResultadoBuroValores.ERROR;

        bool retornar_ok = false;

        const int WM_SYSCOMMAND = 0x112;
        const int MOUSE_MOVE = 0xF012;
        const int SC_MINIMIZE = 0xF020;
        const int CS_DROPSHADOW = 0x00020000;
        const int WS_THICKFRAME = 0x00040000;
        const int WS_SIZEBOX = WS_THICKFRAME;
        const int cGrip = 10;      // Grip size
        const int cCaption = 1;   // Caption bar height;

        //FELVIR01-20190607
        private string MsjActualizarCampos = string.Empty;
        private bool conyugeAct = true;

        string Identidad_Delegado;

        #region

        //
        // Declaraciones del API
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        //
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        //
        #endregion
        #region

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x40000;
                return cp;
            }
        }

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

        List<referencias_solicitud> LstReferencias_solicitante = new List<referencias_solicitud>();
        List<referencias_solicitud> LstReferencias_codeudor = new List<referencias_solicitud>();
        List<referencias_solicitud> LstReferencias_aval1 = new List<referencias_solicitud>();
        List<referencias_solicitud> LstReferencias_aval2 = new List<referencias_solicitud>();

        public s_PreCalificado_J(DataAccess da)
        {
            InitializeComponent();
            //CheckForIllegalCrossThreadCalls = false;
            //this.DoubleBuffered = true;
            //this.SetStyle(ControlStyles.ResizeRedraw, true);

            //dtInfoFinan.Columns.Add("numero_identificacion");
            //dtInfoFinan.Columns.Add("codigo_cliente");
            //dtInfoFinan.Columns.Add("nombre");
            //dtInfoFinan.Columns.Add("rol");
            //dtInfoFinan.Columns.Add("Aportaciones");
            //dtInfoFinan.Columns.Add("ingresos");
            //dtInfoFinan.Columns.Add("otros_ingresos");
            //dtInfoFinan.Columns.Add("deducciones");
            //dtInfoFinan.Columns.Add("ingresos_netos");
            //dtInfoFinan.Columns.Add("estado_buro_interno");
            //dtInfoFinan.Columns.Add("observaciones_buro");
            //gvInfoFinanciera.DataSource = dtInfoFinan;

            //dtCuotasBuro.Columns.Add("seleccion");
            //dtCuotasBuro.Columns.Add("institucion");
            //dtCuotasBuro.Columns.Add("cuota", typeof(float));
            //dtCuotasBuro.Columns.Add("saldo", typeof(float));
            this.da = da;
        }

        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
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

        public void HidePage(TabPage tabPage)
        {
            TabControl parent = (TabControl)tabPage.Parent;
            parent.TabPages.Remove(tabPage);
        }
        
        private void TimeBar_Tick(object sender, EventArgs e)
        {
            labelRelojPanel.Text = DateTime.Now.ToString("hh:mm");
            labelDiaPanel.Text = DateTime.Now.ToString("dddd");
            labelFechaPanel.Text = DateTime.Now.ToString("MMMM") + " " + DateTime.Now.ToString("dd");
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            switch (e.Index)
            {
                case 0:
                    e.Graphics.FillRectangle(new SolidBrush(Color.Red), e.Bounds);
                    break;
                case 1:
                    e.Graphics.FillRectangle(new SolidBrush(Color.Blue), e.Bounds);
                    break;
                default:
                    break;
            }
            // Then draw the current tab button text 
            Rectangle paddedBounds = e.Bounds;
            paddedBounds.Inflate(-2, -2);
            e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, this.Font, SystemBrushes.HighlightText, paddedBounds);
        }

        private void btnInfoFinan_Click(object sender, EventArgs e)
        {
            //p_actualizar_info_financiera();
            //mostrar_tab(tpInfoFinan);
        }
        
        private void button9_Click(object sender, EventArgs e)
        {
            if (retornar_ok)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.Close();
            }
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            NotifyIcon nIcon = new NotifyIcon();
            this.WindowState = FormWindowState.Minimized;
            Size sizeicon = new System.Drawing.Size(12, 12);

            nIcon.ShowBalloonTip(5000, "CreditScoring Coopsafa", "La aplicacion se ha minizido..", ToolTipIcon.Info);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (retornar_ok == true)
                this.DialogResult = DialogResult.OK;
            else
            {
                this.Close();
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

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }

        private void skipMethod()
        {
            this.Close();
        }

        private void s_PreCalificado_J_Load(object sender, EventArgs e)
        {
            //p_llenar_combo_sub_aplicaciones();
            //p_llenar_combo_destinos(); //Se comentó porque el filtro del destno es por tipo de producto 18/08/2020
            //p_llenar_fuente_fondos();

            //ocultar_tabs();
            // btnDatosAfiliado_Click(null, null);
            //txtModo_transunion.Text = "CREAR";
            //int estacion_id = 0;

            //if (gmodo_coopsafa == "UPD")
            //{
            //    pbOficialServ.Image = Properties.Resources.icon_usuario;
            //    //Obteniendo el estado de la solicitud
            //    var dtEstadoSolicitud = da.ObtenerSituacionActualxSolicitud(Int32.Parse(txtNo_solicitud_coopsafa.Text));
            //    gestado_id_solicitud = Int16.Parse(dtEstadoSolicitud.Rows[0]["estado_id"].ToString());
            //    estacion_id = Int16.Parse(dtEstadoSolicitud.Rows[0]["estacion_id"].ToString());

            //    //txtIDSolicitante.Enabled = false;
            //    //txtCodigo_cliente.Enabled = false;
            //    //p_obtener_solicitudxNo(Int32.Parse(txtNo_solicitud_coopsafa.Text));
            //    //LabelNo_solic.Text = txtNo_solicitud_coopsafa.Text;
            //    //comboBox_sub_aplicacion.Visible = false;

            //    //Identidad_Delegado = txtIDSolicitante.Text;
            //    //p_get_tipo_Miembro(Identidad_Delegado);

            //    int codigo_agencia = Int16.Parse(dtEstadoSolicitud.Rows[0]["codigo_agencia_origen"].ToString());
            //    string oficial_servicio = dtEstadoSolicitud.Rows[0]["oficial_servicio"].ToString();
            //    labelFilial.Text = da.ObtenerNombreAgencia(codigo_agencia);
            //    goficial_serviciodesolicitud = oficial_servicio.Trim();
            //    labelOficialDeServicio.Text = oficial_servicio.Trim() + " | " + da.ObtenerNombreUsuario(oficial_servicio);

            //    //LabelNo_solic.Text = txtNo_solicitud_coopsafa.Text;
            //    this.btnActualizarInfo.Visible = true;

            //    try
            //    {
            //        string scodigo_cliente_usuario = da.ObtenerCodigoClientexUsuario(oficial_servicio);
            //        byte[] tmpfotoUsuario = da.ObtenerFotoUsuario(scodigo_cliente_usuario);
            //        pbOficialServ.Image = DocSys.p_CopyDataToBitmap(tmpfotoUsuario);
            //        if (pbOficialServ.Image == null)
            //        {
            //            pbOficialServ.Image = Properties.Resources.icon_usuario;
            //        }
            //    }
            //    catch
            //    {
            //        pbOficialServ.Image = Properties.Resources.icon_usuario;
            //    }
            //    this.CodigoExcepcion = this.da.GetExcepcionSolicitud(Int32.Parse(txtNo_solicitud_coopsafa.Text));
            //}
            ////Modo INS
            //else
            //{
            //    pbOficialServ.Image = Properties.Resources.icon_usuario;
            //    labelFilial.Text = MDI_Menu.nombre_agencia_usuario.Trim();
            //    labelOficialDeServicio.Text = MDI_Menu.nombre_y_usuario_oficial_servicio;
            //    labelOficialDeServicio.Text = goficial_servicio + " | " + da.ObtenerNombreUsuario(goficial_servicio);
            //    goficial_serviciodesolicitud = goficial_servicio;
            //    this.btnActualizarInfo.Visible = false;

            //   // LabelNo_solic.Text = "";

            //    try
            //    {
            //        string scodigo_cliente_usuario = da.ObtenerCodigoClientexUsuario(goficial_servicio);
            //        byte[] tmpfotoUsuario = da.ObtenerFotoUsuario(scodigo_cliente_usuario);
            //        pbOficialServ.Image = DocSys.p_CopyDataToBitmap(tmpfotoUsuario);
            //        if (pbOficialServ.Image == null)
            //        {
            //            pbOficialServ.Image = Properties.Resources.icon_usuario;
            //        }
            //    }
            //    catch
            //    {
            //        pbOficialServ.Image = Properties.Resources.icon_usuario;
            //    }
            //    //p_deshabilitar_figuras_solicitud();
            //    comboBox_sub_aplicacion.Visible = true;
            //}

            //if (gmodo_coopsafa == "CONS")
            //{
            //    this.btnActualizarInfo.Visible = false;
            //    var dtEstadoSolicitud = da.ObtenerSituacionActualxSolicitud(Int32.Parse(txtNo_solicitud_coopsafa.Text));
            //    gestado_id_solicitud = Int16.Parse(dtEstadoSolicitud.Rows[0]["estado_id"].ToString());
            //    estacion_id = Int16.Parse(dtEstadoSolicitud.Rows[0]["estacion_id"].ToString());

            //    //Identidad_Delegado = txtIDSolicitante.Text;
            //    //p_get_tipo_Miembro(Identidad_Delegado);

            //    int codigo_agencia = Int16.Parse(dtEstadoSolicitud.Rows[0]["codigo_agencia_origen"].ToString());
            //    string oficial_servicio = dtEstadoSolicitud.Rows[0]["oficial_servicio"].ToString();
            //    labelFilial.Text = da.ObtenerNombreAgencia(codigo_agencia);
            //    goficial_serviciodesolicitud = oficial_servicio.Trim();
            //    labelOficialDeServicio.Text = oficial_servicio.Trim() + " | " + da.ObtenerNombreUsuario(oficial_servicio);
            //    //LabelNo_solic.Text = txtNo_solicitud_coopsafa.Text;

            //    //txtIDSolicitante.Enabled = false;
            //    //txtCodigo_cliente.Enabled = false;
            //    //p_obtener_solicitudxNo(Int32.Parse(txtNo_solicitud_coopsafa.Text));
            //    //comboBox_sub_aplicacion.Visible = false;
            //    //p_modo_consulta();

            //    pbOficialServ.Image = Properties.Resources.icon_usuario;
            //    try
            //    {
            //        string scodigo_cliente_usuario = da.ObtenerCodigoClientexUsuario(oficial_servicio);
            //        byte[] tmpfotoUsuario = da.ObtenerFotoUsuario(scodigo_cliente_usuario);
            //        pbOficialServ.Image = DocSys.p_CopyDataToBitmap(tmpfotoUsuario);
            //        if (pbOficialServ.Image == null)
            //        {
            //            pbOficialServ.Image = Properties.Resources.icon_usuario;
            //        }
            //    }
            //    catch
            //    {
            //        pbOficialServ.Image = Properties.Resources.icon_usuario;
            //    }
            //    this.CodigoExcepcion = this.da.GetExcepcionSolicitud(Int32.Parse(txtNo_solicitud_coopsafa.Text));
            //}
            /*
			 * Valida si la solicitud está en proceso y en una estación distinta a Afiliación
			 */

            //Si está en proceso y no es Afiliación
            //        if ((this.gestado_id_solicitud > 2) & DocSys.EstacionGlobal != 1000)
            //        {
            //            switch (DocSys.EstacionGlobal)
            //            {
            //                case 1002: //Créditos TGU
            //                    this.btnActualizarInfo.Visible = true;
            //                    this.txtDescripcion_garantia.Enabled = true;
            //                    break;
            //                case 2001: //Créditos SPS
            //                    this.btnActualizarInfo.Visible = true;
            //                    this.txtDescripcion_garantia.Enabled = true;
            //                    break;
            //                case 2003: //Créditos LCBA
            //                    this.btnActualizarInfo.Visible = true;
            //                    this.txtDescripcion_garantia.Enabled = true;
            //                    break;
            //                case 1001: //Nivel Resolutivo Filial
            //                    this.btnActualizarInfo.Visible = true;
            //                    break;
            //                default:
            //                    this.btnActualizarInfo.Visible = false;
            //                    break;
            //            }
            //        }

            //        bool global_es_admon_sistema = da.EsAdministradorSistema(DocSys.vl_user);
            //        bool global_es_admon_temporal = this.da.EsAdminTemporal(DocSys.vl_user);
            //        if (global_es_admon_sistema || global_es_admon_temporal)
            //        {
            //            this.btnActualizarInfo.Visible = true;
            //        }

            //        if (this.CodigoExcepcion != 0)
            //        {
            //            this.btnExcepcion.Visible = true;
            //        }
            //        else
            //        {
            //            this.btnExcepcion.Visible = false;
            //        }

            //        /*
            //* Si la estación es distinta de afiliación
            //*/
            //        if (estacion_id != 1000)
            //        {
            //            this.label15.Text = "Usuario Estación:";
            //        }

            //        //labelUsuario.Text = DocSys.vl_user;
            //        //LabelTnsNames.Text = DocSys.vl_tnsnames;

            //        //p_calcular_indice_concentracion_deuda();

            //        MD_Coop ws_transunion = new MD_Coop();
            //        txtUrlTransUnion.Text = ws_transunion.Url;
        }
    }
}