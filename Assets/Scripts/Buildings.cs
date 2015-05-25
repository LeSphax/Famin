using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Buildings {

    static Buildings instance;
    ObservableDictionary<string, long> buildings;

    public static string HOUSE="Houses";
    public static string OBSERVE_ALL="All";

    public static Buildings GetInstance()
    {
        if (instance == null)
        {
            instance = new Buildings();
        }
        return instance;
    }

    private Buildings()
    {
        InitDictionaries();
    }

    public void AddObserver(Observer o, string buildingToObserve)
    {
        buildings.AddObserver(o, buildingToObserve);
        long x;
        if (!buildings.TryGetValue(buildingToObserve, out x))
        {
            buildings.Add(buildingToObserve, 0);
        }
    }

    void InitDictionaries()
    {
        buildings = new ObservableDictionary<string, long>();
        buildings.OBSERVE_ALL = OBSERVE_ALL;
    }

    public long GetNumberOf(string buildingName)
    {
        return buildings[buildingName];
    }

    public void SetNumberOf(string buildingName, long value)
    {
        buildings[buildingName] = value;
    }

    public void Add(string buildingName, long value)
    {
        buildings[buildingName] += value;
    }

    public long GetPopLimit(){
        return buildings[HOUSE]*5;
    }

}

