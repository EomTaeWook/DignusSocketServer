using Dignus.Collections;
using Dignus.Sockets.Interfaces;

namespace Echo
{
    public class EchoHandler : ISessionComponent
    {
        private ISession _session;
        public void Process(byte[] body)
        {
            if (_session == null)
            {
                return;
            }
            //var str = Encoding.UTF8.GetString(body);
            //LogHelper.Debug($"[{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff")}] {str}");
            var sendBuffer = new ArrayQueue<byte>();
            sendBuffer.AddRange(BitConverter.GetBytes(body.Length));
            sendBuffer.AddRange(body);
            MessageSender.Instance.Echo(_session, sendBuffer.ToArray());
        }

        public void SetSession(ISession session)
        {
            _session = session;
        }
        public ISession GetSession()
        {
            return _session;
        }

        public void Dispose()
        {
            _session = null;
        }
    }
}
