using SlayTheSpireMechanics.VisualLogic.CardContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.SlayTheSpireMechanics.UI.Windows
{
    public class BattleWindow : Window
    {
        [SerializeField] private Player player;
        [SerializeField] private PileCounter[] pileCounters;
        [SerializeField] private ManaWrapper manaWrapper;
        public override void InitElements()
        {
            foreach (PileCounter counter in pileCounters)
            {
                counter.Init(player.CardModelContainer);
            }
            manaWrapper.Init(player.ManaContainer);
        }
    }
}
