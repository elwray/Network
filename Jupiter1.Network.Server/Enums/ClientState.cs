namespace Jupiter1.Network.Server.Enums
{
    public enum ClientState
    {
        Free, // Can be reused for a new connection.
        Zombie, // Client has been disconnected, but don't reuse connection for a couple seconds.
        Connected, // Has been assigned to a client_t, but no gamestate yet.
        Primed, // Gamestate has been sent, but client hasn't sent a usercmd.
        Active // Client is fully in game.
    }
}
