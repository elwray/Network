using Jupiter1.Network.Server.Services.ServerConfiguration;

namespace Jupiter1.Network.Server.Services.DependencyService
{
    internal interface IDependencyService
    {
        void Initialize(IServerConfiguration configuration);
        void RegisterSingleton<TService, TImplementation>() where TService : class
            where TImplementation: class, TService;
        void RegisterSingleton<TService>(TService instance) where TService : class;
        T GetSingleton<T>() where T : class;
    }
}