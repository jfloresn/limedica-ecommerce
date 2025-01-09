
using QueryContracts.Common;
namespace QueryContracts.Xmarket.Seguridad.Parameters
{
    public class ListarRolesAsignablesParameter : QueryParameter
    {
        public string usr_str_red { get; set; }
        public string sis_str_siglas { get; set; }
        public string rol_str_alias { get; set; }
        public int int_rol_sin_asignar { get; set; }

        public int prm_reginicio { get; set; }
        public int prm_regfin { get; set; }




    }
}
