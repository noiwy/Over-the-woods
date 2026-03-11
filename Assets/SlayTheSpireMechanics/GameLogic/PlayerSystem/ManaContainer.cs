using SlayTheSpireMechanics;
using SlayTheSpireMechanics.Actions;
using SlayTheSpireMechanics.VisualLogic.GameControllers.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
namespace Assets.SlayTheSpireMechanics.GameLogic.PlayerSystem
{
    public class ManaContainer
    {
        private readonly ReactiveProperty<int> _mana = new ReactiveProperty<int>(0);
        public IReadOnlyReactiveProperty<int> Mana => _mana;

        private readonly ReactiveProperty<int> _maxMana = new ReactiveProperty<int>(0);
        public IReadOnlyReactiveProperty<int> MaxMana => _maxMana;




        public void IncreaseMana(int value)
        {
            _mana.Value = Math.Clamp(_mana.Value + value, 0, 9);
        }
        public void DecreaseMana(int value)
        {
            _mana.Value = Math.Clamp(_mana.Value - value, 0, 9);
        }
        public IAction RefillMana(ChangeTurnGA changeTurnGA)
        {
            if (changeTurnGA.GameState == GameStateEnum.PlayerTurn)
                _mana.Value = _maxMana.Value;
            return null;
        }
        public ManaContainer(int max)
        {
            _maxMana.Value = max;
            ActionSystem.Subscribe<ChangeTurnGA>(RefillMana, ReactionTiming.Pre);
        }

    }
}
