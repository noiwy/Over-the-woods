using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class InventoryGenerator : MonoBehaviour
{
    public int columnCount;
    public int rowCount;
    public int slotSize;
    public int stepX;
    public int stepY;
    public int framestepY;
    public int framestepX;

    [SerializeField] private HorizontalPlacement horizontalPlacement;
    [SerializeField] private VerticalPlacement verticalPlacement;
    
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();
    public event Action<InventorySlot[]> onGenerationComplete;
     

    public GameObject GridPrefab;
    public GameObject BackPrefab;
    public GameObject SlotPrefab;
    public GridLayoutGroup _gridLayoutGroup;
    public RectTransform _gridRectTransform;
    public RectTransform _backRectTransform;

    [ContextMenu("aboba")]
    public void CreateGrid()
    {
        int prefWidth = columnCount * slotSize + (columnCount - 1) * stepX + framestepX * 2;
        int prefHeight = rowCount * slotSize + (rowCount - 1) * stepY + framestepY * 2;

        GameObject myBack = Instantiate(BackPrefab,transform);
        _backRectTransform = myBack.GetComponent<RectTransform>();
        _backRectTransform.sizeDelta = new Vector2(prefWidth, prefHeight);
        _backRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        _backRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        _backRectTransform.anchoredPosition = new Vector2(0,0);

        GameObject myGrid = Instantiate(GridPrefab, myBack.transform);
        _gridRectTransform = myGrid.GetComponent<RectTransform>();
        _gridLayoutGroup = myGrid.GetComponent<GridLayoutGroup>();

        _gridRectTransform.anchorMin = new Vector2(0,0);
        _gridRectTransform.anchorMax = new Vector2(1,1);
        _gridRectTransform.offsetMin = new Vector2(framestepX, framestepY);
        _gridRectTransform.offsetMax = new Vector2(-framestepX, -framestepY);

        _gridLayoutGroup.cellSize = Vector2.one * slotSize;
        _gridLayoutGroup.spacing = new Vector2(stepX, stepY);
        
        for (int i = 0; i < columnCount * rowCount; i++)
        {
            GameObject mySlot = Instantiate(SlotPrefab, _gridRectTransform);
            inventorySlots.Add(mySlot.GetComponent<InventorySlot>());
        }
        onGenerationComplete?.Invoke(inventorySlots.ToArray());
    }
    enum HorizontalPlacement
    {
    Left,
    Center,
    Right
    }
    enum VerticalPlacement
    {
        Top,
        Down
    }
}

