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

namespace Docsis_Application
{
    public partial class s_consclientes : Form
    {
        delegate void SetEstadoCallback(bool pa_estado);
        string vl_parametro_poner_icono = "S";

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

        public s_consclientes()
        {
            InitializeComponent();            
        }
        private void s_consclientes_Load(object sender, EventArgs e)
        {
            cmbCampos.Text = "Nombres + Ambos Apellidos";
        }
        private void SetEstado_visible_Pb(bool pa_estado)
        {
            if (InvokeRequired)
            {
                SetEstadoCallback d = new SetEstadoCallback(SetEstado_visible_Pb);
                Invoke(d, new object[] { pa_estado });
            }
            else
            {
                progressBar1.Visible = pa_estado;
            }
        }
        private void p_llenar_griview()
        {
            this.Cursor = Cursors.WaitCursor;

            string vl_texto = "";
            string[] ArrayResponses;
            
            int x = 1;            
            ArrayResponses = new string[12];
            foreach (string component in txtTexto.Text.Split(" ".ToCharArray(), StringSplitOptions.None))
            {
                ArrayResponses[x] = component;                
                vl_texto = vl_texto + ArrayResponses[x].ToString()+'%';
            }

            

            SetEstado_visible_Pb(true);
            Thread.Sleep(500);           
            string vl_condicion1 = "";

            if (cmbCampos.Text == "Codigo Cliente")
            {
                vl_condicion1 = "and c.codigo_cliente like '%" + txtTexto.Text + "%'";
            }
            if (cmbCampos.Text == "Nombres")
            {
                string vl_dummy = vl_texto.ToUpper();
                vl_condicion1 = "and upper(nombres) like '%" + vl_dummy + "%'";
            }

            if (cmbCampos.Text == "Nombres + 1er Apellidos")
            {

                string vl_dummy = vl_texto.ToUpper();
                vl_condicion1 = "and upper(nombres||'%'||primer_apellido) like '%" + vl_dummy + "%'";
            }

            if (cmbCampos.Text == "Nombres + Ambos Apellidos")
            {
                string vl_dummy = vl_texto.ToUpper();
                vl_condicion1 = "and upper(trim(c.nombres) ||' '||trim(c.primer_apellido)||' '||trim(c.segundo_apellido)) like '%" + vl_dummy + "%'";
            }

            if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
            {
                DocSys.connOracle.Open();
            }
            string sql = @"Select c.codigo_cliente,
                                  c.nombres,
                                  c.primer_apellido,
                                  c.segundo_apellido,
                                  decode(c.sexo,'F','Femenino','M','Masculino','N/A') sexo,
                                  t.descripcion desc_tipo_cliente,
                                  to_char(c.fecha_de_nacimiento,'DD/MM/YYYY') fecha_de_nacimiento,
                                  to_char(sysdate,'YYYY') - to_char(c.fecha_de_nacimiento,'YYYY') edad_anos,
                                  decode(c.tipo_de_persona,'N','Natural','J','Juridica') tipo_persona, 
                                  c.activo_b,
                                  c.lugar_de_trabajo
                             from mgi_clientes c,
                                  mgi_tipos_de_cliente t
                            Where c.codigo_empresa=1
                              and c.codigo_tipo_cliente=t.codigo_tipo_cliente " + vl_condicion1;

            OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
            cmd.CommandType = CommandType.Text;
            //────────────────────────
            OracleDataReader dr = cmd.ExecuteReader();
            DataTable  mitable = new DataTable("clientes");
            //mitable.Columns.Add("foto", System.Type.GetType("System.Byte[]"));   

            mitable.Columns.Add("foto", System.Type.GetType("System.Byte[]"));   
            mitable.Columns.Add("codigo_cliente");
            mitable.Columns.Add("nombres");
            mitable.Columns.Add("primer_apellido");
            mitable.Columns.Add("segundo_apellido");
            mitable.Columns.Add("sexo");
            mitable.Columns.Add("desc_tipo_cliente");
            mitable.Columns.Add("fecha_nacimiento");
            mitable.Columns.Add("edad");
            mitable.Columns.Add("tipo_persona");
            mitable.Columns.Add("lugar_de_trabajo");
            mitable.Columns.Add("activo_b");
            
            
            string vl_estado="";
            Image vl_foto = null;    
            string sFile="";
            int vl_index_imagelist = 1;
            string vl_valor;

            

            while (dr.Read())
            {
                                
                mitable.Rows.Add(null,
                                 dr["codigo_cliente"].ToString(),
                                 dr["nombres"].ToString(),
                                 dr["primer_apellido"].ToString(),
                                 dr["segundo_apellido"].ToString(),
                                 dr["sexo"].ToString(),
                                 dr["desc_tipo_cliente"].ToString(),
                                 dr["fecha_de_nacimiento"].ToString(),
                                 dr["edad_anos"].ToString(),
                                 dr["tipo_persona"].ToString(),
                                 dr["lugar_de_trabajo"].ToString(),
                                 dr["activo_b"].ToString());
                                                                                                      
            }            
            gvClientes.DataSource = mitable;
            gvClientes.Refresh();            
            dr.Close();
            SetEstado_visible_Pb(false);

            
            //Recorre el grid para setear la foto
            if (vl_parametro_poner_icono == "S")
            {
                DataGridViewCell valueCell = null;
                DataGridViewImageCell displayCell = null;

                int vl_n = 0;
                for (int row = 0; row < gvClientes.Rows.Count; row++)
                {
                    if (gvClientes.Rows[row].Cells["sexo"].Value.ToString()== "Masculino")
                    {
                        vl_n = 0;
                    }
                    else
                    {
                        vl_n = 1;
                    }                    
                    valueCell = gvClientes[icono.Index, row];
                    displayCell = (DataGridViewImageCell)gvClientes[icono.Index, row];
                    displayCell.Value = imageList.Images[vl_n];
                }
            }
            //--------
            
            if (gvClientes.RowCount > 0)
            {
                DataGridViewRow primera_fila = gvClientes.Rows[0];
                if (gvClientes.SelectedRows.Contains(primera_fila))
                {
                    p_obtener_valor_seleccionado();
                    Button_ok.Enabled = true;
                }
            }
            else
            {
                Button_ok.Enabled = false;
            }
            this.Cursor = Cursors.Default;
        }
        private void button_ejecutar_consulta_Click(object sender, EventArgs e)
        {
            if (txtTexto.Text.Length <= 4)
            {
                MessageBox.Show("Debe indicar el texto de busqueda ", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            p_llenar_griview();
        }
        private void Button_ok_Click(object sender, EventArgs e)
        {
            p_obtener_valor_seleccionado();
        }
        private void p_obtener_valor_seleccionado()
        {
            DataGridViewRow row = gvClientes.CurrentRow;
            txtCodigo_cliente.Text = row.Cells["codigo_cliente"].Value.ToString();
        }
        private void gvClientes_DoubleClick(object sender, EventArgs e)
        {
            Button_ok_Click(null, null);
            this.DialogResult = DialogResult.OK;
        }
        private void button_ok_Click_1(object sender, EventArgs e)
        {
            p_obtener_valor_seleccionado();
        }
        private void Button_cerrar_Click(object sender, EventArgs e)
        {

        }
        private void txtTexto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button_ejecutar_consulta_Click(null, null);
                
            }
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
            this.DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        
    }
}
