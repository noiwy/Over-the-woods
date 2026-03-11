using UnityEngine;

[CreateAssetMenu(fileName = "ItemSettings", menuName = "Scriptable Objects/ItemSettings")]
public class ItemSettings : ScriptableObject
{
    public Sprite sprite;
    public bool isStackable;
}
