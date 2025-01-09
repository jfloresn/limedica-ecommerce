
using BaseCommon.Common;
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Xmarket.Cliente;
using QueryContracts.Xmarket.Cliente.Parameters;
using QueryContracts.Xmarket.Cliente.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.Xmarket.Categoria
{
    public class ClienteLeerCorreoQuery : IQueryHandler<ClienteLeerCorreoParameter>
    {

        public QueryResult Handle(ClienteLeerCorreoParameter parameters)
        {
            var result = new ClienteLeerCorreoResult();

            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {

                var parametros = new DynamicParameters();
                parametros.Add("Correo", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.Correo);
                result.Hit = connection.Query<ClienteDTO>(
                                    "ecommerce.leer_usuario_por_correo",
                                    parametros,
                                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

            //if (result.Hit != null)
            //{
               

            //    using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            //    {
            //        var parametros = new DynamicParameters();
            //        parametros.Add("IdCliente", dbType: DbType.Int32, direction: ParameterDirection.Input, value: result.Hit.IdUsuario);
            //        result.Hit.ClienteDirecciones = connection.Query<ClienteDireccion>(
            //                            "Web_xmarket_clientes_direccion",
            //                            parametros,
            //                            commandType: CommandType.StoredProcedure);
            //    }
            //}

            return result;

        }

    }
}
