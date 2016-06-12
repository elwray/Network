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

namespace Jupiter1.Network.Server.Services.DependencyService
{
    internal sealed class DependencyService : IDependencyService
    {
        private Container _container;

        #region IDependencyService
        public void Initialize(IServerConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            if (_container != null)
                throw new InvalidOperationException();

            _container = new Container
            {
                Options = { AllowOverridingRegistrations = true }
            };

            RegisterServices(_container, configuration);

#if DEBUG
            _container.Verify();
#endif
        }

        public void RegisterSingleton<TService, TImplementation>() where TService : class
            where TImplementation : class, TService
        {
            _container.RegisterSingleton<TService, TImplementation>();
        }

        public void RegisterSingleton<TService>(TService instance) where TService : class
        {
            _container.RegisterSingleton<TService>(instance);
        }

        public T GetInstance<T>() where T : class
        {
            return _container.GetInstance<T>();
        }
        #endregion

        private void RegisterServices(Container container, IServerConfiguration configuration)
        {
            container.RegisterSingleton<IBotService, NullBotService>();
            container.RegisterSingleton<IClientService, ClientService.ClientService>();
            container.RegisterSingleton<IMasterService, NullMasterService>();
            container.RegisterSingleton<IServerConfiguration>(configuration);
            container.RegisterSingleton<IServerService, ServerService.ServerService>();
            container.RegisterSingleton<IServerStaticService, ServerStaticService.ServerStaticService>();
            container.RegisterSingleton<ISnapshotService, SnapshotService.SnapshotService>();
            container.RegisterSingleton<ISocketService, SocketService.SocketService>();
        }
    }
}