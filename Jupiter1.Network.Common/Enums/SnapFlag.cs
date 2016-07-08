using System;

namespace Jupiter1.Network.Common.Enums
{
    [Flags]
    public enum SnapFlag : byte
    {
        RateDelayed = 1,
        NotActive = 1 << 1,  // Snapshot used during connection and for zombies.
        ServerCount = 1 << 2 // Toggled every map_restart so transitions can be detected.
    }
}