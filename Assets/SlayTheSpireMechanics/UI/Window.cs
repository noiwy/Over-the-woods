using SlayTheSpireMechanics.VisualLogic.GameControllers.GameStates;
using System.Collections;
using UnityEngine;

namespace Assets.SlayTheSpireMechanics.UI
{
    public abstract class Window : MonoBehaviour
    {
        [SerializeField] private GameStateEnum gameState;
        public GameStateEnum GameState => gameState;

        public abstract void InitElements();

    }
}