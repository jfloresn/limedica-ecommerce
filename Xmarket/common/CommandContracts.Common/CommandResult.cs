namespace CommandContracts.Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using Infraestructure.Common.Types;


    [KnownType("GetKnowTypes")]
    [DataContract(Name = "CommandResult", Namespace = "http://schemas.datacontract.org/2004/07/CommandContracts.Common")]
    public class CommandResult
    {

        private static IEnumerable<Type> GetKnowTypes()
        {
            var types = KnownTypesHelper.GetKnownTypes<CommandResult>();
            return types;
        }
    }
}
