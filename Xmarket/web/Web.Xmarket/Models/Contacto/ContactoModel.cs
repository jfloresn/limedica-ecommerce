using CommandContracts.Xmarket.Contactenos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Xmarket.Models.Contacto
{
    public class ContactoModel
    {
        public SelectList departamentos { get; set; }

        public ContactenosRegistrarCommand contactenosRegistrarCommand { get; set; }
    }
}