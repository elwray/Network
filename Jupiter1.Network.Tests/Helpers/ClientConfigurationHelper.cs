using Jupiter1.Network.Client.Services.ClientConfiguration;

namespace Jupiter1.Network.Tests.Helpers
{
    public class ClientConfigurationHelper : BaseConfigurationHelper<IClientConfiguration>
    {
        #region BaseConfigurationHelper
        public override IClientConfiguration CreateConfiguration()
        {
            return new ClientConfiguration
            {
            };
        }
        #endregion
    }
}