using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mango.Communication.Sessions;

namespace RageServer.Server.Habbo.Messages.Events.Inventory.Trading
{
    class ConfirmDeclineTrading : IMessageEvent
    {
        // INCOMING ID: 403
        public void Handle(Session client, ClientMessage parser)
        {
            throw new NotImplementedException();
        }
    }
}