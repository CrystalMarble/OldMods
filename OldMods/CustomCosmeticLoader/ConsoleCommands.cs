 using System.Collections.Generic;
using System.Reflection;
using MIU;
using UnityEngine;

namespace OldMods.CustomCosmeticLoader
{
    // Token: 0x0200000B RID: 11
    internal class ConsoleCommands
    {
        // Token: 0x06000038 RID: 56 RVA: 0x00002D84 File Offset: 0x00000F84
        [ConsoleCommand(null, null, null, false, false, description = "Shows help for Custom Cosmetic Loader commands")]
        public static string cclHelp()
        {
            Console.Instance.Write("Available commands:", null);
            foreach (MethodInfo methodInfo in typeof(ConsoleCommands).GetMethods())
            {
                if (methodInfo.IsDefined(typeof(ConsoleCommandAttribute), false))
                {
                    ConsoleCommandAttribute customAttribute = methodInfo.GetCustomAttribute<ConsoleCommandAttribute>();
                    string text = customAttribute.name;
                    if (text == null)
                    {
                        text = methodInfo.Name;
                    }
                    if (customAttribute.paramsDescription != null && customAttribute.paramsDescription.Length > 0)
                    {
                        Console.Instance.Write(string.Concat(new string[] { "  ", text, " ", customAttribute.paramsDescription, ": ", customAttribute.description }), null);
                    }
                    else
                    {
                        Console.Instance.Write("  " + text + ": " + customAttribute.description, null);
                    }
                }
            }
            return "";
        }

        // Token: 0x06000039 RID: 57 RVA: 0x00002E9D File Offset: 0x0000109D
        [ConsoleCommand(null, null, null, false, false, description = "Save the custom config loader configuration", hidden = true)]
        public static string cclSaveConfig()
        {
            Config.SaveConfig();
            return "Custom cosmetic loader config saved";
        }

        // Token: 0x0600003A RID: 58 RVA: 0x00002EAC File Offset: 0x000010AC
        [ConsoleCommand(null, null, null, false, false, description = "Enables/disables the custom cosmetic loader", paramsDescription = "[true/false]", hidden = true)]
        public static string cclEnabled(params string[] args)
        {
            if (args.Length == 0)
            {
                return "enabled: " + (Config.enabled ? "true" : "false");
            }
            if (args.Length != 1 || (args[0] != "true" && args[0] != "false"))
            {
                return "Requires a true or false argument";
            }
            if (args[0] == "false")
            {
                Config.enabled = false;
                return "Disabled custom cosmetic loader";
            }
            Config.enabled = true;
            Config.ensureSkinSelected();
            if (!Config.enabled)
            {
                return "No skins found, could not enable!";
            }
            return "Enabled custom cosmetic loader";
        }

        // Token: 0x0600003B RID: 59 RVA: 0x00002F40 File Offset: 0x00001140
        [ConsoleCommand(null, null, null, false, false, description = "Sets the active custom skin", paramsDescription = "[skinName]", hidden = true)]
        public static string cclCurrentSkin(params string[] args)
        {
            if (args.Length == 0)
            {
                return "currentSkin: " + Config.currentSkin;
            }
            if (args.Length != 1)
            {
                return "Requires 1 argument, the name of the custom skin";
            }
            foreach (KeyValuePair<string, Texture2D> keyValuePair in Config.skins)
            {
                if (keyValuePair.Key == args[0])
                {
                    Config.currentSkin = keyValuePair.Key;
                    return "Using custom skin " + keyValuePair.Key;
                }
            }
            return "Couldn't find skin " + args[0];
        }

        // Token: 0x0600003C RID: 60 RVA: 0x00002FEC File Offset: 0x000011EC
        [ConsoleCommand(null, null, null, false, false, description = "Change which cosmetic to hijack", paramsDescription = "[skinName]", hidden = true)]
        public static string cclSkinNameToHijack(params string[] args)
        {
            if (args.Length == 0)
            {
                return "skinNameToHijack: " + Config.skinNameToHijack;
            }
            if (args.Length != 1)
            {
                return "Requires 1 argument, the name of the skin cosmetic to hijack";
            }
            if (args[0] == "*")
            {
                Config.skinNameToHijack = "*";
                return "Hijack skin set to all skins";
            }
            Cosmetic[] skins = CosmeticManager.Skins;
            for (int i = 0; i < skins.Length; i++)
            {
                if (skins[i].Id == args[0])
                {
                    Config.skinNameToHijack = args[0];
                    return "Hijack skin set to " + args[0];
                }
            }
            return "Couldn't find cosmetic " + args[0];
        }

        // Token: 0x0600003D RID: 61 RVA: 0x00003084 File Offset: 0x00001284
        [ConsoleCommand(null, null, null, false, false, description = "Enable/disable custom skins on the My Marble widgets", paramsDescription = "[true/false]", hidden = true)]
        public static string cclInMainMenu(params string[] args)
        {
            if (args.Length == 0)
            {
                return "inMainMenu: " + (Config.inMainMenu ? "true" : "false");
            }
            if (args.Length != 1 || (args[0] != "true" && args[0] != "false"))
            {
                return "Requires a true or false argument";
            }
            Config.inMainMenu = args[0] == "true";
            return "Custom skins in main menu " + (Config.inMainMenu ? "enabled" : "disabled");
        }

        // Token: 0x0600003E RID: 62 RVA: 0x0000310C File Offset: 0x0000130C
        [ConsoleCommand(null, null, null, false, false, description = "Enable/disable custom skins in the cosmetic menu (for previewing hijack skins)", paramsDescription = "[true/false]", hidden = true)]
        public static string cclInCosmeticMenu(params string[] args)
        {
            if (args.Length == 0)
            {
                return "inCosmeticMenu: " + (Config.inCosmeticMenu ? "true" : "false");
            }
            if (args.Length != 1 || (args[0] != "true" && args[0] != "false"))
            {
                return "Requires a true or false argument";
            }
            Config.inCosmeticMenu = args[0] == "true";
            return "Custom skins in cosmetic menu " + (Config.inCosmeticMenu ? "enabled" : "disabled");
        }

        // Token: 0x0600003F RID: 63 RVA: 0x00003194 File Offset: 0x00001394
        [ConsoleCommand(null, null, null, false, false, description = "Enable/disable showing your custom skin in all replays, regardless of player", paramsDescription = "[true/false]", hidden = true)]
        public static string cclInAllReplays(params string[] args)
        {
            if (args.Length == 0)
            {
                return "inAllReplays: " + (Config.inAllReplays ? "true" : "false");
            }
            if (args.Length != 1 || (args[0] != "true" && args[0] != "false"))
            {
                return "Requires a true or false argument";
            }
            Config.inAllReplays = args[0] == "true";
            return "Custom skins in all replays " + (Config.inAllReplays ? "enabled" : "disabled");
        }

        // Token: 0x06000040 RID: 64 RVA: 0x0000321C File Offset: 0x0000141C
        [ConsoleCommand(null, null, null, false, false, description = "Enable/disable overriding a replay's cosmetics with your cosmetics", paramsDescription = "[true/false]", hidden = true)]
        public static string cclOverrideReplayCosmetics(params string[] args)
        {
            if (args.Length == 0)
            {
                return "overrideReplayCosmetics: " + (Config.overrideReplayCosmetics ? "true" : "false");
            }
            if (args.Length != 1 || (args[0] != "true" && args[0] != "false"))
            {
                return "Requires a true or false argument";
            }
            Config.overrideReplayCosmetics = args[0] == "true";
            return "Overriding cosmetics in all replays " + (Config.overrideReplayCosmetics ? "enabled" : "disabled");
        }

        // Token: 0x06000041 RID: 65 RVA: 0x000032A4 File Offset: 0x000014A4
        [ConsoleCommand(null, null, null, false, false, description = "List, add, or remove other players", paramsDescription = "[add/remove, playerName, skinName (if add)]", hidden = true)]
        public static string cclOtherPlayers(params string[] args)
        {
            if (args.Length == 0)
            {
                Console.Instance.Write("otherPlayers: " + Config.otherPlayers.Count.ToString(), null);
                foreach (KeyValuePair<string, string> keyValuePair in Config.otherPlayers)
                {
                    Console.Instance.Write("  " + keyValuePair.Key + ": " + keyValuePair.Value, null);
                }
                return "";
            }
            string text = args[0];
            if (!(text == "add"))
            {
                if (!(text == "remove"))
                {
                    return "First argument must be either \"add\" or \"remove\"";
                }
                if (args.Length != 2)
                {
                    return "Must have 1 argument after \"remove\"";
                }
                string text2 = args[1];
                if (Config.otherPlayers.ContainsKey(text2))
                {
                    Config.otherPlayers.Remove(text2);
                    return "Removed " + text2;
                }
                return "Player " + text2 + " not found";
            }
            else
            {
                if (args.Length != 3)
                {
                    return "Must have 2 arguments after \"add\"";
                }
                string text2 = args[1];
                string text3 = args[2];
                if (!Config.skins.ContainsKey(text3))
                {
                    return "Skin " + text3 + " not found";
                }
                if (Config.otherPlayers.ContainsKey(text2))
                {
                    Config.otherPlayers.Remove(text2);
                    Config.otherPlayers.Add(text2, text3);
                    return "Updated " + text2 + ": " + text3;
                }
                Config.otherPlayers.Add(text2, text3);
                return "Added " + text2 + ": " + text3;
            }
        }
    }
}
