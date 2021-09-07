using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using wfcModel;

namespace Docsis_Application
{
    public partial class s_solic_abiertas : Form
    {
        public DataAccess da;
        bool con_borde = MDI_Menu.con_borde;

        #region
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


        s_miniinfo_solic miniinfo_sol = new s_miniinfo_solic();
        bool vl_mostrar_miniinfo = true;

        bool global_ver_toda_filial = DocSys.p_obtener_si_todas_las_filiales(DocSys.p_obtener_estacion_usuario(DocSys.vl_user));
        bool global_gerente_filial = DocSys.p_obtener_es_gerente_filial(DocSys.vl_user);

        public s_solic_abiertas()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void p_llenar_grid_solicitudes()
        {
            string vl_condi_abierta = " and s.abierta = 'S' ";
            
            try
            {
                string vl_condi_workflow = "";
                string vl_condi_toda_filial = "";
                string vl_condi_gte_filial = "";
                string vl_condi_busqueda = "";
                

                if (global_ver_toda_filial)
                {
                    vl_condi_toda_filial = " and :pa_codigo_agencia_origen=:pa_codigo_agencia_origen";
                }
                else
                {
                    vl_condi_toda_filial = " and s.codigo_agencia_origen=:pa_codigo_agencia_origen ";

                    if (global_gerente_filial)
                    {
                        vl_condi_gte_filial = "";
                    }
                    else
                    {
                        vl_condi_gte_filial = " and oficial_servicio='" + DocSys.vl_user.Trim() + "'";
                    }
                }


                string vl_sql = @"
                       Select distinct s.no_solicitud,
                              s.no_solicitud_formulario,
                              es.descripcion estado_solicitud,
                              initcap(desc_sub_aplicacion) desc_sub_aplicacion,
                              fecha_presentacion,
                              initcap(nombre_agencia) nombre_agencia,
                              oficial_servicio,
                              ms.analista,
                              s.codigo_cliente,
                              f.descripcion_fuente,
                              initcap(nombres)||' '||initcap(primer_apellido)||' '||initcap(segundo_apellido) nombre_cliente,
                              m.desc_moneda,
                              monto_solicitado,
                              meses_plazo,                              
                              no_movimiento,                              
                              e.nombre estacion_actual
                         from dcs_solicitudes s,
                              mgi_sub_aplicaciones sa,
                              mgi_monedas m,
                              mgi_agencias a,
                              mgi_clientes c,
                              dcs_movimientos_solicitudes ms,
                              dcs_wf_fuentes_financiamiento f,
                              dcs_wf_estaciones e,
                              dcs_wf_estado_solicitudes es
                         where ms.no_solicitud=s.no_solicitud 
                           and s.estado_solicitud_id=es.estado_id
                           and s.codigo_sub_aplicacion=sa.codigo_sub_aplicacion
                           and s.codigo_fuente=f.codigo_fuente
                           and s.codigo_agencia_origen=a.codigo_agencia
                           and s.codigo_moneda=m.codigo_moneda
                           and s.codigo_cliente=c.codigo_cliente
                           and s.estacion_id=e.estacion_id
                           and s.workflow_id=ms.workflow_id
                           and s.estacion_id=ms.estacion_id_to
                           and s.no_movimiento_actual=ms.no_movimiento(+)" +
                           vl_condi_workflow +
                           vl_condi_toda_filial +
                           vl_condi_gte_filial +                           
                           vl_condi_abierta +
                    @"  Order by s.no_solicitud desc ";
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd2.CommandType = CommandType.Text;
                //───────────────────


                OracleParameter pa_codigo_agencia_origen = new OracleParameter("pa_codigo_agencia_origen", OracleType.Int32);
                cmd2.Parameters.Add(pa_codigo_agencia_origen);
                pa_codigo_agencia_origen.Direction = ParameterDirection.Input;
                pa_codigo_agencia_origen.Value = DocSys.vl_agencia_usuario;

                OracleDataReader dr = cmd2.ExecuteReader();
                DataTable table = new DataTable();
                table.Columns.Add("no_solicitud");
                table.Columns.Add("no_solicitud_formulario");
                table.Columns.Add("estado_solicitud");
                table.Columns.Add("desc_sub_aplicacion");
                table.Columns.Add("fecha_presentacion");
                table.Columns.Add("nombre_agencia");
                table.Columns.Add("oficial_servicio");
                table.Columns.Add("analista");
                table.Columns.Add("descripcion_fuente");
                table.Columns.Add("codigo_cliente");
                table.Columns.Add("nombre_cliente");
                table.Columns.Add("desc_moneda");
                table.Columns.Add("monto_solicitado");
                table.Columns.Add("meses_plazo");
                table.Columns.Add("estacion_actual");
                table.Columns.Add("no_movimiento");
                while (dr.Read())
                {
                    table.Rows.Add(dr["no_solicitud"].ToString(),
                                   dr["no_solicitud_formulario"].ToString(),
                                   dr["estado_solicitud"].ToString(),
                                   dr["desc_sub_aplicacion"].ToString(),
                                   dr["fecha_presentacion"].ToString(),
                                   dr["nombre_agencia"].ToString(),
                                   dr["oficial_servicio"].ToString(),
                                   dr["analista"].ToString(),
                                   dr["descripcion_fuente"].ToString(),
                                   dr["codigo_cliente"].ToString(),
                                   dr["nombre_cliente"].ToString(),
                                   dr["desc_moneda"].ToString(),
                                   String.Format("{0:###,###,###,##0.00}", float.Parse(dr["monto_solicitado"].ToString())),
                                   String.Format("{0:####}", float.Parse(dr["meses_plazo"].ToString())),
                                   dr["estacion_actual"].ToString(),
                                   dr["no_movimiento"].ToString());
                }
                gvSolicitudes.DataSource = table;
                gvSolicitudes.Refresh();
                table.Dispose();
                dr.Close();

                

                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_llenar_grid_solicitudes " + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        private void s_solic_abiertas_Load(object sender, EventArgs e)
        {
            p_llenar_grid_solicitudes();
        }
        private void button_cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            p_ver_movimientos();
        }
        private void p_ver_movimientos()
        {
            int vl_no_solicitud = 0;
            int vl_no_movimiento = 0;

            if (gvSolicitudes.RowCount > 0)
            {
                DataGridViewRow row = gvSolicitudes.CurrentRow;
                vl_no_solicitud = int.Parse(row.Cells["no_solicitud"].Value.ToString());
                vl_no_movimiento = int.Parse(row.Cells["no_movimiento"].Value.ToString());

                s_solicitud_detalle forma = new s_solicitud_detalle(vl_no_solicitud, vl_no_movimiento);
                forma.da = da;
                forma.ShowDialog();
            }
        }
        private void gvSolicitudes_DoubleClick(object sender, EventArgs e)
        {
            p_ver_movimientos();
        }
        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }

        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
