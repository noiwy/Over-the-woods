using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image image;
    public Color normalColor, selectedColor;
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            if (inventoryItem != null) {inventoryItem.futureParent = transform;}
        }
    }
    public void onSelect()
    {
        image.color = selectedColor;
    }
    public void onDeselect()
    {
        image.color = normalColor;
    }
}
