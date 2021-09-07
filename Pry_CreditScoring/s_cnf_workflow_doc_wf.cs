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
    public partial class s_cnf_workflow_doc_wf : Form
    {
        public DataAccess da;
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

        int vl_workflow_id = 0;
        int vl_documento_id = 0;

        public string gfuente_financiamiento { get; set; }

        public s_cnf_workflow_doc_wf()
        {
            InitializeComponent();
        }

        private void s_cnf_workflow_doc_wf_Load(object sender, EventArgs e)
        {
            rbTodos.Checked = true;
            p_llenar_combo_workflows(); //Llena el workflow
            var dt = da.ObtenerFuentesFondos();//Carga los fondos
            cmbFondos.DataSource = dt;
            cmbFondos.DisplayMember = "descripcion_fuente";
            cmbFondos.ValueMember = "codigo_fuente";
            cmbFondos_SelectionChangeCommitted(null, null);

            rbTodos.Checked = true;
            p_llenar_list_docs_del_wf();
            rbTodos.Checked = true;
            p_llenar_grid_todos_los_documentos();
            rbDoc_titulos.Checked = true;
            rbAll.Checked = true;
        }

        private void p_llenar_list_docs_del_wf()
        {
            string persona = "ALL";
            if (rbAll.Checked)
            {
                persona = "ALL";
            }
            if (rbP1.Checked)
            {
                persona = "P1";
            }
            if (rbP2.Checked)
            {
                persona = "P2";
            }
            if (rbP3.Checked)
            {
                persona = "P3";
            }
            if (rbP4.Checked)
            {
                persona = "P4";
            }
            if (rbP5.Checked)
            {
                persona = "P5";
            }
            if (rbP6.Checked)
            {
                persona = "P6";
            }
            if (rbP7.Checked)
            {
                persona = "P7";
            }

            string seccion ="todos";
            if (rbTodos.Checked)
                seccion = "todos";
            if (rbLegal.Checked)
                seccion = "L";
            if (rbFinanciera.Checked)
                seccion = "F";
            if (rbOtros.Checked)
                seccion = "O";
            if (rbSeccionPrimera.Checked)
                seccion = "P";

            try
            {
                string vl_sql;
                if (rbTodos.Checked)
                {
                    vl_sql = @"SELECT dw.documento_id,descripcion,sigla_doc,persona_solicitud,tipo_exigencia,decode(tipo_exigencia,'R','Requeridos','O','Opcionales') desc_tipo_exigencia
                                FROM dcs_wf_documentos_workflow dw,
                                     dcs_wf_tipo_documentos td
                                WHERE dw.documento_id=td.documento_id      
                                    AND dw.workflow_id=:pa_workflow_id 
                                    AND dw.codigo_sub_aplicacion=:pa_codigo_sub_aplicacion
                                    AND persona_solicitud=decode(:pa_persona_solic,'ALL',persona_solicitud,:pa_persona_solic)
                                ORDER BY persona_solicitud,ORDEN_IN_GRUPO,documento_id";
                }
                else {
                    vl_sql = @"SELECT dw.documento_id,descripcion,sigla_doc,persona_solicitud,tipo_exigencia,decode(tipo_exigencia,'R','Requeridos','O','Opcionales') desc_tipo_exigencia
                                FROM dcs_wf_documentos_workflow dw,
                                     dcs_wf_tipo_documentos td
                                WHERE dw.documento_id=td.documento_id      
                                    AND dw.workflow_id=:pa_workflow_id 
                                    AND dw.codigo_sub_aplicacion=:pa_codigo_sub_aplicacion
                                    AND persona_solicitud=decode(:pa_persona_solic,'ALL',persona_solicitud,:pa_persona_solic)
                                    AND DW.SECCION =:pa_seccion
                                ORDER BY persona_solicitud,ORDEN_IN_GRUPO,documento_id";
                }
               
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);                
                cmd2.Parameters.Add("pa_workflow_id", OracleType.Int32).Value = vl_workflow_id;
                cmd2.Parameters.Add("pa_codigo_sub_aplicacion", OracleType.Int32).Value = string.IsNullOrEmpty(txtCodigo_sub_aplicacion.Text) ? 0 : Int32.Parse(txtCodigo_sub_aplicacion.Text);
                cmd2.Parameters.Add("pa_persona_solic", OracleType.VarChar, 5).Value = persona;
                if (!rbTodos.Checked)
                {
                    cmd2.Parameters.Add("pa_seccion", OracleType.VarChar).Value = seccion;
                }

                OracleDataReader dr = cmd2.ExecuteReader();

                DataTable tabla = new DataTable();
                tabla.Columns.Add("documento_id");
                tabla.Columns.Add("descripcion");
                tabla.Columns.Add("sigla_doc");
                tabla.Columns.Add("tipo_exigencia");
                tabla.Columns.Add("desc_tipo_exigencia");

                while (dr.Read())
                {
                    tabla.Rows.Add(dr["documento_id"].ToString(),
                                   dr["descripcion"].ToString(),
                                   dr["sigla_doc"].ToString(),
                                   dr["tipo_exigencia"].ToString(),
                                   dr["desc_tipo_exigencia"].ToString());
                }
                list_documentos.BeginUpdate();
                list_documentos.SmallImageList = imagesSmall;
                list_documentos.LargeImageList = imagesLarge;
                list_documentos.Clear();
                list_documentos.Groups.Clear();

                string vl_agencia = "";
                bool inicio = true;
                int indgrupo = 0;
                foreach (DataRow fila in tabla.Rows)
                {
                    ListViewItem listItem = new ListViewItem(fila["sigla_doc"].ToString());
                    if (fila["tipo_exigencia"].ToString() == "R")
                        listItem.ImageIndex = 0;

                    if (fila["tipo_exigencia"].ToString() == "O")
                    {
                        listItem.ImageIndex = 0;
                    }

                    if (fila["tipo_exigencia"].ToString() == "R")
                    {
                        listItem.Font = new Font(Font.FontFamily, Font.Size, FontStyle.Bold);
                    }
                    listItem.ToolTipText = "Desc:" + fila["Descripcion"].ToString() + "\r\n" + "Exigencia: " + fila["tipo_exigencia"].ToString() + "\r\n" + "Sigla  " + fila["sigla_doc"].ToString();

                    if (inicio)
                    {
                        indgrupo = 0;
                        list_documentos.Groups.Add(new ListViewGroup(fila["desc_tipo_exigencia"].ToString(), HorizontalAlignment.Left));
                        vl_agencia = fila["desc_tipo_exigencia"].ToString();
                        inicio = false;
                    }
                    if (fila["desc_tipo_exigencia"].ToString() != vl_agencia)
                    {
                        list_documentos.Groups.Add(new ListViewGroup(fila["desc_tipo_exigencia"].ToString(), HorizontalAlignment.Left));
                        vl_agencia = fila["desc_tipo_exigencia"].ToString();
                        indgrupo++;
                    }
                    listItem.Group = list_documentos.Groups[indgrupo];
                    listItem.SubItems.Add(fila["descripcion"].ToString());
                    listItem.SubItems.Add(fila["sigla_doc"].ToString());
                    listItem.SubItems.Add(fila["tipo_exigencia"].ToString());
                    list_documentos.Items.Add(listItem);
                }

                list_documentos.Columns.Add("documento_id", 70, HorizontalAlignment.Left);
                list_documentos.Columns.Add("descripcion", 180, HorizontalAlignment.Left);
                list_documentos.Columns.Add("sigla_dco", 80, HorizontalAlignment.Left);
                list_documentos.Columns.Add("tipo_exigencia", 40, HorizontalAlignment.Left);
                list_documentos.EndUpdate();
                list_documentos.Sort();
                if (rbDoc_iconos.Checked)
                    list_documentos.View = View.LargeIcon;
                if (rbDoc_titulos.Checked)
                    list_documentos.View = View.Tile;
                if (rbDoc_detalle.Checked)
                    list_documentos.View = View.Details;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_llenar_list_miembros : " + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void p_llenar_combo_workflows()
        {
            try
            {
                DataSet dsCombo = new DataSet();
                dsCombo = DocSys.p_Obtener_un_dataset("select workflow_id,nombre_workflow from dcs_workflows where activo='S'", "dcs_workflows");

                comboBox_workflows.DataSource = dsCombo;
                comboBox_workflows.DisplayMember = "dcs_workflows.nombre_workflow";
                comboBox_workflows.ValueMember = "dcs_workflows.workflow_id";
                comboBox_workflows_SelectionChangeCommitted(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en llenar combox Workflows : " + ex.Message);
            }
        }

        private void p_llenar_grid_todos_los_documentos()
        {
            string vl_condicion_busqueda = "";
            this.Cursor = Cursors.WaitCursor;

            if (txtTexto_busqueda.Text != string.Empty)
            {
                vl_condicion_busqueda = " and UPPER(documento_id||trim(descripcion)||sigla_doc) like " + "'%" + txtTexto_busqueda.Text.ToUpper() + "%'";
            }

            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string sql = @"Select * 
                                 from dcs_wf_tipo_documentos td 
                                Where not exists (Select 'x' 
                                 From dcs_wf_documentos_workflow dw 
                                Where td.documento_id=dw.documento_id
                                  and workflow_id=:pa_workflow_id
                                  and codigo_sub_aplicacion=:pa_codigo_sub_aplicacion)
                                  and nvl(activo,'S')='S'" +
                                  vl_condicion_busqueda + @"                                 
                                Order by documento_id";
                OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
                cmd.CommandType = CommandType.Text;
                //────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────                
                OracleParameter pa_workflow_id = new OracleParameter("pa_workflow_id", OracleType.Int32);
                cmd.Parameters.Add(pa_workflow_id);
                pa_workflow_id.Direction = ParameterDirection.Input;
                pa_workflow_id.Value = vl_workflow_id;
                //────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
                OracleParameter pa_codigo_sub_aplicacion = new OracleParameter("pa_codigo_sub_aplicacion", OracleType.Number);
                cmd.Parameters.Add(pa_codigo_sub_aplicacion);
                pa_codigo_sub_aplicacion.Direction = ParameterDirection.Input;
                pa_codigo_sub_aplicacion.Value = Int32.Parse(txtCodigo_sub_aplicacion.Text);
                //────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
                OracleDataReader dr = cmd.ExecuteReader();
                
                DataTable mitable = new DataTable();
                mitable.Columns.Add("img_estado_registro", System.Type.GetType("System.Byte[]"));
                mitable.Columns.Add("documento_id");
                mitable.Columns.Add("descripcion");
                mitable.Columns.Add("sigla_doc");
                mitable.Columns.Add("activo");

                while (dr.Read())
                {
                    mitable.Rows.Add(null,
                                     dr["documento_id"].ToString(),
                                     dr["descripcion"].ToString(),
                                     dr["sigla_doc"].ToString(),
                                     dr["activo"].ToString());
                }
                gv_documentos.DataSource = mitable;
                gv_documentos.Refresh();
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;
        }

        private void p_relacionar_documento_to_wf(string p_tipo_exigencia)
        {
            bool vl_return = false;
            string vl_seccion = "L";
            if (rbLegal.Checked)
                vl_seccion = "L";
            if (rbFinanciera.Checked)
                vl_seccion = "F";
            if (rbOtros.Checked)
                vl_seccion = "O";
            if (rbSeccionPrimera.Checked)
                vl_seccion = "P";

            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string vl_sql = "DCS_P_DOC_DAR_WF";
                OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd.CommandType = CommandType.StoredProcedure;
                //────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
                OracleParameter pa_workflow_id = new OracleParameter("pa_workflow_id", OracleType.Number);
                cmd.Parameters.Add(pa_workflow_id);
                pa_workflow_id.Direction = ParameterDirection.Input;
                pa_workflow_id.Value = vl_workflow_id;
                //────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
                OracleParameter pa_codigo_sub_aplicacion = new OracleParameter("pa_codigo_sub_aplicacion", OracleType.Number);
                cmd.Parameters.Add(pa_codigo_sub_aplicacion);
                pa_codigo_sub_aplicacion.Direction = ParameterDirection.Input;
                pa_codigo_sub_aplicacion.Value = Int32.Parse(txtCodigo_sub_aplicacion.Text);
                //────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
                OracleParameter pa_documento_id = new OracleParameter("pa_documento_id", OracleType.Number);
                cmd.Parameters.Add(pa_documento_id);
                pa_documento_id.Direction = ParameterDirection.Input;
                pa_documento_id.Value = vl_documento_id;
                //────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
                OracleParameter pa_tipo_exigencia = new OracleParameter("pa_tipo_exigencia", OracleType.VarChar, 1);
                cmd.Parameters.Add(pa_tipo_exigencia);
                pa_tipo_exigencia.Direction = ParameterDirection.Input;
                pa_tipo_exigencia.Value = p_tipo_exigencia;
                //────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
                OracleParameter pa_seccion = new OracleParameter("pa_seccion", OracleType.VarChar, 20);
                cmd.Parameters.Add(pa_seccion);
                pa_seccion.Direction = ParameterDirection.Input;
                pa_seccion.Value = vl_seccion;

                cmd.ExecuteReader();
                vl_return = true;
            }
            catch (Exception ex)
            {
                vl_return = false;
                MessageBox.Show("Error en p_relacionar_documento_to_wf :" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void p_documento_quitar_wf(Int16 p_documento_id)
        {
            bool vl_return = false;
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                string vl_sql = "DCS_P_DOC_QUITAR_WF";
                OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd.CommandType = CommandType.StoredProcedure;
                //────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
                OracleParameter pa_workflow_id = new OracleParameter("pa_workflow_id", OracleType.Number);
                cmd.Parameters.Add(pa_workflow_id);
                pa_workflow_id.Direction = ParameterDirection.Input;
                pa_workflow_id.Value = vl_workflow_id;
                //────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
                OracleParameter pa_documento_id = new OracleParameter("pa_documento_id", OracleType.Number);
                cmd.Parameters.Add(pa_documento_id);
                pa_documento_id.Direction = ParameterDirection.Input;
                pa_documento_id.Value = p_documento_id;
                //────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────          
                cmd.ExecuteReader();
                vl_return = true;
            }
            catch (Exception ex)
            {
                vl_return = false;
                MessageBox.Show("Error en p_documento_quitar_wf :" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void p_llenar_combo_sub_aplicaciones()
        {
            string cod_fondo_mg = da.ObtenerConversionFondoMG(txtCodigo_fuente_fondos.Text);
            comboBox_sub_aplicacion.DataSource = null;
            try
            {
                var dt = da.ObtenerSubAplicaciones("MCR", cod_fondo_mg);
                if (dt.Rows.Count > 0)
                {
                    comboBox_sub_aplicacion.DataSource = dt;
                    comboBox_sub_aplicacion.DisplayMember = "desc_sub_aplicacion";
                    comboBox_sub_aplicacion.ValueMember = "codigo_sub_aplicacion";
                    comboBox_sub_aplicacion_SelectionChangeCommitted(null, null);
                }
                else
                {
                    txtCodigo_sub_aplicacion.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en llenar combo_decisiones : " + ex.Message);
            }
        }

        private void rbDoc_iconos_CheckedChanged(object sender, EventArgs e)
        {
            list_documentos.View = View.LargeIcon;
        }

        private void rbDoc_titulos_CheckedChanged(object sender, EventArgs e)
        {
            list_documentos.View = View.Tile;
        }

        private void rbDoc_detalle_CheckedChanged(object sender, EventArgs e)
        {
            list_documentos.View = View.Details;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            p_llenar_grid_todos_los_documentos();
        }

        private void txtTexto_busqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                pictureBox2_Click(null, null);
            }
        }

        private void comboBox_workflows_SelectionChangeCommitted(object sender, EventArgs e)
        {
            vl_workflow_id = int.Parse(comboBox_workflows.SelectedValue.ToString());
            label_workflow.Text = "(" + vl_workflow_id.ToString().Trim() + ")";
            p_llenar_list_docs_del_wf();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCodigo_sub_aplicacion.Text))
            {
                MessageBox.Show("Debe indicar el codigo del producto al que esta relacionado el documento..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataGridViewRow row = gv_documentos.CurrentRow;
            if (row != null)
            {
                int.TryParse(row.Cells["documento_id"].Value.ToString(), out vl_documento_id);
                if (vl_documento_id > 0)
                {
                    contextMenuStrip1.Show(button1, new System.Drawing.Point(0, 25));
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un documento de la lista todos los documentos ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void requeridoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p_relacionar_documento_to_wf("R");
            p_llenar_list_docs_del_wf();
            p_llenar_grid_todos_los_documentos();
        }

        private void opcionalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p_relacionar_documento_to_wf("O");
            p_llenar_list_docs_del_wf();
            p_llenar_grid_todos_los_documentos();
        }

        private void button_quitar_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection usuarios_selected = list_documentos.SelectedItems;
            if (usuarios_selected.Count == 0)
            {
                MessageBox.Show("Debe seleccionar un documento de la lista de documentos asociados al workflow para revocar ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                foreach (ListViewItem item in usuarios_selected)
                {
                    string vl_documento_id = item.SubItems[0].Text;

                    if (DialogResult.No == MessageBox.Show("Desea quitar el documento  " + vl_documento_id + " del workflow " + comboBox_workflows.Text, DocSys.vl_mensaje_avisos, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        return;
                    }
                    p_documento_quitar_wf(Int16.Parse(vl_documento_id));
                    list_documentos.Items[item.Index].Remove();
                    p_llenar_list_docs_del_wf();
                    p_llenar_grid_todos_los_documentos();
                }
            }
        }

        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void comboBox_sub_aplicacion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int vl_codigo_sub_aplicacion = 0;
            txtCodigo_sub_aplicacion.Text = comboBox_sub_aplicacion.SelectedValue.ToString();
            int.TryParse(txtCodigo_sub_aplicacion.Text, out vl_codigo_sub_aplicacion);
            p_llenar_list_docs_del_wf();
            p_llenar_grid_todos_los_documentos();
        }

        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }
        private void list_documentos_DragEnter(object sender, DragEventArgs e){}

        private void label1_Click(object sender, EventArgs e)
        {
            p_llenar_list_docs_del_wf();
        }

        private void list_documentos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Int16 vl_documento_id = 0;
            string vl_nombre_documento = "";
            Int16 vl_sub_app = Int16.Parse(string.IsNullOrEmpty(txtCodigo_sub_aplicacion.Text) ? "0" : txtCodigo_sub_aplicacion.Text);
            ListView.SelectedListViewItemCollection usuarios_selected = list_documentos.SelectedItems;
            if (usuarios_selected.Count == 0)
            {
                MessageBox.Show("Debe seleccionar un documento de la lista de documentos asociados al workflow para revocar ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                foreach (ListViewItem item in usuarios_selected)
                {
                    vl_documento_id = Int16.Parse(item.SubItems[0].Text);
                    vl_nombre_documento = item.SubItems[1].Text;
                }
            }
            s_cnf_workflow_doc_wf02 forma = new s_cnf_workflow_doc_wf02(vl_workflow_id, vl_sub_app, vl_documento_id);
            forma.da = this.da;
            forma.lblDocumento.Text = vl_nombre_documento;
            forma.lblDocID.Text = vl_documento_id.ToString();
            DialogResult result = forma.ShowDialog();
            if (result == DialogResult.OK)
            {
                p_llenar_list_docs_del_wf();
            }
        }

        private void rbAll_CheckedChanged(object sender, EventArgs e)
        {
            p_llenar_list_docs_del_wf();
        }

        private void cmbFondos_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int vl_codigo_ff = 0;
            txtCodigo_fuente_fondos.Text = cmbFondos.SelectedValue.ToString();
            gfuente_financiamiento = cmbFondos.SelectedValue.ToString();
            int.TryParse(txtCodigo_fuente_fondos.Text, out vl_codigo_ff);
            p_llenar_combo_sub_aplicaciones();
        }

        private void rbLegal_CheckedChanged(object sender, EventArgs e)
        {
            p_llenar_list_docs_del_wf();
        }

        private void rbFinanciera_CheckedChanged(object sender, EventArgs e)
        {
            p_llenar_list_docs_del_wf();
        }

        private void rbOtros_CheckedChanged(object sender, EventArgs e)
        {
            p_llenar_list_docs_del_wf();
        }

        private void rbSeccionPrimera_CheckedChanged(object sender, EventArgs e)
        {
            p_llenar_list_docs_del_wf();
        }
    }
}
