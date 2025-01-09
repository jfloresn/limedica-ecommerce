
using QueryContracts.Common;
using System.Collections.Generic;
namespace QueryContracts.Xmarket.Seguridad.Result
{
    public class ListarRolesResult : QueryResult
    {
        public IEnumerable<RolDTO> Hits { get; set; }
        public  System.Int32 totalregistros { get; set; }
        
    }


}
