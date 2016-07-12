using System;
using Jupiter1.Network.Common.Enums;
using Jupiter1.Network.Common.Structures;

namespace Jupiter1.Network.Common.Helpers
{
    public sealed class NetworkAddressHelpers
    {
        public static bool IsLanAddress(NetworkAddress networkAddress)
        {
            if (networkAddress == null)
                throw new ArgumentNullException(nameof(networkAddress));

            if (networkAddress.AddressType == NetworkAddressType.Loopback)
                return true;

            if (networkAddress.AddressType != NetworkAddressType.Ip)
                return false;

            var address = networkAddress?.EndPoint?.Address;
            if (address == null)
                throw new ArgumentException(nameof(networkAddress));

            // Choose which comparison to use based on the class of the address being tested any local adresses of a
            // different class than the address being tested will fail based on the first byte.

            //if (adr.ip[0] == 127 && adr.ip[1] == 0 && adr.ip[2] == 0 && adr.ip[3] == 1)
            //{
            //    return qtrue;
            //}

            //// Class A
            //if ((adr.ip[0] & 0x80) == 0x00)
            //{
            //    for (i = 0; i < numIP; i++)
            //    {
            //        if (adr.ip[0] == localIP[i][0])
            //        {
            //            return qtrue;
            //        }
            //    }
            //    // the RFC1918 class a block will pass the above test
            //    return qfalse;
            //}

            //// Class B
            //if ((adr.ip[0] & 0xc0) == 0x80)
            //{
            //    for (i = 0; i < numIP; i++)
            //    {
            //        if (adr.ip[0] == localIP[i][0] && adr.ip[1] == localIP[i][1])
            //        {
            //            return qtrue;
            //        }
            //        // also check against the RFC1918 class b blocks
            //        if (adr.ip[0] == 172 && localIP[i][0] == 172 && (adr.ip[1] & 0xf0) == 16 && (localIP[i][1] & 0xf0) == 16)
            //        {
            //            return qtrue;
            //        }
            //    }
            //    return qfalse;
            //}

            //// Class C
            //for (i = 0; i < numIP; i++)
            //{
            //    if (adr.ip[0] == localIP[i][0] && adr.ip[1] == localIP[i][1] && adr.ip[2] == localIP[i][2])
            //    {
            //        return qtrue;
            //    }
            //    // also check against the RFC1918 class c blocks
            //    if (adr.ip[0] == 192 && localIP[i][0] == 192 && adr.ip[1] == 168 && localIP[i][1] == 168)
            //    {
            //        return qtrue;
            //    }
            //}
            //return qfalse;

            return false;
        }
    }
}