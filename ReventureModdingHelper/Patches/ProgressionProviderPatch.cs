using HarmonyLib;

namespace ReventureModdingHelper.Patches
{
    [HarmonyPatch(typeof(ProgressionProvider), nameof(ProgressionProvider.GetEndingData))]
    public class ProgressionProviderPatch
    {
        public static void Postfix(ref EndingData __result, ref EndingTypes ending)
        {
            if ((int) ending > 102)
            {
                if(RMH.EndingRegister.ContainsKey(ending))
                    __result = RMH.EndingRegister[ending].Data;
            }
        }
    }
}