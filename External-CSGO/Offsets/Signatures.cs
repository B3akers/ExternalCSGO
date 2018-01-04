using SimpleExternalCheatCSGO.API;
using SimpleExternalCheatCSGO.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleExternalCheatCSGO.Offsets
{
    public class Signatures
    {
        public int dw_LocalPlayer;

        public int dw_traceline;

        public int dw_entitylist;

        public int dw_lineThroughSmoke;

        public int dw_CLobbyScreen;

        public int dw_AcceptMatch;

        public int dw_MatchFound;

        public int dw_MatchAccepted;

        public int dw_ChangeClanTag;

        public int dw_RevealRankFn;

        public int dw_pInput;

        public int dw_ClientState;

        public int dw_viewangles;

        public int dw_lastoutgoingcommand;

        public int dw_bSendPackets;

        public int dw_GlobalVars;

        public int dw_bDormant;

        public int m_pStudioHdr;

        public int dw_GlowObjectManager;

        public int dw_forceJump;

        public int dw_isingame;

        public int m_dwMapDirectory;

        public int dw_HighestEntityIndex;

        public int dw_ModelPrecacheTable;

        public int dw_UserInfoTable;

        public int dw_Convarchartable;

        public int dw_enginecvar;

        public int dw_clientcmd;

        public int dw_inputsystem;

        public int dw_inputenabled;

        public int dw_WeaponTable;

        public int dw_WeaponTableIndex;

        public int dw_forceattack;

        public void Init()
        {
            dw_LocalPlayer = PatternScanner.FindPattern(Globals._csgo.ProcessHandle, "A3 ? ? ? ? C7 05 ? ? ? ? ? ? ? ? E8 ? ? ? ? 59 C3 6A ?", Globals._csgo.CSGOModules["client"], true, 0x1, true, 0) + 16;

            Console.WriteLine("- dw_LocalPlayer         => 0x" + dw_LocalPlayer.ToString("X"));

            dw_traceline = PatternScanner.FindPattern(Globals._csgo.ProcessHandle, "55 8B EC 83 E4 F0 83 EC 7C 56 52", Globals._csgo.CSGOModules["client"], true);

            Console.WriteLine("- dw_traceline           => 0x" + dw_traceline.ToString("X"));
            dw_traceline += (int)Globals._csgo.CSGOModules["client"].BaseAddress;

            dw_lineThroughSmoke = PatternScanner.FindPattern(Globals._csgo.ProcessHandle, "55 8B EC 83 EC 08 8B 15 ? ? ? ? 0F 57 C0", Globals._csgo.CSGOModules["client"], true);

            Console.WriteLine("- dw_lineThroughSmoke    => 0x" + dw_lineThroughSmoke.ToString("X"));
            dw_lineThroughSmoke += (int)Globals._csgo.CSGOModules["client"].BaseAddress;

            dw_entitylist = PatternScanner.FindPattern(Globals._csgo.ProcessHandle, "BB ? ? ? ? 83 FF 01 0F 8C ? ? ? ? 3B F8", Globals._csgo.CSGOModules["client"], true, 0x1, true, 0);

            Console.WriteLine("- dw_entitylist          => 0x" + dw_entitylist.ToString("X"));

            dw_CLobbyScreen = PatternScanner.FindPattern(Globals._csgo.ProcessHandle, "A1 ? ? ? ? 85 C0 74 0F 6A 00", Globals._csgo.CSGOModules["client"], true, 0x1, true, 0);

            Console.WriteLine("- dw_CLobbyScreen        => 0x" + dw_CLobbyScreen.ToString("X"));

            dw_AcceptMatch = PatternScanner.FindPattern(Globals._csgo.ProcessHandle, "55 8B EC 83 E4 F8 83 EC 08 56 8B 35 ? ? ? ? 57 83 BE", Globals._csgo.CSGOModules["client"], true);

            Console.WriteLine("- dw_AcceptMatch         => 0x" + dw_AcceptMatch.ToString("X"));
            dw_AcceptMatch += (int)Globals._csgo.CSGOModules["client"].BaseAddress;

            dw_MatchAccepted = PatternScanner.FindPattern(Globals._csgo.ProcessHandle, "89 B7 ? ? ? ? 8B 4F 04 85 C9", Globals._csgo.CSGOModules["client"], false, 0x2, true);

            Console.WriteLine("- dw_MatchAccepted       => 0x" + dw_MatchAccepted.ToString("X"));

            dw_MatchFound = PatternScanner.FindPattern(Globals._csgo.ProcessHandle, "89 87 ? ? ? ? 8B 87 ? ? ? ? 3B F0", Globals._csgo.CSGOModules["client"], false, 0x2, true);

            Console.WriteLine("- dw_MatchFound          => 0x" + dw_MatchFound.ToString("X"));

            dw_ChangeClanTag = PatternScanner.FindPattern(Globals._csgo.ProcessHandle, "53 56 57 8B DA 8B F9 FF 15", Globals._csgo.CSGOModules["engine"], true);

            Console.WriteLine("- dw_ChangeClanTag       => 0x" + dw_ChangeClanTag.ToString("X"));
            dw_ChangeClanTag += (int)Globals._csgo.CSGOModules["engine"].BaseAddress;

            dw_RevealRankFn = PatternScanner.FindPattern(Globals._csgo.ProcessHandle, "55 8B EC 8B 0D ? ? ? ? 68", Globals._csgo.CSGOModules["client"], true);

            Console.WriteLine("- dw_RevealRankFn        => 0x" + dw_RevealRankFn.ToString("X"));
            dw_RevealRankFn += (int)Globals._csgo.CSGOModules["client"].BaseAddress;

            dw_pInput = PatternScanner.FindPattern(Globals._csgo.ProcessHandle, "B9 ? ? ? ? F3 0F 11 04 24 FF 50 10", Globals._csgo.CSGOModules["client"], true, 0x1, true);

            Console.WriteLine("- dw_pInput              => 0x" + dw_pInput.ToString("X"));

            dw_ClientState = PatternScanner.FindPattern(Globals._csgo.ProcessHandle, "A1 ? ? ? ? 33 D2 6A 00 6A 00 33 C9 89 B0", Globals._csgo.CSGOModules["engine"], true, 0x1, true);

            Console.WriteLine("- dw_ClientState         => 0x" + dw_ClientState.ToString("X"));

            dw_viewangles = PatternScanner.FindPattern(Globals._csgo.ProcessHandle, "F3 0F 11 80 ? ? ? ? D9 46 04", Globals._csgo.CSGOModules["engine"], false, 0x4, true);

            Console.WriteLine("- dw_viewangles          => 0x" + dw_viewangles.ToString("X"));

            dw_lastoutgoingcommand = PatternScanner.FindPattern(Globals._csgo.ProcessHandle, "C7 80 ? ? ? ? ? ? ? ? A1 ? ? ? ? F2 0F 10 05 ? ? ? ? F2 0F 11 80 ? ? ? ? FF 15", Globals._csgo.CSGOModules["engine"], false, 0x2, true);

            Console.WriteLine("- dw_lastoutgoingcommand => 0x" + dw_lastoutgoingcommand.ToString("X"));

            dw_bSendPackets = PatternScanner.FindPattern(Globals._csgo.ProcessHandle, "B3 01 8B 01 8B 40 10 FF D0 84 C0 74 0F 80 BF ? ? ? ? ? 0F 84", Globals._csgo.CSGOModules["engine"], true) + 0x1;

            Console.WriteLine("- dw_bSendPackets        => 0x" + dw_bSendPackets.ToString("X"));

            dw_GlobalVars = PatternScanner.FindPattern(Globals._csgo.ProcessHandle, "68 ? ? ? ? 68 ? ? ? ? FF 50 08 85 C0", Globals._csgo.CSGOModules["engine"], true, 0x1, true);

            Console.WriteLine("- dw_GlobalVars          => 0x" + dw_GlobalVars.ToString("X"));

            dw_bDormant = PatternScanner.FindPattern(Globals._csgo.ProcessHandle, "55 8B EC 53 8B 5D 08 56 8B F1 88 9E ? ? ? ? E8", Globals._csgo.CSGOModules["client"], false, 0xC, true);

            Console.WriteLine("- dw_bDormant            => 0x" + dw_bDormant.ToString("X"));

            m_pStudioHdr = PatternScanner.FindPattern(Globals._csgo.ProcessHandle, "8B B6 ? ? ? ? 85 F6 74 05 83 3E 00 75 02 33 F6 F3 0F 10 44 24", Globals._csgo.CSGOModules["client"], false, 0x2, true);

            Console.WriteLine("- dw_pStudioHdr          => 0x" + m_pStudioHdr.ToString("X"));

            dw_GlowObjectManager = PatternScanner.FindPattern(Globals._csgo.ProcessHandle, "A1 ? ? ? ? A8 01 75 4B", Globals._csgo.CSGOModules["client"], true, 0x1, true) + 4;

            Console.WriteLine("- dw_GlowObjectManager   => 0x" + dw_GlowObjectManager.ToString("X"));

            dw_forceJump = PatternScanner.FindPattern(Globals._csgo.ProcessHandle, "89 0D ? ? ? ? 8B 0D ? ? ? ? 8B F2 8B C1 83 CE 08", Globals._csgo.CSGOModules["client"], true, 0x2, true);

            Console.WriteLine("- dw_forceJump           => 0x" + dw_forceJump.ToString("X"));

            dw_forceattack = PatternScanner.FindPattern(Globals._csgo.ProcessHandle, "89 0D ? ? ? ? 8B 0D ? ? ? ? 8B F2 8B C1 83 CE 04", Globals._csgo.CSGOModules["client"], true, 0x2, true);

            Console.WriteLine("- dw_forceattack         => 0x" + dw_forceattack.ToString("X"));

            dw_isingame = PatternScanner.FindPattern(Globals._csgo.ProcessHandle, "83 B8 ? ? ? ? ? 0F 94 C0 C3", Globals._csgo.CSGOModules["engine"], false, 0x2, true);

            Console.WriteLine("- dw_isingame            => 0x" + dw_isingame.ToString("X"));

            m_dwMapDirectory = PatternScanner.FindPattern(Globals._csgo.ProcessHandle, "05 ? ? ? ? C3 CC CC CC CC CC CC CC 80 3D", Globals._csgo.CSGOModules["engine"], false, 0x1, true);

            Console.WriteLine("- dw_MapDirectory        => 0x" + m_dwMapDirectory.ToString("X"));

            dw_ModelPrecacheTable = PatternScanner.FindPattern(Globals._csgo.ProcessHandle, "8B 8E ? ? ? ? 8B D0 85 C9", Globals._csgo.CSGOModules["engine"], false, 0x2, true);

            Console.WriteLine("- dw_ModelPrecacheTable  => 0x" + dw_ModelPrecacheTable.ToString("X"));

            dw_UserInfoTable = PatternScanner.FindPattern(Globals._csgo.ProcessHandle, "8B 89 ? ? ? ? 85 C9 0F 84 ? ? ? ? 8B 01", Globals._csgo.CSGOModules["engine"], false, 0x2, true);

            Console.WriteLine("- dw_UserInfoTable       => 0x" + dw_UserInfoTable.ToString("X"));

            dw_Convarchartable = PatternScanner.FindPattern(Globals._csgo.ProcessHandle, "8B 3C 85", Globals._csgo.CSGOModules["vstdlib"], true, 0x3, true);

            Console.WriteLine("- dw_Convarchartable     => 0x" + dw_Convarchartable.ToString("X"));

            dw_enginecvar = PatternScanner.FindPattern(Globals._csgo.ProcessHandle, "8B 0D ? ? ? ? C7 05", Globals._csgo.CSGOModules["vstdlib"], true, 0x2, true);

            Console.WriteLine("- dw_enginecvar          => 0x" + dw_enginecvar.ToString("X"));

            dw_clientcmd = PatternScanner.FindPattern(Globals._csgo.ProcessHandle, "55 8B EC A1 ? ? ? ? 33 C9 8B 55 08", Globals._csgo.CSGOModules["engine"], true);

            Console.WriteLine("- dw_clientcmd           => 0x" + dw_clientcmd.ToString("X"));
            dw_clientcmd += (int)Globals._csgo.CSGOModules["engine"].BaseAddress;

            dw_WeaponTable = PatternScanner.FindPattern(Globals._csgo.ProcessHandle, "B9 ? ? ? ? 6A 00 FF 50 08 C3", Globals._csgo.CSGOModules["client"], true, 0x1, true);

            Console.WriteLine("- dw_WeaponTable         => 0x" + dw_WeaponTable.ToString("X"));

            dw_WeaponTableIndex = PatternScanner.FindPattern(Globals._csgo.ProcessHandle, "39 86 ? ? ? ? 74 06 89 86 ? ? ? ? 8B 86", Globals._csgo.CSGOModules["client"], false, 0x2, true);

            Console.WriteLine("- dw_WeaponTableIndex    => 0x" + dw_WeaponTableIndex.ToString("X"));

            dw_inputsystem = PatternScanner.FindPattern(Globals._csgo.ProcessHandle, "8B 0D ? ? ? ? FF 75 10", Globals._csgo.CSGOModules["inputsystem"], true, 0x2, true);

            Console.WriteLine("- dw_inputsystem         => 0x" + dw_inputsystem.ToString("X"));

            dw_inputenabled = MemoryAPI.ReadFromProcess<int>(Globals._csgo.ProcessHandle,
                MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle,
                 MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle,
                       MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, Globals._csgo.CSGOModules["inputsystem"].BaseAddress + dw_inputsystem))
                            + 0x2C /*(11 * 4)*/) + 0x8);

            Console.WriteLine("- dw_inputenabled        => 0x" + dw_inputenabled.ToString("X"));
        }
    }
}
