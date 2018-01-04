using SimpleExternalCheatCSGO.API;
using SimpleExternalCheatCSGO.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleExternalCheatCSGO.SDK
{
    public class CVerifiedUserCmdList
    {
        public IntPtr pThis;

        public void Update(IntPtr pBase)
        {
            pThis = MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, IntPtr.Add(pBase, 0xF0));
        }

        public CVerifiedUserCmd GetVerifiedUserCmdBySequence(int sequence_number)
        {
            return GetVerifiedUserCmd(sequence_number % 150);
        }

        public CVerifiedUserCmd GetVerifiedUserCmd(int iIndex)
        {
            return MemoryAPI.ReadFromProcess<CVerifiedUserCmd>(Globals._csgo.ProcessHandle, IntPtr.Add(pThis, ((iIndex) * 0x68) + 0x4));
        }

        public void SetVerifiedUserCmdBySequence(int sequence_number, CVerifiedUserCmd cmd)
        {
            SetVerifiedUserCmd(sequence_number % 150, cmd);
        }

        public void SetVerifiedUserCmd(int iIndex, CVerifiedUserCmd cmd)
        {
            MemoryAPI.WriteToProcess<CVerifiedUserCmd>(Globals._csgo.ProcessHandle, IntPtr.Add(pThis, ((iIndex) * 0x68) + 0x4), cmd);
        }

    }
}
