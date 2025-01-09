using QueryContracts.Xmarket.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Xmarket.Models.Carrito
{
    public class DireccionCarritoModel
    {


        public string origenVista { get; set; }

        public List<ClienteDireccionDTO> direcciones { get; set; }

    }
}