using SimpleExternalCheatCSGO.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleExternalCheatCSGO.CodeInjection
{
    public static class RevealRank
    {
        public static byte[] Shellcode = {
                 0x68,0x78,0x56,0x34,0x12,
                 0xB8,0x78,0x56,0x34,0x12,        
                 0xFF,0xD0,            
                 0x83,0xC4,0x04,           
                 0xC3,               
                 0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
                 };

        public static int Size = Shellcode.Length;
        public static IntPtr Address;

        public static void Do()
        {
            if (Address == IntPtr.Zero)
            {
                Alloc();
                if (Address == IntPtr.Zero)
                    return;
                Buffer.BlockCopy(BitConverter.GetBytes((int)Address + 16), 0, Shellcode, 1, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(Globals._signatures.dw_RevealRankFn), 0, Shellcode, 6, 4);

                WinAPI.WriteProcessMemory(Globals._csgo.ProcessHandle, Address, Shellcode, Shellcode.Length, 0);
            }
            IntPtr Thread = WinAPI.CreateRemoteThread(Globals._csgo.ProcessHandle, (IntPtr)null, IntPtr.Zero, Address, IntPtr.Zero, 0, (IntPtr)null);

            WinAPI.WaitForSingleObject(Thread, 0xFFFFFFFF);

            WinAPI.CloseHandle(Thread);
        }

        public static void Alloc()
        {
            Address = Globals._alloc.SmartAlloc(Size);
        }
    }
}
