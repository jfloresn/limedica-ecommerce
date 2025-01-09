using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitario.Common
{
   public class ParametroDatos
    {

        public class Parametro { 
            public static  int  TIPO_DOCUMENTO_PEDIDO=45;
            public static int TIPO_DOCUMENTO_CONTACTO = 48;
            public static int TIPO_IDIOMA = 38;
            public static int BANCO = 54;
            public static int TIPO_DIRECCION = 62;
        }

        public class ParametroValor
        {
            public class TIPO_DIRECCION {
                public static string FACTURACION = "FACT";
                public static string ENTREGA = "ENTR";
            }
        }

        public class Pedido
        {
            public class TipoRegistroPedido
            {
                public static string USUARIO_INICIO_SESION = "INSE";
                public static string USUARIO_NO_INICIO_SESION = "SEPU";
            }

            public class Estado
            {
                public static string PAGADO = "PAGA";
                public static string CANCELADO = "CANC";
                public static string REGISTRADO = "REGI";
            }

            public class MetodoPago
            {
                public static string CONTRA_ENTREGA = "COEN";
                public static string BANCA_INTERNET = "BAIN";
            }

        }

    }
}