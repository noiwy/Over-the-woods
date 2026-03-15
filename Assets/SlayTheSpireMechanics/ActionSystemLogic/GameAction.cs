using SlayTheSpireMechanics;
using System;
using System.Collections;


namespace Assets.SlayTheSpireMechanics.ActionSystemLogic
{
    public abstract class GameAction : IAction
    {
        public static Func<IEnumerator, IAction> performer;
    }
}
