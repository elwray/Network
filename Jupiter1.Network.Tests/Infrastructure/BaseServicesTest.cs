using Jupiter1.Network.Common.Services.DependencyService;
using Jupiter1.Network.Tests.Helpers;

namespace Jupiter1.Network.Tests.Infrastructure
{
    public abstract class BaseServicesTests<TDependencyService, TConfiguration, TConfigurationHelper>
        where TDependencyService : IDependencyService, new()
        where TConfiguration : class
        where TConfigurationHelper : BaseConfigurationHelper<TConfiguration>, new()
    {
        private readonly TDependencyService _dependencyService;

        protected BaseServicesTests()
        {
            var helper = new TConfigurationHelper();
            var configuration = helper.CreateConfiguration();
            _dependencyService = new TDependencyService();
            _dependencyService.Initialize(configuration, false);
        }

        protected void RegisterSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _dependencyService.RegisterSingleton<TService, TImplementation>();
        }

        protected void RegisterSingleton<TService>(TService instance) where TService : class
        {
            _dependencyService.RegisterSingleton(instance);
        }

        protected TService GetSingleton<TService>() where TService : class
        {
            return _dependencyService.GetSingleton<TService>();
        }
    }
}