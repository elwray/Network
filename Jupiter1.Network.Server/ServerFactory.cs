using System;
using Jupiter1.Network.Common.Services.DependencyService;
using Jupiter1.Network.Server.Services.BotService;
using Jupiter1.Network.Server.Services.DependencyService;
using Jupiter1.Network.Server.Services.MasterService;
using Jupiter1.Network.Server.Services.ServerConfiguration;
using Jupiter1.Network.Server.Services.ServerService;
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
                _dependencyService = new ServerDependencyService();
                _dependencyService.Initialize(configuration);

                // Rebind IMasterService and IBotService.
                if (masterService != null)
                    _dependencyService.RegisterSingleton<IMasterService>(masterService);
                if (botService != null)
                    _dependencyService.RegisterSingleton<IBotService>(botService);

                // Initialize ISocketService. Bind port and start listen.
                var socketService = _dependencyService.GetSingleton<IServerSocketService>();
                socketService.Initialize();
            }

            return _dependencyService.GetSingleton<IServerService>();
        }
    }
}