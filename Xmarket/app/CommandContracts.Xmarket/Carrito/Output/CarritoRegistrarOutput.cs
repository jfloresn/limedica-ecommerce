
using CommandContracts.Common;
using System;
using System.Collections.Generic;

namespace CommandContracts.Xmarket.Carrito.Output
{
    public class CarritoRegistrarOutput : CommandResult
    {
        public Int32 Estado { get; set; }
        public string Mensaje { get; set; }

        public int CantidadProductos { get; set; }
        public int CantidadProducto { get; set; }


        public long? IdCarrito { get; set; }

        public IEnumerable<ObservacionCarrito> Observacion { get; set; }

        
    }
}