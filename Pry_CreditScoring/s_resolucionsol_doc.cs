using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using wfcModel;
namespace Docsis_Application
{
    public partial class s_resolucionsol_doc : Form
    {
        bool con_borde = MDI_Menu.con_borde;
        bool resoluciones_ok = false;
        bool vl_mostrar_miniinfo = true;
        s_miniinfo_usuario miniinfo_user = new s_miniinfo_usuario();
        public string scodigo_cliente = "";

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

        DataAccess da;

        public s_resolucionsol_doc(DataAccess da)
        {
            InitializeComponent();
            this.da = da;
            p_llenar_combo_areas();
        }

        //cbo_rechazo
        private void p_llenar_combo_areas()
        {
            try
            {
                DataTable dtAreas = da.ObtenerMotivosRechazo();
                cbo_rechazo.DataSource = dtAreas;
                cbo_rechazo.DisplayMember = "DESCRIPCION";
                cbo_rechazo.ValueMember = "COD_RECHAZO";
                cbo_rechazo.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex.TargetSite + " " + ex.Message);
            }
        }

        private void s_resolucionsol_doc_Load(object sender, EventArgs e)
        {
            p_llenar_info();
            labelUser_comite.Text = DocSys.vl_user;
            var dt1 = da.ObtenerSituacionActualxSolicitud(Int32.Parse(txtNo_solicitud.Text));
            Int32 vl_movimiento_Actual = Int32.Parse(dt1.Rows[0]["no_movimiento_actual"].ToString());
            Int32 vl_comite_actual = Int32.Parse(dt1.Rows[0]["estacion_id"].ToString());
            txtPlazo_s.Text = dt1.Rows[0]["plazo"].ToString();
            txtTasa_sol.Text = dt1.Rows[0]["tasa"].ToString();
            cbo_rechazo.Enabled = false;
            try
            {
                scodigo_cliente = dt1.Rows[0]["codigo_cliente"].ToString();
                textBox_oficial.Text = dt1.Rows[0]["oficial_servicio"].ToString();
                textBox_nombre_oficial.Text = da.ObtenerNombreUsuario(textBox_oficial.Text);
            }
            catch
            {

            }
            #region foto del afiliado
            string vl_fecha_ultima_act = "";
            byte[] foto;
            da.ObtenerFotoAfiliado(scodigo_cliente, out foto, out vl_fecha_ultima_act);
            if (foto != null)
            {
                pbFotoVigente.Image = DocSys.p_CopyDataToBitmap(foto);
            }
            #endregion

            groupboxAprobaciones.Visible = false;
            if (!da.ObtenerSiHayResolucionesPendientesxSolicitud(Int32.Parse(txtNo_solicitud.Text), vl_comite_actual, vl_movimiento_Actual))
            {
                btnAprobar.Visible = false;
                btnRechazar.Visible = false;
                labelAviso.Visible = true;
                labelAviso.Text = "Las resoluciones de esta solicitud ya fueron completadas...!";
                groupboxAprobaciones.Visible = true;
                txtMonto_aprobado.ReadOnly = true;
                txtPlazo_aprobado.ReadOnly = true;
                txtTasa_aprobada.ReadOnly = true;
                txtNo_Acta.ReadOnly = true;
                txtCiudad.ReadOnly = true;
                DataTable dtAproba = da.ObtenerMontosAprobado(Int32.Parse(txtNo_solicitud.Text));
                if (dtAproba.Rows.Count > 0)
                {
                    string vl_monto = dtAproba.Rows[0]["monto_aprobado"].ToString();
                    string vl_tasa_aprobada = dtAproba.Rows[0]["tasa_aprobada"].ToString();
                    string vl_plazo_aprobado = dtAproba.Rows[0]["plazo_aprobado"].ToString();
                    string vl_no_acta_resol = dtAproba.Rows[0]["no_acta_resolucion"].ToString();
                    string vl_ciudad_resol = dtAproba.Rows[0]["ciudad_resolucion"].ToString();
                    if (string.IsNullOrEmpty(vl_monto) || string.IsNullOrEmpty(vl_tasa_aprobada) || string.IsNullOrEmpty(vl_plazo_aprobado) || string.IsNullOrEmpty(vl_no_acta_resol)
                        || string.IsNullOrEmpty(vl_ciudad_resol))
                    {
                        btnGuardar.Visible = true;
                        txtMonto_aprobado.ReadOnly = false;
                        txtPlazo_aprobado.ReadOnly = false;
                        txtTasa_aprobada.ReadOnly = false;
                        txtNo_Acta.ReadOnly = false;
                        txtCiudad.ReadOnly = false;
                        txtCiudad.ReadOnly = false;
                        txtMonto_aprobado.Focus();
                    }
                    else
                    {
                        resoluciones_ok = true;
                    }
                }
            }
            else
            {
                bool es_ultima_respuesta = da.ObtenerSiUltimaRespuestaResolucion(Int32.Parse(txtNo_solicitud.Text), vl_movimiento_Actual, vl_comite_actual);
                if (es_ultima_respuesta)
                {
                    groupboxAprobaciones.Visible = true;
                    btnGuardar.Visible = true;
                }
            }
        }

        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            decimal vl_monto_aprobado = 0;
            decimal.TryParse(txtMonto_aprobado.Text, out vl_monto_aprobado);

            int vl_plazo_aprobado = 0;
            int.TryParse(txtPlazo_aprobado.Text, out vl_plazo_aprobado);

            decimal vl_tasa_aprobada = 0;
            decimal.TryParse(txtTasa_aprobada.Text, out vl_tasa_aprobada);

            if (txtDecision_final.Text == "APROBADO" & vl_monto_aprobado == 0)
            {
                MessageBox.Show("Debe indicar monto,plazo y tasa aprobada en esta resolucion de comite...", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtDecision_final.Text == "APROBADO" & vl_plazo_aprobado == 0)
            {
                MessageBox.Show("Debe indicar monto,plazo y tasa aprobada en esta resolucion de comite...", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtDecision_final.Text == "APROBADO" & vl_tasa_aprobada == 0)
            {
                MessageBox.Show("Debe indicar monto,plazo y tasa aprobada en esta resolucion de comite...", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtDecision_final.Text == "APROBADO" & !resoluciones_ok)
            {
                MessageBox.Show("Debe completar el monto, tasa,plazo aprobado y no de acta y cuidad, presione el boton guardar..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.Close();
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }

        private void btnAprobar_Click(object sender, EventArgs e)
        {
            p_resolucion_solicid("APROBADO");
        }

        private void btnRechazar_Click(object sender, EventArgs e)
        {
            cbo_rechazo.Enabled = true;
            p_resolucion_solicid("RECHAZADO");
        }

        private void p_resolucion_solicid(string p_accion)
        {
            var dt1 = da.ObtenerSituacionActualxSolicitud(Int32.Parse(txtNo_solicitud.Text));
            Int32 vl_movimiento_Actual = Int32.Parse(dt1.Rows[0]["no_movimiento_actual"].ToString());
            Int32 vl_comite_actual = Int32.Parse(dt1.Rows[0]["estacion_id"].ToString());
           
            var dt = da.ObtenerDecisionesCommitexUsuarioxSolicitud(Int32.Parse(txtNo_solicitud.Text), labelUser_comite.Text);
            string vl_decision = dt.Rows[0]["decision"].ToString().Trim().ToUpper();
            if (!string.IsNullOrEmpty(vl_decision))
            {
                MessageBox.Show("Su decisión sobre la solicitud No. " + txtNo_solicitud.Text + " ya fue recibida en el sistema..!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(txtComentarios.Text))
            {
                MessageBox.Show("Debe indicar sus comentarios sobre la decisión que ha tomado en esta solicitud de crédito..!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtComentarios.Text))
            {
                MessageBox.Show("Debe indicar sus comentarios sobre la decisión que ha tomado en esta solicitud de crédito..!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (txtComentarios.Text.Length <= 10)
            {
                MessageBox.Show("Debe indicar sus comentarios sobre la decisión que ha tomado en esta solicitud de crédito..!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            
            string vl_texto = p_accion == "APROBADO" ? "Aprobar " : "Rechazar ";

            if (vl_texto == "Rechazar " && cbo_rechazo.SelectedIndex == -1)
            {
                MessageBox.Show("Debe indicar su motivo de rechazo ..!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("Desea " + vl_texto + " esta solicitud ?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            string vl_resultado = "";
            bool es_ultima_respuesta = da.ObtenerSiUltimaRespuestaResolucion(Int32.Parse(txtNo_solicitud.Text), vl_movimiento_Actual, vl_comite_actual);
            int re = Convert.ToInt32(cbo_rechazo.SelectedValue);
            string rep = cbo_rechazo.Text;
            if (da.ProcesarResolucionSolicitud(labelUser_comite.Text, p_accion, txtComentarios.Text, Int32.Parse(txtNo_solicitud.Text), re, rep, out vl_resultado))
            {
                btnAprobar.Enabled = false;
                btnRechazar.Enabled = false;
                p_llenar_resoluciones();
                txtDecision_final.Text = vl_resultado;
                if (vl_resultado == "APROBADO")
                {
                    if (es_ultima_respuesta)
                    {
                        btnGuardar.Visible = true;
                        txtMonto_aprobado.Focus();
                        if (string.IsNullOrEmpty(txtMonto_aprobado.Text) | txtMonto_aprobado.Text == "0.00")
                        {
                            MessageBox.Show("Esta es la ultima respuesta pendiente sobre esta solicitud, Debe indicar el monto, plazo y tasa aprobada por este comite..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                    }
                }
            }
            txtDecision_final.Text = vl_resultado;
        }

        private void p_llenar_info()
        {
            p_llenar_anotaciones(Int32.Parse(txtNo_solicitud.Text));
            p_llenar_adjuntos(Int32.Parse(txtNo_solicitud.Text));
            p_llenar_resoluciones();
            var dt1 = da.ObtenerSituacionActualxSolicitud(Int32.Parse(txtNo_solicitud.Text));
            txtMonto_aprobado.Text = decimal.Parse(dt1.Rows[0]["monto_aprobado"].ToString()).ToString("#,###,###,##0.00");
            txtPlazo_aprobado.Text = dt1.Rows[0]["plazo_aprobado"].ToString();
            txtTasa_aprobada.Text = decimal.Parse(dt1.Rows[0]["tasa_aprobada"].ToString()).ToString("##0.00");

            txtDecision_final.Text = dt1.Rows[0]["estado_solicitud"].ToString();

            txtNo_Acta.Text = dt1.Rows[0]["no_acta_resolucion"].ToString();
            txtCiudad.Text = dt1.Rows[0]["ciudad_resolucion"].ToString();
        }

        private void p_llenar_resoluciones()
        {
            gvResoluciones.AutoGenerateColumns = false;
            gvResoluciones.DataSource = da.ObtenerDecisionesComitexSolicitud(Int32.Parse(txtNo_solicitud.Text));
            gvResoluciones.Refresh();
        }

        private void p_llenar_anotaciones(Int32 p_no_solicitud)
        {
            list_anotaciones.Clear();
            string vl_condi_por_mov = "";
            string vl_orden = " anot.no_anotacion ";

            if (rbOrdenCrono.Checked)
                vl_orden = " anot.no_anotacion ";

            if (rbOrdenxEstacion.Checked)
                vl_orden = " nombre_estacion,anot.no_anotacion ";

            int vl_no_mov = 0;
            list_anotaciones.BeginUpdate();
            list_anotaciones.SmallImageList = imagesSmall;
            list_anotaciones.LargeImageList = imagesLarge;
            list_anotaciones.Clear();
            list_anotaciones.Groups.Clear();
            DataTable tabla = da.ObtenerAnotacionesxSolicitud(p_no_solicitud, vl_orden);

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

        private void p_llenar_adjuntos(Int32 p_no_solicitud)
        {
            var tabla = da.ObtenerAdjuntosxSoliciud(p_no_solicitud);
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
        }

        private void p_abrir_anotacion(Int32 p_no_anotacion)
        {

            s_add_notas forma = new s_add_notas(da, "CONS",
                                                Int32.Parse(txtNo_solicitud.Text),
                                                0,
                                                p_no_anotacion);
            forma.ShowDialog();

        }

        private void p_abrir_adjunto(Int32 p_no_archivo)
        {
            string vl_extesion = "";
            var adjunto = da.ObternerArchivoxNo(p_no_archivo, out vl_extesion);
            if (!DBNull.Value.Equals(adjunto))
            {
                byte[] bits = adjunto;
                string sFile = Application.StartupPath.ToString() + @"\tmp\" + DocSys.vl_user + "tmp" + txtNo_solicitud.Text.Trim() + DateTime.Now.ToString("yyyyMMddhhmmss") + "." + vl_extesion;
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

        private void list_anotaciones_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = list_anotaciones.GetItemAt(e.X, e.Y);
            if (item != null)
            {
                //MessageBox.Show(item.SubItems[1].Text);
                p_abrir_anotacion(Int32.Parse(item.SubItems[0].Text));
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            s_AnalisisCredito_01 forma = new s_AnalisisCredito_01(da, txtNo_solicitud.Text);
            forma.ShowDialog();
        }

        private void txtTasa_aprobada_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox texbox = txtTasa_aprobada;
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

        private void txtMonto_aprobado_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox texbox = txtMonto_aprobado;
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

        private void txtMonto_aprobado_Leave(object sender, EventArgs e)
        {
            TextBox texbox = txtMonto_aprobado;
            double valor_ingresado = 0;
            if (!string.IsNullOrEmpty(texbox.Text))
                valor_ingresado = Convert.ToDouble(texbox.Text);

            texbox.Text = string.Format(String.Format("{0:#,###,###,##0.00}", valor_ingresado));

        }

        private void txtPlazo_aprobado_KeyPress(object sender, KeyPressEventArgs e)
        {
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
        }

        private void button4_Click(object sender, EventArgs e)
        {
            p_llenar_info();
        }

        private void list_adjuntos_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.list_adjuntos.GetItemAt(e.X, e.Y) != null)
            {
                Int32 no_archivo = 0;
                this.list_adjuntos.GetItemAt(e.X, e.Y).Selected = true;
                ListViewItem item = list_adjuntos.GetItemAt(e.X, e.Y);
                if (item != null)
                {

                    Int32.TryParse(item.SubItems[2].Text, out no_archivo);
                }

                Point pt = list_adjuntos.PointToScreen(e.Location);
                if (e.Button == MouseButtons.Right)
                    contextMenu_adjuntos.Show(pt);
                if (e.Clicks == 2)
                    if (no_archivo > 0)
                        p_abrir_adjunto(no_archivo);
            }
        }

        private void rbDocumentos_titulos_CheckedChanged(object sender, EventArgs e)
        {
            list_adjuntos.View = View.Tile;
        }

        private void rbDocumentos_iconos_Click(object sender, EventArgs e)
        {
            list_adjuntos.View = View.LargeIcon;
        }

        private void rbDocumentos_detalle_CheckedChanged(object sender, EventArgs e)
        {
            list_adjuntos.View = View.Details;
        }

        private void lnkTablaresultado_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmsRpts.frmRpt_ResolucionComite forma = new FrmsRpts.frmRpt_ResolucionComite(da);
            forma.gno_solicitud = Convert.ToInt32(txtNo_solicitud.Text);
            forma.ShowDialog();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            decimal vl_monto_aprobado = 0;
            decimal vl_plazo_aprobado = 0;
            decimal vl_tasa_aprobada = 0;
            decimal.TryParse(txtMonto_aprobado.Text, out vl_monto_aprobado);
            decimal.TryParse(txtPlazo_aprobado.Text, out vl_plazo_aprobado);
            decimal.TryParse(txtTasa_aprobada.Text, out vl_tasa_aprobada);
            if (vl_monto_aprobado == 0 && txtDecision_final.Text == "APROBADO")
            {
                MessageBox.Show("El monto aprobado no puede ser cero (0)..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (vl_plazo_aprobado == 0 && txtDecision_final.Text == "APROBADO")
            {
                MessageBox.Show("El plazo aprobado no puede ser cero (0)..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (vl_tasa_aprobada == 0 && txtDecision_final.Text == "APROBADO")
            {
                MessageBox.Show("La tasa aprobada no puede ser cero (0)..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (da.ActualizarResolucionolicitud(Convert.ToInt32(txtNo_solicitud.Text), txtNo_Acta.Text, txtCiudad.Text, vl_monto_aprobado, vl_tasa_aprobada, vl_plazo_aprobado))
            {
                MessageBox.Show("Datos de la resolucion del comite actualizados satisfactoriamente..!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                resoluciones_ok = true;
                return;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            s_analisis_cuantitativo forma = new s_analisis_cuantitativo();
            forma.txtNo_solicitud.Text = txtNo_solicitud.Text;
            forma.da = da;
            DialogResult res = forma.ShowDialog();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmsRpts.frmRpt_AnalisisCualitativo forma = new FrmsRpts.frmRpt_AnalisisCualitativo(da);
            forma.gno_solicitud = Convert.ToInt32(txtNo_solicitud.Text);
            forma.ShowDialog();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            s_miniestado_cliente forma = new s_miniestado_cliente();
            forma.da = da;
            forma.gno_solicitud = Int32.Parse(txtNo_solicitud.Text);
            forma.pbAfiliado.Image = pbFotoVigente.Image;
            forma.ShowDialog();
        }

        private void textBox_oficial_MouseMove(object sender, MouseEventArgs e)
        {
            if (textBox_oficial.Text == string.Empty)
                return;
            miniinfo_user.get_set_codigo_usuario = textBox_oficial.Text;
            Point pos = this.PointToScreen(e.Location);
            miniinfo_user.da = this.da;
            miniinfo_user.Show();
            miniinfo_user.Location = new Point(Control.MousePosition.X - 105, Control.MousePosition.Y + 10);
            miniinfo_user.Refresh();
        }

        private void textBox_oficial_MouseLeave(object sender, EventArgs e)
        {
            vl_mostrar_miniinfo = true;
            miniinfo_user.Hide();
            this.Cursor = Cursors.Default;
        }

        private void pbFotoVigente_Click(object sender, EventArgs e)
        {
            s_PreCalificado_info03 forma = new s_PreCalificado_info03();
            forma.pbFotoVigente.Image = pbFotoVigente.Image;
            forma.codigo_cliente = scodigo_cliente;
            forma.da = da;
            forma.ShowDialog();
        }

    }
}
