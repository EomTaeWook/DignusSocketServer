using Dignus.Collections;
using Dignus.Framework;
using Dignus.Sockets.Interfaces;

namespace Echo
{
    internal class MessageSender : Singleton<MessageSender>
    {
        SynchronizedUniqueSet<ISession> _sessions = new SynchronizedUniqueSet<ISession>();
        public void AddSession(ISession session)
        {
            _sessions.Add(session);
        }
        public void RemoveSession(ISession session)
        {
            _sessions.Remove(session);
        }
        public void Broadcast(ISession sender, byte[] message)
        {
            foreach (var session in _sessions)
            {
                if (sender == session)
                {
                    continue;
                }
                session.Send(message);
            }
        }
        public void Echo(ISession sender, byte[] message)
        {
            sender.Send(message);
        }
    }
}
