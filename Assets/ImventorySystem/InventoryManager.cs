using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int maxHoldItems;
    public InventorySlot[] inventorySlots;
    public GameObject blankItem;
    public InventoryGenerator inventoryGenerator;
    public InventorySlot selectedSlot;
    public int selectedSlotID = -1;
    void Awake()
    {
        inventoryGenerator.onGenerationComplete += a => inventorySlots = a;
    }

    public void SelectSlot(int id)
    {
        if (selectedSlot != null)
        selectedSlot.onDeselect();

        if (id >= 0 && id <= inventorySlots.Length)
        {
            selectedSlot = inventorySlots[id];
            selectedSlot.onSelect();
            selectedSlotID = id;
        }
    }
    public bool TryAddItem(ItemSettings item)
    {
        foreach (InventorySlot inventorySlot in inventorySlots)
        {
            InventoryItem inventoryItem = inventorySlot.GetComponentInChildren<InventoryItem>();
            if (inventoryItem?.item == item && item.isStackable && inventoryItem.count < maxHoldItems)
            {
                inventoryItem.count++;
                inventoryItem.RefreshCount();
                return true;
            }
        }


        foreach (InventorySlot inventorySlot in inventorySlots)
        {
            InventoryItem inventoryItem = inventorySlot.GetComponentInChildren<InventoryItem>();
            if (inventoryItem == null)
            {
                AddItem(item, inventorySlot);
                return true;
            }
        }
        return false;
    }

    void AddItem(ItemSettings itemSettings, InventorySlot inventorySlot)
    {
        GameObject gameObject = Instantiate(blankItem, inventorySlot.transform);
        gameObject.GetComponent<InventoryItem>().Init(itemSettings);
    }
}
