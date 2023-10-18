using Dignus.Collections;
using Dignus.Framework;
using Dignus.Sockets;
using System.Diagnostics;

namespace Echo
{
    internal class MessageSender : Singleton<MessageSender>
    {
        SynchronizedHashSet<Session> _sessions = new SynchronizedHashSet<Session>();
        public void AddSession(Session session)
        {
            _sessions.Add(session);
        }
        public void RemoveSession(Session session)
        {
            _sessions.Remove(session);
        }
        public void Broadcast(byte[] message)
        {
            foreach (var session in _sessions)
            {
                session.Send(message);
            }
        }
    }
}
