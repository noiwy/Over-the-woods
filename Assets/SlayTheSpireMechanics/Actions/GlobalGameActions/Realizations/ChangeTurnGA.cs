using SlayTheSpireMechanics.VisualLogic.GameControllers.GameStates;

namespace SlayTheSpireMechanics.Actions
{
    public class ChangeTurnGA : IAction
    {
        public GameStateEnum GameState {get; set; }

        public ChangeTurnGA(GameStateEnum gameState)
        {
            GameState = gameState;
        }
    }
}