
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Xmarket.Book;
using QueryContracts.Xmarket.Book.Parameters;
using QueryContracts.Xmarket.Book.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace QueryHandlers.Xmarket.Carrito
{
    public class BookListarQuery : IQueryHandler<BookListarParameter>
    {
        public QueryResult Handle(BookListarParameter parameters)
        {
            var result = new BookListarResult();
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                
                parametros.Add("opcion", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.opcionFiltro);
                parametros.Add("idEditorial", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idEditorial);
                parametros.Add("idEspecialidad", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idEspecialdiad);
                parametros.Add("idColeccion", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idColeccion);
                parametros.Add("criterioBusqueda", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.criterioBusqueda);
                
                parametros.Add("registroInicio", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.registroInicio);
                parametros.Add("registroFin", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.registroFin);
                parametros.Add("cantidadRegistros", dbType: DbType.Int32, direction: ParameterDirection.Output);


                result.Hit = connection.Query<BookFiltroDTO>(
                                    "[ecommerce].[usp_book_listar_filtro]",
                                    parametros,
                                    commandType: CommandType.StoredProcedure).ToList();

                result.cantidadRegistro = parametros.Get<Int32>("cantidadRegistros");
            }

            

            return result;
        }
    }
}
