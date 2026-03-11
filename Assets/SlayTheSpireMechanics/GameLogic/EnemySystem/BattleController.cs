using Assets.SlayTheSpireMechanics.Enums;
using SlayTheSpireMechanics.VisualLogic.Battle;
using SlayTheSpireMechanics.VisualLogic.CardContainer;
using SlayTheSpireMechanics.VisualLogic.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace SlayTheSpireMechanics.VisualLogic.GameControllers
{
    public class BattleController : MonoBehaviour
    { 
        public Transform monsterStartPosition;

        public Player player; // igrok

        public List<BattleSetting> battleSettingList = new List<BattleSetting>();

        private HashSet<BattleSituationsEnum> _battleSituations = new();
        public HashSet<BattleSituationsEnum> BattleSituations => _battleSituations;
        
        private Dictionary<Enemy, Vector3> _occupiedSlots = new(); // zanyatie sloti
        private List<Vector3> _emptySlots = new(); // svobodniye sloti
        
        private List<Enemy> _currentEnemies = new();



        public event Action<Enemy> OnEnemyAppeared; // handler subscribed
        public event Action<Enemy> OnEnemyDestroyed;

        [SerializeField] private int _currentTurn = 0;
        public int CurrentTurn => _currentTurn;


        [Range(0,1)][SerializeField] private float _playerLowHealth;
        [Range(0, 1)][SerializeField] private float _enemyLowHealth;



        public void Encounter()
        {
            var battleSetting = battleSettingList[Random.Range(0, battleSettingList.Count)];

            for (int i = 0; i < battleSetting.slots.Count; i++)
            {
                if (i < battleSetting.enemyList.Count) 
                { 
                    GameObject go = Instantiate(battleSetting.enemyList[i], monsterStartPosition.position + battleSetting.slots[i], Quaternion.identity);
                    go.transform.SetParent(monsterStartPosition);
                    Enemy en = go.GetComponent<Enemy>();
                    _occupiedSlots[en] = monsterStartPosition.position + battleSetting.slots[i];
                    en.Init(this);
                
                    _currentEnemies.Add(en);
                    OnEnemyAppeared?.Invoke(en);
                }
                else
                {
                    _emptySlots.Add(monsterStartPosition.position + battleSetting.slots[i]);
                }
            }
        }
        public void CheckSituations()
        {
            if (player.Health.Value < player.MaxHealth.Value * _playerLowHealth)
                _battleSituations.Add(BattleSituationsEnum.PlayerLow);
            else
                if (_battleSituations.Contains(BattleSituationsEnum.PlayerLow)) _battleSituations.Remove(BattleSituationsEnum.PlayerLow);

            bool isEnemyLow = false;
            foreach (var enemy in _currentEnemies)
            {
                if (enemy.Health.Value < enemy.MaxHealth.Value * _playerLowHealth)
                {
                    isEnemyLow = true; break;
                }
            }
            if (isEnemyLow) _battleSituations.Add(BattleSituationsEnum.EnemyLow);
            else if (_battleSituations.Contains(BattleSituationsEnum.EnemyLow)) _battleSituations.Remove(BattleSituationsEnum.EnemyLow);
        }

        public void DoEnemyActions()
        {
            foreach (Enemy enemy in _currentEnemies)
            {
                enemy.Act();
            } 
        }
        public void CheckEnemies()
        {
            foreach (var enemy in _currentEnemies)
            {
                if (enemy.Health.Value == 0)
                {
                    DestroyEnemy(enemy);
                }
            }
        }
        
        public void DestroyEnemy(Enemy enemy)
        {
            _currentEnemies.Remove(enemy);
            _emptySlots.Add(_occupiedSlots[enemy]);
            _occupiedSlots.Remove(enemy);
            OnEnemyDestroyed?.Invoke(enemy);
            Destroy(enemy.gameObject);
        }
        
    }
}