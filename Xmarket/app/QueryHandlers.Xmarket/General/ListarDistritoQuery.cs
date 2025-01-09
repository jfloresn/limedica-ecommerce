
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
    public class ListarDistritoQuery : IQueryHandler<ListarDistritoParameter>
    {
        public QueryResult Handle(ListarDistritoParameter parameters)
        {
            var resultDepartamentos = new ListarDistritoResult();

            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("Departamento", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.departamento);
                parametros.Add("Provincia", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.provincia);

                resultDepartamentos.Hits = connection.Query<DistritoDTO>(
                                    "ecommerce.sp_listar_distrito_por_provincia_departamento",
                                    parametros,
                                    commandType: CommandType.StoredProcedure );

                return resultDepartamentos;
            }
        }
    }
}
