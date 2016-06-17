using Jupiter1.Network.Common.Services.DependencyService;
using SimpleInjector;

namespace Jupiter1.Network.Client.Services.DependencyService
{
    internal sealed class ClientDependencyService : BaseDependencyService
    {
        #region BaseDependencyService
        protected override void RegisterServices<TConfiguration>(Container container, TConfiguration configuration)
        {
        }
        #endregion
    }
}