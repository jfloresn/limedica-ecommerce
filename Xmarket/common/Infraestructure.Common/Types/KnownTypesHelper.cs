namespace Infraestructure.Common.Types
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using StructureMap;

    public static class KnownTypesHelper
    {

        private static IContainer _container;

        public static void setContainer(IContainer container)
        {
            _container = container;
        }

        public static IEnumerable<Type> GetKnownTypes<T>()
        {

            var instances = _container.Model.AllInstances
                .Where(i => i.PluginType == typeof(T));

            return instances.Select(instanceRef => instanceRef.ReturnedType).Distinct().ToList();
        }
    }
}
