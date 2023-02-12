using UnityEngine;

namespace ReventureModdingHelper.Tools
{
    public class Endings
    {
        public static EndingTypes GetEndingType(string modId, string name)
        {
            int baseId = modId.GetHashCode();
            int fullId = baseId + name.GetHashCode();
            return (EndingTypes) fullId;
        }

        public static EndLauncher CreateEndLauncher(EndingTypes type, bool blockInput = true, float overrideDelay = 0.0F, CinematicManager cinematic = null)
        {
            GameObject obj = cinematic != null ? cinematic.gameObject : new GameObject(type.ToString());
            EndLauncher launcher = obj.AddComponent<EndLauncher>();
            launcher.ending = type;
            launcher.blockInput = blockInput;
            launcher.overrideDelay = overrideDelay;
            return launcher;
        }
    }
}