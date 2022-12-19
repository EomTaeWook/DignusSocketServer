using Kosher.Collections;
using Kosher.Sockets.Interface;

namespace Echo
{
    public class DummySerializer: IPacketSerializer 
    {
        public Vector<byte> MakeSendBuffer(IPacket packet)
        {
            return new Vector<byte>();
        }
    }
}
