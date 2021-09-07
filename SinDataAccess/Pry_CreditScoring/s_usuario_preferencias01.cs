using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Docsis_Application
{
    public partial class s_usuario_preferencias01 : Form
    {
        int vl_estacion_id = 0;
        int vl_r = 0;
        int vl_g = 0;
        int vl_b = 0;

        public s_usuario_preferencias01()
        {
            InitializeComponent();
        }
        private void s_usuario_preferencias01_Load(object sender, EventArgs e)
        {
            p_llenar_combo_estaciones();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.AllowFullOpen = true;
            colorDlg.AnyColor = true;
            //colorDlg.SolidColorOnly = false;
            colorDlg.Color = Color.Red;
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                vl_r=colorDlg.Color.R;
                vl_g = colorDlg.Color.G;
                vl_b = colorDlg.Color.B;
                panel1.BackColor = Color.FromArgb(vl_r, vl_g, vl_b);
            }
        }

       

        private void p_llenar_combo_estaciones()
        {
            try
            {
                string vl_query = @"select * from dcs_wf_estaciones where activo='S'";
                OracleCommand cmd = new OracleCommand(vl_query, DocSys.connOracle);
                cmd.CommandType = CommandType.Text;
                //───────────────────

                OracleDataAdapter adaptador = new OracleDataAdapter(cmd);
                DataSet registros = new DataSet();
                adaptador.Fill(registros, "dcs_wf_estaciones");
                comboBoxEstaciones.DataSource = registros;
                comboBoxEstaciones.DisplayMember = "dcs_wf_estaciones.nombre";
                comboBoxEstaciones.ValueMember = "dcs_wf_decisiones.estacion_id";
                comboBoxEstaciones_SelectionChangeCommitted(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en llenar combo_estaciones : " + ex.Message);
            }
        }

        private void p_get_color_rgb_fila_estacion_from(int p_estacion_id_from, out int p_esquema_rgb_R, out int p_esquema_rgb_G, out int p_esquema_rgb_B)
        {

            string sql = @"Select * 
                            From dcs_wf_usuarios_preferencias 
                            Where usuario=:pa_usuario
                              and estacion_id_from=:pa_estacion_id_from";

            if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
            {
                DocSys.connOracle.Open();
            }
            OracleCommand cmd2 = new OracleCommand(sql, DocSys.connOracle);
            cmd2.CommandType = CommandType.Text;
            //───────────────────
            OracleParameter pa_usuario = new OracleParameter("pa_usuario", OracleType.VarChar, 50);
            cmd2.Parameters.Add(pa_usuario);
            pa_usuario.Direction = ParameterDirection.Input;
            pa_usuario.Value = DocSys.vl_user;

            OracleParameter pa_estacion_id_from = new OracleParameter("pa_estacion_id_from", OracleType.Int32);
            cmd2.Parameters.Add(pa_estacion_id_from);
            pa_estacion_id_from.Direction = ParameterDirection.Input;
            pa_estacion_id_from.Value = p_estacion_id_from;
            OracleDataReader dr = cmd2.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                p_esquema_rgb_R = int.Parse(dr["color_rgb_r"].ToString());
                p_esquema_rgb_G = int.Parse(dr["color_rgb_g"].ToString());
                p_esquema_rgb_B = int.Parse(dr["color_rgb_b"].ToString());
            }
            else
            {
                p_esquema_rgb_R = 0;
                p_esquema_rgb_G = 0;
                p_esquema_rgb_B = 0;
            }
        }

        private void comboBoxEstaciones_SelectionChangeCommitted(object sender, EventArgs e)
        {
          
            vl_estacion_id = int.Parse(comboBoxEstaciones.SelectedValue.ToString());
            p_get_color_rgb_fila_estacion_from(vl_estacion_id, out vl_r, out vl_g, out vl_b);
            if (vl_r > 0 | vl_g > 0 | vl_b > 0)
                panel1.BackColor = Color.FromArgb(vl_r, vl_g, vl_b);
            else
                panel1.BackColor = Color.Transparent;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            p_actualizar_preferencia();
        }

        private void p_actualizar_preferencia()
        {
            string vl_quitarcolor = "N";
            if (checkBox_quitarcolor.Checked)
                vl_quitarcolor = "S";
            try
            {

                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                
                string vl_sql = "";
                vl_sql = vl_sql + "DCS_P_PREFERENCE_INSERTAR";
                OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd.CommandType = CommandType.StoredProcedure;
                //───────────────────
                OracleParameter pa_usuario = new OracleParameter("pa_usuario", OracleType.VarChar, 50);
                cmd.Parameters.Add(pa_usuario);
                pa_usuario.Direction = ParameterDirection.Input;
                pa_usuario.Value = DocSys.vl_user;
                //───────────────────
                OracleParameter pa_preferencia = new OracleParameter("pa_preferencia", OracleType.VarChar, 50);
                cmd.Parameters.Add(pa_preferencia);
                pa_preferencia.Direction = ParameterDirection.Input;
                pa_preferencia.Value = "CFFEF";

                OracleParameter pa_quitar = new OracleParameter("pa_quitar", OracleType.VarChar, 50);
                cmd.Parameters.Add(pa_quitar);
                pa_quitar.Direction = ParameterDirection.Input;
                pa_quitar.Value = vl_quitarcolor;

                OracleParameter pa_estacion_id_from = new OracleParameter("pa_estacion_id_from", OracleType.VarChar, 10);
                cmd.Parameters.Add(pa_estacion_id_from);
                pa_estacion_id_from.Direction = ParameterDirection.Input;
                pa_estacion_id_from.Value = vl_estacion_id;

                OracleParameter pa_color_rgb_r = new OracleParameter("pa_color_rgb_r", OracleType.Int16);
                cmd.Parameters.Add(pa_color_rgb_r);
                pa_color_rgb_r.Direction = ParameterDirection.Input;
                pa_color_rgb_r.Value = vl_r;

                OracleParameter pa_color_rgb_g = new OracleParameter("pa_color_rgb_g", OracleType.Int16);
                cmd.Parameters.Add(pa_color_rgb_g);
                pa_color_rgb_g.Direction = ParameterDirection.Input;
                pa_color_rgb_g.Value = vl_g;

                OracleParameter pa_color_rgb_b = new OracleParameter("pa_color_rgb_b", OracleType.Int16);
                cmd.Parameters.Add(pa_color_rgb_b);
                pa_color_rgb_b.Direction = ParameterDirection.Input;
                pa_color_rgb_b.Value = vl_b;
                

                //───────────────────
                cmd.ExecuteReader();
                this.DialogResult = DialogResult.OK;
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_actualizar_preferencia :" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }

        
    }

}
