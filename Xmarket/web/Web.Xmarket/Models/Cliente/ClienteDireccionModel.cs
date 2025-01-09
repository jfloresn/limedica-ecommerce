using CommandContracts.Xmarket.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Xmarket.Models.Cliente
{
    public class ClienteDireccionModel
    {
        public SelectList departamentos { get; set; }
        public SelectList provincias { get; set; }
        public SelectList tipoDireccion { get; set; }
        public SelectList distritos { get; set; }
        public string mensaje { get; set; }
        public int estadoMensaje { get; set; }
        public RegistrarDireccionCommand direccionCommand { get; set; }
    }
}