using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using wfcModel;

namespace Docsis_Application
{
    public partial class s_add_miembros : Form
    {
        bool con_borde = MDI_Menu.con_borde;
        int vl_member_id = 0;
        int vl_zone_id = 0;
        public string Nombre_Miembro = string.Empty;
        public string ID_Miembro = string.Empty;
        int cod_cliente = 0;
        public string modo = "INGRESO";
        public string id_upd = string.Empty;
        public DataAccess da;
        DataTable dtInfoFinan = new DataTable();
        string nombre_completo;

        #region
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

        public s_add_miembros(DataAccess da)
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            this.da = da;
        }

        public s_add_miembros(DataAccess da, string modo, string id)
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            this.da = da;
            this.modo = modo;
            this.id_upd = id;
        }

        private void s_add_miembros_Load(object sender, EventArgs e)
        {
            P_Llenar_Combo_Tipo_Miembro();
            P_Llenar_Combo_Regionales();

            if (modo == "ACTUALIZA")
            {
                txtCogAfiliado.ReadOnly = true;
                txtNoIdentidad.ReadOnly = true;
                ID_Miembro = this.id_upd;
                this.btnAceptar.Text = "Actualizar";
                p_get_datos_solicitantexID(ID_Miembro);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //metodo para llenar el combobox de los tipos de miembro
        private void P_Llenar_Combo_Tipo_Miembro() {
            try
            {
                DataSet mbCombo = new DataSet();
                mbCombo = DocSys.p_Obtener_un_dataset("select ID_TIPO_MIEMBRO ,TIPO_MIEMBRO from WFC.DCS_WF_TIPO_MIEMBRO", "DCS_WF_TIPO_MIEMBRO");
                cbxTipoMiembro.DataSource = mbCombo;
                cbxTipoMiembro.DisplayMember = "DCS_WF_TIPO_MIEMBRO.TIPO_MIEMBRO";
                cbxTipoMiembro.ValueMember = "DCS_WF_TIPO_MIEMBRO.ID_TIPO_MIEMBRO";
                cbxTipoMiembro.SelectedValue = vl_member_id;
            }
            catch (Exception ex) {
                MessageBox.Show("Se ha poducido un error al intentar llenar los Registos de Tipos de Miembros");
            }
        }

        //metodo para llenado del combobox de la zonas de filial
        private void P_Llenar_Combo_Regionales() {
            try
            {
                DataSet rzCombo = new DataSet();
                rzCombo = DocSys.p_Obtener_un_dataset("select CODIGO_ZONA, DESCRIPCION from cef_zonas", "cef_zonas");
                cbxZona.DataSource = rzCombo;
                cbxZona.DisplayMember = "cef_zonas.DESCRIPCION";
                cbxZona.ValueMember = "cef_zonas.CODIGO_ZONA";
                cbxZona.SelectedValue = vl_zone_id;
            }
            catch (Exception ex) {
                MessageBox.Show("Se ha produido un error al intenar llenar los Registros de las Regionales");
            }
        }

        private void txtCodigo_cliente_Leave(object sender, EventArgs e)
        {
            if (txtCogAfiliado.Text != "")
            {
                p_get_datos_solicitantexCC(txtCogAfiliado.Text);
            }
        }

        private void txtIDSolicitante_Leave(object sender, EventArgs e)
        {
            if (txtNoIdentidad.Text != "")
            {
                p_get_datos_solicitantexID(txtNoIdentidad.Text);
            }

        }

        private void p_get_datos_solicitantexID(string id_member)
        {
           DataTable dt = this.da.ObtenerDatosMemberxIdentificacion(id_member);

            if (dt.Rows.Count > 0)
            {
                txtNoIdentidad.Text = dt.Rows[0]["numero_identificacion"].ToString();
                txtCogAfiliado.Text = dt.Rows[0]["codigo_cliente"].ToString();
                txtNombreMiembro.Text = dt.Rows[0]["nombres"].ToString();
                txtPrimer_apellido.Text = dt.Rows[0]["primer_apellido"].ToString();
                txtxSegundoApellido.Text = dt.Rows[0]["segundo_apellido"].ToString();

                var codigo_id = dt.Rows[0]["codigo_tipo_identificacion"].ToString();

                nombre_completo = txtNombreMiembro.Text + " " + txtPrimer_apellido.Text + " " + txtxSegundoApellido.Text;

                //Evalua que la identificación tenga el formato correcto
                Regex rx = new Regex(@"(^([0-9]{4})-([0-9]{4})-([0-9]{5}))|(^([0-9]{2})-([0-9]{4})-([0-9]{4})-([0-9]{5}))");
                Match match = rx.Match(this.txtNoIdentidad.Text);
                bool pasar = match.Success;

                if (codigo_id.Equals("2"))
                {
                    pasar = true;
                }

                if (!pasar)
                {
                    char guion = '-';
                    int aparece = 0;

                    foreach (char item in this.txtNoIdentidad.Text)
                    {
                        if (item.Equals(guion))
                            aparece++;
                    }

                    if (aparece < 3)
                    {
                        MessageBox.Show("El formato de identificación no es válido, debe llevar guiones. Vaya a la solicitud de Afiliación para corregir", "Ir a la Solicitud de Afiliación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCogAfiliado.Text = " ";
                        txtNoIdentidad.Text = " ";
                        txtNombreMiembro.Text = " ";
                        txtPrimer_apellido.Text = " ";
                        txtxSegundoApellido.Text = " ";
                    }
                }

                if (string.IsNullOrEmpty(txtCogAfiliado.Text))
                {
                    txtCogAfiliado_Leave(null, null);
                }

            } else {
                MessageBox.Show("No existe información del cliente");
                txtCogAfiliado.Text = " ";
                txtNombreMiembro.Text = " ";
                txtPrimer_apellido.Text = " ";
                txtxSegundoApellido.Text = " ";
                this.skipMethod();
            }
        }

        private void p_get_datos_solicitantexCC(string p_codigo_cliente)
        {
            DataTable dt = this.da.ObtenerDatosCodigoClienteAddMember(p_codigo_cliente);
            if (dt.Rows.Count > 0)
            {
                var codigo_id = dt.Rows[0]["codigo_tipo_identificacion"].ToString();
                txtCogAfiliado.Text = dt.Rows[0]["codigo_cliente"].ToString();
                txtNoIdentidad.Text = dt.Rows[0]["numero_identificacion"].ToString();
                txtNombreMiembro.Text = dt.Rows[0]["nombres"].ToString();
                txtPrimer_apellido.Text = dt.Rows[0]["primer_apellido"].ToString();
                txtxSegundoApellido.Text = dt.Rows[0]["segundo_apellido"].ToString();

                nombre_completo = txtNombreMiembro.Text + " " + txtPrimer_apellido.Text + " " + txtxSegundoApellido.Text;

                //Evalua que la identificación tenga el formato correcto
                Regex rx = new Regex(@"(^([0-9]{4})-([0-9]{4})-([0-9]{5}))|(^([0-9]{2})-([0-9]{4})-([0-9]{4})-([0-9]{5}))");
                Match match = rx.Match(this.txtNoIdentidad.Text);
                bool pasar = match.Success;

                if (codigo_id.Equals("2"))
                {
                    pasar = true;
                }

                if (!pasar)
                {
                    char guion = '-';
                    int aparece = 0;

                    foreach (char item in this.txtNoIdentidad.Text)
                    {
                        if (item.Equals(guion))
                            aparece++;
                    }

                    if (aparece < 3)
                    {
                        MessageBox.Show("El formato de identificación no es válido, debe llevar guiones. Vaya a la solicitud de Afiliación para corregir", "Ir a la Solicitud de Afiliación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCogAfiliado.Text = " ";
                        txtNoIdentidad.Text = " ";
                        txtNombreMiembro.Text = " ";
                        txtPrimer_apellido.Text = " ";
                        txtxSegundoApellido.Text = " ";
                    }
                }

                if (string.IsNullOrEmpty(txtNoIdentidad.Text))
                {
                    txtIDSolicitante_Leave(null, null);
                }

            }else{
                MessageBox.Show("No existe información del cliente");
                txtCogAfiliado.Text = " ";
                txtNombreMiembro.Text = " ";
                txtPrimer_apellido.Text = " ";
                txtxSegundoApellido.Text = " ";
                p_get_datos_solicitantexID(txtNoIdentidad.Text);
                this.skipMethod();
            }
        }

        private void txtCogAfiliado_KeyPress(object sender, KeyPressEventArgs e)
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
            // al dar enter pasar al siguiente campo
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void txtCogAfiliado_Leave(object sender, EventArgs e)
        {
            if (txtCogAfiliado.Text != "")
            {
                p_get_datos_solicitantexCC(txtCogAfiliado.Text);
            }
        }

        //FELVIR01-20190612
        private void skipMethod()
        {
            this.Close();
        }

        private void txtNoIdentidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            // al dar enter pasar al siguiente campo
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void txtNoIdentidad_Leave(object sender, EventArgs e)
        {
            if (txtNoIdentidad.Text != "")
            {
                p_get_datos_solicitantexID(txtNoIdentidad.Text);
            }
        }

        private void btnAceptar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtCogAfiliado.Text) || string.IsNullOrEmpty(this.txtNoIdentidad.Text))
            {
                MessageBox.Show("Por Favor Llene los campos de Nombre e Identidad para poder ingresar un Miembro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (modo == "ACTUALIZA")
                {
                    vl_member_id = Convert.ToInt32(cbxTipoMiembro.GetItemText(cbxZona.SelectedValue));
                    vl_zone_id = Convert.ToInt32(cbxZona.GetItemText(cbxZona.SelectedValue));
                    ID_Miembro = txtNoIdentidad.Text;
                    bool upd = false;
                    upd = DocSys.Actualiza_Miembro_Comite( ID_Miembro, vl_zone_id, vl_member_id);

                    if (upd == true)
                    {
                        MessageBox.Show("Guardado Exitosamente");
                        Close();
                        s_consulta_miembros add = new s_consulta_miembros(da);
                        add.Show();
                    }
                    else
                    {
                        MessageBox.Show("hubo un error al intentar guardar la informació, por favor intente de nuevo");
                    }
                }
                else { 
                    Nombre_Miembro = nombre_completo;
                    ID_Miembro = txtNoIdentidad.Text;
                    vl_member_id = Convert.ToInt32(cbxTipoMiembro.GetItemText(cbxZona.SelectedValue));
                    vl_zone_id = Convert.ToInt32(cbxZona.GetItemText(cbxZona.SelectedValue));
                    cod_cliente = Convert.ToInt32(txtCogAfiliado.Text);
                    bool record = false;
                    record = DocSys.insertar_Miembro_Comite(Nombre_Miembro, ID_Miembro, vl_zone_id, vl_member_id, cod_cliente);

                    if (record == true)
                    {
                        MessageBox.Show("Guardado Exitosamente");
                    }
                    else
                    {
                        MessageBox.Show("hubo un error al intentar guardar la informació, por favor intente de nuevo");
                    }
                }
            }
            txtNombreMiembro.Clear();
            txtPrimer_apellido.Clear();
            txtxSegundoApellido.Clear();
            txtCogAfiliado.Clear();
            txtNoIdentidad.Clear();
        }
    }

}

