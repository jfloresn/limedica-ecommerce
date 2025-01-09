
using QueryContracts.Common;
using System;
using System.Collections.Generic;

namespace QueryContracts.Xmarket.General.Result
{

    public class AreaGeneralResult :QueryResult
    {
        public IEnumerable< AreaGeneralDTO > Hits { get; set; }
     
    }


}
