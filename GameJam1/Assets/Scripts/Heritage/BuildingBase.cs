using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] private RectTransform errorPos;

    [SerializeField] private GameObject buildingObject;
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

            repairedBuilding.SetActive(true);
            brokenBuilding.SetActive(false);
            Destroy(buildingObject);
            this.gameObject.SetActive(false);
            FindObjectOfType<AudioManager>().PlaySound("Buy_Success");
            FindObjectOfType<AudioManager>().PlaySound("Build");
        }
        else
        {
            FindObjectOfType<AudioManager>().PlaySound("Buy_Fail");
            PlayerInventory.Instance.Create2DText("You don't have enough resources!", errorPos.position, transform, PlayerInventory.Instance.textData2DDefault);
        }

        // repair effect
    }
}
