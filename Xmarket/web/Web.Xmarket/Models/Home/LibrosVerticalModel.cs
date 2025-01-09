using QueryContracts.Xmarket.Banner;
using QueryContracts.Xmarket.Book;
using QueryContracts.Xmarket.Categoria;
using QueryContracts.Xmarket.Editorial;
using QueryContracts.Xmarket.Especialidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Xmarket.Models.Carrito;
using Web.Xmarket.Models.Coleccion;

namespace Web.Xmarket.Models.Home
{
    public class LibrosVerticalModel
    {
        public IEnumerable<QueryContracts.Xmarket.Book.BookFiltroDTO> libros { get; set; }
        public string tipofiltro { get; set; }
        public string idEspecialidad { get; set; }
        public string especialidad { get; set; }

        public string idEditorial { get; set; }
        public string editorial { get; set; }


    }
}