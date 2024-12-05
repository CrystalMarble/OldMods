using System;
using UnityEngine;

namespace OldMods.CustomCosmeticLoader
{
    // Token: 0x0200000E RID: 14
    public static class Shared
    {
        // Token: 0x17000011 RID: 17
        // (get) Token: 0x06000048 RID: 72 RVA: 0x00003694 File Offset: 0x00001894
        public static Cosmetic SkinToHijack
        {
            get
            {
                if (Shared.skinToHijack != null && Shared.skinToHijack.Id != Config.skinNameToHijack)
                {
                    Shared.skinToHijack = null;
                }
                if (Shared.skinToHijack == null)
                {
                    Shared.DetermineSkinToHijack();
                    if (Shared.skinToHijack == null)
                    {
                        Shared.Log("Couldn't find skin " + Config.skinNameToHijack + " to hijack");
                        Config.skinNameToHijack = "Swirl_M";
                        Shared.DetermineSkinToHijack();
                    }
                }
                return Shared.skinToHijack;
            }
        }

        // Token: 0x06000049 RID: 73 RVA: 0x00003704 File Offset: 0x00001904
        private static void DetermineSkinToHijack()
        {
            foreach (Cosmetic cosmetic in CosmeticManager.Skins)
            {
                if (cosmetic.Id == Config.skinNameToHijack)
                {
                    Shared.skinToHijack = cosmetic;
                    return;
                }
            }
        }

        // Token: 0x0600004A RID: 74 RVA: 0x00003744 File Offset: 0x00001944
        public static void ApplyCustomTexture(GameObject marbleObject, Texture2D skin = null)
        {
            if (marbleObject == null)
            {
                Shared.Log("ApplyCustomTexture - marbleObject is null");
                return;
            }
            if (skin == null)
            {
                foreach (var s in Config.skins)
                {
                    Log("Discovered skin: " + s);
                }
                skin = Config.skins[Config.currentSkin];
            }
            MeshRenderer[] componentsInChildren = marbleObject.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < componentsInChildren.Length; i++)
            {
                try
                {

                MIU.Console.print(componentsInChildren[i].name);
                Material[] materials = componentsInChildren[i].materials;
                
                if (materials != null)
                {
                    foreach (Material material in materials)
                    {
                        if (!(material == null) && material.mainTexture != null)
                        {
                            material.mainTexture = skin;
                        }
                    }
                }
                }
                catch (Exception ex)
                {
                    Shared.Log(ex.ToString());
                }
            }
        }

        // Token: 0x0600004B RID: 75 RVA: 0x000037DB File Offset: 0x000019DB
        public static void Log(string message)
        {
            message = "[CUSTOM COSMETIC LOADER] " + message;
            Debug.Log(message);
            Console.WriteLine(message);
            MIU.Console.print(message);
        }

        // Token: 0x04000025 RID: 37
        private static Cosmetic skinToHijack;
    }
}
