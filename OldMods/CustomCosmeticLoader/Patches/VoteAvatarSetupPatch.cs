using System;
using HarmonyLib;
using UnityEngine;

namespace OldMods.CustomCosmeticLoader.Patches
{
    // Token: 0x02000017 RID: 23
    [HarmonyPatch(typeof(VoteAvatar), "Setup")]
    internal class VoteAvatarSetupPatch
    {
        // Token: 0x0600005C RID: 92 RVA: 0x00003DAC File Offset: 0x00001FAC
        private static void Postfix(VoteAvatar __instance, MarbleController marble)
        {
            if (!Config.enabled)
            {
                return;
            }
            if (__instance.widget.cosmeticDisplay.cosmetic.Id == "Zombie")
            {
                return;
            }
            Texture2D texture2D = null;
            if (marble.isMyClientMarble())
            {
                texture2D = Config.skins[Config.currentSkin];
            }
            else if (Config.otherPlayers.ContainsKey(marble.nickname))
            {
                texture2D = Config.skins[Config.otherPlayers[marble.nickname]];
            }
            if (texture2D != null)
            {
                Shared.ApplyCustomTexture(__instance.widget.cosmeticDisplay.gameObject, texture2D);
            }
        }
    }
}
