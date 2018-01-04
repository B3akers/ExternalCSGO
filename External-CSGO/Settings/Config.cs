using SimpleExternalCheatCSGO.Structs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SimpleExternalCheatCSGO.Settings
{
    public enum GlowType
    {
        Color = 0,
        Vis_Color,
        Health,
    };

    public enum BoneSelector
    {
        Head = (1 << 0),
        Neck = (1 << 1),
        Chest = (1 << 2),
        Stomach = (1 << 3)
    };

    public class WeaponConfig
    {
        #region TriggerBOT
        public bool bTriggerBotEnabled = true;
        public bool bFriendlyFire = false;
        public int iDelay = 1;
        public int iPause = 1;
        #endregion
        #region AimBot
        public bool bAimbotEnabled = true;
        public bool bVisibleCheck = true;
        public bool bTargetOnGroundCheck = true;
        public int iAimbotDeathBreak = 350;
        public float flAimbotFov = 12f;
        public float flAimbotSmooth = 15f;
        #endregion
        #region RecoilControlSystem
        public int iBones = (int)BoneSelector.Head | (int)BoneSelector.Neck;
        public float Vertical = 9f;
        public float Horizontal = 9f;
        public bool bRCS = true;
        #endregion
    }

    public static class Config
    {
        #region WeaponConfig
        public static Dictionary<int, WeaponConfig> WeaponsConfig = new Dictionary<int, WeaponConfig>();

        public static WeaponConfig GetWeaponConfig(int weapon_id)
        {
            if (WeaponsConfig.Count <= 0)
            {
                var weaponconfig = new WeaponConfig();
                WeaponsConfig.Add(0, weaponconfig);
            }
            if (WeaponsConfig.ContainsKey(weapon_id))
                return WeaponsConfig[weapon_id];
            return WeaponsConfig[0];
        }
        #endregion

        #region GlobalsConfig
        public static bool AimbotEnabled = true;
        public static bool TriggerBotEnabled = true;
        public static bool MiscEnabled = true;
        public static bool ESPEnabled = true;
        public static bool AutoPistol = true;
        public static int iAimbotKey = 1;
        public static int iTriggerBotKey = 6;
        public static int iTriggerBotWorkWithAimKey = 5;
        public static int iPanicKey = 0x79;
        #endregion

        #region GlowESP
        public static bool bRadarEnabled = false;
        public static bool bGlowEnabled = true;
        public static bool bGlowEnemy = true;
        public static bool bGlowAlly = true;
        public static bool bInnerGlow = false;
        public static bool bFullRender = false;
        public static bool bGlowAfterDeath = false;
        public static bool bGlowWeapons = false;
        public static bool bGlowBomb = false;
        public static bool bCHAMSGOWNOPlayer = false;
        public static GlowType iGlowType = GlowType.Color;
        public static int iGlowToogleKey = -1;
        public static Color bGlowEnemyColor = new Color(200, 0, 0, 200);
        public static Color bGlowEnemyVisibleColor = new Color(255, 0, 65, 150);
        public static Color bGlowAllyColor = new Color(200, 200, 0, 200);
        public static Color bGlowAllyVisibleColor = new Color(0, 169, 251, 150);

        public static Color bChamsEnemyColor = new Color(255, 169, 0, 200);
        public static Color bChamsAllyColor = new Color(0, 255, 50, 200);
        #endregion

        #region BunnyHOP
        public static bool bBunnyHopEnabled = true;
        public static int iBunnyHopKey = 32;
        #endregion

        #region AutoAccept&ShowRanks
        public static bool bAutoAccept = false;
        public static bool bShowRanks = true;
        #endregion

        #region Fakelag
        public static bool bFakeLag = false;
        public static int iFakeLagPower = 4;
        #endregion

        #region FovChanger
        public static bool bFovChangerEnabled = false;
        public static int FovValue = 100;
        #endregion

        #region Namestealer
        public static bool bNameStealerEnabled = false;
        public static int iNameStealerMode = 0;
        public static int iNameStealerType = 0;
        public static bool bNameStealerCustom = false;
        public static int iNameStealerDelay = 250;

        public static string szName1 = "---SimpleExternalCheat";
        public static string szName2 = "SimpleExternalCheat---";
        #endregion

        #region ClantagChanger
        public static bool bClanTagChangerEnabled = false;
        public static int iClanTagChanger = 0;
        public static string szClanTag1 = "---SimpleExternalCheat";
        public static string szClanTag2 = "SimpleExternalCheat---";
        public static int iClantTagDelay = 250;
        #endregion
    }
}
