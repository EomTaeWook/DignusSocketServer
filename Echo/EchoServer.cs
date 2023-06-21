using Dignus.Log;
using Dignus.Sockets;

namespace Echo
{
    internal class EchoServer : ServerBase
    {
        public EchoServer(SessionCreator sessionCreator) : base(sessionCreator)
        {
        }

        protected override void OnAccepted(Session session)
        {
            LogHelper.Info($"[server] session accepted - {session.Id}");

            MessageSender.Instance.AddSession(session);
        }

        protected override void OnDisconnected(Session session)
        {
            LogHelper.Info($"[server] session disconnected - {session.Id}");

            MessageSender.Instance.RemoveSession(session);
        }
    }
}
