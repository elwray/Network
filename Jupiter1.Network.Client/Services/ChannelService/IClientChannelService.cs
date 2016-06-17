using Jupiter1.Network.Common.Structures;

namespace Jupiter1.Network.Client.Services.ChannelService
{
    internal interface IClientChannelService
    {
        void Transmit(NetworkChannel channel, Message message);
        void TransmitNext(NetworkChannel channel);
        bool Process(NetworkChannel channel, Message message);
    }
}