using System.Collections.Generic;
using SlayTheSpireMechanics.VisualLogic.ObjectInterfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace SlayTheSpireMechanics.VisualLogic.Enemies.EnemyTypes.ActionSettings
{
    public abstract class EnemyActionSetting : ScriptableObject
    {
        public ActionType actionType;
        public EnemyChoseVariants  choseVariant;
    }
}