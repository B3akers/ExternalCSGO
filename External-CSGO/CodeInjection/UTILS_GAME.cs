using SimpleExternalCheatCSGO.API;
using SimpleExternalCheatCSGO.Structs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace SimpleExternalCheatCSGO.CodeInjection
{
    public static class UTILS_GAME
    {
        public static void AcceptMatch()
        {
            IntPtr Thread = WinAPI.CreateRemoteThread(Globals._csgo.ProcessHandle, (IntPtr)null, IntPtr.Zero, new IntPtr(Globals._signatures.dw_AcceptMatch), IntPtr.Zero, 0, (IntPtr)null);

            WinAPI.WaitForSingleObject(Thread, 0xFFFFFFFF);

            WinAPI.CloseHandle(Thread);
        }

        public static bool MatchFound()
        {
            IntPtr CLobbyScreen = MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, Globals._csgo.CSGOModules["client"].BaseAddress + Globals._signatures.dw_CLobbyScreen);

            if (CLobbyScreen != IntPtr.Zero)
            {
                int iAccept = MemoryAPI.ReadFromProcess<int>(Globals._csgo.ProcessHandle, CLobbyScreen + Globals._signatures.dw_MatchAccepted);
                int iFound = MemoryAPI.ReadFromProcess<int>(Globals._csgo.ProcessHandle, CLobbyScreen + Globals._signatures.dw_MatchFound);

                return iAccept == 0 && iFound != 0;
            }
            else
                return false;
        }
    }
}
