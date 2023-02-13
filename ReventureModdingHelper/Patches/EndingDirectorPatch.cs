using HarmonyLib;

namespace ReventureModdingHelper.Patches
{
    [HarmonyPatch(typeof(EndingDirector), "Awake")]
    public class EndingDirectorPatch
    {
        public static void Prefix(ref EndingDirector __instance, ref EndingBehaviour ___currentEndingBehaviour, ref EndingCinematicConfiguration ___cinematicConfiguration)
        {
            if (RMH.EndingRegister.ContainsKey(___cinematicConfiguration.ending))
            {
                ___currentEndingBehaviour = RMH.EndingRegister[___cinematicConfiguration.ending].BuildEndingBehaviour();
                ___cinematicConfiguration.ending = EndingTypes.None;
            }
        }
    }
}