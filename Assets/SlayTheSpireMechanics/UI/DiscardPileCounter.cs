using System.Collections;
using System.Collections.Generic;
using SlayTheSpireMechanics.VisualLogic.CardContainer;
using UnityEngine;
using UniRx;

public class DiscardPileCounter : PileCounter
{
    public override void Init(CardModelContainer container)
    {
        base.Init(container);
        container.DiscardPile.ObserveCountChanged().Subscribe(Redraw);
    }
}
