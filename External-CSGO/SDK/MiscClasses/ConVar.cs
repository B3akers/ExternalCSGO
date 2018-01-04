using SimpleExternalCheatCSGO.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleExternalCheatCSGO.SDK.MiscClasses
{
    public class ConVar
    {
        public IntPtr pThis;
        public ConVar(IntPtr Pointer)
        {
            pThis = Pointer;
        }
        public ConVar(string name)
        {
            pThis = MainThread.g_ConVar.GetConVarAddress(name);
        }

        public int GetInt()
        {
            int xor_value = MemoryAPI.ReadFromProcess<int>(Globals._csgo.ProcessHandle, pThis + 0x30);

            xor_value ^= (int)pThis;

            return xor_value;
        }

        public void ClearCallbacks()
        {
            MemoryAPI.WriteToProcess<int>(Globals._csgo.ProcessHandle, pThis + 0x44 + 0xC, 0);
        }

        public bool GetBool()
        {
            return GetInt() != 0;
        }

        public float GetFloat()
        {
            byte[] xored = MemoryAPI.ReadMemory(Globals._csgo.ProcessHandle, pThis + 0x2C, 4);

            byte[] key = BitConverter.GetBytes((int)pThis);

            for (int i = 0; i < 4; ++i)
                xored[i] ^= key[i];

            float value = BitConverter.ToSingle(xored, 0);

            return value;
        }

    }
}
