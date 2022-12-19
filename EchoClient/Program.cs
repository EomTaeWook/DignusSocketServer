using EchoClient.Serializer;
using Kosher.Extensions.Log;
using Kosher.Log;
using Kosher.Sockets;

namespace EchoClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LogBuilder.Configuration(LogConfigXmlReader.Load($"{AppContext.BaseDirectory}KosherLog.config"));
            LogBuilder.Build();


            var sessionCreator = new SessionCreator(new DummySerializer(),
                                                new DummyDeserializer(),
                                                null);

            var client = new ClientModule(sessionCreator);
            client.Connect("127.0.0.1", 35000);
            for (int i = 0; i < 1000; ++i)
            {
                client.Send(new Packet($"client : {i}"));
            }
            
            Console.ReadLine();
            client.Close();
        }
    }
}