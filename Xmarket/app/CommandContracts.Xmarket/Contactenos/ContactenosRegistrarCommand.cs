using CommandContracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CommandContracts.Xmarket.Contactenos
{
    public class ContactenosRegistrarCommand : Command
    {
       
       
       public string nombre { get; set; }
       public string apellido { get; set; }
        public string correo { get; set; }
        public string numeroTelefono { get; set; }

        public string idCiudad { get; set; }
        public string asunto { get; set; }
      

        public string Mensaje { get; set; }



    }
}