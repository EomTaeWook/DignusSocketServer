using Dignus.Collections;
using Dignus.Sockets;
using Dignus.Sockets.Interface;
using System.Text;

namespace Echo
{
    public class EchoHandler : ISessionHandler
    {
        private Session _session;
        public void Process(byte[] body)
        {
            if (_session.IsDispose() == true)
            {
                return;
            }
            var str = Encoding.UTF8.GetString(body);
            //LogHelper.Debug($"[{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff")}] {str}");

            var sendBuffer = new ArrayQueue<byte>();

            sendBuffer.AddRange(BitConverter.GetBytes(body.Length));

            sendBuffer.AddRange(body);

            MessageSender.Instance.Broadcast(sendBuffer.ToArray());
        }

        public void SetSession(Session session)
        {
            _session = session;
        }
        public Session GetSession()
        {
            return _session;
        }

        public void Dispose()
        {

        }
    }
}
