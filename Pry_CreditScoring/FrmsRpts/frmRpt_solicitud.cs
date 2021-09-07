using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using wfcModel;
using System.IO;

namespace Docsis_Application.FrmsRpts
{
    public partial class frmRpt_solicitud : Form
    {
        public rtp_solicitud_Clase data_form;
        public DataAccess da;
        public Int32 gno_solicitud = 0;
		public string requiereGarante = string.Empty;
        bool con_borde = MDI_Menu.con_borde;

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

        public frmRpt_solicitud(DataAccess da, Int32 p_no_solicitud)
        {
            InitializeComponent();
            this.da = da;
            gno_solicitud = p_no_solicitud;

        }
        private void frmRpt_solicitud_Load(object sender, EventArgs e)
        {
            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = ZoomMode.Percent;
            reportViewer1.ZoomPercent = 100; //Seleccionamos el zoom que deseamos utilizar. En este caso un 100%
            mostrar_reporte();
            this.reportViewer1.RefreshReport();
        }

        private void mostrar_reporte()
        {
            try
            {
                List<rtp_solicitud_Clase> data = new List<rtp_solicitud_Clase>();
                obtenerSolicitud(gno_solicitud);
                data.Add(data_form);

                string vl_nombre_prestatario = "";
                
                vl_nombre_prestatario = data_form.primer_nombre + " " + data_form.segundo_nombre + " " + data_form.primer_apellido + " " + data_form.segundo_apellido;
                vl_nombre_prestatario = data_form.nombres_solicitante + " " + data_form.primer_apellido + " " + data_form.segundo_apellido;

                string vl_nombre_codeudor = "";
                vl_nombre_codeudor = data_form.primer_nombre_codeudor + " " + data_form.segundo_nombre_codeudor + " " + data_form.primer_apellido_codeudor + " " + data_form.segundo_apellido_codeudor;

                string vl_nombre_aval1 = "";
                vl_nombre_aval1 = data_form.primer_nombre_aval1 + " " + data_form.segundo_nombre_aval1 + " " + data_form.primer_apellido_aval1 + " " + data_form.segundo_apellido_aval1;

                string vl_nombre_aval2 = "";
                vl_nombre_aval2 = data_form.primer_nombre_aval2 + " " + data_form.segundo_nombre_aval2 + " " + data_form.primer_apellido_aval2 + " " + data_form.segundo_apellido_aval2;

				string vl_nombre_garate = string.Empty;
				vl_nombre_garate = data_form.PrimerNombreGarante + " " + data_form.SegundoNombreGarante + " " + data_form.PrimerApellidoGarante + " " + data_form.SegundoApellidoGarante;

				ReportDataSource rds = new ReportDataSource("dataset1", data);
                ReportParameter pa1 = new ReportParameter("pa_nombre_firma_prestatario", vl_nombre_prestatario);
                ReportParameter pa2 = new ReportParameter("pa_nombre_firma_codeudor", vl_nombre_codeudor);
                ReportParameter pa3 = new ReportParameter("pa_nombre_firma_aval1", vl_nombre_aval1);
                ReportParameter pa4 = new ReportParameter("pa_nombre_firma_aval2", vl_nombre_aval2);				

				DataTable dtsolicitud = da.ObtenerInfoSolicitud2(gno_solicitud);

                string vl_tipo_solicitud = "";
                if (dtsolicitud.Rows[0]["requiere_aval1"].ToString() == "S")
                    vl_tipo_solicitud = "AVAL1";

                if (dtsolicitud.Rows[0]["requiere_aval2"].ToString() == "S")
                    vl_tipo_solicitud = "AVAL2";

                if (dtsolicitud.Rows[0]["requiere_aval1"].ToString() == "N" && dtsolicitud.Rows[0]["requiere_aval2"].ToString() == "N")
                    vl_tipo_solicitud = "SINAVAL";

                this.reportViewer1.LocalReport.DataSources.Add(rds);

                if (vl_tipo_solicitud == "SINAVAL")
                    reportViewer1.LocalReport.ReportEmbeddedResource = "Docsis_Application.Reportes.rptSolicitud.rdlc";

                if (vl_tipo_solicitud == "AVAL1")
                    reportViewer1.LocalReport.ReportEmbeddedResource = "Docsis_Application.Reportes.rptSolicitudAval1.rdlc";

                if (vl_tipo_solicitud == "AVAL2")
                    reportViewer1.LocalReport.ReportEmbeddedResource = "Docsis_Application.Reportes.rptSolicitudAval2.rdlc";

				if (requiereGarante.Equals("S"))
				{
					reportViewer1.LocalReport.ReportEmbeddedResource = "Docsis_Application.Reportes.rptSolicitudGarante.rdlc";
					ReportParameter pa5 = new ReportParameter("pa_nombre_firma_garante", vl_nombre_garate);
					this.reportViewer1.LocalReport.SetParameters(pa5);
				}

                this.reportViewer1.LocalReport.SetParameters(pa1);
                this.reportViewer1.LocalReport.SetParameters(pa2);
                this.reportViewer1.LocalReport.SetParameters(pa3);
                this.reportViewer1.LocalReport.SetParameters(pa4);
                
                this.reportViewer1.LocalReport.Refresh();
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en reporte " + ex.Message);
            }
        }
        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void panelTop_MouseMove(object sender, MouseEventArgs e)
        {
            moverForm();
        }
        private void obtenerSolicitud(Int32 p_no_solicitud)
        {
            solicitud_credito sol = da.ObtenerSolicitudCredito(p_no_solicitud);
            rtp_solicitud_Clase datos_reporte = new rtp_solicitud_Clase();

            datos_reporte.nombre_agencia = da.ObtenerNombreAgencia(sol.codigo_agencia);
            datos_reporte.nombre_usuario = sol.oficial_servicio;
            datos_reporte.monto_solicitado = sol.monto_solicitado;
            datos_reporte.descripcion_fuente = sol.descripcion_fuente;
            datos_reporte.tasa = sol.tasa;
            datos_reporte.plazo = sol.plazo;
            datos_reporte.destino_credito = sol.destino_credito;
            datos_reporte.descripcion_destino = sol.descripcion_destino;
            datos_reporte.desc_sub_aplicacion = sol.desc_sub_aplicacion;
            datos_reporte.desc_moneda = sol.desc_moneda;
            datos_reporte.no_solicitud = sol.no_solicitud;
            datos_reporte.fecha_solicitud = sol.fecha_solicitud.ToString();
            datos_reporte.descripcion_fuente = sol.descripcion_fuente.ToString();
            datos_reporte.codigo_cliente = sol.codigo_cliente;
            datos_reporte.no_identificacion = sol.no_identificacion;
            datos_reporte.nombres_solicitante = sol.nombres;
            try
            {
                string[] nombre = sol.nombres.Split(' ');

                datos_reporte.primer_nombre = nombre[0];
                datos_reporte.segundo_nombre = nombre[1];
            }
            catch
            {
                datos_reporte.primer_nombre = sol.nombres;
            }
            datos_reporte.primer_apellido = sol.primer_apellido;
            datos_reporte.segundo_apellido = sol.segundo_apellido;
            datos_reporte.apellido_casada = sol.apellido_casada;
            datos_reporte.estado_civil = sol.estado_civil;
            datos_reporte.sexo = sol.sexo;

            DateTime vl_fecha_nac;
            try
            {
                vl_fecha_nac = (DateTime)sol.fecha_nacimiento;
                datos_reporte.fecha_nacimiento = vl_fecha_nac.ToString("dd/MM/yyyy");
            }
            catch
            {
                datos_reporte.fecha_nacimiento = "";
            }
            
            datos_reporte.lugar_nacimiento = sol.lugar_nacimiento;
            datos_reporte.nacionalidad = sol.nacionalidad;
            datos_reporte.direccion_residencia = sol.direccion_residencia;
            datos_reporte.fecha_ingreso_coop = sol.fecha_ingreso_coop;
            datos_reporte.telefono_fijo = sol.telefono_fijo;
            datos_reporte.telefono_celular = sol.telefono_celular;
            datos_reporte.telefono_adicional1 = sol.telefono_adicional1;
            datos_reporte.telefono_adicional2 = sol.telefono_adicional2;
            datos_reporte.correo_electronico = sol.correo_electronico;
            datos_reporte.dependientes_hijos = sol.dependientes_hijos;
            datos_reporte.dependientes_otros = sol.dependientes_otros;
            datos_reporte.tipo_vivienda = sol.tipo_vivienda;
            if (sol.tipo_vivienda == "PROPIA")
                datos_reporte.tipo_vivienda = "Propia";
            if (sol.tipo_vivienda == "ALQUILADA")
                datos_reporte.tipo_vivienda = "Alquilada";
            if (sol.tipo_vivienda == "FAMILIAR")
                datos_reporte.tipo_vivienda = "Familiar";
            if (sol.tipo_vivienda == "APLAZOS")
                datos_reporte.tipo_vivienda = "Pagandola a Plazos";


            datos_reporte.tipo_vivienda_especificar = sol.tipo_vivienda_especificar;
            if (sol.nivel_educativo == "P")
                datos_reporte.nivel_educativo = "Primaria";
            if (sol.nivel_educativo == "S")
                datos_reporte.nivel_educativo = "Secundaria";
            if (sol.nivel_educativo == "U")
                datos_reporte.nivel_educativo = "Universitaria";
            if (sol.nivel_educativo == "G")
                datos_reporte.nivel_educativo = "Post Grado";
            datos_reporte.profesion_oficio = sol.profesion_oficio;


            if (sol.tipo_empresa == "Publico")
                datos_reporte.tipo_empresa = "Sector Publico";
            if (sol.tipo_empresa == "Privado")
                datos_reporte.tipo_empresa = "Sector Privado";
            if (sol.tipo_empresa == "Comerciante")
                datos_reporte.tipo_empresa = "Comerciante";
            if (sol.tipo_empresa == "Otros")
                datos_reporte.tipo_empresa = "Otros, especificar :";
            datos_reporte.tipo_empresa_especificar = sol.tipo_empresa_especificar;

            datos_reporte.patrono = sol.patrono;
            datos_reporte.depto_labora = sol.depto_labora;
            datos_reporte.cargo = sol.cargo;
            datos_reporte.ingresos = sol.ingresos;
            datos_reporte.otros_ingresos = sol.otros_ingresos;
            datos_reporte.deducciones = sol.deducciones;
            datos_reporte.antiguedad_laboral = sol.antiguedad_laboral;
            datos_reporte.telefono_laboral1 = sol.telefono_laboral1;
            datos_reporte.ext_laboral1 = sol.ext_laboral1;
            datos_reporte.telefono_laboral2 = sol.telefono_laboral2;
            datos_reporte.ext_laboral2 = sol.ext_laboral2;
            datos_reporte.correo_laboral = sol.correo_laboral;
            datos_reporte.direccion_laboral = sol.direccion_laboral;

            //Nuevos - FELVIR01 - 20190722
            datos_reporte.EdadCodeudor = sol.EdadCodeudor;
            datos_reporte.EdadAval1 = sol.EdadAval1;
            datos_reporte.EdadAval2 = sol.EdadAval2;
            datos_reporte.Edad = sol.EdadPresta;
            datos_reporte.RTN = sol.RTN;
			datos_reporte.RTNAval1 = sol.RTN_Aval1;
			datos_reporte.RTNAval2 = sol.RTN_Aval2;
			datos_reporte.RTNCodeudor = sol.RTN_Codeudor;
			datos_reporte.FechaIngresoLaboral = sol.FechaIngresoLaboral;
			datos_reporte.FechaIngresoLaboralAval1 = sol.FechaIngresoLaboralAval1;
			datos_reporte.FechaIngresoLaboralAval2 = sol.FechaIngresoLaboralAval2;
			datos_reporte.FechaIngresoLaboralCodeudor = sol.FechaIngresoLaboralCodeudor;
			datos_reporte.TipoContrato = sol.TipoContrato;
			switch (sol.NivelEducConyuge)
			{
				case "P":
					datos_reporte.NivelEducativoconyuge = "Primaria";
					break;
				case "S":
					datos_reporte.NivelEducativoconyuge = "Secundaria";
					break;
				case "U":
					datos_reporte.NivelEducativoconyuge = "Universitaria";
					break;
				case "G":
					datos_reporte.NivelEducativoconyuge = "Postgrado";
					break;
				default:
					break;
			}
			datos_reporte.TipoViviendaAval1 = sol.TipoViviendaAval1;
			datos_reporte.TipoViviendaAval2 = sol.TipoViviendaAval2;
			datos_reporte.TipoViviendaCodeudor = sol.TipoViviendaCodeudor;
			requiereGarante = sol.RequiereGarante;

			///////////////              Conyuge   ////////////////////////////////
			datos_reporte.no_identificacion_conyuge = sol.no_identificacion_conyuge;
            datos_reporte.nombres_conyuge = sol.nombres_conyuge;
            datos_reporte.primer_nombre_conyuge = sol.primer_nombre_conyuge;
            datos_reporte.segundo_nombre_conyuge = sol.segundo_nombre_conyuge;
            datos_reporte.primer_apellido_conyuge = sol.primer_apellido_conyuge;
            datos_reporte.segundo_apellido_conyuge = sol.segundo_apellido_conyuge;
            datos_reporte.dependientes_hijos_conyuge = sol.dependientes_hijos_conyuge;
            datos_reporte.dependientes_otros_conyuge = sol.dependientes_otros_conyuge;
            datos_reporte.direccion_residencia_conyuge = sol.direccion_residencia_conyuge;
            datos_reporte.telefono_fijo_conyuge = sol.telefono_fijo_conyuge;
            datos_reporte.celular_conyuge = sol.celular_conyuge;
            datos_reporte.telefono_adicional1_conyuge = sol.telefono_adicional1_conyuge;
            datos_reporte.telefono_adicional2_conyuge = sol.telefono_adicional2_conyuge;
            datos_reporte.correo_conyuge = sol.correo_conyuge;
            if (sol.sexo_conyuge == "M")
            {
                datos_reporte.sexo_conyuge = "Masculino";
            }
            if (sol.sexo_conyuge == "F")
            {
                datos_reporte.sexo_conyuge = "Femenino";
            }

            if (!string.IsNullOrEmpty(sol.codigo_cliente_conyuge) || sol.codigo_cliente_conyuge!="0")
            {
                datos_reporte.es_afiliado_conyuge = sol.es_afiliado_conyuge;
                datos_reporte.codigo_cliente_conyuge = "Codigo Cliente :" + sol.codigo_cliente_conyuge;
            }
            else
            {
                datos_reporte.es_afiliado_conyuge = "NO";
            }
            if (sol.tipo_empresa_conyuge == "Publico")
                datos_reporte.tipo_empresa_conyuge = "Sector Publico";
            if (sol.tipo_empresa_conyuge == "Privado")
                datos_reporte.tipo_empresa_conyuge = "Sector Privado";
            if (sol.tipo_empresa_conyuge == "Comerciante")
                datos_reporte.tipo_empresa_conyuge = "Comerciante";
            if (sol.tipo_empresa_conyuge == "Otros")
                datos_reporte.tipo_empresa_conyuge = "Otros, especificar :";
            datos_reporte.tipo_empresa_especificar_conyuge = sol.tipo_empresa_especificar_conyuge;
            datos_reporte.patrono_conyuge = sol.patrono_conyuge;
            datos_reporte.depto_labora_conyuge = sol.depto_labora_conyuge;
            datos_reporte.cargo_conyuge = sol.cargo_conyuge;
            datos_reporte.antiguedad_conyuge = sol.antiguedad_conyuge;
            datos_reporte.ingresos_conyuge = sol.ingresos_conyuge;
            datos_reporte.otros_ingresos_conyuge = sol.otros_ingresos_conyuge;
            datos_reporte.deducciones_conyuge = sol.deducciones_conyuge;
            datos_reporte.telefono_laboral1_conyuge = sol.telefono_laboral1_conyuge;
            datos_reporte.ext_laboral1_conyuge = sol.ext_laboral1_conyuge;
            datos_reporte.telefono_laboral2_conyuge = sol.telefono_laboral2_conyuge;
            datos_reporte.ext_laboral2_conyuge = sol.ext_laboral2_conyuge;
            datos_reporte.correo_laboral_conyuge = sol.correo_laboral_conyuge;
            datos_reporte.direccion_laboral_conyuge = sol.direccion_laboral_conyuge;

            /////////////////////codeudor /////////////////////////////////////////
            datos_reporte.no_identificacion_codeudor = sol.no_identificacion_codeudor;
            datos_reporte.nombres_codeudor = sol.nombres_codeudor;
            datos_reporte.primer_nombre_codeudor = sol.primer_nombre_codeudor;
            datos_reporte.segundo_nombre_codeudor = sol.segundo_nombre_codeudor;
            datos_reporte.primer_apellido_codeudor = sol.primer_apellido_codeudor;
            datos_reporte.segundo_apellido_codeudor = sol.segundo_apellido_codeudor;
            datos_reporte.dependientes_hijos_codeudor = sol.dependientes_hijos_codeudor;
            datos_reporte.dependientes_otros_codeudor = sol.dependientes_otros_codeudor;
            datos_reporte.direccion_residencia_codeudor = sol.direccion_residencia_codeudor;
            datos_reporte.telefono_fijo_codeudor = sol.telefono_fijo_codeudor;
            datos_reporte.celular_codeudor = sol.celular_codeudor;
            datos_reporte.telefono_adicional1_codeudor = sol.telefono_adicional1_codeudor;
            datos_reporte.telefono_adicional2_codeudor = sol.telefono_adicional2_codeudor;
            datos_reporte.correo_codeudor = sol.correo_codeudor;
            if (sol.sexo_codeudor == "M")
            {
                datos_reporte.sexo_codeudor = "Masculino";
            }
            if (sol.sexo_codeudor == "F")
            {
                datos_reporte.sexo_codeudor = "Femenino";
            }
            if (!string.IsNullOrEmpty(sol.codigo_cliente_codeudor))
            {
                datos_reporte.es_afiliado_codeudor = sol.es_afiliado_codeudor;
                datos_reporte.codigo_cliente_codeudor = "Codigo Cliente :" + sol.codigo_cliente_codeudor;
            }
            else
            {
                datos_reporte.es_afiliado_codeudor = "NO";
            }
            if (sol.tipo_empresa_codeudor == "Publico")
                datos_reporte.tipo_empresa_codeudor = "Sector Publico";
            if (sol.tipo_empresa_codeudor == "Privado")
                datos_reporte.tipo_empresa_codeudor = "Sector Privado";
            if (sol.tipo_empresa_codeudor == "Comerciante")
                datos_reporte.tipo_empresa_codeudor = "Comerciante";
            if (sol.tipo_empresa_codeudor == "Otros") //Pendiente
                datos_reporte.tipo_empresa_codeudor = "Otros, especificar :";

            datos_reporte.tipo_empresa_especificar_codeudor = sol.tipo_empresa_especificar_codeudor;
            datos_reporte.patrono_codeudor = sol.patrono_codeudor;
            datos_reporte.depto_labora_codeudor = sol.depto_labora_codeudor;
            datos_reporte.cargo_codeudor = sol.cargo_codeudor;
            datos_reporte.antiguedad_codeudor = sol.antiguedad_codeudor;
            datos_reporte.ingresos_codeudor = sol.ingresos_codeudor;
            datos_reporte.otros_ingresos_codeudor = sol.otros_ingresos_codeudor;
            datos_reporte.deducciones_codeudor = sol.deducciones_codeudor;
            datos_reporte.telefono_laboral1_codeudor = sol.telefono_laboral1_codeudor;
            datos_reporte.ext_laboral1_codeudor = sol.ext_laboral1_codeudor;
            datos_reporte.telefono_laboral2_codeudor = sol.telefono_laboral2_codeudor;
            datos_reporte.ext_laboral2_codeudor = sol.ext_laboral2_codeudor;
            datos_reporte.correo_laboral_codeudor = sol.correo_laboral_codeudor;
            datos_reporte.direccion_laboral_codeudor = sol.direccion_laboral_codeudor;

            /////////////////////////Aval # 1///////////////////////////////////////////////////            
            datos_reporte.no_identificacion_aval1 = sol.no_identificacion_aval1;
            datos_reporte.nombres_aval1 = sol.nombres_aval1;
            datos_reporte.primer_nombre_aval1 = sol.primer_nombre_aval1;
            datos_reporte.segundo_nombre_aval1 = sol.segundo_nombre_aval1;
            datos_reporte.primer_apellido_aval1 = sol.primer_apellido_aval1;
            datos_reporte.segundo_apellido_aval1 = sol.segundo_apellido_aval1;
            datos_reporte.dependientes_hijos_aval1 = sol.dependientes_hijos_aval1;
            datos_reporte.dependientes_otros_aval1 = sol.dependientes_otros_aval1;
            datos_reporte.direccion_residencia_aval1 = sol.direccion_residencia_aval1;
            datos_reporte.telefono_fijo_aval1 = sol.telefono_fijo_aval1;
            datos_reporte.celular_aval1 = sol.celular_aval1;
            datos_reporte.telefono_adicional1_aval1 = sol.telefono_adicional1_aval1;
            datos_reporte.telefono_adicional2_aval1 = sol.telefono_adicional2_aval1;
            datos_reporte.correo_aval1 = sol.correo_aval1;
            if (sol.sexo_aval1 == "M")
            {
                datos_reporte.sexo_aval1 = "Masculino";
            }
            if (sol.sexo_aval1 == "F")
            {
                datos_reporte.sexo_aval1 = "Femenino";
            }

            if (!string.IsNullOrEmpty(sol.codigo_cliente_aval1))
            {
                datos_reporte.es_afiliado_aval1 = "SI";
                datos_reporte.codigo_cliente_aval1 = "Codigo Cliente :" + sol.codigo_cliente_aval1;
            }
            else
            {
                datos_reporte.es_afiliado_aval1 = "NO";
            }
            if (sol.tipo_empresa_aval1 == "Publico")
                datos_reporte.tipo_empresa_aval1 = "Sector Publico";
            if (sol.tipo_empresa_aval1 == "Privado")
                datos_reporte.tipo_empresa_aval1 = "Sector Privado";
            if (sol.tipo_empresa_aval1 == "Comerciante")
                datos_reporte.tipo_empresa_aval1 = "Comerciante";
            if (sol.tipo_empresa_aval1 == "Otros") //Pendiente
                datos_reporte.tipo_empresa_aval1 = "Otros, especificar :";
            datos_reporte.tipo_empresa_especificar_aval1 = sol.tipo_empresa_especificar_aval1;
            datos_reporte.patrono_aval1 = sol.patrono_aval1;
            datos_reporte.depto_labora_aval1 = sol.depto_labora_aval1;
            datos_reporte.cargo_aval1 = sol.cargo_aval1;
            datos_reporte.antiguedad_aval1 = sol.antiguedad_aval1;
            datos_reporte.ingresos_aval1 = sol.ingresos_aval1;
            datos_reporte.otros_ingresos_aval1 = sol.otros_ingresos_aval1;
            datos_reporte.deducciones_aval1 = sol.deducciones_aval1;
            datos_reporte.telefono_laboral1_aval1 = sol.telefono_laboral1_aval1;
            datos_reporte.ext_laboral1_aval1 = sol.ext_laboral1_aval1;
            datos_reporte.telefono_laboral2_aval1 = sol.telefono_laboral2_aval1;
            datos_reporte.ext_laboral2_aval1 = sol.ext_laboral2_aval1;
            datos_reporte.correo_laboral_aval1 = sol.correo_laboral_aval1;
            datos_reporte.direccion_laboral_aval1 = sol.direccion_laboral_aval1;
			datos_reporte.EdadAval1 = sol.EdadAval1;
			datos_reporte.EstadoCivilAval1 = sol.EstadoCivilAval1;

            /////////////////////////Aval # 1///////////////////////////////////////////////////            
            datos_reporte.no_identificacion_aval2 = sol.no_identificacion_aval2;
            datos_reporte.nombres_aval2 = sol.nombres_aval2;
            datos_reporte.primer_nombre_aval2 = sol.primer_nombre_aval2;
            datos_reporte.segundo_nombre_aval2 = sol.segundo_nombre_aval2;
            datos_reporte.primer_apellido_aval2 = sol.primer_apellido_aval2;
            datos_reporte.segundo_apellido_aval2 = sol.segundo_apellido_aval2;
            datos_reporte.dependientes_hijos_aval2 = sol.dependientes_hijos_aval2;
            datos_reporte.dependientes_otros_aval2 = sol.dependientes_otros_aval2;
            datos_reporte.direccion_residencia_aval2 = sol.direccion_residencia_aval2;
            datos_reporte.telefono_fijo_aval2 = sol.telefono_fijo_aval2;
            datos_reporte.celular_aval2 = sol.celular_aval2;
            datos_reporte.telefono_adicional1_aval2 = sol.telefono_adicional1_aval2;
            datos_reporte.telefono_adicional2_aval2 = sol.telefono_adicional2_aval2;
            datos_reporte.correo_aval2 = sol.correo_aval2;
            if (sol.sexo_aval2 == "M")
            {
                datos_reporte.sexo_aval2 = "Masculino";
            }
            if (sol.sexo_aval2 == "F")
            {
                datos_reporte.sexo_aval2 = "Femenino";
            }

            if (!string.IsNullOrEmpty(sol.codigo_cliente_aval2))
            {
                datos_reporte.es_afiliado_aval2 = "SI";
                datos_reporte.codigo_cliente_aval2 = "Codigo Cliente :" + sol.codigo_cliente_aval2;
            }
            else
            {
                datos_reporte.es_afiliado_aval2 = "NO";
            }
            if (sol.tipo_empresa_aval2 == "Publico")
                datos_reporte.tipo_empresa_aval2 = "Sector Publico";
            if (sol.tipo_empresa_aval2 == "Privado")
                datos_reporte.tipo_empresa_aval2 = "Sector Privado";
            if (sol.tipo_empresa_aval2 == "Comerciante")
                datos_reporte.tipo_empresa_aval2 = "Comerciante";
            if (sol.tipo_empresa_aval2 == "Otros") //Pendiente
                datos_reporte.tipo_empresa_aval2 = "Otros, especificar :";
            datos_reporte.tipo_empresa_especificar_aval2 = sol.tipo_empresa_especificar_aval2;
            datos_reporte.patrono_aval2 = sol.patrono_aval2;
            datos_reporte.depto_labora_aval2 = sol.depto_labora_aval2;
            datos_reporte.cargo_aval2 = sol.cargo_aval2;
            datos_reporte.antiguedad_aval2 = sol.antiguedad_aval2;
            datos_reporte.ingresos_aval2 = sol.ingresos_aval2;
            datos_reporte.otros_ingresos_aval2 = sol.otros_ingresos_aval2;
            datos_reporte.deducciones_aval2 = sol.deducciones_aval2;
            datos_reporte.telefono_laboral2_aval1 = sol.telefono_laboral1_aval2;
            datos_reporte.ext_laboral1_aval2 = sol.ext_laboral1_aval2;
            datos_reporte.telefono_laboral2_aval2 = sol.telefono_laboral2_aval2;
            datos_reporte.ext_laboral2_aval2 = sol.ext_laboral2_aval2;
            datos_reporte.correo_laboral_aval2 = sol.correo_laboral_aval2;
            datos_reporte.direccion_laboral_aval2 = sol.direccion_laboral_aval2;
			datos_reporte.EdadAval2 = sol.EdadAval2;
			datos_reporte.EstadoCivilAval2= sol.EstadoCivilAval2;


			string xml = da.ObtenerReferenciasxSolicitud2(p_no_solicitud);

            StringReader theReader = new StringReader(xml);
            DataSet theDataSet = new DataSet();
            theDataSet.ReadXml(theReader);
            var dt = theDataSet.Tables[1];
            // On all tables' rows
            foreach (DataRow dtRow in dt.Rows)
            {
                #region Principal
                if (dtRow["Rol"].ToString() == "Principal")
                {
                    if (dtRow["no_referencia"].ToString() == "1")
                    {
                        datos_reporte.Ref1 = dtRow["Nombre"].ToString();
                        datos_reporte.Ref1_direc = dtRow["DirecRes"].ToString();
                        datos_reporte.Ref1_telef = dtRow["TelFijo"].ToString();
                        datos_reporte.Ref1_ptoref = dtRow["Ptoreferencia"].ToString();
                        datos_reporte.Ref1_Telcelular = dtRow["Telcelular"].ToString();

                    }
                    if (dtRow["no_referencia"].ToString() == "2")
                    {
                        datos_reporte.Ref2 = dtRow["Nombre"].ToString();
                        datos_reporte.Ref2_direc = dtRow["DirecRes"].ToString();
                        datos_reporte.Ref2_telef = dtRow["TelFijo"].ToString();
                        datos_reporte.Ref2_ptoref = dtRow["Ptoreferencia"].ToString();
                        datos_reporte.Ref2_Telcelular = dtRow["Telcelular"].ToString();
                    }
                    if (dtRow["no_referencia"].ToString() == "3")
                    {
                        datos_reporte.Ref3 = dtRow["Nombre"].ToString();
                        datos_reporte.Ref3_direc = dtRow["DirecRes"].ToString();
                        datos_reporte.Ref3_telef = dtRow["TelFijo"].ToString();
                        datos_reporte.Ref3_ptoref = dtRow["Ptoreferencia"].ToString();
                        datos_reporte.Ref3_Telcelular = dtRow["Telcelular"].ToString();
                    }
                }
                #endregion


                #region Codeudor
                if (dtRow["Rol"].ToString() == "Codeudor")
                {
                    if (dtRow["no_referencia"].ToString() == "1")
                    {
                        datos_reporte.Ref1_codeudor = dtRow["Nombre"].ToString();
                        datos_reporte.Ref1_direc_codeudor = dtRow["DirecRes"].ToString();
                        datos_reporte.Ref1_telef_codeudor = dtRow["TelFijo"].ToString();
                        datos_reporte.Ref1_ptoref_codeudor = dtRow["Ptoreferencia"].ToString();
                        datos_reporte.Ref1_Telcelular_codeudor = dtRow["Telcelular"].ToString();

                    }
                    if (dtRow["no_referencia"].ToString() == "2")
                    {
                        datos_reporte.Ref2_codeudor = dtRow["Nombre"].ToString();
                        datos_reporte.Ref2_direc_codeudor = dtRow["DirecRes"].ToString();
                        datos_reporte.Ref2_telef_codeudor = dtRow["TelFijo"].ToString();
                        datos_reporte.Ref2_ptoref_codeudor = dtRow["Ptoreferencia"].ToString();
                        datos_reporte.Ref2_Telcelular_codeudor = dtRow["Telcelular"].ToString();
                    }
                    if (dtRow["no_referencia"].ToString() == "3")
                    {
                        datos_reporte.Ref3_codeudor = dtRow["Nombre"].ToString();
                        datos_reporte.Ref3_direc_codeudor = dtRow["DirecRes"].ToString();
                        datos_reporte.Ref3_telef_codeudor = dtRow["TelFijo"].ToString();
                        datos_reporte.Ref3_ptoref_codeudor = dtRow["Ptoreferencia"].ToString();
                        datos_reporte.Ref3_Telcelular_codeudor = dtRow["Telcelular"].ToString();
                    }
                }
                #endregion

                #region Aval1
                if (dtRow["Rol"].ToString() == "Aval1")
                {
                    if (dtRow["no_referencia"].ToString() == "1")
                    {
                        datos_reporte.Ref1_aval1 = dtRow["Nombre"].ToString();
                        datos_reporte.Ref1_direc_aval1 = dtRow["DirecRes"].ToString();
                        datos_reporte.Ref1_telef_aval1 = dtRow["TelFijo"].ToString();
                        datos_reporte.Ref1_ptoref_aval1 = dtRow["Ptoreferencia"].ToString();
                        datos_reporte.Ref1_Telcelular_aval1 = dtRow["Telcelular"].ToString();

                    }
                    if (dtRow["no_referencia"].ToString() == "2")
                    {
                        datos_reporte.Ref2_aval1 = dtRow["Nombre"].ToString();
                        datos_reporte.Ref2_direc_aval1 = dtRow["DirecRes"].ToString();
                        datos_reporte.Ref2_telef_aval1 = dtRow["TelFijo"].ToString();
                        datos_reporte.Ref2_ptoref_aval1 = dtRow["Ptoreferencia"].ToString();
                        datos_reporte.Ref2_Telcelular_aval1 = dtRow["Telcelular"].ToString();
                    }
                    if (dtRow["no_referencia"].ToString() == "3")
                    {
                        datos_reporte.Ref3_aval1 = dtRow["Nombre"].ToString();
                        datos_reporte.Ref3_direc_aval1 = dtRow["DirecRes"].ToString();
                        datos_reporte.Ref3_telef_aval1 = dtRow["TelFijo"].ToString();
                        datos_reporte.Ref3_ptoref_aval1 = dtRow["Ptoreferencia"].ToString();
                        datos_reporte.Ref3_Telcelular_aval1 = dtRow["Telcelular"].ToString();
                    }
                }
                #endregion

                #region Aval2
                if (dtRow["Rol"].ToString() == "Aval2")
                {
                    if (dtRow["no_referencia"].ToString() == "1")
                    {
                        datos_reporte.Ref1_aval2 = dtRow["Nombre"].ToString();
                        datos_reporte.Ref1_direc_aval2 = dtRow["DirecRes"].ToString();
                        datos_reporte.Ref1_telef_aval2 = dtRow["TelFijo"].ToString();
                        datos_reporte.Ref1_ptoref_aval2 = dtRow["Ptoreferencia"].ToString();
                        datos_reporte.Ref1_Telcelular_aval2 = dtRow["Telcelular"].ToString();

                    }
                    if (dtRow["no_referencia"].ToString() == "2")
                    {
                        datos_reporte.Ref2_aval2 = dtRow["Nombre"].ToString();
                        datos_reporte.Ref2_direc_aval2 = dtRow["DirecRes"].ToString();
                        datos_reporte.Ref2_telef_aval2 = dtRow["TelFijo"].ToString();
                        datos_reporte.Ref2_ptoref_aval2 = dtRow["Ptoreferencia"].ToString();
                        datos_reporte.Ref2_Telcelular_aval2 = dtRow["Telcelular"].ToString();
                    }
                    if (dtRow["no_referencia"].ToString() == "3")
                    {
                        datos_reporte.Ref3_aval2 = dtRow["Nombre"].ToString();
                        datos_reporte.Ref3_direc_aval2 = dtRow["DirecRes"].ToString();
                        datos_reporte.Ref3_telef_aval2 = dtRow["TelFijo"].ToString();
                        datos_reporte.Ref3_ptoref_aval2 = dtRow["Ptoreferencia"].ToString();
                        datos_reporte.Ref3_Telcelular_aval2 = dtRow["Telcelular"].ToString();
                    }
                }
                #endregion
            }

			#region Garante
			DataTable dtGar = this.da.ObtenerGarante(p_no_solicitud);
			if(dtGar.Rows.Count > 0)
			{
				{
					datos_reporte.IdentidadGarante = dtGar.Rows[0]["no_identificacion"].ToString();
					datos_reporte.PrimerNombreGarante = dtGar.Rows[0]["primer_nombre_g"].ToString();
					datos_reporte.SegundoNombreGarante = dtGar.Rows[0]["segundo_nombre_g"].ToString();
					datos_reporte.PrimerApellidoGarante = dtGar.Rows[0]["primer_apellido_g"].ToString();
					datos_reporte.SegundoApellidoGarante = dtGar.Rows[0]["segundo_apellido_g"].ToString();
					datos_reporte.HijosGarante = Convert.ToInt32(dtGar.Rows[0]["dependientes_hijos_g"].ToString());
					datos_reporte.OtrosDependientesGarante = Convert.ToInt32(dtGar.Rows[0]["dependientes_otros_g"].ToString());
					datos_reporte.edadGarante = dtGar.Rows[0]["edad_garante"].ToString(); ;
					datos_reporte.DireccionGarante = dtGar.Rows[0]["direccion_residencial_g"].ToString();
					datos_reporte.TelefonoFijoGarante = dtGar.Rows[0]["telefono_fijo_garante"].ToString();
					datos_reporte.CelularGarante = dtGar.Rows[0]["celular_garante"].ToString();
					datos_reporte.TelAdicionalGarante1 = dtGar.Rows[0]["telefono_adic1_garante"].ToString();
					datos_reporte.TelAdicionalGarante2 = dtGar.Rows[0]["telefono_adic2_garante"].ToString();
					datos_reporte.CorreoGarante = dtGar.Rows[0]["correo_garante"].ToString();
					if (dtGar.Rows[0]["es_afiliado_garante"].ToString().Equals("S"))
					{
						datos_reporte.EsAfiliadoGarante = "Si";
						datos_reporte.CodigoClienteGarante = Convert.ToInt32(dtGar.Rows[0]["codigo_cliente_g"].ToString());
					}
					else
					{
						datos_reporte.EsAfiliadoGarante = "No";
					}
					if (dtGar.Rows[0]["genero_garante"].ToString().Equals("F"))
					{
						datos_reporte.GeneroGarante = "Femenino";
					}
					else
					{
						datos_reporte.GeneroGarante = "Masculino";
					}

					datos_reporte.TipoEmpresaGarante = dtGar.Rows[0]["tipo_empresa_garante"].ToString();


					datos_reporte.TipoViviendaGarante = dtGar.Rows[0]["TIPO_VIVIENDA_GAR"].ToString();
				
					datos_reporte.TipoViviendaGaranteOtros = dtGar.Rows[0]["TIPO_VIVIENDA_OTROS_GAR"].ToString();

					datos_reporte.PatronoGarante = dtGar.Rows[0]["patrono_garante"].ToString();
					datos_reporte.DeptoLabGarante = dtGar.Rows[0]["depto_labora_garante"].ToString();
					datos_reporte.PosicionGarante = dtGar.Rows[0]["cargo_garante"].ToString();
					datos_reporte.AntiguedadLabGarante = dtGar.Rows[0]["antiguedad_laboral_garante"].ToString();
					datos_reporte.TelLab1Garante = dtGar.Rows[0]["telefono_laboral1_garante"].ToString();
					datos_reporte.TelLab2Garante = dtGar.Rows[0]["telefono_laboral2_garante"].ToString();
					datos_reporte.Extension1Garante = dtGar.Rows[0]["ext_laboral1_garante"].ToString();
					datos_reporte.Extension2Garante = dtGar.Rows[0]["ext_laboral2_garante"].ToString();
					datos_reporte.DireccionLabGarante = dtGar.Rows[0]["direccion_laboral_garante"].ToString();
					datos_reporte.CorreoGarante = dtGar.Rows[0]["correo_laboral_garante"].ToString();
					datos_reporte.NombreConyugeGarante = dtGar.Rows[0]["nombre_conyuge_garante"].ToString();
					datos_reporte.DirLaboralGarante = dtGar.Rows[0]["direc_lab_conyuge_garante"].ToString();
					datos_reporte.CargoConyugeGarante = dtGar.Rows[0]["cargo_conyuge_garante"].ToString();
					//Referencias
					datos_reporte.Ref1_garante = dtGar.Rows[0]["nombre_ref1_garante"].ToString();
					datos_reporte.Ref1_direc_garante = dtGar.Rows[0]["direccion_ref1_garante"].ToString();
					datos_reporte.Ref1_telef_garante = dtGar.Rows[0]["telefono_ref1_garante"].ToString();
					datos_reporte.Ref1_ptoref_garante = dtGar.Rows[0]["localizacion_ref1_garante"].ToString();
					datos_reporte.Ref1_Telcelular_garante = dtGar.Rows[0]["celular_ref1_garante"].ToString();
					datos_reporte.Ref2_garante = dtGar.Rows[0]["nombre_ref2_garante"].ToString();
					datos_reporte.Ref2_direc_garante = dtGar.Rows[0]["direccion_ref2_garante"].ToString();
					datos_reporte.Ref2_telef_garante = dtGar.Rows[0]["telefono_ref2_garante"].ToString();
					datos_reporte.Ref2_ptoref_garante = dtGar.Rows[0]["localizacion_ref2_garante"].ToString();
					datos_reporte.Ref2_Telcelular_garante = dtGar.Rows[0]["celular_ref2_garante"].ToString();
					datos_reporte.Ref3_garante = dtGar.Rows[0]["nombre_ref3_garante"].ToString();
					datos_reporte.Ref3_direc_garante = dtGar.Rows[0]["direccion_ref3_garante"].ToString();
					datos_reporte.Ref3_telef_garante = dtGar.Rows[0]["telefono_ref3_garante"].ToString();

				}
			}
			#endregion

			///////////////////////////
			data_form = datos_reporte;
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
    }

    public class rtp_solicitud_Clase
    {
        //generales solicitud
        public Int32 no_solicitud { get; set; }
        public Int16 workflow_id { get; set; }
        public string usuario_workflow { get; set; }
        public string nombre_usuario { get; set; }
        public Int32 codigo_agencia { get; set; }
        public string descripcion_fuente { get; set; }
        public string nombre_agencia { get; set; }
        public string fecha_solicitud { get; set; }
        public int codigo_sub_aplicacion { get; set; }
        public string desc_sub_aplicacion { get; set; }
        public string desc_moneda { get; set; }
        public int codigo_moneda { get; set; }
        public decimal monto_solicitado { get; set; }
        public Int16 plazo { get; set; }
        public decimal tasa { get; set; }
        public string destino_credito { get; set; }
        public string descripcion_destino { get; set; }       
        public string es_consolidacion { get; set; }
        //solicitante                
        public Int32 codigo_cliente { get; set; }
        public string no_identificacion { get; set; }
        public string nombres_solicitante { get; set; }
        public string primer_nombre { get; set; }
        public string segundo_nombre { get; set; }
        public string primer_apellido { get; set; }
        public string segundo_apellido { get; set; }
        public string apellido_casada { get; set; }
        public string sexo { get; set; }
        public string estado_civil { get; set; }
        public string ventanilla_planilla { get; set; }
        public string fecha_nacimiento { get; set; }
        public string lugar_nacimiento { get; set; }
        public string nacionalidad { get; set; }
        public DateTime? fecha_ingreso_coop { get; set; }
        public string nivel_educativo { get; set; }
        public string profesion_oficio { get; set; }
        public string tipo_vivienda { get; set; }
        public string tipo_vivienda_especificar { get; set; }
        public string direccion_residencia { get; set; }
        public string telefono_fijo { get; set; }
        public string telefono_celular { get; set; }
        public string telefono_adicional1 { get; set; }
        public string telefono_adicional2 { get; set; }
        public string correo_electronico { get; set; }
        public Int16 dependientes_hijos { get; set; }
        public Int16 dependientes_otros { get; set; }
        public string tipo_empresa { get; set; }
        public string tipo_empresa_especificar { get; set; }
        public string actividad_patrono_negocio { get; set; }
        public string patrono { get; set; }
        public string depto_labora { get; set; }
        public string cargo { get; set; }
        public string antiguedad_laboral { get; set; }
        public decimal ingresos { get; set; }
        public decimal otros_ingresos { get; set; }
        public decimal deducciones { get; set; }
        public string telefono_laboral1 { get; set; }
        public string ext_laboral1 { get; set; }
        public string telefono_laboral2 { get; set; }
        public string ext_laboral2 { get; set; }
        public string fax_laboral1 { get; set; }
        public string direccion_laboral { get; set; }
        public string correo_laboral { get; set; }
        public string Ref1 { get; set; }
        public string Ref1_direc { get; set; }
        public string Ref1_telef { get; set; }
        public string Ref1_ptoref { get; set; }
        public string Ref1_Telcelular { get; set; }
        public string Ref2 { get; set; }
        public string Ref2_direc { get; set; }
        public string Ref2_telef { get; set; }
        public string Ref2_ptoref { get; set; }
        public string Ref2_Telcelular { get; set; }
        public string Ref3 { get; set; }
        public string Ref3_direc { get; set; }
        public string Ref3_telef { get; set; }
        public string Ref3_ptoref { get; set; }
        public string Ref3_Telcelular { get; set; }
		//Nuevas
		public int Edad { get; set; }
		public string RTN { get; set; }
		public string TipoContrato { get; set; }
		public string FechaIngresoLaboral { get; set; }
		//Conyuge
		public string no_identificacion_conyuge { get; set; }
        public string nombres_conyuge { get; set; }
        public string primer_nombre_conyuge { get; set; }
        public string segundo_nombre_conyuge { get; set; }
        public string primer_apellido_conyuge { get; set; }
        public string segundo_apellido_conyuge { get; set; }
        public string sexo_conyuge { get; set; }
        public int dependientes_hijos_conyuge { get; set; }
        public int dependientes_otros_conyuge { get; set; }
        public string direccion_residencia_conyuge { get; set; }
        public string telefono_fijo_conyuge { get; set; }
        public string celular_conyuge { get; set; }
        public string telefono_adicional1_conyuge { get; set; }
        public string telefono_adicional2_conyuge { get; set; }
        public string correo_conyuge { get; set; }
        public string es_afiliado_conyuge { get; set; }
        public string codigo_cliente_conyuge { get; set; }
        public string tipo_empresa_conyuge { get; set; }
        public string tipo_empresa_especificar_conyuge { get; set; }
        public string patrono_conyuge { get; set; }
        public string depto_labora_conyuge { get; set; }
        public string cargo_conyuge { get; set; }
        public string antiguedad_conyuge { get; set; }
        public decimal ingresos_conyuge { get; set; }
        public decimal otros_ingresos_conyuge { get; set; }
        public decimal deducciones_conyuge { get; set; }
        public string telefono_laboral1_conyuge { get; set; }
        public string ext_laboral1_conyuge { get; set; }
        public string telefono_laboral2_conyuge { get; set; }
        public string ext_laboral2_conyuge { get; set; }
        public string direccion_laboral_conyuge { get; set; }
        public string correo_laboral_conyuge { get; set; }
		public string NivelEducativoconyuge { get; set; }
		public string ProfesionOficioConyuge { get; set; }
		//Codeudor
		public string no_identificacion_codeudor { get; set; }
        public string nombres_codeudor { get; set; }
        public string primer_nombre_codeudor { get; set; }
        public string segundo_nombre_codeudor { get; set; }
        public string primer_apellido_codeudor { get; set; }
        public string segundo_apellido_codeudor { get; set; }
        public string sexo_codeudor { get; set; }
        public int dependientes_hijos_codeudor { get; set; }
        public int dependientes_otros_codeudor { get; set; }
        public string direccion_residencia_codeudor { get; set; }
        public string telefono_fijo_codeudor { get; set; }
        public string celular_codeudor { get; set; }
        public string telefono_adicional1_codeudor { get; set; }
        public string telefono_adicional2_codeudor { get; set; }
        public string correo_codeudor { get; set; }
        public string es_afiliado_codeudor { get; set; }
        public string codigo_cliente_codeudor { get; set; }
        public string tipo_empresa_codeudor { get; set; }
        public string tipo_empresa_especificar_codeudor { get; set; }
        public string patrono_codeudor { get; set; }
        public string depto_labora_codeudor { get; set; }
        public string cargo_codeudor { get; set; }
        public string antiguedad_codeudor { get; set; }
        public decimal ingresos_codeudor { get; set; }
        public decimal otros_ingresos_codeudor { get; set; }
        public decimal deducciones_codeudor { get; set; }
        public string telefono_laboral1_codeudor { get; set; }
        public string ext_laboral1_codeudor { get; set; }
        public string telefono_laboral2_codeudor { get; set; }
        public string ext_laboral2_codeudor { get; set; }
        public string direccion_laboral_codeudor { get; set; }
        public string correo_laboral_codeudor { get; set; }
        public string nombre_conyuge_codeudor { get; set; }
        public string direclab_conyuge_codeudor { get; set; }
        public string cargo_conyuge_codeudor { get; set; }
        public string Ref1_codeudor { get; set; }
        public string Ref1_direc_codeudor { get; set; }
        public string Ref1_telef_codeudor { get; set; }
        public string Ref1_ptoref_codeudor { get; set; }
        public string Ref1_Telcelular_codeudor { get; set; }
        public string Ref2_codeudor { get; set; }
        public string Ref2_direc_codeudor { get; set; }
        public string Ref2_telef_codeudor { get; set; }
        public string Ref2_ptoref_codeudor { get; set; }
        public string Ref2_Telcelular_codeudor { get; set; }
        public string Ref3_codeudor { get; set; }
        public string Ref3_direc_codeudor { get; set; }
        public string Ref3_telef_codeudor { get; set; }
        public string Ref3_ptoref_codeudor { get; set; }
        public string Ref3_Telcelular_codeudor { get; set; }
		public string RTNCodeudor { get; set; }
		public int EdadCodeudor { get; set; }
		public string TipoViviendaCodeudor { get; set; }
		public string FechaIngresoLaboralCodeudor { get; set; }
										   //Aval1
		public string no_identificacion_aval1 { get; set; }
        public string nombres_aval1 { get; set; }
        public string primer_nombre_aval1 { get; set; }
        public string segundo_nombre_aval1 { get; set; }
        public string primer_apellido_aval1 { get; set; }
        public string segundo_apellido_aval1 { get; set; }
        public string sexo_aval1 { get; set; }
        public int dependientes_hijos_aval1 { get; set; }
        public int dependientes_otros_aval1 { get; set; }
        public string direccion_residencia_aval1 { get; set; }
        public string telefono_fijo_aval1 { get; set; }
        public string celular_aval1 { get; set; }
        public string telefono_adicional1_aval1 { get; set; }
        public string telefono_adicional2_aval1 { get; set; }
        public string correo_aval1 { get; set; }
        public string es_afiliado_aval1 { get; set; }
        public string codigo_cliente_aval1 { get; set; }
        public string tipo_empresa_aval1 { get; set; }
        public string tipo_empresa_especificar_aval1 { get; set; }
        public string patrono_aval1 { get; set; }
        public string depto_labora_aval1 { get; set; }
        public string cargo_aval1 { get; set; }
        public string antiguedad_aval1 { get; set; }
        public decimal ingresos_aval1 { get; set; }
        public decimal otros_ingresos_aval1 { get; set; }
        public decimal deducciones_aval1 { get; set; }
        public string telefono_laboral1_aval1 { get; set; }
        public string ext_laboral1_aval1 { get; set; }
        public string telefono_laboral2_aval1 { get; set; }
        public string ext_laboral2_aval1 { get; set; }
        public string direccion_laboral_aval1 { get; set; }
        public string correo_laboral_aval1 { get; set; }
        public string nombre_conyuge_aval1 { get; set; }
        public string direclab_conyuge_aval1 { get; set; }
        public string cargo_conyuge_aval1 { get; set; }
        public string Ref1_aval1 { get; set; }
        public string Ref1_direc_aval1 { get; set; }
        public string Ref1_telef_aval1 { get; set; }
        public string Ref1_ptoref_aval1 { get; set; }
        public string Ref1_Telcelular_aval1 { get; set; }
        public string Ref2_aval1 { get; set; }
        public string Ref2_direc_aval1 { get; set; }
        public string Ref2_telef_aval1 { get; set; }
        public string Ref2_ptoref_aval1 { get; set; }
        public string Ref2_Telcelular_aval1 { get; set; }
        public string Ref3_aval1 { get; set; }
        public string Ref3_direc_aval1 { get; set; }
        public string Ref3_telef_aval1 { get; set; }
        public string Ref3_ptoref_aval1 { get; set; }
        public string Ref3_Telcelular_aval1 { get; set; }
		public string RTNAval1 { get; set; }
		public string EstadoCivilAval1 { get; set; }
		public int EdadAval1 { get; set; }
		public string TipoViviendaAval1 { get; set; }
		public string OtrosViviendaAval1 { get; set; }
		public string FechaIngresoLaboralAval1 { get; set; }
		//Aval2
		public string no_identificacion_aval2 { get; set; }
        public string nombres_aval2 { get; set; }
        public string primer_nombre_aval2 { get; set; }
        public string segundo_nombre_aval2 { get; set; }
        public string primer_apellido_aval2 { get; set; }
        public string segundo_apellido_aval2 { get; set; }
        public string sexo_aval2 { get; set; }
        public int dependientes_hijos_aval2 { get; set; }
        public int dependientes_otros_aval2 { get; set; }
        public string direccion_residencia_aval2 { get; set; }
        public string telefono_fijo_aval2 { get; set; }
        public string celular_aval2 { get; set; }
        public string telefono_adicional1_aval2 { get; set; }
        public string telefono_adicional2_aval2 { get; set; }
        public string correo_aval2 { get; set; }
        public string es_afiliado_aval2 { get; set; }
        public string codigo_cliente_aval2 { get; set; }
        public string tipo_empresa_aval2 { get; set; }
        public string tipo_empresa_especificar_aval2 { get; set; }
        public string patrono_aval2 { get; set; }
        public string depto_labora_aval2 { get; set; }
        public string cargo_aval2 { get; set; }
        public string antiguedad_aval2 { get; set; }
        public decimal ingresos_aval2 { get; set; }
        public decimal otros_ingresos_aval2 { get; set; }
        public decimal deducciones_aval2 { get; set; }
        public string telefono_laboral1_aval2 { get; set; }
        public string ext_laboral1_aval2 { get; set; }
        public string telefono_laboral2_aval2 { get; set; }
        public string ext_laboral2_aval2 { get; set; }
        public string direccion_laboral_aval2 { get; set; }
        public string correo_laboral_aval2 { get; set; }
        public string nombre_conyuge_aval2 { get; set; }
        public string direclab_conyuge_aval2 { get; set; }
        public string cargo_conyuge_aval2 { get; set; }
        public string Ref1_aval2 { get; set; }
        public string Ref1_direc_aval2 { get; set; }
        public string Ref1_telef_aval2 { get; set; }
        public string Ref1_ptoref_aval2 { get; set; }
        public string Ref1_Telcelular_aval2 { get; set; }
        public string Ref2_aval2 { get; set; }
        public string Ref2_direc_aval2 { get; set; }
        public string Ref2_telef_aval2 { get; set; }
        public string Ref2_ptoref_aval2 { get; set; }
        public string Ref2_Telcelular_aval2 { get; set; }
        public string Ref3_aval2 { get; set; }
        public string Ref3_direc_aval2 { get; set; }
        public string Ref3_telef_aval2 { get; set; }
        public string Ref3_ptoref_aval2 { get; set; }
        public string Ref3_Telcelular_aval2 { get; set; }
		public string RTNAval2 { get; set; }
		public string EstadoCivilAval2 { get; set; }
		public int EdadAval2 { get; set; }
		public string TipoViviendaAval2 { get; set; }
		public string OtrosViviendaAval2 { get; set; }
		public string FechaIngresoLaboralAval2 { get; set; }
		//Garante
		public string IdentidadGarante { get; set; }
		public string PrimerNombreGarante { get; set; }
		public string SegundoNombreGarante { get; set; }
		public string PrimerApellidoGarante { get; set; }
		public string SegundoApellidoGarante { get; set; }
		public string GeneroGarante { get; set; }
		public int HijosGarante { get; set; }
        public string edadGarante { get; set; }
        public int OtrosDependientesGarante { get; set; }
		public string DireccionGarante { get; set; }
		public string TelefonoFijoGarante { get; set; }
		public string CelularGarante { get; set; }
		public string TelAdicionalGarante1 { get; set; }
		public string TelAdicionalGarante2 { get; set; }
		public string CorreoGarante { get; set; }
        public string EsAfiliadoGarante { get; set; }
		public int CodigoClienteGarante { get; set; }
		public string TipoViviendaGarante { get; set; }
		public string TipoViviendaGaranteOtros { get; set; }
		public string TipoEmpresaGarante { get; set; }
		public string TipoEmpresaOtrosGarante { get; set; }
		public string PatronoGarante { get; set; }
		public string DeptoLabGarante { get; set; }
		public string PosicionGarante { get; set; }
		public string AntiguedadLabGarante { get; set; }
		public string TelLab1Garante { get; set; }
		public string TelLab2Garante { get; set; }
		public string Extension1Garante { get; set; }
		public string Extension2Garante { get; set; }
		public string DireccionLabGarante { get; set; }
		public string NombreConyugeGarante { get; set; }
		public string DirLaboralGarante { get; set; }
		public string CargoConyugeGarante { get; set; }
		//Referencias
		public string Ref1_garante { get; set; }
		public string Ref1_direc_garante { get; set; }
		public string Ref1_telef_garante { get; set; }
		public string Ref1_ptoref_garante { get; set; }
		public string Ref1_Telcelular_garante { get; set; }
		public string Ref2_garante { get; set; }
		public string Ref2_direc_garante { get; set; }
		public string Ref2_telef_garante { get; set; }
		public string Ref2_ptoref_garante { get; set; }
		public string Ref2_Telcelular_garante { get; set; }
		public string Ref3_garante { get; set; }
		public string Ref3_direc_garante { get; set; }
		public string Ref3_telef_garante { get; set; }
		public string Ref3_ptoref_garante { get; set; }
		public string Ref3_Telcelular_garante { get; set; }
	}
}
