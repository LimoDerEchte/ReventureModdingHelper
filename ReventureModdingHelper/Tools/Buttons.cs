using System;
using ReventureModdingHelper.Patches;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ReventureModdingHelper.Tools
{
    public class Buttons
    {
        
        /*
         0 => Down; Y -0.4
         90 => Right; X +0.2
         180 => Up;
         270 => Left; X -1.5
        */
        public static Switch CreateButton(Vector3 position, float rotationDegrees, Action onPress, bool reusable = false)
        {
            GameObject obj = Object.Instantiate(GameObject.Find("Switch"));
            obj.transform.position = position;
            obj.transform.rotation = Quaternion.Euler(0, 0, rotationDegrees);
            Switch sw = obj.GetComponent<Switch>();
            sw.extraTargets = Array.Empty<GameObject>();
            sw.target = obj;
            sw.isReusable = reusable;
            obj.AddComponent<ActionButton>().Action = onPress;
            return sw;
        }

        public static Switch CreateEndingButton(Vector3 position, float rotationDegrees, EndLauncher launcher)
        {
            return CreateButton(position, rotationDegrees, launcher.LaunchEnding);
        }
        
        public static Tuple<Switch, EndLauncher> CreateEndingButton(Vector3 position, float rotationDegrees, EndingTypes type, bool blockInput = true, float overrideDelay = 0.0F, CinematicManager cinematic = null)
        {
            EndLauncher launcher = Endings.CreateEndLauncher(type, blockInput, overrideDelay, cinematic);
            Switch @switch = CreateButton(position, rotationDegrees, launcher.LaunchEnding);
            return new Tuple<Switch, EndLauncher>(@switch, launcher);
        }
    }
}