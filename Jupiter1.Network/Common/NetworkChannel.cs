using System;

namespace Jupiter1.Network.Common
{
    public static class NetworkChannel
    {
        public static void Transmit(NetworkChannelInfo channel, int length, byte[] data)
        {
            if (channel == null)
                throw new ArgumentNullException(nameof(channel));
            if (length <= 0)
                throw new ArgumentException(nameof(length));
            if (data == null)
                throw new ArgumentNullException(nameof(data));
        }

        public static void TransmitNextFragment(NetworkChannelInfo channel)
        {
            if (channel == null)
                throw new ArgumentNullException(nameof(channel));
        }

        public static bool Process(NetworkChannelInfo channel, Msg message)
        {
            if (channel == null)
                throw new ArgumentNullException(nameof(channel));
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            return false;
        }
    }
}