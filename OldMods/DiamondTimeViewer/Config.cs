using System;
using System.IO;
using System.Reflection;
using I2.Loc.SimpleJSON;
using UnityEngine;

namespace OldMods.DiamondTimeViewer
{
    public enum DisplayMode
    {
        // Token: 0x04000002 RID: 2
        Never,
        // Token: 0x04000003 RID: 3
        Diamond,
        // Token: 0x04000004 RID: 4
        Gold,
        // Token: 0x04000005 RID: 5
        Always
    }

    // Token: 0x02000003 RID: 3
    public class Config
    {
        // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
        public static void Init()
        {
            if (File.Exists(Config.GetConfigPath()))
            {
                Config.ReadConfig();
                return;
            }
            Config.SaveConfig();
        }

        // Token: 0x06000002 RID: 2 RVA: 0x0000206C File Offset: 0x0000026C
        public static void ReadConfig()
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
                Debug.LogError("Exception reading DiamondViewer config: " + ex.Message);
                return;
            }
            if (jsonnode["enabled"] != null)
            {
                Config.Enabled = jsonnode["enabled"].AsBool;
            }
            if (jsonnode["mode"] != null)
            {
                string text = jsonnode["mode"].Value.ToLower();
                if (!(text == "never"))
                {
                    if (!(text == "diamond"))
                    {
                        if (!(text == "gold"))
                        {
                            if (text == "always")
                            {
                                Config.Mode = DisplayMode.Always;
                            }
                        }
                        else
                        {
                            Config.Mode = DisplayMode.Gold;
                        }
                    }
                    else
                    {
                        Config.Mode = DisplayMode.Diamond;
                    }
                }
                else
                {
                    Config.Mode = DisplayMode.Never;
                }
            }
            if (jsonnode["hideSilver"] != null)
            {
                Config.HideSilver = jsonnode["hideSilver"].AsBool;
            }
        }

        // Token: 0x06000003 RID: 3 RVA: 0x00002194 File Offset: 0x00000394
        public static void SaveConfig()
        {
            JSONNode jsonnode = new JSONClass();
            jsonnode.Add("enabled", Config.Enabled ? "true" : "false");
            switch (Config.Mode)
            {
                case DisplayMode.Never:
                    jsonnode.Add("mode", "never");
                    break;
                case DisplayMode.Diamond:
                    jsonnode.Add("mode", "diamond");
                    break;
                case DisplayMode.Gold:
                    jsonnode.Add("mode", "gold");
                    break;
                case DisplayMode.Always:
                    jsonnode.Add("mode", "always");
                    break;
            }
            jsonnode.Add("hideSilver", Config.HideSilver ? "true" : "false");
            File.WriteAllText(Config.GetConfigPath(), jsonnode.ToString());
        }

        // Token: 0x06000004 RID: 4 RVA: 0x00002275 File Offset: 0x00000475
        public static string GetConfigPath()
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "dtv.json");
        }

        // Token: 0x04000006 RID: 6
        private const string CONFIG_FILE_NAME = "dtv.json";

        // Token: 0x04000007 RID: 7
        public static bool Enabled = true;

        // Token: 0x04000008 RID: 8
        public static DisplayMode Mode = DisplayMode.Diamond;

        // Token: 0x04000009 RID: 9
        public static bool HideSilver = false;
    }
}
