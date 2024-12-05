using System;
using System.Collections.Generic;
using HarmonyLib;
using MIU;
using OldMods.CustomCosmeticLoader;
using UnityEngine;

namespace OldMods.CustomCosmeticLoader.Patches
{
    // Token: 0x0200000F RID: 15
    [HarmonyPatch(typeof(GhostRaceMarbleController), "ApplyCosmetics")]
    internal class GhostRaceMarbleControllerApplyCosmeticsPatch
    {
        // Token: 0x0600004C RID: 76 RVA: 0x000037F0 File Offset: 0x000019F0
        private static void Postfix(GhostRaceMarbleController __instance)
        {
            if (!Config.enabled)
            {
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
            Shared.Log("GhostRaceMarbleControllerApplyCosmeticsPatch Replay name: " + replayName);
            if (Config.otherPlayers.ContainsKey(replayName) || replayName == Player.Current.Name)
            {
                MarbleHolder componentInChildren = __instance.GetComponentInChildren<MarbleHolder>();
                if (Config.skinNameToHijack != "*")
                {
                    componentInChildren.SetMarble(Shared.SkinToHijack);
                }
                Texture2D texture2D;
                if (replayName == Player.Current.Name)
                {
                    texture2D = Config.skins[Config.currentSkin];
                }
                else
                {
                    texture2D = Config.skins[Config.otherPlayers[replayName]];
                }
                Shared.ApplyCustomTexture(componentInChildren.currentMarble, texture2D);
            }
        }
    }
}
