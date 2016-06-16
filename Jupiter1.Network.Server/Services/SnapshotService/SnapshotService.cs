using System;
using Jupiter1.Network.Server.Constants;
using Jupiter1.Network.Server.Enums;
using Jupiter1.Network.Server.Services.ChannelService;
using Jupiter1.Network.Server.Services.ServerConfiguration;
using Jupiter1.Network.Server.Services.ServerStaticService;
using Jupiter1.Network.Server.Structures;

namespace Jupiter1.Network.Server.Services.SnapshotService
{
    internal class SnapshotService : ISnapshotService
    {
        private readonly IServerConfiguration _configuration;
        private readonly IServerStaticService _serverStaticService;
        private readonly IServerChannelService _serverChannelService;

        public SnapshotService(IServerConfiguration configuration, IServerStaticService serverStaticService,
            IServerChannelService serverChannelService)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            if (serverStaticService == null)
                throw new ArgumentNullException(nameof(serverStaticService));
            if (serverChannelService == null)
                throw new ArgumentNullException(nameof(serverChannelService));

            _configuration = configuration;
            _serverStaticService = serverStaticService;
            _serverChannelService = serverChannelService;
        }

        private void BuildSnapshotToClient(Client client)
        {
        }

        private void UpdateServerCommandsToClient()
        {
        }

        private void WriteSnapshotToClient()
        {
        }

        private void WriteDownloadToClient()
        {
        }

        private void SendMessageToClient()
        {
        }

        private void SendSnapshotToClient(Client client)
        {
            //byte msg_buf[MAX_MSGLEN];
            //msg_t msg;

            //// build the snapshot
            //SV_BuildClientSnapshot(client);

            //// bots need to have their snapshots build, but
            //// the query them directly without needing to be sent
            //if (client->gentity && client->gentity->r.svFlags & SVF_BOT)
            //{
            //    return;
            //}

            //MSG_Init(&msg, msg_buf, sizeof(msg_buf));
            //msg.allowoverflow = qtrue;

            //// NOTE, MRE: all server->client messages now acknowledge
            //// let the client know which reliable clientCommands we have received
            //MSG_WriteLong(&msg, client->lastClientCommand);

            //// (re)send any reliable server commands
            //SV_UpdateServerCommandsToClient(client, &msg);

            //// send over all the relevant entityState_t
            //// and the playerState_t
            //SV_WriteSnapshotToClient(client, &msg);

            //// Add any download data if the client is downloading
            //SV_WriteDownloadToClient(client, &msg);

            //// check for overflow
            //if (msg.overflowed)
            //{
            //    Com_Printf("WARNING: msg overflowed for %s\n", client->name);
            //    MSG_Clear(&msg);
            //}

            //SV_SendMessageToClient(&msg, client);
        }

        // Return the number of msec a given size message is supposed to take to clear, based on the current rate.
        private int GetSupposedTimeToSent(Client client, int messageSize)
        {
            // Individual messages will never be larger than fragment size.
            if (messageSize > 1500)
            {
                // TODO: log too long message.
                messageSize = 1500;
            }

            if (_configuration.MaxRate > 1000)
                _configuration.MaxRate = 1000;

            var rate = client.Rate;
            if (rate > _configuration.MaxRate)
                rate = _configuration.MaxRate;

            return (messageSize + ServerConstants.HeaderRateBytes) * 1000 / rate;
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
                    var totalTimeToSent = GetSupposedTimeToSent(client,
                        client.Channel.UnsentLength - client.Channel.UnsentFragmentStart);
                    client.NextSnapshotTime = _serverStaticService.Time + totalTimeToSent;

                    _serverChannelService.TransmitNext(client);

                    continue;
                }

                // Generate and send a new message.
                SendSnapshotToClient(client);
            }
        }
        #endregion
    }
}