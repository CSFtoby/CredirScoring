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
    public partial class s_admin_resoluciones : Form
    {
        Int16 vl_estado_solicitud_id = 0;
        string vl_nivel_resolutivo = "N";
        Int32 vl_movimiento_actual = 0;
        Int32 vl_estacion_actual = 0;
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

        public DataAccess da;
        public s_admin_resoluciones(DataAccess da)
        {
            InitializeComponent();
            this.da = da;
            this.txtOficial_servicio.CharacterCasing = CharacterCasing.Upper;
            this.txtUsuario_to.CharacterCasing = CharacterCasing.Upper;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }

        private void txtNo_solicitud_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNo_solicitud.Text))
            {
                return;
            }
            p_llenar_info();

            var dt1 = da.ObtenerSituacionActualxSolicitud(Int32.Parse(txtNo_solicitud.Text));
            if (dt1.Rows.Count > 0)
            {
                Int32 vl_movimiento_Actual = Int32.Parse(dt1.Rows[0]["no_movimiento_actual"].ToString());
                Int32 vl_comite_actual = Int32.Parse(dt1.Rows[0]["estacion_id"].ToString());
            }
            else
            {
                MessageBox.Show("No de solicitud no existe..!", "Aviso de Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

			bool creada = false;
			bool ValorExcepcion = this.da.GuardarAlertaExcepcion("S", Int32.Parse(txtNo_solicitud.Text), out creada, false);
			this.cbPasarSinExcepcion.Checked = ValorExcepcion;
		}

        private void p_llenar_info()
        {
            p_llenar_resoluciones();
            var dt1 = da.ObtenerSituacionActualxSolicitud(Int32.Parse(txtNo_solicitud.Text));
            if (dt1.Rows.Count > 0)
            {
                textBox_estado_solicitud.Text = dt1.Rows[0]["estado_solicitud"].ToString();
                if (dt1.Rows[0]["permitir_modif_aprob"].ToString() == "S")
                {
                    checkBox1.Checked = true;
                }
                else
                {
                    checkBox1.Checked = false;
                }
            }
            var dt3 = da.ObtenerInfoSolicitud3(Int32.Parse(txtNo_solicitud.Text));
            if (dt3.Rows.Count > 0)
            {
                txtNombre_solicitante.Text = dt3.Rows[0]["nombre"].ToString();
                txtDesc_sub_aplicacion.Text = dt3.Rows[0]["desc_sub_aplicacion"].ToString();
                txtPlazo_s.Text = dt3.Rows[0]["plazo"].ToString();
                txtTasa_sol.Text = dt3.Rows[0]["tasa"].ToString();
                txtMonto_solicitado.Text = decimal.Parse(dt3.Rows[0]["monto_solicitado"].ToString()).ToString("#,###,###,##0.00");
                vl_estado_solicitud_id = Int16.Parse(dt3.Rows[0]["estado_solicitud_id"].ToString());
                txtOficial_servicio.Text = dt3.Rows[0]["oficial_servicio"].ToString();
                labelNombre_oficial_servicio.Text = da.ObtenerNombreUsuario(txtOficial_servicio.Text);
                txtCodigo_agencia.Text = dt3.Rows[0]["codigo_agencia_origen"].ToString();
                labelNombre_agencia.Text = da.ObtenerNombreAgencia(int.Parse(txtCodigo_agencia.Text));
                labelNivelResol.Text = da.ObtenerNombreEstacion(int.Parse(dt3.Rows[0]["estacion_id"].ToString()));
                vl_estacion_actual = Int32.Parse(dt3.Rows[0]["estacion_id"].ToString());
                vl_nivel_resolutivo = da.ObtenerEstacionIsResolutivo(int.Parse(dt3.Rows[0]["estacion_id"].ToString()));
                txtxFechaAprvRchz.Text = dt3.Rows[0]["FECHA_APROBACION_RECHAZO"].ToString();
                //recalculos de solicitudes en transunion
                txtRecalculos_permitidos.Text = dt3.Rows[0]["recalculos_permitidos"].ToString();
                txtRecalculos_actuales.Text = dt3.Rows[0]["recalculos_actuales"].ToString();
                txtRecalPermTodas.Text = da.ObtenerParametro("WFC-0005");
            }

            var dt4 = da.ObtenerSituacionActualxSolicitud(Int32.Parse(txtNo_solicitud.Text));
            if (dt4.Rows.Count > 0)
            {
                txtMonto_aprobado.Text = decimal.Parse(dt1.Rows[0]["monto_aprobado"].ToString()).ToString("#,###,###,##0.00");
                txtPlazo_aprobado.Text = dt1.Rows[0]["plazo_aprobado"].ToString();
                txtTasa_aprobada.Text = decimal.Parse(dt1.Rows[0]["tasa_aprobada"].ToString()).ToString("##0.00");
                txtNo_Acta.Text = dt1.Rows[0]["no_acta_resolucion"].ToString();
                txtCiudad.Text = dt1.Rows[0]["ciudad_resolucion"].ToString();
            }

            DataTable dtResol = da.ObtenerComiteResolucion(Convert.ToInt32(txtNo_solicitud.Text));
            if (dtResol.Rows.Count > 0)
            {
                txtComiteResolucion.Text = dtResol.Rows[0]["nombre"].ToString();
            }

            labelMovimiento_actual.Text = da.ObtenerUltimoMovAprobaciones(Int32.Parse(txtNo_solicitud.Text)).ToString();
        }

        private void p_llenar_resoluciones()
        {
            gvResoluciones.AutoGenerateColumns = false;
            gvResoluciones.DataSource = da.ObtenerDecisionesComitexSolicitud(Int32.Parse(txtNo_solicitud.Text));
            gvResoluciones.Refresh();
        }

        private void s_admin_resoluciones_Load(object sender, EventArgs e){ }

        private void txtNo_solicitud_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            s_buscarusuarios forma = new s_buscarusuarios();
            forma.da = this.da;
            DialogResult resultado = forma.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                txtOficial_servicio.Text = forma.vl_usuario.ToUpper();
                labelNombre_oficial_servicio.Text = da.ObtenerNombreUsuario(txtOficial_servicio.Text);
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            s_buscarfiliales forma = new s_buscarfiliales();
            forma.da = this.da;
            DialogResult resultado = forma.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                txtCodigo_agencia.Text = forma.vl_codigo_agencia;
                labelNombre_agencia.Text = da.ObtenerNombreAgencia(int.Parse(txtCodigo_agencia.Text));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (vl_estado_solicitud_id != 2)
            {
                MessageBox.Show("La solicitud tiene que estar en proceso para agregar personas al proceso de aprobacion, no puede estar APROBADA, ni ingresada en Filial", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (vl_nivel_resolutivo == "N")
            {
                MessageBox.Show("La solicitud tiene que estar en un nivel resolutivo para agregar un usuario aprobador..:!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            s_buscarusuarios forma = new s_buscarusuarios();
            forma.da = this.da;
            DialogResult resultado = forma.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                string vl_usuario_sel = forma.vl_usuario;
                if (da.AdicionarResolucionxUsuario(Int32.Parse(txtNo_solicitud.Text), vl_usuario_sel, vl_estacion_actual, Int32.Parse(labelMovimiento_actual.Text)))
                {
                    p_llenar_resoluciones();
                }
                else
                {
                    MessageBox.Show("No ha sido posible agregar el usuario al comite resolutivo ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void button_quitar_Click(object sender, EventArgs e)
        {
            string vl_llave = "";
            string vl_usuario = "";
            string vl_observaciones = "";
            string vl_pendiente_respuesta = "";
            DataGridViewRow row = gvResoluciones.CurrentRow;
            if (row != null)
            {
                vl_llave = row.Cells["llave"].Value.ToString();
                vl_usuario = row.Cells["usuario_comite"].Value.ToString();
                vl_observaciones = row.Cells["observaciones"].Value.ToString();
                vl_pendiente_respuesta = row.Cells["pendiente_respuesta_b"].Value.ToString();
            }
            if (vl_pendiente_respuesta == "N")
            {
                MessageBox.Show("El registro de aprobación tiene que estar pendiente de respuesta para poder eliminar..! ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("Desea eliminar el registro de aprobacion del usuario " + vl_usuario + " ?", "Aviso de Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            if (vl_pendiente_respuesta == "N")
            {
                if (MessageBox.Show("La aprobación del usuario " + vl_usuario.Trim() + " que esta tratando de eliminar ya fue respondida, seguro que desea eliminar esta registro..", "Aviso Validación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }
            if (da.EliminarResolucionxUsuario(Int32.Parse(txtNo_solicitud.Text), vl_usuario, Int32.Parse(labelMovimiento_actual.Text), vl_llave))
            {
                p_llenar_resoluciones();
            }
            else
            {
                MessageBox.Show("No ha sido posible eliminar la aprobacion seleccionada...", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCambiarOficAgencia_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNo_solicitud.Text))
            {
                return;
            }
            if (string.IsNullOrEmpty(txtOficial_servicio.Text) || labelNombre_oficial_servicio.Text == "")
            {
                MessageBox.Show("Debe indicar el oficial de servicio de la solicitud...", "Aviso de Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtCodigo_agencia.Text) || labelNombre_agencia.Text == "")
            {
                MessageBox.Show("Debe indicar la Filial de la solicitud...", "Aviso de Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //--
            if (MessageBox.Show("Desea realizar cambios de Oficial de Servicio o Filial ?", "Aviso de Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            if (da.ReasignarOficialFilial(Int32.Parse(txtNo_solicitud.Text), int.Parse(txtCodigo_agencia.Text), txtOficial_servicio.Text))
            {
                MessageBox.Show("Cambios realizados satisfactoriamente...", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se ha podido realizar los cambios solicitados", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtOficial_servicio_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtOficial_servicio.Text))
            {
                labelNombre_oficial_servicio.Text = "";
                labelNombre_oficial_servicio.Text = da.ObtenerNombreUsuario(txtOficial_servicio.Text.Trim());
            }
        }

        private void txtCodigo_agencia_Leave(object sender, EventArgs e)
        {
            try
            {
                int.Parse(txtCodigo_agencia.Text);
            }
            catch
            {
                labelNombre_agencia.Text = "";
                MessageBox.Show("Debe ingresar un numero valido de Agencia en el cuadro de texto", "Aviso de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrEmpty(txtCodigo_agencia.Text))
            {
                labelNombre_agencia.Text = "";
                labelNombre_agencia.Text = da.ObtenerNombreAgencia(int.Parse(txtCodigo_agencia.Text));
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNo_solicitud.Text))
            {
                return;
            }
            if (vl_estado_solicitud_id < 3)
            {
                MessageBox.Show("La solicitud tiene que estar APROBADA para modificar las montos aprobados..!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (MessageBox.Show("Desea realizar cambios el los montos aprobados para la solicitud ?", "Aviso de Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            decimal vl_monto_aprobado = 0;
            decimal vl_plazo_aprobado = 0;
            decimal vl_tasa_aprobada = 0;
            decimal.TryParse(txtMonto_aprobado.Text, out vl_monto_aprobado);
            decimal.TryParse(txtPlazo_aprobado.Text, out vl_plazo_aprobado);
            decimal.TryParse(txtTasa_aprobada.Text, out vl_tasa_aprobada);
            if (vl_monto_aprobado == 0)
            {
                MessageBox.Show("El monto aprobado no puede ser cero (0)..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (vl_plazo_aprobado == 0)
            {
                MessageBox.Show("El plazo aprobado no puede ser cero (0)..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (vl_tasa_aprobada == 0)
            {
                MessageBox.Show("La tasa aprobada no puede ser cero (0)..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (da.ReactualizarValoresAprobacion(Convert.ToInt32(txtNo_solicitud.Text), txtNo_Acta.Text, txtCiudad.Text, vl_monto_aprobado, vl_tasa_aprobada, vl_plazo_aprobado))
            {
                MessageBox.Show("Datos de la resolucion del comite actualizados satisfactoriamente..!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("No ha sido posible realizar cambios en los valores aprobados...", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

        private void pbBuscarUsers_Click(object sender, EventArgs e)
        {
            s_buscarusuarios forma = new s_buscarusuarios();
            forma.da = this.da;
            DialogResult resultado = forma.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                txtUsuario_to.Text = forma.vl_usuario.ToUpper();
                labelNombreUsuarioTO.Text = da.ObtenerNombreUsuario(txtUsuario_to.Text);
            }
        }

        private void gvResoluciones_SelectionChanged(object sender, EventArgs e)
        {
            int vl_no_solicitud = 0;
            DataGridViewRow row = gvResoluciones.CurrentRow;
            if (row != null)
            {
                txtUsuario_actual.Text = row.Cells["usuario_comite"].Value.ToString();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                da.RepermitirModifAprob(Int32.Parse(txtNo_solicitud.Text), "S");
            }
            else
            {
                da.RepermitirModifAprob(Int32.Parse(txtNo_solicitud.Text), "N");
            }
        }

        private void lnkTablaresultado_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNo_solicitud.Text))
            {
                return;
            }
            FrmsRpts.frmRpt_ResolucionComite forma = new FrmsRpts.frmRpt_ResolucionComite(da);
            forma.gno_solicitud = Convert.ToInt32(txtNo_solicitud.Text);
            forma.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNo_solicitud.Text))
            {
                return;
            }
            s_analisis_cuantitativo forma = new s_analisis_cuantitativo();
            forma.txtNo_solicitud.Text = txtNo_solicitud.Text;
            forma.da = da;
            DialogResult res = forma.ShowDialog();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNo_solicitud.Text))
            {
                return;
            }
            FrmsRpts.frmRpt_AnalisisCualitativo forma = new FrmsRpts.frmRpt_AnalisisCualitativo(da);
            forma.gno_solicitud = Convert.ToInt32(txtNo_solicitud.Text);
            forma.ShowDialog();
        }

        private void lLxmls_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNo_solicitud.Text))
            {
                return;
            }
            xmlViewer_list forma = new xmlViewer_list();
            forma.labelNo_solicitud.Text = txtNo_solicitud.Text;
            forma.da = this.da;
            forma.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNo_solicitud.Text))
            {
                return;
            }
            if (gvResoluciones.Rows.Count > 1)
            {
                MessageBox.Show("Esta opcion solo puede utilizarse en un nivel resolutivo filial, es decir que solo tiene una apronbación", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string vl_llave = "";
            string vl_usuario = "";
            string vl_observaciones = "";
            string vl_pendiente_respuesta = "";
            DataGridViewRow row = gvResoluciones.CurrentRow;
            if (row != null)
            {
                vl_llave = row.Cells["llave"].Value.ToString();
                vl_usuario = row.Cells["usuario_comite"].Value.ToString();
                vl_observaciones = row.Cells["observaciones"].Value.ToString();
                vl_pendiente_respuesta = row.Cells["pendiente_respuesta_b"].Value.ToString();
            }
            if (vl_pendiente_respuesta == "N")
            {
                MessageBox.Show("El registro de aprobación tiene que estar pendiente de respuesta para poder cambiarlo.! ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrEmpty(txtUsuario_to.Text))
            {
                MessageBox.Show("Debe indicar el usuario del gerente al que desea cambiar..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUsuario_to.Focus();
                return;
            }
            if (MessageBox.Show("Desea realizar cambios de Gerente de Filial para aprobacion de esta solicitud ?", "Aviso de Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            if (da.ReasignarGerenteFilial(Int32.Parse(txtNo_solicitud.Text), txtUsuario_to.Text, Int32.Parse(labelMovimiento_actual.Text), vl_llave))
            {
                MessageBox.Show("Cambios realizados satisfactoriamente...", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                p_llenar_resoluciones();
                txtUsuario_to.Text = "";
            }
            else
            {
                MessageBox.Show("No se ha podido realizar los cambios solicitados", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtUsuario_to_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsuario_to.Text))
            {
                labelNombreUsuarioTO.Text = "";
                labelNombreUsuarioTO.Text = da.ObtenerNombreUsuario(txtUsuario_to.Text.Trim());
            }
        }

        private void txtRecalculos_permitidos_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnCambiar_cantrecal_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNo_solicitud.Text) || string.IsNullOrEmpty(txtRecalculos_permitidos.Text))
            {
                return;
            }

            if (MessageBox.Show("Desea realizar cambios en la cantidad de recalculos permitidos para esta solicitud ?", "Aviso de Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            if (da.PonerRecalcPermitidosxSolic(Int32.Parse(txtNo_solicitud.Text), int.Parse(txtRecalculos_permitidos.Text)))
            {
                MessageBox.Show("Cantidad de recalculos permitidos actualizados satisfactoriamente..!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("No ha sido posible actualizar la cantidad de recalculos ", "Aviso de Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void lLEditarSolic_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNo_solicitud.Text))
            {
                return;
            }
            s_PreCalificado forma = new s_PreCalificado(da);
            forma.gmodo_coopsafa = "UPD";
            forma.txtNo_solicitud_coopsafa.Text = txtNo_solicitud.Text;
            DialogResult res = forma.ShowDialog();
            if (res == DialogResult.OK)
            {

            }
        }

        private void txtNo_solicitud_KeyPress(object sender, KeyPressEventArgs e)
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

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNo_solicitud.Text))
                return;

            if (MessageBox.Show("Actualizar solicitantes de la solicitud ?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            try
            {
                Int32 p_no_solicitud = Int32.Parse(txtNo_solicitud.Text);
                da.RegistrarIDs_x_Solicitud(p_no_solicitud);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

		private void cbPasarSinExcepcion_CheckedChanged(object sender, EventArgs e)
		{
			string bandera = (this.cbPasarSinExcepcion.Checked) ? "S" : "N";
			this.da.CambiarBanderaSolicitudExcepcion(bandera, Int32.Parse(txtNo_solicitud.Text));
		}

        private void lkQuitarCMT_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.gvResoluciones.Rows.Count > 0)
            {
                DataGridViewRow row = this.gvResoluciones.CurrentRow;
                string pendiente = row.Cells["pendiente_respuesta_b"].Value.ToString();
                string vl_usuario = row.Cells["usuario_comite"].Value.ToString();
                string vl_comentario = row.Cells["observaciones"].Value.ToString();
                int vl_sol = Convert.ToInt32(txtNo_solicitud.Text);
                int numero_movimiento = 0;
                int registro = 0;

                DataTable dt = da.get_detalle_sol(vl_sol, vl_usuario);
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        numero_movimiento = Convert.ToInt32(dr["no_movimiento"].ToString());
                    }
                }

                if (String.IsNullOrEmpty(vl_comentario))
                {
                    MessageBox.Show("Esta operación no es permitida, valide que ya no posee comentarios.");
                    return;
                }
                else
                {
                    if (MessageBox.Show("Desea eliminar el Comentario del usuario " + vl_usuario + " ?", "Aviso de Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (Convert.ToBoolean(DialogResult.Yes) == true)
                        {
                            if (da.EliminarComentarioxUsuarioSOL(vl_sol, vl_usuario, numero_movimiento) == true)
                            {
                                MessageBox.Show("Se ha quitado con exito");
                                p_llenar_resoluciones();
                            }
                            else
                            {
                                MessageBox.Show("Hubo un problema");
                            }
                        }
                    }
                }
            }
        }

        private void lkEditarRSL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.gvResoluciones.Rows.Count > 0)
            {
                DataGridViewRow row = this.gvResoluciones.CurrentRow;
                string pendiente = row.Cells["decision"].Value.ToString();
                int registro = 0;

                if (pendiente.Equals("S"))
                {
                    MessageBox.Show("No puede modificar la resolución porque aún no ha sido respondida");
                    return;
                }
                else
                {
                    DialogResult result = MessageBox.Show($"¿Está seguro que desea cambiar la observación del comité?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        string observAnterior = row.Cells["observaciones"].Value.ToString();
                        r_comentarios_solicitud forma = new r_comentarios_solicitud(observAnterior);
                        result = forma.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            string obsrNueva = forma.ObservacionNueva;
                            string usuarioActual = row.Cells["usuario_comite"].Value.ToString();
                            string reiniciar = "N";
                            int vl_sol = Convert.ToInt32(txtNo_solicitud.Text);
                            int numero_movimiento = 0;

                            DataTable dt = da.get_detalle_sol(vl_sol, usuarioActual);
                            if (dt != null)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    numero_movimiento = Convert.ToInt32(dr["no_movimiento"].ToString());
                                }
                            }

                            if (string.IsNullOrEmpty(obsrNueva))
                            {
                                reiniciar = "S";
                            }
                            string vl_usuario = row.Cells["usuario_comite"].Value.ToString();
                            if (da.EditarComentarioxUsuarioSOL(vl_sol, vl_usuario, numero_movimiento, obsrNueva) == true)
                            {
                                MessageBox.Show("Se ha editado con exito");
                                p_llenar_resoluciones();
                            }
                            else
                            {
                                MessageBox.Show("Hubo un problema");
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int vl_sol = Convert.ToInt32(txtNo_solicitud.Text);
            int count = 0;
            string pendiente;

            if (this.gvResoluciones.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in gvResoluciones.Rows) {
                    pendiente = row.Cells["pendiente_respuesta_b"].Value.ToString();
                    if (Convert.ToChar(pendiente) == 'S') {
                        count++;
                    }
                }
            }
            int total = this.gvResoluciones.Rows.Count;
            if (count != total)
            {
                MessageBox.Show("No se puso en proceso, ¡¡asegurece de que la solicitud no tenga resoluciones!!");
            }
            else {
                if (textBox_estado_solicitud.Text == "En proceso")
                {
                    MessageBox.Show("La solitud ya esta en proceso ");
                }
                else {
                    if (da.putProcess(vl_sol) == true)
                    {
                        MessageBox.Show("Se ha editado con exito");
                        p_llenar_info();
                    }
                    else
                    {
                        MessageBox.Show("Hubo un problema");
                    }
                }
            }
            
        }

        private void linkLabel4_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            double tasaActual = Convert.ToDouble(txtTasa_sol.Text);
            DialogResult result = MessageBox.Show($"¿Está seguro que desea cambiar la tasa de la solicitud?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                r_fmrCambioTasa forms = new r_fmrCambioTasa(tasaActual);
                result = forms.ShowDialog();
                if (result == DialogResult.OK)
                {
                    double nuevaTasa = forms.tasaNueva;
                    int vl_sol = Convert.ToInt32(txtNo_solicitud.Text);
                    if (da.EditarTasaSOL(vl_sol, nuevaTasa) == true) {
                        MessageBox.Show("Se ha editado con exito");
                        txtTasa_sol.Text = Convert.ToString(nuevaTasa);
                    }
                    else
                    {
                        MessageBox.Show("Hubo un problema");
                    }
                }
            }
            else
            {
                return;
            }
        }
    }
}
