namespace Jupiter1.Network.Common.Structures
{
    // UserCommand is sent to the server each client frame.
    public sealed class UserCommand
    {
        public int ServerTime { get; set; }
        public int[] Angles { get; set; }
        public int Buttons { get; set; }
        public byte Weapon { get; set; }
        public char ForwardMove { get; set; }
        public char RightMove { get; set; }
        public char UpMove { get; set; }

        public UserCommand()
        {
            Angles = new int[3];
        }
    }
}