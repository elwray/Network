namespace Jupiter1.Network.Server.Services.MasterService
{
    public sealed class NullMasterService : IMasterService
    {
        #region IMasterService
        public void Heartbeat()
        {
        }

        public void Shutdown()
        {
        }
        #endregion
    }
}