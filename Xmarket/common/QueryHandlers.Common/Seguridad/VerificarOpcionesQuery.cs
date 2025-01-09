using Data.Common;
using QueryContracts.Common.Seguridad.Parameters;
using QueryContracts.Common.Seguridad.Results;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.Common
{
    public class VerificarOpcionesQuery : IQueryHandler<VerificarOpcionesParameter>
    {
        public QueryContracts.Common.QueryResult Handle(VerificarOpcionesParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("CodigoRol", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.CodigoRol);
                parametros.Add("CodigoOpcion", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.CodigoOpcion);

                var resultado = new VerificarOpcionesResult();
                resultado = connection.Query<VerificarOpcionesResult>
                        (
                            "seguridad.usp_web_sel_verificar_opcion_por_rol",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();
                return resultado;
            }
        }
    }
}