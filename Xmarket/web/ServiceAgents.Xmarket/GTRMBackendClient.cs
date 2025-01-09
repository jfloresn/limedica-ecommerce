using System.Reflection;
using CommandContracts.Common;
using QueryContracts.Common;
using ServiceAgents.Common;

using ServiceAgents.Xmarket.XmarketServices;
using System.Threading.Tasks;

namespace ServiceAgents.Xmarket
{
    public class GTRMBackendClient : IBackendClient
    {
        static readonly string AssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
        BackendServiceClient client = new BackendServiceClient();

        
 
        public CommandResult ExecuteCommand(Command command)
        {
            return client.ExecuteCommand(command);
        }

        public async Task<CommandResult> ExecuteCommandAsync(Command command)
        {
            return await  client.ExecuteCommandAsync(command);
        }

        public QueryResult ExecuteQuery(QueryParameter parameter)
        {
            return client.ExecuteQuery(parameter);
        }
        public async Task<QueryResult> ExecuteQueryAsync(QueryParameter parameter)
        {
            return await    client.ExecuteQueryAsync(parameter);
        }

               
        public void Dispose()
        {
            client.Close();
        }
    }
}
