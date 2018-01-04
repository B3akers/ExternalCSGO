using SimpleExternalCheatCSGO.API;
using SimpleExternalCheatCSGO.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleExternalCheatCSGO.CodeInjection
{
    //https://www.unknowncheats.me/forum/counterstrike-global-offensive/194139-external-linegoesthroughsmoke.html
    public static class LineGoesThroughSmoke
    {
        public static byte[] Shellcode = {
		0x55,				
		0x8B, 0xEC,							
		0x83, 0xEC, 0x0C,
		0x89, 0xE0,	
		0x8B, 0x0D, 0x12, 0x34, 0x56, 0x78,		
		0x89, 0x08,				
		0x8B, 0x15, 0x12, 0x34, 0x56, 0x78,		
		0x89, 0x50, 0x04,	
		0x8B, 0x0D, 0x12, 0x34, 0x56, 0x78,		
		0x89, 0x48, 0x08,	
		0x83, 0xEC, 0x0C,	
		0x89, 0xE2,	
		0xA1, 0x12, 0x34, 0x56, 0x78,			
		0x89, 0x02,					
		0x8B, 0x0D, 0x12, 0x34, 0x56, 0x78,		
		0x89, 0x4A, 0x04,	
		0xA1, 0x12, 0x34, 0x56, 0x78,		
		0x89, 0x42, 0x08,					
		0xB8, 0x12, 0x34, 0x56, 0x78,
		0xFF, 0xD0,
		0xA3, 0x12, 0x34, 0x56, 0x78,
		0x83, 0xC4, 0x18,
		0xC9,
		0xC3,
		0x12,0x34,0x56,0x78,0x12,0x34,0x56,0x78,0x12,0x34,0x56,0x78,
		0x12,0x34,0x56,0x78,0x12,0x34,0x56,0x78,0x12,0x34,0x56,0x78,
		0x12
        };

        public static int Size = Shellcode.Length;
        public static IntPtr Address;
        public static IntPtr ReturnAddress;

        public static bool Do(Vector start, Vector end)
        {
            if (Address == IntPtr.Zero)
            {
                Alloc();
                if (Address == IntPtr.Zero)
                    return false;
                int param_startAddr = (int)Address + 80;
                int param_endAddr = param_startAddr + 12;
                int returnAddr = param_endAddr + 12;

                ReturnAddress = new IntPtr(returnAddr);

                int param_end_y = param_endAddr + 4;
                int param_end_z = param_endAddr + 8;

                int param_start_y = param_startAddr + 4;
                int param_start_z = param_startAddr + 8;

                Buffer.BlockCopy(BitConverter.GetBytes(param_endAddr), 0, Shellcode, 0xA, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(param_end_y), 0, Shellcode, 0x12, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(param_end_z), 0, Shellcode, 0x1B, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(param_startAddr), 0, Shellcode, 0x28, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(param_start_y), 0, Shellcode, 0x30, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(param_start_z), 0, Shellcode, 0x38, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(Globals._signatures.dw_lineThroughSmoke), 0, Shellcode, 0x40, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(returnAddr), 0, Shellcode, 0x47, 4);
            }

            Buffer.BlockCopy(MemoryAPI.GetStructBytes<struct_Vector>(start.ToStruct()), 0, Shellcode, 0x50, 12);
            Buffer.BlockCopy(MemoryAPI.GetStructBytes<struct_Vector>(end.ToStruct()), 0, Shellcode, 0x5C, 12);

            WinAPI.WriteProcessMemory(Globals._csgo.ProcessHandle, Address, Shellcode, Shellcode.Length, 0);

            IntPtr Thread = WinAPI.CreateRemoteThread(Globals._csgo.ProcessHandle, (IntPtr)null, IntPtr.Zero, Address, (IntPtr)null, 0, (IntPtr)null);

            WinAPI.WaitForSingleObject(Thread, 0xFFFFFFFF);

            WinAPI.CloseHandle(Thread);

            bool returnVal = MemoryAPI.ReadFromProcess<bool>(Globals._csgo.ProcessHandle, ReturnAddress);

            return returnVal;
        }

        public static void Alloc()
        {
            Address = Globals._alloc.SmartAlloc(Size);
        }
    }
}
