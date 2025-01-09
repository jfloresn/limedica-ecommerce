namespace Bootstrapper.Web.Common.DependencyResolution.Registries
{
    using ServiceAgents.Common;



    using Infraestructure.Common.Module;
    using StructureMap;


    public class BackendClientRegistry : Registry
    {
        public BackendClientRegistry()
        {
            this.Scan(x =>
            {
                x.AssemblyFromModule("ServiceAgents");
                x.Include(t => typeof(IBackendClient).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface);
                x.SingleImplementationsOfInterface()
                    .OnAddedPluginTypes(t => t.ContainerScoped());
            }
            );
        }
    }
}
