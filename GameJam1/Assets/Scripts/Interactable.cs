using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Interactable : MonoBehaviour
{
    public float radius;
    public string interactableText;

    public virtual void Interact()
    {

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
