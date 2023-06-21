using Dignus.Collections;
using Dignus.Sockets;

namespace EchoClient
{
    internal class ClientModule : ClientBase
    {
        public static SynchronizedArrayList<DummyPacket> DummyPackets = new SynchronizedArrayList<DummyPacket>();
        private bool _isConnect = false;
        public ClientModule(SessionCreator sessionCreator) : base(sessionCreator)
        {

        }

        protected override void OnConnected(Session session)
        {
            _isConnect = true;
        }

        protected override void OnDisconnected(Session session)
        {
            _isConnect = false;
        }
        public bool IsConnect => _isConnect;
    }
}
