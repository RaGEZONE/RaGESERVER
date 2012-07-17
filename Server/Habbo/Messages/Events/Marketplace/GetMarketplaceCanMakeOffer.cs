using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mango.Communication.Sessions;

namespace RageServer.Server.Habbo.Messages.Events.Marketplace
{
    class GetMarketplaceCanMakeOffer : IMessageEvent
    {
        // INCOMING ID: 3012
        public void Handle(Session client, ClientMessage parser)
        {
            throw new NotImplementedException();
        }
    }
}