using QueryContracts.Common;
using QueryContracts.Xmarket.Carrito;
using System;
using System.Collections.Generic;
using System.Data;

namespace QueryContracts.Xmarket.Book.Parameters
{
    public class BookFiltroParameter : QueryParameter
    {

        public int opcionFiltro { get; set; }
        public int? idEspecialdiad { get; set; }
        public int? idEditorial { get; set; }
      
    }
}