using System;
using Jupiter1.Network.Common.Structures;
using Jupiter1.Network.Server.Structures;

namespace Jupiter1.Network.Server.Services.ChannelService
{
    internal class ServerChannelService : IServerChannelService
    {
        public void Transmit(Client client, Message message)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (message == null)
                throw new ArgumentNullException(nameof(message));
        }

        public void TransmitNext(Client client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
        }

        public bool Process(Client client, Message message)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            return true;
        }
    }
}