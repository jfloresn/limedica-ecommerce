using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Xmarket.Models.Tienda
{
    public class ObtenerLibrosRequest
    {
        public int cantidadRegistroInicio { get; set; }
        public int cantidadRegistroCompaginado { get; set; }
        public int cantidadRegistro { get; set; }
        public string typeSearch { get; set; }
        public string codEdit { get; set; }
        public string codEspe { get; set; }
        public string espe{get;set;}
        public string edit { get; set; }
        public string criterioBusqueda { get; set; }
        
    }
}