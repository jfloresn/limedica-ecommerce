
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Xmarket.Especialidad;
using QueryContracts.Xmarket.Especialidad.Parameters;
using QueryContracts.Xmarket.Especialidad.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.Xmarket.Especialidad
{
    public class LeerEspecialidadQuery : IQueryHandler<LeerEspecialidadParameter>
    {
        public QueryResult Handle(LeerEspecialidadParameter parameters)
        {

            var result = new LeerEspecialidadResult();


            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();

                parametros.Add("idEspecialidad", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idEspecialidad);
              
                result.Hit = connection.Query<EspecialidadDTO>(
                                    "[ecommerce].[usp_especialidad_leer]",
                                    parametros,
                                    commandType: CommandType.StoredProcedure ).FirstOrDefault();
                return result;
            }



        }
    }
}
