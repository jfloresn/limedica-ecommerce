
using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.Xmarket.Seguridad.Result
{
    public class ListarUsuariosPorAreaResult : QueryResult
    {
        public IEnumerable<ListarUsuariosAreaDto> Hits { get; set; }
        public int total_registro { get; set; }
    }

    public class ListarUsuariosAreaDto
    {
        
         public int usr_int_id { get; set; }
         public string usr_str_red { get; set; }
         public string usr_str_nombre  { get; set; }
         public string usr_str_apellidos { get; set; }
         public string usr_str_email { get; set; }

         public int? usr_int_numintentos { get; set; }

         public string NU_CELULAR { get; set; }


        
    }
}
