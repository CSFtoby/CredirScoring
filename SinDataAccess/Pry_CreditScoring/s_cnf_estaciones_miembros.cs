using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using wfcModel;
using System.Threading;

namespace Docsis_Application
{
    public partial class s_cnf_estaciones_miembros : Form
    {
        DataAccess da;
        ImageList il;
        ImageList il2;
        Thread myThread;
        int segundos = 0;
        Int32 p_estacion_id = 0;
        public string usuarios_seleccionados = "";
        public string es_comite_local = "";
        public string es_comite_resolutivo = "";
        public int agencia_local = 0;
        public Image[] imagenes_usr_sele = new Image[100];

        #region Declaraciones del API
        const int WM_SYSCOMMAND = 0x112;
        const int MOUSE_MOVE = 0xF012;
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        //
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private const int cGrip = 13;      // Grip size
        private const int cCaption = 1;   // Caption bar height;
        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rc = new Rectangle(this.ClientSize.Width - cGrip, this.ClientSize.Height - cGrip, cGrip, cGrip);
            ControlPaint.DrawSizeGrip(e.Graphics, this.BackColor, rc);
            rc = new Rectangle(0, 0, this.ClientSize.Width, cCaption);
            e.Graphics.FillRectangle(Brushes.DarkBlue, rc);
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {  // Trap WM_NCHITTEST
                Point pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);
                pos = this.PointToClient(pos);
                if (pos.Y < cCaption)
                {
                    m.Result = (IntPtr)2;  // HTCAPTION
                    return;
                }
                if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)17; // HTBOTTOMRIGHT
                    return;
                }
            }
            base.WndProc(ref m);
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
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
        #endregion

        


        public s_cnf_estaciones_miembros(DataAccess da,Int32 p_estacion_id)
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            this.da = da;
            this.p_estacion_id = p_estacion_id;
            picAncho.Value =51;
            picAlto.Value = 51;
            
            il = new ImageList();
            il.ImageSize = new Size(int.Parse(picAncho.Value.ToString()), int.Parse(picAlto.Value.ToString()));
            il.ColorDepth = ColorDepth.Depth32Bit;


            il2 = new ImageList();
            il2.ImageSize = new Size(int.Parse(picAncho.Value.ToString()), int.Parse(picAlto.Value.ToString()));
            il2.ColorDepth = ColorDepth.Depth32Bit;
        }
        private void s_cnf_estaciones_miembros_Load(object sender, EventArgs e)
        {
            if (es_comite_resolutivo=="N")
            {
                btnAceptar.Visible = false;
                lblTitulo.Text = "Miembros de la estación";
            }
            p_inicia_hilo_info();
        }
        private void p_recopila_info()
        {
            llenar_listview("");
        }
        public void llenar_listview(string texto)
        {
            il.ImageSize = new Size(int.Parse(picAncho.Value.ToString()), int.Parse(picAlto.Value.ToString()));
            il.ColorDepth = ColorDepth.Depth32Bit;

            lvfotos.Clear();
            lvfotos.Groups.Clear();
            il.Images.Clear();
            il2.Images.Clear();
            DataTable dtFotos = new DataTable();
            dtFotos.Columns.Add("foto", typeof(Image));
            dtFotos.Columns.Add("no_emple");
            dtFotos.Columns.Add("codigo_usuario");
            dtFotos.Columns.Add("nombre");
            dtFotos.Columns.Add("departamento");
            dtFotos.Columns.Add("puesto");
            dtFotos.Clear();
            string campo = "d.descri";


            DataTable dt = null;

            if (es_comite_local == "S")
                dt = da.ObtenerMiembrosxEstacion(agencia_local, es_comite_local);
            else
                dt = da.ObtenerMiembrosxEstacion(p_estacion_id);

            for (int x = 0; x < dt.Rows.Count; x++)
            {
                Image col1 = null;
                if (!DBNull.Value.Equals(dt.Rows[x]["foto"]))
                {
                    col1 = DocSys.p_CopyDataToBitmap((byte[])dt.Rows[x]["foto"]);
                }
                else
                {
                    col1 = Properties.Resources.sin_foto;
                }
                
                var col2 = dt.Rows[x]["no_emple"].ToString();
                var col3 = dt.Rows[x]["codigo_usuario"].ToString();
                var col4 = dt.Rows[x]["nombres"].ToString();
                var col5 = dt.Rows[x]["depto"].ToString();
                var col6 = dt.Rows[x]["descri_puesto"].ToString();

                dtFotos.Rows.Add(col1, col2, col3, col4, col5,col6);
            }

            panelCargando.Visible = false;
            segundos = 0;

            lvfotos.View = View.LargeIcon;
            lvfotos.LargeImageList = il;
            lvfotos.SmallImageList = il2;


            string vl_depto = "";
            int i = 0;
            bool inicio = true;
            int indgrupo = 0;
            foreach (DataRow fila in dtFotos.Rows)
            {
                ListViewItem lvi = new ListViewItem();
                il.Images.Add(fila["no_emple"].ToString(), (Image)fila["foto"]);
                il2.Images.Add(fila["no_emple"].ToString(), (Image)fila["foto"]);

                lvi.ImageIndex = i;

                lvi.Tag = fila["no_emple"].ToString();
                lvi.Tag = fila["codigo_usuario"].ToString();
                lvi.ToolTipText = fila["nombre"].ToString();
                lvi.Text = fila["no_emple"].ToString() + "\n" + fila["codigo_usuario"].ToString() +"   "+ fila["nombre"].ToString() + "\n" + fila["puesto"].ToString();


                

                if (inicio)
                {
                    indgrupo = 0;
                    lvfotos.Groups.Add(new ListViewGroup(fila["departamento"].ToString(), HorizontalAlignment.Left));
                    vl_depto = fila["departamento"].ToString();
                    inicio = false;
                }
                if (fila["departamento"].ToString() != vl_depto)
                {
                    lvfotos.Groups.Add(new ListViewGroup(fila["departamento"].ToString(), HorizontalAlignment.Left));
                    vl_depto = fila["departamento"].ToString();
                    indgrupo++;
                }
                lvi.Group = lvfotos.Groups[indgrupo];

                lvfotos.Items.Add(lvi);
                i++;
            }
            //lvfotos.FullRowSelect = true;
            //lvfotos.Columns.Add("Empleado", 70, HorizontalAlignment.Left);
            //lvfotos.Columns.Add("Codigo", 80, HorizontalAlignment.Left);
            //lvfotos.Columns.Add("Nombre", 250, HorizontalAlignment.Center);
            //lvfotos.Columns.Add("Puesto", 250, HorizontalAlignment.Center);

            lvfotos.EndUpdate();
            lvfotos.Sort();

        }
        private void p_inicia_hilo_info()
        {
            segundos = 0;
            myThread = new Thread(new ThreadStart(p_recopila_info));
            panelCargando.Visible = true;
            centrar_panel();
            myThread.Start();

        }
       




        private void btnClose_Click(object sender, EventArgs e)
        {
            btnCancelar_Click(null, null);
        }
        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }
        private void centrar_panel()
        {
            panelCargando.Location = new Point(this.ClientSize.Width / 2 - panelCargando.Size.Width / 2, this.ClientSize.Height / 2 - panelCargando.Size.Height / 2);
            panelCargando.Anchor = AnchorStyles.None;
        }
        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }
        private void lblRefrescar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            p_inicia_hilo_info();
        }       
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (lvfotos.CheckedItems.Count<=0)
            {
                MessageBox.Show("Debe seleccionar los integrantes de esta resolución...", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int n = 0;
            foreach (ListViewItem item in lvfotos.CheckedItems)
            {
                usuarios_seleccionados = usuarios_seleccionados + item.Tag + "|";
                imagenes_usr_sele[n] = il2.Images[item.Index];
                n++;
            }

            this.DialogResult = DialogResult.OK;
        }
    }
}
