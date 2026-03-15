using System;
using System.Collections.Generic;
using Assets.SlayTheSpireMechanics.Enums;
using SlayTheSpireMechanics.Actions;
using SlayTheSpireMechanics.VisualLogic.Enemies.EnemyActions;
using SlayTheSpireMechanics.VisualLogic.Enemies.EnemyTypes;
using SlayTheSpireMechanics.VisualLogic.GameControllers;
using SlayTheSpireMechanics.VisualLogic.ObjectInterfaces;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


namespace SlayTheSpireMechanics.VisualLogic.Enemies
{
    public abstract class Enemy : MonoBehaviour, ITargetable
    {
        [Header("RuntimeInfo")]
        [SerializeField] private EnemyChoseVariants chosenVariant = 0;

        [SerializeField] private ReactiveProperty<int> _health;
        public IReadOnlyReactiveProperty<int> Health => _health;

        [SerializeField] private ReactiveProperty<int> _maxHealth = new ReactiveProperty<int>();
        public IReadOnlyReactiveProperty<int> MaxHealth => _maxHealth;

        [Header("Dependencies")]
        [SerializeField] private GameObject healthBarPrefab;
        [SerializeField] private Animator animator;
        [SerializeField] private BattleController _battleController;
        

        public Dictionary<EnemyChoseVariants, IEnemyAction> EnemyActions = new();

        [Header("Settings")]
        [SerializeField]private EnemySetting enemySetting;
        public SerializedDictionary<EnemyChoseVariants, int> ActionsPercentage = new();
        public SerializedDictionary<EnemyChoseVariants, int> MaxActionRepeat = new();
        public SerializedDictionary<EnemyChoseVariants, int> ActionStartTurn = new();
        public SerializedDictionary<EnemyChoseVariants, BattleSituationsEnum> ActionConditionEnter = new();
        public SerializedDictionary<EnemyChoseVariants, string> AnimationTriggers = new();


        private List<EnemyChoseVariants> _percentageList = new();
        
        private List<EnemyChoseVariants> previousActions = new();



        public void Init(BattleController bc)
        {
            _battleController = bc;
            _health.Value = enemySetting.Health;
            _maxHealth.Value = enemySetting.Health;
            EnemyActions = EnemyActionBinding.GetActionListFromEnemySetting(enemySetting);
            SetChances();

            HealthBarUI healthBar = Instantiate(healthBarPrefab, transform).GetComponentInChildren<HealthBarUI>();
            if (healthBar != null) healthBar.Init(this);

            
        }
        public void DoPlannedAction()
        {
            if (chosenVariant != 0)
            {
                ActionSystem.Instance.AddActionToQueue(EnemyActions[chosenVariant]);
                chosenVariant = 0;
            }
        }
        public void PlanAction()
        {
            chosenVariant = MakeChoice();
        }

        public void Act()
        {
            PlanAction();
            if (animator != null)
            {
                animator.SetTrigger(AnimationTriggers[chosenVariant]);
            }
            else
            {
                DoPlannedAction();
            }
        }
        
        public void SetChances()
        {
            foreach (var action in ActionsPercentage)
            {
                for (int i = 0; i < action.Value; i++)
                {
                    _percentageList.Add(action.Key);
                    
                }
            }
        }

        public EnemyChoseVariants RollChoice()
        {
            return _percentageList[Random.Range(0, _percentageList.Count)];
        }

        public EnemyChoseVariants MakeChoice()
        {
            int attempts = 0;
            bool isAllGood;
            EnemyChoseVariants potential;
            do
            {
                attempts++;
                if (attempts > 1000)
                {
                    throw new Exception($"StackOverflow in {gameObject.name}");
                }
                isAllGood = true;
                potential = RollChoice();
                
                if (ActionConditionEnter.ContainsKey(potential))
                {
                    
                    if (!_battleController.BattleSituations.Contains(ActionConditionEnter[potential]))
                    {
                        isAllGood = false;
                        continue;
                    }
                }
                if (ActionStartTurn.ContainsKey(potential))
                {
                   
                    if (_battleController.CurrentTurn < ActionStartTurn[potential])
                    {
                        isAllGood = false;
                        continue;
                    }
                }
                for (int i = 0; i < MaxActionRepeat[potential]; i++)
                {
                    
                    if (previousActions.Count < i + 1) {break;}
                    if (previousActions[previousActions.Count - 1 - i] == potential)
                    {
                        isAllGood = false;
                        continue;
                    }
                }
                
            } while (!isAllGood);
            return potential;
        }
        public void PlayDeath()
        {
            if (animator != null)
            {
                animator.SetTrigger("Death");
            }
            else
            {
                SelfDestroy();
            }

        }
        public void SelfDestroy()
        {
            Destroy(gameObject);
        }
        
        public void GetDamage(int damage)
        {
            if (damage < 0) { return; }
            _health.Value = _health.Value - damage > 0 ? _health.Value - damage : 0;
        }
        public void GetHeal(int heal)
        {
            if (heal < 0) { return; }
            _health.Value = _health.Value + heal < _maxHealth.Value ? _health.Value + heal : _maxHealth.Value;
        }

        public void IncreaseMaxHealth(int amount)
        {
            if (amount < 0) { return; }
            _maxHealth.Value = _maxHealth.Value + amount < 999 ? _maxHealth.Value + amount : 999;
        }

        public void DecreaseMaxHealth(int amount)
        {
            if (amount < 0) { return; }
            _maxHealth.Value = _maxHealth.Value - amount > 1 ? _maxHealth.Value - amount : 1;
            if (_health.Value > _maxHealth.Value) _health.Value = _maxHealth.Value;
        }
    }
}