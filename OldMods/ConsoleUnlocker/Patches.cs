using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace OldMods.ConsoleUnlocker
{
    [HarmonyPatch(typeof(PlatformSetup), "GetDevIDs")]
    internal class PlatformSetupPatches
    {
        private static void Postfix()
        {
            GlobalContext.Invoke<DevModeActivated>(Array.Empty<object>());
            MIU.Console.Instance.gameObject.SetActive(true);
        }
    }
}
