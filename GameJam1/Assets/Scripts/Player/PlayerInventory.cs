using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }
    private StarterAssetsInputs inputs;

    public GameObject inventoryObject;
    private InventoryVisual inventoryVisual;

    public List<PlantSO> seeds = new List<PlantSO>();
    public List<Item> items = new List<Item>();

    public PlantSO selectedPlant;

    public int balance;

    private void Awake()
    {
        Instance = this;
        inputs = GetComponent<StarterAssetsInputs>();
        inventoryVisual = inventoryObject.GetComponent<InventoryVisual>();
    }
    public void Update()
    {
        if (inputs.inventory)
        {
            inventoryVisual.UpdateItems();
            inventoryObject.SetActive(!inventoryObject.activeSelf);
            inputs.inventory = false;
        }
    }

    public void AddItem(Item item, int amount)
    {
        Item currentItem = null;
        if (item.isSeed)
        {
            foreach (Item seed in seeds)
            {
                if (seed.itemName == item.itemName)
                {
                    currentItem = seed;
                    break;
                }
            }
            if (currentItem)
                currentItem.amount += amount;
        }
        else
        {
            foreach (Item thing in items)
            {
                if (thing.itemName == item.itemName)
                {
                    currentItem = thing;
                    break;
                }
            }
            if (currentItem)
            {
                currentItem.amount = currentItem.amount + amount;
                Debug.Log(amount);
            }
        }
        inventoryVisual.UpdateItems();
    }

    public void SelectPlant(PlantSO plant)
    {
        selectedPlant = plant;
    }

    public void BuyItem(PlantSO item)
    {
        if (balance >= item.price)
        {
            balance -= item.price;
            AddItem(item, 1);
        }
    }

    public void SellItem(Item item)
    {
        foreach (Item thing in items)
        {
            if (thing.itemName == item.itemName && thing.amount > 0)
            {
                break;
            }
        }
        balance += item.sellPrice;
        AddItem(item, -1);
    }
}
