using HarmonyLib;
using MelonLoader;

namespace ReventureModdingHelper.Patches
{
    [HarmonyPatch(typeof(ProgressionProvider), nameof(ProgressionProvider.GetEndingData))]
    public class ProgressionProviderPatch
    {
        public static void Postfix(ref EndingData __result, ref EndingTypes ending)
        {
            if ((int) ending > 102)
            {
                __result = Atto.Core.Get<IProgressionService>().GetEndingData(EndingTypes.ClimbMountain);
            }
        }
    }
}