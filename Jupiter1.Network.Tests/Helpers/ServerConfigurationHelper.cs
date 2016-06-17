using Jupiter1.Network.Server.Services.ServerConfiguration;

namespace Jupiter1.Network.Tests.Helpers
{
    public class ServerConfigurationHelper : BaseConfigurationHelper<IServerConfiguration>
    {
        #region BaseConfigurationHelper
        public override IServerConfiguration CreateConfiguration()
        {
            return new ServerConfiguration
            {
            };
        }
        #endregion
    }
}