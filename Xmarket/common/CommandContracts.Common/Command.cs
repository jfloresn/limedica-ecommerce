namespace CommandContracts.Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using Infraestructure.Common.Types;




    [KnownType("GetKnowTypes")]

    [DataContract(Name = "Command", Namespace = "http://schemas.datacontract.org/2004/07/CommandContracts.Common")]
    public class Command
    {
        private static IEnumerable<Type> GetKnowTypes()
        {
            return KnownTypesHelper.GetKnownTypes<Command>();
        }

        [DataMember]
        public Int32 idSesion { set; get; }
        [DataMember]
        public Int32 idPublico { set; get; }


        [DataMember]
        public Int32 idUsuarioModifica { set; get; }

        [DataMember]
        public Int32 idUsuarioCrea { set; get; }

        [DataMember]
        public string sesionPublica { set; get; }

        [DataMember]
        public TipoTransaccion Transaccion { get; set; }

        public enum TipoTransaccion
        {
            Insertar = 0,
            Modificar = 1,
            Eliminar = 2
        }

    }
}
