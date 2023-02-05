using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    public PlantSO plant;
    public Item item;
    public Image icon;
    [SerializeField]
    private TextMeshProUGUI priceText;

    [SerializeField] private bool isSelling;
    [SerializeField] private RectTransform errorPos;

    private void Update()
    {
        SetPrice();
    }

    public void BuyItem()
    {
        FindObjectOfType<AudioManager>().PlaySound("Click");
        bool bought = PlayerInventory.Instance.BuyItem(plant);
        if (!bought)
        {
            PlayerInventory.Instance.Create2DText("You don't have enough coins!", errorPos.position, transform, PlayerInventory.Instance.textData2DDefault);
            FindObjectOfType<AudioManager>().PlaySound("Buy_Fail");
        }
        else
            FindObjectOfType<AudioManager>().PlaySound("Buy_Success");

    }

    public void SellItem()
    {
        FindObjectOfType<AudioManager>().PlaySound("Click");
        bool sold = PlayerInventory.Instance.SellItem(item, 1);
        if (!sold)
        {
            PlayerInventory.Instance.Create2DText("You don't have enough supplies!", errorPos.position, transform, PlayerInventory.Instance.textData2DDefault);
            FindObjectOfType<AudioManager>().PlaySound("Buy_Fail");
        }
        else
            FindObjectOfType<AudioManager>().PlaySound("Buy_Success");

    }

    public void SellAllItems()
    {
        FindObjectOfType<AudioManager>().PlaySound("Click");
        bool sold = PlayerInventory.Instance.SellItem(item, item.amount);
        if (!sold)
        {
            PlayerInventory.Instance.Create2DText("You don't have enough supplies!", errorPos.position, transform, PlayerInventory.Instance.textData2DDefault);
            FindObjectOfType<AudioManager>().PlaySound("Buy_Fail");
        }
        else
            FindObjectOfType<AudioManager>().PlaySound("Buy_Success");
    }

    public void SetPrice()
    {
        if (isSelling)
            priceText.text = item.sellPrice.ToString();
        else if (!isSelling)
            priceText.text = item.price.ToString();

    }
}
