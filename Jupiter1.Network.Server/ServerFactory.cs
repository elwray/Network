using System;
using Jupiter1.Network.Server.Services.BotService;
using Jupiter1.Network.Server.Services.ClientService;
using Jupiter1.Network.Server.Services.DependencyService;
using Jupiter1.Network.Server.Services.MasterService;
using Jupiter1.Network.Server.Services.ServerConfiguration;
using Jupiter1.Network.Server.Services.ServerService;
using Jupiter1.Network.Server.Services.ServerStaticService;
using Jupiter1.Network.Server.Services.SnapshotService;
using Jupiter1.Network.Server.Services.SocketService;
namespace Jupiter1.Network.Server
{
    public static class ServerFactory
    {
        private static IDependencyService _dependencyService;

        public static IServerService GetService(IServerConfiguration configuration, IMasterService masterService = null,
            IBotService botService = null)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if (_dependencyService == null)
            {
                _dependencyService = new DependencyService();
                _dependencyService.Initialize(configuration);
            }

            return _dependencyService.GetInstance<IServerService>();
        }
    }
}