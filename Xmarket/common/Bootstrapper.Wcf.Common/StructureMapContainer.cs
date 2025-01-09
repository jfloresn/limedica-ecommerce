using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bootstrapper.Wcf.Common.DependencyResolution.Registries;
using CommandHandlers.Common;
using Data.Common.UnitOfWork;
using FluentValidation;
using Infraestructure.Common.Types;
using Infraestructure.Common.Validation;
using QueryHandlers.Common;
using StructureMap;

namespace Bootstrapper.Wcf.Common
{
    public class StructureMapContainer
    {
        private static readonly Lazy<IContainer> _instance = new Lazy<IContainer>(() =>
        {


            WcfStartUp.RegisterModuleName();


            var getexecutingassembly = Assembly.GetExecutingAssembly();
            var namespaceWcfDependencyRegistrar = typeof(StructureMapContainer).Namespace;
            var nameContractValidatorsRegistry = typeof(ContractValidatorsRegistry).Namespace;


            var _container = new Container(config =>
            {
                // Registrar dependencias
                config.For<IValidationEngine>().Use<ValidationEngine>();
                config.For<IUnitOfWork>().Use<UnitOfWork>();
                config.For<CommandDispatcher>().Use<CommandDispatcher>();
                config.For<QueryDispatcher>().Use<QueryDispatcher>();


                // Registrar IValidatorFactory
                config.For<IValidatorFactory>().Use<IocValidatorFactory>();

                // Registrar todos los validadores automáticamente
                config.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.ConnectImplementationsToTypesClosing(typeof(IValidator<>));
                });

                config.Scan(scan =>
                {
                    scan.Assembly(getexecutingassembly);
                    scan.LookForRegistries();
                    scan.IncludeNamespace(namespaceWcfDependencyRegistrar);
                    scan.IncludeNamespace(nameContractValidatorsRegistry);
                    scan.WithDefaultConventions();

                });

            });

            KnownTypesHelper.setContainer(_container);

            return _container;
        });

        public static IContainer Instance => _instance.Value;
    }
}
