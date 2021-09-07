using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using wfcModel;

namespace Docsis_Application
{
    public partial class s_solicitudes_resoluciones : Form
    {

        s_miniinfo_solic miniinfo_sol = new s_miniinfo_solic();

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
        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }
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
                if (MDI_Menu.con_borde)
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

        ImageList il;
        ImageList il2;
        public DataAccess da;
        public Int32 gno_solicitud = 0;
        public s_solicitudes_resoluciones()
        {
            InitializeComponent();
            picAncho.Value = 100;
            picAlto.Value = 100;

            il = new ImageList();
            il.ImageSize = new Size(int.Parse(picAncho.Value.ToString()), int.Parse(picAlto.Value.ToString()));
            il.ColorDepth = ColorDepth.Depth32Bit;


            il2 = new ImageList();
            il2.ImageSize = new Size(55, 55);
            il2.ColorDepth = ColorDepth.Depth32Bit;
        }
        private void s_solicitudes_resoluciones_Load(object sender, EventArgs e)
        {
            llenar_listview();
        }
        public void llenar_listview()
        {
            il.ImageSize = new Size(int.Parse(picAncho.Value.ToString()), int.Parse(picAlto.Value.ToString()));
            il.ColorDepth = ColorDepth.Depth32Bit;

            lvfotos.Clear();
            lvfotos.Groups.Clear();
            il.Images.Clear();
            il2.Images.Clear();
            DataTable dtFotos = new DataTable();
            dtFotos.Columns.Add("foto", typeof(Image));
            dtFotos.Columns.Add("usuario_comite");
            dtFotos.Columns.Add("nombre_usuario");
            dtFotos.Columns.Add("fecha_decision");
            dtFotos.Columns.Add("decision");
            dtFotos.Columns.Add("observaciones");
            dtFotos.Clear();
            DataTable dt = da.ObtenerResolucionesDelComite(gno_solicitud);
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                try
                {
                    byte[] foto = null;
                    try
                    {
                        if (!DBNull.Value.Equals((byte[])dt.Rows[x]["foto"]))
                        {
                            foto = (byte[])dt.Rows[x]["foto"];
                        }
                    }
                    catch
                    {
                    }
                    Image col1 = null;
                    if (foto != null)
                    {
                        col1 = DocSys.p_CopyDataToBitmap((byte[])dt.Rows[x]["foto"]);
                    }
                    else
                    {
                        col1 = Properties.Resources.contacto_icon;
                    }
                    var col2 = dt.Rows[x]["usuario_comite"].ToString() + "\r\n" + dt.Rows[x]["nombre_usuario"].ToString().Trim();
                    var col3 = dt.Rows[x]["nombre_usuario"].ToString().Trim();
                    var col4 = dt.Rows[x]["fecha_decision"].ToString();
                    var col5 = dt.Rows[x]["decision"].ToString();
                    var col6 = dt.Rows[x]["observaciones"].ToString();

                    dtFotos.Rows.Add(col1, col2, col3, col4, col5, col6);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            panelCargando.Visible = false;
            lvfotos.View = View.Tile;
            lvfotos.LargeImageList = il;
            lvfotos.SmallImageList = il2;

            int i = 0;
            foreach (DataRow fila in dtFotos.Rows)
            {

                il.Images.Add(fila["usuario_comite"].ToString(), (Image)fila["foto"]);
                il2.Images.Add(fila["usuario_comite"].ToString(), (Image)fila["foto"]);



                var lvitem = new ListViewItem(new[] { fila["usuario_comite"].ToString().Trim()+"\r\n" +fila["decision"].ToString().Trim(), 
                                                    fila["fecha_decision"].ToString(),
                                                    fila["observaciones"].ToString()});

                lvitem.ImageIndex = i;
                //lvitem = fila["observaciones"].ToString().Trim();
                lvitem.ToolTipText = fila["usuario_comite"].ToString().Trim() + "\r\n" +
                                    fila["fecha_decision"].ToString().Trim() + "\r\n" +
                                    fila["decision"].ToString().Trim() + "\r\n" +
                                    fila["observaciones"].ToString().Trim();

                lvfotos.Items.Add(lvitem);
                i++;
            }
            lvfotos.View = View.Details;
            lvfotos.Columns.Add("Miembros Comite", 180, HorizontalAlignment.Left);
            lvfotos.Columns.Add("Fecha Decisión", 140, HorizontalAlignment.Left);
            lvfotos.Columns.Add("Observaciones", 450, HorizontalAlignment.Left);
            
            lvfotos.EndUpdate();
            radioButton4.Checked = true;

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            lvfotos.View = View.Tile;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            lvfotos.View = View.SmallIcon;
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            lvfotos.View = View.LargeIcon;
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            lvfotos.View = View.Details;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            lvfotos.View = View.List;
        }

        private void lnkTablaresultado_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmsRpts.frmRpt_ResolucionComite forma = new FrmsRpts.frmRpt_ResolucionComite(da);
            forma.gno_solicitud = gno_solicitud;
            forma.ShowDialog();
        }
        private void lvfotos_MouseLeave(object sender, EventArgs e)
        {
            miniinfo_sol.Hide();
            //StatusLabel1.Text = "";
        }
        private void lvfotos_MouseMove(object sender, MouseEventArgs e)
        {
            ListViewItem item = GetItemFromPoint(lvfotos, Cursor.Position);
            if (item != null)
            {
                txtObs.Text = lvfotos.Items[item.Index].SubItems[2].Text;
                //StatusLabel1.Text = lvfotos.Items[item.Index].SubItems[2].Text;
                //miniinfo_sol.label_nombre_cliente.Text = StatusLabel1.Text;
                //Point pos = this.PointToScreen(e.Location);
                //miniinfo_sol.Show();
                //miniinfo_sol.Location = new Point(Control.MousePosition.X - 105, Control.MousePosition.Y + 10);
                //miniinfo_sol.Refresh();
            }
        }
        private ListViewItem GetItemFromPoint(ListView listView, Point mousePosition)
        {
            // translate the mouse position from screen coordinates to 
            // client coordinates within the given ListView
            Point localPoint = listView.PointToClient(mousePosition);
            return listView.GetItemAt(localPoint.X, localPoint.Y);
        }
        private void lvfotos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvfotos.SelectedItems.Count == 0)
                return;

            ListViewItem item = GetItemFromPoint(lvfotos, Cursor.Position);
            if (item != null)
            {
                txtObs.Text = lvfotos.Items[item.Index].SubItems[2].Text;
            }

        }
    }
}
