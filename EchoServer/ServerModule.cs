using Dignus.Log;
using Dignus.Sockets;
using Dignus.Sockets.Interface;

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
            var sessionCreator = new SessionCreator(() =>
            {
                EchoHandler handler = new();
                return Tuple.Create<IPacketSerializer, IPacketDeserializer, ICollection<ISessionHandler>>(
                    new DummySerializer(),
                    new DummyDeserializer(handler),
                    new List<ISessionHandler>() { handler });
            });
            _server = new EchoServer(sessionCreator);
            _server.Start(10000);
            isActive = true;

            LogHelper.Info($"start server... port : {10000}");
            while (isActive)
            {
                Thread.Sleep(33);
            }
        }
    }
}
