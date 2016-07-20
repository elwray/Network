namespace Jupiter1.Network.Server.Structures
{
    internal sealed class SharedEntity
    {
        public EntityState S { get; set; } // communicated by server to clients
        public EntityShared R { get; set; } // shared by both the server system and game
    }
}