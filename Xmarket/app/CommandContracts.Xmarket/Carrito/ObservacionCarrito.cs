using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandContracts.Xmarket.Carrito
{
    public class ObservacionCarrito
    {
        public int IdProducto { get; set; }
       
        public string NombreProducto { get; set; }

        public string Precio { get; set; }

        public string Cantidad { get; set; }


    }
}
