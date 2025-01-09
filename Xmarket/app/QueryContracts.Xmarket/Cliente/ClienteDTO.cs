using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryContracts.Xmarket.Cliente
{
    public class ClienteDTO
    {

        public int IdUsuario { get; set; }

        public string nombre { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string correo { get; set; }

        public bool cambiarPassword { get; set; }
        public bool bloqueado { get; set; }


        public DateTime? fechaNacimiento { get; set; }
        public DateTime? ultimaFechaBloqueo { get; set; }
        public DateTime? ultimaFechaLogin { get; set; }
        public DateTime? fechaVencimientoPassword { get; set; }
        public int numeroIntentos { get; set; }
        public string celular { get; set; }
        public string sexo { get; set; }

        public IEnumerable<ClienteDireccionDTO> ClienteDirecciones { get; set; }


    }
}
