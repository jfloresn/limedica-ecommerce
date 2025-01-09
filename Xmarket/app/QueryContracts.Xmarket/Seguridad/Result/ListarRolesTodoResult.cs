
using QueryContracts.Common;
using System.Collections.Generic;
namespace QueryContracts.Xmarket.Seguridad.Result
{
    public class ListarRolesTodoResult : QueryResult
    {
        public IEnumerable<RolDTO> Hits { get; set; }
        public  System.Int32 totalregistros { get; set; }
        
    }


}
