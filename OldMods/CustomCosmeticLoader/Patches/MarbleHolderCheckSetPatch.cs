using System;
using HarmonyLib;

namespace OldMods.CustomCosmeticLoader.Patches
{
    // Token: 0x02000015 RID: 21
    [HarmonyPatch(typeof(MarbleHolder), "CheckSet")]
    internal class MarbleHolderCheckSetPatch
    {
        // Token: 0x06000058 RID: 88 RVA: 0x00003C88 File Offset: 0x00001E88
        private static void Postfix(MarbleHolder __instance, Cosmetic.Set cs)
        {
            if (!Config.enabled)
            {
                return;
            }
            if (!NetworkManager.IsMultiplayer)
            {
                return;
            }
            MarbleController marbleController = MarbleHolderValues.MbcField.GetValue(__instance) as MarbleController;
            if (marbleController == null)
            {
                return;
            }
            if (!marbleController.isMyClientMarble() && !Config.otherPlayers.ContainsKey(marbleController.nickname))
            {
                return;
            }
            if (cs.skin == "SoccerBall_V4" || cs.skin == "Zombie")
            {
                return;
            }
            __instance.SetMarble(Shared.SkinToHijack);
            __instance.CosmeticSet.skin = cs.skin;
        }
    }
}
