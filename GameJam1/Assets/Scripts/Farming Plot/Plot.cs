using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBublik
{
    public class Plot : Interactable
    {
        
        public override void Interact()
        {
            Debug.Log("INTERACT");
            GetComponentInChildren<Plant>().InteractWithPlant(PlayerInventory.Instance.selectedPlant);
        }
    }
}

