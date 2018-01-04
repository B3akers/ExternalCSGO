using SimpleExternalCheatCSGO.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleExternalCheatCSGO.CodeInjection
{
    public static class ClientCmd
    {

        public static int Size = 256;
        public static IntPtr Address;

        public static void Do(string szCmd)
        {
            if (Address == IntPtr.Zero)
            {
                Alloc();
                if (Address == IntPtr.Zero)
                    return;
            }
            if (szCmd.Length > 255)
                szCmd = szCmd.Substring(0, 255);

            var szCmd_bytes = Encoding.UTF8.GetBytes(szCmd + "\0");

            WinAPI.WriteProcessMemory(Globals._csgo.ProcessHandle, Address, szCmd_bytes, szCmd_bytes.Length, 0);

            IntPtr Thread = WinAPI.CreateRemoteThread(Globals._csgo.ProcessHandle, (IntPtr)null, IntPtr.Zero, new IntPtr(Globals._signatures.dw_clientcmd), Address, 0, (IntPtr)null);

            WinAPI.CloseHandle(Thread);

            WinAPI.WaitForSingleObject(Thread, 0xFFFFFFFF);
        }

        public static void Alloc()
        {
            Address = Globals._alloc.SmartAlloc(Size);
        }
    }
}
