using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryContracts.Xmarket.Cliente
{
  public  class ClienteDireccionDTO
    {
        public ClienteDireccionDTO() {
            this.seleccionado =false;
        }
        public int idDireccion { get; set; }
        public int idUsuario { get; set; }
        public string direccion { get; set; }
        public string ubigeoDistrito { get; set; }
        public string distrito { get; set; }
        public string ubigeoProvincia { get; set; }
        public string provincia { get; set; }
        public string ubigeoDepartamento { get; set; }
        public string departamento { get; set; }
        public string idPais { get; set; }
        public string pais { get; set; }
        public bool estado { get; set; }
        public string tipoDireccion { get; set; }

        public string nombreContacto { get; set; }
        public string celularContacto { get; set; }

        public bool seleccionado { get; set; }


    }
}

