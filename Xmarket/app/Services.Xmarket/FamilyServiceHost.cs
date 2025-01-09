using StructureMap;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using Infraestructure.Common.UserDatabaseConnection;
using Bootstrapper.Wcf.Common;
using System.Linq;
using Infraestructure.Common.Types;

namespace Services.Xmarket
{

    public class FamilyServiceHost : ServiceHost
    {
        public FamilyServiceHost(IContainer container, Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {

            foreach (var cd in ImplementedContracts.Values)
            {
                cd.Behaviors.Add(new StructureMapInstanceProvider(container, serviceType));
            }

            KnownTypesHelper.setContainer(container);

            WcfStartUp.Init(ConnectionStringInHeaderMessage);

        }


        protected override void OnOpening()
        {
            base.OnOpening();
        }

        private bool ConnectionStringInHeaderMessage
        {
            get
            {
                var mainEndpoint = this.Description.Endpoints.SingleOrDefault(x => x.Name == "main");
                if (mainEndpoint == null)
                {
                    throw new Exception("Endpoint 'main' was not found ");
                }
                var res = mainEndpoint.Behaviors.Find<UserDatabaseConnectionBehavior>();
                return res != null;
            }
        }
    }


    public class StructureMapInstanceProvider : IInstanceProvider, IContractBehavior
    {
        private readonly IContainer _container;
        private readonly Type _serviceType;

        public StructureMapInstanceProvider(IContainer container, Type serviceType)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _serviceType = serviceType ?? throw new ArgumentNullException(nameof(serviceType));
        }

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return GetInstance(instanceContext);
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return _container.GetInstance(_serviceType);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            // Implementar lógica para liberar la instancia si es necesario
        }

        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            // No es necesario agregar parámetros de enlace adicionales
        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            // No es necesario agregar comportamiento del cliente
        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            dispatchRuntime.InstanceProvider = this;
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
            // No es necesario validar contratos adicionales
        }
    }
}
