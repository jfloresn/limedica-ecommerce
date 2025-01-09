using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandContracts.Xmarket.Carrito
{
    public class CarritoDetalle
    {
        public int? IdCarrito { get; set; }
        public int? IdProducto { get; set; }
        public string formatoProducto { get; set; }
        public string NombreProducto { get; set; }
        public double? Precio { get; set; }
        public int? Cantidad { get; set; }
        public int? TipoAccion { get; set; }
        public double? SubTotal { get; set; }
        public string Imagen { get; set; }
        public string Estado { get; set; }
        public int? IdUsuarioModificar { get; set; }
        public int? IdUsuarioCrear { get; set; }
        public DateTime FechaCrea { get; set; }
        public DateTime FechaModifica { get; set; }
        public int idSesion { get; set; }
    }
}
