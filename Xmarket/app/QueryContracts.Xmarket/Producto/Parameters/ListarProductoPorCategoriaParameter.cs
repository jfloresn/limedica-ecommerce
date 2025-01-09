using QueryContracts.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace QueryContracts.Xmarket.Producto.Parameters
{
    public class ListarProductoPorCategoriaParameter : QueryParameter
    {

        public DataTable dtDistribuidor { get; set; }

        
        public int RegistroInicio { get; set; }
        public int RegistroFin { get; set; }
        public int IdCategoria { get; set; }
        
    }
}
