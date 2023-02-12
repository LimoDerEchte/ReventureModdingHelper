using HarmonyLib;
using UnityEngine;

namespace ReventureModdingHelper.Patches
{
    [HarmonyPatch(typeof(EndingDirector), "Awake")]
    public class EndingDirectorPatch
    {
        public static void Prefix(ref EndingDirector __instance, ref EndingBehaviour ___currentEndingBehaviour)
        {
            Transform child = __instance.endingsCollection.GetChild(10);
            child.SetParent(null);
            ___currentEndingBehaviour = child.GetComponent<EndingBehaviour>();
        }
    }
}