using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace OldMods.ConsoleUnlocker
{
    [HarmonyPatch(typeof(PlatformSetup), "GetDevIDs")]
    internal class DevIdPatches
    {
        public static void Postfix()
        {
            FieldInfo DevsField = typeof(PlatformSetup).GetField("Devs", BindingFlags.NonPublic | BindingFlags.Static);
            if (DevsField == null) return;
            List<DevUser> Devs = DevsField.GetValue(null) as List<DevUser>;
            DevUser me = new DevUser($"{{\"UserID\": \"{Player.Current.Network.GetNetworkId()}\", \"Flags\": 31}}");
            MonoBehaviour.print($"Me (DevUser) | IsDev: {me.HasFeature(1)} | UnlockAll:  {me.HasFeature(2)} | MultiplayerFlag: {me.HasFeature(4)} | CommandLineAccess: {me.HasFeature(8)} | FPSDisplay: {me.HasFeature(16)}");
            Devs.Add(me);
            DevsField.SetValue(null, Devs);
            GlobalContext.Invoke<DevModeActivated>(Array.Empty<object>());
            MIU.Console.Instance.gameObject.SetActive(true);
        }
    }
}
