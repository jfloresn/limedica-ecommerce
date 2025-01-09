using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Xmarket.Entidad
{
    public class UsuarioDireccionDTO
    {
        public int idUsuarioDireccion { get; set; }
        public int idUsuario { get; set; }
        public string direccion { get; set; }
        public string ubigeoDistrito { get; set; }
        public string ubigeoProvincia { get; set; }
        public string ubigeoDepartamento { get; set; }
        public string idPais { get; set; }
   
        public string tipoDireccion { get; set; }
        public string nombreContacto { get; set; }
        public string celularContacto { get; set; }


    }
}