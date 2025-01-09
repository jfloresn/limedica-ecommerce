
using BaseCommon.Common;
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Xmarket.Cliente;
using QueryContracts.Xmarket.Cliente.Parameters;
using QueryContracts.Xmarket.Cliente.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.Xmarket.Categoria
{
    public class ClienteDireccionLeerQuery : IQueryHandler<ClienteDireccionLeerParameter>
    {

        public QueryResult Handle(ClienteDireccionLeerParameter parameters)
        {
            var result = new ClienteDireccionLeerResult();

            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {

                var parametros = new DynamicParameters();
                parametros.Add("IdClienteDireccion", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idClienteDireccion);
                result.direccion = connection.Query<ClienteDireccionDTO>(
                                    "ecommerce.cliente_direccion_leer",
                                    parametros,
                                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

    
            return result;

        }

    }
}
