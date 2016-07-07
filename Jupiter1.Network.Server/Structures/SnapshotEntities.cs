using Jupiter1.Network.Server.Constants;

namespace Jupiter1.Network.Server.Structures
{
    internal sealed class SnapshotEntities
    {
        public int Count { get; set; }
        public int[] Ids { get; set; }

        public SnapshotEntities()
        {
            Ids = new int[ServerConstants.MaxSnapshotEntities];
        }
    }
}