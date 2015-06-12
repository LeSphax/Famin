using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JobManager : MonoBehaviour
{
    Jobs jobsData;
    Ressources ressourcesData;
    Buildings buildingsData;
    Logger logger;
    public Text textObject;
    string jobName;

    void Awake()
    {
        jobName = textObject.text;
        logger = Logger.GetInstance();
    }

    void Start()
    {
        jobsData = Jobs.GetInstance();
        ressourcesData = Ressources.GetInstance();
        buildingsData = Buildings.GetInstance();
    }

    public void DeleteWorker()
    {
        jobsData.ChangeJob(jobName, Data.UNEMPLOYED, 1);
    }

    public void AddWorker()
    {
        if (jobsData.GetNumberOf(Data.UNEMPLOYED) > 0)
        {
            jobsData.ChangeJob(Data.UNEMPLOYED, jobName, 1);
        }
        else if (buildingsData.GetPopLimit() > jobsData.GetTotalPopulation())
        {
            if (ressourcesData.PayCosts(Data.GetCost(jobName)))
            {
                if (ressourcesData.PayCosts(Data.GetCost(Data.PERSON)))
                {
                    jobsData.Add(jobName, 1);
                }
                else
                {
                    ressourcesData.RefundCost(Data.GetCost(jobName));
                }

            }
        }
        else
        {
            logger.PutLine("Build more ziggourats");
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
