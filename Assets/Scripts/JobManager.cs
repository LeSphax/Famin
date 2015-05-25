using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JobManager : MonoBehaviour
{
    Jobs jobsData;
    Ressources ressourcesData;
    Buildings buildingsData;
    public Text textObject;
    string jobName;

    void Awake()
    {
        jobName = textObject.text;
    }

    void Start()
    {
        jobsData = Jobs.GetInstance();
        ressourcesData = Ressources.GetInstance();
        buildingsData = Buildings.GetInstance();
    }

    public void DeleteWorker()
    {
        if (jobsData.GetNumberOf(jobName) > 0)
        {
            jobsData.Add(jobName, -1);
            jobsData.Add(Data.UNEMPLOYED, 1);
        }
    }

    public void AddWorker()
    {
        if (buildingsData.GetPopLimit() > jobsData.GetTotalPopulation())
        {
            if (jobsData.GetNumberOf(Data.UNEMPLOYED) > 0)
            {
                jobsData.Add(Data.UNEMPLOYED, -1);
                jobsData.Add(jobName, 1);
            }
            else if (ressourcesData.PayCosts(Data.GetCost(jobName)))
            {
                jobsData.Add(jobName, 1);
            }
        }
    }

    public void OnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AddWorker();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            DeleteWorker();
        }
    }
}
