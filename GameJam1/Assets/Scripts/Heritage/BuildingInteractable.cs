using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInteractable : Interactable
{
    [SerializeField] private BuildingBase building;

    public override void Interact()
    {
        PlayerInventory.Instance.OpenBuildingUI(PlayerInventory.Instance.HouseUI);
    }
}
