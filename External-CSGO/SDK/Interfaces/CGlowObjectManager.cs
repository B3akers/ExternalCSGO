using SimpleExternalCheatCSGO.API;
using SimpleExternalCheatCSGO.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleExternalCheatCSGO.SDK
{
    public class CGlowObjectManager
    {
        public IntPtr pThis;
        public void Update()
        {
            pThis = MemoryAPI.ReadFromProcess<IntPtr>(Globals._csgo.ProcessHandle, Globals._csgo.CSGOModules["client"].BaseAddress + Globals._signatures.dw_GlowObjectManager);
        }
        public void RegisterGlowObject(IClientEntity pEntity, Color color, bool bInnerGlow, bool bFullRender = false)
        {
            RegisterGlowObject(pEntity.GetGlowIndex(), color.R, color.G, color.B, color.A, bInnerGlow, bFullRender);
        }

        public IClientEntity GetEntityByGlowIndex(int iGlowIndex)
        {
            GlowObjectDefinition_t glow = MemoryAPI.ReadFromProcess<GlowObjectDefinition_t>(Globals._csgo.ProcessHandle, pThis + (iGlowIndex * 0x38));
            return new IClientEntity(new IntPtr(glow.dwEntity));
        }

        public int Size()
        {
            return MemoryAPI.ReadFromProcess<int>(Globals._csgo.ProcessHandle, Globals._csgo.CSGOModules["client"].BaseAddress + Globals._signatures.dw_GlowObjectManager + 0xC);
        }

        public void RegisterGlowObject(int iGlowIndex, float r, float g, float b, float a, bool bInnerGlow, bool bFullRender = false)
        {
            GlowObjectDefinition_t glow = MemoryAPI.ReadFromProcess<GlowObjectDefinition_t>(Globals._csgo.ProcessHandle, pThis + (iGlowIndex * 0x38));
            glow.fR = r / 255f;
            glow.fG = g / 255f;
            glow.fB = b / 255f;
            glow.fAlpha = a / 255f;
            glow.m_bInnerGlow = bInnerGlow;
            glow.bRenderWhenOccluded = true;
            glow.bRenderWhenUnoccluded = false;
            glow.bFullBloomRender = bFullRender;
            MemoryAPI.WriteToProcess<GlowObjectDefinition_t>(Globals._csgo.ProcessHandle, pThis + (iGlowIndex * 0x38), glow);
        }

    }
}
