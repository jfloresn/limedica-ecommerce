using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Xmarket.Models.Carrito
{
    public class AgregarCarritoModel
    {
        public string bookIsbn { get; set; }
        public int bookId { get; set; }
        public int bookCantidadComprar { get; set; }
        public string bookFormato { get; set; }

    }
}