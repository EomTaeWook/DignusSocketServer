using Kosher.Log;
using Kosher.Sockets;

namespace Echo
{
    internal class EchoServer : BaseServer
    {
        public EchoServer(SessionCreator sessionCreator) : base(sessionCreator)
        {
        }

        protected override void OnAccepted(Session session)
        {
            LogHelper.Info($"[server] session accepted - {session.Id}");
        }

        protected override void OnDisconnected(Session session)
        {
            LogHelper.Info($"[server] session disconnected - {session.Id}");
        }
    }
}
