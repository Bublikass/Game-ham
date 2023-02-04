using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class priceItem
{
    public Item itemSO;
    public int amount;
}

public class BuildingBase : MonoBehaviour
{
    public priceItem[] priceList;

    public int coinAmount;

    [SerializeField] private GameObject repairedBuilding;
    [SerializeField] private GameObject brokenBuilding;

    public virtual void Repair()
    {
        bool hasResources = false;

        foreach (priceItem item in priceList)
        {
            foreach (Item inventoryItem in PlayerInventory.Instance.items)
            {
                if (inventoryItem.itemName == item.itemSO.itemName)
                {
                    if (inventoryItem.amount >= item.amount)
                    {
                        hasResources = true;
                        PlayerInventory.Instance.RemoveItem(item.itemSO, item.amount);
                    }
                    else
                    {
                        hasResources = false;
                        break;
                    }
                }
            }
        }
        if (PlayerInventory.Instance.balance < coinAmount)
            hasResources = false;

        if (hasResources)
        {
            foreach (priceItem item in priceList)
            {
                PlayerInventory.Instance.RemoveItem(item.itemSO, item.amount);
            }
            PlayerInventory.Instance.RemoveBalance(coinAmount);
        }

        // repair effect
    }
}
