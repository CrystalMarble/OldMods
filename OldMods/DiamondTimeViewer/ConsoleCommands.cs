using System;
using MIU;
using static MPScoreboard;

namespace OldMods.DiamondTimeViewer
{
    // Token: 0x02000004 RID: 4
    internal class ConsoleCommands
    {
        // Token: 0x06000007 RID: 7 RVA: 0x000022AC File Offset: 0x000004AC
        private static void printUsage()
        {
            global::MIU.Console.Instance.Write("Usage: dtv [value]", null);
            global::MIU.Console.Instance.Write("Possible values:", null);
            global::MIU.Console.Instance.Write("  never       Never show diamond times", null);
            global::MIU.Console.Instance.Write("  diamond     Show once you've achieved diamond time", null);
            global::MIU.Console.Instance.Write("  gold        Show once you've achieved gold time", null);
            global::MIU.Console.Instance.Write("  always      Always show diamond time", null);
            global::MIU.Console.Instance.Write("", null);
            global::MIU.Console.Instance.Write("  hideSilver  Hide silver when showing diamond", null);
            global::MIU.Console.Instance.Write("  showSilver  Show silver always", null);
            global::MIU.Console.Instance.Write("", null);
            global::MIU.Console.Instance.Write("  enable      Enable the mod", null);
            global::MIU.Console.Instance.Write("  disable     Disable the mod (should be the same as \"never\" unless I messed up)", null);
            global::MIU.Console.Instance.Write("", null);
            global::MIU.Console.Instance.Write("  save        Saves the current config", null);
            global::MIU.Console.Instance.Write("  load        Loads the config file", null);
        }

        // Token: 0x06000008 RID: 8 RVA: 0x00002424 File Offset: 0x00000624
        [ConsoleCommand(null, null, null, false, false, description = "Configures the Diamond Time Viewer", paramsDescription = "[value]")]
        public static string dtv(params string[] args)
        {
            if (args.Length != 1)
            {
                ConsoleCommands.printUsage();
                return "";
            }
            string text = args[0];
            if (text != null)
            {
                switch (text.Length)
                {
                    case 4:
                        {
                            char c = text[0];
                            if (c != 'g')
                            {
                                if (c != 'l')
                                {
                                    if (c == 's')
                                    {
                                        if (text == "save")
                                        {
                                            Config.SaveConfig();
                                            return "Config file saved";
                                        }
                                    }
                                }
                                else if (text == "load")
                                {
                                    Config.ReadConfig();
                                    return "Config file loaded";
                                }
                            }
                            else if (text == "gold")
                            {
                                Config.Mode = DisplayMode.Gold;
                                return "mode set to: gold";
                            }
                            break;
                        }
                    case 5:
                        if (text == "never")
                        {
                            Config.Mode = DisplayMode.Never;
                            return "mode set to: never";
                        }
                        break;
                    case 6:
                        {
                            char c = text[0];
                            if (c != 'a')
                            {
                                if (c == 'e')
                                {
                                    if (text == "enable")
                                    {
                                        Config.Enabled = true;
                                        return "Mod enabled";
                                    }
                                }
                            }
                            else if (text == "always")
                            {
                                Config.Mode = DisplayMode.Always;
                                return "mode set to: always";
                            }
                            break;
                        }
                    case 7:
                        {
                            char c = text[2];
                            if (c != 'a')
                            {
                                if (c == 's')
                                {
                                    if (text == "disable")
                                    {
                                        Config.Enabled = false;
                                        return "Mod disabled";
                                    }
                                }
                            }
                            else if (text == "diamond")
                            {
                                Config.Mode = DisplayMode.Diamond;
                                return "mode set to: diamond";
                            }
                            break;
                        }
                    case 10:
                        {
                            char c = text[0];
                            if (c != 'h')
                            {
                                if (c == 's')
                                {
                                    if (text == "showSilver")
                                    {
                                        Config.HideSilver = false;
                                        return "Showing silver time";
                                    }
                                }
                            }
                            else if (text == "hideSilver")
                            {
                                Config.HideSilver = true;
                                return "Hiding silver time";
                            }
                            break;
                        }
                }
            }
            ConsoleCommands.printUsage();
            return "";
        }
    }
}
