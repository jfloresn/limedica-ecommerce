using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryContracts.Xmarket.Carrito
{
    public class CarritoDetalleDTO
    {

        public string nombreProducto { get; set; }
        public decimal? pecioProducto { get; set; }
        public decimal? subtotal { get; set; }
        public int? idProducto { get; set; }
        public int? cantidadProducto { get; set; }

        public string estado { get; set; }
        public long? idCarrito { get; set; }
        public string formatoProducto { get; set; }
        public string formatoProductoNombre { get; set; }
        public string imagen { get; set; }
        public string bookISBN { get; set; }
        public string bookTitleUrl { get; set; }

        public int idEspecialidadRelacionado { get; set; }
        public int idEspecialidad { get; set; }

        
            
    }
}
