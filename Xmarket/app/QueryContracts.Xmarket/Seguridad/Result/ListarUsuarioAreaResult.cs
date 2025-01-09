
using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.Xmarket.Seguridad.Result
{
    public class ListarUsuarioAreaResult : EliminarPaginaResult
    {
        public IEnumerable<UsuarioAreaDTO> Hits { get; set; }
    }

    public class UsuarioAreaDTO
    {
        
         public int CO_USUARIO { get; set; }
         public string USUARIO { get; set; }
         public string usr_str_red { get; set; }
         public string usr_str_nombre  { get; set; }
         public string usr_str_apellidos { get; set; }
         

        
    }
}
