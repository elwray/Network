using Jupiter1.Network.Common.Structures;
using Jupiter1.Network.Server.Structures;

namespace Jupiter1.Network.Server.Services.ClientService
{
    internal interface IClientService
    {
        // Called when the player is totally leaving the server, either willingly or unwillingly.This is NOT called if
        // the entire server is quiting or crashing -- SV_FinalMessage() will handle that
        void DropClient(Client client, string reason);

        // Check to see if the client wants a file, open it if needed and start pumping the client Fill up msg with
        // data.
        void WriteDownloadToClient(Client client, Message message);
    }
}