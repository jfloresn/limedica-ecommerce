﻿
using QueryContracts.Common;
using System;
using System.Collections.Generic;

namespace QueryContracts.Xmarket.Especialidad.Result
{

    public class ListarEspecialidadResult : QueryResult
    {
        public IEnumerable<EspecialidadDTO> Hits { get; set; }
    }


}