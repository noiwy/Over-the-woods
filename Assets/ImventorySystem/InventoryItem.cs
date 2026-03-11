
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public Transform futureParent;
    public ItemSettings item;

    public int count = 1;

    public TMP_Text _countText;
    public Image _image;

    public void OnBeginDrag(PointerEventData eventData)
    {
        futureParent = transform.parent;
        _image.raycastTarget = false;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }

    public void RefreshCount()
    {
        _countText.text = count.ToString();
        _countText.gameObject.SetActive(count != 1);
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        _image.raycastTarget = true;
        transform.SetParent(futureParent);
    }

    public void Init(ItemSettings itemSettings)
    {
        item = itemSettings;
        _image.sprite = itemSettings.sprite;
        RefreshCount();
    }
}
