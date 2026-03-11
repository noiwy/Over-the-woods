using SlayTheSpireMechanics.Actions;

namespace SlayTheSpireMechanics.VisualLogic.GameControllers.GameStates
{
    public class GameEnemyTurnState : GameState
    {
        public GameEnemyTurnState(GameController gameController) : base(gameController){}

        public override void OnStart()
        {
            _gameController.BattleController.CheckSituations();
            _gameController.BattleController.DoEnemyActions();
        }

        public override void OnUpdate()
        {

        }

        public override void OnEnd()
        {

        }
    }
}