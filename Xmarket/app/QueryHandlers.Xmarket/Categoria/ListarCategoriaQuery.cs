
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Xmarket.Categoria;
using QueryContracts.Xmarket.Categoria.Parameters;
using QueryContracts.Xmarket.Categoria.Result;
using QueryContracts.Xmarket.General.Parameters;
using QueryContracts.Xmarket.General.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.Xmarket.Categoria
{
    public class ListarCategoriaQuery : IQueryHandler<ListarCategoriaParameter>
    {
        public QueryResult Handle(ListarCategoriaParameter parameters)
        {

            var result = new ListarCategoriaResult();


            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                result.Hits = connection.Query<CategoriaDTO>(
                                    "dbo.Web_xmarket_categoria_listar",
                                    null,
                                    commandType: CommandType.StoredProcedure );
                return result;
            }



        }
    }
}
