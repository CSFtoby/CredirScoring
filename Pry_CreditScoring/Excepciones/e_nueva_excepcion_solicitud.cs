using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wfcModel;
using System.Dynamic;
using System.Globalization;
using Docsis_Application.FrmsRpts;

namespace Docsis_Application.Excepciones
{
    public partial class e_nueva_excepcion_solicitud : Form
    {
        DataAccess da;
        //int vl_workflow_id = 0;
        //int vl_record_grid = 0;
        bool con_borde = MDI_Menu.con_borde;
        private int NoSolicitud;
        private List<Lineamientos> lineamientos = new List<Lineamientos>();
        private List<Detalle_Excepcion> detalle_cargada = new List<Detalle_Excepcion>();
        private static List<ReglasExcepciones> reglas_excepciones = new List<ReglasExcepciones>();
        private generalesExcepcion infoExcep = new generalesExcepcion();
        private string Accion;
        private string v_CodigoExcepcion = string.Empty;
        private string v_CodLineamiento = string.Empty;
        private int v_CodigoExcepcionSol;
        public int noFilial { get; set; }
        const string vacio = "____________________";
        public static bool excepcion_editable = false;
        private const string cargada = "C";
        private const string modificada = "MODIF"; //cambio
        private const string eliminada = "ELIM"; //cambio
        private const string agregada = "A";
        private int Estacion_actual { get; set; }
        private string CheckBoxLinSelect = string.Empty;
        private bool editarExcepcion = false;
        private bool ResolucionPendiente = false;
        private bool PrestacionesOk = true;
        private bool ExcepPrestacion = false;

        #region Diseño
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
        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }

        #endregion

        public e_nueva_excepcion_solicitud(int _noSolicitud, string _accion, DataAccess _da, int _codExcepcion, string usuario_estacion, int estacion_actual, string estado = "")
        {
            InitializeComponent();
            this.NoSolicitud = _noSolicitud;
            this.v_CodigoExcepcionSol = _codExcepcion;
            this.Accion = _accion;
            this.da = _da;
            this.Estacion_actual = estacion_actual;
            this.txtNoSolicitud.Text = this.NoSolicitud.ToString();
            //this.Panel_General();
            //this.Panel_Excepciones();
            //this.Panel_Recomendaciones();
            this.cargar_lineamientos();
            this.cargarReglas();
            this.txtCondicionCS.Enabled = false;
            this.txtIngresoNeto.Enabled = false;
            this.txtIngresoNetoDes.Enabled = false;
            this.txtSueldoBruto.Enabled = false;
            this.lblRegla.Text = "Advertencias:\n";
            this.lblRegla.Visible = false;
            this.btnPrestaciones.Visible = false;

            this.cbxMiembro1.Text = vacio;
            this.cbxMiembro2.Text = vacio;
            this.cbxMiembro3.Text = vacio;

            if (this.Accion.Equals(u_Globales.accionModificar))
            {
                this.p_cargar_info_Excepcion(true);
                this.cmbPagoMediante.Enabled = false;
                this.cmbExcepciones.Enabled = false;
                this.txtObservacionesExce.Enabled = false;
                this.txtDetalleDeudas.Enabled = true;
                this.txtRecFilial.Enabled = true;
                this.btnGuardar_solicitud.Visible = false;
                //En caso de que el estado sea devuelto, lo anterior se habilita
                this.cargar_aprobaciones();
                this.estado_excepciones();
                this.cargar_grid_Detalle_excep(this.v_CodigoExcepcionSol);

                if (this.Estacion_actual != (int)Estaciones.Afiliacion)
                {
                    //Evaluar si es nivel resolutivo
                    var resolutivo = this.da.EsNivelResolExcep(this.v_CodigoExcepcionSol, this.Estacion_actual, DocSys.vl_user);

                    //Si es nivel resolutivo:
                    if (resolutivo.Rows[1]["total"].ToString().Equals("1"))
                    {
                        //Si tiene aprobaciones
                        if (resolutivo.Rows[0]["total"].ToString().Equals("1"))
                        {
                            this.ResolucionPendiente = true;
                            //Habilita las opciones de resolución
                            this.rbnAprobar.Enabled = true;
                            this.rbnDenegar.Enabled = true;
                            this.rbnDevolver.Enabled = true;
                        }
                        else
                        {
                            this.ResolucionPendiente = false;
                            this.rbnAprobar.Enabled = false;
                            this.rbnDenegar.Enabled = false;
                            this.rbnDevolver.Enabled = false;
                        }
                    }
                    else
                    {
                        this.ResolucionPendiente = false;
                        this.rbnAprobar.Enabled = false;
                        this.rbnDenegar.Enabled = false;
                        this.rbnDevolver.Enabled = false;
                    }
                }
                else
                {
                    this.ResolucionPendiente = false;
                    this.panel2.Enabled = false;
                    this.txtDetalleDeudas.Enabled = true;
                    this.txtRecFilial.Enabled = true;
                }
            }
            else if (this.Accion.Equals(u_Globales.accionAgregar))
            {
                this.p_cargar_info_Excepcion(false);
                this.lblUsuarioCreacion.Text = usuario_estacion;
                this.ObtenerNombreUsuario(usuario_estacion);
                this.ResolucionPendiente = false;
                this.panel2.Enabled = false;
                this.btnActualizarInfo.Enabled = false;
                this.txtFechaSolicitud.Text = DateTime.Today.ToString();
            }

            this.txtRecNivResl.Enabled = false;

            if (this.Estacion_actual == (int)Estaciones.NivelResolutivoFilial &
                (this.rbnAprobar.Checked == false & this.rbnDenegar.Checked == false))
            {
                this.txtGerente.Enabled = true;
            }
            else
            {
                this.txtGerente.Enabled = false;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            this.moverForm();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            bool bandera = true;
            if (this.Estacion_actual == (int)Estaciones.NivelResolutivoFilial)
            {
                if (this.rbnAprobar.Checked == false & this.rbnDenegar.Checked == false
                    & this.rbnDevolver.Checked == false & string.IsNullOrEmpty(this.txtGerente.Text))
                {
                    bandera = false;
                }
            }

            if (!bandera & this.Estacion_actual == (int)Estaciones.NivelResolutivoFilial)
            {
                MessageBox.Show("Debe ingresar un comentario de gerente antes de enviar la exepción");
                return;
            }

            this.Close();
        }

        private void btnDatosAfiliado_Click(object sender, EventArgs e)
        {
            this.Panel_General(true);
            this.Panel_Excepciones(false);
            this.Panel_Recomendaciones(false);
        }

        #region Manejo paneles
        private void Panel_General(bool mostrar = true)
        {
            this.pnlDatosSolicitante.Visible = mostrar;
            this.pnlDatosSolicitante.Size = new Size(935, 460);
            this.pnlDatosSolicitante.Location = new Point(218, 79);
            //this.lblTituloPanel.Text = "Generales";
        }

        private void Panel_Excepciones(bool mostrar = false)
        {
            this.pnlExcepciones.Visible = mostrar;
            this.pnlExcepciones.Size = new Size(935, 460);
            this.pnlExcepciones.Location = new Point(218, 79);
            //this.lblTituloPanel.Text = "Excepciones";
        }

        private void Panel_Recomendaciones(bool mostrar = false)
        {
            this.pnlRecomendacion.Visible = mostrar;
            this.pnlRecomendacion.Size = new Size(935, 460);
            this.pnlRecomendacion.Location = new Point(218, 79);
            //this.lblTituloPanel.Text = "Recomendaciones";
        }
        #endregion

        private void btnDatosCredito_Click(object sender, EventArgs e)
        {
            this.Panel_General(false);
            this.Panel_Excepciones(true);
            this.Panel_Recomendaciones(false);
        }

        void Cambio_Radio_CheckedChanged(object regla, EventArgs e)
        {
            try
            {
                RadioButton rbtnSelec = (RadioButton)regla;
                if (!this.v_CodLineamiento.Equals(rbtnSelec.Name) | this.v_CodLineamiento == string.Empty)
                {
                    this.v_CodLineamiento = rbtnSelec.Name;
                    this.llenar_ComboExcepciones(v_CodLineamiento);
                    this.CheckBoxLinSelect = rbtnSelec.Text;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en filtro excepciones: " + ex.Message);
            }
        }

        private void e_nueva_excepcion_solicitud_Load(object sender, EventArgs e)
        {
            this.labelOficialDeServicio.Text = DocSys.vl_user;
            //this.labelFilial.Text = DocSys.vl_agencia_usuario;
            labelRelojPanel.Text = DateTime.Now.ToString("hh:mm");
            labelDiaPanel.Text = DateTime.Now.ToString("dddd");
            labelFechaPanel.Text = DateTime.Now.ToString("MMMM") + " " + DateTime.Now.ToString("dd");

            if (this.noFilial == u_Globales.eAfiliacion)
            {
                this.txtRecNivResl.Enabled = false;
            }
        }

        private void cmbExcepciones_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.lblTipoExcepcion.Text = $"Excepción: ({this.cmbExcepciones.SelectedValue.ToString()})";
            this.v_CodigoExcepcion = this.cmbExcepciones.SelectedValue.ToString();
            this.lblError.Text = string.Empty;
            this.lblRegla.Text = string.Empty;
            //Cambios 20190528
            if (this.v_CodigoExcepcion.Equals("6-OE-7"))
            {
                this.txtNombreExcepcion.Visible = true;
                this.label25.Visible = true;
            }
            else
            {
                this.txtNombreExcepcion.Visible = false;
                this.label25.Visible = false;
            }
            //this.evaluar_reglas();
        }

        private void btnAgregarExc_Click(object sender, EventArgs e)
        {
            bool procede = this.advertencia_agregar();

            if (procede)
            {
                this.lblError.Visible = procede;
            }
            else
            {
                if (this.v_CodigoExcepcion.Equals("6-OE-7"))
                {
                    this.txtObservacionesExce.Text = this.txtNombreExcepcion.Text + "*" + this.txtObservacionesExce.Text;
                }

                if (this.v_CodigoExcepcion.Equals(u_Globales.ExcepcionPrestaciones))
                {
                    this.ExcepPrestacion = true;
                }

                this.lblError.Visible = procede;
                this.dgvExcepciones.Rows.Add(
                this.v_CodLineamiento,
                this.CheckBoxLinSelect,
                this.v_CodigoExcepcion,
                this.cmbExcepciones.Text,
                this.txtObservacionesExce.Text);

                if (editarExcepcion)
                {
                    detalle_cargada.Add(new Detalle_Excepcion
                    {
                        Tc_CodigoExcepcion = this.v_CodigoExcepcionSol,
                        Tc_CodLineamiento = this.v_CodLineamiento,
                        Tc_CodTipoExcepcion = this.v_CodigoExcepcion,
                        Tc_Justificacion = this.txtObservacionesExce.Text,
                        Estado = agregada
                    });
                }
            }

            this.txtObservacionesExce.Text = string.Empty;
            this.txtNombreExcepcion.Text = string.Empty;

        }

        private void cmbExcepciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.evaluar_reglas();
        }

        #region Cargas
        private void p_cargar_info_Excepcion(bool existente)
        {
            int valor = (!existente) ? this.NoSolicitud : this.v_CodigoExcepcionSol;

            this.infoExcep = DocSys.p_obtener_datos_n_excepcion(valor, existente);
            this.txtNombre.Text = this.infoExcep.nombre_cliente;
            this.txtNumeroAfiliacion.Text = this.infoExcep.codigo_cliente.ToString();
            this.txtSaldoAportaciones.Text = this.infoExcep.saldo.ToString("0,0.00", CultureInfo.InvariantCulture);
            this.txtTipoPrestamo.Text = this.infoExcep.desc_sub_aplicacion;
            this.txtEmpresa.Text = this.infoExcep.lugar_de_trabajo;
            this.txtFechaIngreso.Text = this.infoExcep.fecha_ingreso_laboral.ToShortDateString();
            this.txtFilial.Text = this.da.ObtenerNombreAgencia(this.infoExcep.codigo_agencia_origen);
            this.txtMontoSolicitado.Text = this.infoExcep.monto_solicitado.ToString("0,0.00", CultureInfo.InvariantCulture);
            this.txtDestinoPrestamo.Text = this.infoExcep.descripcion_destino;
            this.txtPuesto.Text = this.infoExcep.cargo_que_ocupa;
            this.txtCondicionCS.Text = this.infoExcep.condicionCS;
            this.txtSueldoBruto.Text = this.infoExcep.sueldo_bruto.ToString("0,0.00", CultureInfo.InvariantCulture);
            this.txtEdad.Text = this.infoExcep.Edad.ToString();
            this.txtPlazoPrestamo.Text = this.infoExcep.Plazo.ToString();

            if (existente)
            {
                this.txtSolAnt.Enabled = false;
                this.txtFechaAnt.Enabled = false;
                this.txtResolucionAnt.Enabled = false;
                this.txtNuevaFecha.Enabled = false;
                this.txtFechaReconsideracion.Enabled = false;
                this.txtPuestoDesepmpleña.Enabled = false;

                if (infoExcep.pago_mediante.Equals("V"))
                {
                    this.cmbPagoMediante.SelectedIndex = 0;
                }
                else if (infoExcep.pago_mediante.Equals("P"))
                {
                    this.cmbPagoMediante.SelectedIndex = 1;
                }

                this.txtCondicionCS.Text = this.infoExcep.condicionCS;
                this.txtIngresoNeto.Text = this.infoExcep.ingresoNeto.ToString("0,0.00", CultureInfo.InvariantCulture);
                this.txtIngresoNetoDes.Text = this.infoExcep.ingresoNetoD.ToString("0,0.00", CultureInfo.InvariantCulture);
                this.txtPrestaciones.Text = this.infoExcep.Prestaciones.ToString("0,0.00", CultureInfo.InvariantCulture);
                
                this.txtPuestoDesepmpleña.Text = this.infoExcep.puesto_referenciado.ToString();
                if (this.txtPuestoDesepmpleña.Text == "")
                {
                    this.txtPuestoDesepmpleña.Text = "N/A";
                }

                this.txtSolAnt.Text = this.infoExcep.solicitud_anterior.ToString();
                if (this.txtSolAnt.Text == "")
                {
                    this.txtSolAnt.Text = "N/A";
                }

                this.txtFechaAnt.Text = this.infoExcep.fecha_anterior.ToString();
                if (this.txtFechaAnt.Text == "")
                {
                    this.txtFechaAnt.Text = "N/A";
                }

                this.txtResolucionAnt.Text = this.infoExcep.resolucion_anterior.ToString();
                if (this.txtResolucionAnt.Text == "")
                {
                    this.txtResolucionAnt.Text = "N/A";
                }

                this.txtNuevaFecha.Text = this.infoExcep.fehca_nueva_resolucion.ToString();
                if (this.txtNuevaFecha.Text == "")
                {
                    this.txtNuevaFecha.Text = "N/A";
                }

                this.txtFechaReconsideracion.Text = this.infoExcep.fecha_reconsideracion.ToString();
                if (this.txtFechaReconsideracion.Text == "")
                {
                    this.txtFechaReconsideracion.Text = "N/A";
                }
            }
            else
            {
                this.txtSolAnt.Enabled = true;
                this.txtFechaAnt.Enabled = true;
                this.txtResolucionAnt.Enabled = true;
                this.txtNuevaFecha.Enabled = true;
                this.txtFechaReconsideracion.Enabled = true;
                this.txtPuestoDesepmpleña.Enabled = true;
                //info del reporte
                string xml = this.da.ObtenerXmlRespuestaEvaluacion(this.NoSolicitud);
                u_Globales.CondicionTU = string.Empty;
                u_Globales.IngresoNeto = 0;
                u_Globales.IngresoNetoD = 0;
                u_Globales.obtener_info_reporte_TU(xml);
                this.txtCondicionCS.Text = u_Globales.CondicionTU;
                this.txtIngresoNeto.Text = u_Globales.IngresoNeto.ToString("0,0.00", CultureInfo.InvariantCulture);
                this.txtIngresoNetoDes.Text = (Math.Truncate(u_Globales.IngresoNetoD * 100) / 100).ToString("0,0.00", CultureInfo.InvariantCulture);
            }
        }

        private void llenar_ComboExcepciones(string _codLineamiento)
        {
            this.Cursor = Cursors.WaitCursor;
            //this.cmbExcepciones.Items.Clear();
            try
            {
                string sql = @"select cod_tipo_excepcion, tipo_excepcion from excp.dcs_exc_tipo_excepciones 
                               where codigo_lineamiento=:codLineamiento and estado='S'";
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }

                OracleParameter pa_cod_linea = new OracleParameter("codLineamiento", OracleType.VarChar);
                pa_cod_linea.Direction = ParameterDirection.Input;
                pa_cod_linea.Value = _codLineamiento;

                OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(pa_cod_linea);

                OracleDataAdapter adaptador = new OracleDataAdapter(cmd);
                DataSet registros = new DataSet();

                adaptador.Fill(registros, "dcs_exc_tipo_excepciones");

                this.cmbExcepciones.DataSource = registros;
                this.cmbExcepciones.DisplayMember = "dcs_exc_tipo_excepciones.tipo_excepcion";
                this.cmbExcepciones.ValueMember = "dcs_exc_tipo_excepciones.cod_tipo_excepcion";
                this.cmbExcepciones_SelectionChangeCommitted(null, null);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las excepciones.\n" + ex.Message);
            }
            this.Cursor = Cursors.Default;
        }

        private void cargar_lineamientos()
        {
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }

                string sql = @"select codigo_lineamiento,nombre from excp.DCS_EXC_LINEAMIENTOS where estado = 'S' ";

                OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    this.lineamientos.Add(new Lineamientos
                    {
                        CodigoLineamiento = dr["codigo_lineamiento"].ToString(),
                        Lineamiento = dr["nombre"].ToString()
                    });
                }

                dr.Close();

                int posicionY = 20;

                foreach (var item in this.lineamientos)
                {
                    RadioButton newRadio = new RadioButton()
                    {
                        Text = item.Lineamiento,
                        Name = item.CodigoLineamiento,
                        ForeColor = Color.Black,
                        Location = new Point(18, posicionY),
                        Width = 200
                    };
                    newRadio.CheckedChanged += new EventHandler(Cambio_Radio_CheckedChanged);
                    this.pnlRadios.Controls.Add(newRadio);

                    posicionY += 25;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cargarReglas()
        {
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }

                string sql = @"select nvl(codigo_lineamiento,'') codigo_lineamiento,nvl(cod_tipo_excepcion,'') cod_tipo_excepcion, 
                               nvl(cod_tipo_excep_prohibida,'') cod_tipo_excep_prohibida, nvl(cod_linea_prohibida,'') cod_linea_prohibida, 
                               nvl(observaciones,'') observaciones
                               from excp.dcs_exc_reglas_excepciones
                               where estado = 'S' ";

                OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ReglasExcepciones rule = new ReglasExcepciones();
                    rule.codigo_lineamiento = dr["codigo_lineamiento"].ToString();
                    rule.cod_linea_prohibida = dr["cod_linea_prohibida"].ToString();
                    rule.cod_tipo_excepcion = dr["cod_tipo_excepcion"].ToString();
                    rule.cod_tipo_excep_prohibida = dr["cod_tipo_excep_prohibida"].ToString();
                    rule.observaciones = dr["observaciones"].ToString();

                    reglas_excepciones.Add(rule);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Trae las condiciones para aplicar la excepción seleccionada
        /// </summary>
        private void evaluar_reglas()
        {
            this.lblRegla.Text = string.Empty;

            //universos condicionales
            List<ReglasExcepciones> rLinea = new List<ReglasExcepciones>();
            List<ReglasExcepciones> rExcep = new List<ReglasExcepciones>();
            //pequeños universos
            List<ReglasExcepciones> rLinExcp = new List<ReglasExcepciones>();
            List<ReglasExcepciones> rExcpLin = new List<ReglasExcepciones>();
            //int cuenta = this.dgvExcepciones.RowCount;

            //reglas lineamiento - excepción prohibida
            rLinExcp = reglas_excepciones.Where(c => c.codigo_lineamiento.Equals(this.v_CodLineamiento)
                                                                         && c.cod_tipo_excep_prohibida.Equals(this.v_CodigoExcepcion)).ToList();

            //reglas excepción lineamiento prohibido
            rExcpLin = reglas_excepciones.Where(c => c.cod_tipo_excepcion.Equals(this.v_CodigoExcepcion)
                                                                         && c.cod_linea_prohibida.Equals(this.v_CodLineamiento)).ToList();

            //reglas lineamiento
            rLinea = reglas_excepciones.Where(c => c.codigo_lineamiento.Equals(this.v_CodLineamiento)).ToList()
                                                           .Except(rLinExcp).ToList();

            //reglas excepción
            rExcep = reglas_excepciones.Where(c => cod_tipo_excepcion.Equals(this.v_CodigoExcepcion)).ToList()
                                                           .Except(rExcep).ToList();
            int contador = 1;
            this.lblRegla.Text = "Advertencias:\n";
            //reglas lineamiento - excepción prohibida
            foreach (var item in rLinExcp)
            {
                if (item.observaciones.Equals(string.Empty))
                {
                    this.lblRegla.Text += $"{contador}.No se puede combinar {item.codigo_lineamiento} con {item.cod_tipo_excep_prohibida}\n";
                }
                else
                {
                    this.lblRegla.Text += $"{contador}.{item.observaciones}\n";
                }

                contador++;
            }

            //reglas excepción lineamiento prohibido
            foreach (var item in rExcpLin)
            {
                if (item.observaciones.Equals(string.Empty))
                {
                    this.lblRegla.Text += $"{contador}.No se puede combinar {item.codigo_lineamiento} con {item.cod_tipo_excep_prohibida}\n";
                }
                else
                {
                    this.lblRegla.Text += $"{contador}.{item.observaciones}\n";
                }

                contador++;
            }

            //reglas lineamiento
            foreach (var item in rLinea)
            {
                if (item.observaciones.Equals(string.Empty))
                {
                    this.lblRegla.Text += $"{contador}.No se puede combinar {item.codigo_lineamiento} con {item.cod_tipo_excep_prohibida}\n";
                }
                else
                {
                    this.lblRegla.Text += $"{contador}.{item.observaciones}\n";
                }

                contador++;
            }

            //reglas excepción
            foreach (var item in rExcep)
            {
                if (item.observaciones.Equals(string.Empty))
                {
                    this.lblRegla.Text += $"{contador}.No se puede combinar {item.codigo_lineamiento} con {item.cod_tipo_excep_prohibida}\n";
                }
                else
                {
                    this.lblRegla.Text += $"{contador}.{item.observaciones}\n";
                }

                contador++;
            }
        }

        private bool advertencia_agregar()
        {
            bool detener = false;
            string mensaje = string.Empty;
            return detener;
        }

        private void cargar_grid_Detalle_excep(int codExcepcion)
        {
            DataTable ddt = this.da.get_detalle_info(codExcepcion);
            //this.dgvExcepciones.DataSource = ddt;

            detalle_cargada.Clear();
            foreach (DataRow item in ddt.Rows)
            {
                this.dgvExcepciones.Rows.Add(
                item["cod_lineamiento"].ToString(),
                item["lineamiento"].ToString(),
                item["cod_tipo_excepcion"].ToString(),
                item["tipo_excepcion"].ToString(),
                item["observaciones"].ToString());

                if (item["cod_tipo_excepcion"].ToString() == u_Globales.ExcepcionPrestaciones)
                {
                    this.ExcepPrestacion = true;
                }

                if (editarExcepcion)
                {
                    Detalle_Excepcion nuevo = new Detalle_Excepcion
                    {
                        Tc_CodigoExcepcion = this.v_CodigoExcepcionSol,
                        Estado = cargada,
                        Tc_CodLineamiento = item["cod_lineamiento"].ToString(),
                        Tc_CodTipoExcepcion = item["cod_tipo_excepcion"].ToString(),
                        Tc_Justificacion = item["observaciones"].ToString()
                    };

                    detalle_cargada.Add(nuevo);
                }
            }

            DataTable grales = new DataTable();
            grales = this.da.get_info_excep_gral(codExcepcion);
            this.txtRecFilial.Text = grales.Rows[0]["recomendacion_filial"].ToString();
            this.txtRecFilial.Enabled = false;
            this.txtDetalleDeudas.Text = grales.Rows[0]["detalle_deudas_consol"].ToString();
            this.txtDetalleDeudas.Enabled = false;
            this.lblUsuarioCreacion.Text = grales.Rows[0]["usuario_ing_excepcion"].ToString();
            this.ObtenerNombreUsuario(grales.Rows[0]["usuario_ing_excepcion"].ToString());
            this.txtGerente.Text = grales.Rows[0]["RECOMENDACION_NIV_RES"].ToString();
            this.txtFechaSolicitud.Text = grales.Rows[0]["fecha_presentacion"].ToString();

            this.cmbPagoMediante.SelectedIndex = (grales.Rows[0]["pago_mediante"].ToString().Equals("V")) ? 0 : 1;
        }

        private void ObtenerNombreUsuario(string _usuario)
        {
            try
            {
                string nombre = this.da.ObtenerNombreUsuario(_usuario);

                this.lblUsuarioCreacion.Text += " | " + nombre.ToTitleCase();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error al cargar el nombre del usuario {ex.Message}", "Error");
            }
        }

        private void estado_excepciones()
        {
            try
            {
                DataTable estado = DocSys.Obtener_estado_excepcion(this.v_CodigoExcepcionSol);

                int codEstado = Convert.ToInt32(estado.Rows[0]["ESTADO_EXCEP_ID"].ToString());
                string estadoD = estado.Rows[0]["DESCRIPCION"].ToString();

                switch (codEstado)
                {
                    case u_Globales.Aprobado:
                        this.rbnAprobar.Checked = true;
                        break;
                    case u_Globales.Rechazada:
                        this.rbnDenegar.Checked = true;
                        break;
                    case u_Globales.Anulada:
                        this.rbnDevolver.Checked = true;
                        break;
                    default:
                        {
                            this.cmbPagoMediante.Enabled = true;
                            this.cmbExcepciones.Enabled = true;
                            this.txtObservacionesExce.Enabled = true;
                            this.txtDetalleDeudas.Enabled = true;
                            this.txtRecFilial.Enabled = true;
                            this.btnGuardar_solicitud.Visible = true;
                            editarExcepcion = true;
                            this.btnActualizarInfo.Enabled = true;
                            this.rbnAprobar.Checked = false;
                            this.rbnDenegar.Checked = false;
                            this.rbnDevolver.Checked = false;
                            if (this.Estacion_actual != (int)Estaciones.Afiliacion)
                            {
                                this.btnAgregarExc.Enabled = false;
                                this.btnEditar.Enabled = false;
                                this.btnQuitar.Enabled = false;
                            }
                            else
                            {
                                this.btnEditar.Enabled = true;
                                this.btnAgregarExc.Enabled = true;
                                this.btnQuitar.Enabled = true;
                            }
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cargar_aprobaciones()
        {
            try
            {
                DataTable dtApro = this.da.get_aprobaciones_solicitud(this.v_CodigoExcepcionSol);

                int conteo = 1;
                int total_aprobaciones = dtApro.Rows.Count;
                int total_respondidas = 0;
                List<int> decisiones = new List<int>();

                foreach (DataRow item in dtApro.Rows)
                {
                    string pendienteR = item["pendiente_respuesta_b"].ToString();
                    string usuarioC = item["usuario_comite"].ToString();
                    string nombreUsuario = string.Empty;

                    //cuenta las aprobaciones ya respondidas
                    if (pendienteR.Equals(u_Globales.Negativo))
                    {
                        total_respondidas++;
                        decisiones.Add(Convert.ToInt32(item["decision_id"].ToString()));
                    }

                    if (!string.IsNullOrEmpty(usuarioC))
                    {
                        nombreUsuario = this.da.ObtenerNombreUsuario(usuarioC);
                    }
                    else
                    {
                        nombreUsuario = vacio;
                    }

                    switch (conteo)
                    {
                        case 1:
                            this.cbxMiembro1.Checked = (!pendienteR.Equals(u_Globales.Afirmativo)) ? true : false;
                            this.cbxMiembro1.Text = nombreUsuario;
                            break;
                        case 2:
                            this.cbxMiembro2.Checked = (!pendienteR.Equals(u_Globales.Afirmativo)) ? true : false;
                            this.cbxMiembro2.Text = nombreUsuario;
                            break;
                        case 3:
                            this.cbxMiembro3.Checked = (!pendienteR.Equals(u_Globales.Afirmativo)) ? true : false;
                            this.cbxMiembro3.Text = nombreUsuario;
                            break;
                        default:
                            break;
                    }

                    conteo++;
                    this.txtRecNivResl.Multiline = true;
                    this.txtRecNivResl.Text += item["observaciones"].ToString() + " \r\n";
                }

                if (total_aprobaciones == total_respondidas)
                {
                    int modif_Excep = 0;
                    int rechazarD = 0;
                    int aprobarD = 0;

                    foreach (int decisionID in decisiones)
                    {
                        DataTable tmp = this.da.get_resolucion_excepcion(decisionID);

                        if (tmp.Rows[0]["aprobar_excep"].ToString() == u_Globales.Afirmativo)
                        {
                            aprobarD++;
                        }

                        if (tmp.Rows[0]["denegar_excep"].ToString() == u_Globales.Afirmativo)
                        {
                            rechazarD++;
                        }

                        if (tmp.Rows[0]["modif_excep"].ToString() == u_Globales.Afirmativo)
                        {
                            modif_Excep++;
                        }
                    }

                    if (modif_Excep > 0 & this.Estacion_actual == u_Globales.eAfiliacion)
                    {

                        //si la excepción se movió, ya no se edita

                        int estacion_act_exc = this.da.get_estacion_actual_excep(this.v_CodigoExcepcionSol);

                        if (estacion_act_exc == this.Estacion_actual)
                        {
                            this.rbnDevolver.Checked = true;
                            this.cmbExcepciones.Enabled = true;
                            this.cmbPagoMediante.Enabled = true;
                            this.txtRecFilial.Enabled = true;
                            this.txtDetalleDeudas.Enabled = true;
                            this.btnGuardar_solicitud.Enabled = true;
                            this.btnGuardar_solicitud.Visible = true;
                            this.btnImprimir.Visible = false;
                            this.txtObservacionesExce.Enabled = true;
                            excepcion_editable = true;
                            this.btnQuitar.Visible = true;
                            this.btnAgregarExc.Enabled = true;
                            this.btnEditar.Enabled = true;
                            this.btnQuitar.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("La excepción ya no puede ser editada, ya que se movió de estación");
                        }
                    }

                    if (aprobarD == total_aprobaciones)
                    {
                        this.rbnAprobar.Checked = true;
                    }

                    if (rechazarD > 0)
                    {
                        this.rbnDenegar.Checked = true;
                    }

                    if (modif_Excep > 0)
                    {
                        this.rbnDevolver.Checked = true;
                    }
                    else
                    {
                        this.rbnAprobar.Checked = false;
                        this.rbnDenegar.Checked = false;
                        this.rbnDevolver.Checked = false;
                    }

                    //obtiene la fecha de la resolución
                    DataTable resolucion = DocSys.get_datos_resolucion(this.v_CodigoExcepcionSol);
                    this.lblFechaResolucion.Text += " " + resolucion.Rows[0]["fecha_resolucion"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error {ex.Message}", "Error");
            }

        }

        #endregion

        private void btnRecomendaciones_Click(object sender, EventArgs e)
        {
            this.Panel_General(false);
            this.Panel_Excepciones(false);
            this.Panel_Recomendaciones(true);
        }

        private void btnGuardar_solicitud_Click(object sender, EventArgs e)
        {
            bool guardar = true;
            this.txtPrestaciones_Leave(null, null);

            if (this.PrestacionesOk)
            {
                if (this.Accion.Equals(u_Globales.accionAgregar))
                {
                    if (this.cmbPagoMediante.SelectedIndex == -1)
                    {
                        MessageBox.Show("Debe seleccionar el pago mediante");
                        guardar = false;
                        return;
                    }

                    if (this.dgvExcepciones.Rows.Count == 0)
                    {
                        MessageBox.Show("Debe seleccionar al menos una excepción");
                        guardar = false;
                        return;
                    }

                    if (this.txtDetalleDeudas.Text.Equals(string.Empty))
                    {
                        MessageBox.Show("Debe llenar información en el detalle de deudas");
                        guardar = false;
                        return;
                    }

                    if (this.txtRecFilial.Text.Equals(string.Empty))
                    {
                        MessageBox.Show("Debe llena la recomendación a nivel resolutivo filial");
                        guardar = false;
                        return;
                    }

                    if (string.IsNullOrEmpty(this.txtPrestaciones.Text) & this.btnPrestaciones.Visible)
                    {
                        MessageBox.Show("Debe llenar las prestaciones del afiliado.");
                        guardar = false;
                        return;
                    }

                    if (string.IsNullOrEmpty(this.txtGerente.Text.Trim()) & this.Estacion_actual == (int)Estaciones.NivelResolutivoFilial)
                    {
                        MessageBox.Show("Debe agregar el comentario de gerente de filial.");
                        guardar = false;
                        return;
                    }

                    if (guardar)
                        this.p_crear_excepcion();
                    else
                        return;
                }
                else if (this.Accion.Equals(u_Globales.accionModificar))
                {
                    if (this.Estacion_actual == (int)Estaciones.NivelResolutivoFilial)
                    {
                        if (this.rbnAprobar.Checked == false & this.rbnDenegar.Checked == false
                            & this.rbnDevolver.Checked == false & string.IsNullOrEmpty(this.txtGerente.Text))
                        {
                            MessageBox.Show("Debe ingresar un comentario de gerente antes de enviar la exepción");
                            return;
                        }
                    }
                    this.p_actualizar_excepcion();
                }

                this.da.ExcepcionCreada(this.NoSolicitud);

                if (this.v_CodigoExcepcionSol != 0)
                {
                    var result = MessageBox.Show("¿Desea adjuntar documentos antes de continuar?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        e_documento_excep forma = new e_documento_excep(this.v_CodigoExcepcionSol, this.da);
                        result = forma.ShowDialog();
                    }
                }
            }
            else
            {
                MessageBox.Show("El monto solicitado es mayor a las prestaciones. No se puede continuar");
                return;
            }

        }

        /// <summary>
        /// Guarda una nueva excepción
        /// </summary>
        private void p_crear_excepcion()
        {
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }

                string mensaje = string.Empty;
                string sql = "EXCP.DCS_P_GUARDAR_EXCEPCION";
                OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
                cmd.CommandType = CommandType.StoredProcedure;

                //───────────────────PA_CODIGO_EXCEPCION
                OracleParameter pa_codigo_excepcion = new OracleParameter("PA_CODIGO_EXCEPCION", OracleType.Number);
                pa_codigo_excepcion.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pa_codigo_excepcion);

                //───────────────────PA_NO_SOLICITUD
                OracleParameter pa_no_solicitud = new OracleParameter("PA_NO_SOLICITUD", OracleType.Number);
                pa_no_solicitud.Direction = ParameterDirection.Input;
                pa_no_solicitud.Value = this.NoSolicitud;
                cmd.Parameters.Add(pa_no_solicitud);

                //───────────────────PA_PAGO_MEDIANTE
                OracleParameter pa_pago_mediante = new OracleParameter("PA_PAGO_MEDIANTE", OracleType.VarChar);
                pa_pago_mediante.Direction = ParameterDirection.Input;
                pa_pago_mediante.Value = this.Pago_Mediante_Seleccion();
                cmd.Parameters.Add(pa_pago_mediante);

                //───────────────────PA_RECOMENDACION_FILIAL
                OracleParameter pa_recomendacion_filial = new OracleParameter("PA_RECOMENDACION_FILIAL", OracleType.VarChar);
                pa_recomendacion_filial.Direction = ParameterDirection.Input;
                pa_recomendacion_filial.Value = this.txtRecFilial.Text;
                cmd.Parameters.Add(pa_recomendacion_filial);

                //───────────────────PA_DETALLE_DEUDAS_CONSOL
                OracleParameter pa_detalle_deudas_consol = new OracleParameter("PA_DETALLE_DEUDAS_CONSOL", OracleType.VarChar);
                pa_detalle_deudas_consol.Direction = ParameterDirection.Input;
                pa_detalle_deudas_consol.Value = this.txtDetalleDeudas.Text;
                cmd.Parameters.Add(pa_detalle_deudas_consol);

                //───────────────────PA_CODIGO_ESTADO_EXCEP
                OracleParameter pa_codigo_estado_excp = new OracleParameter("PA_CODIGO_ESTADO_EXCEP", OracleType.Number);
                pa_codigo_estado_excp.Direction = ParameterDirection.Input;
                pa_codigo_estado_excp.Value = 1;
                cmd.Parameters.Add(pa_codigo_estado_excp);

                //───────────────────PA_CODIGO_AGENCIA_ORIGEN
                OracleParameter pa_codigo_agencia_origen = new OracleParameter("PA_CODIGO_AGENCIA_ORIGEN", OracleType.Number);
                pa_codigo_agencia_origen.Direction = ParameterDirection.Input;
                pa_codigo_agencia_origen.Value = this.infoExcep.codigo_agencia_origen;
                cmd.Parameters.Add(pa_codigo_agencia_origen);

                //───────────────────PA_SALDO_BRUTO_COM
                OracleParameter pa_saldo_bruto_com = new OracleParameter("PA_SALDO_BRUTO_COM", OracleType.Number);
                pa_saldo_bruto_com.Direction = ParameterDirection.Input;
                pa_saldo_bruto_com.Value = Convert.ToDouble(this.txtSueldoBruto.Text);
                cmd.Parameters.Add(pa_saldo_bruto_com);

                //───────────────────PA_CONDICION_TU
                OracleParameter pa_condicion_tu = new OracleParameter("PA_CONDICION_TU", OracleType.VarChar);
                pa_condicion_tu.Direction = ParameterDirection.Input;
                pa_condicion_tu.Value = this.txtCondicionCS.Text;
                cmd.Parameters.Add(pa_condicion_tu);

                //───────────────────PA_WORKFLOW_ID
                OracleParameter pa_workflow_id = new OracleParameter("PA_WORKFLOW_ID", OracleType.Number);
                pa_workflow_id.Direction = ParameterDirection.Input;
                pa_workflow_id.Value = 1;
                cmd.Parameters.Add(pa_workflow_id);

                //───────────────────PA_INGRESO_NETO_TU
                OracleParameter pa_ingreso_neto_tu = new OracleParameter("PA_INGRESO_NETO_TU", OracleType.Number);
                pa_ingreso_neto_tu.Direction = ParameterDirection.Input;
                pa_ingreso_neto_tu.Value = Convert.ToDouble(this.txtIngresoNeto.Text);
                cmd.Parameters.Add(pa_ingreso_neto_tu);

                //───────────────────PA_INGRESO_NETO_DESP
                OracleParameter pa_ingreso_neto_desp = new OracleParameter("PA_INGRESO_NETO_DESP", OracleType.Number);
                pa_ingreso_neto_desp.Direction = ParameterDirection.Input;
                pa_ingreso_neto_desp.Value = Convert.ToDouble(this.txtIngresoNetoDes.Text);
                cmd.Parameters.Add(pa_ingreso_neto_desp);

                //───────────────────P_EDAD
                if (string.IsNullOrEmpty(this.txtEdad.Text))
                {
                    this.txtEdad.Text = "0";
                }
                OracleParameter pa_edad = new OracleParameter("P_EDAD", OracleType.Number);
                pa_edad.Direction = ParameterDirection.Input;
                pa_edad.Value = Convert.ToInt32(this.txtEdad.Text);
                cmd.Parameters.Add(pa_edad);

                //───────────────────P_PRESTACIONES
                if (string.IsNullOrEmpty(this.txtPrestaciones.Text))
                {
                    this.txtPrestaciones.Text = "0.00";
                }
                OracleParameter pa_prestaciones = new OracleParameter("P_PRESTACIONES", OracleType.Number);
                pa_prestaciones.Direction = ParameterDirection.Input;
                pa_prestaciones.Value = Convert.ToDouble(this.txtPrestaciones.Text);
                cmd.Parameters.Add(pa_prestaciones);
                
                //───────────────────PA_PUESTO_REFERENCIADO
                if (string.IsNullOrEmpty(this.txtPuestoDesepmpleña.Text))
                {
                    this.txtPuestoDesepmpleña.Text = "N/A";
                }
                OracleParameter pa_puesto_referenciado = new OracleParameter("PA_PUESTO_REFERENCIADO", OracleType.VarChar);
                pa_puesto_referenciado.Direction = ParameterDirection.Input;
                pa_puesto_referenciado.Value = this.txtPuestoDesepmpleña.Text;
                cmd.Parameters.Add(pa_puesto_referenciado);

                //───────────────────PA_SOLICITUD_ANTERIOR
                if (this.txtSolAnt.Text == "")
                {
                    this.txtSolAnt.Text = "N/A";
                }
                OracleParameter pa_solicitud_anterior = new OracleParameter("PA_SOLICITUD_ANTERIOR", OracleType.VarChar);
                pa_solicitud_anterior.Direction = ParameterDirection.Input;
                pa_solicitud_anterior.Value = this.txtSolAnt.Text;
                cmd.Parameters.Add(pa_solicitud_anterior);
                
                //───────────────────PA_SOL_ANTERIOR
                if (string.IsNullOrEmpty(this.txtFechaAnt.Text))
                {
                    this.txtFechaAnt.Text = "N/A";
                }
                OracleParameter pa_fec_sol_anterior = new OracleParameter("PA_FECHA_SOL_ANTERIOR", OracleType.VarChar);
                pa_fec_sol_anterior.Direction = ParameterDirection.Input;
                pa_fec_sol_anterior.Value = this.txtFechaAnt.Text;
                cmd.Parameters.Add(pa_fec_sol_anterior);
                
                //───────────────────PA_RESOLUCION_ANTERIOR
                if (string.IsNullOrEmpty(this.txtResolucionAnt.Text))
                {
                    this.txtResolucionAnt.Text = "N/A";
                }
                OracleParameter pa_resolucion_anterior = new OracleParameter("PA_RESOLUCION_ANTERIOR", OracleType.VarChar);
                pa_resolucion_anterior.Direction = ParameterDirection.Input;
                pa_resolucion_anterior.Value = this.txtResolucionAnt.Text;
                cmd.Parameters.Add(pa_resolucion_anterior);
                
                //───────────────────PA_NUEVA_FECHA
                if (string.IsNullOrEmpty(this.txtNuevaFecha.Text))
                {
                    this.txtNuevaFecha.Text = "N/A";
                }
                OracleParameter pa_nueva_fecha = new OracleParameter("PA_NUEVA_FECHA", OracleType.VarChar);
                pa_nueva_fecha.Direction = ParameterDirection.Input;
                pa_nueva_fecha.Value = this.txtNuevaFecha.Text;
                cmd.Parameters.Add(pa_nueva_fecha);
                
                //───────────────────PA_RECONSIDERACION_FECHA
                if (string.IsNullOrEmpty(this.txtFechaReconsideracion.Text))
                {
                    this.txtFechaReconsideracion.Text = "N/A";
                }
                OracleParameter pa_reconsidera_fecha = new OracleParameter("PA_RECONSIDERACION_FECHA", OracleType.VarChar);
                pa_reconsidera_fecha.Direction = ParameterDirection.Input;
                pa_reconsidera_fecha.Value = this.txtFechaReconsideracion.Text;
                cmd.Parameters.Add(pa_reconsidera_fecha);
                

                OracleParameter pa_mensaje = new OracleParameter("PA_MENSAJE", OracleType.VarChar, 200);
                pa_mensaje.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pa_mensaje);

                cmd.ExecuteNonQuery();

                this.v_CodigoExcepcionSol = Convert.ToInt32(cmd.Parameters["PA_CODIGO_EXCEPCION"].Value);
                mensaje = cmd.Parameters["PA_MENSAJE"].Value.ToString();

                if (mensaje.Equals(string.Empty) || mensaje == null)
                {
                    //guardar el detalle en caso de no haber error                     
                    var detalles = this.cargarDetalle(this.v_CodigoExcepcionSol);

                    foreach (var item in detalles)
                    {
                        this.p_guardar_detalle(this.v_CodigoExcepcionSol, item.Tc_CodTipoExcepcion, item.Tc_CodLineamiento, item.Tc_Justificacion);
                    }

                    MessageBox.Show("Excepción agregada con éxito");
                    this.Close();
                }
                else
                {
                    MessageBox.Show(mensaje);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void p_guardar_detalle(int _codExcepcion, string _codTipExc, string _codLinea, string _justificacion)
        {

            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }

                string sql = "excp.dcs_p_crear_det_Excep";
                OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
                cmd.CommandType = CommandType.StoredProcedure;

                //───────────────────PA_CODIGO_EXCEPCION
                OracleParameter pa_codigo_excepcion = new OracleParameter("PA_CODIGO_EXCEPCION", OracleType.Number);
                pa_codigo_excepcion.Direction = ParameterDirection.Input;
                pa_codigo_excepcion.Value = _codExcepcion;
                cmd.Parameters.Add(pa_codigo_excepcion);

                //───────────────────PA_COD_TIPO_EXCEPCION
                OracleParameter pa_cod_tipo_excepcion = new OracleParameter("PA_COD_TIPO_EXCEPCION", OracleType.VarChar);
                pa_cod_tipo_excepcion.Direction = ParameterDirection.Input;
                pa_cod_tipo_excepcion.Value = _codTipExc;
                cmd.Parameters.Add(pa_cod_tipo_excepcion);

                //───────────────────PA_JUSTIFICACION
                OracleParameter pa_justificacion = new OracleParameter("PA_JUSTIFICACION", OracleType.VarChar);
                pa_justificacion.Direction = ParameterDirection.Input;
                pa_justificacion.Value = _justificacion;
                cmd.Parameters.Add(pa_justificacion);

                //───────────────────PA_COD_LINEAMIENTO
                OracleParameter pa_cod_lineamiento = new OracleParameter("PA_COD_LINEAMIENTO", OracleType.VarChar);
                pa_cod_lineamiento.Direction = ParameterDirection.Input;
                pa_cod_lineamiento.Value = _codLinea;
                cmd.Parameters.Add(pa_cod_lineamiento);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private List<Detalle_Excepcion> cargarDetalle(int _codExcepcionSol)
        {
            List<Detalle_Excepcion> detalle = new List<Detalle_Excepcion>();

            foreach (DataGridViewRow item in this.dgvExcepciones.Rows)
            {
                if (item.Cells["cod_lineamiento"].Value != null)
                {
                    Detalle_Excepcion obj = new Detalle_Excepcion();
                    obj.Tc_CodigoExcepcion = _codExcepcionSol;
                    obj.Tc_CodLineamiento = item.Cells["cod_lineamiento"].Value.ToString();
                    obj.Tc_CodTipoExcepcion = item.Cells[2].Value.ToString();
                    obj.Tc_Justificacion = item.Cells["observaciones"].Value.ToString();

                    detalle.Add(obj);
                }
            }

            return detalle;
        }

        //modificar la excepcion
        private void p_actualizar_excepcion()
        {
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }

                string sql = "excp.dcs_p_mod_Excep";
                OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
                cmd.CommandType = CommandType.StoredProcedure;

                //───────────────────pa_codigo_excepcion
                OracleParameter pa_codigo_excepcion = new OracleParameter("pa_codigo_excepcion", OracleType.Number);
                pa_codigo_excepcion.Direction = ParameterDirection.Input;
                pa_codigo_excepcion.Value = this.v_CodigoExcepcionSol;
                cmd.Parameters.Add(pa_codigo_excepcion);

                //───────────────────pa_pago_mediante                       
                OracleParameter pa_pago_mediante = new OracleParameter("pa_pago_mediante", OracleType.VarChar);
                pa_pago_mediante.Direction = ParameterDirection.Input;
                pa_pago_mediante.Value = this.Pago_Mediante_Seleccion();
                cmd.Parameters.Add(pa_pago_mediante);

                //───────────────────pa_recomendacion_filial
                OracleParameter pa_recomendacion_filial = new OracleParameter("pa_recomendacion_filial", OracleType.VarChar);
                pa_recomendacion_filial.Direction = ParameterDirection.Input;
                pa_recomendacion_filial.Value = this.txtRecFilial.Text;
                cmd.Parameters.Add(pa_recomendacion_filial);

                //───────────────────pa_detalle_deudas_consol
                OracleParameter pa_detalle_deudas_consol = new OracleParameter("pa_detalle_deudas_consol", OracleType.VarChar);
                pa_detalle_deudas_consol.Direction = ParameterDirection.Input;
                pa_detalle_deudas_consol.Value = this.txtDetalleDeudas.Text;
                cmd.Parameters.Add(pa_detalle_deudas_consol);

                //───────────────────pa_saldo_bruto_com
                OracleParameter pa_saldo_bruto_com = new OracleParameter("pa_saldo_bruto_com", OracleType.Number);
                pa_saldo_bruto_com.Direction = ParameterDirection.Input;
                pa_saldo_bruto_com.Value = Convert.ToDouble(this.txtSueldoBruto.Text);
                cmd.Parameters.Add(pa_saldo_bruto_com);

                //───────────────────pa_condicion_tu
                OracleParameter pa_condicion_tu = new OracleParameter("pa_condicion_tu", OracleType.VarChar);
                pa_condicion_tu.Direction = ParameterDirection.Input;
                pa_condicion_tu.Value = this.txtCondicionCS.Text;
                cmd.Parameters.Add(pa_condicion_tu);

                //───────────────────pa_ingreso_neto_tu
                OracleParameter pa_ingreso_neto_tu = new OracleParameter("pa_ingreso_neto_tu", OracleType.Number);
                pa_ingreso_neto_tu.Direction = ParameterDirection.Input;
                pa_ingreso_neto_tu.Value = Convert.ToDouble(this.txtIngresoNeto.Text);
                cmd.Parameters.Add(pa_ingreso_neto_tu);

                //───────────────────pa_ingreso_neto_desp
                OracleParameter pa_ingreso_neto_desp = new OracleParameter("pa_ingreso_neto_desp", OracleType.Number);
                pa_ingreso_neto_desp.Direction = ParameterDirection.Input;
                pa_ingreso_neto_desp.Value = Convert.ToDouble(this.txtIngresoNetoDes.Text);
                cmd.Parameters.Add(pa_ingreso_neto_desp);

                //───────────────────pa_recomendacion_nivRes
                OracleParameter pa_recomendacion_nivRes = new OracleParameter("pa_recomendacion_nivRes", OracleType.VarChar);
                pa_recomendacion_nivRes.Direction = ParameterDirection.Input;
                pa_recomendacion_nivRes.Value = this.txtGerente.Text;
                cmd.Parameters.Add(pa_recomendacion_nivRes);

                cmd.ExecuteNonQuery();

                //cambio
                foreach (var item in detalle_cargada)
                {
                    if (!item.Estado.Equals(agregada))
                    {
                        //procedimiento de edición
                        this.p_editar_detalle_Excepcion(this.v_CodigoExcepcionSol, item.Tc_CodTipoExcepcion, item.Tc_Justificacion, item.Tc_CodLineamiento, item.Estado);

                    }
                    else if (item.Estado.Equals(agregada))
                    {
                        //procedimiento para nuevos
                        this.p_guardar_detalle(this.v_CodigoExcepcionSol, item.Tc_CodTipoExcepcion, item.Tc_CodLineamiento, item.Tc_Justificacion);
                    }
                }

                //───────────────────prestaciones
                if (string.IsNullOrEmpty(this.txtPrestaciones.Text))
                {
                    this.txtPrestaciones.Text = "0.00";
                }
                this.da.ActualizarPrestaciones(this.v_CodigoExcepcionSol, Convert.ToDecimal(this.txtPrestaciones.Text));

                MessageBox.Show("Se ha editado la excepción exitosamente", "Operación exitosa");
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void p_editar_detalle_Excepcion(int codigo_excepcion, string codigo_tipo_excep, string justificacion, string codLinea, string _accion)
        {
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }

                string sql = "excp.dcs_p_edit_det_exc";
                OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
                cmd.CommandType = CommandType.StoredProcedure;

                //───────────────────pa_codigo_excepcion
                OracleParameter pa_codigo_excepcion = new OracleParameter("pa_codigo_excepcion", OracleType.Number);
                pa_codigo_excepcion.Direction = ParameterDirection.Input;
                pa_codigo_excepcion.Value = codigo_excepcion;
                cmd.Parameters.Add(pa_codigo_excepcion);

                //───────────────────pa_codigo_tipo_excepcion
                OracleParameter pa_cod_tipo_excepcion = new OracleParameter("pa_codigo_tipo_excepcion", OracleType.VarChar);
                pa_cod_tipo_excepcion.Direction = ParameterDirection.Input;
                pa_cod_tipo_excepcion.Value = codigo_tipo_excep;
                cmd.Parameters.Add(pa_cod_tipo_excepcion);

                //───────────────────pa_justificacion
                OracleParameter pa_justificacion = new OracleParameter("pa_justificacion", OracleType.VarChar);
                pa_justificacion.Direction = ParameterDirection.Input;
                pa_justificacion.Value = justificacion;
                cmd.Parameters.Add(pa_justificacion);

                //───────────────────pa_cod_lineamiento
                OracleParameter pa_cod_lineamiento = new OracleParameter("pa_cod_lineamiento", OracleType.VarChar);
                pa_cod_lineamiento.Direction = ParameterDirection.Input;
                pa_cod_lineamiento.Value = codLinea;
                cmd.Parameters.Add(pa_cod_lineamiento);

                //───────────────────pa_accion
                OracleParameter pa_accion = new OracleParameter("pa_accion", OracleType.VarChar);
                pa_accion.Direction = ParameterDirection.Input;
                if (_accion.Equals(cargada))
                {
                    pa_accion.Value = u_Globales.accionModificar;
                }
                else
                {
                    pa_accion.Value = u_Globales.accionEliminar;
                }
                cmd.Parameters.Add(pa_accion);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string Pago_Mediante_Seleccion()
        {
            string valor = string.Empty;

            if (this.cmbPagoMediante.SelectedIndex == 0)
            {
                valor = "V";
            }
            else if (this.cmbPagoMediante.SelectedIndex == 1)
            {
                valor = "P";
            }

            return valor;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

            if (this.btnEditar.Text.Equals("Editar"))
            {
                this.btnEditar.Text = "Guardar Cambios";

                if (this.dgvExcepciones.RowCount > 0)
                {
                    DataGridViewRow row = this.dgvExcepciones.CurrentRow;
                    row.Cells["observaciones"].ReadOnly = false;
                }
            }
            else
            {
                this.btnEditar.Text = "Editar";
                if (this.dgvExcepciones.RowCount > 0)
                {
                    DataGridViewRow row = this.dgvExcepciones.CurrentRow;
                    row.Cells["observaciones"].ReadOnly = true;

                    if (editarExcepcion)
                    {
                        string rowLin = row.Cells["cod_lineamiento"].Value.ToString();
                        string rowExc = row.Cells[2].Value.ToString();

                        Detalle_Excepcion eObj = detalle_cargada.FirstOrDefault(c => c.Tc_CodLineamiento.Equals(rowLin)
                                                                                                                             && c.Tc_CodTipoExcepcion.Equals(rowExc));

                        if (eObj != null)
                        {
                            eObj.Tc_Justificacion = row.Cells["observaciones"].Value.ToString();
                        }
                    }
                }
            }
        }

        //cambio
        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (this.dgvExcepciones.RowCount > 0)
            {
                DataGridViewRow row = this.dgvExcepciones.CurrentRow;
                string rowLin = row.Cells["cod_lineamiento"].Value.ToString();
                string rowExc = row.Cells[2].Value.ToString();
                this.dgvExcepciones.Rows.Remove(row);

                if (this.Accion.Equals(u_Globales.accionModificar)/*excepcion_editable*/)
                {
                    Detalle_Excepcion eObj = detalle_cargada.FirstOrDefault(c => c.Tc_CodLineamiento.Equals(rowLin) && c.Tc_CodTipoExcepcion.Equals(rowExc));

                    if (eObj != null)
                    {
                        if (eObj.Estado.Equals(cargada) /*&& this.Accion.Equals(u_Globales.accionModificar)*/)
                        {
                            eObj.Estado = eliminada;
                            if (eObj.Tc_CodTipoExcepcion.Equals(u_Globales.ExcepcionPrestaciones))
                            {
                                this.ExcepPrestacion = false;
                            }
                        }
                        else
                        {
                            //this.detalle_cargada.Remove(eObj);
                        }
                    }
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (this.v_CodigoExcepcionSol == 0)
            {
                return;
            }

            frmRpt_FormatoExcepcion formato = new frmRpt_FormatoExcepcion(this.v_CodigoExcepcionSol, this.NoSolicitud, this.da);
            formato.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.btnEditar_Click(null, null);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.btnQuitar_Click(null, null);
        }

        private void cmbPagoMediante_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string valor = this.cmbPagoMediante.Text;
            if (valor.Equals("Pago por Planilla") & !this.txtTipoPrestamo.Text.ToUpper().Contains("FIDUCIARIO")
                & !this.txtTipoPrestamo.Text.ToUpper().Contains("HIPOTECA") & !this.txtTipoPrestamo.Text.ToUpper().Contains("AUTOMOVIL")
                & !this.txtTipoPrestamo.Text.ToUpper().Contains("VIVIEND"))
            {
                this.btnPrestaciones.Visible = true;
                this.PrestacionesOk = true;
            }
            else
            {
                this.btnPrestaciones.Visible = false;
                this.PrestacionesOk = false;
            }
        }

        private void btnPrestaciones_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.EnableRaisingEvents = false;
            string archivo = Application.StartupPath + @"\prestaciones.exe";
            proc.StartInfo.FileName = archivo;
            proc.Start();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.v_CodigoExcepcionSol == 0)
            {
                return;
            }
            this.p_mostrar_espera("Cargando previsualización...", true);
            frmRpt_FormatoExcepcion formato = new frmRpt_FormatoExcepcion(this.v_CodigoExcepcionSol, this.NoSolicitud, this.da);
            panelCargandoSolic.Visible = false;
            formato.MostrarImprimir = false;
            formato.ShowDialog();
        }

        private void p_mostrar_espera(string texto, bool autohie)
        {
            panelCargandoSolic.Visible = true;
            labelCargado.Text = texto;
            //centrar_panel();
            Application.DoEvents();
            if (autohie)
                timerMostrarEspera.Enabled = true;
        }

        private void linkSolicitud_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            p_mostrar_espera("Cargado solicitud..", true);
            s_PreCalificado forma = new s_PreCalificado(da);
            forma.gmodo_coopsafa = "CONS";
            forma.txtNo_solicitud_coopsafa.Text = this.txtNoSolicitud.Text;
            DialogResult res = forma.ShowDialog();
            panelCargandoSolic.Visible = false;
        }

        private void btnAdjuntar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.v_CodigoExcepcionSol != 0)
                {
                    e_documento_excep forma = new e_documento_excep(this.v_CodigoExcepcionSol, this.da);
                    forma.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Debe guardar la excepción antes de adjuntar documentos");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al tratar de registar documento:" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void DarResolucion(int estado)
        {
            //Significa que lo aprueba, así que genera el movimiento
            string mensaje = this.da.ResolucionDirecta(this.v_CodigoExcepcionSol, estado, this.Estacion_actual);
            if (!string.IsNullOrEmpty(mensaje))
            {
                MessageBox.Show(mensaje);
            }
            else
            {
                MessageBox.Show("Resolución agregada con éxito!");
                this.Close();
            }
        }
        private void rbnDevolver_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbnDevolver.Checked & this.ResolucionPendiente)
            {
                //Significa que lo devuelve
                e_add_notas nota_anulacion = new e_add_notas(this.v_CodigoExcepcionSol, this.Estacion_actual, u_Globales.accionAgregar, 0);
                string respuesta = string.Empty;
                nota_anulacion.ShowDialog();
                respuesta = nota_anulacion.respuesta;
                while (this.EvaluarTexto(respuesta))
                {
                    if (nota_anulacion.cerrar)
                    {
                        DialogResult result = MessageBox.Show("¿Desea cancelar la operación?", "Cancelar Operación", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {
                            return;
                        }
                        else
                        {
                            nota_anulacion.ShowDialog();
                        }
                    }
                    else
                    {
                        nota_anulacion.ShowDialog();
                    }

                    respuesta = nota_anulacion.respuesta;
                }

                this.DarResolucion(5);
            }
        }

        private bool EvaluarTexto(string texto)
        {
            return (texto.Equals(string.Empty));
        }

        private void rbnDenegar_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbnDenegar.Checked & this.ResolucionPendiente)
            {
                //Significa que lo rechaza, así que genera el movimiento
                ComentarioResolucion comen = new ComentarioResolucion();
                DialogResult result = comen.ShowDialog();
                if (result == DialogResult.Yes)
                {
                    string comentario = comen.Comentario;

                    if (string.IsNullOrEmpty(comentario))
                    {
                        MessageBox.Show("Debe ingresar el motivo de resolución", "Error");
                        return;
                    }
                    else
                    {
                        string sql = "excp.dcs_p_recomendacion_resol";

                        if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                        {
                            DocSys.connOracle.Open();
                        }

                        OracleParameter pa_comentario = new OracleParameter("pa_comentario", OracleType.VarChar);
                        pa_comentario.Direction = ParameterDirection.Input;
                        pa_comentario.Value = comentario;

                        OracleParameter pa_codigo_excep = new OracleParameter("pa_codigo_excepcion", OracleType.Number);
                        pa_codigo_excep.Direction = ParameterDirection.Input;
                        pa_codigo_excep.Value = this.v_CodigoExcepcionSol;

                        OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(pa_comentario);
                        cmd.Parameters.Add(pa_codigo_excep);

                        try
                        {
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                        this.DarResolucion(3);
                    }
                }
            }
        }

        private void rbnAprobar_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbnAprobar.Checked & this.ResolucionPendiente)
            {
                //Significa que lo aprueba, así que genera el movimiento
                ComentarioResolucion comen = new ComentarioResolucion();
                DialogResult result = comen.ShowDialog();
                if (result == DialogResult.Yes)
                {
                    string comentario = comen.Comentario;

                    if (string.IsNullOrEmpty(comentario))
                    {
                        MessageBox.Show("Debe ingresar el motivo de resolución", "Error");
                        this.rbnAprobar.Checked = false;
                        return;
                    }
                    else
                    {
                        string sql = "excp.dcs_p_recomendacion_resol";

                        if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                        {
                            DocSys.connOracle.Open();
                        }

                        OracleParameter pa_comentario = new OracleParameter("pa_comentario", OracleType.VarChar);
                        pa_comentario.Direction = ParameterDirection.Input;
                        pa_comentario.Value = comentario;

                        OracleParameter pa_codigo_excep = new OracleParameter("pa_codigo_excepcion", OracleType.Number);
                        pa_codigo_excep.Direction = ParameterDirection.Input;
                        pa_codigo_excep.Value = this.v_CodigoExcepcionSol;

                        OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(pa_comentario);
                        cmd.Parameters.Add(pa_codigo_excep);

                        try
                        {
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                        this.DarResolucion(2);
                    }
                }
            }
        }

        private void btnActualizarInfo_Click(object sender, EventArgs e)
        {
            this.p_cargar_info_Excepcion(false);
        }

        private void txtPrestaciones_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtPrestaciones.Text))
                {
                    this.txtPrestaciones.Text = "0.00";
                }

                decimal prestaciones = Convert.ToDecimal(this.txtPrestaciones.Text);
                string valor = this.cmbPagoMediante.Text;

                if (valor.Equals("Pago por Planilla") & !this.txtTipoPrestamo.Text.ToUpper().Contains("FIDUCIARIO")
                & !this.txtTipoPrestamo.Text.ToUpper().Contains("HIPOTECA")
                & !this.txtTipoPrestamo.Text.ToUpper().Contains("AUTOMOVIL")
                & !this.txtTipoPrestamo.Text.ToUpper().Contains("VIVIEND")
                )
                {
                    if (btnPrestaciones.Visible)
                    {
                        if (string.IsNullOrEmpty(this.txtMontoSolicitado.Text))
                            this.txtMontoSolicitado.Text = "0.00";

                        if (string.IsNullOrEmpty(this.txtPrestaciones.Text))
                            this.txtPrestaciones.Text = "0.00";

                        decimal monto = Convert.ToDecimal(this.txtMontoSolicitado.Text);

                        if (monto > prestaciones & !this.ExcepPrestacion)
                        {
                            MessageBox.Show("El monto solicitado es mayor a las prestaciones.");
                            this.PrestacionesOk = false;
                            //this.Close();
                        }
                        else
                            this.PrestacionesOk = true;
                    }
                }
                else
                {
                    this.PrestacionesOk = true;
                }

                this.txtPrestaciones.Text = string.Format(String.Format("{0:###,##0.00}", prestaciones));
            }
            catch (Exception)
            {
            }
        }

        private void txtPrestaciones_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox texbox = this.txtPrestaciones;
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }
            // al dar enter pasar al siguiente campo
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{TAB}");
            }
            //Si el texto esta sombreado todo caer encima al dato del textbox
            if (texbox.SelectionLength >= texbox.Text.Length)
            {
                texbox.Text = "";
            }

            //Para obligar a que sólo se introduzcan números 
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {

                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan 
                    e.Handled = true;
                }
            }
            //Permitir el punto decimal
            bool IsDec = false;
            int nroDec = 0;
            for (int i = 0; i < texbox.Text.Length; i++)
            {
                if (texbox.Text[i] == '.')
                    IsDec = true;

                if (IsDec && nroDec++ >= 2)
                {
                    e.Handled = true;
                    return;
                }
            }
            if (e.KeyChar >= 48 && e.KeyChar <= 57)
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = (IsDec) ? true : false;
            else
                e.Handled = true;
        }
    }

    public class Lineamientos
    {
        public string CodigoLineamiento { get; set; }
        public string Lineamiento { get; set; }
    }

    public class ReglasExcepciones
    {
        public string codigo_lineamiento { get; set; }
        public string cod_tipo_excepcion { get; set; }
        public string cod_tipo_excep_prohibida { get; set; }
        public string cod_linea_prohibida { get; set; }
        public string observaciones { get; set; }
    }

    public class Detalle_Excepcion
    {
        public int Tc_CodigoExcepcion { get; set; }
        public string Tc_CodTipoExcepcion { get; set; }
        public string Tc_Justificacion { get; set; }
        public string Tc_CodLineamiento { get; set; }
        public string Estado { get; set; }
    }
}
