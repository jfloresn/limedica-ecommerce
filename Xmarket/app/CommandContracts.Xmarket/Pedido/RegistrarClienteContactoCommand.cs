using CommandContracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CommandContracts.Xmarket.Pedido
{
    public class RegistrarClienteContactoCommand : Command
    {




        [DisplayName("Tipo Documento Pedido")]
        public string contactoTipoDocumentoPedido { get; set; }

        [DisplayName("Tipo Documento")]
        public string contactoTipoDocumento { get; set; }

        [DisplayName("Número de Documento")]
        public string contactoNumeroDocumento { get; set; }

        [DisplayName("La información de facturación y de entrega es la misma")]
        public bool facturacionInfomoIgualEntrega { get; set; }

        public string sesionPublico { get; set; }

        public List<Direccion> direcciones { get; set; }



    }


    public class Direccion { 
    
        public int idDireccion { get; set; }
        public string tipoDireccion { get; set; }
    
    }

}
