using System;
using System.Data;
using System.Data.OracleClient;
using System.Windows.Forms;
using wfcModel;

namespace Docsis_Application.Excepciones
{
    public partial class e_Admin_Temp : Form
    {
        public DataAccess da;
        string vl_lu;
        string vl_ma;
        string vl_mi;
        string vl_ju;
        string vl_vi;
        string vl_sa;
        string vl_do;
        int vl_min_ini;
        int vl_min_fin;

        public e_Admin_Temp()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            vl_min_ini = Convert.ToInt32(cboMinI.SelectedIndex);
            vl_min_fin = Convert.ToInt32(cboMinF.SelectedIndex);
            validarDias();
            if (txtHoraInicial.Text == "" || txtHoraFinal.Text == "")
            {
                MessageBox.Show("Por favor llene los campos de las Horas!!");
            }
            else {
                if (DocSys.insertar_Horario(Convert.ToInt32(txtHoraInicial.Text), Convert.ToInt32(txtHoraFinal.Text), vl_lu, vl_ma, vl_mi, vl_ju, vl_vi, vl_sa, vl_do, vl_min_ini, vl_min_fin) == true)
                {
                    MessageBox.Show("Se Agrego con exito");
                    p_estado();
                    this.btnAdd.Enabled = false;
                    this.btnDesactivar.Enabled = true;
                    this.btnEditar.Enabled = true;
                    this.ckLu.Enabled = false;
                    this.ckMa.Enabled = false;
                    this.ckMi.Enabled = false;
                    this.ckJu.Enabled = false;
                    this.ckVi.Enabled = false;
                    this.ckSa.Enabled = false;
                    this.ckDo.Enabled = false;
                    this.cboMinI.Enabled = false;
                    this.cboMinF.Enabled = false;
                }
                else {
                    MessageBox.Show("Hubo un problema al intentar agregar el horario!! Intente nuevamente");
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            this.btnAceptar.Visible = true;
            this.txtHoraFinal.ReadOnly = false;
            this.txtHoraInicial.ReadOnly = false;

            this.ckLu.Enabled = true;
            this.ckMa.Enabled = true;
            this.ckMi.Enabled = true;
            this.ckJu.Enabled = true;
            this.ckVi.Enabled = true;
            this.ckSa.Enabled = true;
            this.ckDo.Enabled = true;
            this.cboMinI.Enabled = true;
            this.cboMinF.Enabled = true;
        }

        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            string motivo = btnDesactivar.Text;

            if (DocSys.Desactivar_Horario(motivo) == true)
            {
                MessageBox.Show("Se Actualizo con exito");
                p_estado();
            }
            else
            {
                MessageBox.Show("Hubo un problema al intentar actualizar el horario!! Intente nuevamente");
            }
        }

        private void e_Admin_Temp_Load(object sender, EventArgs e)
        {
            this.btnAdd.Enabled = false;
            this.btnDesactivar.Enabled = false;
            this.btnEditar.Enabled = false;
            this.txtEstado.ReadOnly = true;
            this.btnAceptar.Visible = false;

            this.ckLu.Enabled = false;
            this.ckMa.Enabled = false;
            this.ckMi.Enabled = false;
            this.ckJu.Enabled = false;
            this.ckVi.Enabled = false;
            this.ckSa.Enabled = false;
            this.ckDo.Enabled = false;
            this.cboMinI.Enabled = false;
            this.cboMinF.Enabled = false;

            cargaMinIni();
            cargaMinFIN();

            bool existe = DocSys.ObtenerDatoExisteHorario();

            if (existe == true)
            {
                p_Horas();
                p_estado();
                this.btnAdd.Visible = false;
                this.btnDesactivar.Enabled = true;
                this.btnEditar.Enabled = true;
                this.txtEstado.ReadOnly = true;
                this.txtHoraFinal.ReadOnly = true;
                this.txtHoraInicial.ReadOnly = true;
                this.cboMinI.Enabled = false;
                this.cboMinF.Enabled = false;
            }
            else {
                this.btnAdd.Enabled = true;
                this.btnEditar.Visible = false;
                this.btnDesactivar.Visible = false;
                this.txtEstado.ReadOnly = true;
                
                this.ckLu.Enabled = true;
                this.ckMa.Enabled = true;
                this.ckMi.Enabled = true;
                this.ckJu.Enabled = true;
                this.ckVi.Enabled = true;
                this.ckSa.Enabled = true;
                this.ckDo.Enabled = true;
                this.cboMinI.Enabled = true;
                this.cboMinF.Enabled = true;
            }
        }

        private void p_estado()
        {
            DataTable dt = DocSys.LlenarHorario();
            if (dt.Rows.Count > 0) {
                if (Convert.ToChar(dt.Rows[0]["ACTIVO"].ToString()) == 'I')
                {
                    txtEstado.Text = "INACTIVO";
                    btnDesactivar.Text = "Activar";
                }
                else {
                    txtEstado.Text = "ACTIVO";
                    btnDesactivar.Text = "Desactivar";
                }
            }
        }

        private void p_Horas()
        {
            DataTable dt = DocSys.LlenarHorario();
            if (dt.Rows.Count > 0)
            {
                txtHoraInicial.Text = dt.Rows[0]["HORA_INICIAL"].ToString();
                txtHoraFinal.Text = dt.Rows[0]["HORA_FINAL"].ToString();

                if (dt.Rows[0]["Monday"].ToString() == "Monday")
                    ckLu.Checked = true;
                else
                    ckLu.Checked = false;

                if (dt.Rows[0]["Tuesday"].ToString() == "Tuesday")
                    ckMa.Checked = true;
                else
                    ckMa.Checked = false;

                if (dt.Rows[0]["Wednesday"].ToString() == "Wednesday")
                    ckMi.Checked = true;
                else
                    ckMi.Checked = false;

                if (dt.Rows[0]["Thursday"].ToString() == "Thursday")
                    ckJu.Checked = true;
                else
                    ckJu.Checked = false;

                if (dt.Rows[0]["Friday"].ToString() == "Friday")
                    ckVi.Checked = true;
                else
                    ckVi.Checked = false;

                if (dt.Rows[0]["Saturday"].ToString() == "Saturday")
                    ckSa.Checked = true;
                else
                    ckSa.Checked = false;

                if (dt.Rows[0]["Sunday"].ToString() == "Sunday")
                    ckDo.Checked = true;
                else
                    ckDo.Checked = false;

                cboMinI.Text = dt.Rows[0]["MIN_INICIAL"].ToString();
                cboMinF.Text = dt.Rows[0]["MIN_FINAL"].ToString();
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            vl_min_ini = Convert.ToInt32(cboMinI.SelectedIndex);
            vl_min_fin = Convert.ToInt32(cboMinF.SelectedIndex);
            validarDias();
            if (DocSys.Actualizar_Horario(Convert.ToInt32(txtHoraInicial.Text), Convert.ToInt32(txtHoraFinal.Text), vl_lu, vl_ma, vl_mi, vl_ju, vl_vi, vl_sa, vl_do, vl_min_ini, vl_min_fin) == true)
            {
                MessageBox.Show("Se Actualizo con exito");
                p_estado();
                this.txtHoraFinal.ReadOnly = true;
                this.txtHoraInicial.ReadOnly = true;

                this.ckLu.Enabled = false;
                this.ckMa.Enabled = false;
                this.ckMi.Enabled = false;
                this.ckJu.Enabled = false;
                this.ckVi.Enabled = false;
                this.ckSa.Enabled = false;
                this.ckDo.Enabled = false;
                this.cboMinI.Enabled = false;
                this.cboMinF.Enabled = false;
            }
            else
            {
                MessageBox.Show("Hubo un problema al intentar actualizar el horario!! Intente nuevamente");
            }
        }

        public void validarDias() {
            if (ckLu.Checked==true)
                vl_lu = "Monday";
            else
                vl_lu = "";
            if (ckMa.Checked == true)
                vl_ma = "Tuesday";
            else
                vl_ma = "";
            if (ckMi.Checked == true)
                vl_mi = "Wednesday";
            else
                vl_mi = "";
            if (ckJu.Checked == true)
                vl_ju = "Thursday";
            else
                vl_ju = "";
            if (ckVi.Checked == true)
                vl_vi = "Friday";
            else
                vl_vi = "";
            if (ckSa.Checked == true)
                vl_sa = "Saturday";
            else
                vl_sa = "";
            if (ckDo.Checked == true)
                vl_do = "Sunday";
            else
                vl_do = "";
        }

        public void cargaMinIni() {
            cboMinI.DisplayMember = "Text";
            cboMinI.ValueMember = "Value";
            cboMinI.SelectedIndex = cboMinI.Items.IndexOf("00");

            cboMinI.Items.Add(new { Text = "00", Value = 0 });
            cboMinI.Items.Add(new { Text = "01", Value = 1 });
            cboMinI.Items.Add(new { Text = "02", Value = 2 });
            cboMinI.Items.Add(new { Text = "03", Value = 3 });
            cboMinI.Items.Add(new { Text = "04", Value = 4 });
            cboMinI.Items.Add(new { Text = "05", Value = 5 });
            cboMinI.Items.Add(new { Text = "06", Value = 6 });
            cboMinI.Items.Add(new { Text = "07", Value = 7 });
            cboMinI.Items.Add(new { Text = "08", Value = 8 });
            cboMinI.Items.Add(new { Text = "09", Value = 9 });
            cboMinI.Items.Add(new { Text = "10", Value = 10 });
            cboMinI.Items.Add(new { Text = "11", Value = 11 });
            cboMinI.Items.Add(new { Text = "12", Value = 12 });
            cboMinI.Items.Add(new { Text = "13", Value = 13 });
            cboMinI.Items.Add(new { Text = "14", Value = 14 });
            cboMinI.Items.Add(new { Text = "15", Value = 15 });
            cboMinI.Items.Add(new { Text = "16", Value = 16 });
            cboMinI.Items.Add(new { Text = "17", Value = 17 });
            cboMinI.Items.Add(new { Text = "18", Value = 18 });
            cboMinI.Items.Add(new { Text = "19", Value = 19 });
            cboMinI.Items.Add(new { Text = "20", Value = 20 });
            cboMinI.Items.Add(new { Text = "21", Value = 21 });
            cboMinI.Items.Add(new { Text = "22", Value = 22 });
            cboMinI.Items.Add(new { Text = "23", Value = 23 });
            cboMinI.Items.Add(new { Text = "24", Value = 24 });
            cboMinI.Items.Add(new { Text = "25", Value = 25 });
            cboMinI.Items.Add(new { Text = "26", Value = 26 });
            cboMinI.Items.Add(new { Text = "27", Value = 27 });
            cboMinI.Items.Add(new { Text = "28", Value = 28 });
            cboMinI.Items.Add(new { Text = "29", Value = 29 });
            cboMinI.Items.Add(new { Text = "30", Value = 30 });
            cboMinI.Items.Add(new { Text = "31", Value = 31 });
            cboMinI.Items.Add(new { Text = "32", Value = 32 });
            cboMinI.Items.Add(new { Text = "33", Value = 33 });
            cboMinI.Items.Add(new { Text = "34", Value = 34 });
            cboMinI.Items.Add(new { Text = "35", Value = 35 });
            cboMinI.Items.Add(new { Text = "36", Value = 36 });
            cboMinI.Items.Add(new { Text = "37", Value = 37 });
            cboMinI.Items.Add(new { Text = "38", Value = 38 });
            cboMinI.Items.Add(new { Text = "39", Value = 39 });
            cboMinI.Items.Add(new { Text = "40", Value = 40 });
            cboMinI.Items.Add(new { Text = "41", Value = 41 });
            cboMinI.Items.Add(new { Text = "42", Value = 42 });
            cboMinI.Items.Add(new { Text = "43", Value = 43 });
            cboMinI.Items.Add(new { Text = "44", Value = 44 });
            cboMinI.Items.Add(new { Text = "45", Value = 45 });
            cboMinI.Items.Add(new { Text = "46", Value = 46 });
            cboMinI.Items.Add(new { Text = "47", Value = 47 });
            cboMinI.Items.Add(new { Text = "48", Value = 48 });
            cboMinI.Items.Add(new { Text = "49", Value = 49 });
            cboMinI.Items.Add(new { Text = "50", Value = 50 });
            cboMinI.Items.Add(new { Text = "51", Value = 51 });
            cboMinI.Items.Add(new { Text = "52", Value = 52 });
            cboMinI.Items.Add(new { Text = "53", Value = 53 });
            cboMinI.Items.Add(new { Text = "54", Value = 54 });
            cboMinI.Items.Add(new { Text = "55", Value = 55 });
            cboMinI.Items.Add(new { Text = "56", Value = 56 });
            cboMinI.Items.Add(new { Text = "57", Value = 57 });
            cboMinI.Items.Add(new { Text = "58", Value = 58 });
            cboMinI.Items.Add(new { Text = "59", Value = 59 });
            cboMinI.SelectedIndex = 0;
        }

        public void cargaMinFIN()
        {
            cboMinF.DisplayMember = "Text";
            cboMinF.ValueMember = "Value";
            cboMinF.SelectedIndex = cboMinI.Items.IndexOf("00");

            cboMinF.Items.Add(new { Text = "00", Value = 0 });
            cboMinF.Items.Add(new { Text = "01", Value = 1 });
            cboMinF.Items.Add(new { Text = "02", Value = 2 });
            cboMinF.Items.Add(new { Text = "03", Value = 3 });
            cboMinF.Items.Add(new { Text = "04", Value = 4 });
            cboMinF.Items.Add(new { Text = "05", Value = 5 });
            cboMinF.Items.Add(new { Text = "06", Value = 6 });
            cboMinF.Items.Add(new { Text = "07", Value = 7 });
            cboMinF.Items.Add(new { Text = "08", Value = 8 });
            cboMinF.Items.Add(new { Text = "09", Value = 9 });
            cboMinF.Items.Add(new { Text = "10", Value = 10 });
            cboMinF.Items.Add(new { Text = "11", Value = 11 });
            cboMinF.Items.Add(new { Text = "12", Value = 12 });
            cboMinF.Items.Add(new { Text = "13", Value = 13 });
            cboMinF.Items.Add(new { Text = "14", Value = 14 });
            cboMinF.Items.Add(new { Text = "15", Value = 15 });
            cboMinF.Items.Add(new { Text = "16", Value = 16 });
            cboMinF.Items.Add(new { Text = "17", Value = 17 });
            cboMinF.Items.Add(new { Text = "18", Value = 18 });
            cboMinF.Items.Add(new { Text = "19", Value = 19 });
            cboMinF.Items.Add(new { Text = "20", Value = 20 });
            cboMinF.Items.Add(new { Text = "21", Value = 21 });
            cboMinF.Items.Add(new { Text = "22", Value = 22 });
            cboMinF.Items.Add(new { Text = "23", Value = 23 });
            cboMinF.Items.Add(new { Text = "24", Value = 24 });
            cboMinF.Items.Add(new { Text = "25", Value = 25 });
            cboMinF.Items.Add(new { Text = "26", Value = 26 });
            cboMinF.Items.Add(new { Text = "27", Value = 27 });
            cboMinF.Items.Add(new { Text = "28", Value = 28 });
            cboMinF.Items.Add(new { Text = "29", Value = 29 });
            cboMinF.Items.Add(new { Text = "30", Value = 30 });
            cboMinF.Items.Add(new { Text = "31", Value = 31 });
            cboMinF.Items.Add(new { Text = "32", Value = 32 });
            cboMinF.Items.Add(new { Text = "33", Value = 33 });
            cboMinF.Items.Add(new { Text = "34", Value = 34 });
            cboMinF.Items.Add(new { Text = "35", Value = 35 });
            cboMinF.Items.Add(new { Text = "36", Value = 36 });
            cboMinF.Items.Add(new { Text = "37", Value = 37 });
            cboMinF.Items.Add(new { Text = "38", Value = 38 });
            cboMinF.Items.Add(new { Text = "39", Value = 39 });
            cboMinF.Items.Add(new { Text = "40", Value = 40 });
            cboMinF.Items.Add(new { Text = "41", Value = 41 });
            cboMinF.Items.Add(new { Text = "42", Value = 42 });
            cboMinF.Items.Add(new { Text = "43", Value = 43 });
            cboMinF.Items.Add(new { Text = "44", Value = 44 });
            cboMinF.Items.Add(new { Text = "45", Value = 45 });
            cboMinF.Items.Add(new { Text = "46", Value = 46 });
            cboMinF.Items.Add(new { Text = "47", Value = 47 });
            cboMinF.Items.Add(new { Text = "48", Value = 48 });
            cboMinF.Items.Add(new { Text = "49", Value = 49 });
            cboMinF.Items.Add(new { Text = "50", Value = 50 });
            cboMinF.Items.Add(new { Text = "51", Value = 51 });
            cboMinF.Items.Add(new { Text = "52", Value = 52 });
            cboMinF.Items.Add(new { Text = "53", Value = 53 });
            cboMinF.Items.Add(new { Text = "54", Value = 54 });
            cboMinF.Items.Add(new { Text = "55", Value = 55 });
            cboMinF.Items.Add(new { Text = "56", Value = 56 });
            cboMinF.Items.Add(new { Text = "57", Value = 57 });
            cboMinF.Items.Add(new { Text = "58", Value = 58 });
            cboMinF.Items.Add(new { Text = "59", Value = 59 });
            cboMinF.SelectedIndex = 0;
        }

    }
}
