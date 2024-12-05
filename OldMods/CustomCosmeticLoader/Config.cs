using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using I2.Loc.SimpleJSON;
using UnityEngine;

namespace OldMods.CustomCosmeticLoader
{
    // Token: 0x0200000A RID: 10
    public class Config
    {
        // Token: 0x06000031 RID: 49 RVA: 0x00002912 File Offset: 0x00000B12
        public static void Init()
        {
            bool flag = File.Exists(Config.GetConfigPath());
            if (flag)
            {
                Config.ReadConfig();
            }
            var SkinsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), SKINS_DIR_NAME);
            foreach (var Skin in Directory.GetFiles(SkinsPath))
            {
                var t2d = new Texture2D(2048, 2048);
                t2d.LoadImage(File.ReadAllBytes(Skin));
                skins[Path.GetFileNameWithoutExtension(Skin)] = t2d;
            }
            Config.ensureSkinSelected();
            if (!flag && Config.enabled)
            {
                Config.SaveConfig();
            }

        }

        // Token: 0x06000032 RID: 50 RVA: 0x00002939 File Offset: 0x00000B39
        public static string GetConfigPath()
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ccl.json");

        }

        // Token: 0x06000033 RID: 51 RVA: 0x00002954 File Offset: 0x00000B54
        private static void ReadConfig()
        {
            if (!File.Exists(Config.GetConfigPath()))
            {
                return;
            }
            JSONNode jsonnode;
            try
            {
                jsonnode = JSON.Parse(File.ReadAllText(Config.GetConfigPath()));
            }
            catch (Exception ex)
            {
                Shared.Log("Couldn't load CustomCosmeticLoader config!");
                Debug.LogException(ex);
                return;
            }
            if (jsonnode["enabled"] != null)
            {
                Config.enabled = jsonnode["enabled"].AsBool;
            }
            if (jsonnode["currentSkin"] != null)
            {
                Config.currentSkin = jsonnode["currentSkin"].Value;
            }
            if (jsonnode["skinNameToHijack"] != null)
            {
                Config.skinNameToHijack = jsonnode["skinNameToHijack"].Value;
            }
            if (jsonnode["inMainMenu"] != null)
            {
                Config.inMainMenu = jsonnode["inMainMenu"].AsBool;
            }
            if (jsonnode["inCosmeticMenu"] != null)
            {
                Config.inCosmeticMenu = jsonnode["inCosmeticMenu"].AsBool;
            }
            if (jsonnode["inAllReplays"] != null)
            {
                Config.inAllReplays = jsonnode["inAllReplays"].AsBool;
            }
            if (jsonnode["overrideReplayCosmetics"] != null)
            {
                Config.overrideReplayCosmetics = jsonnode["overrideReplayCosmetics"].AsBool;
            }
            if (jsonnode["otherPlayers"] != null)
            {
                JSONClass asObject = jsonnode["otherPlayers"].AsObject;
                if (asObject != null)
                {
                    foreach (object obj in asObject)
                    {
                        KeyValuePair<string, JSONNode> keyValuePair = (KeyValuePair<string, JSONNode>)obj;
                        string key = keyValuePair.Key;
                        string value = keyValuePair.Value.Value;
                        if (Config.skins.ContainsKey(value))
                        {
                            Shared.Log("Adding other player: " + key + " -> " + value);
                            Config.otherPlayers.Add(key, value);
                        }
                        else
                        {
                            Shared.Log(string.Concat(new string[] { "NOT adding other player ", key, ", skin ", value, " not found" }));
                        }
                    }
                }
            }
            Console.WriteLine($"Skins length: {skins.Count}");
        }

        // Token: 0x06000034 RID: 52 RVA: 0x00002BB4 File Offset: 0x00000DB4
        public static void SaveConfig()
        {
            JSONClass jsonclass = new JSONClass();
            jsonclass.Add("enabled", new JSONData(Config.enabled));
            jsonclass.Add("currentSkin", Config.currentSkin);
            jsonclass.Add("skinNameToHijack", Config.skinNameToHijack);
            jsonclass.Add("inMainMenu", new JSONData(Config.inMainMenu));
            jsonclass.Add("inCosmeticMenu", new JSONData(Config.inCosmeticMenu));
            jsonclass.Add("inAllReplays", new JSONData(Config.inAllReplays));
            jsonclass.Add("overrideReplayCosmetics", new JSONData(Config.overrideReplayCosmetics));
            JSONNode jsonnode = jsonclass;
            JSONNode jsonnode2 = new JSONClass();
            foreach (KeyValuePair<string, string> keyValuePair in Config.otherPlayers)
            {
                jsonnode2.Add(keyValuePair.Key, keyValuePair.Value);
            }
            jsonnode.Add("otherPlayers", jsonnode2);
            File.WriteAllText(Config.GetConfigPath(), jsonnode.ToString());
        }

        // Token: 0x06000035 RID: 53 RVA: 0x00002CE0 File Offset: 0x00000EE0
        public static void ensureSkinSelected()
        {
            if (Config.currentSkin == null || !Config.skins.ContainsKey(Config.currentSkin))
            {
                if (Config.skins.Count == 0)
                {
                    Shared.Log("No custom skins found, disabling mod");
                    Config.enabled = false;
                    return;
                }
                Config.currentSkin = Config.skins.Keys.ToList<string>()[0];
            }
        }

        // Token: 0x04000011 RID: 17
        public const string CONFIG_FILE_NAME = "config.json";

        // Token: 0x04000012 RID: 18
        public const string SKINS_DIR_NAME = "skins";

        // Token: 0x04000013 RID: 19
        public const string PROPERTY_ENABLED = "enabled";

        // Token: 0x04000014 RID: 20
        public const string PROPERTY_CURRENT_SKIN = "currentSkin";

        // Token: 0x04000015 RID: 21
        public const string PROPERTY_SKIN_NAME_TO_HIJACK = "skinNameToHijack";

        // Token: 0x04000016 RID: 22
        public const string PROPERTY_IN_MAIN_MENU = "inMainMenu";

        // Token: 0x04000017 RID: 23
        public const string PROPERTY_IN_COSMETIC_MENU = "inCosmeticMenu";

        // Token: 0x04000018 RID: 24
        public const string PROPERTY_IN_ALL_REPLAYS = "inAllReplays";

        // Token: 0x04000019 RID: 25
        public const string PROPERTY_OVERRIDE_REPLAY_COSMETICS = "overrideReplayCosmetics";

        // Token: 0x0400001A RID: 26
        public const string PROPERTY_OTHER_PLAYERS = "otherPlayers";

        // Token: 0x0400001B RID: 27
        public static bool enabled = true;

        // Token: 0x0400001C RID: 28
        public static string currentSkin;

        // Token: 0x0400001D RID: 29
        public static string skinNameToHijack = "Swirl_M";

        // Token: 0x0400001E RID: 30
        public static bool inMainMenu = true;

        // Token: 0x0400001F RID: 31
        public static bool inCosmeticMenu = false;

        // Token: 0x04000020 RID: 32
        public static bool inAllReplays = false;

        // Token: 0x04000021 RID: 33
        public static bool overrideReplayCosmetics = false;

        // Token: 0x04000022 RID: 34
        public static Dictionary<string, string> otherPlayers = new Dictionary<string, string>();

        // Token: 0x04000023 RID: 35
        public static Dictionary<string, Texture2D> skins = new Dictionary<string, Texture2D>();
    }
}
