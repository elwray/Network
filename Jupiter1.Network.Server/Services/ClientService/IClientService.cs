using Jupiter1.Network.Server.Structures;

namespace Jupiter1.Network.Server.Services.ClientService
{
    internal interface IClientService
    {
        // Called when the player is totally leaving the server, either willingly or unwillingly.This is NOT called if
        // the entire server is quiting or crashing -- SV_FinalMessage() will handle that
        void DropClient(Client client, string reason);
    }
}