namespace Bootstrapper.Web.Common.DependencyResolution
{
    using System.Reflection;
    using Bootstrapper.Web.Common.DependencyResolution.Registries;
    using StructureMap;

    public class WebDependencyRegistrar
    {
        public static Container RegisterDependencies()
        {
            var container = new Container(_ =>
            {
                _.Scan(scan =>
                {
                    scan.Assembly(Assembly.GetExecutingAssembly());
                    scan.LookForRegistries();
                    scan.IncludeNamespaceContainingType<WebDependencyRegistrar>();
                    scan.IncludeNamespaceContainingType<ContractValidatorsRegistry>();
                });
            });

            return container;
        }
    }
}
