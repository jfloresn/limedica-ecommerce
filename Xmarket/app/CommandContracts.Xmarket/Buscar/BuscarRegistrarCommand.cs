using CommandContracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CommandContracts.Xmarket.Buscar
{
    public class BuscarRegistrarCommand : Command
    {
       
        

        public long idSesion { get; set; }


        public string idSesionPublic { get; set; }

        public string texto { get; set; }



    }
}