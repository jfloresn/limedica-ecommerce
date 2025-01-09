
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Xmarket.Seguridad.Parameters;
using QueryContracts.Xmarket.Seguridad.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace QueryHandlers.Xmarket.Seguridad
{
    public class ObtenerRolQuery : IQueryHandler<ObtenerRolParameter>
    {
        public QueryResult Handle(ObtenerRolParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("CO_ROL", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.CO_ROL);

                var result = new ObtenerRolResult();
                result.Hit = connection.Query<RolDTO>(
                                    "seguridad.usp_app_sel_rol",
                                    parametros,
                                    commandType: CommandType.StoredProcedure).FirstOrDefault();
                return result;
            }
        }
    }
}