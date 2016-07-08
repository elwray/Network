namespace Jupiter1.Network.Server.Services.ServerConfiguration
{
    public class ServerConfiguration : IServerConfiguration
    {
        #region IServerConfiguration
        public int ClientTimeout { get; set; }
        public int ClientZombieTime { get; set; }
        public int MaxClientsCount { get; set; }
        public bool IsPaused { get; set; }
        public bool IsRunning { get; set; }
        public int Fps { get; set; }
        public int MaxRate { get; set; }
        public bool LanForceRate { get; set; }
        public int? PadPackets { get; set; }
        #endregion
    }
}