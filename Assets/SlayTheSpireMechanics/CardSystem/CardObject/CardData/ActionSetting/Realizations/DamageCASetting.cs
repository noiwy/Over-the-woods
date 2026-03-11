using UnityEngine;

namespace SlayTheSpireMechanics.VisualLogic
{
    [CreateAssetMenu(fileName = "DamageActionSetting", menuName = "Scriptable Objects/CardActionSetting/DamageActionSetting", order = 1)]
    public class DamageCASetting : CardActionSetting
    {
        public int Damage;
        public int Repeat;
    }
}