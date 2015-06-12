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

    public Text[] costs;

    void Awake()
    {
        buildingName = textObject.text;
        Time.fixedDeltaTime = 1;

    }

    void Start()
    {
        ressourcesData = Ressources.GetInstance();
        buildingsData = Buildings.GetInstance();
    }


    public void AddBuilding()
    {
        if (ressourcesData.PayCosts(Data.GetCost(buildingName)))
        buildingsData.Add(buildingName, 1);
    }

    public void OnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AddBuilding();
        }
    }

    void FixedUpdate()
    {
        if (buildingsData.GetNumberOf(buildingName) == 0)
        {
            Application.LoadLevel("Losing");
        }
    }

    private void ComputeCost()
    {

    }
}
