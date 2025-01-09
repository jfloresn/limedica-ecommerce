using Bootstrapper.Wcf.Common;
using Bootstrapper.Wcf.Common.DependencyResolution;
using Bootstrapper.Wcf.Common.DependencyResolution.Registries;
using CommandHandlers.Common;
using FluentValidation;
using Infraestructure.Common.Validation;
using QueryHandlers.Common;
using StructureMap;
using System;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace Services.Xmarket
{
    public class FamilyServiceHostFactory : ServiceHostFactory
    {
        private static IContainer _container;

        public FamilyServiceHostFactory()
        {
            // Este constructor predeterminado es necesario para WCF
            if (_container == null)
            {
                _container = StructureMapContainer.Instance;

            }
        }

        public FamilyServiceHostFactory(IContainer container)
        {
            _container = StructureMapContainer.Instance;
        }



        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            return new FamilyServiceHost(StructureMapContainer.Instance, serviceType, baseAddresses);
        }
    }
}
