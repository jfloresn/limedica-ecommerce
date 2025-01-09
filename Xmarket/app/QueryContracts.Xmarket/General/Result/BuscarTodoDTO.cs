
using QueryContracts.Common;
using System;
using System.Collections.Generic;

namespace QueryContracts.Xmarket.General.Result
{

    public class BuscarTodoDTO
    {
        public Int32 codigo { get; set; }
        public string numero { get; set; }
        public string descripcion { get; set; }
        public string tipo { get; set; }
        public string ruta { get; set; }
        public Int32 id { get; set; }

        public string Atributo { get; set; }
        public string Controller { get; set; }
        public string Actions { get; set; }

    }


}
