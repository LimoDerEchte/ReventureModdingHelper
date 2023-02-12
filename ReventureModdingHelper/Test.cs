
using System;
using MelonLoader;
using ReventureModdingHelper.Tools;
using UnityEngine;

namespace ReventureModdingHelper
{
    public class Test
    {
        public static void TestItem()
        {
            //Buttons.CreateButton(new Vector3(136, 7.4962F, 0), 0, () => RMH.FloatCooldown = 5F);
            ItemTypes type = Items.GetItemType("test", "stone");
            CharacterItem item = Items.CreateItem("Stone", type, Utils.LoadNewSprite("test/stone.png"), new Vector2(-0.4F, 0.1F));
            TreasureItem titem = Items.SpawnItem(item, new Vector3(145, 8F, 0));
            Items.RegisterItemAction(type, () =>
            {
                RMH.FloatCooldown = 5F;
                Hero.instance.RemoveTreasure(type);
            });

            //Portals.CreateLinkedPortals(new Vector3(136, 7.4962F, 0), new Vector3(37, 0.4962F, 0));

            EndingTypes etype = Endings.GetEndingType("test", "stoneDrop");
            float endingTime = 0;
            Vector3 start = Vector3.zero;
            CinematicManager cm = new PreEndingCreator()
                .AddFrame(() => start = Hero.instance.transform.position)
                .AddRepeatingFrame(() =>
                {
                    endingTime += Time.deltaTime * 180F;
                    if (endingTime > 180)
                        endingTime = 180;
                    Hero.instance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, endingTime));
                    Hero.instance.transform.position = start + new Vector3(0, Mathf.Sin((float)Math.PI / 2F * endingTime / 90F), 0);
                    return endingTime >= 180;
                })
                .BuildCinematicManager();
            titem.onItemPicked.AddListener(() => Endings.CreateEndLauncher(etype, cinematic:cm).LaunchEnding());
        }
    }
}