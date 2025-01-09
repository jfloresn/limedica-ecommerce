using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Xmarket.Models.Coleccion
{
    public class ObtenerLibroColeccionRequest
    {
        public int cantidadRegistroInicio { get; set; }
        public int cantidadRegistroCompaginado { get; set; }
        public int cantidadRegistro { get; set; }
        public int? idColeccion { get; set; }
        public string coleccionNombre { get; set; }
    }
}