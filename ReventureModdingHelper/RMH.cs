
using System;
using System.Collections.Generic;
using MelonLoader;
using ReventureModdingHelper.Tools;
using UnityEngine;

namespace ReventureModdingHelper
{
    public static class BuildInfo
    {
        public const string Name = "ReventureModdingHelper";
        public const string Description = "An API for Reventure mod developers"; 
        public const string Author = "LimoDerEchte"; 
        public const string Company = null; 
        public const string Version = "0.1.0"; 
        public const string DownloadLink = "https://www.nexusmods.com/reventure/mods/2"; 
    }
    
    public class RMH : MelonMod
    {
        public static Dictionary<EndingTypes, EndingCreator> EndingRegister = new Dictionary<EndingTypes, EndingCreator>();
        public static Dictionary<ItemTypes, ItemAction> ItemActions = new Dictionary<ItemTypes, ItemAction>();
        public static float FloatCooldown;


        public override void OnInitializeMelon()
        {
            new EndingCreator("test", "stoneDrop").SetData(
                new EndingDataCreator(Endings.GetEndingType("test", "stoneDrop")).SetText(
                        "Stone Drop",
                        "Sometimes you slip... and sometimes you slip with a rock in your hands!",
                        "Dwayne Johnson",
                        "After multiple surgeries and a lot of hospital debt, the normal life began again.")
                    .SetDaysPassed(450).AddSkinChange(HeroSkinTypes.Injured).SetImage(Utils.LoadNewSprite("test/stone.png")).Build()).Register();
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (sceneName == "5.Gameplay")
            {
                Test.TestItem();
            }else if (sceneName == "6.Ending")
            {
                EndingDirector.instance.enabled = true;
            }
        }

        public override void OnUpdate()
        {
            if (FloatCooldown > 0)
            {
                FloatCooldown -= Time.deltaTime;   
                Hero.instance.gameObject.transform.position += new Vector3(0, Time.deltaTime * 20, 0);
            }
        }

        public class ItemAction
        {
            public Action Action;
            public bool BeforeNormalItems;

            public ItemAction(Action action, bool beforeNormalItems)
            {
                Action = action;
                BeforeNormalItems = beforeNormalItems;
            }
        }
    }
}