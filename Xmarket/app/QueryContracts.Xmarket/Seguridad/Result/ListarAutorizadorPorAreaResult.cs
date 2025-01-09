
using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.Xmarket.Seguridad.Result
{
    public class ListarAutorizadorPorAreaResult : QueryResult
    {
        public IEnumerable<AutoriadorAreaDTO> Hits { get; set; }
        public int total_registro { get; set; }
    }

    public class AutoriadorAreaDTO
    {

        public int CO_USUARIO { get; set; }
        public string usr_str_red { get; set; }
        public string USUARIO { get; set; }
        public string usr_str_nombre { get; set; }
        public string usr_str_apellidos { get; set; }

    }
}
