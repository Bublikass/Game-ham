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
    public SelectedItem selectedPlantVisual;
    public TextMeshProUGUI balanceText;

    public int balance;

    public GameObject interactableUI_GameObject;
    public TextMeshProUGUI UIinteractableText;

    public event EventHandler<OnSelectedPlotChangedEventArgs> OnSelectedPlotChanged;
    public class OnSelectedPlotChangedEventArgs : EventArgs
    {
        public Plot selectedPlot;
    }

    [SerializeField] private GameObject wateringCan;
    [SerializeField] private WateringEffect wateringEffect;

    public GameObject HouseUI;
    public GameObject plotUI;
    public GameObject cartUI;

    public bool houseBuilt;
    public bool cartBuilt;

    public DynamicTextData textDataDefault;
    public DynamicTextData textData2DDefault;

    private void Awake()
    {
        Instance = this;
        inputs = GetComponent<StarterAssetsInputs>();
        inventoryVisual = inventoryObject.GetComponent<InventoryVisual>();
    }

    private void Start()
    {

        foreach (Item seed in seeds)
        {
            seed.amount = 0;
        }

        foreach (Item item in items)
        {
            item.amount = 0;
        }

        balance = 80;
    }

    public void Update()
    {
        if (inputs.inventory)
        {
            inventoryVisual.UpdateItems();
            inventoryObject.SetActive(!inventoryObject.activeSelf);
            if (inventoryObject.activeSelf == false)
                FindObjectOfType<AudioManager>().PlaySound("Bag_Close");
            else
                FindObjectOfType<AudioManager>().PlaySound("Bag_Open");
            inputs.inventory = false;
        }

        CheckForInteractable();

        balanceText.text = "Balance: " + balance.ToString();
    }

    private void LateUpdate()
    {
        inputs.interact = false;
    }

    public void OpenBuildingUI(GameObject UI)
    {
        UI.SetActive(true);
    }

    public void CloseBuildingUI(GameObject UI)
    {
        UI.SetActive(false);
    }

    void CloseAllBuildingUIS()
    {
        HouseUI.SetActive(false);
        plotUI.SetActive(false);
        cartUI.SetActive(false);
    }

    public void DiscountItems()
    {
        foreach (Item item in seeds)
        {
            item.price = Mathf.RoundToInt(item.price * 0.8f);
        }
    }

    public void CreateWorldText(string message, DynamicTextData data)
    {
        DynamicTextManager.CreateText(transform.position + transform.forward * 1f + new Vector3(0,1,0), message, data);
    }

    #region Item Adding/Removing

    public void RemoveBalance(int amount)
    {
        if (balance >= amount)
            balance -= amount;

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
            }
        }
        inventoryVisual.UpdateItems();
    }

    public void RemoveItem(Item item, int amount)
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
            int newAmount = currentItem.amount - amount;
            if (currentItem && newAmount >= 0)
            {
                currentItem.amount -= amount;
            }
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
            int newAmount = currentItem.amount - amount;
            if (currentItem && newAmount >= 0)
            {
                currentItem.amount -= amount;
            }
        }
        inventoryVisual.UpdateItems();
    }

    #endregion

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
                    CloseAllBuildingUIS();
                }
            }
        }
        else
        {
            interactableUI_GameObject.SetActive(false);
            SetPlot(null);
            CloseShop();
            CloseAllBuildingUIS();
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
        selectedPlantVisual.SetItem(plant);
    }

    public bool BuyItem(PlantSO item)
    {
        if (balance >= item.price)
        {
            balance -= item.price;
            AddItem(item, 1);
            return true;
        }
        else
            return false;
    }

    public bool SellItem(Item item, int amount)
    {
        if (item.amount == 0) return false;
        balance += item.sellPrice * amount;
        RemoveItem(item, amount);
        return true;
    }

    public void WaterPlant()
    {
        ThirdPersonController.instance.PlayTargetAnimation("Watering");
        wateringEffect.StartEffect();
        ThirdPersonController.instance.canMove = false;
        wateringCan.SetActive(true);
    }

    void StoppedWatering()
    {
        ThirdPersonController.instance.canMove = true;
        wateringCan.SetActive(false);
    }

    void StopWater()
    {
        wateringEffect.StopEffect();
    }

    public void OpenShop()
    {
        shopObject.SetActive(true);

    }
    public void CloseShop()
    {
        shopObject.SetActive(false);
    }

    public void Create2DText(string message, Vector2 position, Transform parent, DynamicTextData data)
    {
        DynamicTextManager.CreateText2D(position, message, data, parent);
    }
}
