namespace Infraestructure.Common.Validation
{
    using System;

    using FluentValidation;

    using StructureMap;

    public class IocValidatorFactory : ValidatorFactoryBase
    {

        private readonly IContainer _container;

        public IocValidatorFactory(IContainer container)
        {
            _container = container;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            return _container.TryGetInstance(validatorType) as IValidator;
        }
    }
}
