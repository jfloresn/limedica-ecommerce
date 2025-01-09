
using QueryContracts.Common;
namespace QueryContracts.Xmarket.Seguridad.Parameters
{
    public class ListarRolesDashBoardParameter : QueryParameter
    {
        public string rol_str_alias { get; set; }
        public System.Int32 registro_inicio { get; set; }
        public System.Int32 registro_fin { get; set; }
    }
}
