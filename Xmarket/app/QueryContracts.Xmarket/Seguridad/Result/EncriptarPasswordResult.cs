using QueryContracts.Common;
namespace QueryContracts.Xmarket.Seguridad.Result
{
    public class EncriptarPasswordResult : EliminarPaginaResult
    {
        public int filas_afectadas { get; set; }

        public string usr_str_red { get; set; }
        public string usr_str_nombre { get; set; }
        public string usr_str_email { get; set; }
   
    }
}
