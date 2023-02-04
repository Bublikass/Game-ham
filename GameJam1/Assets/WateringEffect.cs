using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringEffect : MonoBehaviour
{
    [SerializeField] private GameObject waterEffect;
    GameObject currentEffect;
    public void StartEffect()
    {
        currentEffect = Instantiate(waterEffect, transform.position, Quaternion.identity, transform);
    }
    public void StopEffect()
    {
        if (currentEffect)
            Destroy(currentEffect);
    }
}
