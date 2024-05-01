using Dignus.Log;
using Dignus.Sockets;
using Dignus.Sockets.Interfaces;

namespace Echo
{
    internal class ServerModule
    {
        EchoServer _server;
        bool isActive = false;
        public ServerModule()
        {
        }
        public void Run()
        {
            var sessionInitializer = new SessionConfiguration(() =>
            {
                EchoHandler handler = new();

                return Tuple.Create<IPacketSerializer, IPacketDeserializer, ICollection<ISessionComponent>>(
                    new DummySerializer(),
                    new DummyDeserializer(handler),
                    new List<ISessionComponent>() { handler });
            });
            _server = new EchoServer(sessionInitializer);
            _server.Start(10000, ProtocolType.Tcp);
            isActive = true;

            LogHelper.Info($"start server... port : {10000}");
            while (isActive)
            {
                Thread.Sleep(33);
            }
        }
    }
}
