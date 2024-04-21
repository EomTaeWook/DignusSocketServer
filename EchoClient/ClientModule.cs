using Dignus.Collections;
using Dignus.Sockets;
using Dignus.Sockets.Interfaces;

namespace EchoClient
{
    internal class ClientModule : ClientBase
    {
        public static SynchronizedArrayQueue<DummyPacket> DummyPackets = new SynchronizedArrayQueue<DummyPacket>();
        private bool _isConnect = false;
        public ClientModule(SessionConfiguration sessionConfiguration) : base(sessionConfiguration)
        {

        }

        protected override void OnConnected(ISession session)
        {
            _isConnect = true;
        }

        protected override void OnDisconnected(ISession session)
        {
            _isConnect = false;
        }
        public bool IsConnect => _isConnect;
    }
}
