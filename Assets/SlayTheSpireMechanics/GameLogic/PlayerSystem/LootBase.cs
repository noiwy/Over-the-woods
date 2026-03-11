using SlayTheSpireMechanics.Actions;
using SlayTheSpireMechanics.VisualLogic.CardContainer;
using UnityEngine;

namespace SlayTheSpireMechanics.VisualLogic.GameControllers
{
    public class LootBase : MonoBehaviour
    {
        public CardSettings[] cardSettings;


        private void OnEnable()
        {
            ActionSystem.AttachPerformer<GiveCardToPlayerGA>(GiveCardToPlayer);
        }
        
        public void GiveCardToPlayer(GiveCardToPlayerGA ga)
        {
            Debug.Log("Enter3");
            for (int i = 0; i < ga.Count; i++)
            {
                ga.Inventory.deck.Add(new CardModel(cardSettings[ga.ID]));
            }
        }
        
        

        public void GiveCardToPlayer(Inventory inventory, int id, int count)
        {
            GiveCardToPlayerGA ga = new GiveCardToPlayerGA(id, count, inventory);
            ActionSystem.Instance.AddActionToBottom(ga);
        }
    }
}