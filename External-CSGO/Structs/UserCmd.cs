using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SimpleExternalCheatCSGO.Structs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CUserCmd
    {
        public int command_number;
        public int tick_count;
        public struct_Vector viewangles;
        public struct_Vector aimdirection;
        public float forwardmove;
        public float sidemove;
        public float upmove;
        public int buttons;
        public char impulse;
        public int weaponselect;
        public int weaponsubtype;
        public int random_seed;
        public short mousedx;
        public short mousedy;
        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public bool hasbeenpredicted;
        public struct_Vector headangles;
        public struct_Vector headoffset;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x2)]
        public char[] unkown;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CVerifiedUserCmd
    {
        public CUserCmd m_cmd;
        public uint m_crc;
    };
}
