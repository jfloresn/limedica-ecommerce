using CommandContracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CommandContracts.Xmarket.Sesion
{
    public class CrearSesionCommand : Command
    {
       

        public string codigoSesionPublico { get; set; }
        public DateTime FechaInicio { get; set; }

        public string IpPublica { get; set; }

        public string idSessionWeb { get; set; }
        public string requestVerificationToken { get; set; }

    }
}