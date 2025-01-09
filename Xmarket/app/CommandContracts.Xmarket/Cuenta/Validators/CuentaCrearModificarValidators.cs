using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;

namespace CommandContracts.Xmarket.General.Validators
{
    public class CuentaCrearModificarValidators : AbstractValidator<CuentaCrearModificarCommand>
    {
        public CuentaCrearModificarValidators()
        {
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("Se requiere el nombre");
            RuleFor(x => x.ApellidoMaterno).NotEmpty().WithMessage("Se requiere el apellido materno");
            RuleFor(x => x.ApellidoPaterno).NotEmpty().WithMessage("Se requiere el apellido paterno");

            RuleFor(x => x.Password).NotNull().WithMessage("Se requiere la clave");
            RuleFor(x => x.PasswordConfirmar).NotNull().WithMessage("Se requiere confirmar la clave");

            RuleFor(x => x.Correo).EmailAddress()

                .WithMessage("Se requiere un correo válido").NotEmpty().WithMessage("Se requiere el correo");

   

        }
    }
}
