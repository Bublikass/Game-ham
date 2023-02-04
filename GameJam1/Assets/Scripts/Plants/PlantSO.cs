using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Plant Item")]
public class PlantSO : Item
{
    public GameObject startPlant;
    public GameObject midPlant;
    public GameObject endPlant;

    public float stageGrowTime;

    public Item harvestItem;

    public Vector3 startPlantPivot;
    public Vector3 midtPlantPivot;
    public Vector3 endPlantPivot;
}
