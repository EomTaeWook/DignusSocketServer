using Dignus.Collections;
using Dignus.Log;
using Dignus.Sockets.Interface;
using System.Text;

namespace Echo
{
    public class DummyDeserializer : IPacketDeserializer
    {
        private const int SizeToInt = sizeof(int);
        private readonly EchoHandler _protocolHandler;

        public DummyDeserializer(EchoHandler protocolHandler)
        {
            _protocolHandler = protocolHandler;
        }

        public void Deserialize(ArrayList<byte> buffer)
        {
            var packetSize = BitConverter.ToInt32(buffer.Read(SizeToInt));
            var bodyBytes = buffer.Read(packetSize);
            _protocolHandler.Process(bodyBytes);
        }

        public bool IsTakedCompletePacket(ArrayList<byte> buffer)
        {
            if (_protocolHandler.GetSession().IsDispose() == true)
            {
                LogHelper.Debug($"{_protocolHandler.GetSession().Id} is closed");
                return false;
            }

            if (buffer.Count <= SizeToInt)
            {
                return false;
            }
            var packetSize = BitConverter.ToInt32(buffer.Peek(SizeToInt));
            return buffer.Count >= packetSize + SizeToInt;
        }
    }
}
