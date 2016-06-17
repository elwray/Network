using System;
using Jupiter1.Network.Common.Services.ChannelService;
using Jupiter1.Network.Common.Structures;

namespace Jupiter1.Network.Client.Services.ChannelService
{
    internal sealed class ClientChannelService : IClientChannelService
    {
        private readonly IChannelService _channelService;

        public ClientChannelService(IChannelService channelService)
        {
            if (channelService == null)
                throw new ArgumentNullException(nameof(channelService));

            _channelService = channelService;
        }

        #region IClientChannelService
        public void Transmit(NetworkChannel channel, Message message)
        {
            if (channel == null)
                throw new ArgumentNullException(nameof(channel));
            if (message == null)
                throw new ArgumentNullException(nameof(message));
        }

        public void TransmitNext(NetworkChannel channel)
        {
            if (channel == null)
                throw new ArgumentNullException(nameof(channel));

            _channelService.TransmitNext(channel);
        }

        public bool Process(NetworkChannel channel, Message message)
        {
            if (channel == null)
                throw new ArgumentNullException(nameof(channel));
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            return false;
        }
        #endregion
    }
}