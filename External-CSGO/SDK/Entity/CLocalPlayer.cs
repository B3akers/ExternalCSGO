using SimpleExternalCheatCSGO.API;
using SimpleExternalCheatCSGO.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleExternalCheatCSGO.SDK
{
    public class CLocalPlayer : CBasePlayer
    {
        public void Update()
        {
            pThis = MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, Globals._csgo.CSGOModules["client"].BaseAddress + Globals._signatures.dw_LocalPlayer);
        }

        public void ResetFov()
        {
            SetFov(GetDefaultFov());
        }

        public int GetCrosshairID()
        {
          return MemoryAPI.ReadFromProcess<int>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_iCrosshairId);
        }

        public int GetFov()
        {
            return MemoryAPI.ReadFromProcess<int>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_iFOV);
        }

        public int GetDefaultFov()
        {
            return MemoryAPI.ReadFromProcess<int>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_iDefaultFOV);
        }

        public void SetFov(int iFov)
        {
            MemoryAPI.WriteToProcess<int>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_iFOV, iFov);
        }

        public Vector GetPunch()
        {
            return new Vector(MemoryAPI.ReadFromProcess<struct_Vector>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_aimPunchAngle));
        }

        public int GetShootsFired()
        {
            return MemoryAPI.ReadFromProcess<int>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_iShotsFired);
        }
    }
}
