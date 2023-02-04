using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : BuildingBase
{
    public override void Repair()
    {
        base.Repair();
        PlayerInventory.Instance.houseBuilt = true;
    }
}
