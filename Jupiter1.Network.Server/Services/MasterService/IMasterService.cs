namespace Jupiter1.Network.Server.Services.MasterService
{
    public interface IMasterService
    {
        void Heartbeat();
        void Shutdown();
    }
}