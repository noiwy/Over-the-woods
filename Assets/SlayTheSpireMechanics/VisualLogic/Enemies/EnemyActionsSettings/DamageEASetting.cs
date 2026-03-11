using UnityEngine;

namespace SlayTheSpireMechanics.VisualLogic.Enemies.EnemyTypes.ActionSettings
{
    [CreateAssetMenu(fileName = "DamageEASetting", menuName = "Scriptable Objects/EASettings/DamageEASetting")]
    public class DamageEASetting : EnemyActionSetting
    {
        public int damage;
        public int repeat;
    }
}