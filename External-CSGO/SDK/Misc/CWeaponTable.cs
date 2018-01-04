using SimpleExternalCheatCSGO.API;
using SimpleExternalCheatCSGO.SDK.MiscClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleExternalCheatCSGO.SDK
{
    public class CWeaponTable
    {
        public IntPtr pThis;

        public void Update()
        {
           pThis = MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, Globals._csgo.CSGOModules["client"].BaseAddress + Globals._signatures.dw_WeaponTable);
        }

        public CCSWeaponInfo GetWeaponInfo(int nTableIndex)
        {
            return new CCSWeaponInfo(MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, pThis + 0xC + (0x10 * nTableIndex)));
        }
    }
}
