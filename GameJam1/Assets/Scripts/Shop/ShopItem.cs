using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public PlantSO plant;
    public Item item;
    public Image icon;

    public void BuyItem()
    {
        PlayerInventory.Instance.BuyItem(plant);
    }

    public void SellItem()
    {
        PlayerInventory.Instance.SellItem(item, 1);
    }

    public void SellAllItems()
    {
        PlayerInventory.Instance.SellItem(item, item.amount);
    }
}
