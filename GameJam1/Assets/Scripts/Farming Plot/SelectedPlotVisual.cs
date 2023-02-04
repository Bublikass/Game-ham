using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using CodeBublik;

public class SelectedPlotVisual : MonoBehaviour
{
    [SerializeField] private Plot selectedPlot;
    [SerializeField] private GameObject visualGameObject;
    private void Start()
    {
        PlayerInventory.Instance.OnSelectedPlotChanged += OnSelectedPlotChanged;
    }

    private void OnSelectedPlotChanged(object sender, PlayerInventory.OnSelectedPlotChangedEventArgs e)
    {
        if (e.selectedPlot == selectedPlot)
            Show();
        else
            Hide();
    }

    private void Show()
    {
        visualGameObject.SetActive(true);
    }

    private void Hide()
    {
        visualGameObject.SetActive(false);
    }
}
