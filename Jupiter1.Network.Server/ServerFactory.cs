using System;
using Jupiter1.Network.Server.Services.BotService;
using Jupiter1.Network.Server.Services.ClientService;
using Jupiter1.Network.Server.Services.MasterService;
using Jupiter1.Network.Server.Services.ServerConfiguration;
using Jupiter1.Network.Server.Services.ServerService;
using Jupiter1.Network.Server.Services.ServerStaticService;
using Jupiter1.Network.Server.Services.SnapshotService;
using Jupiter1.Network.Server.Services.SocketService;
using SimpleInjector;

namespace Jupiter1.Network.Server
{
    public class ServerFactory
    {
        private static Container _container;

        private static void RegisterServices(Container container, IServerConfiguration configuration)
        {
            container.RegisterSingleton<IBotService, NullBotService>();
            container.RegisterSingleton<IClientService, ClientService>();
            container.RegisterSingleton<IMasterService, NullMasterService>();
            container.RegisterSingleton<IServerConfiguration>(configuration);
            container.RegisterSingleton<IServerService, ServerService>();
            container.RegisterSingleton<IServerStaticService, ServerStaticService>();
            container.RegisterSingleton<ISnapshotService, SnapshotService>();
            container.RegisterSingleton<ISocketService, SocketService>();
        }

        public static IServerService GetService(IServerConfiguration configuration, IMasterService masterService = null,
            IBotService botService = null)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if (_container == null)
            {
                _container = new Container
                {
                    Options =
                    {
                        AllowOverridingRegistrations = true
                    }
                };

                RegisterServices(_container, configuration);

                if (masterService != null)
                    _container.RegisterSingleton<IMasterService>(masterService);
                if (botService != null)
                    _container.RegisterSingleton<IBotService>(botService);

#if DEBUG
                _container.Verify();
#endif
            }

            return _container.GetInstance<IServerService>();
        }
    }
}