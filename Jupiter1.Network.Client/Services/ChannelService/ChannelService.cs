using System;
using Jupiter1.Network.Client.Services.ClientConfiguration;
using Jupiter1.Network.Common.Enums;
using Jupiter1.Network.Common.Extensions;
using Jupiter1.Network.Common.Services.ChannelService;
using Jupiter1.Network.Common.Structures;

namespace Jupiter1.Network.Client.Services.ChannelService
{
    internal class ChannelService : BaseChannelService
    {
        private readonly IClientConfiguration _configuration;

        public ChannelService(IClientConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            _configuration = configuration;
        }

        public override void SendPacket(NetworkSource networkSource, NetworkAddress to, Message message)
        {
        }

        public override void WriteClientQPort(Message message)
        {
            message.WriteUInt16((ushort) _configuration.QPort);
        }
    }
}