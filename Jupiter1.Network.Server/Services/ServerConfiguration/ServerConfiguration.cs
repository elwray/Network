namespace Jupiter1.Network.Server.Services.ServerConfiguration
{
    public class ServerConfiguration : IServerConfiguration
    {
        public int ClientTimeout { get; set; }
        public int ClientZombieTime { get; set; }
        public int MaxClientsCount { get; set; }
        public bool IsPaused { get; set; }
        public bool IsRunning { get; set; }
    }
}