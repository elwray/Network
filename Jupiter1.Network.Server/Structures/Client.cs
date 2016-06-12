using Jupiter1.Network.Server.Constants;
using Jupiter1.Network.Server.Enums;

namespace Jupiter1.Network.Server.Structures
{
    internal sealed class Client
    {
        public ClientState State { get; set; }
        public ClientType Type { get; set; }
        public int LastPacketTime { get; set; }
        public int TimeoutCount { get; set; }
        public int NextSnapshotTime { get; set; }
        public int Ping { get; set; }
        public Snapshot[] Snapshots { get; }

        public Client()
        {
            Snapshots = new Snapshot[ServerConstants.PacketsBackup];
        }
    }
}