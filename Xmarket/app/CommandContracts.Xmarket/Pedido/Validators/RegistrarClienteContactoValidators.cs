using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommandContracts.Xmarket.Pedido.Validators
{
  public  class RegistrarClienteContactoValidators : AbstractValidator<RegistrarClienteContactoCommand>
    {
        public RegistrarClienteContactoValidators()
        {
            RuleFor(x => x.contactoNumeroDocumento).NotNull().WithMessage("Se requiere numero de documento");
            RuleFor(x => x.contactoTipoDocumento).NotNull().WithMessage("Se requiere tipo de documento");
            RuleFor(x => x.contactoTipoDocumentoPedido).NotNull().WithMessage("Se requiere tipo de documento de pedido");
    
        }
    }
}