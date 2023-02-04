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

    [SerializeField] private Sprite emptySprite;
    [SerializeField] private Sprite growingSprite;
    [SerializeField] private Sprite waterSprite;
    [SerializeField] private Sprite harvestSprite;

    [SerializeField] private UILookAt canvasUI;

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
        if (plantSo == null) return;
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
        if (currentPlant)
            Destroy(currentPlant.gameObject);

        if (!isPlanted)
        {
            plantedPlant = plantSo;
            timerBetweenStages = plantedPlant.stageGrowTime;
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
            canvasUI.ChangeImage(growingSprite);
        }
    }

    private void Update()
    {
        if (isPlanted)
        {
            if (!needWater && !canHarvest)
            {
                isGrowing = true;
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
        isGrowing = true;
        LeanTween.scale(currentPlant.gameObject, Vector3.one, timerBetweenStages - 2f);
        if (currentPlant.Find("ParticlePosition"))
        {
            Instantiate(waterFX, currentPlant.Find("ParticlePosition").position, Quaternion.identity);
        }
        ThirdPersonController.instance.PlayTargetAnimation("Watering");
        ThirdPersonController.instance.canMove = false;
        canvasUI.ChangeImage(growingSprite);
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
        // effect
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
