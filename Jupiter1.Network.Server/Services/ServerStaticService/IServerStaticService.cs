using Jupiter1.Network.Common.Enums;
using Jupiter1.Network.Server.Structures;

namespace Jupiter1.Network.Server.Services.ServerStaticService
{
    // Persistant server info.
    internal interface IServerStaticService
    {
        Client[] Clients { get; }
        int Time { get; set; }
        int NextSnapshotEntities { get; set; }
        int SnapshotEntitiesCount { get; set; }
        SnapFlag SnapFlagServerBit { get; set; } // ^= SNAPFLAG_SERVERCOUNT every SV_SpawnServer()
    }
}