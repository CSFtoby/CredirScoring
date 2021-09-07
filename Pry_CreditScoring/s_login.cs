using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using NotificacionesDll;
using Docsis_Application.Excepciones;

namespace Docsis_Application
{
    public partial class s_login : Form
    {
        public static string global_tnsnames = "";
        public static string global_usuario = "";
        public static string global_password = "";
        public static bool ventana_con_borde = true;

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


                if (ventana_con_borde)
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
        
        public s_login()
        {
            InitializeComponent();
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
        }

        private void button_cancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
			if (string.IsNullOrEmpty(txtUsuario.Text))
            {
                return;
            }
            if (string.IsNullOrEmpty(txtPass.Text))
            {
                return;
            }
            if (string.IsNullOrEmpty(txtBase.Text))
            {
                return;
            }

            global_usuario = txtUsuario.Text.ToUpper();
            global_password = txtPass.Text;
            global_tnsnames = txtBase.Text;

            try
            {
                DocSys.vl_user = txtUsuario.Text.ToUpper();
                DocSys.vl_pass = txtPass.Text;
                DocSys.p_obtener_string_conexion_oracle(out DocSys.string_db_oracle);
                DocSys.connOracle = new OracleConnection(DocSys.string_db_oracle);
                DocSys.connOracle.Open();
                if (DocSys.connOracle.State.ToString().ToUpper() == "OPEN")
                {
                    if (p_verificar_roles_adecuados())
                    {
                        this.ShowInTaskbar = false;
                        this.ShowIcon = false;
                        this.Visible = false;
                        
                        MDI_Menu menu = new MDI_Menu();
                        menu.labelTnsNames.Text = txtBase.Text.ToUpper();					
						menu.Show();						
                    }
                    else
                    {
                        MessageBox.Show("El usuario ingresado no cuenta con los roles adecuados, para ingresar y utilizar este sistema se requiere el rol MIG_TRANSACCION, consultar con sistemas", DocSys.vl_mensaje_avisos, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message, "A v i s o", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void p_get_info_file(string p_path, out string p_name, out string p_extension)
        {
            int f = p_path.Length - 1;
            string vl_char = "";
            string vl_part = "";
            for (int i = p_path.Length - 1; i >= 0; i--)
            {
                vl_char = p_path.Substring(f, 1);
                if (vl_char == "\\" | vl_char == "/")
                    break;
                f--;
            }
            vl_part = p_path.Substring(f + 1, (p_path.Length - 1) - f);
            string[] ArrayResponses;
            int x = 1;
            ArrayResponses = new string[4];
            foreach (string component in vl_part.Split(".".ToCharArray(), StringSplitOptions.None))
            {
                ArrayResponses[x] = component;
                ++x;
            }

            p_name = ArrayResponses[1].ToString();
            p_extension = ArrayResponses[2].ToString();

            return;
        }

        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }
        
        private void Form1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            moverForm();
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.ShowAlways = true;
            string texto = "Ingresar al sistema (" + DocSys.vl_tnsnames + ")";
            toolTip1.SetToolTip(this.button1, texto);
        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.Focus();
            }
        }

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPass.Focus();
            }
        }

        private void s_login_Load(object sender, EventArgs e)
        {
            CountryRegionName crn = new CountryRegionName();
            labelCultureInfo.Text = CultureInfo.CurrentCulture.Name.ToString();
            labelUbicacion.Text = crn.GetWinCountryRegionName(CultureInfo.CurrentCulture).Trim();
            
            txtBase.Text = DocSys.vl_tnsnames;
            if (txtBase.Text.ToUpper() == "CSF")
                txtPass.Text = "";
            else
            {
                txtPass.Text = "";
                button1.Focus();
            }
            actualizar_reloj();
            if ((labelCultureInfo.Text.ToUpper() != "ES-HN") || (labelUbicacion.Text.ToUpper().Substring(0,8) != "HONDURAS"))
            {
                labelAviso.Text = @"La configuracion regional de esta PC no esta correcta, la configuracion debe ser es-HN," + "\r\n" + "reportar a Soporte con Joel Fernandez,Juan Carlos Lopez, o Carlos Varela";
                button1.Enabled = false;
                txtUsuario.Enabled = false;
                txtPass.Enabled = false;
            }
        }

        private bool p_verificar_roles_adecuados()
        {
            bool vl_return = false;
            string vl_sql = @"SELECT count(*) n
                                FROM user_role_privs
                               Where upper(granted_role) like 'MGI_TRANSACCION' or upper(granted_role) like 'CREDITSCORING_ACCESS_EMP' 
							   or upper(granted_role) like 'CREDITSCORING_ACCESS_APP'";
            try
            {
                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd2.CommandType = CommandType.Text;
                OracleDataReader dr = cmd2.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    if (int.Parse(dr["n"].ToString()) > 0)
                    {
                        vl_return = true;
                    }
                    else
                        vl_return = false;
                }
                return vl_return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al tratar de recuperar los roles del usuario ");
                return vl_return;
            }
        }

        private void TimeBar_Tick(object sender, EventArgs e)
        {
            actualizar_reloj();
        }

        private void actualizar_reloj()
        {
            labelRelojPanel.Text = DateTime.Now.ToString("hh:mm");
            labelDiaPanel.Text = DateTime.Now.ToString("dddd");
            labelFechaPanel.Text = DateTime.Now.ToString("MMMM") + " " + DateTime.Now.ToString("dd");
        }

        private void panelTop_MouseMove(object sender, MouseEventArgs e)
        {
            moverForm();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e){}

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            s_acerca_de forma = new s_acerca_de();
            forma.ShowDialog();
        }
    }

    class CountryRegionName
    {
        // The name of a country or region in English
        int LOCALE_SENGCOUNTRY = 0x1002;

        // Use COM interop to call the Win32 API GetLocalInfo.
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern int GetLocaleInfo(
            // The locale identifier.
           int Locale,
            // The information type.
           int LCType,
            // The buffer size.
           [In, MarshalAs(UnmanagedType.LPWStr)] string lpLCData, int cchData
         );

        // A method to retrieve the .NET Framework Country/Region
        // that maps to the specified CultureInfo.
        public String GetNetCountryRegionName(CultureInfo ci)
        {
            // If the specified CultureInfo represents a specific culture,
            // the attempt to create a RegionInfo succeeds.
            try
            {
                RegionInfo ri = new RegionInfo(ci.LCID);
                return ri.EnglishName;
            }
            // Otherwise, the specified CultureInfo represents a neutral
            // culture, and the attempt to create a RegionInfo fails.
            catch
            {
                return String.Empty;
            }
        }

        // A method to retrieve the Win32 API Country/Region
        // that maps to the specified CultureInfo.
        public String GetWinCountryRegionName(CultureInfo ci)
        {
            int size = GetLocaleInfo(ci.LCID, LOCALE_SENGCOUNTRY, null, 0);
            String str = new String(' ', size);
            int err = GetLocaleInfo(ci.LCID, LOCALE_SENGCOUNTRY, str, size);
            // If the string is not empty, GetLocaleInfo succeeded.
            // It will succeed regardless of whether ci represents
            // a neutral or specific culture.
            if (err != 0)
                return str;
            else
                return String.Empty;
        }
    }

}
