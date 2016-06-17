using Jupiter1.Network.Server.Services.DependencyService;
using Jupiter1.Network.Server.Services.ServerConfiguration;
using Jupiter1.Network.Tests.Helpers;

namespace Jupiter1.Network.Tests.Infrastructure
{
    public abstract class BaseServerServicesTest :
        BaseServicesTests<ServerDependencyService, IServerConfiguration, ServerConfigurationHelper>
    {
    }
}