using SimpleExternalCheatCSGO.API;
using SimpleExternalCheatCSGO.Structs;
using SimpleExternalCheatCSGO.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleExternalCheatCSGO.SDK
{
    public class CBasePlayer : IClientEntity
    {
        public CBasePlayer() { }
        public CBasePlayer(IntPtr pEntity)
        {
            pThis = pEntity;
        }

        public Vector GetHitboxPosition(int iHitbox, string szModelName = "")
        {
            if (szModelName == "")
                szModelName = this.GetModelName();

            var hitbox = CCHitboxManager.GetHitBox(this, szModelName, iHitbox);

            var matrix = GetMatrix3x4(hitbox.bone);

            Vector vMin = MathUtil.VectorTransform(new Vector(hitbox.bbmin), matrix);
            Vector vMax = MathUtil.VectorTransform(new Vector(hitbox.bbmax), matrix);
            Vector vCenter = ((vMin + vMax) * 0.5f);

            return vCenter;
        }

        public bool HasGunGameImmunity()
        {
            return MemoryAPI.ReadFromProcess<bool>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_bGunGameImmunity);
        }

        public bool IsScoped()
        {
           return MemoryAPI.ReadFromProcess<bool>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_bIsScoped);
        }

        public void SetRenderColor(Color col)
        {
            MemoryAPI.WriteToProcess<char>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_clrRender, (char)col.R);
            MemoryAPI.WriteToProcess<char>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_clrRender + 1, (char)col.G);
            MemoryAPI.WriteToProcess<char>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_clrRender + 2, (char)col.B);
            MemoryAPI.WriteToProcess<char>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_clrRender + 3, (char)col.A);
        }

        public void SetWearable(int iHandle)
        {
            MemoryAPI.WriteToProcess<int>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_hMyWearables, iHandle);
        }

        public CBaseCombatWeapon GetActiveWeapon()
        {
            int Handle = MemoryAPI.ReadFromProcess<int>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_hActiveWeapon);

            Handle &= 0xFFF;

            return new CBaseCombatWeapon(MainThread.g_EntityList.GetEntityByIndex(Handle));
        }

        public void SetSpotted(bool val)
        {
            MemoryAPI.WriteToProcess<bool>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_bSpotted, val);
        }

        public List<CBaseCombatWeapon> GetWeapons()
        {
            List<CBaseCombatWeapon> wp = new List<CBaseCombatWeapon>();

            var weapon = GetWeaponHandle(0);

            for (int i = 1; weapon != -1; ++i)
            {
                int id = weapon & 0xFFF;
                wp.Add(new CBaseCombatWeapon(MainThread.g_EntityList.GetEntityByIndex(id)));
                weapon = GetWeaponHandle(i);
            }

            return wp;
        }

        public int GetWeaponHandle(int index)
        {
            return MemoryAPI.ReadFromProcess<int>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_hMyWeapons + (index * 0x4));
        }

        public CBaseCombatWeapon GetWearableWeapon()
        {
            int Handle = MemoryAPI.ReadFromProcess<int>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_hMyWearables);

            Handle &= 0xFFF;

            return new CBaseCombatWeapon(MainThread.g_EntityList.GetEntityByIndex(Handle));
        }

        public matrix3x4 GetMatrix3x4(int iBone)
        {
            IntPtr pBase = MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_dwBoneMatrix);

            return MemoryAPI.ReadFromProcess<matrix3x4>(Globals._csgo.ProcessHandle, pBase + 0x30 * iBone);
        }

        public int GetHealth()
        {
            return MemoryAPI.ReadFromProcess<int>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_iHealth);
        }

        public Team GetTeamNum()
        {
            return (Team)MemoryAPI.ReadFromProcess<int>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_iTeamNum);
        }

        public float GetVelocity()
        {
            return (float)(new Vector(MemoryAPI.ReadFromProcess<struct_Vector>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_vecVelocity)).Lenght2D());
        }

        public Vector GetViewOffset()
        {
            return new Vector(MemoryAPI.ReadFromProcess<struct_Vector>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_vecViewOffset));
        }

        public Int64 GetSpottedMask()
        {
            return MemoryAPI.ReadFromProcess<Int64>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_bSpottedByMask);
        }

        public bool IsOnGround()
        {
            return (GetFlags() & (1 << 0)) != 0;
        }

        public int GetFlags()
        {
            return MemoryAPI.ReadFromProcess<int>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_nFlags);
        }

        public bool IsSpottedByMask(CBasePlayer pEntity)
        {
            return (GetSpottedMask() & (1 << (pEntity.GetIndex() - 1))) != 0;
        }

        public Vector GetEyePosition()
        {
            return GetOrigin() + GetViewOffset();
        }
    }
}
