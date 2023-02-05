using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Transform cameraRoot;

    [SerializeField] private CinemachineVirtualCamera camera;

    [SerializeField] private GameObject balance;
    [SerializeField] private GameObject selectedItem;
    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        camera.Follow = cameraRoot;
        gameObject.SetActive(false);
        ThirdPersonController.instance.canMove = true;
        balance.SetActive(true);
        selectedItem.SetActive(true);
    }
}
