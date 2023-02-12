using System;

namespace ReventureModdingHelper.Patches
{
    public class CinematicStepAction : CinematicStepBase
    {
        private bool _done = false;
        public Func<bool> Func;
        public Action Action;

        public CinematicStepAction(Action action)
        {
            Action = action;
        }
        
        public CinematicStepAction(Func<bool> action)
        {
            Func = action;
        }

        public override void RunStep(CinematicManager manager)
        {
            if(Action == null)
                return;
            Action.Invoke();
            _done = true;
        }

        public override void UpdateStep(CinematicManager manager)
        {
            if(Func == null)
                return;
            _done = Func.Invoke();
        }

        public override bool IsDone()
        {
            return _done;
        }
    }
}