using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryVisual : MonoBehaviour
{
    public Image seedItemsContainer;
    public Image regularItemsContainer;

    public void UpdateItems()
    {
        ItemVisual[] itemsList = regularItemsContainer.GetComponentsInChildren<ItemVisual>();
        ItemVisual[] seedsList = seedItemsContainer.GetComponentsInChildren<ItemVisual>();

        foreach (Item item in PlayerInventory.Instance.seeds)
        {
            UpdateItem(item, seedsList);
        }
        foreach (Item item in PlayerInventory.Instance.items)
        {
            UpdateItem(item, itemsList);
        }
    }

    void UpdateItem(Item itemToUpdate, ItemVisual[] itemsList)
    {

        foreach (ItemVisual itemVisual in itemsList)
        {
            if (itemVisual.item.itemName == itemToUpdate.itemName)
            {
                itemVisual.UpdateItem(itemToUpdate);
            }
        }
    }
}
