using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SimpleExternalCheatCSGO.Structs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct mstudiobbox_t
    {
        public int bone;
        public int group;
        public struct_Vector bbmin;
        public struct_Vector bbmax;
        public int szhitboxnameindex;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public char[] m_iPad01;
        public float m_flRadius;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public int[] m_iPad02;
    };

}
