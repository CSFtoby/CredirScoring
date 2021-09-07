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
    public partial class s_miniinfo_usuario : Form
    {

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
        public DataAccess da;
        string vl_parametro_poner_foto = "S";
        public string get_set_codigo_usuario { get; set; }
        string vl_usuario = "";

        public s_miniinfo_usuario()
        {
            InitializeComponent();
        }
        private void p_info_usuario()
        {
            if (DocSys.connOracle.State == ConnectionState.Closed)
            {
                DocSys.connOracle.Open();
            }
            string vl_sql = @"Select Initcap(nombres)||' '||InitCap(primer_apellido) nombre_comple,
                                     Initcap(a.nombre_agencia) nombre_agencia,
                                     up.correo_electronico,
                                     up.numero_extension,
                                     up.numero_celular,
                                     e.nombre nombre_estacion,
                                     mebo                                    
                                From mgi_usuarios u,
                                     mgi_agencias a,
                                     dcs_wf_usuarios_estaciones ue,
                                     dcs_wf_usuarios_perfiles up,
                                     dcs_wf_estaciones e
                               Where a.codigo_empresa=1
                                 and u.codigo_empresa=1
                                 and u.codigo_agencia=a.codigo_agencia
                                 and u.codigo_usuario=up.usuario(+)
                                 and u.codigo_usuario=ue.usuario(+)
                                 and e.estacion_id=ue.estacion_id
                                 and codigo_usuario=:pa_codigo_usuario";
            OracleCommand cmd = new OracleCommand(vl_sql, DocSys.connOracle);
            cmd.CommandType = CommandType.Text;
            //───────────────────
            OracleParameter pa_codigo_usuario = new OracleParameter("pa_codigo_usuario", OracleType.VarChar, 100);
            cmd.Parameters.Add(pa_codigo_usuario);
            pa_codigo_usuario.Direction = ParameterDirection.Input;
            pa_codigo_usuario.Value = vl_usuario;

            OracleDataReader dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                label_nombre.Text = dr["nombre_comple"].ToString().Trim() + " (" + vl_usuario + ")";
                label_filial.Text = dr["nombre_agencia"].ToString();
                label_estacion_wf.Text = dr["nombre_estacion"].ToString().Trim();
                label_correo.Text = dr["correo_electronico"].ToString().Trim();
                label_extesion.Text = dr["numero_extension"].ToString().Trim();
                label_celular.Text = dr["numero_celular"].ToString().Trim();

                if (!DBNull.Value.Equals(dr["Mebo"]) && vl_parametro_poner_foto == "S")
                {
                    byte[] bits = ((byte[])dr["mebo"]);
                    pbMebo.Image = new Bitmap(DocSys.p_CopyDataToBitmap(bits));
                }
                else
                {
                    pbMebo.Image = null;
                }
            }
            try
            {
                string vl_mostrar_fotocolaborador = da.ObtenerParametro("WFC-0016");
                if (vl_mostrar_fotocolaborador == "S")
                {
                    string codigo_cliente_usuario = da.ObtenerCodigoClientexUsuario(vl_usuario);
                    byte[] fotoemp = da.ObtenerFotoUsuario(codigo_cliente_usuario);
                    pbMebo.Image = DocSys.p_CopyDataToBitmap(fotoemp);
                }
            }
            catch (Exception ex)
            {
                pbMebo.Image = null;
            }
        }
        private void s_miniinfo_Load(object sender, EventArgs e)
        {

        }
        private void s_miniinfo_Activated(object sender, EventArgs e)
        {
            vl_usuario = get_set_codigo_usuario;
            p_info_usuario();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
