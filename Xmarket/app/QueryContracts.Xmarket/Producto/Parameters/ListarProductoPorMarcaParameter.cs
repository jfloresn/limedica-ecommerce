﻿using QueryContracts.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace QueryContracts.Xmarket.Producto.Parameters
{
    public class ListarProductoPorMarcaParameter : QueryParameter
    {

        public DataTable dtDistribuidor { get; set; }

        
        public int RegistroInicio { get; set; }
        public int RegistroFin { get; set; }
        public int IdMarca { get; set; }
        
    }
}