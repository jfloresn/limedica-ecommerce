﻿

using QueryContracts.Common;
using System.Collections.Generic;
namespace QueryContracts.Xmarket.Seguridad.Result
{
    public class ListarRolesAsignablesResult : EliminarPaginaResult
    {
        public IEnumerable<ListarRolesAsignablesDto> Hits { get; set; }
        public int totalregistros { get; set; }
        
    }

    public class ListarRolesAsignablesDto
    {
        public int rol_int_id { get; set; }
        public string rol_str_alias { get; set; }
        public string rol_str_descrip { get; set; }
    }
}
