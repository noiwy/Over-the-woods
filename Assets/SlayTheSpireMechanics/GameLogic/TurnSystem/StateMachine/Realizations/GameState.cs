namespace SlayTheSpireMechanics.VisualLogic.GameControllers.GameStates
{
    public abstract class GameState
    {
        protected GameController _gameController;



        protected GameState(GameController gameController)
        {
            _gameController = gameController;
        }

        public abstract void OnStart();
        public abstract void OnUpdate();
        public abstract void OnEnd();

    }
}