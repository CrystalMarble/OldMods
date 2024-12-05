using System;
using HarmonyLib;
using OldMods.CustomCosmeticLoader;

namespace OldMods.CustomCosmeticLoader.Patches
{
    // Token: 0x02000010 RID: 16
    [HarmonyPatch(typeof(MainMenuPanel), "SetupImages", new Type[] { typeof(bool) })]
    internal class MainMenuPanelSetupImagesPatch
    {
        // Token: 0x0600004E RID: 78 RVA: 0x000038E4 File Offset: 0x00001AE4
        private static void Postfix(MainMenuPanel __instance)
        {
            if (!Config.enabled)
            {
                return;
            }
            if (!Config.inMainMenu)
            {
                return;
            }
            if (__instance.cosmeticDisplay)
            {
                if (Config.skinNameToHijack != "*")
                {
                    __instance.cosmeticDisplay.Clear();
                    GameObjectAllocator.Deinitialize(__instance.cosmeticDisplay.gameObject);
                    __instance.cosmeticDisplay.Setup(Shared.SkinToHijack, 0, true, true);
                }
                Shared.ApplyCustomTexture(__instance.cosmeticDisplay.CachedCosmeticGameObject, null);
            }
        }
    }
}
