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
                EchoHandler handler = new EchoHandler();
                return Tuple.Create<IPacketSerializer, IPacketDeserializer, ICollection<ISessionComponent>>(
                    new DummySerializer(),
                    new DummyDeserializer(handler),
                    new List<ISessionComponent>() { handler });
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
