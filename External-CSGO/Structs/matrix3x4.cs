using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SimpleExternalCheatCSGO.Structs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct float_4
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] second;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct matrix3x4
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float_4[] first;
    }
}
