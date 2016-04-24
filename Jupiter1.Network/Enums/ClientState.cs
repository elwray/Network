namespace Jupiter1.Network.Enums
{
    public enum ClientState
    {
        Free, // can be reused for a new connection
        Zombie, // client has been disconnected, but don't reuse connection for a couple seconds
        Connected, // has been assigned to a client_t, but no gamestate yet
        Primed, // gamestate has been sent, but client hasn't sent a usercmd
        Active // client is fully in game
    }
}