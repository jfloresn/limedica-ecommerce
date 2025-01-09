
using QueryContracts.Common;
using System;
using System.Collections.Generic;

namespace QueryContracts.Xmarket.General.Result
{

    public class DashboardUltimoResult : QueryResult
    {
        public DashboardUltimoDTO Hit { get; set; }

    }


    public class DashboardUltimoDTO
    {
        public Int32 CodigoDashboardUltimo { get; set; }
        public Int32 CodigoUsuario { get; set; }
        public Int32 CodigoDashboard { get; set; }
        public string Dashboard { get; set; }

    }

}
