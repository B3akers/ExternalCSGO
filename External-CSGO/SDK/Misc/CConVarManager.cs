using SimpleExternalCheatCSGO.API;
using SimpleExternalCheatCSGO.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleExternalCheatCSGO.SDK
{
    public class CConVarManager
    {
        public IntPtr pThis;
        public CharCodes codes;
        public CConVarManager()
        {
            codes = MemoryAPI.ReadFromProcess<CharCodes>(Globals._csgo.ProcessHandle, Globals._csgo.CSGOModules["vstdlib"].BaseAddress + Globals._signatures.dw_Convarchartable);
            pThis = MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, Globals._csgo.CSGOModules["vstdlib"].BaseAddress + Globals._signatures.dw_enginecvar);
        }
       
        public IntPtr GetConVarAddress(string name)
        {
            var hash = GetStringHash(name);

            IntPtr Pointer = MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, pThis + 0x34) + ((byte)hash * 4));
            while (Pointer != IntPtr.Zero)
            {
                if (MemoryAPI.ReadFromProcess<int>(Globals._csgo.ProcessHandle, Pointer) == hash)
                {
                    IntPtr ConVarPointer = MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, Pointer + 0x4);

                    if (MemoryAPI.ReadTextFromProcess(Globals._csgo.ProcessHandle, MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, ConVarPointer + 0xC), -1) == name)
                        return ConVarPointer;
                }
                Pointer = MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, Pointer + 0xC);
            }
            return IntPtr.Zero;
        }


        public int GetStringHash(string name)
        {
            int v2 = 0;
            int v3 = 0;
            for (int i = 0; i < name.Length; i += 2)
            {
                v3 = codes.tab[v2 ^ char.ToUpper(name[i])];
                if (i + 1 == name.Length)
                    break;
                v2 = codes.tab[v3 ^ char.ToUpper(name[i + 1])];
            }
            return v2 | (v3 << 8);
        }
    }
}
