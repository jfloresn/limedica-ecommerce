
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
    public class ListarProvinciaQuery : IQueryHandler<ListarProvinciaParameter>
    {
        public QueryResult Handle(ListarProvinciaParameter parameters)
        {
            var resultDepartamentos = new ListarProvinciaResult();

            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("Departamento", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.departamento);

                resultDepartamentos.Hits = connection.Query<ProvinciaDTO>(
                                    "ecommerce.sp_listar_provincias_por_depatarmento",
                                    parametros,
                                    commandType: CommandType.StoredProcedure );

                return resultDepartamentos;
            }
        }
    }
}
