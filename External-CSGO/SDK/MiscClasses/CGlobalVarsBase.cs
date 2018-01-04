using SimpleExternalCheatCSGO.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SimpleExternalCheatCSGO.SDK
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct GlobalVarsBase
    {
        public float realtime;
        public int framecount;
        public float absoluteframetime;
        public float absoluteframestarttimestddev;
        public float curtime;
        public float frametime;
        public int maxClients;
        public int tickcount;
        public float interval_per_tick;
        public float interpolation_amount;
    };
    public class CGlobalVarsBase
    {
        public GlobalVarsBase pGlobals;

        public void Update()
        {
            pGlobals = MemoryAPI.ReadFromProcess<GlobalVarsBase>(Globals._csgo.ProcessHandle, Globals._csgo.CSGOModules["engine"].BaseAddress + Globals._signatures.dw_GlobalVars);
        }

        public GlobalVarsBase Get()
        {
            return pGlobals;
        }
    }
}
