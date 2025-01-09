using System.Runtime.Serialization;

namespace CommandContracts.Common
{
    [DataContract(Name = "CommandError", Namespace = "http://schemas.datacontract.org/2004/07/CommandContracts.Common")]
    public class CommandError
    {
        [DataMember]
        public string PropertyName { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; }

        
        public CommandError() { }

        public CommandError(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }
    }
}
