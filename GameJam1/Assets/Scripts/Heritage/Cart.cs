using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : BuildingBase
{
    public override void Repair()
    {
        base.Repair();
        PlayerInventory.Instance.cartBuilt = true;
    }
}
