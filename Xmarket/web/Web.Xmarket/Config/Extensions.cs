

namespace ServiceAgents.Common
{
    using System.Reflection;
    using System.ServiceModel;

    using CommandContracts.Common;
    using System.Web.Mvc;
    using log4net;
    using QueryContracts.Common;
    using StructureMap;
    using System.Threading.Tasks;

    public static class Extensions
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static object Execute(this QueryParameter parameter)
        {

            var backendClient = AgentsGtrmDependencyResolver.GetInstance<IBackendClient>();
            
            parameter.ServidorWeb = System.Environment.MachineName;

            try
            {
                object objServicio = backendClient.ExecuteQuery(parameter);
                return objServicio;
            }
            catch (EndpointNotFoundException ex)
            {
                log.Error(ex.Message, ex);
                throw;
             }
        }

        public static async Task<object> ExecuteAsync(this QueryParameter parameter)
        {

            IBackendClient backendClient = AgentsGtrmDependencyResolver.GetInstance<IBackendClient>();


            parameter.ServidorWeb = System.Environment.MachineName;

            try
            {
                var objServicio = await backendClient.ExecuteQueryAsync(parameter);
                return objServicio;
            }
            catch (EndpointNotFoundException ex)
            {
                log.Error(ex.Message, ex);
                throw;
            }
        }

        public static object Execute(this Command command)
        {
            var backendClient = AgentsGtrmDependencyResolver.GetInstance<IBackendClient>();

            try
            {
                object objServicio = backendClient.ExecuteCommand(command);
                return objServicio;
            }
            catch (EndpointNotFoundException ex)
            {
                        
                log.Error(ex.Message, ex);
                throw;
            }
        }


        public static async Task<object> ExecuteAsync(this Command command)
        {
            var backendClient = AgentsGtrmDependencyResolver.GetInstance<IBackendClient>();

            try
            {
                object objServicio = await backendClient.ExecuteCommandAsync(command);
                return objServicio;
            }
            catch (EndpointNotFoundException ex)
            {

                log.Error(ex.Message, ex);
                throw;
            }
        }
    }
}
