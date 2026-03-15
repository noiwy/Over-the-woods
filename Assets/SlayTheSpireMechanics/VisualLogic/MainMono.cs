
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
    [SerializeField] private BattleController battleController;
    [SerializeField] private DamageHandler damageHandler;



    public void Start()
    {
        cardViewContainer.Init(player,cardHoverSystem, cardAnimator);
        cardHoverSystem.Init(cardAnimator, player);
        bezier.Init(cardHoverSystem);
        gc.Init(cardViewContainer, battleController);
        damageHandler.Init(battleController, player);


        lootBase.GiveCardToPlayer(player.Inventory, 0, 20);
        lootBase.GiveCardToPlayer(player.Inventory, 1, 18);


    }


    public void DiscardCards()
    {
        player.CardModelContainer.DiscardPlayerHand();
    }

    public void GiveCardToPlayer()
    {
        player.CardModelContainer.RefillPlayerHand();
    }

    public void PlayerTurn()
    {
        gc.ChangeGameState(GameStateEnum.PlayerTurn);
    }

    public void EnemyTurn()
    {
        gc.ChangeGameState(GameStateEnum.EnemyTurn);
    }

   
}
