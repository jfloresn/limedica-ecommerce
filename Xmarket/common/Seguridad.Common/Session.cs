using System;
using System.Collections.Generic;
using System.Web;
namespace Seguridad.Common

{

    public class Session
    {
        public Session () 
        {
            autenticado = false;
        }

        public Int32 CodSession { get; set; }
        public string CodSessionPulbico { get; set; }
        public string IpCliente { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        public bool autenticado { get; set; }
        public string idSesionWeb { get; set; }
        public string idVerificacionToken { get; set; }
        public Usuario Usuario { get; set; }
        public long idUsuarioLibre { get; set; }


    }
}
