namespace Jupiter1.Network.Server.Services.SocketService
{
    internal sealed class LoopbackMessage
    {
        public byte[] Data { get; }

        public int Length { get; set; }

        public LoopbackMessage()
        {
            Data = new byte[Common.Constants.MaxPacketLength];
        }
    }
}