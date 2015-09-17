using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopNumberObserver : MonoBehaviour, Observer
{

    Jobs jobs;
    public Text popNumberObject;

    void Awake()
    {
        jobs = Jobs.GetInstance();
    }

    public void UpdateObserver(object value)
    {
        popNumberObject.text = "" + jobs.GetTotalPopulation();
    }

    void Start()
    {
        jobs.AddObserver(this, Jobs.OBSERVE_ALL);
    }
}
