using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleExternalCheatCSGO.SDK
{
    public enum Team
    {
        NONE = 0,
        SPEC,
        TT,
        CT
    };
    enum HitboxList
    {
        HITBOX_HEAD = 0,
        HITBOX_NECK,
        HITBOX_LOWER_NECK,
        HITBOX_PELVIS,
        HITBOX_BODY,
        HITBOX_THORAX,
        HITBOX_CHEST,
        HITBOX_UPPER_CHEST,
        HITBOX_R_THIGH,
        HITBOX_L_THIGH,
        HITBOX_R_CALF,
        HITBOX_L_CALF,
        HITBOX_R_FOOT,
        HITBOX_L_FOOT,
        HITBOX_R_HAND,
        HITBOX_L_HAND,
        HITBOX_R_UPPER_ARM,
        HITBOX_R_FOREARM,
        HITBOX_L_UPPER_ARM,
        HITBOX_L_FOREARM,
        HITBOX_MAX
    };
}
