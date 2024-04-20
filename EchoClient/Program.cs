using Dignus.Extensions.Log;
using Dignus.Log;
using Dignus.Sockets;
using Dignus.Sockets.Interfaces;
using EchoClient.Serializer;

namespace EchoClient
{
    internal class Program
    {

        static void Main(string[] args)
        {
            LogBuilder.Configuration(LogConfigXmlReader.Load($"{AppContext.BaseDirectory}DignusLog.config"));
            LogBuilder.Build();

            var sessionCreator = new SessionCreator(() =>
            {
                return Tuple.Create<IPacketSerializer,
                    IPacketDeserializer,
                    ICollection<ISessionHandler>>(new DummySerializer(),
                        new DummyDeserializer(),
                        new List<ISessionHandler>() { });
            });

            var poolCount = 1;
            var sendedCount = 10000;

            var clientPool = new List<ClientModule>();

            for (var i = 0; i < poolCount; ++i)
            {
                clientPool.Add(new ClientModule(sessionCreator));
            }

            for (var i = 0; i < clientPool.Count; ++i)
            {
                //clientPool[i].Connect("54.180.99.221", 41000);
                clientPool[i].Connect("127.0.0.1", 10000);
                if (clientPool[i].IsConnect == true)
                {
                    for (int ii = 0; ii < sendedCount; ++ii)
                    {
                        DummyPacket packet = new()
                        {
                            Send = DateTime.Now,
                            Index = i,
                        };
                        clientPool[i].Send(Packet.MakePacket(packet));
                    }
                }
                else
                {

                }
            }
            double avg = 0;

            while (true)
            {
                if (sendedCount * clientPool.Count <= ClientModule.DummyPackets.Count)
                {
                    break;
                }
                DummyPacket packet = new()
                {
                    Send = DateTime.Now,
                    Index = 0,
                };
                clientPool[0].Send(Packet.MakePacket(packet));
                //LogHelper.Debug($"rec count : {ClientModule.DummyPackets.Count}");
            }

            foreach (var item in ClientModule.DummyPackets)
            {
                avg += (item.Receive - item.Send).TotalMilliseconds;
            }
            avg = (avg / 1000) / ClientModule.DummyPackets.Count;

            Console.WriteLine();
            Console.WriteLine(avg);

            ClientModule.DummyPackets.Clear();


            //var result = Parallel.ForEach(clientPool, (c) => 
            //{
            //    //c.Connect("54.180.99.221", 41000);
            //    c.Connect("127.0.0.1", 41000);
            //    if(c.IsConnect == true)
            //    {
            //        for (var i = 0; i < 100; ++i)
            //        {
            //            DummyPacket packet = new()
            //            {
            //                Send = DateTime.Now,
            //            };
            //            c.Send(Packet.MakePacket(packet));
            //        }
            //    }
            //    else
            //    {

            //    }
            //    //c.Connect("54.180.99.221", 41000);
            //    //for (var i = 0; i < 200; ++i)
            //    //{
            //    //    DummyPacket packet = new()
            //    //    {
            //    //        Send = DateTime.Now,
            //    //    };
            //    //    c.Send(Packet.MakePacket(packet));
            //    //}               
            //});

            Parallel.ForEach(clientPool, (c) =>
            {
                c.Close();
                //c.Connect("127.0.0.1", 41000);
            });

            clientPool.Clear();

            //Parallel.For(0, 1, (i) =>
            //{
            //    try
            //    {

            //        client.Connect("54.180.99.221", 41000);
            //        //client.Connect("127.0.0.1", 41000);
            //        for (long ii = 0; ii<1000; ++ii)
            //        {
            //            client.Send(new Packet($"string client : {ii}"));
            //        }
            //    }
            //    catch(Exception ex)
            //    {
            //        LogHelper.Error(ex);
            //    }
            //});

            Console.ReadLine();
        }
    }
}