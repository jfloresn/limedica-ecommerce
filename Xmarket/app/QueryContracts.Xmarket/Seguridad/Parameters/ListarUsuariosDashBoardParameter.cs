

using QueryContracts.Common;
namespace QueryContracts.Xmarket.Seguridad.Parameters
{
    public class ListarUsuariosDashBoardParameter : QueryParameter
    {
        public int? rol_int_id { get; set; }
        public string usr_str_nombre_apellido { get; set; }
        public string usr_str_red { get; set; }

        public System.Int32 registro_inicio { get; set; }
        public System.Int32 registro_fin { get; set; }

        
    }
}
