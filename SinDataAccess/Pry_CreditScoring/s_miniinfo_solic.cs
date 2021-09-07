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
    public partial class s_miniinfo_solic : Form
    {
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

        public string get_set_oficial { get; set; }
        public string get_set_filial_origen { get; set; }
        public string get_set_no_solicitud { get; set; }
        public string get_set_codigo_cliente { get; set; }
        public string get_set_nombre_cliente { get; set; }
        public string get_set_producto { get; set; }
        public string get_set_monto { get; set; }
        public string get_set_plazo { get; set; }
        public string get_set_estacion_act { get; set; }
        public string get_set_no_movimiento { get; set; }

        public s_miniinfo_solic()
        {
            InitializeComponent();
        }

        private void s_miniinfo_solic_Activated(object sender, EventArgs e)
        {
            label_oficial_servicio.Text = get_set_oficial;
            label_filial_origen.Text = get_set_filial_origen;
            label_no_solicitud.Text = get_set_no_solicitud;
            label_codigo_cliente.Text = get_set_codigo_cliente;
            label_nombre_cliente.Text = get_set_nombre_cliente;
            label_producto.Text = get_set_producto;
            label_monto.Text = double.Parse(get_set_monto).ToString("##,###,###,##0.00");
            label_plazo.Text = get_set_plazo;

           
            p_get_info_last_mov();
        }

        private void p_get_info_last_mov()
        {
            try
            {
                string vl_sql = @" Select no_movimiento,
                                         e.descripcion desc_estado,
                                         estacion_id_from,
                                         efrom.nombre estacion_de,
                                         d.descripcion desc_decision, 
                                         estacion_id_to,
                                         eto.nombre estacion_para
                                    from dcs_solicitudes s,
                                         dcs_wf_estado_solicitudes e,
                                         dcs_movimientos_solicitudes m,
                                         dcs_wf_estaciones efrom,
                                         dcs_wf_estaciones eto,
                                         dcs_wf_decisiones d
                                   Where s.no_movimiento_actual=m.no_movimiento
                                     and m.estacion_id_from=efrom.estacion_id(+)
                                     and m.estacion_id_to=eto.estacion_id
                                     and s.estado_solicitud_id=e.estado_id
                                     and m.decision_id=d.decision_id(+)
                                     and s.no_solicitud=:pa_no_solicitud";
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd2.CommandType = CommandType.Text;

                //───────────────────
                OracleParameter pa_no_solicitud = new OracleParameter("pa_no_solicitud", OracleType.Int32);
                cmd2.Parameters.Add(pa_no_solicitud);
                pa_no_solicitud.Direction = ParameterDirection.Input;
                pa_no_solicitud.Value = int.Parse(get_set_no_solicitud);
                //───────────────────            
                OracleDataReader dr = cmd2.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    label_desc_estado.Text = dr["desc_estado"].ToString();
                    label_estacion_de.Text = dr["estacion_de"].ToString();
                    label_estacion_para.Text = dr["estacion_para"].ToString();
                    label_decision.Text = dr["desc_decision"].ToString();
                    groupBox4.Text = "Ultimo  movimiento (" + dr["no_movimiento"].ToString() + ")";
                }
                else
                {
                    label_desc_estado.Text = "";
                    label_estacion_de.Text = "";
                    label_estacion_para.Text = "";
                    label_decision.Text = "";
                    groupBox4.Text = "Ultimo  movimiento ("  + ")";
                }
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
    }
}
