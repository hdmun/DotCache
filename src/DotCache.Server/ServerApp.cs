using DotCache.Network;

namespace DotCache.Server;

public class ServerApp
{
    private readonly TcpServer _tcpServer;
    private readonly TcpSessionManager _tcpSessionManager;

    public ServerApp()
    {
        _tcpServer = new();
        _tcpSessionManager = new();
    }

    public void Initiallize(/* TODO : ServerConfig config*/)
    {
        var ip = "127.0.0.1";
        var port = 6379;
        _tcpServer.Initialize(ip, port, _tcpSessionManager);
    }

    public void Close()
    {
        _tcpServer.Close();
    }
}
