using Dignus.Sockets.Interfaces;

namespace Echo
{
    public class DummySerializer : IPacketSerializer
    {
        ArraySegment<byte> IPacketSerializer.MakeSendBuffer(IPacket packet)
        {
            return Array.Empty<byte>();
        }
    }
}