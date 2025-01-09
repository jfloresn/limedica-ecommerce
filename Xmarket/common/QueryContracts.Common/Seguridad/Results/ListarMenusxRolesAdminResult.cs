

using System.Collections.Generic;
namespace QueryContracts.Common.Seguridad.Results
{
    public class ListarMenusxRolesAdminResult : QueryResult
    {
        public IEnumerable<MenusxRolesDTO> Hits { get; set; }
    }


}
