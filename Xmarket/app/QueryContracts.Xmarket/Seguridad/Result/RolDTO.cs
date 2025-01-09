
using QueryContracts.Common;
using System.Collections.Generic;
namespace QueryContracts.Xmarket.Seguridad.Result
{
    public class RolDTO
    {

        public int? rol_int_id { get; set; }
        public string rol_str_descrip { get; set; }
        public string rol_str_alias { get; set; }
        public string rol_str_usuario { get; set; }
        public string rol_str_dashboard { get; set; }
        public bool rol_bit_publico { get; set; }
        public bool rol_bit_activo { get; set; }


    }
}
