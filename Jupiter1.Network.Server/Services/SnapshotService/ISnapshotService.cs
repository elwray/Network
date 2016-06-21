using Jupiter1.Network.Common.Structures;
using Jupiter1.Network.Server.Structures;

namespace Jupiter1.Network.Server.Services.SnapshotService
{
    internal interface ISnapshotService
    {
        void SendMessageToClient(Client client, Message message);
        void SendClientsMessages();
    }
}