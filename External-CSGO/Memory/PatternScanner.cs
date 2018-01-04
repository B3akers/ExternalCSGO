using SimpleExternalCheatCSGO.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SimpleExternalCheatCSGO.Memory
{
    public static class PatternScanner
    { 
        public static int FindPattern(IntPtr _handle, string pattern_str, GameModule module, bool sub, int offset = 0, bool read = false, int startAddress = 0)
        {
            var returnvalue = FindPatternEx(_handle, pattern_str, module, sub, offset, read, startAddress);
            GC.Collect();
            return returnvalue;
        }

        public static int FindPatternEx(IntPtr _handle, string pattern_str, GameModule module, bool sub, int offset = 0, bool read = false, int startAddress = 0)
        {
            List<byte> temp = new List<byte>();
            string mask = "";
            foreach (string l in pattern_str.Split(' '))
            {
                if (l == "?" || l == "00")
                {
                    temp.Add(0x00);
                    mask += "?";
                }
                else
                {
                    temp.Add((byte)int.Parse(l, System.Globalization.NumberStyles.HexNumber));
                    mask += "x";
                }
            }
            byte[] pattern = temp.ToArray();

            int moduleSize = module.Size;

            IntPtr moduleBase = module.BaseAddress;

            if (startAddress != 0)
            {
                moduleSize = ((int)moduleBase + moduleSize) - startAddress;
                moduleBase = new IntPtr(startAddress);
            }
            if (moduleSize == 0)
                throw new Exception($"Size of module {module} is INVALID.");

            byte[] moduleBytes = new byte[moduleSize];
            int numBytes;

            if (WinAPI.ReadProcessMemory(_handle, moduleBase, moduleBytes, moduleSize, out numBytes))
            {
                for (int i = 0; i < moduleSize; i++)
                {
                    bool found = true;

                    for (int l = 0; l < mask.Length; l++)
                    {
                        found = mask[l] == '?' || moduleBytes[l + i] == pattern[l];

                        if (!found)
                            break;
                    }

                    if (found)
                    {
                        i += (int)moduleBase;
                        if (read)
                            i = MemoryAPI.ReadFromProcess<int>(_handle, new IntPtr(i + offset));
                        if (sub)
                            i -= (int)moduleBase;
                        return i;
                    }

                }
            }
            return 0;
        }
    }
}
