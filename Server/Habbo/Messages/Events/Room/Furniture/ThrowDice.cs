using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mango.Communication.Sessions;

namespace RageServer.Server.Habbo.Messages.Events.Room.Furniture
{
    class ThrowDice : IMessageEvent
    {
        // INCOMING ID: 76
        public void Handle(Session client, ClientMessage parser)
        {
            throw new NotImplementedException();
        }
    }
}