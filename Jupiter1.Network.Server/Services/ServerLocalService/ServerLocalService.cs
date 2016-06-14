namespace Jupiter1.Network.Server.Services.ServerLocalService
{
    internal sealed class ServerLocalService : IServerLocalService
    {
        #region IServerLocalService
        public int ResidualTime { get; set; }
        public int RestartTime { get; set; }
        #endregion
    }
}