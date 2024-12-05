using System;
using HarmonyLib;
using OldMods.CustomCosmeticLoader;

namespace OldMods.CustomCosmeticLoader.Patches

{
    // Token: 0x02000011 RID: 17
    [HarmonyPatch(typeof(MarbleController), "ApplyMyCosmetics")]
    internal class MarbleControllerApplyMyCosmeticsPatch
    {
        // Token: 0x06000050 RID: 80 RVA: 0x00003968 File Offset: 0x00001B68
        [HarmonyPostfix]
        private static void Postfix(MarbleController __instance)
        {
            if (!Config.enabled)
            {
                return;
            }
            if (Config.skinNameToHijack == "*")
            {
                return;
            }
            if (NetworkManager.IsMultiplayer)
            {
                return;
            }
            MarbleHolder mholder = __instance.MHolder;
            string skin = mholder.CosmeticSet.skin;
            mholder.SetMarble(Shared.SkinToHijack);
            mholder.CosmeticSet.skin = skin;
        }
    }
}
