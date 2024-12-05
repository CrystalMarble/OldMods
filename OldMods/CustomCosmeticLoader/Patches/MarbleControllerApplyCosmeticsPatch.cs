using System;
using System.Collections.Generic;
using HarmonyLib;
using MIU;

namespace OldMods.CustomCosmeticLoader.Patches
{
    // Token: 0x02000012 RID: 18
    [HarmonyPatch(typeof(MarbleController), "ApplyCosmetics")]
    internal class MarbleControllerApplyCosmeticsPatch
    {
        // Token: 0x06000052 RID: 82 RVA: 0x000039C8 File Offset: 0x00001BC8
        private static void Prefix(MarbleController __instance, ref ReplayCosmetics cosmetics)
        {
            if (Config.overrideReplayCosmetics)
            {
                cosmetics.Skin = CosmeticManager.MySkin.Id;
                cosmetics.Trail = CosmeticManager.MyTrail.Id;
                cosmetics.Respawn = CosmeticManager.MyRespawn.Id;
                cosmetics.Hat = CosmeticManager.MyHat.Id;
                cosmetics.Blast = CosmeticManager.MyBlast.Id;
            }
            if (!Config.enabled || Config.skinNameToHijack == "*")
            {
                return;
            }
            if (Config.inAllReplays)
            {
                cosmetics.Skin = Config.skinNameToHijack;
                return;
            }
            string replayName = null;
            GamePlayManager.Get(true).GetCurrentReplays(delegate (List<Replay> replays)
            {
                if (replays.Count > 0)
                {
                    replayName = replays[0].Player;
                }
            });
            Shared.Log("MarbleControllerApplyCosmeticsPatch Replay name: " + replayName);
            if (Config.otherPlayers.ContainsKey(replayName) || replayName == Player.Current.Name)
            {
                cosmetics.Skin = Config.skinNameToHijack;
            }
        }
    }
}
