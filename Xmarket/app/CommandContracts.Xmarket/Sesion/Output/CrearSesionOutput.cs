
using CommandContracts.Common;
using System;

namespace CommandContracts.Xmarket.Sesion.Output
{
    public class CrearSesionOutput : CommandResult
    {
        public Int32 Estado { get; set; }
        public string Mensaje { get; set; }

        public int CodigoSesion { get; set; }
    }
}