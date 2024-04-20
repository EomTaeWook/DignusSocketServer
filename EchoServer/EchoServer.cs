using Dignus.Log;
using Dignus.Sockets;
using Dignus.Sockets.Interfaces;

namespace Echo
{
    internal class EchoServer : ServerBase
    {
        public EchoServer(SessionInitializer sessionInitializer) : base(sessionInitializer)
        {
        }

        protected override void OnAccepted(ISession session)
        {
            LogHelper.Info($"[server] session accepted - {session.Id}");

            MessageSender.Instance.AddSession(session);
        }

        protected override void OnDisconnected(ISession session)
        {
            LogHelper.Info($"[server] session disconnected - {session.Id}");

            MessageSender.Instance.RemoveSession(session);
        }
    }
}
