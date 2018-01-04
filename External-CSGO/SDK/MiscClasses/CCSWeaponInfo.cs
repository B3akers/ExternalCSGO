using SimpleExternalCheatCSGO.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleExternalCheatCSGO.SDK.MiscClasses
{
    public enum WeaponType
    {
        WEAPONTYPE_KNIFE = 0,
        WEAPONTYPE_PISTOL,
        WEAPONTYPE_SUBMACHINEGUN,
        WEAPONTYPE_RIFLE,
        WEAPONTYPE_SHOTGUN,
        WEAPONTYPE_SNIPER_RIFLE,
        WEAPONTYPE_MACHINEGUN,
        WEAPONTYPE_C4,
        WEAPONTYPE_PLACEHOLDER,
        WEAPONTYPE_GRENADE,
        WEAPONTYPE_UNKNOWN
    };

    public class CCSWeaponInfo
    {
        public IntPtr pThis;
        public CCSWeaponInfo(IntPtr Pointer)
        {
            pThis = Pointer;
        }

        public WeaponType GetWeaponType()
        {
            return (WeaponType)MemoryAPI.ReadFromProcess<int>(Globals._csgo.ProcessHandle, pThis + 0x00C8);
        }

        public bool IsFullAuto()
        {
            return MemoryAPI.ReadFromProcess<bool>(Globals._csgo.ProcessHandle, pThis + 0x00E8);
        }  
    }
}
