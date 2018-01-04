using SimpleExternalCheatCSGO.API;
using SimpleExternalCheatCSGO.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleExternalCheatCSGO.SDK
{
    public class IClientEntity
    {
        public IntPtr pThis;
        public IClientEntity() { }
        public IClientEntity(IntPtr pEntity)
        {
            pThis = pEntity;
        }

        public string GetNetworkName()
        {
            IntPtr vTables = MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, pThis + 0x8);
            IntPtr vFunction = MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, vTables + 0x8);
            IntPtr vClass = MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, vFunction + 0x1);
            return MemoryAPI.ReadTextFromProcess(Globals._csgo.ProcessHandle, MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, vClass + 0x8),-1);
        }

        public CSGOClassID GetClassID()
        {
            IntPtr vTables = MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, pThis + 0x8);
            IntPtr vFunction = MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, vTables + 0x8);
            IntPtr vClass = MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, vFunction + 0x1);
            return (CSGOClassID)MemoryAPI.ReadFromProcess<int>(Globals._csgo.ProcessHandle, vClass + 0x14);
        }

        public Vector GetOrigin()
        {
            return new Vector(MemoryAPI.ReadFromProcess<struct_Vector>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_vecOrigin));
        }

        public int GetIndex()
        {
            return MemoryAPI.ReadFromProcess<int>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_iIndex);
        }

        public string GetModelName()
        {
            return MemoryAPI.ReadTextFromProcess(Globals._csgo.ProcessHandle, GetModel() + 0x4, 128);
        }

        public IntPtr GetModel()
        {
            return MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_Model);
        }

        public IntPtr GetStudioHdr()
        {
            return MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, pThis + Globals._signatures.m_pStudioHdr));
        }

        public int GetGlowIndex()
        {
            return MemoryAPI.ReadFromProcess<int>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_iGlowIndex);
        }

        public bool IsDormant()
        {
            return MemoryAPI.ReadFromProcess<bool>(Globals._csgo.ProcessHandle, pThis + Globals._signatures.dw_bDormant);
        }

        public override bool Equals(System.Object obj)
        {
            var pEntity = obj as IClientEntity;

            return pEntity == null ? false : pEntity.pThis == this.pThis;
        }

        public override int GetHashCode()
        {
            return (int)pThis;
        }
    }
}
