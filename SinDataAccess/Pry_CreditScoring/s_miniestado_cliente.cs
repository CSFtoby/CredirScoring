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
    public partial class s_miniestado_cliente : Form
    {
        public Int32 gno_solicitud = 0;
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

        public DataAccess da;
        public s_miniestado_cliente()
        {
            InitializeComponent();
        }
        private void s_miniestado_cliente_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtInfoSol = da.ObtenerInfoSolicitud(gno_solicitud);
                
                if (dtInfoSol.Rows.Count > 0)
                {
                    Int32 vl_codigo_cliente = Int32.Parse(dtInfoSol.Rows[0]["codigo_cliente"].ToString());
                    DataTable dtInfoCl = da.ObtenerDatosClientexCodigoCliente(vl_codigo_cliente.ToString());
                    if (dtInfoCl.Rows.Count>0)
                    {
                        txtIDSolicitante.Text = dtInfoCl.Rows[0]["numero_identificacion"].ToString();
                    }
                    txtCodigo_cliente.Text = vl_codigo_cliente.ToString();
                    
                    // da.VeDatosEmpleados();
                    p_obtener_info_cliente(txtIDSolicitante.Text);
                    p_obtener_ctas_ahorro();
                    p_otener_prestamos();
                    p_obtener_infotarjeta();
                    p_obtener_infocoopcel();
                    p_obtener_fe();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }
        private void p_obtener_info_cliente(string p_no_identificacion)
        {
            DataTable dt = da.ObtenerDatosClientexIdentificacion(p_no_identificacion);
            if (dt.Rows.Count > 0)
            {
                txtCodigo_cliente.Text = dt.Rows[0]["codigo_cliente"].ToString();
                txtIDSolicitante.Text = dt.Rows[0]["numero_identificacion"].ToString();
                txtNombre.Text = dt.Rows[0]["nombres"].ToString();
                txtPrimer_apellido.Text = dt.Rows[0]["primer_apellido"].ToString();
                txtSegundo_apellido.Text = dt.Rows[0]["segundo_apellido"].ToString();
                txtApellido_casada.Text = dt.Rows[0]["apellido_de_casada"].ToString();
                txtEstado_civil.Text = dt.Rows[0]["estado_civil"].ToString();
                txtSexo.Text = dt.Rows[0]["sexo"].ToString();
                if (dt.Rows[0]["fecha_de_nacimiento"].ToString().Length >= 10)
                {
                    txtFecha_nacimiento.Text = dt.Rows[0]["fecha_de_nacimiento"].ToString().Substring(0, 10);
                }
                string is_deduccion_x_planila = dt.Rows[0]["notaria"].ToString();
                if (is_deduccion_x_planila == "S")
                {
                    txtVentanilla_planilla.Text = "PLANILLA";
                }
                else
                    txtVentanilla_planilla.Text = "VENTANILLA";
                txtNacionalidad.Text = dt.Rows[0]["nacionalidad"].ToString();
            }
        }
        private void p_obtener_ctas_ahorro()
        {
            gvAhorros.AutoGenerateColumns = false;
            gvAhorros.DataSource = da.ObtenerDetalleAhorrosAfiliado(txtCodigo_cliente.Text);
            gvAhorros.Refresh();
        }
        private void p_otener_prestamos()
        {
            gvPrestamos.AutoGenerateColumns = false;
            gvPrestamos.DataSource = da.ObtenerDetallePrestamosAfiliado(txtCodigo_cliente.Text);
            gvPrestamos.Refresh();
        }
        private void p_obtener_infotarjeta()
        {
            DataTable dtTarjeta = da.ObtenerInfoTarjetaDebicoop(txtCodigo_cliente.Text);
            if (dtTarjeta.Rows.Count > 0)
            {
                cbTarjeta.Checked = true;
            }
            else
            {
                cbTarjeta.Checked = false;
            }
            gvDebicoops.AutoGenerateColumns = false;
            gvDebicoops.DataSource = dtTarjeta;
            gvDebicoops.Refresh();
        }
        private void p_obtener_infocoopcel()
        {
            DataTable dtCoopcel = da.ObtenerInfoCoopcel(txtCodigo_cliente.Text);
            if (dtCoopcel.Rows.Count > 0)
            {
                cbCoopcel.Checked = true;
                txtNumero_cel.Text = dtCoopcel.Rows[0]["coopcel_numero"].ToString();
                txtRedMovil.Text = dtCoopcel.Rows[0]["red_movil"].ToString();
            }
            else
            {
                cbCoopcel.Checked = false;
            }
        }
        private void p_obtener_fe()
        {
            DataTable dtFE = da.ObtenerInfoFE(txtCodigo_cliente.Text);
            if (dtFE.Rows.Count > 0)
            {
                cbFilialElectronica.Checked = true;
                txtUsuarioFE.Text = dtFE.Rows[0]["usuario"].ToString();
                txtUltimo_login.Text = dtFE.Rows[0]["fecha_ultimo_login"].ToString();
                txtCorreo_fe.Text = dtFE.Rows[0]["correo_elec"].ToString();
            }
            else
            {
                cbFilialElectronica.Checked = false;
            }
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }

        private void pbAfiliado_Click(object sender, EventArgs e)
        {
            s_PreCalificado_info03 forma = new s_PreCalificado_info03();
            forma.pbFotoVigente.Image = pbAfiliado.Image;
            forma.codigo_cliente = txtCodigo_cliente.Text;
            forma.da = da;
            forma.ShowDialog();
        }

    }
}
