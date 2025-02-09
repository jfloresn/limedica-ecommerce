using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Xmarket.Models.Test
{
    public class SystemInfoModel
    {
        public string ProcessorName { get; set; }
        public string ProcessorFullName { get; set; }
        public int LogicalCores { get; set; }
        public int PhysicalCores { get; set; }
        public int Threads { get; set; }
        public int SpeedMHz { get; set; }
        public string Socket { get; set; }
        public float CpuUsage { get; set; }
        public string ErrorMessage { get; set; }
        public int usuariosConectador { get; set; }

    }
}