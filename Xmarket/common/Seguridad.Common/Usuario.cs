using System;
using System.Collections.Generic;
using System.Linq;


namespace Seguridad.Common
{
    public class Usuario
    {
      
        public Usuario()
        { 
        
        }
      

        public int Idusuario { get; set; }
        public string CodigoUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string EmailUsuario { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool IsApproved { get; set; }
        public bool IsLockedout { get; set; }
        public bool IsOnline { get; set; }
        public DateTime LastActivityDate { get; set; }
        public DateTime LastLockedOutDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public int IdUsuarioPwd { get; set; }
        public string RecordatorioPwd { get; set; }

     

    }
}
