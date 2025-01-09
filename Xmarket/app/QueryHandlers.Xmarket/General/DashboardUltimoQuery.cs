
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Xmarket.General.Parameters;
using QueryContracts.Xmarket.General.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.Xmarket.General
{
    public class DashboardUltimoQuery : IQueryHandler<DashboardUltimoParameter>
    {
        public QueryResult Handle(DashboardUltimoParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("CodigoUsuario", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.CodigoUsuario);


                var result = new DashboardUltimoResult();
                result.Hit = connection.Query<DashboardUltimoDTO>(
                                    "dbo.usp_web_dashboard_usuario_ultimo",
                                    parametros,
                                    commandType: CommandType.StoredProcedure).FirstOrDefault();


                return result;
            }
        }
    }
}