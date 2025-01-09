using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommandContracts.Xmarket.Cuenta;
using FluentValidation;

namespace CommandContracts.Xmarket.General.Validators
{
    public class ValidarUsuarioValidators : AbstractValidator<ValidarUsuarioCommand>
    {
        public ValidarUsuarioValidators()
        {
            RuleFor(x => x.Password).NotNull().WithMessage("Se requiere el password");

            RuleFor(x => x.Usuario).EmailAddress()
                .WithMessage("Se requiere un correo valido").NotEmpty().WithMessage("Se requeire el correo");
        }
    }
}
