using System;
using System.Collections.Generic;
using HarmonyLib;
using MIU;
using UnityEngine;

namespace OldMods.CustomCosmeticLoader.Patches
{
    // Token: 0x02000014 RID: 20
    [HarmonyPatch(typeof(MarbleHolder), "SetMarble")]
    internal class MarbleHolderSetMarblePatch
    {
        // Token: 0x06000056 RID: 86 RVA: 0x00003AF4 File Offset: 0x00001CF4
        private static void Postfix(MarbleHolder __instance, Cosmetic marbleObject)
        {
            if (!Config.enabled)
            {
                return;
            }
            if (marbleObject.Id != Config.skinNameToHijack && Config.skinNameToHijack != "*")
            {
                return;
            }
            if (!Config.inCosmeticMenu && __instance == CosmeticPanel.cosmHolder)
            {
                return;
            }
            MarbleController marbleController = MarbleHolderValues.MbcField.GetValue(__instance) as MarbleController;
            if (marbleController == null)
            {
                return;
            }
            Texture2D texture2D = null;
            if (NetworkManager.IsMultiplayer)
            {
                if (marbleController.isMyClientMarble())
                {
                    texture2D = Config.skins[Config.currentSkin];
                }
                else
                {
                    if (!Config.otherPlayers.ContainsKey(marbleController.nickname))
                    {
                        return;
                    }
                    texture2D = Config.skins[Config.otherPlayers[marbleController.nickname]];
                }
            }
            else if (MarbleManager.Replaying && !Config.inAllReplays)
            {
                string replayName = null;
                GamePlayManager.Get(true).GetCurrentReplays(delegate (List<Replay> replays)
                {
                    if (replays.Count > 0)
                    {
                        replayName = replays[0].Player;
                    }
                });
                Shared.Log("MarbleHolderSetMarblePatch Replay name: " + replayName);
                if (replayName == Player.Current.Name)
                {
                    texture2D = Config.skins[Config.currentSkin];
                }
                else if (Config.otherPlayers.ContainsKey(replayName))
                {
                    texture2D = Config.skins[Config.otherPlayers[replayName]];
                }
            }
            else
            {
                texture2D = Config.skins[Config.currentSkin];
            }
            if (texture2D != null)
            {
                Shared.ApplyCustomTexture(__instance.currentMarble, texture2D);
            }
        }
    }
}
