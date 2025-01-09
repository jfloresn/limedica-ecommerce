
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Xmarket.General;
using QueryContracts.Xmarket.General.Parameters;
using QueryContracts.Xmarket.General.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.Xmarket.General
{
    public class ListarDepartamentosQuery : IQueryHandler<ListarDepartamentosParameter>
    {
        public QueryResult Handle(ListarDepartamentosParameter parameters)
        {
            var resultDepartamentos = new ListarDepartamentosResult();

            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                resultDepartamentos.Hits = connection.Query<DepartamentoDTO>(
                                    "ecommerce.sp_listar_departamentos",
                                    null,
                                    commandType: CommandType.StoredProcedure );

                return resultDepartamentos;
            }
        }
    }
}
