using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using wfcModel;

namespace Docsis_Application
{
    public partial class s_buscar_sol : Form
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
        Thread myThread;

        #region Para la barra de progreso
        //Delegados//
        delegate void SetValueCallback(int valor);
        private void SetValue(int valor)
        {
            if (this.InvokeRequired)
            {
                SetValueCallback d = new SetValueCallback(SetValue);
                this.Invoke(d, new object[] { valor });
            }
            else
            {

                this.pboxLoading.Visible = false;
                this.pboxLoading02.Visible = false;
                if (valor == 100)
                {
                    this.button_ejecutar.Enabled = true;
                }
                
                
            }
        }
        private void SetMax(int valor)
        {
            if (this.InvokeRequired)
            {
                SetValueCallback d = new SetValueCallback(SetMax);
                this.Invoke(d, new object[] { valor });
            }
            else
            {
                this.button_ejecutar.Enabled = false;                           
                this.pboxLoading.Visible = true;
                this.pboxLoading02.Visible = true;
                

            }
        }
        #endregion

        public s_buscar_sol()
        {                        
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void s_buscar_sol_Load(object sender, EventArgs e)
        {
            comboBoxCampos.Text = "No Solicitud";
        }  

        private void p_puente()
        {            
            p_procesar_busqueda();
        }        
        private void p_procesar_busqueda()
        {
            SetMax(101);
            Thread.Sleep(1000);
            try
            {
                string vl_condi_workflow="";
                string vl_condi_toda_filial = "";
                string vl_condi_gte_filial = "";
                string vl_condi_busqueda = "";
                string vl_condi_abierta = "";
                
                //if (global_ver_toda_filial)
                //{
                //    vl_condi_toda_filial = " and :pa_codigo_agencia_origen=:pa_codigo_agencia_origen";
                //}
                //else
                //{
                //    vl_condi_toda_filial = " and s.codigo_agencia_origen=:pa_codigo_agencia_origen ";

                //    if (global_gerente_filial)
                //    {
                //        vl_condi_gte_filial = "";
                //    }
                //    else
                //    {
                //        vl_condi_gte_filial = " and oficial_servicio='" + DocSys.vl_user.Trim() + "'";
                //    }
                //}

                

                
                switch (comboBoxCampos.Text)
                {
                    case "No Solicitud":
                        
                        try
                        {
                            int vl_tempo = 0;
                            vl_tempo = int.Parse(txtTexto_busqueda.Text.Trim());
                        }
                        catch
                        {
                            SetValue(100);                
                            MessageBox.Show("Debe indicar un valor numerico valido ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);                            
                            return;
                        }
                         vl_condi_busqueda = " and s.no_solicitud = " + txtTexto_busqueda.Text;
                         break;
                    case "No. Solicitud Formulario":

                         try
                         {
                             int vl_tempo = 0;
                             vl_tempo = int.Parse(txtTexto_busqueda.Text.Trim());
                         }
                         catch
                         {
                             SetValue(100);
                             MessageBox.Show("Debe indicar un valor numerico valido ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
                             return;
                         }
                         vl_condi_busqueda = " and s.no_solicitud_formulario = " + txtTexto_busqueda.Text;
                         break;

                    case "Codigo Cliente":
                         try
                         {
                             int vl_tempo = 0;
                             vl_tempo = int.Parse(txtTexto_busqueda.Text.Trim());
                         }
                         catch
                         {
                             SetValue(100);                
                             MessageBox.Show("Debe indicar un valor numerico valido ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
                             return;
                         }
                         vl_condi_busqueda = " and s.codigo_cliente like '%" + txtTexto_busqueda.Text+"%'";
                         break;
                    case "Nombre":
                         string vl_dummy1 = txtTexto_busqueda.Text.ToUpper();
                         vl_condi_busqueda = " and upper(trim(c.nombres) ||' '||trim(c.primer_apellido)||' '||trim(c.segundo_apellido)) like '%" + vl_dummy1 + "%'";                         
                         break; 
                    case "Filial":
                         string vl_dummy2= txtTexto_busqueda.Text.ToUpper();
                         vl_condi_busqueda = " and upper(nombre_agencia) like '%" + vl_dummy2 + "%'";
                         break;
                    case "Oficial de Servicio":
                         string vl_dummy3= txtTexto_busqueda.Text.ToUpper();
                         vl_condi_busqueda = " and upper(oficial_servicio) like '%" + vl_dummy3 + "%'";                         
                         break;

                    default:
                         vl_condi_busqueda = "";
                        break;
                }

                if (radioButton_abiertas.Checked)
                {
                    vl_condi_abierta = " and s.abierta = 'S' ";
                }
                if (radioButton_cerradas.Checked)
                {
                    vl_condi_abierta = " and s.abierta = 'N' ";
                }
                if (radioButton_todas.Checked)
                {
                    vl_condi_abierta = " ";
                }

               
                                
                string vl_sql = @"
                       Select distinct s.no_solicitud,
                              s.no_solicitud_formulario,
                              es.descripcion estado_solicitud,
                              initcap(desc_sub_aplicacion) desc_sub_aplicacion,
                              s.fecha_presentacion,
                              initcap(nombre_agencia) nombre_agencia,
                              s.oficial_servicio,
                              ms.analista,
                              s.codigo_cliente,
                              f.descripcion_fuente,
                              initcap(nombres)||' '||initcap(primer_apellido)||' '||initcap(segundo_apellido) nombre_cliente,
                              m.desc_moneda,
                              s.monto_solicitado,
                              s.meses_plazo,                              
                              no_movimiento,                              
                              e.nombre estacion_actual,
                              s2.resultado_buro
                         from dcs_solicitudes s,
                              dcs_solicitudes2 s2,
                              mgi_sub_aplicaciones sa,
                              mgi_monedas m,
                              mgi_agencias a,
                              mgi_clientes c,
                              dcs_movimientos_solicitudes ms,
                              dcs_wf_fuentes_financiamiento f,
                              dcs_wf_estaciones e,
                              dcs_wf_estado_solicitudes es
                         where ms.no_solicitud=s.no_solicitud 
                           and s.no_solicitud=s2.no_solicitud
                           and s.estado_solicitud_id=es.estado_id
                           and s.codigo_sub_aplicacion=sa.codigo_sub_aplicacion
                           and s.codigo_fuente=f.codigo_fuente
                           and s.codigo_agencia_origen=a.codigo_agencia
                           and s.codigo_moneda=m.codigo_moneda
                           and s.codigo_cliente=c.codigo_cliente
                           and s.estacion_id=e.estacion_id
                           and s.workflow_id=ms.workflow_id
                           and s.estacion_id=ms.estacion_id_to
                           and s.no_movimiento_actual=decode(s.no_movimiento_actual,0,0,ms.no_movimiento) " +                                                        
                           vl_condi_busqueda    +
                           vl_condi_abierta     +
                    @"  Order by s.no_solicitud desc ";
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd2.CommandType = CommandType.Text;
                //───────────────────
                

                //OracleParameter pa_codigo_agencia_origen = new OracleParameter("pa_codigo_agencia_origen", OracleType.Int32);
                //cmd2.Parameters.Add(pa_codigo_agencia_origen);
                //pa_codigo_agencia_origen.Direction = ParameterDirection.Input;
                //pa_codigo_agencia_origen.Value = DocSys.vl_agencia_usuario;

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
                table.Columns.Add("resultado_buro");
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
                                   dr["no_movimiento"].ToString(),
                                   dr["resultado_buro"].ToString());
                }
                gvSolicitudes.AutoGenerateColumns = false;
                gvSolicitudes.DataSource = table;
                gvSolicitudes.Refresh();
                table.Dispose();
                dr.Close();
                
                SetValue(100);                
               
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_procesar_envio " + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
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
        private void button_ejecutar_Click(object sender, EventArgs e)
        {
            if (txtTexto_busqueda.Text.Length <= 1)
            {
                MessageBox.Show("Debe ingresar el texto a buscar ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            p_procesar_busqueda();
            //myThread = new Thread(new ThreadStart(p_puente));
            //myThread.Start();
        }
        private void button_cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void gvSolicitudes_DoubleClick(object sender, EventArgs e)
        {
            p_ver_movimientos();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            p_ver_movimientos();
        }

        private void gvSolicitudes_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (checkBox_vistarapida.Checked)
            {
                DataGridViewRow row = gvSolicitudes.CurrentRow;
                if (row != null)
                {
                    if (e.RowIndex >= 0)
                    {
                        string vl_filial_origen = gvSolicitudes.Rows[e.RowIndex].Cells["nombre_agencia"].Value.ToString();
                        string vl_oficial_servicio = gvSolicitudes.Rows[e.RowIndex].Cells["oficial_servicio"].Value.ToString();
                        string vl_no_solicitud = gvSolicitudes.Rows[e.RowIndex].Cells["no_solicitud"].Value.ToString();
                        string vl_codigo_cliente = gvSolicitudes.Rows[e.RowIndex].Cells["codigo_cliente"].Value.ToString();
                        string vl_nombre_cliente = gvSolicitudes.Rows[e.RowIndex].Cells["nombre_cliente"].Value.ToString();
                        string vl_producto = gvSolicitudes.Rows[e.RowIndex].Cells["desc_sub_aplicacion"].Value.ToString();
                        string vl_monto = gvSolicitudes.Rows[e.RowIndex].Cells["monto_solicitado"].Value.ToString();
                        string vl_plazo = gvSolicitudes.Rows[e.RowIndex].Cells["meses_plazo"].Value.ToString();
                        string vl_estacion_actual = gvSolicitudes.Rows[e.RowIndex].Cells["estacion_actual"].Value.ToString();
                        string vl_no_movimiento = gvSolicitudes.Rows[e.RowIndex].Cells["no_movimiento"].Value.ToString();

                        miniinfo_sol.get_set_filial_origen = vl_filial_origen;
                        miniinfo_sol.get_set_oficial = vl_oficial_servicio;
                        miniinfo_sol.get_set_no_solicitud = vl_no_solicitud.ToString();
                        miniinfo_sol.get_set_codigo_cliente = vl_codigo_cliente;
                        miniinfo_sol.get_set_nombre_cliente = vl_nombre_cliente;
                        miniinfo_sol.get_set_producto = vl_producto;
                        miniinfo_sol.get_set_monto = vl_monto;
                        miniinfo_sol.get_set_plazo = vl_plazo;
                        miniinfo_sol.get_set_estacion_act = vl_estacion_actual;
                        miniinfo_sol.get_set_no_movimiento = vl_no_movimiento;

                        Point pos = this.PointToScreen(e.Location);
                        miniinfo_sol.Show();
                        miniinfo_sol.Location = new Point(Control.MousePosition.X - 105, Control.MousePosition.Y + 10);
                        miniinfo_sol.Refresh();
                    }
                }
            }
            
        }
        private void gvSolicitudes_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = gvSolicitudes.CurrentRow;
            if (row != null)
            {
                vl_mostrar_miniinfo = true;
                miniinfo_sol.Hide();
                this.Cursor = Cursors.Default;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            if (FormWindowState.Normal == WindowState)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }


             
    }
}
