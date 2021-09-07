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
    public partial class s_recibirdocs : Form
    {
        int vl_no_movimiento = 0;

        public s_recibirdocs(int p_no_movimiento)
        {
            InitializeComponent();
            vl_no_movimiento = p_no_movimiento;
            StatusLabel_no_movimiento.Text = "No. Movimiento :" + vl_no_movimiento.ToString();
        }

        private void button_cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_recibir_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("Marcar como recibida la documentación de esta solicitud de crédito  ?", DocSys.vl_mensaje_avisos, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                return;
            }
            p_marcar_como_recibido();
        }

        private void p_marcar_como_recibido()
        {
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string vl_sql = "";
                vl_sql = vl_sql + "DCS_P_MARCAR_RECIBIDO";
                OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd.CommandType = CommandType.StoredProcedure;
                //───────────────────
                OracleParameter pa_no_movimiento = new OracleParameter("pa_no_movimiento", OracleType.Int32);
                cmd.Parameters.Add(pa_no_movimiento);
                pa_no_movimiento.Direction = ParameterDirection.Input;
                pa_no_movimiento.Value = vl_no_movimiento;
                //───────────────────
                OracleParameter pa_user = new OracleParameter("pa_user", OracleType.VarChar, 30);
                cmd.Parameters.Add(pa_user);
                pa_user.Direction = ParameterDirection.Output;
                //───────────────────
                OracleParameter pa_fecha = new OracleParameter("pa_fecha", OracleType.DateTime);
                cmd.Parameters.Add(pa_fecha);
                pa_fecha.Direction = ParameterDirection.Output;
                cmd.ExecuteReader();
                textBox_recibido_por.Text =pa_user.Value.ToString();
                textBox_fecha_recibido.Text=pa_fecha.Value.ToString();

                button_recibir.Enabled = false;
                textBox_mensajes.Text = "Documentos recibidos satisfactoriamente, mensaje de confirmación de recepcion enviado al remitente";
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("p_marcar_como_recibido " + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
    }
}
