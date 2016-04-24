using System.Security.Cryptography;
using Jupiter1.Network.Shared;

namespace Jupiter1.Network.Server
{
    /*
    typedef struct {
        int				areabytes;
        byte			areabits[MAX_MAP_AREA_BYTES];		// portalarea visibility bits
        playerState_t	ps;
        int				num_entities;
        int				first_entity;		// into the circular sv_packet_entities[]
									        // the entities MUST be in increasing state number
									        // order, otherwise the delta compression will fail
        int				messageSent;		// time the message was transmitted
        int				messageAcked;		// time the message was acked
        int				messageSize;		// used to rate drop packets
    } clientSnapshot_t;
    */

    public sealed class ClientSnapshot
    {
        public int AreaBytes { get; set; }
        public byte[] AreaBits { get; set; } // [MAX_MAP_AREA_BYTES] // portalarea visibility bits
        public PlayerState PlayerState { get; set; }
        public int NumEntities { get; set; }
        public int FirstEntity { get; set; } // into the circular sv_packet_entities[] the entities MUST be in
                                             // increasing state number order, otherwise the delta compression will fail
        public int MessageSent { get; set; } // time the message was transmitted
        public int MessageAcked { get; set; } // time the message was acked
        public int MessageSize { get; set; } // used to rate drop packets
    }
}