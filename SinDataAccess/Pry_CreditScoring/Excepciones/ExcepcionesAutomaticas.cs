using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wfcModel;

namespace Docsis_Application.Excepciones
{
	/*
	 * Al guardar la solicitud
	 */
	public class ExcepcionesAutomaticas
	{
		private DataAccess da;

		/// <summary>
		/// Evalua si la solicitud necesita una excepción
		/// </summary>
		/// <param name="CodPrestatario">Código del prestatario de la solicitud</param>
		/// <param name="IngresoMensual">Ingreso mensual del prestatario</param>
		/// <param name="Edad">Edad del prestatario</param>
		/// <param name="TiempoLaborar">Tiempo de laborar del prestatario en meses</param>
		/// <param name="_da">Conexión al DataAccess</param>
		/// <param name="EsPlanilla">Si el prestatario es cliente por planilla es true, por ventanilla false</param>
		/// <param name="RespBuro">Respuesta del buró para el solicitante</param>
		/// <param name="TotalAport">Total en el complemento de aportaciones para el solicitante</param>
		/// <param name="CodAval1">En caso de tener aval, se envía el valor, sino es igual a cero</param>
		/// <param name="tipoPagoAval1">El tipo de afiliado que es el aval 1, tru planilla, false ventanilla</param>
		/// <param name="antLabAval1">Antiguedad laboral del aval 1 en meses</param>
		/// <param name="edadAval1">Edad del aval 1</param>
		/// <param name="ingresoMAval1">Ingreso mensual del Aval 1</param>
		/// <param name="RespBuroAval1">Respuesta del buró para el solicitante</param>
		/// <param name="CodAval2">Código de afiliado del aval2, en caso de no haber, se deja en cero</param>
		/// <param name="tipoPagoAval2">El tipo de afiliado que es el aval2, por ventanilla o planilla</param>
		/// <param name="antLabAval2">Antiguedad laboral que tiene el aval 2</param>
		/// <param name="edadAval2">Edad del aval2</param>
		/// <param name="ingresoMAval2">Ingreso Mensual del aval2</param>
		/// <param name="RespBuroAval2">Respuesta del buró de parte del aval 2</param>
		/// <returns></returns>
		public List<ExcepcionesGeneradas> NecesidadExcepcion(int CodPrestatario, decimal IngresoMensual, int Edad, int TiempoLaborar, DataAccess _da, bool EsPlanilla,
										 int RespBuro = 0, decimal TotalAport = 0,
										 int CodAval1 = 0, bool tipoPagoAval1 = false, int antLabAval1 = 0, int edadAval1 = 0, decimal ingresoMAval1 = 0, int RespBuroAval1 = 0,
										 int CodAval2 = 0, bool tipoPagoAval2 = false, int antLabAval2 = 0, int edadAval2 = 0, decimal ingresoMAval2 = 0, int RespBuroAval2 = 0)
		{
			this.da = _da;
			List<ExcepcionesGeneradas> excepcionesFiguras = new List<ExcepcionesGeneradas>();

			#region Prestatario
			int prestaCP = (int)this.CapacidadPago(IngresoMensual);
			int prestaGIAval1 = (CodAval1 != 0) ? (int)this.GarantiaInsuficiente(CodPrestatario, CodAval1) : (int)TipoLineamientos.SinExcepcion;
			int prestaGIAval2 = (CodAval2 != 0) ? (int)this.GarantiaInsuficiente(CodPrestatario, CodAval2) : (int)TipoLineamientos.SinExcepcion;
			int prestaRCN = (int)this.RecordCrediticioNegativo(RespBuro);
			int prestaIPLab = (int)this.IncumplimientoPoliticaLab(TiempoLaborar, false, EsPlanilla);
			int prestaIPEdad = (int)this.IncumplimientoPoliticaEdad(Edad);
			int prestaOE = (TotalAport > 0) ? (int)TipoLineamientos.OtrasExcepciones : (int)TipoLineamientos.SinExcepcion;
			excepcionesFiguras.Add(new ExcepcionesGeneradas
			{
				Figura = FigurasPrestatario.Prestatario,
				CapacidadPagoInsuficiente = prestaCP,
				GarantiaInsuficienteAval1 = prestaGIAval1,
				GarantiaInsuficienteAval2 = prestaGIAval2,
				RecordCrediticioNegativo = prestaRCN,
				IncumplimientoPoliticaLab = prestaIPEdad,
				IncumplimientoPoliticaEdad = prestaIPLab,
				OtrasExcepciones = prestaOE
			});
			#endregion

			#region Aval1
			if (CodAval1 > 0)
			{
				int aval1CP = (int)this.CapacidadPago(ingresoMAval1);
				int aval1RCN = (int)this.RecordCrediticioNegativo(RespBuroAval1);
				int aval1IPLab = (int)this.IncumplimientoPoliticaLab(antLabAval1, true, tipoPagoAval1);
				int aval1IPEdad = (int)this.IncumplimientoPoliticaEdad(edadAval1);
				excepcionesFiguras.Add(new ExcepcionesGeneradas
				{
					Figura = FigurasPrestatario.Aval1,
					CapacidadPagoInsuficiente = aval1CP,
					RecordCrediticioNegativo = aval1RCN,
					IncumplimientoPoliticaLab = aval1IPLab,
					IncumplimientoPoliticaEdad = aval1IPEdad
				});
			}
			#endregion

			#region Aval2
			if (CodAval2 > 0)
			{
				int aval2CP = (int)this.CapacidadPago(ingresoMAval2);
				int aval2RCN = (int)this.RecordCrediticioNegativo(RespBuroAval2);
				int aval2IPLab = (int)this.IncumplimientoPoliticaLab(antLabAval2, true, tipoPagoAval2);
				int aval2IPEdad = (int)this.IncumplimientoPoliticaEdad(edadAval2);
				excepcionesFiguras.Add(new ExcepcionesGeneradas
				{
					Figura = FigurasPrestatario.Aval2,
					CapacidadPagoInsuficiente = aval2CP,
					RecordCrediticioNegativo = aval2RCN,
					IncumplimientoPoliticaLab = aval2IPLab,
					IncumplimientoPoliticaEdad = aval2IPEdad
				});
			}
			#endregion

			return excepcionesFiguras;
		}

		/// <summary>
		/// Evalua la capacidad de pago del prestatario y aval
		/// </summary>
		/// <param name="ingresoMensual"></param>
		/// <returns></returns>
		private TipoLineamientos CapacidadPago(decimal ingresoMensual, bool esAval = false)
		{
			DataTable parametros = this.da.ValorParametro(!esAval ? u_Globales.SalarioMinimoPrestatario : u_Globales.SalarioMinimoAval);

			if (parametros.Rows.Count > 0)
			{
				decimal minimo = Convert.ToDecimal(parametros.Rows[0]["valor"].ToString());

				if (ingresoMensual > minimo)
				{
					return TipoLineamientos.SinExcepcion;
				}
				else
				{
					return TipoLineamientos.CapacidadPago;
				}
			}
			else
				return TipoLineamientos.SinExcepcion;
		}

		/// <summary>
		/// Para encontrar el contra-avalarse se revisan dos modos 
		/// 1.Cliente ya es aval del aval
		/// </summary>
		/// <param name="codCliente"></param>
		/// <param name="CodAval"></param>
		/// <returns></returns>
		private TipoLineamientos GarantiaInsuficiente(int codCliente, int CodAval)
		{
			DataTable avalarse = this.da.ContraAvalar(codCliente, CodAval);
			if (avalarse.Rows.Count > 0)
			{
				int modoAB = Convert.ToInt32(avalarse.Rows[0]["total"].ToString());
				if (modoAB > 0)
				{
					return TipoLineamientos.GarantiaInsuficiente;
				}
				else
				{
					return TipoLineamientos.SinExcepcion;
				}
			}
			else
			{
				return TipoLineamientos.SinExcepcion;
			}
		}

		/// <summary>
		/// Según el valor del resultado del buró, determina si necesita excepción
		/// </summary>
		/// <param name="resultadoBuro"></param>
		/// <returns></returns>
		private TipoLineamientos RecordCrediticioNegativo(int resultadoBuro)
		{
			if (resultadoBuro != (int)ResultadoBuroValores.APROBADO | resultadoBuro != (int)ResultadoBuroValores.ERROR)
			{
				return TipoLineamientos.RecordCrediticioNegativo;
			}

			return TipoLineamientos.SinExcepcion;
		}

		/// <summary>
		/// Evalua la politica sobre el incumplimiento de tiempo laboral
		/// </summary>
		/// <param name="tiempoLab"></param>
		/// <param name="esAval"></param>
		/// <param name="esPlanilla"></param>
		/// <returns></returns>
		private TipoLineamientos IncumplimientoPoliticaLab(int tiempoLab, bool esAval = false, bool esPlanilla = true)
		{
			DataTable parametros = new DataTable();

			if (!esAval)
			{
				parametros = this.da.ValorParametro(esPlanilla ? u_Globales.AntiguedadLaboralPP : u_Globales.AntiguedadLaboralPV);
				int tiempoPermitido = Convert.ToInt32(parametros.Rows[0]["valor"].ToString());

				if (tiempoLab < tiempoPermitido)
				{
					return TipoLineamientos.IncumplimientoPoliticasLab;
				}
			}
			else
			{
				parametros = this.da.ValorParametro(u_Globales.AntiguedadLaboralAVP);
				int tiempoPermitido = Convert.ToInt32(parametros.Rows[0]["valor"].ToString());

				if (tiempoLab < tiempoPermitido)
				{
					return TipoLineamientos.IncumplimientoPoliticasLab;
				}
			}

			return TipoLineamientos.SinExcepcion;
		}

		/// <summary>
		/// Evalua el incumplimiento de la política sobre la edad
		/// </summary>
		/// <param name="edad"></param>
		/// <returns></returns>
		private TipoLineamientos IncumplimientoPoliticaEdad(int edad)
		{
			DataTable parametros = new DataTable();
			parametros = this.da.ValorParametro(u_Globales.MinimoEdad);
			int minimaEdad = Convert.ToInt32(parametros.Rows[0]["valor"].ToString());
			parametros = this.da.ValorParametro(u_Globales.MaximoEdad);
			int maximaEdad = Convert.ToInt32(parametros.Rows[0]["valor"].ToString());

			if (edad < minimaEdad || edad > maximaEdad)
			{
				return TipoLineamientos.IncumplimientoPoliticasEdad;
			}

			return TipoLineamientos.SinExcepcion;

		}
	}

	public enum TipoLineamientos
	{
		CapacidadPago = 1,
		GarantiaInsuficiente = 2,
		RecordCrediticioNegativo = 3,
		InconformidadDocumentos = 4,
		IncumplimientoPoliticasEdad = 5,
		IncumplimientoPoliticasLab = 6,
		OtrasExcepciones = 7,
		SinExcepcion = -1
	}
}
