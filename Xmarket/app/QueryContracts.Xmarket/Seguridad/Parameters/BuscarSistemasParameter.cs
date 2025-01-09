using QueryContracts.Common;
namespace QueryContracts.Xmarket.Seguridad.Parameters
{
    public class BuscarSistemasParameter : QueryParameter
    {
        public string nombre { get; set; }
        public string alias { get; set; }
        public System.Int32 registro_inicio { get; set; }
        public System.Int32 registro_fin { get; set; }

    }
}
