using Jupiter1.Network.Common.Structures;
using Jupiter1.Network.Server.Enums;
using Jupiter1.Network.Server.Structures;

namespace Jupiter1.Network.Server.Services.ServerLocalService
{
    internal interface IServerLocalService
    {
        ServerState State { get; set; }
        bool Restarting { get; set; }          // If true, send configstring changes during SS_LOADING.
        int ServerId { get; set; }             // Changes each server start.
        int RestartedServerId { get; set; }    // ServerId before a map_restart.
        int ChecksumFeed { get; set; }
        int ChecksumFeedServerId { get; set; } // The feed key that we use to compute the pure checksum strings
                                               // https://zerowing.idsoftware.com/bugzilla/show_bug.cgi?id=475
                                               // the serverId associated with the current checksumFeed
                                               // (always <= serverId).
        int SnapshotCounter { get; set; }      // Incremented for each snapshot built.
        int ResidualTime { get; set; }         // <= 1000 / sv_frame->value
        int NextFrameTime { get; set; }        // When time > nextFrameTime, process world.
        //struct cmodel_s * models[MAX_MODELS];
        //char* configstrings[MAX_CONFIGSTRINGS];
        Entity[] Entities { get; set; }

        //char* entityParsePoint; // used during game VM init

        //// the game virtual machine will update these on init and changes
        //sharedEntity_t* gentities;
        //int gentitySize;
        //int num_entities;       // current number, <= MAX_GENTITIES

        PlayerState[] PlayerStates { get; set; }
        //playerState_t* gameClients;
        //int gameClientSize;     // will be > sizeof(playerState_t) due to game private data

        int RestartTime { get; set; }
    }
}