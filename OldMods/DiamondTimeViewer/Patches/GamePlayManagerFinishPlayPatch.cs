using System;
using HarmonyLib;
using MIU;

namespace OldMods.DiamondTimeViewer.Patches
{
    // Token: 0x02000006 RID: 6
    [HarmonyPatch(typeof(GamePlayManager), "FinishPlay")]
    internal class GamePlayManagerFinishPlayPatch
    {
        // Token: 0x0600000C RID: 12 RVA: 0x0000268C File Offset: 0x0000088C
        private static void Postfix(GamePlayManager __instance, MarbleController marble)
        {
            if (!Config.Enabled)
            {
                return;
            }
            if ((__instance.PlayType == PlayType.Normal || __instance.PlayType == PlayType.Ghost) && !MarbleManager.usedRewind && LevelSelect.instance != null && marble.ElapsedTime < LevelSelect.instance.bestScore)
            {
                LevelSelect.instance.bestScore = marble.ElapsedTime;
                LevelSelect.instance.UpdateMedals();
            }
        }
    }
}
