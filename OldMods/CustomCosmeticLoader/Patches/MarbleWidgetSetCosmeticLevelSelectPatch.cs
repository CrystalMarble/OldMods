using System;
using HarmonyLib;
using OldMods.CustomCosmeticLoader;

namespace OldMods.CustomCosmeticLoader.Patches
{
    // Token: 0x02000016 RID: 22
    [HarmonyPatch(typeof(MarbleWidget), "SetCosmetic", new Type[] { })]
    internal class MarbleWidgetSetCosmeticLevelSelectPatch
    {
        // Token: 0x0600005A RID: 90 RVA: 0x00003D24 File Offset: 0x00001F24
        private static bool Prefix(MarbleWidget __instance)
        {
            if (!Config.enabled)
            {
                return true;
            }
            if (!Config.inMainMenu)
            {
                return true;
            }
            Cosmetic cosmetic = ((Config.skinNameToHijack == "*") ? ((CosmeticManager.MySkin == null) ? CosmeticManager.Skins[0] : CosmeticManager.MySkin) : Shared.SkinToHijack);
            Cosmetic cosmetic2 = ((CosmeticManager.MyHat == null) ? CosmeticManager.Hats[0] : CosmeticManager.MyHat);
            __instance.SetCosmetic(cosmetic, cosmetic2, false);
            Shared.ApplyCustomTexture(__instance.cosmeticDisplay.gameObject, null);
            return false;
        }
    }
}
