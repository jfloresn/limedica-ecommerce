

namespace Bootstrapper.Wcf.Common.DependencyResolution
{
    using System;
    using System.Reflection;

    using Bootstrapper.Wcf.Common.DependencyResolution.Registries;
    using CommandHandlers.Common;
    using Data.Common.UnitOfWork;
    using FluentValidation;
    using Infraestructure.Common.Validation;
    using QueryHandlers.Common;
    using StructureMap;

    public class WcfDependencyRegistrar
    {
        private static readonly Lazy<IContainer> _instance = new Lazy<IContainer>(() =>
        {
            var container = new Container(config =>
            {
                // Registrar IValidatorFactory
                config.For<IValidatorFactory>().Use<IocValidatorFactory>();

                // Registrar todos los validadores automáticamente
                config.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.ConnectImplementationsToTypesClosing(typeof(IValidator<>));
                });

                // Registrar otras dependencias
                config.For<IValidationEngine>().Use<ValidationEngine>();
                config.For<IUnitOfWork>().Use<UnitOfWork>();
                config.For<CommandDispatcher>().Use<CommandDispatcher>();
                config.For<QueryDispatcher>().Use<QueryDispatcher>();
            });

            return container;
        });

        public static IContainer Instance => _instance.Value;
    }
}
