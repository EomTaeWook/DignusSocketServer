using Dignus.Collections;
using Dignus.Log;
using Dignus.Sockets.Interface;
using System.Text;
using System.Text.Json;


namespace EchoClient.Serializer
{
    internal class DummyDeserializer : IPacketDeserializer
    {
        private const int SizeToInt = sizeof(int);

        public DummyDeserializer()
        {
        }

        public void Deserialize(ArrayQueue<byte> buffer)
        {
            var packetSize = BitConverter.ToInt32(buffer.Read(SizeToInt));
            LogHelper.Debug($"[{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff")}] Deserialize packet size : {packetSize} ");

            if (packetSize == 0)
            {

            }
            var bodyBytes = buffer.Read(packetSize);
            var str = Encoding.UTF8.GetString(bodyBytes);
            LogHelper.Debug($"[{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff")}] Deserialize : {str} ");
            var packet = JsonSerializer.Deserialize<DummyPacket>(str);
            packet.Receive = DateTime.Now;
            ClientModule.DummyPackets.Add(packet);
        }
        public bool IsCompletePacketInBuffer(ArrayQueue<byte> buffer)
        {
            if (buffer.Count <= SizeToInt)
            {
                return false;
            }

            var packetSize = BitConverter.ToInt32(buffer.Peek(SizeToInt));

            LogHelper.Info($"[CompletePacket] packetSize : {packetSize}");

            return buffer.Count >= packetSize + SizeToInt;
        }
    }
}
