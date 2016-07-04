namespace Jupiter1.Network.Common.Structures
{
    // UserCommand is sent to the server each client frame.
    public sealed class UserCommand
    {
        public int ServerTime { get; set; }
    }
}

//// usercmd_t is sent to the server each client frame
//typedef struct usercmd_s
//{
//    int serverTime;
//    int angles[3];
//    int buttons;
//    byte weapon;           // weapon 
//    signed char forwardmove, rightmove, upmove;
//}
//usercmd_t;