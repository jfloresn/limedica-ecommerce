
using QueryContracts.Common;

using System;
using System.Collections.Generic;

namespace QueryContracts.Xmarket.Book.Result
{

    public class BookListarResult : QueryResult
    {
        public IEnumerable<BookFiltroDTO> Hit { get; set; }

    
    }


}
