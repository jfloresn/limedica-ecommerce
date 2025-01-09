
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
    public class ListarEspecialidadQuery : IQueryHandler<ListarEspecialidadParameter>
    {
        public QueryResult Handle(ListarEspecialidadParameter parameters)
        {

            var result = new ListarEspecialidadResult();


            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                result.Hits = connection.Query<EspecialidadDTO>(
                                    "[ecommerce].[sp_especialidad_listar]",
                                    null,
                                    commandType: CommandType.StoredProcedure );
                return result;
            }



        }
    }
}
