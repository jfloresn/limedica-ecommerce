
using CommandContracts.Common;
using System;
using System.Collections.Generic;

namespace CommandContracts.Xmarket.Carrito.Output
{
    public class RegistrarClienteSinSesionOutput : CommandResult
    {
        public Int32? Estado { get; set; }
        public string Mensaje { get; set; }

        public long idUsuarioLibre { get; set; }
    
        
    }
}