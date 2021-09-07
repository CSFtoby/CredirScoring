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
    public partial class s_miniinfo_asignacion : Form
    {
        public int get_set_no_movimiento { get; set; }
        public int vl_no_movimiento=0;

        public s_miniinfo_asignacion()
        {
            InitializeComponent();
        }

        private void s_miniinfo_asignacion_Load(object sender, EventArgs e)
        {

        }

        private void p_info_asignacion()
        {
            if (DocSys.connOracle.State == ConnectionState.Closed)
            {
                DocSys.connOracle.Open();
            }
            string vl_sql = @"Select no_solicitud,analista,fecha_asignacion,asignado_por 
                                from dcs_solicitudes_asignaciones
                               Where no_movimiento_solicitud=:pa_no_movimiento_solicitud
                                 and asignacion_actual='S'";
            OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("pa_no_movimiento_solicitud", OracleType.Int32).Value = vl_no_movimiento;
            OracleDataReader dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {


                label_nombre_analista.Text = p_get_nombre_usuario(dr["analista"].ToString())+ " ("+ dr["analista"].ToString()+")";
                label_fecha_asignacion.Text = dr["fecha_asignacion"].ToString();
                label_nombre_asignado_por.Text = p_get_nombre_usuario(dr["asignado_por"].ToString()) + " (" + dr["asignado_por"].ToString() + ")";
            }
        }

        private string p_get_nombre_usuario(string pa_usuario)
        {
            string vl_return = "";
            if (DocSys.connOracle.State == ConnectionState.Closed)
            {
                DocSys.connOracle.Open();
            }
            string vl_sql = @"Select Initcap(nombres)||' '||InitCap(primer_apellido) nombre_completo                                                                     
                                From mgi_usuarios u                                     
                               Where u.codigo_empresa=1                                 
                                 and codigo_usuario=:pa_codigo_usuario";
            OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("pa_codigo_usuario", OracleType.VarChar,50).Value = pa_usuario;
            OracleDataReader dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                vl_return = dr["nombre_completo"].ToString();
            }
            return vl_return;
        }
        
        private void s_miniinfo_asignacion_Activated(object sender, EventArgs e)
        {
            vl_no_movimiento=get_set_no_movimiento;
            p_info_asignacion();
        }
    }
}
