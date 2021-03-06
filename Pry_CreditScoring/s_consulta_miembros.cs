using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wfcModel;

namespace Docsis_Application
{
    public partial class s_consulta_miembros : Form
    {
        DataAccess da;
        bool con_borde = MDI_Menu.con_borde;
        int vl_busqueda = 0;

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

        public s_consulta_miembros(DataAccess da)
        {
            InitializeComponent();
            cargarGrid(0);
            this.da = da;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cargarGrid(int vl_busqueda) {
            if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
            {
                DocSys.connOracle.Open();
            }
            if (vl_busqueda == 0)
            {

                string sql = @"select A.NOMBRE, A.IDENTIDAD, A.COD_CLIENTE, B.TIPO_MIEMBRO AS MIEMBRO, C.DESCRIPCION AS ZONA from WFC.DCS_MIEMBROS A 
                            INNER JOIN WFC.DCS_WF_TIPO_MIEMBRO B 
                            ON A.ID_TIPO_MIEMBRO = B.ID_TIPO_MIEMBRO 
                            INNER JOIN cef_zonas C 
                            ON A.CODIGO_ZONA = C.CODIGO_ZONA 
                            WHERE A.ACTIVO = 'SI' ";

                OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
                cmd.CommandType = CommandType.Text;
                DataTable mitable = new DataTable();

                OracleDataAdapter da = new OracleDataAdapter(cmd);

                da.Fill(mitable);
                da.Dispose();
               
                this.dgvDetalle.DataSource = null;
                this.dgvDetalle.DataSource = mitable;
                dgvDetalle.Refresh();
            }
            else {
                string sql = @"select A.NOMBRE, A.IDENTIDAD, A.COD_CLIENTE, B.TIPO_MIEMBRO AS MIEMBRO, C.DESCRIPCION AS ZONA from WFC.DCS_MIEMBROS A
                            INNER JOIN WFC.DCS_WF_TIPO_MIEMBRO B
                            ON A.ID_TIPO_MIEMBRO = B.ID_TIPO_MIEMBRO
                            INNER JOIN cef_zonas C
                            ON A.CODIGO_ZONA = C.CODIGO_ZONA
                            WHERE A.ACTIVO = 'SI'
                            AND A.COD_CLIENTE = "+ vl_busqueda + " ";
                OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                DataTable busqueda = new DataTable();
                busqueda.Clear();

                busqueda.Columns.Add("nombre");
                busqueda.Columns.Add("identidad");
                busqueda.Columns.Add("cod_Cliente");
                busqueda.Columns.Add("miembro");
                busqueda.Columns.Add("zona");

                while (dr.Read())
                {
                    busqueda.Rows.Add(
                        dr["nombre"].ToString(),
                        dr["identidad"].ToString(),
                        dr["cod_Cliente"].ToString(),
                        dr["miembro"].ToString(),
                        dr["zona"].ToString()
                    );
                }

                if (busqueda.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron datos para la busqueda");
                    cargarGrid(0);
                }
                else {
                    this.dgvDetalle.DataSource = busqueda;
                    dgvDetalle.Refresh();
                    dr.Close();
                }
            }
        }
        
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtNombreOCargo.Text))
            {
                MessageBox.Show("Agregue el Cargo o Nombre de miembro", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            vl_busqueda = Convert.ToInt32(txtNombreOCargo.Text);

           cargarGrid(vl_busqueda);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string id = dgvDetalle.CurrentRow.Cells["identidad"].Value.ToString();
            s_add_miembros addMiem = new s_add_miembros(da,"ACTUALIZA",id);
            addMiem.Show();
            Close();
        }

        private void btnHistorico_Click(object sender, EventArgs e)
        {
            s_Historico_Delegados hist = new s_Historico_Delegados(da);
            hist.Show();
            Close();
        }

        private void btnInhabilitar_Click(object sender, EventArgs e)
        {
            string id = dgvDetalle.CurrentRow.Cells["identidad"].Value.ToString();
            DocSys.Des_Miembro_Comite(id);
            MessageBox.Show("Deshabilitado");
            cargarGrid(0);
        }
    }
}
