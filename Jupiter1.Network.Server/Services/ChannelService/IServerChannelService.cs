using Jupiter1.Network.Common.Structures;
using Jupiter1.Network.Server.Structures;

namespace Jupiter1.Network.Server.Services.ChannelService
{
    internal interface IServerChannelService
    {
        void Transmit(Client client, Message message);
        void TransmitNext(Client client);
        bool Process(Client client, Message message);
    }
}