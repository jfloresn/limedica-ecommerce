using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCommon.Common
{

    public enum TipoCliente
    {
        usuarioFinal = 1,
        usuarioNegocio = 2
    }

    public enum EstadoOperacion
    {
        cancelado = 0,
        pagado = 1,
        rechazado = 2
    }


    public enum Origen
    {
        Vendedor = 1,
        Bodega = 2,
        BodegaVip = 3,
        Complementario = 4,
        ClienteFinal = 5,
        ClienteNegocio = 6
    }

    public class TipoDistribuidor
    {
        public static string CanalVenta = "CV0003";
        public static string FuerzaVenta = "FV0002";
    }

    public enum TipoAccion
    {
        AgregarProducto = 1,
        QuitarProducto = 2,
        AgregarMasProductos = 3,
    }


    public enum TipoAccionCarrito
    {
        AgregarProducto = 1,
        QuitarProducto = 2,
        AgregarMasProductos = 3,
    }

}