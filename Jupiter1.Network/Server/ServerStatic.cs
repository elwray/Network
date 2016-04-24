using System.Net;
using Jupiter1.Network.Shared;

namespace Jupiter1.Network.Server
{
    /*
    // this structure will be cleared only when the game dll changes
    typedef struct {
        qboolean	initialized;				// sv_init has completed

        int			time;						// will be strictly increasing across level changes

        int			snapFlagServerBit;			// ^= SNAPFLAG_SERVERCOUNT every SV_SpawnServer()

        client_t	*clients;					// [sv_maxclients->integer];
        int			numSnapshotEntities;		// sv_maxclients->integer*PACKET_BACKUP*MAX_PACKET_ENTITIES
        int			nextSnapshotEntities;		// next snapshotEntities to use
        entityState_t	*snapshotEntities;		// [numSnapshotEntities]
        int			nextHeartbeatTime;
        challenge_t	challenges[MAX_CHALLENGES];	// to prevent invalid IPs from connecting
        netadr_t	redirectAddress;			// for rcon return messages

        netadr_t	authorizeAddress;			// for rcon return messages
    } serverStatic_t;
    */

    public sealed class ServerStatic
    {
        public bool Initialized { get; set; } // sv_init has completed

        public int Time { get; set; } // will be strictly increasing across level changes

        public int SnapFlagServerBit { get; set; } // ^= SNAPFLAG_SERVERCOUNT every SV_SpawnServer()

        public Client[] Clients { get; set; } // [sv_maxclients->integer];
        public int NumSnapshotEntities { get; set; } // sv_maxclients->integer*PACKET_BACKUP*MAX_PACKET_ENTITIES
        public int NextSnapshotEntities { get; set; } // next snapshotEntities to use
        public EntityState[] SnapshotEntities { get; set; } // [numSnapshotEntities]
        public int NextHeartbeatTime { get; set; }
        public Challenge[] Challenges { get; set; } // to prevent invalid IPs from connecting
        public IPEndPoint RedirectAddress { get; set; } // for rcon return messages

        public IPEndPoint AuthorizeAddress { get; set; } // for rcon return messages
    }
}