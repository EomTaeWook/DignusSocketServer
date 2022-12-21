using Kosher.Sockets;
using Kosher.Sockets.Interface;

namespace Echo
{
    public class EchoHandler : ISessionComponent
    {
        private Session _session;
        public void Process(byte[] body)
        {
            if(_session.IsDispose() == true)
            {
                return;
            }
            _session.Send(body);
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
