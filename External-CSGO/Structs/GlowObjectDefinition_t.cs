using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SimpleExternalCheatCSGO.Structs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct GlowObjectDefinition_t
    {
        public int dwEntity; // 0x0
        public float fR;    // 0x4
        public float fG;  // 0x8
        public float fB; // 0xC
        public float fAlpha; // 0x10
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public char[] cJunk; // 0x14
        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public bool bRenderWhenOccluded; // 0x24
        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public bool bRenderWhenUnoccluded; // 0x25
        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public bool bFullBloomRender; // 0x26
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public char[] junk2; // 0x26
        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public bool m_bInnerGlow; // 0x2C
    }
}
