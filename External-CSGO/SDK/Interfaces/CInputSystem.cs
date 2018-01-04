using SimpleExternalCheatCSGO.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleExternalCheatCSGO.SDK
{
    public class CInputSystem
    {
        public IntPtr pThis;
        public CInputSystem()
        {
            pThis = MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, Globals._csgo.CSGOModules["inputsystem"].BaseAddress + Globals._signatures.dw_inputsystem);
        }

        public bool GetInputState()
        {
            return MemoryAPI.ReadFromProcess<bool>(Globals._csgo.ProcessHandle, pThis + Globals._signatures.dw_inputenabled);
        }

        public void SetInputState(bool enabled)
        {
            MemoryAPI.WriteToProcess<bool>(Globals._csgo.ProcessHandle, pThis + Globals._signatures.dw_inputenabled, enabled);
        }
    }
}
