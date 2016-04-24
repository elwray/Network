using System;

namespace Jupiter1.Network.Common
{
    public static class Channel
    {
        public static void Transmit(NetworkChannel channel, int length, byte[] data)
        {
            if (channel == null)
                throw new ArgumentNullException(nameof(channel));
            if (length <= 0)
                throw new ArgumentException(nameof(length));
            if (data == null)
                throw new ArgumentNullException(nameof(data));
        }

        public static void TransmitNextFragment(NetworkChannel channel)
        {
            if (channel == null)
                throw new ArgumentNullException(nameof(channel));
        }

        public static bool Process(NetworkChannel channel, Msg message)
        {
            if (channel == null)
                throw new ArgumentNullException(nameof(channel));
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            return false;
        }
    }
}