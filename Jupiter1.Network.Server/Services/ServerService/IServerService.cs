namespace Jupiter1.Network.Server.Services.ServerService
{
    public interface IServerService
    {
        void Frame(int ellapsedMilliseconds);
        void Shutdown();
    }
}