﻿namespace Infraestructure.Common.UserDatabaseConnection
{
    using System.Web;

    using StructureMap.Pipeline;
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Dispatcher;

    public class UserDatabaseConnectionMessageInspector : IClientMessageInspector, IDispatchMessageInspector
    {
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {

            var conexion = true;//HttpContext.Current.Session["SessionWrapperConexion"] == null ? false : Convert.ToBoolean(HttpContext.Current.Session["SessionWrapperConexion"]);
            var user = "";
            var password = "";

            if ((user == null || password == null) && conexion == false)
                throw new Exception("Connection for outgoing message header doesn't exist");
             
            var connectionHeader = new Connection
            {
                User = Convert.ToString(user),
                Password = Convert.ToString(password)
            };

            var header = MessageHeader.CreateHeader(HeaderConstants.Name, HeaderConstants.NamespaceURI, connectionHeader);
            request.Headers.Add(header);
            
            return null;
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            return;
        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            var index = request.Headers.FindHeader(HeaderConstants.Name, HeaderConstants.NamespaceURI);
            if (index == -1)
                throw new Exception("Connection header in incomming message doesn't exist");

            request.Headers.UnderstoodHeaders.Add(request.Headers[index]);

            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            return;
        }
    }
}
