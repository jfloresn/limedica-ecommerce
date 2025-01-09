
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Xmarket.General.Parameters;
using QueryContracts.Xmarket.General.Result;
using QueryContracts.Xmarket.Producto;
using QueryContracts.Xmarket.Producto.Parameters;
using QueryContracts.Xmarket.Producto.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.Xmarket.Producto
{
    public class ListarProductoPorMarcaQuery : IQueryHandler<ListarProductoPorMarcaParameter>
    {
        public QueryResult Handle(ListarProductoPorMarcaParameter parameters)
        {

            var result = new ListarProductoPorMarcaResult();


            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("DistribuidorType", dbType: DbType.Object, direction: ParameterDirection.Input, value: parameters.dtDistribuidor);
                parametros.Add("IdMarca", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.IdMarca);
                parametros.Add("prm_reginicio", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.RegistroInicio);
                parametros.Add("prm_regfin", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.RegistroFin);
                parametros.Add("prm_regtotal", dbType: DbType.Int32, direction: ParameterDirection.Output);

                
                result.Hits = connection.Query<ProductoDTO>(
                                    "Web_xmarket_producto_marca_listar",
                                    parametros,
                                    commandType: CommandType.StoredProcedure);


                result.TotalRegistro = parametros.Get<Int32>("prm_regtotal");

                return result;


            }


        }
    }
}
