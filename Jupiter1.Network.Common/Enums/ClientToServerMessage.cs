namespace Jupiter1.Network.Common.Enums
{
    public enum ClientToServerMessage : byte
    {
        Bad,
        Nop,
        Move,
        MoveNoDelta,
        ClientCommand,
        EOF
    }
}