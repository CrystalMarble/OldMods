using System;
using System.Collections.Generic;
using HarmonyLib;

namespace OldMods.DiamondTimeViewer.Patches
{
    // Token: 0x02000007 RID: 7
    [HarmonyPatch(typeof(MedalsDisplay), "Setup")]
    internal class MedalsDisplaySetupPatch
    {
        // Token: 0x0600000E RID: 14 RVA: 0x000026FC File Offset: 0x000008FC
        private static bool Prefix(MedalsDisplay __instance, float silver, float gold, List<string> eggUnlock)
        {
            if (!Config.Enabled)
            {
                return true;
            }
            float diamondTime = LevelSelect.instance.level.DiamondTime;
            bool flag = false;
            if (diamondTime > 0f)
            {
                switch (Config.Mode)
                {
                    case DisplayMode.Never:
                        flag = false;
                        break;
                    case DisplayMode.Diamond:
                        flag = LevelSelect.instance.bestScore > 0f && LevelSelect.instance.bestScore <= diamondTime;
                        break;
                    case DisplayMode.Gold:
                        flag = LevelSelect.instance.bestScore > 0f && LevelSelect.instance.bestScore <= gold;
                        break;
                    case DisplayMode.Always:
                        flag = true;
                        break;
                }
            }
            string text = "<sprite=1> " + SegmentedTime.SPTimeText(silver, false);
            string text2 = "<space=0.5em> ";
            string text3 = "";
            if (flag)
            {
                if (Config.HideSilver)
                {
                    text = "";
                }
                else
                {
                    text2 = "<space=0.25em> ";
                }
                text3 = text2 + "<sprite=3> " + SegmentedTime.SPTimeText(diamondTime, true);
            }
            if (text != "")
            {
                text += text2;
            }
            bool flag2 = eggUnlock != null && eggUnlock.Count > 0;
            bool flag3 = flag2 && UnlockManager.Get().IsUnlocked(eggUnlock[0]) && !CosmeticValues.WasPurchased(eggUnlock[0]);
            string text4 = "";
            if (flag2)
            {
                text4 = text2 + (flag3 ? "<sprite=9>" : "<sprite=8>");
            }
            __instance.MedalTimes.text = string.Concat(new string[]
            {
                text,
                "<sprite=2> ",
                SegmentedTime.SPTimeText(gold, false),
                text3,
                text4
            });
            return false;
        }
    }
}
