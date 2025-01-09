using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Xmarket.Models.Carrito
{
    public class ActualizarCarritoModel
    {
     
        public IEnumerable<ProductoCarrito> productos { get; set; }
  

    }

    public class ProductoCarrito { 
    

        public int bookId { get; set; }
        public int bookCantidad { get; set; }

    }

}