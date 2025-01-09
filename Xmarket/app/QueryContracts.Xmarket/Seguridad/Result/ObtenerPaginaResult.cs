﻿

using QueryContracts.Common;
namespace QueryContracts.Xmarket.Seguridad.Result
{
    public class ObtenerPaginaResult : QueryResult
    {
        public int pag_int_id { get; set; }
        public string pag_str_codmenu { get; set; }
        public string pag_str_codmenu_padre { get; set; }
        public string pag_str_nombre { get; set; }
        public int pag_int_secuencia { get; set; }
        public string pag_str_controller { get; set; }
        public string pag_str_action { get; set; }
        public string pag_str_tipomenu { get; set; }
        public string pag_str_attributes { get; set; }
        public int cantidad { get; set; }
        public bool pag_bit_activo { get; set; }
        public string pag_str_descrip { get; set; }
        public string pag_str_url { get; set; }
        public bool pag_bit_externo { get; set; }

    }
}
