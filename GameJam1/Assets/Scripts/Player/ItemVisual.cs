using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemVisual : MonoBehaviour
{
    public Item item;
    public PlantSO plant;
    public Image icon;
    public TextMeshProUGUI amountText;


    public void UpdateItem(Item itemToUpdate)
    {
        amountText.text = itemToUpdate.amount.ToString();
    }

    public void SelectItem()
    {
        if (plant != null)
        {
            PlayerInventory.Instance.SelectPlant(plant);
            // effect
        }
    }
}
