using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Ennemies
{

    static Ennemies instance;
    ObservableDictionary<string, int> ennemies;
    public static string OBSERVE_ALL = "All";

    public static Ennemies GetInstance()
    {
        if (instance == null)
        {
            instance = new Ennemies();
        }
        return instance;
    }

    private Ennemies()
    {
        InitDictionaries();
    }

    public void AddObserver(Observer o, string jobToObserve)
    {
        ennemies.AddObserver(o, jobToObserve);
        int x;
        if (!ennemies.TryGetValue(jobToObserve, out x))
        {
            ennemies.Add(jobToObserve, 0);
        }
    }

    void InitDictionaries()
    {
        ennemies = new ObservableDictionary<string, int>();
        ennemies.OBSERVE_ALL = OBSERVE_ALL;
    }

    public int GetNumberOf(string jobName)
    {
        return ennemies[jobName];
    }

    public void SetNumberOf(string jobName, int value)
    {
        ennemies[jobName] = value;
    }

    public void Add(string jobName, int value)
    {
        ennemies[jobName] += value;
    }

    public int GetTotalPopulation()
    {
        int pop = 0;
        foreach (int job in ennemies.GetValues())
        {
            pop += job;
        }
        return pop;
    }

    public void KillPeople(int number)
    {
        List<string> keys=new List<string>(ennemies.GetKeys());
        while (number > 0)
        {
            foreach (string key in keys)
            {
                if (number == 0)
                {
                    break;
                }
                else if (ennemies[key] > 0)
                {
                    number -= 1;
                    ennemies[key] -= 1;
                }
            }
        }
    }
}
