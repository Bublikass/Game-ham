using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILookAt : MonoBehaviour
{
    public Image image;
    private void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }

    public void ChangeImage(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
