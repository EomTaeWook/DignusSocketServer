using Kosher.Collections;
using Kosher.Log;
using Kosher.Sockets;
using Kosher.Sockets.Interface;
using System.Text;

namespace Echo
{
    public class EchoHandler : ISessionComponent
    {
        private Session _session;
        public void Process(byte[] body)
        {
            if (_session.IsDispose() == true)
            {
                return;
            }
            var str = Encoding.UTF8.GetString(body);
            LogHelper.Debug($"[{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff")}] {str}");

            var sendBuffer = new ArrayList<byte>();

            sendBuffer.AddRange(BitConverter.GetBytes(body.Length));

            sendBuffer.AddRange(body);

            _session.Send(sendBuffer.ToArray());
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
