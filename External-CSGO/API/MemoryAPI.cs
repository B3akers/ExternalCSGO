using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SimpleExternalCheatCSGO.API
{
    public static class MemoryAPI
    {
        public static byte[] ReadMemory(IntPtr handle, IntPtr adress, int size)
        {
            byte[] data = new byte[size];
            int bytesRead = 0;
            WinAPI.ReadProcessMemory(handle, adress, data, data.Length, out bytesRead);
            if (bytesRead == 0)
                return BitConverter.GetBytes(0);
            return data;
        }

        public static void WriteToProcess<T>(IntPtr _handle, IntPtr address, T str)
        {
            int size = 1;
            if (typeof(T) != typeof(bool))
                size = Marshal.SizeOf(typeof(T));
            byte[] arr = new byte[size];

            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(str, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);
            WinAPI.WriteProcessMemory(_handle, address, arr, arr.Length, 0);
        }


        public static byte[] GetStructBytes<T>(T str)
        {
            int size = Marshal.SizeOf(typeof(T));
            byte[] arr = new byte[size];

            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(str, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);

            return arr;
        }

        public static T ReadFromProcess<T>(IntPtr _handle, IntPtr address)
        {
            int size = 1;
			if (typeof(T) != typeof(bool))
                size = Marshal.SizeOf(typeof(T));
            byte[] data = ReadMemory(_handle, address, size);
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            T stuff = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();
            return stuff;
        }
        public static string ReadTextFromProcess(IntPtr _handle, IntPtr address, int size)
        {
            byte[] data;
            if (size != -1)
                data = ReadMemory(_handle, address, size);
            else
            {
                List<byte> name = new List<byte>();
                int offset = 0;
                byte curbyte = 0x00;
                do
                {
                    curbyte = ReadMemory(_handle, address + offset, 1)[0];
                    name.Add(curbyte);
                    ++offset;
                } while (curbyte != 0x00);
                size = offset;
                data = name.ToArray();
            }
            Encoding encoding = Encoding.Default;
            string result = encoding.GetString(data, 0, (int)size);
            result = result.Substring(0, result.IndexOf('\0'));
            return result;

        }
    }
}
