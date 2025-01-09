using QueryContracts.Xmarket.Book;
using QueryContracts.Xmarket.Book.Result;
using QueryContracts.Xmarket.Coleccion;
using QueryContracts.Xmarket.Coleccion.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Xmarket.Models.Coleccion
{
    public class ColeccionDetalleModel
    {


        public BookListarResult bookListarResult { get; set; }
        public ObtenerLibroColeccionRequest request { get; set; }

        public LeerColeccionResult coleccionResult { get; set; }

        public ColeccionDetalleModel()
        {
            request = new ObtenerLibroColeccionRequest();
            coleccionResult = new LeerColeccionResult();
            bookListarResult = new BookListarResult();
        }
    }
}