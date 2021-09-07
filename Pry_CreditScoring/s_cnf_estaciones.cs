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
    public partial class s_cnf_estaciones : Form
    {
        DataAccess da;
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

        int vl_record_grid = 0;

        public s_cnf_estaciones(DataAccess da)
        {
            InitializeComponent();
            this.da = da;
        }

        private void s_estaciones_doc_Load(object sender, EventArgs e)
        {
            p_llenar_grid_estaciones();
        }
        private void p_llenar_grid_estaciones()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {

                var dt = da.ObtenerListadeEstaciones();
                gvEstaciones.AutoGenerateColumns = false;
                gvEstaciones.DataSource = dt;
                gvEstaciones.Refresh();                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;
        }       
        private void button_cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button_adicionar_Click(object sender, EventArgs e)
        {
            p_get_record();
            s_cnf_estaciones_doc forma = new s_cnf_estaciones_doc(da,"INS",0);
            DialogResult resul = forma.ShowDialog();
            if (resul == DialogResult.OK)
            {
                p_llenar_grid_estaciones();
                p_go_record();
            } 
        }
        private void button_modificar_Click(object sender, EventArgs e)
        {
            p_get_record();
            DataGridViewRow row =gvEstaciones.CurrentRow;
            if (row != null)
            {
                int vl_estacion_id = int.Parse(row.Cells["estacion_id"].Value.ToString());
                s_cnf_estaciones_doc forma = new s_cnf_estaciones_doc(da,"MODIF", vl_estacion_id);                
                DialogResult resul = forma.ShowDialog();
                if (resul == DialogResult.OK)
                {
                    p_llenar_grid_estaciones();
                    p_go_record();
                }
            }                        
        }
        private void button_eliminar_Click(object sender, EventArgs e)
        {
            p_get_record();
            DataGridViewRow row =gvEstaciones.CurrentRow;
            if (row != null)
            {
                int vl_estacion_id = int.Parse(row.Cells["estacion_id"].Value.ToString());
                s_cnf_estaciones_doc forma = new s_cnf_estaciones_doc(da,"ELIM", vl_estacion_id);
                DialogResult resul=forma.ShowDialog();
                if (resul == DialogResult.OK)
                {
                    p_llenar_grid_estaciones();
                    p_go_record();
                }
            }
        }


        private void p_go_record()
        {
            if (gvEstaciones.RowCount > 0) //Tiene que tener registros
            {
                if (gvEstaciones.RowCount > vl_record_grid)
                {
                    gvEstaciones.CurrentCell = this.gvEstaciones[1, vl_record_grid];
                }
                else
                {
                    if (gvEstaciones.RowCount == vl_record_grid)
                    {
                        gvEstaciones.CurrentCell = this.gvEstaciones[2, gvEstaciones.RowCount - 1];
                    }
                    if (gvEstaciones.RowCount == 0)
                    {

                    }
                }
            }
        }
        private void p_get_record()
        {
            DataGridViewRow row = gvEstaciones.CurrentRow;
            if (gvEstaciones.Rows.IndexOf(row) >= 0)
                vl_record_grid = gvEstaciones.Rows.IndexOf(row);
            else
                vl_record_grid = 0;
            Label_currentrow.Text = vl_record_grid.ToString() + "/" + gvEstaciones.RowCount.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            p_get_record();
            DataGridViewRow row =gvEstaciones.CurrentRow;
            if (row != null)
            {
                int vl_estacion_id = int.Parse(row.Cells["estacion_id"].Value.ToString());                
                s_cnf_workflow_user_estacion forma = new s_cnf_workflow_user_estacion(vl_estacion_id);
                DialogResult result = forma.ShowDialog();
                p_go_record();
            }
        }        
        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }
        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            p_get_record();
            p_llenar_grid_estaciones();
            p_go_record();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }
    }
}
