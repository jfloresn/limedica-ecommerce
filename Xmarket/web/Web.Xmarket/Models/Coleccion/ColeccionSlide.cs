using QueryContracts.Xmarket.Coleccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Xmarket.Models.Coleccion
{
    public class ColeccionSlide
    {
        public ColeccionSlide() {

            this.colecciones = new List<ColeccionDTO>();
        }
        public List<ColeccionDTO> colecciones { get; set; }
        public bool esVisible { get; set; }

    }
}