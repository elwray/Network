using System.Net;

namespace Jupiter1.Network.Server
{
    /*
    typedef struct {
        netadr_t	adr;
        int			challenge;
        int			time;				// time the last packet was sent to the autherize server
        int			pingTime;			// time the challenge response was sent to client
        int			firstTime;			// time the adr was first used, for authorize timeout checks
        qboolean	connected;
    } challenge_t;
    */

    public sealed class ChallengeInfo
    {
        public IPEndPoint Address { get; set; }
        public int GameChallenge { get; set; }
        public int Time { get; set; } // time the last packet was sent to the autherize server
        public int PingTime { get; set; } // time the challenge response was sent to client
        public int FirstTime { get; set; } // time the adr was first used, for authorize timeout checks
        public bool Connected { get; set; }
    }
}