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
using Docsis_Application.Excepciones;

namespace Docsis_Application
{
    public partial class s_mover_solicitud : Form
    {
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

        delegate void Functionz();
        Thread myThread;

        public DataAccess da;
        public Int32 gno_solicitud;
        public int gworkflow_id;
        public int gpaso_actual;
        public int gestacion_id_to = 0;
        public int gcodigo_agencia = 0;
        public string gtipo_respuesta = "";
        public Int32 gestacion_actual = 0;
        public Int32 gestado_solicitud_id = 0;
        public string es_nivel_resolutivo = "N";
        public string es_nivel_resolutivo_origen = "N";
        public string es_nivel_local = "N";
        public string es_nivel_con_gtefilial = "N";
        public string gusuario_gerente = "";
        public int resoluciones_requeridas = 0;

        public string miembros_seleccionados = "";


        public s_mover_solicitud(DataAccess da)
        {
            InitializeComponent();
            pbWait.Visible = false;
            this.da = da;
        }

        private void s_mover_solicitud_Load(object sender, EventArgs e)
        {
            try
            {

                ComboBox_decision.DataSource = da.ObtenerDecisiones(gno_solicitud);
                ComboBox_decision.DisplayMember = "descripcion";
                ComboBox_decision.ValueMember = "decision_id";

                labelNo_solicitud.Text = gno_solicitud.ToString();

                var dtinfo = da.ObtenerInfoSolicitud(gno_solicitud);

                gpaso_actual = int.Parse(dtinfo.Rows[0]["paso_actual"].ToString());
                gworkflow_id = int.Parse(dtinfo.Rows[0]["workflow_id"].ToString());
                gestacion_actual = Int32.Parse(dtinfo.Rows[0]["estacion_id"].ToString());
                gestado_solicitud_id = int.Parse(dtinfo.Rows[0]["estado_solicitud_id"].ToString());
                labelPaso.Text = gpaso_actual.ToString();
                ComboBox_decision_SelectionChangeCommitted(null, null);

                //Se cambio la agencia del usuario a la agencia de la solicitud, para que el combobox de gerentes filtre los gerente disponibles para esta filial en nivel resolutivo filial
                //y para cuando es comite I, aparezca los gerentes de esa filial que va participar en la resolucion asi dos pajaros de un solo tiro.
                //ya que con la agencia del usuario no funcionaba porque el usuario del que este comite I siempre va ser o OP, Regional Ceiba o Circunvalacion

                gcodigo_agencia = int.Parse(dtinfo.Rows[0]["codigo_agencia_origen"].ToString());

                llenarComboGerentes();
                labelOficialDeServicio.Text = MDI_Menu.nombre_y_usuario_oficial_servicio;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void btnMoverSolicitud_Click(object sender, EventArgs e)
        {
            var result = miembros_seleccionados.Split('|');
            var cant_resoluciones_selec = result.Count() - 1;

            var resol_req = int.Parse(txtResol_requeridas.Text);

            DataTable dtDecision = da.ObtenerDecisionxId(Convert.ToInt32(labelDecisionID.Text));
            DataTable dtSolic = da.ObtenerInfoSolicitud(gno_solicitud);
            if (gtipo_respuesta == "F")
            {
                if (!da.EliminarResolucionesParaDevolver(gno_solicitud))
                {
                    MessageBox.Show("Error al tratar de devolver la solicitud");
                }
                if (dtSolic.Rows[0]["estado_solicitud_id"].ToString() == "3")
                {
                    MessageBox.Show("Si la solicitud esta aprobada no puede utilizar esta desicion !", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (dtSolic.Rows[0]["estado_solicitud_id"].ToString() == "3" && es_nivel_resolutivo == "S")
            {
                MessageBox.Show("La solicitud esta en estado APROBADO, la solicitud no puede ser enviado a un nivel resolutivo..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //----------------- otros comites  estan en (2) en proceso
            if (dtSolic.Rows[0]["estado_solicitud_id"].ToString() == "2" && es_nivel_resolutivo == "S" && string.IsNullOrEmpty(this.miembros_seleccionados))
            {
                
                MessageBox.Show("Debe indicar los miembros del comite resolutivo...!("+ cant_resoluciones_selec + ")", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                labelDestino_LinkClicked(null, null);
                return;
            }

            if (dtSolic.Rows[0]["estado_solicitud_id"].ToString() == "2" && es_nivel_resolutivo == "S" && cant_resoluciones_selec != resol_req)
            {
                
                MessageBox.Show("Se debe indicar " + resol_req + " resoluciones para este Comite..!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //----------------- comite local estan en 1
            if (dtSolic.Rows[0]["estado_solicitud_id"].ToString() == "1" && es_nivel_resolutivo == "S" && string.IsNullOrEmpty(this.miembros_seleccionados))
            {

                MessageBox.Show("Debe indicar los miembros del comite resolutivo...!(" + cant_resoluciones_selec + ")", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                labelDestino_LinkClicked(null, null);
                return;
            }

            if (dtSolic.Rows[0]["estado_solicitud_id"].ToString() == "1" && es_nivel_resolutivo == "S" && cant_resoluciones_selec != resol_req)
            {

                MessageBox.Show("Se debe indicar " + resol_req + " resoluciones para este Comite..!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //------------------

            if (es_nivel_resolutivo_origen == "S")
            {
                Int32 vl_movimiento_actual = Convert.ToInt32(dtSolic.Rows[0]["no_movimiento_actual"].ToString());
                bool hay_resol_pendientes = da.ObtenerSiHayResolucionesPendientesxSolicitud(gno_solicitud, gestacion_actual, vl_movimiento_actual);
                if (hay_resol_pendientes)
                {
                    MessageBox.Show("Las resoluciones de este comite no han sido completadas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }


            if (dtDecision.Rows[0]["es_desembolso"].ToString() == "S")
            {
                Int32 vl_estado_requerido_desem = Convert.ToInt32(dtDecision.Rows[0]["estado_para_desembolso"].ToString());
                Int32 vl_estado_solicitud_id = Convert.ToInt32(dtSolic.Rows[0]["estado_solicitud_id"].ToString());


                if (vl_estado_requerido_desem != vl_estado_solicitud_id)
                {
                    DataTable dtEstado = da.ObtenerEstadoSolicxID(vl_estado_requerido_desem);
                    string vl_desc_estado_requerido = dtEstado.Rows[0]["descripcion"].ToString();
                    MessageBox.Show("El estado requerido para esta decesión es " + vl_desc_estado_requerido, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            if (MessageBox.Show("Desea mover esta solicitud al area " + labelDestino.Text + " ?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            myThread = new Thread(new ThreadStart(proceso_enviar));
            myThread.Start();

        }
        private void proceso_enviar()
        {
            try
            {
                this.Invoke(new Functionz(delegate()
                {
                    pbWait.Visible = true;
                }));
          

                da.MoverSolicitud(gworkflow_id, gno_solicitud, gpaso_actual, int.Parse(labelDecisionID.Text), gusuario_gerente,miembros_seleccionados);
                Thread.Sleep(4000);
                MessageBox.Show("La solicitud se ha enviado satisfactoriamente !", "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                this.Invoke(new Functionz(delegate()
                {
                    pbWait.Visible = false;
                }));
                MessageBox.Show(ex.Message);
            }

        }
        private void ComboBox_decision_SelectionChangeCommitted(object sender, EventArgs e)
        {
            labelDecisionID.Text = ComboBox_decision.SelectedValue.ToString();


            if (labelDecisionID.Text != "" & labelPaso.Text != "")
            {
                if (!string.IsNullOrEmpty(miembros_seleccionados))
                {
                    if (MessageBox.Show("Ya realizo la seleccion de almenos un miembro o Gerente de Comite, si continua hay que volver a seleccionar !,desea continuar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                    
                    this.miembros_seleccionados = string.Empty;
                    pbFotoMiembro1.Image = null;
                    pbFotoMiembro2.Image = null;
                    pbFotoMiembro3.Image = null;
                    pbFotoMiembro4.Image = null;
                    pbFotoMiembro5.Image = null;
                    
                    var i = this.miembros_seleccionados.Split('|').Count() - 1;
                    lblMiembrossele.Text = "Gerentes de Comite Seleccionados (" + i.ToString() + ")";
                }
                
                var dt = da.ObtenerDestinosxDecision(gestacion_actual, gpaso_actual, int.Parse(labelDecisionID.Text), gworkflow_id);
                if (dt.Rows.Count > 0)
                {
                    labelOrigen.Text = dt.Rows[0]["estacion_origen"].ToString();
                    labelDestino.Text = dt.Rows[0]["estacion_destino"].ToString();
                    
                    gestacion_id_to = Int32.Parse(dt.Rows[0]["estacion_id_to"].ToString());
                    
                    txtMonto_minimo.Text = decimal.Parse(dt.Rows[0]["monto_minimo_resolucion"].ToString()).ToString("###,###,##0.00");
                    txtMonto_maximo.Text = decimal.Parse(dt.Rows[0]["monto_maximo_resolucion"].ToString()).ToString("###,###,##0.00"); 
                    txtResol_requeridas.Text = dt.Rows[0]["resoluciones_requeridas"].ToString();
                    es_nivel_resolutivo_origen = dt.Rows[0]["comite_resolutivo_origen"].ToString();
                    es_nivel_resolutivo = dt.Rows[0]["comite_resolutivo"].ToString();
                    es_nivel_local = dt.Rows[0]["nivel_resolutivo_local"].ToString();
                    es_nivel_con_gtefilial = dt.Rows[0]["nivel_resol_con_gtefilial"].ToString();
                    resoluciones_requeridas = int.Parse(dt.Rows[0]["resoluciones_requeridas"].ToString());
                    gtipo_respuesta = dt.Rows[0]["tipo_respuesta"].ToString();


                    labelAvisos.Text = "";
                    labelDecision.Text = "Decisión (" + gtipo_respuesta + ")";
                    labelDestino.Text = dt.Rows[0]["estacion_destino"].ToString();


                    if (!DBNull.Value.Equals(dt.Rows[0]["icono_origen"]))
                    {
                        byte[] bits = ((byte[])dt.Rows[0]["icono_origen"]);
                        pbEstacion_actual.Image = new Bitmap(DocSys.p_CopyDataToBitmap(bits));
                    }
                    else
                        pbEstacion_actual.Image = null;

                    if (!DBNull.Value.Equals(dt.Rows[0]["icono_destino"]))
                    {
                        byte[] bits = ((byte[])dt.Rows[0]["icono_destino"]);
                        pbEstacion_Destino.Image = new Bitmap(DocSys.p_CopyDataToBitmap(bits));
                    }
                    else
                        pbEstacion_Destino.Image = null;

                    if (es_nivel_resolutivo == "S")
                    {
                        gbCriterios.Visible = true;
                        decimal monto_sol = decimal.Parse(labelMonto_solicitado.Text);
                        decimal monto_min = decimal.Parse(txtMonto_minimo.Text);
                        decimal monto_max = decimal.Parse(txtMonto_maximo.Text);
                        btnMoverSolicitud.Enabled = true;
                        if (monto_max <= 0)
                        {
                            btnMoverSolicitud.Enabled = false;
                            labelAvisos.Text = "Este comite no tiene definidos criterios de aprobación";
                        }
                        else
                        {
                            if (monto_sol > monto_max)
                            {
                                btnMoverSolicitud.Enabled = false;
                                labelAvisos.Text = "Monto excede la capacidad de aprobación.";
                            }
                            if (monto_sol < monto_min)
                            {
                                btnMoverSolicitud.Enabled = false;
                                labelAvisos.Text = "Monto es menor al criterio de aprobación.";
                            }
                        }

                        string vl_forma_pago = da.ObtenerEsVentanillaPlanilla(gno_solicitud);
                        if (labelDestino.Text == "Nivel Resolutivo Filial" && vl_forma_pago == "VENTANILLA" && monto_sol > 100000)
                        {
                            btnMoverSolicitud.Enabled = false;
                            labelAvisos.Text = "Este nivel no puedo ver solicitudes de afiliados por ventanilla por montos mayores a L100,000.00";
                        }

						if (labelDestino.Text == "Nivel Resolutivo Filial" && vl_forma_pago == "VENTANILLA" && (monto_sol >= 50000.01m & monto_sol <= 100000))
						{
							this.txtResol_requeridas.Text = "2";
						}

						//if (es_nivel_local == "S" || es_nivel_con_gtefilial == "S")
						/*if (es_nivel_local == "S" )
                        {
                            groupBoxGtesFilial.Visible = true;
                        }
                        else
                        {
                            groupBoxGtesFilial.Visible = false;
                        }*/

					}
                    else
                    {
                        labelAvisos.Text = "";
                        gbCriterios.Visible = false;
                        groupBoxGtesFilial.Visible = false;
                        pnlGtesSeleccionados.Visible = false;
                        btnMoverSolicitud.Enabled = true;
                    }
                }
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }

        private void labelDestino_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (gestacion_id_to == (int)Estaciones.Afiliacion)
            {
                if (MessageBox.Show("La visualización de los miembros de la Estación de Afiliación puede tardar varios minutos, desea continuar..?","Aviso",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.No)
                {
                    return;
                }
            }
            s_cnf_estaciones_miembros forma = new s_cnf_estaciones_miembros(da, gestacion_id_to);
            forma.es_comite_local = es_nivel_local;
            forma.es_comite_resolutivo = es_nivel_resolutivo;
            forma.agencia_local = gcodigo_agencia;
            DialogResult result = forma.ShowDialog();
            if (result == DialogResult.OK)
            {
                pnlGtesSeleccionados.Visible = true;
                for (int x = 0; x < forma.imagenes_usr_sele.Count(); x++)
                {
                    if (x == 0)
                        pbFotoMiembro1.Image = forma.imagenes_usr_sele[x];
                    if (x == 1)
                        pbFotoMiembro2.Image = forma.imagenes_usr_sele[x];
                    if (x == 2)
                        pbFotoMiembro3.Image = forma.imagenes_usr_sele[x];
                    if (x == 3)
                        pbFotoMiembro4.Image = forma.imagenes_usr_sele[x];
                    if (x == 4)
                        pbFotoMiembro5.Image = forma.imagenes_usr_sele[x];
                }

                this.miembros_seleccionados = forma.usuarios_seleccionados;
                var i = this.miembros_seleccionados.Split('|').Count() - 1;
                lblMiembrossele.Text = "Gerentes de Comite Seleccionados (" + i.ToString() + ")";
            }

        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }

        private void llenarComboGerentes()
        {
            DataTable dtGerentes = da.ObtenerGerentesdeFilialxFilial(gcodigo_agencia);
            cmbGerentesFilial.DataSource = dtGerentes;
            cmbGerentesFilial.DisplayMember = "nombre_gerente";
            cmbGerentesFilial.ValueMember = "codigo_usuario";
            if (dtGerentes.Rows.Count > 0)
            {
                cmbGerentesFilial_SelectionChangeCommitted(null, null);
            }
            else
            {
                MessageBox.Show("No hay un gerente definido en su Filial ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cmbGerentesFilial_SelectionChangeCommitted(object sender, EventArgs e)
        {
            gusuario_gerente = cmbGerentesFilial.SelectedValue.ToString();
            groupBoxGtesFilial.Text = "Gerente de Filial " + gusuario_gerente;
        }

        private void pnlGtesSeleccionados_Click(object sender, EventArgs e)
        {
            MessageBox.Show(miembros_seleccionados);
        }

        private void pnlGtesSeleccionados_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pbFotoMiembro1_Click(object sender, EventArgs e)
        {
            s_PreCalificado_info03 forma = new s_PreCalificado_info03();
            forma.pbFotoVigente.Image = pbFotoMiembro1.Image;
            forma.codigo_cliente = "";
            forma.da = da;
            forma.ShowDialog();
        }
    }
}
