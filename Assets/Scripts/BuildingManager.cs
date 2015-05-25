using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    string nameUnemployed;
    Ressources ressourcesData;
    Buildings buildingsData;
    public Text textObject;
    string buildingName;

    void Awake()
    {
        buildingName = textObject.text;

    }

    void Start()
    {
        ressourcesData = Ressources.GetInstance();
        buildingsData = Buildings.GetInstance();
    }


    public void AddBuilding()
    {
        buildingsData.Add(buildingName, 1);
    }

    public void OnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AddBuilding();
        }
    }
}
