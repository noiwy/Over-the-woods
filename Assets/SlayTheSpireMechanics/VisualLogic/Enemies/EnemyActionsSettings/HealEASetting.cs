using UnityEngine;

namespace SlayTheSpireMechanics.VisualLogic.Enemies.EnemyTypes.ActionSettings
{
    [CreateAssetMenu(fileName = "HealEASetting", menuName = "Scriptable Objects/EASettings/HealEASetting")]
    public class HealEASetting : EnemyActionSetting
    {
        public int damage;
        public int repeat;
    }
}