using Assets.SlayTheSpireMechanics.GameLogic.PlayerSystem;
using SlayTheSpireMechanics.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using Unity.VisualScripting;
using UnityEngine;
using static System.Random;

namespace SlayTheSpireMechanics.VisualLogic.CardContainer
{
    public class CardModelContainer
    {
        private Player _player;
        public int Max;


        public IReadOnlyReactiveCollection<CardModel> DiscardPile => _discardPileList;
        public IReadOnlyReactiveCollection<CardModel> HandPile => _handPileList;
        public IReadOnlyReactiveCollection<CardModel> DrawPile => _drawPileList;


        private ReactiveCollection<CardModel> _discardPileList = new ReactiveCollection<CardModel>();

        private ReactiveCollection<CardModel> _handPileList = new ReactiveCollection<CardModel>();

        private ReactiveCollection<CardModel> _drawPileList  = new ReactiveCollection<CardModel>();

        public int attempts = 0;


        public event Action<CardModel> OnCardPlayed;
        public event Action<List<CardModel>> OnRefillFinished;
        public event Action<List<CardModel>> OnDiscardFinished;


        public CardModelContainer(int max, Player player)
        {
            Max = max;
            _player = player;
        }

        public void SetDeck(List<CardModel> startDeck)
        {
            ClearDecks();
            _drawPileList.AddRange(startDeck);
            _drawPileList.Shuffle();
        }

        public void ClearDecks()
        {
            _drawPileList.Clear();
            _handPileList.Clear();
            _discardPileList.Clear();
        }

        private bool TryDeleteOne(CardModel cardModel)
        {
            if (_handPileList.Contains(cardModel))
            {
                _handPileList.Remove(cardModel);
                return true;
            }
            return false;
        }
        private bool TryDrawOne(CardModel cardModel)
        {
            Debug.Log("DrawOne");
            if (_drawPileList.Contains(cardModel))
            {
                _drawPileList.Remove(cardModel);
                _handPileList.Add(cardModel);
                return true;
            }
            return false;
        }
        private bool TryDiscardOne(CardModel cardModel)
        {
            Debug.Log("DiscardOne");
            if (_handPileList.Contains(cardModel))
            {
                _handPileList.Remove(cardModel);
                _discardPileList.Add(cardModel);
                return true;
            }
            return false;
        }
        public bool TryPlayCard(CardModel cardModel)
        {
            if (_player.ManaContainer.Mana.Value >= cardModel.Cost)
            {
                cardModel.TriggerAction();
                _player.ManaContainer.DecreaseMana(cardModel.Cost);
                OnCardPlayed?.Invoke(cardModel);
                return true;
            }
            return false;
        }




        public void DrawMultipleCards(DrawMultipleCardsGA drawMultipleCardsGA)
        {
            Debug.Log("DrawMulti");
            int cardCount = drawMultipleCardsGA.CardsDrawn;
            List<CardModel> cards = new List<CardModel>();
           
            int startCount = _handPileList.Count;

            int topLimit = cardCount + startCount <= Max ? cardCount + startCount : Max;
            while (_handPileList.Count < topLimit)
            {
                attempts++;
                if (attempts > 100) {attempts = 0; return;}
                CardModel topCard = GetLinkOnFirstElement();
                if (topCard == null)
                {
                    RefillDrawPile();
                    continue;
                }
                if (TryDrawOne(topCard))
                {
                    cards.Add(topCard);
                }
            }
            OnRefillFinished?.Invoke(cards);
            attempts = 0;
        }
        public void DiscardMultiple(Func<CardModel, bool> predicate)
        {
            Debug.Log("DiscardMulti");
            List<CardModel> returnList = new List<CardModel>();
            CardModel[] cards = _handPileList.ToArray();
            foreach (var cardModel in cards)
            {
                if (predicate(cardModel))
                {
                    if (TryDiscardOne(cardModel)) returnList.Add(cardModel);
                }
            }
            OnDiscardFinished?.Invoke(returnList);
        }



        public void RefillPlayerHand()
        {
            DrawMultipleCardsGA drawMultipleCardsGa = new(Max);
            DrawMultipleCards(drawMultipleCardsGa);
        }
        public void DiscardPlayerHand()
        {
            DiscardMultiple((a) => true);
        }


        

        private CardModel GetLinkOnFirstElement()
        {
            if (_drawPileList.Count == 0)
            {
                return null;
            }
            return _drawPileList[0];
        }
        public void RefillDrawPile()
        {
            if (_discardPileList.Count >= 1)
            {
                _drawPileList.AddRange(_discardPileList);
                _discardPileList.Clear();
                _drawPileList.Shuffle();
            }
        }
    }
    
    public static class ListExtension
    {
        private static System.Random rng = new System.Random();
        public static void Shuffle<T>(this ReactiveCollection<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T temp = list[k];
                list[k] = list[n];
                list[n] = temp;
            }
        }
    }
}