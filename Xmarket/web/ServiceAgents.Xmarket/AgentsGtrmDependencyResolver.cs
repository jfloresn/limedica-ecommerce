namespace ServiceAgents.Common
{
    using ServiceAgents.Xmarket;
    using StructureMap;

    public static class AgentsGtrmDependencyResolver
    {
        private static readonly IContainer _container;

         static  AgentsGtrmDependencyResolver()
        {
            _container = new Container();
            // Configurar el contenedor aquí si es necesario
            _container.Configure(config =>
            {
                // Registro de IBackendClient
                config.For<IBackendClient>().Use<GTRMBackendClient>(); // Asegúrate de que BackendClient es tu implementación concreta de IBackendClient
            });

        }

        public static T GetInstance<T>()
        {
            return _container.GetInstance<T>();
        }
    }
}
