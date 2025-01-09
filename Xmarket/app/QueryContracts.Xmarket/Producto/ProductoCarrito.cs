using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryContracts.Xmarket.Producto
{
   public class ProductoCarrito
    {

        public int IdCarrito { get; set; }
        public int Cantidad { get; set; }
        public int IdProducto { get; set; }
        public string IdProductoTxt { get; set; }

        
            
        public string Precio { get; set; }
        public string Peso { get; set; }

    }
}
