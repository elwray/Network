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

        private void SendSnapshotToClient(Client client)
        {
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