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

            _gameController.MakeTransition();
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