using Jupiter1.Network.Server.Enums;

namespace Jupiter1.Network.Server.Services.ServerLocalService
{
    internal sealed class ServerLocalService : IServerLocalService
    {
        #region IServerLocalService
        public ServerState State { get; set; }
        public bool Restarting { get; set; }
        public int ServerId { get; set; }
        public int RestartedServerId { get; set; }
        public int ChecksumFeed { get; set; }
        public int ChecksumFeedServerId { get; set; }
        public int SnapshotCounter { get; set; }
        public int ResidualTime { get; set; }
        public int NextFrameTime { get; set; }

        public int RestartTime { get; set; }
        #endregion
    }
}