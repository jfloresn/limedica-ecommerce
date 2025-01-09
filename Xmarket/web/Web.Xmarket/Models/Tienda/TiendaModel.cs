using QueryContracts.Xmarket.Book;
using QueryContracts.Xmarket.Book.Result;
using QueryContracts.Xmarket.Coleccion;
using QueryContracts.Xmarket.Editorial;
using QueryContracts.Xmarket.Editorial.Result;
using QueryContracts.Xmarket.Especialidad;
using QueryContracts.Xmarket.Especialidad.Result;
using QueryContracts.Xmarket.Parametro;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;
using System.Web.Mvc;

namespace Web.Xmarket.Models.Tienda
{
    public class TiendaModel
    {

        public TiendaModel(string imagen)
        {
            bookListarResult = new BookListarResult();
            request = new ObtenerLibrosRequest();
            imagen = imagen;

        }


        public ObtenerLibrosRequest request { get; set; }
        public BookListarResult bookListarResult { get; set; }

        public LeerEspecialidadResult especialidad { get; set; }
        public LeerEditorialResult editorial { get; set; }
        public IEnumerable<ColeccionDTO> colecciones { get; set; }
        public IEnumerable<ParametroDTO> idiomas {get;set;}
        public IEnumerable<EditorialDTO> editoriales { get; set; }
        public IEnumerable<EspecialidadDTO> especialidades { get; set; }


        public string imagen { get; set; }


    }
}