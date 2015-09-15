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

    void Awake()
    {
        updater = this.gameObject.GetComponent<IUpdatingStrategy>();
        ressources = Ressources.GetInstance();
        Time.fixedDeltaTime = 0.25f;
    }

    void Start()
    {
        ressources.AddObserver(this, updater.RessourceName);
        string ressourceNumber = this.GetComponent<Text>().text;
        ressources.Add(updater.RessourceName, Int64.Parse(ressourceNumber));
    }

    void FixedUpdate()
    {
        ressources.Add(updater.RessourceName, updater.CalculateIncrement());
        ressourceChange.SendMessage("SetRessourceChange",updater.CalculateIncrement()*4);
    }

    public void OnClick()
    {
        ressources.Add(updater.RessourceName, 1);
    }

    public void UpdateObserver(object value)
    {
        ressourceQuantity.text = "" + Convert.ToInt64(value);
    }
}
