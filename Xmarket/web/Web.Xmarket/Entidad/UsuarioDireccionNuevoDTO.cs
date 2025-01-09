using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Xmarket.Entidad
{
    public class UsuarioDireccionNuevoDTO
    {
        public QueryContracts.Xmarket.Cliente.ClienteDireccionDTO direccion { get; set; }

        public int correlativo { get; set; }

    }
}