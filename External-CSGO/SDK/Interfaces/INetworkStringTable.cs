using SimpleExternalCheatCSGO.API;
using SimpleExternalCheatCSGO.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SimpleExternalCheatCSGO.SDK
{
    public class ModelINetworkStringTable
    {
        public IntPtr pThis;
        public Dictionary<string, int> Items = new Dictionary<string, int>();
        public string lastMap = "";
        public ModelINetworkStringTable(IntPtr pThis)
        {
            this.pThis = pThis;
        }

        public void Update()
        {
            Items.Clear();
            IntPtr startAddress = GetItems();
            for (int i = 0; i < 1024; ++i)
            {
                IntPtr namePointer = MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, startAddress + 0xC + (i * 0x34));
                string model_name = MemoryAPI.ReadTextFromProcess(Globals._csgo.ProcessHandle, namePointer, -1);
                if (model_name.StartsWith("models"))
                    Items.Add(model_name, i - 3);
            }
        }

        public IntPtr GetItems()
        {
            return MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, pThis + 0x3C) + 0xC);
        }
    }
}
