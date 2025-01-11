using DotNetty.Transport.Channels;

namespace DotCache.Network;

internal class TcpSessionChannelHandler : ChannelHandlerAdapter
{
    private TcpSessionManager _sessionManager;
    private IChannelHandlerContext _ctx;
    private TcpSession _session;

    public IChannelHandlerContext Ctx => _ctx;

    public TcpSessionChannelHandler(TcpSessionManager sessionManager)
    {
        _sessionManager = sessionManager;
    }

    public override void ChannelActive(IChannelHandlerContext context)
    {
        base.ChannelActive(context);

        _ctx = context;
        _session = _sessionManager.Create();
    }

    public override void ChannelInactive(IChannelHandlerContext context)
    {
        base.ChannelInactive(context);

        _sessionManager.Destory(_session.Guid);
        _sessionManager = null;
        _session = null;
        _ctx = null;
    }

    public override void ChannelRead(IChannelHandlerContext context, object message)
    {
        base.ChannelRead(context, message);
    }
}
