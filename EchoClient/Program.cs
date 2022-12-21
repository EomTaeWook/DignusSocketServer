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


            Parallel.For(0, 10000, (i) =>
            {
                try
                {
                    var client = new ClientModule(sessionCreator);
                    client.Connect("13.125.232.85", 35000);
                    //client.Connect("127.0.0.1", 35000);
                    client.Send(new Packet($"client : {i}"));
                    client.Close();
                }
                catch(Exception ex)
                {
                    LogHelper.Error(ex);
                }
                
            });

            Console.ReadLine();
        } 
    }
}