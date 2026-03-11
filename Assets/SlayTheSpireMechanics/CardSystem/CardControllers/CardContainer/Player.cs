using Assets.SlayTheSpireMechanics.GameLogic.PlayerSystem;
using SlayTheSpireMechanics.Actions;
using SlayTheSpireMechanics.VisualLogic.ObjectInterfaces;
using System;
using UniRx;
using UnityEngine;

namespace SlayTheSpireMechanics.VisualLogic.CardContainer
{
    public class Player : MonoBehaviour, ITargetable
    {
        [SerializeField] private GameObject healthBarPrefab;
        [SerializeField] private Transform playerWrapper;

        [SerializeField] private int _maxCardsHold;
        public int MaxCardsHold => _maxCardsHold;

        [SerializeField] private ReactiveProperty<int> _maxHealth = new ReactiveProperty<int>(80);
        

        [SerializeField] private int maxMana;
        

        public Inventory Inventory { get; private set; }
        public CardModelContainer CardModelContainer { get; private set; }
        public ManaContainer ManaContainer { get; private set; }



        [SerializeField] private ReactiveProperty<int> _health = new ReactiveProperty<int>(80);
        public IReadOnlyReactiveProperty<int> Health => _health;
        public IReadOnlyReactiveProperty<int> MaxHealth => _maxHealth;

        public void Init()
        {
            Inventory = new Inventory();
            CardModelContainer = new CardModelContainer(MaxCardsHold, this);
            ManaContainer = new ManaContainer(maxMana);
            Debug.Log(playerWrapper.name);
            Debug.Log(healthBarPrefab.name);
            GameObject hb = Instantiate(healthBarPrefab, playerWrapper);
            HealthBarUI healthBar = hb.GetComponentInChildren<HealthBarUI>();
            if (healthBar != null) healthBar.Init(this);

        }
        public void GetDamage(int damage)
        {
            _health.Value = _health.Value - damage > 0 ? _health.Value - damage : 0;
            IAction action = new ReceivedDamageGA(this, damage);
            ActionSystem.Instance.AddActionToBottom(action);
        }
    }
}