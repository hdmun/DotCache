using DotNetty.Codecs;
using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Libuv;
using System;
using System.Net;

namespace DotCache.Network;

public class TcpServer
{
    private IEventLoopGroup _dispatcherEventLoopGroup;
    private IEventLoopGroup _workerEventLoopGroup;
    private IChannel _channel;

    private TcpSessionManager _sessionManager;

    public TcpServer()
    {
    }

    public void Initialize(string ip, int port, TcpSessionManager sessionManager)
    {
        var dispatcher = new DispatcherEventLoopGroup();
        _dispatcherEventLoopGroup = dispatcher;
        _workerEventLoopGroup = new WorkerEventLoopGroup(dispatcher, Environment.ProcessorCount);

        var bootstrap = new ServerBootstrap();
        bootstrap.Group(_dispatcherEventLoopGroup, _workerEventLoopGroup);
        bootstrap.Channel<TcpServerChannel>();

        bootstrap
            .Option(ChannelOption.SoBacklog, 1000)
            .Option(ChannelOption.TcpNodelay, true)
            .Option(ChannelOption.SoReuseaddr, true)
            .ChildOption(ChannelOption.SoReuseaddr, true)
            .ChildOption(ChannelOption.TcpNodelay, true)
            .ChildOption(ChannelOption.SoKeepalive, true)
            .ChildHandler(new ActionChannelInitializer<IChannel>(OnInitializeChannel));

        _channel = bootstrap.BindAsync(IPAddress.Parse(ip), port).Result;

        _sessionManager = sessionManager;
    }

    public void Close()
    {
        _dispatcherEventLoopGroup.ShutdownGracefullyAsync().Wait();
        _workerEventLoopGroup.ShutdownGracefullyAsync().Wait();

        var task = _channel.CloseAsync();
        task.Wait();
    }

    private void OnInitializeChannel(IChannel channel)
    {
        IChannelPipeline p = channel.Pipeline;

        // "frameDecoder"
        // "decoder"

        // "frameEncoder"
        // "encoder"

        p.AddLast("handler", new TcpSessionChannelHandler(_sessionManager));
    }
}
