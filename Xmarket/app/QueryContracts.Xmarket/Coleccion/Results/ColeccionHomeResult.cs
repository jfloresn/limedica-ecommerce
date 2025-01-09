
using QueryContracts.Common;

using System;
using System.Collections.Generic;

namespace QueryContracts.Xmarket.Coleccion.Result
{

    public class ColeccionHomeResult : QueryResult
    {
        public IEnumerable<ColeccionDTO> Hit { get; set; }

      

    }


}
