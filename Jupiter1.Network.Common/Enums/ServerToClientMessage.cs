namespace Jupiter1.Network.Common.Enums
{
    public enum ServerToClientMessage : byte
    {
        Bad,
        Nop,
        GameState,
        ConfigString,
        Baseline,
        ServerCommand,
        Download,
        Snapshot,
        EOF
    }
}