using HarmonyLib;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


namespace OldMods
{

    public class CrystalMarble
    {
        public static bool Patched = false;

        public static void OnLoad()
        {
            SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>(CrystalMarble.Patch);
        }
        public static void Patch(Scene scene, LoadSceneMode lsm)
        {
            if (!CrystalMarble.Patched)
            {
                new Harmony("dev.crystalmarble.oldmods").PatchAll();
                CrystalMarble.Patched = false;
                SceneManager.sceneLoaded -= new UnityAction<Scene, LoadSceneMode>(CrystalMarble.Patch);
                CustomCosmeticLoader.Config.Init();
                DiamondTimeViewer.Config.Init();
            }
        }
    }
}
