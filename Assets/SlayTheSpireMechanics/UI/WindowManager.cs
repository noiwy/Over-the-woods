using SlayTheSpireMechanics.VisualLogic.GameControllers.GameStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.SlayTheSpireMechanics.UI
{
    public class WindowManager : MonoBehaviour
    {
        [SerializeField] private List<Window> windows;
        public void Draw(GameStateEnum gameState)
        {
            foreach (var window in windows)
            {
                window.gameObject.SetActive(false);
            }
            Window active = windows.Find(a => a.GameState == gameState);
            active?.gameObject?.SetActive(true);
            active?.InitElements();
        }
        
        
    }
}