namespace Jupiter1.Network.Server.Structures
{
    internal sealed class EntityState
    {
    }
}

//typedef struct entityState_s
//{
//    int number;     // entity index
//    int eType;      // entityType_t
//    int eFlags;

//    trajectory_t pos;  // for calculating position
//    trajectory_t apos; // for calculating angles

//    int time;
//    int time2;

//    vec3_t origin;
//    vec3_t origin2;

//    vec3_t angles;
//    vec3_t angles2;

//    int otherEntityNum; // shotgun sources, etc
//    int otherEntityNum2;

//    int groundEntityNum;  // -1 = in air

//    int constantLight;  // r + (g<<8) + (b<<16) + (intensity<<24)
//    int loopSound;    // constantly loop this sound

//    int modelindex;
//    int modelindex2;
//    int clientNum;    // 0 to (MAX_CLIENTS - 1), for players and corpses
//    int frame;

//    int solid;      // for client side prediction, trap_linkentity sets this properly

//    int event;      // impulse events -- muzzle flashes, footsteps, etc
//    int eventParm;

//    // for players
//    int powerups;   // bit flags
//    int weapon;     // determines weapon and flash model, etc
//    int legsAnim;   // mask off ANIM_TOGGLEBIT
//    int torsoAnim;    // mask off ANIM_TOGGLEBIT

//    int generic1;
//}
//entityState_t;