using Jupiter1.Network.Common.Enums;

namespace Jupiter1.Network.Server.Services.LoopbackService
{
    internal interface ILoopbackService
    {
        void SendPacket(NetworkSource networkSource, byte[] data, int length);
        bool GetPacket(NetworkSource networkSource, byte[] data, out int length);
    }
}