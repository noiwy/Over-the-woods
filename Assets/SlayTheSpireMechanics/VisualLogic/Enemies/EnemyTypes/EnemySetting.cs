using System.Collections.Generic;
using SlayTheSpireMechanics.VisualLogic.Enemies.EnemyTypes.ActionSettings;
using UnityEngine;

namespace SlayTheSpireMechanics.VisualLogic.Enemies.EnemyTypes
{
    
    [CreateAssetMenu(fileName = "EnemySetting", menuName = "Scriptable Objects/EnemySetting")]
    public class EnemySetting : ScriptableObject
    {
        public Sprite Sprite;
        public List<EnemyActionSetting> ActionSettings;
        public int Health;
    }
}