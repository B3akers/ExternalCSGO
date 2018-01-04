using SimpleExternalCheatCSGO.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleExternalCheatCSGO.SDK
{
    public class IClientEntityList
    {
        public IntPtr pThis;
        public List<IntPtr> pPlayerList = new List<IntPtr>();
        public IClientEntityList()
        {
            pThis = Globals._csgo.CSGOModules["client"].BaseAddress + Globals._signatures.dw_entitylist;
        }
        public void Update()
        {
            pPlayerList.Clear();
            for (int i = 1; i <= MainThread.g_GlobalVars.Get().maxClients; ++i)
            {
                IntPtr pEntity = MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, pThis + ((i - 1) * 0x10));
                if (pEntity != IntPtr.Zero)
                    pPlayerList.Add(pEntity);
            }
        }
        public IntPtr GetEntityByIndex(int iIndex)
        {
            IntPtr pEntity = MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, pThis + ((iIndex - 1) * 0x10));
            return pEntity;
        }

        public int GetHighestEntityIndex()
        {
            return MemoryAPI.ReadFromProcess<int>(Globals._csgo.ProcessHandle, pThis + Globals._signatures.dw_HighestEntityIndex);
        }
    }
}
