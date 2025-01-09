using CommandContracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandContracts.Xmarket.Cuenta
{
  public  class ValidarUsuarioCommand : Command
    {
    
        public string Usuario { get; set; }

     
        public string Password { get; set; }
    }
}
