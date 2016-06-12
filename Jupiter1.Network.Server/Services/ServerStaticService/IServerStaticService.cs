﻿using Jupiter1.Network.Server.Structures;

namespace Jupiter1.Network.Server.Services.ServerStaticService
{
    internal interface IServerStaticService
    {
        Client[] Clients { get; }
        int Time { get; set; }
    }
}