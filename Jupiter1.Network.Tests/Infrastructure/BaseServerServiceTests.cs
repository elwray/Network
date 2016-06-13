using Jupiter1.Network.Server.Services.DependencyService;
using Jupiter1.Network.Tests.Helpers;

namespace Jupiter1.Network.Tests.Infrastructure
{
    public abstract class BaseServerServiceTests
    {
        private readonly DependencyService _dependencyService;

        protected BaseServerServiceTests()
        {
            var configuration = ServerConfigurationHelper.GetServerConfiguration();
            _dependencyService = new DependencyService();
            _dependencyService.Initialize(configuration);
        }

        protected void RegisterSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _dependencyService.RegisterSingleton<TService, TImplementation>();
        }

        protected void RegisterSingleton<TService>(TService instance) where TService : class
        {
            _dependencyService.RegisterSingleton<TService>(instance);
        }

        protected T GetSingleton<T>() where T : class
        {
            return _dependencyService.GetSingleton<T>();
        }
    }
}