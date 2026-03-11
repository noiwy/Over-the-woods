using System;
using System.Collections;
using System.Collections.Generic;


namespace SlayTheSpireMechanics
{
    public abstract class GameAction : IAction
    {
        public List<IAction>  PreReactions {get; private set;} = new();
        public List<IAction>  PerformReactions {get; private set;} = new();
        public List<IAction>  PostReactions {get; private set;} = new();

        public void AttachPerformer(Func<IAction, IEnumerator> func)
        {
            return;
        }

        public void Execute()
        {
            return;
        }
    }
}

