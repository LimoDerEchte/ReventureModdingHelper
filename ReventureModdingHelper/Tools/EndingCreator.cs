using System;
using System.Collections.Generic;
using HarmonyLib;
using ReventureModdingHelper.Patches;
using UnityEngine;

namespace ReventureModdingHelper.Tools
{
    public class EndingCreator
    {
        public EndingTypes Type;
        public EndingData Data;

        public EndingCreator(EndingTypes type)
        {
            Type = type;
        }

        public EndingCreator(string modId, string name)
        {
            Type = Endings.GetEndingType(modId, name);
        }

        public EndingCreator SetData(EndingData data)
        {
            Data = data;
            return this;
        }

        public void Register()
        {
            RMH.EndingRegister.Add(Type, this);
        }

        public EndingBehaviour BuildEndingBehaviour()
        {
            GameObject ebh = new GameObject("EndingBehaviour" + (int) Type);
            EndingBehaviour eb = ebh.AddComponent<EndingBehaviour>();
            eb.endingData = Data;
            eb.RefreshEndingName();
            return eb;
        }
    }

    public class EndingDataCreator
    {
        public EndingData CurrentData;
        
        public EndingDataCreator(EndingTypes type)
        {
            CurrentData = new EndingData()
            {
                endingType = type
            };
            CurrentData.endingType = type;
        }

        public EndingDataCreator SetText(string title, string subtitle, string hint, string intro)
        {
            EndingStrings strings = new EndingStrings()
            {
                title = title,
                narration = subtitle,
                hint = hint,
                nextItroText = intro
            };
            Traverse.Create(CurrentData).Field<EndingStrings>("endingStrings").Value = strings;
            return this;
        }

        public EndingDataCreator SetImage(Sprite image)
        {
            CurrentData.endingPic = image;
            return this;
        }

        public EndingDataCreator AddSkinChange(HeroSkinTypes type)
        {
            CurrentData.skinToChange = type;
            CurrentData.changeSkinMode = ChangeSkinModes.OnLoadScreen;
            return this;
        }

        public EndingDataCreator SetDaysPassed(int days)
        {
            CurrentData.amountOfDaysPassed = days;
            return this;
        }
        
        public EndingData Build()
        {
            return CurrentData;
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