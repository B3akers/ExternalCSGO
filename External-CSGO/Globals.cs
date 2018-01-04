using SimpleExternalCheatCSGO.API;
using SimpleExternalCheatCSGO.Offsets;
using SimpleExternalCheatCSGO.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleExternalCheatCSGO
{
    public static class Globals
    {
        public static CSGOProcess _csgo = new CSGOProcess();
        public static NetVarManager _netvar = new NetVarManager();
        public static Signatures _signatures = new Signatures();
        public static SmartAllocator _alloc = new SmartAllocator();
    }
}
