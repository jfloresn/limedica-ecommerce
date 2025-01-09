using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommandContracts.Xmarket.Pedido.Validators
{
  public  class RegistrarPedidoValidators : AbstractValidator<RegistrarPedidoCommand>
    {
        public RegistrarPedidoValidators()
        {
       
            When(m => m.metodoPago!=null && m.metodoPago.Equals("BAIN"), () => {
                RuleFor(x => x.idBanco).NotEmpty().WithMessage("Se requiere el banco de transferencia");
                RuleFor(x => x.transferenciaMontoDeposito).NotNull().WithMessage("Se requiere el monto de transferencia");
                RuleFor(x => x.transferenciaFechaDeposito).NotNull().WithMessage("Se requiere la fecha de transferencia");

            });

        }
    }
}
