using SimpleExternalCheatCSGO.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleExternalCheatCSGO.CodeInjection
{
    public static class SendClantag
    {
        public static byte[] Shellcode = {
                   0xBA,0x67,0x45,0x23,0x01,
                   0xB9,0x67,0x45,0x23,0x01,
                   0xB8,0x67,0x45,0x23,0x01,
                   0xFF,0xD0,
                   0xC3,
                   0x12,0x12,0x12,0x12,0x12,0x12,0x12,0x12,0x12,0x12,0x12,0x12,0x12,0x12,0x12,0x12,
                   0x12,0x12,0x12,0x12,0x12,0x12,0x12,0x12,0x12,0x12,0x12,0x12,0x12,0x12,0x12,0x12
                   };

        public static int Size = Shellcode.Length;
        public static IntPtr Address;

        public static void Do(string tag, string name)
        {
            if (Address == IntPtr.Zero)
            {
                Alloc();
                if (Address == IntPtr.Zero)
                    return;

                int tag_addr = (int)Address + 18;

                int name_addr = tag_addr + 16;

                Buffer.BlockCopy(BitConverter.GetBytes(name_addr), 0, Shellcode, 1, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(tag_addr), 0, Shellcode, 6, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(Globals._signatures.dw_ChangeClanTag), 0, Shellcode, 11, 4);
            }

            var tag_bytes = Encoding.UTF8.GetBytes(tag + "\0");
            var name_bytes = Encoding.UTF8.GetBytes(name + "\0");

            Buffer.BlockCopy(tag_bytes, 0, Shellcode, 18, tag_bytes.Length);
            Buffer.BlockCopy(name_bytes, 0, Shellcode, 34, name_bytes.Length);

            WinAPI.WriteProcessMemory(Globals._csgo.ProcessHandle, Address, Shellcode, Shellcode.Length, 0);

            IntPtr Thread = WinAPI.CreateRemoteThread(Globals._csgo.ProcessHandle, (IntPtr)null, IntPtr.Zero, Address, (IntPtr)null, 0, (IntPtr)null);

            WinAPI.WaitForSingleObject(Thread, 0xFFFFFFFF);

            WinAPI.CloseHandle(Thread);
        }

        public static void Alloc()
        {
            Address = Globals._alloc.SmartAlloc(Size);
        }
    }
}
