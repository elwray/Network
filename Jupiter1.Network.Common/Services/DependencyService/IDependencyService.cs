namespace Jupiter1.Network.Common.Services.DependencyService
{
    public interface IDependencyService
    {
        void Initialize<TConfiguration>(TConfiguration configuration, bool verifyContainer = true)
            where TConfiguration : class;
        void RegisterSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService;
        void RegisterSingleton<TService>(TService instance) where TService : class;
        T GetSingleton<T>() where T : class;
    }
}