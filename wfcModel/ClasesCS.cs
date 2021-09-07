using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace wfcModel
{
    namespace MGI
    {
        namespace SubAplicaciones
        {
            
            public static class MGI_SUB_APLICACIONES
            {
                public static DataAccess da { get; set; }
                public static Int16 Codigo_sub_aplicacion { get; set; }
                public static string Desc_sub_aplicacion { get; set; }
                public static string Codigo_aplicacion { get; set; }
            }

            public class CR_TIPO_PRESTAMO
            {
                public static DataAccess da { get; set; }

                public Int16 Cod_tipopres { get; set; }
                public string desc_tipopres { get; set; }
                public Int16 Num_mesesplazo { get; set; }
                public Int16 Num_mesesplazo_min { get; set; }
                public Int16 Num_mesesplazo_base { get; set; }
                public decimal Por_tasaminima { get; set; }
                public decimal Por_tasamaxima { get; set; }
                public decimal Por_tasaomision { get; set; }
                public decimal Por_tasamoros { get; set; }
               

                public static CR_TIPO_PRESTAMO obtenerTipoPrestamo(Int16 pa_codigo_sub_aplicacion)
                {
                    CR_TIPO_PRESTAMO vl_retorno = da.ObtenerTipoPrestamo(pa_codigo_sub_aplicacion);
                    return vl_retorno;

                }

                
            }
        }
    }
    
}
