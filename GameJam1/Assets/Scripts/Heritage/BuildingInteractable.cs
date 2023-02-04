using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInteractable : Interactable
{
    [SerializeField] private bool house;
    [SerializeField] private bool cart;
    [SerializeField] private bool plot;

    public override void Interact()
    {
        if (house)
            PlayerInventory.Instance.OpenBuildingUI(PlayerInventory.Instance.HouseUI);
        else if (cart)
            PlayerInventory.Instance.OpenBuildingUI(PlayerInventory.Instance.cartUI);
        else if (plot)
            PlayerInventory.Instance.OpenBuildingUI(PlayerInventory.Instance.plotUI);
    }
}
