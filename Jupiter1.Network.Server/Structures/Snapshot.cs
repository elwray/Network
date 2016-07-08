namespace Jupiter1.Network.Server.Structures
{
    internal sealed class Snapshot
    {
        public int SentTime { get; set; }    // Time the message was transmitted.
        public int AckedTime { get; set; }   // Time the message was acked.
        public int MessageSize { get; set; } // Used to rate drop packets.

        public int EntitiesCount { get; set; }
        public int FirstEntity { get; set; }    // Into the circular sv_packet_entities[] the entities MUST be in
                                                // increasing state number order, otherwise the delta compression will
                                                // fail.
        public object PlayerState { get; set; }
    }
}

//int areabytes;
//byte areabits[MAX_MAP_AREA_BYTES];      // portalarea visibility bits
//playerState_t ps;