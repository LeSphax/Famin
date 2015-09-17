using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Buildings {

    static Buildings instance;
    ObservableDictionary<string, int> buildings;

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
        int x;
        if (!buildings.TryGetValue(buildingToObserve, out x))
        {
            buildings.Add(buildingToObserve, 0);
        }
    }

    void InitDictionaries()
    {
        buildings = new ObservableDictionary<string, int>();
        buildings.OBSERVE_ALL = OBSERVE_ALL;
    }

    public int GetNumberOf(string buildingName)
    {
        return buildings[buildingName];
    }

    public void SetNumberOf(string buildingName, int value)
    {
        buildings[buildingName] = value;
    }

    public void Add(string buildingName, int value)
    {
        buildings[buildingName] += value;
    }

    public int GetPopLimit(){
        return buildings[HOUSE]*5;
    }

    public int Burn(int number)
    {
        int initNumber = number;
        List<string> keys = new List<string>(buildings.GetKeys());
        bool isPeopleLeft = true;
        while (number > 0 && isPeopleLeft)
        {
            isPeopleLeft = false;
            foreach (string key in keys)
            {
                if (number == 0)
                {
                    break;
                }
                else if (buildings[key] > 0)
                {
                    number -= 1;
                    buildings[key] -= 1;
                    isPeopleLeft = true;
                }
            }
        }
        return initNumber - number;
    }

}

