using SimpleExternalCheatCSGO.API;
using SimpleExternalCheatCSGO.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleExternalCheatCSGO.SDK
{
    public class CUserCmdList
    {
        public IntPtr pThis;

        public void Update(IntPtr pBase)
        {
            pThis = MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, IntPtr.Add(pBase, 0xEC));
        }

        public CUserCmd GetUserCmdBySequence(int sequence_number)
        {
            return GetUserCmd(sequence_number % 150);
        }

        public CUserCmd GetUserCmd(int iIndex)
        {
            return MemoryAPI.ReadFromProcess<CUserCmd>(Globals._csgo.ProcessHandle, IntPtr.Add(pThis, ((iIndex) * 0x64) + 0x4));
        }

        public void SetUserCmdBySequence(int sequence_number, CUserCmd cmd)
        {
            SetUserCmd(sequence_number % 150, cmd);
        }

        public void SetUserCmd(int iIndex, CUserCmd cmd)
        {
            MemoryAPI.WriteToProcess<CUserCmd>(Globals._csgo.ProcessHandle, IntPtr.Add(pThis, ((iIndex) * 0x64) + 0x4), cmd);
        }
    }
}
