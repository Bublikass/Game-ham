using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public PlantSO plant;
    public Item item;
    public Image icon;

    [SerializeField] private RectTransform errorPos;
    public void BuyItem()
    {
        bool bought = PlayerInventory.Instance.BuyItem(plant);
        if (!bought)
            PlayerInventory.Instance.Create2DText("You don't have enough coins!", errorPos.position, transform, PlayerInventory.Instance.textData2DDefault);
    }

    public void SellItem()
    {
        bool sold = PlayerInventory.Instance.SellItem(item, 1);
        if (!sold)
            PlayerInventory.Instance.Create2DText("You don't have enough supplies!", errorPos.position, transform, PlayerInventory.Instance.textData2DDefault);
    }

    public void SellAllItems()
    {
        bool sold = PlayerInventory.Instance.SellItem(item, item.amount);
        if (!sold)
            PlayerInventory.Instance.Create2DText("You don't have enough supplies!", errorPos.position, transform, PlayerInventory.Instance.textData2DDefault);
    }
}
