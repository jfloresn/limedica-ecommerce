
using QueryContracts.Common;

using System;
using System.Collections.Generic;

namespace QueryContracts.Xmarket.Book.Result
{

    public class BookSearchResult : QueryResult
    {
        public IEnumerable<BookFiltroDTO> Hit { get; set; }

    
    }


}
