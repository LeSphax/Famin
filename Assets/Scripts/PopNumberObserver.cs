using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopNumberObserver : MonoBehaviour, Observer {

	Buildings buildings;
    Jobs jobs;
    public Text popNumberObject;
    string buildingName;
    string buildingNumber;

    void Awake()
    {
        buildings = Buildings.GetInstance();
        jobs = Jobs.GetInstance();
    }

    public void UpdateObserver(object value)
    {
        popNumberObject.text = jobs.GetTotalPopulation()+ "/" + buildings.GetPopLimit();
    }

    void Start(){
        buildings.AddObserver(this, Buildings.OBSERVE_ALL);
        jobs.AddObserver(this, Jobs.OBSERVE_ALL);
    }
}
