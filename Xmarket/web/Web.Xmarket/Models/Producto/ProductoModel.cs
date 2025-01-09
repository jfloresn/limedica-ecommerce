using QueryContracts.Xmarket.Book.Result;
using QueryContracts.Xmarket.Carrito.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Xmarket.Models.Home;

namespace Web.Xmarket.Models.Producto
{
    public class ProductoModel
    {

        public ProductoModel() {

            bookResult = new BookLeerResult();
            bookResult.Hit = new QueryContracts.Xmarket.Book.BookDTO();
            productoCarrito = new LeerCarritoDetalleResult();
       
        }

        public BookLeerResult bookResult { get; set; }
        public LeerCarritoDetalleResult productoCarrito { get; set; }

        public IEnumerable<ProductoSlide> productoRelacionSlides { get; set; }
    }
}