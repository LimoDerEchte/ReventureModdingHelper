using System;
using System.Collections.Generic;
using ReventureModdingHelper.Patches;
using UnityEngine;

namespace ReventureModdingHelper.Tools
{
    public class EndingCreator
    {
        public EndingTypes Type;

        public EndingCreator(EndingTypes type)
        {
            Type = type;
        }

        public EndingCreator(string modId, string name)
        {
            Type = Endings.GetEndingType(modId, name);
        }

        public void Register()
        {
            
        }

        public EndingBehaviour BuildEndingBehaviour()
        {
            GameObject ebh = new GameObject("EndingBehaviour" + (int) Type);
            EndingBehaviour eb = ebh.AddComponent<EndingBehaviour>();
            return eb;
        }
    }
    
    public class PreEndingCreator
    {
        private readonly List<CinematicStepBase> _steps;

        public PreEndingCreator()
        {
            _steps = new List<CinematicStepBase>();
        }
        
        public PreEndingCreator AddFrame(Action action)
        {
            _steps.Add(new CinematicStepAction(action));
            return this;
        }
        
        public PreEndingCreator AddRepeatingFrame(Func<bool> action)
        {
            _steps.Add(new CinematicStepAction(action));
            return this;
        }

        public PreEndingCreator AddFrame(CinematicStepBase frame)
        {
            _steps.Add(frame);
            return this;
        }

        public PreEndingCreator AddDelay(float time)
        {
            AddRepeatingFrame(() =>
            {
                time -= Time.deltaTime;
                return time <= 0;
            });
            return this;
        }

        public CinematicManager BuildCinematicManager()
        {
            GameObject host = GameObject.Find("CMHolder");
            if (host == null)
                host = new GameObject("CMHolder");
            CinematicManager m = host.AddComponent<CinematicManager>();
            foreach(CinematicStepBase step in _steps)
                m.cinematicStepList.Add(step);
            return m;
        }
    }
}