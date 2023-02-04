using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBublik
{
    public class Plot : MonoBehaviour
    {
        
        public void Interact()
        {
            Debug.Log("INTERACT");
            GetComponentInChildren<Plant>().InteractWithPlant(PlayerInventory.Instance.selectedPlant);
        }
    }
}

