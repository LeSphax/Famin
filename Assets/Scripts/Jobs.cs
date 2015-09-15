using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Jobs
{

    static Jobs instance;
    ObservableDictionary<string, int> jobs;

    Ressources ressources;
    public static string OBSERVE_ALL = "Villagers";

    public static Jobs GetInstance()
    {
        if (instance == null)
        {
            instance = new Jobs();
        }
        return instance;
    }

    private Jobs()
    {
        ressources = Ressources.GetInstance();
        InitDictionaries();
    }

    public void AddObserver(Observer o, string jobToObserve)
    {
        jobs.AddObserver(o, jobToObserve);
        int x;
        if (!jobs.TryGetValue(jobToObserve, out x))
        {
            jobs.Add(jobToObserve, 0);
        }
    }

    void InitDictionaries()
    {
        jobs = new ObservableDictionary<string, int>();
        jobs.OBSERVE_ALL = OBSERVE_ALL;
    }

    public int GetNumberOf(string jobName)
    {
        return jobs[jobName];
    }

    public void SetNumberOf(string jobName, int value)
    {
        jobs[jobName] = value;
    }

    public void Add(string jobName, int value)
    {
        jobs[jobName] += value;
    }

    public int GetTotalPopulation()
    {
        int pop = 0;
        foreach (int job in jobs.GetValues())
        {
            pop += job;
        }
        return pop;
    }

    public int KillPeople(int number)
    {
        int initNumber = number;
        List<string> keys = new List<string>(jobs.GetKeys());
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
                else if (jobs[key] > 0)
                {
                    number -= 1;
                    jobs[key] -= 1;
                    isPeopleLeft = true;
                }
            }
        }
        return initNumber - number;
    }

    public bool ChangeJob(string jobToChange, string jobToGet, int number)
    {

        if (jobs[jobToChange] >= number)
        {
            if (ressources.PayCosts(Data.GetCost(jobToGet), number))
            {
                jobs[jobToChange] -= number;
                jobs[jobToGet] += number;
                return true;
            }
            return false;
        }
        return false;
    }

    public bool ChangeJob(string jobToChange, string jobToGet)
    {
        if (ressources.PayCosts(Data.GetCost(jobToGet), jobs[jobToChange]))
        {
            jobs[jobToGet] += jobs[jobToChange];
            jobs[jobToChange] = 0;
            return true;
        }
        return false;
    }
}
