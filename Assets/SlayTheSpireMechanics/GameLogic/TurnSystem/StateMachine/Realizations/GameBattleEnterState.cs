using Assets.SlayTheSpireMechanics.UI;
using SlayTheSpireMechanics;
using SlayTheSpireMechanics.Actions;
using SlayTheSpireMechanics.VisualLogic.CardContainer;
using SlayTheSpireMechanics.VisualLogic.GameControllers;
using SlayTheSpireMechanics.VisualLogic.GameControllers.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.SlayTheSpireMechanics.GameLogic.TurnSystem.StateMachine.Realizations
{
    public class GameBattleEnterState : GameState
    {
        public GameBattleEnterState(GameController gameController) : base(gameController) { }

        public override void OnStart()
        {

            Player player = _gameController.BattleController.Player;
            player.Init();
            player.CardModelContainer.SetDeck(player.Inventory.deck);

            CardViewContainer cardViewContainer = _gameController.CardViewContainer;
            cardViewContainer.Init(player);

            BattleController battleController = _gameController.BattleController;
            battleController.Encounter();

            WindowManager windowManager = _gameController.WindowManager;
            windowManager.Draw(GameStateEnum.BattleEnter);

        }

        public override void OnUpdate()
        {
            _gameController.BattleController.CheckEnemies();
            
        }

        public override void OnEnd()
        {

        }

    }
}