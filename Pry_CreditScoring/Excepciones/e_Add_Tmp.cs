using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wfcModel;

namespace Docsis_Application.Excepciones
{
    public partial class e_Add_Tmp : Form
    {
        int vl_zone_id = 0;
        public DataAccess da;

        public e_Add_Tmp()
        {
            InitializeComponent();
        }
       
        //metodo para llenado del combobox de la zonas de filial
        private void P_Llenar_Combo_Regionales()
        {
            try
            {
                DataSet rzCombo = new DataSet();
                rzCombo = DocSys.p_Obtener_un_dataset("SELECT COD_ZONA, DESCRIPCION FROM EXCP.DCS_EXC_ZONA", "DCS_EXC_ZONA");
                cbxZona.DataSource = rzCombo;
                cbxZona.DisplayMember = "DCS_EXC_ZONA.DESCRIPCION";
                cbxZona.ValueMember = "DCS_EXC_ZONA.COD_ZONA";
                cbxZona.SelectedValue = vl_zone_id;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha produido un error al intenar llenar los Registros de las Regionales");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            salir();
        }

        private void e_Add_Tmp_Load(object sender, EventArgs e)
        {
            P_Llenar_Combo_Regionales();
        }
        
        private void txtCogAfiliado_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCogAfiliado.Text))
            {
                MessageBox.Show("Por favor no deje vacio el campo");
            }
            else
            {
                p_get_nombre(txtCogAfiliado.Text);
            }
        }

        private void p_get_nombre(string codigo)
        {
            string name = string.Empty;
            name = DocSys.p_obtener_nombre(codigo);

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("No existe información del cliente");
                txtCogAfiliado.Text = "";
            }
            else
            {
                txtNombreMiembro.Text = name;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            salir();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            int existe = 0;
            if (string.IsNullOrEmpty(txtCogAfiliado.Text)||cbxZona.SelectedValue == null)
            {
                MessageBox.Show("¡debe llenar todos los campos para poder continuar!");
            }
            else
            {
                if (dtstart.Value >= dtend.Value)
                {
                    MessageBox.Show("por favor, valide que la fecha final sea mayor a la inicial");
                }
                else
                {
                    bool record = false;
                    int vl_zone_id = Convert.ToInt32(cbxZona.GetItemText(cbxZona.SelectedValue));
                    string vl_name = Convert.ToString(txtCogAfiliado.Text);
                    string vl_date = Convert.ToString(dtstart.Value.ToString("dd/MM/yyyy"));
                    string vl_date_end = Convert.ToString(dtend.Value.ToString("dd/MM/yyyy"));
                    string vl_tipo_admin;

                    //validar si no tiene un estado activo
                    DataTable dt = DocSys.AdminTemporal(txtCogAfiliado.Text, vl_zone_id);
                    if (dt.Rows.Count > 0) {
                        int zone = Convert.ToInt32(dt.Rows[0]["COD_ZONA"].ToString());
                        DateTime ini = Convert.ToDateTime(dt.Rows[0]["INICA"].ToString());
                        DateTime fini = Convert.ToDateTime(dt.Rows[0]["FINALIZA"].ToString());
                        DateTime act = DateTime.Now;
                        if(ini <= act && fini >= act && vl_zone_id == zone)
                        {
                            existe = 1;
                        }
                        else{
                            DateTime inio = Convert.ToDateTime(vl_date);
                            DateTime final = Convert.ToDateTime(vl_date_end);

                            if (radioButton1.Checked == true) {
                                vl_tipo_admin = "S";
                                record = DocSys.insertar_Admin_Tmp(vl_name, 0, inio, final, vl_tipo_admin);
                            }
                            else
                            {
                                if (radioButton2.Checked == true)
                                {
                                    vl_tipo_admin = "E";
                                    record = DocSys.insertar_Admin_Tmp(vl_name, vl_zone_id, inio, final,vl_tipo_admin);
                                }
                                else{
                                    MessageBox.Show("debe elegir que tipo de adminiastrador desea");
                                }
                            }
                        }
                    }
                    else
                    {
                        DateTime inio = Convert.ToDateTime(vl_date);
                        DateTime final = Convert.ToDateTime(vl_date_end);
                        if (radioButton1.Checked == true)
                        {
                            vl_tipo_admin = "S";
                            record = DocSys.insertar_Admin_Tmp(vl_name, 0, inio, final, vl_tipo_admin);
                        }
                        else
                        {
                            if (radioButton2.Checked == true)
                            {
                                vl_tipo_admin = "E";
                                record = DocSys.insertar_Admin_Tmp(vl_name, vl_zone_id, inio, final, vl_tipo_admin);
                            }
                            else
                            {
                                MessageBox.Show("debe elegir que tipo de adminiastrador desea");
                            }
                        }
                    }

                    if (record == true)
                    {
                        MessageBox.Show("Guardado Exitosamente");
                        this.Close();
                    }
                    else
                    {
                        if (existe == 1)
                        {
                            MessageBox.Show("este usuario aun tiene su periodo activo para supervisar otra zona");
                            salir();
                        }
                        else {
                            MessageBox.Show("hubo un error al intentar guardar la información, por favor intente de nuevo");
                            txtCogAfiliado.Text = "";
                            txtNombreMiembro.Text = "";
                        }
                    }
                }
            }
        }

        public void salir() {
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.cbxZona.Visible = false;
            this.label4.Visible = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.cbxZona.Visible = true;
            this.label4.Visible = true;
        }
    }
}
