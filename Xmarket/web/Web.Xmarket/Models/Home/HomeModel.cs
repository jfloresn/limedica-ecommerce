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
    public class HomeModel
    {
        public IEnumerable<EditorialDTO> editoriales { get; set; }
        public IEnumerable<EditorialDTO> editoralesTodos { get; set; }
        public IEnumerable<EspecialidadDTO> especialidades { get; set; }
        public IEnumerable<BookFiltroDTO> Books { get; set; }

        public IEnumerable<BookFiltroDTO> BooksPorEspecialidad { get; set; }

        public IEnumerable<BannerDTO> banners { get; set; }
        public IEnumerable<BookFiltroDTO> BooksPorEditorial { get; set; }

        public IEnumerable<ColeccionSlide> coleccionesSlide { get; set; }

        public IEnumerable<ProductoSlide> productoSlides { get; set; }

    }
}