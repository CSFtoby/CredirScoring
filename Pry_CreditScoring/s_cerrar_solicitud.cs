using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using wfcModel;
using System.Xml;
using System.Threading;

namespace Docsis_Application
{
    public partial class s_cerrar_solicitud : Form
    {
        delegate void Functionz();
        Thread myThread;

        public DataAccess da;
        public Int32 gno_solicitud = 0;
        public Int32 gestado_destino = 0;
        public string gestado_solicitud = "";

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
        public s_cerrar_solicitud()
        {
            InitializeComponent();
        }
        private void s_cerrar_solicitud_Load(object sender, EventArgs e)
        {
            p_get_datos_solicitud();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button_cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void p_get_datos_solicitud()
        {
            solicitud_credito sol = da.ObtenerSolicitudCredito(gno_solicitud);
            label_nombre.Text = sol.nombres;
            label_producto.Text = sol.desc_sub_aplicacion;
            label_no_solicitud.Text = gno_solicitud.ToString();
            label_monto.Text = String.Format("{0:###,###,###,##0.00}", sol.monto_aprobado);
            label_plazo.Text = String.Format("{0:##0}", sol.plazo_aprobado);
            label_tasa.Text = String.Format("{0:##0}", sol.tasa_aprobada);
            labelMonto_aprob.Text = String.Format("{0:##########0.00}", sol.monto_aprobado);
            //labelAntiguedad.Text=
            pictureBox_banderin.Image = imageSeguimiento.Images[sol.banderin_id];
            label_Application_id.Text = sol.application_id.ToString();

            if (sol.estado_solicitud_id == 3)
            {
                gestado_solicitud = "3";
                labelTituloObs.Text = " Observaciones de la APROBACION ";
                textBoxInstrucciones_desem.Text = sol.instrucciones_desembolso;
                grupboxInstrucciones.Visible = true;
                gestado_destino = 5; //de la tabla de estados solicitud
                p_llenar_combobox("PORAPROBACION");
            }
            if (sol.estado_solicitud_id == 4)
            {
                gestado_solicitud = "4";
                labelTituloObs.Text = " Observaciones o motivos del RECHAZO ";
                grupboxInstrucciones.Visible = false;
                gestado_destino = 6;//de la tabla de estados solicitud
                p_llenar_combobox("PORRECHAZO");
            }

            DataTable dtInfo = da.ObtenerSituacionActualxSolicitud(gno_solicitud);
            labelEstadoSol.Text = dtInfo.Rows[0]["estado_solicitud"].ToString();
            labelAntiguedad.Text = dtInfo.Rows[0]["dias_antiguedad"].ToString();

            DataTable DtAn = da.ObtenerInfoSolicitud(sol.no_solicitud);
        }
        private void p_invocar_ws()
        {
            this.Invoke(new Functionz(delegate()
            {
                panelPrecali_solic.Visible = true;
            }));
            try
            {
                if (Int16.Parse(labelAntiguedad.Text) < 60)
                {
                    string xml_ = p_construir_xml();

                    MD_Coop ws_transunion = new MD_Coop();
                    string resultado_consulta = ws_transunion.CerrarSolicitud(xml_);
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(resultado_consulta);
                    string vl_Status = xmlDoc.SelectSingleNode("DCResponse/Status").InnerText;

                    if (vl_Status == "Success")
                    {
                        int vl_motivo_cierre_id = 0;
                        try
                        {
                            int.Parse(txtMotivo_id.Text);
                        }
                        catch
                        {
                            vl_motivo_cierre_id = 0;
                        }
                        if (da.ActualizarEstadoSolicitud(gno_solicitud, vl_motivo_cierre_id, gestado_destino, txtComentarios.Text))
                        {
                            this.Invoke(new Functionz(delegate()
                            {
                                panelPrecali_solic.Visible = false;
                            }));
                            MessageBox.Show("Solicitud cerrada satisfactoriamente ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                    else
                    {
                        this.Invoke(new Functionz(delegate()
                        {
                            panelPrecali_solic.Visible = false;
                        }));
                        da.RegistrarEventoErrorPrecali(gno_solicitud, "CERRAR_SOLIC", resultado_consulta, xml_);
						int vl_motivo_cierre_id = 0;
						//para Marcar como cerradas cuando da erro en TransUnion, 
						if (da.ActualizarEstadoSolicitud(gno_solicitud, vl_motivo_cierre_id, gestado_destino, txtComentarios.Text))
						{
							this.Invoke(new Functionz(delegate ()
							{
								panelPrecali_solic.Visible = false;
							}));
							MessageBox.Show("Solicitud cerrada satisfactoriamente ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
							this.DialogResult = DialogResult.OK;
						}

						MessageBox.Show("No se ha podido marcar la solicitud como cerrada, intente nuevamente...", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }
                else
                {
                    int vl_motivo_cierre_id = 0;
                    try
                    {
                        int.Parse(txtMotivo_id.Text);
                    }
                    catch
                    {
                        vl_motivo_cierre_id = 0;
                    }
                    if (da.ActualizarEstadoSolicitud(gno_solicitud,vl_motivo_cierre_id, gestado_destino, txtComentarios.Text))
                    {
                        this.Invoke(new Functionz(delegate()
                        {
                            panelPrecali_solic.Visible = false;
                        }));
                        MessageBox.Show("Solicitud cerrada satisfactoriamente ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                    }
                }

            }
            catch (Exception ex)
            {
                this.Invoke(new Functionz(delegate()
                {
                    panelPrecali_solic.Visible = false;
                }));
                MessageBox.Show("Error al tratar de marcar la solicitud como abandonada" + ex.Message + " " + ex.Source, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        private string p_construir_xml()
        {
            string xml_request = "";
            string[] u = labelOficialDeServicio.Text.Split('|');
            string usuarioWf = u[0];
            try
            {
                xml_request = @"<Request>
                                     <Authentication>
                                       <UserId>" + MDI_Menu.usuario_wstransunion + @"</UserId>
                                       <Password>" + MDI_Menu.pass_wstransunion + @"</Password>
                                     </Authentication>
                                     <ApplicationId>" + label_Application_id.Text + @"</ApplicationId>
                                    <Cierre>        
                                       <UsuarioWorkflow>" + DocSys.vl_user + @"</UsuarioWorkflow>
                                       <Sucursal>" + labelFilial.Text + @"</Sucursal>
                                       <DecisionFinalAnalista>" + labelEstadoSol.Text + @"</DecisionFinalAnalista>
                                       <MontoAprobado>" + labelMonto_aprob.Text + @"</MontoAprobado>
                                       <Tasa>" + label_tasa.Text + @"</Tasa>
                                       <Plazo>" + label_plazo.Text + @"</Plazo> 
                                       <MotivoCambio>" + txtMotivo_id.Text.Trim() + @"</MotivoCambio>
                                       <Comentario>" + txtComentarios.Text + @"</Comentario>
                                   </Cierre>
                                 </Request>";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al tratar de constuir el XML a enviar " + ex.Message);
            }
            return xml_request;
        }
        private void p_llenar_combobox(string p_motivo)
        {
            DataTable dtMotivo = da.ObtenerListaMotivosCierre(p_motivo);
            cmbMotivos_cierre.DataSource = dtMotivo;
            cmbMotivos_cierre.DisplayMember = "descripcion_motivo";
            cmbMotivos_cierre.ValueMember = "motivo_id";
            cmbMotivos_cierre_SelectionChangeCommitted(null, null);
        }
        private void button_abandonar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea cerrar  esta solicitud..?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            Int32 vl_application_id = 0;
            Int32.TryParse(label_Application_id.Text, out vl_application_id);
            //
            // si no tiene application id, es que no se precalifico como un 13/14 solo debe cerrarse aqui en coopsfa
            //
            if (vl_application_id == 0)
            {
                this.Invoke(new Functionz(delegate()
                {
                    panelPrecali_solic.Visible = true;
                }));

                int vl_motivo_cierre_id = 0;
                try
                {
                    int.Parse(txtMotivo_id.Text);
                }
                catch
                {
                    vl_motivo_cierre_id = 0;
                }
                if (da.ActualizarEstadoSolicitud(gno_solicitud, vl_motivo_cierre_id, gestado_destino, txtComentarios.Text))
                {
                    this.Invoke(new Functionz(delegate()
                    {
                        panelPrecali_solic.Visible = false;
                    }));
                    MessageBox.Show("Solicitud cerrada satisfactoriamente ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                }
            }
            else
            {
                myThread = new Thread(new ThreadStart(p_invocar_ws));
                myThread.Start();
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {
            xmlViewer forma = new xmlViewer(p_construir_xml());
            forma.ShowDialog();
            
        }
        private void cmbMotivos_cierre_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtMotivo_id.Text = cmbMotivos_cierre.SelectedValue.ToString();            
        }

    }
}
