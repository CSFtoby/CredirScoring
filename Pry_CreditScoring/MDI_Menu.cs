using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security;
using System.Security.Permissions;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Net;
using System.Net.Mail;
using wfcModel;
using System.Globalization;
using System.Diagnostics;
using Docsis_Application.Excepciones;
using System.Timers;
using NotificacionesDll;
using Docsis_Application.FrmsRpts;

namespace Docsis_Application
{
    public partial class MDI_Menu : Form
    {
        DataAccess da;
        s_miniinfo_usuario miniinfo = new s_miniinfo_usuario();
        s_miniinfo_solic miniinfo_sol = new s_miniinfo_solic();

        public MDI_Menu formaprincipal;

        public static string usuario_wstransunion = "sagrada_familia_prod";
        public static string pass_wstransunion = "Coopsafa@2010";

        public static string tipo_sol;

        public static string nombre_agencia_usuario;
        public static string nombre_y_usuario_oficial_servicio;
        bool expandiendo = false;
        bool colapsando = true; //para que al inicio por primera vez y se manda a llamar el metodo colapsar o haga un desplegados desordenados
        bool vl_mostrar_miniinfo = true;
        public static bool con_borde = true;
        Image fotoUsuario;
        private System.Timers.Timer aTimer;
        private bool verExcepciones = false; //felvir01

        #region Declaracion de Dlls
        SmtpClient cliente = new SmtpClient("smtp.gmail.com", 587);
        WebBrowser Navegador = new WebBrowser(); // Nuestro objeto web broser

        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern void LockWorkStation();
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
        #region
        private const int CS_DROPSHADOW = 0x00020000;
        protected override CreateParams CreateParams
        {
            get
            {
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

        int global_no_movimiento = 0;
        int global_excp_estacopm = 0;

        int global_estacion_id_to = DocSys.p_obtener_estacion_usuario(DocSys.vl_user);
        int global_zona_user = DocSys.ObtenerZonaxUsuario(DocSys.vl_user);
        bool global_ver_toda_filial = DocSys.p_obtener_si_todas_las_filiales(DocSys.p_obtener_estacion_usuario(DocSys.vl_user));
        bool global_gerente_filial = DocSys.p_obtener_es_gerente_filial(DocSys.vl_user);
        bool global_es_admon_sistema = false;
        bool global_tipo_admon_temp_sol = false;
        bool global_tipo_admon_temp_excp = false;
        bool global_es_admon_temp = false;
        bool global_es_nivel_resolutivo = false;

        bool global_primer_load = true;
        int global_no_archivo_contexmenu = 0;
        int global_no_anotacion_contexmenu = 0;

        //para el movimiento del titulo
        private bool Movimiento = false;
        private int mtop = 0;
        private int mleft = 0;

        private const int cGrip = 13;      // Grip size
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

        public MDI_Menu()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.txtTexto_buscar.GotFocus += new System.EventHandler(this.txtTexto_buscar_GotFocus);
            this.txtTexto_buscar.LostFocus += new System.EventHandler(this.txtTexto_buscar_LostFocus);

            try
            {
                da = new DataAccess(s_login.global_usuario, s_login.global_password, s_login.global_tnsnames);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            pnlMenu.Parent = this;

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-HN");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-HN");
        }

        private void MDI_Menu_Load(object sender, EventArgs e)
        {
            label_numero_mensajes.Text = "0";
            panel_mensajes.Visible = false;
            label_oficial.Text = "";
            label_nombre_oficial.Text = "";
            label_no_solicitud.Text = "";
            label_codigo_cliente.Text = "";
            label_nombre_afiliado.Text = "";
            label_monto.Text = "";
            label_plazo.Text = "";
            label_fuente.Text = "";
            label_filial.Text = "";
            label_oficial.Text = "";

            global_es_admon_sistema = da.EsAdministradorSistema(DocSys.vl_user);
            global_es_admon_temp = this.da.EsAdminTemporal(DocSys.vl_user);
            global_tipo_admon_temp_sol = this.da.TipoAdminTemporalS(DocSys.vl_user);
            global_tipo_admon_temp_excp = this.da.TipoAdminTemporalE(DocSys.vl_user);

            DocSys.vl_operacion = "NORM";
            txtTexto_buscar.Text = "Ingrese texto a buscar";
            txtTexto_buscar.CharacterCasing = CharacterCasing.Lower;

            int vl_codigo_agencia;
            string vl_nombre_agencia;
            DocSys.p_obtener_filial_usuario(DocSys.vl_user, out vl_codigo_agencia, out vl_nombre_agencia);
            DocSys.vl_agencia_usuario = vl_codigo_agencia;
            nombre_agencia_usuario = DocSys.ToTitleCase(vl_nombre_agencia);
            nombre_y_usuario_oficial_servicio = DocSys.vl_user.ToUpper().Trim() + " | " + da.ObtenerNombreUsuario(DocSys.vl_user.ToUpper());
            lblSuFilial.Text = nombre_agencia_usuario;

            label_usuario.Text = DocSys.vl_user.ToUpper();
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            try
            {
                p_llenar_estaciones_usuario();
                p_llenar_zonas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No hay definida una estacion de trabajo para este usuario", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            this.Text = "::: CREDITSCORING | Usuario :" + DocSys.vl_user + " | Filial : " + vl_codigo_agencia.ToString() + " - " + vl_nombre_agencia + " | Estacion Wf :" + DocSys.p_get_nombre_estacion(global_estacion_id_to) + " :::";
            if (global_estacion_id_to != 1000)
                carpetasToolStripMenuItem.Visible = false;
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            p_popular_treview();
            treeView.ExpandAll();
            DocSys.vl_nodo_treview = "Node_entradas";
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            global_primer_load = false;
            //Si es creditos activa la funcion
            if (global_estacion_id_to == 1002 || global_estacion_id_to == 2001 || global_estacion_id_to == 3001)
                asignarLaSolicitudToolStripMenuItem.Enabled = true;
            else
                asignarLaSolicitudToolStripMenuItem.Enabled = false;

            //Si es afiliacion desactiva esta funcion
            if (global_estacion_id_to == 1002 || global_estacion_id_to == 2001 || global_estacion_id_to == 3001 ||
                global_estacion_id_to == 1003 || global_estacion_id_to == 2002 || global_estacion_id_to == 3002)
                asignarLaSolicitudToolStripMenuItem.Enabled = true;
            else
                asignarLaSolicitudToolStripMenuItem.Enabled = false;

            //Si es afiliacion desactiva esta funcion
            if (global_estacion_id_to == 1000)
            {
                MenuItem_verSoloMisSolicitudes.Visible = false;
                checkBox_mis_solicitudes.Visible = false;
            }
            else
            {
                MenuItem_verSoloMisSolicitudes.Visible = true;
                checkBox_mis_solicitudes.Visible = true;
            }

            ocultar_menu();
            pnlMenu.Visible = true;

            if (global_es_admon_sistema || (global_es_admon_temp && global_tipo_admon_temp_sol))
            {
                btnConfig.Visible = true;
                administracionDeAprobacionesToolStripMenuItem.Visible = true;
                procesoDeConciliacionToolStripMenuItem.Visible = true;
                reporteDeTiemposPorSolicitudesToolStripMenuItem.Visible = true;
                reporteDeSolicitudesToolStripMenuItem.Visible = true;
                reporteDeExcepcionesToolStripMenuItem.Visible = true;
                reportesMensualesSolicitudesToolStripMenuItem.Visible = true;
                administraciónDeExcepcionesToolStripMenuItem.Visible = true;
            }

            if (global_gerente_filial)
            {
                administraciónDeExcepcionesToolStripMenuItem.Visible = true;
                //administracionDeAprobacionesToolStripMenuItem.Visible = true;
            }

            p_refrescar_info();
            try
            {
                string scodigo_cliente_usuario = da.ObtenerCodigoClientexUsuario(DocSys.vl_user);
                byte[] tmpfotoUsuario = da.ObtenerFotoUsuario(scodigo_cliente_usuario);
                fotoUsuario = DocSys.p_CopyDataToBitmap(tmpfotoUsuario);
            }
            catch { }
            this.pnlSemaforo.Visible = false;
            //Recupera la cantidad de mensajes nuevos al iniciar sesión
            this.ActualizarNoLeidos();
            //this.SetTimer();
            this.timerSemaforo_Tick(null, null);
        }

        private void MDI_Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        protected void txtTexto_buscar_GotFocus(object sender, EventArgs e)
        {
            p_cambiar_modo("CONS");
        }
        protected void txtTexto_buscar_LostFocus(object sender, EventArgs e) { }

        private void p_cambiar_modo(string pa_operacion)
        {
            if (pa_operacion == "CONS")
            {
                DocSys.vl_operacion = "CONS";
                panel_indicativo_superior.BackColor = Color.WhiteSmoke;//Color.FromArgb(252, 167, 66);
                if (txtTexto_buscar.Text == "ingrese texto a buscar")
                    txtTexto_buscar.Text = "";
                Font font = new Font(txtTexto_buscar.Font, FontStyle.Regular);
                panel_indicativo_superior.BackColor = Color.WhiteSmoke;
                txtTexto_buscar.Font = font;
            }

            if (pa_operacion == "NORM")
            {
                DocSys.vl_operacion = "NORM";
                panel_indicativo_superior.BackColor = Color.WhiteSmoke;//Color.FromArgb(255, 255, 220);
                txtTexto_buscar.Text = "Ingrese texto a buscar";
                if (DocSys.vl_nodo_treview == "Node_entradas")
                {
                    p_popular_lista_entradas("");
                }
                Font font = new Font(txtTexto_buscar.Font, FontStyle.Italic);
                txtTexto_buscar.Font = font;
                gv_entradas.Focus();
            }
        }

        private void ocultar_menu()
        {
            int y = Location.Y;
            y = 0;
            pnlMenu.Location = new Point(0 - pnlMenu.Width + 3, y);
        }

        private void mostrar_menu()
        {
            if (expandiendo)
                return;

            pbPerfil_menu.Image = fotoUsuario;
            timerColapsar_menu.Enabled = true;

            int y = 1;
            int y2 = 0;

            int x = pnlMenu.Location.X;
            int x2 = 3;
            for (int i = x; i < 1; i++)
            {
                expandiendo = true;
                Application.DoEvents();
                pnlMenu.Location = new Point(i, y);
                pbActivarMenuDesplegable.Location = new Point(x2, y2);
                i = i + 12;
                x2 = x2 + 12;
            }
            pnlMenu.Location = new Point(1, y);
            pbActivarMenuDesplegable.Location = new Point(x2 + 7, y2);
            expandiendo = true;
            colapsando = false;
        }

        private void colapsar_menu()
        {
            if (colapsando)
                return;
            int x = pnlMenu.Location.X;
            int x2 = pbActivarMenuDesplegable.Location.X + 1;
            int y = Location.Y;
            y = 0;

            for (int i = 0; i >= pnlMenu.Width * -1; i--)
            {
                Application.DoEvents();
                pnlMenu.Location = new Point(i, y);

                if (x2 - Math.Abs(i) >= 1)
                    pbActivarMenuDesplegable.Location = new Point(x2 - Math.Abs(i), 0);

                if (i > -180)
                {
                    i = i - 25;
                }
                else
                {
                    i = i - 1;
                }
            }
            pnlMenu.Location = new Point(0 - pnlMenu.Width + 3, y);
            expandiendo = false;
            colapsando = true;
        }

        public void Enviar(MailMessage mensaje)
        {
            cliente.Send(mensaje);//Enviamos el E-mail
        }

        private void p_refrescar_info_secundaria()
        {
            int vl_no_solicitud = 0;
            if (gv_entradas.RowCount > 0)
            {
                DataGridViewRow rowgrid = gv_entradas.CurrentRow;
                vl_no_solicitud = int.Parse(rowgrid.Cells["no_solicitud"].Value.ToString());
            }
            p_llenar_adjuntos(vl_no_solicitud);
            p_llenar_anotaciones(vl_no_solicitud);
            p_llenar_info_gadget(vl_no_solicitud);
        }

        private void p_llenar_adjuntos(int p_no_solicitud)
        {
            string vl_sql;
            try
            {
                vl_sql = @"Select no_archivo,
                                  nombre_archivo,
                                  extension,
                                  descripcion 
                             From dcs_archivos_adjuntos a,
                                  dcs_wf_tipo_documentos b 
                            Where a.documento_id=b.documento_id 
                              and no_solicitud=:pa_no_solicitud 
                            Order by no_archivo ";
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd2.CommandType = CommandType.Text;
                //─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
                OracleParameter pa_parametro1 = new OracleParameter("pa_no_solicitud", OracleType.Int32);
                cmd2.Parameters.Add(pa_parametro1);
                pa_parametro1.Direction = ParameterDirection.Input;
                pa_parametro1.Value = p_no_solicitud;
                OracleDataReader dr = cmd2.ExecuteReader();
                DataTable tabla = new DataTable();
                tabla.Columns.Add("no_archivo");
                tabla.Columns.Add("nombre_archivo");
                tabla.Columns.Add("extension");
                tabla.Columns.Add("descripcion");
                while (dr.Read())
                {
                    tabla.Rows.Add(dr["no_archivo"].ToString(),
                                   dr["nombre_archivo"].ToString(),
                                   dr["extension"].ToString(),
                                   dr["descripcion"].ToString());
                }
                list_adjuntos.BeginUpdate();
                list_adjuntos.SmallImageList = imagesSmall;
                list_adjuntos.LargeImageList = imagesLarge;
                list_adjuntos.Clear();
                foreach (DataRow fila in tabla.Rows)
                {
                    ListViewItem listItem = new ListViewItem(fila["descripcion"].ToString());
                    listItem.ImageIndex = 3;
                    listItem.ToolTipText = fila["descripcion"].ToString();
                    listItem.SubItems.Add(fila["extension"].ToString());
                    listItem.SubItems.Add(fila["no_archivo"].ToString());
                    list_adjuntos.Items.Add(listItem);
                }
                list_adjuntos.Columns.Add("Nombre Archivo", 180, HorizontalAlignment.Left);
                list_adjuntos.Columns.Add("Ext", 60, HorizontalAlignment.Left);
                list_adjuntos.Columns.Add("No. Documento", 60, HorizontalAlignment.Left);
                list_adjuntos.EndUpdate();
                list_adjuntos.Sort();
                list_adjuntos.View = View.LargeIcon;
                dr.Close();
                if (DocSys.connOracle.State == ConnectionState.Open)
                {
                    DocSys.connOracle.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_llenar_adjuntos : " + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void llenar_anotaciones_excepcion(int codigo_excepcion)
        {
            string vl_orden = " a.no_anotacion ";
            if (rbOrdenCrono.Checked)
                vl_orden = " a.no_anotacion ";
            if (rbOrdenxEstacion.Checked)
                vl_orden = " nombre_estacion,a.no_anotacion ";

            string sql = "select a.no_anotacion, a.no_movimiento_excepcion, a.tipo_anotacion, a.anotacion, "
                        + "a.usuario_ing, a.fecha_ing, e.nombre nombre_estacion "
                        + "from excp.dcs_anotaciones_excepciones a, wfc.dcs_wf_estaciones e "
                        + "where a.codigo_excepcion = :codExcepcion "
                        + "and e.estacion_id = a.estacion_id "
                        + "order by " + vl_orden;
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }

                OracleCommand cmd2 = new OracleCommand(sql, DocSys.connOracle);
                cmd2.CommandType = CommandType.Text;

                OracleParameter pa_parametro1 = new OracleParameter("codExcepcion", OracleType.Int32);
                cmd2.Parameters.Add(pa_parametro1);
                pa_parametro1.Direction = ParameterDirection.Input;
                pa_parametro1.Value = codigo_excepcion;

                OracleDataReader dr = cmd2.ExecuteReader();
                DataTable tabla = new DataTable();

                tabla.Columns.Add("no_anotacion");
                tabla.Columns.Add("no_movimiento_excepcion");
                tabla.Columns.Add("tipo_anotacion");
                tabla.Columns.Add("usuario_ing");
                tabla.Columns.Add("fecha_ing");
                tabla.Columns.Add("nombre_estacion");
                tabla.Columns.Add("anotacion");

                while (dr.Read())
                {
                    tabla.Rows.Add(dr["no_anotacion"].ToString(),
                                   dr["no_movimiento_excepcion"].ToString(),
                                   dr["tipo_anotacion"].ToString(),
                                   dr["usuario_ing"].ToString(),
                                   dr["fecha_ing"].ToString(),
                                   dr["nombre_estacion"].ToString(),
                                   dr["anotacion"].ToString());
                }

                dr.Close();
                if (DocSys.connOracle.State == ConnectionState.Open)
                {
                    DocSys.connOracle.Close();
                }

                list_anotaciones.BeginUpdate();
                list_anotaciones.SmallImageList = imagesSmall;
                list_anotaciones.LargeImageList = imagesLarge;
                list_anotaciones.Clear();
                list_anotaciones.Groups.Clear();

                string vl_estacion = "";
                bool inicio = true;
                int indgrupo = 0;

                foreach (DataRow fila in tabla.Rows)
                {
                    ListViewItem listItem = new ListViewItem(fila["no_anotacion"].ToString());
                    if (fila["tipo_anotacion"].ToString() == "N")
                        listItem.ImageIndex = 4;
                    if (fila["tipo_anotacion"].ToString() == "C")
                        listItem.ImageIndex = 5;

                    listItem.ToolTipText = fila["anotacion"].ToString();

                    if (inicio)
                    {
                        indgrupo = 0;
                        list_anotaciones.Groups.Add(new ListViewGroup(fila["nombre_estacion"].ToString(), HorizontalAlignment.Left));
                        vl_estacion = fila["nombre_estacion"].ToString();
                        inicio = false;
                    }

                    if (fila["nombre_estacion"].ToString() != vl_estacion)
                    {
                        list_anotaciones.Groups.Add(new ListViewGroup(fila["nombre_estacion"].ToString(), HorizontalAlignment.Left));
                        vl_estacion = fila["nombre_estacion"].ToString();
                        indgrupo++;
                    }

                    listItem.Group = list_anotaciones.Groups[indgrupo];
                    listItem.SubItems.Add(fila["usuario_ing"].ToString());
                    listItem.SubItems.Add(fila["fecha_ing"].ToString());
                    list_anotaciones.Items.Add(listItem);
                }

                list_anotaciones.Columns.Add("Anotacion/#", 100, HorizontalAlignment.Left);
                list_anotaciones.Columns.Add("Usuario", 80, HorizontalAlignment.Left);
                list_anotaciones.Columns.Add("Fecha", 140, HorizontalAlignment.Left);
                list_anotaciones.EndUpdate();
                list_anotaciones.Sort();
                list_anotaciones.View = View.Details;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Ha ocurrido un error");
            }
        }

        private void p_llenar_anotaciones(int p_no_solicitud)
        {
            string vl_orden = " anot.no_anotacion ";
            if (rbOrdenCrono.Checked)
                vl_orden = " anot.no_anotacion ";
            if (rbOrdenxEstacion.Checked)
                vl_orden = " nombre_estacion,anot.no_anotacion ";
            int vl_no_mov = 0;

            try
            {
                string vl_sql = @"Select est.nombre nombre_estacion,
                                         anot.no_movimiento_solicitud,
                                         anot.no_anotacion,
                                         anot.anotacion,
                                         anot.tipo_anotacion,
                                         anot.usuario_ing,
                                         anot.fecha_ing  
                                    from dcs_anotaciones_solicitudes anot,
                                         dcs_wf_estaciones est 
                                   where anot.estacion_id=est.estacion_id
                                     and no_solicitud=:pa_no_solicitud 
                                   order by " + vl_orden;
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd2.CommandType = CommandType.Text;

                //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────
                OracleParameter pa_parametro1 = new OracleParameter("pa_no_solicitud", OracleType.Int32);
                cmd2.Parameters.Add(pa_parametro1);
                pa_parametro1.Direction = ParameterDirection.Input;
                pa_parametro1.Value = p_no_solicitud;
                OracleDataReader dr = cmd2.ExecuteReader();
                DataTable tabla = new DataTable();
                tabla.Columns.Add("estacion");
                tabla.Columns.Add("no_anotacion");
                tabla.Columns.Add("anotacion");
                tabla.Columns.Add("tipo_anotacion");
                tabla.Columns.Add("usuario_ing");
                tabla.Columns.Add("fecha_ing");
                while (dr.Read())
                {
                    tabla.Rows.Add(dr["nombre_estacion"].ToString(),
                                   dr["no_anotacion"].ToString(),
                                   dr["anotacion"].ToString(),
                                   dr["tipo_anotacion"].ToString(),
                                   dr["usuario_ing"].ToString(),
                                   dr["fecha_ing"].ToString());
                }
                list_anotaciones.BeginUpdate();
                list_anotaciones.SmallImageList = imagesSmall;
                list_anotaciones.LargeImageList = imagesLarge;
                list_anotaciones.Clear();
                list_anotaciones.Groups.Clear();

                string vl_estacion = "";
                bool inicio = true;
                int indgrupo = 0;
                foreach (DataRow fila in tabla.Rows)
                {
                    ListViewItem listItem = new ListViewItem(fila["no_anotacion"].ToString());
                    if (fila["tipo_anotacion"].ToString() == "N")
                        listItem.ImageIndex = 4;
                    if (fila["tipo_anotacion"].ToString() == "C")
                        listItem.ImageIndex = 5;

                    listItem.ToolTipText = fila["anotacion"].ToString();
                    if (inicio)
                    {
                        indgrupo = 0;
                        list_anotaciones.Groups.Add(new ListViewGroup(fila["estacion"].ToString(), HorizontalAlignment.Left));
                        vl_estacion = fila["estacion"].ToString();
                        inicio = false;
                    }
                    if (fila["estacion"].ToString() != vl_estacion)
                    {
                        list_anotaciones.Groups.Add(new ListViewGroup(fila["estacion"].ToString(), HorizontalAlignment.Left));
                        vl_estacion = fila["estacion"].ToString();
                        indgrupo++;
                    }
                    listItem.Group = list_anotaciones.Groups[indgrupo];
                    listItem.SubItems.Add(fila["usuario_ing"].ToString());
                    listItem.SubItems.Add(fila["fecha_ing"].ToString());
                    list_anotaciones.Items.Add(listItem);
                }
                list_anotaciones.Columns.Add("Anotacion/#", 100, HorizontalAlignment.Left);
                list_anotaciones.Columns.Add("Usuario", 80, HorizontalAlignment.Left);
                list_anotaciones.Columns.Add("Fecha", 140, HorizontalAlignment.Left);
                list_anotaciones.EndUpdate();
                list_anotaciones.Sort();
                list_anotaciones.View = View.Details;
                dr.Close();
                if (DocSys.connOracle.State == ConnectionState.Open)
                {
                    DocSys.connOracle.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_llenar_anotaciones : " + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void p_llenar_estaciones_usuario()
        {
            comboBoxEstacionUsuario.DataSource = da.ObtenerEstacionesxUsuario(DocSys.vl_user);
            comboBoxEstacionUsuario.DisplayMember = "nombre";
            comboBoxEstacionUsuario.ValueMember = "estacion_id";
            comboBoxEstacionUsuario_SelectionChangeCommitted(null, null);
        }

        private void p_llenar_zonas()
        {
            cmbZonaExcepcion.DataSource = da.ObtenerZonasExcp();
            cmbZonaExcepcion.DisplayMember = "DESCRIPCION";
            cmbZonaExcepcion.ValueMember = "COD_ZONA";
        }

        private int p_get_cant_respuestas_paso(int p_paso)
        {
            int vl_return = 0;
            string vl_sql = "select count(*) cant_respuestas from dcs_wf_flujos where paso=:pa_paso and decision_id>0";
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd2.CommandType = CommandType.Text;
                //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────
                OracleParameter pa_prametro1 = new OracleParameter("pa_paso", OracleType.Int32);
                cmd2.Parameters.Add(pa_prametro1);
                pa_prametro1.Direction = ParameterDirection.Input;
                pa_prametro1.Value = p_paso;
                //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────                          
                OracleDataReader dr = cmd2.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    vl_return = int.Parse(dr["cant_respuestas"].ToString());
                }
                else
                {
                    vl_return = 0;
                }
                dr.Close();
                if (DocSys.connOracle.State == ConnectionState.Open)
                {
                    DocSys.connOracle.Close();
                }
                return vl_return;
            }
            catch (Exception ex)
            {
                return vl_return;
            }
        }

        private int p_get_paso_actual(int p_no_solicitud)
        {
            int vl_return = 0;
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string vl_sql = "select * from dcs_solicitudes where no_solicitud=:pa_no_solicitud";
                OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd2.CommandType = CommandType.Text;
                //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────
                OracleParameter pa_prametro1 = new OracleParameter("pa_no_solicitud", OracleType.Int32);
                cmd2.Parameters.Add(pa_prametro1);
                pa_prametro1.Direction = ParameterDirection.Input;
                pa_prametro1.Value = p_no_solicitud;
                //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────             
                OracleDataReader dr = cmd2.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    vl_return = int.Parse(dr["paso_actual"].ToString());
                }
                else
                {
                    vl_return = 0;
                }
                dr.Close();
                if (DocSys.connOracle.State == ConnectionState.Open)
                {
                    DocSys.connOracle.Close();
                }
                return vl_return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_get_paso_actual :" + ex.Message + " " + ex.Source, "::DocSis", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return vl_return;
            }
        }

        private int p_get_estado_solicitud(int p_no_solicitud)
        {
            int vl_return = 0;
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string vl_sql = "select estado_solicitud_id from dcs_solicitudes where no_solicitud=:pa_no_solicitud";
                OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd2.CommandType = CommandType.Text;
                //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────
                OracleParameter pa_prametro1 = new OracleParameter("pa_no_solicitud", OracleType.Int32);
                cmd2.Parameters.Add(pa_prametro1);
                pa_prametro1.Direction = ParameterDirection.Input;
                pa_prametro1.Value = p_no_solicitud;
                //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────               
                OracleDataReader dr = cmd2.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    vl_return = int.Parse(dr["estado_solicitud_id"].ToString());
                }
                else
                {
                    vl_return = 0;
                }
                dr.Close();
                if (DocSys.connOracle.State == ConnectionState.Open)
                {
                    DocSys.connOracle.Close();
                }
                return vl_return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_get_paso_actual :" + ex.Message + " " + ex.Source, "::DocSis", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return vl_return;
            }
        }

        private void p_popular_lista_salidas(string p_filtro)
        {
            p_mostrar_espera("Cargado información", false);
            string vl_condi_toda_filial = "";
            string vl_condi_gte_filial = "";
            string vl_condi_analista = "";

            if (global_ver_toda_filial)
            {
                vl_condi_toda_filial = " and :pa_codigo_agencia_origen=:pa_codigo_agencia_origen";
            }
            else
            {
                vl_condi_toda_filial = " and s.codigo_agencia_origen=:pa_codigo_agencia_origen ";
                if (global_gerente_filial)
                {
                    vl_condi_gte_filial = "";
                }
                else
                {
                    if (global_es_admon_sistema || (global_es_admon_temp && global_tipo_admon_temp_sol))
                    {
                        vl_condi_gte_filial = "";
                    }
                    else
                    {
                        vl_condi_gte_filial = " and oficial_servicio='" + DocSys.vl_user.Trim() + "'";
                    }
                }
            }
            gv_entradas.AutoGenerateColumns = false;
            gv_entradas.DataSource = da.Llenar_lista_salidas(global_estacion_id_to, DocSys.vl_agencia_usuario, vl_condi_toda_filial, vl_condi_gte_filial, vl_condi_analista);
            gv_entradas.Refresh();
            gv_entradas.Columns["estacion_actual"].HeaderText = "Enviado a ";
            panelCargandoSolic.Visible = false;
        }

        private void p_popular_lista_entradas_consulta(string p_filtro)
        {
            string vl_condi_toda_filial = "";
            if (global_ver_toda_filial)
            {
                vl_condi_toda_filial = " and :pa_codigo_agencia_origen=:pa_codigo_agencia_origen";
            }
            else
            {
                vl_condi_toda_filial = " and s.codigo_agencia_origen=:pa_codigo_agencia_origen ";
            }
            string vl_sql = @"Select s.no_solicitud,
                                     initcap(desc_sub_aplicacion) desc_sub_aplicacion,
                                     trunc(fecha_presentacion) fecha_presentacion,
                                     initcap(nombre_agencia) nombre_agencia,
                                     s.codigo_cliente,
                                     f.descripcion_fuente,
                                     initcap(nombres)||' '||initcap(primer_apellido)||' '||initcap(segundo_apellido) nombre_cliente,                                     
                                     monto_solicitado,
                                     meses_plazo,
                                     ms.attr_nueva,
                                     leido,
                                     leido_por,
                                     fecha_leido,
                                     no_movimiento,
                                     con_adjunto                            
                                from dcs_solicitudes s,
                                     dcs_movimientos_solicitudes ms,
                                     mgi_sub_aplicaciones sa,
                                     mgi_agencias a,
                                     mgi_clientes c,                              
                                     dcs_wf_fuentes_financiamiento f
                               where s.no_movimiento_actual=ms.no_movimiento
                                 and s.codigo_sub_aplicacion=sa.codigo_sub_aplicacion
                                 and s.codigo_fuente=f.codigo_fuente
                                 and s.codigo_agencia_origen=a.codigo_agencia
                                 and s.codigo_cliente=c.codigo_cliente      
                                 and s.workflow_id=:pa_workflow_id " + vl_condi_toda_filial + @"                    
                                 and desc_sub_aplicacion||trunc(fecha_presentacion) ||upper(nombre_agencia)||s.codigo_cliente||initcap(nombres)||' '||initcap(primer_apellido)||' '||initcap(segundo_apellido)  like '%" + txtTexto_buscar.Text + @"%' 
                               Order by s.no_solicitud desc";
            if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
            {
                DocSys.connOracle.Open();
            }
            OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
            cmd2.CommandType = CommandType.Text;
            //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────
            OracleParameter pa_workflow_id = new OracleParameter("pa_workflow_id", OracleType.Int32);
            cmd2.Parameters.Add(pa_workflow_id);
            pa_workflow_id.Direction = ParameterDirection.Input;
            pa_workflow_id.Value = DocSys.vl_workflow_id;
            //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────
            OracleParameter pa_codigo_agencia_origen = new OracleParameter("pa_codigo_agencia_origen", OracleType.Int32);
            cmd2.Parameters.Add(pa_codigo_agencia_origen);
            pa_codigo_agencia_origen.Direction = ParameterDirection.Input;
            pa_codigo_agencia_origen.Value = DocSys.vl_agencia_usuario;
            OracleDataReader dr = cmd2.ExecuteReader();
            DataTable table = new DataTable();
            table.Columns.Add("no_solicitud");
            table.Columns.Add("desc_sub_aplicacion");
            table.Columns.Add("fecha_presentacion");
            table.Columns.Add("nombre_agencia");
            table.Columns.Add("descripcion_fuente");
            table.Columns.Add("codigo_cliente");
            table.Columns.Add("nombre_cliente");
            table.Columns.Add("monto_solicitado");
            table.Columns.Add("meses_plazo");
            table.Columns.Add("attr_nueva");
            table.Columns.Add("leido");
            table.Columns.Add("no_movimiento");
            table.Columns.Add("con_adjunto");
            while (dr.Read())
            {
                table.Rows.Add(dr["no_solicitud"].ToString(),
                               dr["desc_sub_aplicacion"].ToString(),
                               dr["fecha_presentacion"].ToString(),
                               dr["nombre_agencia"].ToString(),
                               dr["descripcion_fuente"].ToString(),
                               dr["codigo_cliente"].ToString(),
                               dr["nombre_cliente"].ToString(),
                               String.Format("{0:###,###,###,##0.00}", float.Parse(dr["monto_solicitado"].ToString())),
                               String.Format("{0:####}", float.Parse(dr["meses_plazo"].ToString())),
                               dr["attr_nueva"].ToString(),
                               dr["leido"].ToString(),
                               dr["no_movimiento"].ToString(),
                               dr["con_adjunto"].ToString());
            }
            gv_entradas.DataSource = table;
            gv_entradas.Refresh();
            table.Dispose();
            dr.Close();
            if (DocSys.connOracle.State == ConnectionState.Open)
            {
                DocSys.connOracle.Close();
            }
        }

        private void p_popular_lista_denegadas(string p_filtro)
        {
            p_mostrar_espera("Cargado información", false);
            string vl_condi_toda_filial = "";
            string vl_condi_gte_filial = "";
            string vl_condi_analista = "";

            if (global_ver_toda_filial)
            {
                vl_condi_toda_filial = " and :pa_codigo_agencia_origen=:pa_codigo_agencia_origen";
                vl_condi_analista = "";
                if (MenuItem_verSoloMisSolicitudes.Checked)
                    vl_condi_analista = " and ms.analista='" + DocSys.vl_user + "'";
            }
            else
            {
                vl_condi_toda_filial = " and s.codigo_agencia_origen=:pa_codigo_agencia_origen ";
                if (global_gerente_filial)
                {
                    vl_condi_gte_filial = "";
                }
                else
                {
                    if (global_es_admon_sistema || (global_es_admon_temp && global_tipo_admon_temp_sol))
                    {
                        vl_condi_gte_filial = "";
                    }
                    else
                    {
                        vl_condi_gte_filial = " and oficial_servicio='" + DocSys.vl_user.Trim() + "'";
                    }
                }
            }
            gv_entradas.AutoGenerateColumns = false;
            gv_entradas.DataSource = da.Llenar_lista_rechazadas(global_estacion_id_to, DocSys.vl_agencia_usuario, vl_condi_toda_filial, vl_condi_gte_filial, vl_condi_analista);
            gv_entradas.Refresh();
            panelCargandoSolic.Visible = false;
        }

        private void p_popular_lista_vacio(string p_filtro)
        {
            DataTable table = new DataTable();

            table.Columns.Add("no_solicitud");
            table.Columns.Add("desc_sub_aplicacion");
            table.Columns.Add("fecha_presentacion");
            table.Columns.Add("nombre_agencia");
            table.Columns.Add("oficial_servicio");
            table.Columns.Add("analista");
            table.Columns.Add("descripcion_fuente");
            table.Columns.Add("desc_moneda");
            table.Columns.Add("codigo_cliente");
            table.Columns.Add("nombre_cliente");
            table.Columns.Add("monto_solicitado");
            table.Columns.Add("meses_plazo");
            table.Columns.Add("attr_nueva");
            table.Columns.Add("leido");
            table.Columns.Add("no_movimiento");
            table.Columns.Add("con_adjunto");
            table.Columns.Add("estacion_actual");
            table.Columns.Add("banderin_id");
            gv_entradas.DataSource = table;
            gv_entradas.Refresh();
        }

        private void p_popular_lista_por_carpetas(string p_carpeta_id)
        {
            p_mostrar_espera("Cargado información", false);

            gv_entradas.AutoGenerateColumns = false;
            gv_entradas.DataSource = da.Llenar_lista_por_carpetas(Int32.Parse(p_carpeta_id));
            gv_entradas.Columns["estacion_actual"].HeaderText = "Estación Actual";
            gv_entradas.Refresh();
            panelCargandoSolic.Visible = false;
        }

        private void p_set_grid_attr_visual()
        {
            for (int row = 0; row < gv_entradas.Rows.Count; row++)
            {
                DataGridViewCell valueCell = null;
                DataGridViewImageCell displayCell = null;

                if (gv_entradas.Rows[row].Cells["attr_nueva"].Value.ToString() == "S")
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.Font = new Font(gv_entradas.Font, FontStyle.Bold);
                    DataGridViewRow theRow = gv_entradas.Rows[row];
                    theRow.DefaultCellStyle = style;

                    valueCell = gv_entradas[img_estado_registro.Index, row];
                    displayCell = (DataGridViewImageCell)gv_entradas[img_estado_registro.Index, row];
                    displayCell.Value = image_grid.Images[2];
                }
                else
                {
                    valueCell = gv_entradas[img_estado_registro.Index, row];
                    displayCell = (DataGridViewImageCell)gv_entradas[img_estado_registro.Index, row];
                    displayCell.Value = image_grid.Images[3];
                }
                //Adjunto
                if (gv_entradas.Rows[row].Cells["con_adjunto"].Value.ToString() == "S")
                {
                    valueCell = gv_entradas[img_tiene_adjunto.Index, row];
                    displayCell = (DataGridViewImageCell)gv_entradas[img_tiene_adjunto.Index, row];
                    displayCell.Value = image_grid.Images[1];
                }
                else
                {
                    valueCell = gv_entradas[img_tiene_adjunto.Index, row];
                    displayCell = (DataGridViewImageCell)gv_entradas[img_tiene_adjunto.Index, row];
                    displayCell.Value = image_grid.Images[0];
                }
                //Seguimiento (banderines)
                valueCell = gv_entradas[img_seguimiento.Index, row];
                displayCell = (DataGridViewImageCell)gv_entradas[img_seguimiento.Index, row];
                displayCell.Value = imageSeguimiento.Images[int.Parse(gv_entradas.Rows[row].Cells["banderin_id"].Value.ToString())];

                int vl_estacion_id_from = p_get_estacion_from(int.Parse(gv_entradas.Rows[row].Cells["no_solicitud"].Value.ToString()));
                if (vl_estacion_id_from > 0)
                {
                    int r = 0;
                    int g = 0;
                    int b = 0;
                    p_get_color_rgb_fila_estacion_from(vl_estacion_id_from, out r, out g, out b);
                    if (r > 0 | g > 0 | b > 0)
                        gv_entradas.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(r, g, b);
                }
            }
        }

        private void p_eliminar_adjunto(int p_no_archivo)
        {
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string vl_sql = "";
                vl_sql = vl_sql + "DCS_P_ELIMINAR_ADJUNTO";
                OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd.CommandType = CommandType.StoredProcedure;
                //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────
                OracleParameter pa_no_movimiento = new OracleParameter("pa_no_archivo", OracleType.Int32);
                cmd.Parameters.Add(pa_no_movimiento);
                pa_no_movimiento.Direction = ParameterDirection.Input;
                pa_no_movimiento.Value = p_no_archivo;
                //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────
                cmd.ExecuteReader();
                p_refrescar_info();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_marca_como_leido :" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void p_eliminar_solicitud(int p_no_solicitud)
        {
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string vl_sql = "";
                vl_sql = vl_sql + "DCS_P_ELIMINAR_SOLICITUD";
                OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd.CommandType = CommandType.StoredProcedure;
                //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────
                OracleParameter pa_no_movimiento = new OracleParameter("pa_no_solicitud", OracleType.Int32);
                cmd.Parameters.Add(pa_no_movimiento);
                pa_no_movimiento.Direction = ParameterDirection.Input;
                pa_no_movimiento.Value = p_no_solicitud;
                //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────
                cmd.ExecuteReader();
                p_refrescar_info();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_eliminar_solicitud :" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void p_marca_como_leido(int p_no_movimiento)
        {
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string vl_sql = "";
                vl_sql = vl_sql + "DCS_P_MARCAR_LECTURA";
                OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd.CommandType = CommandType.StoredProcedure;
                //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────
                OracleParameter pa_no_movimiento = new OracleParameter("pa_no_movimiento", OracleType.Int32);
                cmd.Parameters.Add(pa_no_movimiento);
                pa_no_movimiento.Direction = ParameterDirection.Input;
                pa_no_movimiento.Value = p_no_movimiento;
                //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────
                cmd.ExecuteReader();

                Int32 nfila = gv_entradas.CurrentRow.Index;
                DataGridViewCell valueCell = null;
                DataGridViewImageCell displayCell = null;
                if (gv_entradas.Rows[nfila].Cells["attr_nueva"].Value.ToString() == "S")
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.Font = new Font(gv_entradas.Font, FontStyle.Regular);
                    DataGridViewRow theRow = gv_entradas.Rows[nfila];
                    theRow.DefaultCellStyle = style;

                    valueCell = gv_entradas[img_estado_registro.Index, nfila];
                    displayCell = (DataGridViewImageCell)gv_entradas[img_estado_registro.Index, nfila];
                    displayCell.Value = image_grid.Images[2];
                }

                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_marca_como_leido :" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void p_llenar_combo_workflows()
        {
            try
            {
                DataSet dsCombo = new DataSet();
                dsCombo = DocSys.p_Obtener_un_dataset("select workflow_id,nombre_workflow from dcs_workflows where activo='S'", "dcs_workflows");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en llenar combox Workflows : " + ex.Message);
            }
        }

        private int p_get_cant_solic_entradas(int p_workflow_id)
        {
            int vl_return = 0;

            try
            {
                string vl_condi_toda_filial = "";
                string vl_condi_gte_filial = "";
                if (global_ver_toda_filial)
                {
                    vl_condi_toda_filial = " and :pa_codigo_agencia_origen=:pa_codigo_agencia_origen";
                }
                else
                {
                    vl_condi_toda_filial = " and s.codigo_agencia_origen=:pa_codigo_agencia_origen ";
                    if (global_gerente_filial)
                    {
                        vl_condi_gte_filial = "";
                    }
                    else
                    {
                        vl_condi_gte_filial = " and oficial_servicio='" + DocSys.vl_user.Trim() + "'";
                    }
                }

                string sql = @"Select count(*) as solicitudes_nuevas
                                 from dcs_solicitudes s,
                                      dcs_movimientos_solicitudes ms
                                where s.no_movimiento_actual=ms.no_movimiento
                                  and ms.attr_nueva='S'          
                                  and s.estado_solicitud_id=2                           
                                  and s.workflow_id=:pa_workflow_id
                                  and s.estacion_id=:pa_estacion_id_to " +
                                 vl_condi_toda_filial;

                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                OracleCommand cmd2 = new OracleCommand(sql, DocSys.connOracle);
                cmd2.CommandType = CommandType.Text;
                //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────
                OracleParameter pa_workflow_id = new OracleParameter("pa_workflow_id", OracleType.Int32);
                cmd2.Parameters.Add(pa_workflow_id);
                pa_workflow_id.Direction = ParameterDirection.Input;
                pa_workflow_id.Value = DocSys.vl_workflow_id;
                //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────
                OracleParameter pa_estacion_id_to = new OracleParameter("pa_estacion_id_to", OracleType.Int32);
                cmd2.Parameters.Add(pa_estacion_id_to);
                pa_estacion_id_to.Direction = ParameterDirection.Input;
                pa_estacion_id_to.Value = global_estacion_id_to;
                //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────
                OracleParameter pa_codigo_agencia_origen = new OracleParameter("pa_codigo_agencia_origen", OracleType.Int32);
                cmd2.Parameters.Add(pa_codigo_agencia_origen);
                pa_codigo_agencia_origen.Direction = ParameterDirection.Input;
                pa_codigo_agencia_origen.Value = DocSys.vl_agencia_usuario;

                OracleDataReader dr = cmd2.ExecuteReader();
                DataTable table = new DataTable();
                dr.Read();
                vl_return = int.Parse(dr["solicitudes_nuevas"].ToString());
                dr.Close();
                if (DocSys.connOracle.State == ConnectionState.Open)
                {
                    DocSys.connOracle.Close();
                }
                return vl_return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_get_cant_solic_entradas :" + ex.Message);
                return vl_return;
            }
        }

        private int p_get_cant_solic_denegadas(int p_workflow_id)
        {
            int vl_return = 0;
            try
            {
                string vl_condi_toda_filial = "";
                string vl_condi_gte_filial = "";
                if (global_ver_toda_filial)
                {
                    vl_condi_toda_filial = " and :pa_codigo_agencia_origen=:pa_codigo_agencia_origen";
                }
                else
                {
                    vl_condi_toda_filial = " and s.codigo_agencia_origen=:pa_codigo_agencia_origen ";
                    if (global_gerente_filial)
                    {
                        vl_condi_gte_filial = "";
                    }
                    else
                    {
                        vl_condi_gte_filial = " and oficial_servicio='" + DocSys.vl_user.Trim() + "'";
                    }
                }

                string sql = @"Select count(*) as solicitudes_nuevas
                                 from dcs_solicitudes s,
                                      dcs_movimientos_solicitudes ms
                                where s.no_movimiento_actual=ms.no_movimiento
                                  and ms.attr_nueva='S'          
                                  and s.estado_solicitud_id=99                          
                                  and s.workflow_id=:pa_workflow_id
                                  and s.estacion_id=:pa_estacion_id_to " +
                                 vl_condi_toda_filial;

                if (DocSys.connOracle.State == ConnectionState.Closed)
                {
                    DocSys.connOracle.Open();
                }
                OracleCommand cmd2 = new OracleCommand(sql, DocSys.connOracle);
                cmd2.CommandType = CommandType.Text;
                //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────
                OracleParameter pa_workflow_id = new OracleParameter("pa_workflow_id", OracleType.Int32);
                cmd2.Parameters.Add(pa_workflow_id);
                pa_workflow_id.Direction = ParameterDirection.Input;
                pa_workflow_id.Value = DocSys.vl_workflow_id;
                //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────
                OracleParameter pa_estacion_id_to = new OracleParameter("pa_estacion_id_to", OracleType.Int32);
                cmd2.Parameters.Add(pa_estacion_id_to);
                pa_estacion_id_to.Direction = ParameterDirection.Input;
                pa_estacion_id_to.Value = global_estacion_id_to;
                //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────
                OracleParameter pa_codigo_agencia_origen = new OracleParameter("pa_codigo_agencia_origen", OracleType.Int32);
                cmd2.Parameters.Add(pa_codigo_agencia_origen);
                pa_codigo_agencia_origen.Direction = ParameterDirection.Input;
                pa_codigo_agencia_origen.Value = DocSys.vl_agencia_usuario;

                OracleDataReader dr = cmd2.ExecuteReader();
                DataTable table = new DataTable();
                dr.Read();
                vl_return = int.Parse(dr["solicitudes_nuevas"].ToString());
                dr.Close();
                if (DocSys.connOracle.State == ConnectionState.Open)
                {
                    DocSys.connOracle.Close();
                }
                return vl_return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_get_cant_solic_entradas :" + ex.Message);
                return vl_return;
            }
        }

        private int p_get_estacion_from(int p_no_solicitud)
        {
            int vl_return = 0;
            string sql = @"Select estacion_id_from
                                 from dcs_solicitudes s,
                                      dcs_movimientos_solicitudes ms
                                where s.no_movimiento_actual=ms.no_movimiento
                                  and s.no_solicitud=:pa_no_solicitud";

            if (DocSys.connOracle.State == ConnectionState.Closed)
            {
                DocSys.connOracle.Open();
            }
            OracleCommand cmd2 = new OracleCommand(sql, DocSys.connOracle);
            cmd2.CommandType = CommandType.Text;
            //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────
            OracleParameter pa_no_solicitud = new OracleParameter("pa_no_solicitud", OracleType.Int32);
            cmd2.Parameters.Add(pa_no_solicitud);
            pa_no_solicitud.Direction = ParameterDirection.Input;
            pa_no_solicitud.Value = p_no_solicitud;

            OracleDataReader dr = cmd2.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                vl_return = int.Parse(dr["estacion_id_from"].ToString());
            }
            dr.Close();
            if (DocSys.connOracle.State == ConnectionState.Open)
            {
                DocSys.connOracle.Close();

            }
            return vl_return;
        }

        public int p_get_estacion_current(int p_no_solicitud)
        {
            int vl_return = 0;
            string sql = @"Select estacion_id
                            from dcs_solicitudes s                                      
                           where no_solicitud=:pa_no_solicitud";

            if (DocSys.connOracle.State == ConnectionState.Closed)
            {
                DocSys.connOracle.Open();
            }
            OracleCommand cmd2 = new OracleCommand(sql, DocSys.connOracle);
            cmd2.CommandType = CommandType.Text;
            //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────
            OracleParameter pa_no_solicitud = new OracleParameter("pa_no_solicitud", OracleType.Int32);
            cmd2.Parameters.Add(pa_no_solicitud);
            pa_no_solicitud.Direction = ParameterDirection.Input;
            pa_no_solicitud.Value = p_no_solicitud;

            OracleDataReader dr = cmd2.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                vl_return = int.Parse(dr["estacion_id"].ToString());
            }
            dr.Close();
            if (DocSys.connOracle.State == ConnectionState.Open)
            {
                DocSys.connOracle.Close();
            }
            return vl_return;
        }

        private void p_get_color_rgb_fila_estacion_from(int p_estacion_id_from, out int p_esquema_rgb_R, out int p_esquema_rgb_G, out int p_esquema_rgb_B)
        {
            string sql = @"Select * 
                            From dcs_wf_usuarios_preferencias 
                            Where usuario=:pa_usuario
                              and estacion_id_from=:pa_estacion_id_from";

            if (DocSys.connOracle.State == ConnectionState.Closed)
            {
                DocSys.connOracle.Open();
            }
            OracleCommand cmd2 = new OracleCommand(sql, DocSys.connOracle);
            cmd2.CommandType = CommandType.Text;
            //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────
            OracleParameter pa_usuario = new OracleParameter("pa_usuario", OracleType.VarChar, 50);
            cmd2.Parameters.Add(pa_usuario);
            pa_usuario.Direction = ParameterDirection.Input;
            pa_usuario.Value = DocSys.vl_user;

            OracleParameter pa_estacion_id_from = new OracleParameter("pa_estacion_id_from", OracleType.Int32);
            cmd2.Parameters.Add(pa_estacion_id_from);
            pa_estacion_id_from.Direction = ParameterDirection.Input;
            pa_estacion_id_from.Value = p_estacion_id_from;
            OracleDataReader dr = cmd2.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                p_esquema_rgb_R = int.Parse(dr["color_rgb_r"].ToString());
                p_esquema_rgb_G = int.Parse(dr["color_rgb_g"].ToString());
                p_esquema_rgb_B = int.Parse(dr["color_rgb_b"].ToString());
            }
            else
            {
                p_esquema_rgb_R = 0;
                p_esquema_rgb_G = 0;
                p_esquema_rgb_B = 0;
            }
            dr.Close();
            if (DocSys.connOracle.State == ConnectionState.Open)
            {
                DocSys.connOracle.Close();
            }
        }

        private void p_copiar_solicitud_a_carpeta()
        {
            int vl_no_solicitud;
            DataGridViewRow row = gv_entradas.CurrentRow;
            if (gv_entradas.RowCount > 0)
            {
                vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());

                s_carpetas_opciones_add forma = new s_carpetas_opciones_add(vl_no_solicitud);
                DialogResult res = forma.ShowDialog();
                if (res == DialogResult.OK)
                {
                    p_popular_treview();
                }
            }
        }

        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }
        #region Eventos de Objetos

        //FELVIR01- 50190726
        private void panel_mensajes_Click(object sender, EventArgs e)
        {
            s_mensajes_detalle forma = new s_mensajes_detalle(this.da);
            forma.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            p_refrescar_info();
        }

        private void txtTexto_buscar_Enter(object sender, EventArgs e) { }

        private void gv_Entradas_Sorted(object sender, EventArgs e)
        {
            p_set_grid_attr_visual();
        }

        private void gv_Entradas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            p_set_grid_attr_visual();
        }

        private void Button_nueva_solicitud_Click(object sender, EventArgs e)
        {
            if (global_estacion_id_to == 0)
            {
                MessageBox.Show("El usuario no tiene definida una estación de trabajo..!", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (DocSys.p_get_permitir_crear_solicitud(global_estacion_id_to) == "N")
            {
                MessageBox.Show("La estacion " + DocSys.p_get_nombre_estacion(global_estacion_id_to) + " no puede crear solicitudes ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            
            btnNuevaSolic_Click(null, null);
        }

        private void Button_enviar_Click(object sender, EventArgs e)
        {
            if (global_estacion_id_to == 0)
            {
                MessageBox.Show("El usuario no tiene definida una estación de trabajo..!", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (treeView.SelectedNode.Name.ToString().ToUpper() != "NODE_ENTRADAS")
                return;
            if (gv_entradas.RowCount == 0)
            {
                return;
            }

            if (!DocSys.p_valida_politicas_envio_credito())
            {
                return;
            }

            int vl_no_solicitud;
            DataGridViewRow row = gv_entradas.CurrentRow;
            vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());

            if (global_estacion_id_to != DocSys.p_get_estacion_current(vl_no_solicitud))
            {
                MessageBox.Show("La solicitud que esta tratando de enviar ya fue procesada por otro usuario !", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                p_refrescar_info();
                return;
            }

            int vl_paso_sig = (p_get_paso_actual(vl_no_solicitud));

            if (p_get_cant_respuestas_paso(vl_paso_sig) <= 0)
            {
                MessageBox.Show("No hay mas pasos definidos en el Flujo de procesos ", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (gv_entradas.Rows.Count <= 0)
            {
                return;
            }
            s_enviar_sol_wf forma = new s_enviar_sol_wf(DocSys.vl_workflow_id, vl_no_solicitud);
            DialogResult res = forma.ShowDialog();
            if (res == DialogResult.OK)
            {
                p_refrescar_info();
            }
        }

        private void MenuItem_marca_leido_Click(object sender, EventArgs e)
        {
            if (gv_entradas.Rows.Count <= 0)
            {
                return;
            }
            int vl_no_movimiento;
            DataGridViewRow row = gv_entradas.CurrentRow;
            vl_no_movimiento = int.Parse(row.Cells["no_movimiento"].Value.ToString());
            p_marca_como_leido(vl_no_movimiento);
        }

        private void gv_Entradas_SelectionChanged(object sender, EventArgs e)
        {
            int vl_no_solicitud = 0;
            DataGridViewRow row = gv_entradas.CurrentRow;
            if (row != null)
            {
                vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());
            }
            p_llenar_info_gadget(vl_no_solicitud);
            p_llenar_adjuntos(vl_no_solicitud);
            p_llenar_anotaciones(vl_no_solicitud);
            int paso = p_get_paso_actual(vl_no_solicitud);
            if (paso == 10 || paso == 11)
            {
                btnCerrar_solicitud.Visible = true;
            }
            else
            {
                btnCerrar_solicitud.Visible = false;
            }
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            p_refrescar_info();
        }

        private void MenuItem_barraDeNavegación_Click(object sender, EventArgs e)
        {
            if (MenuItem_barraDeNavegación.Checked)
            {
                MenuItem_barraDeNavegación.Checked = false;
                splitContainerVertical.Panel1Collapsed = true;
            }
            else
            {
                MenuItem_barraDeNavegación.Checked = true;
                splitContainerVertical.Panel1Collapsed = false;
            }
        }

        private void MenuItem_panelDeInformacion_Click(object sender, EventArgs e)
        {
            if (MenuItem_panelDeInformacion.Checked)
            {
                MenuItem_panelDeInformacion.Checked = false;
                splitContainerHorizontal.Panel2Collapsed = true;
            }
            else
            {
                MenuItem_panelDeInformacion.Checked = true;
                splitContainerHorizontal.Panel2Collapsed = false;
            }
        }

        private void MenuItem_barraDeEstado_Click(object sender, EventArgs e)
        {
            if (MenuItem_barraDeEstado.Checked)
            {
                MenuItem_barraDeEstado.Checked = false;
            }
            else
            {
                MenuItem_barraDeEstado.Checked = true;
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            list_adjuntos.View = View.Details;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            list_adjuntos.View = View.LargeIcon;
        }

        private void radioButton_documentos_viewtitulos_CheckedChanged(object sender, EventArgs e)
        {
            list_adjuntos.View = View.Tile;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            list_adjuntos.View = View.Details;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Icon icono;
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Seleccione un archivo tipo Imagen";
            openFileDialog.InitialDirectory = Application.StartupPath + "\\logs";
            openFileDialog.Filter = "Todos (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string vl_file = openFileDialog.FileName;
                string vl_only_file = openFileDialog.SafeFileName;
                openFileDialog.Dispose();
                icono = System.Drawing.Icon.ExtractAssociatedIcon(vl_file);
                pictureBox4.Image = icono.ToBitmap();
            }
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                cliente.Credentials = new NetworkCredential("gparedeshn@gmail.com", "sysadm2006");
                cliente.EnableSsl = true;

                MailMessage mnsj = new MailMessage();
                mnsj.Subject = "Hola Mundo"; //asunto
                mnsj.To.Add(new MailAddress("giovany_paredes@sagradafamilia.hn")); //Destinatario
                mnsj.From = new MailAddress("gparedeshn@gmail.com", "Giovany Paredes");//remitente
                OpenFileDialog of = new OpenFileDialog();
                of.ShowDialog();
                string direccion = of.FileName;
                mnsj.Attachments.Add(new Attachment(direccion));//Cargamos el archivo a enviar
                mnsj.Body = "Probando \n\n Enviado desde C#\n\n *UNETE AL GRUPO DE C SHARP .NET PROGRAMACION MEXICO en facebook*";//Contenido del E-mail
                cliente.Send(mnsj);

                MessageBox.Show("El E-Mail se ha Enviado", "Enviado...", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void label_Titulo_lista_MouseDown(object sender, MouseEventArgs e)
        {
            Movimiento = true;
            mtop = e.Y;
            mleft = e.X;
        }

        private void label_Titulo_lista_MouseUp(object sender, MouseEventArgs e)
        {
            Movimiento = false;
        }

        private void label_Titulo_lista_MouseMove(object sender, MouseEventArgs e)
        {
            if (Movimiento)
            {
                Point p1 = label_Titulo_lista.PointToScreen(e.Location);
                p1.X -= mleft;
                p1.Y -= mtop;
                Point p2 = label_Titulo_lista.Parent.PointToClient(p1);
                label_Titulo_lista.Location = p2;
            }
        }

        private void list_adjuntos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = list_adjuntos.GetItemAt(e.X, e.Y);
            if (item != null)
            {
                p_abrir_adjunto(int.Parse(item.SubItems[2].Text));
            }
        }

        private void list_anotaciones_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = list_anotaciones.GetItemAt(e.X, e.Y);
            if (item != null)
            {
                p_abrir_anotacion(int.Parse(item.SubItems[0].Text));
            }
        }

        private void list_anotaciones_SelectedIndexChanged(object sender, EventArgs e) {}

        private void nuevaSolicitudToolStripMenuItem_Click(object sender, EventArgs e) {
            btnNuevaSolic_Click(null, null);
        }

        private void movimientosDeLaSolicitudToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int vl_no_solicitud = 0;
            int vl_no_movimiento = 0;
            if (gv_entradas.RowCount > 0)
            {
                DataGridViewRow row = gv_entradas.CurrentRow;
                vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());
                vl_no_movimiento = int.Parse(row.Cells["no_movimiento"].Value.ToString());

                s_solicitud_detalle forma = new s_solicitud_detalle(vl_no_solicitud, vl_no_movimiento);
                forma.ShowDialog();
            }
        }

        private void MenuItem_panelPiePantalla_Click(object sender, EventArgs e)
        {
            if (MenuItem_panelPiePantalla.Checked)
            {
                MenuItem_panelPiePantalla.Checked = false;
                panel_footer.Height = 0;
                panelprincipal.Height = panelprincipal.Height + 64;
            }
            else
            {
                MenuItem_panelPiePantalla.Checked = true;
                panel_footer.Height = 64;
                panelprincipal.Height = panelprincipal.Height - 64;
            }
        }

        private void p_insertar_adjunto(string p_file, string p_only_file, string p_extension)
        {
            try
            {
                int vl_no_solicitud;
                DataGridViewRow row = gv_entradas.CurrentRow;
                vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());

                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                //System.IO.StreamReader sr = new System.IO.StreamReader(p_file);
                FileInfo fi = new FileInfo(p_file);
                StreamReader sr = new StreamReader(fi.FullName);
                String tempBuff = sr.ReadToEnd();
                sr.Close();

                System.IO.FileStream fs = new System.IO.FileStream(p_file, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                Byte[] data = new byte[fs.Length];
                fs.Read(data, 0, Convert.ToInt32(fs.Length));
                fs.Dispose();

                string vl_sql = "";
                vl_sql = vl_sql + "dcs_p_insertar_adjunto";
                OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd.CommandType = CommandType.StoredProcedure;
                //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────
                OracleParameter pa_parametro1 = new OracleParameter("pa_no_solicitud", OracleType.Int32);
                cmd.Parameters.Add(pa_parametro1);
                pa_parametro1.Direction = ParameterDirection.Input;
                pa_parametro1.Value = vl_no_solicitud;
                //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────
                OracleParameter pa_parametro2 = new OracleParameter("pa_nombre_archivo", OracleType.VarChar, 30);
                cmd.Parameters.Add(pa_parametro2);
                pa_parametro2.Direction = ParameterDirection.Input;
                pa_parametro2.Value = p_only_file;
                //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────
                OracleParameter pa_parametro3 = new OracleParameter("pa_extension", OracleType.VarChar, 10);
                cmd.Parameters.Add(pa_parametro3);
                pa_parametro3.Direction = ParameterDirection.Input;
                pa_parametro3.Value = p_extension;
                //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────                                                  
                OracleParameter pa_parametro4 = new OracleParameter("pa_archivo_bin", OracleType.Blob);
                cmd.Parameters.Add(pa_parametro4);
                pa_parametro4.Direction = ParameterDirection.Input;
                pa_parametro4.Value = data;
                //──────────────────────────────────────────────────────────────────────────────────────────────────────────────────              
                cmd.ExecuteReader();

                Icon icono;
                icono = System.Drawing.Icon.ExtractAssociatedIcon(p_file);
                Image vl_Image = icono.ToBitmap();
                p_insertar_tipo_archivo(p_extension, vl_Image);
                p_llenar_adjuntos(vl_no_solicitud);
                p_llenar_anotaciones(vl_no_solicitud);

                MessageBox.Show("Adjunto ingresado satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_insertar_solicitud :" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void p_insertar_tipo_archivo(string p_extension, Image p_icono)
        {
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                pictureBox4.Image = p_icono;
                Byte[] data = new byte[0];
                data = convertPicBoxImageToByte(pictureBox4);

                string vl_sql = "";
                vl_sql = vl_sql + "DCS_P_INSERTAR_TIPO_ARCHIVO";
                OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd.CommandType = CommandType.StoredProcedure;
                //───────────────────
                OracleParameter pa_parametro1 = new OracleParameter("pa_extension", OracleType.VarChar, 10);
                cmd.Parameters.Add(pa_parametro1);
                pa_parametro1.Direction = ParameterDirection.Input;
                pa_parametro1.Value = p_extension;
                //───────────────────
                OracleParameter pa_parametro4 = new OracleParameter("pa_icono_bin", OracleType.Blob);
                cmd.Parameters.Add(pa_parametro4);
                pa_parametro4.Direction = ParameterDirection.Input;
                pa_parametro4.Value = data;
                //───────────────────
                cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_insertar_tipo_archivo :" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private byte[] convertPicBoxImageToByte(System.Windows.Forms.PictureBox pbImage)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            pbImage.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        //felvir01
        private void p_abrir_adjunto(int p_no_archivo)
        {
            if (!this.dgvExcepciones.Visible)
            {
                string vl_sql = "";
                vl_sql = vl_sql + "Select * from dcs_archivos_adjuntos where no_archivo=:pa_no_archivo";
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd2.CommandType = CommandType.Text;
                //───────────────────
                OracleParameter pa_parametro1 = new OracleParameter("pa_no_archivo", OracleType.Int32);
                cmd2.Parameters.Add(pa_parametro1);
                pa_parametro1.Direction = ParameterDirection.Input;
                pa_parametro1.Value = p_no_archivo;
                //───────────────────

                OracleDataReader dr = cmd2.ExecuteReader();
                dr.Read();
                if (!DBNull.Value.Equals(dr["archivo_bin"]))
                {
                    byte[] bits = ((byte[])dr["archivo_bin"]);
                    string sFile = Application.StartupPath.ToString() + @"\tmp\" + DocSys.vl_user + "tmp" + dr["no_solicitud"] + DateTime.Now.ToString("yyyyMMddhhmmss") + "." + dr["extension"].ToString();
                    System.IO.FileStream fs = new System.IO.FileStream(sFile, System.IO.FileMode.Create);
                    fs.Write(bits, 0, Convert.ToInt32(bits.Length));
                    fs.Close();
                    fs.Dispose();
                    try
                    {
                        System.Diagnostics.Process.Start(sFile);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Process p = new System.Diagnostics.Process();
                        p.StartInfo.FileName = "rundll32.exe";
                        p.StartInfo.Arguments = "shell32.dll,OpenAs_RunDLL " + sFile;
                        p.Start();
                    }
                }
                dr.Close();
            }
            else
            {
                //felvir01: para las excepcion
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }

                string sql = "select archivo_bin, codigo_excepcion, extension from excp.dcs_adjuntos_excep where no_documento = :no_archivo ";

                OracleCommand cmd2 = new OracleCommand(sql, DocSys.connOracle);
                cmd2.CommandType = CommandType.Text;
                //───────────────────
                OracleParameter pa_parametro1 = new OracleParameter("no_archivo", OracleType.Int32);
                cmd2.Parameters.Add(pa_parametro1);
                pa_parametro1.Direction = ParameterDirection.Input;
                pa_parametro1.Value = p_no_archivo;

                OracleDataReader dr = cmd2.ExecuteReader();
                dr.Read();

                if (!DBNull.Value.Equals(dr["archivo_bin"]))
                {
                    byte[] bits = ((byte[])dr["archivo_bin"]);
                    string sFile = Application.StartupPath.ToString() + @"\tmp\" + DocSys.vl_user + "tmp" + dr["codigo_excepcion"] + DateTime.Now.ToString("yyyyMMddhhmmss") + "." + dr["extension"].ToString();
                    System.IO.FileStream fs = new System.IO.FileStream(sFile, System.IO.FileMode.Create);
                    fs.Write(bits, 0, Convert.ToInt32(bits.Length));
                    fs.Close();
                    fs.Dispose();

                    try
                    {
                        System.Diagnostics.Process.Start(sFile);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Process p = new System.Diagnostics.Process();
                        p.StartInfo.FileName = "rundll32.exe";
                        p.StartInfo.Arguments = "shell32.dll,OpenAs_RunDLL " + sFile;
                        p.Start();
                    }
                }
            }
        }

        private void p_abrir_anotacion(int p_no_anotacion)
        {
            if (!this.dgvExcepciones.Visible & gv_entradas.Visible)
            {
                if (gv_entradas.Rows.Count > 0)
                {
                    DataGridViewRow row = gv_entradas.CurrentRow;
                    Int32 vl_no_solicitud = Int32.Parse(row.Cells["no_solicitud"].Value.ToString());
                    s_add_notas forma = new s_add_notas(da, "CONS",
                                                        vl_no_solicitud,
                                                        0,
                                                        p_no_anotacion);
                    forma.ShowDialog();
                }
            }
            else
            {
                if (this.dgvExcepciones.Rows.Count > 0)
                {
                    //notas aqui
                    DataGridViewRow row = this.dgvExcepciones.CurrentRow;
                    int codigo_excepcion = Convert.ToInt32(row.Cells["cod_excepcion_sol"].Value);
                    e_add_notas notas = new e_add_notas(codigo_excepcion, global_estacion_id_to, u_Globales.accionModificar, p_no_anotacion);
                    notas.ShowDialog();
                }
            }

        }

        private void gv_envios_Sorted(object sender, EventArgs e) {}

        private void rbComentarios_iconos_CheckedChanged(object sender, EventArgs e)
        {
            list_anotaciones.View = View.SmallIcon;
        }

        private void rbComentarios_detalle_CheckedChanged(object sender, EventArgs e)
        {
            list_anotaciones.View = View.Tile;
        }

        private void rbComentarios_detalle_CheckedChanged_1(object sender, EventArgs e)
        {
            list_anotaciones.View = View.Details;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            list_adjuntos.View = View.Tile;
        }

        private void button_panel_info_Click(object sender, EventArgs e)
        {
            MenuItem_panelDeInformacion_Click(null, null);
        }

        private void button_panel_nave_Click(object sender, EventArgs e)
        {
            MenuItem_barraDeNavegación_Click(null, null);
        }

        private void linkLabel_ampliar_adjuntos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            s_adjuntos_doc forma = new s_adjuntos_doc();
            forma.ShowDialog();
        }

        private void Button_refrescar_info_Click(object sender, EventArgs e)
        {
            p_refrescar_info();
        }

        private void refrescarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Button_refrescar_info_Click(null, null);
        }

        private void enviarSolicitudToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnMover_solicitud_Click(null, null);
        }

        private void Button_agregar_anotacion_Click(object sender, EventArgs e)
        {
            int vl_no_solicitud = 0;
            if (gv_entradas.RowCount <= 0)
            {
                return;
            }
            DataGridViewRow row = gv_entradas.CurrentRow;
            vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());
            s_add_notas forma = new s_add_notas(da, "INS", vl_no_solicitud, global_estacion_id_to, 0);
            forma.ShowDialog();

            p_llenar_anotaciones(vl_no_solicitud);
        }

        private void agregarAnotacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Button_agregar_anotacion_Click(null, null);
        }

        private void Button_adicionar_documento_Click(object sender, EventArgs e)
        {
            if (gv_entradas.RowCount <= 0)
            {
                return;
            }
            try
            {
                int vl_no_solicitud;
                DataGridViewRow row = gv_entradas.CurrentRow;
                vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());


                s_documentossolic_doc forma = new s_documentossolic_doc(da, vl_no_solicitud);
                forma.ShowDialog();
                p_llenar_adjuntos(vl_no_solicitud);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al tratar de registar documento:" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void agregarDocumentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Button_adicionar_documento_Click(null, null);
        }

        private void gv_Entradas_DoubleClick(object sender, EventArgs e)
        {
            Point pt = Cursor.Position;
            contextMenu_dobleClick.Show(pt);

            return;
        }

        private void Button_protector_pantalla_Click(object sender, EventArgs e)
        {
            SendMessage(this.Handle, 0x112, 0xf140, 0); //Protector de pantalla
        }

        private void Button_bloquea_equipo_Click(object sender, EventArgs e)
        {
            LockWorkStation(); //Se bloquea
        }

        private void Button_salir_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Desea salir de la aplicación  ?", "DocSys", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtTexto_buscar.Text == "")
            {
                return;
            }
            if (DocSys.vl_operacion == "NORM")
            {

            }
            if (DocSys.vl_operacion == "CONS")
            {
                //p_cambiar_modo("NORM");
                p_popular_lista_entradas("and to_char(s.no_solicitud)||lower(desc_sub_aplicacion)||trunc(fecha_presentacion) ||to_char(s.monto_solicitado)||s.codigo_cliente||lower(nombres)||' '||lower(primer_apellido)||' '||lower(segundo_apellido)  like '%" + txtTexto_buscar.Text + @"%'");
                btnCancelar_consulta.Visible = true;
            }
        }

        private void recibirDocumentaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gv_entradas.Rows.Count <= 0)
            {
                return;
            }
            int vl_no_movimiento;
            DataGridViewRow row = gv_entradas.CurrentRow;
            vl_no_movimiento = int.Parse(row.Cells["no_movimiento"].Value.ToString());
            s_recibirdocs forma = new s_recibirdocs(vl_no_movimiento);
            forma.ShowDialog();
        }

        private void timer_resfrescar_info_Tick(object sender, EventArgs e)
        {
            Button_refrescar_info_Click(null, null);
        }

        private void label_usuario_Click(object sender, EventArgs e)
        {
            s_usuario_profile forma = new s_usuario_profile(label_usuario.Text);
            forma.da = this.da;
            forma.ShowDialog();
        }

        private void Button_Buscarsol_Click(object sender, EventArgs e)
        {
            s_buscar_sol forma = new s_buscar_sol();
            forma.da = da;
            forma.ShowDialog();
        }

        private void Label_wfid_Click(object sender, EventArgs e)
        {
            this.Opacity = .85;
            s_cnf_workflow_conf forma = new s_cnf_workflow_conf(da, DocSys.vl_workflow_id);
            forma.ShowDialog();
            this.Opacity = 100;
        } 

        private void linkLabel_mensajes_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {}

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            DataGridViewRow rowgrid = gv_entradas.CurrentRow;
            if (gv_entradas.RowCount > 0)
            {
                if (global_estacion_id_to == 1000)
                {
                    carpetasToolStripMenu.Enabled = true;
                }
                string vl_leido = rowgrid.Cells["leido"].Value.ToString();
                if (vl_leido == "S")
                    MenuItem_marca_leido.Enabled = false;
                else
                    MenuItem_marca_leido.Enabled = true;
            }
            else
            {
                copiarSolicitudACarpetaToolStripMenuItem1.Enabled = false;
            }

            //condiciona si puede o no agregar una excepcion
            string ver_Excepciones = DocSys.puede_ver_excepciones(global_estacion_id_to);
            int vl_codigo_agencia;
            string vl_nombre_agencia;
            DocSys.p_obtener_filial_usuario(DocSys.vl_user, out vl_codigo_agencia, out vl_nombre_agencia);
            int codigoZona = DocSys.excepciones_region(vl_codigo_agencia);

            if (global_estacion_id_to == u_Globales.eAfiliacion)
            {
                this.contextMenu_gridview.Items["nuevaExcepcionToolStripMenuItem"].Visible = true;
                nuevaExcepcionToolStripMenuItem.Visible = true;
                agregarExcepciónManualToolStripMenuItem.Visible = true;
            }
            
            if (ver_Excepciones.Equals(string.Empty) /*&& codigoZona == 4*/)
            {
                this.contextMenu_gridview.Items["verExcepcionesToolStripMenuItem"].Visible = true;
                verExcepcionesToolStripMenuItem.Visible = true;
                agregarExcepciónManualToolStripMenuItem.Visible = true;
            }
            else
            {
                this.contextMenu_gridview.Items["verExcepcionesToolStripMenuItem"].Visible = false;
                verExcepcionesToolStripMenuItem.Visible = false;
                agregarExcepciónManualToolStripMenuItem.Visible = false;
            }
        }

        private void crearUnaCarpetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            s_carpetas_doc forma = new s_carpetas_doc();
            DialogResult res = forma.ShowDialog();
            if (res == DialogResult.OK)
            {
                p_popular_treview();
            }
        }

        private void copiarAUnaCarpetaDeUsuarioToolStripMenuItem_Click(object sender, EventArgs e){}

        private void clasificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int vl_no_solicitud;
            DataGridViewRow row = gv_entradas.CurrentRow;
            vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());

            s_clasificar_sol forma = new s_clasificar_sol(imageSeguimiento, vl_no_solicitud);
            DialogResult res = forma.ShowDialog();
            if (res == DialogResult.OK)
            {
                p_refrescar_info();
            }
        }

        private void copiarSolicitudACarpetaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            p_copiar_solicitud_a_carpeta();
        }

        private void Button_analista_Click(object sender, EventArgs e)
        {
            if (global_estacion_id_to == 1000)
                return;

            if (treeView.SelectedNode.Name.ToString().ToUpper() != "NODE_ENTRADAS")
                return;

            int vl_no_solicitud;
            DataGridViewRow row = gv_entradas.CurrentRow;
            if (row != null)
            {
                vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());

                s_asignar_analista forma = new s_asignar_analista(vl_no_solicitud, global_estacion_id_to);
                DialogResult res = forma.ShowDialog();
                if (res == DialogResult.OK)
                {
                    p_popular_treview();
                }
            }
        }

        private void MenuItem_verSoloMisSolicitudes_Click(object sender, EventArgs e)
        {
            if (MenuItem_verSoloMisSolicitudes.Checked)
                checkBox_mis_solicitudes.Checked = true;
            else
                checkBox_mis_solicitudes.Checked = false;
            p_popular_lista_entradas("");
            p_refrescar_info_secundaria();
        }

        private void checkBox_mis_solicitudes_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_mis_solicitudes.Checked)
                MenuItem_verSoloMisSolicitudes.Checked = true;
            else
                MenuItem_verSoloMisSolicitudes.Checked = false;
            MenuItem_verSoloMisSolicitudes_Click(null, null);
        }

        private void seguimientoDeLaSolicitudToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int vl_no_solicitud = 0;
            int vl_no_movimiento = 0;
            if (gv_entradas.RowCount > 0)
            {
                DataGridViewRow row = gv_entradas.CurrentRow;
                vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());
                vl_no_movimiento = int.Parse(row.Cells["no_movimiento"].Value.ToString());

                s_solicitud_detalle forma = new s_solicitud_detalle(vl_no_solicitud, vl_no_movimiento);
                forma.da = da;
                forma.ShowDialog();
            }
        }

        private void toolStrip2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            return;

            if (toolStrip_princ.Dock == DockStyle.None)
            {
                toolStrip_princ.Dock = System.Windows.Forms.DockStyle.Fill;
                toolStrip_princ.Parent = panel_menutoolbar;
                toolStrip_princ.BringToFront();
            }
            else
            {
                toolStrip_princ.Dock = System.Windows.Forms.DockStyle.None;
                toolStrip_princ.Parent = panelprincipal;
                toolStrip_princ.BringToFront();
                toolStrip_princ.Size = new System.Drawing.Size(500, 40);
            }
        }

        private void Button_abandonar_Click(object sender, EventArgs e)
        {
            int vl_no_solicitud;
            DataGridViewRow row = gv_entradas.CurrentRow;
            if (row != null)
            {
                if (global_estacion_id_to != 1000)
                {
                    MessageBox.Show("Solo el area de servicio al cliente puede abandonar una solicitud ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());
                s_marcar_abandono_doc forma = new s_marcar_abandono_doc(da, vl_no_solicitud, imageSeguimiento);
                forma.labelFilial.Text = lblSuFilial.Text;
                DialogResult resul = forma.ShowDialog();
                if (resul == DialogResult.OK)
                {
                    Button_refrescar_info_Click(null, null);
                }
            }
        }

        private void marcarComoAbandonadaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Button_abandonar_Click(null, null);
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea salir de la aplicación del CreditScoring ", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            this.Close();
        }

        private void MenuItem_avisos_Click(object sender, EventArgs e)
        {
            panel_mensajes_Click(null, null);
        }

        private void asignarLaSolicitudToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Button_analista_Click(null, null);
        }

        private void label_usuario_MouseMove(object sender, MouseEventArgs e)
        {
            miniinfo.get_set_codigo_usuario = label_usuario.Text;

            Point pos = this.PointToScreen(e.Location);
            miniinfo.da = this.da;
            miniinfo.Show();
            miniinfo.Location = new Point(Control.MousePosition.X - 160, Control.MousePosition.Y + 10);
            miniinfo.Refresh();
        }

        private void label_usuario_MouseLeave(object sender, EventArgs e)
        {
            vl_mostrar_miniinfo = true;
            miniinfo.Hide();
            this.Cursor = Cursors.Default;
        }

        private void label_oficial_MouseLeave(object sender, EventArgs e)
        {
            vl_mostrar_miniinfo = true;
            miniinfo.Hide();
            this.Cursor = Cursors.Default;
        }

        private void label_oficial_MouseMove(object sender, MouseEventArgs e)
        {
            miniinfo.get_set_codigo_usuario = label_oficial.Text;

            Point pos = this.PointToScreen(e.Location);
            miniinfo.Show();
            miniinfo.Location = new Point(Control.MousePosition.X - 5, Control.MousePosition.Y + 10);
            miniinfo.Refresh();
        }

        private void gv_entradas_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            miniinfo.Hide();
            if (MenuItemmostrarInforRapida.Checked)
            {
                DataGridViewRow row = gv_entradas.CurrentRow;
                if (row != null)
                {
                    vl_mostrar_miniinfo = true;
                    miniinfo_sol.Hide();
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p_abrir_adjunto(global_no_archivo_contexmenu);
        }

        private void list_adjuntos_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.list_adjuntos.GetItemAt(e.X, e.Y) != null)
            {
                this.list_adjuntos.GetItemAt(e.X, e.Y).Selected = true;
                ListViewItem item = list_adjuntos.GetItemAt(e.X, e.Y);
                if (item != null)
                {
                    int.TryParse(item.SubItems[2].Text, out global_no_archivo_contexmenu);
                }

                Point pt = list_adjuntos.PointToScreen(e.Location);
                if (e.Button == MouseButtons.Right)
                    contextMenu_adjuntos.Show(pt);
                if (e.Clicks == 2)
                    if (global_no_archivo_contexmenu > 0)
                        p_abrir_adjunto(global_no_archivo_contexmenu);
            }
        }

        private void nuevoDocumentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //felvir01
            if (!this.verExcepciones)
                Button_adicionar_documento_Click(null, null);
            else
                agregar_documento_Click(null, null);
        }

        private void solicitudesHistoricasDelAfiliadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int vl_codigo_cliente = 0;
            s_solicitud_conshist forma = new s_solicitud_conshist(vl_codigo_cliente, "");
            forma.ShowDialog();
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int vl_no_solicitud = 0;
            if (gv_entradas.RowCount > 0)
            {
                DataGridViewRow rowgrid = gv_entradas.CurrentRow;
                vl_no_solicitud = int.Parse(rowgrid.Cells["no_solicitud"].Value.ToString());
            }
            else
                return;

            if (p_get_estado_solicitud(vl_no_solicitud) > 1)
            {
                MessageBox.Show("Un documento no puede ser eliminado cuando la solicitud ya esta en proceso", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (DialogResult.Yes == MessageBox.Show("Eliminar el archivo seleccionado ", DocSys.vl_mensaje_avisos, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                p_eliminar_adjunto(global_no_archivo_contexmenu);
            }
        }

        private void list_anotaciones_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.list_anotaciones.GetItemAt(e.X, e.Y) != null)
            {
                this.list_anotaciones.GetItemAt(e.X, e.Y).Selected = true;
                ListViewItem item = list_anotaciones.GetItemAt(e.X, e.Y);
                if (item != null)
                {
                    int.TryParse(item.SubItems[2].Text, out global_no_anotacion_contexmenu);
                }

                Point pt = list_anotaciones.PointToScreen(e.Location);
                if (e.Button == MouseButtons.Right)
                    contextMenu_notas.Show(pt);

                if (e.Clicks == 2)
                    if (global_no_anotacion_contexmenu > 0)
                        p_abrir_anotacion(global_no_anotacion_contexmenu);
            }
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e) {}

        private void toolStripMenuItem9_Click(object sender, EventArgs e){}

        private void marcarConErrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int vl_no_solicitud;
            DataGridViewRow row = gv_entradas.CurrentRow;
            if (row != null)
            {
                if (global_estacion_id_to != 1000)
                {
                    MessageBox.Show("Solo el area de afiliación puede marcar una solicitud con error ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());
                s_solicitud_marcaerror_doc forma = new s_solicitud_marcaerror_doc(vl_no_solicitud);
                DialogResult resul = forma.ShowDialog();
                if (resul == DialogResult.OK)
                {
                    Button_refrescar_info_Click(null, null);
                }
            }
            else
                return;
        }

        private void eliminarSolicitudToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("La opción de eliminar una solicitud ha sido deshabilitada...", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
            int vl_no_solicitud;
            DataGridViewRow row = gv_entradas.CurrentRow;
            if (row != null)
            {
                vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());
                if (da.ObtenerApplicationIDxSolicitud(vl_no_solicitud) != "0")
                {
                    MessageBox.Show("La solicitud no puede ser eliminada porque ya se realizo la precalificación de los solicitantes !", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (p_get_estado_solicitud(vl_no_solicitud) > 1)
                {
                    MessageBox.Show("La solicitud no puede ser elimina porque ya esta en proceso !", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                DataTable dtSol = da.ObtenerInfoSolicitud(vl_no_solicitud);
                if (dtSol.Rows[0]["usuario_ing"].ToString() != DocSys.vl_user)
                {
                    MessageBox.Show("La solicitud solo puede ser elimina por el usuario que la ingresó !", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (DialogResult.Yes == MessageBox.Show("Desea eliminar la solicitud No. " + vl_no_solicitud.ToString() + "?", DocSys.vl_mensaje_avisos, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    p_eliminar_solicitud(vl_no_solicitud);
                    p_refrescar_info();
                }
            }
        }

        private void Button_eliminar_solic_Click(object sender, EventArgs e)
        {
            eliminarSolicitudToolStripMenuItem_Click(null, null);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            s_notas_aceptar forma = new s_notas_aceptar();
            forma.ShowDialog();
        }
        #endregion

        private void verSolicitudesAbiertasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            s_solic_abiertas forma = new s_solic_abiertas();
            forma.da = da;
            forma.ShowDialog();
        }

        private void button_solicopen_Click(object sender, EventArgs e)
        {
            s_solic_abiertas forma = new s_solic_abiertas();
            forma.da = da;
            forma.ShowDialog();
        }

        private void toolStrip_princ_ItemClicked(object sender, ToolStripItemClickedEventArgs e) { } 

        private void btnClose_Click(object sender, EventArgs e)
        {
            salirToolStripMenuItem_Click(null, null);
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

        private void panel_menutoolbar_MouseMove(object sender, MouseEventArgs e)
        {
            moverForm();
        }

        private void pnlMenu_MouseMove(object sender, MouseEventArgs e) {   }

        private void pnlMenu_Click(object sender, EventArgs e)
        {
            colapsar_menu();
        }

        private void pbActivarMenuDesplegable_MouseMove(object sender, MouseEventArgs e)
        {
            mostrar_menu();
        }

        private void pbPerfil_menu_DoubleClick(object sender, EventArgs e)
        {
            label_usuario_Click(null, null);
        }

        private void btnSalir2_Click(object sender, EventArgs e)
        {
            colapsar_menu();
        }

        private void btnConfigurarWf_Click(object sender, EventArgs e)
        {
            if (global_es_admon_sistema || (global_es_admon_temp && global_tipo_admon_temp_sol))
            {
                this.Opacity = .85;
                s_cnf_workflow_conf forma = new s_cnf_workflow_conf(da, DocSys.vl_workflow_id);
                forma.ShowDialog();
                this.Opacity = 100;
            }
            else
            {
                this.Opacity = .90;
                s_usuario_profile forma = new s_usuario_profile(label_usuario.Text);
                forma.ShowDialog();
                this.Opacity = 100;
            }
            colapsar_menu();
        }

        private void btnNuevaSolic_Click(object sender, EventArgs e)
        {
            p_mostrar_espera("Cargado formulario nuevo", true);
            colapsar_menu();

            if (global_estacion_id_to == 0)
            {
                panelCargandoSolic.Visible = false;
                MessageBox.Show("El usuario no tiene definida una estación de trabajo..!", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            if (DocSys.p_get_permitir_crear_solicitud(global_estacion_id_to) == "N")
            {
                panelCargandoSolic.Visible = false;
                MessageBox.Show("La estacion " + DocSys.p_get_nombre_estacion(global_estacion_id_to) + " no puede crear solicitudes ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            s_PreCalificado forma = new s_PreCalificado(da);
            forma.formapadre = this;
            DialogResult res = forma.ShowDialog();
            panelCargandoSolic.Visible = false;
            if (res == DialogResult.OK)
            {
                p_refrescar_info();
            }
        }

        private void btnNuevaSolic_ClickJ(object sender, EventArgs e)
        {
            p_mostrar_espera("Cargado formulario nuevo", true);
            colapsar_menu();

            if (global_estacion_id_to == 0)
            {
                panelCargandoSolic.Visible = false;
                MessageBox.Show("El usuario no tiene definida una estación de trabajo..!", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (DocSys.p_get_permitir_crear_solicitud(global_estacion_id_to) == "N")
            {
                panelCargandoSolic.Visible = false;
                MessageBox.Show("La estacion " + DocSys.p_get_nombre_estacion(global_estacion_id_to) + " no puede crear solicitudes ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            
                s_PreCalificado_J formaJ = new s_PreCalificado_J(da);
                formaJ.formapadre = this;
                formaJ.ShowDialog();
                formaJ.Focus();
                formaJ.BringToFront();
                panelCargandoSolic.Visible = false;
        }

        private void p_popular_treview()
        {
            int vl_cant_carpetas = da.ObtenerCantCarpetasxUsuarios(s_login.global_usuario);

            int vl_solic_entrada = 0;
            int vl_solic_denegadas = 0;

            treeView.Nodes.Clear();

            TreeNode node;
            TreeNode nodehijo;
            node = treeView.Nodes.Add("Solicitudes");
            node.ImageKey = "icon_collapse_tree.png";
            node.SelectedImageKey = "icon_treeclosed.png";

            node.Name = "Node0";

            nodehijo = node.Nodes.Add("Bandeja de Entradas");
            nodehijo.ImageKey = "icon_collapse_tree.png";
            nodehijo.SelectedImageKey = "icon_treeclosed.png";
            nodehijo.Name = "Node_entradas";

            nodehijo = node.Nodes.Add("En proceso");
            nodehijo.ImageKey = "icon_collapse_tree.png";
            nodehijo.SelectedImageKey = "icon_treeclosed.png";
            nodehijo.Name = "Node_salidas";

            nodehijo = node.Nodes.Add("Solicitudes Denegadas");
            nodehijo.ImageKey = "icon_collapse_tree.png";
            nodehijo.SelectedImageKey = "icon_treeclosed.png";
            nodehijo.Name = "Node_denegadas";

            #region Node Denegadas
            TreeNode[] tn2 = treeView.Nodes[0].Nodes.Find("Node_denegadas", true);
            for (int i = 0; i < tn2.Length; i++)
            {
                treeView.SelectedNode = tn2[i];
                treeView.SelectedNode.NodeFont = new Font(treeView.Font, FontStyle.Bold);
                if (vl_solic_denegadas > 0)
                {
                    treeView.SelectedNode.NodeFont = new Font(treeView.Font, FontStyle.Bold);
                    treeView.SelectedNode.Text = "Solicitudes Denegadas " + "(" + vl_solic_denegadas.ToString() + ")";
                    if (!global_primer_load)
                    {
                        System.Media.SoundPlayer player = new System.Media.SoundPlayer();
                        player.SoundLocation = @"online.wav";
                    }
                }
                else
                {
                    treeView.SelectedNode.NodeFont = new Font(treeView.Font, FontStyle.Regular);
                    treeView.SelectedNode.Text = "Solicitudes Denegadas";
                }
            }
            #endregion

            #region Node Entradas
            TreeNode[] tn1 = treeView.Nodes[0].Nodes.Find("Node_entradas", true);
            for (int i = 0; i < tn1.Length; i++)
            {
                treeView.SelectedNode = tn1[i];
                treeView.SelectedNode.NodeFont = new Font(treeView.Font, FontStyle.Bold);
                if (vl_solic_entrada > 0)
                {
                    treeView.SelectedNode.NodeFont = new Font(treeView.Font, FontStyle.Bold);
                    treeView.SelectedNode.Text = "Bandeja de entradas " + "(" + vl_solic_entrada.ToString() + ")";
                    if (!global_primer_load)
                    {
                        System.Media.SoundPlayer player = new System.Media.SoundPlayer();
                        player.SoundLocation = @"online.wav";
                    }
                }
                else
                {
                    treeView.SelectedNode.NodeFont = new Font(treeView.Font, FontStyle.Regular);
                    treeView.SelectedNode.Text = "Bandeja de entradas";
                }
            }
            #endregion

            if (vl_cant_carpetas > 0)
            {
                node = treeView.Nodes.Add("Carpetas del Usuario");
                node.ImageIndex = 10;
                node.ImageKey = "icon_folderuser_tree.png";
                node.SelectedImageKey = "icon_folderuser_tree.png";
                nodehijo.Name = "Node_carpetas_usuarios";
                DataTable dtCarpetas = da.ObtenerCarpetasDelUsuario(s_login.global_usuario);
                for (int i = 0; i < dtCarpetas.Rows.Count; i++)
                {
                    var lcarpeta_id = dtCarpetas.Rows[i]["carpeta_id"].ToString();
                    var lcanti = dtCarpetas.Rows[i]["canti"].ToString();
                    var ldescripcion = dtCarpetas.Rows[i]["descripcion"].ToString();
                    if (lcanti == "0")
                        nodehijo = node.Nodes.Add(ldescripcion);
                    else
                    {
                        //nodehijo = node.Nodes.Add(ldescripcion + " [" + lcanti + "]");
                        nodehijo = node.Nodes.Add(ldescripcion);
                    }
                    nodehijo.ImageKey = "icon_folderclose_tree.png";
                    nodehijo.SelectedImageKey = "icon_folderopen_tree.png";
                    nodehijo.Name = lcarpeta_id;
                }
                node.Collapse();
            }

            /*Excepciones felvir01*/
            //felvir01 - Excepciones
            string ver_Excepciones = DocSys.puede_ver_excepciones(global_estacion_id_to);
            int vl_codigo_agencia;
            string vl_nombre_agencia;
            DocSys.p_obtener_filial_usuario(DocSys.vl_user, out vl_codigo_agencia, out vl_nombre_agencia);
            int codigoZona = DocSys.excepciones_region(vl_codigo_agencia);

            //se agrego una funcion para validar si es un admi temporal de excepciones, en este apartado tambien se maneja la info del combo de las zonas y excepciones 
            bool AdminTemp = Convert.ToBoolean(da.EsAdminTemporal(DocSys.vl_user));

            if (ver_Excepciones.Equals(string.Empty))
            {
                node = treeView.Nodes.Add("N_Excepciones", "Excepciones");
                node.ImageIndex = 10;
                nodehijo.ImageKey = "icon_collapse_tree.png";
                nodehijo.SelectedImageKey = "icon_treeclosed.png";
                //Obtiene las carpetas del sistema

                if (global_estacion_id_to != (int)Estaciones.EstacionValidacion)
                {
                    if (global_estacion_id_to == (int)Estaciones.ComiteII |
                    global_estacion_id_to == (int)Estaciones.ComiteIII |
                    global_estacion_id_to == (int)Estaciones.ComiteILCBA |
                    global_estacion_id_to == (int)Estaciones.ComiteISPS |
                    global_estacion_id_to == (int)Estaciones.ComiteITGU)
                    {
                        var carpetaId = u_Globales.RESOLUTIVO;
                        var descripcion = "PENDIENTE RESOLUCIÓN";
                        nodehijo = node.Nodes.Add(descripcion);
                        nodehijo.ImageKey = "icon_collapse_tree.png";
                        nodehijo.SelectedImageKey = "icon_treeclosed.png";
                        nodehijo.Name = carpetaId;
                    }
                    else
                    {
                        DataTable carpetasExcep = this.da.CarpetasExcepciones();
                        if (carpetasExcep.Rows.Count > 0)
                        {
                            for (int i = 0; i < carpetasExcep.Rows.Count; i++)
                            {
                                var carpetaId = carpetasExcep.Rows[i]["CARPETA_ID"].ToString();
                                var descripcion = carpetasExcep.Rows[i]["DESCRIPCION"].ToString();
                                nodehijo = node.Nodes.Add(descripcion);
                                nodehijo.ImageKey = "icon_collapse_tree.png";
                                nodehijo.SelectedImageKey = "icon_treeclosed.png";
                                nodehijo.Name = carpetaId;
                            }
                        }
                        else
                        {
                            nodehijo = node.Nodes.Add("Bandeja de Entradas");
                            nodehijo.ImageKey = "icon_collapse_tree.png";
                            nodehijo.SelectedImageKey = "icon_treeclosed.png";
                            nodehijo.Name = "Node_excepciones";
                        }
                    }
                }
                else
                {
                    //var carpetaId = u_Globales.MONTO1;
                    //var descripcion = "Bandeja de Entrada";
                    //nodehijo = node.Nodes.Add(descripcion);
                    //nodehijo.ImageKey = "icon_collapse_tree.png";
                    //nodehijo.SelectedImageKey = "icon_treeclosed.png";
                    //nodehijo.Name = carpetaId;

                    var carpetaId = u_Globales.MONTO1;
                    var descripcion = "COMITE I";
                    nodehijo = node.Nodes.Add(descripcion);
                    nodehijo.ImageKey = "icon_collapse_tree.png";
                    nodehijo.SelectedImageKey = "icon_treeclosed.png";
                    nodehijo.Name = carpetaId;

                    carpetaId = u_Globales.MONTO2;
                    descripcion = "COMITE II";
                    nodehijo = node.Nodes.Add(descripcion);
                    nodehijo.ImageKey = "icon_collapse_tree.png";
                    nodehijo.SelectedImageKey = "icon_treeclosed.png";
                    nodehijo.Name = carpetaId;

                    carpetaId = u_Globales.MONTO3;
                    descripcion = "COMITE III";
                    nodehijo = node.Nodes.Add(descripcion);
                    nodehijo.ImageKey = "icon_collapse_tree.png";
                    nodehijo.SelectedImageKey = "icon_treeclosed.png";
                    nodehijo.Name = carpetaId;
                }
            }
        }

        private void treeView_Click(object sender, EventArgs e) { }

        private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Text != "Carpetas del Usuario")
            {
                e.Node.ImageIndex = 6;
            }
        }

        private void treeView_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Text != "Carpetas del Usuario")
            {
                e.Node.ImageIndex = 7;
            }
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Text == "Carpetas del Usuario")
            {
                e.Node.ImageIndex = 10;
            }
        }

        private void treeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Parent != null)
            {
                string parent = e.Node.Parent.Text;
                if (parent == "Solicitudes")
                {
                    if (e.Node.IsExpanded)
                    {
                        e.Node.ImageIndex = 6;
                    }
                    else
                    {
                        e.Node.ImageIndex = 7;
                    }
                }
            }
            else
            {
                string tempo1 = e.Node.Text;
                if (tempo1 == "Solicitudes")
                {
                    if (e.Node.IsExpanded)
                    {
                        e.Node.ImageIndex = 6;
                    }
                    else
                    {
                        e.Node.ImageIndex = 7;
                    }
                }
            }
        }

        private void label10_Click(object sender, EventArgs e) { }

        private void button4_Click(object sender, EventArgs e)
        {
            Button_refrescar_info_Click(null, null);
        }

        private void acercaDeDocSisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            s_acerca_de forma = new s_acerca_de();
            forma.ShowDialog();
        }

        private void MDI_Menu_Resize(object sender, EventArgs e) { }

        private void button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void comboBoxEstacionUsuario_SelectionChangeCommitted(object sender, EventArgs e)
        {
            global_estacion_id_to = int.Parse(comboBoxEstacionUsuario.SelectedValue.ToString());
            global_ver_toda_filial = DocSys.p_obtener_si_todas_las_filiales(global_estacion_id_to);
            if (this.global_estacion_id_to != (int)Estaciones.EstacionValidacion)
                p_refrescar_info();

            DocSys.EstacionGlobal = global_estacion_id_to;

            //Si es creditos TGU,SPS,LCBA activa la funcion
            if (global_estacion_id_to == 1002 || global_estacion_id_to == 2001 || global_estacion_id_to == 3001 ||
                global_estacion_id_to == 1003 || global_estacion_id_to == 2002 || global_estacion_id_to == 3002)
            {
                asignarLaSolicitudToolStripMenuItem.Enabled = true;
                observacionesDelAnalistasToolStripMenuItem.Enabled = true;
            }
            else
            {
                asignarLaSolicitudToolStripMenuItem.Enabled = false;
                observacionesDelAnalistasToolStripMenuItem.Enabled = false;
            }

            //Si es afiliacion desactiva esta funcion
            if (global_estacion_id_to == 1000)
            {
                MenuItem_verSoloMisSolicitudes.Visible = false;
                checkBox_mis_solicitudes.Visible = false;
            }
            else
            {
                MenuItem_verSoloMisSolicitudes.Visible = true;
                checkBox_mis_solicitudes.Visible = true;
            }

            var dt = da.ObtenerEstacionesxId(global_estacion_id_to);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["comite_resolutivo"].ToString() == "S")
                {
                    btnResolucion.Visible = true;
                }
                else
                {
                    btnResolucion.Visible = false;
                }
            }
            else
            {
                btnResolucion.Visible = false;
            }

            //felvir01 - Excepciones
            this.p_popular_treview();
            treeView.ExpandAll();
            string ver_Excepciones = DocSys.puede_ver_excepciones(global_estacion_id_to);
            if (!ver_Excepciones.Equals(string.Empty) /*& this.dgvExcepciones.Visible*/ || (global_es_admon_temp == true && global_tipo_admon_temp_excp))
            {
                this.dgvExcepciones.Visible = false;
                this.lblTituloExcepciones.Visible = false;
            }
            else
            {
                if (this.gv_entradas.Visible)
                {
                    this.dgvExcepciones.Visible = false;
                    this.lblTituloExcepciones.Visible = false;
                }
                else
                {
                    this.dgvExcepciones.Visible = true;
                    this.lblTituloExcepciones.Visible = true;
                }
            }
        }

        private void panel_menutoolbar_DoubleClick(object sender, EventArgs e)
        {
            btnMaximizar_Click(null, null);
        }

        private void panel_menutoolbar_Paint(object sender, PaintEventArgs e) {}

        private void btnResolucion_Click(object sender, EventArgs e)
        {
            int vl_no_solicitud = 0;
            if (gv_entradas.RowCount > 0)
            {
                DataGridViewRow row = gv_entradas.CurrentRow;
                vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());
                string vl_nombre = row.Cells["nombre_cliente"].Value.ToString();
                string vl_producto = row.Cells["producto"].Value.ToString();
                string vl_moneda = row.Cells["desc_moneda"].Value.ToString();
                string vl_monto = String.Format("{0:###,###,###,##0.00}", float.Parse(row.Cells["monto_solicitado"].Value.ToString()));
                string vl_plazo_s = row.Cells["meses_plazo"].Value.ToString();

                if (!da.EsAdministradorSistema(DocSys.vl_user))
                {
                    if (!da.ExisteUsuarioEnResoluciones(DocSys.vl_user, vl_no_solicitud))
                    {
                        MessageBox.Show("Actualmente usted pertenece al nivel resolutivo \"" + comboBoxEstacionUsuario.Text.Trim() + "\", sin embargo, cuando esta solicitud fue enviada a este comite su usuario " + DocSys.vl_user + " no era miembro activo del comite o el oficial de servicio no seleccionó su nombre en la lista de Gerente de Filial al enviar la solicitid a este comite, por lo que no existe el registro para dar su aprobación o rechazo. Solicitar al supervisor de CreditScoring agregar su usuario a esta solicitud (" + vl_no_solicitud.ToString() + ") para dar su aprobacion o rechazo de la solicitud", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                s_resolucionsol_doc forma = new s_resolucionsol_doc(da);
                forma.labelComite.Text += " " + comboBoxEstacionUsuario.Text + " | " + DocSys.vl_user;
                forma.txtNo_solicitud.Text = vl_no_solicitud.ToString();
                forma.txtNombre_solicitante.Text = vl_nombre;
                forma.txtDesc_sub_aplicacion.Text = vl_producto;
                forma.txtDesc_moneda.Text = vl_moneda;
                forma.txtMonto_solicitado.Text = vl_monto;
                forma.txtPlazo_s.Text = vl_plazo_s;
                forma.ShowDialog();
            }
        }

        private void btnMover_solicitud_Click(object sender, EventArgs e)
        {
            if (gv_entradas.RowCount > 0)
            {
                colapsar_menu();

                if (global_estacion_id_to == 0)
                {
                    MessageBox.Show("El usuario no tiene definida una estación de trabajo..!", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (treeView.SelectedNode.Name.ToString().ToUpper() != "NODE_ENTRADAS")
                    return;
                if (gv_entradas.RowCount == 0)
                {
                    return;
                }

                if (!DocSys.p_valida_politicas_envio_credito())
                {
                    return;
                }

                DataGridViewRow row = gv_entradas.CurrentRow;
                Int32 vl_no_solicitud = Int32.Parse(row.Cells["no_solicitud"].Value.ToString());

                if (global_estacion_id_to != DocSys.p_get_estacion_current(vl_no_solicitud))
                {
                    MessageBox.Show("La solicitud que esta tratando de enviar ya fue procesada por otro usuario !", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    p_refrescar_info();
                    return;
                }

                int vl_paso_sig = (p_get_paso_actual(vl_no_solicitud));

                if (p_get_cant_respuestas_paso(vl_paso_sig) <= 0)
                {
                    MessageBox.Show("No hay mas pasos definidos en el Flujo de procesos (Workflow) ", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (gv_entradas.Rows.Count <= 0)
                {
                    return;
                }

                if (da.ObtenerEsAnalizableProducto(vl_no_solicitud) == "S")
                {
                    if (da.ObtenerApplicationIDxSolicitud(vl_no_solicitud) == "0")
                    {
                        MessageBox.Show("La solicitud no puede ser enviada a otra area mientras no se precalifique a los solicitantes !", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                int excepciones_Activas = DocSys.get_excepciones_Activas(vl_no_solicitud);

                if (excepciones_Activas > 0)
                {
                    MessageBox.Show("La solicitud no puede ser enviada porque contiene una excepción en proceso !", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //FELVIR01 - 20190718. Evalua si la solicitud requiere una excepción, en caso de necesitarla y no agregarla no deja pasar.
                bool creada = false;
                bool requiereExcepcion = this.da.GuardarAlertaExcepcion("S", vl_no_solicitud, out creada, false);
                if (requiereExcepcion & !creada)
                {
                    MessageBox.Show("Necesita crear la excepción para la solicitud antes de continuar o pedir al administrador que autorice el envío sin excepción!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Decimal vl_monto_solicitado = Decimal.Parse(row.Cells["monto_solicitado"].Value.ToString());
                s_mover_solicitud forma = new s_mover_solicitud(da);
                forma.labelMonto_solicitado.Text = vl_monto_solicitado.ToString();
                forma.gno_solicitud = vl_no_solicitud;
                forma.gcodigo_agencia = DocSys.vl_agencia_usuario;
                DialogResult nresult = forma.ShowDialog();
                if (nresult == DialogResult.OK)
                {
                    p_refrescar_info();
                }
            }
        }

        private void btnEliminar_sol_Click(object sender, EventArgs e)
        {
            eliminarSolicitudToolStripMenuItem_Click(null, null);
        }

        private void TimeBar_Tick(object sender, EventArgs e)
        {
            return;
            labelRelojPanel.Text = DateTime.Now.ToString("hh:mm");
            labelDiaPanel.Text = DateTime.Now.ToString("dddd");
            labelFechaPanel.Text = DateTime.Now.ToString("MMMM") + " " + DateTime.Now.ToString("dd");
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            salirToolStripMenuItem_Click(null, null);
        }

        private void btnRefrescar_Click_1(object sender, EventArgs e)
        {
            Button_refrescar_info_Click(null, null);
        }

        private void btnAbandonar_solicitud_Click(object sender, EventArgs e)
        {
            int vl_no_solicitud;
            DataGridViewRow row = gv_entradas.CurrentRow;
            if (row != null)
            {
                if (global_estacion_id_to != 1000)
                {
                    MessageBox.Show("Solo el area de servicio al cliente puede abandonar una solicitud ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());
                s_marcar_abandono_doc forma = new s_marcar_abandono_doc(da, vl_no_solicitud, imageSeguimiento);
                DialogResult resul = forma.ShowDialog();
                if (resul == DialogResult.OK)
                {
                    Button_refrescar_info_Click(null, null);

                }
            }
        }

        private void brtMarca_con_error_Click(object sender, EventArgs e)
        {
            marcarConErrorToolStripMenuItem_Click(null, null);
        }

        private void btnAgregar_anotacion_Click(object sender, EventArgs e)
        {
            Button_agregar_anotacion_Click(null, null);
        }

        private void btnAgragar_adjunto_Click(object sender, EventArgs e)
        {
            if (gv_entradas.RowCount <= 0)
            {
                return;
            }
            try
            {
                int vl_no_solicitud;
                DataGridViewRow row = gv_entradas.CurrentRow;
                vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());

                s_documentossolic_doc forma = new s_documentossolic_doc(da, vl_no_solicitud);
                forma.ShowDialog();
                p_llenar_adjuntos(vl_no_solicitud);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al tratar de registar documento:" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnSolicitudes_abiertas_Click(object sender, EventArgs e)
        {
            s_solic_abiertas forma = new s_solic_abiertas();
            forma.da = da;
            forma.ShowDialog();
        }

        private void btnBuscar_sol_Click(object sender, EventArgs e)
        {
            s_buscar_sol forma = new s_buscar_sol();
            forma.da = da;
            forma.ShowDialog();
        }

        private void btnSolicitudes_hist_Click(object sender, EventArgs e)
        {
            int vl_codigo_cliente = 0;
            s_solicitud_conshist forma = new s_solicitud_conshist(vl_codigo_cliente, "");
            forma.da = da;
            forma.ShowDialog();
        }

        private void cbGteFilial_CheckedChanged(object sender, EventArgs e)
        {
            if (cbGteFilial.Checked)
            {
                global_gerente_filial = true;
            }
            else
                global_gerente_filial = false;
            p_refrescar_info();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            s_acerca_de forma = new s_acerca_de();
            forma.ShowDialog();
        }

        private void agregarInstruccionesDeDesembolsoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gv_entradas.RowCount <= 0)
            {
                return;
            }
            try
            {
                int vl_no_solicitud;
                DataGridViewRow row = gv_entradas.CurrentRow;
                vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());

                s_add_instruc_desembolso forma = new s_add_instruc_desembolso(da, vl_no_solicitud);
                forma.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al tratar de agregar instrucciones de desembolso" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void contextMenu_dobleClick_Opening(object sender, CancelEventArgs e) { }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            p_mostrar_espera("Cargado solicitud..", true);
            int vl_no_solicitud = 0;
            int vl_no_movimiento = 0;
            if (gv_entradas.RowCount > 0)
            {
                DataGridViewRow row = gv_entradas.CurrentRow;
                vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());
                vl_no_movimiento = int.Parse(row.Cells["no_movimiento"].Value.ToString());

                DataTable dtSolic = da.ObtenerInfoSolicitud(vl_no_solicitud);
                int vl_estado_solicitud = int.Parse(dtSolic.Rows[0]["estado_solicitud_id"].ToString());
                s_PreCalificado forma = new s_PreCalificado(da);
                if (vl_estado_solicitud == 1 || vl_estado_solicitud == 2)
                {
                    forma.gmodo_coopsafa = "UPD";
                }
                else
                {
                    forma.gmodo_coopsafa = "CONS";
                }

                forma.txtNo_solicitud_coopsafa.Text = vl_no_solicitud.ToString();
                DialogResult res = forma.ShowDialog();
                panelCargandoSolic.Visible = false;
                if (res == DialogResult.OK)
                {
                    p_refrescar_info();
                }
            }
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            int vl_no_solicitud = 0;
            int vl_no_movimiento = 0;
            if (gv_entradas.RowCount > 0)
            {
                DataGridViewRow row = gv_entradas.CurrentRow;
                vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());
                vl_no_movimiento = int.Parse(row.Cells["no_movimiento"].Value.ToString());

                p_marca_como_leido(vl_no_movimiento);

                s_solicitud_detalle forma = new s_solicitud_detalle(vl_no_solicitud, vl_no_movimiento);
                forma.da = da;
                forma.ShowDialog();
            }
        }

        private void panel_menutoolbar_Click(object sender, EventArgs e)
        {
            colapsar_menu();
        }

        private void panel_footer_Click(object sender, EventArgs e)
        {
            colapsar_menu();
        }

        private void pbActivarMenuDesplegable_Click(object sender, EventArgs e)
        {
            colapsar_menu();
        }

        private void timerColapsar_menu_Tick(object sender, EventArgs e)
        {
            colapsar_menu();
            timerColapsar_menu.Enabled = false;
        }

        private void manejoDeExcepcionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int vl_no_solicitud = 0;
            if (gv_entradas.RowCount > 0)
            {
                DataGridViewRow row = gv_entradas.CurrentRow;
                vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());
                string vl_nombre = row.Cells["nombre_cliente"].Value.ToString();
                string vl_producto = row.Cells["producto"].Value.ToString();
                string vl_moneda = row.Cells["desc_moneda"].Value.ToString();
                string vl_monto = String.Format("{0:###,###,###,##0.00}", float.Parse(row.Cells["monto_solicitado"].Value.ToString()));

                s_excepciones_doc01 forma = new s_excepciones_doc01(da, vl_no_solicitud, vl_nombre, vl_producto, vl_monto);
                forma.ShowDialog();
            }
        }

        private void centrar_panel()
        {
            panelCargandoSolic.Location = new Point((this.gv_entradas.Width / 2 - panelCargandoSolic.Size.Width) - 50 / 2, this.gv_entradas.Height / 2 - panelCargandoSolic.Size.Height / 2);
            panelCargandoSolic.Anchor = AnchorStyles.None;
        }

        public static void tempo(Panel panel)
        {
            panel.Visible = false;
        }

        private void abrirSolicitudToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p_mostrar_espera("Cargado solicitud..", true);
            int vl_no_solicitud = 0;
            int vl_no_movimiento = 0;
            if (gv_entradas.RowCount > 0)
            {
                DataGridViewRow row = gv_entradas.CurrentRow;
                vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());
                vl_no_movimiento = int.Parse(row.Cells["no_movimiento"].Value.ToString());
                
                string nodo_actual = treeView.SelectedNode.Text;
                
                DataTable dtSolic = da.ObtenerInfoSolicitud(vl_no_solicitud);
                int vl_estado_solicitud = int.Parse(dtSolic.Rows[0]["estado_solicitud_id"].ToString());
                s_PreCalificado forma = new s_PreCalificado(da);
                if (vl_estado_solicitud == 1 || vl_estado_solicitud == 2)
                {
                    forma.gmodo_coopsafa = "UPD";
                }
                else
                {
                    forma.gmodo_coopsafa = "CONS";
                }

                if (nodo_actual == "En proceso")
                {
                    forma.gmodo_coopsafa = "CONS";
                }

                forma.txtNo_solicitud_coopsafa.Text = vl_no_solicitud.ToString();
                DialogResult res = forma.ShowDialog();
                panelCargandoSolic.Visible = false;
                if (res == DialogResult.OK)
                {
                    p_refrescar_info();
                }
            }
        }

        private void nuevaSolicitudToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (tipo_sol == "N")
            {
                btnNuevaSolic_Click(null, null);
            }else
                btnNuevaSolic_ClickJ(null, null);
        }

        private void timerMostrarEspera_Tick(object sender, EventArgs e)
        {
            panelCargandoSolic.Visible = false;
            Application.DoEvents();
        }
        private void p_mostrar_espera(string texto, bool autohie)
        {
            panelCargandoSolic.Visible = true;
            labelCargado.Text = texto;
            centrar_panel();
            Application.DoEvents();
            if (autohie)
                timerMostrarEspera.Enabled = true;
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Int32 vl_no_solicitud = 0;
            Int32 vl_no_movimiento = 0;
            if (gv_entradas.RowCount > 0)
            {
                DataGridViewRow row = gv_entradas.CurrentRow;
                vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());
                vl_no_movimiento = int.Parse(row.Cells["no_movimiento"].Value.ToString());
                FrmsRpts.frmRpt_solicitud forma = new FrmsRpts.frmRpt_solicitud(da, vl_no_solicitud);
                forma.ShowDialog();
            }
        }

        private void btnCerrar_solicitud_Click(object sender, EventArgs e)
        {
            int vl_no_solicitud = 0;
            if (gv_entradas.RowCount > 0)
            {
                DataGridViewRow row = gv_entradas.CurrentRow;
                vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());
                s_cerrar_solicitud forma = new s_cerrar_solicitud();
                forma.da = da;
                forma.labelFilial.Text = lblSuFilial.Text;
                forma.gno_solicitud = vl_no_solicitud;
                DialogResult result = forma.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Button_refrescar_info_Click(null, null);
                }
            }
        }

        private void btnCancelar_consulta_Click(object sender, EventArgs e)
        {
            btnCancelar_consulta.Visible = false;
            button4_Click(null, null);
            txtTexto_buscar.Text = "";
        }

        private void txtTexto_buscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            btnCancelar_consulta.Visible = true;
        }

        private void eliminarCarpetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            s_carpetas_opciones_borrar forma = new s_carpetas_opciones_borrar();
            DialogResult res = forma.ShowDialog();
            if (res == DialogResult.OK)
            {
                p_popular_treview();
            }
        }

        private void eliminarCarpetaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            s_carpetas_opciones_borrar forma = new s_carpetas_opciones_borrar();
            DialogResult res = forma.ShowDialog();
            if (res == DialogResult.OK)
            {
                p_popular_treview();
            }
        }

        private void sdfaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            s_carpetas_doc forma = new s_carpetas_doc();
            DialogResult res = forma.ShowDialog();
            if (res == DialogResult.OK)
            {
                p_popular_treview();
            }
        }

        private void quitarDeLaCarpetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int vl_no_solicitud;
            DataGridViewRow row = gv_entradas.CurrentRow;
            if (gv_entradas.RowCount > 0)
            {
                vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());
                string vl_carpeta = treeView.SelectedNode.Text.ToString();
                string vl_carpeta_id = treeView.SelectedNode.Name.ToString();
                if (MessageBox.Show("Desea quitar la solicitud de esta carpeta (" + vl_carpeta + ")", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                try
                {
                    da.QuitarSolicDeCarperta(vl_no_solicitud, Convert.ToInt16(vl_carpeta_id));
                    p_refrescar_info();
                    p_popular_treview();
                    p_popular_lista_entradas("");
                    treeView.SelectedNode = treeView.Nodes[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void calculoDeLiquidaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            s_calcliquidacion_doc forma = new s_calcliquidacion_doc();
            forma.da = da;
            forma.ShowDialog();
        }

        private void elaborarAnalisisCuantitativoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p_mostrar_espera("Cargado..", true);
            int vl_no_solicitud = 0;
            int vl_no_movimiento = 0;
            if (gv_entradas.RowCount > 0)
            {
                DataGridViewRow row = gv_entradas.CurrentRow;
                vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());
                vl_no_movimiento = int.Parse(row.Cells["no_movimiento"].Value.ToString());
                
                DataTable dtSolic = da.ObtenerInfoSolicitud(vl_no_solicitud);
                int vl_estado_solicitud = int.Parse(dtSolic.Rows[0]["estado_solicitud_id"].ToString());
                s_analisis_cuantitativo forma = new s_analisis_cuantitativo();
                forma.txtNo_solicitud.Text = vl_no_solicitud.ToString();
                forma.da = da;
                DialogResult res = forma.ShowDialog();
                panelCargandoSolic.Visible = false;
            }
        }

        private void buscarSolicitudesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnBuscar_sol_Click(null, null);
        }

        private void lblSuFilial_Click(object sender, EventArgs e)
        {
            s_filiales_usuario forma = new s_filiales_usuario();
            forma.da = da;
            DialogResult result = forma.ShowDialog();
            if (result == DialogResult.OK)
            {
                DocSys.vl_agencia_usuario = int.Parse(forma.codigo_filial_seleccionada);
                lblSuFilial.Text = forma.nombre_filia_seleccionada;
                p_refrescar_info();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (global_es_admon_sistema || (global_es_admon_temp && global_tipo_admon_temp_sol))
            {
                this.Opacity = .85;
                s_cnf_workflow_conf forma = new s_cnf_workflow_conf(da, DocSys.vl_workflow_id);
                forma.ShowDialog();
                this.Opacity = 100;
            }
        }

        private void label_Titulo_lista_Click(object sender, EventArgs e)
        {
            Notificacion.show_Toast(5000, "Tiene una nueva notificación! Ir a los mensajes para ver los detalles.");
        }

        private void certificaciónDeCreditoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p_mostrar_espera("Cargado..", true);
            int vl_no_solicitud = 0;
            int vl_no_movimiento = 0;
            if (gv_entradas.RowCount > 0)
            {
                DataGridViewRow row = gv_entradas.CurrentRow;
                vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());
                vl_no_movimiento = int.Parse(row.Cells["no_movimiento"].Value.ToString());
                
                FrmsRpts.frmRpt_ResolucionComite forma = new FrmsRpts.frmRpt_ResolucionComite(da);
                forma.gno_solicitud = Convert.ToInt32(vl_no_solicitud.ToString());
                forma.ShowDialog();
                panelCargandoSolic.Visible = false;
            }
        }

        private void analisisCualitativoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p_mostrar_espera("Cargado..", true);
            int vl_no_solicitud = 0;
            int vl_no_movimiento = 0;
            if (gv_entradas.RowCount > 0)
            {
                DataGridViewRow row = gv_entradas.CurrentRow;
                vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());
                vl_no_movimiento = int.Parse(row.Cells["no_movimiento"].Value.ToString());

                FrmsRpts.frmRpt_AnalisisCualitativo forma = new FrmsRpts.frmRpt_AnalisisCualitativo(da);
                forma.gno_solicitud = Convert.ToInt32(vl_no_solicitud.ToString());
                forma.ShowDialog();
                panelCargandoSolic.Visible = false;
            }
        }

        private void observacionesDelAnalistasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (global_estacion_id_to == 1002 || global_estacion_id_to == 2001 || global_estacion_id_to == 3001)
            {
                p_mostrar_espera("Cargado..", true);
                int vl_no_solicitud = 0;
                int vl_no_movimiento = 0;
                if (gv_entradas.RowCount > 0)
                {
                    DataGridViewRow row = gv_entradas.CurrentRow;
                    vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());
                    vl_no_movimiento = int.Parse(row.Cells["no_movimiento"].Value.ToString());
                    string vl_nombre = row.Cells["nombre_cliente"].Value.ToString();
                    string vl_producto = row.Cells["producto"].Value.ToString();
                    string vl_moneda = row.Cells["desc_moneda"].Value.ToString();
                    string vl_monto = String.Format("{0:###,###,###,##0.00}", float.Parse(row.Cells["monto_solicitado"].Value.ToString()));
                    string vl_plazo_s = row.Cells["meses_plazo"].Value.ToString();

                    s_analistas_observacion forma = new s_analistas_observacion();
                    forma.da = da;
                    forma.gno_solicitud = Convert.ToInt32(vl_no_solicitud.ToString());
                    forma.txtNo_solicitud.Text = vl_no_solicitud.ToString();
                    forma.txtNombre_solicitante.Text = vl_nombre;
                    forma.txtProducto.Text = vl_producto;
                    forma.txtMonto_solicitado.Text = vl_monto;

                    forma.ShowDialog();
                    panelCargandoSolic.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("No tiene acceso a visualizar esta opcion...", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void imprimirVisualizarSolicitudToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Int32 vl_no_solicitud = 0;
            Int32 vl_no_movimiento = 0;
            if (gv_entradas.RowCount > 0)
            {
                DataGridViewRow row = gv_entradas.CurrentRow;
                vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());
                vl_no_movimiento = int.Parse(row.Cells["no_movimiento"].Value.ToString());
                FrmsRpts.frmRpt_solicitud forma = new FrmsRpts.frmRpt_solicitud(da, vl_no_solicitud);
                forma.ShowDialog();
            }
        }

        private void administracionDeAprobacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            s_admin_resoluciones forma = new s_admin_resoluciones(da);
            forma.ShowDialog();
        }

        private void hojaDeRutasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Int32 vl_no_solicitud = 0;
            //Int32 vl_no_movimiento = 0;
            if (gv_entradas.RowCount > 0)
            {
                DataGridViewRow row = gv_entradas.CurrentRow;
                vl_no_solicitud = Int32.Parse(row.Cells["no_solicitud"].Value.ToString());
                FrmsRpts.frmRpt_HojaRuta forma = new FrmsRpts.frmRpt_HojaRuta();
                forma.da = this.da;
                forma.gno_solicitud = vl_no_solicitud;
                forma.ShowDialog();
            }
        }

        private void hojaDeControlDeDocumentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Int32 vl_no_solicitud = 0;
            Int32 vl_no_movimiento = 0;
            if (gv_entradas.RowCount > 0)
            {
                DataGridViewRow row = gv_entradas.CurrentRow;
                vl_no_solicitud = Int32.Parse(row.Cells["no_solicitud"].Value.ToString());
                FrmsRpts.frmRpt_ControlDocumental forma = new FrmsRpts.frmRpt_ControlDocumental();
                forma.da = this.da;
                forma.gno_solicitud = vl_no_solicitud;
                forma.ShowDialog();
            }
        }

        private void procesoDeConciliacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            s_conciliacion_doc02 forma = new s_conciliacion_doc02();
            forma.da = this.da;
            forma.ShowDialog();
        }

        private void btnChat_Click(object sender, EventArgs e) { }

        private void libreriaToolStripMenuItem_Click(object sender, EventArgs e) { }

        private void observacionesDePrestamosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Si es prestamos TGU,SPS,LCBA, sino no puede ver
            if (global_estacion_id_to == 1003 || global_estacion_id_to == 2002 || global_estacion_id_to == 3002)
            {
                p_mostrar_espera("Cargado..", true);
                int vl_no_solicitud = 0;
                int vl_no_movimiento = 0;
                if (gv_entradas.RowCount > 0)
                {
                    DataGridViewRow row = gv_entradas.CurrentRow;
                    vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());
                    vl_no_movimiento = int.Parse(row.Cells["no_movimiento"].Value.ToString());
                    string vl_nombre = row.Cells["nombre_cliente"].Value.ToString();
                    string vl_producto = row.Cells["producto"].Value.ToString();
                    string vl_moneda = row.Cells["desc_moneda"].Value.ToString();
                    string vl_monto = String.Format("{0:###,###,###,##0.00}", float.Parse(row.Cells["monto_solicitado"].Value.ToString()));
                    string vl_plazo_s = row.Cells["meses_plazo"].Value.ToString();

                    s_Prestamos_observaciones forma = new s_Prestamos_observaciones();
                    forma.da = da;
                    forma.gno_solicitud = Convert.ToInt32(vl_no_solicitud.ToString());
                    forma.txtNo_solicitud.Text = vl_no_solicitud.ToString();
                    forma.txtNombre_solicitante.Text = vl_nombre;
                    forma.txtProducto.Text = vl_producto;
                    forma.txtMonto_solicitado.Text = vl_monto;

                    forma.ShowDialog();
                    panelCargandoSolic.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("No tiene acceso a visualizar esta opcion...", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void formatoDeExcepcionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //felvir01
            if (this.dgvExcepciones.RowCount > 0)
            {
                DataGridViewRow row = this.dgvExcepciones.CurrentRow;
                int codigo_excepcion = Convert.ToInt32(row.Cells["cod_excepcion_sol"].Value);
                int noSolicitud = Convert.ToInt32(row.Cells["no_solicitud_exc"].Value);

                FrmsRpts.frmRpt_FormatoExcepcion forma = new FrmsRpts.frmRpt_FormatoExcepcion(codigo_excepcion, noSolicitud, this.da);
                forma.ShowDialog();
            }
            else
            {
                MessageBox.Show("Para imprimir el formato debe seleccionar una excepcion");
            }
        }

        private void pbFotoVigente_Click(object sender, EventArgs e)
        {
            s_PreCalificado_info03 forma = new s_PreCalificado_info03();
            forma.pbFotoVigente.Image = pbFotoVigente.Image;
            forma.codigo_cliente = label_codigo_cliente.Text;
            forma.da = da;
            forma.ShowDialog();
        }

        private void calculoDePrestacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.EnableRaisingEvents = false;
            string archivo = Application.StartupPath + @"\prestaciones.exe";
            proc.StartInfo.FileName = archivo;
            proc.Start();
        }

        private void reporteDeTiemposPorSolicitudesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmsRpts.frmRpt_Reporte1 frep1 = new FrmsRpts.frmRpt_Reporte1();
            frep1.da = this.da;
            frep1.ShowDialog();
        }

        private void label1_Click_1(object sender, EventArgs e){}

        private void reporteDeSolicitudesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmsRpts.frmRpt_Reporte2 frep2 = new FrmsRpts.frmRpt_Reporte2();
            frep2.da = this.da;
            frep2.ShowDialog();
        }

        //felvir01
        private void gv_entradas_CellContentClick(object sender, DataGridViewCellEventArgs e){}

        //felvir01
        private void nuevaExcepcionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.nuevaExcepciónToolStripMenuItem_Click(null, null);
        }

        #region Excepciones

        private void enviar_excepcion_Click(object sender, EventArgs e)
        {
            if (this.dgvExcepciones.RowCount > 0)
            {
                if (global_estacion_id_to == 0)
                {
                    MessageBox.Show("El usuario no tiene definida una estación de trabajo..!", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!DocSys.p_valida_politicas_envio_credito())
                {
                    return;
                }

                DataGridViewRow row = this.dgvExcepciones.CurrentRow;
                int codExcepcion = Convert.ToInt32(row.Cells["cod_excepcion_sol"].Value);

                int estacion_actual_exce = DocSys.get_estacion_excepcion(codExcepcion);

                if (global_estacion_id_to != estacion_actual_exce && estacion_actual_exce != -1)
                {
                    MessageBox.Show("La solicitud que esta tratando de enviar ya fue procesada por otro usuario !", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                int paso_actual = DocSys.get_paso_actual(codExcepcion);
                int continua = DocSys.get_cantidad_respuestas_paso(paso_actual);
                if (continua < 0)
                {
                    MessageBox.Show("No hay mas pasos definidos en el Flujo de procesos (Workflow) ", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //En caso de estar cerrada pero necesitar anularla
                DataTable dt = DocSys.Obtener_estado_excepcion(codExcepcion);
                if ((/*dt.Rows[0]["DESCRIPCION"].ToString().Equals("RECHAZADA") || */dt.Rows[0]["DESCRIPCION"].ToString().Equals("ANULADA")) && global_estacion_id_to == u_Globales.eAfiliacion)
                {
                    MessageBox.Show("La excepción ya no puede ser movida.", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (this.dgvExcepciones.Rows.Count <= 0)
                {
                    return;
                }

                if (global_estacion_id_to != (int)Estaciones.Afiliacion &
                    global_estacion_id_to != (int)Estaciones.EstacionValidacion &
                    global_estacion_id_to != (int)Estaciones.NivelResolutivoFilial)
                {
                    MessageBox.Show("No hay opciones disponibles para la estación actual.");
                    return;
                }

                e_mover_excepcion mover = new e_mover_excepcion(this.da, codExcepcion, global_estacion_id_to, paso_actual);
                DialogResult nresult = mover.ShowDialog();
                if (nresult == DialogResult.OK)
                {
                    p_refrescar_info(); //modificar
                }
            }
            p_refrescar_info();
        }

        private void contextMenuDGVExcep_Opening(object sender, CancelEventArgs e)
        {
            if (global_estacion_id_to != u_Globales.eAfiliacion)
            {
                this.contextMenuDGVExcep.Items["asignar_excepcion"].Enabled = true;
                this.contextMenuDGVExcep.Items["anularExcepciónToolStripMenuItem"].Enabled = true;
            }
            else
            {
                this.contextMenuDGVExcep.Items["asignar_excepcion"].Enabled = false;
                this.contextMenuDGVExcep.Items["anularExcepciónToolStripMenuItem"].Enabled = false;
            }
        }

        #endregion

        private void abrir_excepcion_Click(object sender, EventArgs e)
        {
            p_mostrar_espera("Cargado excepción..", true);
            int vl_no_solicitud = 0;
            int vl_cod_excepcion = 0;
            if (this.dgvExcepciones.RowCount > 0)
            {
                DataGridViewRow row = this.dgvExcepciones.CurrentRow;
                vl_no_solicitud = int.Parse(row.Cells["no_solicitud_exc"].Value.ToString());
                vl_cod_excepcion = int.Parse(row.Cells["cod_excepcion_sol"].Value.ToString());
                string estado = row.Cells["estado_excep"].Value.ToString();

                e_nueva_excepcion_solicitud forma = new e_nueva_excepcion_solicitud(vl_no_solicitud, u_Globales.accionModificar, this.da, vl_cod_excepcion, string.Empty, global_estacion_id_to, estado);
                forma.noFilial = global_estacion_id_to;
                DialogResult res = forma.ShowDialog();
                panelCargandoSolic.Visible = false;
                p_refrescar_info();
            }
        }

        private void nueva_excepcion_Click(object sender, EventArgs e)
        {
            nuevaExcepciónToolStripMenuItem_Click(null, null);
        }

        private void verExcepcionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.manejoDeExcepcionesToolStripMenuItem_Click(null, null);
        }

        private void menu_princ_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //felvir01
            //condiciona si puede o no agregar una excepcion
            string ver_Excepciones = DocSys.puede_ver_excepciones(global_estacion_id_to);
            if (global_estacion_id_to == u_Globales.eAfiliacion /*&& ver_Excepciones*/)
            {
                this.contextMenu_gridview.Items["nuevaExcepcionToolStripMenuItem"].Visible = true;
            }
            else
            {
                this.contextMenu_gridview.Items["nuevaExcepcionToolStripMenuItem"].Visible = false;
            }
        }

        private void agregar_documento_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvExcepciones.RowCount <= 0)
                {
                    return;
                }

                DataGridViewRow row = this.dgvExcepciones.CurrentRow;
                int codigoExcepcion = int.Parse(row.Cells["cod_excepcion_sol"].Value.ToString());

                e_documento_excep forma = new e_documento_excep(codigoExcepcion, this.da);
                forma.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al tratar de registar documento:" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void adjuntos_excepcion(int codigo_Excepcion)
        {
            try
            {
                DataTable adjuntos = this.da.get_adjuntos_excepcion(codigo_Excepcion);

                list_adjuntos.BeginUpdate();
                list_adjuntos.SmallImageList = imagesSmall;
                list_adjuntos.LargeImageList = imagesLarge;
                list_adjuntos.Clear();

                foreach (DataRow item in adjuntos.Rows)
                {
                    string nombre_doc = (item["formato_excepcion"].ToString().Equals("S")) ? "Formato_Excepcion" : item["nombre_documento"].ToString();
                    ListViewItem listItem = new ListViewItem(nombre_doc);
                    listItem.ImageIndex = 3;
                    listItem.ToolTipText = nombre_doc;
                    listItem.SubItems.Add(item["extension"].ToString());
                    listItem.SubItems.Add(item["no_documento"].ToString());
                    list_adjuntos.Items.Add(listItem);
                }

                list_adjuntos.Columns.Add("Nombre Archivo", 180, HorizontalAlignment.Left);
                list_adjuntos.Columns.Add("Ext", 60, HorizontalAlignment.Left);
                list_adjuntos.Columns.Add("No. Documento", 60, HorizontalAlignment.Left);
                list_adjuntos.EndUpdate();
                list_adjuntos.Sort();
                list_adjuntos.View = View.LargeIcon;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_llenar_adjuntos : " + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void agregar_anotacion_Click(object sender, EventArgs e)
        {
            if (this.dgvExcepciones.RowCount > 0)
            {
                DataGridViewRow row = this.dgvExcepciones.CurrentRow;

                int codigo_excepcion = Convert.ToInt32(row.Cells["cod_excepcion_sol"].Value);

                e_add_notas notas = new e_add_notas(codigo_excepcion, global_estacion_id_to, u_Globales.accionAgregar, 5);
                notas.ShowDialog();
            }
        }

        private void ver_movimientos_Click(object sender, EventArgs e)
        {
            if (this.dgvExcepciones.RowCount > 0)
            {
                DataGridViewRow row = this.dgvExcepciones.CurrentRow;

                int codigo_excepcion = Convert.ToInt32(row.Cells["cod_excepcion_sol"].Value);

                e_excepcion_mov movimientos = new e_excepcion_mov(codigo_excepcion, this.da);
                movimientos.ShowDialog();
            }
        }

        private void asignar_excepcion_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvExcepciones.RowCount > 0)
                {
                    DataGridViewRow row = this.dgvExcepciones.CurrentRow;
                    int codigo_excepcion = Convert.ToInt32(row.Cells["cod_excepcion_sol"].Value);

                    s_asignar_analista asignar = new s_asignar_analista(codigo_excepcion, global_estacion_id_to, true);
                    asignar.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error {ex.InnerException} - {ex.Message}", "Error");
            }
        }

        private void anularExcepciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dgvExcepciones.RowCount > 0)
            {
                DataGridViewRow row = this.dgvExcepciones.CurrentRow;

                int codigo_excep = Convert.ToInt32(row.Cells["cod_excepcion_sol"].Value);

                if (DocSys.anular_excepcion_permitido(codigo_excep))
                {
                    //e_add_notas nota_anulacion = new e_add_notas(codigo_excep, global_estacion_id_to, u_Globales.accionAgregar, 0, true);
                    //nota_anulacion.ShowDialog();
                    e_motivo_anulacion anular = new e_motivo_anulacion();
                    DialogResult resultado = anular.ShowDialog();

                    if (!e_motivo_anulacion.Cancelo)
                    {
                        var motivo = e_motivo_anulacion.Motivo;
                        if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                        {
                            DocSys.connOracle.Open();
                        }

                        string sql = "excp.dcs_anular_excepcion";
                        OracleParameter pa_codigo_Excepcion = new OracleParameter("pa_codigo_excepcion", OracleType.Number);
                        pa_codigo_Excepcion.Direction = ParameterDirection.Input;
                        pa_codigo_Excepcion.Value = codigo_excep;

                        OracleParameter pa_opcion = new OracleParameter("pa_opcion", OracleType.VarChar);
                        pa_opcion.Direction = ParameterDirection.Input;
                        pa_opcion.Value = "MENU";

                        OracleParameter pa_motivo = new OracleParameter("pa_motivo", OracleType.VarChar);
                        pa_motivo.Direction = ParameterDirection.Input;
                        pa_motivo.Value = motivo;

                        OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(pa_codigo_Excepcion);
                        cmd.Parameters.Add(pa_opcion);
                        cmd.Parameters.Add(pa_motivo);

                        OracleDataReader dr = cmd.ExecuteReader();
                        p_refrescar_info();

                        MessageBox.Show("Excepción anulada con éxito", "Operación exitosa");
                    }
                }
                else
                {
                    MessageBox.Show("La excepción no se puede anular debido a su estado", "Error");
                }
            }
        }

        private void reporteDeExcepcionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmsRpts.frmRpt_Excepciones reporte = new FrmsRpts.frmRpt_Excepciones(this.da);
            reporte.ShowDialog();
        }

        private void dgvExcepciones_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //aqui marca los elementos
            this.p_set_grid_attr_visual_Exc();
        }

        private void p_set_grid_attr_visual_Exc()
        {
            for (int row = 0; row < this.dgvExcepciones.Rows.Count; row++)
            {
                DataGridViewCell valueCell = null;
                DataGridViewImageCell displayCell = null;
                //si la excepción está en el paso 17, significa que es la anulación	
                int codigoExcepcion = Convert.ToInt32(this.dgvExcepciones["cod_excepcion_sol", row].Value);
                int paso_actual = DocSys.get_paso_actual(codigoExcepcion);

                //En prueba 2019-05-17
                //if (dgvExcepciones.Rows[row].Cells["abierta"].Value.ToString() == "S")
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();

                    style.Font = new Font(dgvExcepciones.Font, FontStyle.Bold);
                    dgvExcepciones.Rows[row].DefaultCellStyle = style;

                    valueCell = dgvExcepciones[img_estado_registro.Index, row];
                    displayCell = (DataGridViewImageCell)dgvExcepciones[img_estado_registro.Index, row];
                    if (paso_actual == 100)
                    {
                        displayCell.Value = image_grid.Images[5];
                    }
                    else
                    {
                        displayCell.Value = image_grid.Images[2];
                    }
                }

                if (paso_actual == 100)
                {
                    this.dgvExcepciones.Rows[row].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
        }

        private void marcar_leida_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvExcepciones.RowCount > 0)
                {
                    string sql = "excp.dcs_p_marcar_excep_leida";

                    if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                    {
                        DocSys.connOracle.Open();
                    }

                    DataGridViewRow row = this.dgvExcepciones.CurrentRow;
                    int vl_cod_excepcion = int.Parse(row.Cells["cod_excepcion_sol"].Value.ToString());

                    OracleParameter pa_codigo_Excepcion = new OracleParameter("pa_codigo_excepcion", OracleType.Number);
                    pa_codigo_Excepcion.Direction = ParameterDirection.Input;
                    pa_codigo_Excepcion.Value = vl_cod_excepcion;

                    OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(pa_codigo_Excepcion);

                    cmd.ExecuteNonQuery();

                    Int32 nfila = this.dgvExcepciones.CurrentRow.Index;
                    DataGridViewCell valueCell = null;
                    DataGridViewImageCell displayCell = null;
                    if (this.dgvExcepciones.Rows[nfila].Cells["abierta"].Value.ToString() == "N")
                    {
                        DataGridViewCellStyle style = new DataGridViewCellStyle();
                        style.Font = new Font(this.dgvExcepciones.Font, FontStyle.Regular);
                        DataGridViewRow theRow = this.dgvExcepciones.Rows[nfila];
                        theRow.DefaultCellStyle = style;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void administraciónDeExcepcionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            e_admnin_excepciones admin_exc = new e_admnin_excepciones(this.da, global_gerente_filial, DocSys.vl_agencia_usuario);
            admin_exc.ShowDialog();
        }

        private void historialExcepcionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            e_historial_excepciones hist = new e_historial_excepciones(this.da, this.global_estacion_id_to);
            hist.Show();
        }

        private void pbScore_result_Click(object sender, EventArgs e)
        {
            s_AnalisisCredito_01 forma = new s_AnalisisCredito_01(da, label_no_solicitud.Text);
            forma.ShowDialog();
        }

        private void lblScore_Click(object sender, EventArgs e)
        {
            pbScore_result_Click(null, null);
        }

        private void pnlScoreCrediticio_Click(object sender, EventArgs e)
        {
            pbScore_result_Click(null, null);
        }

        private void lblResul_evaluacion_Click(object sender, EventArgs e)
        {
            pbScore_result_Click(null, null);
        }

        #region Excepciones de CS

        private void p_refrescar_info()
        {
            if (DocSys.vl_nodo_treview == "Node_entradas")
            {
                gv_entradas.DataSource = null;
                p_popular_lista_entradas("");
            }
            if (DocSys.vl_nodo_treview == "Node_salidas")
            {
                gv_entradas.DataSource = null;
                p_popular_lista_salidas("");
            }
            else
            {
                DataTable carp = this.da.CarpetasExcepciones();
                this.dgvExcepciones.DataSource = null;
                if (DocSys.vl_nodo_treview == u_Globales.RESOLUTIVO)
                {
                    p_popular_lista_entradas(string.Empty, 1);
                }
                else if (DocSys.vl_nodo_treview == u_Globales.MONTO1)
                {
                    this.p_popular_lista_entradas(string.Empty, 1, u_Globales.FiltroMonto1);
                }
                else if (DocSys.vl_nodo_treview == u_Globales.MONTO2)
                {
                    this.p_popular_lista_entradas(string.Empty, 1, u_Globales.FiltroMonto2);
                }
                else if (DocSys.vl_nodo_treview == u_Globales.MONTO3)
                {
                    this.p_popular_lista_entradas(string.Empty, 1, u_Globales.FiltroMonto3);
                }
                else
                {
                    for (int i = 0; i < carp.Rows.Count; i++)
                    {
                        if (DocSys.vl_nodo_treview == carp.Rows[i]["CARPETA_ID"].ToString())
                        {
                            p_popular_lista_entradas(string.Empty, Convert.ToInt32(carp.Rows[i]["CARPETA_ID"].ToString()));
                        }
                    }
                }
            }

            if (!verExcepciones)
            {
                int vl_no_solicitud = 0;
                if (gv_entradas.RowCount > 0)
                {
                    DataGridViewRow rowgrid = gv_entradas.CurrentRow;
                    vl_no_solicitud = int.Parse(rowgrid.Cells["no_solicitud"].Value.ToString());
                }
                p_llenar_adjuntos(vl_no_solicitud);
                p_llenar_anotaciones(vl_no_solicitud);
                p_llenar_info_gadget(vl_no_solicitud);
                btnCancelar_consulta.Visible = false;
                txtTexto_buscar.Text = "";
                p_cambiar_modo("NORM");
            }
            else
            {
                int vl_codExcepcion = 0;
                int vl_no_solicitud = 0; //cambio				
                if (this.dgvExcepciones.RowCount > 0)
                {
                    DataGridViewRow rowgrid = this.dgvExcepciones.CurrentRow;
                    vl_codExcepcion = int.Parse(rowgrid.Cells["cod_excepcion_sol"].Value.ToString());
                    vl_no_solicitud = int.Parse(rowgrid.Cells["no_solicitud_exc"].Value.ToString());
                }
                adjuntos_excepcion(vl_codExcepcion);
                llenar_anotaciones_excepcion(vl_codExcepcion);
                p_llenar_info_gadget(vl_no_solicitud);
                btnCancelar_consulta.Visible = false;
                this.dgvExcepciones.Visible = true;
                txtTexto_buscar.Text = "";
            }
        }

        private void p_popular_lista_entradas(string p_filtro, int _codExcepcion = 1, string filtroMonto = "")
        {
            p_mostrar_espera("Cargado información", false);
            global_ver_toda_filial = DocSys.p_obtener_si_todas_las_filiales(global_estacion_id_to);
            string vl_condi_toda_filial = "";
            string vl_condi_gte_filial = "";
            string vl_condi_analista = "";
            string v_oficial_servicios = string.Empty; //felvir01
            int vl_zona = 0;
            DateTime vl_date_start;
            DateTime vl_date_end;
            DateTime vl_actual_date = DateTime.Now;
            cmbZonaExcepcion.Visible = false;
            lbzona.Visible = false;

            cmbZonaExcepcion.Visible = false;
            lbzona.Visible = false;

            if (global_ver_toda_filial)
            {
                vl_condi_toda_filial = " and :pa_codigo_agencia_origen=:pa_codigo_agencia_origen";
                vl_condi_analista = "";
                if (MenuItem_verSoloMisSolicitudes.Checked)
                    vl_condi_analista = " and ms.analista='" + DocSys.vl_user + "'";
            }
            else
            {
                vl_condi_toda_filial = " and s.codigo_agencia_origen=:pa_codigo_agencia_origen ";
                if (global_gerente_filial)
                {
                    vl_condi_gte_filial = "";
                }
                else
                {
                    //cambio 2021. en todos los casos se agrega el filtro de Admin temporal 
                    if (global_es_admon_sistema || (global_es_admon_temp && global_tipo_admon_temp_sol) || (global_es_admon_temp && global_tipo_admon_temp_excp))
                    {
                        vl_condi_gte_filial = "";
                        v_oficial_servicios = string.Empty;
                    }
                    else
                    {
                        vl_condi_gte_filial = " and oficial_servicio='" + DocSys.vl_user.Trim() + "'";
                        v_oficial_servicios = " and e.oficial_servicios='" + DocSys.vl_user.Trim() + "'";
                    }
                }
            }
            //reestablecer este cambio 
            if (!this.verExcepciones) //felvir01
            {
                gv_entradas.AutoGenerateColumns = false;
                gv_entradas.DataSource = da.Llenar_lista_entradas(global_estacion_id_to, DocSys.vl_agencia_usuario, vl_condi_toda_filial, vl_condi_gte_filial, vl_condi_analista, p_filtro);
                gv_entradas.Refresh();
                gv_entradas.Columns["estacion_actual"].HeaderText = "Estación Actual";
                panelCargandoSolic.Visible = false;
            }
            else
            {
                //cambios enero-2019
                if (global_es_admon_sistema || (global_es_admon_temp && global_tipo_admon_temp_sol) || (global_es_admon_temp && global_tipo_admon_temp_excp))
                {
                    vl_condi_gte_filial = "";
                    v_oficial_servicios = string.Empty;
                }
                else
                {
                    vl_condi_gte_filial = " and oficial_servicio='" + DocSys.vl_user.Trim() + "'";
                    v_oficial_servicios = " and e.oficial_servicios='" + DocSys.vl_user.Trim() + "'";
                }

                string analista = string.Empty;
                if (this.checkBox_mis_solicitudes.Visible && this.checkBox_mis_solicitudes.Checked)
                    analista = this.label_usuario.Text;

                bool afiliacion = (global_estacion_id_to == 1000) ? true : false;
                string tabla = string.Empty;
                //Cambios
                if (global_estacion_id_to != 1000)
                {
                    tabla = ", excp.dcs_excepciones_aprobaciones apro ";
                    v_oficial_servicios = @" and apro.no_movimiento_actual = m.no_movimiento
									 and apro.usuario_comite = '" + DocSys.vl_user.Trim() + "' ";
                }

                if (global_gerente_filial && global_estacion_id_to == 1000)
                {
                    v_oficial_servicios = " and e.codigo_agencia_origen = " + DocSys.vl_agencia_usuario;
                }
                else if (global_estacion_id_to > 1001)
                {
                    tabla = string.Empty;
                    v_oficial_servicios = string.Empty;
                }

                this.dgvExcepciones.AutoGenerateColumns = false;
                this.dgvExcepciones.DataSource = DocSys.p_cargar_excepciones(global_estacion_id_to, analista, afiliacion, v_oficial_servicios, tabla, _codExcepcion, (int)Estaciones.EstacionValidacion, filtroMonto, DocSys.vl_agencia_usuario);
                //this.ResaltarAnuladas();
                this.panelCargandoSolic.Visible = false;
               
                //validar si es administrador de excepciones
                global_es_admon_sistema = da.EsAdministradorSistema(DocSys.vl_user);

                //valida si es nivel resolutivo 
                global_es_nivel_resolutivo = this.da.EsAdminNivelResolutivo(DocSys.vl_user);

                //validar si es administrador temporal y si esta dentro del rango de fechas
                global_es_admon_temp = this.da.EsAdminTemporal(DocSys.vl_user);

                //valida so es admon de excepciones 
                global_tipo_admon_temp_excp = this.da.TipoAdminTemporalE(DocSys.vl_user);

                this.dgvExcepciones.AutoGenerateColumns = false;
                //this.ResaltarAnuladas();

                //cambio 2021. Identificación de estacion validacion para cargar por zonas las excepciones 
                if (global_estacion_id_to == 1006)
                {
                    cmbZonaExcepcion.Visible = true;
                    lbzona.Visible = true;

                    v_oficial_servicios = "";

                    this.cmbZonaExcepcion.Visible = true;
                    int zon = Convert.ToInt32(cmbZonaExcepcion.SelectedIndex);
                    int global_estacion_id_to = Convert.ToInt32(comboBoxEstacionUsuario.SelectedValue);

                    this.dgvExcepciones.DataSource = DocSys.p_cargar_excepciones(global_estacion_id_to, analista, afiliacion, " ", tabla, _codExcepcion, (int)Estaciones.EstacionValidacion, filtroMonto, DocSys.vl_agencia_usuario, zon);
                    this.panelCargandoSolic.Visible = false;
                } else
                {
                    if (global_estacion_id_to == 1001 || global_estacion_id_to == 3003 || global_estacion_id_to == 2003 ||
                            global_estacion_id_to == 1004 || global_estacion_id_to == 4000 || global_estacion_id_to == 5000)
                    {
                        this.dgvExcepciones.DataSource = DocSys.p_cargar_excepcionesE(global_estacion_id_to, analista, afiliacion, v_oficial_servicios, tabla, _codExcepcion, (int)Estaciones.EstacionValidacion, filtroMonto, DocSys.vl_agencia_usuario);
                    }
                    else {
                        this.dgvExcepciones.DataSource = DocSys.p_cargar_excepcionesN(global_estacion_id_to, analista, afiliacion, v_oficial_servicios, tabla, _codExcepcion, (int)Estaciones.EstacionValidacion, filtroMonto, DocSys.vl_agencia_usuario);
                    }
                }

                this.panelCargandoSolic.Visible = false;
            }
        }

        private void p_llenar_info_gadget(int p_no_solicitud)
        {
            label_oficial.Text = "";
            label_nombre_oficial.Text = "";
            label_no_solicitud.Text = "";
            label_codigo_cliente.Text = "";
            label_nombre_afiliado.Text = "";
            label_monto.Text = "";
            label_plazo.Text = "";
            label_fuente.Text = "";
            label_filial.Text = "";
            //cambio hoy
            if (gv_entradas.RowCount > 0 && this.gv_entradas.Visible == true)
            {
                DataGridViewRow row = gv_entradas.CurrentRow;
                string vl_oficial_remitente = "";
                string vl_nombre_oficial_remitente = "";
                DocSys.p_obtener_oficial_remitente(p_no_solicitud, out vl_oficial_remitente, out vl_nombre_oficial_remitente);
                label_oficial.Text = vl_oficial_remitente;
                label_nombre_oficial.Text = vl_nombre_oficial_remitente;
                #region foto Oficial Serv
                try
                {
                    //string scodigo_cliente_usuario = da.ObtenerCodigoClientexUsuario(vl_oficial_remitente);
                    //byte[] tmpfotoUsuario = da.ObtenerFotoUsuario(scodigo_cliente_usuario);
                    //pbOficialServ.Image = DocSys.p_CopyDataToBitmap(tmpfotoUsuario);
                    //if (pbOficialServ.Image == null)
                    //{
                    //    pbOficialServ.Image = null;
                    //    pbOficialServ.Visible = false;
                    //}
                    //else
                    //{
                    //    pbOficialServ.Visible = true;
                    //}

                }
                catch
                {
                    pbOficialServ.Image = null;
                }
                #endregion

                label_no_solicitud.Text = row.Cells["no_solicitud"].Value.ToString();
                label_codigo_cliente.Text = row.Cells["codigo_cliente"].Value.ToString();
                label_nombre_afiliado.Text = row.Cells["nombre_cliente"].Value.ToString();
                label_monto.Text = String.Format("{0:###,###,###,##0.00}", float.Parse(row.Cells["monto_solicitado"].Value.ToString()));
                label_plazo.Text = String.Format("{0:###0}", float.Parse(row.Cells["meses_plazo"].Value.ToString()));
                label_fuente.Text = row.Cells["descripcion_fuente"].Value.ToString();
                label_filial.Text = row.Cells["nombre_agencia"].Value.ToString();
                try
                {
                    byte[] fotoAfiliado;
                    string vl_fecha_ultima_act = "";
                    da.ObtenerFotoAfiliado(label_codigo_cliente.Text, out fotoAfiliado, out vl_fecha_ultima_act);
                    if (fotoAfiliado != null)
                    {
                        pbFotoVigente.Image = DocSys.p_CopyDataToBitmap(fotoAfiliado);
                        pbFotoVigente.Visible = true;
                    }
                    else
                    {
                        pbFotoVigente.Image = null;
                        pbFotoVigente.Visible = false;
                    }
                }
                catch
                {
                    pbFotoVigente.Visible = false;
                    pbFotoVigente.Image = null;
                }


            }
            else if (this.dgvExcepciones.RowCount > 0 && this.gv_entradas.Visible == false)
            {
                foreach (DataGridViewRow row in gv_entradas.Rows)
                {
                    if (row.Cells["no_solicitud"].Value.ToString().Equals(p_no_solicitud.ToString()))
                    {
                        string vl_oficial_remitente = "";
                        string vl_nombre_oficial_remitente = "";
                        DocSys.p_obtener_oficial_remitente(p_no_solicitud, out vl_oficial_remitente, out vl_nombre_oficial_remitente);
                        label_oficial.Text = vl_oficial_remitente;
                        label_nombre_oficial.Text = vl_nombre_oficial_remitente;
                        #region foto Oficial Serv
                        try
                        {
                            //string scodigo_cliente_usuario = da.ObtenerCodigoClientexUsuario(vl_oficial_remitente);
                            //byte[] tmpfotoUsuario = da.ObtenerFotoUsuario(scodigo_cliente_usuario);
                            //pbOficialServ.Image = DocSys.p_CopyDataToBitmap(tmpfotoUsuario);
                            //if (pbOficialServ.Image == null)
                            //{
                            //    pbOficialServ.Image = null;
                            //    pbOficialServ.Visible = false;
                            //}
                            //else
                            //{
                            //    pbOficialServ.Visible = true;
                            //}

                        }
                        catch
                        {
                            pbOficialServ.Image = null;
                        }
                        #endregion

                        label_no_solicitud.Text = row.Cells["no_solicitud"].Value.ToString();
                        label_codigo_cliente.Text = row.Cells["codigo_cliente"].Value.ToString();
                        label_nombre_afiliado.Text = row.Cells["nombre_cliente"].Value.ToString();
                        label_monto.Text = String.Format("{0:###,###,###,##0.00}", float.Parse(row.Cells["monto_solicitado"].Value.ToString()));
                        label_plazo.Text = String.Format("{0:###0}", float.Parse(row.Cells["meses_plazo"].Value.ToString()));
                        label_fuente.Text = row.Cells["descripcion_fuente"].Value.ToString();
                        label_filial.Text = row.Cells["nombre_agencia"].Value.ToString();
                        try
                        {
                            byte[] fotoAfiliado;
                            string vl_fecha_ultima_act = "";
                            da.ObtenerFotoAfiliado(label_codigo_cliente.Text, out fotoAfiliado, out vl_fecha_ultima_act);
                            if (fotoAfiliado != null)
                            {
                                pbFotoVigente.Image = DocSys.p_CopyDataToBitmap(fotoAfiliado);
                                pbFotoVigente.Visible = true;
                            }
                            else
                            {
                                pbFotoVigente.Image = null;
                                pbFotoVigente.Visible = false;
                            }
                        }
                        catch
                        {
                            pbFotoVigente.Visible = false;
                            pbFotoVigente.Image = null;
                        }
                        break;
                    }
                }
            }
        }

        private void gv_entradas_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            //cambio
            if (MenuItemmostrarInforRapida.Checked && this.gv_entradas.Visible == true)
            {
                DataGridViewRow row = gv_entradas.CurrentRow;
                if (row != null)
                {
                    if (e.RowIndex >= 0)
                    {
                        string vl_filial_origen = gv_entradas.Rows[e.RowIndex].Cells["nombre_agencia"].Value.ToString();
                        string vl_oficial_servicio = gv_entradas.Rows[e.RowIndex].Cells["oficial_servicio"].Value.ToString();
                        string vl_no_solicitud = gv_entradas.Rows[e.RowIndex].Cells["no_solicitud"].Value.ToString();
                        string vl_codigo_cliente = gv_entradas.Rows[e.RowIndex].Cells["codigo_cliente"].Value.ToString();
                        string vl_nombre_cliente = gv_entradas.Rows[e.RowIndex].Cells["nombre_cliente"].Value.ToString();
                        string vl_producto = gv_entradas.Rows[e.RowIndex].Cells["producto"].Value.ToString();
                        string vl_monto = gv_entradas.Rows[e.RowIndex].Cells["monto_solicitado"].Value.ToString();
                        string vl_plazo = gv_entradas.Rows[e.RowIndex].Cells["meses_plazo"].Value.ToString();
                        string vl_estacion_actual = gv_entradas.Rows[e.RowIndex].Cells["estacion_actual"].Value.ToString();
                        string vl_no_movimiento = gv_entradas.Rows[e.RowIndex].Cells["no_movimiento"].Value.ToString();

                        miniinfo_sol.get_set_filial_origen = vl_filial_origen;
                        miniinfo_sol.get_set_oficial = vl_oficial_servicio;
                        miniinfo_sol.get_set_no_solicitud = vl_no_solicitud.ToString();
                        miniinfo_sol.get_set_codigo_cliente = vl_codigo_cliente;
                        miniinfo_sol.get_set_nombre_cliente = vl_nombre_cliente;
                        miniinfo_sol.get_set_producto = vl_producto;
                        miniinfo_sol.get_set_monto = vl_monto;
                        miniinfo_sol.get_set_plazo = vl_plazo;
                        miniinfo_sol.get_set_estacion_act = vl_estacion_actual;
                        miniinfo_sol.get_set_no_movimiento = vl_no_movimiento;

                        byte[] foto;
                        string vl_fecha_ultima_act = "";
                        da.ObtenerFotoAfiliado(vl_codigo_cliente, out foto, out vl_fecha_ultima_act);
                        if (foto != null)
                        {
                            miniinfo_sol.pbFotoVigente.Image = DocSys.p_CopyDataToBitmap(foto);
                        }
                        else
                        {
                            miniinfo_sol.pbFotoVigente.Image = Properties.Resources.contacto_icon;
                        }

                        Point pos = this.PointToScreen(e.Location);
                        miniinfo_sol.Show();
                        miniinfo_sol.Location = new Point(Control.MousePosition.X - 105, Control.MousePosition.Y + 10);
                        miniinfo_sol.Refresh();
                    }
                }
            }
            else
            {
                if (e.ColumnIndex == 12) //Oficial de Servicio
                {
                    if (e.RowIndex >= 0)
                    {
                        miniinfo.get_set_codigo_usuario = gv_entradas.Rows[e.RowIndex].Cells["oficial_servicio"].Value.ToString();

                        Point pos = this.PointToScreen(e.Location);
                        miniinfo.da = this.da;
                        miniinfo.Show();
                        miniinfo.Location = new Point(Control.MousePosition.X - 160, Control.MousePosition.Y + 10);
                        miniinfo.Refresh();
                    }
                }
            }
        }

        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            DocSys.vl_nodo_treview = e.Node.Name;
            if (e.Node.Name == "")
                return;

            if (e.Node.Name.Substring(0, 1).ToUpper() == "N") //"la letra 'N' de Node_"
            {
                switch (e.Node.Name)
                {
                    case "Node_entradas":
                        gv_entradas.DataSource = null;
                        this.verExcepciones = false; //felvir01
                        p_popular_lista_entradas("");
                        p_refrescar_info_secundaria();
                        this.btnResolucion.Visible = true; //cambio
                        panelSearch.Visible = true;
                        this.dgvExcepciones.Visible = false; //felvir01
                        this.gv_entradas.Visible = true; //felvir01
                        this.lblTituloExcepciones.Visible = false;
                        break;

                    case "Node_salidas":
                        gv_entradas.DataSource = null;
                        this.verExcepciones = false; //felvir01
                        this.gv_entradas.Visible = true; //felvir01
                        panelSearch.Visible = false;
                        this.btnResolucion.Visible = true; //cambio
                        p_cambiar_modo("NORM");
                        p_popular_lista_salidas("");
                        p_refrescar_info_secundaria();
                        this.dgvExcepciones.Visible = false; //felvir01
                        this.lblTituloExcepciones.Visible = false;
                        break;

                    case "Node_denegadas":
                        p_popular_lista_denegadas("");
                        this.verExcepciones = false; //felvir01
                        p_refrescar_info_secundaria();
                        this.btnResolucion.Visible = true; //cambio
                        panelSearch.Visible = false;
                        p_cambiar_modo("NORM");
                        this.gv_entradas.Visible = true; //felvir01
                        this.dgvExcepciones.Visible = false; //felvir01
                        this.lblTituloExcepciones.Visible = false;
                        break;
                    //felvir01
                    /*case "1":
						this.dgvExcepciones.DataSource = null;
						this.verExcepciones = true; //felvir01
						this.gv_entradas.Visible = false;
						this.btnResolucion.Visible = false; //cambio
															//panel de búsqueda?
						this.p_popular_lista_entradas(string.Empty, 1);
						this.dgvExcepciones.Location = new Point(2, 72);
						this.dgvExcepciones.Visible = true;
						this.lblTituloExcepciones.Visible = true;
						break;*/

                    default:
                        p_popular_lista_vacio("");
                        this.verExcepciones = false; //felvir01
                        p_refrescar_info_secundaria();
                        this.dgvExcepciones.Visible = false; //felvir01						
                        break;
                }
                label_Titulo_lista.Text = e.Node.Text;
            }
            else
            {
                //En caso de no ser estación validación
                if (global_estacion_id_to != (int)Estaciones.EstacionValidacion)
                {
                    if (global_estacion_id_to == (int)Estaciones.ComiteII |
                        global_estacion_id_to == (int)Estaciones.ComiteIII |
                        global_estacion_id_to == (int)Estaciones.ComiteILCBA |
                        global_estacion_id_to == (int)Estaciones.ComiteISPS |
                        global_estacion_id_to == (int)Estaciones.ComiteITGU)
                    {
                        this.dgvExcepciones.DataSource = null;
                        this.verExcepciones = true; //felvir01
                        this.gv_entradas.Visible = false;
                        this.btnResolucion.Visible = false;

                        this.p_popular_lista_entradas(string.Empty, 1);
                        //p_refrescar_info_secundaria();
                        this.dgvExcepciones.Location = new Point(2, 72);
                        this.dgvExcepciones.Visible = true;
                        this.lblTituloExcepciones.Visible = true;
                        label_Titulo_lista.Text = "Pendiente de resolución";
                    }
                    else
                    {
                        int i = 0;
                        bool resultado = int.TryParse(e.Node.Name, out i);
                        if (resultado)
                        {
                            this.dgvExcepciones.DataSource = null;
                            this.verExcepciones = true; //felvir01
                            this.gv_entradas.Visible = false;
                            this.btnResolucion.Visible = false;

                            this.p_popular_lista_entradas(string.Empty, i);
                            //p_refrescar_info_secundaria();
                            this.dgvExcepciones.Location = new Point(2, 72);
                            this.dgvExcepciones.Visible = true;
                            this.lblTituloExcepciones.Visible = true;

                            switch (i)
                            {
                                case 1:
                                    label_Titulo_lista.Text = "En Proceso";
                                    break;
                                case 2:
                                    label_Titulo_lista.Text = "Aprobadas";
                                    break;
                                case 3:
                                    label_Titulo_lista.Text = "Rechazada";
                                    break;
                                case 4:
                                    label_Titulo_lista.Text = "Anulada";
                                    break;
                                case 5:
                                    label_Titulo_lista.Text = "Devuelto";
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            p_popular_lista_por_carpetas(e.Node.Name);
                            p_refrescar_info_secundaria();
                            label_Titulo_lista.Text = e.Node.Text;
                        }
                    }
                }
                else
                {
                    this.dgvExcepciones.DataSource = null;
                    this.verExcepciones = true; //felvir01
                    this.gv_entradas.Visible = false;
                    this.btnResolucion.Visible = false;

                    switch (e.Node.Name)
                    {
                        case u_Globales.MONTO1:
                            this.p_popular_lista_entradas(string.Empty, 1, u_Globales.FiltroMonto1);
                            break;
                        case u_Globales.MONTO2:
                            this.p_popular_lista_entradas(string.Empty, 1, u_Globales.FiltroMonto2);
                            break;
                        case u_Globales.MONTO3:
                            this.p_popular_lista_entradas(string.Empty, 1, u_Globales.FiltroMonto3);
                            break;
                        default:
                            break;
                    }
                    //p_refrescar_info_secundaria();
                    this.dgvExcepciones.Location = new Point(2, 72);
                    this.dgvExcepciones.Visible = true;
                    this.lblTituloExcepciones.Visible = true;
                }
            }
        }

        /**********************************************************************************/
        private void nuevaExcepciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int vl_no_solicitud = 0;
            if (gv_entradas.RowCount > 0)
            {
                DataGridViewRow row = gv_entradas.CurrentRow;
                vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());

                int totalExcepciones = DocSys.total_Excepciones(vl_no_solicitud);

                int parametroTotal = DocSys.total_Excepciones_permitidas();

                if ((totalExcepciones + 1) <= parametroTotal)
                {
                    int excepciones_procesadas = DocSys.get_excepciones_Activas(vl_no_solicitud);
                    if (excepciones_procesadas > 0)
                    {
                        MessageBox.Show("La solicitud ya tiene una excepción en proceso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    //cambio
                    string estadoSolic = row.Cells["estado_solicitud"].Value.ToString();
                    if (!estadoSolic.Equals("Creada en Afiliacion"))
                    {
                        if (!estadoSolic.Equals("En proceso"))
                        {
                            MessageBox.Show("La solicitud ya ha sido procesada, no puede agregar ninguna excepción", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    int excep_aprob_rec = DocSys.get_Estado_excepciones(vl_no_solicitud);
                    if (excep_aprob_rec > 0)
                    {
                        MessageBox.Show("La solicitud ya cuenta con una excepción cerrada. Para agregar otra debe anular la actual", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    e_nueva_excepcion_solicitud forma = new e_nueva_excepcion_solicitud(vl_no_solicitud, u_Globales.accionAgregar, this.da, 0, this.label_usuario.Text, global_estacion_id_to);
                    forma.noFilial = global_estacion_id_to;
                    forma.ShowDialog();
                }
                else if (totalExcepciones == -1)
                {
                    MessageBox.Show("No se ha podido comprobar la cantidad de excepciones que contiene la solicitud.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if ((totalExcepciones + 1) > parametroTotal)
                {
                    MessageBox.Show("Se ha excedido el número de excepciones permitidas por solicitud", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /**********************************************************************************/
        private void dgvExcepciones_SelectionChanged(object sender, EventArgs e)
        {
            //cambio hoy
            if (this.dgvExcepciones.RowCount > 0)
            {
                DataGridViewRow row = this.dgvExcepciones.CurrentRow;
                int vl_no_solicitud = int.Parse(row.Cells["no_solicitud_exc"].Value.ToString()); ;

                int codigo_Excepcion = Convert.ToInt32(row.Cells["cod_excepcion_sol"].Value.ToString());
                this.adjuntos_excepcion(codigo_Excepcion);
                p_llenar_info_gadget(vl_no_solicitud);
                this.llenar_anotaciones_excepcion(codigo_Excepcion);
            }
        }

        #endregion

        private void configurarPáginaToolStripMenuItem_Click(object sender, EventArgs e) { }

        private void MenuItemmostrarInforRapida_Click(object sender, EventArgs e) { }

        //Solo para pruebas 2019-05-17
        private void cerrarExcepciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvExcepciones.RowCount > 0)
                {
                    DataGridViewRow row = this.dgvExcepciones.CurrentRow;
                    int vl_cod_excepcion = int.Parse(row.Cells["cod_excepcion_sol"].Value.ToString());
                    string sql = @"dcs_p_cerrar_excepcion";

                    if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                    {
                        DocSys.connOracle.Open();
                    }

                    OracleParameter pa_codigo_Excepcion = new OracleParameter("p_codigo_excepcion", OracleType.Number);
                    pa_codigo_Excepcion.Direction = ParameterDirection.Input;
                    pa_codigo_Excepcion.Value = vl_cod_excepcion;

                    OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(pa_codigo_Excepcion);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    this.p_refrescar_info();
                    MessageBox.Show("Excepción Cerrada con éxito");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void agregarExcepciónManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dgvExcepciones.RowCount > 0)
            {
                DataGridViewRow row = this.dgvExcepciones.CurrentRow;
                int vl_cod_excepcion = int.Parse(row.Cells["cod_excepcion_sol"].Value.ToString());
                bool respuesta = this.da.ExcepcionManual(vl_cod_excepcion);
                MessageBox.Show("Excepción manual agregada con éxito");
            }
        }

        private void timerSemaforo_Tick(object sender, EventArgs e)
        {
            string instrucciones = string.Empty;
            if (global_estacion_id_to == 1000)
            {
                if (!global_gerente_filial)
                {
                    instrucciones = " and oficial_servicios = '" + DocSys.vl_user + "' ";
                }
            }

            DataTable semaforo = this.da.ExcepcionesSemaforo(global_estacion_id_to, instrucciones);
            if (semaforo.Rows.Count == 3)
            {
                this.lblVerde.Text = semaforo.Rows[0]["total"].ToString();
                this.lblAmarillo.Text = semaforo.Rows[1]["total"].ToString();
                this.lblRojo.Text = semaforo.Rows[2]["total"].ToString();
            }
        }

        private void dgvExcepciones_VisibleChanged(object sender, EventArgs e)
        {
            if (this.dgvExcepciones.Visible)
            {
                this.pnlSemaforo.Visible = true;
            }
            else
            {
                this.pnlSemaforo.Visible = false;
            }
        }

        private void linkMensajes_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            s_mensajes_detalle verMensajes = new s_mensajes_detalle(this.da);
            verMensajes.ShowDialog();
        }

        private void preanálisisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (global_estacion_id_to == 1000 | global_estacion_id_to == 1001 | global_estacion_id_to == 1002)
            {
                s_preanalisis form = new s_preanalisis(this.da, lblSuFilial.Text);
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Esta opción no está disponible para su estación");
            }
        }

        private void ActualizarNoLeidos()
        {
            CheckForIllegalCrossThreadCalls = false;
            try
            {
                int totalAntes = Convert.ToInt32(this.lblMensajesNuevos.Text);
                int totalDespues = this.da.TotalMensajesNuevos(DocSys.vl_user);
                this.lblMensajesNuevos.Text = totalDespues.ToString();

                if (totalAntes < totalDespues)
                {

                    Notificacion.show_Toast(5000, "Tiene una nuevas notificaciónes");
                }

            }
            catch (Exception)
            {
                this.lblMensajesNuevos.Text = "0";
            }
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            this.ActualizarNoLeidos();
        }

        private void buscarExcepcionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            e_buscar_excepcion forma = new e_buscar_excepcion(this.da);
            forma.ShowDialog();
        }

        private void lblVerde_Click(object sender, EventArgs e)
        {
            try
            {
                string instrucciones = string.Empty;
                if (global_estacion_id_to == 1000)
                {
                    if (!global_gerente_filial)
                    {
                        instrucciones = " and oficial_servicios = '" + DocSys.vl_user + "' ";
                    }
                }

                var excepcionesVerdes = this.da.ExcepcionesSemaforo(global_estacion_id_to, TipoSemaforo.Verde, instrucciones);
                e_excepciones_semaforo forma = new e_excepciones_semaforo(excepcionesVerdes, TipoSemaforo.Verde);
                forma.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lblAmarillo_Click(object sender, EventArgs e)
        {
            try
            {
                string instrucciones = string.Empty;
                if (global_estacion_id_to == 1000)
                {
                    if (!global_gerente_filial)
                    {
                        instrucciones = " and oficial_servicios = '" + DocSys.vl_user + "' ";
                    }
                }

                var excepcionesVerdes = this.da.ExcepcionesSemaforo(global_estacion_id_to, TipoSemaforo.Amarillo, instrucciones);
                e_excepciones_semaforo forma = new e_excepciones_semaforo(excepcionesVerdes, TipoSemaforo.Amarillo);
                forma.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lblRojo_Click(object sender, EventArgs e)
        {
            try
            {
                string instrucciones = string.Empty;
                if (global_estacion_id_to == 1000)
                {
                    if (!global_gerente_filial)
                    {
                        instrucciones = " and oficial_servicios = '" + DocSys.vl_user + "' ";
                    }
                }

                var excepcionesVerdes = this.da.ExcepcionesSemaforo(global_estacion_id_to, TipoSemaforo.Rojo, instrucciones);
                e_excepciones_semaforo forma = new e_excepciones_semaforo(excepcionesVerdes, TipoSemaforo.Rojo);
                forma.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void timerFaby_Tick(object sender, EventArgs e)
        {
            this.ActualizarNoLeidos();
        }

        private void reportesMensualesSolicitudesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RptMensualesSolicitudes forma = new RptMensualesSolicitudes(this.da);
            forma.ShowDialog();
        }

        private void cbo_excp_estacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.p_popular_lista_entradas(string.Empty, 1);
        }

        //LLenado de las excepciones por zonas cuando se cambia el comnbobox, cambio 2021
        private void cmbZonaExcepcion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            global_es_admon_temp = this.da.EsAdminTemporal(DocSys.vl_user);
            int zon = Convert.ToInt32(cmbZonaExcepcion.SelectedIndex);
            int global_estacion_id_to = Convert.ToInt32(comboBoxEstacionUsuario.SelectedValue);
            
            if (DocSys.vl_nodo_treview == u_Globales.MONTO1)
            {
                this.dgvExcepciones.DataSource = DocSys.p_cargar_excepciones(global_estacion_id_to, "", false, " ", "", 0, (int)Estaciones.EstacionValidacion, u_Globales.FiltroMonto1, DocSys.vl_agencia_usuario, zon);
            }
            else if (DocSys.vl_nodo_treview == u_Globales.MONTO2)
            {
                this.dgvExcepciones.DataSource = DocSys.p_cargar_excepciones(global_estacion_id_to, "", false, " ", "", 0, (int)Estaciones.EstacionValidacion, u_Globales.FiltroMonto2, DocSys.vl_agencia_usuario, zon);
            }
            else if (DocSys.vl_nodo_treview == u_Globales.MONTO3)
            {
                this.dgvExcepciones.DataSource = DocSys.p_cargar_excepciones(global_estacion_id_to, "", false, " ", "", 0, (int)Estaciones.EstacionValidacion, u_Globales.FiltroMonto3, DocSys.vl_agencia_usuario, zon);
            }
        }

        //private void personaNaturalToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    tipo_sol = "N";
        //    Button_nueva_solicitud_Click(null, null);
        //}

        //private void personaJuridícaToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (global_estacion_id_to == 0)
        //    {
        //        MessageBox.Show("El usuario no tiene definida una estación de trabajo..!", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return;
        //    }
        //    if (DocSys.p_get_permitir_crear_solicitud(global_estacion_id_to) == "N")
        //    {
        //        MessageBox.Show("La estacion " + DocSys.p_get_nombre_estacion(global_estacion_id_to) + " no puede crear solicitudes ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //        return;
        //    }

        //    btnNuevaSolic_Click(null, null);
        //}
    }
}