
using CommandContracts.Common;
using System;

namespace CommandContracts.Xmarket.General.Output
{
    public class CuentaCrearModificarOutput : CommandResult
    {
        public Int32 Estado { get; set; }
        public string Mensaje { get; set; }

        public int CodigoCliente { get; set; }
    }
}