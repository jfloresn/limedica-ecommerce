
using QueryContracts.Common;
namespace QueryContracts.Xmarket.Seguridad.Parameters
{
    public class BuscarPaginasParameter : QueryParameter 
    {
        public string pag_str_codmenu { get; set; }
        public string pag_str_nombre { get; set; }
        public string CodigoMenuPadre { get; set; }
        
        public System.Int32 registro_inicio { get; set; }
        public System.Int32 registro_fin { get; set; }

     
    }
}
