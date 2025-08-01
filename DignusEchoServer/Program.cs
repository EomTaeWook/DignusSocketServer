﻿using Dignus.Log;
using Dignus.Sockets;
using Dignus.Sockets.Interfaces;
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
            sessionInitializer.SocketOption.SendBufferSize = 65536 * 20;
            EchoServer echoServer = new(sessionInitializer);
            echoServer.Start(5000);
            LogHelper.Info($"start server... port : {5000}");
            Console.ReadKey();
        }

        static Tuple<IPacketSerializer, IPacketHandler, ICollection<ISessionComponent>> PacketHandlerSetupFactory()
        {
            EchoHandler handler = new();

            PacketSerializer packetSerializer = new(handler);

            return Tuple.Create<IPacketSerializer, IPacketHandler, ICollection<ISessionComponent>>(
                    packetSerializer,
                    packetSerializer,
                    [handler]);
        }

        static Tuple<IPacketSerializer, IPacketHandler, ICollection<ISessionComponent>> EchoSetupFactory()
        {
            EchoSerializer packetSerializer = new();

            return Tuple.Create<IPacketSerializer, IPacketHandler, ICollection<ISessionComponent>>(
                    packetSerializer,
                    packetSerializer,
                    []);
        }
    }
}