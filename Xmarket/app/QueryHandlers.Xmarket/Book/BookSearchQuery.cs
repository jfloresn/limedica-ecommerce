
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
    public class BookSearchQuery : IQueryHandler<BookSearchParameter>
    {
        public QueryResult Handle(BookSearchParameter parameters)
        {
            string store = "[ecommerce].[usp_book_buscar]";

            var result = new BookListarResult();
            DataTable dtCriterio = new DataTable("TY_CRITERIO");
            dtCriterio.Columns.Add("id", typeof(int));
            dtCriterio.Columns.Add("criterio", typeof(string));
            dtCriterio.Columns.Add("type", typeof(string));

            if (parameters.criteriosBusqueda != null)
            {
                foreach (CriterioBusqueda row in parameters.criteriosBusqueda)
                {
                    DataRow drog = dtCriterio.NewRow();
                    drog["id"] = row.id;
                    drog["criterio"] = row.criterio;
                    drog["type"] = row.type;
                    dtCriterio.Rows.Add(drog);
                }
            }


            bool containsIsb = new[] { "IS" }
             .All(tipo => parameters.criteriosBusqueda.Any(obj => obj.type.Equals(tipo)));


            bool onlyTi = parameters.criteriosBusqueda.All(obj => obj.type == "TI");

            bool esInFirstPositionEspe = parameters.criteriosBusqueda.Any() && parameters.criteriosBusqueda.First().type == "ES";
            bool esInFirstPositionAutor = parameters.criteriosBusqueda.Any() && parameters.criteriosBusqueda.First().type == "AU";
            bool esInFirstPositioColeccion = parameters.criteriosBusqueda.Any() && parameters.criteriosBusqueda.First().type == "CO";




            if (containsIsb)
            {
                store = "[ecommerce].[usp_book_buscar_isbn]";
            }
            else if (esInFirstPositionAutor)
            {
                store = "[ecommerce].[usp_book_buscar_autor]";
            }
            else if (esInFirstPositionEspe)
            {
                store = "[ecommerce].[usp_book_buscar_especialidad]";
            }
            else if (onlyTi)
            {
                store = "[ecommerce].[usp_book_buscar_title]";
            }
            else if (esInFirstPositioColeccion)
            {
                store = "[ecommerce].[usp_book_buscar_coleccion]";
            }


            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();

                parametros.Add("Criterios", dbType: DbType.Object, direction: ParameterDirection.Input, value: dtCriterio);
                parametros.Add("registroInicio", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.registroInicio);
                parametros.Add("registroFin", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.registroFin);
                parametros.Add("cantidadRegistros", dbType: DbType.Int32, direction: ParameterDirection.Output);

                result.Hit = connection.Query<BookFiltroDTO>(
                                    store,
                                    parametros,
                                    commandType: CommandType.StoredProcedure).ToList();

                result.cantidadRegistro = parametros.Get<Int32>("cantidadRegistros");
            }

            return result;
        }
    }
}
