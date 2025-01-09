using QueryContracts.Xmarket.Coleccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Xmarket.Models.Coleccion
{
    public class ColeccionModel
    {
        public IEnumerable<ColeccionDTO> colecciones { get; set; }

    }
}