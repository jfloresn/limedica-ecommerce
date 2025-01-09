using CommandContracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CommandContracts.Xmarket.Cliente
{
    public class RegistrarDireccionCommand : Command
    {

        public int idUsuario { get; set; }
        public string direccion { get; set; }
        public string ubigeoDistrito { get; set; }
        public string ubigeoProvincia { get; set; }
        public string ubigeoDepartamento { get; set; }
        public string idPais { get; set; }
        public int estado { get; set; }
        
        public string tipoDireccion { get; set; }
        public string nombreContacto { get; set; }
        public string celularContacto { get; set; }

    }
}