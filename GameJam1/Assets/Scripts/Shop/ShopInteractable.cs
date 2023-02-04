using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteractable : Interactable
{
    public override void Interact()
    {
        PlayerInventory.Instance.OpenShop();
    }
}
