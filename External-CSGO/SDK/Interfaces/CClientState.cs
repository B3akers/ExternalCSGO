using SimpleExternalCheatCSGO.API;
using SimpleExternalCheatCSGO.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleExternalCheatCSGO.SDK
{
    public class CClientState
    {
        public IntPtr pThis;
        public ModelINetworkStringTable m_pModelPrecacheTable = null;
        public void Update()
        {
            pThis = MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, Globals._csgo.CSGOModules["engine"].BaseAddress + Globals._signatures.dw_ClientState);
            if (IsInGame())
            {
                if (m_pModelPrecacheTable == null)
                    m_pModelPrecacheTable = GetModelPrecacheTable();
                if (m_pModelPrecacheTable != null && m_pModelPrecacheTable.lastMap != GetMapName())
                {
                    m_pModelPrecacheTable.Update();
                    m_pModelPrecacheTable.lastMap = GetMapName();
                }
            }
        }

        public int GetModelIndexByName(string model_name)
        {
            if (m_pModelPrecacheTable == null)
                return -1;
            if (m_pModelPrecacheTable.Items.ContainsKey(model_name))
                return m_pModelPrecacheTable.Items[model_name];
            else
                return -1;
        }

        public void ForceUpdate()
        {
            MemoryAPI.WriteToProcess<int>(Globals._csgo.ProcessHandle, pThis + 0x174, -1);
        }

        public int GetLastOutGoingCommand()
        {
            return MemoryAPI.ReadFromProcess<int>(Globals._csgo.ProcessHandle, pThis + Globals._signatures.dw_lastoutgoingcommand);
        }

        public void OneTickAttack()
        {
            MemoryAPI.WriteToProcess<int>(Globals._csgo.ProcessHandle, Globals._csgo.CSGOModules["client"].BaseAddress + Globals._signatures.dw_forceattack, 6);
        }

        public void PlusAttack()
        {
            MemoryAPI.WriteToProcess<int>(Globals._csgo.ProcessHandle, Globals._csgo.CSGOModules["client"].BaseAddress + Globals._signatures.dw_forceattack, 5);
        }

        public void MinusAttack()
        {
            MemoryAPI.WriteToProcess<int>(Globals._csgo.ProcessHandle, Globals._csgo.CSGOModules["client"].BaseAddress + Globals._signatures.dw_forceattack, 4);
        }
  
        public void PlusJump()
        {
            MemoryAPI.WriteToProcess<int>(Globals._csgo.ProcessHandle, Globals._csgo.CSGOModules["client"].BaseAddress + Globals._signatures.dw_forceJump, 5);
        }

        public void MinusJump()
        {
            MemoryAPI.WriteToProcess<int>(Globals._csgo.ProcessHandle, Globals._csgo.CSGOModules["client"].BaseAddress + Globals._signatures.dw_forceJump, 4);
        }

        public int GetJumpState()
        {
            return MemoryAPI.ReadFromProcess<int>(Globals._csgo.ProcessHandle, Globals._csgo.CSGOModules["client"].BaseAddress + Globals._signatures.dw_forceJump);
        }

        public int GetAttackState()
        {
            return MemoryAPI.ReadFromProcess<int>(Globals._csgo.ProcessHandle, Globals._csgo.CSGOModules["client"].BaseAddress + Globals._signatures.dw_forceattack);
        }

        public string GetMapDirectory()
        {
            return MemoryAPI.ReadTextFromProcess(Globals._csgo.ProcessHandle, pThis + Globals._signatures.m_dwMapDirectory, 64);
        }

        public string GetMapName()
        {
            string directory = GetMapDirectory();
            return directory.Substring(5, directory.LastIndexOf('.') - 5);
        }

        public ModelINetworkStringTable GetModelPrecacheTable()
        {
            IntPtr pPointer = MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, pThis + Globals._signatures.dw_ModelPrecacheTable);

            return pPointer != IntPtr.Zero ? new ModelINetworkStringTable(pPointer) : null;
        }

        public IntPtr GetUserInfoTable()
        {
            return MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, pThis + Globals._signatures.dw_UserInfoTable);
        }

        public player_info_s GetPlayerInfo(int index)
        {
            var items = MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, GetUserInfoTable() + 0x3C) + 0xC);
            return MemoryAPI.ReadFromProcess<player_info_s>(Globals._csgo.ProcessHandle, MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, items + 0x20 + ((index - 1) * 0x34)));
        }

        public bool IsInGame()
        {
            return MemoryAPI.ReadFromProcess<int>(Globals._csgo.ProcessHandle, pThis + Globals._signatures.dw_isingame) == 6;
        }

        public void SetSendPacket(bool bSendPacket)
        {
            MemoryAPI.WriteToProcess<bool>(Globals._csgo.ProcessHandle, Globals._csgo.CSGOModules["engine"].BaseAddress + Globals._signatures.dw_bSendPackets, bSendPacket);
        }
        public bool GetSendPacket()
        {
            return MemoryAPI.ReadFromProcess<bool>(Globals._csgo.ProcessHandle, Globals._csgo.CSGOModules["engine"].BaseAddress + Globals._signatures.dw_bSendPackets);
        }
        public Vector GetViewAngles()
        {
            return new Vector(MemoryAPI.ReadFromProcess<struct_Vector>(Globals._csgo.ProcessHandle, pThis + Globals._signatures.dw_viewangles));
        }

        public void SetViewAngles(Vector angles)
        {
            MemoryAPI.WriteToProcess<struct_Vector>(Globals._csgo.ProcessHandle, pThis + Globals._signatures.dw_viewangles, angles.ToStruct());
        }
    }
}
