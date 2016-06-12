using System;
using System.Linq;
using Jupiter1.Network.Server.Constants;
using Jupiter1.Network.Server.Enums;
using Jupiter1.Network.Server.Services.BotService;
using Jupiter1.Network.Server.Services.ClientService;
using Jupiter1.Network.Server.Services.MasterService;
using Jupiter1.Network.Server.Services.ServerConfiguration;
using Jupiter1.Network.Server.Services.ServerStaticService;
using Jupiter1.Network.Server.Services.SnapshotService;

namespace Jupiter1.Network.Server.Services.ServerService
{
    internal sealed class ServerService : IServerService
    {
        private readonly IServerConfiguration _configuration;
        private readonly IServerStaticService _serverStaticService;
        private readonly ISnapshotService _snapshotService;
        private readonly IClientService _clientService;
        private readonly IMasterService _masterService;
        private readonly IBotService _botService;

        public ServerService(IServerConfiguration configuration, IServerStaticService serverStaticService,
            ISnapshotService snapshotService, IClientService clientService, IMasterService masterService,
            IBotService botService)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            if (serverStaticService == null)
                throw new ArgumentNullException(nameof(serverStaticService));
            if (snapshotService == null)
                throw new ArgumentNullException(nameof(snapshotService));
            if (clientService == null)
                throw new ArgumentNullException(nameof(clientService));
            if (masterService == null)
                throw new ArgumentNullException(nameof(masterService));
            if (botService == null)
                throw new ArgumentNullException(nameof(botService));

            _configuration = configuration;
            _serverStaticService = serverStaticService;
            _snapshotService = snapshotService;
            _clientService = clientService;
            _masterService = masterService;
            _botService = botService;
        }

        #region IServerService
        // Player movement occurs as a result of packet events, which happen before SV_Frame is called.
        public void Frame(int ellapsedMilliseconds)
        {
            if (!_configuration.IsRunning)
                return;

            if (IsPaused())
                return;

            //// if it isn't time for the next frame, do nothing
            //if (sv_fps->integer < 1)
            //{
            //    Cvar_Set("sv_fps", "10");
            //}
            //frameMsec = 1000 / sv_fps->integer;

            //sv.timeResidual += msec;

            //if (!com_dedicated->integer) SV_BotFrame(svs.time + sv.timeResidual);

            //if (com_dedicated->integer && sv.timeResidual < frameMsec)
            //{
            //    // NET_Sleep will give the OS time slices until either get a packet
            //    // or time enough for a server frame has gone by
            //    NET_Sleep(frameMsec - sv.timeResidual);
            //    return;
            //}

            //// if time is about to hit the 32nd bit, kick all clients
            //// and clear sv.time, rather
            //// than checking for negative time wraparound everywhere.
            //// 2giga-milliseconds = 23 days, so it won't be too often
            //if (svs.time > 0x70000000)
            //{
            //    SV_Shutdown("Restarting server due to time wrapping");
            //    Cbuf_AddText("vstr nextmap\n");
            //    return;
            //}
            //// this can happen considerably earlier when lots of clients play and the map doesn't change
            //if (svs.nextSnapshotEntities >= 0x7FFFFFFE - svs.numSnapshotEntities)
            //{
            //    SV_Shutdown("Restarting server due to numSnapshotEntities wrapping");
            //    Cbuf_AddText("vstr nextmap\n");
            //    return;
            //}

            //if (sv.restartTime && svs.time >= sv.restartTime)
            //{
            //    sv.restartTime = 0;
            //    Cbuf_AddText("map_restart 0\n");
            //    return;
            //}

            //// update infostrings if anything has been changed
            //if (cvar_modifiedFlags & CVAR_SERVERINFO)
            //{
            //    SV_SetConfigstring(CS_SERVERINFO, Cvar_InfoString(CVAR_SERVERINFO));
            //    cvar_modifiedFlags &= ~CVAR_SERVERINFO;
            //}
            //if (cvar_modifiedFlags & CVAR_SYSTEMINFO)
            //{
            //    SV_SetConfigstring(CS_SYSTEMINFO, Cvar_InfoString_Big(CVAR_SYSTEMINFO));
            //    cvar_modifiedFlags &= ~CVAR_SYSTEMINFO;
            //}

            //if (com_speeds->integer)
            //{
            //    startTime = Sys_Milliseconds();
            //}
            //else {
            //    startTime = 0;  // quite a compiler warning
            //}

            //// update ping based on the all received frames
            //SV_CalcPings();

            _botService.Frame(_serverStaticService.Time);

            //// run the game simulation in chunks
            //while (sv.timeResidual >= frameMsec)
            //{
            //    sv.timeResidual -= frameMsec;
            //    svs.time += frameMsec;

            //    // let everything in the world think and move
            //    VM_Call(gvm, GAME_RUN_FRAME, svs.time);
            //}

            //if (com_speeds->integer)
            //{
            //    time_game = Sys_Milliseconds() - startTime;
            //}

            // Check timeouts.
            CheckTimeouts();

            // Send messages back to the clients.
            _snapshotService.SendClientsMessages();

            // Send a hearbeat to the master if needed.
            _masterService.Heartbeat();
        }
        #endregion

        internal void CheckTimeouts()
        {
            var timeout = _serverStaticService.Time - 1000 * _configuration.ClientTimeout;
            var zombie = _serverStaticService.Time - 1000 * _configuration.ClientZombieTime;

            foreach (var client in _serverStaticService.Clients)
            {
                // Message times may be wrong across a changelevel. Set to current server time.
                if (client.LastPacketTime > _serverStaticService.Time)
                    client.LastPacketTime = _serverStaticService.Time;

                if (client.State == ClientState.Zombie && client.LastPacketTime < zombie)
                {
                    client.State = ClientState.Free;
                    continue;
                }

                // Client in Connected, Primed or Active state.
                if (client.State >= ClientState.Connected && client.LastPacketTime < timeout)
                {
                    ++client.TimeoutCount;
                    if (client.TimeoutCount > 5)
                    {
                        _clientService.DropClient(client, ReasonConstants.TimedOut);
                        client.State = ClientState.Free;
                    }
                }
                else
                {
                    client.TimeoutCount = 0;
                }
            }
        }

        internal bool IsPaused()
        {
            // Only pause if there is just a single client connected.
            _configuration.IsPaused = _serverStaticService.Clients
                .Count(x => x.State >= ClientState.Connected && x.Type != ClientType.Bot) <= 1;
            return _configuration.IsPaused;
        }
    }
}