using Kosher.Sockets.Interface;
using System.Text;

namespace EchoClient
{
    public class Packet : IPacket
    {
        public byte[] Body;
        public Packet(byte[] body)
        {
            Body = body;
        }
        public Packet(string body)
        {
            Body = Encoding.UTF8.GetBytes(body);
        }
        public int GetLength()
        {
            return Body.Length;
        }
    }
}
