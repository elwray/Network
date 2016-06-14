namespace Jupiter1.Network.Server.Constants
{
    public static class ServerConstants
    {
        // There needs to be enough loopback messages to hold a complete
        // gamestate of maximum size
        public const int MaxLoopbackMessages = 16;

        public const int PacketsBackup = 32;

        public const int DefaultFps = 10;
    }
}