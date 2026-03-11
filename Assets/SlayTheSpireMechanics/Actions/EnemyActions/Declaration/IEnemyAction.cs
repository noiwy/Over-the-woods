using SlayTheSpireMechanics.VisualLogic.ObjectInterfaces;
using System.Collections.Generic;

namespace SlayTheSpireMechanics.VisualLogic.Enemies.EnemyActions
{
    public class IEnemyAction : IAction
    {
        public ITargetable Target { get; set; }
    }
}