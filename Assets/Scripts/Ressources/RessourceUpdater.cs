using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class RessourceUpdater : MonoBehaviour, Observer
{


    IUpdatingStrategy updater;
    Ressources ressources;
    public Text ressourceQuantity;
    public Text ressourceChange;
    public static float updateInterval = 0.25f;

    void Awake()
    {
        updater = this.gameObject.GetComponent<IUpdatingStrategy>();
        ressources = Ressources.GetInstance();
    }

    void Start()
    {
        ressources.AddObserver(this, updater.RessourceName);
        string ressourceNumber = this.GetComponent<Text>().text;
        ressources.Add(updater.RessourceName, Int64.Parse(ressourceNumber));
        InvokeRepeating("RessourceUpdate", updateInterval, updateInterval);
    }

    void RessourceUpdate()
    {
        double recolt = updater.CalculateIncrement();
        ressources.Add(updater.RessourceName, recolt * updateInterval);
        ressourceChange.SendMessage("SetRessourceChange", recolt);

    }

    public void UpdateObserver(object value)
    {
        ressourceQuantity.text = "" + Convert.ToInt64(value);
    }
}
