namespace Jupiter1.Network.Server.Services.ServerConfiguration
{
    public interface IServerConfiguration
    {
        int ClientTimeout { get; set; }
        int ClientZombieTime { get; set; }
        int MaxClientsCount { get; set; }
        bool IsPaused { get; set; }
        bool IsRunning { get; set; }
    }
}