using ModularMotion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class Plant : MonoBehaviour
{
    [SerializeField] private Transform plantFX;
    [SerializeField] private Transform waterFX;
    [SerializeField] private Transform harvestFX;

    [SerializeField] private Sprite emptySprite;
    [SerializeField] private Sprite growingSprite;
    [SerializeField] private Sprite waterSprite;
    [SerializeField] private Sprite harvestSprite;

    [SerializeField] private UILookAt canvasUI;

    [SerializeField] private DynamicTextData textData;

    int plantState = 0;
    bool isPlanted = false;
    bool isGrowing = false;

    float timerBetweenStages = 2f;
    float timer;

    Transform currentPlant;

    public bool needWater;
    public bool canHarvest;

    //private PlantSO plant;
    private PlantSO plantedPlant;

    public void InteractWithPlant(PlantSO plantSo)
    {
        if (plantSo == null)
        {
            PlayerInventory.Instance.CreateWorldText("You have to select a seed first!", PlayerInventory.Instance.textDataDefault);
            return;
        }
        //plant = plantSo;
        if (canHarvest)
        {
            HarvestPlant();
            return;
        }

        if (!needWater && !isPlanted)
        {
            PlantObject(plantSo);
        }
        else if (needWater)
        {
            WaterPlant();
        }
    }

    public void PlantObject(PlantSO plantSo)
    {
        if (plantSo.amount == 0)
        {
            PlayerInventory.Instance.CreateWorldText("You don't have enough seeds to plant!", PlayerInventory.Instance.textDataDefault);
            return;
        }

        if (currentPlant)
            Destroy(currentPlant.gameObject);

        if (!isPlanted)
        {
            plantSo.amount -= 1;
            plantedPlant = plantSo;
            timerBetweenStages = plantedPlant.stageGrowTime;
            if (PlayerInventory.Instance.houseBuilt)
                timerBetweenStages *= 0.8f;
            currentPlant = Instantiate(plantedPlant.startPlant, transform.position, Quaternion.identity, transform).transform;
            timer = timerBetweenStages;
            isPlanted = true;
            needWater = false;
            canHarvest = false;
            isGrowing = true;
            currentPlant.localPosition = Vector3.zero;
            transform.localPosition = plantedPlant.startPlantPivot;
            LeanTween.scale(currentPlant.gameObject, Vector3.one, timerBetweenStages - 2f);
            Instantiate(plantFX, transform.position, Quaternion.identity);
            canvasUI.ChangeImage(plantedPlant.harvestIcon);
        }
    }

    private void Update()
    {
        if (isPlanted)
        {
            if (!needWater && !canHarvest && isGrowing)
            {
                timer -= Time.deltaTime;
            }

            if (timer <= 0 && plantState < 2 && needWater == false)
            {
                timer = timerBetweenStages;
                plantState++;
                UpdateStage();
            }
        }
    }

    void WaterPlant()
    {
        needWater = false;
        LeanTween.scale(currentPlant.gameObject, Vector3.one, timerBetweenStages - 2f);
        PlayerInventory.Instance.WaterPlant();
        canvasUI.ChangeImage(plantedPlant.harvestIcon);
        Invoke(nameof(StartGrowing), 1f);
    }

    void StartGrowing()
    {
        isGrowing = true;
    }

    void HarvestPlant()
    {
        Debug.Log("HARVEST");
        if (currentPlant)
            Destroy(currentPlant.gameObject);

        isPlanted = false;
        canHarvest = false;
        plantState = 0;
        int amount = Random.Range(1, 5);
        PlayerInventory.Instance.AddItem(plantedPlant.harvestItem, amount);
        canvasUI.ChangeImage(emptySprite);
        Instantiate(harvestFX, transform.position, Quaternion.identity);
        PlayerInventory.Instance.CreateWorldText("+" + amount.ToString() + " " + plantedPlant.harvestItem.itemName, textData);
    }

    void UpdateStage()
    {
        Debug.Log("UPDATING");
        if (currentPlant)
            Destroy(currentPlant.gameObject);

        if (plantState == 1)
        {
            currentPlant = Instantiate(plantedPlant.midPlant, transform.position, Quaternion.identity, transform).transform;
            needWater = true;
            isGrowing = false;
            canHarvest = false;
            transform.localPosition = plantedPlant.midtPlantPivot;
            canvasUI.ChangeImage(waterSprite);
        }
        else if (plantState == 2)
        {
            currentPlant = Instantiate(plantedPlant.endPlant, transform.position, Quaternion.identity, transform).transform;
            needWater = false;
            canHarvest = true;
            isGrowing = false;
            transform.localPosition = plantedPlant.endPlantPivot;
            canvasUI.ChangeImage(harvestSprite);
        }
    }
}
