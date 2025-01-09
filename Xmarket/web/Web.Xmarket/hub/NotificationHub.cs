using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Xmarket.hub
{
    public class NotificationHub : Hub
    {
        public void SendMessage(string message)
        {
            Clients.All.receiveMessage(message);
        }
    }
}
