
namespace QueryContracts.Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Infraestructure.Common.Types;

   
    [KnownType("GetKnowTypes")]
    [DataContract(Name = "QueryResult", Namespace = "http://schemas.datacontract.org/2004/07/QueryContracts.Common")]
    public class QueryResult
    {

        public QueryResult()
        {

            this.Estatus = new QueryStatus();


        }

        private static IEnumerable<Type> GetKnowTypes()
        {
            IEnumerable<Type> types = KnownTypesHelper.GetKnownTypes<QueryResult>();
            return types;
        }


        [DataMember]
        public int EstadoResult { get; set; }
        [DataMember]
        public string MensajeResult { get; set; }

        [DataMember]
        public QueryStatus Estatus { get; set; }
        [DataMember]
        public int cantidadRegistro { get; set; }

    }

    public class QueryStatus
    {


        public int CodigoStatus { get; set; }
        public string Mensaje { get; set; }



    }

}
