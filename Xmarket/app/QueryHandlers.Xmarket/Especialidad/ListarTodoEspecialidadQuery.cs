
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
    public class ListarTodoEspecialidadQuery : IQueryHandler<ListarTodoEspecialidadParameter>
    {
        public QueryResult Handle(ListarTodoEspecialidadParameter parameters)
        {

            var result = new ListarTodoEspecialidadResult();


            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                result.Hits = connection.Query<EspecialidadDTO>(
                                    "[ecommerce].[sp_especialidad_listar_todo]",
                                    null,
                                    commandType: CommandType.StoredProcedure );
                return result;
            }



        }
    }
}
