
using QueryContracts.Common;
namespace QueryContracts.Xmarket.Seguridad.Parameters
{
    public class EncriptarPasswordParameter : QueryParameter
    {
        public int usr_int_id { get; set; }
        public string usr_str_password { get; set; }
    }
}
