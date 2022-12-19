﻿using Kosher.Collections;
using Kosher.Log;
using Kosher.Sockets.Interface;
using System.Text;

namespace EchoClient.Serializer
{
    internal class DummyDeserializer : IPacketDeserializer
    {
        public void Deserialize(Vector<byte> buffer)
        {
            var bytes = buffer.Read(sizeof(int));
            var length = BitConverter.ToInt32(bytes);
            var bodyBytes = buffer.Read(length);
            var text = Encoding.UTF8.GetString(bodyBytes);
            LogHelper.Debug($"Receive Deserialize : {text}");
        }

        public bool IsTakedCompletePacket(Vector<byte> buffer)
        {
            if (buffer.Count < sizeof(int))
            {
                return false;
            }
            LogHelper.Debug($"[{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff")}] buffer size : {buffer.Count}");
            var bytes = buffer.Peek(sizeof(int));
            var length = BitConverter.ToInt32(bytes);
            return length + sizeof(int) <= buffer.Count;
        }
    }
}
