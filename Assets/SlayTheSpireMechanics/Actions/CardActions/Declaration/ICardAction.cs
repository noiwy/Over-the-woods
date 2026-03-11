using SlayTheSpireMechanics.VisualLogic.ObjectInterfaces;

namespace SlayTheSpireMechanics.VisualLogic.Card
{
    public interface ICardAction : IAction
    {
        public ITargetable Target { get; set; }
        public bool IsGlobal { get; set; }
    }
}