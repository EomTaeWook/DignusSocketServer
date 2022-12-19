using Kosher.Log;
using Kosher.Sockets;
using Kosher.Sockets.Interface;

namespace Echo
{
    internal class ServerModule
    {
        readonly EchoHandler handler = new EchoHandler();
        readonly DummyDeserializer _dummyDeserializer;
        readonly DummySerializer _dummySerializer;

        EchoServer _server;
        bool isActive = false;
        public ServerModule()
        {
            _dummyDeserializer = new DummyDeserializer(handler);
            _dummySerializer = new DummySerializer();
        }
        public void Run()
        {
            var sessionCreator = new SessionCreator(_dummySerializer,
                                                _dummyDeserializer,
                                                new List<ISessionComponent>() { handler });
            _server = new EchoServer(sessionCreator);
            _server.Start(35000);
            isActive = true;

            LogHelper.Debug("start server...");
            while (isActive)
            {
                Thread.Sleep(33);
            }
        }
    }
}
