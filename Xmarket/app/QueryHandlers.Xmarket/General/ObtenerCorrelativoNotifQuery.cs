
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Xmarket.General.Parameters;
using QueryContracts.Xmarket.General.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.Xmarket.General
{
    public class ObtenerCorrelativoNotifQuery : IQueryHandler<ObtenerCorrelativoNotifParameter>
    {
        public QueryResult Handle(ObtenerCorrelativoNotifParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
               var result = connection.Query<ObtenerCorrelativoNotifResult>(
                                    "dbo.usp_web_sel_corrlt_notifi_gen",
                                    null,
                                    commandType: CommandType.StoredProcedure ).FirstOrDefault();
                return result;
            }

        }
    }
}
