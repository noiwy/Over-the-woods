using UnityEngine;

namespace SlayTheSpireMechanics.VisualLogic
{
    [CreateAssetMenu(fileName = "CardSettings", menuName = "Scriptable Objects/CardSettings")]
    public class CardSettings : ScriptableObject
    {
        public string description;
        public int cost;
        public string title;
        public Sprite sprite;
        
        public ActionType ActionType;
        public CardActionSetting ActionSetting;
    }
}