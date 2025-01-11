using System;
using System.Collections.Concurrent;

namespace DotCache.Network;

public class TcpSessionManager
{
    private ConcurrentDictionary<string, TcpSession> _sessions;

    public TcpSessionManager()
    {
        _sessions = new();
    }

    public TcpSession Create()
    {
        var guid = Guid.NewGuid().ToString();
        var session = new TcpSession(guid);

        var added = _sessions.TryAdd(guid, session);
        if (false == added)
        {
            return null;
        }

        return session;
    }

    public bool Destory(string guid)
    {
        var removed = _sessions.TryRemove(guid, out _);
        return removed;
    }
}