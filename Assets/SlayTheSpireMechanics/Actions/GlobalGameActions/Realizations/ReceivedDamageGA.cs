using SlayTheSpireMechanics.VisualLogic.ObjectInterfaces;

namespace SlayTheSpireMechanics.Actions
{
    public class ReceivedDamageGA : IAction
    {
        ITargetable Target { get; set; }
        
        int Damage { get; set; }
        
        public ReceivedDamageGA(ITargetable target, int damage)
        {
            Target = target;
            Damage = damage;
        }
    }
}