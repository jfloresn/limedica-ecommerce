
namespace ServiceAgents.Common
{
    using System;
    using System.Threading.Tasks;
    using CommandContracts.Common;

    using QueryContracts.Common;

    public interface IBackendClient : IDisposable
    {
        CommandResult ExecuteCommand(Command command);

         Task<CommandResult> ExecuteCommandAsync(Command command);

         Task<QueryResult> ExecuteQueryAsync(QueryParameter parameter);

        QueryResult ExecuteQuery(QueryParameter parameter);
    }
}
