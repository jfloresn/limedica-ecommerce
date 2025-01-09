using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitario.Common
{
    public static class ConstanteGeneral
    {


        public static string CACHE_ESPECIALIDAD = "cache_especialidad";
        public static string CACHE_EDITORIAL = "cache_editorial";
        public static string CACHE_SESION = "cache_sesion";


        public static string WEB_SESSION_CLIENTE_DIRECCION_CARRITO = "session_cliente_direccion_carrito";
        public static class BOOK_FILTRO {


            public static  int FILTRO_TODOS=0;
            public static int FILTRO_POR_BOOK = 1;
            public static int FILTRO_POR_EDITORIAL = 2;
            public static int FILTRO_POR_ESPECIALIDAD = 3;
            public static int FILTRO_POR_NOVEDADES = 4;
            public static int FILTRO_POR_COLECCION = 5;
            public static int FILTRO_POR_BUSQUEDA = 6;



        }
        public static class CARRITO
        {


            public static string ITEM_FISICO ="fs";
            public static string ITEM_EBOOK = "vm";
            public static string ITEM_IBRIDO = "hi";

        }
        public static class ESTADO
        {


            public static int ACTIVO = 1;
            public static int DESACTIVADO = 0;




        }
    

        public static class URL_WEB
        {
            public static String LIMEDICA = "https://limedica.pe";
            public static String SITE_NAME = "limedica.pe";


        }


        public static class METADATA_WEB
        {
            public static String TITULO = "Title";
            public static String DESCRIPCION = "Descripcion";
            public static String OG_TITULO = "OgTitle";
            public static String OG_DESCRIPCION = "OgDescripcion";
            public static String CANONICAL = "Canonical";
            public static String OG_TYPE = "ogType";
            public static String OG_URL = "OgUrl";
            public static String OG_SITE_NAME = "ogSiteName";
            public static String OG_URL_IMAGE = "OgUrlImagen";
            public static String OG_KEYWORDS = "OgKeywords";


        }

        public static class TIPO_DIRECCIONES
        {


            public static String FACTURACION = "FACT";
            public static String ENTREGA = "ENTR";
 



        }
        public static class CUENTA_VISTA
        {


            public static int MI_CUENTA = 0;
            public static int ORDENES = 1;
            public static int METODO_PAGO = 2;
            public static int DIRECCIONES = 3;
            public static int DETALLE_CUENTA = 4;
         




        }
        public static class BOOK_TYPE_SEARCH_URL
        {


            public static string EDITORIAL = "ed";
            public static string ESPECIALIDAD = "es";
            public static string E_BOOK= "eb";
            public static string NOVEDADES = "nv";
            public static string BUSCAR = "sa";




        }
        public static class PAIS
        {


            public static string PERU = "PE";
       




        }
        public static class TIPO_BANNER
        {


            public static string NORMAL = "NORM";
            public static string FOTO = "FOTO";
            public static string FOTO_LINK = "FOLK";
            public static string VIDEO = "VIDE";
            public static string VIDEO_YOUTUBE = "YOUT";






        }
        public static class PATH_CUENTA
        {


            public static string MICUENTA = "/cuenta/micuenta";
            public static string DETALLE_CUENTA = "/cuenta/detallecuenta";
            public static string DIRECCIONES = "/cuenta/direcciones";
            public static string ORDENES = "/cuenta/ordenes";
            public static string MEOTOD_PAGO = "/cuenta/metodopago";




        }
        public enum TipoAccion
        {
            AgregarProducto = 1,
            QuitarProducto = 2,
            AgregarMasProductos = 3,
        }


        public const string RESPONSE_CORRECTO = "0";

    }
}

