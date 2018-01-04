using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SimpleExternalCheatCSGO.Structs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct cplane_t
    {
        struct_Vector normal;
        float dist;
        char type;
        char signbits;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        char[] pad;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CBaseTrace
    {
        public struct_Vector startpos;
        public struct_Vector endpos;
        public cplane_t plane;
        public float fraction;
        public int contents;
        public ushort dispFlags;
        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public bool allsolid;
        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public bool startsolid;
    };

    [StructLayout(LayoutKind.Explicit)]
    public struct csurface_t
    {
        [FieldOffset(0x0)]
        public IntPtr name;
        [FieldOffset(0x4)]
        public short surfaceProps;
        [FieldOffset(0x6)]
        public ushort flags;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct trace_t
    {
        public CBaseTrace baseTrace;
        public float fractionleftsolid;
        public csurface_t surface;
        public int hitgroup;
        public short physicsbone;
        public ushort worldSurfaceIndex;
        public IntPtr m_pEnt;
        public int hitbox;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x24)]
        public char[] shit;
    };
}
