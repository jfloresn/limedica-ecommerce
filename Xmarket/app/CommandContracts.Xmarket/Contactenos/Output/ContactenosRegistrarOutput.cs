
using CommandContracts.Common;
using System;
using System.Collections.Generic;

namespace CommandContracts.Xmarket.Contactenos.Output
{
    public class ContactenosRegistrarOutput : CommandResult
    {
        public Int32? Estado { get; set; }
        public string Mensaje { get; set; }
        public int idContacto { get; set; }




    }
}