using SimpleExternalCheatCSGO.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleExternalCheatCSGO.SDK
{
    public class CBaseCombatWeapon : IClientEntity
    {
        public CBaseCombatWeapon(IntPtr pEntity)
        {
            pThis = pEntity;
        }

        public int GetAvailableAmmo()
        {
            return MemoryAPI.ReadFromProcess<int>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_iClip1);
        }

        public int GetWeaponID()
        {
            return MemoryAPI.ReadFromProcess<int>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_iItemDefinitionIndex);
        }

        public void SetAccountID(int accountid)
        {
            MemoryAPI.WriteToProcess<int>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_iAccountID, accountid);
        }

        public void SetItemDefinitionIndex(int itemdefiniotion)
        {
            MemoryAPI.WriteToProcess<int>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_iItemDefinitionIndex, itemdefiniotion);
        }

        public void SetFallbackPaintKit(int fallback_skin)
        {
            MemoryAPI.WriteToProcess<int>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_nFallbackPaintKit, fallback_skin);
        }

        public void SetOriginalOwnerXuidLow(int xuidlow)
        {
            MemoryAPI.WriteToProcess<int>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_OriginalOwnerXuidLow, xuidlow);
        }

        public void SetOriginalOwnerXuidHigh(int xuidhigh)
        {
            MemoryAPI.WriteToProcess<int>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_OriginalOwnerXuidHigh, xuidhigh);
        }

        public void SetFallbackWear(float wear)
        {
            MemoryAPI.WriteToProcess<float>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_flFallbackWear, wear);
        }

        public void SetItemIDHigh(int id)
        {
            MemoryAPI.WriteToProcess<int>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_iItemIDHigh, id);
        }

        public void SetFallbackStatTrak(int stattrak)
        {
            MemoryAPI.WriteToProcess<int>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_nFallbackStatTrak, stattrak);
        }

        public int GetFallbackPaintKit()
        {
            return MemoryAPI.ReadFromProcess<int>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_nFallbackPaintKit);
        }

        public int GetFallbackStatTrak()
        {
            return MemoryAPI.ReadFromProcess<int>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_nFallbackStatTrak);
        }

        public float GetFallbackWear()
        {
            return MemoryAPI.ReadFromProcess<float>(Globals._csgo.ProcessHandle, pThis + Globals._netvar.m_flFallbackWear);
        }

        public bool IsBomb()
        {
            var id = this.GetWeaponID();
            switch (id)
            {
                case 49:
                    return true;
            }

            return false;
        }

        public bool IsGrenade()
        {
            var id = this.GetWeaponID();
            switch (id)
            {
                case 43:
                case 44:
                case 45:
                case 46:
                case 47:
                case 48:
                    return true;
            }

            return false;
        }

        public bool IsKnife()
        {
            var id = this.GetWeaponID();
            switch (id)
            {
                case 42:
                case 59:
                case 500:
                case 505:
                case 506:
                case 507:
                case 508:
                case 509:
                case 512:
                case 514:
                case 515:
                case 516:
                    return true;
            }

            return false;
        }

        public bool IsPistol()
        {
            var id = this.GetWeaponID();
            switch (id)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 30:
                case 32:
                case 36:
                case 61:
                    return true;
            }

            return false;
        }

        public bool IsSniper()
        {
            var id = this.GetWeaponID();
            switch (id)
            {
                case 9:
                case 11:
                case 38:
                case 40:
                    return true;
            }

            return false;
        }

        public bool IsRestrictedRcs()
        {
            var id = this.GetWeaponID();
            switch (id)
            {
                case 1:
                case 9:
                case 11:
                case 25:
                case 27:
                case 29:
                case 35:
                case 38:
                case 40:
                    return true;
            }

            return false;
        }

        public int GetWeaponTableIndex()
        {
            return MemoryAPI.ReadFromProcess<int>(Globals._csgo.ProcessHandle, pThis + Globals._signatures.dw_WeaponTableIndex);
        }

    }
}
