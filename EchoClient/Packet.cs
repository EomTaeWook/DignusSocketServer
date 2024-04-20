using Dignus.Sockets.Interfaces;
using System.Text;
using System.Text.Json;

namespace EchoClient
{
    public class Packet : IPacket
    {
        public byte[] Body;

        public Packet(string body)
        {
            Body = Encoding.UTF8.GetBytes(body);
        }
        public int GetLength()
        {
            return Body.Length;
        }
        public static Packet MakePacket<T>(T packetData)
        {
            var packet = new Packet(JsonSerializer.Serialize(packetData));
            return packet;
        }
    }
}
