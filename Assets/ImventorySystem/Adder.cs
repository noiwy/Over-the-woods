using UnityEngine;

public class Adder : MonoBehaviour
{
    public ItemSettings[] itemSettings;
    public InventoryManager inventoryManager;

    void Update()
    {
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0)
            {
                inventoryManager.SelectSlot(number-1);
            }
        }
    }
    public void AddItem(int id)
    {
        bool isAptemptSucsessful = inventoryManager.TryAddItem(itemSettings[id]);
        if (isAptemptSucsessful)
        {
            Debug.Log("ItemAdded");
        }
        else
        {
            Debug.Log("Inventory full");
        }
    }
}
