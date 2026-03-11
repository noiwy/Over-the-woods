using System.Collections.Generic;
using SlayTheSpireMechanics.VisualLogic.Enemies.EnemyActions;
using SlayTheSpireMechanics.VisualLogic.Enemies.EnemyTypes;
using SlayTheSpireMechanics.VisualLogic.Enemies.EnemyTypes.ActionSettings;

namespace SlayTheSpireMechanics.VisualLogic.Enemies
{
    public static class EnemyActionBinding
    {
        public static Dictionary<EnemyChoseVariants, IEnemyAction> GetActionListFromEnemySetting(EnemySetting enemySetting)
        {

            Dictionary<EnemyChoseVariants, IEnemyAction> enemyActionsList = new();
            foreach (var actionSetting in enemySetting.ActionSettings)
            {
                switch (actionSetting.actionType)
                {
                    case ActionType.Damage:
                        DamageEASetting damageEAsetting = (DamageEASetting)actionSetting;
                        enemyActionsList[actionSetting.choseVariant] = new DamageEA(damageEAsetting.damage,  damageEAsetting.repeat);
                        break;
                    case ActionType.Heal:
                        HealEASetting healingSetting = (HealEASetting)actionSetting;
                        enemyActionsList[actionSetting.choseVariant] = new HealEA(healingSetting.damage, healingSetting.repeat);
                        break;
                }
            }
            return enemyActionsList;
        }
    }
}