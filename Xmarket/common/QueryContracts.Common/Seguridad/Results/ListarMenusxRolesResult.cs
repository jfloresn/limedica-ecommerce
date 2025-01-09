

using System.Collections.Generic;
namespace QueryContracts.Common.Seguridad.Results
{
    public class ListarMenusxRolesResult : QueryResult
    {
        public IEnumerable<MenusxRolesDTO> Hits { get; set; }
    }


}
