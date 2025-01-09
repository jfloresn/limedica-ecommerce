
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Xmarket.Cliente;
using QueryContracts.Xmarket.Cliente.Parameters;
using QueryContracts.Xmarket.Cliente.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.Xmarket.Categoria
{
    public class ClienteLeerQuery : IQueryHandler<ClienteLeerParameter>
    {

        public QueryResult Handle(ClienteLeerParameter parameters)
        {
            var result = new ClienteLeerResult();

            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {

                var parametros = new DynamicParameters();
                parametros.Add("IdCliente", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.IdCliente);
                result.Hit = connection.Query<ClienteDTO>(
                                    "ecommerce.leer_usuario_por_id",
                                    parametros,
                                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }


            //using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            //{
            //    var parametros = new DynamicParameters();
            //    parametros.Add("IdCliente", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.IdCliente);
            //    result.Hit.ClienteDirecciones = connection.Query<ClienteDireccion>(
            //                        "Web_xmarket_clientes_direccion",
            //                        parametros,
            //                        commandType: CommandType.StoredProcedure);
            //}

            return result;

        }

    }
}
