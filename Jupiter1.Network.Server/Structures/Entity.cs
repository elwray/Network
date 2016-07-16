namespace Jupiter1.Network.Server.Structures
{
    //#define	MAX_ENT_CLUSTERS	16

    internal sealed class Entity
    {
        //    struct worldSector_s *worldSector;
        //    struct svEntity_s *nextEntityInWorldSector;

        //    entityState_t baseline;     // for delta compression of initial sighting
        //    int numClusters;        // if -1, use headnode instead
        //    int clusternums[MAX_ENT_CLUSTERS];
        //    int lastCluster;        // if all the clusters don't fit in clusternums
        //    int areanum, areanum2;
        public int SnapshotCounter { get; set; } // Used to prevent double adding from portal views.
    }
}