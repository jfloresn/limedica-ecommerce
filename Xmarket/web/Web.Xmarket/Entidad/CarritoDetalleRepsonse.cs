using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Xmarket.Entidad
{
    public class CarritoDetalleRepsonse
    {
        public string nombreProducto { get; set; }
        public int? cantidadProducto { get; set; }
        public string formatoProductoNombre { get; set; }
        public string imagen { get; set; }
        public string bookISBN { get; set; }
    }
}