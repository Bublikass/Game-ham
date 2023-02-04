using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DynamicTextManager : MonoBehaviour
{

    public static DynamicTextData defaultData;
    public static GameObject canvasPrefab;
    public static GameObject canvasPrefab2D;
    public static Transform mainCamera;

    [SerializeField] private DynamicTextData _defaultData;
    [SerializeField] private GameObject _canvasPrefab;
    [SerializeField] private GameObject _canvasPrefab2D;
    [SerializeField] private Transform _mainCamera;

    private void Awake()
    {
        defaultData = _defaultData;
        mainCamera = _mainCamera;
        canvasPrefab = _canvasPrefab;
        canvasPrefab2D = _canvasPrefab2D;
    }

    public static void CreateText2D(Vector2 position, string text, DynamicTextData data, Transform parent)
    {
        GameObject newText = Instantiate(canvasPrefab2D, position, Quaternion.identity, parent);
        newText.transform.GetComponent<DynamicText2D>().Initialise(text, data);
        Destroy(newText, 2f);
    }

    public static void CreateText(Vector3 position, string text, DynamicTextData data)
    {
        GameObject newText = Instantiate(canvasPrefab, position, Quaternion.identity);
        newText.transform.GetComponent<DynamicText>().Initialise(text, data);
    }

}
