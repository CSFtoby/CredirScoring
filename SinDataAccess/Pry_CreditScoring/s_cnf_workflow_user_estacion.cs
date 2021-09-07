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
    public partial class s_cnf_workflow_user_estacion : Form
    {

        bool con_borde = MDI_Menu.con_borde;

        #region Mover
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
        #region Sombra
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

        bool vl_primer_vez=true;
        int vl_estacion_id=0;


        public s_cnf_workflow_user_estacion(int pa_estacion_id)
        {
            InitializeComponent();
            vl_estacion_id = pa_estacion_id;
        }

        private void s_cnf_workflow_user_estacion_Load(object sender, EventArgs e)
        {
            
            p_llenar_combo_estaciones_de(vl_estacion_id);            
            p_llenar_list_miembros();            
            p_llenar_grid_todos_los_usuarios();
            
            if (vl_estacion_id > 0)
            {                
               comboBoxEstaciones.SelectedValue = vl_estacion_id;
               ComboBox_estaciones_de_SelectionChangeCommitted(null, null);
               vl_primer_vez = false;
            }
            else
                ComboBox_estaciones_de_SelectionChangeCommitted(null, null);





        }


        private void p_llenar_list_miembros()
        {
            string vl_condicion_busqueda = "";
            if (txtTexto_busqueda_miembros.Text != string.Empty)                
            {
                vl_condicion_busqueda = " and UPPER(u.codigo_cliente||u.codigo_usuario||u.nombres||' '||u.primer_apellido||' '||u.segundo_apellido||nombre_agencia) like " + "'%" + txtTexto_busqueda_miembros.Text.ToUpper() + "%'";
            }

            try
            {
                string vl_sql = @"Select ue.usuario,u.nombres||' '||u.primer_apellido nombre,
                                         c.sexo,e.nombre estacion,nombre_agencia,
                                         correo_electronico,
                                         numero_extension,
                                         titular_id,
                                         u.codigo_cliente
                                    From dcs_wf_usuarios_estaciones ue,
                                         mgi_usuarios u,
                                         dcs_wf_estaciones e,
                                         mgi_agencias a,
                                         mgi_clientes c        
                                   Where a.codigo_empresa=1                                     
                                     and u.codigo_empresa=1                                     
                                     and u.codigo_agencia=a.codigo_agencia
                                     and u.codigo_cliente=c.codigo_cliente(+)
                                     and u.codigo_empresa=c.codigo_empresa(+)
                                     and ue.usuario=u.codigo_usuario
                                     and ue.estacion_id=e.estacion_id
                                     and ue.estacion_id=:pa_estacion_id"+
                                    vl_condicion_busqueda + @"
                                   order by nombre_agencia,ue.usuario";
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd2.CommandType = CommandType.Text;

                OracleParameter pa_estacion_id = new OracleParameter("pa_estacion_id", OracleType.Int32);
                cmd2.Parameters.Add(pa_estacion_id);
                pa_estacion_id.Direction = ParameterDirection.Input;
                pa_estacion_id.Value =vl_estacion_id;
                OracleDataReader dr = cmd2.ExecuteReader();

                DataTable tabla = new DataTable();
                tabla.Columns.Add("usuario");
                tabla.Columns.Add("nombre");
                tabla.Columns.Add("sexo");
                tabla.Columns.Add("estacion");
                tabla.Columns.Add("nombre_agencia");
                tabla.Columns.Add("correo_electronico");
                tabla.Columns.Add("numero_extension");
                tabla.Columns.Add("titular_id");
                tabla.Columns.Add("codigo_cliente");
                
                while (dr.Read())
                {
                    tabla.Rows.Add(dr["usuario"].ToString(),
                                   dr["nombre"].ToString(),
                                   dr["sexo"].ToString(),
                                   dr["estacion"].ToString(),
                                   dr["nombre_agencia"].ToString(),
                                   dr["correo_electronico"].ToString(),
                                   dr["numero_extension"].ToString(),
                                   dr["titular_id"].ToString(),
                                   dr["codigo_cliente"].ToString());
                }
                list_usuarios.BeginUpdate();
                list_usuarios.SmallImageList = imagesSmall;
                list_usuarios.LargeImageList = imagesLarge;
                list_usuarios.Clear();
                list_usuarios.Groups.Clear();

                string vl_agencia = "";
                bool inicio = true;
                int indgrupo = 0;
                foreach (DataRow fila in tabla.Rows)
                {
                    ListViewItem listItem = new ListViewItem(fila["usuario"].ToString());
                    if (fila["sexo"].ToString() == "F")
                        listItem.ImageIndex = 1;

                    if (fila["sexo"].ToString() == "M")
                    {
                        listItem.ImageIndex = 0;                        
                    }
                    if (fila["sexo"].ToString() == "")
                    {
                        listItem.ImageIndex = 2;
                    }
                    if (fila["titular_id"].ToString() == fila["codigo_cliente"].ToString())
                    {
                        listItem.Font = new Font(Font.FontFamily, Font.Size, FontStyle.Bold);
                    }
                    listItem.ToolTipText = "Nombre:" + fila["nombre"].ToString() + "\r\n" + "eMail: " + fila["correo_electronico"].ToString() + "\r\n" + "Ext:  " + fila["numero_extension"].ToString();
                    
                    if (inicio)
                    {
                        indgrupo = 0;
                        list_usuarios.Groups.Add(new ListViewGroup(fila["nombre_agencia"].ToString(), HorizontalAlignment.Left));
                        vl_agencia = fila["nombre_agencia"].ToString();
                        inicio = false;
                    }
                    if (fila["nombre_agencia"].ToString() != vl_agencia)
                    {
                        list_usuarios.Groups.Add(new ListViewGroup(fila["nombre_agencia"].ToString(), HorizontalAlignment.Left));
                        vl_agencia = fila["nombre_agencia"].ToString();
                        indgrupo++;
                    }
                    listItem.Group = list_usuarios.Groups[indgrupo];
                    listItem.SubItems.Add(fila["nombre"].ToString());
                    listItem.SubItems.Add(fila["correo_electronico"].ToString());
                    listItem.SubItems.Add(fila["numero_extension"].ToString());
                    list_usuarios.Items.Add(listItem);
                }

                list_usuarios.Columns.Add("usuario", 80, HorizontalAlignment.Left);
                list_usuarios.Columns.Add("nombre", 200, HorizontalAlignment.Left);
                list_usuarios.Columns.Add("correo_electronico", 150, HorizontalAlignment.Left);
                list_usuarios.Columns.Add("numero_extension", 80, HorizontalAlignment.Left);
                list_usuarios.EndUpdate();
                list_usuarios.Sort();
                if (rbMiembros_iconos.Checked)
                    list_usuarios.View = View.LargeIcon;
                if (rbMiembros_titulos.Checked)
                    list_usuarios.View = View.Tile;
                if (rbMiembros_detalle.Checked)
                    list_usuarios.View = View.Details;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_llenar_list_miembros : " + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void p_llenar_grid_todos_los_usuarios()
        {
            string vl_condicion_busqueda = "";
            this.Cursor = Cursors.WaitCursor;

            if (txtTexto_busqueda_todos.Text != string.Empty)
            {
                vl_condicion_busqueda = " and UPPER(codigo_cliente||codigo_usuario||nombres||' '||primer_apellido||' '||segundo_apellido||nombre_agencia) like " + "'%" + txtTexto_busqueda_todos.Text.ToUpper() + "%'";
            }

            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string sql = @"Select codigo_cliente,u.codigo_usuario,InitCap(nombres||' '||primer_apellido||' '||segundo_apellido) nombres,Initcap(e.nombre) estacion_actual,Initcap(nombre_agencia) nombre_agencia
                                 From mgi_usuarios u,
                                      mgi_agencias a,
                                      dcs_wf_usuarios_estaciones ue,
                                      dcs_wf_estaciones e        
                                Where codigo_cliente is not null
                                  and u.codigo_empresa=a.codigo_empresa
                                  and u.codigo_agencia=a.codigo_agencia
                                  and u.codigo_usuario=ue.USUARIO(+)
                                  and ue.estacion_id=e.estacion_id(+)                                     
                                  and not exists (Select codigo_usuario 
                                                    From dcs_wf_usuarios_estaciones ue 
                                                   Where u.codigo_usuario=ue.usuario
                                                     and estacion_id=:pa_estacion_id) " +
                                  vl_condicion_busqueda + @"
                                 and nvl(u.activo_b,'S')='S'
                                Order by 2";
                OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
                cmd.CommandType = CommandType.Text;
                //────────────────────────                
                OracleParameter pa_estacion_id = new OracleParameter("pa_estacion_id", OracleType.Int32);
                cmd.Parameters.Add(pa_estacion_id);
                pa_estacion_id.Direction = ParameterDirection.Input;
                pa_estacion_id.Value = vl_estacion_id;

                OracleDataReader dr = cmd.ExecuteReader();



                DataTable mitable = new DataTable();
                mitable.Columns.Add("img_estado_registro", System.Type.GetType("System.Byte[]"));
                mitable.Columns.Add("codigo_usuario");
                mitable.Columns.Add("codigo_cliente");
                mitable.Columns.Add("nombres");
                mitable.Columns.Add("estacion_actual");
                mitable.Columns.Add("nombre_agencia");

                while (dr.Read())
                {
                    mitable.Rows.Add(null,
                                     dr["codigo_usuario"].ToString(),
                                     dr["codigo_cliente"].ToString(),
                                     dr["nombres"].ToString(),
                                     dr["estacion_actual"].ToString(),
                                     dr["nombre_agencia"].ToString());
                }
                gv_usuarios.DataSource = mitable;
                gv_usuarios.Refresh();
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;
        }
        private void p_llenar_combo_estaciones_de(int p_estacion_id)
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
                if (p_estacion_id == 0)
                    ComboBox_estaciones_de_SelectionChangeCommitted(null, null);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en llenar combo_estaciones : " + ex.Message);
            }
        }        
        private bool p_revocar_pertenencia(string p_usuario,int p_estacion_id)
        {
            bool vl_return = false;
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string vl_sql = "DCS_P_REVOCAR_ESTACION";
                OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd.CommandType = CommandType.StoredProcedure;
                //───────────────────
                OracleParameter pa_usuario = new OracleParameter("pa_usuario", OracleType.VarChar, 50);
                cmd.Parameters.Add(pa_usuario);
                pa_usuario.Direction = ParameterDirection.Input;
                pa_usuario.Value = p_usuario;
                //───────────────────
                OracleParameter pa_estacion_id = new OracleParameter("pa_estacion_id", OracleType.Int32);
                cmd.Parameters.Add(pa_estacion_id);
                pa_estacion_id.Direction = ParameterDirection.Input;
                pa_estacion_id.Value = p_estacion_id;
                
                cmd.ExecuteReader();
                vl_return = true;
                return vl_return;
            }
            catch (Exception ex)
            {
                vl_return = false;
                MessageBox.Show("Error en p_revocar_pertenencia :" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return vl_return;
            }  
        }
        private bool p_otorgar_pertenencia(string p_usuario, int p_estacion_id)
        {
            bool vl_return = false;
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string vl_sql = "DCS_P_OTORGAR_ESTACION";
                OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd.CommandType = CommandType.StoredProcedure;
                //───────────────────
                OracleParameter pa_usuario = new OracleParameter("pa_usuario", OracleType.VarChar, 50);
                cmd.Parameters.Add(pa_usuario);
                pa_usuario.Direction = ParameterDirection.Input;
                pa_usuario.Value = p_usuario;
                //───────────────────
                OracleParameter pa_estacion_id = new OracleParameter("pa_estacion_id", OracleType.Int32);
                cmd.Parameters.Add(pa_estacion_id);
                pa_estacion_id.Direction = ParameterDirection.Input;
                pa_estacion_id.Value = p_estacion_id;

                cmd.ExecuteReader();
                vl_return = true;
                return vl_return;
            }
            catch (Exception ex)
            {
                vl_return = false;
                MessageBox.Show("Error en p_otorgar_pertenencia :" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return vl_return;
            }
        }
        private void p_obtener_estacion_actual_usuario(string p_usuario,out int p_estacion_id,out string p_nombre_estacion)
        {
            p_estacion_id = 0;
            p_nombre_estacion = "";
            
            this.Cursor = Cursors.WaitCursor;
            
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string sql = @"Select ue.estacion_id,e.nombre nombre_estacion
                                 From dcs_wf_usuarios_estaciones ue,
                                      dcs_wf_estaciones e
                                Where ue.ESTACION_ID=e.estacion_id
                                  and usuario=:pa_usuario";
                OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
                cmd.CommandType = CommandType.Text;
                //────────────────────────                
                OracleParameter pa_usuario = new OracleParameter("pa_usuario", OracleType.VarChar, 50);
                cmd.Parameters.Add(pa_usuario);
                pa_usuario.Direction = ParameterDirection.Input;
                pa_usuario.Value = p_usuario;

                OracleDataReader dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    p_estacion_id = int.Parse(dr["estacion_id"].ToString());
                    p_nombre_estacion = dr["nombre_estacion"].ToString();
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;
        }


        private void ComboBox_estaciones_de_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
            vl_estacion_id = int.Parse(comboBoxEstaciones.SelectedValue.ToString());            
            p_llenar_list_miembros();
        }        
        private void rbComentarios_iconos_CheckedChanged(object sender, EventArgs e)
        {
            list_usuarios.View = View.LargeIcon;
        }
        private void rbComentarios_titulos_CheckedChanged(object sender, EventArgs e)
        {
            list_usuarios.View = View.Tile;
        }
        private void rbComentarios_detalle_CheckedChanged(object sender, EventArgs e)
        {
            list_usuarios.View = View.Details;
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            p_llenar_grid_todos_los_usuarios();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            if (vl_estacion_id == 0)
            {
                MessageBox.Show("Debe seleccionar una estacion de la lista ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string vl_usuario = "";
            DataGridViewRow row = gv_usuarios.CurrentRow;
            if (row != null)
            {
                vl_usuario = row.Cells["codigo_usuario"].Value.ToString();
            }
            else
                return;

            int vl_estacion_actual_tmp = 0;
            string vl_nombre_estacion_actual_tmp = "";
            p_obtener_estacion_actual_usuario(vl_usuario,out vl_estacion_actual_tmp, out vl_nombre_estacion_actual_tmp);
            //if (vl_estacion_actual_tmp > 0)
            //{
            //    if (DialogResult.Yes == MessageBox.Show("El Usuario ya pertenece a la estacion " + vl_nombre_estacion_actual_tmp + ", debe revocar su pertenencia primero para poder asignarlo a otra estacion, desea ir a la estación del usuario ? ", DocSys.vl_mensaje_avisos, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            //    {
            //        vl_estacion_id = vl_estacion_actual_tmp;
            //        comboBoxEstaciones.SelectedValue = vl_estacion_id;
            //        p_llenar_list_miembros();
            //        return;
            //    }
            //    else
            //        return;
            //}
            
            if (row != null)
            {
                vl_usuario = row.Cells["codigo_usuario"].Value.ToString();
                if (p_otorgar_pertenencia(vl_usuario, vl_estacion_id))
                {                    
                    p_llenar_list_miembros();
                    p_llenar_grid_todos_los_usuarios();
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un usuario de la lista todos los empleados ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }
        private void button_quitar_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection usuarios_selected =  list_usuarios.SelectedItems;
            if (usuarios_selected.Count==0)
            {
                MessageBox.Show("Debe seleccionar un usuario de la lista de miembros actuales para revocar su pertenencia a la estacion ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                foreach (ListViewItem item in usuarios_selected)
                {
                    string vl_usuario=item.SubItems[0].Text;
                    if (DialogResult.No == MessageBox.Show("Desea revocar la pertenecia del usuario "+vl_usuario+" de la estacion "+comboBoxEstaciones.Text,DocSys.vl_mensaje_avisos,MessageBoxButtons.YesNo,MessageBoxIcon.Question))
                    {
                        return;
                    }
                    if (p_revocar_pertenencia(vl_usuario, vl_estacion_id))
                    {
                        list_usuarios.Items[item.Index].Remove();
                        p_llenar_list_miembros();
                        p_llenar_grid_todos_los_usuarios();
                    }
                }
            }            
        }

        private void txtTexto_busqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                pictureBox2_Click(null, null);
            }
        }
        

        private void pbBuscar_miembros_Click(object sender, EventArgs e)
        {
            p_llenar_list_miembros();
        }

        private void txtTexto_busqueda_miembros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                pbBuscar_miembros_Click(null, null);                
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtTexto_busqueda_todos_TextChanged(object sender, EventArgs e)
        {

        }

        private void gv_usuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
       

       
    }
}
