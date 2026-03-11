using System;
using System.Collections;
using System.Collections.Generic;
using SlayTheSpireMechanics.VisualLogic.CardContainer;
using UnityEngine;

public class PileCounter : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;
    private CardModelContainer cardModelContainer;

    public virtual void Init(CardModelContainer container)
    {
        text =  GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }

    protected void Redraw(int count)
    {
        Debug.Log("redraw");
        text.text = count.ToString();
    }
}
