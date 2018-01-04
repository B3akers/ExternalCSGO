using SimpleExternalCheatCSGO.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleExternalCheatCSGO.SDK
{
    public class CInput
    {
        public IntPtr pThis;
        public CVerifiedUserCmdList pCVerifiedUserCmdList = new CVerifiedUserCmdList();
        public CUserCmdList pCUserCmdList = new CUserCmdList();
        public CInput()
        {
            pThis = Globals._csgo.CSGOModules["client"].BaseAddress + Globals._signatures.dw_pInput;
        }
        public void Update()
        {         
            pCUserCmdList.Update(pThis);
            pCVerifiedUserCmdList.Update(pThis);
        }

        public CVerifiedUserCmdList GetVerifiedUserCmd()
        {
            return pCVerifiedUserCmdList;
        }

        public CUserCmdList GetUserCmd()
        {
            return pCUserCmdList;
        }
        

    }
}
