using Kosher.Log;
using Kosher.Sockets;
using Kosher.Sockets.Interface;

namespace Echo
{
    public class EchoHandler : ISessionComponent
    {
        private Session _session;
        public void Process(byte[] body)
        {
            _session.Send(body);
            LogHelper.Debug($"{_session.Id} send - {body.Length}");
        }
        
        public void SetSession(Session session)
        {
            _session = session;
        }
    }
}
