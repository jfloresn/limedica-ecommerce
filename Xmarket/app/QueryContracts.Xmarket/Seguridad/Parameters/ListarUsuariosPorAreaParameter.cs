

using QueryContracts.Common;
namespace QueryContracts.Xmarket.Seguridad.Parameters
{
    public class ListarUsuariosPorAreaParameter : QueryParameter
    {
        public int CO_AREA { get; set; }
        public int prm_reginicio { get; set; }
        public int prm_regfin { get; set; }
    


    }
}
