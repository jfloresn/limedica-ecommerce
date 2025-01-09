using CommandContracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CommandContracts.Xmarket.Pedido
{
    public class RegistrarClienteSinSesionCommand : Command
    {
        [DisplayName("Nombre")]
        public string entregaNombre { get; set; }

        [DisplayName("Apellidos")]
        public string entregaApellido { get; set; }

        [DisplayName("Dirección")]
        public string entregaDireccion { get; set; }

        [DisplayName("Referencia")]
        public string entregaReferencia { get; set; }

        [DisplayName("Departamento")]
        public string entregaDepartamento { get; set; }

        [DisplayName("Provincia")]
        public string entregaProvincia { get; set; }

        [DisplayName("Distrito")]
        public string entregaDistrito { get; set; }

        [DisplayName("Correo Electronico")]
        public string contactoCorreoElectronico { get; set; }

        [DisplayName("Telefono")]
        public string contactoTelefono { get; set; }

        [DisplayName("Tipo Documento Pedido")]
        public string contactoTipoDocumentoPedido { get; set; }

        [DisplayName("Tipo Documento")]
        public string contactoTipoDocumento { get; set; }

        [DisplayName("Número de Documento")]
        public string contactoNumeroDocumento { get; set; }

        [DisplayName("La información de facturación y de entrega es la misma")]
        public bool facturacionInfomoIgualEntrega { get; set; }

        public string sesionPublico { get; set; }

    }
}
