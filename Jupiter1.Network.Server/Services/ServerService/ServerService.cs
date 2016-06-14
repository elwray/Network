using System;
using System.Linq;
using Jupiter1.Network.Server.Constants;
using Jupiter1.Network.Server.Enums;
using Jupiter1.Network.Server.Services.BotService;
using Jupiter1.Network.Server.Services.ClientService;
using Jupiter1.Network.Server.Services.MasterService;
using Jupiter1.Network.Server.Services.ServerConfiguration;
using Jupiter1.Network.Server.Services.ServerLocalService;
using Jupiter1.Network.Server.Services.ServerStaticService;
using Jupiter1.Network.Server.Services.SnapshotService;

namespace Jupiter1.Network.Server.Services.ServerService
{
    internal sealed class ServerService : IServerService
    {
        private readonly IServerConfiguration _configuration;
        private readonly IServerStaticService _serverStaticService;
        private readonly IServerLocalService _serverLocalService;
        private readonly ISnapshotService _snapshotService;
        private readonly IClientService _clientService;
        private readonly IMasterService _masterService;
        private readonly IBotService _botService;

        public ServerService(IServerConfiguration configuration, IServerStaticService serverStaticService,
            IServerLocalService serverLocalService, ISnapshotService snapshotService, IClientService clientService,
            IMasterService masterService, IBotService botService)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            if (serverStaticService == null)
                throw new ArgumentNullException(nameof(serverStaticService));
            if (serverLocalService == null)
                throw new ArgumentNullException(nameof(serverLocalService));
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
            _serverLocalService = serverLocalService;
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

            // If it isn't time for the next frame, do nothing.
            if (_configuration.Fps < 1)
                _configuration.Fps = ServerConstants.DefaultFps;
            var serverFrameTime = 1000 / _configuration.Fps;

            _serverLocalService.ResidualTime += ellapsedMilliseconds;

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

            // Update ping based on the all received frames
            UpdatePings();

            _botService.Frame(_serverStaticService.Time);

            // Run the game simulation in chunks.
            while (_serverLocalService.ResidualTime >= serverFrameTime)
            {
                _serverLocalService.ResidualTime -= serverFrameTime;
                _serverStaticService.Time += serverFrameTime;

                // TODO:
                //// let everything in the world think and move
                //VM_Call(gvm, GAME_RUN_FRAME, svs.time);
            }

            // Check timeouts.
            CheckTimeouts();

            // Send messages back to the clients.
            _snapshotService.SendClientsMessages();

            // Send a hearbeat to the master if needed.
            _masterService.Heartbeat();
        }

        public void Shutdown()
        {
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
                        _clientService.DropClient(client, TextConstants.TimedOut);
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
            var count = _serverStaticService.Clients
                .Count(x => x.State >= ClientState.Connected && x.Type != ClientType.Bot);
            _configuration.IsPaused = count <= 1;

            return _configuration.IsPaused;
        }

        internal void UpdatePings()
        {
            //int i, j;
            //client_t* cl;
            //int total, count;
            //int delta;
            //playerState_t* ps;

            foreach (var client in _serverStaticService.Clients)
            {
                if (client.State != ClientState.Active)
                {
                    client.Ping = 999;
                    continue;
                }

                // TODO:
                //if (!cl->gentity)
                //{
                //    cl->ping = 999;
                //    continue;
                //}
                //if (cl->gentity->r.svFlags & SVF_BOT)
                //{
                //    cl->ping = 0;
                //    continue;
                //}

                var count = 0;
                var total = 0;
                foreach (var snapshot in client.Snapshots)
                {
                    if (snapshot.AckedTime <= 0)
                        continue;

                    var delta = snapshot.AckedTime - snapshot.SentTime;
                    total += delta;

                    ++count;
                }

                if (count == 0)
                {
                    client.Ping = 999;
                }
                else
                {
                    client.Ping = total / count;
                    if (client.Ping > 999)
                        client.Ping = 999;
                }
            }
        }
    }
}