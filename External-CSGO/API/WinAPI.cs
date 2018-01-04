using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SimpleExternalCheatCSGO.API
{
    public static class WinAPI
    {

        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern IntPtr RtlAdjustPrivilege(int Privilege, bool bEnablePrivilege,
bool IsThreadPrivilege, out bool PreviousValue);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [StructLayout(LayoutKind.Sequential)]
        public struct CLIENT_ID
        {
            public IntPtr UniqueProcess;
            public IntPtr UniqueThread;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct THREAD_BASIC_INFORMATION
        {
            public int ExitStatus;
            public IntPtr TebBaseAdress;
            public CLIENT_ID ClientId;
            public IntPtr AffinityMask;
            public int Priority;
            public int BasePriority;
        }

        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern int NtQueryInformationThread(IntPtr threadHandle, ThreadInfoClass threadInformationClass, ref THREAD_BASIC_INFORMATION threadInformation, int threadInformationLength, IntPtr returnLengthPtr);


        [DllImport("kernel32.dll")]
        public static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, int nSize, uint flNewProtect, out uint lpflOldProtect);

        public enum Protection : uint
        {
            PAGE_NOACCESS = 0x01,
            PAGE_READONLY = 0x02,
            PAGE_READWRITE = 0x04,
            PAGE_WRITECOPY = 0x08,
            PAGE_EXECUTE = 0x10,
            PAGE_EXECUTE_READ = 0x20,
            PAGE_EXECUTE_READWRITE = 0x40,
            PAGE_EXECUTE_WRITECOPY = 0x80,
            PAGE_GUARD = 0x100,
            PAGE_NOCACHE = 0x200,
            PAGE_WRITECOMBINE = 0x400
        }
        public enum ThreadInfoClass : int
        {
            ThreadBasicInformation = 0,
            ThreadQuerySetWin32StartAddress = 9
        }

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, int size, int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttribute, IntPtr dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        [DllImport("kernel32.dll")]
        public static extern void WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

        [DllImport("kernel32.dll")]
        public static extern bool TerminateThread(IntPtr hHandle, uint dwExitCode);

        [DllImport("kernel32.dll")]
        public static extern bool SetThreadPriority(IntPtr hHandle, int nPriority);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenThread(int dwDesiredAccess, bool bInheritHandle, int dwThreadId);

        [DllImport("kernel32.dll")]
        public static extern int GetThreadId(IntPtr hHandle);

        [DllImport("ntdll.dll")]
        public static extern int RtlRemoteCall(IntPtr Process, IntPtr Thread, IntPtr CallSite, long ArgumentCount, uint Arguments, bool PassContext, bool AlreadySuspended);

        [DllImport("kernel32.dll")]
        public static extern int SuspendThread(IntPtr hHandle);

        [DllImport("kernel32.dll")]
        public static extern int ResumeThread(IntPtr handle);

        [DllImport("kernel32.dll")]
        public static extern bool GetThreadContext(IntPtr hHandle, [Out] int[] context);

        [DllImport("kernel32.dll")]
        public static extern bool SetThreadContext(IntPtr thread, [In] int[] context);

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, uint dwFreeType);

        public const int
            MEM_COMMIT = 0x1000,
            MEM_RELEASE = 0x800,
            MEM_RESERVE = 0x2000;

        public const int
            PAGE_READWRITE = 0x40,
            PROCESS_VM_OPERATION = 0x0008,
            PROCESS_VM_READ = 0x0010,
            PROCESS_VM_WRITE = 0x0020;
    }
}
