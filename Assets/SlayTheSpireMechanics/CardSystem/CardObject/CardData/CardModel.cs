using System;
using SlayTheSpireMechanics.VisualLogic.Card;
using SlayTheSpireMechanics.VisualLogic.ObjectInterfaces;
using UnityEngine;
namespace SlayTheSpireMechanics.VisualLogic
{
    public class CardModel
    {
        public string Description { get; private set; }
        public int Cost { get; private set; }
        public string Title { get; private set; }
        
        public Sprite Sprite { get; private set; }
        
        public ICardAction Action { get; private set; }
        
        public ActionType ActionType { get; private set; }


        public CardModel(CardSettings cardSettings)
        {
            Description = cardSettings.description;
            Cost = cardSettings.cost;
            Title = cardSettings.title;
            Sprite = cardSettings.sprite;
            ActionType = cardSettings.ActionType;
            Action = CardActionBinding.GetActionFromCardSetting(cardSettings);
        }

        public void TriggerAction()
        {
            ActionSystem.Instance.AddActionToQueue(Action);
        }

    }
}