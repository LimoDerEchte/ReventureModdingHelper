using HarmonyLib;

namespace ReventureModdingHelper.Patches
{
    [HarmonyPatch(typeof(Hero), nameof(Hero.MainAction))]
    public class HeroItemActionPatch
    {
        public static bool Prefix(ref Hero __instance, ref FTUXComponent ___FTUXUseItem)
        {
            foreach (var action in RMH.ItemActions)
            {
                if (action.Value.BeforeNormalItems && __instance.HasItem(action.Key))
                {
                    action.Value.Action.Invoke();
                    ___FTUXUseItem.TriggerEvent();
                    return false;
                }
            }
            return true;
        }

        public static void Postfix(ref Hero __instance, ref FTUXComponent ___FTUXUseItem)
        {
            if (!Traverse.Create(___FTUXUseItem).Field<bool>("triggered").Value)
            {
                foreach (var action in RMH.ItemActions)
                {
                    if (!action.Value.BeforeNormalItems && __instance.HasItem(action.Key))
                    {
                        action.Value.Action.Invoke();
                        ___FTUXUseItem.TriggerEvent();
                        return;
                    }
                }
            }
        }
    }
}