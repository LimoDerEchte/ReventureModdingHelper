using System;
using UnityEngine;

namespace ReventureModdingHelper.Patches
{
    public class ActionButton : MonoBehaviour
    {
        public Action Action;

        public void Activate_()
        {
            Action.Invoke();
        }
    }
}