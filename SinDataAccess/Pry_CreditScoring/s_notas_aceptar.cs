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
    public partial class s_notas_aceptar : Form
    {
        public s_notas_aceptar()
        {
            InitializeComponent();
        }

        private void s_notas_aceptar_Load(object sender, EventArgs e)
        {
            p_llenar_grid_anotaciones();            
        }
        public void p_llenar_grid_anotaciones()
        {            
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string sql = @"Select a.fecha_ing,a.no_solicitud,c.nombres||' '||primer_apellido||' '||segundo_apellido nombre_cliente, 
                                      a.estacion_id, e.nombre estacion, a.usuario_ing,a.anotacion,aceptada,aceptada_por,a.no_anotacion
                                 From dcs_anotaciones_solicitudes a,                            
                                      dcs_solicitudes s,
                                      mgi_clientes c,
                                      dcs_wf_estaciones e
                                Where a.no_solicitud=s.no_solicitud    
                                  and a.estacion_id=e.estacion_id    
                                  and s.codigo_cliente=c.codigo_cliente   
                                  and c.codigo_empresa=cr_globales.C_EMPRESA
                                  and a.tipo_anotacion='C'
                                  and a.aceptada='N' 
                                  and a.usuario_ing=:pa_user";
                OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
                cmd.CommandType = CommandType.Text;
                //────────────────────────
                OracleParameter pa_user = new OracleParameter("pa_user", OracleType.VarChar);
                cmd.Parameters.Add(pa_user);
                pa_user.Direction = ParameterDirection.Input;
                pa_user.Value = DocSys.vl_user;
                //---------------
                OracleDataReader dr = cmd.ExecuteReader();
                DataTable mitable = new DataTable("clientes");

                
                mitable.Columns.Add("fecha_ing");
                mitable.Columns.Add("no_solicitud");
                mitable.Columns.Add("nombre_cliente");
                mitable.Columns.Add("estacion");
                mitable.Columns.Add("analista");                
                mitable.Columns.Add("aceptada");
                mitable.Columns.Add("aceptada_por");
                mitable.Columns.Add("no_anotacion");
                mitable.Columns.Add("anotacion");

                while (dr.Read())
                {
                    mitable.Rows.Add(dr["fecha_ing"].ToString(),
                                     dr["no_solicitud"].ToString(),
                                     dr["nombre_cliente"].ToString(),
                                     dr["estacion"].ToString(),
                                     dr["usuario_ing"].ToString(),
                                     dr["aceptada"].ToString(),
                                     dr["aceptada_por"].ToString(),
                                     dr["no_anotacion"].ToString(),
                                     dr["anotacion"].ToString());

                }
                gv_Anotaciones.DataSource = mitable;
                gv_Anotaciones.Columns[6].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                gv_Anotaciones.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                gv_Anotaciones.Refresh();
                dr.Close();
            }
            catch (Exception ex)
            {
                
            }
            this.Cursor = Cursors.Default;
        }        
        private void gv_Anotaciones_SelectionChanged(object sender, EventArgs e)
        {
            string vl_anotacion = "";
            DataGridViewRow row = gv_Anotaciones.CurrentRow;
            if (row != null)
            {
                vl_anotacion = row.Cells["anotacion"].Value.ToString();
                txtTexto.Text = vl_anotacion;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string vl_anotacion = "";
            int vl_no_solicitud=0;
            int vl_no_anotacion=0;
            DataGridViewRow row = gv_Anotaciones.CurrentRow;
            if (row != null)
            {
                vl_anotacion = row.Cells["anotacion"].Value.ToString();
                vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());
                vl_no_anotacion = int.Parse(row.Cells["no_anotacion"].Value.ToString());
                txtTexto.Text = vl_anotacion;
            }
            if (MessageBox.Show("Desea dar por aceptada la nota de condición para la solicitud No. "+vl_no_solicitud.ToString()+" ?",DocSys.vl_mensaje_avisos,MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.No)
            {
                return;
            }
            p_aceptar_anotacion(vl_no_anotacion);
            MessageBox.Show("Anotacion aceptada satisfactoriamente !", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtTexto.Clear();
            p_llenar_grid_anotaciones();

        }

        private void p_aceptar_anotacion(int p_no_anotacion)
        {
            try
            {
                string vl_tipo_anotacion = "N";
                
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string vl_sql = "";
                vl_sql = vl_sql + "dcs_p_anotacion_aceptar";
                OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd.CommandType = CommandType.StoredProcedure;
                //───────────────────
                OracleParameter pa_no_anotacion = new OracleParameter("pa_no_anotacion", OracleType.Int32);
                cmd.Parameters.Add(pa_no_anotacion);
                pa_no_anotacion.Direction = ParameterDirection.Input;
                pa_no_anotacion.Value = p_no_anotacion;                                

                //───────────────────
                cmd.ExecuteReader();                                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_aceptar_anotacion :" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
       
    }
}
