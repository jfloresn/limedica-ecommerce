namespace QueryContracts.Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using Infraestructure.Common.Types;


    [KnownType("GetKnowTypes")]
    [DataContract(Name = "QueryParameter", Namespace = "http://schemas.datacontract.org/2004/07/QueryContracts.Common")]
    public class QueryParameter
    {

       [DataMember]
        public string ServidorWeb { get; set; }
        [DataMember]
        public string CodigoUsuario { get; set; }
        [DataMember]
        public int registroInicio { get; set; }
        [DataMember]
        public int registroFin { get; set; }
        private static IEnumerable<Type> GetKnowTypes()
        {
            var knownTypes = KnownTypesHelper.GetKnownTypes<QueryParameter>();
            return knownTypes;
        }
    }
}
