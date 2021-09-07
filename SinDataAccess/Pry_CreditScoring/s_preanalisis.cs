using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using wfcModel;

namespace Docsis_Application
{
	public partial class s_preanalisis : Form
	{
		delegate void Functionz();
		DataAccess da;
		string usuario_wstransunion = MDI_Menu.usuario_wstransunion;
		string pass_wstransunion = MDI_Menu.pass_wstransunion;
		private string codigoAgencia;
		string gxml_cuotas_buro = string.Empty;
		DataTable dtCuotasBuro = new DataTable();
		int tipoPago = 0;

		public s_preanalisis(DataAccess _da, string _codigoAgencia)
		{
			InitializeComponent();
			this.da = _da;
			this.codigoAgencia = _codigoAgencia;

			dtCuotasBuro.Columns.Add("seleccion");
			dtCuotasBuro.Columns.Add("institucion");
			dtCuotasBuro.Columns.Add("cuota", typeof(float));
			dtCuotasBuro.Columns.Add("saldo", typeof(float));
		}

		private void btnObtener_cuotas_buro_Click(object sender, EventArgs e)
		{
			Thread myThread;
			myThread = new Thread(new ThreadStart(p_obtener_detalle_cuotas_en_buro_solicitante));
			myThread.Start();
		}

		private void p_obtener_detalle_cuotas_en_buro_solicitante()
		{
			CheckForIllegalCrossThreadCalls = false;
			DataTable vl_retorno = new DataTable();
			this.Invoke(new Functionz(delegate ()
			{
				panelWait_cuotasconsolo.Visible = true;
				txtTotal_cuotas_consolidar.Text = "0.00";
				txtTotal_capital_consolidar.Text = "0.00";
			}));			
			string xml_request = @"<Request>
                                      <Authentication>
                                         <UserId>" + usuario_wstransunion + @"</UserId>
                                         <Password>" + pass_wstransunion + @"</Password>
                                      </Authentication>" + @"
                                      <Cedula>" + txtIDSolicitante.Text + @"</Cedula>
                                      <Prestamo>" + this.Pago_Mediante_Seleccion() + @"</Prestamo>
                                      <UsuarioWorkflow>" + DocSys.vl_user + @"</UsuarioWorkflow>
                                      <Sucursal>" + codigoAgencia + @"</Sucursal>
                                   </Request>";
			try
			{
				MD_Coop ws_transunion = new MD_Coop();
				var resultado_consulta = ws_transunion.ObtenerCuotas(xml_request);
				if (resultado_consulta != "<tabla></tabla>")
				{
					gxml_cuotas_buro = resultado_consulta;
					DataTable dt = xml_to_datatable(resultado_consulta);
					dtCuotasBuro.Rows.Clear();

					Int32 vl_idBitacora = 0;
					Int32 vl_no_solicitud = 0;
					Int32 vl_appid = 0;
					try
					{
						vl_idBitacora = Int32.Parse(dt.DataSet.Tables[0].Rows[0][0].ToString());
						//vl_no_solicitud = Int32.Parse(txtNo_solicitud_coopsafa.Text);
						//vl_appid = Int32.Parse(txtApplicationID.Text);
					}
					catch
					{

					}
					//3 - Obtencion de Cuotas en el log
					da.p_generar_bitacorabusqueda_tu(vl_idBitacora, vl_no_solicitud, vl_appid, 3);

					if (dt.DataSet.Tables.Count > 1)
					{
						for (int x = 0; x < dt.DataSet.Tables[1].Rows.Count; x++)
						{
							dtCuotasBuro.Rows.Add(false,
												  dt.DataSet.Tables[1].Rows[x][0].ToString(),
												  dt.DataSet.Tables[1].Rows[x][2].ToString(),
												  dt.DataSet.Tables[1].Rows[x][1].ToString());
						}
						vl_retorno = dtCuotasBuro;
					}
				}
			}
			catch (Exception ex)
			{
				//MessageBox.Show(ex.Message);
			}

			this.Invoke(new Functionz(delegate ()
			{
				panelWait_cuotasconsolo.Visible = false;

			}));
			gvCuotasBuro.DataSource = vl_retorno;
			if (vl_retorno.Rows.Count <= 0)
			{
				MessageBox.Show("No se encontró detalle de cuotas en el buro..!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private string Pago_Mediante_Seleccion()
		{
			string valor = string.Empty;

			if (tipoPago == 0)
			{
				valor = "Ventanilla";
			}
			else if (tipoPago == 1)
			{
				valor = "Planilla";
			}

			return valor;
		}

		public DataTable xml_to_datatable(string xmlData)
		{
			StringReader theReader = new StringReader(xmlData);
			DataSet theDataSet = new DataSet();
			theDataSet.ReadXml(theReader);

			return theDataSet.Tables[0];
		}

		private void cmbPagoMediante_SelectionChangeCommitted(object sender, EventArgs e)
		{
			tipoPago = this.cmbPagoMediante.SelectedIndex;
		}

		private void gvCuotasBuro_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			gvCuotasBuro.CommitEdit(DataGridViewDataErrorContexts.Commit);
		}

		private void calcular_cuotas_consolo(int indice)
		{

			double total_cuotas = 0;
			double total_capital = 0;
			for (int i = 0; i < gvCuotasBuro.Rows.Count; i++)
			{
				//string seleccionada="false";
				//if (!string.IsNullOrEmpty(gvCuotasBuro.Rows[i].Cells[0].Value.ToString()))
				//{
				//    seleccionada = "true";
				//}

				string seleccionada = gvCuotasBuro.Rows[i].Cells[0].Value.ToString();
				if (seleccionada == "true")
				{

					var cuotas = gvCuotasBuro.Rows[i].Cells[2].Value.ToString();
					var saldos = gvCuotasBuro.Rows[i].Cells[3].Value.ToString();
					total_cuotas = total_cuotas + double.Parse(cuotas);
					total_capital = total_capital + double.Parse(saldos);
				}
			}
			txtTotal_cuotas_consolidar.Text = total_cuotas.ToString("###,###,##0.00");
			txtTotal_capital_consolidar.Text = total_capital.ToString("###,###,###,##0.00");
		}

		private void gvCuotasBuro_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				var tempo = gvCuotasBuro.Rows[e.RowIndex].Cells[0].Value;
				calcular_cuotas_consolo(0);
			}
		}
	}
}
