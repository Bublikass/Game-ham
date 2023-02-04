using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [Header("Item Information")]
    public Sprite itemIcon;
    public string itemName;

    public bool isSeed;
    public int amount;

    public int price;
    public int sellPrice;
}