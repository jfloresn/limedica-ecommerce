
namespace Bootstrapper.Wcf.Common
{
    using System;
    using System.Configuration;
    using System.Data.Entity;

    using Bootstrapper.Wcf.Common.DependencyResolution;

    using Data.Common;
    using Data.Common.DbConnectionFactories;

    using Infraestructure.Common.Configuration;
    using Infraestructure.Common.Logging;
    using Infraestructure.Common.Logging.AppenderBuilders;
    using Infraestructure.Common.Module;

    public class WcfStartUp
    {
        public static void Init(bool connectionStringInHeaderMessage = false)
        {
            RegisterModuleName();
            ConfigureLogging();
            ConfigureDatabaseSettings(connectionStringInHeaderMessage);
        }

        public static void Init()
        {
            RegisterModuleName();
            ConfigureLogging();

        }

        public static void ConfigureDatabaseSettings(bool connectionStringInHeaderMessage)
        {
            if (connectionStringInHeaderMessage)
                Database.DefaultConnectionFactory = new UserSessionConnectionFactory();
            else
                Database.DefaultConnectionFactory = new DefaultConnectionFactory();

            Database.SetInitializer<DatabaseContext>(null);
        }

        public static void RegisterModuleName()
        {
            ModuleStorage.RegisterCurrentModuleFromConfigFile();
        }



        public static void ConfigureLogging()
        {
            if (!AppConfigurationSettings.IsProductionEnvironment)
            {
                LoggingConfigurator.AddAppenderBuilder<ConsoleAppenderBuilder>();
            }
            LoggingConfigurator.AddAppenderBuilder<FileAppenderBuilder>();
            LoggingConfigurator.AddAppenderBuilder<AdoNetAppenderBuilder>();
            LoggingConfigurator.Configure();
        }
    }
}