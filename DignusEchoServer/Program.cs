using Dignus.Log;
using Dignus.Sockets;
using DignusEchoServer.Handler;
using DignusEchoServer.Serializer;

namespace DignusEchoServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LogBuilder.Configuration(LogConfigXmlReader.Load($"{AppContext.BaseDirectory}DignusLog.config"));
            LogBuilder.Build();

            var sessionInitializer = new SessionConfiguration(EchoSetupFactory);
            sessionInitializer.SocketOption.SendBufferSize = 65536;
            sessionInitializer.SocketOption.MaxPendingSendBytes = int.MaxValue;
            EchoServer echoServer = new(sessionInitializer);
            echoServer.Start(5000);
            LogHelper.Info($"start server... port : {5000}");
            Console.ReadKey();
        }

        static SessionSetup PacketHandlerSetupFactory()
        {
            EchoHandler handler = new();

            PacketSerializer packetSerializer = new(handler);

            return new SessionSetup(
                    packetSerializer,
                    packetSerializer,
                    [handler]);
        }

        static SessionSetup EchoSetupFactory()
        {
            EchoSerializer packetSerializer = new();

            return new SessionSetup(
                    packetSerializer,
                    packetSerializer,
                    []);
        }
    }
}