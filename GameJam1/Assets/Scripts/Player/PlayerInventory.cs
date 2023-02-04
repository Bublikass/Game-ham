using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;
using TMPro;
using CodeBublik;
using System;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }
    private StarterAssetsInputs inputs;

    public GameObject inventoryObject;
    private InventoryVisual inventoryVisual;

    public GameObject shopObject;

    public List<PlantSO> seeds = new List<PlantSO>();
    public List<Item> items = new List<Item>();

    public PlantSO selectedPlant;

    public int balance;

    public GameObject interactableUI_GameObject;
    public TextMeshProUGUI UIinteractableText;

    public event EventHandler<OnSelectedPlotChangedEventArgs> OnSelectedPlotChanged;
    public class OnSelectedPlotChangedEventArgs : EventArgs
    {
        public Plot selectedPlot;
    }

    //[SerializeField] private  

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

        CheckForInteractable();
    }

    private void LateUpdate()
    {
        inputs.interact = false;
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

    public void CheckForInteractable()
    {
        Collider[] things = Physics.OverlapSphere(transform.position, 0.3f);
        List<Collider> interactables = new List<Collider>();

        if (things.Length > 0)
        {
            foreach (Collider collider in things)
            {
                if (collider.CompareTag("Interactable"))
                    interactables.Add(collider);
            }
        }

        if (interactables.Count > 0)
        {
            foreach (Collider collider in interactables)
            {
                Interactable interactableObject = collider.GetComponent<Interactable>();
                if (interactableObject)
                {
                    interactableUI_GameObject.SetActive(true);
                    string interactableText = interactableObject.interactableText;
                    UIinteractableText.text = interactableText;
                    Plot plotObject = collider.GetComponent<Plot>();
                    if (plotObject)
                        SetPlot(plotObject);
                    if (inputs.interact)
                    {
                        interactableObject.Interact();
                        break;
                    }
                }
                else
                {
                    SetPlot(null);
                    CloseShop();
                }
            }
        }
        else
        {
            interactableUI_GameObject.SetActive(false);
            SetPlot(null);
            CloseShop();
        }
    }
    void SetPlot(Plot selectedPlot)
    {
        //this.selectedPlot = selectedPlot;

        OnSelectedPlotChanged?.Invoke(this, new OnSelectedPlotChangedEventArgs
        {
            selectedPlot = selectedPlot
        });
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

    public void WaterPlant()
    {
        ThirdPersonController.instance.PlayTargetAnimation("Watering");
        ThirdPersonController.instance.canMove = false;
    }

    void StoppedWatering()
    {
        ThirdPersonController.instance.canMove = true;
    }

    public void OpenShop()
    {
        shopObject.SetActive(true);
    }
    public void CloseShop()
    {
        shopObject.SetActive(false);
    }
}
