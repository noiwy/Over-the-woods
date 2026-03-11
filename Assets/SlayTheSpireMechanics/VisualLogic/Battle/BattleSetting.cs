using System.Collections.Generic;
using SlayTheSpireMechanics.VisualLogic.Enemies;
using UnityEngine;

namespace SlayTheSpireMechanics.VisualLogic.Battle
{
    [CreateAssetMenu(fileName = "BattleSetting", menuName = "Scriptable Objects/BattleSetting")]
    public class BattleSetting : ScriptableObject
    {
        public List<GameObject> enemyList;
        public List<int> additionalHealth;
        public List<Vector3> slots;
    }
}