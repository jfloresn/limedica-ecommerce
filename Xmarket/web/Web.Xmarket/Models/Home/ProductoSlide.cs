using QueryContracts.Xmarket.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Xmarket.Models.Home
{
    public class ProductoSlide
    {

        public ProductoSlide() {
            books = new List<BookFiltroDTO>();
                }
        public List<BookFiltroDTO> books { get; set; }
        public bool esVisible { get; set; }

    }


}