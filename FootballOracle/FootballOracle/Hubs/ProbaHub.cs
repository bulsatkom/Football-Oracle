using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballOracle.Hubs
{
    public class ProbaHub : Hub
    {
        public void assssd()
        {
          
        }

        public void sendMessage(string msg)
        {
            Clients.Caller.addMessage(msg);
        }
    }
}