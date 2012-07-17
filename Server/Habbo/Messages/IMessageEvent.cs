using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mango.Communication.Sessions;

namespace RageServer.Server.Habbo.Messages
{
    interface IMessageEvent
    {
        void Handle(Session client, ClientMessage parser);
    }
}
