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
    public partial class s_clasificar_sol : Form
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


        int vl_solicitud;
        ImageList img_banderines;
        public s_clasificar_sol(ImageList pa_img_banderines,int pa_solicitud)
        {
            InitializeComponent();
            img_banderines = pa_img_banderines;
            vl_solicitud = pa_solicitud;

        }

        private void s_clasificar_sol_Load(object sender, EventArgs e)
        {
            string vl_sql;
            try
            {
                vl_sql = @"select * from dcs_wf_banderines where activo='S'";
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd2.CommandType = CommandType.Text;
                //───────────────────
                
                OracleDataReader dr = cmd2.ExecuteReader();
                DataTable tabla = new DataTable();
                tabla.Columns.Add("banderin_id");
                tabla.Columns.Add("color");
                
                while (dr.Read())
                {
                    tabla.Rows.Add(dr["banderin_id"].ToString(),
                                   dr["color"].ToString());
                }

                listView_banderin.BeginUpdate();
                listView_banderin.SmallImageList = img_banderines;
                listView_banderin.LargeImageList = img_banderines;
                listView_banderin.Clear();
                foreach (DataRow fila in tabla.Rows)
                {
                    ListViewItem listItem = new ListViewItem(fila["Color"].ToString());
                    listItem.ImageIndex = int.Parse(fila["banderin_id"].ToString());
                    listItem.ToolTipText = fila["Color"].ToString();
                    
                    listItem.SubItems.Add(fila["banderin_id"].ToString());
                    listItem.SubItems.Add(fila["color"].ToString());
                    listView_banderin.Items.Add(listItem);
                }
                listView_banderin.Columns.Add("Banderin_id", 80, HorizontalAlignment.Left);
                listView_banderin.Columns.Add("Color", 160, HorizontalAlignment.Left);                                
                listView_banderin.EndUpdate();
                listView_banderin.Sort();
                listView_banderin.View = View.LargeIcon;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_llenar_adjuntos : " + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void p_asignar()
        {

        }
        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }  
        private void listView_banderin_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = listView_banderin.GetItemAt(e.X, e.Y);
            if (item != null)
            {
                int vl_banderin_id = int.Parse(item.SubItems[1].Text);

                try
                {
                    if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                    {
                        DocSys.connOracle.Open();
                    }
                    string vl_sql = "dcs_p_clasificar_sol";
                    OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //───────────────────
                    OracleParameter pa_solicitud = new OracleParameter("pa_solicitud", OracleType.Int16);
                    cmd.Parameters.Add(pa_solicitud);
                    pa_solicitud.Direction = ParameterDirection.Input;
                    pa_solicitud.Value = vl_solicitud;
                    //───────────────────
                    OracleParameter pa_banderin_id = new OracleParameter("pa_banderin_id", OracleType.Int16);
                    cmd.Parameters.Add(pa_banderin_id);
                    pa_banderin_id.Direction = ParameterDirection.Input;
                    pa_banderin_id.Value = vl_banderin_id;
                    //----------------------
                    cmd.ExecuteReader();

                    this.Close();
                    this.DialogResult = DialogResult.OK;
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error en p_insertar_solicitud :" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            } 
        }
        private void button_desclasificar_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Desea quitar la clasificación que dio a la solicitud  ?", "DocSys", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                try
                {
                    if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                    {
                        DocSys.connOracle.Open();
                    }
                    string vl_sql = "dcs_p_clasificar_sol";
                    OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //───────────────────
                    OracleParameter pa_solicitud = new OracleParameter("pa_solicitud", OracleType.Int16);
                    cmd.Parameters.Add(pa_solicitud);
                    pa_solicitud.Direction = ParameterDirection.Input;
                    pa_solicitud.Value = vl_solicitud;
                    //───────────────────
                    OracleParameter pa_banderin_id = new OracleParameter("pa_banderin_id", OracleType.Int16);
                    cmd.Parameters.Add(pa_banderin_id);
                    pa_banderin_id.Direction = ParameterDirection.Input;
                    pa_banderin_id.Value = 0;
                    //----------------------
                    cmd.ExecuteReader();

                    this.Close();
                    this.DialogResult = DialogResult.OK;
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error en p_insertar_solicitud :" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }
    }
}
