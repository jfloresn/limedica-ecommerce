

namespace Services.Xmarket
{
    using System.ServiceModel;
    using CommandContracts.Common;
    using CommandHandlers.Common;
    using DistributedServices.Common;
    using QueryContracts.Common;
    using QueryHandlers.Common;


    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class GTRMBackendService : IBackendService
    {

        private readonly CommandDispatcher commandDispatcher;
        private readonly QueryDispatcher queryDispatcher;

        public GTRMBackendService(CommandDispatcher commandDispatcher, QueryDispatcher queryDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
            this.queryDispatcher = queryDispatcher;
        }

        public CommandResult ExecuteCommand(Command command)
        {
            return commandDispatcher.Dispatch(command);
        }

        public QueryResult ExecuteQuery(QueryParameter parameter)
        {
            return queryDispatcher.Dispatch(parameter);
        }
    }
}
