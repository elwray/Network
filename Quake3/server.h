// server.h

#include "../game/q_shared.h"
#include "../qcommon/qcommon.h"
#include "../game/g_public.h"
#include "../game/bg_public.h"

//=============================================================================

#define	PERS_SCORE				0		// !!! MUST NOT CHANGE, SERVER AND
										// GAME BOTH REFERENCE !!!


typedef struct {
	int				areabytes;
	byte			areabits[MAX_MAP_AREA_BYTES];		// portalarea visibility bits
	playerState_t	ps;
	int				num_entities;
	int				first_entity;		// into the circular sv_packet_entities[]
										// the entities MUST be in increasing state number
										// order, otherwise the delta compression will fail
	int				messageSent;		// time the message was transmitted
	int				messageAcked;		// time the message was acked
	int				messageSize;		// used to rate drop packets
} clientSnapshot_t;

typedef enum {
	CS_FREE,		// can be reused for a new connection
	CS_ZOMBIE,		// client has been disconnected, but don't reuse
					// connection for a couple seconds
	CS_CONNECTED,	// has been assigned to a client_t, but no gamestate yet
	CS_PRIMED,		// gamestate has been sent, but client hasn't sent a usercmd
	CS_ACTIVE		// client is fully in game
} clientState_t;

typedef struct netchan_buffer_s {
	msg_t           msg;
	byte            msgBuffer[MAX_MSGLEN];
	struct netchan_buffer_s *next;
} netchan_buffer_t;

//=============================================================================


// MAX_CHALLENGES is made large to prevent a denial
// of service attack that could cycle all of them
// out before legitimate users connected
#define	MAX_CHALLENGES	1024

#define	AUTHORIZE_TIMEOUT	5000

typedef struct {
	netadr_t	adr;
	int			challenge;
	int			time;				// time the last packet was sent to the autherize server
	int			pingTime;			// time the challenge response was sent to client
	int			firstTime;			// time the adr was first used, for authorize timeout checks
	qboolean	connected;
} challenge_t;


#define	MAX_MASTERS	8				// max recipients for heartbeat packets


// this structure will be cleared only when the game dll changes
typedef struct {
	qboolean	initialized;				// sv_init has completed

	int			time;						// will be strictly increasing across level changes

	int			snapFlagServerBit;			// ^= SNAPFLAG_SERVERCOUNT every SV_SpawnServer()

	client_t	*clients;					// [sv_maxclients->integer];
	int			numSnapshotEntities;		// sv_maxclients->integer*PACKET_BACKUP*MAX_PACKET_ENTITIES
	int			nextSnapshotEntities;		// next snapshotEntities to use
	entityState_t	*snapshotEntities;		// [numSnapshotEntities]
	int			nextHeartbeatTime;
	challenge_t	challenges[MAX_CHALLENGES];	// to prevent invalid IPs from connecting
	netadr_t	redirectAddress;			// for rcon return messages

	netadr_t	authorizeAddress;			// for rcon return messages
} serverStatic_t;

//=============================================================================

extern	serverStatic_t	svs;				// persistant server info across maps
extern	server_t		sv;					// cleared each map
extern	vm_t			*gvm;				// game virtual machine

#define	MAX_MASTER_SERVERS	5

extern	cvar_t	*sv_fps;
extern	cvar_t	*sv_timeout;
extern	cvar_t	*sv_zombietime;
extern	cvar_t	*sv_rconPassword;
extern	cvar_t	*sv_privatePassword;
extern	cvar_t	*sv_allowDownload;
extern	cvar_t	*sv_maxclients;

extern	cvar_t	*sv_privateClients;
extern	cvar_t	*sv_hostname;
extern	cvar_t	*sv_master[MAX_MASTER_SERVERS];
extern	cvar_t	*sv_reconnectlimit;
extern	cvar_t	*sv_showloss;
extern	cvar_t	*sv_padPackets;
extern	cvar_t	*sv_killserver;
extern	cvar_t	*sv_mapname;
extern	cvar_t	*sv_mapChecksum;
extern	cvar_t	*sv_serverid;
extern	cvar_t	*sv_maxRate;
extern	cvar_t	*sv_minPing;
extern	cvar_t	*sv_maxPing;
extern	cvar_t	*sv_gametype;
extern	cvar_t	*sv_pure;
extern	cvar_t	*sv_floodProtect;
extern	cvar_t	*sv_lanForceRate;
extern	cvar_t	*sv_strictAuth;

//===========================================================

//
// sv_main.c
//
void SV_FinalMessage (char *message);
void QDECL SV_SendServerCommand( client_t *cl, const char *fmt, ...);


void SV_AddOperatorCommands (void);
void SV_RemoveOperatorCommands (void);


void SV_MasterHeartbeat (void);
void SV_MasterShutdown (void);




//
// sv_init.c
//
void SV_SetConfigstring( int index, const char *val );
void SV_GetConfigstring( int index, char *buffer, int bufferSize );

void SV_SetUserinfo( int index, const char *val );
void SV_GetUserinfo( int index, char *buffer, int bufferSize );

void SV_ChangeMaxClients( void );
void SV_SpawnServer( char *server, qboolean killBots );



//
// sv_client.c
//
void SV_GetChallenge( netadr_t from );

void SV_DirectConnect( netadr_t from );

void SV_AuthorizeIpPacket( netadr_t from );

void SV_ExecuteClientMessage( client_t *cl, msg_t *msg );
void SV_UserinfoChanged( client_t *cl );

void SV_ClientEnterWorld( client_t *client, usercmd_t *cmd );
void SV_DropClient( client_t *drop, const char *reason );

void SV_ExecuteClientCommand( client_t *cl, const char *s, qboolean clientOK );
void SV_ClientThink (client_t *cl, usercmd_t *cmd);

void SV_WriteDownloadToClient( client_t *cl , msg_t *msg );

//
// sv_ccmds.c
//
void SV_Heartbeat_f( void );

//
// sv_snapshot.c
//
void SV_AddServerCommand( client_t *client, const char *cmd );
void SV_UpdateServerCommandsToClient( client_t *client, msg_t *msg );
void SV_WriteFrameToClient (client_t *client, msg_t *msg);
void SV_SendMessageToClient( msg_t *msg, client_t *client );
void SV_SendClientMessages( void );
void SV_SendClientSnapshot( client_t *client );

//
// sv_game.c
//
int	SV_NumForGentity( sharedEntity_t *ent );
sharedEntity_t *SV_GentityNum( int num );
playerState_t *SV_GameClientNum( int num );
svEntity_t	*SV_SvEntityForGentity( sharedEntity_t *gEnt );
sharedEntity_t *SV_GEntityForSvEntity( svEntity_t *svEnt );
void		SV_InitGameProgs ( void );
void		SV_ShutdownGameProgs ( void );
void		SV_RestartGameProgs( void );
qboolean	SV_inPVS (const vec3_t p1, const vec3_t p2);

//
// sv_bot.c
//
void		SV_BotFrame( int time );
int			SV_BotAllocateClient(void);
void		SV_BotFreeClient( int clientNum );

void		SV_BotInitCvars(void);
int			SV_BotLibSetup( void );
int			SV_BotLibShutdown( void );
int			SV_BotGetSnapshotEntity( int client, int ent );
int			SV_BotGetConsoleMessage( int client, char *buf, int size );

int BotImport_DebugPolygonCreate(int color, int numPoints, vec3_t *points);
void BotImport_DebugPolygonDelete(int id);

//============================================================
//
// high level object sorting to reduce interaction tests
//

void SV_ClearWorld (void);
// called after the world model has been loaded, before linking any entities

void SV_UnlinkEntity( sharedEntity_t *ent );
// call before removing an entity, and before trying to move one,
// so it doesn't clip against itself

void SV_LinkEntity( sharedEntity_t *ent );
// Needs to be called any time an entity changes origin, mins, maxs,
// or solid.  Automatically unlinks if needed.
// sets ent->v.absmin and ent->v.absmax
// sets ent->leafnums[] for pvs determination even if the entity
// is not solid


clipHandle_t SV_ClipHandleForEntity( const sharedEntity_t *ent );


void SV_SectorList_f( void );


int SV_AreaEntities( const vec3_t mins, const vec3_t maxs, int *entityList, int maxcount );
// fills in a table of entity numbers with entities that have bounding boxes
// that intersect the given area.  It is possible for a non-axial bmodel
// to be returned that doesn't actually intersect the area on an exact
// test.
// returns the number of pointers filled in
// The world entity is never returned in this list.


int SV_PointContents( const vec3_t p, int passEntityNum );
// returns the CONTENTS_* value from the world and all entities at the given point.


void SV_Trace( trace_t *results, const vec3_t start, vec3_t mins, vec3_t maxs, const vec3_t end, int passEntityNum, int contentmask, int capsule );
// mins and maxs are relative

// if the entire move stays in a solid volume, trace.allsolid will be set,
// trace.startsolid will be set, and trace.fraction will be 0

// if the starting point is in a solid, it will be allowed to move out
// to an open area

// passEntityNum is explicitly excluded from clipping checks (normally ENTITYNUM_NONE)


void SV_ClipToEntity( trace_t *trace, const vec3_t start, const vec3_t mins, const vec3_t maxs, const vec3_t end, int entityNum, int contentmask, int capsule );
// clip to a specific entity

//
// sv_net_chan.c
//
void SV_Netchan_Transmit( client_t *client, msg_t *msg);
void SV_Netchan_TransmitNextFragment( client_t *client );
qboolean SV_Netchan_Process( client_t *client, msg_t *msg );

