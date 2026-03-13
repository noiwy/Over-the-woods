using SlayTheSpireMechanics.Actions;
using SlayTheSpireMechanics.VisualLogic.CardContainer;
using System.Diagnostics;

namespace SlayTheSpireMechanics.VisualLogic.GameControllers.GameStates
{
    public class GamePlayerTurnState : GameState
    {
        public GamePlayerTurnState(GameController gameController) : base(gameController){}

        public override void OnStart()
        {
            _gameController.BattleController.Player.CardModelContainer.RefillPlayerHand();
        }

        public override void OnUpdate()
        {
            _gameController.BattleController.CheckEnemies();
        }

        public override void OnEnd()
        {
            _gameController.BattleController.Player.CardModelContainer.DiscardPlayerHand();
        }
    }
}