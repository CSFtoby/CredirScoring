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

namespace Docsis_Application.Excepciones
{
	public partial class e_resoluciones_excp : Form
	{
		DataAccess da;
		private int CodigoExcepcion;

		public e_resoluciones_excp(DataAccess _da, int codExcepcion)
		{
			InitializeComponent();
			this.da = _da;
			this.CodigoExcepcion = codExcepcion;
		}

		private void e_resoluciones_excp_Load(object sender, EventArgs e)
		{
			try
			{
				string sql = "select nvl(a.usuario_comite, 'Pendiente de asignación'), a.pendiente_respuesta_b, NVL(a.observaciones, 'Pendiente') observaciones, "
						+ "case a.pendiente_respuesta_b "
						+ "when 'N' then "
						+ "(select e.descripcion from excp.DCS_EXC_TIPO_DECISIONES d, DCS_EXC_ESTADOS_EXCEPCION e "
						+ "where d.ESTADO_EXCEP_ID = e.ESTADO_EXCEP_ID and a.decision_id = d.decision_id) "
						+ "else "
						+ "'Sin resolución' "
						+ "end estado "
						+ "from excp.DCS_EXCEPCIONES_APROBACIONES a "
						+ "where vigente = 'S' "
						+ "and codigo_excepcion = :pa_codigo_excepcion ";

				OracleParameter pa_codigo_Excepcion = new OracleParameter("pa_codigo_excepcion", OracleType.Number);
				pa_codigo_Excepcion.Direction = ParameterDirection.Input;
				pa_codigo_Excepcion.Value = this.CodigoExcepcion;

				OracleCommand cmd = new OracleCommand(sql, DocSys.connOracle);
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.Add(pa_codigo_Excepcion);

				DataTable aprobaciones = new DataTable();
				OracleDataAdapter da = new OracleDataAdapter(cmd);
				da.Fill(aprobaciones);

				this.dgvDetalle.DataSource = aprobaciones;
			}
			catch (Exception ex)
			{
				MessageBox.Show($"{ex.TargetSite} - {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}			
		}
	}
}
