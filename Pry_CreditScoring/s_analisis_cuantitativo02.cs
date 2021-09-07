using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wfcModel;

namespace Docsis_Application
{
    public partial class s_analisis_cuantitativo02 : Form
    {
        public DataAccess da;
        public string g_guid;

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

        public s_analisis_cuantitativo02()
        {
            InitializeComponent();
        }

        private void s_analisis_cuantitativo02_Load(object sender, EventArgs e)
        {
            gvProyeccion.AutoGenerateColumns = false;
            gvProyeccion.DataSource = da.ObtenerDataGeneradaTirCat(g_guid);
            gvProyeccion.Refresh();
            txtGuidID_generado.Text = g_guid;

            var dt = (DataTable)gvProyeccion.DataSource;
            object sumObject = dt.Compute("sum(interes)", string.Empty);
            //var x = dt.AsEnumerable().Select(r => Convert.ToDecimal(r.Field<decimal>("Interes"))).Sum();

            txtTotal_intereses.Text = string.Format("{0:#,###,###,##0.00}", decimal.Parse(sumObject.ToString()));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void s_analisis_cuantitativo02_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }
    }
}
