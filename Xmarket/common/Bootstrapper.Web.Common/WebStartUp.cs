namespace Bootstrapper.Web.Common
{
    using System.Web.Mvc;

    using Bootstrapper.Web.Common.DependencyResolution;

    using FluentValidation.Mvc;

    using Infraestructure.Common.Logging;
    using Infraestructure.Common.Logging.AppenderBuilders;
    using Infraestructure.Common.Module;
    using Infraestructure.Common.Types;
    using Infraestructure.Common.Validation;

    using StructureMap;

    public class WebStartUp
    {
        public static void Init()
        {
            RegisterModuleName();
            ConfigureLogging();

            ConfigureValidation();
        }

        private static void RegisterModuleName()
        {
            ModuleStorage.RegisterCurrentModuleFromConfigFile();
        }

        private static void ConfigureValidation()
        {
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            ModelValidatorProviders.Providers.Clear();

            var container = WebDependencyRegistrar.RegisterDependencies();

            DependencyResolver.SetResolver(new WebDependencyResolver(container));


            KnownTypesHelper.setContainer(container);

            FluentValidationModelValidatorProvider.Configure(
                provider => { provider.ValidatorFactory = new IocValidatorFactory(container); });


        }



        private static void ConfigureLogging()
        {
            LoggingConfigurator.AddAppenderBuilder<FileAppenderBuilder>();
            LoggingConfigurator.Configure();
        }
    }
}