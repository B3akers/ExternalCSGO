using SimpleExternalCheatCSGO.API;
using SimpleExternalCheatCSGO.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleExternalCheatCSGO.Offsets
{
    //https://github.com/Y3t1y3t/CSGO-Dumper/tree/master/Dumper/src
    public class NetVarManager
    {
        public Dictionary<string, Dictionary<string, int>> _tables = new Dictionary<string, Dictionary<string, int>>();

        void ScanTable(IntPtr table, int level, int offset, string name)
        {
            var count = MemoryAPI.ReadFromProcess<int>(Globals._csgo.ProcessHandle, table + 0x4);
            for (var i = 0; i < count; ++i)
            {
                int propID = MemoryAPI.ReadFromProcess<int>(Globals._csgo.ProcessHandle, table) + i * 0x3C;
                string propName = MemoryAPI.ReadTextFromProcess(Globals._csgo.ProcessHandle, MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, new IntPtr(propID)), 64);
                var isBaseClass = propName.IndexOf("baseclass") == 0;
                int propOffset = offset + MemoryAPI.ReadFromProcess<int>(Globals._csgo.ProcessHandle, new IntPtr(propID + 0x2C));
                if (!isBaseClass)
                {
                    if (!_tables.ContainsKey(name))
                        _tables.Add(name, new Dictionary<string, int>());
                    if (!_tables[name].ContainsKey(propName))
                        _tables[name].Add(propName, propOffset);

                }

                var child = MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, new IntPtr(propID + 0x28));
                if (child == IntPtr.Zero)
                    continue;

                if (isBaseClass)
                    --level;

                ScanTable(child, ++level, propOffset, name);
            }
        }

        //NET_VARS
        public int m_vecOrigin;
        public int m_iHealth;
        public int m_iGlowIndex;
        public int m_iTeamNum;
        public int m_vecViewOffset;
        public int m_Model;
        public int m_dwBoneMatrix;
        public int m_iIndex;
        public int m_bSpottedByMask;
        public int m_iItemDefinitionIndex;
        public int m_iClip1;
        public int m_iFOV;
        public int m_iDefaultFOV;
        public int m_iShotsFired;
        public int m_aimPunchAngle;
        public int m_nFlags;
        public int m_hMyWearables;

        public int m_OriginalOwnerXuidLow;
        public int m_OriginalOwnerXuidHigh;
        public int m_nFallbackPaintKit;
        public int m_nFallbackSeed;
        public int m_flFallbackWear;
        public int m_nFallbackStatTrak;
        public int m_iAccountID;
        public int m_iItemIDHigh;
        public int m_iEntityLevel;

        public int m_vecVelocity;
        public int m_hActiveWeapon;
        public int m_hMyWeapons;

        public int m_bIsScoped;
        public int m_bSpotted;
        public int m_iCrosshairId;

        public int m_bGunGameImmunity;
        public int m_clrRender;

        public void Init()
        {
            var _firstclass = new IntPtr(PatternScanner.FindPattern(Globals._csgo.ProcessHandle, "A1 ? ? ? ? 8B 4F 0C 85 C0 74 18 0F 1F 00 39 48 14 74 09 8B 40 10 85 C0 75 F4 EB 07 8B 58 08 85 DB 75 0E 68 ? ? ? ? FF 15 ? ? ? ? 83 C4 04 8B 47 18", Globals._csgo.CSGOModules["client"], false, 0x1, true));

            _firstclass = MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, _firstclass);

            do
            {
                var table = MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, (_firstclass + 0xC));
                if (table != IntPtr.Zero)
                {
                    string table_name = MemoryAPI.ReadTextFromProcess(Globals._csgo.ProcessHandle, MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, (table + 0xC)), 32);
                    ScanTable(table, 0, 0, table_name);
                }
                _firstclass = MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, (_firstclass + 0x10));
            } while (_firstclass != IntPtr.Zero);

            m_vecOrigin = _tables["DT_BaseEntity"]["m_vecOrigin"];

            Console.WriteLine("- m_vecOrigin            => 0x" + m_vecOrigin.ToString("X"));

            m_iHealth = _tables["DT_CSPlayer"]["m_iHealth"];

            Console.WriteLine("- m_iHealth              => 0x" + m_iHealth.ToString("X"));

            m_iGlowIndex = _tables["DT_CSPlayer"]["m_flFlashDuration"] + 0x18;

            Console.WriteLine("- m_iGlowIndex           => 0x" + m_iGlowIndex.ToString("X"));

            m_iTeamNum = _tables["DT_BaseEntity"]["m_iTeamNum"];

            Console.WriteLine("- m_iTeamNum             => 0x" + m_iTeamNum.ToString("X"));

            m_vecViewOffset = _tables["DT_CSPlayer"]["m_vecViewOffset[0]"];

            Console.WriteLine("- m_vecViewOffset        => 0x" + m_vecViewOffset.ToString("X"));

            m_Model = 0x6C;

            Console.WriteLine("- m_Model                => 0x" + m_Model.ToString("X"));

            m_dwBoneMatrix = _tables["DT_BaseAnimating"]["m_nForceBone"] + 28;

            Console.WriteLine("- m_dwBoneMatrix         => 0x" + m_dwBoneMatrix.ToString("X"));

            m_iIndex = 0x64;

            Console.WriteLine("- m_iIndex               => 0x" + m_iIndex.ToString("X"));

            m_bSpottedByMask = _tables["DT_BaseEntity"]["m_bSpottedByMask"];

            Console.WriteLine("- m_bSpottedByMask       => 0x" + m_bSpottedByMask.ToString("X"));

            m_iItemDefinitionIndex = _tables["DT_BaseCombatWeapon"]["m_iItemDefinitionIndex"];

            Console.WriteLine("- m_iItemDefinitionIndex => 0x" + m_iItemDefinitionIndex.ToString("X"));

            m_iClip1 = _tables["DT_BaseCombatWeapon"]["m_iClip1"];

            Console.WriteLine("- m_iClip1               => 0x" + m_iClip1.ToString("X"));

            m_iFOV = _tables["DT_CSPlayer"]["m_iFOV"];

            Console.WriteLine("- m_iFOV                 => 0x" + m_iFOV.ToString("X"));

            m_iDefaultFOV = _tables["DT_CSPlayer"]["m_iDefaultFOV"];

            Console.WriteLine("- m_iDefaultFOV          => 0x" + m_iDefaultFOV.ToString("X"));

            m_iShotsFired = _tables["DT_CSPlayer"]["m_iShotsFired"];

            Console.WriteLine("- m_iShotsFired          => 0x" + m_iShotsFired.ToString("X"));

            m_aimPunchAngle = _tables["DT_BasePlayer"]["m_aimPunchAngle"];

            Console.WriteLine("- m_aimPunchAngle        => 0x" + m_aimPunchAngle.ToString("X"));

            m_nFlags = _tables["DT_CSPlayer"]["m_fFlags"];

            Console.WriteLine("- m_fFlags               => 0x" + m_nFlags.ToString("X"));

            m_hMyWearables = _tables["DT_BaseCombatCharacter"]["m_hMyWearables"];

            Console.WriteLine("- m_hMyWearables         => 0x" + m_hMyWearables.ToString("X"));

            m_iEntityLevel = _tables["DT_BaseAttributableItem"]["m_iEntityLevel"];

            Console.WriteLine("- m_iEntityLevel         => 0x" + m_iEntityLevel.ToString("X"));

            m_iItemIDHigh = _tables["DT_BaseAttributableItem"]["m_iItemIDHigh"];

            Console.WriteLine("- m_iItemIDHigh          => 0x" + m_iItemIDHigh.ToString("X"));

            m_iAccountID = _tables["DT_BaseAttributableItem"]["m_iAccountID"];

            Console.WriteLine("- m_iAccountID           => 0x" + m_iAccountID.ToString("X"));

            m_OriginalOwnerXuidLow = _tables["DT_BaseAttributableItem"]["m_OriginalOwnerXuidLow"];

            Console.WriteLine("- m_OriginalOwnerXuidLow => 0x" + m_OriginalOwnerXuidLow.ToString("X"));

            m_OriginalOwnerXuidHigh = _tables["DT_BaseAttributableItem"]["m_OriginalOwnerXuidHigh"];

            Console.WriteLine("- m_OriginalOwneruidHigh => 0x" + m_OriginalOwnerXuidHigh.ToString("X"));

            m_nFallbackPaintKit = _tables["DT_BaseAttributableItem"]["m_nFallbackPaintKit"];

            Console.WriteLine("- m_nFallbackPaintKit    => 0x" + m_nFallbackPaintKit.ToString("X"));

            m_nFallbackSeed = _tables["DT_BaseAttributableItem"]["m_nFallbackSeed"];

            Console.WriteLine("- m_nFallbackSeed        => 0x" + m_nFallbackSeed.ToString("X"));

            m_flFallbackWear = _tables["DT_BaseAttributableItem"]["m_flFallbackWear"];

            Console.WriteLine("- m_flFallbackWear       => 0x" + m_flFallbackWear.ToString("X"));

            m_nFallbackStatTrak = _tables["DT_BaseAttributableItem"]["m_nFallbackStatTrak"];

            Console.WriteLine("- m_nFallbackStatTrak    => 0x" + m_nFallbackStatTrak.ToString("X"));

            m_vecVelocity = _tables["DT_BasePlayer"]["m_vecVelocity[0]"];

            Console.WriteLine("- m_vecVelocity          => 0x" + m_vecVelocity.ToString("X"));

            m_hActiveWeapon = _tables["DT_CSPlayer"]["m_hActiveWeapon"];

            Console.WriteLine("- m_hActiveWeapon        => 0x" + m_hActiveWeapon.ToString("X"));

            m_hMyWeapons = _tables["DT_CSPlayer"]["m_hMyWeapons"];

            Console.WriteLine("- m_hMyWeapons           => 0x" + m_hMyWeapons.ToString("X"));

            m_bIsScoped = _tables["DT_CSPlayer"]["m_bIsScoped"];

            Console.WriteLine("- m_bIsScoped            => 0x" + m_bIsScoped.ToString("X"));

            m_iCrosshairId = _tables["DT_CSPlayer"]["m_bHasDefuser"] + 92;

            Console.WriteLine("- m_iCrosshairId         => 0x" + m_iCrosshairId.ToString("X"));

            m_bGunGameImmunity = _tables["DT_CSPlayer"]["m_bGunGameImmunity"];

            Console.WriteLine("- m_bGunGameImmunity     => 0x" + m_bGunGameImmunity.ToString("X"));

            m_bSpotted = _tables["DT_BaseEntity"]["m_bSpotted"];

            Console.WriteLine("- m_bSpotted             => 0x" + m_bSpotted.ToString("X"));

            m_clrRender = _tables["DT_BaseEntity"]["m_clrRender"];

            Console.WriteLine("- m_clrRender            => 0x" + m_clrRender.ToString("X"));
            
        }
    }
}
