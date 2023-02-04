using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectedItem : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI amountText;
    public TextMeshProUGUI itemName;
    private Item currentItem;
    public void SetItem(Item item)
    {
        if (item == null) return;
        currentItem = item;
        image.enabled = true;
        itemName.text = currentItem.itemName;
        amountText.text = currentItem.amount.ToString();
        image.sprite = currentItem.itemIcon;
    }

    private void Update()
    {
        if (currentItem)
        {
            amountText.text = currentItem.amount.ToString();
        }
    }
}
