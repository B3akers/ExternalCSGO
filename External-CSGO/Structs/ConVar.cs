using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SimpleExternalCheatCSGO.Structs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ConVar_struct
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] pad_0x0000; //0x0000
        public IntPtr pNext; //0x0004 
        public int bRegistered; //0x0008 
        public IntPtr pszName; //0x000C 
        public IntPtr pszHelpString; //0x0010 
        public int nFlags; //0x0014 
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] pad_0x0018; //0x0018
        public IntPtr pParent; //0x001C 
        public IntPtr pszDefaultValue; //0x0020 
        public IntPtr strString; //0x0024 
        public int StringLength; //0x0028 
        public float fValue; //0x002C 
        public int nValue; //0x0030 
        public int bHasMin; //0x0034 
        public float fMinVal; //0x0038 
        public int bHasMax; //0x003C 
        public float fMaxVal; //0x0040 
        public IntPtr fnChangeCallback; //0x0044
    }
}
