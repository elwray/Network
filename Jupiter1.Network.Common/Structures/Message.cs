namespace Jupiter1.Network.Common.Structures
{
    public class Message
    {
        public byte[] Data { get; set; }
        public int Length { get; set; }

        public Message()
        {
        }

        public Message(int length)
        {
            Data = new byte[length];
        }
    }
}