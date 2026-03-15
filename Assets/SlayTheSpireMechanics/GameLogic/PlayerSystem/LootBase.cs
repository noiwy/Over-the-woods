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

        }
        
        public void GiveCardToPlayer(Inventory inventory, int id, int count)
        {
            Debug.Log("Enter3");
            for (int i = 0; i < count; i++)
            {
                inventory.deck.Add(new CardModel(cardSettings[id]));
            }
        }
    }
}