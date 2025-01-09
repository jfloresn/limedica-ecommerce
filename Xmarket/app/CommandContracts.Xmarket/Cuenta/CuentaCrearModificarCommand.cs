using CommandContracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CommandContracts.Xmarket.General
{
    public class CuentaCrearModificarCommand : Command
    {
    
    
        public string Nombre { get; set; }
        [DisplayName("Apellido Paterno")]
        public string ApellidoPaterno { get; set; }
        [DisplayName("Apellido Materno")]
        public string ApellidoMaterno { get; set; }

        [DisplayName("Correo")]
        public string Correo { get; set; }

        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("Confirmar Password")]
        public string PasswordConfirmar { get; set; }

 

    }
}