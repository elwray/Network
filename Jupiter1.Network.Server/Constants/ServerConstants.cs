namespace Jupiter1.Network.Server.Constants
{
    public static class ServerConstants
    {
        // There needs to be enough loopback messages to hold a complete
        // gamestate of maximum size
        public const int MaxLoopbackMessages = 16;

        // Number of old messages that must be kept on client and server for delta comrpession and ping estimation.
        public const int PacketsBackup = 32;

        public const int PacketMask = PacketsBackup - 1;

        public const int DefaultFps = 10;

        // Include our header, IP header, and some overhead.
        public const int HeaderRateBytes = 48;

        // Max string commands buffered for restransmit.
        public const int MaxRaliableCommands = 64;

        public const int EncodeStart = 4;
        public const int DecodeStart = 12;

        public const int MaxSnapshotEntities = 1024;

        // Bit vector of area visibility.
        public const int MaxMapAreaBytes = 32;

        public const int MaxGameEntities = 1024;
    }
}