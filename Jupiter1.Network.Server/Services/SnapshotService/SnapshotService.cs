using System;
using Jupiter1.Network.Server.Enums;
using Jupiter1.Network.Server.Services.ServerStaticService;
using Jupiter1.Network.Server.Structures;

namespace Jupiter1.Network.Server.Services.SnapshotService
{
    internal class SnapshotService : ISnapshotService
    {
        private readonly IServerStaticService _serverStaticService;

        public SnapshotService(IServerStaticService serverStaticService)
        {
            if (serverStaticService == null)
                throw new ArgumentNullException(nameof(serverStaticService));

            _serverStaticService = serverStaticService;
        }

        private void SendSnapshotToClient(Client client)
        {
        }

        #region ISnapshotService
        public void SendClientsMessages()
        {
            foreach (var client in _serverStaticService.Clients)
            {
                // Client not connected.
                if (client.State == ClientState.Free)
                    continue;

                // Not time yet.
                if (_serverStaticService.Time < client.NextSnapshotTime)
                    continue;

                // Send additional message fragments if the last message was too large to send at once.
                if (client.Channel.HasUnsentFragments)
                {
                    // TODO:
                    //c->nextSnapshotTime = svs.time +
                    //    SV_RateMsec(c, c->netchan.unsentLength - c->netchan.unsentFragmentStart);
                    //SV_Netchan_TransmitNextFragment(c);
                    //continue;
                }

                // Generate and send a new message.
                SendSnapshotToClient(client);
            }
        }
        #endregion
    }
}