using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ReventureModdingHelper.Tools
{
    public class Portals
    {
        public static Tuple<Portal, Portal> CreateLinkedPortals(Vector3 posA, Vector3 posB, bool redA = false, bool redB = true)
        {
            Portal a = CreatePortal(posA, redA);
            Portal b = CreatePortal(posB, redB);
            LinkPortals(a, b);
            return new Tuple<Portal, Portal>(a, b);
        }

        public static void LinkPortals(Portal a, Portal b)
        {
            a.destination = b.transform;
            b.destination = a.transform;
        }
        
        public static Portal CreatePortal(Vector3 pos, bool red = false)
        {
            Transform parent = GameObject.Find("PrincessPortal").transform;
            GameObject obj = Object.Instantiate(parent.Find(red ? "PrincessPortal_Fortress" : "PrincessPortal_Castle").gameObject);
            obj.transform.position = pos;
            Portal p = obj.GetComponent<Portal>();
            p.centerOnRoom = "";
            return p;
        }
    }
}