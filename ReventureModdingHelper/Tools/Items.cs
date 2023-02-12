using System;
using HarmonyLib;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ReventureModdingHelper.Tools
{
    public class Items
    {
        public static void RegisterItemAction(ItemTypes item, Action action, bool beforeNormalItems = false)
        {
            if(!RMH.ItemActions.ContainsKey(item))
                RMH.ItemActions.Add(item, new RMH.ItemAction(action, beforeNormalItems));
        }
        
        public static ItemTypes GetItemType(string modId, string name)
        {
            int baseId = modId.GetHashCode();
            int fullId = baseId + name.GetHashCode();
            return (ItemTypes) fullId;
        }

        public static TreasureChest SpawnItemChest(CharacterItem item, Vector3 position, bool faceLeft)
        {
            GameObject titem = SpawnItem(item, new Vector3(100000, 100000, 100000)).gameObject;
            GameObject obj = Object.Instantiate(GameObject.Find(faceLeft ? "TreasureChest_Trinket" : "TreasureChest_Shovel"));
            obj.transform.position = position;
            obj.name = "TreasureChest_" + item.gameObject.name;
            TreasureChest chest = obj.GetComponent<TreasureChest>();
            chest.item = ItemTypes.None;
            chest.content = titem;
            return chest;
        }

        public static TreasureItem SpawnItem(CharacterItem item, Vector3 position)
        {
            GameObject obj = Object.Instantiate(GameObject.Find("Items").transform.Find("Item Prototype").gameObject);
            obj.SetActive(true);
            obj.name = "Item " + item.gameObject.name;
            obj.transform.position = position;
            TreasureItem titem = obj.GetComponent<TreasureItem>();
            titem.skill = ItemTypes.None;
            titem.onItemPicked.RemoveAllListeners();
            titem.BeforePick.RemoveAllListeners();
            Traverse.Create(titem).Field<CharacterItem>("itemGrantedPrefab").Value = item;
            obj.GetComponent<SpriteRenderer>().sprite = Traverse.Create(item).Field<SpriteRenderer>("idleSprite").Value.sprite;
            return titem;
        }

        public static CharacterItem CreateItem(string name, ItemTypes type, Sprite texture, Vector2 spriteOffset)
        {
            GameObject tmp = new GameObject(name);
            SpriteRenderer sprite = tmp.AddComponent<SpriteRenderer>();
            sprite.sprite = texture;
            sprite.sortingOrder = -1;
            CharacterItem item = tmp.AddComponent<CharacterItem>();
            Traverse.Create(item).Field<Vector2>("displayPosition").Value = spriteOffset;
            Traverse.Create(item).Field<ItemTypes>("itemType").Value = type;
            tmp.transform.localPosition = spriteOffset;
            return item;
        }
    }
}