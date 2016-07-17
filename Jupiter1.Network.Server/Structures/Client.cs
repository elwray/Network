using Jupiter1.Network.Common.Structures;
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
        public int Rate { get; set; }
        public Snapshot[] Snapshots { get; }
        public NetworkChannel Channel { get; set; }

        public SharedEntity GameEntity { get; set; }

        public string[] ReliableCommands { get; set; }
        public int ReliableSequence { get; set; }
        public int ReliableAcknowledge { get; set; }
        public int ReliableSent { get; set; }

        public bool RateDelayed { get; set; }
        public int SnapshotMsec { get; set; }

        public string DownloadName { get; set; }

        // Reliable client message sequence.
        public int LastClientCommand { get; set; }

        public int DeltaMessage { get; set; }

        public Client()
        {
            Snapshots = new Snapshot[ServerConstants.PacketsBackup];
            ReliableCommands = new string[ServerConstants.MaxRaliableCommands];
        }
    }
}

//typedef struct client_s
//{
//    clientState_t state;
//    char userinfo[MAX_INFO_STRING];     // name, etc

//    char reliableCommands[MAX_RELIABLE_COMMANDS][MAX_STRING_CHARS];
//	int reliableSequence;       // last added reliable message, not necesarily sent or acknowledged yet
//    int reliableAcknowledge;    // last acknowledged reliable message
//    int reliableSent;           // last sent reliable message, not necesarily acknowledged yet
//    int messageAcknowledge;

//    int gamestateMessageNum;    // netchan->outgoingSequence of gamestate
//    int challenge;

//    usercmd_t lastUsercmd;
//    int lastMessageNum;     // for delta compression
//    int lastClientCommand;  // reliable client message sequence
//    char lastClientCommandString[MAX_STRING_CHARS];
//    sharedEntity_t* gentity;            // SV_GentityNum(clientnum)
//    char name[MAX_NAME_LENGTH];         // extracted from userinfo, high bits masked

//    // downloading
//    char downloadName[MAX_QPATH]; // if not empty string, we are downloading
//    fileHandle_t download;          // file being downloaded
//    int downloadSize;       // total bytes (can't use EOF because of paks)
//    int downloadCount;      // bytes sent
//    int downloadClientBlock;    // last block we sent to the client, awaiting ack
//    int downloadCurrentBlock;   // current block number
//    int downloadXmitBlock;  // last block we xmited
//    unsigned char* downloadBlocks[MAX_DOWNLOAD_WINDOW]; // the buffers for the download blocks
//    int downloadBlockSize[MAX_DOWNLOAD_WINDOW];
//    qboolean downloadEOF;       // We have sent the EOF block
//    int downloadSendTime;   // time we last got an ack from the client

//    int deltaMessage;       // frame last client usercmd message
//    int nextReliableTime;   // svs.time when another reliable command will be allowed
//    int lastPacketTime;     // svs.time when packet was last received
//    int lastConnectTime;    // svs.time when connection started
//    int nextSnapshotTime;   // send another snapshot when svs.time >= nextSnapshotTime
//    qboolean rateDelayed;       // true if nextSnapshotTime was set based on rate instead of snapshotMsec
//    int timeoutCount;       // must timeout a few frames in a row so debugging doesn't break
//    clientSnapshot_t frames[PACKET_BACKUP]; // updates can be delta'd from here
//    int ping;
//    int rate;               // bytes / second
//    int snapshotMsec;       // requests a snapshot every snapshotMsec unless rate choked
//    int pureAuthentic;
//    qboolean gotCP; // TTimo - additional flag to distinguish between a bad pure checksum, and no cp command at all
//    netchan_t netchan;
//    // TTimo
//    // queuing outgoing fragmented messages to send them properly, without udp packet bursts
//    // in case large fragmented messages are stacking up
//    // buffer them into this queue, and hand them out to netchan as needed
//    netchan_buffer_t* netchan_start_queue;
//    netchan_buffer_t** netchan_end_queue;
//}
//client_t;