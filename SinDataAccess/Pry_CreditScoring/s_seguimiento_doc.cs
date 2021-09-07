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
    public partial class s_seguimiento_doc : Form
    {
        int vl_no_solicitud;

        public s_seguimiento_doc(int pa_no_solicitud)
        {
            InitializeComponent();
            vl_no_solicitud = pa_no_solicitud;
            txtNo_solicitud.Text = vl_no_solicitud.ToString();
        }
        private void s_seguimiento_doc_Load(object sender, EventArgs e)
        {
            p_llenar_grid_rutas();
        }

        private void p_llenar_grid_rutas()
        {
            try
            {
                string vl_sql;
                vl_sql = @"  Select enviado_por,
                                    fecha_envio,
                                    estacion_id_from,
                                    e1.nombre as nombre_from,
                                    estacion_id_to,
                                    '' as nombre_to,
                                    no_movimiento
                               from dcs_movimientos_solicitudes ms, 
                                    dcs_wf_estaciones e1
                              where ms.estacion_id_to=e1.estacion_id                                
                                and ms.estacion_id_from=0
                                and no_solicitud= :pa_no_solicitud                                                              
                             UNION
                             Select enviado_por,
                                    fecha_envio,
                                    estacion_id_from,
                                    e1.nombre as nombre_from,
                                    estacion_id_to,
                                    e2.nombre as nombre_to,
                                    no_movimiento
                               from dcs_movimientos_solicitudes ms, 
                                    dcs_wf_estaciones e1,
                                    dcs_wf_estaciones e2
                              where ms.estacion_id_from=e1.estacion_id
                                and ms.estacion_id_to=e2.estacion_id
                                and no_solicitud= :pa_no_solicitud
                              order by no_movimiento ";
                
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd2.CommandType = CommandType.Text;

                //───────────────────
                OracleParameter pa_param1 = new OracleParameter("pa_no_solicitud", OracleType.Int32);
                cmd2.Parameters.Add(pa_param1);
                pa_param1.Direction = ParameterDirection.Input;
                pa_param1.Value = int.Parse(txtNo_solicitud.Text);
                //───────────────────
                
                OracleDataReader dr = cmd2.ExecuteReader();

                DataTable table = new DataTable();
                table.Columns.Add("no_movimiento");
                table.Columns.Add("enviado_por");
                table.Columns.Add("fecha_envio");
                table.Columns.Add("nombre_from");
                table.Columns.Add("nombre_to");                
                while (dr.Read())
                {
                    table.Rows.Add(dr["no_movimiento"].ToString(),
                                   dr["enviado_por"].ToString(),
                                   dr["fecha_envio"].ToString(),
                                   dr["nombre_from"].ToString(),
                                   dr["nombre_to"].ToString());
                }                
                gvRuta.DataSource = table;
                gvRuta.Refresh();
                table.Dispose();
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void p_llenar_anotaciones(int p_no_solicitud,int p_no_movimiento)
        {
            string vl_sql;
            try
            {
                vl_sql = "";
                vl_sql = @" Select * 
                              from dcs_anotaciones_solicitudes 
                             Where no_solicitud=:pa_no_solicitud 
                               and no_movimiento_solicitud=:pa_no_movimiento 
                             Order by no_anotacion";
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd2.CommandType = CommandType.Text;

                //───────────────────
                OracleParameter pa_parametro1 = new OracleParameter("pa_no_solicitud", OracleType.Int32);
                cmd2.Parameters.Add(pa_parametro1);
                pa_parametro1.Direction = ParameterDirection.Input;
                pa_parametro1.Value = p_no_solicitud;

                OracleParameter pa_parametro2 = new OracleParameter("pa_no_movimiento", OracleType.Int32);
                cmd2.Parameters.Add(pa_parametro2);
                pa_parametro2.Direction = ParameterDirection.Input;
                pa_parametro2.Value = p_no_movimiento;

                OracleDataReader dr = cmd2.ExecuteReader();
                DataTable tabla = new DataTable();
                tabla.Columns.Add("no_anotacion");
                tabla.Columns.Add("anotacion");
                tabla.Columns.Add("usuario_ing");
                while (dr.Read())
                {
                    tabla.Rows.Add(dr["no_anotacion"].ToString(),
                                dr["anotacion"].ToString(),
                                dr["usuario_ing"].ToString());
                }

                // Suspending automatic refreshes as items are added/removed.

                list_anotaciones.BeginUpdate();
                list_anotaciones.SmallImageList = imagesSmall;
                list_anotaciones.LargeImageList = imagesLarge;
                list_anotaciones.Clear();
                foreach (DataRow fila in tabla.Rows)
                {
                    ListViewItem listItem = new ListViewItem(fila["anotacion"].ToString().Substring(0, 10));
                    listItem.ImageIndex = 4;


                    // Add sub-items for Details view.
                    listItem.SubItems.Add(fila["no_anotacion"].ToString());
                    listItem.SubItems.Add(fila["usuario_ing"].ToString());
                    list_anotaciones.Items.Add(listItem);
                }

                // Add column headers for Details view.
                list_anotaciones.Columns.Add("Anotacion", 180, HorizontalAlignment.Left);
                list_anotaciones.Columns.Add("No", 60, HorizontalAlignment.Left);
                list_anotaciones.Columns.Add("Usuario", 60, HorizontalAlignment.Left);

                // Re-enable the display.
                list_anotaciones.EndUpdate();
                list_anotaciones.Sort();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_llenar_anotaciones : " + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void gvRuta_SelectionChanged(object sender, EventArgs e)
        {
            
            int vl_no_movimiento = 0;
            DataGridViewRow row = gvRuta.CurrentRow;
            
            vl_no_movimiento = int.Parse(row.Cells["no_movimiento"].Value.ToString());
            
            p_llenar_anotaciones(vl_no_solicitud,vl_no_movimiento);
            
        }

        private void list_anotaciones_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = list_anotaciones.GetItemAt(e.X, e.Y);
            if (item != null)
            {
                //MessageBox.Show(item.SubItems[1].Text);
                p_abrir_anotacion(int.Parse(item.SubItems[1].Text));

            }
        }

        private void p_abrir_anotacion(int p_no_anotacion)
        {
            string vl_sql = "";
            vl_sql = vl_sql + "Select * from dcs_anotaciones_solicitudes where no_anotacion=:pa_no_anotacion";
            if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
            {
                DocSys.connOracle.Open();
            }
            OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
            cmd2.CommandType = CommandType.Text;
            //───────────────────
            OracleParameter pa_parametro1 = new OracleParameter("pa_no_anotacion", OracleType.Int32);
            cmd2.Parameters.Add(pa_parametro1);
            pa_parametro1.Direction = ParameterDirection.Input;
            pa_parametro1.Value = p_no_anotacion;
            //───────────────────

            OracleDataReader dr = cmd2.ExecuteReader();
            dr.Read();
            if (!DBNull.Value.Equals(dr["anotacion"].ToString()))
            {
               // s_add_notas forma = new s_add_notas("CONS", int.Parse(dr["no_solicitud"].ToString()), dr["anotacion"].ToString());
                //forma.ShowDialog();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            list_anotaciones.View = View.LargeIcon;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            list_anotaciones.View = View.List;
        }

        private void button_ejecutar_consulta_Click(object sender, EventArgs e)
        {
            p_llenar_grid_rutas();
        }

        private void txtNo_solicitud_MouseLeave(object sender, EventArgs e)
        {
            vl_no_solicitud = int.Parse(txtNo_solicitud.Text);
        }
        
    }
}
