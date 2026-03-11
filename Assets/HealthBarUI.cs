using SlayTheSpireMechanics.VisualLogic.CardContainer;
using SlayTheSpireMechanics.VisualLogic.Enemies.EnemyActions;
using SlayTheSpireMechanics.VisualLogic.ObjectInterfaces;
using TMPro;
using UniRx;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    
    public ITargetable entity;
    public RectTransform Filler;
    public TMP_Text textMeshPro;
    public RectTransform BG;
    public float width;
    public void Init(ITargetable targetable)
    {
        entity = targetable;
        Debug.Log(Filler.offsetMax);
        entity.Health.CombineLatest(entity.MaxHealth, (h, mh) => new { current = h, max = mh })
        .Subscribe(data => ChangeHealth(data.current, data.max)).AddTo(this);

        Debug.Log(BG.sizeDelta);

        width = BG.sizeDelta.x + Filler.offsetMax.x;
    }

    public void ChangeHealth(int current, int max)
    {
        float currentw = width - (float)current/max * width;
        textMeshPro.text = $"{current} / {max}";
        Debug.Log(currentw);

        Filler.offsetMax = new Vector2(-currentw, Filler.offsetMax.y);

    }
}
