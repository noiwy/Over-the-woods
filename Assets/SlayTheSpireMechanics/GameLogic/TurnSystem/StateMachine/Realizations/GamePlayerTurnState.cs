using SlayTheSpireMechanics.Actions;

namespace SlayTheSpireMechanics.VisualLogic.GameControllers.GameStates
{
    public class GamePlayerTurnState : GameState
    {
        public GamePlayerTurnState(GameController gameController) : base(gameController){}

        public override void OnStart()
        {
            
            RefillHandGA refillHandGa = new RefillHandGA();
            ActionSystem.Instance.AddActionToBottom(refillHandGa);
        }

        public override void OnUpdate()
        {
            _gameController.BattleController.CheckEnemies();
        }

        public override void OnEnd()
        {
            
            ActionSystem.Instance.AddActionToBottom(new DiscardAllCardsGA());
        }
    }
}