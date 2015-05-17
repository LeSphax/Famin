using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ObservableJobs : MonoBehaviour{

    static ObservableJobs instance;
    ObservableDictionary<string, long> jobs;
    Dictionary<string, List<Observer>> observers;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        InitDictionaries();
    }

    public static ObservableJobs GetInstance()
    {
        return instance;
    }

    public void AddObserver(Observer o, string jobToObserve)
    {
        List<Observer> list;
        if (observers.TryGetValue(jobToObserve,out list)){
            list.Add(o);
        }
        else{
            list=new List<Observer>();
            list.Add(o);
            observers.Add(jobToObserve,list);
            jobs.Add(jobToObserve, 0);
        }
        
    }

    void InitDictionaries()
    {
        observers = new Dictionary<string, List<Observer>>();
        jobs = new ObservableDictionary<string, long>(observers);
    }

    public long GetNumberOf(string jobName)
    {
        return jobs[jobName];
    }

    public void SetNumberOf(string jobName, long value)
    {
        jobs[jobName]=value;
    }

    public void Add(string jobName, long value)
    {
        jobs[jobName] += value;
    }
}
