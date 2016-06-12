using System;
using Jupiter1.Network.Server.Structures;

namespace Jupiter1.Network.Server.Services.ClientService
{
    internal sealed class ClientService : IClientService
    {
        #region IClientService
        public void DropClient(Client client, string reason)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
        }
        #endregion
    }
}