using Jupiter1.Network.Common.Structures;
using Jupiter1.Network.Server.Constants;
using Jupiter1.Network.Server.Enums;
using Jupiter1.Network.Server.Structures;

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
        public Entity[] Entities { get; set; }

        public PlayerState[] PlayerStates { get; set; }

        public int RestartTime { get; set; }
        #endregion

        public ServerLocalService()
        {
            Entities = new Entity[ServerConstants.MaxGameEntities];
        }
    }
}