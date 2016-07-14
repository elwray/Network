using System;
using Jupiter1.Network.Common.Constants;
using Jupiter1.Network.Common.Enums;
using Jupiter1.Network.Common.Extensions;
using Jupiter1.Network.Common.Helpers;
using Jupiter1.Network.Common.Structures;
using Jupiter1.Network.Server.Constants;
using Jupiter1.Network.Server.Enums;
using Jupiter1.Network.Server.Extensions;
using Jupiter1.Network.Server.Services.ChannelService;
using Jupiter1.Network.Server.Services.ServerConfiguration;
using Jupiter1.Network.Server.Services.ServerLocalService;
using Jupiter1.Network.Server.Services.ServerStaticService;
using Jupiter1.Network.Server.Structures;

namespace Jupiter1.Network.Server.Services.SnapshotService
{
    /*
     * Delta encode a client frame onto the network channel
     * A normal server packet will look like:
     * 4	sequence number (high bit set if an oversize fragment)
     * <optional reliable commands>
     * 1	svc_snapshot
     * 4	last client reliable command
     * 4	serverTime
     * 1	lastframe for delta compression
     * 1	snapFlags
     * 1	areaBytes
     * <areabytes>
     * <playerstate>
     * <packetentities>
     */
    internal class SnapshotService : ISnapshotService
    {
        private readonly IServerConfiguration _configuration;
        private readonly IServerStaticService _serverStaticService;
        private readonly IServerLocalService _serverLocalService;
        private readonly IServerChannelService _serverChannelService;

        public SnapshotService(IServerConfiguration configuration, IServerStaticService serverStaticService,
            IServerLocalService serverLocalService, IServerChannelService serverChannelService)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            if (serverStaticService == null)
                throw new ArgumentNullException(nameof(serverStaticService));
            if (serverLocalService == null)
                throw new ArgumentNullException(nameof(serverLocalService));
            if (serverChannelService == null)
                throw new ArgumentNullException(nameof(serverChannelService));

            _configuration = configuration;
            _serverStaticService = serverStaticService;
            _serverLocalService = serverLocalService;
            _serverChannelService = serverChannelService;
        }

        // Writes a delta update of an entityState_t list to the message.
        private void EmitPacketEntities(Snapshot from, Snapshot to, Message message)
        {
        }

        // Decides which entities are going to be visible to the client, and copies off the playerstate and areabits.
        // This properly handles multiple recursive portals, but the render currently doesn't. For viewing through
        // other player's eyes, clent can be something other than client->gentity.
        private void BuildClientSnapshot(Client client)
        {
            //    vec3_t org;
            //    clientSnapshot_t* frame;
            //    snapshotEntityNumbers_t entityNumbers;
            //    int i;
            //    sharedEntity_t* ent;
            //    entityState_t* state;
            //    svEntity_t* svEnt;
            //    sharedEntity_t* clent;
            //    int clientNum;
            //    playerState_t* ps;

            // Bump the counter used to prevent double adding.
            ++_serverLocalService.SnapshotCounter;

            // This is the frame we are creating.
            var snapshot = client.Snapshots[client.Channel.OutgoingSequence & ServerConstants.PacketMask];

            // Clear everything in this snapshot.
            var snapshotEntities = new SnapshotEntities();
            // TODO:
            //    Com_Memset(frame->areabits, 0, sizeof(frame->areabits) );

            // https://zerowing.idsoftware.com/bugzilla/show_bug.cgi?id=62
            snapshot.EntitiesCount = 0;

            // TODO:
            //    clent = client->gentity;
            //    if (!clent || client->state == CS_ZOMBIE)
            //        return;
            //
            //    // grab the current playerState_t
            //    ps = SV_GameClientNum(client - svs.clients);
            //    frame->ps = *ps;
            //
            //    // never send client's own entity, because it can
            //    // be regenerated from the playerstate
            //    clientNum = frame->ps.clientNum;
            //    if (clientNum < 0 || clientNum >= MAX_GENTITIES)
            //    {
            //        Com_Error(ERR_DROP, "SV_SvEntityForGentity: bad gEnt");
            //    }
            //    svEnt = &sv.svEntities[clientNum];
            //
            //    svEnt->snapshotCounter = sv.snapshotCounter;
            //
            //    // find the client's viewpoint
            //    VectorCopy(ps->origin, org);
            //    org[2] += ps->viewheight;
            //
            //    // add all the entities directly visible to the eye, which
            //    // may include portal entities that merge other viewpoints
            //    SV_AddEntitiesVisibleFromPoint(org, frame, &entityNumbers, qfalse);

            //    // if there were portals visible, there may be out of order entities
            //    // in the list which will need to be resorted for the delta compression
            //    // to work correctly.  This also catches the error condition
            //    // of an entity being included twice.
            //    qsort(entityNumbers.snapshotEntities, entityNumbers.numSnapshotEntities,
            //        sizeof(entityNumbers.snapshotEntities[0]), SV_QsortEntityNumbers);

            //    // now that all viewpoint's areabits have been OR'd together, invert
            //    // all of them to make it a mask vector, which is what the renderer wants
            //    for (i = 0; i < MAX_MAP_AREA_BYTES / 4; i++)
            //    {
            //        ((int*) frame->areabits)[i] = ((int*) frame->areabits)[i] ^ -1;
            //    }

            //    // copy the entity states out
            //    frame->num_entities = 0;
            //    frame->first_entity = svs.nextSnapshotEntities;
            //    for (i = 0; i < entityNumbers.numSnapshotEntities; i++)
            //    {
            //        ent = SV_GentityNum(entityNumbers.snapshotEntities[i]);
            //        state = &svs.snapshotEntities[svs.nextSnapshotEntities % svs.numSnapshotEntities];
            //        *state = ent->s;
            //        svs.nextSnapshotEntities++;
            //        // this should never hit, map should always be restarted first in SV_Frame
            //        if (svs.nextSnapshotEntities >= 0x7FFFFFFE)
            //        {
            //            Com_Error(ERR_FATAL, "svs.nextSnapshotEntities wrapped");
            //        }
            //        frame->num_entities++;
            //    }
        }

        // (Re)send all server commands the client hasn't acknowledged yet.
        private void UpdateServerCommandsToClient(Client client, Message message)
        {
            // Write any unacknowledged serverCommands.
            for (var i = client.ReliableAcknowledge + 1; i <= client.ReliableSequence; ++i)
            {
                message.WriteByte((byte) ServerToClientMessage.ServerCommand);
                message.WriteInt32(i);
                message.WriteAsciiString(client.ReliableCommands[i & (ServerConstants.MaxRaliableCommands - 1)]);
            }
            client.ReliableSent = client.ReliableSequence;
        }

        private void WriteSnapshotToClient(Client client, Message message)
        {
            // This is the snapshot we are creating.
            var snapshot = client.Snapshots[client.Channel.OutgoingSequence & ServerConstants.PacketMask];

            Snapshot oldSnapshot;
            int lastSnapshot;

            // Try to use a previous frame as the source for delta compressing the snapshot.
            if (client.DeltaMessage <= 0 || client.State != ClientState.Active)
            {
                // Client is asking for a retransmit.
                oldSnapshot = null;
                lastSnapshot = 0;
            }
            else if (client.Channel.OutgoingSequence - client.DeltaMessage >= (ServerConstants.PacketsBackup - 3))
            {
                // Client hasn't gotten a good message through in a long time.
                // TODO: Com_DPrintf("%s: Delta request from out of date packet.\n", client->name);
                oldSnapshot = null;
                lastSnapshot = 0;
            }
            else
            {
                // We have a valid snapshot to delta from.
                oldSnapshot = client.Snapshots[client.DeltaMessage & ServerConstants.PacketMask];
                lastSnapshot = client.Channel.OutgoingSequence - client.DeltaMessage;

                // The snapshot's entities may still have rolled off the buffer, though.
                if (oldSnapshot.FirstEntity <=
                    _serverStaticService.NextSnapshotEntities - _serverStaticService.SnapshotEntitiesCount)
                {
                    // TODO: Com_DPrintf("%s: Delta request from out of date entities.\n", client->name);
                    oldSnapshot = null;
                    lastSnapshot = 0;
                }
            }

            message.WriteByte((byte) ServerToClientMessage.Snapshot);

            // Send over the current server time so the client can drift its view of time to try to match.
            message.WriteInt32(_serverStaticService.Time);

            // What we are delta'ing from.
            message.WriteByte((byte) lastSnapshot);

            var snapFlags = _serverStaticService.SnapFlagServerBit;
            if (client.RateDelayed)
            {
                snapFlags |= SnapFlag.RateDelayed;
            }
            if (client.State != ClientState.Active)
            {
                snapFlags |= SnapFlag.NotActive;
            }
            message.WriteByte((byte) snapFlags);

            // Send over the areabits.
            message.WriteByte((byte) snapshot.AreaBytes);
            message.WriteData(snapshot.AreaBits, snapshot.AreaBytes);

            // Delta encode the playerstate.
            message.WriteDeltaPlayerState(oldSnapshot?.PlayerState, snapshot.PlayerState);

            // Delta encode the entities.
            EmitPacketEntities(oldSnapshot, snapshot, message);

            // Padding for rate debugging.
            if (_configuration.PadPackets != null)
            {
                for (var i = 0; i < _configuration.PadPackets.Value; ++i)
                    message.WriteByte((byte) ServerToClientMessage.Nop);
            }
        }

        private void WriteDownloadToClient(Client client, Message message)
        {
        }

        private void SendSnapshotToClient(Client client)
        {
            var message = new Message();

            // Build the snapshot.
            BuildClientSnapshot(client);

            // TODO:
            // Bots need to have their snapshots build, but the query them directly without needing to be sent.
            //if (client->gentity && client->gentity->r.svFlags & SVF_BOT)
            //{
            //    return;
            //}
            //
            //MSG_Init(&msg, msg_buf, sizeof(msg_buf));
            //msg.allowoverflow = qtrue;

            // NOTE, MRE: all server->client messages now acknowledge let the client know which reliable clientCommands
            // we have received.
            message.WriteInt32(client.LastClientCommand);

            // (Re)send any reliable server commands.
            UpdateServerCommandsToClient(client, message);

            // Send over all the relevant entityState_t and the playerState_t.
            WriteSnapshotToClient(client, message);

            // Add any download data if the client is downloading.
            WriteDownloadToClient(client, message);

            // TODO:
            // check for overflow
            //if (msg.overflowed)
            //{
            //    Com_Printf("WARNING: msg overflowed for %s\n", client->name);
            //    MSG_Clear(&msg);
            //}

            SendMessageToClient(client, message);
        }

        // Return the number of msec a given size message is supposed to take to clear, based on the current rate.
        private int GetSupposedTimeToSent(Client client, int messageSize)
        {
            // Individual messages will never be larger than fragment size.
            // messageSize > 1500 - in quake source code.
            if (messageSize > CommonConstants.FragmentSize - 1)
            {
                // TODO: log too long message.
                // messageSize = 1500 - in quake source code.
                messageSize = CommonConstants.FragmentSize - 1;
            }

            if (_configuration.MaxRate > 1000)
                _configuration.MaxRate = 1000;

            var rate = client.Rate;
            if (rate > _configuration.MaxRate)
                rate = _configuration.MaxRate;

            return (messageSize + ServerConstants.HeaderRateBytes) * 1000 / rate;
        }

        #region ISnapshotService
        public void SendMessageToClient(Client client, Message message)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            // Record information about the message
            var index = client.Channel.OutgoingSequence & ServerConstants.PacketMask;
            var snapshot = client.Snapshots[index];
            snapshot.MessageSize = message.Length;
            snapshot.SentTime = _serverStaticService.Time;
            snapshot.AckedTime = -1;

            // Send the datagram.
            _serverChannelService.Transmit(client, message);

            // Set nextSnapshotTime based on rate and requested number of updates.

            // Local clients get snapshots every frame
            // TTimo - https://zerowing.idsoftware.com/bugzilla/show_bug.cgi?id=491
            // added sv_lanForceRate check
            if (client.Channel.RemoteAddress.AddressType == NetworkAddressType.Loopback ||
                (_configuration.LanForceRate && NetworkAddressHelpers.IsLanAddress(client.Channel.RemoteAddress)))
            {
                client.NextSnapshotTime = _serverStaticService.Time - 1;
                return;
            }

            // Normal rate / snapshotMsec calculation.
            var totalTimeToSent = GetSupposedTimeToSent(client, message.Length);
            if (totalTimeToSent < client.SnapshotMsec)
            {
                // Never send more packets than this, no matter what the rate is at.
                totalTimeToSent = client.SnapshotMsec;
                client.RateDelayed = false;
            }
            else
            {
                client.RateDelayed = true;
            }

            client.NextSnapshotTime = _serverStaticService.Time + totalTimeToSent;

            // Don't pile up empty snapshots while connecting.
            if (client.State != ClientState.Active)
            {
                // A gigantic connection message may have already put the nextSnapshotTime more than a second away, so
                // don't shorten it do shorten if client is downloading.
                if (client.DownloadName == null && client.NextSnapshotTime < _serverStaticService.Time + 1000)
                    client.NextSnapshotTime = _serverStaticService.Time + 1000;
            }
        }

        public void SendClientsMessages()
        {
            foreach (var client in _serverStaticService.Clients)
            {
                // Client not connected.
                if (client.State == ClientState.Free)
                    continue;

                // Not time yet.
                if (_serverStaticService.Time < client.NextSnapshotTime)
                    continue;

                // Send additional message fragments if the last message was too large to send at once.
                if (client.Channel.HasUnsentFragments)
                {
                    var totalTimeToSent = GetSupposedTimeToSent(client,
                        client.Channel.UnsentLength - client.Channel.UnsentFragmentStart);
                    client.NextSnapshotTime = _serverStaticService.Time + totalTimeToSent;

                    _serverChannelService.TransmitNext(client);

                    continue;
                }

                // Generate and send a new message.
                SendSnapshotToClient(client);
            }
        }
        #endregion
    }
}