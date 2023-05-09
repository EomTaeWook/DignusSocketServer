using Kosher.Log;
using Kosher.Sockets;
using Kosher.Sockets.Interface;
using Kosher.Sockets.ObjectPool;

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
                return Tuple.Create<IPacketSerializer, IPacketDeserializer, ICollection<ISessionComponent>>(new DummySerializer(),
                                                                                                            new DummyDeserializer(handler),
                                                                                                            new List<ISessionComponent>() { handler });
            },
            LohMemoryPool<byte>.Instance);
            _server = new EchoServer(sessionCreator);
            _server.Start(41000);
            isActive = true;

            LogHelper.Info($"start server... port : {41000}");
            while (isActive)
            {
                Thread.Sleep(33);
            }
        }
    }
}
