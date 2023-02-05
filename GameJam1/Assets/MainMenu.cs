using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Transform cameraRoot;

    [SerializeField] private GameObject camera;

    [SerializeField] private GameObject balance;
    [SerializeField] private GameObject selectedItem;
    [SerializeField] private GameObject backpackIcon;
    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        camera.SetActive(false);
        gameObject.SetActive(false);
        ThirdPersonController.instance.canMove = true;
        balance.SetActive(true);
        selectedItem.SetActive(true);
        backpackIcon.SetActive(true);
        
    }
}
