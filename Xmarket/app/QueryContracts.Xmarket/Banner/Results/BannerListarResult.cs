using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using QueryContracts.Common;

namespace QueryContracts.Xmarket.Banner.Results
{



    public class BannerListarResult : QueryResult
    {


        public IEnumerable<BannerDTO> Hits { get; set; }




    }


}
