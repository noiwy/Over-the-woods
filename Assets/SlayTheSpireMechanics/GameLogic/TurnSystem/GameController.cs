using System;
using SlayTheSpireMechanics.Actions;
using SlayTheSpireMechanics.VisualLogic.CardContainer;
using SlayTheSpireMechanics.VisualLogic.GameControllers.GameStates;
using UnityEngine;

namespace SlayTheSpireMechanics.VisualLogic.GameControllers
{
    public class GameController : MonoBehaviour
    {
        private GameState _gameState;
        public CardViewContainer CardViewContainer { get; private set; }
        public BattleController BattleController { get; private set; }

        public void Init(CardViewContainer container, BattleController controller)
        {
            CardViewContainer = container;
            BattleController = controller;
            ChangeGameState(new ChangeTurnGA(GameStateEnum.PlayerTurn));
        }
        private void OnEnable()
        {
            ActionSystem.AttachPerformer<ChangeTurnGA>(ChangeGameState);
        }
        private void Update()
        {
            _gameState.OnUpdate();
        }


        public void ChangeGameState(ChangeTurnGA changeTurnGA)
        {
            _gameState?.OnEnd();
            GameStateEnum newGameState = changeTurnGA.GameState;
            switch (newGameState)
            {
                case GameStateEnum.PlayerTurn:
                    _gameState = new GamePlayerTurnState(this);
                    break;
                case GameStateEnum.EnemyTurn:
                    _gameState = new GameEnemyTurnState(this);
                    break;
                
            }
            _gameState?.OnStart();
        }
    }
}