using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mango.Communication.Sessions;

namespace RageServer.Server.Habbo.Messages.Events.Users
{
    class GetUserTags : IMessageEvent
    {
        // INCOMING ID: 263
        public void Handle(Session client, ClientMessage parser)
        {
            throw new NotImplementedException();
        }
    }
}