using Kosher.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchoClient
{
    internal class ClientModule : BaseClient
    {
        public ClientModule(SessionCreator sessionCreator) : base(sessionCreator)
        {

        }

        protected override void OnConnected(Session session)
        {
            
        }

        protected override void OnDisconnected(Session session)
        {
            
        }
    }
}
