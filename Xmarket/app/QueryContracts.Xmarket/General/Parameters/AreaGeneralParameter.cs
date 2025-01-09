using QueryContracts.Common;
using System;
using System.Collections.Generic;

namespace QueryContracts.Xmarket.General.Parameters
{
    public class AreaGeneralParameter : QueryParameter
    {
        public int? prm_codigo_padre { get; set; }
        public int? prm_codigo_empresa { get; set; }
       
    }
}
