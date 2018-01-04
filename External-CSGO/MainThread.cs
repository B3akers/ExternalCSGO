using SimpleExternalCheatCSGO.SDK;
using SimpleExternalCheatCSGO.Settings;
using SimpleExternalCheatCSGO.Structs;
using SimpleExternalCheatCSGO.CodeInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SimpleExternalCheatCSGO.API;
using System.Runtime.InteropServices;
using SimpleExternalCheatCSGO.SDK.MiscClasses;
using SimpleExternalCheatCSGO.Util;
using SimpleExternalCheatCSGO.Memory;
using System.Reflection;

namespace SimpleExternalCheatCSGO
{
    public static class MainThread
    {
        public static IClientEntityList g_EntityList;
        public static CInput g_Input;
        public static CConVarManager g_ConVar;
        public static CInputSystem g_InputSystem;

        public static CClientState g_pEngineClient = new CClientState();
        public static CGlobalVarsBase g_GlobalVars = new CGlobalVarsBase();
        public static CGlowObjectManager g_GlowObjectManager = new CGlowObjectManager();
        public static CLocalPlayer pLocal = new CLocalPlayer();
        public static CWeaponTable g_WeaponTable = new CWeaponTable();

        public static Vector last_vecPunch = new Vector(0, 0, 0);

        public static bool[] lastKeyState = new bool[255];
        public static bool[] currentKeyState = new bool[255];

        public static int lastOutgoingcommand;

        public static int chockedPackets = 0;

        public static bool bLastFakeLag = false;

        public static bool bDoForceUpdate = false;

        public static bool bSwapClantag = false;

        public static bool bSwapNamestealer = false;

        public static string LastTag = "";

        public static char[] Tag = new char[15];

        public static CBasePlayer pLastTarget = null;

        public static int BreakTickCount = 0;

        public static int ClantagTickCount = 0;

        public static int NamestealerTickCount = 0;

        public static bool WaitBreak = false;

        public static bool IsKeyPress(int key)
        {
            return !lastKeyState[key] && currentKeyState[key];
        }

        public static bool IsKeyState(int key)
        {
            return currentKeyState[key];
        }

        public static void Update()
        {
            g_GlobalVars.Update();
            pLocal.Update();
            g_EntityList.Update();
            g_GlowObjectManager.Update();
            g_Input.Update();
            g_WeaponTable.Update();
        }

        public static Thread TriggerThread;

        public static void StartTrigger()
        {
            Thread.Sleep(500);

            while (true)
            {
                #region TriggerBOT
                if (pLocal.pThis != IntPtr.Zero && g_pEngineClient.IsInGame() && pLocal.GetHealth() > 0)
                {
                    var LocalWeapon = pLocal.GetActiveWeapon();

                    var LocalWeaponID = LocalWeapon.GetIndex();

                    var LocalWeaponConfig = Config.GetWeaponConfig(LocalWeaponID);


                    if (LocalWeaponConfig.bTriggerBotEnabled && Config.TriggerBotEnabled)
                    {
                        bool isKeyPressed = IsKeyState(Config.iTriggerBotKey) || IsKeyState(Config.iTriggerBotWorkWithAimKey);

                        if (LocalWeapon.IsKnife() || LocalWeapon.IsBomb() || LocalWeapon.IsGrenade())
                            isKeyPressed = false;

                        if (isKeyPressed)
                        {
                            var cross = pLocal.GetCrosshairID();

                            if (cross > 0 && cross <= 64)
                            {
                                CBasePlayer pTarget = new CBasePlayer(g_EntityList.GetEntityByIndex(cross));
                                if (pTarget.pThis != IntPtr.Zero && !pTarget.IsDormant() && pTarget.GetHealth() > 0 && !pTarget.HasGunGameImmunity())
                                {
                                    if (LocalWeaponConfig.bFriendlyFire || pLocal.GetTeamNum() != pTarget.GetTeamNum())
                                    {
                                        Thread.Sleep(LocalWeaponConfig.iDelay);
                                        g_pEngineClient.OneTickAttack();
                                        Thread.Sleep(LocalWeaponConfig.iPause);
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion
                Thread.Sleep(5);
            }
        }

        public static void GlowESP(Team iLocalTeamNum, bool bLocalPlayerAlive)
        {
            if (Config.ESPEnabled && (Config.bGlowWeapons || Config.bGlowBomb) && Config.bGlowEnabled && (!Config.bGlowAfterDeath || !bLocalPlayerAlive))
            {
                for (int i = 0; i < g_GlowObjectManager.Size(); ++i)
                {
                    var pObject = g_GlowObjectManager.GetEntityByGlowIndex(i);

                    var class_id = pObject.GetClassID();

                    #region PlayerESP
                    if (class_id == CSGOClassID.CCSPlayer)
                    {
                        var pEntity = new CBasePlayer(pObject.pThis);

                        int EntityHealth = pEntity.GetHealth();

                        Team EntityTeamnum = pEntity.GetTeamNum();

                        if (!pEntity.IsDormant() && EntityHealth > 0)
                        {
                            bool bFillerGood = true;
                            if (iLocalTeamNum == EntityTeamnum && !Config.bGlowAlly)
                                bFillerGood = false;
                            else if (iLocalTeamNum != EntityTeamnum && !Config.bGlowEnemy)
                                bFillerGood = false;
                            if (bFillerGood)
                            {
                                Color color = new Color();
                                switch (Config.iGlowType)
                                {
                                    case GlowType.Color:
                                        color = iLocalTeamNum == EntityTeamnum ? Config.bGlowAllyVisibleColor : Config.bGlowEnemyVisibleColor;
                                        break;
                                    case GlowType.Vis_Color:
                                        bool bVisible = pEntity.IsSpottedByMask(pLocal);
                                        if (bVisible)
                                            color = iLocalTeamNum == EntityTeamnum ? Config.bGlowAllyVisibleColor : Config.bGlowEnemyVisibleColor;
                                        else
                                            color = iLocalTeamNum == EntityTeamnum ? Config.bGlowAllyColor : Config.bGlowEnemyColor;
                                        break;
                                    case GlowType.Health:
                                        color = new Color(255 - (EntityHealth * 2.55f), EntityHealth * 2.55f, 0, 255);
                                        break;
                                }
                                g_GlowObjectManager.RegisterGlowObject(pEntity, color, Config.bInnerGlow, Config.bFullRender);
                            }
                        }
                    }
                    #endregion

                    #region BombESP
                    else if (class_id == CSGOClassID.CPlantedC4 || class_id == CSGOClassID.CC4)
                        g_GlowObjectManager.RegisterGlowObject(i, 250, 0, 0, 200, Config.bInnerGlow, Config.bFullRender);
                    #endregion

                    #region WeaponESP
                    else if (class_id != CSGOClassID.CBaseWeaponWorldModel && (pObject.GetNetworkName().Contains("Weapon") || class_id == CSGOClassID.CAK47 || class_id == CSGOClassID.CDEagle))
                        g_GlowObjectManager.RegisterGlowObject(i, 200, 0, 50, 200, Config.bInnerGlow, Config.bFullRender);
                    else if (class_id == CSGOClassID.CBaseCSGrenadeProjectile || class_id == CSGOClassID.CDecoyProjectile ||
                             class_id == CSGOClassID.CMolotovProjectile || class_id == CSGOClassID.CSmokeGrenadeProjectile)
                        g_GlowObjectManager.RegisterGlowObject(i, 200, 0, 50, 200, Config.bInnerGlow, Config.bFullRender);
                    else if (class_id == CSGOClassID.CBaseAnimating && iLocalTeamNum == Team.CT)
                        g_GlowObjectManager.RegisterGlowObject(i, 100, 100, 200, 200, Config.bInnerGlow, Config.bFullRender);
                    #endregion
                }
            }
        }

        public static void Start()
        {
            GC.Collect();

            g_EntityList = new IClientEntityList();
            g_Input = new CInput();
            g_ConVar = new CConVarManager();
            g_InputSystem = new CInputSystem();

            var dwMouseEnabled = new ConVar("cl_mouseenable");

            var name = new ConVar("name");

            TriggerThread = new Thread(new ThreadStart(StartTrigger));
            TriggerThread.IsBackground = true;
            TriggerThread.Start();

            while (true)
            {
                try
                {
                    g_pEngineClient.Update();

                    if (g_pEngineClient.IsInGame())
                    {
                        bool bMouseEnabled = dwMouseEnabled.GetBool();

                        #region UpdateRegion

                        Update();

                        bool bLocalPlayerAlive = pLocal.GetHealth() > 0;
                        Team iLocalTeamNum = pLocal.GetTeamNum();

                        Vector localPlayerEyePosition = pLocal.GetEyePosition();

                        Vector viewangles = g_pEngineClient.GetViewAngles();

                        bool FindNewAimBotTarget = false;

                        var LocalWeapon = pLocal.GetActiveWeapon();

                        var LocalWeaponID = LocalWeapon.GetWeaponID();

                        var LocalWeaponConfig = Config.GetWeaponConfig(LocalWeaponID);

                        bool bAimBotKey = IsKeyState(Config.iAimbotKey) || IsKeyState(Config.iTriggerBotWorkWithAimKey);

                        if (LocalWeapon.IsKnife() || LocalWeapon.IsBomb() || LocalWeapon.IsGrenade())
                            bAimBotKey = false;

                        if (!bLocalPlayerAlive)
                            bAimBotKey = false;

                        if (LocalWeapon.GetAvailableAmmo() <= 0)
                            bAimBotKey = false;

                        if (!Config.AimbotEnabled)
                            bAimBotKey = false;

                        if (!bAimBotKey)
                        {
                            WaitBreak = false;
                            pLastTarget = null;
                        }

                        if (LocalWeaponConfig.bAimbotEnabled && bAimBotKey && pLastTarget == null)
                            FindNewAimBotTarget = true;

                        if (pLastTarget != null && LocalWeaponConfig.bVisibleCheck && !pLastTarget.IsSpottedByMask(pLocal))
                            FindNewAimBotTarget = true;

                        #endregion

                        #region KeyStateCheck
                        for (int i = 0; i < 255; ++i)
                            currentKeyState[i] = WinAPI.GetAsyncKeyState(i) != 0;

                        if (Config.iGlowToogleKey != -1 && IsKeyPress(Config.iGlowToogleKey))
                            Config.bGlowEnabled = !Config.bGlowEnabled;
                        if (Config.iPanicKey != -1 && IsKeyPress(Config.iPanicKey))
                        {
                            Globals._csgo.ProcessHandle = IntPtr.Zero;
                            Globals._csgo.CheckHandle();
                        }
                        #endregion

                        #region GlowESP

                        GlowESP(iLocalTeamNum, bLocalPlayerAlive);

                        #endregion

                        #region ForeachPlayerList
                        CBasePlayer pTarget = null;
                        float fDistance = -1;
                        List<string> names_to_steal = new List<string>();

                        foreach (var pPointer in g_EntityList.pPlayerList)
                        {
                            CBasePlayer pEntity = new CBasePlayer(pPointer);

                            int EntityHealth = pEntity.GetHealth();

                            Team EntityTeamnum = pEntity.GetTeamNum();

                            #region Namestealer
                            if (Config.MiscEnabled && Config.bNameStealerEnabled && !Config.bNameStealerCustom)
                                if (Config.iNameStealerType == 0 && EntityTeamnum == iLocalTeamNum)
                                    names_to_steal.Add(new string(g_pEngineClient.GetPlayerInfo(pEntity.GetIndex()).m_szPlayerName));
                                else if (Config.iNameStealerType == 1 && EntityTeamnum != iLocalTeamNum)
                                    names_to_steal.Add(new string(g_pEngineClient.GetPlayerInfo(pEntity.GetIndex()).m_szPlayerName));
                                else if (Config.iNameStealerType == 2)
                                    names_to_steal.Add(new string(g_pEngineClient.GetPlayerInfo(pEntity.GetIndex()).m_szPlayerName));
                            #endregion

                            #region Radar
                            if (Config.ESPEnabled && Config.bRadarEnabled)
                                pEntity.SetSpotted(true);
                            #endregion

                            #region RenderColor
                            if (Config.ESPEnabled && Config.bCHAMSGOWNOPlayer)
                                if (EntityTeamnum == iLocalTeamNum)
                                    pEntity.SetRenderColor(Config.bChamsAllyColor);
                                else
                                    pEntity.SetRenderColor(Config.bChamsEnemyColor);
                            #endregion

                            #region GlowESPOnlyPlayers
                            if (Config.ESPEnabled && !Config.bGlowWeapons && !Config.bGlowBomb && Config.bGlowEnabled && (!Config.bGlowAfterDeath || !bLocalPlayerAlive))
                            {
                                if (!pEntity.IsDormant() && EntityHealth > 0)
                                {
                                    bool bFillerGood = true;
                                    if (iLocalTeamNum == EntityTeamnum && !Config.bGlowAlly)
                                        bFillerGood = false;
                                    else if (iLocalTeamNum != EntityTeamnum && !Config.bGlowEnemy)
                                        bFillerGood = false;
                                    if (bFillerGood)
                                    {
                                        Color color = new Color();
                                        switch (Config.iGlowType)
                                        {
                                            case GlowType.Color:
                                                color = iLocalTeamNum == EntityTeamnum ? Config.bGlowAllyVisibleColor : Config.bGlowEnemyVisibleColor;
                                                break;
                                            case GlowType.Vis_Color:
                                                bool bVisible = pEntity.IsSpottedByMask(pLocal);
                                                if (bVisible)
                                                    color = iLocalTeamNum == EntityTeamnum ? Config.bGlowAllyVisibleColor : Config.bGlowEnemyVisibleColor;
                                                else
                                                    color = iLocalTeamNum == EntityTeamnum ? Config.bGlowAllyColor : Config.bGlowEnemyColor;
                                                break;
                                            case GlowType.Health:
                                                color = new Color(255 - (EntityHealth * 2.55f), EntityHealth * 2.55f, 0, 255);
                                                break;
                                        }
                                        g_GlowObjectManager.RegisterGlowObject(pEntity, color, Config.bInnerGlow, Config.bFullRender);
                                    }
                                }
                            }
                            #endregion

                            #region FindAimBotTarget
                            if (FindNewAimBotTarget)
                            {
                                if (!pEntity.IsDormant() && EntityHealth > 0 && !pEntity.HasGunGameImmunity())
                                {
                                    if (LocalWeaponConfig.bFriendlyFire || EntityTeamnum != iLocalTeamNum)
                                    {
                                        if (!LocalWeaponConfig.bVisibleCheck || pEntity.IsSpottedByMask(pLocal))
                                        {
                                            if (!LocalWeaponConfig.bTargetOnGroundCheck || pEntity.IsOnGround())
                                            {
                                                float cur_distance = MathUtil.FovToPlayer(localPlayerEyePosition, viewangles, pEntity, 0);

                                                if (cur_distance <= LocalWeaponConfig.flAimbotFov)
                                                {
                                                    if (fDistance == -1 || cur_distance < fDistance)
                                                    {
                                                        fDistance = cur_distance;
                                                        pTarget = pEntity;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                        #endregion

                        #region LocalPlayerFunctions

                        #region BunnyHop

                        bool bLocalIsOnGround = pLocal.IsOnGround();

                        if (bMouseEnabled && Config.MiscEnabled && Config.bBunnyHopEnabled && IsKeyState(Config.iBunnyHopKey) && pLocal.GetVelocity() > 45f)
                        {
                            if (bLocalIsOnGround)
                                g_pEngineClient.PlusJump();
                            else if (g_pEngineClient.GetJumpState() == 5)
                                g_pEngineClient.MinusJump();
                        }
                        #endregion

                        #region FovChanger

                        if (Config.MiscEnabled && Config.bFovChangerEnabled && !pLocal.IsScoped())
                            pLocal.SetFov(Config.FovValue);
                        else if (!pLocal.IsScoped())
                            pLocal.ResetFov();
                        #endregion

                        #endregion

                        #region Aimbot
                        bool b_do_RCS = false;
                        bool b_do_Aimbot = false;
                        if (FindNewAimBotTarget && fDistance != -1)
                            pLastTarget = pTarget;

                        if (LocalWeaponConfig.bAimbotEnabled && bAimBotKey && pLastTarget != null)
                        {
                            if (pLastTarget.GetHealth() <= 0)
                            {
                                if (!WaitBreak)
                                {
                                    BreakTickCount = Environment.TickCount + LocalWeaponConfig.iAimbotDeathBreak;
                                    WaitBreak = true;
                                }
                                else if (BreakTickCount <= Environment.TickCount)
                                {
                                    WaitBreak = false;
                                    pLastTarget = null;
                                }
                            }
                            else
                            {
                                List<Vector> hitboxes = new List<Vector>();

                                if ((LocalWeaponConfig.iBones & (int)BoneSelector.Head) != 0)
                                    hitboxes.Add(pLastTarget.GetHitboxPosition((int)HitboxList.HITBOX_HEAD));
                                if ((LocalWeaponConfig.iBones & (int)BoneSelector.Neck) != 0)
                                    hitboxes.Add(pLastTarget.GetHitboxPosition((int)HitboxList.HITBOX_NECK));
                                if ((LocalWeaponConfig.iBones & (int)BoneSelector.Chest) != 0)
                                    hitboxes.Add(pLastTarget.GetHitboxPosition((int)HitboxList.HITBOX_CHEST));
                                if ((LocalWeaponConfig.iBones & (int)BoneSelector.Stomach) != 0)
                                    hitboxes.Add(pLastTarget.GetHitboxPosition((int)HitboxList.HITBOX_BODY));

                                float best = -1;
                                Vector calcang = new Vector(0, 0, 0);

                                for (int i = 0; i < hitboxes.Count(); i++)
                                {
                                    Vector temp_calcang = MathUtil.CalcAngle(localPlayerEyePosition, hitboxes[i]);
                                    Vector delta_bone = viewangles - temp_calcang;
                                    delta_bone.NormalizeAngles();
                                    float len = (float)delta_bone.Lenght2D();
                                    if (best == -1 || len < best)
                                    {
                                        best = len;
                                        calcang = temp_calcang;
                                    }
                                }

                                Vector current_punch = new Vector(last_vecPunch._x * 2f, last_vecPunch._y * (LocalWeaponConfig.Horizontal / 5f), 0);
                                calcang -= current_punch;
                                calcang.NormalizeAngles();
                                Vector delta = calcang - viewangles;
                                delta.NormalizeAngles();
                                if (LocalWeaponConfig.flAimbotSmooth != 0f)
                                    calcang = viewangles + (delta / LocalWeaponConfig.flAimbotSmooth);
                                calcang.NormalizeAngles();
                                viewangles = calcang;
                                b_do_Aimbot = true;
                            }
                        }
                        #endregion

                        #region RecoilControlSystem
                        if (Config.AimbotEnabled && LocalWeaponConfig.Vertical != 0f && LocalWeaponConfig.Horizontal != 0f && LocalWeaponConfig.bRCS)
                        {
                            if (pLocal.GetShootsFired() > 1 && IsKeyState(1) && LocalWeapon.GetAvailableAmmo() > 0)
                            {
                                Vector Punch = pLocal.GetPunch();

                                float[] multiple = { LocalWeaponConfig.Vertical / 5f, LocalWeaponConfig.Horizontal / 5f };

                                Vector current_RCS_Punch = new Vector((Punch._x * multiple[0]) - (last_vecPunch._x * multiple[0]), (Punch._y * multiple[1]) - (last_vecPunch._y * multiple[1]), 0);
                                Vector ViewAngle_RCS = viewangles - current_RCS_Punch;
                                ViewAngle_RCS.NormalizeAngles();
                                Vector delta = (viewangles - ViewAngle_RCS);
                                delta.NormalizeAngles();

                                if ((float)delta.Lenght2D() <= 3.1f)
                                    viewangles = ViewAngle_RCS;

                                last_vecPunch = Punch;

                                b_do_RCS = true;
                            }
                            else
                                last_vecPunch = new Vector(0, 0, 0);
                        }
                        #endregion

                        #region ShowRanks
                        if (Config.ESPEnabled && Config.bShowRanks)
                        {
                            var Command = g_pEngineClient.GetLastOutGoingCommand();

                            if (Command != lastOutgoingcommand)
                            {
                                var VerifiedCommandSystem = g_Input.GetVerifiedUserCmd();

                                var VerifiedCommand = VerifiedCommandSystem.GetVerifiedUserCmdBySequence(Command);

                                if ((VerifiedCommand.m_cmd.buttons & (1 << 16)) != 0)
                                    RevealRank.Do();
                            }

                            lastOutgoingcommand = Command;
                        }
                        #endregion

                        #region FakeLag
                        if (Config.MiscEnabled && Config.bFakeLag)
                        {
                            if (chockedPackets < Config.iFakeLagPower)
                            {
                                g_pEngineClient.SetSendPacket(false);
                                chockedPackets++;
                            }
                            else
                            {
                                g_pEngineClient.SetSendPacket(true);
                                chockedPackets = 0;
                            }
                        }
                        else if (Config.bFakeLag != bLastFakeLag)
                            g_pEngineClient.SetSendPacket(true);

                        bLastFakeLag = Config.bFakeLag;
                        #endregion

                        #region SetAngles
                        if (b_do_Aimbot || b_do_RCS)
                            g_pEngineClient.SetViewAngles(viewangles);
                        #endregion

                        #region ClanTagChanger
                        if (Config.MiscEnabled && Config.bClanTagChangerEnabled)
                        {
                            if (Environment.TickCount > ClantagTickCount)
                            {
                                if (Config.iClanTagChanger == 0)
                                {
                                    if (bSwapClantag)
                                        SendClantag.Do(Config.szClanTag1, "ozon");
                                    else
                                        SendClantag.Do(Config.szClanTag2, "ozon");
                                    bSwapClantag = !bSwapClantag;
                                }
                                else if (Config.iClanTagChanger == 1)
                                {
                                    if (LastTag != Config.szClanTag1)
                                    {
                                        LastTag = Config.szClanTag1;
                                        int start = 7 - LastTag.Length / 2;
                                        for (int i = 0; i < 15; i++)
                                        {
                                            if (i < start || i >= start + LastTag.Length)
                                                Tag[i] = ' ';
                                            else
                                                Tag[i] = LastTag[i - start];
                                        }
                                        SendClantag.Do(new string(Tag), "ozon");
                                    }
                                    else
                                    {
                                        char temp_var;

                                        for (int i = 0; i < (15 - 1); i++)
                                        {
                                            temp_var = Tag[15 - 1];
                                            Tag[15 - 1] = Tag[i];
                                            Tag[i] = temp_var;
                                        }
                                        SendClantag.Do(new string(Tag), "ozon");
                                    }
                                }
                                else if (Config.iClanTagChanger == 2)
                                    SendClantag.Do(DateTime.Now.ToString("hh:mm:ss"), "ozon");
                                ClantagTickCount = Environment.TickCount + Config.iClantTagDelay;
                            }
                        }
                        #endregion

                        #region Namestealer
                        if (Config.MiscEnabled && Config.bNameStealerEnabled)
                        {
                            if (Environment.TickCount > NamestealerTickCount)
                            {
                                if (Config.iNameStealerMode == 0)
                                {
                                    name.ClearCallbacks();
                                    if (Config.bNameStealerCustom)
                                    {
                                        if (bSwapNamestealer)
                                            ClientCmd.Do("name \"" + Config.szName1 + "\"");
                                        else
                                            ClientCmd.Do("name \"" + Config.szName2 + "\"");
                                        bSwapNamestealer = !bSwapNamestealer;
                                    }
                                    else
                                        ClientCmd.Do("name \"" + names_to_steal[RandomHelper.RandomInt(0, names_to_steal.Count - 1)] + "   \"");
                                }
                                NamestealerTickCount = Environment.TickCount + Config.iNameStealerDelay;
                            }
                        }
                        #endregion

                        #region AutoPistol
                        if (Config.MiscEnabled && bMouseEnabled && Config.AutoPistol && IsKeyState(0x01) && LocalWeaponID != 64 && LocalWeapon.IsPistol())
                        {
                            if (g_pEngineClient.GetAttackState() == 5)
                                g_pEngineClient.MinusAttack();
                            else
                                g_pEngineClient.PlusAttack();
                        }
                        #endregion

                        #region ForceUpdate
                        if (bDoForceUpdate)
                        {
                            g_pEngineClient.ForceUpdate();
                            bDoForceUpdate = false;
                        }
                        #endregion

                        Buffer.BlockCopy(currentKeyState, 0, lastKeyState, 0, 255);
                    }
                    else
                    {
                        if (!Globals._csgo.CheckHandle())
                            break;

                        Thread.Sleep(1000);

                        #region AutoAccept
                        if (Config.MiscEnabled && Config.bAutoAccept)
                        {
                            if (UTILS_GAME.MatchFound())
                            {
                                Thread.Sleep(1500);
                                UTILS_GAME.AcceptMatch();
                            }
                        }
                        #endregion
                    }
                }
                catch
                {
                    if (!Globals._csgo.CheckHandle())
                        break;
                }
                Thread.Sleep(5);
            }
            Program.StartCheat();
        }

    }
}
