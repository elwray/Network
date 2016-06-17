using System;
using Jupiter1.Network.Common.Services.ChannelService;
using Jupiter1.Network.Common.Services.DependencyService;
using Jupiter1.Network.Server.Services.BotService;
using Jupiter1.Network.Server.Services.ChannelService;
using Jupiter1.Network.Server.Services.ClientService;
using Jupiter1.Network.Server.Services.LoopbackService;
using Jupiter1.Network.Server.Services.MasterService;
using Jupiter1.Network.Server.Services.ServerLocalService;
using Jupiter1.Network.Server.Services.ServerService;
using Jupiter1.Network.Server.Services.ServerStaticService;
using Jupiter1.Network.Server.Services.SnapshotService;
using Jupiter1.Network.Server.Services.SocketService;
using SimpleInjector;

namespace Jupiter1.Network.Server.Services.DependencyService
{
    public sealed class ServerDependencyService : BaseDependencyService
    {
        #region BaseDependencyService
        protected override void RegisterServices<TConfiguration>(Container container, TConfiguration configuration)
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            container.RegisterSingleton<IBotService, NullBotService>();
            container.RegisterSingleton<IClientService, ClientService.ClientService>();
            container.RegisterSingleton<ILoopbackService, LoopbackService.LoopbackService>();
            container.RegisterSingleton<IMasterService, NullMasterService>();
            container.RegisterSingleton<IChannelService, ChannelService.ChannelService>();
            container.RegisterSingleton<IServerChannelService, ServerChannelService>();
            container.RegisterSingleton(configuration);
            container.RegisterSingleton<IServerLocalService, ServerLocalService.ServerLocalService>();
            container.RegisterSingleton<IServerService, ServerService.ServerService>();
            container.RegisterSingleton<IServerStaticService, ServerStaticService.ServerStaticService>();
            container.RegisterSingleton<ISnapshotService, SnapshotService.SnapshotService>();
            container.RegisterSingleton<IServerSocketService, ServerSocketService>();
        }
        #endregion
    }
}