using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Docsis_Application.Excepciones
{
	public static class u_Globales
	{
		public const string flujoSol = "SOL";//felvir01
		public const string flujoExc = "EXC";//felvir01

		public const string accionAgregar = "INS";
		public const string accionModificar = "MODIF";
		public const string accionEliminar = "ELIM";
		public const string accionConsultar = "CONS";

		public static string CondicionTU = string.Empty;
		public static decimal IngresoNeto = 0;
		public static decimal IngresoNetoD = 0;

		public const string ReglaLineamiento = "LIN";
		public const string ReglaExcepcion = "EXC";

		public const int eAfiliacion = 1000;

		public const string Afirmativo = "S";
		public const string Negativo = "N";

		public const int EnProceso = 1;
		public const int Aprobado = 2;
		public const int Rechazada = 3;
		public const int Anulada = 4;

		public const string SalarioMinimoPrestatario = "WFC-0030";
		public const string SalarioMinimoAval = "WFC-0030";

		public const string AntiguedadLaboralPP = "WFC-0032"; //antigüedad laboral prestatario por planilla
		public const string AntiguedadLaboralPV = "WFC-0033"; //antiguedad laboral prestatario por ventanilla
		public const string AntiguedadLaboralAVP = "WFC-0034"; //Antiguedad laboral avales por ventanilla y planilla	

		public const string MinimoEdad = "WFC-0035";
		public const string MaximoEdad = "WFC-0036";

		public const string MONTO1 = "MONTO1";
		public const string MONTO2 = "MONTO2";
		public const string MONTO3 = "MONTO3";
		public const string RESOLUTIVO = "RESOLUTIVO";

		public const string FiltroMonto1 = " between 0 and 3000000.00 ";
		public const string FiltroMonto2 = " between 3000000.01 and 1800000.00 ";
		public const string FiltroMonto3 = " >= 1800000.01 ";

		public const string ExcepcionPrestaciones = "2-GI-6";

		public static void obtener_info_reporte_TU(string p_resultado_consulta)
		{
			if (string.IsNullOrEmpty(p_resultado_consulta))
				return;

			CondicionTU = string.Empty;
			IngresoNetoD = 0;
			IngresoNeto = 0;
			
			XmlDocument xmlDoc = new XmlDocument();

			string resultado_consulta = p_resultado_consulta;
			string resultadoBuro = string.Empty;
			string gxml_respuesta = resultado_consulta;
			try
			{
				xmlDoc.LoadXml(resultado_consulta);
				string vl_Status = xmlDoc.SelectSingleNode("DCResponse/Status").InnerText;
				if (vl_Status.ToString() == "Success")
				{
					XmlDocument OutputXML = new XmlDocument();					
					OutputXML.LoadXml(xmlDoc.SelectSingleNode("DCResponse/ContextData/Field[@key='OutputXML']").InnerText);

					string gxml_outputxml = OutputXML.InnerXml;
					string vl_condicion = OutputXML.SelectSingleNode("DCResponse/Multi/item0/DecisionPrecalificado").InnerText;
					string vl_ingreso_neto = string.Empty;
					string vl_ingreso_neto_d = string.Empty;

					try
					{
						vl_ingreso_neto = OutputXML.SelectSingleNode("DCResponse/Multi/item0/IngresoNeto").InnerText;
						vl_ingreso_neto_d = OutputXML.SelectSingleNode("DCResponse/Multi/item0/IngresoNetoNuevo").InnerText;
					}
					catch (Exception)
					{
						vl_ingreso_neto = "0";
						vl_ingreso_neto_d = "0";
					}

					CondicionTU = vl_condicion;
					IngresoNeto = Convert.ToDecimal(vl_ingreso_neto);
					IngresoNetoD = Convert.ToDecimal(vl_ingreso_neto_d);
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <summary>
		/// Obtiene el resultado del buró
		/// </summary>
		/// <param name="resultado">Cadena que captura</param>
		/// <param name="tipoAval">1 - Aval1, 2 - Aval2, 3 - Prestatario</param>
		/// <returns></returns>
		public static ResultadoBuroValores resultadoBuroExcepcion(string resultado, int tipoAval)
		{
			if (string.IsNullOrEmpty(resultado))
				return ResultadoBuroValores.ERROR;
			try
			{
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(resultado);
				string respuesta = string.Empty;
				string vl_Status = xmlDoc.SelectSingleNode("DCResponse/Status").InnerText;
				if (vl_Status.ToString() == "Success")
				{
					XmlDocument OutputXML = new XmlDocument();
					OutputXML.LoadXml(xmlDoc.SelectSingleNode("DCResponse/ContextData/Field[@key='OutputXML']").InnerText);
					string gxml_outputxml = OutputXML.InnerXml;					
					if(tipoAval == 1) //Aval1
					{
						respuesta = OutputXML.SelectSingleNode("DCResponse/Multi/item2/DecisionPrecalificado").InnerText;
					}
					else if(tipoAval == 2) //Aval2
					{
						respuesta = OutputXML.SelectSingleNode("DCResponse/Multi/item3/DecisionPrecalificado").InnerText;
					}
					else if (tipoAval == 3) //Principal
					{
						respuesta = OutputXML.SelectSingleNode("DCResponse/Multi/item0/DecisionPrecalificado").InnerText;
					}

					if (respuesta.ToUpper().Equals("RECHAZADO"))
					{
						return ResultadoBuroValores.RECHAZADO;
					}
					else if (respuesta.ToUpper().Equals("REFERIDO"))
					{
						return ResultadoBuroValores.REFERIDO;
					}
					else if (respuesta.ToUpper().Equals("APROBADO"))
					{
						return ResultadoBuroValores.APROBADO;
					}
					else if (respuesta.ToUpper().Equals("CONDICIONADO"))
					{
						return ResultadoBuroValores.CONDICIONADO;
					}
				}

				return ResultadoBuroValores.ERROR;
			}
			catch (Exception ex)
			{
				return ResultadoBuroValores.ERROR;
			}
		}
	}

	public class ExcepcionesGeneradas
	{
		public FigurasPrestatario Figura { get; set; }		
		public int CapacidadPagoInsuficiente { get; set; }
		public int GarantiaInsuficienteAval1 { get; set; }
		public int GarantiaInsuficienteAval2 { get; set; }
		public int RecordCrediticioNegativo { get; set; }
		public int IncumplimientoPoliticaLab { get; set; }
		public int IncumplimientoPoliticaEdad { get; set; }
		public int OtrasExcepciones { get; set; }
	}

	public enum TipoCliente
	{
		Planilla = 1,
		Ventanilla = 2
	}

	public enum FigurasPrestatario
	{
		Prestatario = 1,
		Codeudor = 2,
		Aval1 = 3,
		Aval2 = 4,
		GaranteHipotecario = 5
	}

	public enum ResultadoBuroValores
	{
		RECHAZADO = 1,
		APROBADO = 2,
		REFERIDO = 3,
		CONDICIONADO = 4,
		ERROR = -1
	}

	public enum TipoMensaje
	{
		SOLICITUD = 1,
		EXCEPCION = 2
	}

	public enum Estaciones
	{
		Afiliacion = 1000,
		NivelResolutivoFilial = 1001,
		CreditosTGU = 1002,
		PrestamosTGU = 1003,
		ComiteITGU = 1004,
		EstacionTransito = 1005,
		EstacionValidacion = 1006,
		CreditosSPS = 2001,
		PrestamosSPS = 2002,
		ComiteISPS = 2003,
		CreditosLCBA = 3001,
		PrestamosLCBA = 3002,
		ComiteILCBA = 3003,
		ComiteII = 4000,
		ComiteIII = 5000,
		ComiteEmpleados = 6000,
		Legal = 7000,
		JefaturaCreditos = 9000,
		GerenciaRegMetro = 9001,
		GerenciaRegCentroSur = 9002,
		GerenciaRegLitAtl = 9003,
		GerenciaRegNorOccidente = 9004,
		GerenciaNegocios = 9005
	}
}
