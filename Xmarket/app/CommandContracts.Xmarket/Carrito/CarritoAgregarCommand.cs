﻿using CommandContracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CommandContracts.Xmarket.Carrito
{
    public class CarritoAgregarCommand : Command
    {
       
       
        public Carrito Carrito { get; set; }
      


    }
}