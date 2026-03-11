using System.Collections;
using System.Collections.Generic;
using SlayTheSpireMechanics.VisualLogic.CardContainer;
using UniRx;
using UnityEngine;

public class DrawPileCounter : PileCounter
{
    public override void Init(CardModelContainer container)
    {
        base.Init(container);
        container.DrawPile.ObserveCountChanged().Subscribe(Redraw);
    }
}
