using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Jobs
{

    static Jobs instance;
    ObservableDictionary<string, long> jobs;

    public static string FARMER = "Farmer";
    public static double COST_PERSON = 20;

    public static string OBSERVE_ALL = "All";

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
        InitDictionaries();
    }

    public void AddObserver(Observer o, string jobToObserve)
    {
        jobs.AddObserver(o, jobToObserve);
        long x;
        if (!jobs.TryGetValue(jobToObserve, out x))
        {
            jobs.Add(jobToObserve, 0);
        }
    }

    void InitDictionaries()
    {
        jobs = new ObservableDictionary<string, long>();
        jobs.OBSERVE_ALL = OBSERVE_ALL;
    }

    public long GetNumberOf(string jobName)
    {
        return jobs[jobName];
    }

    public void SetNumberOf(string jobName, long value)
    {
        jobs[jobName] = value;
    }

    public void Add(string jobName, long value)
    {
        jobs[jobName] += value;
    }

    public long GetTotalPopulation()
    {
        long pop = 0;
        foreach (long job in jobs.GetValues())
        {
            pop += job;
        }
        return pop;
    }

    public void KillPeople(int number)
    {
        List<string> keys=new List<string>(jobs.GetKeys());
        while (number > 0)
        {
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
                }
            }
        }
    }
}
