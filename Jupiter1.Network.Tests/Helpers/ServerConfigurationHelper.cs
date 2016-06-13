using Jupiter1.Network.Server.Services.ServerConfiguration;

namespace Jupiter1.Network.Tests.Helpers
{
    public static class ServerConfigurationHelper
    {
        public static IServerConfiguration GetServerConfiguration()
        {
            return new ServerConfiguration
            {
            };
        }
    }
}