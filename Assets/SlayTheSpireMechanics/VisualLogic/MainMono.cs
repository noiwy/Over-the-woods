
using Assets.SlayTheSpireMechanics.UI;
using SlayTheSpireMechanics;
using SlayTheSpireMechanics.Actions;
using SlayTheSpireMechanics.VisualLogic;
using SlayTheSpireMechanics.VisualLogic.Card;
using SlayTheSpireMechanics.VisualLogic.CardContainer;
using SlayTheSpireMechanics.VisualLogic.GameControllers;
using SlayTheSpireMechanics.VisualLogic.GameControllers.GameStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MainMono : MonoBehaviour
{
    
    [SerializeField] private LootBase lootBase;
    [SerializeField] private Player player;
    [SerializeField] private CardViewContainer cardViewContainer;
    [SerializeField] private CardHoverSystem cardHoverSystem;
    [SerializeField] private GameController gc;
    [SerializeField] private Bezier bezier;
    [SerializeField] private CardAnimator cardAnimator;
    [SerializeField] private GameObject buttons;
    [SerializeField] private BattleController battleController;
    [SerializeField] private DamageHandler damageHandler;
    [SerializeField] private PileCounter[] pileCounters;
    [SerializeField] private ManaWrapper manaWrapper;


    public void Start()
    {
        player.Init();
   
        cardViewContainer.Init(player,cardHoverSystem, cardAnimator);
        cardHoverSystem.Init(cardAnimator, player);
        bezier.Init(cardHoverSystem);
        gc.Init(cardViewContainer, battleController);
        damageHandler.Init(battleController, player);

        foreach (var counter in pileCounters)
        {
            counter.Init(player.CardModelContainer);
        }
        manaWrapper.Init(player.ManaContainer);

        ActionSystem.Instance.AddActionToBottom(new GiveCardToPlayerGA(0, 20, player.Inventory));
        ActionSystem.Instance.AddActionToBottom(new GiveCardToPlayerGA(1, 18, player.Inventory));
        

        StartCoroutine(EnableBtns());
        battleController.Encounter();
    }

    public IEnumerator EnableBtns()
    {
        yield return new WaitForSeconds(0.14f);
        buttons.SetActive(true);
    }
    public void DiscardCards()
    {
        ActionSystem.Instance.AddActionToBottom(new DiscardAllCardsGA());
    }

    public void GiveCardToPlayer()
    {
        
        ActionSystem.Instance.AddActionToBottom(new RefillHandGA());
    }

    public void PlayerTurn()
    {
        player.CardModelContainer.SetDeck(player.Inventory.deck);
        ActionSystem.Instance.AddActionToBottom(new ChangeTurnGA(GameStateEnum.PlayerTurn));
    }

    public void EnemyTurn()
    {
        ActionSystem.Instance.AddActionToBottom(new ChangeTurnGA(GameStateEnum.EnemyTurn));
    }

   
}
