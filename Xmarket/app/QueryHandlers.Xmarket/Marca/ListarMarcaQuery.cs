
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Xmarket.General.Parameters;
using QueryContracts.Xmarket.General.Result;
using QueryContracts.Xmarket.Marca;
using QueryContracts.Xmarket.Marca.Parameters;
using QueryContracts.Xmarket.Marca.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.Xmarket.Marca
{
    public class ListarMarcaQuery : IQueryHandler<ListarMarcaParameter>
    {
        public QueryResult Handle(ListarMarcaParameter parameters)
        {

            var result = new ListarMarcaResult();


            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                result.Hits = connection.Query<MarcaDTO>(
                                    "dbo.Web_xmarket_marcas_listar",
                                    null,
                                    commandType: CommandType.StoredProcedure );
                return result;
            }



        }
    }
}
