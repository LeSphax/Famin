using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class BuildingNumberObserver : MonoBehaviour, Observer {

    Buildings buildings;
    public Text buildingNameObject;
    public Text buildingNumberObject;
    string buildingName;
    string buildingNumber;

    void Awake()
    {
        buildings = Buildings.GetInstance();
        buildingName = buildingNameObject.text;
        buildingNumber = buildingNumberObject.text;
    }

    public void UpdateObserver(object value)
    {
        buildingNumberObject.text = "" + value;
    }

    void Start()
    {
        buildings.AddObserver(this, buildingName);
        buildings.Add(buildingName, Int64.Parse(buildingNumber));
        buildingNameObject.text = buildingName + " : ";
    }
}
