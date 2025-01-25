using QueryContracts.Common;
using QueryContracts.Xmarket.Carrito;
using System;
using System.Collections.Generic;
using System.Data;

namespace QueryContracts.Xmarket.Book.Parameters
{
    public class BookSearchParameter : QueryParameter
    {

        public BookSearchParameter() {

            this.criteriosBusqueda = new List<CriterioBusqueda>();
        }
       
        public IEnumerable<CriterioBusqueda> criteriosBusqueda { get; set; }
        
    }
}