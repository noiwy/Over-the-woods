using SlayTheSpireMechanics.VisualLogic.CardContainer;

namespace SlayTheSpireMechanics.Actions
{
    public class GiveCardToPlayerGA : IAction
    {
        public int ID { get; private set; }
        
        public int Count { get;private set; }
        public Inventory Inventory { get; private set; }

        public GiveCardToPlayerGA(int id, int count, Inventory inventory)
        {
            ID = id;
            this.Count = count;
            this.Inventory = inventory;
        }
    }
}