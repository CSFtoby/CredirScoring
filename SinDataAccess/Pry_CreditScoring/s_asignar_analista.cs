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
    public partial class s_asignar_analista : Form
    {
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

        int vl_no_solicitud = 0;
        int vl_estacion_id  = 0;
        string vl_analista = "";
		private bool EsExcepcion = false;//felvir0

        public s_asignar_analista(int pa_no_solicitud,int pa_estacion_id, bool esExcepcion = false)
        {
            InitializeComponent();
            vl_estacion_id = pa_estacion_id;
            vl_no_solicitud = pa_no_solicitud;
            p_llenar_listbox_analistas();
			this.EsExcepcion = esExcepcion;
        }

        private void p_llenar_listbox_analistas()
        {
            try
            {
                DataSet dsCombo = new DataSet();
                dsCombo = DocSys.p_Obtener_un_dataset(@"Select codigo_usuario,rpad(codigo_usuario,15,' ')||' '||Initcap(nombres||' '||primer_apellido) nombre   
                                                          From dcs_wf_usuarios_estaciones ue,mgi_usuarios u  
                                                         Where codigo_empresa=1
                                                           and ue.usuario=u.codigo_usuario
                                                           and estacion_id=" + vl_estacion_id.ToString(), "mgi_usuarios");
                listBox_analistas.DataSource = dsCombo;
                listBox_analistas.DisplayMember = "mgi_usuarios.nombre";
                listBox_analistas.ValueMember = "mgi_usuarios.codigo_usuario";

                listBox_analistas_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_llenar_listbox_analistas() : " + ex.Message);
            }
        }

        private void listBox_analistas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                vl_analista = listBox_analistas.SelectedValue.ToString();
                analistaSelec.Text = vl_analista;
            }
            catch
            {
            }        
        }

        private void button_asignar_Click(object sender, EventArgs e)
        {
			if (!this.EsExcepcion)
			{
				this.asignar_analista_solicitud();
			}
			else
			{
				this.asignar_analista_excepcion();
			}
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void labelComite_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();   
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            moverForm();
        }

		//felvir01
		private void asignar_analista_solicitud()
		{
			try
			{
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}
				string vl_sql = "DCS_P_ASIGNAR_SOLICITUD";
				OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
				cmd.CommandType = CommandType.StoredProcedure;
				//───────────────────
				OracleParameter pa_solicitud = new OracleParameter("pa_no_solicitud", OracleType.Int32);
				cmd.Parameters.Add(pa_solicitud);
				pa_solicitud.Direction = ParameterDirection.Input;
				pa_solicitud.Value = vl_no_solicitud;
				//───────────────────
				OracleParameter pa_usuario_analista = new OracleParameter("pa_usuario_analista", OracleType.VarChar, 50);
				cmd.Parameters.Add(pa_usuario_analista);
				pa_usuario_analista.Direction = ParameterDirection.Input;
				pa_usuario_analista.Value = vl_analista;
				//----------------------
				cmd.ExecuteReader();

				this.Close();
				this.DialogResult = DialogResult.OK;
				return;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error en button_asignar_Click:" + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
		}

		private void asignar_analista_excepcion()
		{
			try
			{
				if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
				{
					DocSys.connOracle.Open();
				}

				string sql = "excp.dcs_p_asignar_excep";

				//───────────────────pa_codigo_excepcion
				OracleParameter pa_codigo_excepcion = new OracleParameter("pa_codigo_excepcion", OracleType.Number);
				pa_codigo_excepcion.Direction = ParameterDirection.Input;
				pa_codigo_excepcion.Value = this.vl_no_solicitud;

				//───────────────────pa_analista
				OracleParameter pa_analista = new OracleParameter("pa_analista", OracleType.VarChar, 30);
				pa_analista.Direction = ParameterDirection.Input;
				pa_analista.Value = this.vl_analista;

				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add(pa_codigo_excepcion);
				cmd.Parameters.Add(pa_analista);

				cmd.ExecuteNonQuery();

				MessageBox.Show("Excepción asignada con éxito");
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ha ocurrido un error al asignar el analista {ex.InnerException} - {ex.Message}", "Error");
			}
		}
    }
}
