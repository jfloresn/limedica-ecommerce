using QueryContracts.Common;
using QueryContracts.Xmarket.Carrito;
using System;
using System.Collections.Generic;
using System.Data;

namespace QueryContracts.Xmarket.Coleccion.Parameters
{
    public class ColeccionHomeParameter : QueryParameter
    {

        public int opcionFiltro { get; set; }
        public int? idEspecialdiad { get; set; }
        public int? idEditorial { get; set; }
      
    }
}