
using CommandContracts.Common;
using System;
using System.Collections.Generic;

namespace CommandContracts.Xmarket.Cliente.Output
{
    public class RegistrarDireccionOutput : CommandResult
    {
        public Int32? Estado { get; set; }
        public string Mensaje { get; set; }
        public int IdUsuarioDireccion { get; set; }

    
        
    }
}