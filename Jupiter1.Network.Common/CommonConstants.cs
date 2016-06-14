namespace Jupiter1.Network.Common
{
    public static class CommonConstants
    {
        public const int MaxPacketLength = 1400;
        public const int FragmentSize = MaxPacketLength - 100;
        // Two ints and a short.
        public const int PacketHeaderSize = 10;

        public const int FragmentBit = 1 << 31;
    }
}