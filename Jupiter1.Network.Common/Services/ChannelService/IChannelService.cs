using Jupiter1.Network.Common.Structures;

namespace Jupiter1.Network.Common.Services.ChannelService
{
    public interface IChannelService
    {
        bool Process(NetworkChannel channel, Message message);
        void Transmit(NetworkChannel channel, byte[] data, int length);
        void TransmitNext(NetworkChannel channel);
    }
}