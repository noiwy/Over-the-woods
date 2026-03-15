using Assets.SlayTheSpireMechanics.ActionSystemLogic;
using SlayTheSpireMechanics.VisualLogic.GameControllers.GameStates;

namespace SlayTheSpireMechanics.Actions
{
    public class ChangeTurnGA : ICallback
    {
        public GameStateEnum GameState {get; set; }

        public ChangeTurnGA(GameStateEnum gameState)
        {
            GameState = gameState;
        }
    }
}