using QueryContracts.Common;
using QueryContracts.Xmarket.Carrito;
using System;
using System.Collections.Generic;
using System.Data;

namespace QueryContracts.Xmarket.Book.Parameters
{
    public class BookListarParameter : QueryParameter
    {

        public int opcionFiltro { get; set; }
        public int? idEspecialdiad { get; set; }
        public int? idEditorial { get; set; }
        public int? idColeccion { get; set; }
        public string criterioBusqueda { get; set; }
        
    }
}