using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UniRx;
using Assets.SlayTheSpireMechanics.GameLogic.PlayerSystem;

namespace Assets.SlayTheSpireMechanics.UI
{
    public class ManaWrapper : MonoBehaviour
    {
        
        public TMPro.TextMeshProUGUI text;
        private ManaContainer _manaContainer;
        public void Init(ManaContainer mc)
        {
            text =  GetComponentInChildren<TMPro.TextMeshProUGUI>();
            _manaContainer = mc;

            var sub = Observable.CombineLatest(_manaContainer.Mana,
            _manaContainer.MaxMana,
            (current, max) => new { current, max }).Subscribe(data => Redraw(data.current, data.max)).AddTo(this);
        }

        public void Redraw(int current, int max)
        {
            text.text = $"{current}/{max}";
        }


    }
}
