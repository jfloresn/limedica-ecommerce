﻿
namespace DistributedServices.Common
{
    using System.ServiceModel;
    using System.Threading.Tasks;
    using CommandContracts.Common;

    using QueryContracts.Common;

    [ServiceContract]
    public interface IBackendService
    {

        [OperationContract]
        [FaultContract(typeof(CommandFault))]
        CommandResult ExecuteCommand(Command command);

        [OperationContract]
        QueryResult ExecuteQuery(QueryParameter parameter);
    }
}
