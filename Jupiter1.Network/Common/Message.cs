using System;

namespace Jupiter1.Network.Common
{
    public static class Message
    {
        public static void WriteInt16(Msg message, short value)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
        }

        public static void WriteInt32(Msg message, int value)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
        }

        public static void WriteData(Msg message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
        }
    }
}