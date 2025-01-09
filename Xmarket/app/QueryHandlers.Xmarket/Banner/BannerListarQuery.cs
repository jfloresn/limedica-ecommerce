
namespace QueryHandlers.Xmarket.Banner
{
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using Data.Common;
    using QueryContracts.Common;
    using QueryContracts.Xmarket.Banner.Parameters;
    using QueryContracts.Xmarket.Banner.Results;
    using QueryHandlers.Common;
    using QueryHandlers.Common.Dapper;
    using System;
    using QueryContracts.Xmarket.Banner;
    public class BannerListarQuery : IQueryHandler<BannerListarParameter>
    {
        public QueryResult Handle(BannerListarParameter parameters)
        {
            BannerListarResult result = new BannerListarResult();
            result.EstadoResult = 0;
            result.MensajeResult = "";

            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {   
                result.Hits= connection.Query<BannerDTO>
                  (
                      "[ecommerce].[usp_banner_listar]",
                       null,
                       commandType: CommandType.StoredProcedure
                  );
                return result;
            }
        }
    }
}
