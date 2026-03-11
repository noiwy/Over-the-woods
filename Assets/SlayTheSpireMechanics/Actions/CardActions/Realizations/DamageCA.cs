using SlayTheSpireMechanics.VisualLogic.Card;
using SlayTheSpireMechanics.VisualLogic.ObjectInterfaces;

namespace SlayTheSpireMechanics.VisualLogic.CardActionsCode
{
    public class DamageCA : ICardAction
    {
        public ITargetable Target { get; set; }
        
        public bool IsGlobal { get; set; }
        public int Damage { get; set; }
        public int Repeat {get; set;}

        public DamageCA(bool isGlobal, int damage, int repeat = 1)
        {
            Target = null;
            IsGlobal = isGlobal;
            Damage = damage;
            Repeat = repeat;
        }

    }
}