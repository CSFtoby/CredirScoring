using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using wfcModel;
using System.Xml;
using System.Threading;

namespace Docsis_Application
{
    public partial class s_marcar_abandono_doc : Form
    {
        public DataAccess da;
        int vl_no_solicitud = 0;
        ImageList img_banderines;

        delegate void Functionz();
        Thread myThread;

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
                // add the drop shadow flag for automatically drawing
                // a drop shadow around the form
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
        #endregion

        public s_marcar_abandono_doc(DataAccess da, int pa_no_solicitud, ImageList imgbanderines)
        {
            InitializeComponent();
            vl_no_solicitud = pa_no_solicitud;
            img_banderines = imgbanderines;
            this.da = da;
        }
        private void s_marcar_abandono_doc_Load(object sender, EventArgs e)
        {
            p_get_datos_solicitud();
            //if (label_Application_id.Text.Trim() == "0")
            //{
            //    button_abandonar.Enabled = false;
            //    MessageBox.Show("Esta solicitud no fue precalificada en TransUnion, no puede ser marcada como abandonada !", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}
        }
        private void p_get_datos_solicitud()
        {
            label_Application_id.Text = da.ObtenerApplicationIDxSolicitud(vl_no_solicitud);
            if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
            {
                DocSys.connOracle.Open();
            }
            string vl_sql = @" Select s.no_solicitud,monto_solicitado,nombres||' '||primer_apellido nombre,desc_sub_aplicacion,meses_plazo,no_movimiento_actual,banderin_id,
                                      nvl(round((sysdate - fecha_presentacion)) + 1,0) dias_antiguead
                                 From dcs_solicitudes s,mgi_clientes c,mgi_sub_aplicaciones sa 
                                Where c.codigo_empresa=sa.codigo_empresa
                                  and c.codigo_empresa=1 
                                  and s.codigo_cliente=c.codigo_cliente
                                  and s.codigo_sub_aplicacion=sa.codigo_sub_aplicacion 
                                  and s.no_solicitud=:pa_no_solicitud";

            OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
            cmd.CommandType = CommandType.Text;
            //───────────────────
            OracleParameter pa_no_solicitud = new OracleParameter("pa_no_solicitud", OracleType.Int32);
            cmd.Parameters.Add(pa_no_solicitud);
            pa_no_solicitud.Direction = ParameterDirection.Input;
            pa_no_solicitud.Value = vl_no_solicitud;
            OracleDataReader dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                label_nombre.Text = dr["nombre"].ToString();
                label_producto.Text = dr["desc_sub_aplicacion"].ToString();
                label_no_solicitud.Text = dr["no_solicitud"].ToString();
                label_monto.Text = String.Format("{0:###,###,###,##0.00}", float.Parse(dr["monto_solicitado"].ToString()));
                label_plazo.Text = String.Format("{0:##0}", float.Parse(dr["meses_plazo"].ToString()));
                pictureBox_banderin.Image = img_banderines.Images[int.Parse(dr["banderin_id"].ToString())];
                labelAntiguedad.Text = dr["dias_antiguead"].ToString();
            }
        }
        private void button_cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button_abandonar_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(txtComentarios.Text))
            {
                MessageBox.Show("Debe escribir algun comentario porque se esta marcado como abandonada esta solicitud ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("Desea marcar como abandonada esta solicitud..?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;


            myThread = new Thread(new ThreadStart(p_invocar_ws));
            myThread.Start();
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
                        
                        if (da.ActualizarEstadoSolicitud(vl_no_solicitud,98, 98, txtComentarios.Text))
                        {
                            this.Invoke(new Functionz(delegate()
                            {
                                panelPrecali_solic.Visible = false;
                            }));
                            MessageBox.Show("Solicitud marcada como abandonada satisfactoriamente ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                    else
                    {
                        this.Invoke(new Functionz(delegate()
                        {
                            panelPrecali_solic.Visible = false;
                        }));
                        da.RegistrarEventoErrorPrecali(vl_no_solicitud, "ABANDONAR_SOLIC", resultado_consulta, xml_);
                        if (da.ActualizarEstadoSolicitud(vl_no_solicitud,98, 98, txtComentarios.Text))
                        {
                            this.Invoke(new Functionz(delegate()
                            {
                                panelPrecali_solic.Visible = false;
                            }));
                            MessageBox.Show("Solicitud marcada como abandonada satisfactoriamente ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                        }
                        
                        
                        return;
                    }
                }
                else
                {
                    if (da.ActualizarEstadoSolicitud(vl_no_solicitud,98, 98, txtComentarios.Text))
                    {
                        this.Invoke(new Functionz(delegate()
                        {
                            panelPrecali_solic.Visible = false;
                        }));
                        MessageBox.Show("Solicitud marcada como abandonada satisfactoriamente ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }
        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            moverForm();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            button_cerrar_Click(null, null);
        }
        private string p_construir_xml()
        {
            string xml_request = "";
            string[] u = labelOficialDeServicio.Text.Split('|');
            string usuarioWf = DocSys.vl_user;
            try
            {
                xml_request = @"<Request>
                                     <Authentication>
                                       <UserId>" + MDI_Menu.usuario_wstransunion + @"</UserId>
                                       <Password>" + MDI_Menu.pass_wstransunion + @"</Password>
                                     </Authentication>
                                     <ApplicationId>" + label_Application_id.Text + @"</ApplicationId>
                                    <Cierre>        
                                       <UsuarioWorkflow>" + usuarioWf + @"</UsuarioWorkflow>
                                       <Sucursal>" + labelFilial.Text + @"</Sucursal>
                                       <DecisionFinalAnalista>DESISTIR</DecisionFinalAnalista>
                                       <MontoAprobado/>
                                       <Tasa/>
                                       <Plazo/> 
                                       <MotivoCambio>98</MotivoCambio>
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
        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string xmls = p_construir_xml();
            xmlViewer forma = new xmlViewer(xmls);
            forma.ShowDialog();
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
